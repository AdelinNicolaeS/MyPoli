using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Secretary : IEntity
    {
        public Secretary()
        {
            SecretarySpecializations = new HashSet<SecretarySpecialization>();
        }

        public Guid Id { get; set; }
        public decimal? Salary { get; set; }
        public int? Experience { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<SecretarySpecialization> SecretarySpecializations { get; set; }
    }
}
