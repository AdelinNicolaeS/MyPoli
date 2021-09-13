using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.Common.Exceptions
{

    public class ValidationErrorException : Exception
    {
        public readonly ValidationResult ValidationResult;
        public readonly object Model;

        public ValidationErrorException(ValidationResult result, object model)
        {
            ValidationResult = result;
            Model = model;
        }
    }



}
