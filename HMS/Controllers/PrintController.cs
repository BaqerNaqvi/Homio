using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using HmsServices.OpdForms;
using HMS.Models.ReportModels;
//https://www.youtube.com/watch?v=odwV0jFA7Wo
// version details https://www.tektutorialshub.com/download-crystal-reports-for-visual-studio/
namespace HMS.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult PrintOpdSlip(string opdId)
        {
            try
            {
                var reportData = OpdService.GetOpdById(opdId);
                var rd = new ReportDocument();
                rd.Load(Path.Combine((Server.MapPath("~/Reports/opdReceipt.rpt"))));
                rd.SetDataSource(new List<OpdSlipReportModel>
            {
                new OpdSlipReportModel
                {
                    Name = reportData.Name,
                    DateTime = reportData.DateTime.ToString(),
                    Amount = reportData.DocFee,
                    DoctorName = reportData.DocName,
                    RecievedBy = Session["userFirstName"].ToString(),
                    TokenNo = reportData.DailyNo.ToString(),
                    Age = reportData.Age,
                    PatientId = reportData.PatientNo,
                    VisitNo=reportData.VisitNo ?? 0
                }
            });
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                var stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", reportData.Name + "_" + reportData.PatientNo + ".pdf");
            }
            catch (Exception excep)
            {

            }
            return null;
        }

        public ActionResult PrintLabSlip(string patientId)
        {
            try
            {
                    var reportData = HmsServices.Labs.LabService.GetPatientWithLabDetail(patientId);
                    var rd = new ReportDocument();
                    var path = (Server.MapPath("~/Reports/labReceipt.rpt"));
                    rd.Load(Path.Combine(path));

                var source = new List<LabSlipReportModel>
                {
                    new LabSlipReportModel
                    {
                        FullName = reportData.PatientInfo.Name,
                        DateTime = reportData.PatientInfo.RequestedOn,
                        Amount = 500,//(int)reportData.PatientInfo.Amount,
                        RecievedBy = Session["userFirstName"]?.ToString(),
                        Id = reportData.PatientInfo.Id.ToString()
                    }
                };
               
                rd.SetDataSource(source);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    var stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", reportData.PatientInfo.Name + "_Labs_" + reportData.PatientInfo.Id.ToString() + ".pdf");
            }
            catch (Exception excep)
            {

            }
            return null;
        }
    }
}