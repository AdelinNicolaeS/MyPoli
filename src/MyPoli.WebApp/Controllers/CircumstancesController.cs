using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.CircumstanceOperations;
using MyPoli.BusinessLogic.Implementation.Grades;
using MyPoli.BusinessLogic.Implementation.NotificationOperations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;
using MyPoli.Common.DTOs;
using MyPoli.DataAccess;
using MyPoli.Entities;

using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary, Student, Teacher")]
    public class CircumstancesController : BaseController
    {
        private readonly CircumstanceService circumstanceService;
        private readonly GradeService gradeService;
        private readonly NotificationService notificationService;

        public CircumstancesController(ControllerDependencies dependencies, CircumstanceService _circumstanceService, GradeService _gradeService, NotificationService _notificationService) : base(dependencies)
        {
            circumstanceService = _circumstanceService;
            gradeService = _gradeService;
            notificationService = _notificationService;
        }     
        // GET: Circumstances
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSort = sortOrder == "description_asc" ? "description_desc" : "description_asc";
            ViewBag.StartDateSort = sortOrder == "start_asc" ? "start_desc" : "start_asc";
            ViewBag.EndDateSort = sortOrder == "end_asc" ? "end_desc" : "end_asc";
            ViewBag.AcceptedSort = sortOrder == "accepted_asc" ? "accepted_desc" : "accepted_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var circumstances = circumstanceService.IndexToWrite(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Circumstance>.Create(circumstances, pageNumber ?? 1, Utils.PageSize));
        }

        [Authorize(Roles = "Student")]
        // GET: Circumstances/Create
        public IActionResult Create()
        {
            var model = new CircumstanceCreateVM()
            {
                StudentId = CurrentUser.Id,
                StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text")
            };
            return View(model);
        }

        // POST: Circumstances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CircumstanceCreateVM model)
        {
            if (ModelState.IsValid)
            {
                circumstanceService.CreateCircumstanceFromModel(model);
                return RedirectToAction(nameof(Index));
            }
            model.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text", model.StudentId);
            return View(model);
        }
        [Authorize(Roles = "Student, Secretary")]
        // GET: Circumstances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var circumstance = await circumstanceService.GetCircumstanceById(id);
            if (circumstance == null)
            {
                return View(Utils.NotFound);
            }
            if(CurrentUser.Roles.Contains("Student") && (circumstance.StudentId != CurrentUser.Id))
            {
                return View(Utils.Unauthorized);
            }

            var model = new CircumstanceEditVM()
            {
               // Name = circumstance.Student.Person.FirstName + " " + circumstance.Student.Person.LastName,
                StudentId = circumstance.StudentId,
                Description = circumstance.Description,
                EndDate = circumstance.EndDate,
                StartDate = circumstance.StartDate,
                Id = circumstance.Id,
                StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text")
            };
            return View(model);
        }

        // POST: Circumstances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student, Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, CircumstanceEditVM model)
        {
            if (id != model.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    circumstanceService.EditCircumstance(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircumstanceExists(model.Id))
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
            model.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text", model.StudentId);
            return View(model);
        }

        [Authorize(Roles = "Student, Secretary")]
        // GET: Circumstances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var circumstance = await circumstanceService.GetCircumstanceById(id);
            if (circumstance == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Student") && (circumstance.StudentId != CurrentUser.Id))
            {
                return View(Utils.Unauthorized);
            }

            return View(circumstance);
        }

        [Authorize(Roles = "Student, Secretary")]
        // POST: Circumstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            circumstanceService.DeleteCircumstance(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CircumstanceExists(Guid id)
        {
            return circumstanceService.CircumstanceExists(id);
        }

        [Authorize(Roles = "Secretary"), ActionName("Accept")]
        // GET: Theses/Accept/5
        public async Task<IActionResult> AcceptAsync(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var circumstance = await circumstanceService.GetCircumstanceById(id);
            if (circumstance == null)
            {
                return View(Utils.NotFound);
            }
            return View(circumstance);
        }
        [Authorize(Roles = "Secretary")]
        public IActionResult AcceptCircumstance(Circumstance circumstance)
        {
            circumstanceService.AcceptCircumstance(circumstance.Id);
            notificationService.AcceptCircumstance(circumstance.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
