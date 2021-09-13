using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;

namespace MyPoli.BusinessLogic.Implementation.FeedbackOperations.Validations
{
    public class FeedbackEditValidator : AbstractValidator<FeedbackEditVM>
    {
        private readonly ServiceDependencies Dependencies;
        public FeedbackEditValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.LectureGrade)
                .Must(g => g >= 1 && g <= 10).WithMessage("Grade must be between 1 and 10")
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LectureOpinion)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.SeminarGrade)
                .Must(g => g >= 1 && g <= 10).WithMessage("Grade must be between 1 and 10")
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.SeminarOpinion)
               .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}