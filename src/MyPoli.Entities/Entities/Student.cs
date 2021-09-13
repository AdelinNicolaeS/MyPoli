using System;
using System.Collections.Generic;
using MyPoli.Common;
using MyPoli.Entities;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Student : IEntity
    {
        public Student()
        {
            Certificates = new HashSet<Certificate>();
            StudentSubjects = new HashSet<StudentSubject>();
            Circumstances = new HashSet<Circumstance>();
        }

        public Guid Id { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Thesis Thesis { get; set; }
        public virtual Group Group { get; set; }
        public virtual Person Person { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Circumstance> Circumstances { get; set; }
    }
}
