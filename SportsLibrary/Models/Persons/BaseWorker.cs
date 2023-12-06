using SportsLibrary.Interfaces.IPersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Models.Persons
{
    internal class BaseWorker : BasePerson, IWorker
    {
        public string Position { get; set; }
        public string[] Duties { get; set; }
        public decimal Salary { get; set; }
    }
}
