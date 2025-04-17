using PokeSaveRomManager.Data.Entities;

namespace PokeSaveRomManager.Api.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByAuth0IdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<bool> ExistsAsync(string email);
    }
}
