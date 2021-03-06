﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppOpd
    {
        public long Id { get; set; }
        public long DailyNo { get; set; }
        public string PatientNo { get; set; }
        public string DateTime { get; set; }
        public string Name { get; set; }
        public string GuardianName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<int> VisitNo { get; set; }
        public string DoctorId { get; set; }

        public string DocName { get; set; }
        public bool MartialStatus { get; set; }

        public int DocFee { get; set; }

        public string Degree { get; set; }
        public bool Status { get; set; }
        public string Mode { get; set; }
        public Nullable<int> Discount { get; set; }
        public string DiscountBy { get; set; }
        public string InsuranceNo { get; set; }

    }

    public static class AppOpdMapper
    {
        public static AppOpd MaptoOpd(this OPD source)
        {
            if (source == null)
            {
                return null;
            }
            return new AppOpd
            {
                DateTime = source.DateTime.ToLongDateString()+" "+source.DateTime.ToShortTimeString(),
                Address = source.Address,
                Age = source.Age,
                CNIC = source.CNIC,
                DailyNo = source.DailyNo,
                DocName = source.Doctor.Title+" "+ source.Doctor.FirstName+" "+ source.Doctor.LastName,
                DoctorId = source.DoctorId,
                Gender = source.Gender,
                GuardianName = source.GuardianName,
                Id = source.Id,
                Name = source.Name,
                PatientNo = source.PatientNo,
                Phone = source.Phone,
                VisitNo = source.VisitNo,
                MartialStatus = source.MartialStatus,
                DocFee= source.Doctor.Fee,
                Status =(bool) source.Status,
                Degree = source.Doctor.Degree,
                Discount= source.Discount??0,
                DiscountBy= source.DiscountBy,
                InsuranceNo = source.InsuranceNo
            };
        }

        public static AppOpd_RowModel MaptoOpd_Row(this OPD source)
        {
            return new AppOpd_RowModel
            {
                DateTime = source.DateTime.ToLongDateString() + " " + source.DateTime.ToShortTimeString(),              
                DailyNo = source.DailyNo,
                DocName = source.Doctor.Title + " " + source.Doctor.FirstName + " " + source.Doctor.LastName,
                DoctorId = source.DoctorId,               
                Id = source.Id,               
                VisitNo = source.VisitNo,
                DocFee = source.Doctor.Fee,              
                Discount = source.Discount??0,
            };
        }

        public static AppIp ConvertToIpForm(this OPD source)
        {
            return new AppIp
            {
                DateTime = source.DateTime.ToLongDateString() + " " + source.DateTime.ToShortTimeString(),
                Address = source.Address,
                Age = source.Age,
                CNIC = source.CNIC,
                DocName = source.Doctor.Title + " " + source.Doctor.FirstName + " " + source.Doctor.LastName,
                DoctorId = source.DoctorId,
                Gender = source.Gender,
                GuardianName = source.GuardianName,
                Id = 0,
                Name = source.Name,
                PatientNo = source.PatientNo,
                Phone = source.Phone,
                Caste = null,
                MartialStatus = source.MartialStatus,
                BloodGroup = "B+",
                AdvanceAmount = 0,
                OpdId = source.Id
            };
        }
    }



    public class AppOpd_RowModel
    {
        public long Id { get; set; }
        public long DailyNo { get; set; }
     

        public string DateTime { get; set; }
      
        public Nullable<int> VisitNo { get; set; }
        public string DoctorId { get; set; }

        public string DocName { get; set; }
        public int DocFee { get; set; }

        public Nullable<int> Discount { get; set; }

    }


    public class AppOpd_RowModelModel
    {
        public List<AppOpd_RowModel> Records { get; set; }

        public AppOpd Opd { get; set; }
    }
}
