using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class EmployerList : System.Web.UI.Page
    {
        List<cState> States = new List<cState>();
        public static List<Employer> oEmployer = new List<Employer>();

        public class cPagePanel
        {
            public string PanelName { get; set; }
            public string PanelURL { get; set; }
        }
        public static List<cPagePanel> PanelList = new List<cPagePanel>();
        static  public  string sEmployerStatus;
        public static string sEmployerCard;
        public static int PageCount = 1;
        public static int iEmployerCount;
        public static int ab = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                sEmployerStatus = Request.QueryString.Get("EmployerStatus");


                if (!IsPostBack)
                {

                    FillEmployerStatus();
                    FillLocation();
                    GetEmployerDetails();
                    ShowEmployerCard(0);
                }

                iEmployerCount = oEmployer.Count;
                PageCount = iEmployerCount / 9;

                if (iEmployerCount % 9 != 0)
                {
                    PageCount++;
                }


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
                    button.Click += Button_Click;
                    PlaceHolder1.Controls.Add(button);
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string buttonId = button.ID;
                EmployerListLiteral.Text = "";
                ShowEmployerCard(Convert.ToInt32(buttonId));
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void FillEmployerStatus()
        {
            try
            {
                List<Tuple<int, string, bool>> EmployerStatusTypeList = new List<Tuple<int, string, bool>>();
                EmployerStatusTypeList.Clear();                                     //Add By Krishna
                EmployerStatus.GetEmployerStatusList(ref EmployerStatusTypeList);

                EmployerListTabDDL.Items.Clear();                               //Add By Krishna
                foreach (Tuple<int, string, bool> row in EmployerStatusTypeList)
                {
                    if (row.Item3)
                    {
                        EmployerListTabDDL.Items.Add(row.Item2.ToString());
                        EmployerListTabDDL.Items.FindByText(row.Item2).Value = row.Item1.ToString();
                    }
                }
                for (int i = 0; i < EmployerListTabDDL.Items.Count; i++)
                {
                    if (EmployerListTabDDL.Items[i].Value == sEmployerStatus)
                        EmployerListTabDDL.Items[i].Selected = true;
                    else
                        EmployerListTabDDL.Items[i].Selected = false;

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GetEmployerDetails()
        {
            try
            {
                int EmployerStatusSel = Convert.ToInt32(EmployerListTabDDL.SelectedValue);
                oEmployer.Clear();
                EmployerManagement.GetEmployerListByLocation(ref oEmployer, "all", EmployerStatusSel);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void FillLocation()
        {
            try
            {
                LocationManagement.GetStateList("all", ref States);
                LocationDDL.Items.Clear();
                LocationDDL.Items.Add("All");
                LocationDDL.Items[0].Value = "all";
                for (int i = 0; i < States.Count; i++)
                {
                    LocationDDL.Items.Add(States[i].StateName + "-" + States[i].Country.CountryName);
                    LocationDDL.Items[i + 1].Value = States[i].StateCode;

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected bool ShowEmployerCard(int PageNo)
        {
            bool bRetValue = false;
            try
            {
                string imagetag = string.Empty;
                string cardColor = string.Empty;

                int StartCardNo = PageNo * 9;
                int EndCardNo = (PageNo + 1) * 9;

                for (int i = StartCardNo; i < EndCardNo && i < oEmployer.Count; i++)
                {

                    sEmployerStatus = EmployerStatus.GetEmployerStatus(Convert.ToInt32(oEmployer[i].EmployerStatus));
                    //switch (Convert.ToInt32(oEmployer[i].EmployerStatus))
                    //{
                    //    case EmployerStatus.NewRegistration:
                    //        //cardColor = "border-danger";
                    //        sEmployerStatus = "New Registration";
                    //        break;
                    //    case EmployerStatus.RegistrationComplete:
                    //        //cardColor = "border-danger";
                    //        sEmployerStatus = "Registration Fee Due";
                    //        break;
                    //    case EmployerStatus.RegistrationFeePaid:
                    //        //cardColor = "caborderrd-primary";
                    //        sEmployerStatus = "Employer with no vacancy";
                    //        break;
                    //    case EmployerStatus.OpenVacancy:
                    //        //cardColor = "border-success";
                    //        sEmployerStatus = "Active Employer With Open Vacancy";
                    //        break;
                    //    case EmployerStatus.InProcessVacancy:
                    //        //cardColor = "border-primary";
                    //        sEmployerStatus = "InProcess Vacancies";
                    //        break;
                    //    case EmployerStatus.RecruitmentFeePaid:
                    //        //cardColor = "border-success";
                    //        sEmployerStatus = "Recruitment Fee paid";
                    //        break;
                    //    case EmployerStatus.FilledVacancy:
                    //        //cardColor = "border-primary";
                    //        sEmployerStatus = "Inactive Employer With Filled Vacacny";
                    //        break;
                    //    case EmployerStatus.RegistrationRenewalPending:
                    //        //cardColor = "border-danger";
                    //        sEmployerStatus = "Inactive Employer Registration Renewal Fee Due";
                    //        break;
                    //    case EmployerStatus.RegistrationRenewalFeePaid:
                    //        //cardColor = "border-success";
                    //        sEmployerStatus = "Action Employer Registration Renewed";
                    //        break;
                    //    default:
                    //        //cardColor = "border-secondary";
                    //        break;
                    //}

                    string ColorTag = string.Empty;
                    string EmployerBusinessLogo = string.Empty;
                    if (oEmployer[i].BusinessLogo != null)
                    {
                        EmployerBusinessLogo = Convert.ToBase64String(oEmployer[i].BusinessLogo);

                        imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";
                    }
                    else
                    {
                        imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                    }



                    if (oEmployer[i].EmployerStatus == EmployerListTabDDL.SelectedValue.ToString() || EmployerListTabDDL.SelectedValue.ToString() == "-1")
                    {
                        sEmployerCard =

                            "<div class=\"col-xs-6 col-md-4 py-2\">" +
                            "<div class=\"card h-100 "+ cardColor + "\">" +
                            "<div class=\"card-body-u\">" +
                            "<div class=\"col-sm-12\">" +
                            "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + sEmployerStatus + "</strong></i></h6>" +
                            "<div class=\"row\">" +
                            "<div class=\"float-left col p-2\">" +
                            "<h2>" + oEmployer[i].Name + "</h2>" +
                            "<p><i class=\"fa fa-envelope\"></i> <strong>Email: </strong> " + oEmployer[i].email + " </p>" +
                            "<ul class=\"list-unstyled\">" +
                            "<li><i class=\"fa fa-phone\"></i> <strong>Phone</strong>:" + oEmployer[i].WhatsAppNumber + "</li>" +
                            "</ul>" +
                            "</div>" +
                            "<div class=\"float-right col text-right mb-4\">" +
                            imagetag +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "<div class=\"profile-bottom\">" +
                            "<div class=\"float-left col-xs-6 p-2\">" +
                            "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oEmployer[i].LocationStateCode).StateName + "," + LocationManagement.GetStateDetail(oEmployer[i].LocationStateCode).Country.CountryName +
                            "</div>" +
                            "<div class=\"float-right col-xs-6\">" +
                            "<a href = \".\\EmployerDetails.aspx?EmployerId=" + oEmployer[i].EmployerID + "\"class=\"btn btn-success rounded-pill btn-sm\">" +
                            "<i class=\"fas fa-user\"></i>View Profile</a>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>" +
                            "</div>";

                        EmployerListLiteral.Text = EmployerListLiteral.Text + sEmployerCard;

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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            return bRetValue;
        }

        protected void PrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(EmployerListTabDDL.SelectedValue))
                {
                    case EmployerStatus.OpenVacancy:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.OpenVacancy, false);
                        break;
                    case EmployerStatus.RegistrationFeePaid:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.RegistrationFeePaid, false);
                        break;
                    case EmployerStatus.AllEmployers:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.AllEmployers, false);
                        break;
                    case EmployerStatus.FilledVacancy:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.FilledVacancy, false);
                        break;
                    case EmployerStatus.NewRegistration:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.NewRegistration, false);
                        break;
                    case EmployerStatus.InProcessVacancy:
                        Response.Redirect(@"~\PrintEmployerList.aspx?EmployerStatus=" + EmployerStatus.InProcessVacancy, false);
                        break;

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EmployerListTabDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetEmployerDetails();
                EmployerListLiteral.Text = "";
                ShowEmployerCard(0);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void LocationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sJobLocation = LocationDDL.SelectedValue;
                oEmployer.Clear();
                EmployerManagement.GetEmployerListByLocation(ref oEmployer, LocationDDL.SelectedValue);
                EmployerListLiteral.Text = "";
                ShowEmployerCard(0);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = true;
        }
    }
}