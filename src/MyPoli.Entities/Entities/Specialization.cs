using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Specialization : IEntity
    {
        public Specialization()
        {
            SecretarySpecializations = new HashSet<SecretarySpecialization>();
            Groups = new HashSet<Group>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SecretarySpecialization> SecretarySpecializations { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
