using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    internal interface IPerson : IEntity
    {
        /// <summary>
        /// A Person's public get and protected set string first name.
        /// </summary>
        public string? FirstName { get; protected set; }

        /// <summary>
        /// A Person's public get and protected set string last name.
        /// </summary>
        public string? LastName { get; protected set; }

        /// <summary>
        /// A Person's public get and protected set integer age name.
        /// </summary>
        public int? Age { get; protected set; }


        public ILocation? Origin { get; protected set; }

    }
}
