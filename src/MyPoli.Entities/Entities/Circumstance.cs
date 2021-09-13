using System;
using MyPoli.Common;

namespace MyPoli.Entities
{
    public partial class Circumstance : IEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Accepted { get; set; }

        public virtual Student Student { get; set; }
    }
}
