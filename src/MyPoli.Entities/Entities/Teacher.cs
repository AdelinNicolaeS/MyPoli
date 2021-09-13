using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Teacher : IEntity
    {
        public Teacher()
        {
            SubjectTeachers = new HashSet<SubjectTeacher>();
            TeacherGroups = new HashSet<TeacherGroup>();
            Theses = new HashSet<Thesis>();
          //  Grades = new HashSet<Grade>();
        }

        public Guid Id { get; set; }
        public decimal? Salary { get; set; }
        public int? Experience { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<TeacherGroup> TeacherGroups { get; set; }
        public virtual ICollection<Thesis> Theses { get; set; }
       // public virtual ICollection<Grade> Grades { get; set; }
    }
}
