﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.FeedbackOperations.Validations;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;

namespace MyPoli.BusinessLogic.Implementation.FeedbackOperations
{
    public class FeedbackService : BaseService
    {
        private readonly FeedbackCreateValidator feedbackCreateValidator;
        private readonly FeedbackEditValidator feedbackEditValidator;
        private readonly FeedbackCreateBySubjectValidator feedbackCreateBySubjectValidator;
        public FeedbackService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            feedbackCreateValidator = new FeedbackCreateValidator(serviceDependencies);
            feedbackEditValidator = new FeedbackEditValidator(serviceDependencies);
            feedbackCreateBySubjectValidator = new FeedbackCreateBySubjectValidator(serviceDependencies);
        }

        public async Task<IQueryable<Feedback>> IndexToWriteAsync(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            IQueryable<Feedback> feedbacks = UnitOfWork.Feedbacks.Get()
               .Where(e => !e.IsDeleted)
               .Include(g => g.StudentSubject)
                   .ThenInclude(g => g.Student)
                       .ThenInclude(s => s.Person)
               .Include(g => g.StudentSubject)
                   .ThenInclude(ss => ss.Subject)
               ;
            if (currentUser.Roles.Contains("Teacher"))
            {
                var teacher = GetTeacherById(currentUser.Id);
                feedbacks = feedbacks.Where(f => teacher.SubjectTeachers.Select(st => st.SubjectId).Contains(f.IdSubject));
            } else if (currentUser.Roles.Contains("Student"))
            {
                var student = await GetStudentById(currentUser.Id);
                feedbacks = feedbacks.Where(f => f.IdStudent == student.Id);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                feedbacks = feedbacks.Where(f => f.StudentSubject.Subject.Name.ToUpper().Contains(searchString) ||
                    f.StudentSubject.Student.Person.FirstName.ToUpper().Contains(searchString) || f.SeminarGrade.ToString().Contains(searchString) ||
                    f.StudentSubject.Student.Person.LastName.ToUpper().Contains(searchString) || f.LectureGrade.ToString().Contains(searchString)); 
            }
            return sortOrder switch
            {
                "student_desc" => feedbacks.OrderByDescending(f => f.StudentSubject.Student.Person.FirstName).ThenByDescending(f => f.StudentSubject.Student.Person.LastName),
                "subject_asc" => feedbacks.OrderBy(f => f.StudentSubject.Subject.Name),
                "subject_desc" => feedbacks.OrderByDescending(f => f.StudentSubject.Subject.Name),
                "gradelecture_asc" => feedbacks.OrderBy(f => f.LectureGrade),
                "gradelecture_desc" => feedbacks.OrderByDescending(f => f.LectureGrade),
                "gradeseminar_asc" => feedbacks.OrderBy(f => f.SeminarGrade),
                "gradeseminar_desc" => feedbacks.OrderByDescending(f => f.SeminarGrade),
                "lecture_asc" => feedbacks.OrderBy(f => f.LectureOpinion),
                "lecture_desc" => feedbacks.OrderByDescending(f => f.LectureOpinion),
                "seminar_asc" => feedbacks.OrderBy(f => f.SeminarOpinion),
                "seminar_desc" => feedbacks.OrderByDescending(f => f.SeminarOpinion),
                "date_asc" => feedbacks.OrderBy(f => f.DateTime),
                "date_desc" => feedbacks.OrderByDescending(f => f.DateTime),
                _ => feedbacks.OrderBy(f => f.StudentSubject.Student.Person.FirstName).ThenBy(f => f.StudentSubject.Student.Person.LastName),
            };
        }

        public async Task<bool> TeacherAllowedAsync(Guid teacherId, Guid? feedbackId)
        {
            var feedback = await GetFeedbackById(feedbackId);
            if(feedback == null)
            {
                return false;
            }
            var teacher = GetTeacherById(teacherId);
            if(teacher == null)
            {
                return false;
            }
            return teacher.SubjectTeachers.Select(st => st.SubjectId).Contains(feedback.IdSubject);
        }

        public async Task<bool> StudentAllowedAsync(Guid studentId, Guid? feedbackId)
        {
            var feedback = await GetFeedbackById(feedbackId);
            if (feedback == null)
            {
                return false;
            }
            return feedback.StudentSubject.IdStudent == studentId;
        }

