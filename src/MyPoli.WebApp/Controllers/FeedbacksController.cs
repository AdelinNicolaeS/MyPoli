using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.FeedbackOperations;
using MyPoli.BusinessLogic.Implementation.SubjectOperations;
using MyPoli.Common;
using MyPoli.DataAccess;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.WebApp.Code.Base;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.BusinessLogic.Implementation.BadWordOperations;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Teacher, Secretary")]
    public class FeedbacksController : BaseController
    {
        private readonly FeedbackService feedbackService;
        private readonly SubjectService subjectService;
        private readonly StudentService studentService;
        private readonly BadWordService badWordsService;
        public FeedbacksController(ControllerDependencies dependencies, FeedbackService feedbackService, SubjectService subjectService, StudentService studentService, BadWordService badWordsService) : base(dependencies)
        {
            this.subjectService = subjectService;
            this.feedbackService = feedbackService;
            this.studentService = studentService;
            this.badWordsService = badWordsService;
        }

        // GET: Feedbacks
        [ActionName("Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string SearchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StudentSort = String.IsNullOrEmpty(sortOrder) ? "student_desc" : "";
            ViewBag.SubjectSort = sortOrder == "subject_asc" ? "subject_desc" : "subject_asc";
            ViewBag.GradeLectureSort = sortOrder == "gradelecture_asc" ? "gradelecture_desc" : "gradelecture_asc";
            ViewBag.GradeSeminarSort = sortOrder == "gradeseminar_asc" ? "gradeseminar_desc" : "gradeseminar_asc";
            ViewBag.LectureSort = sortOrder == "lecture_asc" ? "lecture_desc" : "lecture_asc";
            ViewBag.SeminarSort = sortOrder == "seminar_asc" ? "seminar_desc" : "seminar_asc";
            ViewBag.DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var feedbacks = await feedbackService.IndexToWriteAsync(sortOrder, SearchString, CurrentUser);
            return View(PaginatedList<Feedback>.Create(feedbacks, pageNumber ?? 1, Utils.PageSize));
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }
            if (CurrentUser.Roles.Contains("Student") && !(await feedbackService.StudentAllowedAsync(CurrentUser.Id, id)))
            {
                 return View(Utils.Unauthorized);   
            }
            if (CurrentUser.Roles.Contains("Teacher") && !(await feedbackService.TeacherAllowedAsync(CurrentUser.Id, id)))
            {
                return View(Utils.Unauthorized);
            }

            var feedback = await feedbackService.GetFeedbackById(id);
            
            if (feedback == null)
            {
                return View(Utils.NotFound);
            }

            return View(feedback);
        }

        [Authorize(Roles = "Student")]
        // GET: Feedbacks/Create
        public async Task<IActionResult> CreateAsync()
        {
            var model = new FeedbackCreateVM()
            {
                SubjectIds = new SelectList(await feedbackService.GetSubjectsByStudentAsync(CurrentUser), "Value", "Text"),
            };
            return View(model);
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedbackCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var badWords = await badWordsService.IndexToWrite("", "").ToListAsync();
                feedbackService.CreateFeedbackFromModelAsync(model, badWords);
                return RedirectToAction(nameof(Index));
            }
            model.SubjectIds = new SelectList(await feedbackService.GetSubjectsByStudentAsync(CurrentUser), "Value", "Text");
            return View(model);
        }

        // GET: Feedbacks/Edit/5
        [Authorize(Roles = "Student, Secretary")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            if (CurrentUser.Roles.Contains("Student") && !(await feedbackService.StudentAllowedAsync(CurrentUser.Id, id)))
            {
                return View(Utils.Unauthorized);
            }

            var feedback = await feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return View(Utils.NotFound);
            }
            var model = new FeedbackEditVM()
            {
                Id = feedback.Id,
                LectureGrade = feedback.LectureGrade,
                LectureOpinion = feedback.LectureOpinion,
                SeminarGrade = feedback.SeminarGrade,
                SeminarOpinion = feedback.SeminarOpinion,
            };
            return View(model);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Student, Secretary")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Guid id, FeedbackEditVM model)
        {
            if (id != model.Id)
            {
                return View(Utils.NotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var badWords = await badWordsService.IndexToWrite("", "").ToListAsync();
                    feedbackService.EditFeedbackFromModel(model, badWords);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(model.Id))
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
            return View(model);
        }

        // GET: Feedbacks/Delete/5
        [Authorize(Roles = "Student, Secretary")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return View(Utils.NotFound);
            }

            if (CurrentUser.Roles.Contains("Student") && !(await feedbackService.StudentAllowedAsync(CurrentUser.Id, id)))
            {
                return View(Utils.Unauthorized);
            }

            var feedback = await feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return View(Utils.NotFound);
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [Authorize(Roles = "Student, Secretary")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            feedbackService.DeleteFeedback(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(Guid id)
        {
            return feedbackService.FeedbackExists(id);
        }

        [Authorize(Roles = "Student")]
        // GET: Feedbacks/Create
        public async Task<IActionResult> CreateBySubject(Guid Id)
        {        
            var subjectIds = studentService.GetStudentById(CurrentUser.Id).StudentSubjects.Select(ss => ss.IdSubject);
            if (!subjectIds.Contains(Id))
            {
                return View(Utils.Unauthorized);
            }
            var feedback = await feedbackService.FeedbackExistsAsync(CurrentUser.Id, Id);
            if (feedback != null)
            {
                return RedirectToAction("Details", "Feedbacks", new { id = feedback.Id });
            }

            var subject = subjectService.GetSubjectById(Id);
            var model = new FeedbackCreateBySubjectVM()
            {
                SubjectId = Id,
                SubjectName = new string(subject.Name),
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBySubjectAsync(FeedbackCreateBySubjectVM model)
        {
            if (ModelState.IsValid)
            {
                var badWords = await badWordsService.IndexToWrite("", "").ToListAsync();
                feedbackService.CreateFeedbackFromModelWithSubject(model, badWords);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
