using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.SubjectTeacherOperations;
using MyPoli.DataAccess;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary")]
    public class SubjectTeachersController : BaseController
    {
        private readonly SubjectTeacherService subjectTeacherService;

        public SubjectTeachersController(ControllerDependencies dependencies, SubjectTeacherService subjectTeacherService) : base(dependencies)
        {
            this.subjectTeacherService = subjectTeacherService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await subjectTeacherService.IndexToWriteAsync(CurrentUser));
        }

       
        // GET: SubjectTeachers/Create
        public IActionResult Create()
        {
            var model = new SubjectTeacherVM
            {
                SubjectIds = new SelectList(subjectTeacherService.GetSubjects(), "Id", "Name"),
                TeacherIds = new SelectList(subjectTeacherService.GetTeachersWithOneName(), "Id", "Name")
            };
            return View(model);
        }

        // POST: SubjectTeachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectTeacherVM subjectTeacherVM)
        {
            if (ModelState.IsValid)
            {
                var subjectTeacher = new SubjectTeacher()
                {
                    SubjectId = subjectTeacherVM.SubjectId,
                    TeacherId = subjectTeacherVM.TeacherId
                };
                subjectTeacherService.AddSubjectTeacher(subjectTeacher);
                return RedirectToAction(nameof(Index));
            }
            subjectTeacherVM.SubjectIds = new SelectList(subjectTeacherService.GetSubjects(), "Id", "Name");
            subjectTeacherVM.TeacherIds = new SelectList(subjectTeacherService.GetTeachersWithOneName(), "Id", "Name");
            return View(subjectTeacherVM);
        }

        // GET: SubjectTeachers/Delete/5
        public async Task<IActionResult> Delete(Guid? TeacherId, Guid? SubjectId)
        {
            if (TeacherId == null || SubjectId == null)
            {
                return NotFound();
            }

            var subjectTeacher = await subjectTeacherService.GetById(TeacherId, SubjectId);
            if (subjectTeacher == null)
            {
                return NotFound();
            }

            return View(subjectTeacher);
        }

        // POST: SubjectTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? TeacherId, Guid? SubjectId)
        {
            var subjectTeacher = await subjectTeacherService.GetById(TeacherId, SubjectId);
            subjectTeacherService.RemoveSubjectTeacher(subjectTeacher);
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectTeacherExists(Guid TeacherId, Guid SubjectId)
        {
            return subjectTeacherService.SubjectTeacherExists(TeacherId, SubjectId);
        }
    }
}
