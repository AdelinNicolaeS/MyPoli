using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class FeedbackCreateBySubjectVM : IValidatableObject
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Subject is Required")]
        public Guid SubjectId { get; set; }
        [Required(ErrorMessage = "Opinion about lecture is Required")]
        public string LectureOpinion { get; set; }
        [Required(ErrorMessage = "Opinion about Seminar is Required")]
        public string SeminarOpinion { get; set; }
        [Required(ErrorMessage = "Lecture Grade is Required")]
        public int LectureGrade { get; set; }
        [Required(ErrorMessage = "Seminar Grade is Required")]
        public int SeminarGrade { get; set; }
        public string SubjectName { get; set; }
        public SelectListItem Subject { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (SeminarGrade < 1 || SeminarGrade > 10)
            {
                yield return new ValidationResult("Grade out of boundaries", new List<string> { nameof(SeminarGrade) });
            }
            if (LectureGrade < 1 || LectureGrade > 10)
            {
                yield return new ValidationResult("Grade out of boundaries", new List<string> { nameof(LectureGrade) });
            }
        }
    }
}
