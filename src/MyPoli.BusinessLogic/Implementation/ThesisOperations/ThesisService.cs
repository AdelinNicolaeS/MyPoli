using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyPoli.BusinessLogic.Implementation.ThesisOperations.Validations;
using MyPoli.Common;
using MyPoli.Common.DTOs;
using MyPoli.Entities;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.ThesisOperations
{
    public class ThesisService : BaseService
    {
        private readonly ThesisValidator thesisValidator;
        public ThesisService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.thesisValidator = new ThesisValidator(serviceDependencies);
        }

        public IQueryable<Thesis> IndexList(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            var aux = UnitOfWork.Theses.Get();
            if (currentUser.Roles.Contains("Student") || currentUser.Roles.Contains("Teacher"))
            {
                aux = aux
                    .Where(th => th.StudentId == currentUser.Id || th.TeacherId == currentUser.Id);
            }
            IQueryable<Thesis> theses = aux
                .Include(th => th.Student)
                    .ThenInclude(s => s.Person)
                .Include(th => th.Teacher)
                    .ThenInclude(t => t.Person);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                theses = theses.Where(th => th.Student.Person.FirstName.ToUpper().Contains(searchString) || th.Student.Person.LastName.ToUpper().Contains(searchString)
                || th.Teacher.Person.FirstName.ToUpper().Contains(searchString) || th.Teacher.Person.LastName.ToUpper().Contains(searchString) || th.Date.ToString().ToUpper().Contains(searchString)
                || th.Title.ToUpper().Contains(searchString) || th.Description.ToUpper().Contains(searchString) || th.ApprovedByTeacher.ToString().ToUpper().Contains(searchString));
            }
            return sortOrder switch
            {
                "teacher_desc" => theses.OrderByDescending(c => c.Teacher.Person.FirstName).ThenByDescending(c => c.Teacher.Person.LastName),
                "teacher_asc" => theses.OrderBy(c => c.Teacher.Person.FirstName).ThenBy(c => c.Teacher.Person.LastName),
                "title_asc" => theses.OrderBy(c => c.Title),
                "title_desc" => theses.OrderByDescending(c => c.Title),
                "date_asc" => theses.OrderBy(c => c.Date),
                "date_desc" => theses.OrderByDescending(c => c.Date),
                "description_asc" => theses.OrderBy(c => c.Description),
                "description_desc" => theses.OrderByDescending(c => c.Description),
                "approved_asc" => theses.OrderBy(c => c.ApprovedByTeacher),
                "approved_desc" => theses.OrderByDescending(c => c.ApprovedByTeacher),
                "student_desc" => theses.OrderByDescending(c => c.Student.Person.FirstName).ThenByDescending(c => c.Student.Person.LastName),
                _ => theses.OrderBy(c => c.Student.Person.FirstName).ThenBy(c => c.Student.Person.LastName)
            };
        }

        internal bool StudentAlreadyHasThesis(Guid studentId)
        {
            return UnitOfWork.Theses.Get().Select(t => t.StudentId).Any(id => id == studentId);
        }

        public void CreateThesisFromModel(ThesisCreateVM model)
        {
            ExecuteInTransaction(uow =>
            {
                var thesis = new Thesis()
                {
                    ApprovedByTeacher = false,
                    Content = Utils.FileToByteArray(model.Content),
                    Date = model.Date,
                    Description = model.Description,
                    Id = Guid.NewGuid(),
                    StudentId = CurrentUser.Id,
                    TeacherId = model.TeacherId,
                    Title = model.Title
                };
                uow.Theses.Insert(thesis);
                uow.SaveChanges();
            });
        }

        public Thesis GetThesisById(Guid? id)
        {
            return UnitOfWork.Theses.Get()
                .Include(th => th.Student)
                    .ThenInclude(s => s.Person)
                .Include(th => th.Teacher)
                    .ThenInclude(t => t.Person)
                .FirstOrDefault(th => th.Id == id);
        }

        public void EditThesis(ThesisEditVM model, CurrentUserDto currentUser)
        {
            ExecuteInTransaction(uow => {
                var thesis = uow.Theses.Get().FirstOrDefault(t => t.Id == model.Id);
                if (currentUser.Roles.Contains("Student") && (thesis.TeacherId != model.TeacherId || thesis.Date != model.Date
                || thesis.Description != model.Description || thesis.Title != model.Title || model.Content != null))
                {
                    thesis.ApprovedByTeacher = false;
                }
                thesis.TeacherId = model.TeacherId;
                if (model.Content != null)
                {
                    thesis.Content = Utils.FileToByteArray(model.Content);
                }
                thesis.Date = model.Date;
                thesis.Description = model.Description;
                thesis.Title = model.Title;
                uow.Theses.Update(thesis);
                uow.SaveChanges();
            });
        }
        public void ApproveThesisByTeacher(Guid id)
        {
            ExecuteInTransaction(uow => {
                var thesis = uow.Theses.Get().FirstOrDefault(t => t.Id == id);
                thesis.ApprovedByTeacher = true /*model.ApprovedByTeacher*/;
                uow.Theses.Update(thesis);
                uow.SaveChanges();
            });
        }

        public bool ThesisExists(Guid id)
        {
            return UnitOfWork.Theses.Get().Any(t => t.Id == id);
        }

        public void DeleteThesis(Guid id)
        {
            ExecuteInTransaction(uow =>
            {
                var thesis = uow.Theses.Get().FirstOrDefault(th => th.Id == id);
                uow.Theses.Delete(thesis);
                uow.SaveChanges();
            });
        }

        public byte[] GetContent(Guid id)
        {
            var content = UnitOfWork.Theses.Get().FirstOrDefault(th => th.Id == id).Content;
            return content;
        }

        public string GetName(Guid id)
        {
            var thesis = GetThesisById(id);
            return thesis.Title + "_" + thesis.Student.Person.LastName + thesis.Student.Person.FirstName + ".pdf";    
        }

      
    }
}
