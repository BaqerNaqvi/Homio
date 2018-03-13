using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices.Models
{
    public class AddLabToPatientResponseModel
    {
        public List<AppLab_TestDd> LabTestsDd { get; set; }

        public App_PatientLab PatientInfo { get; set; }

        public List<App_PatientLabs_Labs> PatientLabs { get; set; }
    }
}
