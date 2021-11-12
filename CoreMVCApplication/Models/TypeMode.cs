using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CoreMVCApplication.Models
{
    public class TypeMode
    {
        [Key]
        public int TypeMaster_id { get; set; }
    
        public string type { get; set; }
       
        public bool status { get; set; }
        
        //public string Employment_type { get; set; }
        //public string bread_type { get; set; }
        //public string prep_type { get; set; }
    }
}
