﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SportsLibrary.Commands
{
    /// <summary>
    /// Represents a basic, void, command. Implements <see cref="ICommand"/>.
    /// </summary>
    public class BasicCommand : ICommand
    {
        #region Delegates

        /// <summary>
        /// Represents a method that will be called to execute the command.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public delegate void IExecute(object parameter);

        /// <summary>
        /// Represents a method that will be called to determine if the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public delegate bool ICanExecute(object parameter);

        private readonly IExecute execute;
        private readonly ICanExecute canExecute;

        #endregion

        #region Execute

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter)
        {
            if(parameter == null) throw new ArgumentNullException(nameof(parameter));
            return canExecute.Invoke(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public void Execute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            execute.Invoke(parameter);
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BasicCommand class.
        /// </summary>
        /// <param name="_execute">The method to be called when the command is executed.</param>
        /// <param name="_canExecute">The method to determine if the command can be executed.</param>
        /// <exception cref="ArgumentNullException">Thrown if _execute or _canExecute is null.</exception>
        public BasicCommand(IExecute _execute, ICanExecute _canExecute)
        {
            execute = _execute ?? throw new ArgumentNullException(nameof(_execute));
            canExecute = _canExecute ?? throw new ArgumentNullException(nameof(_canExecute));
        }

        #endregion
    }

    /// <summary>
    /// Represents a command that returns a result of executed method. Implements <see cref="ICommand"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the command.</typeparam>
    public class ReturnCommand<TResult> : ICommand
    {
        #region Delegates

        /// <summary>
        /// Represents a method that will be called to execute the command and return a result.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        /// <returns>The result of the command.</returns>
        public delegate TResult IExecute(object parameter);

        /// <summary>
        /// Represents a method that will be called to determine if the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public delegate bool ICanExecute(object parameter);

        private readonly IExecute execute;
        private readonly ICanExecute canExecute;

        #endregion

        #region Execute

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        /// <returns>True if the command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            return canExecute.Invoke(parameter);
        }

        /// <summary>
        /// Gets the result of the command.
        /// </summary>
        public TResult? Result { get; private set; }

        /// <summary>
        /// Executes the command and sets the result.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public void Execute(object? parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            // Set the result
            Result = execute.Invoke(parameter);
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ReturnCommand class.
        /// </summary>
        /// <param name="_execute">The method to be called when the command is executed and returns a result.</param>
        /// <param name="_canExecute">The method to determine if the command can be executed.</param>
        /// <exception cref="ArgumentNullException">Thrown if _execute or _canExecute is null.</exception>
        public ReturnCommand(IExecute _execute, ICanExecute _canExecute)
        {
            execute = _execute ?? throw new ArgumentNullException(nameof(_execute));
            canExecute = _canExecute ?? throw new ArgumentNullException(nameof(_canExecute));
        }

        #endregion
    }
}
