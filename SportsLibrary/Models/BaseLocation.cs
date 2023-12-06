using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models
{
    internal class BaseLocation : BaseEntity, ILocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
