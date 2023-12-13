using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class CandidateList : System.Web.UI.Page
    {

        public static List<Candidate> oCandidateList = new List<Candidate>();
        List<cState> cStates = new List<cState>();
        public static Candidate _Candidate = new Candidate();
        public class cPagePanel
        {
            public string PanelName { get; set; }
            public string PanelURL { get; set; }
        }
        public static List<cPagePanel> PanelList = new List<cPagePanel>(); 
        public static int PageCount = 1;
        public static int CandidateCount;
        static public string sCandidateStatus;
        public static string CandidateCard;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), "CandidateList Page opened.");
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                sCandidateStatus = Request.QueryString.Get("CandidateStatus");

                if (!IsPostBack)
                {

                    SetCandidateTabDDL();
                    GetCandidateDetails();
                    ShowCandidateListPage(0);
                    FillLocationDDL();
                }
                CandidateCount = oCandidateList.Count;
                PageCount = CandidateCount / 9;

                if (CandidateCount % 9 != 0) PageCount++;

                for (int i = 0; i < PageCount; i++)
                {
                    cPagePanel pagePanel = new cPagePanel();
                    pagePanel.PanelName = "panel" + i.ToString();
                    PanelList.Add(pagePanel);                       //PanelList
                }
                for (int i = 0; i < PageCount; i++)
                {
                    Button button = new Button();
                    button.Text = (i + 1).ToString();
                    button.ID = i.ToString();
                    button.CssClass = "button-25";
                    button.Attributes.Add("AutoPostBack", "True");
                    button.Click += new EventHandler(Button_Click1);
                    PlaceHolder1.Controls.Add(button);             //PaginationPanel.Controls.Add(button);  
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void SetCandidateTabDDL()
        {
            try
            {
                List<Tuple<int, string, bool>> CandidateStatusTypeList = new List<Tuple<int, string, bool>>();
                CandidateStatusTypeList.Clear();                                        //Add By Krishna
                CandidateStatus.GetCandidateStatusList(ref CandidateStatusTypeList);

                CandidateListTabDDL.Items.Clear();                                  //Add By Krishna
                foreach (Tuple<int,string, bool> row in CandidateStatusTypeList)
                {
                    if (row.Item3)
                    {
                        CandidateListTabDDL.Items.Add(row.Item2.ToString());
                        CandidateListTabDDL.Items.FindByText(row.Item2).Value = row.Item1.ToString();
                    }
                }

                for (int i = 0; i < CandidateListTabDDL.Items.Count; i++)
                {
                    
                    if (CandidateListTabDDL.Items[i].Value == sCandidateStatus)
                        CandidateListTabDDL.Items[i].Selected = true;
                    else
                        CandidateListTabDDL.Items[i].Selected = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void GetCandidateDetails()
        {
            try
            {
                oCandidateList.Clear();
                CandidateManagement.GetCandidateList("all", ref oCandidateList, Convert.ToInt32(CandidateListTabDDL.SelectedValue), true);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void FillLocationDDL()
        {
            try
            {
                LocationManagement.GetStateList("all", ref cStates);
                LocationDDL.Items.Clear();
                LocationDDL.Items.Add("All");
                LocationDDL.Items[0].Value = "all";
                for (int i = 0; i < cStates.Count; i++)
                {
                    LocationDDL.Items.Add(cStates[i].StateName + "-" + cStates[i].Country.CountryName);
                    LocationDDL.Items[i + 1].Value = cStates[i].StateCode;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected bool ShowCandidateListPage(int PageNo)
        {
            bool bRetValue = false;
            try
            {
                string imagetag = string.Empty;
                string cardColor = string.Empty;
                string VacancyStatus = string.Empty;


                int StartCardNo = PageNo * 9;
                int EndCardNo = (PageNo + 1) * 9;

                for (int i = StartCardNo; i < EndCardNo && i < oCandidateList.Count; i++)
                {
                    sCandidateStatus = CandidateStatus.GetCandidateStatus(Convert.ToInt32(oCandidateList[i].PersonalProfile.Status));

                    switch (Convert.ToInt32(oCandidateList[i].PersonalProfile.Status))
                    {
                        //case 1:
                        //    cardColor = "border-secondary";
                        //    sCandidateStatus = "New Registration";
                        //    break;
                        case CandidateStatus.ProfileCreated:
                            cardColor = "border-info";
                            break;
                        case CandidateStatus.RegistrationFeePaid:
                            cardColor = "border-success";
                            break;
                        case CandidateStatus.AppliedtoVacancy:
                            cardColor = "border-success";
                            break;
                        case CandidateStatus.Hired:
                            cardColor = "border-primary";
                            break;
                        case CandidateStatus.Rejected:
                            cardColor = "border-danger";
                            break;

                        default: cardColor = "border-secondary"; break;
                    }

                    string CandidatePhotoMem = string.Empty;

                    string ColorTag = string.Empty;

                    if (oCandidateList[i].PersonalProfile.Photo != null)
                    {
                        CandidatePhotoMem = Convert.ToBase64String(oCandidateList[i].PersonalProfile.Photo);

                        imagetag = "<img src = \"data:image/jpg; base64, " + CandidatePhotoMem + " \" alt=\"\" class=\"img-circle img-fluid\">";
                    }
                    else
                    {
                        imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                    }
                    if (oCandidateList[i].PersonalProfile.Photo != null)
                        CandidatePhotoMem = Convert.ToBase64String(oCandidateList[i].PersonalProfile.Photo);

                    if (oCandidateList[i].PersonalProfile.Status == CandidateListTabDDL.SelectedValue.ToString() || CandidateListTabDDL.SelectedValue.ToString() == CandidateStatus.AllCandidates.ToString())
                    {
                        string CandidateId = oCandidateList[i].CandidateID;
                        CandidateManagement.GetCandidateBriefProfile(CandidateId, ref _Candidate);
                        string CandidateCard = "<div class=\"col-xs-6 col-md-4 py-2\">" +
                            "<div class=\"card h-100 " + cardColor + "\">" +
                            "<div class=\"card-body-u\">" +
                            " <div class=\"col-sm-12\">" +
                            "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + sCandidateStatus + "</strong></i></h6>" +
                            "<div class=\"row\">" +
                            "<div class=\"float-left col p-2\">" +
                            "<h2>" + oCandidateList[i].PersonalProfile.Name + "</h2>" +
                             "<p><i class=\"fa fa-commenting\"></i> <strong>About: </strong> " + _Candidate.CanidateBriefProfile + " </p>" +
                             "</div>" +
                             "<div class=\"float-right col text-right mb-4\">" +
                              imagetag +
                             "</div>" +
                             "</div>" +
                            "</div>" +
                            "<div class=\"profile-bottom text-center\">" +
                            // "<div class=\"col-sm-12\">"+
                            "<div class=\"float-left col-xs-6 p-2\">" +
                               "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oCandidateList[i].ContactDetails.CurrentStateCode).StateName + "," + LocationManagement.GetStateDetail(oCandidateList[i].ContactDetails.CurrentStateCode).Country.CountryName +
                               "</div>" +
                               "<div class=\"float-right col-xs-6\">" +
                                 "<a href = \".\\CandidateProfile.aspx?CandidateId=" + oCandidateList[i].CandidateID + "\"class=\"btn btn-success rounded-pill btn-sm\">" +
                                 "<i class=\"fas fa-user\"></i>View Profile</a>" +
                               "</div>" +
                            // "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div> ";

                        CandidateListLit.Text = CandidateListLit.Text + CandidateCard;
                    }
                }
                foreach (Control control in PlaceHolder1.Controls)
                {
                    Button selectedbtn = (Button)control;
                    if (control.ID == (PageNo).ToString())
                    {
                        selectedbtn.CssClass = "button-active-25";
                    }
                    else
                    {
                        selectedbtn.CssClass = "button-25";
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            return bRetValue;
        }
        private void Button_Click1(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string buttonId = button.ID;
                CandidateListLit.Text = "";
                ShowCandidateListPage(Convert.ToInt32(buttonId));
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void CandidateListTabDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetCandidateDetails();
                CandidateListLit.Text = "";
                ShowCandidateListPage(0);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void LocationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sJobLocation = LocationDDL.SelectedValue;
                oCandidateList.Clear();
                CandidateManagement.GetCandidateList(LocationDDL.SelectedValue, ref oCandidateList, Convert.ToInt32(CandidateListTabDDL.SelectedValue), true);
                CandidateListLit.Text = "";
                ShowCandidateListPage(0);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = true;
        }
        protected void PrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(CandidateListTabDDL.SelectedValue))
                {
                    //case CandidateStatus.NewRegistration:
                    //    Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=1", false);
                    //    break;
                    case CandidateStatus.AllCandidates:
                        Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=" + CandidateStatus.AllCandidates, false);
                        break;
                    case CandidateStatus.ProfileCreated:
                        Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=2", false);
                        break;
                    case CandidateStatus.AppliedtoVacancy:
                        Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=4", false);
                        break;
                    case CandidateStatus.Hired:
                        Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=8", false);
                        break;
                    case CandidateStatus.Rejected:
                        Response.Redirect(@"~\PrintCandidateList.aspx?CandidateStatus=" + CandidateStatus.Rejected, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
    }
}