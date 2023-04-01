using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPoli.BusinessLogic.Implementation.NotificationOperations;
using MyPoli.WebApp.Code.Base;
using MyPoli.WebApp.Models;

namespace MyPoli.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly NotificationService notificationService;

        public HomeController(ControllerDependencies dependencies, NotificationService notificationService)
           : base(dependencies)
        {
            this.notificationService = notificationService;
        }

        public IActionResult Index()
        {
            notificationService.RecalculateCurrentUserNotifications();
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
