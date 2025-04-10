using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using BC;
using System.Web.UI.WebControls;
using Template_new.Common;
using System.Web.Security;

namespace Template_new.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpCachePolicyBase varWK0 = base.Response.Cache;
                DateTime utcNow = DateTime.UtcNow;
                varWK0.SetExpires(utcNow.AddHours(-1.0));
                base.Response.Cache.SetNoStore();
                Session["Ucon"] = null;
                Session["LoginEmployeeCode"] = null;

            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult LoginCheck(string Username, string Upassword)
        {
            RequestLoginDetails request = new RequestLoginDetails();
            request.requestLoginDetails = new Entity.LoginEntity();
            request.requestLoginDetails.UserName = Username;
            request.requestLoginDetails.Password = Upassword;
            ResponseLoginDetails response = new ResponseLoginDetails();
            LoginBC bc = new LoginBC();
            response = bc.LoginsBC(request);
            var data = "";
            if (response.result)
            {

                Session["Ucon"] = response;
                if (response.JS_LoginEmployeeCode.Rows.Count > 0)
                {
                    Session["LoginEmployeeCode"] = response.JS_LoginEmployeeCode.Rows[0]["EMPLOYEECODE"].ToString();
                    data = response.result + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LoginDetails)
                        + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_ScreenDetails)
                        + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_LoginEmployeeCode);


                }
                else
                {
                    data = false + "|" + "ERROR.";
                }



            }
            else
            {
                ManageError Err = new ManageError();
                string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);



                data = response.result + "|" + resultjson;
            }
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        public ActionResult GetSessionUserValue()
        {
            var data = "";
            if (Session["Ucon"] != null)
            {
                ResponseLoginDetails responseSession = new ResponseLoginDetails();
                responseSession = (ResponseLoginDetails)Session["Ucon"];
                data = Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_LoginDetails)
                    + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_ScreenDetails)
                    + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseSession.JS_LoginEmployeeCode);
            }
            else
            {
                Session["Ucon"] = null;
                // return RedirectToAction("Index");
            }

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }

        [HttpPost]
        public ActionResult logoutnew()
        {
            RequestLoginDetails request = new RequestLoginDetails();
            request.requestLoginDetails = new Entity.LoginEntity();
            request.requestLoginDetails.EmployeeCode = Session["LoginEmployeeCode"].ToString();
            ResponseLoginDetails response = new ResponseLoginDetails();
            LoginBC bc = new LoginBC();
            response = bc.LogoutUpdateBC(request);


            Session["Ucon"] = null;
            Session["LoginEmployeeCode"] = null;
            base.Session.Clear();
            base.Session.Abandon();
            base.Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectPermanent("~/");


        }
    }
}