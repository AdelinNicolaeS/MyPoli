using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPoli.BusinessLogic.Implementation.CircumstanceOperations.Validations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common.DTOs;
using MyPoli.Common.Extensions;
using MyPoli.Entities;


namespace MyPoli.BusinessLogic.Implementation.CircumstanceOperations
{
    public class CircumstanceService : BaseService
    {
        private readonly CircumstanceValidator circumstanceValidator;
        public CircumstanceService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            circumstanceValidator = new CircumstanceValidator(serviceDependencies);
        }

        public IQueryable<Circumstance> IndexToWrite(string sortOrder, string searchString, CurrentUserDto currentUser)
        {
            var aux = UnitOfWork.Circumstances.Get();
            if (currentUser.Roles.Contains("Student")) // un student isi poate vedea doar propriile reclamatii
            {
                aux = aux.Where(c => c.StudentId == currentUser.Id);
            } // profii si secretarele le pot vedea pe toate
            IQueryable<Circumstance> circumstances = aux
                .Include(c => c.Student)
                    .ThenInclude(s => s.Person);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                circumstances = circumstances.Where(c => c.Student.Person.FirstName.ToUpper().Contains(searchString) || c.Student.Person.LastName.ToUpper().Contains(searchString)
                || c.Description.ToUpper().Contains(searchString) || c.EndDate.ToString().ToUpper().Contains(searchString) || c.StartDate.ToString().ToUpper().Contains(searchString))
                    ;
            }
            return sortOrder switch
            {
                "start_asc" => circumstances.OrderBy(c => c.StartDate),
                "start_desc" => circumstances.OrderByDescending(c => c.StartDate),
                "end_asc" => circumstances.OrderBy(c => c.EndDate),
                "end_desc" => circumstances.OrderByDescending(c => c.EndDate),
                "description_asc" => circumstances.OrderBy(c => c.Description),
                "description_desc" => circumstances.OrderByDescending(c => c.Description),
                "accepted_asc" => circumstances.OrderBy(c => c.Accepted),
                "accepted_desc" => circumstances.OrderByDescending(c => c.Accepted),
                "name_desc" => circumstances.OrderByDescending(c => c.Student.Person.FirstName).ThenByDescending(c => c.Student.Person.LastName),
                _ => circumstances.OrderBy(c => c.Student.Person.FirstName).ThenBy(c => c.Student.Person.LastName)
            };
        }

        public void CreateCircumstanceFromModel(CircumstanceCreateVM model)
        {
            ExecuteInTransaction(uow => {
                circumstanceValidator.Validate(model).ThenThrow(model);

                var circumstance = new Circumstance()
                {
                    Id = Guid.NewGuid(),
                    Accepted = false,
                    Description = model.Description,
                    EndDate = model.EndDate,
                    StartDate = model.StartDate,
                    StudentId = model.StudentId
                };

                uow.Circumstances.Insert(circumstance);
                uow.SaveChanges();
            });
        }

        public async Task<Circumstance> GetCircumstanceById(Guid? id)
        {
            return await UnitOfWork.Circumstances.Get()
                .Include(c => c.Student)
                    .ThenInclude(s => s.Person)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void EditCircumstance(CircumstanceEditVM model)
        {
            ExecuteInTransaction(uow => {

                var circumstance = uow.Circumstances.Get().FirstOrDefault(c => c.Id == model.Id);
                if(model.EndDate != circumstance.EndDate || model.Description != circumstance.Description || model.StartDate != circumstance.StartDate)
                {
                    circumstance.Accepted = false;
                }
                circumstance.Description = model.Description;
                circumstance.EndDate = model.EndDate;
                circumstance.StartDate = model.StartDate;
                uow.Circumstances.Update(circumstance);
                uow.SaveChanges();
            });
        }

        public bool CircumstanceExists(Guid id)
        {
            return UnitOfWork.Circumstances.Get().Any(c => c.Id == id);
        }

        public void DeleteCircumstance(Guid id)
        {
            ExecuteInTransaction(uow => {
                var circumstance = uow.Circumstances.Get().FirstOrDefault(c => c.Id == id);
                uow.Circumstances.Delete(circumstance);
                uow.SaveChanges();
            });
        }

        public void AcceptCircumstance(Guid id)
        {
            ExecuteInTransaction(uow => {
                var circumstance = uow.Circumstances.Get().FirstOrDefault(c => c.Id == id);
                circumstance.Accepted = true;
                uow.Circumstances.Update(circumstance);
                uow.SaveChanges();
            });
        }
    }
}
