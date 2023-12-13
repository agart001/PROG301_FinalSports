#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    internal interface IRepo<TKey,TValue> : IEntity
    {
        public Dictionary<TKey,TValue>? Contents { get; protected set; }

        public void Add(TKey key, TValue value);
        public void Remove(TKey key);

        public void ValueAdd(TKey key, TValue value);

        public void ValueRemove(TKey key);

        public TValue GetValue(TKey key);
    }
}
