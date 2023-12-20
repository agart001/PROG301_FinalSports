using SportsLibrary.Commands;
using SportsLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsLibrary.ViewModels
{
    /// <summary>
    /// Represents a base view model for a specific type. Implements <see cref="IViewModel{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the model associated with the view model.</typeparam>
    public class BaseViewModel<T> : IViewModel<T> where T : notnull
    {
        #region PropertyChanged

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for a specific property.
        /// </summary>
        /// <param name="name">The name of the property that changed. Automatically provided by the compiler if not specified.</param>
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region GetModelCommand

        /// <summary>
        /// Gets or sets the command to retrieve the model.
        /// </summary>
        public ReturnCommand<T>? GetModelCommand { get; set; }

        /// <summary>
        /// Flag indicating whether the GetModelCommand can be executed.
        /// </summary>
        private bool getModel = false;

        /// <summary>
        /// Checks if the GetModelCommand can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter, not used in this case.</param>
        /// <returns>True if the command can be executed, otherwise false.</returns>
        private bool CanGetModel(object parameter) => getModel;

        /// <summary>
        /// Executes the GetModelCommand to retrieve the model.
        /// </summary>
        /// <param name="parameter">The command parameter, not used in this case.</param>
        /// <returns>The model.</returns>
        private T GetModel(object parameter)
        {
            if(Model == null) throw new NullReferenceException(nameof(Model));
            return Model;
        }

        #endregion

        #region Model

        /// <summary>
        /// Gets or sets the associated model.
        /// </summary>
        private T? model;

        /// <summary>
        /// Gets or sets the associated model. Raises PropertyChanged event for "Model" when set.
        /// </summary>
        internal T? Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for BaseViewModel.
        /// </summary>
        public BaseViewModel() { }

        /// <summary>
        /// Constructor for BaseViewModel with an initial model.
        /// Initializes a new instance with the provided model and sets up the GetModelCommand.
        /// </summary>
        /// <param name="model">The initial model for the view model.</param>
        public BaseViewModel(T model)
        {
            getModel = true;
            GetModelCommand = new ReturnCommand<T>(GetModel, CanGetModel);
            Model = model;
        }

        #endregion
    }
}
