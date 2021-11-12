using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please Enter Email..")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password..")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "NewPassword")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Mobile_No { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


    }
}
