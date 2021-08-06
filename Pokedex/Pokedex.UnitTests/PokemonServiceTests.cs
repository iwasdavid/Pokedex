using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;
using Pokedex.API.Services;
using Xunit;

namespace Pokedex.UnitTests
{
    public class PokemonServiceTests
    {
        private readonly ILogger<PokemonService> _fakeLogger;
        private readonly IPokemonClient _fakePokemonClient;

        public PokemonServiceTests()
        {
            _fakeLogger = A.Fake<ILogger<PokemonService>>();
            _fakePokemonClient = A.Fake<IPokemonClient>();
        }

        [Fact]
        public async Task GetPokemon_Correctly_Returns_New_Pokemon()
        {
            const string pokemonName = "mewtwo";
            const string pokemonDescription = "Some text about mewtwo";
            const string pokemonHabitat = "rare";
            const bool pokemonIsLegendary = true;

            var pokemonDto = new PokemonDto()
            {
                Name = "mewtwo",
                FlavorTextEntries = new List<FlavorTextEntry>() {
                    new FlavorTextEntry() { FlavorText = pokemonDescription, Language = new Language() { Name = "en" } }
                },
                Habitat = new Habitat { Name = pokemonHabitat },
                isLegendary = true
            };

            A.CallTo(() => _fakePokemonClient.GetPokemon(pokemonName)).Returns(new PokemonClientResult(true, pokemonDto));

            var sut = new PokemonService(_fakePokemonClient, _fakeLogger);

            var result = await sut.GetPokemon(pokemonName);

            Assert.True(result.success);
            Assert.Equal(pokemonName, result.pokemon.Name);
            Assert.Equal(pokemonDescription, result.pokemon.Description);
            Assert.Equal(pokemonHabitat, result.pokemon.Habitat);
            Assert.Equal(pokemonIsLegendary, result.pokemon.IsLegendary);
        }

        [Fact]
        public async Task GetPokemon_Returns_Success_False_When_No_Pokemon_Found()
        {
            const string pokemonName = "mewtwo";

            A.CallTo(() => _fakePokemonClient.GetPokemon(pokemonName)).Returns(new PokemonClientResult(false, null));

            var sut = new PokemonService(_fakePokemonClient, _fakeLogger);

            var result = await sut.GetPokemon(pokemonName);

            Assert.False(result.success);
            Assert.Null(result.pokemon);
        }

        [Fact]
        public async Task GetPokemon_Correctly_Returns_Default_Values_When_Pokemon_Is_Found_Without_Values()
        {
            const string pokemonName = "mewtwo";

            var pokemonDto = new PokemonDto();

            A.CallTo(() => _fakePokemonClient.GetPokemon(pokemonName)).Returns(new PokemonClientResult(true, pokemonDto));

            var sut = new PokemonService(_fakePokemonClient, _fakeLogger);

            var result = await sut.GetPokemon(pokemonName);

            Assert.True(result.success);
            Assert.Equal("No name found", result.pokemon.Name);
            Assert.Equal("No description found", result.pokemon.Description);
            Assert.Equal("No habitat found", result.pokemon.Habitat);
            Assert.False(result.pokemon.IsLegendary);
        }
    }
}
