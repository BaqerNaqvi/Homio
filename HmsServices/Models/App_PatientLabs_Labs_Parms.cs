using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class App_PatientLabs_Labs_Parms
    {
        public long Id { get; set; }
        public long ParmId { get; set; }
        public string ParmValue { get; set; }
        public long TestId { get; set; }
        public long PatientLabId { get; set; }

    }

    public static class App_PatMapper
    {
        public static App_PatientLabs_Labs_Parms Mapper(this PatientLabs_Labs_Parms source)
        {

            return new App_PatientLabs_Labs_Parms
            {
                Id = source.Id,
                TestId = source.TestId,
                ParmId = source.ParmId,
                ParmValue = source.ParmValue,
                PatientLabId = source.PatientLabId
            };
        }
    }
}
