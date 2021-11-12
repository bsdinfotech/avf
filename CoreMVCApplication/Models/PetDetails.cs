using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVCApplication.Models
{
    public class PetDetails
    {
        [Key]
        public int Pet_id { get; set; }
        public int PetParent_Id { get; set; }
        public int PetParent { get; set; }
        public string PetName { get; set; }
        public int BreedId { get; set; }
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
        public string Notes { get; set; }
        public bool Status { get; set; }
        public IFormFile PetImage { get; set; }
        public string ExistingImage { get; set; }
        public string  Spay_Meutered { get; set; }
        public string Loc_Chipping { get; set; }
        public string  Insurance { get; set; }
        public string PetNotes { get; set; }
    }
}
