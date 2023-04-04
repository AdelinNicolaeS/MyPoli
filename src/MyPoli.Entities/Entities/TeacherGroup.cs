using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class TeacherGroup : IEntity
    {
        public TeacherGroup()
        {
           // Grades = new HashSet<Grade>();
        }

        public Guid IdTeacher { get; set; }
        public Guid IdGroup { get; set; }

        public virtual Group IdGroupNavigation { get; set; }
        public virtual Teacher IdTeacherNavigation { get; set; }
        //public virtual ICollection<Grade> Grades { get; set; }
    }
}
