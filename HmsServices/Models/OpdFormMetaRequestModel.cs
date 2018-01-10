using System.Security.Policy;

namespace HmsServices.Models
{
    public class OpdFormMetaRequestModel
    {
        public string PatientNo { get; set; }

        public string DoctorId { get; set; }
    }

    public class OpdFormMetaReaponseModel
    {
        public int DailyNo { get; set; }
        public long SerialNo { get; set; }

        public string PatientNo { get; set; }
        public int VisitNo { get; set; }

        public string  DateTime { get; set; }

        public AppOpd OpdForm { get; set; }
    }
}