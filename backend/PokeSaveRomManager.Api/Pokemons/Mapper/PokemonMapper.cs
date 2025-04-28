using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Mapper
{
    public static class PokemonMapper
    {
        public static PokemonDto ToDto(this Pokemon pokemon)
        {
            return new PokemonDto
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                DexNumber = pokemon.DexNumber,
                Game = pokemon.Game.Name,
                HP = pokemon.HP,
                Attack = pokemon.Attack,
                Defense = pokemon.Defense,
                Special = pokemon.Special,
                SpAttack = pokemon.SpAttack,
                SpDefense = pokemon.SpDefense,
                Speed = pokemon.Speed,
                Type1 = pokemon.Type1.Name,
                Type2 = pokemon.Type2?.Name,
                FormName = pokemon.FormName,
                IsRegionalForm = pokemon.IsRegionalForm,
                IsMega = pokemon.IsMega,
                IsGigantamax = pokemon.IsGigantamax
            };
        }

        public static IEnumerable<PokemonDto> ToDtos(this IEnumerable<Pokemon> pokemons)
        {
            return pokemons.Select(p => p.ToDto());
        }
        public static Pokemon ToEntity(this PokemonCreateDto dto)
        {
            if (dto == null)
                return null;

            return new Pokemon
            {
                Name = dto.Name,
                DexNumber = dto.DexNumber,
                HP = dto.HP,
                Attack = dto.Attack,
                Defense = dto.Defense,
                Special = dto.Special,
                SpAttack = dto.SpAttack,
                SpDefense = dto.SpDefense,
                Speed = dto.Speed,
                Type1Id = dto.Type1Id,
                Type2Id = dto.Type2Id ?? null, // Nullable type
                FormName = dto.FormName ?? string.Empty, // Default to empty string if null
                IsRegionalForm = dto.IsRegionalForm,
                IsMega = dto.IsMega,
                IsGigantamax = dto.IsGigantamax
            };
        }

        public static void UpdateFromDto(this Pokemon pokemon, PokemonUpdateDto dto)
        {
            if (pokemon == null || dto == null)
                return;

            pokemon.Name = dto.Name;
            pokemon.DexNumber = dto.DexNumber;
            pokemon.HP = dto.HP;
            pokemon.Attack = dto.Attack;
            pokemon.Defense = dto.Defense;
            pokemon.Special = dto.Special;
            pokemon.SpAttack = dto.SpAttack;
            pokemon.SpDefense = dto.SpDefense;
            pokemon.Speed = dto.Speed;
            pokemon.Type1Id = dto.Type1Id;
            pokemon.Type2Id = dto.Type2Id ?? null; // Nullable type
            pokemon.FormName = dto.FormName ?? string.Empty; // Default to empty string if null
            pokemon.IsRegionalForm = dto.IsRegionalForm;
            pokemon.IsMega = dto.IsMega;
            pokemon.IsGigantamax = dto.IsGigantamax;
        }
    }
}
