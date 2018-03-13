using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;
using HmsServices.Models;

namespace HmsServices.Labs
{
    public static class LabService
    {
        #region Lab Test
        public static LabTestMgmtResponseModel GetLabTests(SearchModel searchModel)
        {
            var response = new LabTestMgmtResponseModel
            {
                Pagging = searchModel.Pagging,
                LabTest = new List<AppLab_Test>()
            };
            using (var dbContext = new HMSEntities())
            {
                var labs = new List<Lab_Tests>();
                if (string.IsNullOrEmpty(searchModel.SearchString))
                {
                    var tests = dbContext.Lab_Tests.ToList();
                    //response.LabTest = tests;
                    labs = tests;
                }
                else
                {
                    var tests = dbContext.Lab_Tests
                      .Where(lab => lab.Name.ToLower().Contains(searchModel.SearchString.ToLower())).ToList();
                    labs = tests;
                }

                if (!labs.Any())
                {
                    response.Pagging.Current++;
                    return response;
                }

                var itemsToSkip = response.Pagging.Current * response.Pagging.ItemPerPage;
                var actualForms = labs.Skip(itemsToSkip).Take(response.Pagging.ItemPerPage).ToList();
                response.LabTest = actualForms.Select(lap => lap.Mapper()).ToList();
                response.Pagging.TotalItems = labs.Count();
                response.Pagging.Current++;
                return response;
            }
        }

        public static LabTestResponseModelDd GetLabTestsForMapping(string term)
        {
            var response = new LabTestResponseModelDd
            {
                LabTests = new List<AppLab_TestDd>()
            };
            using (var dbContext = new HMSEntities())
            {
                if (!string.IsNullOrEmpty(term))
                {
                    var tests = dbContext.Lab_Tests
                        .Where(lab => lab.Name.ToLower().Contains(term.ToLower())).ToList();
                    if (tests.Any())
                    {
                        var list = tests.Select(l => l.MapperDd()).ToList();
                        response.LabTests = list;
                    }
                }
                else
                {
                    var tests = dbContext.Lab_Tests.ToList();
                    if (tests.Any())
                    {
                        var list = tests.Select(l => l.MapperDd()).ToList();
                        response.LabTests = list;
                    }
                }
            }
            return response;
        }


        public static AppLab_Test SetLabStatus(AppLab_Test source)
        {
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.Lab_Tests.FirstOrDefault(doc => doc.Id == source.Id);
                if (data != null)
                {
                    data.Status = source.Status;
                    dbContext.SaveChanges();
                    return data.Mapper();
                }
            }
            return source;
        }

        public static AppLab_Test AddUpdateTest(AppLab_Test source)
        {
            var test = new Lab_Tests
            {
                Name = source.Name,
                Fee = source.Fee,
                Interval = source.Interval,
                Status = true,
                Id = source.Id
            };
            using (var dbContext = new HMSEntities())
            {
                var dbtest = dbContext.Lab_Tests.FirstOrDefault(t => t.Id == source.Id);
                if (dbtest != null)
                {
                    dbtest.Name = source.Name;
                    dbtest.Fee = source.Fee;
                    dbtest.Interval = source.Interval;
                }
                else
                {
                    test.CreationDate = DateTime.Now;
                    dbContext.Lab_Tests.Add(test);
                }
                dbContext.SaveChanges();
            }
            return test.Mapper();
        }

        public static void DeleteTest(AppLab_Test source)
        {
            using (var dbContext = new HMSEntities())
            {
                var dbtest = dbContext.Lab_Tests.FirstOrDefault(t => t.Id == source.Id);
                if (dbtest != null)
                {
                    var parms = dbContext.Lab_Mapping.Where(l => l.TestId == source.Id).ToList();
                    if (parms.Any())
                    {
                        dbContext.Lab_Mapping.RemoveRange(parms);
                    }
                    dbContext.Lab_Tests.Remove(dbtest);
                    dbContext.SaveChanges();
                }
            }
        }
        #endregion

