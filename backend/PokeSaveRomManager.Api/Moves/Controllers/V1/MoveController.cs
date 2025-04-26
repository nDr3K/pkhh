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
    [Route(ApiRoutes.Moves.Root)]
    public class MoveController : Controller
    {
        private readonly IMoveService _moveService;
        private readonly ILogger<MoveController> _logger;

        public MoveController(IMoveService moveService, ILogger<MoveController> logger)
        {
            _moveService = moveService;
            _logger = logger;
        }

        // Move
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MoveDto>>> GetAll()
        {
            _logger.LogInformation("Getting all moves");
            var moves = await _moveService.GetAllAsync();
            return Ok(moves);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MoveDto>> GetById(int id)
        {
            var move = await _moveService.GetByIdAsync(id);
            if (move == null)
            {
                _logger.LogWarning($"Move with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Getting move with id {id}");
            return Ok(move);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MoveDto>> Create(MoveCreateDto moveCreateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating move");
            var move = await _moveService.CreateAsync(moveCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = move.Id }, move);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, MoveUpdateDto moveUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state");
                return BadRequest(ModelState);
            }

            var moveExists = await _moveService.ExistsAsync(id);
            if (!moveExists)
            {
                _logger.LogWarning($"Move with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Updating move with id {id}");
            await _moveService.UpdateAsync(id, moveUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var moveExists = await _moveService.ExistsAsync(id);
            if (!moveExists)
            {
                _logger.LogWarning($"Move with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Deleting move with id {id}");
            await _moveService.DeleteAsync(id);
            return NoContent();
        }

        // MoveName
        [HttpGet(ApiRoutes.Moves.Names)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MoveNameDto>>> GetAllNames()
        {
            _logger.LogInformation("Getting all move names");
            var moveNames = await _moveService.GetAllNamesAsync();
            return Ok(moveNames);
        }

        [HttpGet(ApiRoutes.Moves.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MoveNameDto>> GetNameById(int id)
        {
            var moveName = await _moveService.GetNameByIdAsync(id);
            if (moveName == null)
            {
                _logger.LogWarning($"Move name with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"Getting move name with id {id}");
            return Ok(moveName);
        }

        [HttpPost(ApiRoutes.Moves.Names)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MoveNameDto>> CreateName(MoveNameCreateDto moveNameCreateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating move name");
            var moveName = await _moveService.CreateNameAsync(moveNameCreateDto);
            return CreatedAtAction(nameof(GetNameById), new { id = moveName.Id }, moveName);
        }

        [HttpPut(ApiRoutes.Moves.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateName(int id, MoveNameUpdateDto moveNameUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state");
                return BadRequest(ModelState);
            }

            var moveNameExists = await _moveService.ExistsNameAsync(id);
            if (!moveNameExists)
            {
                _logger.LogWarning($"Move name with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Updating move name with id {id}");
            await _moveService.UpdateNameAsync(id, moveNameUpdateDto);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Moves.Names + "/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteName(int id)
        {
            var moveNameExists = await _moveService.ExistsNameAsync(id);
            if (!moveNameExists)
            {
                _logger.LogWarning($"Move name with id {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Deleting move name with id {id}");
            await _moveService.DeleteNameAsync(id);
            return NoContent();
        }
    }
}
