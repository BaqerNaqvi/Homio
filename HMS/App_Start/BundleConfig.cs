using System.Web;
using System.Web.Optimization;

namespace HMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {           

            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/loginScript").Include(
                      "~/Scripts/jquery-3.2.1.min.js",
                      "~/Scripts/Accounts/index-login.js",
                       "~/Scripts/Utils/util.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/layoutScript").Include(
                       "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootstrap-multiselect.js",
                        "~/Scripts/material.min.js",
                        "~/Scripts/chartist.min.js",
                        "~/Scripts/arrive.min.js",
                        "~/Scripts/perfect-scrollbar.jquery.min.js",
                        "~/Scripts/bootstrap-notify.js",
                         "~/Scripts/Utils/util.js",
                        "~/Scripts/material-dashboard.js",
                         "~/Scripts/toastr.js",
                        "~/Scripts/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/OPDListViewJs").Include(
                   "~/Scripts/OPD/listView.js",
                   "~/Scripts/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/OPDAddViewJs").Include(
                 "~/Scripts/OPD/addNew.js",
                  "~/Scripts/jquery-mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ipFormsAddViewJs").Include(
             "~/Scripts/IP/ipFormaddNew.js",
             "~/Scripts/jquery-mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/IpListViewJs").Include(
                 "~/Scripts/IP/ipFormList.js",
                 "~/Scripts/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/hmsDocsJs").Include(
              "~/Scripts/Docs/HmsDocs.js"));


            bundles.Add(new ScriptBundle("~/bundles/LabMgmtJs").Include(
                  "~/Scripts/Labs/Management/lab_mgmt.js",
                  "~/Scripts/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/LabParms").Include(
                "~/Scripts/Labs/Parms/labsParms.js",
                "~/Scripts/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/LabMapping").Include(
             "~/Scripts/Labs/Mapping/labMapping.js",
             "~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/PatientLabs").Include(
             "~/Scripts/Labs/Patient/PatientLabs.js",
             "~/Scripts/jquery.simplePagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/PatientLabs_AddEdit").Include(
            "~/Scripts/Labs/Patient/PatientLabs_AddEdit.js",
             "~/Scripts/select2.js",
             "~/Scripts/jquery-mask.min.js"));

            //Cascading Style sheets 

            bundles.Add(new StyleBundle("~/Content/loginCss").Include(
                      "~/Content/style-login.css",
                      "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/OPDListViewCss").Include(
                   "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/IPListViewCss").Include(
                  "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/LayoutCss").Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/demo.css",
                    "~/Content/materialdashboard.css",
                      "~/Content/toastr.css",
                      "~/Content/custom.css",
                      "~/Content/bootstrap-multiselect.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/LabMgmtCss").Include(
                 "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/LabParmsCss").Include(
                 "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/LabParmsMapping").Include(
                "~/Content/css/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/PatientLabsCss").Include(
              "~/Content/simplePagination.css"));

            bundles.Add(new StyleBundle("~/Content/PatientLabs_AddEdit").Include(
              "~/Content/css/select2.min.css"));
        }
    }
}
