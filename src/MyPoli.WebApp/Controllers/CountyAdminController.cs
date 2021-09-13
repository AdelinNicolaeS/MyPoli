using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoli.WebApp.Code.Base;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountyAdminController : BaseController
    {
        public CountyAdminController(ControllerDependencies dependencies)
            : base(dependencies)
        {

        }

        [HttpGet]
        public IActionResult PrivateAction()
        {
            return Ok("top secret");
        }
    }
}
