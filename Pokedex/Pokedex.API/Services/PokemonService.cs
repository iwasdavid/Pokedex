using Microsoft.Extensions.Logging;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.API.Services
{
    public record PokemonServiceResult(bool success, Pokemon pokemon);

    public class PokemonService : IPokemonService
    {
        private readonly IPokemonClient _pokemonClient;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(IPokemonClient pokemonClient, ILogger<PokemonService> logger)
        {
            _pokemonClient = pokemonClient;
            _logger = logger;
        }

        public async Task<PokemonServiceResult> GetPokemon(string name)
        {
            var pokemonClientResult = await _pokemonClient.GetPokemon(name);
            
            if(pokemonClientResult.success)
            {
                _logger.LogInformation($"Successfully retrieved pokemon {name} from Pokemon client");
                var pokemonDto = pokemonClientResult.PokemonDto;

                var pokemon = new Pokemon
                {
                    Name = pokemonDto?.Name ?? "No name found",
                    Habitat = pokemonDto.Habitat?.Name ?? "No habitat found",
                    IsLegendary = pokemonDto.isLegendary,
                    Description = pokemonDto?.FlavorTextEntries
                                    ?.FirstOrDefault(text => text?.Language?.Name.ToLower() == "en")?.FlavorText
                                    ?? "No description found"
                };

                _logger.LogInformation($"Returning PokemonServiceResult of true, Pokemon: {name}.");
                return new PokemonServiceResult(true, pokemon);
            }

            _logger.LogWarning($"Pokemon client returned false for pokemon: {name}.");
            return new PokemonServiceResult(false, null);
        }

        public Task<PokemonServiceResult> GetTranslatedPokemon(string name)
        {
            throw new NotImplementedException();
        }
    }
}
