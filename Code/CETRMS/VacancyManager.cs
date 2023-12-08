using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{

    public static class VacancyManager
    {
        /// <summary>
        /// Function to get list of vacancies published by any employer
        /// </summary>
        /// <param name="EmployerID">
        /// Employer ID of the employer, whose vacacny list is required. '0: all' 
        /// </param>
        /// <param name="VacancyList">
        /// Reference variable to list of vacancy, which will be filled by fetching data from databasae.
        /// </param>
        /// <param name="VacancyStatus">
        /// Vacancy Status can be '0: all', '1: open', or '2: closed'
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Vacancy list cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Vacancy list fetched updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetVacancyListByEmployer(string EmployerID, ref List<Vacancy> VacancyList, int VacancyStatus = 0)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            SqlDataAdapter dbAdapter = new SqlDataAdapter();
            DataTable dtData = new DataTable();
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetVacancyListByEmployer";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerID);
                dbCommand.Parameters.AddWithValue("@VacancyStatus", VacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Vacancy VacancyItem = new Vacancy();
                        VacancyItem.VacancyID = row["VacancyID"].ToString();
                        VacancyItem.UEEmployerID = row["UEEmployerId"].ToString();
                        VacancyItem.VacancyStatusTypeID = row["VacancyStatusTypeId"].ToString();
                        VacancyItem.VacancyName = row["VacancyName"].ToString();
                        VacancyItem.VacancyCode = row["VacancyCode"].ToString();
                        VacancyItem.PrimaryLocation = row["PrimaryLocation"].ToString();
                        VacancyItem.JobType = row["JobType"].ToString(); 
                        VacancyItem.EmployementStatus = row["EmployementStatus"].ToString();
                        if (row["CandidatesRequired"] != DBNull.Value)
                        VacancyItem.CandidatesRequired = (int)row["CandidatesRequired"];
                        if (row["RequiredMinExp"] != DBNull.Value)
                            VacancyItem.RequiredMinExp = (int)row["RequiredMinExp"];
                        VacancyItem.RequiredMinQualification = row["RequiredMinQualification"].ToString();
                        if (row["PostingDate"] != DBNull.Value)
                            VacancyItem.PostingDate = (DateTime)row["PostingDate"];
                        VacancyItem.VacancyDetails = row["VacancyDetails"].ToString();
                        if (row["SalaryOffered"] != DBNull.Value)
                            VacancyItem.SalaryOffered = Convert.ToDouble(row["SalaryOffered"]);
                        VacancyItem.VacancyStatusTypeName = row["VacancyStatusTypeName"].ToString();
                        if(row["SalaryCycle"] != DBNull.Value)
                        VacancyItem.SalaryCycle =Convert.ToInt32(row["SalaryCycle"]);


                        VacancyItem.StateName = LocationManagement.GetStateDetail(VacancyItem.PrimaryLocation).StateName;
                        VacancyItem.CurrencySymbol = LocationManagement.GetStateDetail(VacancyItem.PrimaryLocation).Country.CurrencySymbol;

                        VacancyList.Add(VacancyItem);
                    }
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                iRetValue = -1;
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of vacancies published by any employer
        /// </summary>
        /// <param name="JobLocation">
        /// JobLocation of the vacancy, whose vacacny list is required. 'all' 
        /// </param>
        /// <param name="VacancyList">
        /// Reference variable to list of vacancy, which will be filled by fetching data from databasae.
        /// </param>
        /// <param name="VacancyStatus">
        /// Vacancy Status can be '0: all', '1: open', or '2: closed'
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Vacancy list cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Vacancy list fetched updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetVacancyListByLocation(string JobLocation, ref List<Vacancy> VacancyList, int VacancyStatus = 0)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            SqlDataAdapter dbAdapter = new SqlDataAdapter();
            DataTable dtData = new DataTable();
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetVacancyListByLocation";
                dbCommand.Parameters.AddWithValue("@JobLocation", JobLocation);
                dbCommand.Parameters.AddWithValue("@VacancyStatus", VacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Vacancy VacancyItem = new Vacancy();
                        VacancyItem.VacancyID = row["VacancyID"].ToString();
                        VacancyItem.UEEmployerID = row["UEEmployerId"].ToString();
                        VacancyItem.VacancyStatusTypeID = row["VacancyStatusTypeId"].ToString();
                        VacancyItem.VacancyName = row["VacancyName"].ToString();
                        VacancyItem.VacancyCode = row["VacancyCode"].ToString();
                        VacancyItem.PrimaryLocation = row["PrimaryLocation"].ToString();
                        VacancyItem.JobType = row["JobType"].ToString();

                        if (row["EmployementStatus"] != DBNull.Value)
                            VacancyItem.EmployementStatus = row["EmployementStatus"].ToString();

                      if(row["CandidatesRequired"] !=DBNull.Value)
                        VacancyItem.CandidatesRequired = (int)row["CandidatesRequired"];

                        if (row["RequiredMinExp"] != DBNull.Value)
                            VacancyItem.RequiredMinExp = (int)row["RequiredMinExp"];

                        VacancyItem.RequiredMinQualification = row["RequiredMinQualification"].ToString();

                        if (row["PostingDate"] != DBNull.Value)
                            VacancyItem.PostingDate = (DateTime)row["PostingDate"];

                        VacancyItem.VacancyDetails = row["VacancyDetails"].ToString();

                        if (row["SalaryOffered"] != DBNull.Value)
                            VacancyItem.SalaryOffered = (double)row["SalaryOffered"];

                        VacancyItem.VacancyStatusTypeName = row["VacancyStatusTypeName"].ToString();

                        VacancyItem.StateName = LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).StateName + "-"
                                                + LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).Country.CountryName;

                        VacancyItem.CurrencySymbol = row["CurrencySymbol"].ToString();

                        VacancyList.Add(VacancyItem);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get Vacancy Dashboard Data.
        /// </summary>
        /// <param name="DashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <param name="JobLocation">
        /// Location whose vacancy summary is required. Location code is to be passed. Default 'all : Gives summary of all location'.
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
        public static int GetVacancyDashboardData(ref string DashboardJSON, string JobLocation = "all")
        {
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
                dbCommand.CommandText = "sp_GetVacancyDashboard";
                dbCommand.Parameters.AddWithValue("@VacancyLocation", JobLocation);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalVacancies", (int)row["TotalVacancies"]);
                        DashboardData.Add("OpenVacancies", (int)row["OpenVacancies"]);
                        DashboardData.Add("VacanciesUnderScheduledInterview", (int)row["VacanciesUnderScheduledInterview"]);
                        DashboardData.Add("CloseVacanciesAfterFinalSelection", (int)row["CloseVacanciesAfterFinalSelection"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);
                    }

                }
                else
                {
                      Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalVacancies", 0);
                        DashboardData.Add("OpenVacancies", 0);
                        DashboardData.Add("VacanciesUnderScheduledInterview", 0);
                        DashboardData.Add("CloseVacanciesAfterFinalSelection", 0);

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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Add New Vacancy.
        /// </summary>
        /// <param name="newVacancyDetails">
        /// Vacancy class object contains data of about vacancy.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy cannot be added due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>-2</term>
        /// <description>Vacancy cannot be added due to mismatch in EmployerID.</description>
        /// </item>
        /// <item>
        /// <term>VacancyID</term>
        /// <description>Vacancy Added successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int AddVacancy(Vacancy newVacancyDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", ">>>AddVacancy(Vacancy - " + new JavaScriptSerializer().Serialize(newVacancyDetails) + ")");
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
                dbCommand.CommandText = "sp_AddVacancy";
                dbCommand.Parameters.AddWithValue("@UEEmployerId", newVacancyDetails.UEEmployerID);
                dbCommand.Parameters.AddWithValue("@VacancyName", newVacancyDetails.VacancyName);
                dbCommand.Parameters.AddWithValue("@PrimaryLocation", newVacancyDetails.PrimaryLocation);
                dbCommand.Parameters.AddWithValue("@JobType", newVacancyDetails.JobType);
                dbCommand.Parameters.AddWithValue("@EmployementStatus", newVacancyDetails.EmployementStatus);
                dbCommand.Parameters.AddWithValue("@CandidatesRequired", newVacancyDetails.CandidatesRequired);
                dbCommand.Parameters.AddWithValue("@RequiredMinExp", newVacancyDetails.RequiredMinExp);
                dbCommand.Parameters.AddWithValue("@RequiredMinQualification", newVacancyDetails.RequiredMinQualification);
                dbCommand.Parameters.AddWithValue("@VacancyDetails", newVacancyDetails.VacancyDetails);
                dbCommand.Parameters.AddWithValue("@SalaryOffered", newVacancyDetails.SalaryOffered);
                dbCommand.Parameters.AddWithValue("@SalaryCycle", newVacancyDetails.SalaryCycle);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                
                iRetValue = (int)dtData.Rows[0][0];
                logger.log(logger.LogSeverity.INF, logger.LogEvents.VACANCY_MANAGEMENT, "", "AddVacancy :: Vacancy added successfully");

                Notification notification = new Notification();
                Employer employer = new Employer();
                EmployerManagement.GetEmployerByID(newVacancyDetails.UEEmployerID, ref employer);
                notification.NotificationType = cNotificationType.AdminNotification;
                notification.NotificationMessage = "New Vacancy of "+newVacancyDetails.VacancyName+" Added By " + employer.BusinessName;
                notification.hyperlink = URLs.VacancyDetailsURL + iRetValue;
                notification.UEClientID = "-1";
                NotificationManagement.AddNewNotification(ref notification);
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", "AddVacancy :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", "<<<AddVacancy :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to Get Vacancy Details.
        /// </summary>
        /// <param name="VacancyID">
        /// Vacancy ID
        /// </param>
        /// <param name="VacancyDetails">
        /// Vacancy class object to read data of vacancy.
        /// </param>/// 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Vacancy cannot be fetched due to mismatch in VacancyID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Vacancy fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int GetVacancyDetails(string VacancyID, ref Vacancy VacancyDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", ">>>GetVacancyDetails(VacancyID = " + VacancyID + ", ref Vacancy VacancyDetails)");
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
                dbCommand.CommandText = "sp_GetVacancyDetails";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count >= 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        VacancyDetails.VacancyID = row["VacancyID"].ToString();
                        VacancyDetails.UEEmployerID = row["UEEmployerId"].ToString();
                        VacancyDetails.VacancyName = row["VacancyName"].ToString();
                        VacancyDetails.JobType = row["JobType"].ToString();
                        VacancyDetails.EmployementStatus = row["EmployementStatus"].ToString();
                        if (row["CandidatesRequired"] != DBNull.Value)
                            VacancyDetails.CandidatesRequired = (int)row["CandidatesRequired"];
                        else
                            VacancyDetails.CandidatesRequired = 0;
                        if (row["RequiredMinExp"] != DBNull.Value)
                            VacancyDetails.RequiredMinExp = (int)row["RequiredMinExp"];
                        else
                            VacancyDetails.RequiredMinExp = 0;
                        VacancyDetails.RequiredMinQualification = row["RequiredMinQualification"].ToString();
                        VacancyDetails.VacancyDetails = row["VacancyDetails"].ToString();
                        if (row["SalaryOffered"] != DBNull.Value)
                            VacancyDetails.SalaryOffered = (double)row["SalaryOffered"];
                        else
                            VacancyDetails.SalaryOffered = 0;
                        VacancyDetails.VacancyStatusTypeID = row["VacancyStatusTypeID"].ToString();
                        if (row["PostingDate"] != DBNull.Value)
                            VacancyDetails.PostingDate = (DateTime)row["PostingDate"];
                        else
                            VacancyDetails.PostingDate = System.DateTime.Now;
                        
                        VacancyDetails.PrimaryLocation = row["PrimaryLocation"].ToString();

                        if (row["SalaryCycle"] != DBNull.Value)
                            VacancyDetails.SalaryCycle = Convert.ToInt32(row["SalaryCycle"]);

                        VacancyDetails.StateName = LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).StateName + "-"
                                              + LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).Country.CountryName;

                        VacancyDetails.CurrencySymbol = LocationManagement.GetCountryDetail(row["PrimaryLocation"].ToString()).CurrencySymbol;


                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", "GetVacancyDetails :: data : " + new JavaScriptSerializer().Serialize(VacancyDetails));
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.VACANCY_MANAGEMENT, "", "GetVacancyDetails :: Vacancy details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", "GetVacancyDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", "<<<GetVacancyDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update Vacancy.
        /// </summary>
        /// <param name="VacancyDetails">
        /// Vacancy class object contains data of about vacancy.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>-2</term>
        /// <description>Vacancy cannot be updated due to mismatch in EmployerID.</description>
        /// </item>
        /// <item>
        /// <term>VacancyID</term>
        /// <description>Vacancy updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int UpdateVacancy(Vacancy VacancyDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", ">>>UpdateVacancy(VacancyDetails=" + new JavaScriptSerializer().Serialize(VacancyDetails) + ")");
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
                dbCommand.CommandText = "sp_UpdateVacancyDetails";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyDetails.VacancyID);
                dbCommand.Parameters.AddWithValue("@VacancyName", VacancyDetails.VacancyName);
                dbCommand.Parameters.AddWithValue("@PrimaryLocation", VacancyDetails.PrimaryLocation);
                dbCommand.Parameters.AddWithValue("@JobType", VacancyDetails.JobType);
                dbCommand.Parameters.AddWithValue("@EmployementStatus", VacancyDetails.EmployementStatus);
                dbCommand.Parameters.AddWithValue("@CandidatesRequired", VacancyDetails.CandidatesRequired);
                dbCommand.Parameters.AddWithValue("@RequiredMinExp", VacancyDetails.RequiredMinExp);
                dbCommand.Parameters.AddWithValue("@RequiredMinQualification", VacancyDetails.RequiredMinQualification);
                dbCommand.Parameters.AddWithValue("@VacancyDetails", VacancyDetails.VacancyDetails);
                dbCommand.Parameters.AddWithValue("@SalaryOffered", VacancyDetails.SalaryOffered);
                dbCommand.Parameters.AddWithValue("@SalaryCycle", VacancyDetails.SalaryCycle);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                iRetValue = (int)dtData.Rows[0][0];
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.VACANCY_MANAGEMENT, "", "UpdateVacancy :: Vacancy details updated successfully.");

                Notification notification = new Notification();
                Employer employer = new Employer();
                EmployerManagement.GetEmployerByID(VacancyDetails.UEEmployerID, ref employer);
                notification.NotificationType = cNotificationType.AdminNotification;
                notification.NotificationMessage = "Vacancy details of " + VacancyDetails.VacancyName + " updated By " + employer.BusinessName;
                notification.hyperlink = URLs.VacancyDetailsURL + VacancyDetails.VacancyID;
                notification.UEClientID = "-1";
                NotificationManagement.AddNewNotification(ref notification);

            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", "UpdateVacancy :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", "<<<UpdateVacancy :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update Vacancy Status.
        /// </summary>
        /// <param name="VacancyID">
        /// Vacancy Id.
        /// </param>
        /// <param name="iVacancyStatus">
        /// Vacancy Status 1: Open, 2: Filled, 3: Closed, 4: Paused, 5: InProcess.
        /// </param>/// 
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>-2</term>
        /// <description>Vacancy cannot be updated due to mismatch in EmployerID.</description>
        /// </item>
        /// <item>
        /// <term>VacancyID</term>
        /// <description>Vacancy updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int UpdateVacancyStatus(string VacancyID, int iVacancyStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", ">>>UpdateVacancyStatus(VacancyID=" + VacancyID + ", iVacancyStatus=" + iVacancyStatus + ")");
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
                dbCommand.CommandText = "sp_UpdateVacancyStatus";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyID);
                dbCommand.Parameters.AddWithValue("@VacancyStatus", iVacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                iRetValue = (int)dtData.Rows[0][0];
                logger.log(logger.LogSeverity.INF, logger.LogEvents.VACANCY_MANAGEMENT, "", "UpdateVacancyStatus :: Vacancy status updated successfully.");

            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", "UpdateVacancyStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.VACANCY_MANAGEMENT, "", "<<<UpdateVacancyStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Vacancy - JobApplication statistics Data.
        /// </summary>
        /// <param name="JobApplicationStatisticsJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <param name="VacancyID">
        /// Vacancy ID of which statistics is required.
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
        public static int GetJobApplicationStatistics(string VacancyID, ref string JobApplicationStatisticsJSON)
        {
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
                dbCommand.CommandText = "sp_JobApplicationStatistics";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> StatisticsData = new Dictionary<string, int>();
                        StatisticsData.Add("ApplicationReceived", (int)row["ApplicationReceived"]);
                        StatisticsData.Add("InterviewScheduled", (int)row["InterviewScheduled"]);
                        StatisticsData.Add("RejectedCandidates", (int)row["RejectedCandidates"]);

                        JobApplicationStatisticsJSON = JsonConvert.SerializeObject(StatisticsData);
                        iRetValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int GetLocationList(string VacancyStatus, ref string LocationListJSON)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);

            Dictionary<string, string> LocationList = new Dictionary<string, string>();
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetStateListByVacancyStatus";
                dbCommand.Parameters.AddWithValue("@VacancyStatus", VacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        LocationList.Add(
                            row["PrimaryLocation"].ToString(),
                            row["StateName"].ToString() + "-" + row["CountryName"].ToString()
                            );
                        iRetValue = 1;
                    }
                    LocationListJSON = JsonConvert.SerializeObject(LocationList);
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of vacancies published by any employer
        /// </summary>
        /// <param name="JobLocation">
        /// JobLocation of the vacancy, whose vacacny list is required. 'all' 
        /// </param>
        /// <param name="VacancyList">
        /// Reference variable to list of vacancy, which will be filled by fetching data from databasae.
        /// </param>
        /// <param name="VacancyStatus">
        /// Vacancy Status can be '0: all', '1: open', or '2: closed'
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Vacancy list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Vacancy list cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Vacancy list fetched updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetOpenVacancyListByLocation(string JobLocation, ref List<Vacancy> VacancyList, int VacancyStatus = 0)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            SqlDataAdapter dbAdapter = new SqlDataAdapter();
            DataTable dtData = new DataTable();
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetOpenVacancyListByLocation";
                dbCommand.Parameters.AddWithValue("@JobLocation", JobLocation);
                dbCommand.Parameters.AddWithValue("@VacancyStatus", VacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Vacancy VacancyItem = new Vacancy();
                        VacancyItem.VacancyID = row["VacancyID"].ToString();
                        VacancyItem.UEEmployerID = row["UEEmployerId"].ToString();
                        VacancyItem.VacancyStatusTypeID = row["VacancyStatusTypeId"].ToString();
                        VacancyItem.VacancyName = row["VacancyName"].ToString();
                        VacancyItem.VacancyCode = row["VacancyCode"].ToString();
                        VacancyItem.PrimaryLocation = row["PrimaryLocation"].ToString();
                        VacancyItem.JobType = row["JobType"].ToString();

                        if (row["EmployementStatus"] != DBNull.Value)
                            VacancyItem.EmployementStatus = row["EmployementStatus"].ToString();

                        if (row["CandidatesRequired"] != DBNull.Value)
                            VacancyItem.CandidatesRequired = (int)row["CandidatesRequired"];

                        if (row["RequiredMinExp"] != DBNull.Value)
                            VacancyItem.RequiredMinExp = (int)row["RequiredMinExp"];

                        VacancyItem.RequiredMinQualification = row["RequiredMinQualification"].ToString();

                        if (row["PostingDate"] != DBNull.Value)
                            VacancyItem.PostingDate = (DateTime)row["PostingDate"];

                        VacancyItem.VacancyDetails = row["VacancyDetails"].ToString();

                        if (row["SalaryOffered"] != DBNull.Value)
                            VacancyItem.SalaryOffered = (double)row["SalaryOffered"];

                        VacancyItem.VacancyStatusTypeName = row["VacancyStatusTypeName"].ToString();

                        VacancyItem.StateName = LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).StateName + "-"
                                                + LocationManagement.GetStateDetail(row["PrimaryLocation"].ToString()).Country.CountryName;

                        VacancyItem.CurrencySymbol = row["CurrencySymbol"].ToString();

                        VacancyList.Add(VacancyItem);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }

    }
}