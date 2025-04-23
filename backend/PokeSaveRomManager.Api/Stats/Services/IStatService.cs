using PokeSaveRomManager.Api.Stats.DTOs;

namespace PokeSaveRomManager.Api.Stats.Services
{
    public interface IStatService
    {
        Task<IEnumerable<StatDto>> GetAllAsync();
        Task<StatDto> GetByIdAsync(int id);
        Task<StatDto> AddAsync(StatCreateDto statDto);
        Task UpdateAsync(int id, StatUpdateDto statDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
