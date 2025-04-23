using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Stats.Repositories
{
    public interface IStatRepository
    {
        Task<IEnumerable<Stat>> GetAllAsync();
        Task<Stat> GetByIdAsync(int id);
        Task<Stat> AddAsync(Stat stat);
        Task UpdateAsync(Stat stat);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
