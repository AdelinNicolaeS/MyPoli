using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPoli.WebApp.Code.Base;
using MyPoli.WebApp.Models;

namespace MyPoli.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ControllerDependencies dependencies)
           : base(dependencies)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
