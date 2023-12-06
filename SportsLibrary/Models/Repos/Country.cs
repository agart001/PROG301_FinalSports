using SportsLibrary.Interfaces;
using SportsLibrary.Interfaces.ILocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models.Repos
{
    internal class Country : BaseLocation, IRepo<Type, List<ILocation>>
    {
        public Dictionary<Type, List<ILocation>>? Contents { get; set; }
    }
}
