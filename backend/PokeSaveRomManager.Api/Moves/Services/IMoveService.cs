using PokeSaveRomManager.Api.Moves.DTOs;

namespace PokeSaveRomManager.Api.Moves.Services
{
    public interface IMoveService
    {
        // Move
        Task<IEnumerable<MoveDto>> GetAllAsync();
        Task<MoveDto> GetByIdAsync(int id);
        Task<MoveDto> CreateAsync(MoveCreateDto move);
        Task UpdateAsync(int id, MoveUpdateDto move);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<MoveDto>> GetByTypeIdAsync(int typeId);
        Task<IEnumerable<MoveDto>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<MoveDto>> GetByTypeIdAndCategoryIdAsync(int typeId, int categoryId);

        // MoveName
        Task<IEnumerable<MoveNameDto>> GetAllNamesAsync();
        Task<MoveNameDto> GetNameByIdAsync(int id);
        Task<MoveNameDto> CreateNameAsync(MoveNameCreateDto moveName);
        Task UpdateNameAsync(int id, MoveNameUpdateDto moveName);
        Task DeleteNameAsync(int id);
        Task<bool> ExistsNameAsync(int id);
    }
}
