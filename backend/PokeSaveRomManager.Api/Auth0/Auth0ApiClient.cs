using PokeSaveRomManager.Api.Auth0.Interfaces;
using PokeSaveRomManager.Api.Auth0.Models;
using System.Net.Http.Headers;

namespace PokeSaveRomManager.Api.Auth0
{
    public class Auth0ApiClient: IAuth0ApiClient
    {
        private readonly HttpClient _httpClient;

        public Auth0ApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{config["Auth0:Domain"]}");
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("/userinfo");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserInfoResponse>();
        }
    }
}
