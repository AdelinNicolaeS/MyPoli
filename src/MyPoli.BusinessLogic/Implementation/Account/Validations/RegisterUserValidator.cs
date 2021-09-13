using FluentValidation;

namespace MyPoli.BusinessLogic.Implementation.Account
{
    public class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(NotAlreadyExist)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.BirthDay)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.CityId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.CountyId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.GenderId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.PrivacyId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }

        public bool NotAlreadyExist(string email)
        {
            return true;
        }
    }
}
