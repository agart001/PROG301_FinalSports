using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    internal class BaseEntity : IEntity
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        public BaseEntity()
        {
            ID = Guid.NewGuid();
            Name = string.Empty;
        }

        public  BaseEntity(string? name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}
