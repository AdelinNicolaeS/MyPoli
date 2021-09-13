using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.ThesisOperations;

namespace MyPoli.BusinessLogic.Models
{
    public class ThesisCreateVM : IValidatableObject
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Student is Required")]
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Teacher is Required")]
        public Guid TeacherId { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(3000, ErrorMessage = "{0} cannot exceed {1} limit")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Content is Required")]
        public IFormFile Content { get; set; }
        public bool ApprovedByTeacher { get; set; }
        public SelectList TeacherIds { get; set; }
        public SelectList StudentIds { get; set; }
        public string StudentName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var thesisService = validationContext.GetService(typeof(ThesisService)) as ThesisService;
            if(thesisService.StudentAlreadyHasThesis(StudentId))
            {
                yield return new ValidationResult("Studentul are deja licenta", new List<string>() { nameof(StudentId), nameof(StudentName) });
            }
            if(Date <= DateTime.Now)
            {
                yield return new ValidationResult("Data trebuie sa fie in viitor", new List<string>() { nameof(Date) });
            }
        }
    }
}
