using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;

namespace Pokedex.API.Services
{
    public record PokemonClientResult(bool success, PokemonDto PokemonDto);

    public class PokemonClient : IPokemonClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<PokemonClient> _logger;

        public PokemonClient(HttpClient client, ILogger<PokemonClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<PokemonClientResult> GetPokemon(string name)
        {
            var url = $"api/v2/pokemon-species/{name}";
            var response = await _client.GetAsync(url);

            _logger.LogInformation($"Status code of {response.StatusCode} for API call to: {url}.");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var pokemon = JsonSerializer.Deserialize<PokemonDto>(json);
                return new PokemonClientResult(true, pokemon);
            }

            return new PokemonClientResult(false, null);
        }
    }
}
