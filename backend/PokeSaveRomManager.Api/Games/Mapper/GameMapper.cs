using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Data.Domain;
using System.Security.AccessControl;
using static PokeSaveRomManager.Api.Shared.Constants.ApiRoutes;

namespace PokeSaveRomManager.Api.Games.Mapper
{
    public static class GameMapper
    {
        // Game
        public static GameDto ToDto(this Game game)
        {
            if (game == null)
                return null;

            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Generation = game.Generation,
                Official = game.Official,
                Region = game.Region,
                PokemonIds = game.Pokemon?.Select(p => p.Id).ToList() ?? [],
                TeamIds = game.Teams?.Select(t => t.Id).ToList() ?? [],
                BoxIds = game.Boxes?.Select(b => b.Id).ToList() ?? [],
                MoveGameIds = game.Moves?.Select(m => m.Id).ToList() ?? [],
                TypeGameIds = game.Types?.Select(t => t.Id).ToList() ?? [],
                AbilityGameIds = game.Abilities?.Select(a => a.Id).ToList() ?? []
            };
        }

        public static IEnumerable<GameDto> ToDtos(this IEnumerable<Game> games)
        {
            if (games == null)
                return new List<GameDto>();

            return games.Select(g => g.ToDto());
        }

        public static Game ToEntity(this GameCreateDto gameDto)
        {
            if (gameDto == null)
                return null;

            return new Game
            {
                Name = gameDto.Name,
                Generation = gameDto.Generation,
                Official = gameDto.Official,
                Region = gameDto.Region
            };
        }

        public static void UpdateFromDto(this Game game, GameUpdateDto gameDto)
        {
            if (game == null || gameDto == null)
                return;

            game.Name = gameDto.Name;
            game.Generation = gameDto.Generation;
            game.Official = gameDto.Official;
            game.Region = gameDto.Region;
        }


        // GameType
        public static GameTypeDto ToDto(this GameType gameType)
        {
            if (gameType == null)
                return null;

            return new GameTypeDto
            {
                Id = gameType.Id,
                TypeName = gameType.Type.Name,
                GameName = gameType.Game.Name,
                TypeInGameId = gameType.TypeInGameId
            };
        }

        public static IEnumerable<GameTypeDto> ToDtos(this IEnumerable<GameType> gameTypes)
        {
            if (gameTypes == null)
                return new List<GameTypeDto>();

            return gameTypes.Select(g => g.ToDto());
        }

        public static GameType ToEntity(this GameTypeCreateDto gameTypeDto)
        {
            if (gameTypeDto == null)
                return null;
            return new GameType
            {
                TypeId = gameTypeDto.TypeId,
                GameId = gameTypeDto.GameId,
                TypeInGameId = gameTypeDto.TypeInGameId
            };
        }

        public static void UpdateFromDto(this GameType gameType, GameTypeUpdateDto gameTypeDto)
        {
            if (gameType == null || gameTypeDto == null)
                return;

            gameType.TypeId = gameTypeDto.TypeId;
            gameType.GameId = gameTypeDto.GameId;
            gameType.TypeInGameId = gameTypeDto.TypeInGameId;
        }
    }
}
