using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.GroupOperations.Validations;
using MyPoli.Common.Extensions;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.GroupOperations
{
    public class GroupService : BaseService
    {
        private readonly GroupValidator groupValidator;
        public GroupService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            groupValidator = new GroupValidator(serviceDependencies);
        }
        public IQueryable<Group> IndexToWrite(string sortOrder, string searchString, Common.DTOs.CurrentUserDto currentUser)
        {
            IQueryable<Group> groups;
            if (currentUser.Roles.Contains("Student"))
            {
                var student = GetStudentById(currentUser.Id);
                groups = UnitOfWork.Groups.Get().Where(g => g.Id == student.GroupId);
            } else if (currentUser.Roles.Contains("Teacher"))
            {
                var teacher = GetTeacherBy(currentUser.Id);
                var teacherGroups = teacher.TeacherGroups.Select(tg => tg.IdGroup);
                groups = UnitOfWork.Groups.Get().Where(g => teacherGroups.Contains(g.Id));
            } else
            {
                groups = UnitOfWork.Groups.Get()
                .Where(g => !g.IsDeleted)
                .Include(s => s.Specialization);
            }
            if(!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                groups = groups.Where(g => g.Name.ToUpper().Contains(searchString) || g.Specialization.Description.ToUpper().Contains(searchString));
            }
            return sortOrder switch
            {
                "name_desc" => groups.OrderByDescending(g => g.Name),
                "specialization_desc" => groups.OrderByDescending(g => g.Specialization.Description),
                "specialization_asc" => groups.OrderBy(g => g.Specialization.Description),
                _ => groups.OrderBy(g => g.Name),
            };
        }

        internal bool NameAlreadyUsed(Guid id, string name)
        {
            return UnitOfWork.Groups.Get().Where(g => !g.IsDeleted).Any(g => g.Id != id && g.Name == name);
        }

        private Teacher GetTeacherBy(Guid id)
        {
            return UnitOfWork.Teachers.Get()
                .Include(t => t.Person)
                .Include(t => t.TeacherGroups)
                    .ThenInclude(tg => tg.IdGroupNavigation)
                        .ThenInclude(g => g.Specialization)
                .FirstOrDefault(t => /*!t.Person.IsDeleted &&*/ t.Id == id);
        }

        private Student GetStudentById(Guid id)
        {
            return UnitOfWork.Students.Get()
                .Include(s => s.Person)
                .Include(s => s.Group)
                    .ThenInclude(g => g.Specialization)
                .FirstOrDefault(s => /*!s.Person.IsDeleted &&*/ s.Id == id);
        }

        public IQueryable<Specialization> GetSpecializations()
        {
            return UnitOfWork.Specializations.Get();
        }

        public void AddGroup(Group group)
        {
            ExecuteInTransaction(uow =>
            {
                groupValidator.Validate(group).ThenThrow(group);
                group.Id = Guid.NewGuid();
                group.IsDeleted = false;
                uow.Groups.Insert(group);
                uow.SaveChanges();
            });
        }
        public Task<Group> GetGroupById(Guid? id)
        {
            return UnitOfWork.Groups.Get()
                //.Where(g => !g.IsDeleted)
                .Include(g => g.Specialization)
                .Include(g => g.Students)
                    .ThenInclude(s => s.Person)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public void UpdateGroup(Group group)
        {
            ExecuteInTransaction(uow =>
            {
                groupValidator.Validate(group).ThenThrow(group);
                uow.Groups.Update(group);
                uow.SaveChanges();
            });
        }

        public bool GroupExists(Guid id)
        {
            return UnitOfWork.Groups.Get().Any(g => g.Id == id);
        }

        public void DeleteGroup(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var group = uow.Groups.Get().FirstOrDefault(g => g.Id == id);
               // uow.Groups.Delete(group);
                group.IsDeleted = true;
                uow.SaveChanges();
            });
        }

        public IQueryable<Group> GetGroups() {
            return UnitOfWork.Groups.Get();
        } 
    }
}
