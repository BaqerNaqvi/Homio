using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmsServices.Docs;
using HmsServices.Labs;
using HmsServices.Models;
using HmsServices.OpdForms;

namespace HMS.Controllers
{
    public class LabsController : Controller
    {
        // GET: LabsMgmt
        public ActionResult LabManagement()
        {
            var tests = LabService.GetLabTests(new SearchModel
            {
                SearchString = string.Empty,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 10
                }
            });
            return View(tests);
        }

        public JsonResult SearchLabs(SearchModel model)
        {
            var tests = LabService.GetLabTests(model);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(AppLab_Test source)
        {
            var test = LabService.SetLabStatus( source);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNew(AppLab_Test source)
        {
            var newTest = LabService.AddNewTest(source);
            return Json(newTest, JsonRequestBehavior.AllowGet);
        }
    }
}