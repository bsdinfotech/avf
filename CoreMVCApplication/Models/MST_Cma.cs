using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Models
{
    public class MST_Cma
    {
	public int 	cma_id { get; set; }
		public string cma_name { get; set; }
		public string cma_shortname { get; set; }
		public int cma_parent { get; set; }
		public string cma_type { get; set; }
		public bool cma_Status { get; set; }
		public string cma_country { get; set; } 
		public string cma_category { get; set; }
		public string file { get; set; }
		public IFormFile fileUpload { get; set; }

	}
}
