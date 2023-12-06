using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    internal interface IEntity : ISerializable
    {
        /// <summary>
        /// An Entity's public get and protected set Guid identication number.
        /// </summary>
        public Guid ID { get; protected set; }

        /// <summary>
        /// An Entity's public get and protected set string name.
        /// </summary>
        public string? Name { get; protected set; }
    }
}
