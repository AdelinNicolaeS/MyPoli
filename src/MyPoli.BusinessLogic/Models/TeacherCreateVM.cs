using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.StudentOperations;
using MyPoli.BusinessLogic.Implementation.TeacherOperations;

namespace MyPoli.BusinessLogic.Models
{
    public class TeacherCreateVM : IValidatableObject
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthday is Required")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nationality is Required")]
        public Guid NationalityId { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public Guid GenderId { get; set; }
        [Required(ErrorMessage = "Salary is Required")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "Experience is Required")]
        public int? Experience { get; set; }
        public SelectList GenderIds { get; set; }
        public SelectList NationalityIds { get; set; }

        public MultiSelectList SubjectIds { get; set; }
        public MultiSelectList GroupIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var studentService = validationContext.GetService(typeof(StudentService)) as StudentService;
            if (studentService.EmailNotAvailable(Email))
            {
                yield return new ValidationResult("Email already used", new List<string>() { nameof(Email) });
            }
        }
    }
}
