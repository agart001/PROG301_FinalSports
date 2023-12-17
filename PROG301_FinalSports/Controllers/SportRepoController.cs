using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PROG301_FinalSports.Controllers.YourNamespace.Controllers;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.ViewModels;

namespace PROG301_FinalSports.Controllers
{
    public class SportRepoController : BaseRepoController<Guid, Sport>
    {
        public SportRepoController()
        {
            VM = new RepoViewModel<Guid, Sport>(new SportRepo());
        }


        // GET: SportController
        public ActionResult Index()
        {
            ViewData["Keys"] = GetKeys();
            ViewData["Values"] = GetValues();
            return View(VM);
        }

        [HttpGet]
        public ActionResult ViewTeams(string sportname)
        {
            var hold = sportname;
            Sport? sport = null;
            foreach(var col in GetValues())
            {
                sport = col.Where(c => c.Name == sportname).FirstOrDefault();
                if (sport != null) break;
            }

            if (sport == null) throw new NullReferenceException(sportname);
            var rep = (IRepo<Guid, Team>)sport;
            var _json = JsonConvert.SerializeObject(sport, Formatting.Indented);
            TempData["json"] = _json;

            return RedirectToAction("Index", "Sport");
        }

        // GET: SportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
