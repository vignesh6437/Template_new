using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DAC
{
    public partial class WMSDAL
    {
        public ResponseGRNEntry GRNEntryPageloadDAL(RequestGRNEntry request)
        {

            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[Masters].[GRNENTRY_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.JS_Supplierdetails = ds.Tables[0];
                            response.result = true;
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("GRNEntryPageloadDAL: " + "Method Name GRNEntryPageloadDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;
            }


            return response;
        }
        public ResponseGRNEntry GRNEntryInsertDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            string GRNNO = "";
            try
            {
                DataSet ds = new DataSet();

                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        int rowcount = 0;
                        SqlCommand cmd = new SqlCommand("[Masters].[GRNENTRY_HEADERINSERT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.ReqGRNEntry.GRNNO.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@DATE", request.ReqGRNEntry.DATE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.ReqGRNEntry.USERCODE.Trim()));

                        SqlDataAdapter oda = new SqlDataAdapter(cmd);

                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                GRNNO = ds.Tables[1].Rows[0]["GRNNO"].ToString();

                                foreach (GRNEntryDetailEntity det in request.requestGRNEntrydetails)
                                {

                                    SqlCommand cmd1 = new SqlCommand("[Masters].[GRNENTRY_DEATILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@GRNNO", GRNNO));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.ReqGRNEntry.USERCODE.Trim()));
                                    SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                                    oda1.Fill(ds);
                                    if (ds != null)
                                    {
                                        if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                        {
                                            rowcount++;
                                        }
                                    }
                                }
                            }
                        }
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestGRNEntrydetails.Count == rowcount)
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
                string responselog = createlog("GRNEntryInsertDAL: " + "Method Name GRNEntryInsertDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
        public ResponseGRNEntry GRNEntryUpdateDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            string GRNNO = "";
            try
            {
                DataSet ds = new DataSet();

                int totalcount = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        int rowcount = 0;
                        SqlCommand cmd = new SqlCommand("[Masters].[GRNENTRY_HEADERUPDATE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.ReqGRNEntry.AUTOID.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@GRNNO", request.ReqGRNEntry.GRNNO.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@DATE", request.ReqGRNEntry.DATE.Trim()));
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.ReqGRNEntry.USERCODE.Trim()));

                        SqlDataAdapter oda = new SqlDataAdapter(cmd);

                        oda.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                GRNNO = ds.Tables[1].Rows[0]["GRNNO"].ToString();

                                foreach (GRNEntryDetailEntity det in request.requestGRNEntrydetails)
                                {

                                    SqlCommand cmd1 = new SqlCommand("[Masters].[GRNENTRY_DEATILSINSERT]", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add(new SqlParameter("@GRNNO", GRNNO));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMCODE", det.ITEMCODE.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@ITEMNAME", det.ITEMNAME.Trim()));
                                    cmd1.Parameters.Add(new SqlParameter("@QUANTITY", det.QUANTITY));
                                    cmd1.Parameters.Add(new SqlParameter("@USERCODE", request.ReqGRNEntry.USERCODE.Trim()));
                                    SqlDataAdapter oda1 = new SqlDataAdapter(cmd1);
                                    oda1.Fill(ds);
                                    if (ds != null)
                                    {
                                        if (ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0].ToString() == "SUCCESS")
                                        {
                                            rowcount++;
                                        }
                                    }
                                }
                            }
                        }
                        if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS" && request.requestGRNEntrydetails.Count == rowcount)
                        {
                            totalcount++;
                        }
                        if (request.requestGRNEntrydetails.Count == rowcount)
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
                string responselog = createlog("BOMMasterUpdateDAL: " + "Method Name BOMMasterUpdateDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }

        public ResponseGRNEntry GRNEntryEditDtsDAL(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[MASTERS].[GRNENTRY_EDIT]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@AUTOID", request.ReqGRNEntry.AUTOID.Trim()));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.BomHeader = ds.Tables[0];
                            response.BomDetails = ds.Tables[1];
                            response.result = true;
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                string responsetime = DateTime.Now.ToString("yyyy MM dd hh:mm:ss.fff tt");
                string responselog = createlog("BOMMasterEditDtsDAL: " + "Method Name BOMMasterEditDtsDAL" + " -- " + ex.StackTrace + "-- " + ex.Source + " -- " + ex.Message);
                response.ErrorContainer.Add(new ErrorItem { DataItem = responselog, ErrorNo = "LWMS00000" });
                response.result = false;

            }


            return response;

        }
    }
}
