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

        public static AppLab_Test AddNewTest(AppLab_Test source)
        {
            var test = new Lab_Tests
            {
                Name = source.Name,
                Fee = source.Fee,
                Interval = source.Interval,
                CreationDate = DateTime.Now,
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
                    dbContext.Lab_Tests.Add(test);
                }
                dbContext.SaveChanges();
            }
            return test.Mapper();
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

        public static LabMappingResponseModel GetLabMapping(long labid)
        {
            var responseList = new LabMappingResponseModel();
            using (var dbContext = new HMSEntities())
            {
                var mappings = dbContext.Lab_Mapping.Where(map => map.TestId == labid).ToList();
                if (mappings.Any())
                {
                    responseList.LabMapping = mappings.Select(ma => ma.Mapper()).ToList();
                    responseList.TestName = responseList.LabMapping.First().TestName;
                }
            }
            return responseList;
        }

        public static void AddNewMapping(AppLab_mapping source)
        {
            var test = new Lab_Mapping
            {
                TestId                = source.TestId,
                ParmId = source.ParmId,
                CreationDate = DateTime.Now
            };
            using (var dbContext = new HMSEntities())
            {
               dbContext.Lab_Mapping.Add(test);                
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
    }
}
