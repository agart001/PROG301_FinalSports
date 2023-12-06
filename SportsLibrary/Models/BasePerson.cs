using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    internal abstract class BasePerson : BaseEntity, IPerson
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public ILocation? Origin { get; set; }

        public BasePerson() : base()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Origin = null;
        }

        public BasePerson(string? name) : base(name)
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Origin = null;
        }

        public BasePerson(string? firstName, string? lastName, int? age, ILocation? origin)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Name = $"{FirstName} {LastName}";
            Age = age ?? 0;
            Origin = origin;
        }

        public BasePerson(string? name,string? firstName, string? lastName, int? age, ILocation? origin) : base(name)
        {
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Age = age ?? 0;
            Origin = origin;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
