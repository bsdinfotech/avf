using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CoreMVCApplication.Models
{
    public class Patient
    {
		public int PetParent_Id { get; set; }
		public string FullName { get; set; }
		public string Gender { get; set; }
		public string Mobile_no { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Notes { get; set; }

        public int Pet_id { get; set; }
        public int PetParent { get; set; }
        public string PetName { get; set; }
        public string Breed { get; set; }
        public string PetType { get; set; }
        public string Pet_Image { get; set; }
        public string PetGender { get; set; }
        public string Pet_DOB { get; set; }
        public int Pet_Weight { get; set; }
        public string Colour { get; set; }
        public string AdoptedOn { get; set; }
        public string MicrochipNo { get; set; }
        public string DateOfChipping { get; set; }
        public string PetNotes { get; set; }
        public bool Status { get; set; }
        public IFormFile PetImage { get; set; }
        public string ExistingImage { get; set; }
        public string Spay_Meutered { get; set; }
        public string Loc_Chipping { get; set; }
        public string Insurance { get; set; }
        public string qr { get; set; }

        public string city { get; set; }
        public int pincode { get; set; }
        public string area { get; set; }

        public string QRCodeText { get; set; }
       
    }
}
