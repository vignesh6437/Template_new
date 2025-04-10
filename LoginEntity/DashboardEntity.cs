using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace LoginEntity
{
    public class DashboardEntity
    {

        public string Autoid { get; set; }
        public string Activeemployee { get; set; }
        public string InActiveemployee { get; set; }
        public string TotalEmployee { get; set; }
        public string Designation { get; set; }
        public string EmailID { get; set; }
        public string ContactNO { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string USERCODE { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
  
    public class RequestDashboard
    {

        public DashboardEntity requestDashboard { get; set; }
       
    }


    public class ResponseDashboard
    {

        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }


        public DataTable JS_DashboardDetails { get; set; }
        public DataTable Activeemployee { get; set; }
        public DataTable InActiveemployee { get; set; }
        public DataTable TotalEmployee { get; set; }
        public DataTable JS_Status { get; set; }

        public DataTable JS_Department { get; set; }
        public DataTable Js_Designation { get; set; }
        public DataTable Js_CHATCONTACTS { get; set; }
        public DataTable messages { get; set; }
        public DataTable NotifiMessages { get; set; }

    }
}
