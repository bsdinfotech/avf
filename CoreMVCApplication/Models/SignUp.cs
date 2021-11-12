using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class SignUp
    {
        [Required (ErrorMessage ="Please Enter Name")]
        public string FullName {get;set;}
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress (ErrorMessage ="Please Enter Valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Re Password")]
        public string RePassword { get; set; }
    }
}
