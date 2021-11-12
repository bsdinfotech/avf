using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CoreMVCApplication.Models
{
    public class MenuMaster
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool Views { get; set; }
        public bool Adds { get; set; } 
        public bool Modify { get; set; }
        public bool inquire { get; set; }
        public bool deleted { get; set; }
        public bool Parent { get; set; }
        public string PageName { get; set; }
        public string MenuParents { get; set; }
        public string MenuImage { get; set; }
        public IFormFile MenuImageFile { get; set; }
        public int PrintOrder { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public bool Status { get; set; }
        public string ExistingImage { get; set; }
    }
}
