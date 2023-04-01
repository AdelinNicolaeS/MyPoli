using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.Grades;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.BusinessLogic.Implementation.SubjectOperations;
using MyPoli.Common;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;
using MyPoli.BusinessLogic.Implementation.NotificationOperations;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary, Teacher")]
    public class GradesController : BaseController
    {
        private readonly GradeService gradeService;
        private readonly SubjectService subjectService;
        private readonly StudentService studentService;
        private readonly NotificationService notificationService;
        public GradesController(ControllerDependencies dependencies, GradeService gradeService, SubjectService subjectService, StudentService studentService, NotificationService notificationService) : base(dependencies)
        {
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.studentService = studentService;
            this.notificationService = notificationService;
        }


        // GET: Grades
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SubjectSort = sortOrder == "subject_asc" ? "subject_desc" : "subject_asc";
            ViewBag.GradeSort = sortOrder == "grade_asc" ? "grade_desc" : "grade_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var grades = gradeService.IndexToWrite(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Grade>.Create(grades, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Grades
        public IActionResult Archive(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SubjectSort = sortOrder == "subject_asc" ? "subject_desc" : "subject_asc";
            ViewBag.GradeSort = sortOrder == "grade_asc" ? "grade_desc" : "grade_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var grades = gradeService.IndexToWriteArchive(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Grade>.Create(grades, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Grades/Details/5
        //public IActionResult Details(Guid? idSubject, Guid? idStudent)
        //{
        //    if (idSubject == null || idStudent == null)
        //    {
        //        return View(Utils.NotFound);
        //    }

        //    var grade = gradeService.GetGradeByIds(idSubject, idStudent);

        //    if (grade == null)
        //    {
        //        return View(Utils.NotFound);
        //    }

        //    if(CurrentUser.Roles.Contains("Teacher"))
        //    {
        //        var grades = gradeService.IndexToWrite("", "", CurrentUser);
        //        if(!grades.Contains(grade))
        //        {
        //            return View(Utils.Unauthorized);
        //        }
        //    }

        //    return View(grade);
        //}

        // GET: Grades/Create
        public IActionResult Create()
        {
            var model = new GradeVM
            {
                SubjectIds = new SelectList(gradeService.GetSubjectsByTeacher(CurrentUser), "Id", "Name"),
                GroupIds = new SelectList(gradeService.GetGroupsByTeacher(CurrentUser), "Id", "Name"),
                StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text")
            };
            return View(model);
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GradeVM gradeVM)
        {
            if (ModelState.IsValid)
            {
                gradeService.AddGradeFromModel(gradeVM, CurrentUser.Id);
                notificationService.CreateGradeNotification(gradeVM.IdStudent);
                return RedirectToAction(nameof(Index));
            }
            gradeVM.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text");
            gradeVM.SubjectIds = new SelectList(gradeService.GetSubjectsByTeacher(CurrentUser), "Id", "Name");
            gradeVM.GroupIds = new SelectList(gradeService.GetGroupsByTeacher(CurrentUser), "Id", "Name");
            return View(gradeVM);
        }

        // GET: Grades/Edit/5
        public IActionResult Edit(Guid? idSubject, Guid? idStudent)
        {
            if (idSubject == null || idStudent == null)
            {
                return View(Utils.NotFound);
            }
            var subject = subjectService.GetSubjectById(idSubject);
            var student = studentService.GetStudentById(idStudent);
            if(subject == null || student == null || subject.IsDeleted || student.Person.IsDeleted)
            {
                return View(Utils.NotFound);
            }

            var grade = gradeService.GetGradeByIds(idSubject, idStudent);
            if (grade == null)
            {
                return View(Utils.NotFound);
            }

            if (CurrentUser.Roles.Contains("Teacher"))
            {
                var grades = gradeService.IndexToWrite("", "", CurrentUser);
                if (!grades.Contains(grade))
                {
                    return View(Utils.Unauthorized);
                }
            }
            var gradeVM = new GradeVM()
            {
                GradeValue = grade.GradeValue,
                IdStudent = grade.IdStudent,
                IdSubject = grade.IdSubject,
                IdGroup = grade.IdGroup
            };
            gradeVM.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text");
            gradeVM.SubjectIds = new SelectList(gradeService.GetSubjectsByTeacher(CurrentUser), "Id", "Name");
            gradeVM.GroupIds = new SelectList(gradeService.GetGroupsByTeacher(CurrentUser), "Id", "Name");
            return View(gradeVM);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid idSubject, Guid idStudent, GradeVM gradeVM)
        {
            if (idStudent != gradeVM.IdStudent || idSubject != gradeVM.IdSubject)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    gradeService.EditGradeFromModel(gradeVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(gradeVM.IdSubject, gradeVM.IdStudent))
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
            gradeVM.StudentIds = new SelectList(gradeService.GetStudentsOneName(), "Value", "Text");
            gradeVM.SubjectIds = new SelectList(gradeService.GetSubjects(), "Id", "Name");
            gradeVM.GroupIds = new SelectList(gradeService.GetGroupsByTeacher(CurrentUser), "Id", "Name");
            return View(gradeVM);
        }

        // GET: Grades/Delete/5
        public IActionResult Delete(Guid? idSubject, Guid? idStudent)
        {
            if (idSubject == null || idStudent == null)
            {
                return View(Utils.NotFound);
            }
            if (idSubject == null || idStudent == null)
            {
                return View(Utils.NotFound);
            }
            var subject = subjectService.GetSubjectById(idSubject);
            var student = studentService.GetStudentById(idStudent);
            if (subject == null || student == null || subject.IsDeleted || student.Person.IsDeleted)
            {
                return View(Utils.NotFound);
            }

            var grade = gradeService.GetGradeByIds(idSubject, idStudent);
            if (grade == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Teacher"))
            {
                var grades = gradeService.IndexToWrite("", "", CurrentUser);
                if (!grades.Contains(grade))
                {
                    return View(Utils.Unauthorized);
                }
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid idSubject, Guid idStudent)
        {
            gradeService.DeleteGrade(idSubject, idStudent);
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(Guid idSubject, Guid idStudent)
        {
            return gradeService.GradeExists(idSubject, idStudent);
        }

        public JsonResult GetSubjectsOfStudent(Guid IdStudent)
        {
            var list = gradeService.GetSubjectsOfStudent(IdStudent, CurrentUser).Select(s => new SelectListItem() { 
                Value = s.Id.ToString(),
                Text = s.Name
            });
            return Json(list);
        }
        public JsonResult GetStudentsOfSubjectAndGroup(Guid IdSubject, Guid IdGroup)
        {
            var list = gradeService.GetStudentsOfSubjectAndGroup(IdSubject, IdGroup, CurrentUser).Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Person.FirstName + " " + s.Person.LastName
            }).OrderBy(s => s.Text);
            return Json(list);
        }
    }
}
