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
namespace HMS.Controllers
{
    public class PrintController : Controller
    {
        // GET: Print
        public ActionResult PrintOpdSlip(string opdId)
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
                    RecievedBy = "Faisal Khan",
                    TokenNo = reportData.DailyNo.ToString(),
                    Age = reportData.Age,
                    PatientId = reportData.PatientNo,
                    VisitNo=3
                }
            });
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            var stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", reportData.Name + "_" + reportData.PatientNo + ".pdf");

        }


    }
}