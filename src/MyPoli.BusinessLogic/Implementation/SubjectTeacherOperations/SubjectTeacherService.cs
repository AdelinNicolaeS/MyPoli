using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.SubjectTeacherOperations.Validations;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.SubjectTeacherOperations
{
    public class SubjectTeacherService : BaseService
    {
        private readonly SubjectTeacherValidator subjectTeacherValidator;
        public SubjectTeacherService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            subjectTeacherValidator = new SubjectTeacherValidator(serviceDependencies);
        }

        public Task<List<SubjectTeacher>> IndexToWriteAsync(Common.DTOs.CurrentUserDto currentUser)
        {
            var subjectTeachers = UnitOfWork.SubjectTeachers.Get()
                .Include(s => s.Teacher)
                    .ThenInclude(t => t.Person)
                .Include(s  => s.Teacher)
                    .ThenInclude(t => t.TeacherGroups)
                .Include(s => s.Subject);
            //if(CurrentUser.Roles.Contains("Student"))
            //{
            //    var student = GetStudentById(currentUser.Id);
            //    subjectTeachers = subjectTeachers.Where(st => st.Teacher.TeacherGroups.Select(tg => tg.IdGroup).Contains((Guid)student.GroupId) &&)
            //}
            return subjectTeachers.OrderBy(s => s.Subject.Name).ToListAsync();
        }

        private Student GetStudentById(Guid id)
        {
            return UnitOfWork.Students.Get().FirstOrDefault();
        }

        public IOrderedQueryable<TeacherOneNameVM> GetTeachersWithOneName()
        { // selectListItem
            var teachers = UnitOfWork.Teachers.Get()
                .Include(t => t.Person)
                .Select(t => new TeacherOneNameVM()
                {
                    Id = t.Id,
                    Name = t.Person.FirstName + " " + t.Person.LastName
                });
            return teachers.OrderBy(t => t.Name);
        }

        public IOrderedQueryable<Subject> GetSubjects()
        {
            return UnitOfWork.Subjects.Get().OrderBy(s => s.Name);
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return UnitOfWork.Teachers.Get().Include(t => t.Person).OrderBy(t => t.Person.FirstName).ThenBy(t => t.Person.LastName);
        }

        public void AddSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            ExecuteInTransaction(uow => {
                subjectTeacherValidator.Validate(subjectTeacher).ThenThrow(subjectTeacher);
                uow.SubjectTeachers.Insert(subjectTeacher);
                uow.SaveChanges();
            });
        }

        public Task<SubjectTeacher> GetById(Guid? TeacherId, Guid? SubjectId)
        {
            return UnitOfWork.SubjectTeachers.Get()
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                    .ThenInclude(t => t.Person)
                .FirstOrDefaultAsync(s => s.SubjectId == SubjectId && s.TeacherId == TeacherId);
        }

        public void UpdateSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            ExecuteInTransaction(uow =>
            {
                //_context.Update(subjectTeacher);
                //await _context.SaveChangesAsync();
                uow.SubjectTeachers.Update(subjectTeacher);
                uow.SaveChanges();
            });
        }

        public void RemoveSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            ExecuteInTransaction(uow =>
            {
                uow.SubjectTeachers.Delete(subjectTeacher);
                uow.SaveChanges();
            });
        }

        public bool SubjectTeacherExists(Guid teacherId, Guid subjectId)
        {
            return UnitOfWork.SubjectTeachers.Get().Any(s => s.SubjectId == subjectId && s.TeacherId == teacherId);
        }
    }
}
