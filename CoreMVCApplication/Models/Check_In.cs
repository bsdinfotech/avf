using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Check_In
    {
		[Key]
		public int CheckId { get; set; }
		public int patientId { get; set; }
		public string PetS_OwnerName { get; set; }
		public string petName { get; set; }
		public int Weight { get; set; }
		public string BodyTem { get; set; }
		public string Admission_Date { get; set; }
		public string Admission_Time { get; set; }
		public string Lvaccination_Date { get; set; }
		public string Visit_Type { get; set; }
		public string Ression_for_visit { get; set; }
		
	}
}
