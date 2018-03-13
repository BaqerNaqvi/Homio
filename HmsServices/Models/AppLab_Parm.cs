using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppLab_Parm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string NormarVal { get; set; }
        public bool Status { get; set; }
        public int Price { get; set; }

        public  List<AppLab_mapping> Lab_Mappings { get; set; }

    }


    public class AppLab_ParmDd
    {
        public long id { get; set; }
        public string Name { get; set; }

        public string NorVal { get; set; }
        public int Price { get; set; }

    }

  
    public static class LabParmMapper
    {
        public static AppLab_Parm Mapper(this Lab_Parms source)
        {
            List<AppLab_mapping> mapping = null;
            if (source.Lab_Mapping != null && source.Lab_Mapping.Any())
            {
                mapping = source.Lab_Mapping.Select(map => map.Mapper()).ToList();
            }
            return new AppLab_Parm
            {
                Id = source.Id,
                Name = source.Name,
                Status = source.Status,
                Price= source.Price??0,
              NormarVal = source.NormarVal,
              Unit = source.Unit,
              Lab_Mappings = mapping
            };
        }

        public static AppLab_ParmDd MapperDd(this Lab_Parms source)
        {
            return new AppLab_ParmDd
            {
                id = source.Id,
                Name = source.Name,
                Price = source.Price ?? 0,
                NorVal = source.NormarVal
            };
        }
    }



    public class AppLab_Parm_ForPatient  // custom 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NormarVal { get; set; }
        public string ActualVal { get; set; }
     //   public int Price { get; set; }

        public long TestId { get; set; }

        public long ParmId { get; set; }

        public long PatientLabId { get; set; }
    }

    public static class LabParmMapper_ForPatient
    {
        public static AppLab_Parm_ForPatient Mapper_LabParmMapper_ForPatient( PatientLabs_Labs_Parms parm, long testId)
        {
            return new AppLab_Parm_ForPatient
            {
                Id = parm.Id,
                NormarVal = parm.Lab_Parms.NormarVal,
                Name = parm.Lab_Parms.Name,
                ActualVal = parm.ParmValue,
                TestId = testId,
                //Price = source.Price ?? 0,
                ParmId = parm.ParmId
            };
        }
    }
}
