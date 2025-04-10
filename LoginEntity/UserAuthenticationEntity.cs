using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Entity
{
    public class UserAuthenticationEntity
    {
        public string screenid { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string IsActive { get; set; }
        public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string FunctionId { get; set; }
        public string ProcessId { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string connectionname { get; set; }
        public string view { get; set; }
        public string HOD { get; set; }
        public string Debit { get; set; }
        public string UH { get; set; }
        public string SEH { get; set; }
        public string Cancel { get; set; }
        public string edit { get; set; }
    }


    public class RequestUserAuthentication
    {
        public UserAuthenticationEntity requestUserAuthentication { get; set; }
        public List<UserAuthenticationEntity> requestUserRoleScreenDetails { get; set; }
    }


    public class ResponseUserAuthentication
    {
        
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
        public DataTable JS_UserAuthenticationDetails { get; set; }
        public DataTable JS_ScreenDetails { get; set; }
        public DataTable JS_UserName { get; set; }
    }
}
