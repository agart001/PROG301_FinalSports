using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces.ILocations
{
    internal interface IState : ILocation
    {
        public ILocation Country { get; protected set; }
    }
}
