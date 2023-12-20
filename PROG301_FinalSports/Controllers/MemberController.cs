using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.Utilities;
using SportsLibrary.ViewModels;
using System.Xml.Linq;

namespace PROG301_FinalSports.Controllers
{
    /// <summary>
    /// Controller for managing member-related actions.
    /// </summary>
    public class MemberController : BaseViewModelController<PersonViewModel, BasePerson>
    {
        #region Internal Methods

        /// <summary>
        /// Sets the view model using the provided data.
        /// </summary>
        /// <param name="data">The data to set the view model.</param>
        internal override void SetVM(BasePerson data) => VM = new PersonViewModel(data);

        /// <summary>
        /// Checks and returns the edited string data, or null if it's empty.
        /// </summary>
        /// <param name="data">The string data to check.</param>
        /// <returns>The edited string data or null if empty.</returns>
        internal string? CheckEditString(string? data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            else return data;
        }

        /// <summary>
        /// Checks and returns the edited integer data, or null if it's empty.
        /// </summary>
        /// <param name="data">The string data to parse into an integer.</param>
        /// <returns>The edited integer data or null if empty.</returns>
        internal int? CheckEditInt(string? data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            else return int.Parse(data);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberController"/> class.
        /// </summary>
        public MemberController()
        {
            VM = new PersonViewModel(new BasePerson());
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Displays the index view for the MemberController.
        /// </summary>
        /// <param name="name">The name parameter for the index view.</param>
        /// <returns>The index view.</returns>
        public ActionResult Index(string name)
        {
            var session_json = HttpContext.Session.GetString("Repo");
            if (session_json == null) throw new NullReferenceException($"| {HttpContext.Session} : 'GetString()' : 'Repo' |");

            var _json = JsonUtils.SearchJsonForStringValue(session_json, name);
            TempData["InitPass"] = _json;
            var teamName = TempData["PassOtherData"];
            TempData["InitOtherData"] = teamName;

            var model = JsonUtils.Deserialize<BasePerson>(_json);
            VM = new PersonViewModel(model);
            ViewData["Name"] = name;
            return View(VM);
        }

        #region Edit

        /// <summary>
        /// Displays the edit view for the MemberController.
        /// </summary>
        /// <returns>The edit view.</returns>
        public ActionResult Edit()
        {
            var mod = TempData["InitPass"];
            TempData["ActionPass"] = mod;
            var teamName = TempData["InitOtherData"];
            TempData["TeamName"] = teamName;
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for editing data in the MemberController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the edit request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            var mod = TempData["ActionPass"] as string;
            if (mod == null) throw new NullReferenceException($"| {TempData} : 'ActionPass' |");
            var defModel = JsonUtils.Deserialize<BasePerson>(mod);

            var values = new dynamic?[]
            {
            CheckEditString(collection["FirstName"].FirstOrDefault()),
            CheckEditString(collection["LastName"].FirstOrDefault()),
            CheckEditInt(collection["Age"].FirstOrDefault()),
            CheckEditString(collection["Position"].FirstOrDefault())
            };

            var firstName = values[0] ?? defModel.FirstName;
            var lastName = values[1] ?? defModel.LastName;
            var age = values[2] ?? defModel.Age;
            var position = values[3] ?? defModel.Position;

            var model = new BasePerson(firstName, lastName, age, position);
            var _json = JsonUtils.Serialize(model);

            var session_json = HttpContext.Session.GetString("Repo");
            if (session_json == null) throw new NullReferenceException($"| {HttpContext.Session} : 'GetString()' : 'Repo' |");
            if (defModel.Name == null) throw new NullReferenceException(nameof(defModel));

            var updated = JsonUtils.ReplaceJsonStringValue(session_json, defModel.Name, _json);
            HttpContext.Session.SetString("Repo", updated);

            var _name = TempData["TeamName"] as string 
                ?? throw new NullReferenceException(nameof(TempData));

            return RedirectToAction("Index", "Team", new
            {
                name = _name
            });
        }

        #endregion

        #endregion
    }
}
