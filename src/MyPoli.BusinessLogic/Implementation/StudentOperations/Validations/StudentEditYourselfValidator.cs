using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.StudentOperations.Validations
{
    public class StudentEditYourselfValidator : AbstractValidator<StudentEditYourselfVM>
    {
        private readonly ServiceDependencies Dependencies;
        public StudentEditYourselfValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must((student, email) => { return NotAlreadyExistEmail(student, email); });
            RuleFor(r => r.Address)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Birthday)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.EndDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            //RuleFor(r => r.FirstName)
            //    .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.GenderId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
          
            //RuleFor(r => r.LastName)
            //    .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.NationalityId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.StartDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
         
        }
        private bool NotAlreadyExistEmail(StudentEditYourselfVM student, string email)
        {
            return !Dependencies.UnitOfWork.People.Get().Any(p => student.Id != p.Id && p.Email == email);
        }
    }
}
