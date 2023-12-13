using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    internal class BaseRepo<TKey, TValue> : BaseEntity, IRepo<TKey, TValue>
    {
        public Dictionary<TKey, TValue>? Contents { get; set; }

        public BaseRepo()
        {
            Contents = new Dictionary<TKey, TValue>();
        }

        public BaseRepo(Dictionary<TKey, TValue>? contents)
        {
            Contents = contents;
        }

        public BaseRepo(TKey[] keys, TValue[] values)
        {
            Contents = new Dictionary<TKey, TValue>();
            for(int i = 0; i < keys.Length; i++)
            {
                Contents.Add(keys[i], values[i]);
            }
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            Contents.Add(key, value);
        }

        public void Remove(TKey key)
        {
            Contents.Remove(key);
        }

        public void ValueAdd<T>(TKey key, TValue value)
        {
        }

        public void ValueRemove(TKey key)
        {
            throw new NotImplementedException();
        }

        public TValue GetValue(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
