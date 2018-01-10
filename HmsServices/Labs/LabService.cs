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
        public static LabTestMgmtResponseModel GetLabTests(SearchModel searchModel)
        {
            var response = new LabTestMgmtResponseModel
            {
                Pagging = searchModel.Pagging,
                LabTest =new List<AppLab_Test>()
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
                Status = true
            };
            using (var dbContext = new HMSEntities())            {
               
                dbContext.Lab_Tests.Add(test);
                dbContext.SaveChanges();
            }
            return test.Mapper();
        }
    }
}
