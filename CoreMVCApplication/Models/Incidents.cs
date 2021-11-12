using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreMVCApplication.Models
{
    public class Incidents
    {
        [Key]
        public int Incident_id { get; set; }
        public string DateofIncident { get; set; }
        public string TimeOfIncident { get; set; }
     
        public string CaseType { get; set; }
    
        public string IncidentReportedTo { get; set; }
     
        public string PatientImpocted { get; set; }
       
        public string Notes { get; set; }
    }
}
