using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPoli.Common;

#nullable disable

namespace MyPoli.Entities
{
    public partial class Group : IEntity
    {
        public Group()
        {
            Students = new HashSet<Student>();
            TeacherGroups = new HashSet<TeacherGroup>();
        }

        public Guid Id { get; set; }
        public Guid? SpecializationId { get; set; }
    
        public string Name { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherGroup> TeacherGroups { get; set; }
        public bool IsDeleted { get; set; }
    }
}
