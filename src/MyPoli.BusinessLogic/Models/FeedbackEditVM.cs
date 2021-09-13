using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class FeedbackEditVM
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Subject is Required")]
        public string LectureOpinion { get; set; }
        [Required(ErrorMessage = "Opinion about Seminar is Required")]
        public string SeminarOpinion { get; set; }
        [Required(ErrorMessage = "Lecture Grade is Required")]
        public int LectureGrade { get; set; }
        [Required(ErrorMessage = "Seminar Grade is Required")]
        public int SeminarGrade { get; set; }
        public SelectList SubjectIds { get; set; }
    }
}
