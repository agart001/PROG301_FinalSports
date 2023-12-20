using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.ViewModels
{
    /// <summary>
    /// Represents a view model for a person. Inherits from <see cref="BaseViewModel{T}"/> with T as <see cref="BasePerson"/>.
    /// </summary>
    public class PersonViewModel : BaseViewModel<BasePerson>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string? FirstName
        {
            get { return Model?.FirstName; }
            set
            {
                if (Model != null)
                {
                    Model.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string? LastName
        {
            get { return Model?.LastName; }
            set
            {
                if (Model != null)
                {
                    Model.LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int? Age
        {
            get { return Model?.Age; }
            set
            {
                if (Model != null)
                {
                    Model.Age = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the person.
        /// </summary>
        public string? Position
        {
            get { return Model?.Position; }
            set
            {
                if (Model != null)
                {
                    Model.Position = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for PersonViewModel.
        /// </summary>
        public PersonViewModel()
        {
        }

        /// <summary>
        /// Constructor for PersonViewModel with an initial person data.
        /// Initializes a new instance with the provided person data.
        /// </summary>
        /// <param name="data">The initial person data for the view model.</param>
        public PersonViewModel(BasePerson data) : base(data)
        {
        }

        #endregion
    }
}
