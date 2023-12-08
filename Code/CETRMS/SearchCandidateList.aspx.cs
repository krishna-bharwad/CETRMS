using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class SearchCandidateList : System.Web.UI.Page
    {
        public static List<Candidate> ListCandidateSearch = new List<Candidate>();
        public static List<cCountry> oCountry = new List<cCountry>();
        public static List<cState> oStates = new List<cState>();
        public List<JobApplication> oJobApplication = new List<JobApplication>();
        Candidate oCandidate = new Candidate();
        public MaskedCandidate oMaskedCandidate = new MaskedCandidate();
        public List<MaskedCandidate> ListMasCan = new List<MaskedCandidate>();
        public static string JobAppId;

        public static string DefaultLocation = "001012";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillCountryDDL();
                    FillStateDDL();
                    GetCandidateList();
                    DisplayCandidateList(DefaultLocation);
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }


        protected void GetCandidateList()
        {
            try
            {
                ListCandidateSearch.Clear();
                CandidateManagement.GetCandidateMaskedListByLocation(DefaultLocation, ref ListMasCan, 2);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        protected bool DisplayCandidateList(string Location)
        {
            bool bRetValue = false;
            try
            {
                string cardColor = string.Empty;
                string CandidateStatus = string.Empty;
                string CandidatePhotoMem = string.Empty;
                string imagetag = string.Empty;
                CandidateListLit.Text = "";


                for (int i = 0; i < ListMasCan.Count; i++)
                {
                    if (ListMasCan[i].Photo != null)
                    {
                        CandidatePhotoMem = Convert.ToBase64String(ListMasCan[i].Photo);

                        imagetag = "<img src = \"data:image/jpg; base64, " + CandidatePhotoMem + " \" alt=\"\" class=\"img-circle img-fluid\">";
                    }
                    else
                    {
                        imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                    }


                    string CandidateCard =
                        "<div class=\"col-sm-4 py-2\">" +
                            "<div class=\"card h-100 " + cardColor + "\">" +
                            "<div class=\"card-body-u\">" +
                            " <div class=\"col-sm-12\">" +                      //// 3 
                            "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + "</strong></i></h6>" +
                             "<div class=\"col-xs-6\">" +            ////1
                            "<h2>" + "</h2>" +
                             "<p><strong>About: </strong> " + ListMasCan[i].CanidateBriefProfile + " </p>" +
                             "</div>" +
                             "<div class=\"row\">" +
                             "<div class=\"col-sm-6 mb-4\">" +             ////2
                              imagetag +
                             "</div>" +
                             "<div class=\"col-xs-6\">" +
                           "<p><strong>Gender:</strong>" + ListMasCan[i].Gender + "</p>" + "<br/>" + "<p><strong>Age:</strong>"
                             + ListMasCan[i].Age + "</p>" +


                             "</div>" +
                             "</div>" +
                            "</div>" +                              ////3
                            "<div class=\"profile-bottom text-center\">" +
                            // "<div class=\"col-sm-12\">"+
                            "<div class=\"float-left p-2 col-xs-6\">" +             ////4
                               "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + ListMasCan[i].Location +
                               "</div>" +                                           /////4
                               "<div class=\"float-right col-xs-6\">" +             ////5
                                 "<a href = \".\\#SignupModalCenter\" data-toggle=\"modal\" data-dismiss=\"modal\" data-target=\"#SignupModalCenter\" + class=\"btn btn-success rounded-pill btn-sm\">" +
                                 "<i class=\"fas fa-user\"></i>Hire Now</a>" +
                               "</div>" +                                               ////5
                                                                                        // "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div> ";

                    CandidateListLit.Text = CandidateListLit.Text + CandidateCard;
                }

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }
        protected void FillCountryDDL()
        {
            try
            {
                oCountry.Clear();
                LocationManagement.GetCountryList(ref oCountry);
                CountryDDL.DataSource = oCountry;
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }

        }
        protected void FillStateDDL()
        {
            try
            {
                oStates.Clear();
                LocationManagement.GetStateList(CountryDDL.SelectedValue, ref oStates);
                StateDDL.DataSource = oStates;
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }

        protected void CountryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillStateDDL();
                //GetCandidateGV(StateDDL.SelectedValue.Trim());
                DisplayCandidateList(StateDDL.SelectedValue.Trim());
                //DisplayCandidateList();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }

        protected void StateDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //GetCandidateGV(StateDDL.SelectedValue.Trim());
                string CandidateLocation = StateDDL.SelectedValue;
                ListCandidateSearch.Clear();
                CandidateManagement.GetCandidateMaskedListByLocation(CandidateLocation, ref ListMasCan, 2);
                CandidateListLit.Text = "";
                DisplayCandidateList(StateDDL.SelectedValue.Trim());
                //DisplayCandidateList();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
    }
}