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
    
    public partial class PatientLabs_Labs
    {
        public long Id { get; set; }
        public long PatientLabId { get; set; }
        public long TestId { get; set; }
    
        public virtual Lab_Tests Lab_Tests { get; set; }
        public virtual PatientLab PatientLab { get; set; }
    }
}
