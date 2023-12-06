using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces.IPersons
{
    internal interface IWorker : IPerson
    {
        /// <summary>
        /// A Worker's public get and protected set string position(occupation).
        /// </summary>
        public string Position { get; protected set; }

        /// <summary>
        /// A Worker's public get and protected set string array of their duties(responsiblities).
        /// </summary>
        public string[] Duties { get; protected set; }

        /// <summary>
        /// A Worker's public get and protected set decimal salary(pay).
        /// </summary>
        public decimal Salary { get; protected set; }
    }
}
