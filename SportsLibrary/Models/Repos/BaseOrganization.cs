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
        public BaseOrganization() : base() { }
        public BaseOrganization(Dictionary<Type, List<IPerson>> data) : base(data) { }
        public BaseOrganization(Type[] keys, List<IPerson>[] values) : base(keys, values) { }
        public ILocation Location { get; set; }
        public string? Symbol { get; set; }
    }
}
