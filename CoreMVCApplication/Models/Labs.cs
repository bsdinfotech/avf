using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Labs
    {
        [Key]
        public int Lab_Id { get; set; }
        public string PetName { get; set; }
        public int Appointment_type { get; set; }
        public string Appointment_type_Name { get; set; }
        public string LabType { get; set; }
        public string Lab_Notes { get; set; }
        public int PetParent_Id { get; set; }
        public string PetParent_Name { get; set; }
        public string DateRequested { get; set; }
    }
}
