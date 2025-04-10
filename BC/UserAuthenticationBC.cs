using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;
using DAC;


namespace BC
{
    public partial class UserAuthenticationBC
    {
        public ResponseUserAuthentication FetchUserAuthenticationPageLoadBC()
        {
            ResponseUserAuthentication response = new ResponseUserAuthentication();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserAuthenticationPageLoadDAL();

            return response;
        }


        //Fetch ScreenDetails by UserName
        public ResponseUserAuthentication FetchScreenDetailsbyUserNameBC(RequestUserAuthentication request)
        {
            ResponseUserAuthentication response = new ResponseUserAuthentication();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchScreenDetailsbyUserNameDAL(request);

            return response;
        }

        //Fetch User Details By User Code
        public ResponseUserAuthentication FetchUserAuthenticationbyUserCodeBC(RequestUserAuthentication request)
        {


            ResponseUserAuthentication response = new ResponseUserAuthentication();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserAuthenticationbyUserCodeDAL(request);

            return response;

        }
        //Insert 
        public ResponseUserAuthentication InsertUserAuthenticationBC(RequestUserAuthentication request)
        {


            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertUserAuthenticationDAL(request);
            }
            return response;

        }

        //Search 
        public ResponseUserAuthentication SearchUserAuthenticationBC(RequestUserAuthentication request)
        {

            ResponseUserAuthentication response = new ResponseUserAuthentication();
            WMSDAL DAL = new WMSDAL();
            response = DAL.SearchUserAuthenticationDAL(request);

            return response;
        }

        public List<ErrorItem> Validate(RequestUserAuthentication request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            bool isusername = false;
            foreach (UserAuthenticationEntity req in request.requestUserRoleScreenDetails)
            {
                if (req.UserName == "")
                    isusername = true;
            }

            if (isusername == true)
            {
                err.Add(new ErrorItem { DataItem = "User Name", ErrorNo = "IAL0067" });
            }

            return err;
        }
    }

}
