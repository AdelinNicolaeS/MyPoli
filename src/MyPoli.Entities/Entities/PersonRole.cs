using MyPoli.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoli.Entities
{
    public class PersonRole : IEntity
    {
        public Guid PersonId { get; set; }
        public int RoleId { get; set; }
        public Person Person { get; set; }
        public Role Role { get; set; }
    }
}
