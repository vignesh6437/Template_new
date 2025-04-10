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
using LoginEntity;


namespace Template_new.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult DashboardPageLoad()
        {
            ResponseDashboard response = new ResponseDashboard();
            RequestDashboard request = new RequestDashboard();
            request.requestDashboard = new DashboardEntity();
            request.requestDashboard.USERCODE = Session["LoginEmployeeCode"].ToString();
            DashboardBC bc = new DashboardBC();
            response = bc.DashboardPageLoadBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.Activeemployee)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.InActiveemployee)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.TotalEmployee)
                + "|" + Utility.DataTableToJSONWithJavaScriptSerializer(response.Js_CHATCONTACTS);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        #region UpdateProfile.................................
        public ActionResult About()
        {
            return View();
        }

        public ActionResult UpdateProfilePageLoad()
        {
            ResponseUpdateProfile response = new ResponseUpdateProfile();
            RequestUpdateProfile request= new RequestUpdateProfile();
            request.requestUpdateProfile = new UpdateProfile();
            request.requestUpdateProfile.CreatedBy = Session["LoginEmployeeCode"].ToString();
            UpdateProfileBC bc = new UpdateProfileBC();
            response = bc.UpdateProfilePageLoadBC(request);
            string resulttable =  Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Details)+'|'+
                Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_MAILID);

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult InsertProfile( string username, string emaiid, string password, string conformpassword,string IMAGE,string userautoid,string mailautoid)
        {
            RequestUpdateProfile request = new RequestUpdateProfile();
            ResponseUpdateProfile response = new ResponseUpdateProfile();
            request.requestUpdateProfile = new UpdateProfile();
            //request.requestEmployeeMaster.Autoid = Autoid;
            request.requestUpdateProfile.username = username;
            request.requestUpdateProfile.password = password;
            request.requestUpdateProfile.conformpassword = conformpassword;
            request.requestUpdateProfile.EmailID = emaiid;
            request.requestUpdateProfile.img = emaiid;
            request.requestUpdateProfile.MAILAUTOID = mailautoid;
            request.requestUpdateProfile.USERAUTOID = userautoid;

            string photopath = Server.MapPath("~/RearDoorPhoto/");
            string photofullpath = "";
            string photofilename = "";

            Directory.CreateDirectory(photopath);
            string photocount = Request.Form["photocountS"];
            for (int i = 0; i < Convert.ToInt32(photocount); i++)
            {
                var PostedFileRearDoor = System.Web.HttpContext.Current.Request.Files["photosA" + i];
                if (PostedFileRearDoor != null && PostedFileRearDoor.ContentLength > 0)
                {
                    string photofileName = DateTime.Now.Ticks.ToString();
                    photofullpath = photopath + "/" + "RefImg" + photofileName + ".jpg";
                    photofilename += "RefImg" + photofileName + ".jpg";
                    // Use Server.MapPath here 
                    PostedFileRearDoor.SaveAs(photofullpath);
                }
            }

            if (photocount == "0")
            {
                request.requestUpdateProfile.img = IMAGE;
            }
            else
            {
                request.requestUpdateProfile.img = photofilename;
            }
            request.requestUpdateProfile.CreatedBy = Session["LoginEmployeeCode"].ToString();
           
            UpdateProfileBC bc = new UpdateProfileBC();
           
            response = bc.UpdateProfileInsertBC(request);

            ManageError Err = new ManageError();
            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;
            return Json(resultjson);
        }

        [HttpGet]
        public JsonResult GetMessages(string sender, string receiver)
        {
            
                // Create the request object
                ResponseDashboard response = new ResponseDashboard();
                RequestDashboard request = new RequestDashboard();
                request.requestDashboard = new DashboardEntity();
                request.requestDashboard.Sender = sender;
                request.requestDashboard.Receiver = receiver;
                DashboardBC bc = new DashboardBC();
                response = bc.GetMessagesBC(request);

            //// Return the response as JSON
            //if (response.result)
            //{
            //    // Assuming response contains a list of messages
            //    return Json(new { success = true, messages = response.messages }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { success = false, message = "Error retrieving messages" }, JsonRequestBehavior.AllowGet);
            //}

            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.messages);
               

            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult SendMessage(string sender, string receiver, string content, string timestamp)
        {
            // Create a new request object for sending the message
            ResponseDashboard response = new ResponseDashboard();
            RequestDashboard request = new RequestDashboard();
            request.requestDashboard = new DashboardEntity();
            request.requestDashboard.Sender = sender;
            request.requestDashboard.Receiver = receiver;
            request.requestDashboard.Content = content;
            request.requestDashboard.Timestamp = DateTime.Parse(timestamp);
               

            // Call the SendMessageDAL method to insert the message into the database
             //response = new HomeController().SendMessageDAL(request);
            DashboardBC bc = new DashboardBC();
            response = bc.SendMessagesBC(request);

            // Return a response to indicate success or failure
            if (response.result)
            {
                return Json(new { success = true, message = "Message sent successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error sending message" });
            }
        }

        [HttpGet]
        public ActionResult MessageNotification()
        {
            ResponseDashboard response = new ResponseDashboard();
            RequestDashboard request = new RequestDashboard();
            request.requestDashboard = new DashboardEntity();
            request.requestDashboard.USERCODE = Session["LoginEmployeeCode"].ToString();
            DashboardBC bc = new DashboardBC();
            response = bc.MessageNotificationBC(request);
            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.NotifiMessages);
            var data = resulttable;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        #endregion




    }
}