using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Pokemons.Mapper
{
    public static class PokemonMapper
    {
        // Pokemon
        #region Pokemon
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
        #endregion


        // PokemonForm
        #region PokemonForm
        public static PokemonFormDto ToDto(this PokemonForm form)
        {
            return new PokemonFormDto
            {
                Id = form.Id,
                PokemonName = form.Pokemon.Name,
                Name = form.Name,
                IsRegional = form.IsRegional,
                IsMega = form.IsMega,
                IsGigantamax = form.IsGigantamax,
                FormOrder = form.FormOrder,
                Type1Id = form.Type1Id,
                Type2Id = form.Type2Id
            };
        }

        public static IEnumerable<PokemonFormDto> ToDtos(this IEnumerable<PokemonForm> forms)
        {
            return forms.Select(f => f.ToDto());
        }

        public static PokemonForm ToEntity(this PokemonFormCreateDto dto, int pokemonId)
        {
            if (dto == null)
                return null;

            return new PokemonForm
            {
                PokemonId = pokemonId,
                Name = dto.Name,
                IsRegional = dto.IsRegional,
                IsMega = dto.IsMega,
                IsGigantamax = dto.IsGigantamax,
                FormOrder = dto.FormOrder,
                Type1Id = dto.Type1Id,
                Type2Id = dto.Type2Id ?? null // Nullable type
            };
        }

        public static void UpdateFromDto(this PokemonForm form, int pokemonId, PokemonFormUpdateDto dto)
        {
            if (form == null || dto == null)
                return;

            form.PokemonId = pokemonId;
            form.Name = dto.Name;
            form.IsRegional = dto.IsRegional;
            form.IsMega = dto.IsMega;
            form.IsGigantamax = dto.IsGigantamax;
            form.FormOrder = dto.FormOrder;
            form.Type1Id = dto.Type1Id;
            form.Type2Id = dto.Type2Id ?? null; // Nullable type
        }
        #endregion

        // PokemonAbility
        #region PokemonAbility
        public static PokemonAbilityDto ToDto(this PokemonAbility ability)
        {
            return new PokemonAbilityDto
            {
                Id = ability.Id,
                PokemonName = ability.Pokemon.Name,
                AbilityName = ability.Ability.Name.Name,
                IsHidden = ability.IsHidden,
                AbilitySlot = ability.AbilitySlot
            };
        }

        public static IEnumerable<PokemonAbilityDto> ToDtos(this IEnumerable<PokemonAbility> abilities)
        {
            return abilities.Select(a => a.ToDto());
        }

        public static PokemonAbility ToEntity(this PokemonAbilityCreateDto dto, int pokemonId)
        {
            if (dto == null)
                return null;

            return new PokemonAbility
            {
                PokemonId = pokemonId,
                AbilityId = dto.AbilityId,
                IsHidden = dto.IsHidden,
                AbilitySlot = dto.AbilitySlot
            };
        }

        public static void UpdateFromDto(this PokemonAbility ability, int pokemonId, PokemonAbilityUpdateDto dto)
        {
            if (ability == null || dto == null)
                return;

            ability.PokemonId = pokemonId;
            ability.AbilityId = dto.AbilityId;
            ability.IsHidden = dto.IsHidden;
            ability.AbilitySlot = dto.AbilitySlot;
        }
        #endregion
    }
}
