using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class ThesisEditVM
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public IFormFile Content { get; set; }
        public bool ApprovedByTeacher { get; set; }
        public SelectList StudentIds { get; set; }
        public SelectList TeacherIds { get; set; }
        public byte[] OldContent { get; set; }
    }
}
