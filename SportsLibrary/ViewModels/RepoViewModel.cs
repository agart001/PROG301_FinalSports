using SportsLibrary.Commands;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using System.Windows.Input;

namespace SportsLibrary.ViewModels
{
    /// <summary>
    /// Represents a view model for a repository. Inherits from <see cref="BaseViewModel{T}"/> with T as <see cref="IRepo{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the model's key which is <see langword="notnull"/>.</typeparam>
    /// <typeparam name="TValue">The type of the model's values.</typeparam>
    public class RepoViewModel<TKey, TValue> : BaseViewModel<IRepo<TKey, TValue>> where TKey : notnull
    {
        #region Set Contents

        /// <summary>
        /// Gets or sets the command to set contents for the repository.
        /// </summary>
        public ICommand? SetContentsCommand { get; protected set; }

        private bool setContents = false;
        private bool CanSetContents(object parameter) => setContents;

        /// <summary>
        /// Sets the contents for the repository.
        /// </summary>
        /// <param name="parameter">The contents to set.</param>
        private void SetContents(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            ICollection<KeyValuePair<TKey, ICollection<TValue>>> contents =
                (ICollection<KeyValuePair<TKey, ICollection<TValue>>>)parameter
                ?? throw new ArgumentException(nameof(parameter));

            if (Model.Contents != null) Model.Contents.Clear();

            Model.SetContents(contents);

            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #region Get Commands

        #region Get Value

        /// <summary>
        /// Gets or sets the command to get a value from the repository.
        /// </summary>
        public ReturnCommand<ICollection<TValue>>? GetValueCommand { get; protected set; }

        private bool getValue = false;
        private bool CanGetValue(object parameter) => getValue;

        /// <summary>
        /// Gets a value from the repository.
        /// </summary>
        /// <param name="parameter">The key to retrieve the value.</param>
        /// <returns>The value associated with the key.</returns>
        private ICollection<TValue> GetValue(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            TKey key = (TKey)parameter ?? throw new ArgumentException(nameof(parameter));

            return Model.GetValue(key) ?? throw new ArgumentException(nameof(key));
        }

        #endregion

        #region Get Values

        /// <summary>
        /// Gets or sets the command to get all values from the repository.
        /// </summary>
        public ReturnCommand<ICollection<ICollection<TValue>>>? GetValuesCommand { get; protected set; }

        private bool getValues = false;
        private bool CanGetValues(object parameter) => getValues;

        /// <summary>
        /// Gets all values from the repository.
        /// </summary>
        /// <returns>All values in the repository.</returns>
        private ICollection<ICollection<TValue>> GetValues(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));

            return Model.GetValues() ?? throw new ArgumentNullException(nameof(Model.GetValues));
        }

        #endregion

        #region Get Key

        /// <summary>
        /// Gets or sets the command to get a key from the repository.
        /// </summary>
        public ReturnCommand<TKey>? GetKeyCommand { get; protected set; }

        private bool getKey = false;
        private bool CanGetKey(object parameter) => getKey;

        /// <summary>
        /// Gets a key from the repository.
        /// </summary>
        /// <param name="parameter">The value to retrieve the key.</param>
        /// <returns>The key associated with the value.</returns>
        private TKey GetKey(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            ICollection<TValue> col = (ICollection<TValue>)parameter ?? throw new ArgumentException(nameof(parameter));

            return Model.GetKey(col) ?? throw new ArgumentException(nameof(col));
        }

        #endregion

        #region Get Keys

        /// <summary>
        /// Gets or sets the command to get all keys from the repository.
        /// </summary>
        public ReturnCommand<ICollection<TKey>>? GetKeysCommand { get; protected set; }

        private bool getKeys = false;
        private bool CanGetKeys(object parameter) => getKeys;

