using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.Grades.Validations
{
    public class GradeValidator : AbstractValidator<GradeVM>
    {
        private readonly ServiceDependencies Dependencies;
        public GradeValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.GradeValue)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(InRange);
            RuleFor(r => r.IdStudent)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.IdSubject)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }

        private bool InRange(int grade)
        {
            return (grade <= 10 && grade >= 1);
        }
    }
}
