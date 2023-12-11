using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "admin", ">>>OpenDashboard");
                if (!IsPostBack)
                {
                    UpdateData();
                    GetCandidateDashboardData();
                    GetEmployerDashboardData();
                    GetVacancyDashboardData();
                    GetInterviewDashboardData();
                    GetPaymentDashboardData();
                }
                logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "admin", "<<<OpenDashboard");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
        }
        protected void GetCandidateDashboardData()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetCandidateDashboardData");
            try
            {

                string DataJson = string.Empty;
                CandidateManagement.GetCandidateDashboardData(ref DataJson);
                dynamic CandidateDashboardData = JsonConvert.DeserializeObject(DataJson);
                //NewRegistrationLBL.Text = CandidateDashboardData.NewRegistration;
                CandidateCompletedDetailsLBL.Text = CandidateDashboardData.CandidateCompletedDetails;
                CandidateUnderSelectionProcessLBL.Text = CandidateDashboardData.CandidateUnderSelectionProcess;
                CandidateFinalSelectedLBL.Text = CandidateDashboardData.CandidateFinalSelected;
                CandidateRejectedLBL.Text = CandidateDashboardData.CandidateRejected;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetCandidateDashboardData");
        }

        protected void GetEmployerDashboardData()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetEmployerDashboardData");
            try
            {
                string DataJson = string.Empty;
                EmployerManagement.GetEmployerDashboardData(ref DataJson);
                dynamic EmployerDashboardData = JsonConvert.DeserializeObject(DataJson);
                EmployerOnBoardLBL.Text = EmployerDashboardData.EmployerOnBoarded;
                ActiveEmployersWithOpenVacancyLBL.Text = EmployerDashboardData.ActiveEmployersWithOpenVacancy;
                InactiveEmployerWithFilledVacacnyLBL.Text = EmployerDashboardData.InactiveEmployerWithFilledVacacny;
                EmployersWithNoVacancyLBL.Text = EmployerDashboardData.EmployersWithNoVacancy;
                EmployerInProcessVacancies.Text = EmployerDashboardData.EmployersInProcessVacancies;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetEmployerDashboardData");
        }

        protected void GetVacancyDashboardData()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetVacancyDashboardData");
            try
            {
                string DataJson = string.Empty;
                VacancyManager.GetVacancyDashboardData(ref DataJson);
                dynamic VacancyDashboardData = JsonConvert.DeserializeObject(DataJson);
                TotalVacanciesLBL.Text = VacancyDashboardData.TotalVacancies;
                OpenVacanciesLBL.Text = VacancyDashboardData.OpenVacancies;
                VacanciesUnderScheduledInterviewLBL.Text = VacancyDashboardData.VacanciesUnderScheduledInterview;
                CloseVacanciesAfterFinalSelectionLBL.Text = VacancyDashboardData.CloseVacanciesAfterFinalSelection;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetVacancyDashboardData");
        }
        protected void GetInterviewDashboardData()
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetInterviewDashboardData");
            try
            {
                string DataJson = string.Empty;
                InterviewManagement.GetInterviewDashboardData(ref DataJson);
                dynamic InterviewDashboardData = JsonConvert.DeserializeObject(DataJson);
                InterviewProposedLBL.Text = InterviewDashboardData.ProposedInterviews;
                InterviewScheduledLBL.Text = InterviewDashboardData.ScheduledInterviews;
                InterviewCompletedLBL.Text = InterviewDashboardData.CompletedInterviews;
                InterviewDroppedLBL.Text = InterviewDashboardData.DroppedInterviews;
                InterviewCancelledLBL.Text = InterviewDashboardData.CancelledInterviews;
                InterviewStartedLBL.Text = 0.ToString();
                InterviewRejectedLBL.Text = 0.ToString();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetInterviewDashboardData");
        }
        protected void GetPaymentDashboardData() //Prashant
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetPaymentDashboardData");
            try
            {
                string DataJson = string.Empty;
                if (PaymentManagement.GetPaymentDashboardData(ref DataJson) == RetValue.Success)
                {
                    dynamic PaymentDashboardData = JsonConvert.DeserializeObject(DataJson);
                    TotalPaymentRequestedLBL.Text = PaymentDashboardData.TotalPaymentRequested;
                    TotalPaymentReceivedLBL.Text = PaymentDashboardData.TotalPaymentReceived;
                    TotalOutstandingPaymentWithinTimeLimitLBL.Text = PaymentDashboardData.TotalOutstandingPaymentWithinTimeLimit;
                    TotalOutstandingPaymentOutOfTimeLimitLBL.Text = PaymentDashboardData.TotalOutstandingPaymentOutOfTimeLimit;
                    //InterviewCancelledLBL.Text = PaymentDashboardData.TotalPaymentDueFinalization;
                    TotalPaymentNotificationGeneratedLBL.Text = PaymentDashboardData.TotalPaymentNotificationGenerated;
                    TotalPaymentTransactionDoneLBL.Text = PaymentDashboardData.TotalPaymentTransactionsDone;
                    TotalPaymentTransactionOverDueLBL.Text = PaymentDashboardData.TotalPaymentTransactionsDue;
                    TotalTransferReferralDetailsLBL.Text = PaymentDashboardData.TotalReferralDetailsTransfer;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetPaymentDashboardData");
        }

        //protected void CandidateNewRegistration_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/CandidateList.aspx?CandidateStatus=1");
        //}

        protected void CandidateCompletedDetailsLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Candidate", ">>>CandidateCompletedDetails");
            try
            {
                Response.Redirect("~/CandidateList.aspx?CandidateStatus=" + CandidateStatus.AllCandidates, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<CandidateCompletedDetails");
        }

        protected void CandidateUnderSelectionProcessLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Candidate", ">>>CandidateUnderSelectionProcess");
            try
            {
                Response.Redirect("~/CandidateList.aspx?CandidateStatus=" + CandidateStatus.AppliedtoVacancy, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<CandidateUnderSelectionProcess");
        }

        protected void CandidateFinalSelectedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Candidate", ">>>CandidateFinalSelected");
            try
            {
                Response.Redirect("~/CandidateList.aspx?CandidateStatus=" + CandidateStatus.Hired, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<CandidateFinalSelected");
        }

        protected void CandidateRejectedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Candidate", ">>>CandidateRejected");
            try
            {
                Response.Redirect("~/CandidateList.aspx?CandidateStatus=" + CandidateStatus.Rejected, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<CandidateRejected");
        }

        //protected void EmployerOnBoardedLB_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/EmployerList.aspx?EmployerStatus=-1");
        //}

        protected void ActiveEmployersWithOpenVacancy_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Employer", ">>>ActiveEmployersWithOpenVacancy");
            try
            {

                Response.Redirect("~/EmployerList.aspx?EmployerStatus=" + EmployerStatus.OpenVacancy, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<ActiveEmployersWithOpenVacancy");
        }

        protected void InactiveEmployersWithFilledVacacnyLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Employer", ">>>InactiveEmployersWithFilledVacacny");
            try
            {
                Response.Redirect("~/EmployerList.aspx?EmployerStatus=" + EmployerStatus.FilledVacancy, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InactiveEmployersWithFilledVacacny");
        }

        protected void EmployersWithNoVacancyLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Employer", ">>>EmployersWithNoVacancy");
            try
            { 
                Response.Redirect("~/EmployerList.aspx?EmployerStatus=" + EmployerStatus.RegistrationFeePaid, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<EmployersWithNoVacancy");
        }
        protected void EmployersInprocessVacancies_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Employer", ">>>EmployersInprocessVacancies");
            try
            {
                Response.Redirect("~/EmployerList.aspx?EmployerStatus=" + EmployerStatus.InProcessVacancy, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<EmployersInprocessVacancies");
        }

        protected void TotalVacanciesLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Vacancy", ">>>TotalVacancies");
            try
            {
                Response.Redirect("~/VacancyList.aspx?VacancyStatus=0", false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalVacancies");
        }

        protected void OpenVacanciesLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Vacancy", ">>>OpenVacancies");
            try
            {
                Response.Redirect("~/VacancyList.aspx?VacancyStatus=1", false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<OpenVacancies");
        }

        protected void VacanciesUnderScheduledInterviewLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Vacancy", ">>>VacanciesUnderScheduledInterview");
            try
            {
                Response.Redirect("~/VacancyList.aspx?VacancyStatus=" + cVacancyStatus.InProcess, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<VacanciesUnderScheduledInterview");
        }

        protected void CloseVacanciesAfterFinalSelectionLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Vacancy", ">>>CloseVacanciesAfterFinalSelection");
            try
            {
                Response.Redirect("~/VacancyList.aspx?VacancyStatus=4", false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<CloseVacanciesAfterFinalSelection");
        }
        // Prashant : Added Payment dashboard list pages
        protected void TotalPaymentReceivedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Payment", ">>>TotalPaymentReceived");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.PaymentDone, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalPaymentReceived");
        }
        protected void OutstandingPaymentWithinTimeLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Payment", ">>>OutstandingPaymentWithinTime");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.PaymentDueWithinTime, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<OutstandingPaymentWithinTime");
        }
        protected void OutstandingPaymentOutofTimeLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Payment", ">>>OutstandingPaymentOutofTime");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.PaymentDueOutOfTime, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<OutstandingPaymentOutofTime");
        }
        protected void TotalPaymentRequestedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Payment", ">>>TotalPaymentRequested");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.All, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalPaymentRequested");
        }

        protected void InterviewProposedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewProposed");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewProposed, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewProposed");
        }

        protected void InterviewScheduledLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewScheduled");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewScheduled, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewScheduled");
        }

        protected void InterviewStartedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewStarted");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewStarted, false);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewStarted");
        }

        protected void InterviewDroppedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewDropped");
            try
            {

                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewDropped, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewDropped");
        }
        protected void InterviewCancelledLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewCancelled");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewCancelled, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewCancelled");
        }
        protected void InterviewRejectedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewRejected");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewRejected, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewRejected");
        }
        protected void InterviewCompletedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>InterviewCompleted");
            try
            {
                Response.Redirect("~/InterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewCompleted, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<InterviewCompleted");
        }
        protected void TotalPaymentNotificaitonGeneratedLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>TotalPaymentNotificaitonGenerated");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.All, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalPaymentNotificaitonGenerated");
        }
        protected void TotalPaymentTransactionDoneLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>TotalPaymentTransactionDone");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.PaymentDone, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalPaymentTransactionDone");

        }
        protected void TotalPaymentTransactionOverDueLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Interview", ">>>TotalPaymentTransactionOverDue");
            try
            {
                Response.Redirect("~/PaymentList.aspx?PaymentStatus=" + cPaymentStatus.PaymentDueOutOfTime, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalPaymentTransactionOverDue");

        }
        protected void UpdateData()
        {
            PaymentManagement.UpdateAllPaymentStatus();
        }

        protected void EmployerOnBoardLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Empployer", ">>>EmployerOnBoard");
            try
            {
                Response.Redirect("~/EmployerList.aspx?EmployerStatus=" + EmployerStatus.AllEmployers, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<EmployerOnBoard");

        }

        protected void TotalTransferReferralDetailsLB_Click(object sender, EventArgs e)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "Referral", ">>>TotalTransferReferralDetails");
            try
            {
                Response.Redirect("~/ReferralDetails.aspx?ReferaalStatus=" + CReferralStatus.ReferralPaid, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "Exception Message" + Message);

            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<TotalTransferReferralDetails");
        }
    }
}