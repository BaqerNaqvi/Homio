using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class App_PatientLabs_Labs
    {
          public long Id { get; set; }
        public long PatientLabId { get; set; }
        public long TestId { get; set; }
        public string TestName { get; set; }
    
        public  AppLab_Test Lab_Test { get; set; }

        public List<AppLab_Parm_ForPatient> ParmForPatientLabs { get; set; }

        // custom prop

        public int TotalFee { get; set; }
    }

    public static class App_PatientLabs_LabsMapper
    {
        public static App_PatientLabs_Labs MapperHm(this PatientLabs_Labs source)
        {
            var list = new List<AppLab_Parm_ForPatient>();
            foreach (var labMapping in source.Lab_Tests.Lab_Mapping)
            {
                var somethign = labMapping.Lab_Parms.PatientLabs_Labs_Parms.FirstOrDefault(m => m.TestId == source.TestId && m.PatientLabId==source.PatientLabId);
                var actualOb = LabParmMapper_ForPatient.Mapper_LabParmMapper_ForPatient(somethign, source.TestId);
                list.Add(actualOb);
            }
            return new App_PatientLabs_Labs
            {
                Id = source.Id,
                TestId = source.TestId,
                Lab_Test = source.Lab_Tests.Mapper(),
                TestName = source.Lab_Tests.Name,
                ParmForPatientLabs = list,
                PatientLabId= source.PatientLabId
            };
        }
    }
}
