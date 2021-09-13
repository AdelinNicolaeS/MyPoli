using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Nationality : IEntity
    {
        public Nationality()
        {
            People = new HashSet<Person>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
