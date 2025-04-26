using PokeSaveRomManager.Data.Domain;

namespace PokeSaveRomManager.Api.Moves.Repositories
{
    public interface IMoveLearningMethodRepository
    {
        Task<IEnumerable<MoveLearningMethod>> GetAllAsync();
        Task<MoveLearningMethod> GetByIdAsync(int id);
        Task<MoveLearningMethod> CreateAsync(MoveLearningMethod moveLearningMethodCreateDto);
        Task UpdateAsync(MoveLearningMethod moveLearningMethodUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
