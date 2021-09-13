using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.Entities;

namespace MyPoli.BusinessLogic.Implementation.GroupOperations.Validations
{
    public class GroupValidator : AbstractValidator<Group>
    {
        private readonly ServiceDependencies Dependencies;
        public GroupValidator(ServiceDependencies dependencies)
        {
            Dependencies = dependencies;

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must((group, name) => { return NotAlreadyExist(group, name); });
            RuleFor(r => r.SpecializationId)
                .NotEmpty().WithMessage("Camp obligatoriu");
            
        }
        private bool NotAlreadyExist(Group group, string name)
        {
            return !Dependencies.UnitOfWork.Groups.Get().Any(g => g.Name == name && g.Id != group.Id);
        }
    }
}
