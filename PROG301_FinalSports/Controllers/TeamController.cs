﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROG301_FinalSports.Controllers.YourNamespace.Controllers;
using SportsLibrary.Interfaces;
using SportsLibrary.Models;

namespace PROG301_FinalSports.Controllers
{
    public class TeamController : BaseRepoController<Type, object>
    {
        // GET: TeamController
        public ActionResult Index()
        {
            var json = TempData["json"] as string;
            if (json == null) throw new NullReferenceException(nameof(TempData));

            var rep = (IRepo<Type, object>)JsonConvert.DeserializeObject<Team>(json);

            if(rep == null) throw new NullReferenceException(nameof(Team));
            SetVM(rep);

            ViewData["Keys"] = GetKeys();
            ViewData["Values"] = GetValues();
            var hold = GetValues();

            return View(VM);
        }

        // GET: TeamController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamController/Create
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

        // GET: TeamController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeamController/Edit/5
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

        // GET: TeamController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeamController/Delete/5
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