using Microsoft.AspNetCore.Mvc;
using MyPoli.Common.DTOs;

namespace MyPoli.WebApp.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDto CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }

       
    }
}
