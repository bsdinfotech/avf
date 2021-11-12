using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Report
    {
        [Key] 
        public string Pet_DOB { get; set; }
        public int Lab_Id { get; set; }
        public int Pet_id { get; set; }
        public string PetParent_Id { get; set; }
        public string FullName { get; set; }
        public string PetName { get; set; }
        public string Age { get; set; }
        public string PetBreedName { get; set; }
        public string GenderName { get; set; }
        public string Mobile_No { get; set; }
        public string Email { get; set; }
        public int Clientcode { get; set; }
        public string ReDate { get; set; }
        public string Ref { get; set; }
        public string COLOR { get; set; }
        
    }
}