        #region Parms
        public static LabParmsResponseModel GetLabParms(SearchModel searchModel)
        {
            var response = new LabParmsResponseModel
            {
                Pagging = searchModel.Pagging,
                LabParms = new List<AppLab_Parm>()
            };
            using (var dbContext = new HMSEntities())
            {
                var labs = new List<Lab_Parms>();
                if (string.IsNullOrEmpty(searchModel.SearchString))
                {
                    var tests = dbContext.Lab_Parms.ToList();
                    //response.LabTest = tests;
                    labs = tests;
                }
                else
                {
                    var tests = dbContext.Lab_Parms
                      .Where(lab => lab.Name.ToLower().Contains(searchModel.SearchString.ToLower())).ToList();
                    labs = tests;
                }

                if (!labs.Any())
                {
                    response.Pagging.Current++;
                    return response;
                }

                var itemsToSkip = response.Pagging.Current * response.Pagging.ItemPerPage;
                var actualForms = labs.Skip(itemsToSkip).Take(response.Pagging.ItemPerPage).ToList();
                response.LabParms = actualForms.Select(lap => lap.Mapper()).ToList();
                response.Pagging.TotalItems = labs.Count();
                response.Pagging.Current++;
                return response;
            }
        }

        public static LabParmsResponseModelDd GetLabParmsByLabId(string labTestId)
        {
            var response = new LabParmsResponseModelDd
            {
                LabParms = new List<AppLab_ParmDd>()
            };
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.Lab_Parms.Where(parm => 
                parm.Lab_Mapping.Any(p => p.TestId.ToString() == labTestId)).ToList();
                if (data != null && data.Any())
                {
                    var list = data.Select(l => l.MapperDd()).ToList();
                    response.LabParms = list;
                }
            }
            return response;
        }

        public static LabParmsResponseModelDd GetLabParmsForMapping(string term)
        {
            var response = new LabParmsResponseModelDd
            {
                LabParms = new List<AppLab_ParmDd>()
            };
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.Lab_Parms.Where(parm => parm.Name.ToLower().Contains(term.ToLower())).ToList();
                if (data != null && data.Any())
                {
                  var list =  data.Select(l => l.MapperDd()).ToList();
                    response.LabParms = list;
                }
            }
            return response;
        }

        public static AppLab_Parm SetLabParmStatus(AppLab_Parm source)
        {
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.Lab_Parms.FirstOrDefault(doc => doc.Id == source.Id);
                if (data != null)
                {
                    data.Status = source.Status;
                    dbContext.SaveChanges();
                    return data.Mapper();
                }
            }
            return source;
        }

        public static AppLab_Parm AddNewParm(AppLab_Parm source)
        {
            var test = new Lab_Parms
            {
                Name = source.Name,
                Unit = source.Unit,
                NormarVal = source.NormarVal,
                Price= source.Price,
                Status = true,
                Id = source.Id
            };
            using (var dbContext = new HMSEntities())
            {
                var parm = dbContext.Lab_Parms.FirstOrDefault(p => p.Id == test.Id);
                if (parm != null)
                {
                    parm.Name = test.Name;
                    parm.Unit = test.Unit;
                    parm.Price = test.Price;
                    parm.NormarVal = test.NormarVal;
                }
                else
                {
                    dbContext.Lab_Parms.Add(test);
                }
                dbContext.SaveChanges();
            }
            return test.Mapper();
        }
        #endregion

        #region Mapping
        public static LabMappingResponseModel GetLabMapping(long labid)
        {
            var responseList = new LabMappingResponseModel();
            using (var dbContext = new HMSEntities())
            {
                var mappings = dbContext.Lab_Mapping.Where(map => map.TestId == labid).ToList();
                if (mappings.Any())
                {
                    responseList.LabMapping = mappings.Select(ma => ma.Mapper()).ToList();
                }
                responseList.TestName = dbContext.Lab_Tests.FirstOrDefault(l => l.Id == labid).Name;

            }
            return responseList;
        }

        public static void AddNewMapping(List<AppLab_mapping> source)
        {

            using (var dbContext = new HMSEntities())
            {
                foreach (var obj in source)
                {
                    var test = new Lab_Mapping
                    {
                        TestId = obj.TestId,
                        ParmId = obj.ParmId,
                        CreationDate = DateTime.Now
                    };
                    dbContext.Lab_Mapping.Add(test);
                }
                dbContext.SaveChanges();
            }
        }

        public static void DeleteMapping(AppLab_mapping source)
        {
            using (var dbContext = new HMSEntities())
            {
                var dbobj = dbContext.Lab_Mapping.FirstOrDefault(o => o.Id == source.Id);
                dbContext.Lab_Mapping.Remove(dbobj);
                dbContext.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// List View
        /// </summary>
        public static GetPatientWithLabsResponseModel GetPatientWithLabs(SearchModel searchModel)

        {
            var response = new GetPatientWithLabsResponseModel
            {
                Pagging = searchModel.Pagging,
                PatientWithLabs = new List<App_PatientLab>()
            };
            using (var dbContext = new HMSEntities())
            {
                var labs = new List<PatientLab>();
                if (string.IsNullOrEmpty(searchModel.SearchString))
                {
                    var tests = dbContext.PatientLabs.ToList();
                    labs = tests;
                }
                else
                {
                    var tests = dbContext.PatientLabs
                      .Where(lab => lab.Name.ToLower().Contains(searchModel.SearchString.ToLower()) ||
                      lab.Id.ToString().Contains(searchModel.SearchString.ToLower()) ||
                      lab.GuardianName.ToLower().Contains(searchModel.SearchString.ToLower()) ||
                      lab.Phone.ToLower().Contains(searchModel.SearchString.ToLower()) ||
                      lab.RegisteredBy.ToLower().Contains(searchModel.SearchString.ToLower()) ||
                      lab.PerformedBy.ToLower().Contains(searchModel.SearchString.ToLower()) ||
                      lab.PatientNo.ToLower().Contains(searchModel.SearchString.ToLower())).ToList();
                    labs = tests;
                }

                if (!labs.Any())
                {
                    response.Pagging.Current++;
                    return response;
                }

                var itemsToSkip = response.Pagging.Current * response.Pagging.ItemPerPage;
                var actualForms = labs.Skip(itemsToSkip).Take(response.Pagging.ItemPerPage).ToList();
                response.PatientWithLabs = actualForms.Select(lap => lap.MapPatientLab()).ToList();
                response.Pagging.TotalItems = labs.Count();
                response.Pagging.Current++;
                return response;
            }
        }


        public static AddLabToPatientResponseModel GetPatientWithLabDetail(string patientLabId)
        {
            using (var dbContext = new HMSEntities())
            {
                var patient = dbContext.PatientLabs.FirstOrDefault(l => l.Id.ToString() == patientLabId);
                var allTestDbs = dbContext.PatientLabs_Labs.Where(o => o.PatientLabId.ToString() == patientLabId).ToList();
                var actualTests = allTestDbs.Select(p => p.MapperHm()).ToList();
                return new AddLabToPatientResponseModel
                {
                    PatientInfo = patient.MapPatientLab(),
                    PatientLabs = actualTests
                };
            }
        }

      

        public static App_PatientLabs_Labs AddPatientLabs(List<AppLab_Parm_ForPatient> labs)
        {
            var lab = new PatientLabs_Labs
            {
                PatientLabId = labs.First().PatientLabId,
                TestId = labs.First().TestId,
            };
            using (var dbContext = new HMSEntities())
            {
                try
                {
                    
                    dbContext.PatientLabs_Labs.Add(lab);
                    dbContext.SaveChanges();
                    var patientLabId =long.MaxValue;
                    foreach (var lo in labs)
                    {
                        var parm = new PatientLabs_Labs_Parms
                        {
                            Id = 0,
                            TestId = lo.TestId,
                            ParmId = lo.ParmId,
                            ParmValue = lo.ActualVal ?? "",
                            PatientLabId = lo.PatientLabId
                        };
                        patientLabId = lo.PatientLabId;
                        dbContext.PatientLabs_Labs_Parms.Add(parm);
                    }

                    var dbtest = dbContext.PatientLabs.FirstOrDefault(t => t.Id == patientLabId);
                    if (dbtest != null)
                    {
                        if (labs.Any(l => !string.IsNullOrEmpty(l.ActualVal)))
                        {
                            dbtest.Reported = DateTime.Now;
                        }
                    }

                   dbContext.SaveChanges();
                }
                catch (Exception dd)
                {

                }

                var allTestDbs = dbContext.PatientLabs_Labs.Include("Lab_Tests").ToList();

                var totalFeeTillNow = allTestDbs.Where(o => o.PatientLabId == lab.PatientLabId).ToList().Sum(o=> o.Lab_Tests.Fee);

                var thistest = allTestDbs.Where(o => o.PatientLabId == lab.PatientLabId && o.TestId == labs.First().TestId);
                var final= thistest.FirstOrDefault().MapperHm();

                final.TotalFee = totalFeeTillNow;  // to show on the screen

                var rec= dbContext.PatientLabs.FirstOrDefault(p => p.Id == lab.PatientLabId);
                rec.Amount = totalFeeTillNow;
                dbContext.SaveChanges();


                return final;
            }
        }

        public static App_PatientLabs_Labs UpdatePatientLabs(List<AppLab_Parm_ForPatient> labs)
        {
            using (var dbContext = new HMSEntities())
            {
                var patientLabId = long.MaxValue;
                foreach (var lo in labs)
                {
                    var p = dbContext.PatientLabs_Labs_Parms.FirstOrDefault(o => o.Id == lo.Id);
                    if (p!=null)
                    {
                        p.ParmValue = lo.ActualVal ?? ""; 
                    }
                    patientLabId = lo.PatientLabId;
                }

                var dbtest = dbContext.PatientLabs.FirstOrDefault(t => t.Id == patientLabId);
                if (dbtest != null)
                {
                    if (labs.Any(l => !string.IsNullOrEmpty(l.ActualVal)))
                    {
                        dbtest.Reported = DateTime.Now;
                    }
                }
                dbContext.SaveChanges();
                var allTestDbs = dbContext.PatientLabs_Labs.Include("Lab_Tests").ToList();
                var data = allTestDbs.Where(o => o.PatientLabId == labs.First().PatientLabId && o.TestId == labs.First().TestId);
                return data.FirstOrDefault().MapperHm();
            }

        }

        public static long AddPatientToLab(App_PatientLab PatientInfo)
        {
            using (var dbContext = new HMSEntities())
            {
                var pat = PatientInfo;
                var patient = new PatientLab
                {
                    PatientNo = pat.PatientNo,
                    Name = pat.Name,
                    Age = pat.Age,
                    Gender = pat.Gender,
                    RegisteredBy = pat.RegisteredBy,
                    PerformedBy = pat.PerformedBy,
                    Requested = DateTime.Now,
                    Reported = null,
                    BloodGroup = pat.BloodGroup,
                    GuardianName = pat.GuardianName,
                    Phone = pat.Phone,
                    Address = pat.Address,
                    MaritalStatus = pat.MaritalStatus,
            };
                dbContext.PatientLabs.Add(patient);
                dbContext.SaveChanges();
                return patient.Id;
            }
        }

        public static bool UpdatePatientToLab(App_PatientLab source)
        {

            using (var dbContext = new HMSEntities())
            {
                var dbtest = dbContext.PatientLabs.FirstOrDefault(t => t.Id == source.Id);

                if (source.Discount != null && source.Discount > 0)
                {                   
                    if (dbtest == null || dbtest.Amount==null || source.Discount > dbtest.Amount)
                    {
                        return false;
                    }
                }

             
                if (dbtest != null)
                {
                    dbtest.Address = source.Address;
                    dbtest.Age = source.Age;
                    dbtest.BloodGroup = source.BloodGroup;
                    dbtest.Gender = source.Gender;
                    dbtest.GuardianName = source.GuardianName;
                    dbtest.Name = source.Name;
                    dbtest.PerformedBy = source.PerformedBy;
                    dbtest.Phone = source.Phone;
                    dbtest.RegisteredBy = source.RegisteredBy;
                    dbtest.MaritalStatus = source.MaritalStatus;
                    dbtest.Gender = source.Gender;
                    dbtest.Discount = source.Discount;
                    dbtest.DiscountBy = source.DiscountBy;
                }
                dbContext.SaveChanges();
                return true;
            }
        }

        public static App_PatientLab GetPatientByMrNo(string mrNo)
        {
            using (var dbContext = new HMSEntities())
            {
                var pdata = dbContext.OPDs.Where(o => o.PatientNo == mrNo);
                if (pdata.Any())
                {
                    var source = pdata.First();
                    var obj = new App_PatientLab
                    {
                        Id = 0,
                        Name = source.Name,
                        PatientNo = source.PatientNo,
                        RequestedOn = DateTime.Now.ToString(),
                        Address = source.Address,
                        Phone = source.Phone,
                        Age = source.Age,
                        Gender = source.Gender,
                        GuardianName = source.GuardianName,
                        MaritalStatus = source.MartialStatus
                    };
                    return obj;
                }
            }
            return new App_PatientLab();
        }


        public static AppLab_Test AddMixReport(CreateMixReportModel source)
        {
            var test = new Lab_Tests
            {
                Name = source.Name,
                Fee = source.Fee,
                Interval = 30,
                Status = true,               
            };
            using (var dbContext = new HMSEntities())
            {
                test.CreationDate = DateTime.Now;
                dbContext.Lab_Tests.Add(test);
                dbContext.SaveChanges();


                foreach (var obj in source.ParmIds)
                {
                    var testMapping = new Lab_Mapping
                    {
                        TestId = test.Id,
                        ParmId = obj,
                        CreationDate = DateTime.Now
                    };
                    dbContext.Lab_Mapping.Add(testMapping);
                }
                dbContext.SaveChanges();

                return test.Mapper();
            }           
        }


        public static int RemovePatientLab(App_PatientLabs_Labs model)
        {
            using(var dbcontext = new HMSEntities())
            {
                var parms = dbcontext.PatientLabs_Labs_Parms.Where(l => l.PatientLabId == model.PatientLabId && l.TestId == model.TestId).ToList();
                var labs = dbcontext.PatientLabs_Labs.Where(l => l.PatientLabId == model.PatientLabId && l.TestId == model.TestId).ToList();
                dbcontext.PatientLabs_Labs_Parms.RemoveRange(parms);
                dbcontext.SaveChanges();
                dbcontext.PatientLabs_Labs.RemoveRange(labs);
                dbcontext.SaveChanges();


                var totalFeeTillNow = dbcontext.PatientLabs_Labs.Where(o => o.PatientLabId == model.PatientLabId).ToList().Sum(o => o.Lab_Tests.Fee);

                var rec = dbcontext.PatientLabs.FirstOrDefault(p => p.Id == model.PatientLabId);
                rec.Amount = totalFeeTillNow;
                if(rec.Amount< rec.Discount)
                {
                    rec.Discount = 0;
                    rec.DiscountBy = "";
                }

                dbcontext.SaveChanges();

                return totalFeeTillNow;
            }
        }

    }
}
