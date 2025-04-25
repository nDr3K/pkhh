using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Abilities.Repositories
{
    public interface IAbilityRepository
    {
        Task<IEnumerable<Ability>> GetAllAsync();
        Task<Ability> GetByIdAsync(int id);
        Task<Ability> CreateAsync(Ability ability);
        Task UpdateAsync(Ability ability);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
