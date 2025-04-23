using Type = PokeSaveRomManager.Data.Domain.Type;

namespace PokeSaveRomManager.Api.Types.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<Type>> GetAllAsync();
        Task<Type> GetByIdAsync(int id);
        Task<Type> AddAsync(Type type);
        Task UpdateAsync(Type type);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
