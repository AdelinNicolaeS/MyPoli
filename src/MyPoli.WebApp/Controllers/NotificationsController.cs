using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPoli.BusinessLogic.Implementation.NotificationOperations;
using MyPoli.BusinessLogic.Models;
using MyPoli.Common;
using MyPoli.Entities;
using MyPoli.WebApp.Code.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoli.WebApp.Controllers
{
    [Authorize(Roles = "Student, Teacher, Secretary")]
    public class NotificationsController : BaseController
    {
        private readonly NotificationService notificationService;
        public NotificationsController(ControllerDependencies dependencies, NotificationService _notificationService) : base(dependencies)
        {
            notificationService = _notificationService;
        }
        // GET: Feedbacks
        [ActionName("Index")]
        public IActionResult Index(int? pageNumber)
        {
            IQueryable<Notification> notifications;
            if (CurrentUser.Roles.Contains("Secretary"))
            {
                notifications = notificationService.IndexAdmin();
            }
            else
            {
                notifications = notificationService.IndexStudentTeacher();
            }
            return View(PaginatedList<Notification>.Create(notifications, pageNumber ?? 1, Utils.PageSize));
            //return View(notifications);
        }

        public IActionResult MarkRead(Guid? Id)
        {
            if(Id == null)
            {
                return View(Utils.NotFound);
            }
            notificationService.MarkRead(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
