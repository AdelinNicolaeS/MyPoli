using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.ThesisOperations.Validations
{
    public class ThesisValidator : AbstractValidator<ThesisCreateVM>
    {
        private readonly ServiceDependencies Dependencies;
        public ThesisValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Content)
                .NotEmpty().WithMessage("Continut obligatoriu!");
            RuleFor(r => r.Date)
                .NotEmpty().WithMessage("Data obligatorie!");
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Descriere obligatorie!");
            RuleFor(r => r.StudentId)
                .NotEmpty().WithMessage("Student obligatoriu!");
            RuleFor(r => r.TeacherId)
                .NotEmpty().WithMessage("Profesor obligatoriu!");
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Titlu obligatoriu!");
            
        }
        
    }
}
