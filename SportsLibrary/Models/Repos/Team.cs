using SportsLibrary.Interfaces;
using SportsLibrary.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models.Repos
{
    internal class Team : BaseOrganization
    {
        public string? Mascot { get; protected set; }

        public Team() { }

        public Team(List<IPerson> staff, List<IPerson> players)
        {
            Contents.Add(typeof(Staff), staff);
            Con
        }
    }
}
