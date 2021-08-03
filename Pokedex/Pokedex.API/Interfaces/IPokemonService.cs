using Pokedex.API.Models;
using System.Threading.Tasks;

namespace Pokedex.API.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemon(string name);
        Task<Pokemon> GetTranslatedPokemon(string name);
    }
}
