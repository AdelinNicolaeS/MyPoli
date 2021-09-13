using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoli.Common;

namespace MyPoli.Entities
{
    public class Feedback : IEntity
    {
        public Guid Id { get; set; }
        public Guid IdSubject { get; set; }
        public Guid IdStudent { get; set; }
        public string LectureOpinion { get; set; }
        public int LectureGrade { get; set; }
        public string SeminarOpinion { get; set; }
        public int SeminarGrade { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual StudentSubject StudentSubject { get; set; }
    }
}
