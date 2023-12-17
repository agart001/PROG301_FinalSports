using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SportsLibrary.Interfaces;
using SportsLibrary.ViewModels;

namespace PROG301_FinalSports.Controllers
{
    public class BaseViewModelController<TModel, TData> : Controller where TData : class
    {
        internal object voidarg = new object();
        internal TModel? VM { get; set; }
        internal virtual void SetVM(TData data)
        {
            throw new NotImplementedException();
        }

        internal string ResetRepo<TClass>()
        {
            var hold = typeof(TData);
            var json = TempData["json"] as string;
            if (json == null) throw new NullReferenceException(nameof(TempData));

            var model = JsonConvert.DeserializeObject<TClass>(json);

            if (model == null) throw new NullReferenceException(nameof(TClass));
            SetVM(model as TData);
            return json;
        }

        internal void PassRepo<TClass>(TClass obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            TempData["json"] = json;
        }
    }
}
