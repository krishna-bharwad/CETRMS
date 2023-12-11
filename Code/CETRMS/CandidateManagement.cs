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
using CETRMS;

namespace CETRMS
{
    public static class CandidateManagement
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
        public static int AuthenticateCandidate(string _UserId, string _Password)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>AuthenticateCandidate(" + _UserId + ", " + _Password + ")");
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
                dbCommand.CommandText = "sp_AuthenticateCETClient";
                dbCommand.Parameters.AddWithValue("@UserId", _UserId);
                dbCommand.Parameters.AddWithValue("@Password", _Password);
                dbCommand.Parameters.AddWithValue("@ClientType", 2);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                iRetValue = (int)dtData.Rows[0][0];
                if (iRetValue < 1)
                {
                    string ExceptionMessage = string.Empty;
                    switch (iRetValue)
                    {
                        case 0:
                            ExceptionMessage = "Incorrect userid";
                            break;
                        case -2:
                            ExceptionMessage = "Incorrect Password";
                            break;
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "AuthenticateCandidate :: " + ExceptionMessage);
                    //throw new CETRMSExceptions(ExceptionMessage);
                }
                else
                {
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "AuthenticateCandidate :: Successfully authenticated.");
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "AuthenticateCandidate :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<AuthenticateCandidate :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Full Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateFullDetails(string CandidateID, ref Candidate CandidateDetail, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateFullDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateFullDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.PersonalProfile.Name = row["Name"].ToString();
                        CandidateDetail.PersonalProfile.CandidateEmail = row["CandidateEmail"].ToString();

