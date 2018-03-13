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

    public class GetPatientWithLabsResponseModel
    {
        public List<App_PatientLab> PatientWithLabs { get; set; }
        public PaggingModel Pagging { get; set; }
    }

    public class LabParmsResponseModel
    {
        public List<AppLab_Parm> LabParms { get; set; }
        public PaggingModel Pagging { get; set; }
    }

    public class LabParmsResponseModelDd
    {
        public List<AppLab_ParmDd> LabParms { get; set; }
    }

    public class LabTestResponseModelDd
    {
        public List<AppLab_TestDd> LabTests { get; set; }
    }

    public class LabMappingResponseModel
    {
        public string TestName { get; set; }
        public List<AppLab_mapping> LabMapping { get; set; }
    }
}
