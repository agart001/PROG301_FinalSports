using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsLibrary.Interfaces;
using SportsLibrary.ViewModels;
using SportsLibrary.Utilities;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using SportsLibrary.Models;
using Microsoft.VisualBasic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PROG301_FinalSports.Controllers
{
    /// <summary>
    /// Represents a base controller for view models. Inherits from <see cref="Controller"/>.
    /// </summary>
    /// <typeparam name="TModel">The type of the view model.</typeparam>
    /// <typeparam name="TData">The type of the data associated with the view model, which is <see langword="notnull"/>.</typeparam>
    public class BaseViewModelController<TModel, TData> : Controller where TData : class
    {
        #region Internal Fields

        /// <summary>
        /// An internal object used for methods with no arguments.
        /// </summary>
        internal object voidarg = new object();

        /// <summary>
        /// The view model associated with the controller.
        /// </summary>
        internal TModel? VM { get; set; }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Sets the view model using the provided data.
        /// This method is meant to be overridden by derived classes.
        /// </summary>
        /// <param name="data">The data to set the view model.</param>
        internal virtual void SetVM(TData data)
        {
            // This method is intentionally not implemented in the base class.
            // It should be overridden in derived classes to provide specific functionality.
            throw new NotImplementedException();
        }

        #endregion
    }


    /// <summary>
    /// Represents a base controller for repositories. Inherits from <see cref="BaseViewModelController{TModel, TData}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the repository, which are <see langword="notnull"/>.</typeparam>
    /// <typeparam name="TValue">The type of values in the repository.</typeparam>
    public abstract class BaseRepoController<TKey, TValue> : BaseViewModelController<RepoViewModel<TKey, TValue>, IRepo<TKey, TValue>> where TKey : notnull
    {
        #region VM Methods

        /// <summary>
        /// Sets the view model using the provided repository.
        /// </summary>
        /// <param name="vm">The repository to set the view model.</param>
        internal override void SetVM(IRepo<TKey, TValue> vm) => VM = new RepoViewModel<TKey, TValue>(vm);

        /// <summary>
        /// Serializes the view model to JSON.
        /// </summary>
        /// <returns>Serialized JSON representation of the view model.</returns>
        internal string SerializeVM()
        {
            if (VM == null) throw new NullReferenceException(nameof(VM));

            var vm = VM as IViewModel<IRepo<TKey, TValue>>;
            if (vm == null || vm.GetModelCommand == null) throw new NullReferenceException(nameof(vm.GetModelCommand));

            vm.GetModelCommand.Execute(voidarg);
            var model = vm.GetModelCommand.Result;

            var json = JsonUtils.Serialize(model);
            if (json == null) throw new NullReferenceException(nameof(json));
            return json;
        }

        /// <summary>
        /// Serializes the contents of the view model to JSON.
        /// </summary>
        /// <returns>Serialized JSON representation of the view model contents.</returns>
        internal virtual string SerializeVMContents()
        {
            if (VM == null) throw new NullReferenceException(nameof(VM));

            var vm = VM as IViewModel<IRepo<TKey, TValue>>;
            if (vm == null || vm.GetModelCommand == null) throw new NullReferenceException(nameof(vm.GetModelCommand));

            vm.GetModelCommand.Execute(voidarg);

            if (vm.GetModelCommand.Result == null) throw new NullReferenceException(nameof(vm.GetModelCommand.Result));

            var contents = vm.GetModelCommand.Result.Contents;

            var json = JsonUtils.Serialize(contents);
            if (json == null) throw new NullReferenceException(nameof(json));
            return json;
        }

        /// <summary>
        /// Resets the view model contents using the provided JSON.
        /// </summary>
        /// <param name="json">JSON representation of the contents to reset.</param>
        internal void ResetVMContents(string json)
        {
            var contents = JsonUtils.Deserialize<List<KeyValuePair<TKey, ICollection<TValue>>>>(json);
            if (VM != null && VM.SetContentsCommand != null)
                VM.SetContentsCommand.Execute(contents);
        }

        /// <summary>
        /// Resets the view model using the provided JSON for a specific model type.
        /// </summary>
        /// <typeparam name="TModel">The type of model to reset the view model with.</typeparam>
        /// <param name="json">JSON representation of the model to reset.</param>
        internal void ResetVMModel<TModel>(string json)
        {
            var model = JsonUtils.Deserialize<TModel>(json);
            if (model == null) throw new NullReferenceException(nameof(json));

            var cast = (IRepo<TKey, TValue>)model;
            if (cast == null) throw new NullReferenceException(nameof(model));

            VM = new RepoViewModel<TKey, TValue>(cast);
        }

        #endregion

        #region ActionResult Methods

        /// <summary>
        /// Selects contents from the view model based on the provided control and name.
        /// </summary>
        /// <param name="control">The control to redirect to.</param>
        /// <param name="_name">The name parameter for the redirect.</param>
        /// <returns>ActionResult representing the redirect action.</returns>
        public ActionResult SelectContents(string control, string _name)
        {
            var data = TempData["InitOtherData"];
            TempData["PassOtherData"] = data;
            return RedirectToAction("Index", control, new
            {
                name = _name
            });
        }

        #endregion

        #region Command Methods

        #region Get

        /// <summary>
        /// Gets the contents from the view model.
        /// </summary>
        /// <returns>The contents of the view model.</returns>
        internal ICollection<KeyValuePair<TKey, ICollection<TValue>>> GetContents()
        {
            if (VM == null) throw new NullReferenceException(nameof(VM));
            if (VM.GetModelCommand == null) throw new NullReferenceException(nameof(VM.GetModelCommand));

            VM.GetModelCommand.Execute(voidarg);
            var model = VM.GetModelCommand.Result;

            if (model == null) throw new NullReferenceException(nameof(model));

            if (model.Contents == null) throw new NullReferenceException(nameof(model.Contents));

            return model.Contents;
        }

        /// <summary>
        /// Gets the keys from the view model.
        /// </summary>
        /// <returns>The keys of the view model.</returns>
        internal ICollection<TKey>? GetKeys()
        {
            if (VM == null) throw new NullReferenceException(nameof(VM));
            if (VM.GetKeysCommand == null) throw new NullReferenceException(nameof(VM.GetKeysCommand));

            VM.GetKeysCommand.Execute(voidarg);

            return VM.GetKeysCommand.Result;
        }

        /// <summary>
        /// Gets the values from the view model.
        /// </summary>
        /// <returns>The values of the view model.</returns>
        internal ICollection<ICollection<TValue>>? GetValues()
        {
            if (VM == null) throw new NullReferenceException(nameof(VM));
            if (VM.GetValuesCommand == null) throw new NullReferenceException(nameof(VM.GetValuesCommand));

            VM.GetValuesCommand.Execute(voidarg);

            return VM.GetValuesCommand.Result;
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds a key to the view model.
        /// </summary>
        /// <param name="key">The key to add.</param>
        internal void AddKey(TKey key)
        {
            var hold = VM;
            if (VM == null) throw new ArgumentNullException(nameof(VM));
            if (VM.AddKeyCommand == null) throw new ArgumentNullException(nameof(VM.AddKeyCommand));

            VM.AddKeyCommand.Execute(key);
        }

        /// <summary>
        /// Adds a value to the view model.
        /// </summary>
        /// <param name="key">The key to associate with the value.</param>
        /// <param name="value">The value to add.</param>
        internal void ValueAdd(TKey key, TValue value)
        {
            if (VM == null) throw new ArgumentNullException(nameof(VM));
            if (VM.ValueAddCommand == null) throw new ArgumentNullException(nameof(VM.ValueAddCommand));

            if(value == null) throw new ArgumentNullException(nameof(value));

            VM.ValueAddCommand.Execute(new object[] { key, value });
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes a key from the view model.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        internal void Remove(TKey key)
        {
            if (VM == null) throw new ArgumentNullException(nameof(VM));
            if (VM.RemoveCommand == null) throw new ArgumentNullException(nameof(VM.RemoveCommand));

            VM.RemoveCommand.Execute(key);
        }

        /// <summary>
        /// Removes a value from the view model.
        /// </summary>
        /// <param name="key">The key associated with the value to remove.</param>
        /// <param name="value">The value to remove.</param>
        internal void ValueRemove(TKey key, TValue value)
        {
            if (VM == null) throw new ArgumentNullException(nameof(VM));
            if (VM.ValueRemoveCommand == null) throw new ArgumentNullException(nameof(VM.ValueRemoveCommand));

            if (value == null) throw new ArgumentNullException(nameof(value));

            VM.ValueRemoveCommand.Execute(new object[] { key, value });
        }

        #endregion

        #endregion
    }

}
