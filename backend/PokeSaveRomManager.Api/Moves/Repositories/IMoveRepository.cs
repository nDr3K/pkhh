using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Moves.Repositories
{
    public interface IMoveRepository
    {
        // Move
        Task<IEnumerable<Move>> GetAllAsync();
        Task<Move> GetByIdAsync(int id);
        Task<Move> CreateAsync(Move move);
        Task UpdateAsync(Move move);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Move>> GetByTypeIdAsync(int typeId);
        Task<IEnumerable<Move>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Move>> GetByTypeIdAndCategoryIdAsync(int typeId, int categoryId);

        // MoveName
        Task<IEnumerable<MoveName>> GetAllNamesAsync();
        Task<MoveName> GetNameByIdAsync(int id);
        Task<MoveName> CreateNameAsync(MoveName moveName);
        Task UpdateNameAsync(MoveName moveName);
        Task DeleteNameAsync(int id);
        Task<bool> ExistsNameAsync(int id);
    }
}
