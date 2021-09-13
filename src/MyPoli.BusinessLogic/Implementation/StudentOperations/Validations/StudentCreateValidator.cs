using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.StudentOperations.Validations
{
    public class StudentCreateValidator : AbstractValidator<StudentCreateVM>
    {
        private readonly ServiceDependencies Dependencies;
        public StudentCreateValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must(NotAlreadyExistEmail);
            RuleFor(r => r.Address)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Birthday)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.EndDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.GenderId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.GroupId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.NationalityId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.StartDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.StatusId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
        private bool NotAlreadyExistEmail(string email)
        {
            return !Dependencies.UnitOfWork.People.Get().Any(p => p.Email == email);
        }
    }
}
