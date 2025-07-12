using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Roms.DTOs;
using PokeSaveRomManager.Api.Roms.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Roms.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Roms.Root)]
    public class RomController : Controller
    {
        private readonly ILogger<RomController> _logger;
        private readonly IRomService _romService;

        public RomController(ILogger<RomController> logger, IRomService romService)
        {
            _logger = logger;
            _romService = romService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.RomPolicy)]
        public async Task<IActionResult> UploadRom([FromForm] RomUploadDto romDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _romService.UploadRomAsync(romDto);

                return Ok(new { message = "ROM file uploaded successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing ROM upload");
                return StatusCode(500, "An error occurred while processing the ROM file.");
            }
        }
    }
}
