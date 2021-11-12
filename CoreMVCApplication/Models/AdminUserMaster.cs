using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class AdminUserMaster
    {
        
        public int id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Place_of_Posting { get; set; }
      
        public string Profile_Image_Name { get; set; }
        [Required]
        public string Mobile_No { get; set; }
        [Required]
        public string Telephone_No { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool Status { get; set; }
        public string profiletype { get; set; }
        //public string Usertype { get; set; }
        public string username { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ExistingImage { get; set; }

        public string ReEnterPassword { get; set; }


    }
}
