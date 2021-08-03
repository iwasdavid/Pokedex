using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.API.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
