using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class StudentSubject : IEntity
    {
        public Guid IdStudent { get; set; }
        public Guid IdSubject { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}
