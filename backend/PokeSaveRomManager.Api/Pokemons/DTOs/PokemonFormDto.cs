using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Pokemons.DTOs
{
    public class PokemonFormDto
    {
        public int Id { get; set; }

        [Required]
        public string PokemonName { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
        public int? FormOrder { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
    }

    public class  PokemonFormCreateDto
    {
        public int PokemonId { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
        public int? FormOrder { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
    }

    public class PokemonFormUpdateDto
    {
        public int PokemonId { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
        public int? FormOrder { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
    }
}
