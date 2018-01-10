using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmsServices.Docs;
using HmsServices.Models;
using HMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {

        [Authorize]
        public ActionResult ListOfDocs()
        {
            var docs = DocService.GetAllDocs();
            return View(docs);
        }

        public JsonResult ChangeStatus(AppUserDoc source)
        {
            var updatedDoc = DocService.SetDocStatus(source);
            return Json(updatedDoc, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult AddNewDoc(string docId)
        {
            var doc = new AppUserDoc
            {
                ShiftDays = "Friday,Monday"
            };
            if (!string.IsNullOrEmpty(docId))
            {
                doc = DocService.GetDoc(docId);
            }
            return View(doc);
        }
       


        public JsonResult AddNewDocAjax(AppUserDoc model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                #region New
                #region Manager
                var context = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                #endregion
                #region Creation
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    Fee = model.Fee,
                    CreationTime = DateTime.Now,
                    LastEditDateTime = DateTime.Now,
                    Status = true,
                    Degree = model.Degree,
                    Title = model.Title,
                    Type = "Doctor",
                    ShiftFrom = model.ShiftFrom,
                    ShiftToo = model.ShifTo,
                    ShiftDays = model.ShiftDays,
                    PMDCNo= model.PMDCNo

                };
                var result = userManager.Create(user, "123456");
                if (result.Succeeded)
                {
                    return Json(new
                    {
                        isSuccess = true,
                        doc = user,
                        data = new List<string> { "Doctor added" }
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    isSuccess = false,
                    data = AddErrors(result)
                }, JsonRequestBehavior.AllowGet);
                #endregion 
                #endregion
            }
            #region Update
            var doc = DocService.UpdateDoc(model);
            return Json(new
            {
                isSuccess = true,
                doc = doc,
                data = new List<string> { "Doctor updated" }
            }, JsonRequestBehavior.AllowGet); 
            #endregion
        }

        private List<string> AddErrors(IdentityResult result)
        {
            return result.Errors.ToList();
        }
    }
}