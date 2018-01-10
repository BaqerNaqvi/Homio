using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppIp
    {
        public long Id { get; set; }
        public string PatientNo { get; set; }
        public string DateTime { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DoctorId { get; set; }
        public bool MartialStatus { get; set; }
        public int AdvanceAmount { get; set; }
        public string Caste { get; set; }
        public string BloodGroup { get; set; }
        public Nullable<long> OpdId { get; set; }

        public int AdmissionFee { get; set; }

        public string DocName { get; set; }

        public string Degree { get; set; }

        ///////
        public int SerialNo { get; set; }

        public int MonthlyNo { get; set; }

        public int RoomId { get; set; }
    }

    public static class AppIpMapper
    {
        public static AppIp MaptoIp(this IpForm source)
        {
            return new AppIp
            {
                DateTime = source.DateTime.ToLongDateString() + " " + source.DateTime.ToShortTimeString(),
                Address = source.Address,
                Age = source.Age,
                CNIC = source.CNIC,
                DocName = source.Doctor.Title + " " + source.Doctor.FirstName + " " + source.Doctor.LastName,
                DoctorId = source.DoctorId,
                Degree= source.Doctor.Degree,
                Gender = source.Gender,
                GuardianName = source.GuardianName,
                Id = source.Id,
                Name = source.Name,
                PatientNo = source.PatientNo,
                Phone = source.Phone,
                Caste = source.Caste,
                MartialStatus = source.MartialStatus,
                BloodGroup = source.BloodGroup,
                AdvanceAmount = source.AdvanceAmount,
                OpdId = source.OpdId,
                AdmissionFee= source.AdmissionFee
            };
        }
    }
}
