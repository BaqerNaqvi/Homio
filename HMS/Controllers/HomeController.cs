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
                DoctorList = DocService.GetAllDocsForDd()
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


        private String readHtmlPage(string url)
        {
            String result = "";
            String message = HttpUtility.UrlEncode("Hey Irfan\nThis is alert message from kamsham.pk");
            String strPost = "id=test11&pass=pakistan89&msg=" + message +
                             "&to=923084449991" + "&mask=SMS4CONNECT&type=xml&lang=English";
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest) WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse) objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }
    }
}