using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class NewPharmacyRequest
    {
        [Key]
        public int NewPharmacy_id { get; set; }
        public string Pet_id { get; set; }
        public string Appointment_type { get; set; }
        public string Appointment_type_name { get; set; }
        public string Pharmacy_name { get; set; }
        public string Prescription_Date { get; set; }
        public string Prescription { get; set; }
        public int Quantity { get; set; }
        public string Refills { get; set; }
        public string Bill_To { get; set; }
        public int Quantity_ToReturn { get; set; }
        public string Return_Reason { get; set; }
        public string Return_Location { get; set; }
        public string Adjustment_Date { get; set; }
        public string Credit_ToAccount { get; set; }
        public string PetParent_Id { get; set; }
    }
}
