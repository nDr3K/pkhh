using PokeSaveRomManager.Api.Auth0.Models;

namespace PokeSaveRomManager.Api.Auth0.Interfaces
{
    public interface IAuth0ApiClient
    {
        public Task<UserInfoResponse> GetUserInfoAsync(string accessToken);
    }
}
