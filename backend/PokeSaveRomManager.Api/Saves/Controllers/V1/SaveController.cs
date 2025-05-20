using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Api.Saves.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Models;
using System.Security.Claims;

namespace PokeSaveRomManager.Api.Saves.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Saves.Root)]
    public class SaveController : Controller
    {
        private readonly ISaveService _saveService;
        private readonly ILogger<SaveController> _logger;

        public SaveController(ISaveService saveService, ILogger<SaveController> logger)
        {
            _saveService = saveService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<IEnumerable<SaveDto>>>> GetAll([FromRoute] int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (saves, totalCount) = await _saveService.GetAllAsync(userId, pageNumber, pageSize);
            _logger.LogInformation("Retrieved {Count} saves for User with ID {UserId}", saves.Count(), userId);
            return Ok(saves);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SaveDetailDto>> GetById(int id)
        {
            var save = await _saveService.GetByIdAsync(id);
            if (save == null)
            {
                _logger.LogWarning("Save with ID {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Retrieved save with ID {Id}", id);
            return Ok(save);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateSaveFile([FromForm] SaveFileUploadDto saveFileUploadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in token claims");
                    return Unauthorized("User ID not found in token");
                }

                await _saveService.Create(userId, saveFileUploadDto);

                return Ok(new { message = "ROM file uploaded successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing ROM upload");
                return StatusCode(500, "An error occurred while processing the ROM file.");
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadSaveFile(int id, [FromForm] SaveFileUploadDto saveFileUploadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in token claims");
                    return Unauthorized("User ID not found in token");
                }

                await _saveService.Update(userId, id, saveFileUploadDto);

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
