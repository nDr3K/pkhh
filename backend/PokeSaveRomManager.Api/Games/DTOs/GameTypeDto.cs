using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Games.DTOs
{
    public class GameTypeDto
    {
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public int TypeInGameId { get; set; }
    }

    public class GameTypeCreateDto
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        public int TypeInGameId { get; set; }
    }

    public class GameTypeUpdateDto
    {
        [Required]
        public int TypeId { get; set; }

        [Required]
        public int TypeInGameId { get; set; }
    }
}
