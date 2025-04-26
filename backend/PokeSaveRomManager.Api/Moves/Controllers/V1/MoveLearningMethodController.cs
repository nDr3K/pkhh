using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Moves.DTOs;
using PokeSaveRomManager.Api.Moves.Services;
using PokeSaveRomManager.Api.Shared.Constants;

namespace PokeSaveRomManager.Api.Moves.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Moves.LearningMethods)]
    public class MoveLearningMethodController : Controller
    {
        private readonly IMoveLearningMethodService _moveLearningMethodService;
        private readonly ILogger<MoveLearningMethodController> _logger;

        public MoveLearningMethodController(IMoveLearningMethodService moveLearningMethodService, ILogger<MoveLearningMethodController> logger)
        {
            _moveLearningMethodService = moveLearningMethodService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MoveLearningMethodDto>>> GetAll()
        {
            _logger.LogInformation("Retrieving all move learning methods");
            var moveLearningMethods = await _moveLearningMethodService.GetAllAsync();
            return Ok(moveLearningMethods);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MoveLearningMethodDto>> GetById(int id)
        {
            var moveLearningMethod = await _moveLearningMethodService.GetByIdAsync(id);
            if (moveLearningMethod == null)
            {
                _logger.LogWarning($"Move learning method with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Retrieving move learning method with ID {id}");
            return Ok(moveLearningMethod);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(MoveLearningMethodCreateDto moveLearningMethodDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state");
                return BadRequest(ModelState);
            }
            var createdMoveLearningMethod = await _moveLearningMethodService.CreateAsync(moveLearningMethodDto);
            _logger.LogInformation($"Created move learning method with ID {createdMoveLearningMethod.Id}");
            return CreatedAtAction(nameof(GetById), new { id = createdMoveLearningMethod.Id }, createdMoveLearningMethod);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, MoveLearningMethodUpdateDto moveLearningMethodDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _moveLearningMethodService.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning($"Move learning method with ID {id} not found");
                return NotFound();
            }

            await _moveLearningMethodService.UpdateAsync(id, moveLearningMethodDto);
            _logger.LogInformation($"Updated move learning method with ID {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _moveLearningMethodService.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning($"Move learning method with ID {id} not found");
                return NotFound();
            }

            await _moveLearningMethodService.DeleteAsync(id);
            _logger.LogInformation($"Deleted move learning method with ID {id}");
            return NoContent();
        }
    }
}
