using SportsLibrary.Commands;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using System.Windows.Input;

namespace SportsLibrary.ViewModels
{
    public class RepoViewModel<TKey, TValue> : BaseViewModel<IRepo<TKey, TValue>> where TKey : notnull
    {
        public ICommand? SetContentsCommand { get; protected set; }
        private bool setContents = false;
        private bool CanSetCotents(object parameter) => setContents;

        private void SetContents(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            Dictionary<TKey, ICollection<TValue>> contents = (Dictionary<TKey, ICollection<TValue>>)parameter 
                ?? throw new ArgumentException(nameof(parameter));
            Model.SetContents(contents);
        }

        public ReturnCommand<ICollection<TValue>>? GetValueCommand { get; protected set; }
        private bool getValue = false;
        private bool CanGetValue(object parameter) => getValue;
        private ICollection<TValue> GetValue(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            TKey key = (TKey)parameter ?? throw new ArgumentException(nameof(parameter));

            return Model.GetValue(key) ?? throw new ArgumentException(nameof(key));
        }

        public ReturnCommand<ICollection<ICollection<TValue>>>? GetValuesCommand { get; protected set; }
        private bool getValues = false;
        private bool CanGetValues(object parameter) => getValues;
        
        private ICollection<ICollection<TValue>> GetValues(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            return Model.GetValues() ?? throw new ArgumentNullException(nameof(Model.GetValues));
        }


        public ReturnCommand<TKey>? GetKeyCommand { get; protected set; }
        private bool getKey = false;
        private bool CanGetKey(object parameter) => getKey;
        
        private TKey GetKey(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            ICollection<TValue> col = (ICollection<TValue>)parameter ?? throw new ArgumentException(nameof(parameter));
            return Model.GetKey(col) ?? throw new ArgumentException(nameof(col));
        }

        public ReturnCommand<ICollection<TKey>>? GetKeysCommand { get; protected set; }
        private bool getKeys = false;
        private bool CanGetKeys(object parameter) => getKeys;

        private ICollection<TKey> GetKeys(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            return Model.GetKeys() ?? throw new ArgumentNullException(nameof(Model.GetKeys)); ;
        }

        public BasicCommand? AddCommand { get; protected set; }
        private bool canAdd = false;
        private bool CanAdd(object parameter) => canAdd;

        private void Add(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            ICollection<TValue> value = (ICollection<TValue>)parameters[1] ?? throw new ArgumentException(nameof(parameters));
            Model.Add(key, value);
        }

        public BasicCommand? ValueAddCommand { get; protected set; }
        private bool canValueAdd = false;
        private bool CanValueAdd(object parameter) => canValueAdd;

        private void ValueAdd(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            TValue value = (TValue)parameters[1] ?? throw new ArgumentException(nameof(parameters));
            Model.ValueAdd(key, value);
        }

        public BasicCommand? RemoveCommand { get; protected set; }
        private bool canRemove = false;
        private bool CanRemove(object parameter) => canRemove;

        private void Remove(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            TKey key = (TKey)parameter ?? throw new ArgumentException(nameof(parameter));
            Model.Remove(key);
        }

        public BasicCommand? ValueRemoveCommand { get; protected set; }
        private bool canValueRemove = false;
        private bool CanValueRemove(object parameter) => canValueRemove;

        private void ValueRemove(object parameter)
        {
            if (Model == null) throw new NullReferenceException(nameof(Model));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            object[] parameters = (object[])parameter;
            TKey key = (TKey)parameters[0] ?? throw new ArgumentException(nameof(parameters));
            TValue value = (TValue)parameters[1] ?? throw new ArgumentException(nameof(parameters));
            Model.ValueRemove(key, value);
        }

        public RepoViewModel()
        {
        }

        public RepoViewModel(IRepo<TKey, TValue> data) : base(data)
        {
            setContents = true;
            getValue = true;
            getValues = true;
            getKey = true;
            getKeys = true;
            canAdd = true;
            canValueAdd = true;
            canRemove = true;
            canValueRemove = true;

            SetContentsCommand = new BasicCommand(SetContents, CanSetCotents);

            GetValueCommand = new ReturnCommand<ICollection<TValue>>(GetValue, CanGetValue);
            GetValuesCommand = new ReturnCommand<ICollection<ICollection<TValue>>>(GetValues, CanGetValues);

            GetKeyCommand = new ReturnCommand<TKey>(GetKey, CanGetKey);
            GetKeysCommand = new ReturnCommand<ICollection<TKey>>(GetKeys, CanGetKeys);

            AddCommand = new BasicCommand(Add, CanAdd);
            ValueAddCommand = new BasicCommand(ValueAdd, CanValueAdd);

            RemoveCommand = new BasicCommand(Remove, CanRemove);
            ValueRemoveCommand = new BasicCommand(ValueRemove, CanValueRemove);
        }
    }
}
