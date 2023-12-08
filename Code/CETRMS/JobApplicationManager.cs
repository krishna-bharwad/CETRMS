using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{
    public static class JobApplicationManager
    {
        /// <summary>
        /// Function to fetch job application details.
        /// </summary>
        /// <param name="JobApplicationID">
        /// Job Application id.
        /// </param>/// 
        /// <param name="JobApplicationDetails">
        /// Job Application object in which fetched details will be inserted.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Job application details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Job application details cannot be fetched due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Job application details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetJobApplicationDetails(string JobApplicationID, ref JobApplication JobApplicationDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", ">>>GetJobApplicationDetails(JobApplicationID="+ JobApplicationID + ", ref JobApplication JobApplicationDetails)");
            int iRetValue = RetValue.NoRecord;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetJobApplicationDetails";
                dbCommand.Parameters.AddWithValue("@JobApplicationID", JobApplicationID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        JobApplicationDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        JobApplicationDetails.VacancyID = row["VacancyID"].ToString();
                        JobApplicationDetails.CandidateID = row["CandidateID"].ToString();
                        JobApplicationDetails.ApplicationStatus = (int)row["ApplicationStatus"];
                        JobApplicationDetails.CompanyRemarks = row["CompanyRemarks"].ToString();
                        JobApplicationDetails.AppliedOn = (DateTime)row["AppliedOn"];

                        if(row["RemarksOn"] != DBNull.Value)
                            JobApplicationDetails.RemarksOn = (DateTime)row["RemarksOn"];

                        if(JobApplicationDetails.ApplicationStatus == JobApplicationStatus.OfferLetterIssued)
                        {
                            byte[] OfferLetterData = (byte[])row["OfferLetterData"];
                            string TempFileURL = string.Empty;
                            common.ConverBinaryDataToTempFile(OfferLetterData, "OfferLetter_" + JobApplicationDetails.JobApplicationID + System.DateTime.Now.ToString("ddMMyyhhmmss") + ".pdf", ref TempFileURL);
                            JobApplicationDetails.OfferLetterURL = TempFileURL;
                        }
                        

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationDetails :: data : " + new JavaScriptSerializer().Serialize(JobApplicationDetails));
                    }

                    iRetValue = RetValue.Success;
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationDetails :: Job Application Details fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "<<<GetJobApplicationDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to fetch job application list.
        /// </summary>
        /// <param name="VacancyID">
        /// Vacancy id for which list of job applications are required.
        /// </param>/// 
        /// <param name="JobApplicationList">
        /// Job Application list object in which fetched details will be inserted.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Job application list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Job application list cannot be fetched due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Job application list fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetJobApplicationListByVacancy(string VacancyID, ref List<JobApplication> JobApplicationList, int ApplicationStatus = -1)                 
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", ">>>GetJobApplicationListByVacancy(VacancyID = "+ VacancyID + ", ref List<JobApplication> JobApplicationList, int ApplicationStatus = -1)");
            int iRetValue = RetValue.NoRecord;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_JobApplicationList";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyID);
                dbCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        JobApplication JobApplicationDetails = new JobApplication();
                        JobApplicationDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        JobApplicationDetails.VacancyID = row["VacancyID"].ToString();
                        JobApplicationDetails.CandidateID = row["CandidateID"].ToString();
                        JobApplicationDetails.ApplicationStatus = (int)row["ApplicationStatus"];
                        JobApplicationDetails.CompanyRemarks = row["CompanyRemarks"].ToString();
                        if(row["AppliedOn"] != DBNull.Value)
                        JobApplicationDetails.AppliedOn = (DateTime)row["AppliedOn"];
                        if(row["RemarksOn"] != DBNull.Value)
                        JobApplicationDetails.RemarksOn = (DateTime)row["RemarksOn"];

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByVacancy :: data : " + new JavaScriptSerializer().Serialize(JobApplicationDetails));

                        JobApplicationList.Add(JobApplicationDetails);
                    }

                    iRetValue = RetValue.Success;
                }

                logger.log(logger.LogSeverity.INF, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByVacancy :: ");
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByVacancy :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "<<<GetJobApplicationListByVacancy :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to fetch job application list.
        /// </summary>
        /// <param name="EmployerID">
        /// Vacancy id for which list of job applications are required.
        /// </param>/// 
        /// <param name="JobApplicationList">
        /// Job Application list object in which fetched details will be inserted.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Job application list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Job application list cannot be fetched due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Job application list fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetJobApplicationListByEmployer(string EmployerID, ref List<JobApplication> JobApplicationList, int ApplicationStatus = -1)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", ">>>GetJobApplicationListByEmployer(EmployerID = " + EmployerID + ", ref List<JobApplication> JobApplicationList, ApplicationStatus = " + ApplicationStatus + ")");
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
                dbCommand.CommandText = "sp_JobApplicationListByEmployer";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerID);
                dbCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        JobApplication JobApplicationDetails = new JobApplication();
                        JobApplicationDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        JobApplicationDetails.VacancyID = row["VacancyID"].ToString();
                        JobApplicationDetails.CandidateID = row["CandidateID"].ToString();
                        JobApplicationDetails.ApplicationStatus = (int)row["ApplicationStatus"];
                        JobApplicationDetails.CompanyRemarks = row["CompanyRemarks"].ToString();
                        JobApplicationDetails.AppliedOn = (DateTime)row["AppliedOn"];
                        if(row["RemarksOn"]!=DBNull.Value)
                        JobApplicationDetails.RemarksOn = (DateTime)row["RemarksOn"];

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByEmployer :: data : " + new JavaScriptSerializer().Serialize(JobApplicationDetails));
                        JobApplicationList.Add(JobApplicationDetails);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByEmployer :: Job Application list fetched successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByEmployer :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "<<<GetJobApplicationListByEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }

        /// <summary>
        /// Function to fetch job application list.
        /// </summary>
        /// <param name="CandidateID">
        /// Candidate id for which list of job applications are required.
        /// </param>/// 
        /// <param name="JobApplicationList">
        /// Job Application list object in which fetched details will be inserted.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Job application list cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Job application list cannot be fetched due to mismatch in passed arguments.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Job application list fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetJobApplicationListByCandidate(string CandidateID, ref List<JobApplication> JobApplicationList, int ApplicationStatus = -1)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", ">>>GetJobApplicationListByCandidate(EmployerID = " + CandidateID + ", ref List<JobApplication> JobApplicationList, ApplicationStatus = " + ApplicationStatus + ")");
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
                dbCommand.CommandText = "sp_JobApplicationListByCandidate";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);
                dbCommand.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                Vacancy vacancy = new Vacancy();
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        JobApplication JobApplicationDetails = new JobApplication();
                        JobApplicationDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        JobApplicationDetails.VacancyID = row["VacancyID"].ToString();
                        JobApplicationDetails.CandidateID = row["CandidateID"].ToString();
   
                        JobApplicationDetails.ApplicationStatus = (int)row["ApplicationStatus"];
                        JobApplicationDetails.CompanyRemarks = row["CompanyRemarks"].ToString();
                        JobApplicationDetails.AppliedOn = (DateTime)row["AppliedOn"];
                        if(row["RemarksOn"]!= DBNull.Value)
                        JobApplicationDetails.RemarksOn = (DateTime)row["RemarksOn"];

                        VacancyManager.GetVacancyDetails(JobApplicationDetails.VacancyID, ref vacancy);
                        JobApplicationDetails.Reserve1 = vacancy.VacancyName;

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByCandidate :: data : " + new JavaScriptSerializer().Serialize(JobApplicationDetails));
                        JobApplicationList.Add(JobApplicationDetails);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByCandidate :: Job Application list fetched successfully.");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                iRetValue = -1;
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GetJobApplicationListByCandidate :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "<<<GetJobApplicationListByCandidate :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int UpdateJobApplicationStatus(string JobApplicationID, int JobAplicationStatus)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", ">>>UpdateJobApplicationStatus(JobApplicationID=" + JobApplicationID + ")");
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
                dbCommand.CommandText = "sp_UpdateJobApplicationStatus";
                dbCommand.Parameters.AddWithValue("@JobApplicationID", JobApplicationID);
                dbCommand.Parameters.AddWithValue("@JobApplicationStatus", JobAplicationStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;

                Candidate candidate = new Candidate();
                Vacancy vacancy = new Vacancy();
                Employer employer = new Employer();
                JobApplication jobApplication = new JobApplication();
                JobApplicationManager.GetJobApplicationDetails(JobApplicationID, ref jobApplication);
                CandidateManagement.GetCandidateFullDetails(jobApplication.CandidateID, ref candidate);
                VacancyManager.GetVacancyDetails(jobApplication.VacancyID, ref vacancy);
                EmployerManagement.GetEmployerByID(vacancy.UEEmployerID, ref employer);

                if (JobAplicationStatus == JobApplicationStatus.EmployerRecruitmentFeeDue)
                {
                    //CandidateManagement.UpdateCandidateStatus(jobApplication.CandidateID, CandidateStatus.RecruitementFeeDue);

                    WhatsAppManagement.SendMessage(candidate.ContactDetails.ContactNumberCountryCode.Trim()
                                                    + candidate.ContactDetails.ContactNumber.Trim(),
                                                    "Congratulations!!!, You have been selected for " + vacancy.VacancyName + " by " + employer.BusinessName);

                    /*Insert Employer Recruitment fee*/
                    PaymentTypeDetails paymentTypeDetails = new PaymentTypeDetails();
                    paymentTypeDetails.PaymentType = cPaymentType.EmployerRecruitmentFee;
                    PaymentManagement.GetPaymentTypeDetails(ref paymentTypeDetails);
                    Payment EmpRecFeePayment = new Payment();
                    EmpRecFeePayment.PaymentType = cPaymentType.EmployerRecruitmentFee;
                    if (paymentTypeDetails.CalculationMode == "amount")
                        EmpRecFeePayment.Amount = paymentTypeDetails.Amount;
                    else
                        EmpRecFeePayment.Amount = vacancy.SalaryOffered * (paymentTypeDetails.Amount / 100);
                    EmpRecFeePayment.TaxAmount = EmpRecFeePayment.Amount * (paymentTypeDetails.Tax / 100);
                    EmpRecFeePayment.DueDate = System.DateTime.Now.AddDays(paymentTypeDetails.DueDays);
                    EmpRecFeePayment.Currency = paymentTypeDetails.Currency;
                    EmpRecFeePayment.UEClientID = employer.EmployerID;
                    EmpRecFeePayment.Reserve1 = JobApplicationID;
                    EmpRecFeePayment.Reserve2 = string.Empty;
                    PaymentManagement.InsertDuePayment(ref EmpRecFeePayment);

                    /*Insert Candidate Recruitment fee*/
                    Payment CandRecFeePayment = new Payment();
                    paymentTypeDetails.PaymentType = cPaymentType.CandidateRecruitmentFee;
                    PaymentManagement.GetPaymentTypeDetails(ref paymentTypeDetails);
                    CandRecFeePayment.PaymentType = cPaymentType.CandidateRecruitmentFee;
                    if (paymentTypeDetails.CalculationMode == "amount")
                        CandRecFeePayment.Amount = paymentTypeDetails.Amount;
                    else
                        CandRecFeePayment.Amount = vacancy.SalaryOffered * (paymentTypeDetails.Amount / 100);
                    CandRecFeePayment.TaxAmount = EmpRecFeePayment.Amount * (paymentTypeDetails.Tax / 100);
                    CandRecFeePayment.DueDate = System.DateTime.Now.AddDays(paymentTypeDetails.DueDays);
                    CandRecFeePayment.Currency = paymentTypeDetails.Currency;
                    CandRecFeePayment.UEClientID = employer.EmployerID;
                    CandRecFeePayment.Reserve1 = JobApplicationID;
                    CandRecFeePayment.Reserve2 = string.Empty;
                    PaymentManagement.InsertDuePayment(ref CandRecFeePayment);

                    Notification notification = new Notification();
                    notification.NotificationType = cNotificationType.PersonalisedNotification;
                    notification.NotificationMessage = "Your are requested to pay Recruitment fee as per the policy after hiring of the employee. Please go to Payment section to check the due payment.";
                    notification.UEClientID = employer.EmployerID;
                    notification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref notification);
                }
                if(JobAplicationStatus == JobApplicationStatus.EmployerRecruitmentFeePaid)
                {
                    CandidateManagement.UpdateCandidateStatus(candidate.CandidateID, CandidateStatus.Hired);

                    Notification CandNotification = new Notification();
                    CandNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    CandNotification.NotificationMessage = "Congratulations!!!, You have been selected for " + vacancy.VacancyName + " by " + employer.BusinessName + ". Recruiter will be contacting you shortly.";
                    CandNotification.UEClientID = candidate.CandidateID;
                    CandNotification.hyperlink = "#";
                    NotificationManagement.AddNewNotification(ref CandNotification);

                    VacancyManager.UpdateVacancyStatus(jobApplication.VacancyID, cVacancyStatus.Filled);
                }
                if(JobAplicationStatus == JobApplicationStatus.CandidateRecruitmentFeePaid)
                {
                    CandidateManagement.UpdateCandidateStatus(jobApplication.CandidateID, CandidateStatus.Hired);
                }
                if (JobAplicationStatus == JobApplicationStatus.Rejected)
                {
                    CandidateManagement.UpdateCandidateStatus(jobApplication.CandidateID, CandidateStatus.Rejected);
                    WhatsAppManagement.SendMessage(candidate.ContactDetails.ContactNumberCountryCode.Trim()
                                                    + candidate.ContactDetails.ContactNumber.Trim(),
                                                    "Your candidature for " + vacancy.VacancyName + " has been dropped" + " by " + employer.BusinessName);
                }
                if (JobAplicationStatus == JobApplicationStatus.OfferLetterIssued)
                {
                    CandidateManagement.UpdateCandidateStatus(jobApplication.CandidateID, CandidateStatus.Rejected);
                    WhatsAppManagement.SendMessage(candidate.ContactDetails.ContactNumberCountryCode.Trim()
                                                    + candidate.ContactDetails.ContactNumber.Trim(),
                                                    "Congratulations!!!, " + employer.BusinessName + " has issued offer letter.");
                }

                logger.log(logger.LogSeverity.INF, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "UpdateJobApplicationStatus :: Job Application status updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "UpdateJobApplicationStatus :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "<<<UpdateJobApplicationStatus :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GenerateOfferLetter(string JobApplicationID)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                OfferLetterDetails offerLetter = new OfferLetterDetails();
                JobApplication jobApplication = new JobApplication();
                Employer employer = new Employer();
                Candidate candidate = new Candidate();
                Vacancy vacancy = new Vacancy();
                GetJobApplicationDetails(JobApplicationID, ref jobApplication);
                VacancyManager.GetVacancyDetails(jobApplication.VacancyID, ref vacancy);
                EmployerManagement.GetEmployerByID(vacancy.UEEmployerID, ref employer, true);
                CandidateManagement.GetCandidateFullDetails(jobApplication.CandidateID, ref candidate, true);
                offerLetter.EmployerName = employer.Name;
                offerLetter.EmployerBusinessName = employer.BusinessName;
                offerLetter.EmployerAddress = employer.address + ", "
                                                + LocationManagement.GetStateDetail(employer.LocationStateCode).StateName+", "
                                                + LocationManagement.GetStateDetail(employer.LocationStateCode).Country.CountryName;
                offerLetter.EmployerEmail = employer.email;
                offerLetter.EmployerPhone = employer.WhatsAppNumber;
                offerLetter.JobTitle = vacancy.VacancyName;
                offerLetter.RecipientName = candidate.PersonalProfile.Name;
                offerLetter.RecipientAddress = candidate.ContactDetails.CurrentAddress + ", "
                                                + LocationManagement.GetCityDetail(candidate.ContactDetails.CurrentStateCode, candidate.ContactDetails.CurrentCityCode).CityName + ", "
                                                + LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).StateName + ", "
                                                + LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).Country.CountryName;
                offerLetter.Salary = vacancy.SalaryOffered.ToString();
                offerLetter.OfferLetterDate = System.DateTime.Now;
                offerLetter.StartDate = System.DateTime.Now.AddDays(2);
                offerLetter.EmployerBusinessLogo = employer.BusinessLogo;

                string deviceInfo = "";
                string[] streamId;
                Warning[] warning;

                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                ReportViewer OfferLetterRV = new ReportViewer();
                OfferLetterRV.ProcessingMode = ProcessingMode.Local;
                OfferLetterRV.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("OfferLetter.rdlc");

                DataTable OfferLetterData = new DataTable();
                OfferLetterData.Columns.Add(new DataColumn("EmployerName", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("EmployerBusinessName", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("EmployerAddress", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("EmployerEmail", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("EmployerPhone", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("EmployerBusinessLogo", typeof(byte[])));
                OfferLetterData.Columns.Add(new DataColumn("OfferLetterDate", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("RecipientName", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("RecipientAddress", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("JobTitle", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("StartDate", typeof(string)));
                OfferLetterData.Columns.Add(new DataColumn("Salary", typeof(string)));

                OfferLetterData.Rows.Add(
                    offerLetter.EmployerName,
                    offerLetter.EmployerBusinessName,
                    offerLetter.EmployerAddress,
                    offerLetter.EmployerEmail,
                    offerLetter.EmployerPhone,
                    offerLetter.EmployerBusinessLogo,
                    offerLetter.OfferLetterDate.ToString("d"),
                    offerLetter.RecipientName,
                    offerLetter.RecipientAddress,
                    offerLetter.JobTitle,
                    offerLetter.StartDate.ToString("d"),
                    offerLetter.Salary
                    );

                ReportDataSource OfferLetterDS = new ReportDataSource("DataSet1", OfferLetterData);
                OfferLetterRV.LocalReport.DataSources.Clear();
                OfferLetterRV.LocalReport.DataSources.Add(OfferLetterDS);
                byte[] OfferLetterDataBytes = OfferLetterRV.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamId, out warning);

                if(OfferLetterDataBytes.Length != 0)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataTable dtData = new DataTable();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandText = "UPDATE JobApplications Set OfferLetterData = @OfferLetterData, OfferLetterGeneratedOn = @OfferLetterGeneratedOn Where JobApplicationID = @JobApplicationID";
                    dbCommand.Parameters.AddWithValue("@OfferLetterData", OfferLetterDataBytes);
                    dbCommand.Parameters.AddWithValue("@OfferLetterGeneratedOn", System.DateTime.Now);
                    dbCommand.Parameters.AddWithValue("@JobApplicationID", jobApplication.JobApplicationID);
                    dbCommand.ExecuteNonQuery();

                    dbConnection.Close();

                    UpdateJobApplicationStatus(jobApplication.JobApplicationID, JobApplicationStatus.OfferLetterIssued);
                    
                    iRetValue = RetValue.Success;
                }
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.JOBAPPLICATION_MANAGEMENT, "", "GenerateOfferLetter :: " + Message);
            }
            finally
            {
            }
            return iRetValue;
        }
    }
}