using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{

    public static class InterviewManagement
    {
        /// <summary>
        /// Function to Propose to new interview to be called by employer. The function will enter porposed interview details in database for further pursual and fix video call meeting by universal education staff.
        /// </summary>
        /// <param name="InterviewDetail">
        /// Details of the staff member, which is to be added into database. The parameter is passed as reference. 
        /// Required parameters are:
        /// InterviewDetail.JobApplicationID, 
        /// InterviewDetail.PreferredDateTime, 
        /// InterviewDetail.EmployerRemarks
        /// InterviewDetail.ChosenTimeZone
        /// InterviewDetail.DurationInMinutes
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Proposed interview request cannot be created due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Proposed interview request cannot be created due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Proposed interview request created successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int ProposeNewInterview(Interview InterviewDetail)
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
                dbCommand.CommandText = "sp_ProposeNewInterview";
                dbCommand.Parameters.AddWithValue("@JobApplicationID", InterviewDetail.JobApplicationID);
                dbCommand.Parameters.AddWithValue("@PreferredDateTime", InterviewDetail.PreferredDateTime);
                dbCommand.Parameters.AddWithValue("@EmployerRemarks", InterviewDetail.EmployerRemarks);
                dbCommand.Parameters.AddWithValue("@ChosenTimeZone", InterviewDetail.ChosenTimeZone);
                dbCommand.Parameters.AddWithValue("@DurationInMinutes", InterviewDetail.DurationInMinutes);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["InterviewID"];
                        JobApplicationManager.UpdateJobApplicationStatus(InterviewDetail.JobApplicationID, JobApplicationStatus.InterviewRequested);


                        Notification notification = new Notification();
                        Employer EmployerDetails = new Employer();
                        JobApplication JADetails = new JobApplication();
                        Vacancy VacancyDetails = new Vacancy();

                        JobApplicationManager.GetJobApplicationDetails(InterviewDetail.JobApplicationID, ref JADetails);
                        VacancyManager.GetVacancyDetails(JADetails.VacancyID, ref VacancyDetails);
                        EmployerManagement.GetEmployerByID(VacancyDetails.UEEmployerID, ref EmployerDetails);

                        notification.NotificationType = cNotificationType.AdminNotification;
                        notification.UEClientID = "-1";
                        notification.NotificationMessage = "New interview schedule request received from " + EmployerDetails.BusinessName;
                        notification.hyperlink = URLs.InterviewDetailsURL+iRetValue;
                        NotificationManagement.AddNewNotification(ref notification);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>ProposeNewInterview():" + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Delete scheduled Interview. Function will cancel zoom video call.
        /// </summary>
        /// <param name="InterviewDetail">
        /// Details of the Interview, whose zoom call is scheduled. 
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Scheduled Interview cannot be cancelled due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Scheduled Interview cannot be cancelled due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Scheduled Interview cancelled successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int DeleteScheduledInterview(Interview InterviewDetail)
        {
            int iRetValue = 0;
            if (ZoomVideoCallMeetingManagment.CancelVCMeeting(ref InterviewDetail) == 1)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
                try
                {
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataTable dtData = new DataTable();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "sp_UpdateZoomMeetingStatus";
                    dbCommand.Parameters.AddWithValue("@InterviewID", InterviewDetail.InterviewID);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_Status", InterviewDetail.ZoomVCMeetingStatus.Status);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_duration", InterviewDetail.ZoomVCMeetingStatus.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_start_time", InterviewDetail.ZoomVCMeetingStatus.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_end_time", InterviewDetail.ZoomVCMeetingStatus.end_time);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_host_id", InterviewDetail.ZoomVCMeetingStatus.host_id);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_dept", InterviewDetail.ZoomVCMeetingStatus.dept);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_participants_count", InterviewDetail.ZoomVCMeetingStatus.participants_count);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_source", InterviewDetail.ZoomVCMeetingStatus.source);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_topic", InterviewDetail.ZoomVCMeetingStatus.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_total_minutes", InterviewDetail.ZoomVCMeetingStatus.total_minutes);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_type", InterviewDetail.ZoomVCMeetingStatus.type);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_user_email", InterviewDetail.ZoomVCMeetingStatus.user_email);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_user_name", InterviewDetail.ZoomVCMeetingStatus.user_name);
                    dbAdapter.SelectCommand = dbCommand;
                    dbAdapter.Fill(dtData);
                    if (dtData.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtData.Rows)
                        {
                            iRetValue = (int)row["Status"];
                            JobApplicationManager.UpdateJobApplicationStatus(InterviewDetail.JobApplicationID, JobApplicationStatus.InterviewCancelled);
                        }
                    }
                }
                catch (Exception ex)
                {
                    iRetValue = -1;
                    string Message = "Error: " + ex.Message + "\r\n";
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Message = Message + t.ToString();
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>DeleteScheduledInterview()" + Message);

                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Re-schedule Interview on new date.
        /// </summary>
        /// <param name="InterviewDetails">
        /// Details of the Interview, which is required to be reschduled. InterviewDetails should be available for update. 
        /// Compulsary Parameters are ZoomVideoCallRequest, ZoomVideCallResponse.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview cannot be updated due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int ReScheduleInterview(Interview InterviewDetails)
        {
            int iRetValue = 0;
            string RequestStr = new JavaScriptSerializer().Serialize(InterviewDetails.ZoomVCMeetingRequest);
            InterviewDetails.ZoomVCMeetingRequest.start_time = InterviewDetails.PreferredDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (ZoomVideoCallMeetingManagment.ReScheduleVCMeeting(ref InterviewDetails) == 1)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
                try
                {
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "sp_CreateNewZoomMeeting";
                    dbCommand.Parameters.AddWithValue("@InterviewID", InterviewDetails.InterviewID);
                    dbCommand.Parameters.AddWithValue("@UERemarks", InterviewDetails.UERemarks);
                    dbCommand.Parameters.AddWithValue("@PreferredDateTime", InterviewDetails.PreferredDateTime);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_duration", InterviewDetails.ZoomVCMeetingRequest.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_start_time", InterviewDetails.ZoomVCMeetingRequest.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_timezone", InterviewDetails.ZoomVCMeetingRequest.timezone);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_topic", InterviewDetails.ZoomVCMeetingRequest.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_CompleteRequest", RequestStr);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_uuid", InterviewDetails.ZoomVCMeetingResponse.uuid);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_id", InterviewDetails.ZoomVCMeetingResponse.id);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_host_id", InterviewDetails.ZoomVCMeetingResponse.host_id);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_host_email", InterviewDetails.ZoomVCMeetingResponse.host_email);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_topic", InterviewDetails.ZoomVCMeetingResponse.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_type", InterviewDetails.ZoomVCMeetingResponse.type);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_status", InterviewDetails.ZoomVCMeetingResponse.status);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_start_time", InterviewDetails.ZoomVCMeetingResponse.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_duration", InterviewDetails.ZoomVCMeetingResponse.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_timezone", InterviewDetails.ZoomVCMeetingResponse.timezone);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_created_at", InterviewDetails.ZoomVCMeetingResponse.created_at);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_start_url", InterviewDetails.ZoomVCMeetingResponse.start_url);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_join_url", InterviewDetails.ZoomVCMeetingResponse.join_url);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_password", InterviewDetails.ZoomVCMeetingResponse.password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_h323_password", InterviewDetails.ZoomVCMeetingResponse.h323_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_pstn_password", InterviewDetails.ZoomVCMeetingResponse.pstn_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_encrypted_password", InterviewDetails.ZoomVCMeetingResponse.encrypted_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_pre_schedule", InterviewDetails.ZoomVCMeetingResponse.pre_schedule);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_CompleteRequest", new JavaScriptSerializer().Serialize(InterviewDetails.ZoomVCMeetingResponse));
                    dbCommand.ExecuteNonQuery();
                    iRetValue = 1;
                }
                catch (Exception ex)
                {
                    iRetValue = -1;
                    string Message = "Error: " + ex.Message + "\r\n";
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Message = Message + t.ToString();
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>ReScheduleInterview() : " + Message);

                }
                finally
                {
                    dbConnection.Close();
                }
            }

            return iRetValue;
        }
        /// <summary>
        /// Function to Get Interview Details by Interview ID
        /// </summary>
        /// <param name="InterviewID">
        /// Unique ID of the Interview fow which details are required.
        /// </param>/// 
        /// <param name="InterviewDetails">
        /// Object of type interview in which fetched details will be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetInterviewDetails(string InterviewID, ref Interview InterviewDetails)
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
                dbCommand.CommandText = "sp_GetInterviewDetails";
                dbCommand.Parameters.AddWithValue("@InterviewID", InterviewID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if(dtData.Rows.Count > 0)
                {
                    foreach(DataRow row in dtData.Rows)
                    {
                        InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];
                        InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];
                        InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();
                        if(row["ZVCRQ_duration"] != DBNull.Value)
                        InterviewDetails.ZoomVCMeetingRequest.duration = Convert.ToInt32(row["ZVCRQ_duration"]);
                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                   //     InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();
                        InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];
                    }
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewDetails() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }

            return iRetValue;
        }
        /// <summary>
        /// Function to Get Interview Video Call Details by Interview ID from Zoom Service API. The function will fetch latest data of Zoom Meeting status and update information in database.
        /// </summary>
        /// <param name="InterviewDetails">
        /// Object of type interview in which fetched details will be filled. Fetched data will be filled in ZoomVideoCallMeetingStatus object of interview object.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview zoom meetings status details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview zoom meetings status cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview zoom meetings status fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetInterviewVideoCallDetails(ref Interview InterviewDetails)
        {
            int iRetValue = 0;
            if(ZoomVideoCallMeetingManagment.GetZoomVCStatus(ref InterviewDetails) == 1)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
                try
                {
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataTable dtData = new DataTable();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "sp_UpdateZoomMeetingStatus";
                    dbCommand.Parameters.AddWithValue("@InterviewID", InterviewDetails.InterviewID);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_Status", InterviewDetails.ZoomVCMeetingStatus.Status);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_duration", InterviewDetails.ZoomVCMeetingStatus.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_start_time", InterviewDetails.ZoomVCMeetingStatus.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_end_time", InterviewDetails.ZoomVCMeetingStatus.end_time);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_host_id", InterviewDetails.ZoomVCMeetingStatus.host_id);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_dept", InterviewDetails.ZoomVCMeetingStatus.dept);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_participants_count", InterviewDetails.ZoomVCMeetingStatus.participants_count);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_source", InterviewDetails.ZoomVCMeetingStatus.source);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_topic", InterviewDetails.ZoomVCMeetingStatus.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_total_minutes", InterviewDetails.ZoomVCMeetingStatus.total_minutes);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_type", InterviewDetails.ZoomVCMeetingStatus.type);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_user_email", InterviewDetails.ZoomVCMeetingStatus.user_email);
                    dbCommand.Parameters.AddWithValue("@ZVCMS_user_name", InterviewDetails.ZoomVCMeetingStatus.user_name);
                    dbAdapter.SelectCommand = dbCommand;
                    dbAdapter.Fill(dtData);
                    if (dtData.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtData.Rows)
                        {
                            iRetValue = (int)row["Status"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    iRetValue = -1;
                    string Message = "Error: " + ex.Message + "\r\n";
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Message = Message + t.ToString();
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewVideoCallDetails() : " + Message);

                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Get List of Scheduled Interview Details by status
        /// </summary>
        /// <param name="InterviewList">
        /// Fetched list of interviews will be filled in ref object of Interview List.
        /// </param>
        /// <param name="InterviewStatus">
        /// Interview status for which list is required.  
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Parameter Value</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>All Scheduled Interviews</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatus.InterviewProposed :: 1</term>
        /// <description>List of Proposed Interviews by Employer</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatusInterviewScheduled :: 2</term>
        /// <description>List of interview Scheduled by Universal Education Staff</description>
        /// </item>
        /// </list> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewStarted :: 3</term>
        /// <description>List of Started/running Interviews by Employer</description>
        /// </item>     
        /// <item>
        /// <term>InterviewCallStatus.InterviewCompleted :: 4</term>
        /// <description>List of Completed Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewDropped :: 5</term>
        /// <description>List of Dropped Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewCancelled :: 6</term>
        /// <description>List of Cancelled Interviews</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewRejected :: 7</term>
        /// <description>List of Rejected Interviews</description>
        /// </item>         
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetInterviewListByStatus(ref List<Interview> InterviewList, int InterviewStatus = -1)
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
                dbCommand.CommandText = "sp_GetInterviewListByStatus";
                dbCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Interview InterviewDetails = new Interview();

                        if (row["InterviewID"] != DBNull.Value)
                            InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        if (row["JobApplicationID"] != DBNull.Value)
                            InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        if (row["PreferredDateTime"] != DBNull.Value)
                            InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];

                        if (row["InterviewStatus"] != DBNull.Value)
                            InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];

                        if (row["EmployerRemarks"] != DBNull.Value)
                            InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        if (row["UERemarks"] != DBNull.Value)
                            InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        if (row["CandidateRemarks"] != DBNull.Value)
                            InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();
                        if (row["ZVCRQ_duration"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.duration = Convert.ToInt32(row["ZVCRQ_duration"]);

                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        if (row["ZVCRQ_start_time"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        if (row["ZVCRQ_timezone"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        if (row["ZVCRQ_topic"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        if (row["ZVCRS_uuid"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        if (row["ZVCRS_id"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        if (row["ZVCRS_host_id"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        if (row["ZVCRS_host_email"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        if (row["ZVCRS_type"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        if (row["DurationInMinutes"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        if (row["ZVCRS_status"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        if (row["ZVCRS_start_time"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();
                        if (row["ZVCRS_duration"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        if (row["ZVCRS_timezone"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        if (row["ZVCRS_created_at"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        if (row["ZVCRS_start_url"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        if (row["ZVCRS_join_url"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        if (row["ZVCRS_password"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        if (row["ZVCRS_h323_password"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        if (row["ZVCRS_pstn_password"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        if (row["ZVCRS_encrypted_password"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        if (row["ZVCRS_pre_schedule"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        if (row["ZVCMS_Status"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        if (row["ZVCMS_duration"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        if (row["ZVCMS_start_time"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        if (row["ZVCMS_end_time"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        if (row["ZVCMS_host_id"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        if (row["ZVCMS_dept"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        if (row["ZVCMS_participants_count"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        if (row["ZVCMS_source"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        if (row["ZVCMS_topic"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        if (row["ZVCMS_total_minutes"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                        //    InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();

                        if (row["ZVCMS_user_email"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        if (row["ZVCMS_user_name"] != DBNull.Value) 
                            InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        if (row["ChosenTimeZone"] != DBNull.Value)
                            InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();
                        if (row["DurationInMinutes"] != DBNull.Value)
                            InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];

                        InterviewList.Add(InterviewDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewListByStatus() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Get List of Scheduled Interview Details by Date
        /// </summary>
        /// <param name="InterviewDate">
        /// Unique ID of the Candidate fow which details are required.
        /// </param>/// 
        /// <param name="InterviewList">
        /// Fetched list of interviews will be filled in ref object of Interview List.
        /// </param>
        /// <param name="InterviewStatus">
        /// Interview status for which list is required.  
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Parameter Value</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>All Scheduled Interviews</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatus.InterviewProposed :: 1</term>
        /// <description>List of Proposed Interviews by Employer</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatusInterviewScheduled :: 2</term>
        /// <description>List of interview Scheduled by Universal Education Staff</description>
        /// </item>
        /// </list> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewStarted :: 3</term>
        /// <description>List of Started/running Interviews by Employer</description>
        /// </item>     
        /// <item>
        /// <term>InterviewCallStatus.InterviewCompleted :: 4</term>
        /// <description>List of Completed Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewDropped :: 5</term>
        /// <description>List of Dropped Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewCancelled :: 6</term>
        /// <description>List of Cancelled Interviews</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewRejected :: 7</term>
        /// <description>List of Rejected Interviews</description>
        /// </item>         
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetInterviewListByDate(DateTime InterviewDate, ref List<Interview> InterviewList, int InterviewStatus = -1)
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
                dbCommand.CommandText = "sp_GetInterviewListByDate";
                dbCommand.Parameters.AddWithValue("@InterviewDate", InterviewDate);
                dbCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Interview InterviewDetails = new Interview();

                        InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];
                        InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];
                        InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.duration = (int)row["ZVCRQ_duration"];
                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();
                        InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];

                        InterviewList.Add(InterviewDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewListByDate() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Get List of Scheduled Interview Details by Vacancy
        /// </summary>
        /// <param name="VacancyId">
        /// Unique ID of the Candidate fow which details are required.
        /// </param>/// 
        /// <param name="InterviewList">
        /// Fetched list of interviews will be filled in ref object of Interview List.
        /// </param>
        /// <param name="InterviewStatus">
        /// Interview status for which list is required.  
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Parameter Value</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>All Scheduled Interviews</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatus.InterviewProposed :: 1</term>
        /// <description>List of Proposed Interviews by Employer</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatusInterviewScheduled :: 2</term>
        /// <description>List of interview Scheduled by Universal Education Staff</description>
        /// </item>
        /// </list> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewStarted :: 3</term>
        /// <description>List of Started/running Interviews by Employer</description>
        /// </item>     
        /// <item>
        /// <term>InterviewCallStatus.InterviewCompleted :: 4</term>
        /// <description>List of Completed Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewDropped :: 5</term>
        /// <description>List of Dropped Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewCancelled :: 6</term>
        /// <description>List of Cancelled Interviews</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewRejected :: 7</term>
        /// <description>List of Rejected Interviews</description>
        /// </item>         
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        public static int GetInterviewListByVacancy(int VacancyId, ref List<Interview> InterviewList, int InterviewStatus = -1)
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
                dbCommand.CommandText = "sp_GetInterviewListByVacancyId";
                dbCommand.Parameters.AddWithValue("@VacancyId", VacancyId);             //Edited by Durgesh
                dbCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)              // Edited by  Durgesh 1  replaced  by 0                
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Interview InterviewDetails = new Interview();

                        InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];
                        InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];
                        InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();
                        if(row["ZVCRQ_duration"] != DBNull.Value)                               //Edited by Durgesh
                        InterviewDetails.ZoomVCMeetingRequest.duration = (int)row["ZVCRQ_duration"];
                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                    //    InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();
                        if(row["DurationInMinutes"] != DBNull.Value)                        // Edited by Durgesh
                        InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];

                        InterviewList.Add(InterviewDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInteriewListByVacancy() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Get List of Scheduled Interview Details by Employer ID
        /// </summary>
        /// <param name="EmployerId">
        /// Unique ID of the Candidate fow which details are required.
        /// </param>/// 
        /// <param name="InterviewList">
        /// Fetched list of interviews will be filled in ref object of Interview List.
        /// </param>
        /// <param name="InterviewStatus">
        /// Interview status for which list is required.  
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Parameter Value</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>All Scheduled Interviews</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatus.InterviewProposed :: 1</term>
        /// <description>List of Proposed Interviews by Employer</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatusInterviewScheduled :: 2</term>
        /// <description>List of interview Scheduled by Universal Education Staff</description>
        /// </item>
        /// </list> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewStarted :: 3</term>
        /// <description>List of Started/running Interviews by Employer</description>
        /// </item>     
        /// <item>
        /// <term>InterviewCallStatus.InterviewCompleted :: 4</term>
        /// <description>List of Completed Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewDropped :: 5</term>
        /// <description>List of Dropped Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewCancelled :: 6</term>
        /// <description>List of Cancelled Interviews</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewRejected :: 7</term>
        /// <description>List of Rejected Interviews</description>
        /// </item>         
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        public static int GetInterviewListByEmployer(string EmployerId, ref List<Interview> InterviewList, int InterviewStatus = -1)
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
                dbCommand.CommandText = "sp_GetInterviewListByEmployerId";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerId);
                dbCommand.Parameters.AddWithValue("@EInterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Interview InterviewDetails = new Interview();

                        InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];
                        InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];
                        InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();
                        if(row["ZVCRQ_duration"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.duration = Convert.ToInt32(row["ZVCRQ_duration"]);
                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                     //   InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();

                        if(row["DurationInMinutes"] != DBNull.Value)
                        InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];

                        InterviewList.Add(InterviewDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewListByEmployer() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Get List of Scheduled Interview Details by Candidate ID
        /// </summary>
        /// <param name="CandidateID">
        /// Unique ID of the Candidate fow which details are required.
        /// </param>/// 
        /// <param name="InterviewList">
        /// Fetched list of interviews will be filled in ref object of Interview List.
        /// </param>
        /// <param name="InterviewStatus">
        /// Interview status for which list is required.  
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Parameter Value</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>All Scheduled Interviews</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatus.InterviewProposed :: 1</term>
        /// <description>List of Proposed Interviews by Employer</description>
        /// </item>
        /// <item>
        /// <term>InterviewCallStatusInterviewScheduled :: 2</term>
        /// <description>List of interview Scheduled by Universal Education Staff</description>
        /// </item>
        /// </list> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewStarted :: 3</term>
        /// <description>List of Started/running Interviews by Employer</description>
        /// </item>     
        /// <item>
        /// <term>InterviewCallStatus.InterviewCompleted :: 4</term>
        /// <description>List of Completed Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewDropped :: 5</term>
        /// <description>List of Dropped Interviews by Employer</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewCancelled :: 6</term>
        /// <description>List of Cancelled Interviews</description>
        /// </item> 
        /// <item>
        /// <term>InterviewCallStatus.InterviewRejected :: 7</term>
        /// <description>List of Rejected Interviews</description>
        /// </item>         
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Interview details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Interview details cannot be fetched due to mismatch in InterviewID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Interview details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetInterviewListByCandidate(string CandidateID, ref List<Interview> InterviewList, int InterviewStatus = -1)
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
                dbCommand.CommandText = "sp_GetInterviewListByCandidateID";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateID);         //Edit by Krishna 
                dbCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Interview InterviewDetails = new Interview();

                        InterviewDetails.InterviewID = row["InterviewID"].ToString();
                        InterviewDetails.JobApplicationID = row["JobApplicationID"].ToString();
                        InterviewDetails.PreferredDateTime = (DateTime)row["PreferredDateTime"];
                        InterviewDetails.InterviewStatus = (int)row["InterviewStatus"];
                        InterviewDetails.EmployerRemarks = row["EmployerRemarks"].ToString();
                        InterviewDetails.UERemarks = row["UERemarks"].ToString();
                        InterviewDetails.CandidateRemarks = row["CandidateRemarks"].ToString();

                        if (row["ZVCRQ_duration"] != DBNull.Value)
                            InterviewDetails.ZoomVCMeetingRequest.duration = Convert.ToInt32(row["ZVCRQ_duration"]);

                        //InterviewDetails.ZoomVCMeetingRequest.password = row["ZVCRQ_password"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.start_time = row["ZVCRQ_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.timezone = row["ZVCRQ_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingRequest.topic = row["ZVCRQ_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.uuid = row["ZVCRS_uuid"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.id = row["ZVCRS_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_id = row["ZVCRS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.host_email = row["ZVCRS_host_email"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.topic = row["ZVCRS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.type = row["ZVCRS_type"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.status = row["ZVCRS_status"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_time = row["ZVCRS_start_time"].ToString();


                        InterviewDetails.ZoomVCMeetingResponse.duration = row["ZVCRS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.timezone = row["ZVCRS_timezone"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.created_at = row["ZVCRS_created_at"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.start_url = row["ZVCRS_start_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.join_url = row["ZVCRS_join_url"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.password = row["ZVCRS_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.h323_password = row["ZVCRS_h323_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pstn_password = row["ZVCRS_pstn_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.encrypted_password = row["ZVCRS_encrypted_password"].ToString();
                        InterviewDetails.ZoomVCMeetingResponse.pre_schedule = row["ZVCRS_pre_schedule"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.Status = row["ZVCMS_Status"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.duration = row["ZVCMS_duration"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.start_time = row["ZVCMS_start_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.end_time = row["ZVCMS_end_time"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.host_id = row["ZVCMS_host_id"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.dept = row["ZVCMS_dept"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.participants_count = row["ZVCMS_participants_count"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.source = row["ZVCMS_source"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.topic = row["ZVCMS_topic"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.total_minutes = row["ZVCMS_total_minutes"].ToString();
                       // InterviewDetails.ZoomVCMeetingStatus.type = row["type"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_email = row["ZVCMS_user_email"].ToString();
                        InterviewDetails.ZoomVCMeetingStatus.user_name = row["ZVCMS_user_name"].ToString();
                        InterviewDetails.ChosenTimeZone = row["ChosenTimeZone"].ToString();
                        InterviewDetails.DurationInMinutes = (int)row["DurationInMinutes"];

                        InterviewList.Add(InterviewDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewListByCandidate() : " + Message);

            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to create zoom video call for an interview. The function will fill ZoomVideoCallMeetingRequest and ZoomVideoCallMeetingResponse received from Zoom Meeting service APIs.
        /// </summary>
        /// <param name="interview">
        /// Details of the interview for which video call will be scheduled. 
        /// Compulsary details are interview.JobApplicationID, interview.ChosenTimeZone, interview.PreferredDateTime, interview.DurationInMinutes.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Zoom Video Meeting cannot be created due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Zoom Video Meeting cannot be created due to mismatch in Interview ID.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Zoom Video Meeting created successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int ScheduleInterviewVideoCall(ref Interview interview)      
        {
            int iRetValue = 0;
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>ScheduleInterviewVideoCall(" + interview.InterviewID + ")");
            Candidate CandidateDetails = new Candidate();
            Employer EmployerDetails = new Employer();
            JobApplication JADetails = new JobApplication(); 
            Vacancy VacancyDetails = new Vacancy();

            JobApplicationManager.GetJobApplicationDetails(interview.JobApplicationID, ref JADetails);
            VacancyManager.GetVacancyDetails(JADetails.VacancyID, ref VacancyDetails);
            CandidateManagement.GetCandidatePersonalDetails(JADetails.CandidateID, ref CandidateDetails);
            EmployerManagement.GetEmployerByID(VacancyDetails.UEEmployerID, ref EmployerDetails);

            interview.ZoomVCMeetingRequest.topic = "Interview of Mr. " + CandidateDetails.PersonalProfile.Name + " with " + EmployerDetails.BusinessName;
            interview.ZoomVCMeetingRequest.timezone = interview.ChosenTimeZone;
            interview.ZoomVCMeetingRequest.start_time = interview.PreferredDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            interview.ZoomVCMeetingRequest.type = 2;
            //interview.ZoomVCMeetingRequest.default_password = false;
            //interview.ZoomVCMeetingRequest.settings.auto_recording = "local";
            interview.ZoomVCMeetingRequest.duration = interview.DurationInMinutes;
            interview.ZoomVCMeetingRequest.host_email = EmployerDetails.email;
            string RequestStr = new JavaScriptSerializer().Serialize(interview.ZoomVCMeetingRequest);

            if (ZoomVideoCallMeetingManagment.CreateZoomVCMeeting(ref interview) == 1)
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
                try
                {
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataTable dtData = new DataTable();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "sp_CreateNewZoomMeeting";
                    dbCommand.Parameters.AddWithValue("@InterviewID", interview.InterviewID);
                    dbCommand.Parameters.AddWithValue("@UERemarks", interview.UERemarks);
                    dbCommand.Parameters.AddWithValue("@PreferredDateTime", interview.PreferredDateTime);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_duration", interview.ZoomVCMeetingRequest.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_start_time", interview.ZoomVCMeetingRequest.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_timezone", interview.ZoomVCMeetingRequest.timezone);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_topic", interview.ZoomVCMeetingRequest.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCRQ_CompleteRequest", RequestStr);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_uuid", interview.ZoomVCMeetingResponse.uuid);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_id", interview.ZoomVCMeetingResponse.id);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_host_id", interview.ZoomVCMeetingResponse.host_id);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_host_email", interview.ZoomVCMeetingResponse.host_email);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_topic", interview.ZoomVCMeetingResponse.topic);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_type", interview.ZoomVCMeetingResponse.type);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_status", interview.ZoomVCMeetingResponse.status);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_start_time", interview.ZoomVCMeetingResponse.start_time);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_duration", interview.ZoomVCMeetingResponse.duration);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_timezone", interview.ZoomVCMeetingResponse.timezone);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_created_at", interview.ZoomVCMeetingResponse.created_at);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_start_url", interview.ZoomVCMeetingResponse.start_url);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_join_url", interview.ZoomVCMeetingResponse.join_url);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_password", interview.ZoomVCMeetingResponse.password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_h323_password", interview.ZoomVCMeetingResponse.h323_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_pstn_password", interview.ZoomVCMeetingResponse.pstn_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_encrypted_password", interview.ZoomVCMeetingResponse.encrypted_password);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_pre_schedule", interview.ZoomVCMeetingResponse.pre_schedule);
                    dbCommand.Parameters.AddWithValue("@ZVCRS_CompleteRequest", new JavaScriptSerializer().Serialize(interview.ZoomVCMeetingResponse));
                    dbCommand.ExecuteNonQuery();

                    iRetValue = 1;

                    JobApplicationManager.UpdateJobApplicationStatus(interview.JobApplicationID, JobApplicationStatus.InterviewScheduled);
                    CandidateManagement.UpdateCandidateStatus(CandidateDetails.CandidateID, CandidateStatus.InterviewScheduled);
                    JobApplicationManager.UpdateJobApplicationStatus(JADetails.JobApplicationID, JobApplicationStatus.InterviewScheduled);
                    VacancyManager.UpdateVacancyStatus(VacancyDetails.VacancyID, cVacancyStatus.InProcess);

                    Notification notification = new Notification();
                    notification.NotificationType = cNotificationType.PersonalisedNotification;
                    notification.UEClientID = EmployerDetails.EmployerID;
                    notification.NotificationMessage = "Video call for interview request processed.";
                    notification.hyperlink = URLs.InterviewDetailsURL + iRetValue;
                    NotificationManagement.AddNewNotification(ref notification);


                    Notification CandNotification = new Notification();
                    CandNotification.NotificationType = cNotificationType.PersonalisedNotification;
                    CandNotification.UEClientID = CandidateDetails.CandidateID;
                    CandNotification.NotificationMessage = "An interview is scheduled with "+EmployerDetails.BusinessName+" for your job application for the post of "+VacancyDetails.VacancyName;
                    CandNotification.hyperlink = URLs.InterviewDetailsURL + iRetValue;
                    NotificationManagement.AddNewNotification(ref CandNotification);

                    WhatsAppManagement.SendMessage(CandidateDetails.ContactDetails.ContactNumberCountryCode.Trim()
                                                    + CandidateDetails.ContactDetails.ContactNumber.Trim(),
                                                    "An interview is scheduled for your job application for the post of " + VacancyDetails.VacancyName
                                                    + " on " + interview.ZoomVCMeetingRequest.start_time
                                                    + " with employer " + EmployerDetails.BusinessName
                                                    + " URL for VC is " + interview.ZoomVCMeetingResponse.join_url);

                    WhatsAppManagement.SendMessage(EmployerDetails.WhatsAppNumber.Trim(),
                                                    "An interview is scheduled for your vacancy " + VacancyDetails.VacancyName
                                                    + " at " + interview.ZoomVCMeetingRequest.start_time
                                                    + " with a candidate "
                                                    + " URL for VC is " + interview.ZoomVCMeetingResponse.start_url);

                    Email.SendEmployerInterviewNotification(interview.InterviewID);
                    Email.SendCandidateInterviewNotification(interview.InterviewID);
                }
                catch (Exception ex)
                {
                    iRetValue = -1;
                    string Message = "Error: " + ex.Message + "\r\n";
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Message = Message + t.ToString();
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>ScheduleInterviewVideoCall() : " + Message);

                }
                finally
                {
                    dbConnection.Close();
                }
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "<<<ScheduleInterviewVideoCall() :: " + iRetValue.ToString());
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
        public static int GetInterviewDashboardData(ref string DashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>GetInterviewDashboardData()");
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
                dbCommand.CommandText = "sp_GetInterviewDashboard";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("ProposedInterviews", (int)row["ProposedInterviews"]);
                        DashboardData.Add("ScheduledInterviews", (int)row["ScheduledInterviews"]);
                        DashboardData.Add("CompletedInterviews", (int)row["CompletedInterviews"]);
                        DashboardData.Add("DroppedInterviews", (int)row["DroppedInterviews"]);
                        DashboardData.Add("CancelledInterviews", (int)row["CancelledInterviews"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "GetInterviewDashboardData :: Successfully fetched Interview Dashboard Data.");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "GetInterviewDashboardData :: " + ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "<<<GetInterviewDashboardData :: " + iRetValue.ToString());
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
        public static int GetMaskedInterviewDetails(string InterviewID, ref cMaskedInterviewDetails maskedInterviewDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetMaskedInterviewDetails(" + InterviewID + ", ref Candidate CandidateDetail)");
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
                dbCommand.CommandText = "sp_GetMaskedInterviewDetails";
                dbCommand.Parameters.AddWithValue("@InteviewID", InterviewID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
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
                            maskedInterviewDetails.InterviewScheduleDate = DateTime.ParseExact(row["InterviewScheduleDate"].ToString(), "MM/dd/yyyy HH:mm:ss", new CultureInfo("fr-FR"));

                        maskedInterviewDetails.InteviewTimeZone = row["InteviewTimeZone"].ToString();
                        maskedInterviewDetails.ZoomMeetingId = row["ZoomMeetingId"].ToString();
                        maskedInterviewDetails.ZoomStartUrl = row["ZoomStartUrl"].ToString();
                        maskedInterviewDetails.ZoomMeetingStatus = row["ZoomMeetingStatus"].ToString();

                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewDetails :: Data : " + new JavaScriptSerializer().Serialize(maskedInterviewDetails));
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewDetails :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "GetMaskedInterviewDetails :: " + ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetMaskedInterviewDetails :: " + iRetValue.ToString());
            return iRetValue;
        }
    }
}