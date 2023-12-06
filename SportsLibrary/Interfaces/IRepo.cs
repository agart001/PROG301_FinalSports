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
    }
}
