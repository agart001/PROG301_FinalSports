using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportsLibrary.Interfaces;
using SportsLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    /// <summary>
    /// Represents a base repository. Inherits from <see cref="BaseEntity"/> and implements <see cref="IRepo{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the repository key which is <see langword="notnull"/>.</typeparam>
    /// <typeparam name="TValue">The type of the repository values.</typeparam>
    public class BaseRepo<TKey, TValue> : BaseEntity, IRepo<TKey, TValue> where TKey : notnull
    {
        #region Contents

        /// <summary>
        /// Gets or sets the collection of key-value pairs representing the contents of the repository.
        /// </summary>
        public ICollection<KeyValuePair<TKey, ICollection<TValue>>>? Contents { get; set; }

        /// <summary>
        /// Sets the contents of the repository.
        /// </summary>
        /// <param name="contents">The collection of key-value pairs to be set as contents.</param>
        public void SetContents(ICollection<KeyValuePair<TKey, ICollection<TValue>>>? contents) => Contents = contents;

        #endregion

        #region Get

        #region Values

        /// <summary>
        /// Gets the collection of values associated with a specified key.
        /// </summary>
        /// <param name="key">The key to look up in the contents.</param>
        /// <returns>The collection of values associated with the key.</returns>
        public ICollection<TValue>? GetValue(TKey key)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.FirstOrDefault(kvp => kvp.Key.Equals(key)).Value;
        }

        /// <summary>
        /// Gets all collections of values from the repository.
        /// </summary>
        /// <returns>The collection of all values in the repository.</returns>
        public ICollection<ICollection<TValue>>? GetValues()
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.Select(kvp => kvp.Value).ToList();
        }

        #endregion

        #region Keys

        /// <summary>
        /// Gets the key associated with a specified collection of values.
        /// </summary>
        /// <param name="col">The collection of values to look up in the contents.</param>
        /// <returns>The key associated with the collection of values.</returns>
        public TKey? GetKey(ICollection<TValue> col)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.FirstOrDefault(kvp => kvp.Value == col).Key;
        }

        /// <summary>
        /// Gets all keys from the repository.
        /// </summary>
        /// <returns>The collection of all keys in the repository.</returns>
        public ICollection<TKey>? GetKeys()
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            return Contents.Select(kvp => kvp.Key).ToList();
        }

        #endregion

        #endregion

        #region Add

        /// <summary>
        /// Adds a key-value pair to the repository.
        /// </summary>
        /// <param name="key">The key to be added.</param>
        /// <param name="col">The collection of values associated with the key.</param>
        public void Add(TKey key, ICollection<TValue> col)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents.Add(new KeyValuePair<TKey, ICollection<TValue>>(key, col));
        }

        /// <summary>
        /// Adds a key with an empty collection of values to the repository.
        /// </summary>
        /// <param name="key">The key to be added.</param>
        public void AddKey(TKey key)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            Contents.Add(new KeyValuePair<TKey, ICollection<TValue>>(key, new List<TValue>()));
        }

        /// <summary>
        /// Adds a value to the collection associated with a specified key in the repository.
        /// </summary>
        /// <param name="key">The key to look up in the contents.</param>
        /// <param name="value">The value to be added to the collection.</param>
        public void ValueAdd(TKey key, TValue value)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            var existingPair = Contents.FirstOrDefault(kvp =>
            {
                var kvpKey = (IEntity)kvp.Key;
                var _key = (IEntity)key;
                return kvpKey.Name == _key.Name;
            });

            if (existingPair.Equals(default(KeyValuePair<TKey, ICollection<TValue>>)))
            {
                throw new KeyNotFoundException($"Key '{key}' not found in Contents.");
            }

            existingPair.Value.Add(value);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes a key-value pair from the repository based on the specified key.
        /// </summary>
        /// <param name="key">The key to be removed.</param>
        public void Remove(TKey key)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            var existingPair = Contents.FirstOrDefault(kvp => kvp.Key.Equals(key));

            if (existingPair.Equals(default(KeyValuePair<TKey, ICollection<TValue>>)))
            {
                throw new KeyNotFoundException($"Key '{key}' not found in Contents.");
            }

            Contents.Remove(existingPair);
        }

        /// <summary>
        /// Removes a value from the collection associated with a specified key in the repository.
        /// </summary>
        /// <param name="key">The key to look up in the contents.</param>
        /// <param name="value">The value to be removed from the collection.</param>
        public void ValueRemove(TKey key, TValue value)
        {
            if (Contents == null) throw new NullReferenceException(nameof(Contents));
            var existingPair = Contents.FirstOrDefault(kvp => kvp.Key.Equals(key));

            if (existingPair.Equals(default(KeyValuePair<TKey, ICollection<TValue>>)))
            {
                throw new KeyNotFoundException($"Key '{key}' not found in Contents.");
            }

            existingPair.Value.Remove(value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for BaseRepo.
        /// Initializes a new instance with an empty contents collection.
        /// </summary>
        public BaseRepo()
        {
            Contents = new List<KeyValuePair<TKey, ICollection<TValue>>>();
        }

        /// <summary>
        /// Constructor for BaseRepo with a specified name.
        /// Initializes a new instance with the provided name and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        public BaseRepo(string? name) : base(name)
        {
            Contents = new List<KeyValuePair<TKey, ICollection<TValue>>>();
        }

        /// <summary>
        /// Constructor for BaseRepo with a specified name and description.
        /// Initializes a new instance with the provided name, description, and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="description">The description of the repository.</param>
        public BaseRepo(string? name, string? description) : base(name, description)
        {
            Contents = new List<KeyValuePair<TKey, ICollection<TValue>>>();
        }

        /// <summary>
        /// Constructor for BaseRepo with a specified name and contents collection.
        /// Initializes a new instance with the provided name and contents collection.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="contents">The initial contents collection of the repository.</param>
        public BaseRepo(string? name, ICollection<KeyValuePair<TKey, ICollection<TValue>>>? contents) : base(name)
        {
            Contents = contents;
        }

        /// <summary>
        /// Constructor for BaseRepo with a specified name, keys, and values arrays.
        /// Initializes a new instance with the provided name, creating key-value pairs from the arrays.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="keys">The array of keys.</param>
        /// <param name="values">The array of values.</param>
        public BaseRepo(string? name, TKey[] keys, ICollection<TValue>[] values) : base(name)
        {
            Contents = new List<KeyValuePair<TKey, ICollection<TValue>>>();
            for (int i = 0; i < keys.Length; i++)
            {
                Contents.Add(new KeyValuePair<TKey, ICollection<TValue>>(keys[i], values[i]));
            }
        }

        #endregion
    }


    /// <summary>
    /// Represents a repository for sports. Inherits from <see cref="BaseRepo{TKey, TValue}"/>, 
    /// with TKey as <see cref="Category"/> and TValue as <see cref="Sport"/>.
    /// </summary>
    public class SportRepo : BaseRepo<Category, Sport>
    {
        #region Constructors

        /// <summary>
        /// Default constructor for SportRepo.
        /// Initializes a new instance with an empty contents collection and sets the name based on the type.
        /// </summary>
        public SportRepo() : base()
        {
            // Set the name based on the type
            Name = GetType().Name;
        }

        /// <summary>
        /// Constructor for SportRepo with a specified name and contents collection.
        /// Initializes a new instance with the provided name and contents collection.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="contents">The initial contents collection of the repository.</param>
        public SportRepo(string? name, ICollection<KeyValuePair<Category, ICollection<Sport>>>? contents) : base(name, contents) { }

        #endregion
    }


    /// <summary>
    /// Represents a sport. Inherits from <see cref="BaseRepo{TKey, TValue}"/>,
    /// with TKey as <see cref="Category"/> and TValue as <see cref="Team"/>.
    /// </summary>
    public class Sport : BaseRepo<Category, Team>
    {
        #region Constructors

        /// <summary>
        /// Default constructor for Sport.
        /// Initializes a new instance with an empty contents collection.
        /// </summary>
        public Sport() : base() { }

        /// <summary>
        /// Constructor for Sport with a specified name.
        /// Initializes a new instance with the provided name and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        public Sport(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Sport with a specified name and description.
        /// Initializes a new instance with the provided name, description, and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        /// <param name="description">The description of the sport.</param>
        public Sport(string? name, string? description) : base(name, description) { }

        /// <summary>
        /// Constructor for Sport with a specified name and contents collection.
        /// Initializes a new instance with the provided name and contents collection.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        /// <param name="contents">The initial contents collection of the sport.</param>
        public Sport(string? name, ICollection<KeyValuePair<Category, ICollection<Team>>>? contents) : base(name, contents) { }

        /// <summary>
        /// Constructor for Sport with a specified name, description, and contents collection.
        /// Initializes a new instance with the provided name, description, and contents collection.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        /// <param name="description">The description of the sport.</param>
        /// <param name="contents">The initial contents collection of the sport.</param>
        public Sport(string? name, string? description, ICollection<KeyValuePair<Category, ICollection<Team>>>? contents) : base(name, contents)
        {
            // Set the description, or throw ArgumentNullException if description is null
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Constructor for Sport with a specified name, keys, and values arrays.
        /// Initializes a new instance with the provided name, creating key-value pairs from the arrays.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        /// <param name="keys">The array of keys.</param>
        /// <param name="values">The array of values.</param>
        public Sport(string? name, Category[] keys, ICollection<Team>[] values) : base(name, keys, values) { }

        #endregion
    }


    /// <summary>
    /// Represents a team. Inherits from <see cref="BaseRepo{TKey, TValue}"/> and implements <see cref="ITeam"/> interface,
    /// with TKey as <see cref="Category"/> and TValue as <see cref="BasePerson"/>.
    /// </summary>
    public class Team : BaseRepo<Category, BasePerson>, ITeam
    {
        #region Wins / Loses

        #region Wins

        /// <summary>
        /// Gets or sets the number of wins for the team.
        /// </summary>
        public int? Wins { get; set; }

        /// <summary>
        /// Sets the number of wins for the team.
        /// </summary>
        /// <param name="wins">The number of wins to set.</param>
        public void SetWins(int? wins) => Wins = wins;

        #endregion

        #region Loses

        /// <summary>
        /// Gets or sets the number of loses for the team.
        /// </summary>
        public int? Loses { get; set; }

        /// <summary>
        /// Sets the number of loses for the team.
        /// </summary>
        /// <param name="loses">The number of loses to set.</param>
        public void SetLoses(int? loses) => Loses = loses;

        #endregion

        /// <summary>
        /// Calculates and returns the win-loss ratio for the team.
        /// </summary>
        /// <returns>The win-loss ratio.</returns>
        public double WinLossRatio()
        {
            // Check if Wins and Loses are not null
            if (Wins == null || Loses == null) throw new InvalidOperationException(nameof(WinLossRatio));

            // Calculate and return the win-loss ratio
            return (double)Wins / (double)Loses;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for Team.
        /// Initializes a new instance with an empty contents collection.
        /// </summary>
        public Team() : base() { }

        /// <summary>
        /// Constructor for Team with a specified name.
        /// Initializes a new instance with the provided name and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        public Team(string? name) : base(name) { }

        /// <summary>
        /// Constructor for Team with a specified name and description.
        /// Initializes a new instance with the provided name, description, and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="description">The description of the team.</param>
        public Team(string? name, string? description) : base(name, description) { }

        /// <summary>
        /// Constructor for Team with a specified name, description, wins, and loses.
        /// Initializes a new instance with the provided name, description, wins, loses, and an empty contents collection.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="description">The description of the team.</param>
        /// <param name="wins">The number of wins.</param>
        /// <param name="loses">The number of loses.</param>
        public Team(string? name, string? description, int wins, int loses) : base(name, description)
        {
            // Set wins and loses
            Wins = wins;
            Loses = loses;
        }

        /// <summary>
        /// Constructor for Team with a specified name and contents collection.
        /// Initializes a new instance with the provided name and contents collection.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="contents">The initial contents collection of the team.</param>
        public Team(string? name, ICollection<KeyValuePair<Category, ICollection<BasePerson>>>? contents) : base(name, contents) { }

        /// <summary>
        /// Constructor for Team with a specified name, keys, and values arrays.
        /// Initializes a new instance with the provided name, creating key-value pairs from the arrays.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="keys">The array of keys.</param>
        /// <param name="values">The array of values.</param>
        public Team(string? name, Category[] keys, ICollection<BasePerson>[] values) : base(name, keys, values) { }

        #endregion
    }
}