                        if (row["RegistrationDate"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.RegistrationDate = (DateTime)row["RegistrationDate"];

                        CandidateDetail.PersonalProfile.CandidateIntro = row["CandidateIntro"].ToString();

                        if (row["DateOfbirth"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.DateOfbirth = (DateTime)row["DateOfbirth"];

                        CandidateDetail.PersonalProfile.Gender = row["Gender"].ToString();
                        CandidateDetail.PersonalProfile.Status = row["Status"].ToString();
                        CandidateDetail.PersonalProfile.ValidationOTP = row["ValidationOTP"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCode = row["ReferralCode"].ToString();

                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PersonalProfile.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + CandidateDetail.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            CandidateDetail.CandidatePhotoImageURL = TFileURL;
                        }

                        if (row["MaritalStatus"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.MaritalStatus = row["MaritalStatus"].ToString();

                        // CandidateDetail.PersonalProfile.MaritalStatus = row["MaritalStatus"].ToString();                
                        CandidateDetail.PersonalProfile.Nationality = row["Nationality"].ToString();
                        CandidateDetail.PersonalProfile.LastUpdatedOn = row["LastUpdatedOn"].ToString();
                        CandidateDetail.PersonalProfile.CandidateCast = row["CandidateCast"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCandidate = row["IsCandidateReferred"].ToString();
                        CandidateDetail.PersonalProfile.VerifyEmail = row["VerifyEmail"].ToString();

                        CandidateDetail.ContactDetails.ContactNumberCountryCode = row["ContactNumberCountryCode"].ToString();
                        CandidateDetail.ContactDetails.ContactNumber = row["ContactNumber"].ToString();
                        CandidateDetail.ContactDetails.CurrentStateCode = row["CurrentStateCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentCityCode = row["CurrentCityCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentAddress = row["PermanentAddress"].ToString();
                        CandidateDetail.ContactDetails.PermanentStateCode = row["PermanentStateCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentCityCode = row["PermanentCityCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentAddress = row["CurrentAddress"].ToString();

                        CandidateDetail.PassportDetails.PassportFileType = row["PassportFileType"].ToString();
                        CandidateDetail.PassportDetails.PassportFileName = row["PassportFileName"].ToString();

                        if (row["PassportData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PassportDetails.PassportData = (byte[])row["PassportData"];
                            string TFileURL = string.Empty, TFileName = "CandidatePassport_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["PassportData"], TFileName, ref TFileURL);
                            CandidateDetail.PassportDetails.PassportFileURL = TFileURL;
                        }

                        CandidateDetail.PassportDetails.MotherName = row["MotherName"].ToString();
                        CandidateDetail.PassportDetails.SpouseName = row["SpouseName"].ToString();

                        if (row["PassportIssueDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportIssueDate = (DateTime)row["PassportIssueDate"];

                        if (row["PassportExpiryDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportExpiryDate = (DateTime)row["PassportExpiryDate"];

                        CandidateDetail.PassportDetails.PassportNumber = row["PassportNumber"].ToString();
                        CandidateDetail.PassportDetails.PassportIssueLocation = row["PassportIssueLocation"].ToString();
                        CandidateDetail.PassportDetails.GivenName = row["GivenName"].ToString();
                        CandidateDetail.PassportDetails.LegalGuardianName = row["LegalGuardianName"].ToString();
                        CandidateDetail.PassportDetails.Surname = row["Surname"].ToString();

                        CandidateDetail.VisaDetails.VisaTypeID = row["VisaTypeID"].ToString();
                        CandidateDetail.VisaDetails.DetailsOfVisa = row["VisaDetails"].ToString();

                        if (row["VisaValidUpto"] != DBNull.Value)
                            CandidateDetail.VisaDetails.VisaValidUpto = (DateTime)row["VisaValidUpto"];

                        CandidateDetail.VisaDetails.VisaFileType = row["VisaFileType"].ToString();
                        CandidateDetail.VisaDetails.VisaFileName = row["VisaFileName"].ToString();

                        if (row["VisaData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.VisaDetails.VisaFileData = (byte[])row["VisaData"];
                            string TFileURL = string.Empty, TFileName = "CandidateVisa_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["VisaData"], TFileName, ref TFileURL);
                            CandidateDetail.VisaDetails.VisaFileURL = TFileURL;
                        }

                        CandidateDetail.QualificationDetails.HighestQualification = row["HighestQualification"].ToString();
                        CandidateDetail.QualificationDetails.UniversityName = row["UniversityName"].ToString();
                        CandidateDetail.QualificationDetails.UniversityLocation = row["UniversityLocation"].ToString();
                        CandidateDetail.QualificationDetails.QualificationYear = row["QualificationYear"].ToString();

                        if (row["NoticePeriod"] != DBNull.Value)
                            CandidateDetail.OtherDetails.NoticePeriod = (int)row["NoticePeriod"];

                        CandidateDetail.OtherDetails.ResumeFileType = row["ResumeFileType"].ToString();
                        CandidateDetail.OtherDetails.ResumeFileName = row["ResumeFileName"].ToString();

                        if (row["ResumeData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.OtherDetails.ResumeData = (byte[])row["ResumeData"];
                            string TFileURL = string.Empty, TFileName = "CandidateResume_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["ResumeData"], TFileName, ref TFileURL);
                            CandidateDetail.OtherDetails.ResumeFileURL = TFileURL;
                        }

                        if (row["TotalExperienceMonths"] != DBNull.Value)
                            CandidateDetail.OtherDetails.TotalExperienceMonth =  (int)row["TotalExperienceMonths"];

                        CandidateDetail.BankAccountDetails.BankAccountNumber = row["BankAccountNumber"].ToString();     //Durgesh
                        CandidateDetail.BankAccountDetails.IFSCCode = row["IFSCCODE"].ToString();                       //Durgesh

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateFullDetails :: Candidate Data : " + new JavaScriptSerializer().Serialize(CandidateDetail));
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateFullDetails :: Data fetched successfully.");
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                //CandidateDetail.PersonalProfile.MaritalStatus = "nn";
                //CandidateDetail.PersonalProfile.MaritalStatus = "-1";
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateFullDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateFullDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get List of Candidate location wise.
        /// </summary>
        /// <param name="CandidateLocation">
        /// Location of Candidate. Provide 'all : For list of candidates from all locations'
        /// </param>
        /// <param name="CanidateList">
        /// Candidate List variable, which is to be filled.
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
        public static int GetCandidateList(string CandidateLocation, ref List<Candidate> CanidateList, int CandidateStatus = 0, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateList(" + CandidateLocation + ", ref List<Candidate> CanidateList)");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dtDataSet = new DataSet();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GeCandidateListByLocation";
                dbCommand.Parameters.AddWithValue("@JobLocation", CandidateLocation);
                dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtDataSet);
                dtData = dtDataSet.Tables[0];
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Candidate CandidateDetail = new Candidate();
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.PersonalProfile.Name = row["Name"].ToString();
                        CandidateDetail.PersonalProfile.CandidateEmail = row["CandidateEmail"].ToString();
                        if (row["RegistrationDate"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.RegistrationDate = (DateTime)row["RegistrationDate"];
                        CandidateDetail.PersonalProfile.CandidateIntro = row["CandidateIntro"].ToString();

                        if (row["DateOfbirth"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.DateOfbirth = (DateTime)row["DateOfbirth"];
                        CandidateDetail.PersonalProfile.Gender = row["Gender"].ToString();
                        CandidateDetail.PersonalProfile.Status = row["Status"].ToString();
                        CandidateDetail.PersonalProfile.ValidationOTP = row["ValidationOTP"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCode = row["ReferralCode"].ToString();

                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PersonalProfile.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + CandidateDetail.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            CandidateDetail.CandidatePhotoImageURL = TFileURL;
                        }

                        CandidateDetail.PersonalProfile.MaritalStatus = row["MaritalStatus"].ToString();
                        CandidateDetail.PersonalProfile.Nationality = row["Nationality"].ToString();
                        CandidateDetail.PersonalProfile.LastUpdatedOn = row["LastUpdatedOn"].ToString();
                        CandidateDetail.PersonalProfile.CandidateCast = row["CandidateCast"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCandidate = row["IsCandidateReferred"].ToString();
                        CandidateDetail.PersonalProfile.VerifyEmail = row["VerifyEmail"].ToString();

                        CandidateDetail.ContactDetails.ContactNumberCountryCode = row["ContactNumberCountryCode"].ToString();
                        CandidateDetail.ContactDetails.ContactNumber = row["ContactNumber"].ToString();
                        CandidateDetail.ContactDetails.CurrentStateCode = row["CurrentStateCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentCityCode = row["CurrentCityCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentAddress = row["PermanentAddress"].ToString();
                        CandidateDetail.ContactDetails.PermanentStateCode = row["PermanentStateCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentCityCode = row["PermanentCityCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentAddress = row["CurrentAddress"].ToString();

                        CandidateDetail.PassportDetails.PassportFileType = row["PassportFileType"].ToString();
                        CandidateDetail.PassportDetails.PassportFileName = row["PassportFileName"].ToString();

                        if (row["PassportData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PassportDetails.PassportData = (byte[])row["PassportData"];
                            string TFileURL = string.Empty, TFileName = "CandidatePassport_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["PassportData"], TFileName, ref TFileURL);
                            CandidateDetail.PassportDetails.PassportFileURL = TFileURL;
                        }

                        CandidateDetail.PassportDetails.MotherName = row["MotherName"].ToString();
                        CandidateDetail.PassportDetails.SpouseName = row["SpouseName"].ToString();

                        if (row["PassportIssueDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportIssueDate = (DateTime)row["PassportIssueDate"];

                        if (row["PassportExpiryDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportExpiryDate = (DateTime)row["PassportExpiryDate"];

                        CandidateDetail.PassportDetails.PassportNumber = row["PassportNumber"].ToString();
                        CandidateDetail.PassportDetails.PassportIssueLocation = row["PassportIssueLocation"].ToString();
                        CandidateDetail.PassportDetails.GivenName = row["GivenName"].ToString();
                        CandidateDetail.PassportDetails.LegalGuardianName = row["LegalGuardianName"].ToString();
                        CandidateDetail.PassportDetails.Surname = row["Surname"].ToString();

                        CandidateDetail.VisaDetails.VisaTypeID = row["VisaTypeID"].ToString();
                        CandidateDetail.VisaDetails.DetailsOfVisa = row["VisaDetails"].ToString();

                        if (row["VisaValidUpto"] != DBNull.Value)
                            CandidateDetail.VisaDetails.VisaValidUpto = (DateTime)row["VisaValidUpto"];

                        CandidateDetail.VisaDetails.VisaFileType = row["VisaFileType"].ToString();
                        CandidateDetail.VisaDetails.VisaFileName = row["VisaFileName"].ToString();

                        if (row["VisaData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.VisaDetails.VisaFileData = (byte[])row["VisaData"];
                            string TFileURL = string.Empty, TFileName = "CandidateVisa_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["VisaData"], TFileName, ref TFileURL);
                            CandidateDetail.VisaDetails.VisaFileURL = TFileURL;
                        }

                        CandidateDetail.QualificationDetails.HighestQualification = row["HighestQualification"].ToString();
                        CandidateDetail.QualificationDetails.UniversityName = row["UniversityName"].ToString();
                        CandidateDetail.QualificationDetails.UniversityLocation = row["UniversityLocation"].ToString();
                        CandidateDetail.QualificationDetails.QualificationYear = row["QualificationYear"].ToString();

                        if (row["NoticePeriod"] != DBNull.Value)
                            CandidateDetail.OtherDetails.NoticePeriod = (int)row["NoticePeriod"];
                        else
                            CandidateDetail.OtherDetails.NoticePeriod = 0;
                        CandidateDetail.OtherDetails.ResumeFileType = row["ResumeFileType"].ToString();
                        CandidateDetail.OtherDetails.ResumeFileName = row["ResumeFileName"].ToString();

                        if (row["ResumeData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.OtherDetails.ResumeData = (byte[])row["ResumeData"];
                            string TFileURL = string.Empty, TFileName = "CandidateResume_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["ResumeData"], TFileName, ref TFileURL);
                            CandidateDetail.OtherDetails.ResumeFileURL = TFileURL;
                        }

                        if (row["TotalExperienceMonths"] != DBNull.Value)
                            CandidateDetail.OtherDetails.TotalExperienceMonth =(int)row["TotalExperienceMonths"];
                        else
                            CandidateDetail.OtherDetails.TotalExperienceMonth = 0;

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateList :: Candidate data : " + new JavaScriptSerializer().Serialize(CandidateDetail));

                        CanidateList.Add(CandidateDetail);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateList :: Candidate list successfully fetched.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateList :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Candidate Dashboard Data.
        /// </summary>
        /// <param name="DashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <param name="CandidateLocation">
        /// Location whose candidate summary is required. Location code is to be passed. Default 'all : Gives summary of all location'.
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
        public static int GetCandidateDashboardData(ref string DashboardJSON, string CandidateLocation = "all")
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateDashboardData(ref string DashboardJSON, " + CandidateLocation + ")");
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
                dbCommand.CommandText = "sp_GetCandidateDashboard";
                dbCommand.Parameters.AddWithValue("@CandidateLocation", CandidateLocation);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("NewRegistration", (int)row["NewRegistration"]);
                        DashboardData.Add("CandidateCompletedDetails", (int)row["CandidateCompletedDetails"]);
                        DashboardData.Add("CandidateUnderSelectionProcess", (int)row["CandidateUnderSelectionProcess"]);
                        DashboardData.Add("CandidateFinalSelected", (int)row["CandidateFinalSelected"]);
                        DashboardData.Add("CandidateRejected", (int)row["CandidateRejected"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateDashboardData :: " + DashboardJSON);
                    }
                }
                else
                {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("NewRegistration", 0);
                        DashboardData.Add("CandidateCompletedDetails",0);
                        DashboardData.Add("CandidateUnderSelectionProcess", 0);
                        DashboardData.Add("CandidateFinalSelected", 0);
                        DashboardData.Add("CandidateRejected", 0);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateDashboardData :: " + DashboardJSON);
                  

                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateDashboardData :: Dashboard Data fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Candidate Facebook SignUp. Function is to be filed with data received from facebook server.
        /// </summary>
        /// <param name="FacebookProfile">
        /// Facebook Profile
        /// </param>       
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Facebook cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Facebook cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Facebook Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int CandidatefacebookSignIn(FacebookUserclass FacebookProfile, ref string ClientID, ref int ClientStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>CandidatefacebookSignIn()");
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
                dbCommand.CommandText = "sp_CandidateSignUp";
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
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidatefacebookSignIn :: Successfully signed up. ClientID - " + ClientID.ToString());
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidatefacebookSignIn :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<CandidatefacebookSignIn :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Candidate Google SignUp. Function is to be filed with data received from facebook server.
        /// </summary>
        /// <param name="GoogleProfile">
        /// Google Profile
        /// </param>      
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Google cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Google cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Google Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int CandidateGoogleSignIn(GoogleUserclass GoogleProfile, ref string ClientID, ref int ClientStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>CandidateGoogleSignIn()");
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
                dbCommand.CommandText = "sp_CandidateSignUp";
                dbCommand.Parameters.AddWithValue("@UEAuthenticationType", 2);
                dbCommand.Parameters.AddWithValue("@AuthenticationID", GoogleProfile.id);
                dbCommand.Parameters.AddWithValue("@AuthenticationName", GoogleProfile.name);
                dbCommand.Parameters.AddWithValue("@AuthenticationProfilePicURL", GoogleProfile.picture);
                dbCommand.Parameters.AddWithValue("@UEAdminID", GoogleProfile.id);
                dbCommand.Parameters.AddWithValue("@Password", DBNull.Value);
                dbCommand.Parameters.AddWithValue("@Email", GoogleProfile.email);
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
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidateGoogleSignIn :: Successfully signed up. ClientID - " + ClientID.ToString());
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidateGoogleSignIn :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<CandidateGoogleSignIn()");
            return iRetValue;
        }
        /// <summary>
        /// Candidate Personal Profile SignUp. 
        /// </summary>
        /// <param name="PersonalProfile">
        /// Candidate's Personal Profile 
        /// </param>       
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Personal Profile  cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Personal Profile  cannot be signed up due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Personal Profile  Signed up successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int CandidatePersonalProfileSignUp(PersonalProfileClass PersonalProfile, ref string ClientID, ref int ClientStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>CandidatePersonalProfileSignUp()");
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
                dbCommand.CommandText = "sp_CandidateSignUp";
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

                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidatePersonalProfileSignUp :: Successfully signed up. ClientID - " + ClientID.ToString());
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidatePersonalProfileSignUp :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<CandidatePersonalProfileSignUp :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get UE Client ID from Authenticator ID received by Google or Facebook Authentication services.
        /// </summary>
        /// <param name="AuthenticatorID">
        /// Authentication ID received from LDAP services
        /// </param>
        /// <param name="CETClientID">
        /// Fetched Client ID will be stored in CETClientID string.
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
        public static int GetCETClientIDByAuthenticatorId(string AuthenticatorID, ref string CETClientID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCETClientIDByAuthenticatorId(" + AuthenticatorID + ", ref string CETClientID)");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "select ClientID from CETClient where AuthenticationID = @AuthenticationID and ClientTypeID=2";
                dbCommand.Parameters.AddWithValue("@AuthenticationID", AuthenticatorID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CETClientID = row["ClientId"].ToString();
                        iRetValue = 1;
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCETClientIDByAuthenticatorId :: Client ID fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCETClientIDByAuthenticatorId :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCETClientIDByAuthenticatorId :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to insert job application.
        /// </summary>
        /// <param name="JobApplicationDetails">
        /// Job Application object which is to be inserted.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Job application cannot be inserted due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Job application cannot be inserted due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Job application inserted successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int SendJobApplication(JobApplication JobApplicationDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>SendJobApplication(JobApplication JobApplicationDetails)");
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
                dbCommand.CommandText = "sp_InsertJobApplication";
                dbCommand.Parameters.AddWithValue("@VacancyID", JobApplicationDetails.VacancyID);
                dbCommand.Parameters.AddWithValue("@CandidateID", JobApplicationDetails.CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["ApplicationStatus"];

                        Notification EmployerNotification = new Notification();
                        Employer employer = new Employer();
                        Vacancy vacancy = new Vacancy();
                        VacancyManager.GetVacancyDetails(JobApplicationDetails.VacancyID, ref vacancy);
                        EmployerManagement.GetEmployerByID(vacancy.CETEmployerId, ref employer);
                        EmployerNotification.NotificationType = cNotificationType.PersonalisedNotification;
                        EmployerNotification.CETClientID = vacancy.CETEmployerId;
                        EmployerNotification.NotificationMessage = "New Job Application received for " + vacancy.VacancyName;
                        EmployerNotification.hyperlink = URLs.JobApplicationDetails;
                        NotificationManagement.AddNewNotification(ref EmployerNotification);

                        Candidate candidate = new Candidate();
                        CandidateManagement.GetCandidateContactlDetails(JobApplicationDetails.CandidateID, ref candidate);
                        WhatsAppManagement.SendMessage(candidate.ContactDetails.ContactNumberCountryCode.Trim() + candidate.ContactDetails.ContactNumber.Trim(),
                                                        "You application for " + vacancy.VacancyName + " is submitted successfully.");

                        WhatsAppManagement.SendMessage(employer.WhatsAppNumber.Trim(), "New Job Application received for " + vacancy.VacancyName);
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "SendJobApplication :: Job application submitted successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "SendJobApplication :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<SendJobApplication :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's personal profile
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="personalProfile">
        /// Object of class Candidate.personal, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate personal details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate personal details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate personal details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidatePersonalProfile(string CandidateID, Candidate.Personal personalProfile)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidatePersonalProfile(" + CandidateID + ", Candidate.Personal personalProfile)");
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
                dbCommand.CommandText = "sp_UpdateCandidatePersonalProfile";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@Name", personalProfile.Name);
                dbCommand.Parameters.AddWithValue("@CandidateEmail", personalProfile.CandidateEmail);
                dbCommand.Parameters.AddWithValue("@CandidateIntro", personalProfile.CandidateIntro);
                dbCommand.Parameters.AddWithValue("@DateOfbirth", personalProfile.DateOfbirth);
                dbCommand.Parameters.AddWithValue("@Gender", personalProfile.Gender);

                string CandidatePhotoImageURL = HttpContext.Current.Server.MapPath(@".\TempFiles\FTP\" + personalProfile.CandidatePhotoImageURL);
                if (File.Exists(CandidatePhotoImageURL))
                {
                    FileInfo fileInfo = new FileInfo(CandidatePhotoImageURL);
                    byte[] data = new byte[fileInfo.Length];
                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }
                    dbCommand.Parameters.AddWithValue("@Photo", data);
                    fileInfo.Delete();
                }
                else
                    dbCommand.Parameters.AddWithValue("@Photo", System.Data.SqlTypes.SqlBinary.Null);

                dbCommand.Parameters.AddWithValue("@MaritalStatus", personalProfile.MaritalStatus);
                dbCommand.Parameters.AddWithValue("@Nationality", personalProfile.Nationality);
                dbCommand.Parameters.AddWithValue("@CandidateCast", personalProfile.CandidateCast);
                dbCommand.Parameters.AddWithValue("@VerifyEmail", personalProfile.VerifyEmail);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                        Candidate candidate = new Candidate();
                        CandidateManagement.GetCandidateFullDetails(CandidateID, ref candidate);

                        if (candidate.PersonalProfile.Status == CandidateStatus.ProfileCreated.ToString())
                        {
                            PaymentTypeDetails paymentTypeDetails = new PaymentTypeDetails();
                            paymentTypeDetails.PaymentType = cPaymentType.CandidateRegistrationFee;
                            PaymentManagement.GetPaymentTypeDetails(ref paymentTypeDetails);
                            Payment payment = new Payment();
                            payment.PaymentType = cPaymentType.CandidateRegistrationFee;
                            payment.Currency = paymentTypeDetails.Currency;
                            payment.Amount = paymentTypeDetails.Amount;
                            payment.TaxAmount = (paymentTypeDetails.Amount * paymentTypeDetails.Tax) / 100;
                            payment.CETClientID = candidate.CandidateID;
                            payment.DueDate = System.DateTime.Now;
                            payment.Reserve1 = string.Empty;
                            payment.Reserve2 = string.Empty;
                            PaymentManagement.InsertDuePayment(ref payment);

                            Notification notification = new Notification();
                            notification.NotificationType = cNotificationType.PersonalisedNotification;
                            notification.NotificationMessage = "Your are requested to pay Registration fee as per the policy after completion of profile. Please go to Payment section to check the due payment.";
                            notification.CETClientID = candidate.CandidateID;
                            notification.hyperlink = "#";
                            NotificationManagement.AddNewNotification(ref notification);

                        }
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidatePersonalProfile :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidatePersonalProfile :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidatePersonalProfile :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Personal Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidatePersonalDetails(string CandidateID, ref Candidate CandidateDetail, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidatePersonalDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidatePersonalDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.PersonalProfile.Name = row["Name"].ToString();
                        CandidateDetail.PersonalProfile.CandidateEmail = row["CandidateEmail"].ToString();

                        if (row["RegistrationDate"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.RegistrationDate = (DateTime)row["RegistrationDate"];

                        CandidateDetail.PersonalProfile.CandidateIntro = row["CandidateIntro"].ToString();


                        if (row["DateOfbirth"] != DBNull.Value)
                            CandidateDetail.PersonalProfile.DateOfbirth = (DateTime)row["DateOfbirth"];

                        CandidateDetail.PersonalProfile.Gender = row["Gender"].ToString();
                        CandidateDetail.PersonalProfile.Status = row["Status"].ToString();
                        CandidateDetail.PersonalProfile.ValidationOTP = row["ValidationOTP"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCode = row["ReferralCode"].ToString();

                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PersonalProfile.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + CandidateDetail.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            CandidateDetail.CandidatePhotoImageURL = TFileURL;
                        }
                        CandidateDetail.PersonalProfile.MaritalStatus = row["MaritalStatus"].ToString();
                        CandidateDetail.PersonalProfile.Nationality = row["Nationality"].ToString();
                        CandidateDetail.PersonalProfile.LastUpdatedOn = row["LastUpdatedOn"].ToString();
                        CandidateDetail.PersonalProfile.CandidateCast = row["CandidateCast"].ToString();
                        CandidateDetail.PersonalProfile.ReferralCandidate = row["IsCandidateReferred"].ToString();
                        CandidateDetail.PersonalProfile.VerifyEmail = row["VerifyEmail"].ToString();

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidatePersonalDetails :: Data : " + new JavaScriptSerializer().Serialize(CandidateDetail.PersonalProfile));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidatePersonalDetails :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidatePersonalDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidatePersonalDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's Passport profile
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="PassportDetails">
        /// Object of class Candidate.Passport, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Passport details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Passport details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Passport details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidatePassportDetails(string CandidateID, Candidate.Passport PassportDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidatePassportDetails(" + CandidateID + ", Candidate.Passport PassportDetails)");
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
                dbCommand.CommandText = "sp_UpdateCandidatePassportDetails";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@GivenName", PassportDetails.GivenName);
                dbCommand.Parameters.AddWithValue("@LegalGuardianName", PassportDetails.LegalGuardianName);
                dbCommand.Parameters.AddWithValue("@Surname", PassportDetails.Surname);
                dbCommand.Parameters.AddWithValue("@MotherName", PassportDetails.MotherName);
                dbCommand.Parameters.AddWithValue("@SpouseName", PassportDetails.SpouseName);
                dbCommand.Parameters.AddWithValue("@PassportIssueDate", PassportDetails.PassportIssueDate);
                dbCommand.Parameters.AddWithValue("@PassportExpiryDate", PassportDetails.PassportExpiryDate);
                dbCommand.Parameters.AddWithValue("@PassportNumber", PassportDetails.PassportNumber);
                dbCommand.Parameters.AddWithValue("@PassportIssueLocation", PassportDetails.PassportIssueLocation);
                dbCommand.Parameters.AddWithValue("@PassportFileType", PassportDetails.PassportFileType);
                dbCommand.Parameters.AddWithValue("@PassportFileName", PassportDetails.PassportFileName);
                //dbCommand.Parameters.AddWithValue("@PassportData", PassportDetails.PassportData);

                string CandidatePassportFileURL = HttpContext.Current.Server.MapPath(@".\TempFiles\FTP\" + PassportDetails.PassportFileURL);
                if (File.Exists(CandidatePassportFileURL))
                {
                    FileInfo fileInfo = new FileInfo(CandidatePassportFileURL);
                    byte[] data = new byte[fileInfo.Length];
                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }
                    dbCommand.Parameters.AddWithValue("@PassportData", data);
                    fileInfo.Delete();
                }
                else
                    dbCommand.Parameters.AddWithValue("@PassportData", System.Data.SqlTypes.SqlBinary.Null);

                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidatePassportDetails :: Executed Successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidatePassportDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidatePassportDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Full Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidatePassportDetails(string CandidateID, ref Candidate CandidateDetail, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidatePassportDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidatePassportDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.PassportDetails.PassportFileType = row["PassportFileType"].ToString();
                        CandidateDetail.PassportDetails.PassportFileName = row["PassportFileName"].ToString();

                        if (row["PassportData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.PassportDetails.PassportData = (byte[])row["PassportData"];
                            string TFileURL = string.Empty, TFileName = "CandidatePassport_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["PassportData"], TFileName, ref TFileURL);
                            CandidateDetail.PassportDetails.PassportFileURL = TFileURL;
                        }
                        CandidateDetail.PassportDetails.MotherName = row["MotherName"].ToString();
                        CandidateDetail.PassportDetails.SpouseName = row["SpouseName"].ToString();

                        if (row["PassportIssueDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportIssueDate = (DateTime)row["PassportIssueDate"];

                        if (row["PassportExpiryDate"] != DBNull.Value)
                            CandidateDetail.PassportDetails.PassportExpiryDate = (DateTime)row["PassportExpiryDate"];

                        CandidateDetail.PassportDetails.PassportNumber = row["PassportNumber"].ToString();
                        CandidateDetail.PassportDetails.PassportIssueLocation = row["PassportIssuePlace"].ToString();
                        CandidateDetail.PassportDetails.GivenName = row["GivenName"].ToString();
                        CandidateDetail.PassportDetails.LegalGuardianName = row["LegalGuardianName"].ToString();
                        CandidateDetail.PassportDetails.Surname = row["Surname"].ToString();

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidatePassportDetails :: data" + new JavaScriptSerializer().Serialize(CandidateDetail.PassportDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidatePassportDetails :: Passport details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidatePassportDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidatePassportDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's VISA profile
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="VisaDetails">
        /// Object of class Candidate.VISA, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate VISA details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate VISA details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate VISA details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidateVisaDetails(string CandidateID, Candidate.Visa VisaDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidateVisaDetails(" + CandidateID + ", Candidate.Visa VisaDetails)");
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
                dbCommand.CommandText = "sp_UpdateCandidateVisaDetails";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@VisaTypeID", VisaDetails.VisaTypeID);
                dbCommand.Parameters.AddWithValue("@VisaDetails", VisaDetails.DetailsOfVisa);
                dbCommand.Parameters.AddWithValue("@VisaValidUpto", VisaDetails.VisaValidUpto);
                dbCommand.Parameters.AddWithValue("@VisaFileType", VisaDetails.VisaFileType);
                dbCommand.Parameters.AddWithValue("@VisaFileName", VisaDetails.VisaFileName);
                //dbCommand.Parameters.AddWithValue("@VisaData", VisaDetails.VisaFileData);

                string VisaFileURL = HttpContext.Current.Server.MapPath(@".\TempFiles\FTP\" + VisaDetails.VisaFileURL);
                if (File.Exists(VisaFileURL))
                {
                    FileInfo fileInfo = new FileInfo(VisaFileURL);
                    byte[] data = new byte[fileInfo.Length];
                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }
                    dbCommand.Parameters.AddWithValue("@VisaData", data);
                    fileInfo.Delete();
                }
                else
                    dbCommand.Parameters.AddWithValue("@VisaData", System.Data.SqlTypes.SqlBinary.Null);

                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateVisaDetails :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateVisaDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidateVisaDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Full Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateVisaDetails(string CandidateID, ref Candidate CandidateDetail, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateVisaDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateVisaDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.VisaDetails.VisaTypeID = row["VisaTypeID"].ToString();
                        CandidateDetail.VisaDetails.DetailsOfVisa = row["VisaDetails"].ToString();

                        if (row["VisaValidUpto"] != DBNull.Value)
                            CandidateDetail.VisaDetails.VisaValidUpto = (DateTime)row["VisaValidUpto"];
                        CandidateDetail.VisaDetails.VisaFileType = row["VisaFileType"].ToString();
                        CandidateDetail.VisaDetails.VisaFileName = row["VisaFileName"].ToString();

                        if (row["VisaData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.VisaDetails.VisaFileData = (byte[])row["VisaData"];
                            string TFileURL = string.Empty, TFileName = "CandidateVisa_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["VisaData"], TFileName, ref TFileURL);
                            CandidateDetail.VisaDetails.VisaFileURL = TFileURL;
                        }

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateVisaDetails :: data : " + new JavaScriptSerializer().Serialize(CandidateDetail.VisaDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateVisaDetails :: Canidate visa details fetched  successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateVisaDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateVisaDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's Qualification profile
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="QualificationDetails">
        /// Object of class Candidate.Qualification, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Qualification details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Qualification details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Qualification details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidateQualificationDetails(string CandidateID, Candidate.Qualification QualificationDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidateQualificationDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_UpdateCandidateQualificationDetails";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@HighestQualification", QualificationDetails.HighestQualification);
                dbCommand.Parameters.AddWithValue("@UniversityName", QualificationDetails.UniversityName);
                dbCommand.Parameters.AddWithValue("@UniversityLocation", QualificationDetails.UniversityLocation);
                dbCommand.Parameters.AddWithValue("@QualificationYear", QualificationDetails.QualificationYear);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateQualificationDetails :: Candidate Qualification Details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateQualificationDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidateQualificationDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Full Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateQualificationDetails(string CandidateID, ref Candidate CandidateDetail)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateQualificationDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateQualificationDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.QualificationDetails.HighestQualification = row["HighestQualification"].ToString();
                        CandidateDetail.QualificationDetails.UniversityName = row["UniversityName"].ToString();
                        CandidateDetail.QualificationDetails.UniversityLocation = row["UniversityLocation"].ToString();
                        CandidateDetail.QualificationDetails.QualificationYear = row["QualificationYear"].ToString();

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateQualificationDetails :: data : " + new JavaScriptSerializer().Serialize(CandidateDetail.QualificationDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateQualificationDetails :: Candidate Qualification Details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateQualificationDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateQualificationDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's contact profile
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="ContactDetails">
        /// Object of class Candidate.contact, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate contact details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate contact details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate contact details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidateContactDetails(string CandidateID, Candidate.Contact ContactDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidateContactDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_UpdateCandidateContactDetails";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@ContactNumberCountryCode", ContactDetails.ContactNumberCountryCode);
                dbCommand.Parameters.AddWithValue("@ContactNumber", ContactDetails.ContactNumber);
                dbCommand.Parameters.AddWithValue("@CurrentStateCode", ContactDetails.CurrentStateCode);
                dbCommand.Parameters.AddWithValue("@CurrentCityCode", ContactDetails.CurrentCityCode);
                dbCommand.Parameters.AddWithValue("@PermanentAddress", ContactDetails.PermanentAddress);
                dbCommand.Parameters.AddWithValue("@PermanentStateCode", ContactDetails.PermanentStateCode);
                dbCommand.Parameters.AddWithValue("@PermanentCityCode", ContactDetails.PermanentCityCode);
                dbCommand.Parameters.AddWithValue("@CurrentAddress", ContactDetails.CurrentAddress);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateContactDetails :: Candidate Contact details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateContactDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidateContactDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's Bank Account Details
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes Bank Account Detail is required to be updated.
        /// </param>
        /// <param name="BankAccount">
        /// Object of class Candidate.BankAccount, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Bank Account Details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Bank Account Details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Bank Account Details updated successfully.</description>
        /// </item> 
        /// </list>
        public static int UpdateBankAccountDetails(string CandidateID, Candidate.BankAccount BankAccount)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidateBankAccountDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
            int iRetValue = -1;
            SqlConnection dbconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbconnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                dbCommand.Connection = dbconnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_UpdateBankAccountDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbCommand.Parameters.AddWithValue("@BankAccountNumber", BankAccount.BankAccountNumber);
                dbCommand.Parameters.AddWithValue("@IFSCCode", BankAccount.IFSCCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateBankAccountDetails :: Candidate Bank Account Details Updated Successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateBankAccountDetails :: " + Message);
            }
            finally
            {
                dbconnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidateBankAccountDetails :: " + iRetValue.ToString());
            return iRetValue;
        }


        /// <summary>
        /// Function to get Full Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateContactlDetails(string CandidateID, ref Candidate CandidateDetail)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateContactlDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateContactDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();
                        CandidateDetail.ContactDetails.ContactNumberCountryCode = row["ContactNumberCountryCode"].ToString();
                        CandidateDetail.ContactDetails.ContactNumber = row["ContactNumber"].ToString();
                        CandidateDetail.ContactDetails.CurrentStateCode = row["CurrentStateCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentCityCode = row["CurrentCityCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentAddress = row["PermanentAddress"].ToString();
                        CandidateDetail.ContactDetails.PermanentStateCode = row["PermanentStateCode"].ToString();
                        CandidateDetail.ContactDetails.PermanentCityCode = row["PermanentCityCode"].ToString();
                        CandidateDetail.ContactDetails.CurrentAddress = row["CurrentAddress"].ToString();

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateContactlDetails :: data : " + new JavaScriptSerializer().Serialize(CandidateDetail.ContactDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateContactlDetails :: Contact details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateContactlDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateContactlDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update candidate's other details as resume and experience
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate ID whoes profile is required to be updated.
        /// </param>
        /// <param name="OtherDetails">
        /// Object of class Candidate.other, which will be update in database.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate other details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate other details cannot be updated due to mismatch in AuthenticatorId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate other details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int UpdateCandidateOtherDetails(string CandidateID, Candidate.Other OtherDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>UpdateCandidateOtherDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_UpdateCandidateOtherDetails";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@ResumeFileType", OtherDetails.ResumeFileType);
                dbCommand.Parameters.AddWithValue("@ResumeFileName", OtherDetails.ResumeFileName);
                //dbCommand.Parameters.AddWithValue("@ResumeData", OtherDetails.ResumeData);
                dbCommand.Parameters.AddWithValue("@NoticePeriod", OtherDetails.NoticePeriod);
                dbCommand.Parameters.AddWithValue("@TotalExperienceMonths", OtherDetails.TotalExperienceMonth);

                string ResumeFileURL = HttpContext.Current.Server.MapPath(@".\TempFiles\FTP\" + OtherDetails.ResumeFileURL);
                if (File.Exists(ResumeFileURL))
                {
                    FileInfo fileInfo = new FileInfo(ResumeFileURL);
                    byte[] data = new byte[fileInfo.Length];
                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        fs.Read(data, 0, data.Length);
                    }
                    dbCommand.Parameters.AddWithValue("@ResumeData", data);
                    fileInfo.Delete();
                }
                else
                    dbCommand.Parameters.AddWithValue("@ResumeData", System.Data.SqlTypes.SqlBinary.Null);

                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Status"];
                    }
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateOtherDetails :: Candidate Other Details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "UpdateCandidateOtherDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<UpdateCandidateOtherDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get other Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate details cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateOtherDetails(string CandidateID, ref Candidate CandidateDetail, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateOtherDetails(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateOtherDetails";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CandidateID = row["CandidateId"].ToString();

                        if (row["NoticePeriod"] != DBNull.Value)
                            CandidateDetail.OtherDetails.NoticePeriod = (int)row["NoticePeriod"];
                        else
                            CandidateDetail.OtherDetails.NoticePeriod = 0;
                        CandidateDetail.OtherDetails.ResumeFileType = row["ResumeFileType"].ToString();
                        CandidateDetail.OtherDetails.ResumeFileName = row["ResumeFileName"].ToString();

                        if (row["ResumeData"] != DBNull.Value)
                        {
                            if (ForWeb)
                                CandidateDetail.OtherDetails.ResumeData = (byte[])row["ResumeData"];
                            string TFileURL = string.Empty, TFileName = "CandidateResume_" + CandidateDetail.CandidateID.ToString() + ".pdf";
                            common.ConverBinaryDataToTempFile((byte[])row["ResumeData"], TFileName, ref TFileURL);
                            CandidateDetail.OtherDetails.ResumeFileURL = TFileURL;
                        }

                        if (row["TotalExperienceMonths"] != DBNull.Value)
                            CandidateDetail.OtherDetails.TotalExperienceMonth = (int)row["TotalExperienceMonths"];

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateOtherDetails :: data : " + new JavaScriptSerializer().Serialize(CandidateDetail.OtherDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateOtherDetails :: Candidate Other details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateOtherDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateOtherDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Brief Profile of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <param name="CandidateDetail">
        /// Object of candidate class in which details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Candidate Brief Profile cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Candidate Brief Profile cannot be fetched Incorrect candidate-id</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Candidate Brief Profile fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetCandidateBriefProfile(string CandidateID, ref Candidate CandidateDetail)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateBriefProfile(" + CandidateID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetCandidateBriefProfile";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateDetail.CanidateBriefProfile = row["Profile"].ToString();
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: data : " + new JavaScriptSerializer().Serialize(CandidateDetail.OtherDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: Candidate details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateBriefProfile :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Brief Profile Details of Candidate.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id, whose details are required
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>NILL</term>
        /// <description>Candidate details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>sRetValue</term>
        /// <description>Candidate brief profile.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static string GetCandidateBriefProfile(string CandidateID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateBriefProfile(" + CandidateID + ")");
            string sRetValue = string.Empty;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetCandidateBriefProfile";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        sRetValue = row["Profile"].ToString();
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: data : " + sRetValue);
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: Candidate details fetched successfully.");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateBriefProfile :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateBriefProfile :: " + sRetValue.ToString());
            return sRetValue;
        }
        public static int GetCandidateIDByReferralCode(string ReferralCode, ref string CandidateID)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "Select CandidateId from CETCandidate where ReferralCode='"+ReferralCode+"'";
                dbCommand.ExecuteScalar();
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateID = row["CandidateId"].ToString();
                        IRetValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                IRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
        /// <summary>
        /// Function to Masked Profile Details of Candidate.
        /// </summary>
        /// <param name="JobApplicationID">
        /// JobApplicationID id, whose details are required
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
        public static int GetCandidateMaskedDetails(string JobApplicationID, ref MaskedCandidate maskedCandidate, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidateMaskedDetails(" + JobApplicationID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedCandidateDetails";
                dbCommand.Parameters.AddWithValue("@JobApplicationID", JobApplicationID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        maskedCandidate.VacancyName = row["VacancyName"].ToString();
                        maskedCandidate.JobApplicationID = Convert.ToInt32(JobApplicationID);
                        maskedCandidate.CandidateID = (int)row["CandidateId"];
                        maskedCandidate.Age = row["Age"].ToString();
                        maskedCandidate.Gender = row["Gender"].ToString();
                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                maskedCandidate.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + maskedCandidate.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            maskedCandidate.CandidateProfileImgURL = TFileURL;
                        }
                        maskedCandidate.Location = row["Location"].ToString();
                        maskedCandidate.CanidateBriefProfile = row["BriefProfile"].ToString();
                        if(row["InterviewStatus"] != DBNull.Value)
                        maskedCandidate.InterviewStatus = (int)row["InterviewStatus"];      //Durgesh
                        maskedCandidate.JobApplicationStatus = (int)row["ApplicationStatus"];

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedDetails :: Data : " + new JavaScriptSerializer().Serialize(maskedCandidate));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedDetails :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidateMaskedDetails :: " + iRetValue.ToString());
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
        public static int GetCandidateMaskedList(string VacancyID, ref List<MaskedCandidate> maskedCandidates, int CandidateStatus=0, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidateMaskedList(" + VacancyID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedCandidateListByVacancy";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyID);
                dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        MaskedCandidate maskedCandidate = new MaskedCandidate();
                        maskedCandidate.VacancyName = row["VacancyName"].ToString();
                        maskedCandidate.JobApplicationID = (int)row["JobApplicationID"];
                        maskedCandidate.CandidateID = (int)row["CandidateId"];
                        maskedCandidate.Age = row["Age"].ToString();
                        maskedCandidate.Gender = row["Gender"].ToString();
                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                maskedCandidate.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + maskedCandidate.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            maskedCandidate.CandidateProfileImgURL = TFileURL;
                        }
                        maskedCandidate.Location = row["Location"].ToString();
                        maskedCandidate.CanidateBriefProfile = row["BriefProfile"].ToString();
                        maskedCandidate.JobApplicationStatus = Convert.ToInt32(row["JobApplicationStatus"]);

                        maskedCandidates.Add(maskedCandidate);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedList :: Data : " + new JavaScriptSerializer().Serialize(maskedCandidate));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedList :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidateMaskedList :: " + iRetValue.ToString());
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
        public static int GetCandidateMaskedListByEmployer(string EmployerID, ref List<MaskedCandidate> maskedCandidates, int CandidateStatus = 0, bool ForWeb = false)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidateMaskedListByEmployer(" + EmployerID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedCandidateListByEmployer";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerID);
                dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        MaskedCandidate maskedCandidate = new MaskedCandidate();
                        maskedCandidate.VacancyName = row["VacancyName"].ToString();
                        maskedCandidate.JobApplicationID = (int)row["JobApplicationID"];
                        maskedCandidate.CandidateID = (int)row["CandidateId"];
                        maskedCandidate.Age = row["Age"].ToString();
                        maskedCandidate.Gender = row["Gender"].ToString();
                        if (row["Photo"] != DBNull.Value)
                        {
                            if (ForWeb)
                                maskedCandidate.Photo = (byte[])row["Photo"];
                            string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + maskedCandidate.CandidateID.ToString() + ".jpg";
                            common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            maskedCandidate.CandidateProfileImgURL = TFileURL;
                        }
                        maskedCandidate.Location = row["Location"].ToString();
                        maskedCandidate.CanidateBriefProfile = row["BriefProfile"].ToString();
                        maskedCandidate.JobApplicationStatus = Convert.ToInt32(row["JobApplicationStatus"]);            //Durgesh

                        maskedCandidates.Add(maskedCandidate);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByEmployer :: Data : " + new JavaScriptSerializer().Serialize(maskedCandidate));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByEmployer :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByEmployer :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidateMaskedListByEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
 
        public static int UpdateCandidateStatus(string CandidateID, int CandidateStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>CandidateUpdateStatus(" + CandidateID + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [CETCandidate] SET [Status] = @CandidateStatus WHERE CandidateID = @CandidateID";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;



                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidateUpdateStatus :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "CandidateUpdateStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<CandidateUpdateStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to Masked Profile List of Candidate.
        /// </summary>
        /// <param name="LocationCode">
        /// LocationCode, whose list is required
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
        public static int GetCandidateMaskedListByLocation(string LocationCode, ref List<MaskedCandidate> maskedCandidates, int CandidateStatus = 0)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidateMaskedListByLocation(" + LocationCode + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedCandidateListByLocation";
                dbCommand.Parameters.AddWithValue("@LocationCode", LocationCode);
                dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        MaskedCandidate maskedCandidate = new MaskedCandidate();
                       // maskedCandidate.VacancyName = row["VacancyName"].ToString();
                        maskedCandidate.CandidateID = (int)row["CandidateId"];
                        maskedCandidate.Age = row["Age"].ToString();
                        maskedCandidate.Gender = row["Gender"].ToString();
                      //  maskedCandidate.Name = row["Name"].ToString();                      //Added By Krishna
                        if (row["Photo"] != DBNull.Value)
                        {
                            maskedCandidate.Photo = (byte[])row["Photo"];
                            //string TFileURL = string.Empty, TFileName = "CandidateProfileImg_" + maskedCandidate.CandidateID.ToString() + ".jpg";
                            //common.ConverBinaryDataToTempFile((byte[])row["Photo"], TFileName, ref TFileURL);
                            //maskedCandidate.CandidateProfileImgURL = TFileURL;
                        }
                        maskedCandidate.Location = row["Location"].ToString();
                        maskedCandidate.CanidateBriefProfile = row["BriefProfile"].ToString();

                        maskedCandidates.Add(maskedCandidate);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByLocation :: Data : " + new JavaScriptSerializer().Serialize(maskedCandidate));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByLocation :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedListByLocation :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidateMaskedListByEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetMaskedInterviewListByCandidateId(string CandidateId, ref List<cMaskedInterviewDetails> maskedInterviewLists, int InterviewStatus = 0)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetMaskedInterviewListByCandidateId(" + CandidateId + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedInterviewListByCandidateId";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateId);
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

                        if (row["InterviewScheduleDate"] != DBNull.Value)
                            maskedInterviewDetails.InterviewScheduleDate = Convert.ToDateTime(row["InterviewScheduleDate"]);

                        maskedInterviewDetails.InteviewTimeZone = row["InteviewTimeZone"].ToString();
                        maskedInterviewDetails.ZoomMeetingId = row["ZoomMeetingId"].ToString();
                        maskedInterviewDetails.ZoomStartUrl = row["ZoomJoinUrl"].ToString();
                        maskedInterviewDetails.ZoomMeetingStatus = row["ZoomMeetingStatus"].ToString();

                        maskedInterviewLists.Add(maskedInterviewDetails);

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByCandidateId :: Data : " + new JavaScriptSerializer().Serialize(maskedInterviewDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByCandidateId :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByCandidateId :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetMaskedInterviewListByCandidateId :: " + iRetValue.ToString());
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
        public static int GetCandidateMobileDashboardData(string CandidateID, ref string DashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetCandidateMobileDashboardData()");
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
                dbCommand.CommandText = "sp_CandidateMobileAppDashboard";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("TotalVacancies", (int)row["TotalVacancies"]);
                        DashboardData.Add("TotalJobApplications", (int)row["TotalJobApplications"]);
                        DashboardData.Add("ScheduleInterview", (int)row["TotalScheduledInterviews"]);
                        DashboardData.Add("ApplicationReceived", (int)row["AwaitedResults"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateMobileDashboardData :: Successfully fetched Candidate Dashboard Data.");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateMobileDashboardData :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "<<<GetCandidateMobileDashboardData :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int VerifyCandidateEmail(string CandidateId, int EmailStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>VerifyCandidateEmail(" + CandidateId + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [CETCandidate] SET [VerifyEmail] = @VerifyEmail WHERE CandidateId = @CandidateId";
                dbCommand.Parameters.AddWithValue("@CandidateId", CandidateId);
                dbCommand.Parameters.AddWithValue("@VerifyEmail", EmailStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;

                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "VerifyCandidateEmail :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "VerifyCandidateEmail :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<VerifyCandidateEmail :: " + iRetValue.ToString());
            return iRetValue;
        }


        ///// <summary>
        ///// Function to get Brief Profile Details of Candidate.
        ///// </summary>
        ///// <param name="CandidateID">
        ///// Candidate id, whose details are required
        ///// </param>
        ///// <returns>
        ///// <list type="bullet|number|table">
        ///// <listheader>
        ///// <term>Return Code</term>
        ///// <description>description</description>
        ///// </listheader>
        ///// <item>
        ///// <term>NILL</term>
        ///// <description>Candidate Experience cannot be fetched due to exception/error in code execution.</description>
        ///// </item>
        ///// <item>
        ///// <term>sRetValue</term>
        ///// <description>Candidate Experience.</description>
        ///// </item> 
        ///// </list>
        ///// </returns>  
        ///// <exception cref="CETRMSExceptions">
        ///// Custom CETRMSException to return user authentication 
        ///// </exception>
        //public static string GetCandidateExperience(string CandidateID, ref Candidate CandidateExp)
        //{
        //    logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", ">>>GetCandidateExperience(" + CandidateID + ")");
        //    string sRetValue = string.Empty;
        //    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
        //    try
        //    {
        //        dbConnection.Open();
        //        SqlCommand dbCommand = new SqlCommand();
        //        SqlDataAdapter dbAdapter = new SqlDataAdapter();
        //        DataTable dtData = new DataTable();
        //        dbCommand.Connection = dbConnection;
        //        dbCommand.CommandType = CommandType.Text;
        //        dbCommand.CommandText = "select dbo.fn_GetExpInYears ([TotalExperienceMonths]) as 'TotalExperienceMonths' FROM UECandidate WHERE CandidateId=@CandidateID";
        //        dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
        //        dbAdapter.SelectCommand = dbCommand;
        //        dbAdapter.Fill(dtData);
        //        if (dtData.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dtData.Rows)
        //            {
        //                if (row["TotalExperienceMonths"] != DBNull.Value)
        //                    CandidateExp.OtherDetails.TotalExperienceMonth = (int)row["TotalExperienceMonths"];

        //                logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateExperience :: data : " + sRetValue);
        //            }
        //        }
        //        logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateExperience :: Candidate Experience fetched successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        string Message = "Error: " + ex.Message + "\r\n";
        //        System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
        //        Message = Message + t.ToString();
        //        logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "GetCandidateExperience :: " + Message);
        //    }
        //    finally
        //    {
        //        dbConnection.Close();
        //    }
        //    logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANDIDATE_MANAGEMENT, "", "<<<GetCandidateExperience :: " + sRetValue.ToString());
        //    return sRetValue;
        //}
    }
}