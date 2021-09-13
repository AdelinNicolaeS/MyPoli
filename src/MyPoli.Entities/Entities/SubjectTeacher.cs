using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class SubjectTeacher : IEntity
    {
        public SubjectTeacher()
        {
            Grades = new HashSet<Grade>();
        }
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
