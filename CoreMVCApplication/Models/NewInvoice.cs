using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class NewInvoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public int PatientID { get; set; }
        public string InvoiceTo { get; set; }
        public string AdmissionDate { get; set; }
        public string AppointmentType { get; set; }
        public string Descriptions { get; set; }
        public int Quantity { get; set; }
        public int ActualCharges { get; set; }
        public int Discount { get; set; }
        public string TotalRupees { get; set; }
        public string Remarks { get; set; }
        public string PetName { get; set; }
        public int PetParent_Id { get; set; }
        public string Pharmacy_name { get; set; }
        public string Appointment_id { get; set; }
        public string PetS_ParentName { get; set; }


    }
}
