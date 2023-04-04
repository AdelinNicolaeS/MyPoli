using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Grade : IEntity
    {
        public Guid IdSubject { get; set; }
        public Guid IdStudent { get; set; }

        public Guid IdTeacher { get; set; }
        public int GradeValue { get; set; }

        public virtual StudentSubject StudentSubject { get; set; }
        public virtual SubjectTeacher SubjectTeacher { get; set; }
        public bool IsDeleted { get; set; }
    }
}
