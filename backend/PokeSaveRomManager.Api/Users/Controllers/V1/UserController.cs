using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Users.Services;

namespace PokeSaveRomManager.Api.Users.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // This endpoint will be called after successful Auth0 authentication
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> HandleAuth0Login()
        {
            _logger.LogInformation("Handling Auth0 login for user: {User}", User.Identity?.Name);
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("User is not authenticated");
                return Unauthorized("User is not authenticated");
            }
            // Get existing user or create new one based on Auth0 data
            var user = await _userService.GetOrCreateUserFromClaimsAsync(User);

            // Return user data and potentially a session token or other application-specific info
            return Ok(new
            {
                UserId = user.Id,
                user.Name,
                user.Email,
                Message = "User successfully authenticated"
            });
        }
    }
}
