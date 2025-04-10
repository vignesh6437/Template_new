using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Transactions;
using System.Data.SqlClient;
using System.Data;


namespace DAC
{
    public partial class WMSDAL
    {
        public ResponseUserAuthentication FetchUserAuthenticationPageLoadDAL()
        {
            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[USERS].[UserAuthentication_FetchPageLoad]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_UserName = ds.Tables[0];
                            response.JS_ScreenDetails = ds.Tables[1];// get the User Creation Details
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserAuthenticationPageLoadDAL: " + "Method Name FetchUserAuthenticationPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }


        //Fetch ScreenDetails by UserName
        public ResponseUserAuthentication FetchScreenDetailsbyUserNameDAL(RequestUserAuthentication request)
        {
            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserAuthentication_FetchScreenDetailsByUserName", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", request.requestUserAuthentication.UserName.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_ScreenDetails = ds.Tables[0];
                            //response.JS_UserName = ds.Tables[1];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserAuthenticationbyDepartmentDAL: " + "Method Name FetchUserAuthenticationbyDepartmentDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }



            return response;
        }

        //Fetch User Details By User Code
        public ResponseUserAuthentication FetchUserAuthenticationbyUserCodeDAL(RequestUserAuthentication request)
        {


            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserAuthentication_FetchUserAuthenticationbyUserCode", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestUserAuthentication.UserCode.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_UserAuthenticationDetails = ds.Tables[0];// get the Shift Details
                            response.result = true;
                        }
                        response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = request.requestUserAuthentication.UserCode.ToUpper() });
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("FetchUserAuthenticationbyUserCodeDAL: " + "Method Name FetchUserAuthenticationbyUserCodeDALs" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        //Insert 
        public ResponseUserAuthentication InsertUserAuthenticationDAL(RequestUserAuthentication request)
        {


            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                int rowcount = 0;
                DataSet ds = new DataSet();
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[USERS].[UserAuthentication_DELETE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERNAME", request.requestUserAuthentication.UserName);
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);

                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                foreach (UserAuthenticationEntity req in request.requestUserRoleScreenDetails)
                                {
                                    SqlCommand cmd1 = new SqlCommand("Users.UserAuthentication_Insert", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@SCREENID", req.screenid);
                                    cmd1.Parameters.AddWithValue("@USERNAME", req.UserName);
                                    cmd1.Parameters.AddWithValue("@CREATEDBY", req.CreatedBy);
                                    cmd1.Parameters.AddWithValue("@CREATEDDATE", req.CreatedDate);
                                    cmd1.Parameters.AddWithValue("@MODIFIEDBY", req.ModifiedBy);
                                    cmd1.Parameters.AddWithValue("@MODIFIEDDATE", req.ModifiedDate);
                                    SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                                    oda1.Fill(ds);
                                    if (ds != null)
                                    {

                                        rowcount++;
                                    }
                                }
                            }
                        }
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestUserRoleScreenDetails.Count == rowcount)
                        {
                            scope.Complete();
                            response.result = true;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                        else
                        {
                            response.result = false;
                            response.ErrorContainer.Add(new ErrorItem { ErrorNo = ds.Tables[0].Rows[0][1].ToString(), DataItem = ds.Tables[0].Rows[0][2].ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertUserAuthenticationDAL: " + "Method Name InsertUserAuthenticationDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }

        //Search 
        public ResponseUserAuthentication SearchUserAuthenticationDAL(RequestUserAuthentication request)
        {

            ResponseUserAuthentication response = new ResponseUserAuthentication();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("Users.UserAuthentication_Search", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERNAME", request.requestUserAuthentication.UserName));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestUserAuthentication.UserCode));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_UserAuthenticationDetails = ds.Tables[0];// get the Shift Details
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("SearchUserAuthenticationDAL: " + "Method Name SearchUserAuthenticationDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }


            return response;
        }
    }
}
