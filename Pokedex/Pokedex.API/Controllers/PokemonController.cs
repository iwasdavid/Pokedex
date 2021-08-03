using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;
using System;

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
        public ActionResult<Pokemon> Get(string name)
        {
            try
            {
                var pokemon = _pokemonService.GetPokemon(name);

                if (pokemon is not null)
                {
                    return Ok(pokemon);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
