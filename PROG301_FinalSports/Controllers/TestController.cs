using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROG301_FinalSports.Controllers.YourNamespace.Controllers;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;
using SportsLibrary.ViewModels;

namespace PROG301_FinalSports.Controllers
{
    public class TestController : BaseRepoController<string, int>
    {
        public TestController() : base()
        {
            VM = new RepoViewModel<string, int>(new TestRepo());
        }

        //public TestController(IRepo<string , int> repo) : base(repo) { }

        // GET: TestController
        public ActionResult Index()
        {
            var keys = GetKeys();
            var values = GetValues();
            ViewData["Keys"] = keys;
            ViewData["Values"] = values;
            return View();
        }

        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestController/Create
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

        // GET: TestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestController/Edit/5
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

        // GET: TestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestController/Delete/5
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
