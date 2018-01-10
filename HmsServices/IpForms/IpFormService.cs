using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;
using HmsServices.Models;

namespace HmsServices.IpForms
{
    public static class IpFormService
    {
        public static List<AppWardsRoom> GetWards()
        {
            using (var dbcontext = new HMSEntities())
            {
                var items = dbcontext.WardsRooms.Where(ward => !ward.Booked).ToList();
                if (items != null && items.Any())
                {
                    return items.Select(obj => obj.MappAppWardsRoom()).ToList();
                }
            }
            return null;
        }

        public static void AddNewIp(AppIp source)
        {
            var ruleDate = DateTime.Now.Date;
            var obj = new IpForm
            {
                MartialStatus = source.MartialStatus,
                DateTime = DateTime.Now,
                Name = source.Name,
                OpdId = source.OpdId,
                PatientNo = source.PatientNo,
                Phone = source.Phone,
                GuardianName = source.GuardianName,
                Gender = source.Gender,
                DoctorId = source.DoctorId,
                Caste = source.Caste,
                CNIC = source.CNIC,
                Age = source.Age,
                BloodGroup = source.BloodGroup,
                Address = source.Address,
                AdvanceAmount = source.AdvanceAmount,
            };
            using (var dbcontext = new HMSEntities())
            {
                try
                {
                    if (string.IsNullOrEmpty(obj.PatientNo))
                    {
                        obj.PatientNo = DateTime.Now.Year + "" + DateTime.Now.Month + "-" + DateTime.Now.Day +
                            (dbcontext.OPDs.Count(form => EntityFunctions.TruncateTime(form.DateTime) == ruleDate) + 1) + "";
                    }


                    dbcontext.IpForms.Add(obj);
                    var room = dbcontext.WardsRooms.FirstOrDefault(war => war.Id == source.RoomId);
                    room.Booked = true;

                    dbcontext.SaveChanges();
                }
                catch (Exception dcdf)
                {

                }
            }
        }

        public static IpResponseModel GetAllIpForms(SearchModel searchModel)
        {
            var response = new IpResponseModel
            {
                Pagging = searchModel.Pagging,
                AppIps = new List<AppIp>()
            };
            using (var dbContext = new HMSEntities())
            {
                List<IpForm> forms;
                var str = searchModel.SearchString;
                if (string.IsNullOrEmpty(str))
                {
                    forms = dbContext.IpForms.ToList();
                }
                else
                {
                    forms = dbContext.IpForms.Where(op => op.Caste.ToString().Contains(str) ||
                   op.Id.ToString().Contains(str) || op.PatientNo.Contains(str) || op.Doctor.FirstName.Contains(str) ||
                   op.Doctor.LastName.Contains(str) || op.Name.Contains(str) || op.GuardianName.Contains(str) ||
                     (!string.IsNullOrEmpty(op.CNIC) && op.CNIC.Contains(str)) ||
                      (!string.IsNullOrEmpty(op.Phone) && op.Phone.Contains(str)))
                    .ToList();
                }
                if (!forms.Any())
                {
                    response.Pagging.Current++;
                    return response;
                }

                var itemsToSkip = response.Pagging.Current * response.Pagging.ItemPerPage;
                var totalForms = forms.Select(fobj => fobj.MaptoIp());
                var actualForms = totalForms.Skip(itemsToSkip).Take(response.Pagging.ItemPerPage).ToList();
                response.AppIps = actualForms;
                response.Pagging.TotalItems = totalForms.Count();
                response.Pagging.Current++;
                return response;
            }
        }
    }
}
