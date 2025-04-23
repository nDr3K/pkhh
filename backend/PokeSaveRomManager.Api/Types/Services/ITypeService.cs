using PokeSaveRomManager.Api.Types.DTOs;

namespace PokeSaveRomManager.Api.Types.Services
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeDto>> GetAllAsync();
        Task<TypeDto> GetByIdAsync(int id);
        Task<TypeDto> AddAsync(TypeCreateDto typeDto);
        Task UpdateAsync(int id, TypeUpdateDto typeDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
