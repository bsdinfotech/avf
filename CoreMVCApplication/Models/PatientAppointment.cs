using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
	public class PatientAppointment
	{
		[Key]
		public int Appointment_id { get; set; }
		public int CheckId { get; set; }
		public string PetName { get; set; }
		public int BreadId { get; set; }
		public string Breed { get; set; }
		public string PetType_id { get; set; }
		public string PetType { get; set; }
		public string PetTypeName { get; set; }
		public string PetS_ParentName { get; set; }
		public string Appointment_status { get; set; }
		public string Appointment_Date { get; set; }
		public string Appointment_Type { get; set; }
		public string Appointment_Time { get; set; }
		public string Appointment_Duration { get; set; }
		public string Appointment_Notes { get; set; }
		public bool Status { get; set; }
		public int Pet_Weight { get; set; }
		public int petId { get; set; }
		public int PetParent_Id { get; set; }
		public int doctorId { get; set; }
		
		public string doctor_Fees { get; set; }

		public string Visit_Type { get; set; }
		public string Ression_for_visit { get; set; }
		public string BodyTem { get; set; }
		public string Lvaccination_Date { get; set; }
		public string PrimaryDiagnoses { get; set; }
		public string SecondaryDiagnoses { get; set; }
		public string Procedures { get; set; }
		public string PatientAllergies { get; set; }
		public string Medication { get; set; }
		public string Imaging_Radiologist { get; set; }
		public string Requested_By { get; set; }
		public string Imaging_Result { get; set; }
		public string Imaging_Type { get; set; }
		public string Prescription_Date { get; set; }
		public string Prescription { get; set; }
		public int Quantity { get; set; }
		public string Refills { get; set; }
		public int FlagCheckIn { get; set; }
	}
}
