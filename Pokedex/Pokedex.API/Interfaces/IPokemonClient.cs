using System.Threading.Tasks;
using Pokedex.API.Services;

namespace Pokedex.API.Interfaces
{
    public interface IPokemonClient
    {
        Task<PokemonClientResult> GetPokemon(string name);
    }
}
