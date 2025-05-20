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
                GameId = dto.GameId,
            };
        }

        public static IEnumerable<Pokemon> ToEntities(this IEnumerable<PokemonCreateDto> dtos)
        {
            return dtos.Select(dto => dto.ToEntity());
        }

        public static void UpdateFromDto(this Pokemon pokemon, PokemonUpdateDto dto)
        {
            if (pokemon == null || dto == null)
                return;

            pokemon.Name = dto.Name;
            pokemon.DexNumber = dto.DexNumber;
            pokemon.GameId = dto.GameId;
        }
        #endregion


        // PokemonForm
        #region PokemonForm
        public static PokemonFormDto ToDto(this PokemonForm form)
        {
            return new PokemonFormDto
            {
                Id = form.Id,
                InternalId = form.InternalId ?? form.Pokemon.Id,
                PokemonId = form.Pokemon.Id,
                PokemonName = form.Pokemon.Name,
                Name = form.Name,
                HP = form.HP,
                Attack = form.Attack,
                Defense = form.Defense,
                Special = form.Special,
                SpAttack = form.SpAttack,
                SpDefense = form.SpDefense,
                Speed = form.Speed,
                Type1 = form.Type1.Name,
                Type2 = form.Type2?.Name,
                IsDefault = form.IsDefault,
                IsRegional = form.IsRegional,
                IsMega = form.IsMega,
                IsGigantamax = form.IsGigantamax
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
                InternalId = dto.InternalId,
                Name = dto.Name,
                HP = dto.HP,
                Attack = dto.Attack,
                Defense = dto.Defense,
                Special = dto.Special,
                SpAttack = dto.SpAttack,
                SpDefense = dto.SpDefense,
                Speed = dto.Speed,
                Type1Id = dto.Type1Id,
                Type2Id = dto?.Type2Id,
                IsDefault = dto.IsDefault,
                IsRegional = dto.IsRegional,
                IsMega = dto.IsMega,
                IsGigantamax = dto.IsGigantamax
            };
        }

        public static IEnumerable<PokemonForm> ToEntities(this IEnumerable<PokemonFormCreateDto> dtos)
        {
            return dtos.Select(dto => dto.ToEntity(dto.PokemonId));
        }

        public static void UpdateFromDto(this PokemonForm form, int pokemonId, PokemonFormUpdateDto dto)
        {
            if (form == null || dto == null)
                return;

            form.PokemonId = pokemonId;
            form.InternalId = dto.InternalId;
            form.Name = dto.Name;
            form.HP = dto.HP;
            form.Attack = dto.Attack;
            form.Defense = dto.Defense;
            form.Special = dto.Special;
            form.SpAttack = dto.SpAttack;
            form.SpDefense = dto.SpDefense;
            form.Speed = dto.Speed;
            form.Type1Id = dto.Type1Id;
            form.Type2Id = dto?.Type2Id;
            form.IsDefault = dto.IsDefault;
            form.IsRegional = dto.IsRegional;
            form.IsMega = dto.IsMega;
            form.IsGigantamax = dto.IsGigantamax;
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

        // PokemonMove
        #region PokemonMove
        public static PokemonMoveDto ToDto(this PokemonMove move)
        {
            return new PokemonMoveDto
            {
                Id = move.Id,
                PokemonName = move.Pokemon.Name,
                GameInMoveName = move.GameMove.Move.Name.Name,
                MethodName = move.Method.Name,
                Level = move.Level,
                TMNumber = move.TMNumber,
                IsTutor = move.IsTutor,
                IsEggMove = move.IsEggMove
            };
        }

        public static IEnumerable<PokemonMoveDto> ToDtos(this IEnumerable<PokemonMove> moves)
        {
            return moves.Select(m => m.ToDto());
        }

        public static PokemonMove ToEntity(this PokemonMoveCreateDto dto, int pokemonId)
        {
            if (dto == null)
                return null;

            return new PokemonMove
            {
                PokemonId = pokemonId,
                GameInMoveId = dto.GameInMoveId,
                MethodId = dto.MethodId,
                Level = dto.Level,
                TMNumber = dto.TMNumber,
                IsTutor = dto.IsTutor,
                IsEggMove = dto.IsEggMove
            };
        }

        public static void UpdateFromDto(this PokemonMove move, int pokemonId, PokemonMoveUpdateDto dto)
        {
            if (move == null || dto == null)
                return;

            move.PokemonId = pokemonId;
            move.GameInMoveId = dto.GameInMoveId;
            move.MethodId = dto.MethodId;
            move.Level = dto.Level;
            move.TMNumber = dto.TMNumber;
            move.IsTutor = dto.IsTutor;
            move.IsEggMove = dto.IsEggMove;
        }
        #endregion
    }
}
