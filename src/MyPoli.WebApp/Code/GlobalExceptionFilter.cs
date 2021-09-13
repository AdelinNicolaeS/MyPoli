using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using MyPoli.Common.Exceptions;
using System;

namespace MyPoli.WebApp.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilterAttribute> logger;

        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            
            switch (context.Exception)
            {
                case NotFoundErrorException notFound:
                    context.Result = new ViewResult
                    {
                        ViewName = "Views/Shared/Error_NotFound.cshtml"
                    };

                    break;
                case UnauthorizedAccessException unauthorizedAccess:
                    context.Result = new ViewResult
                    {
                        ViewName = "Views/Shared/Error_Unauthorized.cshtml"
                    };
                    break;
                //case ValidationErrorException validationError:
                //    foreach (var validationResult in validationError.ValidationResult.Errors)
                //    {
                //        context.ModelState.AddModelError(validationResult.PropertyName, validationResult.ErrorMessage);
                //    }

                //    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                //    context.Result = new ViewResult
                //    {
                //        ViewName = $"Views/{descriptor.ControllerName}/{descriptor.ActionName}.cshtml"
                //    };
                //    break;
                case ValidationErrorException validationError:
                    foreach (var validationResult in validationError.ValidationResult.Errors)
                    {
                        context.ModelState.AddModelError(validationResult.PropertyName, validationResult.ErrorMessage);
                    }



                    var modelMetadata = new EmptyModelMetadataProvider();



                    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                    context.Result = new ViewResult
                    {
                        ViewName = $"Views/{descriptor.ControllerName}/{descriptor.ActionName}.cshtml",
                        ViewData = new ViewDataDictionary(modelMetadata, context.ModelState)
                        {
                            Model = validationError.Model
                        }
                    };



                    break;
                default:
                    context.Result = new ViewResult
                    {
                        ViewName = "Views/Shared/Error_InternalServerError.cshtml"
                    };
                    break;

            }
        }
    }
}
