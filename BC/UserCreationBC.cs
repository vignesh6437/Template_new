using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DAC;


namespace BC
{
    public class UserCreationBC
    {

        public ResponseUserCreation FetchUserCreationPageLoadBC()
        {
            ResponseUserCreation response = new ResponseUserCreation();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserCreationPageLoadDAL();

            return response;
        }
        //Fetch User Details By User Code
        public ResponseUserCreation FetchUserCreationbyEmpNameBC(RequestUserCreation request)
        {

            ResponseUserCreation response = new ResponseUserCreation();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserCreationbyEmpNameDAL(request);

            return response;
         

        }
        //Insert 
        public ResponseUserCreation InsertUserCreationBC(RequestUserCreation request)
        {
            ResponseUserCreation response = new ResponseUserCreation();
                
            WMSDAL DAL = new WMSDAL();
            response = DAL.InsertUserCreationDAL(request);
            return response;

        }
        public ResponseUserCreation FetchUserCreationbyUserDetailsBC(RequestUserCreation request)
        {

            ResponseUserCreation response = new ResponseUserCreation();
            WMSDAL DAL = new WMSDAL();
            response = DAL.FetchUserCreationbyUserDetailsDAL(request);

            return response;


        }
        //Update 
        public ResponseUserCreation UpdateUserCreationBC(RequestUserCreation request)
        {


            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateUserCreationDAL(request);
            }
            return response;

        }

        public List<ErrorItem> Validate(RequestUserCreation request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestUserCreation.EmployeeCode == "")
                err.Add(new ErrorItem { DataItem = "Employee Code", ErrorNo = "SSB0010" });
            if (request.requestUserCreation.UserName == "")
                err.Add(new ErrorItem { DataItem = "User Name", ErrorNo = "SSB0009" });
            if (request.requestUserCreation.UserPassword == "")
                err.Add(new ErrorItem { DataItem = "Password", ErrorNo = "SSB0009" });
            if (request.requestUserCreation.ConfirmPassword == "")
                err.Add(new ErrorItem { DataItem = "Confirm Password", ErrorNo = "SSB0009" });
            if (request.requestUserCreation.UserPassword != "" && request.requestUserCreation.ConfirmPassword !="")
            {
                if (request.requestUserCreation.UserPassword != request.requestUserCreation.ConfirmPassword)
                    err.Add(new ErrorItem { DataItem = "Password and Confirm Password Shoudbe Match", ErrorNo = "SSB0000" });
            }
            if (request.requestUserCreation.RecordStatus == "")
                err.Add(new ErrorItem { DataItem = "Status", ErrorNo = "SSB0010" });



            return err;
        }
       
    }
}
