using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Models
{
    public class MarketPlace
    {
      public int MaketPlace_Id { get; set; }
        public string ItemName { get; set; }
        public string Market_Category { get; set; }
        public string  Min_Quantitiy { get; set; }
        public string Price { get; set; }
        public string product_Img { get; set; }
        public IFormFile ItemImg { get; set; }
    }
}
