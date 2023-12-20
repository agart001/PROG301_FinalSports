using Newtonsoft.Json;
using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    /// <summary>
    /// Abstract base class representing an entity with common properties and methods. Implements <see cref="IEntity"/>.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// An Entity's Guid identification number.
        /// </summary>
        public Guid ID { get; set; }

        #region Name

        /// <summary>
        /// An Entity's string name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Sets an entity's name.
        /// </summary>
        /// <param name="name">The name to be set.</param>
        public void SetName(string? name) => Name = name;

        #endregion

        #region Description

        /// <summary>
        /// An Entity's string description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Sets an entity's description.
        /// </summary>
        /// <param name="description">The description to be set.</param>
        public void SetDescription(string? description) => Description = description;

        /// <summary>
        /// Gets an Entity's "about" string.
        /// </summary>
        /// <returns>The "about" string</returns>
        public virtual string? About() => Description;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for BaseEntity.
        /// Initializes a new instance with a new Guid and an empty name.
        /// </summary>
        public BaseEntity()
        {
            ID = Guid.NewGuid();
            Name = string.Empty;
        }

        /// <summary>
        /// Constructor for BaseEntity with a specified name.
        /// Initializes a new instance with a new Guid and the provided name.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        public BaseEntity(string? name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }

        /// <summary>
        /// Constructor for BaseEntity with specified name and description.
        /// Initializes a new instance with a new Guid, the provided name, and description.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="description">The description of the entity.</param>
        public BaseEntity(string? name, string? description)
        {
            ID = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor for BaseEntity with specified name, description, and ID.
        /// Initializes a new instance with the provided ID, name, and description.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="description">The description of the entity.</param>
        /// <param name="id">The ID of the entity.</param>
        public BaseEntity(string? name, string? description, Guid id)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        #endregion

        #region ISerializable

        /// <summary>
        /// Virtual method for implementing serialization of BaseEntity.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The StreamingContext representing the source or destination of the serialization.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// Represents a generic statistic with data of type T. Inherits from <see cref="BaseEntity"/> 
    /// and implements <see cref="IStat{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of data for the statistic.</typeparam>
    public class Stat<T> : BaseEntity, IStat<T>
    {
        #region Data

        /// <summary>
        /// Gets or sets the data associated with the statistic.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Sets the data for the statistic.
        /// </summary>
        /// <param name="data">The data to be set.</param>
        public void SetStat(T data) => Data = data;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for Stat.
        /// Initializes a new instance with default values.
        /// </summary>
        public Stat() { }

        /// <summary>
        /// Constructor for Stat with a specified name.
        /// Initializes a new instance with the provided name and default values.
        /// </summary>
        /// <param name="name">The name of the statistic.</param>
        public Stat(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Stat with specified name and description.
        /// Initializes a new instance with the provided name, description, and default values.
        /// </summary>
        /// <param name="name">The name of the statistic.</param>
        /// <param name="description">The description of the statistic.</param>
        public Stat(string name, string? description) : base(name, description) { }

        /// <summary>
        /// Constructor for Stat with specified data.
        /// Initializes a new instance with the provided data and default values for other properties.
        /// </summary>
        /// <param name="data">The data for the statistic.</param>
        public Stat(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Constructor for Stat with specified name and data.
        /// Initializes a new instance with the provided name, data, and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the statistic.</param>
        /// <param name="data">The data for the statistic.</param>
        public Stat(string? name, T data) : base(name)
        {
            Data = data;
        }

        /// <summary>
        /// Constructor for Stat with specified name, description, and data.
        /// Initializes a new instance with the provided name, description, data, and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the statistic.</param>
        /// <param name="description">The description of the statistic.</param>
        /// <param name="data">The data for the statistic.</param>
        public Stat(string name, string? description, T data) : base(name, description)
        {
            Data = data;
        }

        #endregion
    }


    /// <summary>
    /// Represents a category, which is a specialized type of <see cref="IEntity"/>. Inherits  from <see cref="BaseEntity"/>.
    /// </summary>
    public class Category : BaseEntity
    {
        #region Constructors

        /// <summary>
        /// Default constructor for Category.
        /// Initializes a new instance with default values.
        /// </summary>
        public Category() { }

        /// <summary>
        /// Constructor for Category with a specified name.
        /// Initializes a new instance with the provided name and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        public Category(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Category with specified name and description.
        /// Initializes a new instance with the provided name, description, and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="description">The description of the category.</param>
        public Category(string? name, string? description) : base(name, description) { }

        /// <summary>
        /// Constructor for Category with specified name, description, and ID.
        /// Initializes a new instance with the provided ID, name, description, and default values for other properties.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="description">The description of the category.</param>
        /// <param name="id">The ID of the category.</param>
        public Category(string? name, string? description, Guid id) : base(name, description, id) { }

        #endregion
    }
}
