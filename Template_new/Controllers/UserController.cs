using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BC;
using Entity;
using System.Web.Script.Serialization;
using Template_new.Common;
using System.IO;

namespace Template_new.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        #region   USER CREATION.................
        public ActionResult UserCreation()
        {
            Session["View"] = "UAC001";
            return View();
        }

        //Page Load
        public ActionResult GetUserCreationPageLoad()
        {


            UserCreationEntity usercreationentity = new UserCreationEntity();
            RequestUserCreation requestusercreation = new RequestUserCreation();
            ResponseUserCreation responseusercreation = new ResponseUserCreation();

            requestusercreation.requestUserCreation = usercreationentity;
            UserCreationBC bc = new UserCreationBC();
            responseusercreation = bc.FetchUserCreationPageLoadBC();



            string resulttable =Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_Employee) + "|" +
                Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_RecordStatus)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseusercreation.JS_UserCreationDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //Fetch Grid
        public ActionResult GetUserCreationByEmployeename(string EmpCode)
        {


            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.EmployeeCode = EmpCode;
            UserCreationBC bc = new UserCreationBC();
            response = bc.FetchUserCreationbyEmpNameBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Employee);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //Insert and Update
        [HttpPost]
        public ActionResult InsertUserCreation(string actiontype, string Autoid, string Empcode, string EmpName, string UserName, string Password, string ConfirmPassword, string Status,string IMAGE)
        {


            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.Autoid = Autoid;
            request.requestUserCreation.EmployeeCode = Empcode;
            request.requestUserCreation.EmployeeName = EmpName;
            request.requestUserCreation.RecordStatus = Status;
            request.requestUserCreation.UserName = UserName;
            request.requestUserCreation.UserPassword = Password;
            request.requestUserCreation.ConfirmPassword = ConfirmPassword;
            request.requestUserCreation.CreatedBy = Session["LoginEmployeeCode"].ToString();
            request.requestUserCreation.CreatedDate = DateTime.Now;
            request.requestUserCreation.ModifiedBy = Session["LoginEmployeeCode"].ToString();
            request.requestUserCreation.ModifiedDate = DateTime.Now;
            //   request.requestUserCreation.Recortimestamp = Request.Form["hrecordtimestamp"] == string.Empty ? 0 : Convert.ToInt64(Request.Form["hrecordtimestamp"]);
            //string photopath = Server.MapPath("~/RearDoorPhoto/");
            //string photofullpath = "";
            //string photofilename = "";

            //Directory.CreateDirectory(photopath);
            //string photocount = Request.Form["photocountS"];
            //for (int i = 0; i < Convert.ToInt32(photocount); i++)
            //{
            //    var PostedFileRearDoor = System.Web.HttpContext.Current.Request.Files["photosA" + i];
            //    if (PostedFileRearDoor != null && PostedFileRearDoor.ContentLength > 0)
            //    {
            //        string photofileName = DateTime.Now.Ticks.ToString();
            //        photofullpath = photopath + "/" + "RefImg" + photofileName + ".jpg";
            //        photofilename += "RefImg" + photofileName + ".jpg";
            //        // Use Server.MapPath here 
            //        PostedFileRearDoor.SaveAs(photofullpath);
            //    }
            //}

            //if (photocount == "0")
            //{
            //    request.requestUserCreation.img = IMAGE;
            //}
            //else
            //{
            //    request.requestUserCreation.img = photofilename;
            //}

            UserCreationBC bc = new UserCreationBC();
            string value = "Update";
            if (value != actiontype)
                response = bc.InsertUserCreationBC(request);
            else
                response = bc.UpdateUserCreationBC(request);

            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;

            return Json(resultjson);
        }


        //Fetch Grid
        public ActionResult GetUserCreationByID(string usercode)
        {

            RequestUserCreation request = new RequestUserCreation();
            ResponseUserCreation response = new ResponseUserCreation();
            request.requestUserCreation = new UserCreationEntity();
            request.requestUserCreation.EmployeeCode = usercode;
            UserCreationBC bc = new UserCreationBC();
            response = bc.FetchUserCreationbyUserDetailsBC(request);

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_UserCreationDetails);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region User Authentication................................................................
        public ActionResult UserAuthentication()
        {
            Session["View"] = "UAC002";
            return View();
        }

        //Page Load
        public ActionResult GetUserAuthenticationPageLoad()
        {

            UserAuthenticationEntity userauthenticationentity = new UserAuthenticationEntity();
            RequestUserAuthentication requestuserauthentication = new RequestUserAuthentication();
            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            requestuserauthentication.requestUserAuthentication = userauthenticationentity;
            UserAuthenticationBC bc = new UserAuthenticationBC();
            responseuserauthentication = bc.FetchUserAuthenticationPageLoadBC();



            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_UserName)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_ScreenDetails);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        //Fetch ScreenDetails by UserName
        public ActionResult FetchScreenDetailsByUserName(string username)
        {

            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            RequestUserAuthentication requestuserauthentication = new RequestUserAuthentication();
            requestuserauthentication.requestUserAuthentication = new UserAuthenticationEntity();
            requestuserauthentication.requestUserAuthentication.UserName = username;
            UserAuthenticationBC bc = new UserAuthenticationBC();
            responseuserauthentication = bc.FetchScreenDetailsbyUserNameBC(requestuserauthentication);


            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_ScreenDetails);
            //+ '|' + Utility.DataTableToJSONWithJavaScriptSerializer(responseuserauthentication.JS_UserName);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult InsertUserAuthenticationMaster(string actiontype)
        {

            String[] screenid = Request.Form["screenid"].Split('^');
            //String[] view = Request.Form["view"].Split('^');
            //String[] Cancel = Request.Form["Cancel"].Split('^');

            RequestUserAuthentication request = new RequestUserAuthentication();
            ResponseUserAuthentication responseuserauthentication = new ResponseUserAuthentication();
            request.requestUserAuthentication = new UserAuthenticationEntity();
            request.requestUserAuthentication.UserName = Request.Form["username"];
            request.requestUserRoleScreenDetails = new List<UserAuthenticationEntity>();
            for (int i = 0; i < screenid.Length; i++)
            {
                UserAuthenticationEntity ua = new UserAuthenticationEntity();
                ua.screenid = screenid[i];
                ua.UserName = Request.Form["username"];
                //ua.view = view[i];
                //ua.Cancel = Cancel[i];
                //ua.HOD = Request.Form["HOD"];
                //ua.Debit = Request.Form["Debit"];
                //ua.UH = Request.Form["UH"];
                //ua.SEH = Request.Form["SEH"];
                ua.CreatedBy = "ADMIN";
                ua.CreatedDate = DateTime.Now;
                ua.ModifiedBy = "ADMIN";
                ua.ModifiedDate = DateTime.Now;
                request.requestUserRoleScreenDetails.Add(ua);
            }

            UserAuthenticationBC bc = new UserAuthenticationBC();
            string value = "Update";

            if (value != Request.Form["actiontype"].ToString())
                responseuserauthentication = bc.InsertUserAuthenticationBC(request);



            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(responseuserauthentication.ErrorContainer);
            resultjson = responseuserauthentication.result + "|" + resultjson;

            return Json(resultjson);
        }


        #endregion
    }
}