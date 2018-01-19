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
        #region Labs
        public ActionResult LabManagement()
        {
            var tests = LabService.GetLabTests(new SearchModel
            {
                SearchString = string.Empty,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 15
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
            var test = LabService.SetLabStatus(source);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNew(AppLab_Test source)
        {
            var newTest = LabService.AddNewTest(source);
            return Json(newTest, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Parms

        public ActionResult LabParms()
        {
            var tests = LabService.GetLabParms(new SearchModel
            {
                SearchString = string.Empty,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 15
                }
            });
            return View(tests);
        }


        public JsonResult SearchParms(SearchModel model)
        {
            var tests = LabService.GetLabParms(model);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchParmsForMapping(string searchTerm)
        {
            var tests = LabService.GetLabParmsForMapping(searchTerm);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ChangeParmStatus(AppLab_Parm source)
        {
            var test = LabService.SetLabParmStatus(source);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNewParm(AppLab_Parm source)
        {
            var newTest = LabService.AddNewParm(source);
            return Json(newTest, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult LabDetails(long labId)
        {
            var mappings = LabService.GetLabMapping(labId);
            return View(mappings);
        }

        public JsonResult AddNewMapping(AppLab_mapping source)
        {
           LabService.AddNewMapping(source);
           return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMapping(AppLab_mapping source)
        {
            LabService.DeleteMapping(source);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}