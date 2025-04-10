using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAC
{
    public partial class WMSDAL
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["MRFTIMESHEET"].ConnectionString;

        public string createlog(string content)
        {
            string filepath = ConfigurationSettings.AppSettings["logpath"].ToString();
            string Errordescription = ConfigurationSettings.AppSettings["logdescription"].ToString();

            try
            {
                string CurrentMachineName = Environment.MachineName;
                content += "  --  CurrentMachineName : " + CurrentMachineName;
                string UniqueID = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

                System.IO.StreamWriter logWriter;
                DateTime currentTime = DateTime.Now;
                string logdate = "";
                logdate += currentTime.Year.ToString();

                if (int.Parse(currentTime.Month.ToString()) < 10)
                {
                    logdate += "0" + currentTime.Month.ToString();
                }
                else
                {
                    logdate += currentTime.Month.ToString();
                }

                if (int.Parse(currentTime.Day.ToString()) < 10)
                {
                    logdate += "0" + currentTime.Day.ToString();
                }
                else
                {
                    logdate += currentTime.Day.ToString();
                }

                //filepath after defined
                string logFile = filepath + "accesslogs_" + logdate + ".txt";

                if (System.IO.File.Exists(logFile))
                {
                    logWriter = System.IO.File.AppendText(logFile);
                }
                else
                {
                    logWriter = System.IO.File.CreateText(logFile);
                    logWriter.WriteLine("UniqueId,Page,IP,Date,Time,Param");
                }
                if (content != "")
                {
                    logWriter.WriteLine(UniqueID + "," + content);
                }
                logWriter.Close();
            }
            catch (Exception exp)
            {
                string Message = "ex.message :" + exp.Message + " -- Ex.Source :" + exp.Source + " -- e.starktrace : " + exp.StackTrace;
                String source = "Logger - accesslog";
                String log = "Application";
            }
            return Errordescription;
        }


    }
}
