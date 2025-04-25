using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Games.DTOs
{
    public class GameMoveDto
    {
        public int Id { get; set; }

        [Required]
        public string GameName { get; set; }

        [Required]
        public string MoveName { get; set; }

        [Required]
        public int MoveInGameId { get; set; }
    }

    public class GameMoveCreateDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int MoveId { get; set; }

        [Required]
        public int MoveInGameId { get; set; }
    }

    public class GameMoveUpdateDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int MoveId { get; set; }

        [Required]
        public int MoveInGameId { get; set; }
    }
}
