using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    internal interface ILocation : IEntity
    {
        public double Latitude { get; protected set; }

        public double Longitude { get; protected set; }
    }
}
