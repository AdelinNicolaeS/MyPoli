using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.TeacherOperations.Validations
{
    public class TeacherValidator : AbstractValidator<TeacherCreateVM>
    {
        private readonly ServiceDependencies Dependencies;
        public TeacherValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email obligatoriu!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must(NotAlreadyExistEmail);
            RuleFor(r => r.Address)
                .NotEmpty().WithMessage("Adresa obligatoriu!");
            RuleFor(r => r.Birthday)
                .NotEmpty().WithMessage("Zi de nastere obligatorie!");
            RuleFor(r => r.Experience)
                .NotEmpty().WithMessage("Experienta obligatorie!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Prenume obligatoriu!");
            RuleFor(r => r.GenderId)
                .NotEmpty().WithMessage("Sex obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Nume obligatoriu!");
            RuleFor(r => r.NationalityId)
                .NotEmpty().WithMessage("Natie obligatorie!");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Parola obligatorie!");
            RuleFor(r => r.Phone)
                .NotEmpty().WithMessage("Telefon obligatoriu!");
            RuleFor(r => r.Salary)
               .NotEmpty().WithMessage("Salariu obligatoriu!");
        }

        private bool NotAlreadyExistEmail(string email)
        {
            return !Dependencies.UnitOfWork.People.Get().Any(p => p.Email == email);
        }
    }
}
