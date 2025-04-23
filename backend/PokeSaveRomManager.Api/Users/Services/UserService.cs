using PokeSaveRomManager.Api.Auth0.Interfaces;
using PokeSaveRomManager.Api.Users.Repositories;
using PokeSaveRomManager.Data.Domain;
using System.Security.Claims;

namespace PokeSaveRomManager.Api.Users.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth0ApiClient _auth0ApiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IAuth0ApiClient auth0ApiClient,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _auth0ApiClient = auth0ApiClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<User> GetOrCreateUserFromClaimsAsync(ClaimsPrincipal claimsPrincipal)
        {
            // Extract sub claim (always available in the access token)
            var sub = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                     claimsPrincipal.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(sub))
            {
                throw new InvalidOperationException("Unable to get user ID from token");
            }

            // Try to get email and name from claims first
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ??
                       claimsPrincipal.FindFirst("email")?.Value;

            var name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ??
                      claimsPrincipal.FindFirst("name")?.Value;

            // If email or name is missing, get from Auth0 userinfo endpoint
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
            {
                // Get access token from the request
                string accessToken = GetAccessTokenFromRequest();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    try
                    {
                        // Call Auth0 userinfo endpoint through our client
                        var userInfo = await _auth0ApiClient.GetUserInfoAsync(accessToken);

                        // Fill in missing information
                        if (string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userInfo.Email))
                        {
                            email = userInfo.Email;
                        }

                        if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(userInfo.Name))
                        {
                            name = userInfo.Name;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log exception but continue with what we have
                        _logger.LogError(ex, "Error fetching user info from Auth0");
                    }
                }
            }

            // Check if user exists in database
            var user = await _userRepository.GetByAuth0IdAsync(sub);

            if (user == null)
            {
                // Create new user
                user = new User
                {
                    Auth0Id = sub,
                    Email = email,
                    Name = name,
                    CreatedAt = DateTime.UtcNow
                };

                // Save to database
                await _userRepository.CreateAsync(user);
            }
            else
            {
                // Update existing user if needed
                bool needsUpdate = false;

                if (!string.IsNullOrEmpty(email) && user.Email != email)
                {
                    user.Email = email;
                    needsUpdate = true;
                }

                if (!string.IsNullOrEmpty(name) && user.Name != name)
                {
                    user.Name = name;
                    needsUpdate = true;
                }

                if (needsUpdate)
                {
                    user.UpdatedAt = DateTime.UtcNow;
                    await _userRepository.UpdateAsync(user);
                }
            }

            return user;
        }

        private string GetAccessTokenFromRequest()
        {
            var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }
    }
}
