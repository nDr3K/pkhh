using PokeSaveRomManager.Api.Saves.DTOs;

namespace PokeSaveRomManager.Api.Saves.Services
{
    public interface ISaveService
    {
        Task<(IEnumerable<SaveDto> Saves, int TotalCount)> GetAllAsync(int userId, int pageNumber, int pageSize);
        Task<SaveDetailDto> GetByIdAsync(int saveId);
        Task<SaveDetailDto> Create(string userId, SaveFileUploadDto save);
        Task<SaveDetailDto> Update(string userId, int saveId, SaveFileUploadDto save);
        Task Delete(string userId, int saveId);
    }
}
