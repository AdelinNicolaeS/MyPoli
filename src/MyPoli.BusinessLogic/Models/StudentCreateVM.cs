using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.StudentOperations;

namespace MyPoli.BusinessLogic.Models
{
    public class StudentCreateVM : IValidatableObject
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
        [Required(ErrorMessage = "Status is Required")]
        public Guid StatusId { get; set; }
        [Required(ErrorMessage = "Group is Required")]
        public Guid GroupId { get; set; }
        [Required(ErrorMessage = "Start Date is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is Required")]
        public DateTime EndDate { get; set; }
        public Boolean IsSeenInCourse { get; set; }
        public Boolean IsSeenInGroup { get; set; }
        public SelectList GroupIds { get; set; }
        public SelectList StatusIds { get; set; }
        public SelectList NationalityIds { get; set; }
        public SelectList GenderIds { get; set; }
        public MultiSelectList SubjectIds { get; set; }

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
