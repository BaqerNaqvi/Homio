using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;
using HmsServices.Models;

namespace HmsServices.OpdForms
{
    public static class OpdService
    {
        public static OpdFormMetaReaponseModel GetMetaData(OpdFormMetaRequestModel requestModel)
        {
            using (var dbcontext = new HMSEntities())
            {
                var allForms = dbcontext.OPDs.ToList();
                var ruleDate = DateTime.Now.Date;
                var patientNo = requestModel.PatientNo?.Trim();
                var visitNo = 0;
                var dailyNo = 0;
                long serialNo = 0;

                AppOpd existedform = new AppOpd();
                if (allForms.Any())
                {
                   
                        dailyNo =
                                       dbcontext.OPDs.Count(
                                            form =>
                                                EntityFunctions.TruncateTime(form.DateTime) == ruleDate &&
                                                form.DoctorId == requestModel.DoctorId);
                  
                    serialNo = allForms.Count;
                   
                    if (string.IsNullOrEmpty(patientNo))
                    {
                        patientNo = DateTime.Now.Year + "" + DateTime.Now.Month + "-" + DateTime.Now.Day + 
                            (dbcontext.OPDs.Count(form =>  EntityFunctions.TruncateTime(form.DateTime) == ruleDate) + 1) + "";
                    }
                    else
                    {
                        visitNo = allForms.Count(form => form.PatientNo == patientNo);
                        var tempoForm = allForms.FirstOrDefault(form => form.PatientNo == patientNo);
                        if (tempoForm != null)
                        {
                            existedform = tempoForm.MaptoOpd();
                        }
                    }
                    return new OpdFormMetaReaponseModel
                    {
                        DateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                        DailyNo = ++dailyNo,
                        PatientNo = patientNo,
                        VisitNo = ++visitNo,
                        SerialNo = ++serialNo,
                        OpdForm = existedform
                    };
                }
            }
            return new OpdFormMetaReaponseModel
            {
                DateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                DailyNo =1,
                PatientNo = 1+"",
                VisitNo = 1,
                SerialNo =1,
                OpdForm = null
            };
        }

        public static AppOpd CreateUPdateNewForm(AppOpd source)
        {
            using (var dbcontext = new HMSEntities())
            {
                var dbObj = dbcontext.OPDs.FirstOrDefault(form => form.Id == source.Id);
                if (dbObj == null)
                {
                    dbObj = new OPD
                    {
                        DateTime = DateTime.Now,
                        Name = source.Name,
                        DailyNo = source.DailyNo,
                        PatientNo = source.PatientNo,
                        VisitNo = source.VisitNo,
                        MartialStatus = source.MartialStatus,
                        Address = source.Address,
                        Age = source.Age,
                        CNIC = source.CNIC,
                        DoctorId = source.DoctorId,
                        Gender = source.Gender,
                        GuardianName = source.GuardianName,
                        Phone = source.Phone,
                    };
                    dbcontext.OPDs.Add(dbObj);
                    dbcontext.SaveChanges();
                    return GetOpdById(dbObj.Id.ToString());
                }
            }
            return null;
        }

        public static OpdResponseModel GetAllFAppOpds(SearchModel searchModel)
        {
            var response = new OpdResponseModel
            {
                Pagging = searchModel.Pagging,
                AppOpds = new List<AppOpd>()
            };
            using (var dbContext = new HMSEntities())
            {
                List<OPD> forms;               
                var str = searchModel.SearchString;
                if (string.IsNullOrEmpty(str))
                {
                    forms = dbContext.OPDs.ToList();
                }
                else
                {
                    forms = dbContext.OPDs.Where( op => op.DailyNo.ToString().Contains(str) || 
                    op.Id.ToString().Contains(str) || op.PatientNo.Contains(str) || op.Doctor.FirstName.Contains(str) ||
                    op.Doctor.LastName.Contains(str) || op.Name.Contains(str) || op.GuardianName.Contains(str) ||
                      (!string.IsNullOrEmpty(op.CNIC) &&  op.CNIC.Contains(str))  ||
                       (!string.IsNullOrEmpty(op.Phone) && op.Phone.Contains(str)))
                    .ToList();
                }
                if (!forms.Any())
                {
                    response.Pagging.Current++;
                    return response;
                }

                var itemsToSkip = response.Pagging.Current*response.Pagging.ItemPerPage;
                var totalForms = forms.Select(fobj => fobj.MaptoOpd());
                var actualForms= totalForms.Skip(itemsToSkip).Take(response.Pagging.ItemPerPage).ToList();
                response.AppOpds = actualForms;
                response.Pagging.TotalItems = totalForms.Count();
                response.Pagging.Current++;
                return response;
            }
        }


        public static AppOpd GetOpdById(string opdId)
        {            
            using (var dbContext = new HMSEntities())
            {
                return dbContext.OPDs.FirstOrDefault(opd => opd.Id.ToString() == opdId).MaptoOpd();
            }
        }

        public static AppIp ConvertToIp(string opdId)
        {
            using (var dbContext = new HMSEntities())
            {
                var totalserial = dbContext.IpForms.Count();
                var thisMonthSerial = dbContext.IpForms.Count(ip => ip.DateTime.Month == DateTime.Now.Month);
                var obj = dbContext.OPDs.FirstOrDefault(opd => opd.Id.ToString() == opdId).ConvertToIpForm();
                obj.SerialNo = ++totalserial;
                obj.MonthlyNo = ++thisMonthSerial;
                return obj;
            }
        }
    }
}
