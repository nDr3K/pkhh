using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Services;
using PokeSaveRomManager.Api.Moves.DTOs;
using PokeSaveRomManager.Api.Moves.Services;
using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Services;
using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Api.Roms.Mapper;
using PokeSaveRomManager.Api.Roms.Repositories;
using PokeSaveRomManager.Api.Types.Services;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;
using System.Linq;
using MoveName = PokeSaveRomManager.Parser.Core.Models.Data.MoveName;

namespace PokeSaveRomManager.Api.Roms.Services.Handler
{
    public class RomServiceHandler : IRomServiceHandler
    {
        private readonly IGameService _gameService;
        private readonly ITypeService _typeService;
        private readonly IGameTypeService _gameTypeService;
        private readonly IMoveService _moveService;
        private readonly IGameMoveService _gameMoveService;
        private readonly IPokemonService _pokemonService;
        private readonly IPokemonFormService _pokemonFormService;
        private readonly IRomRepository _romRepository;

        public RomServiceHandler(
            IGameService gameService, 
            ITypeService typeService, 
            IGameTypeService gameTypeService, 
            IMoveService moveService, 
            IGameMoveService gameMoveService, 
            IPokemonService pokemonService,
            IPokemonFormService pokemonFormService,
            IRomRepository romRepository
        )
        {
            _gameService = gameService;
            _typeService = typeService;
            _gameTypeService = gameTypeService;
            _moveService = moveService;
            _gameMoveService = gameMoveService;
            _pokemonService = pokemonService;
            _pokemonFormService = pokemonFormService;
            _romRepository = romRepository;
        }

        public async Task RegisterRomDataAsync(RomSaveDto dto, ParsedRomData data)
        {
            using var transaction = await _romRepository.BeginTransactionAsync(); // Start transaction
            try
            {

                var gameId = await CreateGameAsync(dto);

                await AddTypesAsync(data.Types, gameId);
                await ProcessMovesAsync(data.Moves, data.MoveNames, gameId);
                await ProcessPokemonAsync(data.PokemonStats, data.PokemonNames, gameId);

                await transaction.CommitAsync(); // Commit transaction
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback transaction on error
                throw new Exception("An error occurred while processing the ROM data.", ex);
            }
        }

        private async Task<int> CreateGameAsync(RomSaveDto dto)
        {
            var gameDto = dto.MapToGame();
            var game = await _gameService.AddGameAsync(gameDto);
            return game.Id;
        }

        private async Task AddTypesAsync(IEnumerable<TypeData> typesData, int gameId)
        {
            var typesDto = await _typeService.GetAllAsync();
            var gameTypes = typesData.MapToGameTypes(typesDto);
            await _gameTypeService.AddRangeAsync(gameId, gameTypes);
        }

