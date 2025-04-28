using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeSaveRomManager.Api.Pokemons.DTOs;
using PokeSaveRomManager.Api.Pokemons.Services;
using PokeSaveRomManager.Api.Shared.Constants;
using PokeSaveRomManager.Api.Shared.Policies;

namespace PokeSaveRomManager.Api.Pokemons.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.Pokemon.Forms)]
    public class PokemonFormController : Controller
    {
        private readonly IPokemonFormService _pokemonFormService;
        private readonly ILogger<PokemonFormController> _logger;

        public PokemonFormController(IPokemonFormService pokemonFormService, ILogger<PokemonFormController> logger)
        {
            _pokemonFormService = pokemonFormService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonFormDto>> GetById(int id)
        {
            var form = await _pokemonFormService.GetByIdAsync(id);
            if (form == null)
            {
                _logger.LogWarning($"PokemonForm with ID {id} not found");
                return NotFound();
            }

            _logger.LogInformation($"Fetched PokemonForm with ID {id}");
            return Ok(form);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PokemonFormDto>>> GetAll(int pokemonId)
        {
            _logger.LogInformation($"Fetching all PokemonForms for Pokemon ID: {pokemonId}");
            var forms = await _pokemonFormService.GetAllAsync(pokemonId);
            return Ok(forms);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<ActionResult<PokemonFormDto>> Create(int pokemonId, PokemonFormCreateDto form)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return BadRequest(ModelState);
            }

            var createdForm = await _pokemonFormService.CreateAsync(pokemonId, form);
            _logger.LogInformation($"Created PokemonForm with ID {createdForm.Id} for Pokemon ID {pokemonId}");
            return CreatedAtAction(nameof(GetById), new { id = createdForm.Id }, createdForm);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Update(int id, int pokemonId, PokemonFormUpdateDto form)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (!await _pokemonFormService.ExistsAsync(id))
            {
                _logger.LogWarning($"PokemonForm with ID {id} not found");
                return NotFound();
            }

            await _pokemonFormService.UpdateAsync(id, pokemonId, form);
            _logger.LogInformation($"Updated PokemonForm with ID {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = PermissionPolicies.PokemonPolicy)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _pokemonFormService.ExistsAsync(id))
            {
                _logger.LogWarning($"PokemonForm with ID {id} not found");
                return NotFound();
            }

            await _pokemonFormService.DeleteAsync(id);
            _logger.LogInformation($"Deleted PokemonForm with ID {id}");
            return NoContent();
        }
    }
}
