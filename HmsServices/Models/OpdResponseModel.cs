using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices.Models
{
    public class OpdResponseModel
    {
        public List<AppOpd> AppOpds { get; set; }
        public PaggingModel Pagging { get; set; }
    }


    public class IpResponseModel
    {
        public List<AppIp> AppIps { get; set; }
        public PaggingModel Pagging { get; set; }
    }

    public class LabTestMgmtResponseModel
    {
        public List<AppLab_Test> LabTest { get; set; }
        public PaggingModel Pagging { get; set; }
    }
}
