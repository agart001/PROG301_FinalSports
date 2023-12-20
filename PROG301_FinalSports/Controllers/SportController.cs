using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.Utilities;
using SportsLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace PROG301_FinalSports.Controllers
{
    /// <summary>
    /// Controller for managing sports-related actions.
    /// </summary>
    public class SportController : BaseRepoController<Category, Team>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SportController"/> class.
        /// </summary>
        public SportController()
        {
            VM = new RepoViewModel<Category, Team>(new Sport());
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Displays the index view for the SportController.
        /// </summary>
        /// <param name="name">The name parameter for the index view.</param>
        /// <returns>The index view.</returns>
        public ActionResult Index(string name)
        {
            var session_json = HttpContext.Session.GetString("Repo");
            if(session_json == null) throw new NullReferenceException(nameof(session_json));

            var _json = JsonUtils.SearchJsonForStringValue(session_json, name);
            var model = JsonUtils.Deserialize<Sport>(_json);
            TempData["InitPass"] = _json;
            TempData["InitName"] = name;
            VM = new RepoViewModel<Category, Team>(model);

            ViewData["Contents"] = GetContents();
            ViewData["Name"] = name;
            return View(VM);
        }

        /// <summary>
        /// Redirects to the action for viewing members.
        /// </summary>
        /// <param name="control">The control parameter for the view members action.</param>
        /// <param name="name">The name parameter for the view members action.</param>
        /// <returns>The action result for viewing members.</returns>
        public ActionResult ViewMembers(string control, string name)
        {
            return SelectContents(control, name);
        }

        #region Category

        /// <summary>
        /// Displays the add category view for the SportController.
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
        /// Handles the HTTP POST request for adding a category in the SportController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add category request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(IFormCollection collection)
        {
            var mod = TempData["ActionPass"] as string;
            if(mod == null) throw new NullReferenceException(nameof(mod));

            var _name = TempData["ActionName"] as string;
            if(_name == null) throw new NullReferenceException(nameof(_name));

            ResetVMModel<Sport>(mod);

            var name = collection["Name"];
            var description = collection["Description"];
            var category = new Category(name, description);
            AddKey(category);

            var _json = SerializeVM();

            var session_json = HttpContext.Session.GetString("Repo");
            if(session_json == null) throw new NullReferenceException(nameof(session_json));

            var updated = JsonUtils.ReplaceJsonStringValue(session_json, _name, _json);
            HttpContext.Session.SetString("Repo", updated);

            return RedirectToAction("Index", new { name = _name });
        }

        #endregion

        #region Team

        /// <summary>
        /// Displays the add team view for the SportController.
        /// </summary>
        /// <param name="json">The JSON data parameter for the add team view.</param>
        /// <returns>The add team view.</returns>
        public ActionResult AddTeam(string json)
        {
            var name = TempData["InitName"];
            var mod = TempData["InitPass"];
            TempData["ActionPass"] = mod;
            TempData["ActionName"] = name;
            TempData["key"] = json;
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for adding a team in the SportController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add team request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeam(IFormCollection collection)
        {
            var json = TempData["key"] as string 
                ?? throw new NullReferenceException(nameof(TempData));

            var key = JsonUtils.Deserialize<Category>(json);

            var mod = TempData["ActionPass"] as string 
                ?? throw new NullReferenceException(nameof(TempData));

            var _name = TempData["ActionName"] as string 
                ?? throw new NullReferenceException(nameof(TempData));

            ResetVMModel<Sport>(mod);

            var name = collection["Name"];
            var description = collection["Description"];
            var wins = int.Parse(collection["Wins"]);
            var loses = int.Parse(collection["Loses"]);

            var team = new Team(name, description, wins, loses);
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
