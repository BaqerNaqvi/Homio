using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HmsServices.Docs;
using HmsServices.Models;
using HmsServices.OpdForms;
using HMS.Models;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
       [Authorize]
        public ActionResult Index()
       {
         //  readHtmlPage("http://www.sms4connect.com/api/sendsms.php/sendsms/url");

            var formResponse = OpdService.GetAllFAppOpds(new SearchModel
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

        [Authorize]
        public ActionResult OpdpForms()
        {
            var dataModel = new OpdPageModel
            {
                DoctorList = DocService.GetAllDocsForDd(),
                CreatedOpdForm = new AppOpd
                {
                    DateTime = " ",
                    VisitNo = 1,
                    PatientNo = " ",
                    Name=" ",
                    GuardianName=" ",
                    CNIC=" ",
                    Address=" ",
                    Phone=" ",
                    InsuranceNo=" "
                },
                Mode = "new"
            };
            return View(dataModel);
        }

        public JsonResult GetFormMetadata(OpdFormMetaRequestModel model)
        {
            var response = OpdService.GetMetaData(model);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateForm(AppOpd request)
        {
            var slip = OpdService.CreateUPdateNewForm(request);
            return Json(slip, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchOpdForms(SearchModel model)
        {
            var response =  OpdService.GetAllFAppOpds(model);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOpdFormById(string opdId, string mode )
        {
            var allRecWithOpd=OpdService.GetOpdByIdWithRows(opdId);
            if(mode== "recreate")
            {
                allRecWithOpd.Opd.VisitNo = allRecWithOpd.Records.Count + 1;
                allRecWithOpd.Opd.DoctorId = "0";
                allRecWithOpd.Opd.Discount = 0;
                allRecWithOpd.Opd.DiscountBy = " ";
                allRecWithOpd.Opd.DocFee = 0;
                allRecWithOpd.Opd.DateTime = DateTime.Now.ToLongTimeString();
                allRecWithOpd.Opd.Id = 0;
            }
            var dataModel = new OpdPageModel
            {
               CreatedOpdForm = allRecWithOpd?.Opd,
               Mode = mode,
               DoctorList = DocService.GetAllDocsForDd(),
               Records= allRecWithOpd?.Records
            };
            //recreate
            return View("OpdpForms", dataModel);
        }

    }
}