using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreMVCApplication.Models
{
    public class FileUpload
    {
        public int id { get; set; }
        public string file{ get; set; }
        public IFormFile fileUpload{ get; set; }
    }
}
