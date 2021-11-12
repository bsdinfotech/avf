using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class PetParent
    {
		[Key]
		public int PetParent_Id { get; set; }
		public string FullName { get; set; }
		public string Gender { get; set; }
		public string Mobile_no { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Notes { get; set; }
		public bool Status { get; set; }
		


	}
}
