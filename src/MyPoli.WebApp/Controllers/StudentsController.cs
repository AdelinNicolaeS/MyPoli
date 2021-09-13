using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.Grades;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.Common;
using MyPoli.DataAccess;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Secretary")]
    public class StudentsController : BaseController
    {
        private readonly StudentService studentService;
        private readonly GradeService gradeService;

        public StudentsController(ControllerDependencies dependencies, StudentService studentService, GradeService gradeService) : base(dependencies)
        {
            this.studentService = studentService;
            this.gradeService = gradeService;
        }

        // GET: Students
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
            ViewBag.LastNameSort = sortOrder == "last_asc" ? "last_desc" : "last_asc";
            ViewBag.EmailSort = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            ViewBag.NationalitySort = sortOrder == "nationality_asc" ? "nationality_desc" : "nationality_asc";
            ViewBag.StartDateSort = sortOrder == "startdate_asc" ? "startdate_desc" : "startdate_asc";
            ViewBag.EndDateSort = sortOrder == "enddate_asc" ? "enddate_desc" : "enddate_asc";
            ViewBag.GroupSort = sortOrder == "group_asc" ? "group_desc" : "group_asc";
            ViewBag.StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var students = studentService.IndexToWrite(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Student>.Create(students, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Students/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var student = studentService.GetStudentById(id);
            if (student == null)
            {
                return View(Utils.NotFound);
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Secretary")]
        public IActionResult Create()
        {
            var model = new StudentCreateVM
            {
                GroupIds = new SelectList(studentService.GetGroups(), "Id", "Name"),
                StatusIds = new SelectList(studentService.GetStatuses(), "Id", "Name"),
                NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name"),
                GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name"),
                SubjectIds = new MultiSelectList(studentService.GetSubjects(), "Id", "Name")
            };
            return View(model);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary")]
        public IActionResult Create(StudentCreateVM studentVM)
        {
            studentVM.GroupIds = new SelectList(studentService.GetGroups(), "Id", "Name");
            studentVM.StatusIds = new SelectList(studentService.GetStatuses(), "Id", "Name");
            studentVM.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name");
            studentVM.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name");
            studentVM.SubjectIds = new MultiSelectList(studentService.GetSubjects(), "Id", "Name");
            if (ModelState.IsValid)
            {
                Request.Form.TryGetValue("SubjectIds", out var SubjectIds);
                studentService.CreateStudentFromModel(studentVM, SubjectIds);
                return RedirectToAction(nameof(Index));
            }
            return View(studentVM);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var student = studentService.GetStudentById(id);

            if (student == null)
            {
                return View(Utils.NotFound);
            }

            var studentEdit = new StudentEditVM()
            {
                Id = student.Id,
                Address = student.Person.Address,
                Birthday = student.Person.Birthday,
                Email = student.Person.Email,
                EndDate = student.EndDate,
                FirstName = student.Person.FirstName,
                GenderId = student.Person.GenderId,
                GroupId = student.GroupId,
                StartDate = student.StartDate,
                StatusId = student.StatusId,
                LastName = student.Person.LastName,
                NationalityId = student.Person.NationalityId,
                Phone = student.Person.Phone,
                RoleId = student.Person.RoleId,
                SubjectIds = null,
            };
            studentEdit.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name", studentEdit.GenderId);
            studentEdit.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name", studentEdit.NationalityId);
            studentEdit.GroupIds = new SelectList(studentService.GetGroups(), "Id", "Name", studentEdit.GroupId);
            studentEdit.StatusIds = new SelectList(studentService.GetStatuses(), "Id", "Name", studentEdit.StatusId);
            var selectedSubjectIds = student.StudentSubjects.Select(st => st.IdSubject);
            ViewData["AllSubjectIds"] = studentService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });
            return View(studentEdit);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, StudentEditVM studentEdit)
        {
            if (id != studentEdit.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Request.Form.TryGetValue("SubjectIds", out var SubjectIds);
                    var subjectGuids = SubjectIds.Select(si => Guid.Parse(si));
                    studentService.EditStudent(studentEdit, subjectGuids);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentEdit.Id))
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
            var student = studentService.GetStudentById(id);
            studentEdit.FirstName = student.Person.FirstName;
            studentEdit.LastName = student.Person.LastName;
            studentEdit.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name");
            studentEdit.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name");
            studentEdit.GroupIds = new SelectList(studentService.GetGroups(), "Id", "Name");
            studentEdit.StatusIds = new SelectList(studentService.GetStatuses(), "Id", "Name");
            var selectedSubjectIds = student.StudentSubjects.Select(st => st.IdSubject);
            ViewData["AllSubjectIds"] = studentService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });
            return View(studentEdit);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }
            var student = studentService.GetStudentById(id);
            if (student == null)
            {
                return View(Utils.NotFound);
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            studentService.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Guid id)
        {
            return studentService.GetStudents().Any(e => e.Id == id);
        }

        // GET: Students/EditYourself/5
        [Authorize(Roles = "Student")]
        public IActionResult EditYourself()
        {
           
            var student = studentService.GetStudentById(CurrentUser.Id);

            if (student == null)
            {
                return View(Utils.NotFound);
            }

            var studentEdit = new StudentEditYourselfVM()
            {
                Id = student.Id,
                Address = student.Person.Address,
                Birthday = student.Person.Birthday,
                Email = student.Person.Email,
                EndDate = student.EndDate,
                FirstName = student.Person.FirstName,
                GenderId = student.Person.GenderId,
                StartDate = student.StartDate,
                LastName = student.Person.LastName,
                NationalityId = student.Person.NationalityId,
                Phone = student.Person.Phone,
                RoleId = student.Person.RoleId,
            };
            studentEdit.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name", studentEdit.GenderId);
            studentEdit.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name", studentEdit.NationalityId);
            return View(studentEdit);
        }

        // POST: Students/EditYourself/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public IActionResult EditYourself(StudentEditYourselfVM studentEdit)
        {
            studentEdit.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name", studentEdit.GenderId);
            studentEdit.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name", studentEdit.NationalityId);
            if (CurrentUser.Id != studentEdit.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    studentService.EditYourself(studentEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(studentEdit.Id))
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

         
            studentEdit.GenderIds = new SelectList(studentService.GetGenders(), "Id", "Name", studentEdit.GenderId);
            studentEdit.NationalityIds = new SelectList(studentService.GetNationalities(), "Id", "Name", studentEdit.NationalityId);
            return View(studentEdit);
        }

        [Authorize(Roles = "Student")]
        // GET: Students/Catalog/5
        public IActionResult Catalog(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SubjectNameSort = String.IsNullOrEmpty(sortOrder) ? "subject_desc" : "";
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
            var grades = gradeService.GetGradesOfStudent(sortOrder, SearchString, CurrentUser.Id);
            if (grades == null)
            {
                return View(Utils.NotFound);
            }
            return View(PaginatedList<Grade>.Create(grades, pageNumber ?? 1, Utils.PageSize));
        }

    }
}
