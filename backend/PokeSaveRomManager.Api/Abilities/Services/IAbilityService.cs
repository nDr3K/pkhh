using PokeSaveRomManager.Api.Abilities.DTOs;

namespace PokeSaveRomManager.Api.Abilities.Services
{
    public interface IAbilityService
    {
        //Ability
        Task<IEnumerable<AbilityDto>> GetAllAsync();
        Task<AbilityDto> GetByIdAsync(int id);
        Task<AbilityDto> CreateAsync(AbilityCreateDto abilityCreateDto);
        Task UpdateAsync(int id, AbilityUpdateDto abilityUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        //AbilityName
        Task<IEnumerable<AbilityNameDto>> GetAllNamesAsync();
        Task<AbilityNameDto> GetNameByIdAsync(int id);
        Task<AbilityNameDto> CreateNameAsync(AbilityNameCreateDto abilityNameCreateDto);
        Task UpdateNameAsync(int id, AbilityNameUpdateDto abilityNameUpdateDto);
        Task DeleteNameAsync(int id);
        Task<bool> ExistsNameAsync(int id);
    }
}
