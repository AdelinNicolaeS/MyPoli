using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyPoli.BusinessLogic.Implementation.Grades.Validations;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.Grades
{
    public class GradeService : BaseService
    {
        private readonly GradeValidator gradeValidator;
        public GradeService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.gradeValidator = new GradeValidator(serviceDependencies);
        }

        public IQueryable<Grade> IndexToWrite(string sortOrder, string searchString)
        {
            IQueryable<Grade> grades;
            var grades1 = UnitOfWork.Grades.Get()
               .Where(e => !e.IsDeleted)
               .Include(g => g.StudentSubject)
                   .ThenInclude(g => g.Student)
                       .ThenInclude(s => s.Person)
               .Include(g => g.StudentSubject)
                   .ThenInclude(ss => ss.Subject)
               .Where(g => !g.StudentSubject.Student.Person.IsDeleted && !g.StudentSubject.Subject.IsDeleted)
               ;
            if (CurrentUser.Roles.Contains("Teacher"))
            {
                var teacher = GetTeacherById(CurrentUser.Id);
                grades1 = grades1.Where(g => g.IdTeacher == CurrentUser.Id);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                grades = grades1.Where(s => s.StudentSubject.Subject.Name.ToUpper().Contains(searchString) ||
                    s.StudentSubject.Student.Person.FirstName.ToUpper().Contains(searchString) ||
                    s.StudentSubject.Student.Person.LastName.ToUpper().Contains(searchString) || s.GradeValue.ToString().Contains(searchString));
            } else
            {
                grades = grades1;
            }
            return sortOrder switch
            {
                "name_desc" => grades.OrderByDescending(g => g.StudentSubject.Student.Person.FirstName).ThenByDescending(g => g.StudentSubject.Student.Person.LastName),
                "subject_asc" => grades.OrderBy(g => g.StudentSubject.Subject.Name),
                "subject_desc" => grades.OrderByDescending(g => g.StudentSubject.Subject.Name),
                "grade_asc" => grades.OrderBy(g => g.GradeValue),
                "grade_desc" => grades.OrderByDescending(g => g.GradeValue),
                _ => grades.OrderBy(g => g.StudentSubject.Student.Person.FirstName).ThenBy(g => g.StudentSubject.Student.Person.LastName),
            };
        }

        public List<int> GetGradeValues()
        {
            var grades = UnitOfWork.Grades.Get()
               .Where(e => !e.IsDeleted)
               .Where(g => !g.StudentSubject.Student.Person.IsDeleted && !g.StudentSubject.Subject.IsDeleted)
               ;
            if (CurrentUser.Roles.Contains("Teacher"))
            {
                grades = grades.Where(g => g.IdTeacher == CurrentUser.Id);
            } else if(CurrentUser.Roles.Contains("Student"))
            {
                grades = grades.Where(g => g.IdStudent == CurrentUser.Id);
            }

            var gradeValues = grades.Select(g => g.GradeValue).ToList();
            
            int[] occurences = new int[10];
            foreach(var grade in gradeValues)
            {
                occurences[grade - 1]++;
            }
            return occurences.ToList();
        }

        public IQueryable<Grade> IndexToWriteArchive(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            IQueryable<Grade> grades;
            var grades1 = UnitOfWork.Grades.Get()
               .Include(g => g.StudentSubject)
                   .ThenInclude(g => g.Student)
                       .ThenInclude(s => s.Person)
               .Include(g => g.StudentSubject)
                   .ThenInclude(ss => ss.Subject)
               .Where(g => g.StudentSubject.Student.Person.IsDeleted || g.StudentSubject.Subject.IsDeleted)
               ;
            if (currentUser.Roles.Contains("Teacher"))
            {
                var teacher = GetTeacherById(currentUser.Id);
                grades1 = grades1.Where(g => g.IdTeacher == currentUser.Id);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                grades = grades1.Where(s => s.StudentSubject.Subject.Name.ToUpper().Contains(searchString) ||
                    s.StudentSubject.Student.Person.FirstName.ToUpper().Contains(searchString) ||
                    s.StudentSubject.Student.Person.LastName.ToUpper().Contains(searchString) || s.GradeValue.ToString().Contains(searchString));
            } else
            {
                grades = grades1;
            }
            return sortOrder switch
            {
                "name_desc" => grades.OrderByDescending(g => g.StudentSubject.Student.Person.FirstName).ThenByDescending(g => g.StudentSubject.Student.Person.LastName),
                "subject_asc" => grades.OrderBy(g => g.StudentSubject.Subject.Name),
                "subject_desc" => grades.OrderByDescending(g => g.StudentSubject.Subject.Name),
                "grade_asc" => grades.OrderBy(g => g.GradeValue),
                "grade_desc" => grades.OrderByDescending(g => g.GradeValue),
                _ => grades.OrderBy(g => g.StudentSubject.Student.Person.FirstName).ThenBy(g => g.StudentSubject.Student.Person.LastName),
            };
        }

        public IQueryable<Group> GetGroupsByTeacher(CurrentUserDto currentUser)
        {
            return UnitOfWork.Groups.Get()
                .Where(g => !g.IsDeleted)
                .Include(g => g.TeacherGroups)
                .Where(g => g.TeacherGroups.Select(tg => tg.IdTeacher).Contains(currentUser.Id));
        }

        public IQueryable<Subject> GetSubjectsByTeacher(CurrentUserDto currentUser)
        {
            var subjects = UnitOfWork.Subjects.Get()
                .Where(s => !s.IsDeleted)
                .Include(s => s.SubjectTeachers)
                .Where(s => s.SubjectTeachers.Select(st => st.TeacherId).Contains(currentUser.Id));
            return subjects;
        }

        private Teacher GetTeacherById(Guid id)
        {
            return UnitOfWork.Teachers.Get()
                .Include(t => t.SubjectTeachers)
                .Include(t => t.TeacherGroups)
                .FirstOrDefault(t => t.Id == id);
        }

        public Grade GetGradeByIds(Guid? idSubject, Guid? idStudent)
        {
            return UnitOfWork.Grades.Get()
                .Include(g => g.StudentSubject)
                    .ThenInclude(ss => ss.Student)
                        .ThenInclude(s => s.Person)
                .Include(g => g.StudentSubject)
                    .ThenInclude(ss => ss.Subject)
                .FirstOrDefault(g => g.IdStudent == idStudent && g.IdSubject == idSubject);
        }

        public IQueryable<Subject> GetSubjects()
        { // SelectListItems
            return UnitOfWork.Subjects.Get();
        }

        public IOrderedQueryable<SelectListItem> GetStudentsOneName()
        {
            var students = UnitOfWork.Students.Get()
                 .Include(s => s.Person)
                 .Where(s => !s.Person.IsDeleted)
                 .Select(s => new SelectListItem()
                 {
                     Value = s.Id.ToString(),
                     Text = s.Person.FirstName + " " + s.Person.LastName
                 });
            return students.OrderBy(t => t.Text);
        }

        public IQueryable<Student> GetStudents()
        {
            return UnitOfWork.Students.Get().Include(s => s.Person);
        }

        public void AddGradeFromModel(GradeVM gradeVM, Guid idTeacher)
        {
            ExecuteInTransaction(uow =>
            {
                gradeValidator.Validate(gradeVM).ThenThrow(gradeVM);
                var grade = new Grade()
                {
                    GradeValue = gradeVM.GradeValue,
                    IdStudent = gradeVM.IdStudent,
                    IdSubject = gradeVM.IdSubject,
                    IdTeacher = idTeacher,
                    //IdGroup = gradeVM.IdGroup,
                    IsDeleted = false
                };
                uow.Grades.Insert(grade);
                uow.SaveChanges();
            });
        }

        public void EditGradeFromModel(GradeVM gradeVM)
        {
            ExecuteInTransaction(uow =>
            {
                gradeValidator.Validate(gradeVM).ThenThrow(gradeVM);
                var grade = uow.Grades.Get().FirstOrDefault(g => g.IdStudent == gradeVM.IdStudent && g.IdSubject == gradeVM.IdSubject);
                grade.GradeValue = gradeVM.GradeValue;

                uow.Grades.Update(grade);
                uow.SaveChanges();
            });
        }

        public bool GradeExists(Guid idSubject, Guid idStudent)
        {
            return UnitOfWork.Grades.Get().Any(g => g.IdSubject == idSubject && g.IdStudent == idStudent);
        }

        public void DeleteGrade(Guid idSubject, Guid idStudent)
        {
            ExecuteInTransaction(uow =>
            {
                var grade = uow.Grades.Get().FirstOrDefault(g => g.IdStudent == idStudent && g.IdSubject == idSubject);
                uow.Grades.Delete(grade);
                //grade.IsDeleted = true;
                uow.SaveChanges();
            });
        }

        public IQueryable<Subject> GetSubjectsOfStudent(Guid studentId, CurrentUserDto currentUser)
        {
            return UnitOfWork.StudentSubjects.Get()
                .Where(ss => ss.IdStudent == studentId)
                .Include(ss => ss.Subject)
                .Select(ss => ss.Subject);
        }

        public IIncludableQueryable<Student, Person> GetStudentsOfSubjectAndGroup(Guid idSubject)
        {
            var students = UnitOfWork.Students.Get()
            //    .Where(s => s.GroupId == idGroup)
                .Include(s => s.StudentSubjects)
                    .ThenInclude(ss => ss.Grade)
                .Where(s => s.StudentSubjects.Select(ss => ss.IdSubject).Contains(idSubject))
                .Where(s => !s.StudentSubjects.Select(ss => ss.Grade.IdSubject).Contains(idSubject))
                .Include(s => s.Person);
            return students;
        }

        public IQueryable<Grade> GetGradesOfStudent(string sortOrder, string searchString, Guid? studentId)
        {
            IQueryable<Grade> grades =  UnitOfWork.Grades.Get()
                .Where(g => g.IdStudent == studentId)
                .Include(g => g.StudentSubject)
                    .ThenInclude(ss => ss.Subject);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                grades = grades.Where(s => s.StudentSubject.Subject.Name.ToUpper().Contains(searchString) || s.GradeValue.ToString().Contains(searchString));
            }
            return sortOrder switch
            {
                "subject_desc" => grades.OrderByDescending(g => g.StudentSubject.Subject.Name),
                "grade_asc" => grades.OrderBy(g => g.GradeValue),
                "grade_desc" => grades.OrderByDescending(g => g.GradeValue),
                _ => grades.OrderBy(g => g.StudentSubject.Subject.Name)
            };
        }
    }
}
