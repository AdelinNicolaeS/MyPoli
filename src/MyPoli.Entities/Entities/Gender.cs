using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Gender : IEntity
    {
        public Gender()
        {
            People = new HashSet<Person>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
