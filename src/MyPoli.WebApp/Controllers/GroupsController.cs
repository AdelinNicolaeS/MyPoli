using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.GroupOperations;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.Common;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Secretary, Student, Teacher")]
    public class GroupsController : BaseController
    {
        private readonly GroupService groupService;
        private readonly StudentService studentService;

        public GroupsController(ControllerDependencies dependencies, GroupService groupService, StudentService studentService) : base(dependencies)
        {
            this.groupService = groupService;
            this.studentService = studentService;
        }

        // GET: Groups
        public IActionResult Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SpecializationSort = sortOrder == "specialization_asc" ? "specialization_desc" : "specialization_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var groups = groupService.IndexToWrite(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Group>.Create(groups, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var group = await groupService.GetGroupById(id);
            if (group == null)
            {
                return View(Utils.NotFound);
            }

            return View(group);
        }

        // GET: Groups/Create
        [Authorize(Roles = "Secretary")]
        public IActionResult Create()
        {
            var model = new GroupVM
            {
                SpecializationIds = new SelectList(groupService.GetSpecializations(), "Id", "Description")
            };
            return View(model);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupVM groupVM)
        {
            if (ModelState.IsValid)
            {
                var group = new Group()
                {
                    Name = groupVM.Name,
                    SpecializationId = groupVM.SpecializationId,
                };
                groupService.AddGroup(group);
                return RedirectToAction(nameof(Index));
            }
            groupVM.SpecializationIds = new SelectList(groupService.GetSpecializations(), "Id", "Description");
            return View(groupVM);
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Secretary")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var group = await groupService.GetGroupById(id);
            if (group == null)
            {
                return View(Utils.NotFound);
            }
            var groupVM = new GroupVM()
            {
                Id = group.Id,
                Name = group.Name,
                SpecializationId = group.SpecializationId,
                SpecializationIds = new SelectList(groupService.GetSpecializations(), "Id", "Description")
            };
            return View(groupVM);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, GroupVM groupVM)
        {
            groupVM.SpecializationIds = new SelectList(groupService.GetSpecializations(), "Id", "Description");
            if (id != groupVM.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var group = new Group()
                    {
                        Id = groupVM.Id,
                        Name = groupVM.Name,
                        SpecializationId = groupVM.SpecializationId
                    };
                    groupService.UpdateGroup(group);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(groupVM.Id))
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
            groupVM.SpecializationIds = new SelectList(groupService.GetSpecializations(), "Id", "Description");
            return View(groupVM);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "Secretary")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            var group = await groupService.GetGroupById(id);
            if (group == null)
            {
                return View(Utils.NotFound);
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Secretary")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            groupService.DeleteGroup(id);
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(Guid id)
        {
            return groupService.GroupExists(id);
        }

        public JsonResult GetGroups()
        {
            var subjects = groupService.GetGroups().Select(e => new SelectListItem()
            {
                Text = e.Name,
                Value = e.Id.ToString()
            });
            return Json(subjects);
        }

        [Authorize(Roles = "Student")]
        public RedirectToActionResult GetGroupOfUser()
        {
            var student = studentService.GetStudentById(CurrentUser.Id);
            //return View("Details", student.Group);
            return RedirectToAction("Details", "Groups", new { id = student.GroupId });
        }
    }
}
