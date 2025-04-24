using PokeSaveRomManager.Api.Abilities.DTOs;
using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Abilities.Mapper
{
    public static class AbilityMapper
    {
        public static AbilityDto ToDto(this Ability ability)
        {
            return new AbilityDto
            {
                Id = ability.Id,
                Name = ability.Name
            };
        }
        public static IEnumerable<AbilityDto> ToDtos(this IEnumerable<Ability> abilities)
        {
            return abilities.Select(c => c.ToDto());
        }

        public static Ability ToEntity(this AbilityCreateDto abilityyCreateDto)
        {
            if (abilityyCreateDto == null)
                return null;

            return new Ability
            {
                Name = abilityyCreateDto.Name
            };
        }

        public static void UpdateFromDto(this Ability ability, AbilityUpdateDto abilityUpdateDto)
        {
            if (ability == null || abilityUpdateDto == null)
                return;

            ability.Name = abilityUpdateDto.Name;
        }
    }
}
