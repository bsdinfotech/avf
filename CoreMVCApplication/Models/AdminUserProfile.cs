using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Models
{
    public class AdminUserProfile
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool Status { get; set; }
    }
}
