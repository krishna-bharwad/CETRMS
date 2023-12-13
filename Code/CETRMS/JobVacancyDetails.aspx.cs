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
    public partial class JobVacancyDetails : System.Web.UI.Page
    {
        public   string VacancyID;
        public   Vacancy vacancy = new Vacancy();
        public  Employer employer = new Employer();
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

                VacancyID = Request.QueryString.Get("VacancyID").ToString();
                if (!IsPostBack)
                {
                    GetVacancyDetails();
                    ShowVacancyDetails();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }

        }
        protected void GetVacancyDetails()
        {
            try
            {
                VacancyManager.GetVacancyDetails(VacancyID, ref vacancy);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected void ShowVacancyDetails()
        {
            try
            {
                EmployerManagement.GetEmployerByID(vacancy.CETEmployerId, ref employer, true);

                string EmployerBusinessLogo = string.Empty;
                string imagetag = string.Empty;

                if (employer.BusinessLogo != null)
                {
                    EmployerBusinessLogo = Convert.ToBase64String(employer.BusinessLogo);

                    imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";

                    BusinessLogo.ImageUrl = String.Format("data:image/jpg;base64,{0}", EmployerBusinessLogo);
                }
                else
                {
                    imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";


                }

                string VacancyStatus = string.Empty;
                switch (vacancy.VacancyStatusTypeID)
                {
                    case "all":

                        VacancyStatus = "Total Vacancies";
                        break;
                    case "1":

                        VacancyStatus = "Open Vacancies";
                        break;
                    case "5":

                        VacancyStatus = "Vacancies Under Scheduled Interview";
                        break;
                    case "4":

                        VacancyStatus = "Close Vacancies After Final Selection";
                        break;

                }

                VacancyStatusLBL.Text = VacancyStatus;
                EmployerBusinessNameLBL.Text = employer.BusinessName;
                EmployerNameLBL.Text = employer.BusinessName;
                VacancyNameLBL.Text = vacancy.VacancyName;
                JobLocationLBL.Text = LocationManagement.GetStateDetail(vacancy.PrimaryLocation).StateName + "," + LocationManagement.GetStateDetail(vacancy.PrimaryLocation).Country.CountryName;
                JobTypeLBL.Text = vacancy.JobType;
                CandidatesRequiredLBL.Text = vacancy.CandidatesRequired.ToString();
                VacancyDetailsLBL.Text = vacancy.VacancyDetails;
                EmployementStatusLBL.Text = vacancy.EmployementStatus;
                RequiredMinExpLBL.Text = vacancy.RequiredMinExp.ToString();
                RequiredMinQualificationLBL.Text = vacancy.RequiredMinQualification;
                SalaryOfferedLBL.Text = LocationManagement.GetStateDetail(vacancy.PrimaryLocation).Country.CurrencySymbol + vacancy.SalaryOffered.ToString();


                string SalaryCycleMsg = string.Empty;
                switch (vacancy.SalaryCycle)
                {
                    case 0:
                        SalaryCycleMsg = "All";
                        break;
                    case 1:
                        SalaryCycleMsg = "Weekly";
                        break;
                    case 2:
                        SalaryCycleMsg = "Monthly";
                        break;
                    case 3:
                        SalaryCycleMsg = "Yearly";
                        break;
                }
                SalaryCycleLBL.Text = SalaryCycleMsg;

                string EmployerStatus = string.Empty;
                switch (employer.EmployerStatus)
                {
                    case "all":
                        EmployerStatus = "Employer On Boarded";
                        break;
                    case "4":
                        EmployerStatus = "Active Employer With Open Vacancy";
                        break;
                    case "7":
                        EmployerStatus = "Inactive Employer With Filled Vacacny";
                        break;
                    case "1":
                        EmployerStatus = "Employers With No Vacancy";
                        break;
                    case "5":
                        EmployerStatus = "InProcess Vacancies";
                        break;
                    case "2":
                        EmployerStatus = "Registration Complete";
                        break;
                }
                AppliedOnLBL.Text = Convert.ToDateTime(employer.RegisteredOn).ToString();
                ProfileUpdatedOnLBL.Text = Convert.ToDateTime(employer.UpdateOn).ToString();
                EmployerStatusLBL.Text = EmployerStatus;
                EmployerBusinessNameLBL.Text = employer.BusinessName;
                ContactPersonNameLBL.Text = employer.Name;
                //BusinessNameLBL.Text = _employer.BusinessName;
                WhatsAppNumberLBL.Text = employer.WhatsAppNumber;
                EmployerAddressLBL.Text = employer.address + "," + LocationManagement.GetCityDetail(employer.LocationStateCode, employer.LocationCityCode).CityName + "," + LocationManagement.GetStateDetail(employer.LocationStateCode).StateName;
                EmployerEmailLBL.Text = employer.email;

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        //protected void JobVacancyDetailsPrintBTN_Click(object sender, ImageClickEventArgs e)
        //{
            //Response.Redirect(@"~\PrintJobVacancyDetails.aspx?VacancyID=" + vacancy.VacancyID);
        //}

        protected void JobVacancyDetailsPrintBTN_Click1(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"~\PrintJobVacancyDetails.aspx?VacancyID=" + VacancyID, false);
        }
    }
}