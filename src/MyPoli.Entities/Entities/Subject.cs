using System;
using System.Collections.Generic;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Subject : IEntity
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            SubjectTeachers = new HashSet<SubjectTeacher>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public bool IsDeleted { get; set; }
    }
}
