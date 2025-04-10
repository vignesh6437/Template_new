using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Entity
{
    
        public class EmployeeMasterEntity
        {
            public string Autoid { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string Department { get; set; }
            public string Designation { get; set; }
            public string EmailID { get; set; }
            public string ContactNO { get; set; }
            public string CreatedBy { get; set; }
            public string Status { get; set; }
        }

        public class RequestEmployeeMaster
        {

            public EmployeeMasterEntity requestEmployeeMaster { get; set; }
        }


        public class ResponseEmployeeMaster
        {

            public bool result { get; set; }


            public List<ErrorItem> ErrorContainer { get; set; }


            public DataTable JS_EmployeeMasterDetails { get; set; }

            public DataTable JS_Status { get; set; }

            public DataTable JS_Department { get; set; }
            public DataTable Js_Designation { get; set; }

        }
    
}
