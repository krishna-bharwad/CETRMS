using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CETRMS
{
    public static class NotificationManagement
    {
        /// <summary>
        /// Add New Notification as Unread into system. Required values are  notification.NotificationType, notification.NotificationMessage, notification.CETClientID, notification.hyperlink.
        /// </summary>
        /// <param name="notification">
        /// Object of type notification, which will be inserted into database.
        /// </param>/// 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>New Notification cannot be inserted due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>JNew Notification cannot be inserted due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>New Notification inserted successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddNewNotification(ref Notification notification)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", ">>>AddNewNotification()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_InsertNewNotification";
                dbCommand.Parameters.AddWithValue("@NotificationType", notification.NotificationType);
                dbCommand.Parameters.AddWithValue("@NotificationMessage", notification.NotificationMessage);
                dbCommand.Parameters.AddWithValue("@CETClientID", notification.CETClientID);
                dbCommand.Parameters.AddWithValue("@Hyperlink", notification.hyperlink);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        notification.NotificationID = row["NotificationID"].ToString();
                        notification.NotificationStatus = 1;
                        logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "AddNewNotification :: Successfully inserted notification Data.");
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "AddNewNotification :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "<<<AddNewNotification :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Get Notification Details.
        /// </summary>
        /// <param name="NotificationId">
        /// Notification ID, whose details are required to be fetched.
        /// </param> 
        /// <param name="notification">
        /// Object of type notification, which will be inserted into database.
        /// </param> 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Notification details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Notification details cannot be fetched due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Notification detaisl fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetNotificationDetails(string NotificationId, ref Notification notification)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", ">>>GetNotificationDetails("+ NotificationId + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetNotificationDetails";
                dbCommand.Parameters.AddWithValue("@NotificationID", NotificationId);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        notification.NotificationID = row["NotificationID"].ToString();
                        notification.NotificationTime = Convert.ToDateTime(row["NotificationTime"]);
                        notification.NotificationMessage = row["NotificationMessage"].ToString();
                        notification.NotificationType = (int)row["NotificationType"];
                        notification.NotificationStatus = (int)row["NotificationStatus"];
                        notification.hyperlink = row["HyperLink"].ToString();
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationDetails :: Successfully fetched notification details");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "<<<GetNotificationDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Update Notication Status.
        /// </summary> 
        /// <param name="notification">
        /// Object of type notification, which will be updated into database. New Notification status should be marked in notificaion object.
        /// </param> 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Notification details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Notification details cannot be updated due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Notification detaisl updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateNotificationStatus(Notification notification)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", ">>>UpdateNotificationStatus()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_UpdateNotificationStatus";
                dbCommand.Parameters.AddWithValue("@NotificationID", notification.NotificationID);
                dbCommand.Parameters.AddWithValue("@NotificationStatus", notification.NotificationStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "UpdateNotificationStatus :: Notification status updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "UpdateNotificationStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "<<<UpdateNotificationStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Update Notication Status to Read.
        /// </summary> 
        /// <param name="NotificationID">
        /// Notification ID, whose status is required to be updated to read.
        /// </param> 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Notification details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Notification details cannot be updated due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Notification detaisl updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int MarkNotificationRead(string NotificationID)
        {
            int iRetValue = 0;
            Notification notification = new Notification();
            notification.NotificationID = NotificationID;
            notification.NotificationStatus = cNotificationStatus.NotificationRead;
            iRetValue = NotificationManagement.UpdateNotificationStatus(notification);
            return iRetValue;
        }
        public static int GetNotificationList(ref List<Notification> notifications, string CETClientID, int NotificationType = -1, int NotificationStatus = -1, int NotificationCount = -1 )
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", ">>>GetNotificationList("+ CETClientID + "," + NotificationType + ","+ NotificationStatus + "," + NotificationCount + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                int nCount = 0;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetNotificationList";
                dbCommand.Parameters.AddWithValue("@CETClientID", CETClientID);
                dbCommand.Parameters.AddWithValue("@NotificationType", NotificationType);
                dbCommand.Parameters.AddWithValue("@NotificationStatus", NotificationStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    if (NotificationCount == -1) NotificationCount = dtData.Rows.Count;
                    foreach (DataRow row in dtData.Rows)
                    {
                        Notification notification = new Notification();
                        notification.NotificationID = row["NotificationID"].ToString();
                        notification.NotificationTime = Convert.ToDateTime(row["NotificationTime"]);
                        notification.NotificationMessage = row["NotificationMessage"].ToString();
                        notification.NotificationType  = Convert.ToInt32(row["NotificationType"]);
                        notification.NotificationStatus = Convert.ToInt32(row["NotificationStatus"]);
                        notification.hyperlink = row["HyperLink"].ToString();

                        nCount++;
                        if(NotificationCount >= nCount)
                            notifications.Add(notification);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationList :: Successfully fetched notification list");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "<<<GetNotificationList :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetNotificationStatistics(ref string NotificationSummaryJSON, string CETClientID = "-1", int NotificationType = -1)
        {
       
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", ">>>GetNotificationStatistics(" + CETClientID + ", " + NotificationType + ")");
            int iRetValue = 0;
            int iTemp;
            if(!Int32.TryParse(CETClientID,out iTemp))
            {
                logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationStatistics :: Incorrect id - " + CETClientID + ")");
                return iRetValue;
            }
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetNotificationSummary";
                dbCommand.Parameters.AddWithValue("@CETClientID", CETClientID);
                dbCommand.Parameters.AddWithValue("@NotificationType", NotificationType);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalNotification", (int)row["TotalNotification"]);
                        DashboardData.Add("NotificationDrafted", (int)row["NotificationDrafted"]);
                        DashboardData.Add("NotificationSent", (int)row["NotificationSent"]);
                        DashboardData.Add("NotificationUnread", (int)row["NotificationUnread"]);
                        DashboardData.Add("NotificationRead", (int)row["NotificationRead"]);
                        DashboardData.Add("NotificationDropped", (int)row["NotificationDropped"]);

                        NotificationSummaryJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationStatistics :: Successfully fetched notification statistics Data.");
                    }
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "GetNotificationStatistics :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", "<<<GetNotificationStatistics :: " + iRetValue.ToString());
            return iRetValue;
        }
    }
}