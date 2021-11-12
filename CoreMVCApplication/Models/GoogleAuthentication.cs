using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Models
{
    public class GoogleAuthentication
    {
        public string Issuer { get; set; }
        public string OriginalIssuer { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