        /// <summary>
        /// Gets all keys from the repository.
        /// </summary>
        /// <returns>All keys in the repository.</returns>
        private ICollection<TKey> GetKeys(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));

            return Model.GetKeys() ?? throw new ArgumentNullException(nameof(Model.GetKeys));
        }

        #endregion

        #endregion

        #region Add Commands

        #region Add

        /// <summary>
        /// Gets or sets the command to add a key-value pair to the repository.
        /// </summary>
        public BasicCommand? AddCommand { get; protected set; }

        private bool canAdd = false;
        private bool CanAdd(object parameter) => canAdd;

        /// <summary>
        /// Adds a key-value pair to the repository.
        /// </summary>
        /// <param name="parameter">An array containing key and value to add.</param>
        private void Add(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            ICollection<TValue> value = (ICollection<TValue>)parameters[1] ?? throw new ArgumentException(nameof(parameters));

            Model.Add(key, value);

            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #region Add Key

        /// <summary>
        /// Gets or sets the command to add a key to the repository.
        /// </summary>
        public BasicCommand? AddKeyCommand { get; protected set; }

        private bool canAddKey = false;
        private bool CanAddKey(object parameter) => canAddKey;

        /// <summary>
        /// Adds a key to the repository.
        /// </summary>
        /// <param name="parameter">The key to add.</param>
        private void AddKey(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            TKey key = (TKey)parameter ?? throw new ArgumentException(nameof(parameter));

            Model.AddKey(key);

            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #region Value Add

        /// <summary>
        /// Gets or sets the command to add a value to an existing key in the repository.
        /// </summary>
        public BasicCommand? ValueAddCommand { get; protected set; }

        private bool canValueAdd = false;
        private bool CanValueAdd(object parameter) => canValueAdd;

        /// <summary>
        /// Adds a value to an existing key in the repository.
        /// </summary>
        /// <param name="parameter">An array containing key and value to add.</param>
        private void ValueAdd(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            TValue value = (TValue)parameters[1] ?? throw new ArgumentException(nameof(parameters));

            Model.ValueAdd(key, value);
            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #endregion

        #region Remove Commands

        #region Remove

        /// <summary>
        /// Gets or sets the command to remove a key from the repository.
        /// </summary>
        public BasicCommand? RemoveCommand { get; protected set; }

        private bool canRemove = false;
        private bool CanRemove(object parameter) => canRemove;

        /// <summary>
        /// Removes a key from the repository.
        /// </summary>
        /// <param name="parameter">The key to remove.</param>
        private void Remove(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            TKey key = (TKey)parameter ?? throw new ArgumentException(nameof(parameter));

            Model.Remove(key);

            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #region Value Remove

        /// <summary>
        /// Gets or sets the command to remove a value from an existing key in the repository.
        /// </summary>
        public BasicCommand? ValueRemoveCommand { get; protected set; }

        private bool canValueRemove = false;
        private bool CanValueRemove(object parameter) => canValueRemove;

        /// <summary>
        /// Removes a value from an existing key in the repository.
        /// </summary>
        /// <param name="parameter">An array containing key and value to remove.</param>
        private void ValueRemove(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            TValue value = (TValue)parameters[1] ?? throw new ArgumentException(nameof(parameters));

            Model.ValueRemove(key, value);
            OnPropertyChanged(nameof(Model));
        }

        #endregion

        #endregion

        #region Constructors 

        /// <summary>
        /// Default constructor for RepoViewModel.
        /// </summary>
        public RepoViewModel()
        {
        }

        /// <summary>
        /// Constructor for RepoViewModel with an initial repository.
        /// Initializes a new instance with the provided repository.
        /// </summary>
        /// <param name="data">The initial repository data for the view model.</param>
        public RepoViewModel(IRepo<TKey, TValue> data) : base(data)
        {
            setContents = true;
            getValue = true;
            getValues = true;
            getKey = true;
            getKeys = true;
            canAdd = true;
            canAddKey = true;
            canValueAdd = true;
            canRemove = true;
            canValueRemove = true;

            SetContentsCommand = new BasicCommand(SetContents, CanSetContents);

            GetValueCommand = new ReturnCommand<ICollection<TValue>>(GetValue, CanGetValue);
            GetValuesCommand = new ReturnCommand<ICollection<ICollection<TValue>>>(GetValues, CanGetValues);

            GetKeyCommand = new ReturnCommand<TKey>(GetKey, CanGetKey);
            GetKeysCommand = new ReturnCommand<ICollection<TKey>>(GetKeys, CanGetKeys);

            AddCommand = new BasicCommand(Add, CanAdd);
            AddKeyCommand = new BasicCommand(AddKey, CanAddKey);
            ValueAddCommand = new BasicCommand(ValueAdd, CanValueAdd);

            RemoveCommand = new BasicCommand(Remove, CanRemove);
            ValueRemoveCommand = new BasicCommand(ValueRemove, CanValueRemove);
        }

        #endregion
    }

}
