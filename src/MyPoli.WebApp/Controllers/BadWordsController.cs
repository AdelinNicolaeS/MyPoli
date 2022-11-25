using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoli.BusinessLogic.Implementation.BadWordOperations;
using MyPoli.Entities;
using MyPoli.WebApp.Code.Base;
using System;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary")]
    public class BadWordsController : BaseController
    {
        private readonly BadWordService badWordsService;

        public BadWordsController(ControllerDependencies dependencies, BadWordService badWordsService) : base(dependencies)
        {
            this.badWordsService = badWordsService;
        }

        public IActionResult Index()
        {
            var badWords = badWordsService.IndexToWrite("", "");
            return View(badWords);
        }

        // GET: BadWordsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BadWordsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BadWord model)
        {
            if (ModelState.IsValid)
            {
                badWordsService.CreateBadWordFromModel(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: BadWordsController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badWord = badWordsService.GetBadWordById(id);
            if (badWord == null)
            {
                return NotFound();
            }

            return View(badWord);
        }

        // POST: BadWordsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            badWordsService.DeleteBadWord(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
