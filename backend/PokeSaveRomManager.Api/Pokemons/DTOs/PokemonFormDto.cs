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
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public bool IsDefault { get; set; } = false;
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }

    public class  PokemonFormCreateDto
    {
        public int PokemonId { get; set; }
        public int InternalId { get; set; }

        [Required]
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public bool IsDefault { get; set; } = false;
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }

    public class PokemonFormUpdateDto
    {
        public int PokemonId { get; set; }
        public int InternalId { get; set; }

        [Required]
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public bool IsDefault { get; set; } = false;
        public bool IsRegional { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }
}
