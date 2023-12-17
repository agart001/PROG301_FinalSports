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
    /// Base class for view models implementing the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class BaseViewModel<T> : IViewModel<T> where T : notnull
    {
        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the specified property.
        /// </summary>
        /// <param name="name">The name of the property that changed. If not provided, the calling member's name will be used.</param>
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public T? Model { get; set; }

        public BaseViewModel() { }

        public BaseViewModel(T model)
        {
            Model = model;
        }
    }

    /// <summary>
    /// ViewModel for managing an collection of items.
    /// </summary>
    /// <typeparam name="T">Type of items in the collection.</typeparam>
    public class ICollectionViewModel<T> : BaseViewModel<ICollection<T>>
    {
        private ICollection<T> collection = null;

        #region collection

        /// <summary>
        /// Gets the collection of items.
        /// </summary>
        public ICollection<T> Col
        {
            get
            {
                GetCol.Execute(null);
                return GetCol.Result;
            }
        }

        private ReturnCommand<ICollection<T>> GetCol { get; set; }

        private bool getCol = false;

        private bool CanGetCol(object parameter) => getCol;

        private ICollection<T> ReturnCol(object parameter)
        {
            return collection;
        }

        #endregion

        #region Count

        /// <summary>
        /// Gets the count of items in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                GetCount.Execute(null);
                return GetCount.Result;
            }
        }

        private ReturnCommand<int> GetCount { get; set; }

        private bool getCount = false;

        private bool CanGetCount(object parameter) => getCount;

        private int ReturnCount(object parameter)
        {
            return collection.Count();
        }

        #endregion

        #region Content Type

        /// <summary>
        /// Gets the type of the content in the collection.
        /// </summary>
        public Type ContentType
        {
            get
            {
                if (collection != null)
                {
                    var first = collection.FirstOrDefault();
                    GetContentType.Execute(first ?? throw new NullReferenceException(nameof(first)));
                }
                return GetContentType.Result;
            }
        }

        private ReturnCommand<Type> GetContentType { get; set; }

        private bool getContentType = false;

        private bool CanGetContentType(object parameter) => getContentType;

        private Type ReturnContentType(object parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            return parameter.GetType();
        }

        #endregion

        #region Content Fields

        /// <summary>
        /// Gets the fields and their values of the content in the collection.
        /// </summary>
        public Dictionary<string, object> ContentFields
        {
            get
            {
                if (collection != null)
                {
                    var first = collection.FirstOrDefault();
                    GetContentFields.Execute(first);
                }
                return GetContentFields.Result;
            }
        }

        private ReturnCommand<Dictionary<string, object>> GetContentFields { get; set; }

        private bool getContentFields = false;
        private bool CanGetContentFields(object parameter) => getContentFields;

        private Dictionary<string, object> ReturnContentFields(object parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            Dictionary<string, object> result = new Dictionary<string, object>();

            Type type = parameter.GetType();

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                result[property.Name] = property.GetValue(parameter) ?? throw new NullReferenceException(nameof(parameter));
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ICollectionViewModel{T}"/> class.
        /// </summary>
        /// <param name="_collection">The collection of items.</param>
        /// <param name="_getCol">Flag indicating whether to retrieve the collection.</param>
        /// <param name="_getCount">Flag indicating whether to retrieve the count of items.</param>
        /// <param name="_getContentType">Flag indicating whether to retrieve the content type.</param>
        /// <param name="_getContentFields">Flag indicating whether to retrieve the content fields.</param>
        public ICollectionViewModel(ICollection<T> data) : base(data)
        {
            getCol = true;
            getCount = true;
            getContentType = true;
            getContentFields = true;

            GetCol = new ReturnCommand<ICollection<T>>(ReturnCol, CanGetCol);
            GetCount = new ReturnCommand<int>(ReturnCount, CanGetCount);
            GetContentType = new ReturnCommand<Type>(ReturnContentType, CanGetContentType);
            GetContentFields = new ReturnCommand<Dictionary<string, object>>(ReturnContentFields, CanGetContentFields);
        }
    }
}
