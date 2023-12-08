
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace CETRMS
{
    public static class logger
    {
        public enum LogSeverity { DBG = 0, INF, ERR };
        public enum LogEvents
        {
            UE_LOGIN = 1,
            EMPLOYER_MANAGEMENT = 2,
            CANDIDATE_MANAGEMENT = 3,
            INTERVIEW_MANAGEMENT = 4,
            VACANCY_MANAGEMENT = 5,
            LOCATION_MANAGEMENT = 6,
            EMPLOYER_WEBSERVICE = 7,
            CANIDATE_WEBSERVICE = 8,
            JOBAPPLICATION_MANAGEMENT = 9,
            PAYMENT_MANAGEMENT =10,
            NOTIFICATION_MANAGEMENT = 11,
            WEBPAGE = 12,
            TESTIMONIAL_MANAGEMENT = 13,
            EMPLOYER_MOBILEAPP = 14,
            CANDIDATE_MOBILEAPP = 15
        };

        private static Boolean isLogInitialized;
        private static SqlConnection dbConnection;
        private static int AllowedLogSeverity;
        public static bool initLog()
        {
            bool retVal = false;
            if (!isLogInitialized)
            {
                dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SyslogDB"].ConnectionString);
                AllowedLogSeverity = Convert.ToInt32(ConfigurationManager.AppSettings["LogSeverity"].ToString());
                isLogInitialized = true;
                retVal = true;
            }
            return retVal;

        }
        public static string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            string IPAddress = string.Empty;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
        public static bool log(LogSeverity severity, LogEvents eventId, string userName, string message)
        {
            if(severity == LogSeverity.ERR) ReportToEdgePMS(message);

            if (ConfigurationManager.AppSettings["EnableLogging"].ToString() != "1")
                return true;

            bool retVal = false;
            if (!isLogInitialized) initLog();

            int iSevValue = 0;
            switch (severity)
            {
                case LogSeverity.DBG: iSevValue = 0; break;
                case LogSeverity.INF: iSevValue = 1; break;
                case LogSeverity.ERR: iSevValue = 2; break;
            }

            if (iSevValue < AllowedLogSeverity) return false;
           

            string LogSevStr = "";

            if (severity == LogSeverity.DBG)
                LogSevStr = "DBG";
            else if (severity == LogSeverity.INF)
                LogSevStr = "INF";
            else if (severity == LogSeverity.ERR)
                LogSevStr = "ERR";

            System.IO.FileInfo oInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);

            string pagename = System.Web.HttpContext.Current.Request.Url.ToString();
            string clientIp = GetIPAddress();
            try
            {
                SqlCommand dbCommand = new SqlCommand();

                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "INSERT INTO log (EventId, ApplicationName, UserName, LogTime, LogSeverity, LogMessage, clientIp, pagename) values (@EventId, @ApplicationName, @UserName, @LogTime, @Severity, @LogMessage, @clientIp, @PageName);";
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddWithValue("@EventId", eventId);
                dbCommand.Parameters.AddWithValue("@UserName", userName);
                dbCommand.Parameters.AddWithValue("@LogTime", System.DateTime.Now); //2023-03-02 10:48:38.573
                dbCommand.Parameters.AddWithValue("@Severity", LogSevStr);
                dbCommand.Parameters.AddWithValue("@LogMessage", message);
                dbCommand.Parameters.AddWithValue("@clientIp", clientIp);
                dbCommand.Parameters.AddWithValue("@PageName", pagename);
                dbCommand.Parameters.AddWithValue("@ApplicationName", ConfigurationManager.AppSettings["ApplicationName"]);
                dbCommand.ExecuteNonQuery();
                dbCommand.Dispose();
                dbConnection.Close();
                retVal = true;
            }
            catch (Exception ex)
            {
                string messege = ex.Message;
                AddSystemFileLog("logger error:" + ex.Message);
                AddSystemFileLog("Missed log:" + message);
            }
            finally
            {
                dbConnection.Close();
            }
            return retVal;
        }

        public static int ReportToEdgePMS(string message)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                if (ConfigurationManager.AppSettings["EdgePMSSupport"] == "0")
                    return iRetValue;
                EdgePMS1.WebService1 service1 = new EdgePMS1.WebService1();
                service1.SendReportError(
                    ConfigurationManager.AppSettings["EdgePMSSupportCustomerID"],
                    ConfigurationManager.AppSettings["SoftwareVersion"],
                    message
                    );
                service1.SendReportErrorCompleted += Service1_SendReportErrorCompleted;
                iRetValue = RetValue.Success;
            }
            catch(Exception ex)
            {
                string error_message = ex.Message;
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }

        private static void Service1_SendReportErrorCompleted(object sender, EdgePMS1.SendReportErrorCompletedEventArgs e)
        {
            
        }
        public static bool AddSystemFileLog(string LogMessage)
        {
            bool bRetValue = false;
            string fileName = HttpContext.Current.Server.MapPath(@".\TempFiles\Log.txt");

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToString());
            sb.Append(" ");
            sb.Append(LogMessage);
            sb.Append("\r\n");

            // flush every 20 seconds as you do it
            File.AppendAllText(fileName, sb.ToString());
            sb.Clear();

            return bRetValue;
        }
    }
}