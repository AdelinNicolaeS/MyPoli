using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.SubjectOperations;
using MyPoli.Common;
using MyPoli.DataAccess;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Secretary, Teacher")]
    public class SubjectsController : BaseController
    {
        private readonly SubjectService subjectService;

        public SubjectsController(ControllerDependencies dependencies, SubjectService subjectService) : base(dependencies)
        {
            this.subjectService = subjectService;
        }

        // GET: Subjects
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var subjects = subjectService.IndexToWrite(sortOrder, searchString, CurrentUser);
            return View(PaginatedList<Subject>.Create(subjects, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Subjects/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var subject = subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return View(Utils.NotFound);
            }
            var subjects = subjectService.IndexToWrite("", "", CurrentUser);
            if(!subjects.Contains(subject))
            {
                return View(Utils.Unauthorized);
            }
            return View(subject);
        }

        // GET: Subjects/Create
        [Authorize(Roles = "Secretary")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subjectService.AddSubject(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var subject = subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return View(Utils.NotFound);
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name")] Subject subject)
        {
            if (id != subject.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    subjectService.Update(subject);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
                    {
                        return View(Utils.NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var subject = subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return View(Utils.NotFound);
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            subjectService.RemoveSubject(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(Guid id)
        {
            return subjectService.SubjectExists(id);
        }

        public JsonResult GetSubjects()
        {
            var subjects = subjectService.GetSubjects().Select(e=> new SelectListItem()
            {
                Text = e.Name,
                Value = e.Id.ToString()
            });
            return Json(null);
        }
    }
}
