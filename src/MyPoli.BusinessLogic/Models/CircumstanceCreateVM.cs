using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class CircumstanceCreateVM : IValidatableObject
    {
        [Required(ErrorMessage = "Student is Required")]
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "StartDate is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is Required")]
        public DateTime EndDate { get; set; }
        public SelectList StudentIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(EndDate > DateTime.Now || StartDate > DateTime.Now)
            {
                yield return new ValidationResult("Data unrealistic", new List<string> { nameof(EndDate), nameof(StartDate) });
            }
            if(StartDate <= DateTime.MinValue || StartDate >= DateTime.MaxValue || EndDate <= DateTime.MinValue || EndDate >= DateTime.MaxValue)
            {
                yield return new ValidationResult("Data out of boundaries", new List<string> { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}
