using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;
using HmsServices.Models;

namespace HmsServices.Docs
{
    public static class DocService
    {
        public static List<AppUserDoc> GetAllDocs(bool onlyActive=false)
        {
            using (var dbContext = new HMSEntities())
            {
                var docs = dbContext.AspNetUsers.Where(doc => doc.Fee > 0 && (doc.Status || doc.Status==onlyActive)).ToList();
                if (docs.Any())
                {
                   return docs.Select(doco => doco.MapToDoc()).ToList();
                }
            }
            return null;
        }

        public static List<AppUserDoc_Dd> GetAllDocsForDd()
        {
            using (var dbContext = new HMSEntities())
            {
                var docs = dbContext.AspNetUsers.Where(doc => doc.Fee > 0 && (doc.Status)).ToList();
                if (docs.Any())
                {
                    return docs.Select(doco => doco.MapToDocDd()).ToList();
                }
            }
            return null;
        }

        public static AppUserDoc SetDocStatus(AppUserDoc source)
        {
            using (var dbContext = new HMSEntities())
            {
                var data =dbContext.AspNetUsers.FirstOrDefault(doc => doc.Id == source.Id);
                if (data != null)
                {
                    data.Status = source.Status;
                    dbContext.SaveChanges();
                    return data.MapToDoc();
                }
            }
            return source;
        }

        public static AppUserDoc GetDoc(string id)
        {
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.AspNetUsers.FirstOrDefault(doc => doc.Id == id);
                if (data != null)
                {
                    return data.MapToDoc();
                }
            }
            return null;
        }

        public static AppUserDoc UpdateDoc(AppUserDoc source)
        {
            using (var dbContext = new HMSEntities())
            {
                var data = dbContext.AspNetUsers.FirstOrDefault(doc => doc.Id == source.Id);
                if (data != null)
                {

                    data.FirstName = source.FirstName;
                    data.LastName = source.LastName;
                    data.Fee = source.Fee;
                    data.Degree = source.Degree;
                    data.Title = source.Title;
                    data.ShiftFrom = source.ShiftFrom;
                    data.ShiftToo = source.ShifTo;
                    data.Email = source.Email;
                    data.PMDCNo = source.PMDCNo;


                    data.ShiftDays = source.ShiftDays;
                    dbContext.SaveChanges();
                    return data.MapToDoc();
                }
            }
            return source;
        }

    
    }
}
