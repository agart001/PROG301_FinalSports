using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SportsLibrary.Utilities;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.ViewModels;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

namespace PROG301_FinalSports.Controllers
{
    /// <summary>
    /// Controller for managing sports repositories.
    /// </summary>
    public class SportRepoController : BaseRepoController<Category, Sport>
    {
        #region Action Methods

        /// <summary>
        /// Displays the index view for the SportRepoController.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            var session_json = HttpContext.Session.GetString("Repo");

            // Check if session has existing data
            if (session_json != null)
            {
                ResetVMModel<SportRepo>(session_json);
            }
            else
            {
                // Load startup data from file if session is empty
                var baseDir = Directory.GetParent(Directory.GetCurrentDirectory());
                var path = Path.Combine($"{baseDir}\\SportsLibrary\\JSONs\\startupRepo.json");
                var startupJson = System.IO.File.ReadAllText(path);
                var startup = JsonUtils.Deserialize<SportRepo>(startupJson);

                VM = new RepoViewModel<Category, Sport>(startup);
            }

            if(VM == null || VM.GetModelCommand == null) throw new NullReferenceException(nameof(VM));

            VM.GetModelCommand.Execute(voidarg);
            var model = VM.GetModelCommand.Result 
                ?? throw new ArgumentNullException(nameof(VM.GetModelCommand.Result));
            var _json = JsonUtils.Serialize(model);

            ViewData["Contents"] = model.Contents;
            HttpContext.Session.SetString("Repo", _json);
            return View(VM);
        }

        /// <summary>
        /// Redirects to the action for viewing teams.
        /// </summary>
        /// <param name="control">The control parameter for the view teams action.</param>
        /// <param name="name">The name parameter for the view teams action.</param>
        /// <returns>The action result for viewing teams.</returns>
        public ActionResult ViewTeams(string control, string name)
        {
            return SelectContents(control, name);
        }

        #region Category

        /// <summary>
        /// Displays the add category view for the SportRepoController.
        /// </summary>
        /// <returns>The add category view.</returns>
        public ActionResult AddCategory()
        {
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for adding a category in the SportRepoController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add category request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(IFormCollection collection)
        {
            if (VM == null)
            {
                // Restore VM from session if it's null
                var rep = HttpContext.Session.GetString("Repo") 
                    ?? throw new NullReferenceException(nameof(HttpContext.Session));
                ResetVMModel<SportRepo>(rep);
            }

            var name = collection["Name"];
            var description = collection["Description"];
            var category = new Category(name, description);
            AddKey(category);

            var updated = SerializeVM();
            HttpContext.Session.SetString("Repo", updated);

            return RedirectToAction("Index");
        }

        #endregion

        #region Sport

        /// <summary>
        /// Displays the add sport view for the SportRepoController.
        /// </summary>
        /// <param name="json">The JSON data parameter for the add sport view.</param>
        /// <returns>The add sport view.</returns>
        public ActionResult AddSport(string json)
        {
            TempData["key"] = json;
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request for adding a sport in the SportRepoController.
        /// </summary>
        /// <param name="collection">The form collection from the POST request.</param>
        /// <returns>The action result after processing the add sport request.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSport(IFormCollection collection)
        {
            if (VM == null)
            {
                // Restore VM from session if it's null
                var rep = HttpContext.Session.GetString("Repo") 
                    ?? throw new NullReferenceException(nameof(HttpContext.Session));
                ResetVMModel<SportRepo>(rep);
            }

            var json = TempData["key"] as string 
                ?? throw new NullReferenceException(nameof(TempData));
            var key = JsonUtils.Deserialize<Category>(json);

            var name = collection["Name"];
            var description = collection["Description"];
            var sport = new Sport(name, description);

            ValueAdd(key, sport);

            var updated = SerializeVM();
            HttpContext.Session.SetString("Repo", updated);

            return RedirectToAction("Index");
        }

        #endregion

        #endregion
    }
}
