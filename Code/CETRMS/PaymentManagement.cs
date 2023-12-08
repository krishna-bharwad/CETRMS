using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CETRMS
{
    public static class PaymentManagement
    {
        /// <summary>
        /// Function to get Payment Dashboard Data.
        /// </summary>
        /// <param name="PaymentDashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Dashboard data cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int GetPaymentDashboardData(ref string PaymentDashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentDashboardData()");
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
                dbCommand.CommandText = "sp_GetPaymentDashboard";
                dbCommand.Parameters.AddWithValue("@UEClientID", "-1");
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, string> DashboardData = new Dictionary<string, string>();
                        DashboardData.Add("TotalPaymentRequested", row["TotalPaymentRequested"].ToString());
                        DashboardData.Add("TotalPaymentReceived", row["TotalPaymentReceived"].ToString());
                        DashboardData.Add("TotalOutstandingPaymentWithinTimeLimit", row["TotalOutstandingPaymentWithinTimeLimit"].ToString());
                        DashboardData.Add("TotalOutstandingPaymentOutOfTimeLimit", row["TotalOutstandingPaymentOutOfTimeLimit"].ToString());
                        DashboardData.Add("TotalPaymentDueFinalization", row["TotalPaymentDueFinalization"].ToString());
                        DashboardData.Add("TotalPaymentNotificationGenerated", row["TotalPaymentNotificationGenerated"].ToString());
                        DashboardData.Add("TotalPaymentTransactionsDone", row["TotalPaymentTransactionsDone"].ToString());
                        DashboardData.Add("TotalPaymentTransactionsDue", row["TotalPaymentTransactionsDue"].ToString());
                        DashboardData.Add("TotalReferralDetailsTransfer", row["TotalReferralDetailsTransfer"].ToString());

                        PaymentDashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: Successfully fetched payment Dashboard Data.");
                    }
                }

                else
                {
                    Dictionary<string, string> DashboardData = new Dictionary<string, string>();
                    DashboardData.Add("TotalPaymentRequested", "0");
                    DashboardData.Add("TotalPaymentReceived", "0");
                    DashboardData.Add("TotalOutstandingPaymentWithinTimeLimit", "0");
                    DashboardData.Add("TotalOutstandingPaymentOutOfTimeLimit","0");
                    DashboardData.Add("TotalPaymentDueFinalization", "0");
                    DashboardData.Add("TotalPaymentNotificationGenerated", "0");
                    DashboardData.Add("TotalPaymentTransactionsDone", "0");
                    DashboardData.Add("TotalPaymentTransactionsDue","0");
                    DashboardData.Add("TotalReferralDetailsTransfer", "0");

                    PaymentDashboardJSON = JsonConvert.SerializeObject(DashboardData);

                    logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: Successfully fetched payment Dashboard Data.");
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Payment Dashboard Data for chosen client.
        /// </summary>
        /// <param name="ClientID">
        /// Client ID whose payment dashboard data is required
        /// </param>/// 
        /// <param name="PaymentDashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Dashboard data cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentDashboardData(string ClientID, ref string PaymentDashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentDashboardData(ClientID = "+ ClientID + ")");
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
                dbCommand.CommandText = "sp_GetPaymentDashboard";
                dbCommand.Parameters.AddWithValue("@UEClientID", ClientID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalPaymentRequested", (int)row["TotalPaymentRequested"]);
                        DashboardData.Add("TotalPaymentReceived", (int)row["TotalPaymentReceived"]);
                        DashboardData.Add("TotalOutstandingPaymentWithinTimeLimit", (int)row["TotalOutstandingPaymentWithinTimeLimit"]);
                        DashboardData.Add("TotalOutstandingPaymentOutOfTimeLimit", (int)row["TotalOutstandingPaymentOutOfTimeLimit"]);
                        DashboardData.Add("TotalPaymentDueFinalization", (int)row["TotalPaymentDueFinalization"]);
                        DashboardData.Add("TotalPaymentNotificationGenerated", (int)row["TotalPaymentNotificationGenerated"]);
                        DashboardData.Add("TotalPaymentTransactionsDone", (int)row["TotalPaymentTransactionsDone"]);
                        DashboardData.Add("TotalPaymentNotificationGenerated", (int)row["TotalPaymentNotificationGenerated"]);

                        PaymentDashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: Successfully fetched payment Dashboard Data.");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Payment Dashboard Data for selected period.
        /// </summary>
        /// <param name="FromPaymentDate">
        /// Starting date from which dashboard data is required.
        /// </param> 
        /// <param name="ToPaymentDate">
        /// Starting date till which dashboard data is required.
        /// </param> /// 
        /// <param name="PaymentDashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Dashboard data cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentDashboardData(DateTime FromPaymentDate, DateTime ToPaymentDate, ref string PaymentDashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentDashboardData(FromPaymentDate = " + FromPaymentDate + ", ToPaymentDate  = " + ToPaymentDate + ")");
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
                dbCommand.CommandText = "sp_GetPaymentDashboardByPeriod";
                dbCommand.Parameters.AddWithValue("@FromDate", FromPaymentDate);
                dbCommand.Parameters.AddWithValue("@ToDate", ToPaymentDate);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalPaymentRequested", (int)row["TotalPaymentRequested"]);
                        DashboardData.Add("TotalPaymentReceived", (int)row["TotalPaymentReceived"]);
                        DashboardData.Add("TotalOutstandingPaymentWithinTimeLimit", (int)row["TotalOutstandingPaymentWithinTimeLimit"]);
                        DashboardData.Add("TotalOutstandingPaymentOutOfTimeLimit", (int)row["TotalOutstandingPaymentOutOfTimeLimit"]);
                        DashboardData.Add("TotalPaymentDueFinalization", (int)row["TotalPaymentDueFinalization"]);
                        DashboardData.Add("TotalPaymentNotificationGenerated", (int)row["TotalPaymentNotificationGenerated"]);
                        DashboardData.Add("TotalPaymentTransactionsDone", (int)row["TotalPaymentTransactionsDone"]);
                        DashboardData.Add("TotalPaymentNotificationGenerated", (int)row["TotalPaymentNotificationGenerated"]);

                        PaymentDashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: Successfully fetched payment Dashboard Data.");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to insert due payment entry in database, on which notification will be generated.
        /// </summary>
        /// <param name="payment">
        /// object of payment class in from which payment details will be inserted into data.
        /// </param> 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>payment data cannot be inserted due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>payment data inserted successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int InsertDuePayment(ref Payment payment)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>InsertDuePayment()");
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
                dbCommand.CommandText = "sp_InsertDuePayment";
                dbCommand.Parameters.AddWithValue("@Currency", payment.Currency);
                dbCommand.Parameters.AddWithValue("@Amount", payment.Amount);
                dbCommand.Parameters.AddWithValue("@TaxAmount", payment.TaxAmount);
                dbCommand.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
                dbCommand.Parameters.AddWithValue("@UEClientID", payment.UEClientID);
                dbCommand.Parameters.AddWithValue("@DueDate", payment.DueDate);
                dbCommand.Parameters.AddWithValue("@Reserve1", payment.Reserve1);
                dbCommand.Parameters.AddWithValue("@Reserve2", payment.Reserve2);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        payment.PaymentOrderNo =  row["PaymentOrderNo"].ToString();
                        iRetValue = (int)row["IsFirstTimeEntry"];
                        logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "InsertDuePayment :: Successfully inserted payment Data.");
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "InsertDuePayment :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<InsertDuePayment :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update payment status in database and move UEClient status to next level
        /// </summary>
        /// <param name="payment">
        /// object of payment class in which payment details will be filled.
        /// </param> 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdatePaymentStatus(Payment payment)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>UpdatePaymentStatus()");
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
                dbCommand.CommandText = "sp_UpdatePaymentStatus";
                dbCommand.Parameters.AddWithValue("@PaymentRecID", payment.PaymentRecID);
                dbCommand.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);
                dbCommand.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                dbCommand.Parameters.AddWithValue("@StripePaymentID", payment.Stripe.StripePaymentID);
                dbCommand.Parameters.AddWithValue("@StripePaymentDate", payment.Stripe.StripePaymentDate);
                dbCommand.Parameters.AddWithValue("@StripePaymentStatus", payment.Stripe.StripePaymentStatus);
                dbCommand.Parameters.AddWithValue("@StripePaymentMessage", payment.Stripe.StripePaymentMessage);
                dbCommand.Parameters.AddWithValue("@StripePaymentReceiptURL", payment.Stripe.StripePaymentReceiptURL);
                dbCommand.Parameters.AddWithValue("@StripePaymentMethod", payment.Stripe.StripePaymentMethod);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;

                if(payment.PaymentType == cPaymentType.EmployerRegistrationFee && payment.PaymentStatus == cPaymentStatus.PaymentDone) 
                {
                    string ClientID = payment.UEClientID;
                    Employer employer = new Employer();
                    EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);

                    Notification AdminNotification = new Notification();
                    AdminNotification.NotificationType = cNotificationType.AdminNotification;
                    AdminNotification.UEClientID = "-1";
                    AdminNotification.NotificationMessage = "New Employer sign up : " + employer.BusinessName;
                    AdminNotification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                    NotificationManagement.AddNewNotification(ref AdminNotification);

                    Notification EmpNotification = new Notification();
                    EmpNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    EmpNotification.UEClientID = employer.EmployerID;
                    EmpNotification.NotificationMessage = "You payment for Registration Fee is successful. You can download invoice copy from Payments section of Mobile App.";
                    EmpNotification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref EmpNotification);
                }
                if (payment.PaymentType == cPaymentType.CandidateRegistrationFee && payment.PaymentStatus == cPaymentStatus.PaymentDone)
                {
                    string ClientID = payment.UEClientID;
                    Candidate candidate = new Candidate();
                    CandidateManagement.GetCandidatePersonalDetails(payment.UEClientID, ref candidate);

                    Notification AdminNotification = new Notification();
                    AdminNotification.NotificationType = cNotificationType.AdminNotification;
                    AdminNotification.UEClientID = "-1";
                    AdminNotification.NotificationMessage = "New Candidate sign up : " + candidate.PersonalProfile.Name;
                    AdminNotification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                    NotificationManagement.AddNewNotification(ref AdminNotification);

                    Notification CandNotification = new Notification();
                    CandNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    CandNotification.UEClientID = candidate.CandidateID;
                    CandNotification.NotificationMessage = "You payment for Registration Fee is successful. You can download invoice copy from Payments section of Mobile App.";
                    CandNotification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref CandNotification);
                }
                if (payment.PaymentType == cPaymentType.EmployerRecruitmentFee && payment.PaymentStatus == cPaymentStatus.PaymentDone)
                {
                    string ClientID = payment.UEClientID;
                    Employer employer = new Employer();
                    EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);

                    Notification AdminNotification = new Notification();
                    AdminNotification.NotificationType = cNotificationType.AdminNotification;
                    AdminNotification.UEClientID = "-1";
                    AdminNotification.NotificationMessage = "Employer Recruitment Fee paid by " + employer.BusinessName;
                    AdminNotification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                    NotificationManagement.AddNewNotification(ref AdminNotification);

                    Notification EmpNotification = new Notification();
                    EmpNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    EmpNotification.UEClientID = employer.EmployerID;
                    EmpNotification.NotificationMessage = "You payment for Employer Recruitment Fee is successful. You can download invoice copy from Payments section of Mobile App.";
                    EmpNotification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref EmpNotification);
                }
                if (payment.PaymentType == cPaymentType.CandidateRecruitmentFee && payment.PaymentStatus == cPaymentStatus.PaymentDone)
                {
                    string ClientID = payment.UEClientID;
                    Employer employer = new Employer();
                    EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);

                    Notification AdminNotification = new Notification();
                    AdminNotification.NotificationType = cNotificationType.AdminNotification;
                    AdminNotification.UEClientID = "-1";
                    AdminNotification.NotificationMessage = "Employer Recruitment Fee paid by " + employer.BusinessName;
                    AdminNotification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                    NotificationManagement.AddNewNotification(ref AdminNotification);

                    Notification EmpNotification = new Notification();
                    EmpNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    EmpNotification.UEClientID = employer.EmployerID;
                    EmpNotification.NotificationMessage = "You payment for Candidate Recruitment Fee is successful. You can download invoice copy from Payments section of Mobile App.";
                    EmpNotification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref EmpNotification);
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "UpdatePaymentStatus :: Payment status updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "UpdatePaymentStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<UpdatePaymentStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get payment details based on payment id
        /// </summary>
        /// <param name="PaymentID">
        /// Payment ID, whose payment details are required.
        /// </param> 
        /// <param name="payment">
        /// object of payment class in which payment details will be filled.
        /// </param>         
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentDetails(string PaymentID, ref Payment payment, bool ForWeb = false, bool GenerateReceipt = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentDetails(PaymentID = " + PaymentID + ")");
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
                dbCommand.CommandText = "sp_GetPaymentDetails";
                dbCommand.Parameters.AddWithValue("@PaymentRecID", PaymentID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        payment.PaymentRecID = row["PaymentRecID"].ToString();
                        payment.PaymentOrderNo = row["PaymentOrderNo"].ToString();
                        payment.Currency = row["Currency"].ToString();
                        payment.Amount = (double)row["Amount"];
                        payment.TaxAmount = (double)row["TaxAmount"];
                        payment.DueDate = Convert.ToDateTime(row["DueDate"]);
                        payment.PaymentType = (int)row["PaymentType"];
                        payment.TransactionType = row["TransactionType"].ToString();
                        payment.UEClientID = row["UEClientID"].ToString();
                        payment.PaymentStatus = (int)row["PaymentStatus"];
                        payment.NotificationID = (int)row["NotificationID"];
                        payment.NotificationType = (int)row["NotificationType"];
                        if (row["StripePaymentDate"] != DBNull.Value)
                            payment.Stripe.StripePaymentDate = (DateTime)row["StripePaymentDate"];
                        //payment.Stripe.StripePaymentID = (string)row["StripePaymentID"];
                        payment.Stripe.StripePaymentID = row["StripePaymentID"].ToString();
                        payment.Stripe.StripePaymentMessage = row["StripePaymentMessage"].ToString();
                        payment.Stripe.StripePaymentMethod = row["StripePaymentMethod"].ToString();
                        payment.Stripe.StripePaymentReceiptURL = row["StripePaymentReceiptURL"].ToString();
                        payment.Stripe.StripePaymentStatus = row["StripePaymentStatus"].ToString();
                        payment.InvoiceNo = row["InvoiceNo"].ToString();
                        payment.Reserve1 = row["Reserve1"].ToString();
                        payment.Reserve2 = row["Reserve2"].ToString();

                        if (payment.PaymentStatus == cPaymentStatus.PaymentDone && GenerateReceipt)
                        {
                            byte[] PaymentData = null;
                            ReceiptManagement.GetPaymentReceipt(payment.PaymentRecID, ref PaymentData);

                            if(PaymentData.Length != 0)
                            {
                                string FileURL = string.Empty;
                                common.ConverBinaryDataToTempFile(PaymentData, "Invoice_" + payment.PaymentOrderNo + ".pdf", ref FileURL);
                                payment.InvoiceURL = FileURL;
                            }
                        }
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDetails :: Successfully fetched payment Dashboard Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get payment details based on payment id
        /// </summary>
        /// <param name="PaymentID">
        /// Payment ID, whose payment details are required.
        /// </param> 
        /// <param name="payment">
        /// object of payment class in which payment details will be filled.
        /// </param>         
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentDetailsForReceipt(string PaymentID, ref Payment payment, bool ForWeb =false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentDetailsForReceipt(PaymentID = " + PaymentID + ")");
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
                dbCommand.CommandText = "sp_GetPaymentDetails";
                dbCommand.Parameters.AddWithValue("@PaymentRecID", PaymentID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        payment.PaymentRecID = row["PaymentRecID"].ToString();
                        payment.PaymentOrderNo = row["PaymentOrderNo"].ToString();
                        payment.Currency = row["Currency"].ToString();
                        payment.Amount = (double)row["Amount"];
                        payment.TaxAmount = (double)row["TaxAmount"];
                        payment.DueDate = Convert.ToDateTime(row["DueDate"]);
                        payment.PaymentType = (int)row["PaymentType"];
                        payment.TransactionType = row["TransactionType"].ToString();
                        payment.UEClientID = row["UEClientID"].ToString();
                        payment.PaymentStatus = (int)row["PaymentStatus"];
                        payment.NotificationID = (int)row["NotificationID"];
                        payment.NotificationType = (int)row["NotificationType"];
                        if (row["StripePaymentDate"] != DBNull.Value)
                            payment.Stripe.StripePaymentDate = (DateTime)row["StripePaymentDate"];
                        payment.Stripe.StripePaymentID = (string)row["StripePaymentID"];
                        payment.Stripe.StripePaymentMessage = row["StripePaymentMessage"].ToString();
                        payment.Stripe.StripePaymentMethod = row["StripePaymentMethod"].ToString();
                        payment.Stripe.StripePaymentReceiptURL = row["StripePaymentReceiptURL"].ToString();
                        payment.Stripe.StripePaymentStatus = row["StripePaymentStatus"].ToString();
                        payment.InvoiceNo = row["InvoiceNo"].ToString();
                        payment.Reserve1 = row["Reserve1"].ToString();
                        payment.Reserve2 = row["Reserve2"].ToString();
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentDetailsForReceipt :: Successfully fetched payment Dashboard Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentDetailsForReceipt :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get payment details list default for all clients, all payment types, all payment status types.
        /// </summary>
        /// <param name="payments">
        /// Object List payment in which list of payment details will be filled.
        /// </param> 
        /// <param name="UEClientID">
        /// Client whose payment details are required. (Default -1 : for all candidates)
        /// </param>         
        /// <param name="PaymentStatus">
        /// Payment Status type whose payment list is required. (Default -1 : for all payment status type)
        /// </param>   
        /// <param name="PaymentType">
        /// Payment type whose payment list is required. (Default -1 : for all payment type)
        /// </param>           
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment list data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment list data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentList(ref List<Payment> payments, string UEClientID = "-1", int PaymentStatus = -1, int PaymentType = -1, bool GenerateReceipt = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentList()");
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
                dbCommand.CommandText = "sp_GetPaymentList";
                dbCommand.Parameters.AddWithValue("@UEClientId", UEClientID);
                dbCommand.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                dbCommand.Parameters.AddWithValue("@PaymentType", PaymentType);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Payment payment = new Payment();
                        payment.PaymentRecID = row["PaymentRecID"].ToString();
                        payment.PaymentOrderNo = row["PaymentOrderNo"].ToString();
                        payment.Currency = row["Currency"].ToString();
                        payment.Amount = (double)row["Amount"];
                        payment.TaxAmount = (double)row["TaxAmount"];
                        payment.DueDate = Convert.ToDateTime(row["DueDate"]);
                        payment.PaymentType = (int)row["PaymentType"];
                        payment.TransactionType = row["TransactionType"].ToString();
                        payment.UEClientID = row["UEClientID"].ToString();
                        payment.PaymentStatus = (int)row["PaymentStatus"];
                        payment.NotificationID = (int)row["NotificationID"];
                        payment.NotificationType = (int)row["NotificationType"];
                        if (row["StripePaymentDate"] != DBNull.Value)
                            payment.Stripe.StripePaymentDate = (DateTime)row["StripePaymentDate"];
                        payment.Stripe.StripePaymentID = row["StripePaymentID"].ToString();
                        payment.Stripe.StripePaymentMessage = row["StripePaymentMessage"].ToString();
                        payment.Stripe.StripePaymentMethod = row["StripePaymentMethod"].ToString();
                        payment.Stripe.StripePaymentReceiptURL = row["StripePaymentReceiptURL"].ToString();
                        payment.Stripe.StripePaymentStatus = row["StripePaymentStatus"].ToString();
                        payment.InvoiceNo = row["InvoiceNo"].ToString();
                        payment.Reserve1 = row["Reserve1"].ToString();
                        payment.Reserve2 = row["Reserve2"].ToString();
                        if (payment.PaymentStatus == cPaymentStatus.PaymentDone && GenerateReceipt)
                        {
                            logger.log(logger.LogSeverity.DBG,logger.LogEvents.PAYMENT_MANAGEMENT,"","1");
                            byte[] PaymentData = null;
                            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "2");
                            ReceiptManagement.GetPaymentReceipt(payment.PaymentRecID, ref PaymentData);
                            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "3");

                            if (PaymentData.Length != 0)
                            {
                                logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "4: DataLength - "+PaymentData.Length.ToString());
                                string FileURL = string.Empty;
                                logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "5");
                                common.ConverBinaryDataToTempFile(PaymentData, "Invoice_" + payment.PaymentOrderNo + ".pdf", ref FileURL);
                                logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "6: fileurl - "+FileURL);
                                payment.InvoiceURL = FileURL;
                                logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "7");
                            }
                        }
                        payments.Add(payment);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentList :: Successfully fetched payment Dashboard Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentList :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get payment details list for selected period default for all clients, all payment types, all payment status types.
        /// </summary>
        /// <param name="payments">
        /// Object List payment in which list of payment details will be filled.
        /// </param> 
        /// <param name="UEClientID">
        /// Client whose payment details are required. (Default -1 : for all candidates)
        /// </param>         
        /// <param name="PaymentStatus">
        /// Payment Status type whose payment list is required. (Default -1 : for all payment status type)
        /// </param>   
        /// <param name="PaymentType">
        /// Payment type whose payment list is required. (Default -1 : for all payment type)
        /// </param>       
        /// <param name="FromPaymentDate">
        /// Started date from payment details are required.
        /// </param>
        /// <param name="ToPaymentDate">
        /// end date till which payment details are required.
        /// </param>        
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment list data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment list data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetPaymentList(ref List<Payment> payments, DateTime FromPaymentDate, DateTime ToPaymentDate, string UEClientID = "-1", int PaymentStatus = -1, int PaymentType = -1, bool GenerateReceipt = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentListByPeriod()");
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
                dbCommand.CommandText = "sp_GetPaymentListByPeriod";
                dbCommand.Parameters.AddWithValue("@UEClientId", UEClientID);
                dbCommand.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                dbCommand.Parameters.AddWithValue("@PaymentType", PaymentType);
                dbCommand.Parameters.AddWithValue("@FromDate", FromPaymentDate);
                dbCommand.Parameters.AddWithValue("@ToDate", ToPaymentDate);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Payment payment = new Payment();
                        payment.PaymentRecID = row["PaymentRecID"].ToString();
                        payment.PaymentOrderNo = row["PaymentOrderNo"].ToString();
                        payment.Currency = row["Currency"].ToString();
                        payment.Amount = (float)row["Amount"];
                        payment.TaxAmount = (float)row["TaxAmount"];
                        payment.DueDate = Convert.ToDateTime(row["DueDate"]);
                        payment.PaymentType = (int)row["PaymentType"];
                        payment.TransactionType = row["TransactionType"].ToString();
                        payment.UEClientID = row["UEClientID"].ToString();
                        payment.PaymentStatus = (int)row["PaymentStatus"];
                        payment.NotificationID = (int)row["NotificationID"];
                        payment.NotificationType = (int)row["NotificationType"];
                        if (row["StripePaymentDate"] != DBNull.Value)
                            payment.Stripe.StripePaymentDate = (DateTime)row["StripePaymentDate"];
                        payment.Stripe.StripePaymentID = (string)row["StripePaymentID"];
                        payment.Stripe.StripePaymentMessage = row["StripePaymentMessage"].ToString();
                        payment.Stripe.StripePaymentMethod = row["StripePaymentMethod"].ToString();
                        payment.Stripe.StripePaymentReceiptURL = row["StripePaymentReceiptURL"].ToString();
                        payment.Stripe.StripePaymentStatus = row["StripePaymentStatus"].ToString();
                        payment.InvoiceNo = row["InvoiceNo"].ToString();
                        if (payment.PaymentStatus == cPaymentStatus.PaymentDone && GenerateReceipt)
                        {
                            byte[] PaymentData = null;
                            ReceiptManagement.GetPaymentReceipt(payment.PaymentRecID, ref PaymentData);

                            if (PaymentData.Length != 0)
                            {
                                string FileURL = string.Empty;
                                common.ConverBinaryDataToTempFile(PaymentData, "Invoice_" + payment.PaymentOrderNo + ".pdf", ref FileURL);
                                payment.InvoiceURL = FileURL;
                            }
                        }
                        payments.Add(payment);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentListByPeriod :: Successfully fetched payment Dashboard Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentListByPeriod :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentListByPeriod :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to insert payment log
        /// </summary>
        /// <param name="paymentLog">
        /// Payment Log object to add
        /// </param>        
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Payment list data cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Payment list data updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddPaymentLog(PaymentLog paymentLog)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>AddPaymentLog()");
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
                dbCommand.CommandText = "sp_AddPaymentLog";
                dbCommand.Parameters.AddWithValue("@PaymentRecID", paymentLog.PaymentRecID);
                dbCommand.Parameters.AddWithValue("@LogDateTime", paymentLog.PaymentLogTime);
                dbCommand.Parameters.AddWithValue("@PaymentStatus", paymentLog.PaymentStatus);
                dbCommand.Parameters.AddWithValue("@PaymentLog", paymentLog.PaymentLogMessage);
                dbCommand.ExecuteNonQuery();
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "AddPaymentLog :: Successfully added payment log Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "AddPaymentLog :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<AddPaymentLog :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetPaymentTypeDetails(ref PaymentTypeDetails paymentTypeDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentTypeDetails()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                //dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "SELECT [PaymentTypeID],[Amount],[Tax],[AmountType],[DueDays],[Currency] FROM [dbo].[UEPaymentType] WHERE PaymentTypeID = @PaymentTypeID";
                dbCommand.Parameters.AddWithValue("@PaymentTypeID", paymentTypeDetails.PaymentType);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        paymentTypeDetails.PaymentType = (int)row["PaymentTypeID"];
                        paymentTypeDetails.CalculationMode = row["AmountType"].ToString();
                        paymentTypeDetails.Currency = row["Currency"].ToString();
                        paymentTypeDetails.Amount = (double)row["Amount"];
                        paymentTypeDetails.Tax = (double)row["Tax"];
                        paymentTypeDetails.DueDays = Convert.ToInt32(row["DueDays"]);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentTypeDetails :: Successfully fetched payment Dashboard Data.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentTypeDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentTypeDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetPaymentLogByClientId(string UEClientID, ref List<PaymentLog> PaymentLog)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentLog()");
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
                dbCommand.CommandText = "sp_GetPaymentLog";
                dbCommand.Parameters.AddWithValue("@UEClientID", UEClientID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        PaymentLog paymentlog = new PaymentLog();
                        paymentlog.LogId = row["LogId"].ToString();
                        paymentlog.PaymentRecID = row["PaymentRecID"].ToString();
                        paymentlog.PaymentLogTime = (DateTime)row["LogDateTime"];
                        paymentlog.PaymentLogMessage = row["PaymentLog"].ToString();
                        paymentlog.PaymentStatus = (int)row["PaymentStatus"];
                        PaymentLog.Add(paymentlog);
                    }
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentLog :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentLog :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetPaymentLogByRecId(string PaymentRecID, ref List<PaymentLog> PaymentLog)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentLog()");
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
                dbCommand.CommandText = "sp_GetPaymentLogByRecID";
                dbCommand.Parameters.AddWithValue("@PaymentRecID", PaymentRecID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        PaymentLog paymentlog = new PaymentLog();
                        paymentlog.LogId = row["LogId"].ToString();
                        paymentlog.PaymentRecID = row["PaymentRecID"].ToString();
                        paymentlog.PaymentLogTime = (DateTime)row["LogDateTime"];
                        paymentlog.PaymentLogMessage = row["PaymentLog"].ToString();
                        paymentlog.PaymentStatus = (int)row["PaymentStatus"];
                        PaymentLog.Add(paymentlog);
                    }
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentLog :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentLog :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetPaymentMasterData(ref DataTable PaymentMasterData, string TransactionType = "CR")
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);

            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "SELECT [PaymentTypeName],[Amount], [Tax], [AmountType],[DueDays],[Currency] FROM [UEPaymentType] where [TransactionType] = @TransactionType";
                dbCommand.Parameters.AddWithValue("@TransactionType", TransactionType);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(PaymentMasterData);
                if (PaymentMasterData.Rows.Count > 0)
                    iRetValue = 1;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentMasterData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int UpdatePaymentMaster(string sPaymentTypeName, string nAmount, string nTax, string sAmountType, string nDueDays, string sCurrency)
        {
            int iRetValue = RetValue.NoRecord;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);

            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [UEPaymentType]"+
                                        "SET [Amount] = @Amount"+
                                        ",[AmountType] = @AmountType"+
                                        ",[Tax] = @Tax " +
                                        ",[DueDays] = @DueDays" +
                                        ",[Currency] = @Currency "+
                                        "WHERE [PaymentTypeName] LIKE '%'+@PaymentTypeName+'%'";
                dbCommand.Parameters.AddWithValue("@PaymentTypeName", sPaymentTypeName);
                dbCommand.Parameters.AddWithValue("@Amount", nAmount);
                dbCommand.Parameters.AddWithValue("@Tax", nTax);
                dbCommand.Parameters.AddWithValue("@AmountType", sAmountType);
                dbCommand.Parameters.AddWithValue("@DueDays", nDueDays);
                dbCommand.Parameters.AddWithValue("@Currency", sCurrency);
                dbCommand.ExecuteNonQuery();
                iRetValue = RetValue.Success;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "UpdatePaymentMaster :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int UpdateAllPaymentStatus()
        {
            int iRetValue = RetValue.NoRecord;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);

            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_UpdatePaymentTable";
                dbCommand.ExecuteNonQuery();
                iRetValue = RetValue.Success;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "UpdateAllPaymentStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int ParsePaymentLog(string strPaymentLog, ref PaymentLogInfo paymentLogInfo)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                //dynamic paymentlogData = JsonConvert.DeserializeObject(strPaymentLog);
                string sObject = common.GetValue("object", strPaymentLog);
                paymentLogInfo.Amount = (Convert.ToDouble(common.GetValue(" amount", strPaymentLog))/100).ToString();
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                int iSeconds = Convert.ToInt32(common.GetValue(" created", strPaymentLog));
                origin = origin.AddSeconds(iSeconds);
                paymentLogInfo.CreatedAt = origin;
                switch (sObject)
                {
                    case "charge":
                        paymentLogInfo.status = common.GetValue(" status", strPaymentLog);
                        if (paymentLogInfo.status == "succeeded")
                        {
                            paymentLogInfo.id = common.GetValue(" balance_transaction", strPaymentLog);
                            string PaymentMethod = common.GetValue(" payment_method_details", strPaymentLog);
                            if(PaymentMethod.IndexOf("card:") == 0)
                            {
                                PaymentMethod = common.GetValue("funding", strPaymentLog);
                                PaymentMethod = PaymentMethod + "-card\r\n";
                                PaymentMethod = PaymentMethod + "Network: " + common.GetValue(" brand", strPaymentLog) + "\r\n";
                                PaymentMethod = PaymentMethod + "Card No.: ****"+common.GetValue(" last4", strPaymentLog) + "\r\n";
                            }
                            paymentLogInfo.payment_method = PaymentMethod;
                            paymentLogInfo.remarks = common.GetValue(" receipt_url", strPaymentLog);
                        }
                        else
                        {
                            paymentLogInfo.payment_method = string.Empty;
                            paymentLogInfo.remarks = common.GetValue(" failure_message", strPaymentLog);
                        }
                        break;
                    case "payment_intent":
                        paymentLogInfo.id = common.GetValue("id", strPaymentLog); ;
                        paymentLogInfo.status = "Payment Initiated.";
                        paymentLogInfo.payment_method = string.Empty;
                        paymentLogInfo.remarks = String.Empty;
                        break;
                }
                paymentLogInfo.StripePaymentLog = string.Empty;
                iRetValue = RetValue.Success;
            }
            catch(Exception ex)
            {
                iRetValue = RetValue.Error;
                string error = ex.Message;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            return iRetValue;
        }
        public static int GetPaymentLogInfoList(string PaymentRecId, ref List<PaymentLogInfo> paymentLogInfos)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                List<PaymentLog> paymentLogs = new List<PaymentLog>();
                GetPaymentLogByRecId(PaymentRecId, ref paymentLogs);
                if (paymentLogs.Count > 0)
                {
                    foreach (PaymentLog paymentLog in paymentLogs)
                    {
                        PaymentLogInfo paymentLogInfo = new PaymentLogInfo();
                        ParsePaymentLog(paymentLog.PaymentLogMessage, ref paymentLogInfo);
                        //paymentLogInfo.id = paymentLog.LogId;
                        paymentLogInfo.PaymentRecID = paymentLog.PaymentRecID;

                        paymentLogInfos.Add(paymentLogInfo);
                    }
                    iRetValue = RetValue.Success;
                }
            }
            catch(Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return iRetValue;
        }
        public static int GetPaymentLogInfo(string PaymentLogID, ref PaymentLogInfo paymentLogInfo)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);

                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "Select LogId, PaymentRecID,LogDateTime,PaymentStatus,PaymentLog from UEPaymentLog where LogId = @LogId";
                dbCommand.Parameters.AddWithValue("@LogId", PaymentLogID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        //PaymentLog paymentlog = new PaymentLog();
                        //paymentlog.LogId = row["LogId"].ToString();
                        //paymentlog.PaymentRecID = row["PaymentRecID"].ToString();
                        //paymentlog.PaymentLogTime = (DateTime)row["LogDateTime"];
                        //paymentlog.PaymentLogMessage = row["PaymentLog"].ToString();
                        //paymentlog.PaymentStatus = (int)row["PaymentStatus"];

                        ParsePaymentLog(row["PaymentLog"].ToString(), ref paymentLogInfo);
                        paymentLogInfo.id = row["LogId"].ToString();
                        paymentLogInfo.PaymentRecID = row["PaymentRecID"].ToString();
                        iRetValue = RetValue.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return iRetValue;
        }
    }
}