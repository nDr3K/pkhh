using PokeSaveRomManager.Api.Natures.DTOs;

namespace PokeSaveRomManager.Api.Natures.Services
{
    public interface INatureService
    {
        Task<IEnumerable<NatureDto>> GetAllAsync();
        Task<NatureDto> GetByIdAsync(int id);
        Task<NatureDto> AddAsync(NatureCreateDto natureDto);
        Task UpdateAsync(int id, NatureUpdateDto natureDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
