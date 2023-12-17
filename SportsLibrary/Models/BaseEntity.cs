using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    public abstract class BaseEntity : IEntity
    {
        public Guid ID { get; set; }

        #region Name

        public string? Name { get; set; }
        public void SetName(string? name) => Name = name;

        #endregion

        #region Description

        public string? Description { get; set; }
        public void SetDescription(string? description) => Description = description;

        #endregion

        public virtual string? About() => Description;

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

        public BaseEntity(string? name, string? description)
        {
            ID = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class Stat<T> : BaseEntity, IStat<T>
    {
        public T Data { get; set; }

        public void SetStat(T data) => Data = data;

        public Stat() { }

        public Stat(string? name) : base(name) { }

        public Stat(string name, string? description) : base(name, description) { }

        public Stat(T data)
        {
            Data = data;
        }

        public Stat(string? name, T data) : base(name) 
        {
            Data = data;
        }

        public Stat(string name, string? description, T data) : base(name, description) 
        { 
            Data = data;
        }
    }

    public class Category : BaseEntity, ICategory
    {
        public Category() { }

        public Category(string? name) : base(name) { }

        public Category(string? name, string? description) : base(name, description) { }
    }
}
