using Microsoft.EntityFrameworkCore;
using MyPoli.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.BusinessLogic.Implementation.NotificationOperations
{
    public class NotificationService : BaseService
    {
        public NotificationService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public IQueryable<Notification> IndexStudentTeacher()
        {
            IQueryable<Notification> notifications = UnitOfWork.Notifications.Get()
                .Include(n => n.Person)
                .Where(n => n.Person.Id == CurrentUser.Id)
                .Include(n => n.NotificationType)
                .OrderByDescending(n => n.Date);
            return notifications;
        }

        public void RecalculateCurrentUserNotifications()
        {
            var currentPerson = UnitOfWork.People.Get()
                .FirstOrDefault(p => p.Id == CurrentUser.Id);
            if (currentPerson != null)
            {
                CurrentUser.UnreadNotifications = currentPerson.UnreadNotifications;
            }
        }

        public int GetUnreadNotifications()
        {
            var count = UnitOfWork.People.Get()
                .FirstOrDefault(p => p.Id == CurrentUser.Id)
                .UnreadNotifications;
            return count;
        }

        public IQueryable<Notification> IndexAdmin()
        {
            IQueryable<Notification> notifications = UnitOfWork.Notifications.Get()
                .Include(n => n.Person)
                .Include(n => n.NotificationType)
                .OrderByDescending(n => n.Date);
            return notifications;
        }

        public void MarkRead(Guid? id)
        {
            ExecuteInTransaction(uow =>
            {
                var notification = uow.Notifications.Get()
                    .FirstOrDefault(n => n.Id == id);
                notification.IsRead = true;
                uow.Notifications.Update(notification);

                var person = uow.People.Get()
                   .FirstOrDefault(p => p.Id == notification.PersonId);
                person.UnreadNotifications--;
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }

        public void CreateFeedbackNotification(IQueryable<Guid> teacherIds)
        {
            ExecuteInTransaction(uow =>
            {
                var notifications = teacherIds.Select(id => new Notification()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    NotificationTypeId = "FEEDBACK",
                    PersonId = id
                });
                foreach(var notification in notifications) {
                    uow.Notifications.Insert(notification);
                    var person = uow.People.Get()
                        .FirstOrDefault(p => p.Id == notification.PersonId);
                    person.UnreadNotifications++;
                    uow.People.Update(person);
                }
                uow.SaveChanges();
            });
        }
        public void CreateGradeNotification(Guid studentId)
        {
            ExecuteInTransaction(uow =>
            {
                var person = uow.People.Get()
                    .FirstOrDefault(p => p.Id == studentId);
                person.UnreadNotifications++;
                var notification = new Notification()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    NotificationTypeId = "GRADE",
                    PersonId = studentId
                };
                uow.Notifications.Insert(notification);
                uow.People.Update(person);
                uow.SaveChanges();
            });
        }
        public void CreateThesisNotification(Guid teacherId)
        {
            ExecuteInTransaction(uow =>
            {
                var person = uow.People.Get()
                   .FirstOrDefault(p => p.Id == teacherId);
                person.UnreadNotifications++;
                var notification = new Notification()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    NotificationTypeId = "THESIS",
                    PersonId = teacherId
                };
                uow.People.Update(person);
                uow.Notifications.Insert(notification);
                uow.SaveChanges();
            });
        }

        public void CreateApproveThesisNotification(Guid thesisId)
        {
            ExecuteInTransaction(uow =>
            {
                var studentId = uow.Theses.Get()
                    .FirstOrDefault(t => t.Id == thesisId)
                    .StudentId;
                var person = uow.People.Get()
                   .FirstOrDefault(p => p.Id == studentId);
                person.UnreadNotifications++;
                var notification = new Notification()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    NotificationTypeId = "APPROVE-THESIS",
                    PersonId = studentId
                };
                uow.People.Update(person);
                uow.Notifications.Insert(notification);
                uow.SaveChanges();
            });
        }

        public void AcceptCircumstance(Guid circumstanceId)
        {
            ExecuteInTransaction(uow =>
            {
                var studentId = uow.Circumstances.Get()
                    .FirstOrDefault(c => c.Id == circumstanceId)
                    .StudentId;
                var person = uow.People.Get()
                   .FirstOrDefault(p => p.Id == studentId);
                person.UnreadNotifications++;
                var notification = new Notification()
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRead = false,
                    NotificationTypeId = "APPROVE-CIRCUMSTANCE",
                    PersonId = studentId
                };
                uow.People.Update(person);
                uow.Notifications.Insert(notification);
                uow.SaveChanges();
            });
        }
    }
}
