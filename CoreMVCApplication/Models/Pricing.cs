using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CoreMVCApplication.Models
{
    public class Pricing
    {
        public int Pricing_Id { get; set; }
        public string ItemName { get; set; }
        public string Category {get;set;}
        public string price { get; set; }
    }
}
