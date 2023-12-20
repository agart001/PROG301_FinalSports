#nullable enable

using SportsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    /// <summary>
    /// Interface representing a repository, which contracts <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="TKey">The repository's key type which is <see langword="notnull"/>.</typeparam>
    /// <typeparam name="TValue">The repository's value type.</typeparam>
    public interface IRepo<TKey, TValue> : IEntity where TKey : notnull
    {
        #region Contents

        /// <summary>
        /// A repository's, public get and protected set, collection of key-value pairs.
        /// </summary>
        public ICollection<KeyValuePair<TKey, ICollection<TValue>>>? Contents { get; protected set; }

        /// <summary>
        /// Sets a repository's contents.
        /// </summary>
        /// <param name="contents">The contents to be set.</param>
        public void SetContents(ICollection<KeyValuePair<TKey, ICollection<TValue>>>? contents);

        #endregion

        #region Get

        #region Values

        /// <summary>
        /// Gets a value based on a key.
        /// </summary>
        /// <param name="key">The search key.</param>
        /// <returns>The search value (nullable).</returns>
        public ICollection<TValue>? GetValue(TKey key);

        /// <summary>
        /// Gets a collection of the value collections in the repository.
        /// </summary>
        /// <returns>The values present in the repository (nullable).</returns>
        public ICollection<ICollection<TValue>>? GetValues();

        #endregion

        #region Keys

        /// <summary>
        /// Gets a key based on value.
        /// </summary>
        /// <param name="col">The search value.</param>
        /// <returns>The search key (nullable).</returns>
        public TKey? GetKey(ICollection<TValue> col);

        /// <summary>
        /// Gets a collection of the keys in the repository.
        /// </summary>
        /// <returns>The keys present in the repository (nullable).</returns>
        public ICollection<TKey>? GetKeys();

        #endregion

        #endregion

        #region Add

        /// <summary>
        /// Adds a key-value pair to the repository's contents.
        /// </summary>
        /// <param name="key">The pair's key.</param>
        /// <param name="col">The pair's value.</param>
        public void Add(TKey key, ICollection<TValue> col);

        /// <summary>
        /// Adds a key and initializes its contents with an empty collection.
        /// </summary>
        /// <param name="key">The key to be added.</param>
        public void AddKey(TKey key);

        /// <summary>
        /// Adds a value to a key's collection in the repository's contents.
        /// </summary>
        /// <param name="key">The search key.</param>
        /// <param name="value">The value to be added.</param>
        public void ValueAdd(TKey key, TValue value);

        #endregion

        #region Remove

        /// <summary>
        /// Removes a key-value pair from the repository's contents.
        /// </summary>
        /// <param name="key">The key of the pair to be removed.</param>
        public void Remove(TKey key);

        /// <summary>
        /// Removes a value from a key's collection in the repository's contents.
        /// </summary>
        /// <param name="key">The search key.</param>
        /// <param name="value">The value to be removed.</param>
        public void ValueRemove(TKey key, TValue value);

        #endregion
    }
}
