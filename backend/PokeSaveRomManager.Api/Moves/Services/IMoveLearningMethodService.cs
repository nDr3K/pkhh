using PokeSaveRomManager.Api.Moves.DTOs;

namespace PokeSaveRomManager.Api.Moves.Services
{
    public interface IMoveLearningMethodService
    {
        Task<IEnumerable<MoveLearningMethodDto>> GetAllAsync();
        Task<MoveLearningMethodDto> GetByIdAsync(int id);
        Task<MoveLearningMethodDto> CreateAsync(MoveLearningMethodCreateDto moveLearningMethod);
        Task UpdateAsync(int id, MoveLearningMethodUpdateDto moveLearningMethod);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
