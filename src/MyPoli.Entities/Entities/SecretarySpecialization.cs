using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class SecretarySpecialization : IEntity
    {
        public Guid IdSecretary { get; set; }
        public Guid IdSpecialization { get; set; }

        public virtual Secretary IdSecretaryNavigation { get; set; }
        public virtual Specialization IdSpecializationNavigation { get; set; }
    }
}
