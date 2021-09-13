using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoli.Common;

namespace MyPoli.Entities
{
    public partial class Thesis : IEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
        public bool ApprovedByTeacher { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
