using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class ScheduleInterviewPage : System.Web.UI.Page
    {
        public string EmployerId;
        public string CandidateId;
        public string vacancyId;
        protected List<Employer> employers = new List<Employer>();
        protected List<Vacancy> vacancies = new List<Vacancy>();
        protected List<Interview> interviews = new List<Interview>();
        public List<JobApplication> jobApplication = new List<JobApplication>();
        public Candidate candidate = new Candidate();
        public Employer employer = new Employer();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (!IsPostBack)
                {
                    FillEmployerDDL(); //Prashant
                    FillVacancyDDL();
                    FillProposedInterviewGV();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void FillEmployerDDL() //Prashant
        {
            try
            {
                employers.Add(new Employer { BusinessName = "All", EmployerID = EmployerStatus.AllEmployers.ToString() });
                EmployerManagement.GetEmployerList(ref employers, EmployerStatus.OpenVacancy);
                EmployerDDL.DataSource = employers;
                EmployerDDL.DataTextField = "BusinessName";
                EmployerDDL.DataValueField = "EmployerId";
                EmployerDDL.DataBind();
                EmployerDDL.SelectedValue = EmployerStatus.AllEmployers.ToString();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }

        }
        protected void FillVacancyDDL()
        {
            try
            {
                EmployerId = EmployerDDL.SelectedValue;
                vacancies.Add(new Vacancy { VacancyName = "All", UEEmployerID = EmployerId, VacancyID = cVacancyStatus.All.ToString() });
                VacancyManager.GetVacancyListByEmployer(EmployerId, ref vacancies, cVacancyStatus.InProcess);
                VacancyDDL.DataSource = vacancies;
                VacancyDDL.DataTextField = "VacancyName";
                VacancyDDL.DataValueField = "VacancyId";
                VacancyDDL.DataBind();
                VacancyDDL.SelectedValue = cVacancyStatus.All.ToString();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void EmployerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVacancyDDL();
            FillProposedInterviewGV();
        }
        protected void VacancyDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProposedInterviewGV();
        }
        protected void FillProposedInterviewGV()
        {
            try
            {
                DataTable dt = new DataTable();
                int VacancyId = Convert.ToInt32(VacancyDDL.SelectedValue);
                interviews.Clear();

                if (VacancyId == cVacancyStatus.All)
                    InterviewManagement.GetInterviewListByEmployer(EmployerDDL.SelectedValue, ref interviews, InterviewCallStatus.InterviewProposed);
                else 
                    InterviewManagement.GetInterviewListByVacancy(VacancyId, ref interviews, InterviewCallStatus.InterviewProposed);
    
                JobApplicationManager.GetJobApplicationListByVacancy(VacancyId.ToString(), ref jobApplication, JobApplicationStatus.InterviewScheduled);

                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("EmployerName", typeof(string)),
                new DataColumn("CandidateName", typeof(string)),
                new DataColumn("VacancyName", typeof(string)),
                new DataColumn("DurationInMinutes",typeof(int)),
                new DataColumn("ChosenTimeZone", typeof(string)),
                new DataColumn("InterviewID", typeof(string))});


                int ColsCount = dt.Columns.Count;

                for (int i = 0; i < interviews.Count; ++i)
                {
                    JobApplication jobApplication = new JobApplication();
                    JobApplicationManager.GetJobApplicationDetails(interviews[i].JobApplicationID, ref jobApplication);
                    Vacancy vacancy = new Vacancy();
                    VacancyManager.GetVacancyDetails(jobApplication.VacancyID, ref vacancy);
                    CandidateId = jobApplication.CandidateID;
                    CandidateManagement.GetCandidateFullDetails(jobApplication.CandidateID, ref candidate);
                    EmployerManagement.GetEmployerByID(vacancy.UEEmployerID, ref employer);
                    dt.Rows.Add(employer.Name, candidate.PersonalProfile.Name, vacancy.VacancyName, interviews[i].DurationInMinutes, interviews[i].ChosenTimeZone, interviews[i].InterviewID);
                }
                ProposedInterviewsGV.DataSource = dt;
                ProposedInterviewsGV.DataBind();

                if (dt.Rows.Count != 0 || EmployerDDL.Text == "-1" || VacancyDDL.Text == "-1")
                    InterviewNotProposed_txt.Text = string.Empty;
                else
                    InterviewNotProposed_txt.Text = "No Interview Proposed";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void ScheduleInterviewGv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string interviewId = e.Row.Cells[0].Text;
                    LinkButton lb = (LinkButton)e.Row.Cells[1].FindControl("btn");
                    lb.CommandArgument = interviewId;
                    lb.Text = interviewId;
                }
                e.Row.Cells[0].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void InterviewID_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = (LinkButton)sender;
                string InterviewId = lb.CommandArgument.ToString();
                Response.Redirect(URLs.InterviewDetailsURL + InterviewId, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

    }
}