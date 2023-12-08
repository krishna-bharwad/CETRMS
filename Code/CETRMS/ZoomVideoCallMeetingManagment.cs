using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{
    public static class ZoomVideoCallMeetingManagment
    {
        /// <summary>
        /// Function to create Zoom Meeting. Meeting ID and other details like start_url, join_url, password etc will be filled in same interview object
        /// </summary>
        /// <param name="interview">
        /// Details of the interview for which zoom meeting will be created. Meeting ID and other details like start_url, join_url, password etc will be filled in same interview object
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Zoom Meeting cannot be created due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Zoom Meeting cannot be created due to bad request.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Zoom Meeting created successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int CreateZoomVCMeeting(ref Interview interview)
        {
            int iRetValue = 0;
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", ">>>CreateZoomVCMeeting(" + new JavaScriptSerializer().Serialize(interview).ToString() + ")");
            try
            {

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var apiSecret = ConfigurationManager.AppSettings["ZoomAPIKeySecret"];
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = ConfigurationManager.AppSettings["ZoomAPIKey"],
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/users/developer.cet22@gmail.com/meetings");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(interview.ZoomVCMeetingRequest);

                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;

                
                logger.log(logger.LogSeverity.INF, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "numericStatusCode::" + numericStatusCode.ToString());

                if (numericStatusCode == 201)
                {
                    var jObject = JObject.Parse(restResponse.Content);

                    // Read Response in Interview.ZoomVideoCallMeetingResponse object
                    interview.ZoomVCMeetingResponse.uuid = (string)jObject["uuid"];
                    interview.ZoomVCMeetingResponse.id = (string)jObject["id"];
                    interview.ZoomVCMeetingResponse.host_id = (string)jObject["host_id"];
                    interview.ZoomVCMeetingResponse.topic = (string)jObject["topic"];
                    interview.ZoomVCMeetingResponse.type = (string)jObject["type"];
                    interview.ZoomVCMeetingResponse.status = (string)jObject["status"];
                    interview.ZoomVCMeetingResponse.start_time = (string)jObject["start_time"];
                    interview.ZoomVCMeetingResponse.duration = (string)jObject["duration"];
                    interview.ZoomVCMeetingResponse.timezone = (string)jObject["timezone"];
                    interview.ZoomVCMeetingResponse.start_url = (string)jObject["start_url"];
                    interview.ZoomVCMeetingResponse.join_url = (string)jObject["join_url"];
                    interview.ZoomVCMeetingResponse.password = (string)jObject["password"];
                    interview.ZoomVCMeetingResponse.h323_password = (string)jObject["h323_password"];
                    interview.ZoomVCMeetingResponse.pstn_password = (string)jObject["pstn_password"];
                    interview.ZoomVCMeetingResponse.encrypted_password = (string)jObject["encrypted_password"];
                    interview.ZoomVCMeetingResponse.pre_schedule = (string)jObject["pre_schedule"];
                    interview.ZoomVCMeetingResponse.settings.host_video = (string)jObject["settings"]["host_video"];
                    interview.ZoomVCMeetingResponse.settings.participant_video = (string)jObject["settings"]["participant_video"];
                    interview.ZoomVCMeetingResponse.settings.cn_meeting = (string)jObject["settings"]["cn_meeting"];
                    interview.ZoomVCMeetingResponse.settings.in_meeting = (string)jObject["settings"]["in_meeting"];
                    interview.ZoomVCMeetingResponse.settings.join_before_host = (string)jObject["settings"]["join_before_host"];
                    interview.ZoomVCMeetingResponse.settings.jbh_time = (string)jObject["settings"]["jbh_time"];
                    interview.ZoomVCMeetingResponse.settings.mute_upon_entry = (string)jObject["settings"]["mute_upon_entry"];
                    interview.ZoomVCMeetingResponse.settings.watermark = (string)jObject["settings"]["watermark"];
                    interview.ZoomVCMeetingResponse.settings.use_pmi = (string)jObject["settings"]["use_pmi"];
                    interview.ZoomVCMeetingResponse.settings.approval_type = (string)jObject["settings"]["approval_type"];
                    interview.ZoomVCMeetingResponse.settings.audio = (string)jObject["settings"]["audio"];
                    interview.ZoomVCMeetingResponse.settings.auto_recording = (string)jObject["settings"]["auto_recording"];
                    interview.ZoomVCMeetingResponse.settings.enforce_login = (string)jObject["settings"]["enforce_login"];
                    interview.ZoomVCMeetingResponse.settings.enforce_login_domains = (string)jObject["settings"]["enforce_login_domains"];
                    interview.ZoomVCMeetingResponse.settings.alternative_hosts = (string)jObject["settings"]["alternative_hosts"];
                    interview.ZoomVCMeetingResponse.settings.alternative_host_update_polls = (string)jObject["settings"]["alternative_host_update_polls"];
                    interview.ZoomVCMeetingResponse.settings.close_registration = (string)jObject["settings"]["close_registration"];
                    interview.ZoomVCMeetingResponse.settings.show_share_button = (string)jObject["settings"]["show_share_button"];
                    interview.ZoomVCMeetingResponse.settings.allow_multiple_devices = (string)jObject["settings"]["allow_multiple_devices"];
                    interview.ZoomVCMeetingResponse.settings.registrants_confirmation_email = (string)jObject["settings"]["registrants_confirmation_email"];
                    interview.ZoomVCMeetingResponse.settings.waiting_room = (string)jObject["settings"]["waiting_room"];
                    interview.ZoomVCMeetingResponse.settings.request_permission_to_unmute_participants = (string)jObject["settings"]["request_permission_to_unmute_participants"];
                    interview.ZoomVCMeetingResponse.settings.registrants_email_notification = (string)jObject["settings"]["registrants_email_notification"];
                    interview.ZoomVCMeetingResponse.settings.meeting_authentication = (string)jObject["settings"]["meeting_authentication"];
                    interview.ZoomVCMeetingResponse.settings.encryption_type = (string)jObject["settings"]["encryption_type"];
                    interview.ZoomVCMeetingResponse.settings.alternative_hosts_email_notification = (string)jObject["settings"]["alternative_hosts_email_notification"];
                    interview.ZoomVCMeetingResponse.settings.device_testing = (string)jObject["settings"]["device_testing"];
                    interview.ZoomVCMeetingResponse.settings.focus_mode = (string)jObject["settings"]["focus_mode"];
                    interview.ZoomVCMeetingResponse.settings.enable_dedicated_group_chat = (string)jObject["settings"]["enable_dedicated_group_chat"];
                    interview.ZoomVCMeetingResponse.settings.private_meeting = (string)jObject["settings"]["private_meeting"];
                    interview.ZoomVCMeetingResponse.settings.email_notification = (string)jObject["settings"]["email_notification"];
                    interview.ZoomVCMeetingResponse.settings.host_save_video_order = (string)jObject["settings"]["host_save_video_order"];

                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, "", "<<<CreateZoomVCMeeting() :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get status of zoom meeting. Status of the meeting will be filled in passed interview object.
        /// </summary>
        /// <param name="interview">
        /// Details of the interview.Status of the meeting will be filled in passed interview object.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Zoom Meeting status cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Zoom Meeting status cannot be fetched  due to bad request.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Zoom Meeting status fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetZoomVCStatus(ref Interview interview)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                if (interview.InterviewStatus == InterviewCallStatus.InterviewCancelled)
                    return RetValue.Success;

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var apiSecret = ConfigurationManager.AppSettings["ZoomAPIKeySecret"];
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = ConfigurationManager.AppSettings["ZoomAPIKey"],
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                var client = new RestClient();
                //DateTime MeetingStartTime = DateTime.ParseExact(interview.ZoomVCMeetingResponse.start_time, "MM/dd/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime MeetingStartTime = DateTime.Parse(interview.ZoomVCMeetingResponse.start_time);
                //if (MeetingStartTime >= System.DateTime.Now )
                client = new RestClient("https://api.zoom.us/v2/past_meetings/" + interview.ZoomVCMeetingResponse.id);
                
                //else
                var request = new RestRequest();
                request.Method = Method.Get;
                request.RequestFormat = DataFormat.None;

                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;

                if(numericStatusCode != 200)
                {
                    client = new RestClient("https://api.zoom.us/v2/meetings/" + interview.ZoomVCMeetingResponse.id);
                    request.Method = Method.Get;
                    request.RequestFormat = DataFormat.None;

                    request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                    restResponse = client.Execute(request);
                    statusCode = restResponse.StatusCode;
                    numericStatusCode = (int)statusCode;

                }

                if (numericStatusCode == 200)
                {
                    var jObject = JObject.Parse(restResponse.Content);
                    interview.ZoomVCMeetingStatus.Status = (string)jObject["status"];
                    interview.ZoomVCMeetingStatus.id = (string)jObject["id"];
                    interview.ZoomVCMeetingStatus.uuid = (string)jObject["uuid"];
                    interview.ZoomVCMeetingStatus.duration = (string)jObject["duration"];
                    interview.ZoomVCMeetingStatus.start_time = (string)jObject["start_time"];
                    interview.ZoomVCMeetingStatus.end_time = (string)jObject["end_time"];
                    interview.ZoomVCMeetingStatus.host_id = (string)jObject["host_id"];
                    interview.ZoomVCMeetingStatus.dept = (string)jObject["dept"];
                    interview.ZoomVCMeetingStatus.participants_count = (string)jObject["participants_count"];
                    interview.ZoomVCMeetingStatus.source = (string)jObject["source"];
                    interview.ZoomVCMeetingStatus.topic = (string)jObject["topic"];
                    interview.ZoomVCMeetingStatus.total_minutes = (string)jObject["total_minutes"];
                    interview.ZoomVCMeetingStatus.type = (string)jObject["type"];
                    interview.ZoomVCMeetingStatus.user_email = (string)jObject["user_email"];
                    interview.ZoomVCMeetingStatus.user_name = (string)jObject["user_name"];

                    using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString))
                    {
                        dbConnection.Open();
                        string query = string.Empty; ;
                        if (interview.ZoomVCMeetingStatus.Status != "waiting")
                        {
                            interview.InterviewStatus = InterviewCallStatus.InterviewCompleted;
                            interview.ZoomVCMeetingResponse.start_time = interview.ZoomVCMeetingStatus.start_time;
                            JobApplication JADetails = new JobApplication();
                            Candidate candidate = new Candidate();

                            JobApplicationManager.GetJobApplicationDetails(interview.JobApplicationID, ref JADetails);
                            CandidateManagement.GetCandidatePersonalDetails(JADetails.CandidateID, ref candidate);
                            if (candidate.PersonalProfile.Status == CandidateStatus.InterviewScheduled.ToString())
                                CandidateManagement.UpdateCandidateStatus(JADetails.CandidateID, CandidateStatus.InterviewAppeared);
                            if (JADetails.ApplicationStatus == JobApplicationStatus.InterviewScheduled)
                                JobApplicationManager.UpdateJobApplicationStatus(JADetails.JobApplicationID, JobApplicationStatus.InterviewCompleted);

                            query = "Update UEInterviews set InterviewStatus = " + InterviewCallStatus.InterviewCompleted
                                + ", ZVCRS_start_time = '" + interview.ZoomVCMeetingStatus.start_time+"'"
                                + " where InterviewID = " + interview.InterviewID;
                        }
                        else
                        {
                            interview.InterviewStatus = InterviewCallStatus.InterviewScheduled;
                            interview.ZoomVCMeetingResponse.start_time = interview.ZoomVCMeetingStatus.start_time;
                            query = "Update UEInterviews set InterviewStatus = " + InterviewCallStatus.InterviewScheduled
                                + ", ZVCRS_start_time = '" + interview.ZoomVCMeetingStatus.start_time + "'"
                                + " where InterviewID = " + interview.InterviewID;
                        }
                        SqlCommand dbcmd = new SqlCommand(query, dbConnection);
                        dbcmd.ExecuteNonQuery();
                    }
                    using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString))
                    {
                        dbConnection.Open();

                        string query = "sp_UpdateZoomMeetingStatus";
                        SqlCommand dbcmd = new SqlCommand(query, dbConnection);
                        dbcmd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbcmd.Parameters.AddWithValue("@InterviewID", interview.InterviewID);
                        
                        if (interview.ZoomVCMeetingStatus.Status != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_Status", interview.ZoomVCMeetingStatus.Status);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_Status", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.duration != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_duration", interview.ZoomVCMeetingStatus.duration);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_duration", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.start_time != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_start_time", interview.ZoomVCMeetingStatus.start_time);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_start_time", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.start_time != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_end_time", interview.ZoomVCMeetingStatus.start_time);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_end_time", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.host_id != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_host_id", interview.ZoomVCMeetingStatus.host_id);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_host_id", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.dept != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_dept", interview.ZoomVCMeetingStatus.dept);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_dept", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.participants_count != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_participants_count", interview.ZoomVCMeetingStatus.participants_count);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_participants_count", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.source != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_source", interview.ZoomVCMeetingStatus.source);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_source", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.topic != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_topic", interview.ZoomVCMeetingStatus.topic);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_topic", DBNull.Value);

                        if(interview.ZoomVCMeetingStatus.total_minutes != null)
                        dbcmd.Parameters.AddWithValue("@ZVCMS_total_minutes", interview.ZoomVCMeetingStatus.total_minutes);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_total_minutes", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.type != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_type", interview.ZoomVCMeetingStatus.type);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_type", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.user_email != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_user_email", interview.ZoomVCMeetingStatus.user_email);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_user_email", DBNull.Value);

                        if (interview.ZoomVCMeetingStatus.user_name != null)
                            dbcmd.Parameters.AddWithValue("@ZVCMS_user_name", interview.ZoomVCMeetingStatus.user_name);
                        else
                            dbcmd.Parameters.AddWithValue("@ZVCMS_user_name", DBNull.Value);

                        dbcmd.ExecuteNonQuery();
                    }
                    iRetValue = RetValue.Success;
                }
                else
                {
                    //  Temporary Commented for mail testing reason
                    interview.InterviewStatus = InterviewCallStatus.InterviewDropped;
                    interview.ZoomVCMeetingStatus.Status = "Meeting not exists";
                    JobApplication JADetails = new JobApplication();
                    Candidate candidate = new Candidate();

                    JobApplicationManager.GetJobApplicationDetails(interview.JobApplicationID, ref JADetails);
                    CandidateManagement.GetCandidatePersonalDetails(JADetails.CandidateID, ref candidate);
                    if (candidate.PersonalProfile.Status == CandidateStatus.InterviewScheduled.ToString())
                        CandidateManagement.UpdateCandidateStatus(JADetails.CandidateID, CandidateStatus.InterviewDropped);
                    if (JADetails.ApplicationStatus == JobApplicationStatus.InterviewScheduled)
                        JobApplicationManager.UpdateJobApplicationStatus(JADetails.JobApplicationID, JobApplicationStatus.InterviewDropped);
                    
                    using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString))
                    {
                        dbConnection.Open();
                        string query = "Update UEInterviews set InterviewStatus = " + InterviewCallStatus.InterviewDropped + " where InterviewID = " + interview.InterviewID;
                        SqlCommand dbcmd = new SqlCommand(query, dbConnection);
                        dbcmd.ExecuteNonQuery();
                        interview.ZoomVCMeetingStatus.Status = "Meeting not completed.";
                    }
                    iRetValue = RetValue.NoRecord;
                }
            }
            catch(Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return iRetValue;
        }
        /// <summary>
        /// Function to cancel zoom meeting.
        /// </summary>
        /// <param name="interview">
        /// Details of the interview which must have zoom meeting id
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Zoom Meeting status cannot be cancelled due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Zoom Meeting status cannot be cancelled  due to bad request.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Zoom Meeting status cancelled successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int CancelVCMeeting(ref Interview interview)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var apiSecret = ConfigurationManager.AppSettings["ZoomAPIKeySecret"];
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = ConfigurationManager.AppSettings["ZoomAPIKey"],
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/meetings/" + interview.ZoomVCMeetingResponse.id);
                var request = new RestRequest();
                request.Method = Method.Delete;
                request.RequestFormat = DataFormat.None;

                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode == 204)
                {
                    //var jObject = JObject.Parse(restResponse.Content);
                    interview.InterviewStatus = InterviewCallStatus.InterviewCancelled;
                    interview.ZoomVCMeetingStatus.Status = "Meeting Cancelled";
                    JobApplication JADetails = new JobApplication();
                    Candidate candidate = new Candidate();

                    JobApplicationManager.GetJobApplicationDetails(interview.JobApplicationID, ref JADetails);
                    CandidateManagement.GetCandidatePersonalDetails(JADetails.CandidateID, ref candidate);
                    if (candidate.PersonalProfile.Status == CandidateStatus.InterviewScheduled.ToString())
                        CandidateManagement.UpdateCandidateStatus(JADetails.CandidateID, CandidateStatus.AvailableForOpening);
                    if (JADetails.ApplicationStatus == JobApplicationStatus.InterviewScheduled)
                        JobApplicationManager.UpdateJobApplicationStatus(JADetails.JobApplicationID, JobApplicationStatus.InterviewCancelled);

                    using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString))
                    {
                        dbConnection.Open();
                        string query = "Update UEInterviews set InterviewStatus = " + InterviewCallStatus.InterviewCancelled + " where InterviewID = " + interview.InterviewID;
                        SqlCommand dbcmd = new SqlCommand(query, dbConnection);
                        dbcmd.ExecuteNonQuery();
                        interview.ZoomVCMeetingStatus.Status = "Meeting cancelled.";
                    }
                    iRetValue = RetValue.Success;
                }
                else
                    iRetValue = RetValue.NoRecord;
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return iRetValue;
        }
        /// <summary>
        /// Function to reschedule zoom meeting.
        /// </summary>
        /// <param name="interview">
        /// Details of the interview which must have zoom meeting id
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Zoom Meeting status cannot be reschedule due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Zoom Meeting status cannot be reschedule  due to bad request.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Zoom Meeting status reschedule successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int ReScheduleVCMeeting(ref Interview interview)
        {
            int iRetValue = 0;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var apiSecret = ConfigurationManager.AppSettings["ZoomAPIKeySecret"];
                byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = ConfigurationManager.AppSettings["ZoomAPIKey"],
                    Expires = now.AddSeconds(300),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var client = new RestClient("https://api.zoom.us/v2/meetings/" + interview.ZoomVCMeetingResponse.id);
                var request = new RestRequest();
                request.Method = Method.Patch;
                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(interview.ZoomVCMeetingRequest);


                request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
                RestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode;
                int numericStatusCode = (int)statusCode;

                if (numericStatusCode == 204)
                {
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
            finally
            {

            }
            return iRetValue;
        }
    }
}