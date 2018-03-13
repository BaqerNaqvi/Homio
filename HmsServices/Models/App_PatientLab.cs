using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class App_PatientLab
    {
        public long Id { get; set; }
        public string PatientNo { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string RegisteredBy { get; set; }
        public string PerformedBy { get; set; }
        public string RequestedOn { get; set; }
        public string ReportedOn { get; set; }
        public string BloodGroup { get; set; }
        public string GuardianName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Nullable<int> Discount { get; set; }
        public string DiscountBy { get; set; }
        public Nullable<int> Amount { get; set; }

        public Nullable<bool> MaritalStatus { get; set; }
    }

    public static class MapperLab
    {
        public static App_PatientLab MapPatientLab(this PatientLab source)
        {
            return new App_PatientLab
            {
                Id = source.Id,
                PatientNo = source.PatientNo??"",
                Address = source.Address??"",
                Age = source.Age,
                BloodGroup = source.BloodGroup,
                Gender = source.Gender,
                GuardianName = source.GuardianName,
                Name = source.Name,
                PerformedBy = source.PerformedBy??"",
                Phone = source.Phone,
                RegisteredBy = source.RegisteredBy,
                ReportedOn = source.Reported.ToString(),
                RequestedOn = source.Requested.ToString(),
                MaritalStatus = source.MaritalStatus,
                Amount = source.Amount,
                DiscountBy = source.DiscountBy,
                Discount= source.Discount
            };
        }
    }
}
