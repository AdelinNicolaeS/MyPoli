using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.BusinessLogic.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "The email is mandatory.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The old password is mandatory.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "The new password is mandatory.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "The confirm password is mandatory.")]
        [Compare("NewPassword", ErrorMessage = "The password and the confirm password must be the same.")]
        public string ConfirmPassword { get; set; }
    }
}
