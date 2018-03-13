using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace HMS.Models.ReportModels
{
    public class OpdSlipReportModel
    {
        public string Name { get; set; }
        public string DateTime { get; set; }
        public int Amount { get; set; }
        public string DoctorName { get; set; }
        public string RecievedBy { get; set; }
        public string TokenNo { get; set; }
        public int Age { get; set; }
        public string PatientId { get; set; }

        public int VisitNo { get; set; }
    }


    public class LabSlipReportModel
    {
        public string FullName { get; set; }
        public string DateTime { get; set; }
        public int Amount { get; set; }
        
        public string Id { get; set;
        }      
        public string RecievedBy { get; set; }
      
    }
}