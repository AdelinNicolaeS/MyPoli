using MyPoli.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoli.Entities
{
    public class Role : IEntity
    {
        public Role()
        {
            PersonRoles = new HashSet<PersonRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PersonRole> PersonRoles { get; set; }
    }
}
