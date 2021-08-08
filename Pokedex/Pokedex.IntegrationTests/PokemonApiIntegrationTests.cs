using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Pokedex.API;
using Pokedex.API.Models;
using Xunit;

namespace Pokedex.IntegrationTests
{
    public class PokemonApiIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PokemonApiIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Pokemon_Api_Basic_Endpoint_Returns_Pokemon_If_Pokemon_Is_Found()
        {
            var client = _factory.CreateClient();
            const string url = "pokemon/mewtwo";

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);

            Assert.NotNull(pokemon);
            Assert.Equal("mewtwo", pokemon.Name);
            Assert.NotNull(pokemon.Description);
            Assert.NotNull(pokemon.Habitat);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("dave")]
        [InlineData("")]
        public async Task Pokemon_Api_Basic_Endpoint_Returns_404_If_Pokemon_Is_Not_Found(string pokemonName)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"pokemon/{pokemonName}");

            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [Fact]
        public async Task Pokemon_Api_Translated_Endpoint_Returns_Pokemon_If_Pokemon_Is_Found()
        {
            var client = _factory.CreateClient();
            const string url = "pokemon/translated/pikachu";

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var pokemon = JsonSerializer.Deserialize<Pokemon>(json, options);

            Assert.NotNull(pokemon);
            Assert.Equal("pikachu", pokemon.Name);
            Assert.NotNull(pokemon.Description);
            Assert.NotNull(pokemon.Habitat);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("dave")]
        [InlineData("")]
        public async Task Pokemon_Api_Translated_Endpoint_Returns_404_If_Pokemon_Is_Not_Found(string pokemonName)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"pokemon/translated/{pokemonName}");

            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }
    }
}
