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
                Path = game.Path,
                Generation = game.Generation,
                Official = game.Official,
                Region = game.Region,
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
                Path = gameDto.Path,
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
            game.Path = gameDto.Path;
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
                TypeId = gameType.TypeId,
                TypeName = gameType.Type.Name,
                TypeInGameId = gameType.TypeInGameId
            };
        }

        public static IEnumerable<GameTypeDto> ToDtos(this IEnumerable<GameType> gameTypes)
        {
            if (gameTypes == null)
                return new List<GameTypeDto>();

            return gameTypes.Select(g => g.ToDto());
        }

        public static GameType ToEntity(int gameId, GameTypeCreateDto gameTypeDto)
        {
            if (gameTypeDto == null)
                return null;

            return new GameType
            {
                TypeId = gameTypeDto.TypeId,
                GameId = gameId,
                TypeInGameId = gameTypeDto.TypeInGameId
            };
        }

        public static IEnumerable<GameType> ToEntities(int gameId, IEnumerable<GameTypeCreateDto> gameTypeDtos)
        {
            if (gameTypeDtos == null)
                return new List<GameType>();

            return gameTypeDtos.Select(g => ToEntity(gameId, g));
        }

        public static void UpdateFromDto(this GameType gameType, int gameId, GameTypeUpdateDto gameTypeDto)
        {
            if (gameType == null || gameTypeDto == null)
                return;

            gameType.TypeId = gameTypeDto.TypeId;
            gameType.GameId = gameId;
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
                AbilityInGameId = gameAbility.AbilityInGameId
            };
        }

        public static IEnumerable<GameAbilityDto> ToDtos(this IEnumerable<GameAbility> gameAbilities)
        {
            if (gameAbilities == null)
                return new List<GameAbilityDto>();

            return gameAbilities.Select(g => g.ToDto());
        }

        public static GameAbility ToEntity(int gameId, GameAbilityCreateDto gameAbilityDto)
        {
            if (gameAbilityDto == null)
                return null;

            return new GameAbility
            {
                AbilityId = gameAbilityDto.AbilityId,
                GameId = gameId,
                AbilityInGameId = gameAbilityDto.AbilityInGameId
            };
        }

        public static void UpdateFromDto(this GameAbility gameAbility, int gameId, GameAbilityUpdateDto gameAbilityDto)
        {
            if (gameAbility == null || gameAbilityDto == null)
                return;

            gameAbility.AbilityId = gameAbilityDto.AbilityId;
            gameAbility.GameId = gameId;
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
                MoveInGameId = gameMove.MoveInGameId
            };
        }

        public static IEnumerable<GameMoveDto> ToDtos(this IEnumerable<GameMove> gameMoves)
        {
            if (gameMoves == null)
                return new List<GameMoveDto>();

            return gameMoves.Select(g => g.ToDto());
        }

        public static GameMove ToEntity(int gameId, GameMoveCreateDto gameMoveDto)
        {
            if (gameMoveDto == null)
                return null;

            return new GameMove
            {
                MoveId = gameMoveDto.MoveId,
                GameId = gameId,
                MoveInGameId = gameMoveDto.MoveInGameId
            };
        }

        public static IEnumerable<GameMove> ToEntities(int gameId, IEnumerable<GameMoveCreateDto> gameMoveDtos)
        {
            if (gameMoveDtos == null)
                return new List<GameMove>();

            return gameMoveDtos.Select(g => ToEntity(gameId, g));
        }

        public static void UpdateFromDto(this GameMove gameMove, int gameId, GameMoveUpdateDto gameMoveDto)
        {
            if (gameMove == null || gameMoveDto == null)
                return;

            gameMove.MoveId = gameMoveDto.MoveId;
            gameMove.GameId = gameId;
            gameMove.MoveInGameId = gameMoveDto.MoveInGameId;
        }
        #endregion
    }
}
