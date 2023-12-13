using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class EmployerDetails : System.Web.UI.Page
    {
       
        public   Employer _employer = new Employer();
        public  Vacancy _Vacancy = new Vacancy();
        public  List<Vacancy> oVacancy = new List<Vacancy>();
        public List<Interview> oInterview = new List<Interview>();
        public UEClient oUEClient = new UEClient();

        public  string EmployerId;

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
                EmployerId = Request.QueryString["EmployerID"].ToString();
                if (!IsPostBack)
                {
                    GetEmployerDetails();
                    ShowEmployerDetails();
                    FillEmployerVacancyHistory();
                    FillEmployerInterviewHistoryGV();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void GetEmployerDetails()
        {
            try
            {
                EmployerManagement.GetEmployerByID(EmployerId, ref _employer, true);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void FillEmployerVacancyHistory()
        {


            try
            {
                DataTable dtEmployerVacancy = new DataTable();
                //string EmployerVacancy = _employer.EmployerID;
                EmployerManagement.GetEmployerByID(EmployerId, ref _employer, true);
                VacancyManager.GetVacancyListByEmployer(_employer.EmployerID,ref oVacancy,0);
                dtEmployerVacancy.Columns.AddRange(new DataColumn[3]{
                                                new DataColumn("VacancyName",typeof(string)),
                                                //new DataColumn("VacancyCode",typeof(string)),
                                                new DataColumn("PrimaryLocation",typeof(string)),
                                                new DataColumn("VacancyDetails",typeof(string))
                                                //new DataColumn("RequiredMinExp",typeof(int)),
                                                //new DataColumn("RequiredMinQualification",typeof(string)),
                                               

                });

                for (int i = 0; i < oVacancy.Count; i++)
                {
                    string VacancyDetails = oVacancy[i].VacancyDetails;
                    if (VacancyDetails.Length > 50)
                    {
                        VacancyDetails = VacancyDetails.Substring(0, 47);
                        VacancyDetails = VacancyDetails + "...";
                    }

                    dtEmployerVacancy.Rows.Add(oVacancy[i].VacancyName,
                                             //oVacancy[i].VacancyCode,
                                             LocationManagement.GetStateDetail(oVacancy[i].PrimaryLocation).StateName + "-" + LocationManagement.GetStateDetail(oVacancy[i].PrimaryLocation).Country.CountryName,
                                             VacancyDetails
                                             //oVacancy[i].RequiredMinExp,
                                             //oVacancy[i].RequiredMinQualification
                                            );

                }

                EmployerVacancyHistoryGV.DataSource = dtEmployerVacancy;
                EmployerVacancyHistoryGV.DataBind();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }

        }

        protected void FillEmployerInterviewHistoryGV()
        {
            try
            {
                DataTable EmployerInterview = new DataTable();
                //string InterviewEmployerID = _employer.EmployerID;
                EmployerManagement.GetEmployerByID(EmployerId, ref _employer, true);
                InterviewManagement.GetInterviewListByEmployer(_employer.EmployerID, ref oInterview, -1);
                EmployerInterview.Columns.AddRange(new DataColumn[4]{
                                                new DataColumn("InterviewID",typeof(string)),
                                                new DataColumn("InterviewDate",typeof(string)),
                                                new DataColumn("Topic",typeof(string)),
                                                new DataColumn("InterviewStatus",typeof(string))
                });

                for (int i = 0; i < oInterview.Count; i++)
                {
                    string InterviewStatus = string.Empty;
                    switch(oInterview[i].InterviewStatus)
                    {
                        case InterviewCallStatus.InterviewProposed:
                            InterviewStatus = "Interview Proposed";
                            break;
                        case InterviewCallStatus.InterviewScheduled:
                            InterviewStatus = "Interview Scheduled";
                            break;
                        case InterviewCallStatus.InterviewCompleted:
                            InterviewStatus = "Interview Completed";
                            break;
                        case InterviewCallStatus.InterviewCancelled:
                            InterviewStatus = "Interview Cancelled";
                            break;
                        case InterviewCallStatus.InterviewDropped:
                            InterviewStatus = "Interview Dropped";
                            break;
                        case InterviewCallStatus.InterviewRejected:
                            InterviewStatus = "Interview Rejected";
                            break;
                        case InterviewCallStatus.InterviewStarted:
                            InterviewStatus = "Interview Started";
                            break;
                    }
                    EmployerInterview.Rows.Add(oInterview[i].InterviewID,
                                             oInterview[i].ZoomVCMeetingResponse.start_time,
                                             oInterview[i].ZoomVCMeetingResponse.topic,
                                             InterviewStatus
                                            );

                }

                EmployerInterviewHistoryGV.DataSource = EmployerInterview;
                EmployerInterviewHistoryGV.DataBind();


            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void ShowEmployerDetails()
        {
            try
            {
                string EmployerStatus = string.Empty;
                switch (_employer.EmployerStatus)
                {
                    case "all":
                        EmployerStatus = "Employer On Boarded";
                        break;
                    case "1":
                        EmployerStatus = "Employer Registered";
                        break;
                    case "2":
                        EmployerStatus = "Registration Fee Due";
                        break;
                    case "3":
                        EmployerStatus = "Registration Fee Paid";
                        break;
                    case "4":
                        EmployerStatus = "Active Employer With Open Vacancy";
                        break;
                    case "5":
                        EmployerStatus = "Employer with InProcess Vacancies";
                        break;
                    case "6":
                        EmployerStatus = "Recruitment Fee Paid";
                        break;
                    case "7":
                        EmployerStatus = "Inactive Employer With Filled Vacacny";
                        break;
                    case "8":
                        EmployerStatus = "InActive Emplpyer Registration Renewal Pending";
                        break;
                    case "9":
                        EmployerStatus = "Active Emplpyer Registration Renewed";
                        break;
                }


                string EmployerBusinessLogo = string.Empty;
                string imagetag = string.Empty;

                if (_employer.BusinessLogo != null)
                {
                    EmployerBusinessLogo = Convert.ToBase64String(_employer.BusinessLogo);
                    //imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";
                    BusinessLogo.ImageUrl = String.Format("data:image/jpg;base64,{0}", EmployerBusinessLogo);
                    // BusinessLogo1.ImageUrl = String.Format("data:image/jpg;base64,{0}", EmployerBusinessLogo);
                }
                else
                {
                    imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                }
                // string sUEClientTypeID = oUEClient.ClientTypeID;
                AppliedOnLBL.Text = Convert.ToDateTime(_employer.RegisteredOn).ToString();
                ProfileUpdatedOnLBL.Text = Convert.ToDateTime(_employer.UpdateOn).ToString();
                EmployerStatusLBL.Text = EmployerStatus;
                EmployerBusinessNameLBL.Text = _employer.BusinessName;
                EmployerDetailLBL.Text = _employer.BusinessName;
                EmployerNameLBL.Text = _employer.Name;
                //BusinessNameLBL.Text = _employer.BusinessName;
                WhatsAppNumberLBL.Text = _employer.WhatsAppNumber;
                EmployerAddressLBL.Text = _employer.address + "," + LocationManagement.GetCityDetail(_employer.LocationStateCode, _employer.LocationCityCode).CityName + "," + LocationManagement.GetStateDetail(_employer.LocationStateCode).StateName;
                EmployerEmailLBL.Text = _employer.email;


                /*------------------------------------*/
                // Facts
                string FactsData = string.Empty;
                EmployerManagement.GetEmployerMobileDashboardData(EmployerId, ref FactsData);
                dynamic EmployerDashboardData = JsonConvert.DeserializeObject(FactsData);
                VacanciesPublishedLBL.Text = EmployerDashboardData.TotalVacancies;
                ScheduledInterviewsLBL.Text = EmployerDashboardData.ScheduleInterview;
                JobApplicationLBL.Text = EmployerDashboardData.ApplicationReceived;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void EmployerDetailsPrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"~\PrintEmployerDetails.aspx?EmployerID="+ EmployerId, false);
        }

        protected void EmployerVacancyHistoryGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                EmployerVacancyHistoryGV.PageIndex = e.NewPageIndex;
                EmployerVacancyHistoryGV.SelectedIndex = -1;
                FillEmployerVacancyHistory();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void EmployerInterviewHistoryGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                EmployerInterviewHistoryGV.PageIndex = e.NewPageIndex;
                FillEmployerInterviewHistoryGV();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
    }
}