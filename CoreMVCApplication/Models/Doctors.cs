using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Doctors
    {
        [Key]
        public int  Dr_id { get; set; }
      
        public string Dr_Name { get; set; }
        public string Dr_Gender { get; set; }
        public string Dr_Designation { get; set; }
        public string Dr_Fees { get; set; }
        public string Dr_Time { get; set; }
        public  string  Dr_Email { get; set; }
        public string Dr_MobileNo { get; set; }
        public string Dr_Address { get; set; }
        public bool Dr_status { get; set; }
    }
}
