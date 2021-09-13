using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPoli.BusinessLogic.Implementation.GroupOperations;

namespace MyPoli.BusinessLogic.Models
{
    public class GroupVM : IValidatableObject
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "SpecializationId is Required")]
        public Guid? SpecializationId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(5, ErrorMessage = "The {0} value must have {1} characters. ")]
        public string Name { get; set; }

        public SelectList SpecializationIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var groupService = validationContext.GetService(typeof(GroupService)) as GroupService;
            if(groupService.NameAlreadyUsed(Id, Name))
            {
                yield return new ValidationResult("Name already used", new List<string>() { nameof(Name)});
            }
        }
    }
}