        private async Task ProcessMovesAsync(IEnumerable<MoveData> movesData, IEnumerable<MoveName> moveNames, int gameId)
        {
            var existingMoves = await _moveService.GetAllAsync();
            var existingNames = await _moveService.GetAllNamesAsync();
            var gameTypes = await _gameTypeService.GetAllAsync(gameId);
            var typeMap = gameTypes.ToDictionary(t => t.TypeInGameId, t => t.TypeId); //To retrieve the typeId from the gameTypeId for the moves

            // Resolve and Insert Missing MoveNames
            var nameLookup = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var newNames = new List<MoveNameCreateDto>();

            foreach (var name in moveNames)
            {
                if (!existingNames.Any(n => n.Name.Equals(name.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    newNames.Add(new MoveNameCreateDto { Name = name.Name });
                }
                else
                {
                    var existing = existingNames.First(n => n.Name.Equals(name.Name, StringComparison.OrdinalIgnoreCase));
                    nameLookup[existing.Name] = existing.Id;
                }
            }

            if (newNames.Count != 0)
            {
                var addedNames = await _moveService.AddNameRangeAsync(newNames); // AddRangeAsync
                foreach (var added in addedNames)
                {
                    nameLookup[added.Name] = added.Id;
                }
            }

            // Identify and Insert Missing Moves
            var newMoves = new List<MoveCreateDto>();
            var gameMoves = new List<GameMoveCreateDto>();

            foreach (var move in movesData)
            {
                var moveName = moveNames.First(mn => mn.Id == move.Id);
                var nameId = nameLookup[moveName.Name];
                var typeId = typeMap[move.Type];
                if (!existingMoves.Any(m =>
                    m.NameId == nameId &&
                    m.TypeId == typeId &&
                    m.Power == move.Power &&
                    m.Accuracy == move.Accuracy &&
                    m.PP == move.PP))
                {
                    newMoves.Add(new MoveCreateDto
                    {
                        CategoryId = move.Category.MapToCategoryId(),
                        NameId = nameId,
                        TypeId = typeId,
                        Power = move.Power,
                        Accuracy = move.Accuracy,
                        PP = move.PP,
                        Effect = "",
                        Priority = move.Priority,
                    });
                }
            }

            var insertedMoves = new List<MoveDto>();

            if (newMoves.Count != 0)
            {
                insertedMoves = [.. (await _moveService.AddRangeAsync(newMoves))];
            }

            // Map new moves by (NameId, TypeId, Power, Accuracy, PP)
            var insertedMoveLookup = insertedMoves.ToDictionary(
                m => (m.NameId, m.TypeId, m.Power, m.Accuracy, m.PP),
                m => m.Id
            );

            // Now build GameMoveCreateDto
            foreach (var move in movesData)
            {
                var moveName = moveNames.First(mn => mn.Id == move.Id);
                var nameId = nameLookup[moveName.Name];
                var typeId = typeMap[move.Type];

                var existingMove = existingMoves.FirstOrDefault(m =>
                    m.NameId == nameId &&
                    m.TypeId == typeId &&
                    m.Power == move.Power &&
                    m.Accuracy == move.Accuracy &&
                    m.PP == move.PP
                );

                int moveId;
                if (existingMove != null)
                {
                    moveId = existingMove.Id;
                }
                else
                {
                    moveId = insertedMoveLookup[(nameId, typeId, move.Power, move.Accuracy, move.PP)];
                }

                gameMoves.Add(new GameMoveCreateDto
                {
                    MoveId = moveId,
                    MoveInGameId = move.Id
                });
            }

            // Insert GameMoves
            if (gameMoves.Count != 0)
                await _gameMoveService.AddRangeAsync(gameId, gameMoves);
        }

        private async Task ProcessPokemonAsync(IEnumerable<PokemonStatsData> statsData, IEnumerable<PokemonNameData> nameData, int gameId)
        {
            var gameTypes = await _gameTypeService.GetAllAsync(gameId);
            var typeMap = gameTypes.ToDictionary(t => t.TypeInGameId, t => t.TypeId); //To retrieve the typeId from the gameTypeId

            // Combine name and stats by InternalId
            // PartialPokemonData is used for entities such as Egg
            var (pokemonData, partialPokemonData) = ParserDataMapper.MapPokemonData(statsData, nameData);

            // Create Pokemon entities
            var newPokemons = pokemonData.Concat(partialPokemonData).Select(p => new PokemonCreateDto
            {
                DexNumber = p.DexNumber ?? throw new Exception($"Missing Dex number for {p.Name}"),
                Name = p.Name,
                GameId = gameId
            }).ToList();

            var addedPokemons = await _pokemonService.AddRangeAsync(newPokemons);

            // Create Forms
            var newForms = pokemonData.Select(p =>
            {
                var pokemonId = addedPokemons
                    .First(pk => pk.DexNumber == p.DexNumber && pk.Name == p.Name).Id;

                return new PokemonFormCreateDto
                {
                    PokemonId = pokemonId,
                    InternalId = p.InternalId,
                    Name = "Base",
                    HP = p.BaseHP,
                    Attack = p.BaseAttack,
                    Defense = p.BaseDefense,
                    Special = p.BaseSpecial,
                    SpAttack = p.BaseSpAttack,
                    SpDefense = p.BaseSpDefense,
                    Speed = p.BaseSpeed,
                    Type1Id = typeMap.TryGetValue(p.Type1Id, out var t1) ? t1 : throw new Exception($"Unknown Type1Id {p.Type1Id}"),
                    Type2Id = p.Type2Id != 0 ? (typeMap.TryGetValue(p.Type2Id, out var t2) ? t2 : null) : null,
                    IsDefault = true
                };
            }).ToList();

            foreach (var partial in partialPokemonData)
            {
                var pokemonId = addedPokemons
                    .First(pk => pk.DexNumber == partial.DexNumber && pk.Name == partial.Name).Id;

                newForms.Add(new PokemonFormCreateDto
                {
                    PokemonId = pokemonId,
                    InternalId = partial.InternalId,
                    Name = "Egg", //TODO() maybe later will have more forms
                    HP = 0,
                    Attack = 0,
                    Defense = 0,
                    Special = 0,
                    SpAttack = 0,
                    SpDefense = 0,
                    Speed = 0,
                    Type1Id = typeMap.Last().Value,
                    Type2Id = null,
                    IsDefault = true
                });
            }

            await _pokemonFormService.AddRangeAsync(newForms);
        }
    }
}
