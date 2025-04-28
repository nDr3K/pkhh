using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Saves.Repositories
{
    public interface ISaveRepository
    {
        Task<(IEnumerable<Save> Saves, int TotalCount)> GetAllAsync(string userId, int pageNumber, int pageSize);
        Task<Save> GetByIdAsync(int saveId);
    }
}
