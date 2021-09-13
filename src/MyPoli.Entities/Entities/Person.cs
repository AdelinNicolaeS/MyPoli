using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Person : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PasswordHash { get; set; }    // new
        public int RoleId { get; set; }     // new
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid NationalityId { get; set; }
        public Guid GenderId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Secretary Secretary { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<PersonRole> PersonRoles { get; set; }

    }
}
