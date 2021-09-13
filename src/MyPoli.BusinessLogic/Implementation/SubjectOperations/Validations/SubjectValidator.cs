using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.SubjectOperations.Validations
{
    public class SubjectValidator : AbstractValidator<Subject>
    {
        private readonly ServiceDependencies Dependencies;
        public SubjectValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExist);
        }

        private bool NotAlreadyExist(string subjectName)
        {
            return !Dependencies.UnitOfWork.Subjects.Get().Any(s => s.Name == subjectName);
        }
    }
}
