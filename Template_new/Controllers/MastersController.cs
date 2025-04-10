using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BC;
using Entity;
using System.Web.Script.Serialization;
using Template_new.Common;

namespace Template_new.Controllers
{
    public class MastersController : Controller
    {
        #region EmployeeMaster.................................
        public ActionResult EmployeeMaster()
        {
            return View();
        }

        public ActionResult EmployeeMasterPageLoad()
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            EmployeeMasterBC bc = new EmployeeMasterBC();
            response = bc.EmployeeMasterPageLoadBC();
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Status)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Department)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.Js_Designation)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_EmployeeMasterDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult InsertEmployeeMaster(string actiontype, string Autoid, string Empcode, string EmpName, string department, string designation, string emailid, string contactno, string status)
        {
            RequestEmployeeMaster request = new RequestEmployeeMaster();
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            request.requestEmployeeMaster = new EmployeeMasterEntity();
            request.requestEmployeeMaster.Autoid = Autoid;
            request.requestEmployeeMaster.EmployeeCode = Empcode;
            request.requestEmployeeMaster.EmployeeName = EmpName;
            request.requestEmployeeMaster.Department = department;
            request.requestEmployeeMaster.Designation = designation;
            request.requestEmployeeMaster.EmailID = emailid;
            request.requestEmployeeMaster.ContactNO = contactno;
            request.requestEmployeeMaster.CreatedBy = Session["LoginEmployeeCode"].ToString();
            request.requestEmployeeMaster.Status = status;
            EmployeeMasterBC bc = new EmployeeMasterBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.EmployeeMasterInsertBC(request);
            }
            else
            {
                response = bc.EmployeeMasterUpdateBC(request);
            }

            ManageError Err = new ManageError();
            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;
            return Json(resultjson);
        }

        public ActionResult EmployeeMasterEdit(string autoid)
        {

            RequestEmployeeMaster request = new RequestEmployeeMaster();
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            request.requestEmployeeMaster = new EmployeeMasterEntity();
            request.requestEmployeeMaster.Autoid = autoid;
            EmployeeMasterBC bc = new EmployeeMasterBC();
            response = bc.EmployeeMasterEditBC(request);
            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_EmployeeMasterDetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}