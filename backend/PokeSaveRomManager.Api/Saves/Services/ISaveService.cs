using PokeSaveRomManager.Api.Saves.DTOs;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public interface ISaveService
    {
        Task<(IEnumerable<SaveDto> Saves, int TotalCount)> GetAllAsync(string userId, int pageNumber, int pageSize);
        Task<SaveDetailDto> GetByIdAsync(int saveId);
    }
}
