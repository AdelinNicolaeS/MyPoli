using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.BusinessLogic.Models
{
    public class TeacherOneNameVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
