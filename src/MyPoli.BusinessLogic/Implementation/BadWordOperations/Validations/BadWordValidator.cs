using FluentValidation;
using MyPoli.BusinessLogic.Models;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.BadWordOperations.Validations
{
    class BadWordValidator : AbstractValidator<BadWord>
    {
        private readonly ServiceDependencies Dependencies;
        public BadWordValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Value)
                .Must(v => v.Length <= 50).WithMessage("Lungime prea mare")
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}

