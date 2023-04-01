using MyPoli.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.Entities
{
    public class Notification : IEntity
    {
        public Guid Id { get; set; }
        public Boolean IsRead { get; set; }
        public String NotificationTypeId { get; set; }
        public Guid PersonId { get; set; }
        public DateTime Date { get; set; }

        public virtual NotificationType NotificationType { get; set; }
        public virtual Person Person { get; set; }
    }
}
