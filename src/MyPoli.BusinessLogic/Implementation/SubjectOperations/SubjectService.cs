using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.SubjectOperations.Validations;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.SubjectOperations
{
    public class SubjectService : BaseService
    {
        private readonly SubjectValidator subjectValidator;
        public SubjectService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.subjectValidator = new SubjectValidator(serviceDependencies);
        }
        public IQueryable<Subject> IndexToWrite(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            IQueryable<Subject> subjects = GetSubjects();
            if(currentUser.Roles.Contains("Student"))
            {
                var student = GetStudentById(currentUser.Id);
                var subjectIds = student.StudentSubjects.Select(ss => ss.IdSubject);
                subjects = subjects.Where(s => subjectIds.Contains(s.Id));
            } else if(CurrentUser.Roles.Contains("Teacher"))
            {
                var teacher = GetTeacherById(currentUser.Id);
                var subjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
                subjects = subjects.Where(s => subjectIds.Contains(s.Id));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                subjects = subjects.Where(s => s.Name.ToUpper().Contains(searchString));
            }
            if (sortOrder == "name_desc")
            {
                return subjects.OrderByDescending(s => s.Name);
            }
            return subjects;
        }
        private Teacher GetTeacherById(Guid id)
        {
            return UnitOfWork.Teachers.Get()
                .Include(t => t.Person)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(st => st.Subject)
                .FirstOrDefault(t => t.Id == id);
        }

        private Student GetStudentById(Guid? id)
        {
            var student = UnitOfWork.Students.Get()
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Subject)
                .Include(s => s.Person)
                .Include(s => s.Person)
                    .ThenInclude(p => p.Nationality)
                .Include(s => s.Person)
                    .ThenInclude(p => p.Gender)
                .Include(s => s.Group)
                .Include(s => s.Status)
                .FirstOrDefault(s => s.Id == id);

            return student;
        }


        public void AddSubject(Subject subject)
        {
            ExecuteInTransaction(uow =>
            {
                subjectValidator.Validate(subject).ThenThrow(subject);

                subject.Id = Guid.NewGuid();
                subject.IsDeleted = false;
                uow.Subjects.Insert(subject);
                uow.SaveChanges();
            });
        }
        public Subject GetSubjectById(Guid? id)
        {
            return GetSubjects()
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Student)
                        .ThenInclude(s => s.Person)
                .Include(s => s.SubjectTeachers)
                    .ThenInclude(s => s.Teacher)
                        .ThenInclude(t => t.Person)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Update(Subject subject)
        {
            ExecuteInTransaction(uow =>
            {
                uow.Subjects.Update(subject);
                uow.SaveChanges();
            });
        }

        public bool SubjectExists(Guid id)
        {
            return UnitOfWork.Subjects.Get().Any(s => s.Id == id);
        }

        public void RemoveSubject(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var subject = GetSubjectById(id);
                subject.IsDeleted = true;
               // uow.Subjects.Delete(subject);
                uow.SaveChanges();
            });
        }

        public IQueryable<Subject> GetSubjects()
        {
            return UnitOfWork.Subjects.Get().Where(s => !s.IsDeleted);
        }

        public IQueryable<Guid> GetTeachersIdsOfSubject(Guid id)
        {
            var teacherIds = UnitOfWork.SubjectTeachers.Get()
                .Where(st => st.SubjectId == id)
                .Select(st => st.TeacherId);
            return teacherIds;
        }
    }
}
