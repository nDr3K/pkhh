using PokeSaveRomManager.Data.Entities;
using System.Security.Claims;

namespace PokeSaveRomManager.Api.Users.Services
{
    public interface IUserService
    {
        Task<User> GetOrCreateUserFromClaimsAsync(ClaimsPrincipal userClaims);
    }
}
