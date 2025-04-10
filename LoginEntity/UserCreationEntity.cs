using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Entity
{
    public class UserCreationEntity
    {
        public string Autoid { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string RecordStatus { get; set; }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
         public string Processname { get; set; }
        public string ConfirmPassword { get; set; }
        public string img { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
       
    }

   
    public class RequestUserCreation
    {
      
        public UserCreationEntity requestUserCreation { get; set; }
    }


    public class ResponseUserCreation
    {

        public bool result { get; set; }

   
        public List<ErrorItem> ErrorContainer { get; set; }

  
        public DataTable JS_UserCreationDetails { get; set; }

        public DataTable JS_RecordStatus { get; set; }

        public DataTable JS_Employee { get; set; }
        public DataTable Js_Process { get; set; }
  

        public string EmployeeCodeExist { get; set; }

        public string UserNameExist { get; set; }
    }

    
}
