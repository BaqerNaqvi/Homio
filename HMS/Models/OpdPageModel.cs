using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HmsServices.Models;

namespace HMS.Models
{
    public class OpdPageModel
    {
        public List<AppUserDoc_Dd> DoctorList { get; set; }
    }


    public class IpPageModel
    {


        public AppIp GeneralData { get; set; }

        public List<AppUserDoc_Dd> DoctorList { get; set; }

        public List<AppWardsRoom> WardsList { get; set; }
    }
}