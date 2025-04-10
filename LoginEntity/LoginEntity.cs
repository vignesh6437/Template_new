using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LoginEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmployeeCode { get; set; }
        public string Modifiedby { get; set; }
        public string oldpassword { get; set; }

    }

    public class RequestLoginDetails
    {

        public LoginEntity requestLoginDetails { get; set; }
    }

    public class ResponseLoginDetails
    {

        public bool result { get; set; }


        public List<ErrorItem> ErrorContainer { get; set; }

        public DataTable JS_LoginDetails { get; set; }

        public DataTable JS_ScreenDetails { get; set; }
        public DataTable JS_Transporter { get; set; }
        public DataTable JS_OldPassword { get; set; }

        public DataTable JS_LoginEmployeeCode { get; set; }
        public DataTable JS_Map { get; set; }
        public DataTable JS_Mapframe { get; set; }
        public DataTable JS_EMPTYCOUNT { get; set; }
    }
}
