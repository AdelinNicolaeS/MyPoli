using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MyPoli.BusinessLogic.Models;


namespace MyPoli.BusinessLogic.Implementation.CertificatesOperations.Validations
{
    public class CertificateValidator : AbstractValidator<CertificateVM>
    {
        private readonly ServiceDependencies Dependencies;
        public CertificateValidator(ServiceDependencies dependencies)
        {
            this.Dependencies = dependencies;

            RuleFor(r => r.Reason)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            //RuleFor(r => r.Date)
            //    .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.StudentId)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
    }
}
