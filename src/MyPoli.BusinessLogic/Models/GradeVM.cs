using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class GradeVM
    {
        public Guid IdSubject { get; set; }
        public Guid IdStudent { get; set; }
        public int GradeValue { get; set; }
        public SelectList SubjectIds { get; set; }
        public SelectList StudentIds { get; set; }
        public SelectList GroupIds { get; set; }
        public Guid IdGroup { get; set; }
    }
}
