using SportsLibrary.Interfaces;
using SportsLibrary.Interfaces.IPersons;
using SportsLibrary.Interfaces.IRepos;
using SportsLibrary.Models.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models.Persons
{
    internal class Player : BaseWorker, IPlayer
    {
        public IOrganization Team { get; set; }
        public Dictionary<string, object> Stats { get; set; }


        public Player() { }

        public Player(string? name, string? firstName, string? lastName, int? age, ILocation? origin,
            string position, string[] duties, decimal salary) : 
            base(name, firstName, lastName, age, origin,
                position, duties, salary) { }

        public Player(string? name, string? firstName, string? lastName, int? age, ILocation? origin,
            string position, string[] duties, decimal salary, 
            IOrganization team, Dictionary<string, object> stats) :
            base(name, firstName, lastName, age, origin, position, duties, salary)
        {
            Team = team;
            Stats = stats;
        }
    }
}
