using SportsLibrary.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    /// <summary>
    /// Interface representing an entity which contracts <see cref="ISerializable"/>.
    /// </summary>
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

    /// <summary>
    /// Interface representing a "stat", or statistic, which contracts <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="T">The statistic data.</typeparam>
    public interface IStat<T> : IEntity
    {
        T? Data { get; protected set; }
        public void SetStat(T Data);
    }

    /// <summary>
    /// Interface representing a view model, which <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <typeparam name="TModel">The model with in the view which is <see langword="notnull"/>.</typeparam>
    public interface IViewModel<TModel> : INotifyPropertyChanged where TModel : notnull
    {
        public ReturnCommand<TModel>? GetModelCommand { get; set; }
    }
}
