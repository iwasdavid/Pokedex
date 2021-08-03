using Pokedex.API.Interfaces;
using Pokedex.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.API.Services
{
    public class PokemonService : IPokemonService
    {
        public Task<Pokemon> GetPokemon(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Pokemon> GetTranslatedPokemon(string name)
        {
            throw new NotImplementedException();
        }
    }
}
