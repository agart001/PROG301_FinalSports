using SportsLibrary.Interfaces;
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

        public BaseWorker() : base() { }

        public BaseWorker(string? name, string? firstName, string? lastName, int? age, ILocation? origin) :
            base(name, firstName, lastName, age, origin) { }

        public BaseWorker(string? name, string? firstName, string? lastName, int? age, ILocation? origin, string position, string[] duties, decimal salary) :
            base(name, firstName, lastName, age, origin)
        {
            Position = position;
            Duties = duties;
            Salary = salary;
        }
    }
}
