using SportsLibrary.Interfaces.IRepos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces.IPersons
{
    internal interface IPlayer : IWorker
    {
        public IOrganization Team { get; protected set; }

        public Enum Stat { get; protected set; }
        public Hashtable Stats { get; protected set; }
    }
}
