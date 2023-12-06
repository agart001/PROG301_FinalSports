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

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
