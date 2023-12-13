using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class GetInterviewDetails : System.Web.UI.Page
    {
        static Interview interview = new Interview();
        JobApplication jobApplication = new JobApplication();
        Employer employer = new Employer();
        Vacancy vacancy = new Vacancy();
        Candidate candidate = new Candidate();
        cState cstate = new cState();
        cState estate = new cState();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (Request.QueryString["NotificationID"] != null)
                {
                    NotificationManagement.MarkNotificationRead(Request.QueryString["NotificationID"].ToString());
                }
                if (!IsPostBack)
                {
                    GetInterviewDetail();
                    ScheduledInterviewButtonValidation();

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void ScheduledInterviewButtonValidation()
        {
            try
            {
                MeetingStartTimeTXT.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

                if (interview.InterviewStatus == InterviewCallStatus.InterviewProposed)
                {
                    RescheduleInterviewLB.Visible = false;
                    ScheduleInterviewLB.Visible = true;
                }
                else
                {
                    RescheduleInterviewLB.Visible = true;
                    ScheduleInterviewLB.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void GetInterviewDetail()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), ">>>GetInterviewDetail()");
            try
            {
                string InterviewId = Request.QueryString["InterviewID"];
                InterviewManagement.GetInterviewDetails(InterviewId, ref interview);

                string JobApplicationId = interview.JobApplicationID;
                JobApplicationManager.GetJobApplicationDetails(JobApplicationId, ref jobApplication);

                string CandidateId = jobApplication.CandidateID;
                CandidateManagement.GetCandidateFullDetails(CandidateId, ref candidate, true);

                string VacancyId = jobApplication.VacancyID;
                VacancyManager.GetVacancyDetails(VacancyId, ref vacancy);

                string EmployerId = vacancy.CETEmployerId;
                EmployerManagement.GetEmployerByID(EmployerId, ref employer, true);

                string locationCode = candidate.ContactDetails.CurrentStateCode;
                cstate = LocationManagement.GetStateDetail(locationCode);

                string Emplocationcode = employer.LocationStateCode;
                if (employer.LocationCityCode != null)
                    estate = LocationManagement.GetStateDetail(Emplocationcode);

                if (interview.InterviewStatus != InterviewCallStatus.InterviewProposed && interview.InterviewStatus != InterviewCallStatus.InterviewCancelled)
                {
                    ZoomVideoCallMeetingManagment.GetZoomVCStatus(ref interview);
                    Topic.Text = interview.ZoomVCMeetingResponse.topic;
                    StartURLHL.Text = interview.ZoomVCMeetingResponse.start_url;
                    StartURLHL.NavigateUrl = interview.ZoomVCMeetingResponse.start_url;
                    HostURL.Text = interview.ZoomVCMeetingResponse.join_url;
                    Password.Text = interview.ZoomVCMeetingResponse.encrypted_password;
                    AppPassword.Text = interview.ZoomVCMeetingResponse.password;
                    MeetingIDLBL.Text = interview.ZoomVCMeetingResponse.id;
                    ZVSchduledStartTimeLBL.Text = interview.ZoomVCMeetingResponse.start_time;
                    ZVTimeZoneLBL.Text = interview.ZoomVCMeetingResponse.timezone;
                }

                switch (interview.InterviewStatus)
                {
                    case InterviewCallStatus.InterviewProposed:
                        InterviewStatusLBL.Text = "Interview Proposed";
                        VcStatus.Text = "Meeting not scheduled";
                        ScheduleMeetingPanel.Visible = true;
                        NotificationPanel.Visible = false;
                        MeetingProposedStatusPanel.Visible = false;
                        MeetingCompletionStatusPanel.Visible = false;
                        VcStatus.Text = "Not yet Schduled";
                        break;
                    case InterviewCallStatus.InterviewScheduled:
                        InterviewStatusLBL.Text = "Interview Scheduled";
                        ScheduleMeetingPanel.Visible = true;
                        NotificationPanel.Visible = true;
                        CancelInterview.Enabled = true;
                        MeetingProposedStatusPanel.Visible = true;
                        MeetingCompletionStatusPanel.Visible = false;
                        VcStatus.Text = "waiting";
                        break;
                    case InterviewCallStatus.InterviewStarted:
                        InterviewStatusLBL.Text = "Interview Started";
                        ScheduleMeetingPanel.Visible = false;
                        NotificationPanel.Visible = true;
                        MeetingProposedStatusPanel.Visible = true;
                        MeetingCompletionStatusPanel.Visible = false;
                        VcStatus.Text = "Started";
                        break;
                    case InterviewCallStatus.InterviewCompleted:
                        InterviewStatusLBL.Text = "Interview Completed";
                        ScheduleMeetingPanel.Visible = false;
                        NotificationPanel.Visible = false;
                        MeetingProposedStatusPanel.Visible = true;
                        MeetingCompletionStatusPanel.Visible = true;
                        VcStatus.Text = "Completed";
                        ZVStartTime.Text = interview.ZoomVCMeetingStatus.start_time;
                        ParticipantCount.Text = interview.ZoomVCMeetingStatus.participants_count;
                        EndTime.Text = interview.ZoomVCMeetingStatus.end_time;
                        TotalMinute.Text = interview.ZoomVCMeetingStatus.duration;
                        break;
                    case InterviewCallStatus.InterviewDropped:
                        InterviewStatusLBL.Text = "Interview Dropped";
                        VcStatus.Text = "Meeting not scheduled";
                        NotificationPanel.Visible = false;
                        ScheduleMeetingPanel.Visible = false;
                        MeetingProposedStatusPanel.Visible = true;
                        MeetingCompletionStatusPanel.Visible = false;
                        VcStatus.Text = "Dropped";
                        break;
                    case InterviewCallStatus.InterviewCancelled:
                        InterviewStatusLBL.Text = "Interview Cancelled";
                        VcStatus.Text = "Meeting not scheduled";
                        NotificationPanel.Visible = false;
                        ScheduleMeetingPanel.Visible = false;
                        MeetingCompletionStatusPanel.Visible = false;
                        MeetingProposedStatusPanel.Visible = true;
                        VcStatus.Text = "Cancelled";
                        break;
                    case InterviewCallStatus.InterviewRejected:
                        InterviewStatusLBL.Text = "Interview Rejected";
                        VcStatus.Text = "Meeting not scheduled";
                        ScheduleMeetingPanel.Visible = false;
                        NotificationPanel.Visible = false;
                        MeetingCompletionStatusPanel.Visible = false;
                        MeetingProposedStatusPanel.Visible = true;
                        VcStatus.Text = "Rejected";
                        break;
                }
                int employerStatus = Convert.ToInt32(employer.EmployerStatus);
                switch (employerStatus)
                {
                    case EmployerStatus.NewRegistration:
                        EmployerStatusLBL.Text = "NewRegistration";
                        break;
                    case EmployerStatus.RegistrationComplete:
                        EmployerStatusLBL.Text = "RegistrationComplete";
                        break;
                    case EmployerStatus.RegistrationFeePaid:
                        EmployerStatusLBL.Text = "RegistrationFeePaid";
                        break;
                    case EmployerStatus.OpenVacancy:
                        EmployerStatusLBL.Text = "OpenVacancy";
                        break;
                    case EmployerStatus.InProcessVacancy:
                        EmployerStatusLBL.Text = "InProcessVacancy ";
                        break;
                    case EmployerStatus.RecruitmentFeePaid:
                        EmployerStatusLBL.Text = "RecruitmentFeePaid";
                        break;
                    case EmployerStatus.FilledVacancy:
                        EmployerStatusLBL.Text = "FilledVacancy";
                        break;
                    case EmployerStatus.RegistrationRenewalPending:
                        EmployerStatusLBL.Text = "RegistrationRenewalPending";
                        break;
                    case EmployerStatus.RegistrationRenewalFeePaid:
                        EmployerStatusLBL.Text = "RegistrationRenewalFeePaid";
                        break;
                    case EmployerStatus.InActive:
                        EmployerStatusLBL.Text = "InActive";
                        break;
                }

                int candidateStatus = Convert.ToInt32(candidate.PersonalProfile.Status);

                switch (candidateStatus)
                {
                    case CandidateStatus.NewRegistration:
                        CandidateStatusLBL.Text = "NewRegistration";
                        break;
                    case CandidateStatus.ProfileCreated:
                        CandidateStatusLBL.Text = "ProfileCreated";
                        break;
                    case CandidateStatus.RegistrationFeePaid:
                        CandidateStatusLBL.Text = "RegistrationFeePaid";
                        break;
                    case CandidateStatus.AppliedtoVacancy:
                        CandidateStatusLBL.Text = "AppliedtoVacancy";
                        break;
                    case CandidateStatus.InterviewScheduled:
                        CandidateStatusLBL.Text = "InterviewScheduled ";
                        break;
                    case CandidateStatus.InterviewAppeared:
                        CandidateStatusLBL.Text = "InterviewAppeared";
                        break;
                    case CandidateStatus.RecruitmentFeePaid:
                        CandidateStatusLBL.Text = "RecruitmentFeePaid";
                        break;
                    case CandidateStatus.Hired:
                        EmployerStatusLBL.Text = "Hired";
                        break;
                    case CandidateStatus.Rejected:
                        EmployerStatusLBL.Text = "Rejected";
                        break;
                    case CandidateStatus.AvailableForOpening:
                        EmployerStatusLBL.Text = "AvailableForOpening";
                        break;
                    case CandidateStatus.RegistrationRenewalPending:
                        EmployerStatusLBL.Text = "RegistrationRenewalPending";
                        break;
                    case CandidateStatus.RegistrationRenewalFeePaid:
                        EmployerStatusLBL.Text = "RegistrationRenewalFeePaid";
                        break;
                    case CandidateStatus.InActive:
                        EmployerStatusLBL.Text = "InActive";
                        break;
                }
                //int jobApplicationStatus = jobApplication.ApplicationStatus;
                //switch (jobApplicationStatus)
                //{
                //    case JobApplicationStatus.NewApplication:
                //        JobApplicationStatusLBL.Text = "NewApplication";
                //        break;
                //    case JobApplicationStatus.ApplicationViewed:
                //        JobApplicationStatusLBL.Text = "ApplicationViewed";
                //        break;
                //    case JobApplicationStatus.InterviewScheduled:
                //        JobApplicationStatusLBL.Text = "InterviewScheduled";
                //        break;
                //    case JobApplicationStatus.Selected:
                //        JobApplicationStatusLBL.Text = "Selected";
                //        break;
                //    case JobApplicationStatus.Hired:
                //        JobApplicationStatusLBL.Text = "Hired";
                //        break;
                //    case JobApplicationStatus.Rejected:
                //        JobApplicationStatusLBL.Text = "Rejected";
                //        break;
                //    case JobApplicationStatus.InterviewRequested:
                //        JobApplicationStatusLBL.Text = "InterviewRequested";
                //        break;
                //    case JobApplicationStatus.InterviewCancelled:
                //        JobApplicationStatusLBL.Text = "InterviewCancelled";
                //        break;
                //}
                int vacancyStatus = Convert.ToInt32(vacancy.VacancyStatusTypeID);
                switch (vacancyStatus)
                {
                    case cVacancyStatus.Open:
                        VacancyStatusLBL.Text = "Open";
                        break;
                    case cVacancyStatus.Filled:
                        VacancyStatusLBL.Text = "Filled";
                        break;
                    case cVacancyStatus.Pause:
                        VacancyStatusLBL.Text = "Pause";
                        break;
                    case cVacancyStatus.Close:
                        VacancyStatusLBL.Text = " Close";
                        break;
                    case cVacancyStatus.InProcess:
                        VacancyStatusLBL.Text = "InProcess";
                        break;
                }
                EmployerNameHL.Text = employer.Name;
                EmployerLocationLBL.Text = estate.Country.CountryName;
                EmployerNameHL.NavigateUrl = URLs.EmployerDetailsURL + employer.EmployerID;
                EmployerEmailLBL.Text = employer.email;
                EmployerContactNoLBL.Text = employer.WhatsAppNumber;
                cState EmployerLocation = new cState();
                EmployerLocation = LocationManagement.GetStateDetail(employer.LocationStateCode);
                EmployerLocationLBL.Text = EmployerLocation.StateName + ", " + EmployerLocation.Country.CountryName;
                string EmployerPhotoMem = string.Empty;
                if (employer.BusinessLogo != null)
                {
                    EmployerPhotoMem = Convert.ToBase64String(employer.BusinessLogo);
                    BusinessLogo_IMG.ImageUrl = String.Format("data:image/jpg;base64,{0}", EmployerPhotoMem);
                }
                else
                {
                    BusinessLogo_IMG.ImageUrl = "images/user.png";
                }


                VacanyNameHL.Text = vacancy.VacancyName;
                VacanyNameHL.NavigateUrl = URLs.VacancyDetailsURL + vacancy.VacancyID;
                if (vacancy.VacancyDetails.Length >= 100)
                    VacancyDetailsLBL.Text = vacancy.VacancyDetails.Substring(0, 100) + "...";
                else
                    VacancyDetailsLBL.Text = vacancy.VacancyDetails;
                cState state = new cState();
                state = LocationManagement.GetStateDetail(vacancy.PrimaryLocation);
                VacancyLocationLBL.Text = state.StateName + ", " + state.Country.CountryName;

                CandidateNameHL.Text = candidate.PersonalProfile.Name;
                CandidateLocationLBL.Text = cstate.Country.CountryName;
                CandidateNameHL.NavigateUrl = URLs.CandidateDetailsURL + candidate.CandidateID;
                string CandidatePhotoMem = string.Empty;
                if (candidate.PersonalProfile.Photo != null)
                {
                    CandidatePhotoMem = Convert.ToBase64String(candidate.PersonalProfile.Photo);
                    Candidate_IMG.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoMem);
                }
                else
                {
                    Candidate_IMG.ImageUrl = "images/user.png";
                }
                CandidatePhoneLBL.Text = candidate.ContactDetails.ContactNumber;
                CandidateEmailLBL.Text = candidate.PersonalProfile.CandidateEmail;

                InterviewIDLBL.Text = interview.InterviewID;
                ChosenTimeZoneLBL.Text = string.Format("{0}", interview.ChosenTimeZone);
                DurationInMinutesLBL.Text = string.Format("{0}", interview.DurationInMinutes);
                // PreferredDateTimeLBL.Text = string.Format("{0}", interview.PreferredDateTime);
                PreferredDateTimeLBL.Text = interview.PreferredDateTime.ToString("MM-dd-yyyy hh:mm:ss");
                /*------------------------------*/
                if (interview.PreferredDateTime > System.DateTime.Now)
                    MeetingStartTimeTXT.Text = interview.PreferredDateTime.ToString("yyyy-MM-ddThh:mm:ss.ss");
                else
                    MeetingStartTimeTXT.Text = System.DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ss");
                /*------------------------------*/

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), "<<<GetInterviewDetail()");
        }

        protected void ScheduleInterview_btn(object send, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), ">>>ScheduleInterview_btn()");
            try
            {
                if (MeetingStartTimeTXT.Text != "")
                {
                    interview.PreferredDateTime = Convert.ToDateTime(MeetingStartTimeTXT.Text);
                    if (InterviewManagement.ScheduleInterviewVideoCall(ref interview) == 1)
                    {
                        GetInterviewDetail();
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), "<<<ScheduleInterview_btn()");
        }
        protected void RescheduleInterview_btn(object send, EventArgs e)
        {
            try
            {
                int RValue;
                if (MeetingStartTimeTXT.Text != "")
                {
                    interview.PreferredDateTime = Convert.ToDateTime(MeetingStartTimeTXT.Text);
                    RValue = InterviewManagement.ReScheduleInterview(interview);
                    if (RValue == 1)
                    {
                        GetInterviewDetail();
                        Console.WriteLine("Interview Rescheduled Successfully on {0}", interview.PreferredDateTime);
                    }
                    else
                    {
                        Console.WriteLine("Error in rescheduling interview");
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void SendEmailToIndivisuals(object send, EventArgs e)
        {
            try
            {
                if (Mail_CheckBox.Checked)
                {
                    Email.SendEmployerInterviewNotification(interview.InterviewID);
                    Email.SendCandidateInterviewNotification(interview.InterviewID);
                    MailCandidate_Lbl.Text = Email.SendCandidateInterviewNotification(interview.InterviewID);
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void CancelInterview_btn(object send, EventArgs e)
        {
            try
            {
                ZoomVideoCallMeetingManagment.CancelVCMeeting(ref interview);
                GetInterviewDetail();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
    }
} 

