using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class SearchVacancyList : System.Web.UI.Page
    {
        public static List<Vacancy> oVacancy = new List<Vacancy>();
        public static List<cCountry> CountryList = new List<cCountry>();
        public static List<cState> StatesList = new List<cState>();
        public static string DefaultLocation = "001012";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetCountryDDL();
                    GetStateDDL();
                    GetVacancyDetail();
                    GetVacancyList(DefaultLocation);
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

        protected void GetCountryDDL()
        {
            try
            {
                CountryList.Clear();
                LocationManagement.GetCountryList(ref CountryList);
                CountryDDL.DataSource = CountryList;
                CountryDDL.DataTextField = "CountryName";
                CountryDDL.DataValueField = "CountryCode";
                CountryDDL.DataBind();
                CountryDDL.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        protected void GetStateDDL()
        {
            try
            {
                StatesList.Clear();
                LocationManagement.GetStateList(CountryDDL.SelectedValue, ref StatesList);
                StateDDL.DataSource = StatesList;
                StateDDL.DataTextField = "StateName";
                StateDDL.DataValueField = "StateCode";
                StateDDL.DataBind();
                StateDDL.Items.Insert(0, new ListItem("All", "0"));
                StateDDL.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        protected void GetVacancyDetail()
        {
            try
            {
                oVacancy.Clear();
                VacancyManager.GetVacancyListByLocation(DefaultLocation, ref oVacancy);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected bool GetVacancyList(string Location)
        {
            bool bRetValue = false;
            try
            {
                string cardColor = string.Empty;
                string CandidateStatus = string.Empty;
                VacancyListLit.Text = "";
                for (int i = 0; i < oVacancy.Count; i++)
                {

                    string VacancyCard =

                           "<div class=\"col-sm-4  mb-3\">" +
                        "<div class=\"card h-100 mx-auto border-success\">" +
                        "<div class=\"card-body-u\">" +
                        " <div class=\"col-sm-12\">" +
                        "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + oVacancy[i].JobType + "</strong></i></h6>" +
                         "<div class=\"col-xs-6\">" +
                        "<h2><strong>Vacancy Name:</strong>" + oVacancy[i].VacancyName + "</h2>" +
                         "<p><strong> Vacancy Details:</strong> " + oVacancy[i].VacancyDetails + " </p>" +
                         "</div>" +
                         "<div class=\"col-xs-6 mb-4 \">" +
                         "<p><span></span></p>" +
                         "</div>" +
                        "</div>" +
                        "<div class=\"text-center\">" +
                        // "<div class=\"col-sm-12\">"+
                        "<div class=\"float-left p-2 col-xs-6\">" +
                           "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oVacancy[i].PrimaryLocation).StateName + "," + LocationManagement.GetStateDetail(oVacancy[i].PrimaryLocation).Country.CountryName +
                           "</div>" +
                           "<div class=\"float-right col-xs-6\">" +
                             "<a href = \".\\#SignupModalCenter\" data-toggle =\"modal\" data-dismiss=\"modal\" data-target=\"#SignupModalCenter\" class=\"btn btn-success rounded-pill btn-sm align-text-bottom \">" +
                             "<i class=\"fas fa-user\"></i>Get a Job</a>" +
                           "</div>" +
                        // "</div>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "</div> ";

                    VacancyListLit.Text = VacancyListLit.Text + VacancyCard;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }

        protected void GetVacancyGV()
        {

            try
            {
                DataTable DataTableSearchVacancy = new DataTable();
                VacancyManager.GetVacancyListByLocation("all",ref oVacancy);
                DataTableSearchVacancy.Columns.AddRange(new DataColumn[1]{new DataColumn("VacancyName",typeof(string))});
                for (int i = 0; i < oVacancy.Count; i++)
                {
                    DataTableSearchVacancy.Rows.Add(oVacancy[i].VacancyName);
                }

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {

            }
         }

        protected void CountryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStateDDL();
                oVacancy.Clear();
                GetVacancyList(StateDDL.SelectedValue.Trim());
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        protected void StateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string VacancyLocation = StateDDL.SelectedValue;
                oVacancy.Clear();
                VacancyManager.GetVacancyListByLocation(VacancyLocation, ref oVacancy, 0);
                VacancyListLit.Text = "";
                GetVacancyList(StateDDL.SelectedValue.Trim());
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
    }
}