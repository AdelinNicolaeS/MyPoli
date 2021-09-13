using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MyPoli.BusinessLogic.Implementation.TeacherOperations.Validations;
using MyPoli.Common;
using MyPoli.Common.Extensions;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.TeacherOperations
{
    public class TeacherService : BaseService
    {
        private readonly TeacherValidator teacherValidator;
        public TeacherService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.teacherValidator = new TeacherValidator(serviceDependencies);
        }

        public void CreateTeacherFromModel(TeacherCreateVM model, StringValues subjectIds, StringValues groupIds)
        {
            ExecuteInTransaction(uow =>
            {
                teacherValidator.Validate(model).ThenThrow(model);

                var teacher = new Teacher()
                {
                    Id = Guid.NewGuid(),
                    Experience = model.Experience,
                    Salary = model.Salary
                };

                var person = new Person()
                {
                    Id = teacher.Id,
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
                    RoleId = 2 // rol de profesor
                };

                foreach(var stringId in subjectIds)
                {
                    var guidSubject = Guid.Parse(stringId);
                    var subjectTeacher = new SubjectTeacher()
                    {
                        SubjectId = guidSubject,
                        TeacherId = teacher.Id
                    };
                    uow.SubjectTeachers.Insert(subjectTeacher);
                }

                foreach(var stringId in groupIds)
                {
                    var guidGroup = Guid.Parse(stringId);
                    var teacherGroup = new TeacherGroup()
                    {
                        IdGroup = guidGroup,
                        IdTeacher = teacher.Id
                    };
                    uow.TeacherGroups.Insert(teacherGroup);
                }

                uow.Teachers.Insert(teacher);
                uow.People.Insert(person);

                uow.SaveChanges();
            });
        }

        public IQueryable<Teacher> IndexToWrite(string sortOrder, string searchWord)
        {
            IQueryable<Teacher> teachers = UnitOfWork.Teachers.Get()
                .Include(t => t.TeacherGroups)
                    .ThenInclude(tg => tg.IdGroupNavigation)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(s => s.Subject)
               // .Include(t => t.Person)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Nationality)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Gender);
            if (!String.IsNullOrEmpty(searchWord))
            {
                searchWord = searchWord.ToUpper();
                teachers = teachers.Where(s => s.Person.FirstName.ToUpper().Contains(searchWord) || s.Person.LastName.ToUpper().Contains(searchWord)
                || s.Person.Email.ToUpper().Contains(searchWord) || s.Person.Nationality.Name.ToUpper().Contains(searchWord) || s.Person.Phone.ToUpper().Contains(searchWord)
                || s.Salary.ToString().ToUpper().Contains(searchWord) || s.Experience.ToString().ToUpper().Contains(searchWord))
                ;
            }
            return sortOrder switch
            {
                "last_desc" => teachers.OrderByDescending(s => s.Person.LastName),
                "last_asc" => teachers.OrderBy(s => s.Person.LastName),
                "email_asc" => teachers.OrderBy(s => s.Person.Email),
                "email_desc" => teachers.OrderByDescending(s => s.Person.Email),
                "nationality_asc" => teachers.OrderBy(s => s.Person.Nationality.Name),
                "nationality_desc" => teachers.OrderByDescending(s => s.Person.Nationality.Name),
                "experience_asc" => teachers.OrderBy(s => s.Experience),
                "experience_desc" => teachers.OrderByDescending(s => s.Experience),
                "salary_asc" => teachers.OrderBy(s => s.Salary),
                "salary_desc" => teachers.OrderByDescending(s => s.Salary),
                "phone_asc" => teachers.OrderBy(s => s.Person.Phone),
                "phone_desc" => teachers.OrderByDescending(s => s.Person.Phone),
                "first_desc" => teachers.OrderByDescending(s => s.Person.FirstName),
                _ => teachers.OrderBy(s => s.Person.FirstName),
            };

        }

        public void EditTeacher(TeacherEditVM teacherEdit, IEnumerable<Guid> newSubjectGuids, IEnumerable<Guid> newGroupGuids)
        {
            ExecuteInTransaction(uow =>
            {

                var teacher = uow.Teachers.Get()
                    .Include(t => t.SubjectTeachers)
                    .Include(t => t.TeacherGroups)
                    .FirstOrDefault(s => s.Id == teacherEdit.Id);
                teacher.Salary = teacherEdit.Salary;
                teacher.Experience = teacherEdit.Experience;

                var person = uow.People.Get().FirstOrDefault(s => s.Id == teacherEdit.Id);
                //person.LastName = teacherEdit.LastName;
                //person.FirstName = teacherEdit.FirstName;
                person.Address = teacherEdit.Address;
                person.Email = teacherEdit.Email;
                person.GenderId = teacherEdit.GenderId;
                person.NationalityId = teacherEdit.NationalityId;
                person.Phone = teacherEdit.Phone;
                person.RoleId = teacherEdit.RoleId;

                var oldSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId);
                foreach(var myGuid in oldSubjectIds)
                {
                    if (!newSubjectGuids.Contains(myGuid)) 
                    {
                        var st = GetSubjectTeacherById(teacher.Id, myGuid);
                        uow.SubjectTeachers.Delete(st);
                    }
                }

                foreach (var myGuid in newSubjectGuids)
                {
                    var subjectTeacher = new SubjectTeacher()
                    {
                        SubjectId = myGuid,
                        TeacherId = teacher.Id
                    };
                    if (!uow.SubjectTeachers.Get().Contains(subjectTeacher)) {
                        uow.SubjectTeachers.Insert(subjectTeacher);
                    }
                }

                var OldGroupIds = teacher.TeacherGroups.Select(tg => tg.IdGroup);
                foreach(var myGuid in OldGroupIds)
                {
                    if(!newGroupGuids.Contains(myGuid))
                    {
                        var tg = GetTeacherGroupById(teacher.Id, myGuid);
                        uow.TeacherGroups.Delete(tg);
                    }
                }
                foreach(var myGuid in newGroupGuids)
                {
                    var teacherGroup = new TeacherGroup()
                    {
                        IdGroup = myGuid,
                        IdTeacher = teacher.Id
                    };
                    if(!uow.TeacherGroups.Get().Contains(teacherGroup))
                    {
                        uow.TeacherGroups.Insert(teacherGroup);
                    }
                }

                uow.Teachers.Update(teacher);
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }

        private TeacherGroup GetTeacherGroupById(Guid teacherid, Guid groupGuid)
        {
            return UnitOfWork.TeacherGroups.Get()
                .FirstOrDefault(tg => tg.IdTeacher == teacherid && tg.IdGroup == groupGuid);
        }

        public SubjectTeacher GetSubjectTeacherById(Guid? TeacherId, Guid? SubjectId)
        {
            return UnitOfWork.SubjectTeachers.Get()
                //.Include(s => s.Subject)
                //.Include(s => s.Teacher)
                //    .ThenInclude(t => t.Person)
                .FirstOrDefault(s => s.SubjectId == SubjectId && s.TeacherId == TeacherId);
        }
        public void DeleteTeacher(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
               // var teacher = uow.Teachers.Get().FirstOrDefault(t => t.Id == id);
                var person = uow.People.Get().FirstOrDefault(p => p.Id == id);
                //uow.Teachers.Delete(teacher);
                //uow.People.Delete(person);
                person.IsDeleted = true;
                uow.SaveChanges();
            });
        }

        public IOrderedQueryable<Teacher> GetTeachers()
        {
            return UnitOfWork.Teachers.Get()
                .Include(t => t.Person)
                .Where(t => !t.Person.IsDeleted)
                .Include(t => t.TeacherGroups)
                    .ThenInclude(tg => tg.IdGroupNavigation)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(s => s.Subject)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Nationality)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Gender)
                .OrderBy(t => t.Person.FirstName)
                    .ThenBy(t => t.Person.LastName);
        }

        public void EditTeacherYourself(TeacherEditYourselfVM teacherEdit)
        {
            ExecuteInTransaction(uow =>
            {
                var person = uow.People.Get().FirstOrDefault(s => s.Id == teacherEdit.Id);
                person.Address = teacherEdit.Address;
                person.Email = teacherEdit.Email;
                person.GenderId = teacherEdit.GenderId;
                person.NationalityId = teacherEdit.NationalityId;
                person.Phone = teacherEdit.Phone;

                uow.People.Update(person);
                uow.SaveChanges();
            });
        }

        public Teacher GetTeacherById(Guid? id)
        {
            return UnitOfWork.Teachers.Get()
                //.Include(t => t.Person)
                .Where(t => !t.Person.IsDeleted)
                .Include(t => t.TeacherGroups)
                    .ThenInclude(tg => tg.IdGroupNavigation)
                .Include(t => t.SubjectTeachers)
                    .ThenInclude(s => s.Subject)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Nationality)
                .Include(t => t.Person)
                    .ThenInclude(p => p.Gender)
                .FirstOrDefault(t => t.Id == id);
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
            return UnitOfWork.Nationalities.Get();
        }

        public IQueryable<Status> GetStatuses()
        {
            return UnitOfWork.Statuses.Get();
        }
        public IQueryable<Student> GetStudents()
        {
            return UnitOfWork.Students.Get();
        }

        public IOrderedQueryable<Subject> GetSubjects()
        {
            return UnitOfWork.Subjects.Get().Where(s => !s.IsDeleted).OrderBy(s => s.Name);
        }

        public bool TeacherExists(Guid id)
        {
            return GetTeachers().Any(t => t.Id == id);
        }
        public IOrderedQueryable<SelectListItem> GetTeachersOneName()
        {
            var teachers = UnitOfWork.Teachers.Get()
                .Include(t => t.Person)
                .Where(t => !t.Person.IsDeleted)
                .Select(t => new SelectListItem()
                {
                    Value = t.Id.ToString(),
                    Text = t.Person.FirstName + " " + t.Person.LastName
                });
            return teachers.OrderBy(t => t.Text);
        }
    }
}
