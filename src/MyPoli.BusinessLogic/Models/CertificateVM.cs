using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPoli.BusinessLogic.Models

{
    public class CertificateVM
    {
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Student is Required")]
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Reason is Required")]
        public string Reason { get; set; }
        public SelectList StudentIds { get; set; }
    }
}
