using MyPoli.Common;
using System;
using System.Collections.Generic;

namespace MyPoli.Entities
{
    public class User : IEntity
    {
        public User()
        {
            PersonRoles = new HashSet<PersonRole>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte GenderId { get; set; }
        public byte PrivacyId { get; set; }

        public virtual ICollection<PersonRole> PersonRoles { get; set; }
    }
}
