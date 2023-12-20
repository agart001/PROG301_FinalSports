using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.Utilities;
using SportsLibrary.ViewModels;

namespace PROG301_FinalSports.Controllers
{
    /// <summary>
    /// Controller for managing teams and their categories.
    /// </summary>
    public class TeamController : BaseRepoController<Category, BasePerson>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamController"/> class.
        /// </summary>
        public TeamController()
        {
            VM = new RepoViewModel<Category, BasePerson>(new Team());
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Displays the index view for the TeamController.
        /// </summary>
        /// <param name="name">The name parameter for the index view.</param>
        /// <returns>The index view.</returns>
        public ActionResult Index(string name)
        {
            var session_json = HttpContext.Session.GetString("Repo") 
                ?? throw new NullReferenceException(nameof(HttpContext.Session));
            var _json = JsonUtils.SearchJsonForStringValue(session_json, name);
            var model = JsonUtils.Deserialize<Team>(_json);
            TempData["InitPass"] = _json;
            TempData["InitName"] = name;
            VM = new RepoViewModel<Category, BasePerson>(model);

            ViewData["Contents"] = GetContents();
            ViewData["Name"] = name;
            return View(VM);
        }

        /// <summary>
        /// Redirects to the action for viewing a member.
        /// </summary>
        /// <param name="control">The control parameter for the view member action.</param>
        /// <param name="json">The JSON data parameter for the view member action.</param>
        /// <returns>The action result for viewing a member.</returns>
        public ActionResult ViewMember(string control, string json)
        {
            var name = TempData["InitName"];
            TempData["InitOtherData"] = name;
            return SelectContents(control, json);
        }

        #region Category

        /// <summary>
        /// Displays the add category view for the TeamController.
        /// </summary>
        /// <returns>The add category view.</returns>
        public ActionResult AddCategory()
        {
            var name = TempData["InitName"];
            var mod = TempData["InitPass"];
            TempData["ActionPass"] = mod;
            TempData["ActionName"] = name;
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for adding a category in the TeamController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add category request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(IFormCollection collection)
        {
            var mod = TempData["ActionPass"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            var _name = TempData["ActionName"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            ResetVMModel<Team>(mod);

            var name = collection["Name"];
            var description = collection["Description"];
            var category = new Category(name, description);
            AddKey(category);

            var _json = SerializeVM();
            var session_json = HttpContext.Session.GetString("Repo") 
                ?? throw new NullReferenceException(nameof(HttpContext.Session));

            var updated = JsonUtils.ReplaceJsonStringValue(session_json, _name, _json);
            HttpContext.Session.SetString("Repo", updated);

            return RedirectToAction("Index", new { name = _name });
        }

        #endregion

        #region Member

        /// <summary>
        /// Displays the add member view for the TeamController.
        /// </summary>
        /// <param name="json">The JSON data parameter for the add member view.</param>
        /// <returns>The add member view.</returns>
        public ActionResult AddMember(string json)
        {
            var name = TempData["InitName"];
            var mod = TempData["InitPass"];
            TempData["ActionPass"] = mod;
            TempData["ActionName"] = name;
            TempData["key"] = json;
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for adding a member in the TeamController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add member request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(IFormCollection collection)
        {
            var json = TempData["key"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            var key = JsonUtils.Deserialize<Category>(json);

            var mod = TempData["ActionPass"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            var _name = TempData["ActionName"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            ResetVMModel<Team>(mod);

            var firstName = collection["FirstName"];
            var lastName = collection["LastName"];
            var age = int.Parse(collection["Age"]);
            var position = collection["Position"];

            var team = new BasePerson(firstName, lastName, age, position);
            ValueAdd(key, team);

            var _json = SerializeVM();
            var session_json = HttpContext.Session.GetString("Repo") 
                ?? throw new NullReferenceException(nameof(HttpContext.Session));

            var updated = JsonUtils.ReplaceJsonStringValue(session_json, _name, _json);
            HttpContext.Session.SetString("Repo", updated);

            return RedirectToAction("Index", new { name = _name });
        }

        #endregion

        #endregion
    }
}
