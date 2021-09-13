using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models
{
    public class CircumstanceEditVM
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        //[Required(ErrorMessage = "StudentId is Required")]
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "StartDate is Required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is Required")]
        public DateTime EndDate { get; set; }
        public SelectList StudentIds { get; set; }
    }
}
