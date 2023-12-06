using SportsLibrary.Interfaces;
using SportsLibrary.Interfaces.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models.Repos
{
    internal class BaseOrganization : BaseRepo<Type, List<IPerson>>, IOrganization
    {
        public ILocation Location { get; set; }
        public string? Symbol { get; set; }
    }
}
