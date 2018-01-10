using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppLab_mapping
    {
        public long Id { get; set; }
        public long ParmId { get; set; }
        public long TestId { get; set; }
        public string CreationDate { get; set; }

        public  AppLab_Parm Lab_Parm { get; set; }
        public  AppLab_Test Lab_Test { get; set; }
    }

    public static class LabMappingMapper
    {
        public static AppLab_mapping Mapper(this Lab_Mapping source)
        {
            return new AppLab_mapping
            {
                Id = source.Id,
              CreationDate = source.CreationDate.ToShortDateString(),
           //   Lab_Parm = source.Lab_Parms.Mapper(),
            //  Lab_Test = source.Lab_Tests.Mapper(),
              ParmId = source.ParmId,
              TestId = source.TestId
            };
        }
    }
}
