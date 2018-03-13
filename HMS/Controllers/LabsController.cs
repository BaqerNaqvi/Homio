using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmsServices.Docs;
using HmsServices.Labs;
using HmsServices.Models;
using HmsServices.OpdForms;
using HMS.DataSets;

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

        public JsonResult SearchLabsForMapping(string term)
        {
            var tests = LabService.GetLabTestsForMapping(term);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatus(AppLab_Test source)
        {
            var test = LabService.SetLabStatus(source);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddUpdateTest(AppLab_Test source)
        {
            var newTest = LabService.AddUpdateTest(source);
            return Json(newTest, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTest(AppLab_Test source)
        {
           LabService.DeleteTest(source);
            return Json(true, JsonRequestBehavior.AllowGet);
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


        public ActionResult SearchLabTest(string searchTerm)
        {
            var tests = LabService.GetLabTests(new SearchModel
            {
                SearchString = searchTerm,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 15
                }
            });
            return Json(tests, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchParms(SearchModel model)
        {
            var tests = LabService.GetLabParms(model);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchParmsForMapping(string term)
        {
            var response = new LabParmsResponseModelDd
            {
                LabParms = new List<AppLab_ParmDd>()
            };
            if (string.IsNullOrEmpty(term))
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            var tests = LabService.GetLabParmsForMapping(term);
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

        public JsonResult GetLabParmsByLabId(string labId)
        {
            var tests = LabService.GetLabParmsByLabId(labId);
            return Json(tests, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Mapping
        public ActionResult LabDetails(long labId)
        {
            var mappings = LabService.GetLabMapping(labId);
            return View(mappings);
        }

        public JsonResult AddNewMapping(List<AppLab_mapping> source)
        {
            LabService.AddNewMapping(source);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMapping(AppLab_mapping source)
        {
            LabService.DeleteMapping(source);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult PatientLabs()
        {
            var patientsWithLabs = LabService.GetPatientWithLabs(new SearchModel
            {
                SearchString = string.Empty,
                Pagging = new PaggingModel
                {
                    Current = 0,
                    ItemPerPage = 10
                }
            });
            return View(patientsWithLabs);
        }

        public ActionResult SearchPatientLabs(SearchModel model)
        {
            var patientsWithLabs = LabService.GetPatientWithLabs(model);
            return Json(patientsWithLabs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PatientLabDetails(string patientLabId)
        {
            var labs = LabService.GetLabTestsForMapping(null);
            if (!string.IsNullOrEmpty(patientLabId))
            {
                var modelObj = LabService.GetPatientWithLabDetail(patientLabId);
                modelObj.LabTestsDd = labs.LabTests;
                return View(modelObj);
            }

            var model = new AddLabToPatientResponseModel
            {
                LabTestsDd = labs.LabTests,
                PatientInfo = new App_PatientLab
                {
                   // ReportedOn = DateTime.Now.ToString(),
                    RequestedOn = DateTime.Now.ToString(),
                    Name = " ",
                    GuardianName = " ",
                    Phone = " ",
                    Address = " "
                }
            };
            return View(model);
        }


        public JsonResult AddPatientLabs(List<AppLab_Parm_ForPatient> labs)
        {
          var lab = LabService.AddPatientLabs(labs);
          return Json(lab, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePatientLabs(List<AppLab_Parm_ForPatient> labs)
        {
            var lab = LabService.UpdatePatientLabs(labs);
            return Json(lab, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNewPatientToLab(App_PatientLab model)
        {
            var patientlabId = LabService.AddPatientToLab(model);
            return Json(patientlabId, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePatientToLab(App_PatientLab model)
        {
            var response= LabService.UpdatePatientToLab(model);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientByMrNo(string mrNo)
        {
            var data = LabService.GetPatientByMrNo(mrNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CreateMixReport(CreateMixReportModel request)
        {
            var lab = LabService.AddMixReport(request);
            return Json(lab, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateLabFromOpd(string opdId)
        {
            var patient = OpdService.GetOpdById(opdId);
            var model = new AddLabToPatientResponseModel
            {
                PatientInfo = new App_PatientLab
                {
                    // ReportedOn = DateTime.Now.ToString(),
                    RequestedOn = DateTime.Now.ToString(),
                    Name = patient.Name,
                    GuardianName = patient.GuardianName,
                    Phone = patient.Phone,
                    Address = patient.Address,
                    PatientNo = opdId,
                    Gender = patient.Gender,
                    Age = patient.Age,
                    MaritalStatus = patient.MartialStatus
                }
            };
            return View("PatientLabDetails", model);
        }


        public JsonResult RemovePatientLab(App_PatientLabs_Labs model)
        {
            var fee = LabService.RemovePatientLab(model);
            return Json(fee, JsonRequestBehavior.AllowGet);
        }

    }
}