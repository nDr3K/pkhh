using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Abilities.Repositories
{
    public interface IAbilityRepository
    {
        //Ability
        Task<IEnumerable<Ability>> GetAllAsync();
        Task<Ability> GetByIdAsync(int id);
        Task<Ability> CreateAsync(Ability ability);
        Task UpdateAsync(Ability ability);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        //AbilityName
        Task<IEnumerable<AbilityName>> GetAllNamesAsync();
        Task<AbilityName> GetNameByIdAsync(int id);
        Task<AbilityName> CreateNameAsync(AbilityName abilityName);
        Task UpdateNameAsync(AbilityName abilityName);
        Task DeleteNameAsync(int id);
        Task<bool> ExistsNameAsync(int id);
    }
}
