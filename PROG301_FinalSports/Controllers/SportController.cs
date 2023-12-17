using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROG301_FinalSports.Controllers.YourNamespace.Controllers;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.ViewModels;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace PROG301_FinalSports.Controllers
{
    public class SportController : BaseRepoController<Guid, Team>
    {
        private readonly IRepo<Guid, Team> _repo;

        // GET: SportController

        public ActionResult Index()
        {
            var json = TempData["json"] as string;
            if (json == null) throw new NullReferenceException(nameof(TempData));

            var rep = (IRepo<Guid, Team>)JsonConvert.DeserializeObject<Sport>(json);

            if (rep == null) throw new NullReferenceException(nameof(Sport));
            SetVM(rep);

            ViewData["Keys"] = GetKeys();
            ViewData["Values"] = GetValues();
            TempData["json"] = json;
            return View(VM);
        }

        public ActionResult ViewMembers(string teamname)
        {
            var _json = TempData["json"] as string;
            if (_json == null) throw new NullReferenceException(nameof(TempData));

            var rep = (IRepo<Guid, Team>)JsonConvert.DeserializeObject<Sport>(_json);

            if (rep == null) throw new NullReferenceException(nameof(Sport));
            SetVM(rep);

            var hold = GetValues();

            Team? team = null;
            foreach (var col in GetValues())
            {
                team = col.Where(c => c.Name == teamname).FirstOrDefault();
                if (team != null) break;
            }

            if (team == null) throw new NullReferenceException(teamname);
            var json = JsonConvert.SerializeObject(team, Formatting.Indented);
            TempData["json"] = json;
            return RedirectToAction("Index", "Team");
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
