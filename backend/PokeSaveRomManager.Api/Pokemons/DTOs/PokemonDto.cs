using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Api.Pokemons.DTOs
{
    public class PokemonDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int DexNumber { get; set; }
        public string Game { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string FormName { get; set; }
        public bool IsRegionalForm { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }

    public class PokemonCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int DexNumber { get; set; }
        public int GameId { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public string FormName { get; set; }
        public bool IsRegionalForm { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }

    public class PokemonUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int DexNumber { get; set; }
        public int GameId { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int? Special { get; set; }
        public int? SpAttack { get; set; }
        public int? SpDefense { get; set; }
        public int Speed { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set; }
        public string FormName { get; set; }
        public bool IsRegionalForm { get; set; } = false;
        public bool IsMega { get; set; } = false;
        public bool IsGigantamax { get; set; } = false;
    }
}