        public void CreateFeedbackFromModelAsync(FeedbackCreateVM model, List<BadWord> badWords)
        {
            var lectureOpinion = model.LectureOpinion;
            var seminarOpinion = model.SeminarOpinion;
            var lectureWords = Utils.ConvertArrayToList(lectureOpinion.Split(Utils.delimiterChars));
            var seminarWords = Utils.ConvertArrayToList(seminarOpinion.Split(Utils.delimiterChars));

            ExecuteInTransaction(uow => {
                feedbackCreateValidator.Validate(model).ThenThrow(model);

                badWords.ForEach(bw => {
                    lectureWords.ForEach(lw =>
                    {
                        var val1 = Utils.EditDist(bw.Value, lw);
                        var val2 = Utils.MinimumDiff_EditDist;
                        if (val1 < val2)
                        {
                            lectureOpinion = lectureOpinion.Replace(lw, new string('*', lw.Length));
                        }
                    });
                    seminarWords.ForEach(sw =>
                    {
                        if (Utils.EditDist(bw.Value, sw) <= Utils.MinimumDiff_EditDist)
                        {
                            seminarOpinion = seminarOpinion.Replace(sw, new string('*', sw.Length));
                        }
                    });
                });

                var feedback = new Feedback()
                {
                    Id = Guid.NewGuid(),
                    DateTime = DateTime.Now,
                    IdStudent = CurrentUser.Id,
                    IdSubject = model.SubjectId,
                    SeminarGrade = model.SeminarGrade,
                    SeminarOpinion = seminarOpinion,
                    LectureGrade = model.LectureGrade,
                    LectureOpinion = lectureOpinion,
                    IsDeleted = false
                };
                uow.Feedbacks.Insert(feedback);
                uow.SaveChanges();
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetSubjectsByStudentAsync(CurrentUserDto currentUser)
        {
            var student = await GetStudentById(currentUser.Id);
            return student.StudentSubjects.Where(ss => !ss.Subject.IsDeleted &&
            !UnitOfWork.Feedbacks.Get().Select(f => new { f.IdStudent, f.IdSubject}).Any(x => x.IdSubject == ss.IdSubject && x.IdStudent == student.Id))
            .Select(ss => new SelectListItem()
            {
                Value = ss.IdSubject.ToString(),
                Text = ss.Subject.Name
            });
        }

        public void EditFeedbackFromModel(FeedbackEditVM model, List<BadWord> badWords)
        {
            var lectureOpinion = model.LectureOpinion;
            var seminarOpinion = model.SeminarOpinion;
            var lectureWords = Utils.ConvertArrayToList(lectureOpinion.Split(Utils.delimiterChars));
            var seminarWords = Utils.ConvertArrayToList(seminarOpinion.Split(Utils.delimiterChars));
            ExecuteInTransaction(uow => {
                feedbackEditValidator.Validate(model).ThenThrow(model);
                var feedback = uow.Feedbacks.Get().FirstOrDefault(f => f.Id == model.Id);
                badWords.ForEach(bw => {
                    lectureWords.ForEach(lw =>
                    {
                        if (Utils.EditDist(bw.Value, lw) <= Utils.MinimumDiff_EditDist)
                        {
                            lectureOpinion = lectureOpinion.Replace(lw, new string('*', lw.Length));
                        }
                    });
                    seminarWords.ForEach(sw =>
                    {
                        if (Utils.EditDist(bw.Value, sw) <= Utils.MinimumDiff_EditDist)
                        {
                            seminarOpinion = seminarOpinion.Replace(sw, new string('*', sw.Length));
                        }
                    });
                });

                feedback.LectureGrade = model.LectureGrade;
                feedback.LectureOpinion = lectureOpinion;
                feedback.SeminarGrade = model.SeminarGrade;
                feedback.SeminarOpinion = seminarOpinion;

                uow.Feedbacks.Update(feedback);
                uow.SaveChanges();
            });
        }

        public Task<Feedback> GetFeedbackById(Guid? id)
        {
            return UnitOfWork.Feedbacks.Get()
                .Include(g => g.StudentSubject)
                   .ThenInclude(g => g.Student)
                       .ThenInclude(s => s.Person)
               .Include(g => g.StudentSubject)
                   .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public bool FeedbackExists(Guid id)
        {
            return UnitOfWork.Feedbacks.Get().Any(f => f.Id == id);
        }

        public void DeleteFeedback(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var feedback = uow.Feedbacks.Get().FirstOrDefault(f => f.Id == id);
                //feedback.IsDeleted = true;

                uow.Feedbacks.Delete(feedback);
                uow.SaveChanges();
            }); 
        }

        private Teacher GetTeacherById(Guid id)
        {
            return UnitOfWork.Teachers.Get()
                .Include(t => t.SubjectTeachers)
                .Include(t => t.TeacherGroups)
                //.Include(t => t.Person)
                .FirstOrDefault(t => t.Id == id);
        }

        private Task<Student> GetStudentById(Guid id)
        {
            return UnitOfWork.Students.Get()
                .Include(s => s.Person)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void CreateFeedbackFromModelWithSubject(FeedbackCreateBySubjectVM model, List<BadWord> badWords)
        {
            var lectureOpinion = model.LectureOpinion;
            var seminarOpinion = model.SeminarOpinion;
            var lectureWords = Utils.ConvertArrayToList(lectureOpinion.Split(Utils.delimiterChars));
            var seminarWords = Utils.ConvertArrayToList(seminarOpinion.Split(Utils.delimiterChars));
            ExecuteInTransaction(uow => {
                feedbackCreateBySubjectValidator.Validate(model).ThenThrow(model);

                badWords.ForEach(bw => {
                    lectureWords.ForEach(lw =>
                    {
                        if(Utils.EditDist(bw.Value, lw) <= Utils.MinimumDiff_EditDist)
                        {
                            lectureOpinion = lectureOpinion.Replace(lw, new string('*', lw.Length));
                        }
                    });
                    seminarWords.ForEach(sw =>
                    {
                        if (Utils.EditDist(bw.Value, sw) <= Utils.MinimumDiff_EditDist)
                        {
                            seminarOpinion = seminarOpinion.Replace(sw, new string('*', sw.Length));
                        }
                    });
                });
                var feedback = new Feedback()
                {
                    Id = Guid.NewGuid(),
                    DateTime = DateTime.Now,
                    IdStudent = CurrentUser.Id,
                    IdSubject = model.SubjectId,
                    SeminarGrade = model.SeminarGrade,
                    SeminarOpinion = seminarOpinion,
                    LectureGrade = model.LectureGrade,
                    LectureOpinion = lectureOpinion,
                    IsDeleted = false
                };
                uow.Feedbacks.Insert(feedback);
                uow.SaveChanges();
            });
        }

        public async Task<Feedback> FeedbackExistsAsync(Guid studentId, Guid subjectId)
        {
            return await UnitOfWork.Feedbacks.Get().FirstOrDefaultAsync(f => f.IdStudent == studentId && f.IdSubject == subjectId);
        }
    }
}
