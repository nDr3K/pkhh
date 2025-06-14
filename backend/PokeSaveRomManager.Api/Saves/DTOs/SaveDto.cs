using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.DTOs
{
    public class SaveDto
    {
        public int Id { get; set; }
        public string Game { get; set; }
        public string Name { get; set; }
        public SaveDtoPokemon[] Team { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SaveDtoPokemon
    {
        public int Id { get; set; } // PokemonInstanceId
        public int DexNumber { get; set; }
        public string Name { get; set; }
    }

    public class SaveDetailDto
    {
        public int Id { get; set; }
        public string Game { get; set; } // Game name
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public string PlayTime { get; set; }
        public int[] Badges { get; set; } // List of owned badges
        public bool IsFavorite { get; set; }
        public string PlayerName { get; set; }
        public SaveDetailDtoPokemon[] Team { get; set; } = [];
        public SaveDetailDtoPokemon[] Boxes { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SaveDetailDtoPokemon
    {
        public int Id { get; set; } // PokemonInstanceId
        public int DexNumber { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Ability { get; set; }
        public int Level { get; set; }
        public string Nature { get; set; }
        public string Move1 { get; set; }
        public string Move2 { get; set; }
        public string Move3 { get; set; }
        public string Move4 { get; set; }
    }
}
