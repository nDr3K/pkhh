using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Saves.DTOs;
using PokeSaveRomManager.Api.Saves.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Models;

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
        public async Task<ActionResult<PagedResponse<IEnumerable<SaveDto>>>> GetAll([FromRoute] string userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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
    }
}
