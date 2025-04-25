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

        // Related collections - IDs only for DTOs
        public List<int> PokemonIds { get; set; } = [];
        public List<int> TeamIds { get; set; } = [];
        public List<int> BoxIds { get; set; } = [];
        public List<int> MoveGameIds { get; set; } = [];
        public List<int> TypeGameIds { get; set; } = [];
        public List<int> AbilityGameIds { get; set; } = [];
    }

    public class GameCreateDto
    {
        [Required]
        public string Name { get; set; }

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