using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices.Models
{
    public class CreateMixReportModel
    {
        public string Name { get; set; }

        public int Fee { get; set; }


        public long PatientLabId { get; set; }

        public List<long> ParmIds { get; set; }

    }
}
