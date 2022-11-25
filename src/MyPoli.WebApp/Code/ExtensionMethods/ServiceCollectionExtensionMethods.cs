using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyPoli.BusinessLogic;
using MyPoli.BusinessLogic.Implementation.Account;
using MyPoli.BusinessLogic.Implementation.BadWordOperations;
using MyPoli.BusinessLogic.Implementation.CertificatesOperations;
using MyPoli.BusinessLogic.Implementation.CircumstanceOperations;
using MyPoli.BusinessLogic.Implementation.FeedbackOperations;
using MyPoli.BusinessLogic.Implementation.Grades;
using MyPoli.BusinessLogic.Implementation.GroupOperations;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.BusinessLogic.Implementation.SubjectOperations;
using MyPoli.BusinessLogic.Implementation.SubjectTeacherOperations;
using MyPoli.BusinessLogic.Implementation.TeacherOperations;
using MyPoli.BusinessLogic.Implementation.ThesisOperations;
using MyPoli.Common.DTOs;
using MyPoli.WebApp.Code.Base;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyPoli.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddMyPoliBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<StudentService>();
            services.AddScoped<SubjectService>();
            services.AddScoped<TeacherService>();
            services.AddScoped<GroupService>();
            services.AddScoped<SubjectTeacherService>();
            services.AddScoped<GradeService>();
            services.AddScoped<CertificateService>();
            services.AddScoped<ThesisService>();
            services.AddScoped<CircumstanceService>();
            services.AddScoped<FeedbackService>();
            services.AddScoped<BadWordService>();
            // adaug serviciile aici ex StudentsService
            return services;
        }

        public static IServiceCollection AddMyPoliCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var firstname = claims?.FirstOrDefault(c => c.Type == "FirstName")?.Value;
                var lastname = claims?.FirstOrDefault(c => c.Type == "LastName")?.Value;
                var email = claims?.FirstOrDefault(c => c.Type == "Email")?.Value;
                var userroles = claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);

                return new CurrentUserDto
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Email = email,
                    FirstName = firstname,
                    LastName = lastname,
                    Roles = userroles
                };
            });

            return services;
        }
    }
}
