using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CoreMVCApplication.Models
{
    public class Image
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string image { get; set; }
       
    }
}
