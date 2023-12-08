using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{

    public static class EmployerManagement
    {
        /// <summary>
        /// Function to authenticate credentials of UEEmployer
        /// </summary>
        /// <param name="_UserId">
        /// Userid of employer trying to log in to the system.
        /// </param>
        /// <param name="_Password">
        /// Password of the employer trying to log in to the system.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>User cannot be authenticated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Incorrect user-id</description>
        /// </item>
        /// <item>
        /// <term>-2</term>
        /// <description>Incorrect Password</description>
        /// </item> 
        /// <item>
        /// <term>1</term>
        /// <description>Authentic user.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int AuthenticateEmployer(string _UserId, string _Password)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>AuthenticateEmployer(" + _UserId + ", " + _Password + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AuthenticateUEClient";
                dbCommand.Parameters.AddWithValue("@UserId", _UserId);
                dbCommand.Parameters.AddWithValue("@Password", _Password);
                dbCommand.Parameters.AddWithValue("@ClientType", 1);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                iRetValue = (int)dtData.Rows[0][0];
                if (iRetValue < 1)
                {
                    string ExceptionMessage = string.Empty;
                    switch (iRetValue)
                    {
                        case -1:
                            iRetValue = 0;
                            ExceptionMessage = "Incorrect userid";
                            break;
                        case -2:
                            ExceptionMessage = "Incorrect Password";
                            break;
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", _UserId + " cannot login. Message: " + ExceptionMessage);
                }
                else
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", _UserId + " successfully loggedIn.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<AuthenticateEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Employer Facebook SignUp. Function is to be filed with data received from facebook server.
        /// </summary>
        /// <param name="FacebookProfile">
        /// Facebook ID
        /// </param>       
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Employer Facebook cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Employer Facebook cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Employer Facebook Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int EmployerfacebookSignIn(FacebookUserclass FacebookProfile, ref string ClientID, ref int ClientStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>EmployerfacebookSignIn(" + new JavaScriptSerializer().Serialize(FacebookProfile) + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_EmployerSignUp";
                dbCommand.Parameters.AddWithValue("@UEAuthenticationType", 3);
                dbCommand.Parameters.AddWithValue("@AuthenticationID", FacebookProfile.id);
                dbCommand.Parameters.AddWithValue("@AuthenticationName", FacebookProfile.name);
                dbCommand.Parameters.AddWithValue("@AuthenticationProfilePicURL", FacebookProfile.imageurl);
                dbCommand.Parameters.AddWithValue("@UEAdminID", FacebookProfile.id);
                dbCommand.Parameters.AddWithValue("@Password", DBNull.Value);
                dbCommand.Parameters.AddWithValue("@Email", FacebookProfile.email);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        ClientID = row["ClientId"].ToString();
                        ClientStatus = (int)row["ClientStatus"];
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerfacebookSignIn :: Successfully Signed Up.");
                iRetValue = 1;

                //Notification notification = new Notification();
                //notification.NotificationType = cNotificationType.AdminNotification;
                //notification.UEClientID = "-1";
                //notification.NotificationMessage = "New Employer sign up : " + FacebookProfile.name;
                //notification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                //NotificationManagement.AddNewNotification(ref notification);
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerfacebookSignIn ::" + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<EmployerfacebookSignIn ::" + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Employer Google SignUp. Function is to be filed with data received from facebook server.
        /// </summary>
        /// <param name="GoogleProfile">
        /// Google ID
        /// </param>       
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Employer Google cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Employer Google cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Employer Google Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int EmployerGoogleSignIn(GoogleUserclass GoogleProfile, ref string ClientID, ref int ClientStatus )
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>EmployerGoogleSignIn(" + new JavaScriptSerializer().Serialize(GoogleProfile) + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_EmployerSignUp";
                dbCommand.Parameters.AddWithValue("@UEAuthenticationType", 2);
                dbCommand.Parameters.AddWithValue("@AuthenticationID", GoogleProfile.id);
                dbCommand.Parameters.AddWithValue("@AuthenticationName", GoogleProfile.name);
                dbCommand.Parameters.AddWithValue("@AuthenticationProfilePicURL", GoogleProfile.picture);
                dbCommand.Parameters.AddWithValue("@UEAdminID", GoogleProfile.id);
                dbCommand.Parameters.AddWithValue("@Password", DBNull.Value);
                dbCommand.Parameters.AddWithValue("@Email", GoogleProfile.email);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if(dtData.Rows.Count>0)
                {
                    foreach(DataRow row in dtData.Rows)
                    {
                        ClientID = row["ClientId"].ToString();
                        ClientStatus = (int)row["ClientStatus"];
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerGoogleSignIn :: Successfully Signed Up.");
                iRetValue = 1;

                //Notification notification = new Notification();
                //notification.NotificationType = cNotificationType.AdminNotification;
                //notification.UEClientID = "-1";
                //notification.NotificationMessage = "New Employer sign up : " + GoogleProfile.name;
                //notification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                //NotificationManagement.AddNewNotification(ref notification);
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerGoogleSignIn ::" + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<EmployerGoogleSignIn ::" + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Employer Personal Profile SignUp. 
        /// </summary>
        /// <param name="PersonalProfile">
        /// Employer's Personal Profile 
        /// </param>       
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Employer Personal Profile  cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Employer Personal Profile  cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Employer Personal Profile  Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int EmployerPersonalProfileSignUp(PersonalProfileClass PersonalProfile, ref string ClientID, ref int ClientStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>EmployerPersonalProfileSignUp(" + new JavaScriptSerializer().Serialize(PersonalProfile) + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_EmployerSignUp";
                dbCommand.Parameters.AddWithValue("@UEAuthenticationType", 1);
                dbCommand.Parameters.AddWithValue("@AuthenticationID", PersonalProfile.email);
                dbCommand.Parameters.AddWithValue("@AuthenticationName", PersonalProfile.ProfileName);
                dbCommand.Parameters.AddWithValue("@AuthenticationProfilePicURL", DBNull.Value);
                dbCommand.Parameters.AddWithValue("@UEAdminID", PersonalProfile.email);
                dbCommand.Parameters.AddWithValue("@Password", PersonalProfile.Password);
                dbCommand.Parameters.AddWithValue("@Email", PersonalProfile.email);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        ClientID = row["ClientId"].ToString();
                        ClientStatus = (int)row["ClientStatus"];
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerPersonalProfileSignUp :: Successfully Signed Up.");
                iRetValue = 1;

                //Notification notification = new Notification();
                //notification.NotificationType = cNotificationType.AdminNotification;
                //notification.UEClientID = "-1";
                //notification.NotificationMessage = "New Employer sign up : " + PersonalProfile.ProfileName;
                //notification.hyperlink = URLs.EmployerDetailsURL + ClientID;
                //NotificationManagement.AddNewNotification(ref notification);

            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "EmployerPersonalProfileSignUp ::" + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<EmployerPersonalProfileSignUp ::" + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get UE Client ID from Authenticator ID received by Google or Facebook Authentication services.
        /// </summary>
        /// <param name="AuthenticatorID">
        /// Authentication ID received from LDAP services
        /// </param>
        /// <param name="UEClientID">
        /// Fetched Client ID will be stored in UEClientID string.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Client ID cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Client ID cannot be fetched due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Client ID fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetUEClientIdByAuthenticatorId(string AuthenticatorID, ref string UEClientID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetUEClientIdByAuthenticatorId("+AuthenticatorID+")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "select ClientID from UEClient where AuthenticationID = @AuthenticationID and ClientTypeID=1 ";
                dbCommand.Parameters.AddWithValue("@AuthenticationID", AuthenticatorID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        UEClientID = row["ClientId"].ToString();
                        iRetValue = 1;
                        logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetUEClientIdByAuthenticatorId :: Successfully Retreived ClientID.");
                    }
                }
                else
                {
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetUEClientIdByAuthenticatorId :: Failed to Retreived ClientID. Incorrect AuthenticationID");
                    iRetValue = 0;
                }


            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetUEClientIdByAuthenticatorId :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetUEClientIdByAuthenticatorId :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update employer details.
        /// </summary>
        /// <param name="EmployerDetails">
        /// Details of the employer, which is to be updated.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>employer details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>employer details cannot be updated due to mismatch in EmployerId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>employer details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateEmployer(Employer EmployerDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>UpdateEmployer(" + new JavaScriptSerializer().Serialize(EmployerDetails) + ")");
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
                dbCommand.CommandText = "sp_UpdateEmployerDetails";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerDetails.EmployerID);
                dbCommand.Parameters.AddWithValue("@Name", EmployerDetails.Name);
                dbCommand.Parameters.AddWithValue("@BusinessName", EmployerDetails.BusinessName);
                dbCommand.Parameters.AddWithValue("@Address", EmployerDetails.address);
                dbCommand.Parameters.AddWithValue("@Email", EmployerDetails.email);
                dbCommand.Parameters.AddWithValue("@WhatsAppNumber", EmployerDetails.WhatsAppNumber);
                dbCommand.Parameters.AddWithValue("@LocationStateCode", EmployerDetails.LocationStateCode);
                dbCommand.Parameters.AddWithValue("@LocationCityCode", EmployerDetails.LocationCityCode);
                dbCommand.Parameters.AddWithValue("@VerifyEmail", EmployerDetails.VerifyEmail);

                if (EmployerDetails.TempImageURL != string.Empty && EmployerDetails.TempImageURL != null)
                {

                    string BusinessLogoFilePath = HttpContext.Current.Server.MapPath(@".\TempFiles\FTP\" + EmployerDetails.TempImageURL);
                    if (File.Exists(BusinessLogoFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(BusinessLogoFilePath);
                        byte[] data = new byte[fileInfo.Length];
                        using (FileStream fs = fileInfo.OpenRead())
                        {
                            fs.Read(data, 0, data.Length);
                        }
                        dbCommand.Parameters.AddWithValue("@BusinessLogo", data);
                        fileInfo.Delete();
                    }
                }
                else
                    dbCommand.Parameters.AddWithValue("@BusinessLogo", System.Data.SqlTypes.SqlBinary.Null);
                //if (EmployerDetails.BusinessLogo != null)
                //    dbCommand.Parameters.AddWithValue("@BusinessLogo", EmployerDetails.BusinessLogo);
                //else
                //    dbCommand.Parameters.AddWithValue("@BusinessLogo", System.Data.SqlTypes.SqlBinary.Null);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {


                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];

                        Employer emp = new Employer();
                        EmployerManagement.GetEmployerByID(EmployerDetails.EmployerID, ref emp);

                        if(emp.EmployerStatus == EmployerStatus.RegistrationComplete.ToString())
                        {
                            PaymentTypeDetails paymentTypeDetails = new PaymentTypeDetails();
                            paymentTypeDetails.PaymentType = cPaymentType.EmployerRegistrationFee;
                            PaymentManagement.GetPaymentTypeDetails(ref paymentTypeDetails);
                            Payment payment = new Payment();
                            payment.PaymentType = cPaymentType.EmployerRegistrationFee;
                            payment.Currency = paymentTypeDetails.Currency;
                            payment.Amount = paymentTypeDetails.Amount;
                            payment.TaxAmount = (paymentTypeDetails.Amount * paymentTypeDetails.Tax) / 100;
                            payment.UEClientID = emp.EmployerID;
                            payment.DueDate = System.DateTime.Now;
                            payment.Reserve1 = string.Empty;
                            payment.Reserve2 = string.Empty;
                            PaymentManagement.InsertDuePayment(ref payment);

                            Notification notification = new Notification();
                            notification.NotificationType = cNotificationType.PersonalisedNotification;
                            notification.NotificationMessage = "Your are requested to pay Registration fee as per the policy after completion of profile. Please go to Payment section to check the due payment.";
                            notification.UEClientID = emp.EmployerID;
                            notification.hyperlink = "#";
                            NotificationManagement.AddNewNotification(ref notification);

                            Notification Adminnotification = new Notification();
                            Adminnotification.NotificationType = cNotificationType.AdminNotification;
                            Adminnotification.NotificationMessage = "New Employer Registered: " + EmployerDetails.BusinessName;
                            Adminnotification.UEClientID = "-1";
                            Adminnotification.hyperlink = URLs.EmployerDetailsURL+EmployerDetails.EmployerID;
                            NotificationManagement.AddNewNotification(ref Adminnotification);

                        }
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "UpdateEmployer :: Employer Details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "UpdateEmployer :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<UpdateEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int DeleteEmployer(Employer EmployerDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>DeleteEmployer(" + new JavaScriptSerializer().Serialize(EmployerDetails) + ")");
            int iRetValue = 0;
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<DeleteEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int DeleteEmployer(string _EmployerId)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>DeleteEmployer(_EmployerId" + _EmployerId.ToString() + ")");
            int iRetValue = 0;
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<DeleteEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Employer details based on Client Id.
        /// </summary>
        /// <param name="EmployerId">
        /// Client Id of employer whose information is required. 
        /// </param>
        /// <param name="EmployerData">
        /// Employer fetched based on passed Client ID / Employer ID will be stored in Employer Variable. 
        /// </param>/// 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Employer data cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Employer data cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Employer fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetEmployerByID(string EmployerId, ref Employer EmployerData, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetEmployerByID(" + EmployerId + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dsData = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetEmployerDetails";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerId);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dsData);

                if (dsData.Tables[0].Rows.Count > 0)                                    // Replace Tables[1] to Tables[0] by Krishna       
                {
                    foreach (DataRow row in dsData.Tables[0].Rows)                         // Replace Tables[1] to Tables[0] by Krishna
                    {
                        EmployerData.EmployerID = row["EmployerID"].ToString();
                        EmployerData.Name = row["Name"].ToString();
                        EmployerData.BusinessName = row["BusinessName"].ToString();
                        EmployerData.address = row["Address"].ToString();
                        EmployerData.email = row["Email"].ToString();
                        EmployerData.WhatsAppNumber = row["WhatsAppNumber"].ToString();
                        EmployerData.LocationStateCode = row["LocationStateCode"].ToString();
                        EmployerData.EmployerStatus = row["EmployerStatus"].ToString();             //Edited by Durgesh
                        EmployerData.LocationCityCode = row["LocationCityCode"].ToString();
                        //if (row["RegisteredOn"] != null)
                        //    EmployerData.RegisteredOn = Convert.ToDateTime(row["RegisteredOn"]);
                        //    //EmployerData.RegisteredOn = (DateTime)row["RegisteredOn"];
                        //if (row["UpdateOn"] != null)
                        //    EmployerData.UpdateOn = Convert.ToDateTime(row["UpdateOn"]);

                        if (row["BusinessLogo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                EmployerData.BusinessLogo = (byte[])row["BusinessLogo"];
                            string TFileURL = string.Empty, TFileName = "EmployerBusinessLogo_" + EmployerData.EmployerID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["BusinessLogo"], TFileName, ref TFileURL);
                            EmployerData.TempImageURL = TFileURL;
                        }
                        if (row["RegisteredOn"] != DBNull.Value)
                            EmployerData.RegisteredOn = (DateTime)row["RegisteredOn"];

                        if (row["UpdateOn"] != DBNull.Value)
                            EmployerData.UpdateOn = (DateTime)row["UpdateOn"];

                        EmployerData.VerifyEmail = row["VerifyEmail"].ToString();

                        iRetValue = 1;
                        logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerByID :: Employer Details fetched successfully.");
                    }
                }
                else
                {
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerByID :: Employer Details not found. Incorrect EmployerID");
                    iRetValue = 0;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerByID :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetEmployerByID :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetEmployerList(ref List<Employer> EmployerList, int EmployerStatus = -1)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetEmployerList(ref List<Employer> EmployerList, EmployerStatus = "+ EmployerStatus + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dsData = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetEmployerList";
                dbCommand.Parameters.AddWithValue("@EmployerStatus", EmployerStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dsData);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsData.Tables[0].Rows)
                    {
                        Employer EmployerData = new Employer();

                        EmployerData.EmployerID = row["EmployerID"].ToString();
                        EmployerData.Name = row["Name"].ToString();
                        EmployerData.BusinessName = row["BusinessName"].ToString();
                        EmployerData.address = row["Address"].ToString();
                        EmployerData.email = row["Email"].ToString();
                        EmployerData.WhatsAppNumber = row["WhatsAppNumber"].ToString();
                        EmployerData.LocationStateCode = row["LocationStateCode"].ToString();
                        EmployerData.LocationCityCode = row["LocationCityCode"].ToString();
                        if (row["RegisteredOn"] != DBNull.Value)
                            EmployerData.RegisteredOn = Convert.ToDateTime(row["RegisteredOn"]);
                        if (row["UpdateOn"] != DBNull.Value)
                            EmployerData.UpdateOn = Convert.ToDateTime(row["UpdateOn"]);

                        if (row["BusinessLogo"] != DBNull.Value)
                            EmployerData.BusinessLogo = (byte[])row["BusinessLogo"];

                        EmployerData.VerifyEmail = row["VerifyEmail"].ToString();

                        EmployerList.Add(EmployerData);     //Edited by Durgesh

                        iRetValue = 1;
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerList :: data : " + new JavaScriptSerializer().Serialize(EmployerData));
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerList :: Employer Details fetched successfully.");
                }
                else
                {
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerList :: Employer Details not found. Incorrect EmployerID");
                    iRetValue = 0;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetEmployerByID :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetEmployerListByName(string _EmployerName, ref List<Employer> EmployerList, int EmployerStatus = -1)
        {
            int iRetValue = 0;
            return iRetValue;
        }
        public static int GetEmployerListByLocation(ref List<Employer> EmployerList, string _EmployerLocation = "all", int EmployerStatus = -1)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetEmployerListByLocation(ref List<Employer> EmployerList, _EmployerLocation = " + _EmployerLocation + ",EmployerStatus = " + EmployerStatus + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dsData = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetEmployerListByLocation";
                dbCommand.Parameters.AddWithValue("@EmployerStatus", EmployerStatus);
                dbCommand.Parameters.AddWithValue("@BusinessLocation", _EmployerLocation);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dsData);

                if (dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsData.Tables[0].Rows)
                    {
                        Employer EmployerData = new Employer();

                        EmployerData.EmployerID = row["EmployerID"].ToString();
                        EmployerData.Name = row["Name"].ToString();
                        EmployerData.BusinessName = row["BusinessName"].ToString();
                        EmployerData.address = row["Address"].ToString();
                        EmployerData.email = row["Email"].ToString();
                        EmployerData.WhatsAppNumber = row["WhatsAppNumber"].ToString();
                        EmployerData.LocationStateCode = row["LocationStateCode"].ToString();
                        EmployerData.LocationCityCode = row["LocationCityCode"].ToString();
                        EmployerData.EmployerStatus = row["EmployerStatus"].ToString();      //Edited by Durgesh

                        //if(row["RegisteredOn"] != null)
                        //    EmployerData.RegisteredOn = Convert.ToDateTime(row["RegisteredOn"]);

                        if (row["RegisteredOn"] != DBNull.Value)
                            EmployerData.RegisteredOn = (DateTime)row["RegisteredOn"];

                        if (row["UpdateOn"] != DBNull.Value)
                            EmployerData.UpdateOn = (DateTime)row["UpdateOn"];

                        //if (row["UpdateOn"] != null)
                        //    EmployerData.UpdateOn = Convert.ToDateTime(row["UpdateOn"]);

                        if (row["BusinessLogo"] != DBNull.Value)
                            EmployerData.BusinessLogo = (byte[])row["BusinessLogo"];
                        EmployerData.VerifyEmail = row["VerifyEmail"].ToString();


                        EmployerList.Add(EmployerData);
                        iRetValue = 1;
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerListByLocation :: data : " + new JavaScriptSerializer().Serialize(EmployerData));
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerListByLocation :: Employer Details fetched successfully.");
                }
                else
                {
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerListByLocation :: Employer Details not found. Incorrect EmployerID");
                    iRetValue = 0;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerListByLocation :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetEmployerListByLocation :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Employer Dashboard Data.
        /// </summary>
        /// <param name="DashboardJSON">
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
        /// <term>0</term>
        /// <description>Dashboard data cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int GetEmployerDashboardData(ref string DashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetEmployerDashboardData()");
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
                dbCommand.CommandText = "sp_GetEmployerDashboard";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if(dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("EmployerOnBoarded", (int)row["EmployerOnBoarded"]);
                        DashboardData.Add("ActiveEmployersWithOpenVacancy", (int)row["ActiveEmployersWithOpenVacancy"]);
                        DashboardData.Add("InactiveEmployerWithFilledVacacny", (int)row["InactiveEmployerWithFilledVacacny"]);
                        DashboardData.Add("EmployersWithNoVacancy", (int)row["EmployersWithNoVacancy"]);
                        DashboardData.Add("EmployersInProcessVacancies", (int)row["EmployersInProcessVacancies"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerDashboardData :: Successfully fetched Employer Dashboard Data.");
                    }
                }
                else
                {
                    Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                    DashboardData.Add("EmployerOnBoarded", 0);
                    DashboardData.Add("ActiveEmployersWithOpenVacancy", 0);
                    DashboardData.Add("InactiveEmployerWithFilledVacacny", 0);
                    DashboardData.Add("EmployersWithNoVacancy", 0);
                    DashboardData.Add("EmployersInProcessVacancies", 0);

                    DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetEmployerDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Employer Mobile Dashboard Data.
        /// </summary>
        /// <param name="DashboardJSON">
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
        /// <term>0</term>
        /// <description>Dashboard data cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int GetEmployerMobileDashboardData(string EmployerID, ref string DashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetEmployerMobileDashboardData()");
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
                dbCommand.CommandText = "sp_GetEmployerMobileAppDashboard";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalVacancies", (int)row["TotalVacancies"]);
                        DashboardData.Add("OpenVacancies", (int)row["OpenVacancies"]);
                        DashboardData.Add("ScheduleInterview", (int)row["ScheduleInterview"]);
                        DashboardData.Add("ApplicationReceived", (int)row["ApplicationReceived"]);
                        DashboardData.Add("ActionPending", (int)row["ActionPending"]);
                        DashboardData.Add("TotalHired", (int)row["TotalHired"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerMobileDashboardData :: Successfully fetched Employer Dashboard Data.");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetEmployerMobileDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetEmployerMobileDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to Masked Profile List of Candidate.
        /// </summary>
        /// <param name="VacancyID">
        /// VacancyID id, whose list is required
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>NILL</term>
        /// <description>Candidate Masked details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>sRetValue</term>
        /// <description>Candidate Masked profile.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetMaskedInterviewListByEmployerId(string EmployerID, ref List<cMaskedInterviewDetails> maskedInterviewLists, int InterviewStatus = 0)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetMaskedInterviewListByEmployerId(" + EmployerID + ", ref Candidate CandidateDetail)");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetMaskedInterviewListByEmployerId";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerID);
                dbCommand.Parameters.AddWithValue("@EInterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cMaskedInterviewDetails maskedInterviewDetails = new cMaskedInterviewDetails();
                        maskedInterviewDetails.VacancyName = row["VacancyName"].ToString();
                        maskedInterviewDetails.JobApplicationID = (int)row["JobApplicationID"];
                        maskedInterviewDetails.CandidateID = (int)row["CandidateId"];
                        maskedInterviewDetails.Age = row["Age"].ToString();
                        maskedInterviewDetails.Gender = row["Gender"].ToString();

                        if (row["Photo"] != DBNull.Value)
                            maskedInterviewDetails.Photo = (byte[])row["Photo"];
                        
                        maskedInterviewDetails.CandidateLocation = row["CandidateLocation"].ToString();
                        maskedInterviewDetails.BriefProfile = row["BriefProfile"].ToString();
                        maskedInterviewDetails.InterviewID = row["InterviewID"].ToString();

                        if(row["InterviewScheduleDate"] != DBNull.Value)
                            maskedInterviewDetails.InterviewScheduleDate = Convert.ToDateTime(row["InterviewScheduleDate"]);
                        
                        maskedInterviewDetails.InteviewTimeZone = row["InteviewTimeZone"].ToString();
                        maskedInterviewDetails.ZoomMeetingId = row["ZoomMeetingId"].ToString();
                        maskedInterviewDetails.ZoomStartUrl = row["ZoomStartUrl"].ToString();
                        maskedInterviewDetails.ZoomMeetingStatus = row["ZoomMeetingStatus"].ToString();

                        maskedInterviewLists.Add(maskedInterviewDetails);

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByEmployerId :: Data : " + new JavaScriptSerializer().Serialize(maskedInterviewDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByEmployerId :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByEmployerId :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetMaskedInterviewListByEmployerId :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int UpdateEmployerStatus(string EmployerId, int EmployerStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateEmployerStatus(" + EmployerId + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [dbo].[UEEmployer] SET [EmployerStatus] = @EmployerStatus WHERE EmployerID = @EmployerId";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerId);
                dbCommand.Parameters.AddWithValue("@EmployerStatus", EmployerStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;

                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateEmployerStatus :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateEmployerStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateEmployerStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int VerifyEmployerEmail(string EmployerId, int EmailStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>VerifyEmployerEmail(" + EmployerId + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [UEEmployer] SET [VerifyEmail] = @VerifyEmail WHERE EmployerID = @EmployerId";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerId);
                dbCommand.Parameters.AddWithValue("@VerifyEmail", EmailStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;

                logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "VerifyEmployerEmail :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "VerifyEmployerEmail :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<VerifyEmployerEmail :: " + iRetValue.ToString());
            return iRetValue;
        }
    }
}
