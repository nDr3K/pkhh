using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Api.Games.Services;
using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Services;
using PokeSaveRomManager.Api.Saves.Mapper;
using PokeSaveRomManager.Api.Saves.Models;
using PokeSaveRomManager.Api.Saves.Repositories;
using PokeSaveRomManager.Data.Domain;
using PokeSaveRomManager.Parser.Core.Models;
using PokeSaveRomManager.Parser.Core.Models.Data;

namespace PokeSaveRomManager.Api.Saves.Services.Handler
{
    public class SaveServiceHandler : ISaveServiceHandler
    {
        private readonly IGameMoveService _gameMoveService;
        private readonly IPokemonService _pokemonService;
        private readonly IPokemonInstanceRepository _pokemonInstanceRepository;
        private readonly IPartyRepository _partyRepository;
        private readonly IBoxRepository _boxRepository;

        public SaveServiceHandler(IGameMoveService gameMoveService, IPokemonService pokemonService, IPokemonInstanceRepository pokemonInstanceRepository, IPartyRepository partyRepository, IBoxRepository boxRepository)
        {
            _gameMoveService = gameMoveService;
            _pokemonService = pokemonService;
            _pokemonInstanceRepository = pokemonInstanceRepository;
            _partyRepository = partyRepository;
            _boxRepository = boxRepository;
        }

        public async Task<SaveFileData> GetDatas(ParsedSaveData saveData, int gameId)
        {
            var allPokemon = saveData.Party.Concat(saveData.Boxes.SelectMany(b => b.Pokemon)).ToList();

            var allPokemonIds = allPokemon.Select(p => p.PokemonId).Distinct().Cast<int?>().ToList();
            var pokemons = (await _pokemonService.GetForGameByIds(gameId, allPokemonIds))
                .ToDictionary(p => p.PokemonId);

            var allMoveIds = allPokemon
                .SelectMany(p => new[] {
                    p.Move1.MoveId,
                    p.Move2.MoveId,
                    p.Move3.MoveId,
                    p.Move4.MoveId
                })
                .Distinct()
                .ToList();
            var moves = (await _gameMoveService.GetForGameByIds(gameId, allMoveIds))
                .ToDictionary(m => m.MoveInGameId);

            // Process Pokemon data
            return new SaveFileData()
            {
                Party = saveData.Party.Select(p => CreatePokemonData(p, pokemons, moves)).ToList(),
                Boxes = saveData.Boxes
                    .SelectMany(b => b.Pokemon)
                    .Select(p => CreatePokemonData(p, pokemons, moves))
                    .ToList()
            };
        }

        private PokemonData CreatePokemonData(PokemonSaveData pokemonSaveData, Dictionary<int, PokemonFormDto> pokemonFormMap, Dictionary<int, GameMoveDto> moveMap)
        {
            if (!pokemonFormMap.TryGetValue(pokemonSaveData.PokemonId, out var formData))
                throw new Exception($"Pokemon with InternalId {pokemonSaveData.PokemonId} not found");

            return new PokemonData()
            {
                PokemonId = formData.PokemonId,
                FormId = formData.Id,
                AbilityId = null,
                Move1Id = moveMap.TryGetValue(pokemonSaveData.Move1.MoveId, out var move1) ? move1.Id : null,
                Move2Id = moveMap.TryGetValue(pokemonSaveData.Move2.MoveId, out var move2) ? move2.Id : null,
                Move3Id = moveMap.TryGetValue(pokemonSaveData.Move3.MoveId, out var move3) ? move3.Id : null,
                Move4Id = moveMap.TryGetValue(pokemonSaveData.Move4.MoveId, out var move4) ? move4.Id : null,
                Level = pokemonSaveData.Level,
                HPEV = pokemonSaveData.HPEV,
                AttackEV = pokemonSaveData.AttackEV,
                DefenseEV = pokemonSaveData.DefenseEV,
                SpeedEV = pokemonSaveData.SpeedEV,
                SpecialEV = pokemonSaveData.SpecialEV,
                SpecialAttackEV = pokemonSaveData.SpecialAttackEV,
                SpecialDefenseEV = pokemonSaveData.SpecialDefenseEV,
                HPIV = pokemonSaveData.HPIV,
                AttackIV = pokemonSaveData.AttackIV,
                DefenseIV = pokemonSaveData.DefenseIV,
                SpeedIV = pokemonSaveData.SpeedIV,
                SpecialIV = pokemonSaveData.SpecialIV,
                SpecialAttackIV = pokemonSaveData.SpecialAttackIV,
                SpecialDefenseIV = pokemonSaveData.SpecialDefenseIV,
                MaxHp = pokemonSaveData.MaxHp,
                Attack = pokemonSaveData.Attack,
                Defense = pokemonSaveData.Defense,
                Speed = pokemonSaveData.Speed,
                Special = pokemonSaveData.Special,
                SpecialAttack = pokemonSaveData.SpecialAttack,
                SpecialDefense = pokemonSaveData.SpecialDefense
            };
        }

        public async Task DeletePokemonInstances(int saveId)
        {
            await _pokemonInstanceRepository.DeletePokemons(saveId);
        }

        public async Task DeleteParty(int partyId)
        {
            await _partyRepository.DeleteParty(partyId);
        }

        public async Task DeleteBox(int boxId)
        {
            await _boxRepository.Delete(boxId);
        }

        public async Task<SaveBox> CreateBox(SaveBox saveBox)
        {
            return await _boxRepository.Create(saveBox);
        }

        public async Task<IEnumerable<PokemonInstance>> SavePokemonInstances(IEnumerable<PokemonData> pokemonData, int saveId)
        {
            var pokemonInstances = pokemonData.ToDomain(saveId);
            return await _pokemonInstanceRepository.SavePokemonInstances(pokemonInstances);
        }
    }
}
