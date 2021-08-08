using System.Threading.Tasks;
using Pokedex.API.Models;

namespace Pokedex.API.Interfaces
{
    public interface ITranslator
    {
        Task<string> TranslateDescription(Pokemon pokemon);
    }
}
