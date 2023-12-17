using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.Interfaces
{
    public interface IFactory<TResult> : IEntity
    {
        /// <summary>
        /// Creates object based on factory method.
        /// </summary>
        /// <returns>Returns the an instance of the factory's defined entity.</returns>
        TResult Create();

        /// <summary>
        /// Creates multiple objects based on the factory method.
        /// </summary>
        /// <returns>Returns the an collection of the factory's defined entity.</returns>
        ICollection<TResult> CreateMultiple();
    }
}
