using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;
using MyPoli.Entities;


namespace MyPoli.BusinessLogic.Implementation.CircumstanceOperations.Validations
{
    public class CircumstanceValidator : AbstractValidator<CircumstanceCreateVM>
    {
        private readonly ServiceDependencies Dependencies;
        public CircumstanceValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.StudentId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.StartDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.EndDate)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Description)
               .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
