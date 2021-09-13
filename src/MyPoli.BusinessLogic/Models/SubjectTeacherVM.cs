using System;

namespace MyPoli.BusinessLogic.Models
{
    public class SubjectTeacherVM
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList SubjectIds { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList TeacherIds { get; set; }
    }
}
