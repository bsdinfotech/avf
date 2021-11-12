using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class ImagingRequest
    {
        [Key]
        public int Imaging_Id { get; set; }
        public string PetName { get; set; }
        public int Appointment_type { get; set; }
        public string Appointment_type_Name { get; set; }
        public string Imaging_Type { get; set; }
        public string Imaging_Radiologist { get; set; }
        public string Requested_By { get; set; }
        public string Imaging_Result { get; set; }
        public string Imaging_Notes { get; set; }
        public int PetParent_Id { get; set; }
        public string DateRequested { get; set; }
        public string OtherImaging_Type { get; set; }

    }
}
