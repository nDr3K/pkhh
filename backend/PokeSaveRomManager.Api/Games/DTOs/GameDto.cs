using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Games.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Generation { get; set; }

        public bool Official { get; set; } = true;

        public string Region { get; set; }
    }

    public class GameCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        [Range(0, 20)]
        public int Generation { get; set; }

        public bool Official { get; set; } = true;

        public string Region { get; set; }
    }

    public class GameUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Generation { get; set; }

        public bool Official { get; set; } = true;

        public string Region { get; set; }
    }
}