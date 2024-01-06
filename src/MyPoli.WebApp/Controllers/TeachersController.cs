using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MyPoli.BusinessLogic.Implementation.TeacherOperations;
using MyPoli.BusinessLogic.Implementation.ThesisOperations;
using MyPoli.Common;

using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary, Teacher")]
    public class TeachersController : BaseController
    {
        private readonly TeacherService teacherService;
        private readonly ThesisService thesisService;

        public TeachersController(ControllerDependencies dependencies, TeacherService teacherService, ThesisService thesisService) : base(dependencies)
        {
            this.teacherService = teacherService;
            this.thesisService = thesisService;
        }

        // GET: Teachers
       // [Authorize(Roles = "Secretary")]
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
            ViewBag.LastNameSort = sortOrder == "last_asc" ? "last_desc" : "last_asc";
            ViewBag.EmailSort = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            ViewBag.NationalitySort = sortOrder == "nationality_asc" ? "nationality_desc" : "nationality_asc";
            ViewBag.ExperienceSort = sortOrder == "experience_asc" ? "experience_desc" : "experience_asc";
            ViewBag.SalarySort = sortOrder == "salary_asc" ? "salary_desc" : "salary_asc";
            ViewBag.PhoneSort = sortOrder == "phone_asc" ? "phone_desc" : "phone_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var teachers = teacherService.IndexToWrite(sortOrder, SearchString);
            return View(PaginatedList<Teacher>.Create(teachers, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Teachers/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Teacher") && CurrentUser.Id != id)
            {
                return View(Utils.Unauthorized);
            }

            var teacher = teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return View(Utils.NotFound);
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize(Roles = "Secretary")]
        public IActionResult Create()
        {
            var model = new TeacherCreateVM
            {
                GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name"),
                NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name"),
                SubjectIds = new MultiSelectList(teacherService.GetSubjects(), "Id", "Name"),
                GroupIds = new MultiSelectList(teacherService.GetGroups(), "Id", "Name")
            };
            return View(model);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeacherCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Request.Form.TryGetValue("SubjectIds", out var SubjectIds);
                Request.Form.TryGetValue("GroupIds", out var GroupIds);
                teacherService.CreateTeacherFromModel(model, SubjectIds, GroupIds);
                return RedirectToAction(nameof(Index));
            }
            model.GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name");
            model.NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name");
            model.SubjectIds = new MultiSelectList(teacherService.GetSubjects(), "Id", "Name");
            model.GroupIds = new MultiSelectList(teacherService.GetGroups(), "Id", "Name");
            return View(model);
        }

        // GET: Teachers/Edit/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var teacher = teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return View(Utils.NotFound);
            }
            var model = new TeacherEditVM()
            {
                Address = teacher.Person.Address,
                Birthday = teacher.Person.Birthday,
                Email = teacher.Person.Email,
                Experience = teacher.Experience,
                FirstName = teacher.Person.FirstName,
                GenderId = teacher.Person.GenderId,
                Id = teacher.Id,
                LastName = teacher.Person.LastName,
                NationalityId = teacher.Person.NationalityId,
                Phone = teacher.Person.Phone,
                RoleId = teacher.Person.RoleId,
                Salary = teacher.Salary,
                SubjectIds = null,
                GroupIds = null
            };
            model.GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name");
            model.NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name");

            var selectedSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
            ViewData["AllSubjectIds"] = teacherService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });

            var selectedGroupIds = teacher.TeacherGroups.Select(tg => tg.IdGroup);
            ViewData["AllGroupIds"] = teacherService.GetGroups().Select(g => new SelectListItem()
            {
                Selected = selectedGroupIds.Contains(g.Id),
                Value = g.Id.ToString(),
                Text = g.Name
            });
            return View(model);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Secretary")]
        public IActionResult Edit(Guid id, TeacherEditVM model)
        {
            if (id != model.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Request.Form.TryGetValue("SubjectIds", out var SubjectIds);
                    var subjectGuids = SubjectIds.Select(si => Guid.Parse(si));

                    Request.Form.TryGetValue("GroupIds", out var GroupIds);
                    var groupGuids = GroupIds.Select(gi => Guid.Parse(gi));
                    teacherService.EditTeacher(model, subjectGuids, groupGuids);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(model.Id))
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
            var teacher = teacherService.GetTeacherById(id);
            model.FirstName = teacher.Person.FirstName;
            model.LastName = teacher.Person.LastName;
            model.GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name");
            model.NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name");

            var selectedSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
            ViewData["AllSubjectIds"] = teacherService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });

            var selectedGroupIds = teacher.TeacherGroups.Select(tg => tg.IdGroup);
            ViewData["AllGroupIds"] = teacherService.GetGroups().Select(g => new SelectListItem()
            {
                Selected = selectedGroupIds.Contains(g.Id),
                Value = g.Id.ToString(),
                Text = g.Name
            });
            return View(model);
        }

        // GET: Teachers/EditYourself/5
        [Authorize(Roles = "Teacher")]
        public IActionResult EditYourself()
        {

            var teacher = teacherService.GetTeacherById(CurrentUser.Id);
            if (teacher == null)
            {
                return View(Utils.NotFound);
            }
            var model = new TeacherEditYourselfVM()
            {
                Address = teacher.Person.Address,
                Birthday = teacher.Person.Birthday,
                Email = teacher.Person.Email,
                FirstName = teacher.Person.FirstName,
                GenderId = teacher.Person.GenderId,
                Id = teacher.Id,
                LastName = teacher.Person.LastName,
                NationalityId = teacher.Person.NationalityId,
                Phone = teacher.Person.Phone,
            };
           
            model.FirstName = teacher.Person.FirstName;
            model.LastName = teacher.Person.LastName;
            model.Birthday = teacher.Person.Birthday;
            model.GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name");
            model.NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name");

            var selectedSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
            ViewData["AllSubjectIds"] = teacherService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });

            var selectedGroupIds = teacher.TeacherGroups.Select(tg => tg.IdGroup);
            ViewData["AllGroupIds"] = teacherService.GetGroups().Select(g => new SelectListItem()
            {
                Selected = selectedGroupIds.Contains(g.Id),
                Value = g.Id.ToString(),
                Text = g.Name
            });
            return View(model);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        public IActionResult EditYourself(TeacherEditYourselfVM model)
        {
            if (CurrentUser.Id != model.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    teacherService.EditTeacherYourself(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(model.Id))
                    {
                        return View(Utils.NotFound);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { Id = model.Id});
            }
            var teacher = teacherService.GetTeacherById(model.Id);
            model.FirstName = teacher.Person.FirstName;
            model.LastName = teacher.Person.LastName;
            model.Birthday = teacher.Person.Birthday;
            model.GenderIds = new SelectList(teacherService.GetGenders(), "Id", "Name");
            model.NationalityIds = new SelectList(teacherService.GetNationalities(), "Id", "Name");

            var selectedSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
            ViewData["AllSubjectIds"] = teacherService.GetSubjects().Select(s => new SelectListItem()
            {
                Selected = selectedSubjectIds.Contains(s.Id),
                Value = s.Id.ToString(),
                Text = s.Name
            });

            var selectedGroupIds = teacher.TeacherGroups.Select(tg => tg.IdGroup);
            ViewData["AllGroupIds"] = teacherService.GetGroups().Select(g => new SelectListItem()
            {
                Selected = selectedGroupIds.Contains(g.Id),
                Value = g.Id.ToString(),
                Text = g.Name
            });
            return View(model);
        }

        // GET: Teachers/Delete/5
        [Authorize(Roles = "Secretary")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var teacher = teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return View(Utils.NotFound);
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [Authorize(Roles = "Secretary")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            teacherService.DeleteTeacher(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(Guid id)
        {
            //return _context.Teachers.Any(e => e.Id == id);
            return teacherService.TeacherExists(id);
        }

    }
}
