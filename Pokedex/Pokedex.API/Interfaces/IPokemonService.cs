using System.Threading.Tasks;
using Pokedex.API.Services;

namespace Pokedex.API.Interfaces
{
    public interface IPokemonService
    {
        Task<PokemonServiceResult> GetPokemon(string name, bool translate = false);
    }
}
