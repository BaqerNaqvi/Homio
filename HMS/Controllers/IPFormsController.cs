using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmsServices.Docs;
using HmsServices.IpForms;
using HmsServices.Models;
using HmsServices.OpdForms;
using HMS.Models;

namespace HMS.Controllers
{
    public class IpFormsController : Controller
    {
        // GET: IPForms
        [Authorize]
        public ActionResult CreateIpForm(string opdId)
        {
            var opd = new AppIp {BloodGroup = "B+"};
            if (!string.IsNullOrEmpty(opdId))
            {
                opd = OpdService.ConvertToIp(opdId);
                opd.DateTime = DateTime.Now.ToLongTimeString();
            }

            var dataModel = new IpPageModel
            {
                DoctorList = DocService.GetAllDocsForDd(),
                WardsList= IpFormService.GetWards(),
                GeneralData = opd
            };
            return View(dataModel);
        }


        public JsonResult CreateFormAjax(AppIp data)
        {
            IpFormService.AddNewIp(data);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IpFormsList()
        {
            var formResponse = IpFormService.GetAllIpForms(new SearchModel
            {
                SearchString = string.Empty,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 10
                }
            });
            return View(formResponse);
        }

        public JsonResult SearchIpForms(SearchModel model)
        {
            var response = IpFormService.GetAllIpForms(model);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}