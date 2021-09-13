using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoli.Common;

namespace MyPoli.Entities
{
    public class Certificate : IEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

        public virtual Student Student { get; set; }
    }

}
