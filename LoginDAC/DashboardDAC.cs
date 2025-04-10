using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Transactions;
using LoginEntity;

namespace DAC
{
    public partial class WMSDAL
    {
        public ResponseDashboard DashboardPageLoadDAL(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[DASHBOARD].[SCREEN_PAGELOAD]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestDashboard.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.Activeemployee = ds.Tables[0];
                            response.InActiveemployee = ds.Tables[1];
                            response.TotalEmployee = ds.Tables[2];
                            response.Js_CHATCONTACTS = ds.Tables[3];
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
        public ResponseDashboard SendMessageDAL(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[dbo].[SEND_MESSAGE]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SENDER", request.requestDashboard.Sender));
                        cmd.Parameters.Add(new SqlParameter("@RECEIVER", request.requestDashboard.Receiver));
                        cmd.Parameters.Add(new SqlParameter("@CONTENT", request.requestDashboard.Content));
                        cmd.Parameters.Add(new SqlParameter("@TIMESTAMP", request.requestDashboard.Timestamp));

                        con.Open();
                        cmd.ExecuteNonQuery();

                        scope.Complete();
                        response.result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorContainer.Add(new ErrorItem { DataItem = ex.Message, ErrorNo = "CHAT00001" });
                response.result = false;
            }
            return response;
        }


        public class Message
        {
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public string Content { get; set; }
            public string Timestamp { get; set; }
        }

        public ResponseDashboard GetMessagesDAL(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[dbo].[GET_MESSAGES]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SENDER", request.requestDashboard.Sender));
                        cmd.Parameters.Add(new SqlParameter("@RECEIVER", request.requestDashboard.Receiver));

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Create a DataTable to hold the message data
                        DataTable messageTable = new DataTable();
                        messageTable.Columns.Add("Sender", typeof(string));
                        messageTable.Columns.Add("Receiver", typeof(string));
                        messageTable.Columns.Add("Content", typeof(string));
                        messageTable.Columns.Add("Timestamp", typeof(string));

                        // Loop through the SqlDataReader and add each row to the DataTable
                        while (reader.Read())
                        {
                            DataRow row = messageTable.NewRow();
                            row["Sender"] = reader["Sender"].ToString();
                            row["Receiver"] = reader["Receiver"].ToString();
                            row["Content"] = reader["Content"].ToString();
                            row["Timestamp"] = reader["Timestamp"].ToString();
                            messageTable.Rows.Add(row);
                        }

                        // Now, assign the DataTable to the response
                        response.messages = messageTable;  // Set the messages as DataTable
                        response.result = true;

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorContainer.Add(new ErrorItem { DataItem = ex.Message, ErrorNo = "CHAT00002" });
                response.result = false;
            }
            return response;
        }

        public ResponseDashboard MessageNotificationDAL(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            response.ErrorContainer = new List<ErrorItem>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("[DBO].[GETMESSAGE_NOTIFI]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@USERCODE", request.requestDashboard.USERCODE));
                        con.Open();
                        SqlDataAdapter oda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        if (ds != null)
                        {
                            response.NotifiMessages = ds.Tables[0]; 
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

    }
}
