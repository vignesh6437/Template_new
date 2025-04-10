using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;


namespace DAC
{
    public partial class WMSDAL
    {

        public ResponseUserCreation FetchUserCreationPageLoadDAL()
        {
            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserCreation_FetchPageLoad", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Employee = ds.Tables[0];
                            response.JS_RecordStatus = ds.Tables[1];
                            response.JS_UserCreationDetails = ds.Tables[2];  // get the User Creation Details

                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserCreationPageLoadDAL: " + "Method Name FetchUserCreationPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }
        //Fetch User Details By User Code
        public ResponseUserCreation FetchUserCreationbyEmpNameDAL(RequestUserCreation request)
        {

            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserCreation_FetchEmployeeName", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEECODE", request.requestUserCreation.EmployeeCode.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Employee = ds.Tables[0];// get the Shift Details
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestUserCreation.EmployeeCode.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserCreationbyUserCodeDAL: " + "Method Name FetchUserCreationbyUserCodeDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }



            return response;

        }
        //Insert 
        public ResponseUserCreation InsertUserCreationDAL(RequestUserCreation request)
        {

            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserCreation_Insert", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEECODE", request.requestUserCreation.EmployeeCode.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEENAME", request.requestUserCreation.EmployeeName.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestUserCreation.RecordStatus));
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", request.requestUserCreation.UserName));
                        cmd.Parameters.Add(new SqlParameter("@USERPASSWORD", request.requestUserCreation.UserPassword));
                        cmd.Parameters.Add(new SqlParameter("@CONFIRMPASSWORD", request.requestUserCreation.ConfirmPassword));
                        //cmd.Parameters.Add(new SqlParameter("@IMAGE", request.requestUserCreation.img));
                        cmd.Parameters.Add(new SqlParameter("@CREATEDBY", request.requestUserCreation.CreatedBy.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CREATEDDATE", request.requestUserCreation.CreatedDate));
                        cmd.Parameters.Add(new SqlParameter("@MODIFIEDBY", request.requestUserCreation.ModifiedBy.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MODIFIEDDATE", request.requestUserCreation.ModifiedDate));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;

                            response.JS_UserCreationDetails = ds.Tables[0];
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertUserCreationDAL: " + "Method Name InsertUserCreationDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        public ResponseUserCreation FetchUserCreationbyUserDetailsDAL(RequestUserCreation request)
        {

            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserCreation_FetchUserCreationsbyEmployeeCode", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestUserCreation.EmployeeCode.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_UserCreationDetails = ds.Tables[0];
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestUserCreation.EmployeeCode.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserCreationbyUserCodeDAL: " + "Method Name FetchUserCreationbyUserCodeDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }



            return response;

        }
        //Update 
        public ResponseUserCreation UpdateUserCreationDAL(RequestUserCreation request)
        {


            ResponseUserCreation response = new ResponseUserCreation();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserCreation_Update", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestUserCreation.Autoid.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEECODE", request.requestUserCreation.EmployeeCode.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEENAME", request.requestUserCreation.EmployeeName.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@RECORDSTATUS", request.requestUserCreation.RecordStatus));
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", request.requestUserCreation.UserName));
                        cmd.Parameters.Add(new SqlParameter("@USERPASSWORD", request.requestUserCreation.UserPassword));
                        cmd.Parameters.Add(new SqlParameter("@CONFIRMPASSWORD", request.requestUserCreation.ConfirmPassword));
                        //cmd.Parameters.Add(new SqlParameter("@IMAGE", request.requestUserCreation.img));
                        cmd.Parameters.Add(new SqlParameter("@CREATEDBY", request.requestUserCreation.CreatedBy.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@CREATEDDATE", request.requestUserCreation.CreatedDate));
                        cmd.Parameters.Add(new SqlParameter("@MODIFIEDBY", request.requestUserCreation.ModifiedBy.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@MODIFIEDDATE", request.requestUserCreation.ModifiedDate));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                response.result = true;
                            else
                                response.result = false;
                            response.JS_UserCreationDetails = ds.Tables[0];// get the Shift Details
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertShiftDetailsDAL: " + "Method Name InsertShiftDetailsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }



            return response;

        }

    }
}
