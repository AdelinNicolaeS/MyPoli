using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Primitives;
using MyPoli.BusinessLogic.Implementation.StudentOperations.Validations;
using MyPoli.Common;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.StudentOperations
{
    public class StudentService : BaseService
    {
        private readonly StudentCreateValidator studentCreateValidator;
        private readonly StudentEditValidator studentEditValidator;
        private readonly StudentEditYourselfValidator studentEditYourselfValidator;
        public StudentService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            studentCreateValidator = new StudentCreateValidator(serviceDependencies);
            studentEditValidator = new StudentEditValidator(serviceDependencies);
            studentEditYourselfValidator = new StudentEditYourselfValidator(serviceDependencies);
        }

        public IQueryable<Student> IndexToWrite(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            var students1 = UnitOfWork.Students.Get();
            IQueryable<Student> students;
            if (currentUser.Roles.Contains("Student"))
            {
                var student = GetStudentById(currentUser.Id);
                students1 = students1.Where(s => s.GroupId == student.GroupId);
            }
            var students2 = students1
                .Include(s => s.Person)
                    .ThenInclude(p => p.Nationality)
                .Where(s => !s.Person.IsDeleted)
                .Include(s => s.Group)
                .Include(s => s.Status);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                students = students2.Where(s => s.Person.FirstName.ToUpper().Contains(searchString) || s.Person.LastName.ToUpper().Contains(searchString)
                || s.Person.Email.ToUpper().Contains(searchString) || s.Person.Nationality.Name.ToUpper().Contains(searchString) || s.StartDate.ToString().ToUpper().Contains(searchString)
                || s.EndDate.ToString().ToUpper().Contains(searchString) || s.Group.Name.ToUpper().Contains(searchString) || s.Status.Name.ToUpper().Contains(searchString));
            } else
            {
                students = students2;
            }
            return sortOrder switch
            {
                "last_desc" => students.OrderByDescending(s => s.Person.LastName),
                "last_asc" => students.OrderBy(s => s.Person.LastName),
                "email_asc" => students.OrderBy(s => s.Person.Email),
                "email_desc" => students.OrderByDescending(s => s.Person.Email),
                "nationality_asc" => students.OrderBy(s => s.Person.Nationality.Name),
                "nationality_desc" => students.OrderByDescending(s => s.Person.Nationality.Name),
                "startdate_asc" => students.OrderBy(s => s.StartDate),
                "startdate_desc" => students.OrderByDescending(s => s.StartDate),
                "enddate_asc" => students.OrderBy(s => s.EndDate),
                "enddate_desc" => students.OrderByDescending(s => s.EndDate),
                "group_asc" => students.OrderBy(s => s.Group.Name),
                "group_desc" => students.OrderByDescending(s => s.Group.Name),
                "status_asc" => students.OrderBy(s => s.Status.Name),
                "status_desc" => students.OrderByDescending(s => s.Status.Name),
                "first_desc" => students.OrderByDescending(s => s.Person.FirstName),
                _ => students.OrderBy(s => s.Person.FirstName),
            };
        }

        internal bool EmailAlreadyUsed(Guid Id, string email)
        {
            return UnitOfWork.People.Get().Where(p => !p.IsDeleted).Any(p => p.Id != Id && p.Email == email);
        }

        internal bool EmailNotAvailable(string email)
        {
            return UnitOfWork.People.Get().Where(p => !p.IsDeleted).Any(p => p.Email == email);
        }

        public void CreateStudentFromModel(StudentCreateVM model, StringValues subjectIds)
        {
            ExecuteInTransaction(uow =>
            {
                studentCreateValidator.Validate(model).ThenThrow(model);

                var student = new Student()
                {
                    Id = Guid.NewGuid(),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    GroupId = model.GroupId,
                    StatusId = model.StatusId,
                };
                var person = new Person()
                {
                    Id = student.Id,
                    PasswordHash = Utils.MyHashFunction(model.Password),
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NationalityId = model.NationalityId,
                    GenderId = model.GenderId,
                    Phone = model.Phone,
                    IsDeleted = false,
                    RoleId = 3 // rol de student
                };
                uow.Personroles.Insert(new PersonRole()
                {
                    PersonId = student.Id,
                    RoleId = 3
                });

                foreach (var stringId in subjectIds)
                {
                    var guidSubject = Guid.Parse(stringId);
                    var studentSubject = new StudentSubject()
                    {
                        IdStudent = student.Id,
                        IdSubject = guidSubject
                    };
                    uow.StudentSubjects.Insert(studentSubject);
                }

                uow.Students.Insert(student);
                uow.People.Insert(person);

                uow.SaveChanges();
            });
        }

        public IQueryable<Subject> GetSubjects()
        { // SelectListItem
            return UnitOfWork.Subjects.Get();
        }

        public void EditStudent(StudentEditVM studentEdit, IEnumerable<Guid> newSubjectGuids)
        {
            ExecuteInTransaction(uow =>
            {
                studentEditValidator.Validate(studentEdit).ThenThrow(studentEdit);
                var student = uow.Students.Get()
                    .Include(s => s.StudentSubjects)
                        .ThenInclude(ss => ss.Grade)
                    .FirstOrDefault(s => s.Id == studentEdit.Id);
                student.EndDate = studentEdit.EndDate;
                student.GroupId = studentEdit.GroupId;
                student.StatusId = studentEdit.StatusId;

                var person = uow.People.Get().FirstOrDefault(s => s.Id == studentEdit.Id);
                person.Address = studentEdit.Address;
                person.Email = studentEdit.Email;
                person.GenderId = studentEdit.GenderId;
                person.NationalityId = studentEdit.NationalityId;
                person.Phone = studentEdit.Phone;
                person.RoleId = studentEdit.RoleId;

                var oldSubjectIds = student.StudentSubjects.Select(ss => ss.IdSubject);
                foreach (var myGuid in oldSubjectIds)
                {
                    if (!newSubjectGuids.Contains(myGuid))
                    {
                        var ss = GetStudentSubjectById(student.Id, myGuid);
                        if (ss.Grade != null)
                        {
                            uow.Grades.Delete(ss.Grade);
                        }
                        uow.StudentSubjects.Delete(ss);
                    }
                }

                foreach (var myGuid in newSubjectGuids)
                {
                    var studentSubject = new StudentSubject()
                    {
                        IdStudent = student.Id,
                        IdSubject = myGuid
                    };
                    if (!uow.StudentSubjects.Get().Contains(studentSubject))
                    {
                        uow.StudentSubjects.Insert(studentSubject);
                    }
                }

                uow.Students.Update(student);
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }

        private StudentSubject GetStudentSubjectById(Guid studentId, Guid subjectId)
        {
            return UnitOfWork.StudentSubjects.Get().FirstOrDefault(ss => ss.IdSubject == subjectId && ss.IdStudent == studentId);
        }

        public void DeleteStudent(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var student = uow.Students.Get().FirstOrDefault(s => s.Id == id);
                var person = uow.People.Get().FirstOrDefault(p => p.Id == id);
               
                person.IsDeleted = true;
                uow.SaveChanges();
            });
        }

        public IQueryable<Gender> GetGenders()
        {
            return UnitOfWork.Genders.Get();
        }

        public IQueryable<Group> GetGroups()
        {
            return UnitOfWork.Groups.Get().Where(g => !g.IsDeleted).OrderBy(g => g.Name);
        }

        public IQueryable<Nationality> GetNationalities()
        {
            return UnitOfWork.Nationalities.Get().OrderBy(n => n.Name);
        }

        public IQueryable<Status> GetStatuses()
        {
            return UnitOfWork.Statuses.Get();
        }
        public IQueryable<Student> GetStudents()
        {
            return UnitOfWork.Students.Get();
        }

        public Student GetStudentById(Guid? id)
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
                    .ThenInclude(g => g.Specialization)
                .Include(s => s.Status)
                .FirstOrDefault(s => s.Id == id);

            return student;
        }

        public void EditYourself(StudentEditYourselfVM studentEdit)
        {
            ExecuteInTransaction(uow =>
            {
                studentEditYourselfValidator.Validate(studentEdit).ThenThrow(studentEdit);
                var student = uow.Students.Get()
                    .Include(s => s.StudentSubjects)
                        .ThenInclude(ss => ss.Grade)
                    .FirstOrDefault(s => s.Id == studentEdit.Id);
                student.EndDate = studentEdit.EndDate;

                var person = uow.People.Get().FirstOrDefault(s => s.Id == studentEdit.Id);
                person.Address = studentEdit.Address;
                person.Email = studentEdit.Email;
                person.GenderId = studentEdit.GenderId;
                person.NationalityId = studentEdit.NationalityId;
                person.Phone = studentEdit.Phone;

                uow.Students.Update(student);
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }
    }
}
