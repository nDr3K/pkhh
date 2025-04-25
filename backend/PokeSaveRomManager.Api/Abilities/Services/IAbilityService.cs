using PokeSaveRomManager.Api.Abilities.DTOs;

namespace PokeSaveRomManager.Api.Abilities.Services
{
    public interface IAbilityService
    {
        Task<IEnumerable<AbilityDto>> GetAllAsync();
        Task<AbilityDto> GetByIdAsync(int id);
        Task<AbilityDto> CreateAsync(AbilityCreateDto abilityCreateDto);
        Task UpdateAsync(int id, AbilityUpdateDto abilityUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
