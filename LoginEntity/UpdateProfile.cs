using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UpdateProfile
    {

        public string Autoid { get; set; }
        public string img { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string conformpassword { get; set; }
        public string EmailID { get; set; }
        public string ContactNO { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string MAILAUTOID { get; set; }
        public string USERAUTOID { get; set; }
    }

    public class RequestUpdateProfile
    {

        public UpdateProfile requestUpdateProfile { get; set; }
    }


    public class ResponseUpdateProfile
    {

        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }


        public DataTable JS_Details { get; set; }
        public DataTable JS_MAILID { get; set; }

      

    }
}
