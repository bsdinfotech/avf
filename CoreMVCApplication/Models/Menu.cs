using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Models
{
    public class Menu
    {
        public int MID { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string MenuParent{get;set;}
    }
}
