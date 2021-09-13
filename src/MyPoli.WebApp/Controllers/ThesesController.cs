using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.Grades;
using MyPoli.BusinessLogic.Implementation.TeacherOperations;
using MyPoli.BusinessLogic.Implementation.ThesisOperations;
using MyPoli.Common;
using MyPoli.Common.DTOs;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;
using MyPoli.BusinessLogic.Implementation.StudentOperations;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Secretary, Teacher")]
    public class ThesesController : BaseController
    {
        private readonly ThesisService thesisService;
        private readonly GradeService gradeService;
        private readonly TeacherService teacherService;
        private readonly StudentService studentService;

        public ThesesController(ControllerDependencies dependencies, ThesisService _thesisService, GradeService _gradeService, TeacherService _teacherService, StudentService _studentService) : base(dependencies)
        {
            gradeService = _gradeService;
            thesisService = _thesisService;
            teacherService = _teacherService;
            studentService = _studentService;
        }

        // GET: Theses
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StudentSort = String.IsNullOrEmpty(sortOrder) ? "student_desc" : "";
            ViewBag.TeacherSort = sortOrder == "teacher_desc" ? "teacher_asc" : "teacher_desc";
            ViewBag.TitleSort = sortOrder == "title_desc" ? "title_asc" : "title_desc";
            ViewBag.DateSort = sortOrder == "date_desc" ? "date_asc" : "date_desc";
            ViewBag.DescriptionSort = sortOrder == "description_desc" ? "description_asc" : "description_desc";
            ViewBag.ApprovedSort = sortOrder == "approved_desc" ? "approved_asc" : "approved_desc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var theses = thesisService.IndexList(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Thesis>.Create(theses, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Theses/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            var student = studentService.GetStudentById(CurrentUser.Id);
            var model = new ThesisCreateVM()
            {
                StudentId = CurrentUser.Id,
                StudentName = student.Person.FirstName + " " + student.Person.LastName,
                StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text"),
                TeacherIds = new SelectList(teacherService.GetTeachersOneName(), "Value", "Text")
            };
            return View(model);
        }

        // POST: Theses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ThesisCreateVM model)
        {
            if (ModelState.IsValid)
            {
                thesisService.CreateThesisFromModel(model);
                return RedirectToAction(nameof(Index));
            }
            model.StudentId = CurrentUser.Id;
            model.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text");
            model.TeacherIds = new SelectList(teacherService.GetTeachersOneName(), "Value", "Text");
            return View(model);
        }

        // GET: Theses/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var thesis = thesisService.GetThesisById(id);
            if (thesis == null)
            {
                return View(Utils.NotFound);
            }
            if(CurrentUser.Roles.Contains("Student") && thesis.StudentId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            if (CurrentUser.Roles.Contains("Teacher") && thesis.TeacherId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            var model = new ThesisEditVM
            {
                StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text"),
                TeacherIds = new SelectList(teacherService.GetTeachersOneName(), "Value", "Text"),
                ApprovedByTeacher = thesis.ApprovedByTeacher,
                OldContent = thesis.Content,
                Date = thesis.Date,
                Description = thesis.Description,
                Id = thesis.Id,
                StudentId = thesis.StudentId,
                TeacherId = thesis.TeacherId,
                Title = thesis.Title
            };

            return View(model);
        }


        // POST: Theses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ThesisEditVM model)
        {
            if (id != model.Id)
            {
                return View(Utils.NotFound);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    thesisService.EditThesis(model, CurrentUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThesisExists(model.Id))
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
            model.StudentId = CurrentUser.Id;
            model.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text");
            model.TeacherIds = new SelectList(teacherService.GetTeachersOneName(), "Value", "Text");
            return View(model);
        }

        [Authorize(Roles = "Student, Secretary")]
        // GET: Theses/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var thesis = thesisService.GetThesisById(id);
            if (thesis == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Student") && thesis.StudentId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            if (CurrentUser.Roles.Contains("Teacher") && thesis.TeacherId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }

            return View(thesis);
        }

        // POST: Theses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Student, Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            thesisService.DeleteThesis(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ThesisExists(Guid id)
        {
            return thesisService.ThesisExists(id);
        }

        public IActionResult Download(Guid id)
        {
            var thesis = thesisService.GetThesisById(id);
            if (thesis == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Student") && thesis.StudentId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            if (CurrentUser.Roles.Contains("Teacher") && thesis.TeacherId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            return File(thesisService.GetContent(id), "application/pdf", thesisService.GetName(id));
        }

        public IActionResult ViewThesis(Guid id)
        {
            var thesis = thesisService.GetThesisById(id);
            if (thesis == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Student") && thesis.StudentId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            if (CurrentUser.Roles.Contains("Teacher") && thesis.TeacherId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            return File(thesisService.GetContent(id), "application/pdf");
        }

        // GET: Theses/Approve/5
        [Authorize(Roles = "Teacher")]
        public IActionResult Approve(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var thesis = thesisService.GetThesisById(id);
            if (thesis == null)
            {
                return View(Utils.NotFound);
            }
            if (thesis.TeacherId != CurrentUser.Id)
            {
                return View(Utils.Unauthorized);
            }
            return View(thesis);
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult ApproveThesis(Thesis thesis)
        {
             thesisService.ApproveThesisByTeacher(thesis.Id);
             return RedirectToAction(nameof(Index));
        }
    }
}
