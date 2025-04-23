using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Natures.Repositories
{
    public interface INatureRepository
    {
        Task<IEnumerable<Nature>> GetAllAsync();
        Task<Nature> GetByIdAsync(int id);
        Task<Nature> AddAsync(Nature nature);
        Task UpdateAsync(Nature nature);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
