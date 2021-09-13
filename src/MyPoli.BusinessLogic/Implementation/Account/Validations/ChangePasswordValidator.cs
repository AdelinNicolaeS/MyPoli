using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;


namespace MyPoli.BusinessLogic.Implementation.Account.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        private readonly ServiceDependencies Dependencies;
        public ChangePasswordValidator(ServiceDependencies serviceDependencies)
        {
            Dependencies = serviceDependencies;

            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("The Email field is required");

            RuleFor(r => r.OldPassword)
               .NotEmpty().WithMessage("The Old Password field is required");

            RuleFor(r => r.ConfirmPassword)
              .NotEmpty().WithMessage("The Password field is required");

            RuleFor(r => new LoginModelVM(r.Email, r.OldPassword))
                .Must(CorrectCredentials).WithMessage("Not Valid authentification data");

            RuleFor(r => new { r.NewPassword, r.OldPassword })
               .Must(x => x.NewPassword != x.OldPassword).WithMessage("New Password should be different from Old Password.");
            
            RuleFor(r => new { r.NewPassword, r.ConfirmPassword })
              .Must(x => x.NewPassword == x.ConfirmPassword).WithMessage("Please insert your correct new password");

        }

        private bool CorrectCredentials(LoginModelVM loginModel)
        {
            var passHash = Utils.MyHashFunction(loginModel.Password);
            return Dependencies.UnitOfWork.People.Get().Any(p => p.Email == loginModel.Email && p.PasswordHash == passHash);
        }
    }
}
