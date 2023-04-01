using System;
using System.Collections.Generic;

namespace MyPoli.Common.DTOs
{
    public class CurrentUserDto // userul curent logat in aplicatie, daca nu e nimeni e 
    {
        public CurrentUserDto()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; }
        public int UnreadNotifications { get; set; }
        public List<string> Roles { get; set; }

    }
}
