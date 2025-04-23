using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Shared.Policies;
using PokeSaveRomManager.Api.Stats.DTOs;
using PokeSaveRomManager.Api.Stats.Services;

namespace PokeSaveRomManager.Api.Stats.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/stat")]
    public class StatController : Controller
    {
        private readonly IStatService _statService;
        private readonly ILogger<StatController> _logger;

        public StatController(IStatService statService, ILogger<StatController> logger)
        {
            _statService = statService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatDto>>> GetStats()
        {
            _logger.LogInformation("Getting all stats");
            var stats = await _statService.GetAllAsync();
            return Ok(stats);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatDto>> GetStat(int id)
        {
            _logger.LogInformation("Getting stat with id: {Id}", id);
            var stat = await _statService.GetByIdAsync(id);
            if (stat == null)
            {
                _logger.LogWarning("Stat with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(stat);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.StatsPolicy)]
        public async Task<ActionResult<StatDto>> CreateStat(StatCreateDto stat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new stat: {StatName}", stat.Name);
            var createdStat = await _statService.AddAsync(stat);
            return CreatedAtAction(nameof(GetStat), new { id = createdStat.Id }, createdStat);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.StatsPolicy)]
        public async Task<IActionResult> UpdateStat(int id, StatUpdateDto stat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating stat with id: {Id}", id);
            var existingStat = await _statService.GetByIdAsync(id);

            if (existingStat == null)
            {
                _logger.LogWarning("Stat with id: {Id} not found", id);
                return NotFound();
            }

            await _statService.UpdateAsync(id, stat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.StatsPolicy)]
        public async Task<IActionResult> DeleteStat(int id)
        {
            var existingStat = await _statService.GetByIdAsync(id);
            if (existingStat == null)
            {
                _logger.LogWarning("Stat with id: {Id} not found", id);
                return NotFound();
            }
            await _statService.DeleteAsync(id);
            return NoContent();
        }
    }
}
