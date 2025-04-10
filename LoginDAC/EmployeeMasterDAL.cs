using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Transactions;

namespace DAC
{
    public partial class WMSDAL
    {

        public ResponseEmployeeMaster EmployeeMasterPageLoadDAL()
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[EMPLOYEEMASTER_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Status = ds.Tables[0];
                            response.JS_Department = ds.Tables[1];
                            response.Js_Designation = ds.Tables[2];
                            response.JS_EmployeeMasterDetails = ds.Tables[3];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }


            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EmployeeMasterPageLoadDAL: " + "Method Name EmployeeMasterPageLoadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }
        public ResponseEmployeeMaster InsertEmployeeMasterDAL(RequestEmployeeMaster request)
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = new List<ErrorItem>();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[EMPLOYEEMASTER_INSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEECODE", request.requestEmployeeMaster.EmployeeCode));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEENAME", request.requestEmployeeMaster.EmployeeName));
                        cmd.Parameters.Add(new SqlParameter("@DEPARTMENT", request.requestEmployeeMaster.Department));
                        cmd.Parameters.Add(new SqlParameter("@DESIGNATION", request.requestEmployeeMaster.Designation));
                        cmd.Parameters.Add(new SqlParameter("@EMAILID", request.requestEmployeeMaster.EmailID));
                        cmd.Parameters.Add(new SqlParameter("@CONTACTNO", request.requestEmployeeMaster.ContactNO));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestEmployeeMaster.Status));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestEmployeeMaster.CreatedBy));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
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
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("InsertPartMasterDAL: " + "Method Name InsertPartMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }
            return response;
        }

        public ResponseEmployeeMaster EmployeeMasterEditDAL(RequestEmployeeMaster request)
        {

            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[EMPLOYEEMASTER_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestEmployeeMaster.Autoid.Trim().ToUpper()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_EmployeeMasterDetails = ds.Tables[0];
                            response.result = true;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("EmployeeMasterEditDAL: " + "Method Name EmployeeMasterEditDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }



            return response;

        }

        //Update 
        public ResponseEmployeeMaster UpdateEmployeeMasterDAL(RequestEmployeeMaster request)
        {


            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[EMPLOYEEMASTER_UPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEECODE", request.requestEmployeeMaster.EmployeeCode));
                        cmd.Parameters.Add(new SqlParameter("@EMPLOYEENAME", request.requestEmployeeMaster.EmployeeName));
                        cmd.Parameters.Add(new SqlParameter("@DEPARTMENT", request.requestEmployeeMaster.Department));
                        cmd.Parameters.Add(new SqlParameter("@DESIGNATION", request.requestEmployeeMaster.Designation));
                        cmd.Parameters.Add(new SqlParameter("@EMAILID", request.requestEmployeeMaster.EmailID));
                        cmd.Parameters.Add(new SqlParameter("@CONTACTNO", request.requestEmployeeMaster.ContactNO));
                        cmd.Parameters.Add(new SqlParameter("@STATUS", request.requestEmployeeMaster.Status));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestEmployeeMaster.CreatedBy));
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.requestEmployeeMaster.Autoid.Trim()));

                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
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
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("UpdateEmployeeMasterDAL: " + "Method Name UpdateEmployeeMasterDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }
            return response;
        }
    }
}
