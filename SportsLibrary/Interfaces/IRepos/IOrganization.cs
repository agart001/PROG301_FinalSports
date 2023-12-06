using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces.IRepos
{
    internal interface IOrganization
    {
        public ILocation Location { get; protected set; }
        public string? Symbol { get; protected set; }
    }
}
