//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HmsServices.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class IpForm
    {
        public long Id { get; set; }
        public string PatientNo { get; set; }
        public System.DateTime DateTime { get; set; }
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
    
        public virtual AspNetUser Doctor { get; set; }
    }
}
