using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLibrary.Interfaces;
using SportsLibrary.ViewModels;

namespace PROG301_FinalSports.Controllers
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using SportsLibrary.Commands;
    using SportsLibrary.Interfaces;
    using SportsLibrary.Models;

    namespace YourNamespace.Controllers
    {
        public class BaseRepoController<TKey, TValue> : Controller where TKey : notnull
        {
            internal object voidarg = new object();
            public RepoViewModel<TKey, TValue>? VM { get; set; }

            internal void SetVM(IRepo<TKey, TValue> vm) => VM = new RepoViewModel<TKey, TValue>(vm);

            internal void ResetVM<TClass>()
            {
                var json = TempData["json"] as string;
                if (json == null) throw new NullReferenceException(nameof(TempData));

                var rep = (IRepo<TKey, TValue>)JsonConvert.DeserializeObject<TClass>(json);

                if (rep == null) throw new NullReferenceException(nameof(TClass));
                SetVM(rep);
            }

            internal ICollection<TKey> GetKeys()
            {
                if(VM == null) throw new NullReferenceException(nameof(VM));
                if(VM.GetKeysCommand == null) throw new NullReferenceException(nameof(VM.GetKeysCommand));
                VM.GetKeysCommand.Execute(voidarg);
                return VM.GetKeysCommand.Result;
            }

            internal ICollection<ICollection<TValue>> GetValues()
            {
                if (VM == null) throw new NullReferenceException(nameof(VM));
                if (VM.GetValuesCommand == null) throw new NullReferenceException(nameof(VM.GetValuesCommand));
                VM.GetValuesCommand.Execute(voidarg);
                return VM.GetValuesCommand.Result;
            }

            /*
            public BaseRepoController() { }
            public BaseRepoController(IRepo<TKey, TValue> data)
            {
                if (data == null) throw new ArgumentNullException(nameof(data));

                VM = new RepoViewModel<TKey, TValue>(data);
            }
            */

            // Action method for setting contents
            public ActionResult SetContents(Dictionary<TKey, ICollection<TValue>> contents)
            {
                if(VM == null) throw new ArgumentNullException(nameof(VM));
                if(VM.SetContentsCommand == null) throw new ArgumentNullException(nameof(VM.SetContentsCommand));

                VM.SetContentsCommand.Execute(contents);
                // You can redirect to another action or return a view here if needed
                return RedirectToAction("Index");
            }

            // Action method for getting value
            public ActionResult GetValue(TKey key)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.GetValueCommand == null) throw new ArgumentNullException(nameof(VM.GetValueCommand));

                VM.GetValueCommand.Execute(key);
                ICollection<TValue> value = VM.GetValueCommand.Result;
                // You can pass the value to a view or return JSON, etc.
                return RedirectToAction("Index");
                //return Json(value, JsonRequestBehavior.AllowGet);
            }

            // Action method for getting key
            public ActionResult GetKey(ICollection<TValue> col)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.GetKeyCommand == null) throw new ArgumentNullException(nameof(VM.GetKeyCommand));


                VM.GetKeyCommand.Execute(col);
                TKey key = VM.GetKeyCommand.Result;
                // You can pass the key to a view or return JSON, etc.
                return RedirectToAction("Index");
                //return Json(key, JsonRequestBehavior.AllowGet);
            }

            // Action method for adding
            public ActionResult Add(TKey key, ICollection<TValue> value)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.AddCommand == null) throw new ArgumentNullException(nameof(VM.AddCommand));

                VM.AddCommand.Execute(new object[] { key, value });
                // You can redirect to another action or return a view here if needed
                return RedirectToAction("Index");
            }

            // Action method for value adding
            public ActionResult ValueAdd(TKey key, TValue value)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.ValueAddCommand == null) throw new ArgumentNullException(nameof(VM.ValueAddCommand));

                VM.ValueAddCommand.Execute(new object[] { key, value });
                // You can redirect to another action or return a view here if needed
                return RedirectToAction("Index");
            }

            // Action method for removing
            public ActionResult Remove(TKey key)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.RemoveCommand == null) throw new ArgumentNullException(nameof(VM.RemoveCommand));

                VM.RemoveCommand.Execute(key);
                // You can redirect to another action or return a view here if needed
                return RedirectToAction("Index");
            }

            // Action method for value removing
            public ActionResult ValueRemove(TKey key, TValue value)
            {
                if (VM == null) throw new ArgumentNullException(nameof(VM));
                if (VM.ValueRemoveCommand == null) throw new ArgumentNullException(nameof(VM.ValueRemoveCommand));

                VM.ValueRemoveCommand.Execute(new object[] { key, value });
                // You can redirect to another action or return a view here if needed
                return RedirectToAction("Index");
            }
        }
    }

}
