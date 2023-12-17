using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    public interface IEntity : ISerializable
    {
        /// <summary>
        /// An Entity's, public get and protected set, Guid identification number.
        /// </summary>
        public Guid ID { get; protected set; }

        #region Name

        /// <summary>
        /// An Entity's, public get and protected set, string name.
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// Set's a entity's name.
        /// </summary>
        /// <param name="name">The name to be set.</param>
        public void SetName(string? name);

        #endregion

        #region Description

        /// <summary>
        /// An Entity's, public get and protected set, string description.
        /// </summary>
        public string? Description { get; protected set;}

        /// <summary>
        /// Sets a entity's description.
        /// </summary>
        /// <param name="description">The description to be set.</param>
        public void SetDescription(string? description);

        #endregion

        /// <summary>
        /// Gets an Entity's "about" string.
        /// </summary>
        /// <returns>The "about" string</returns>
        public string? About();
    }

    public interface IStat<T> : IEntity
    {
        T Data { get; protected set; }
        public void SetStat(T Data);
    }

    public interface ICategory : IEntity
    {
    }
}
