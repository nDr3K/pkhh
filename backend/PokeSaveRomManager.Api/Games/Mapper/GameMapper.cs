using PokeSaveRomManager.Api.Games.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Games.Mapper
{
    public static class GameMapper
    {
        // Map Game -> GameDto
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
                MoveGameIds = game.MoveGame?.Select(m => m.Id).ToList() ?? [],
                TypeGameIds = game.TypeGame?.Select(t => t.Id).ToList() ?? []
            };
        }

        // Map IEnumerable<Game> -> IEnumerable<GameDto>
        public static IEnumerable<GameDto> ToDtos(this IEnumerable<Game> games)
        {
            if (games == null)
                return new List<GameDto>();

            return games.Select(g => g.ToDto());
        }

        // Map GameCreateDto -> Game
        public static Game ToEntity(this GameCreateDto gameCreateDto)
        {
            if (gameCreateDto == null)
                return null;

            return new Game
            {
                Name = gameCreateDto.Name,
                Generation = gameCreateDto.Generation,
                Official = gameCreateDto.Official,
                Region = gameCreateDto.Region
            };
        }

        // Map GameUpdateDto -> Game (updates existing Game)
        public static void UpdateFromDto(this Game game, GameUpdateDto gameUpdateDto)
        {
            if (game == null || gameUpdateDto == null)
                return;

            game.Name = gameUpdateDto.Name;
            game.Generation = gameUpdateDto.Generation;
            game.Official = gameUpdateDto.Official;
            game.Region = gameUpdateDto.Region;
        }
    }
}
