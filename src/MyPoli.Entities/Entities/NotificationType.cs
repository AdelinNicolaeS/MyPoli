using System;
using System.Collections.Generic;

namespace MyPoli.Entities
{
    public class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }
        public String Id { get; set; }
        public String Value { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
