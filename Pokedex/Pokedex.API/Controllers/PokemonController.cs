using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;
using System;
using System.Threading.Tasks;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {

        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<Pokemon>> Get(string name)
        {
            try
            {
                var pokemonServiceResult = await _pokemonService.GetPokemon(name);

                if (pokemonServiceResult.success)
                {
                    _logger.LogInformation($"Pokemon: {name} found.");
                    return Ok(pokemonServiceResult.pokemon);
                }
                _logger.LogWarning($"Pokemon: {name} not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown when attempting to retrieve Pokemon: {name}. Error Message: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
