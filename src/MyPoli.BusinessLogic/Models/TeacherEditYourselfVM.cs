using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.StudentOperations;

namespace MyPoli.BusinessLogic.Models
{
    public class TeacherEditYourselfVM : IValidatableObject
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthday is Required")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nationality is Required")]
        public Guid NationalityId { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public Guid GenderId { get; set; }

        public SelectList NationalityIds { get; set; }
        public SelectList GenderIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var studentService = validationContext.GetService(typeof(StudentService)) as StudentService;
            if (studentService.EmailAlreadyUsed(Id, Email))
            {
                yield return new ValidationResult("Email already used", new List<string>() { nameof(Email) });
            }
        }
    }
}
