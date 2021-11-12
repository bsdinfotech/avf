using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Short
    {
        [Key]
        public int Short_id { get; set; }

        public string type_Master { get; set; }

        public bool status { get; set; }

        //public string Employment_type { get; set; }
        //public string bread_type { get; set; }
        //public string prep_type { get; set; }
        public string short_name { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
    }
}
