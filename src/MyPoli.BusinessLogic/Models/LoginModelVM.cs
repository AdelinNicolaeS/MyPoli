using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.BusinessLogic.Models
{
    public class LoginModelVM
    {
        [Required(ErrorMessage = "The email is mandatory.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password is mandatory.")]
        public string Password { get; set; }

        public LoginModelVM(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
