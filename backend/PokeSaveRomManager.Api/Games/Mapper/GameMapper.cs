using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Mapper
{
    public static class GameMapper
    {
        // Game
        #region Game
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
        #endregion

        // GameType
        #region Type
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
        #endregion

        // GameAbility
        #region Ability
        public static GameAbilityDto ToDto(this GameAbility gameAbility)
        {
            if (gameAbility == null)
                return null;

            return new GameAbilityDto
            {
                Id = gameAbility.Id,
                AbilityName = gameAbility.Ability.Name.Name,
                GameName = gameAbility.Game.Name,
                AbilityInGameId = gameAbility.AbilityInGameId
            };
        }

        public static IEnumerable<GameAbilityDto> ToDtos(this IEnumerable<GameAbility> gameAbilities)
        {
            if (gameAbilities == null)
                return new List<GameAbilityDto>();

            return gameAbilities.Select(g => g.ToDto());
        }

        public static GameAbility ToEntity(this GameAbilityCreateDto gameAbilityDto)
        {
            if (gameAbilityDto == null)
                return null;

            return new GameAbility
            {
                AbilityId = gameAbilityDto.AbilityId,
                GameId = gameAbilityDto.GameId,
                AbilityInGameId = gameAbilityDto.AbilityInGameId
            };
        }

        public static void UpdateFromDto(this GameAbility gameAbility, GameAbilityUpdateDto gameAbilityDto)
        {
            if (gameAbility == null || gameAbilityDto == null)
                return;

            gameAbility.AbilityId = gameAbilityDto.AbilityId;
            gameAbility.GameId = gameAbilityDto.GameId;
            gameAbility.AbilityInGameId = gameAbilityDto.AbilityInGameId;
        }
        #endregion

        // GameMove
        #region Move
        public static GameMoveDto ToDto(this GameMove gameMove)
        {
            if (gameMove == null)
                return null;

            return new GameMoveDto
            {
                Id = gameMove.Id,
                MoveName = gameMove.Move.Name.Name,
                GameName = gameMove.Game.Name,
                MoveInGameId = gameMove.MoveInGameId
            };
        }

        public static IEnumerable<GameMoveDto> ToDtos(this IEnumerable<GameMove> gameMoves)
        {
            if (gameMoves == null)
                return new List<GameMoveDto>();

            return gameMoves.Select(g => g.ToDto());
        }

        public static GameMove ToEntity(this GameMoveCreateDto gameMoveDto)
        {
            if (gameMoveDto == null)
                return null;

            return new GameMove
            {
                MoveId = gameMoveDto.MoveId,
                GameId = gameMoveDto.GameId,
                MoveInGameId = gameMoveDto.MoveInGameId
            };
        }

        public static void UpdateFromDto(this GameMove gameMove, GameMoveUpdateDto gameMoveDto)
        {
            if (gameMove == null || gameMoveDto == null)
                return;

            gameMove.MoveId = gameMoveDto.MoveId;
            gameMove.GameId = gameMoveDto.GameId;
            gameMove.MoveInGameId = gameMoveDto.MoveInGameId;
        }
        #endregion
    }
}
