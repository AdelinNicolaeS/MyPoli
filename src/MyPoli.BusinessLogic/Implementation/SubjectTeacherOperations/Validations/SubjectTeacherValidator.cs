using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.SubjectTeacherOperations.Validations
{
    public class SubjectTeacherValidator : AbstractValidator<SubjectTeacher>
    {
        private readonly ServiceDependencies Dependencies;
        public SubjectTeacherValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.SubjectId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.TeacherId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
