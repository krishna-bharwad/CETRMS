using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class VacancyList : System.Web.UI.Page
    {

       public static List<Vacancy> oVacancyList = new List<Vacancy>();
        public static Employer oEmployer = new Employer();
        List<cState> VacancyStates = new List<cState>();
        public class cPagePanel
        {
            public string PanelName { get; set; }
            public string PanelURL { get; set; }
        }
        public static List<cPagePanel> PanelList = new List<cPagePanel>(); //Prashant
        public static int PageCount=1;
        public static int VacancyCount;
        public static string VacanyStatus;
        public static string VacancyCard;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.VACANCY_MANAGEMENT, Session["uerms_username"].ToString(), "VacancyList Page opened.");

                VacanyStatus = Request.QueryString.Get("VacancyStatus");

                if (!IsPostBack)
                {

                    GetVacancyTabDDL();
                    GetVacancyDetails();
                    //ShowVacancyList();
                    ShowVacancyListPage(0);
                    FillVacancyLocationDDL();
                }
                VacancyCount = oVacancyList.Count;
                PageCount = VacancyCount / 9;

                if (VacancyCount % 9 != 0) PageCount++;

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
                    //button.Attributes.Add("CssClass", "button-25");
                    button.Attributes.Add("AutoPostBack", "True");
                    //button.Click += Button_Click1;
                    button.Click += new EventHandler(Button_Click1);
                    PlaceHolder1.Controls.Add(button);             //PaginationPanel.Controls.Add(button);  
                }
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        protected void GetVacancyTabDDL()
        {
            try
            {
                for (int i = 0; i < VacancyLitstTabDDL.Items.Count; i++)
                {
                    if (VacancyLitstTabDDL.Items[i].Value == VacanyStatus)
                        VacancyLitstTabDDL.Items[i].Selected = true;
                    else
                        VacancyLitstTabDDL.Items[i].Selected = false;

                }
            }
            catch(Exception ex)
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
                oVacancyList.Clear();

                VacancyManager.GetVacancyListByLocation("all", ref oVacancyList, Convert.ToInt32(VacancyLitstTabDDL.SelectedValue));

                // Get Number of pages required to show vacancy cards[3x3] per page
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }

        }

        protected void FillVacancyLocationDDL()
        {
            try
            {
                LocationManagement.GetStateList("all", ref VacancyStates);
                EmployerLocationDDL.Items.Clear();
                EmployerLocationDDL.Items.Add("All");
                EmployerLocationDDL.Items[0].Value = "all";
                for (int i = 0; i < VacancyStates.Count; i++)
                {
                    EmployerLocationDDL.Items.Add(VacancyStates[i].StateName + "-" + VacancyStates[i].Country.CountryName);
                    EmployerLocationDDL.Items[i + 1].Value = VacancyStates[i].StateCode;
                }
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        protected bool ShowVacancyList()
        {
            bool bRetValue = false;
            try
            {
                string imagetag = string.Empty;
                string cardColor = string.Empty;
                string VacancyStatus = string.Empty;
                VacancyListLit.Text = "<asp:Panel id=\"" + PanelList[0].PanelName + "\" runat=\"server\" Visible=\"true\">\r\n<div class=\"row\">\r\n"; //Prashant
                int iPagePanel = 0;

                for (int i = 0; i < oVacancyList.Count; i++)
                {
                    switch (Convert.ToInt32(oVacancyList[i].VacancyStatusTypeID))
                    {
                        case cVacancyStatus.Open:
                            cardColor = "border-info";
                            VacancyStatus = "Open Vacancies";
                            break;
                        case cVacancyStatus.InProcess:
                            cardColor = "border-success";
                            VacancyStatus = "Vacancies Under Scheduled Interview";
                            break;
                        case cVacancyStatus.Close:
                            cardColor = "border-primary";
                            VacancyStatus = "Close Vacancies After Final Selection";
                            break;
                        default: cardColor = "card-secondary"; break;
                    }

                    if (oVacancyList[i].VacancyStatusTypeID == VacancyLitstTabDDL.SelectedValue.ToString() || VacancyLitstTabDDL.SelectedValue.ToString() == "0")
                    {
                        string EmployerId = oVacancyList[i].UEEmployerID;
                        EmployerManagement.GetEmployerByID(EmployerId, ref oEmployer, true);

                        string EmployerBusinessLogo = string.Empty;
                        if (oEmployer.BusinessLogo != null)
                        {
                            EmployerBusinessLogo = Convert.ToBase64String(oEmployer.BusinessLogo);

                            imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";
                        }
                        else
                        {
                            imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                        }
                        string VacancyCard = string.Empty; //Prashant
                        if (i % 9 == 0 && i != 0)
                        {
                            iPagePanel++;
                            VacancyCard = "</div>\r\n</asp:Panel>\r\n<asp:Panel id=\"" + PanelList[iPagePanel].PanelName + "\" runat=\"server\" Visible=\"false\">\r\n<div class=\"row\">\r\n";
                        }

                        VacancyCard = VacancyCard + " <div class=\"col-xs-6 col-md-4 py-2\">\r\n" +
                                "<div class=\"card h-100 " + cardColor + "\">\r\n" +
                                    "<div class=\"card-body-u\">\r\n" +
                                        " <div class=\"col-sm-12\">\r\n" +
                                            "<h6 class=\"brief\" align=\"right\"><i><strong>" + VacancyStatus + "</strong></i></h6>\r\n" +
                                           "<div class=\"row\">\r\n" +
                                            "<div class=\"float-left col p-2\">\r\n" +
                                                "<h2>" + oVacancyList[i].VacancyName + "</h2>\r\n" +
                                                "<p><strong>Vacancy Detail: </strong> " + oVacancyList[i].VacancyDetails + " </p>\r\n" +
                                                "<ul class=\"list-unstyled\">\r\n" +
                                                    "<li> <i class=\"fa fa-user-circle\"></i><strong>Employer Name:</strong>" + oEmployer.Name + "</li>\r\n" +
                                                "</ul>\r\n" +
                                            "</div>\r\n" +
                                            "<div class=\"float-right col text-right mb-4\">\r\n" +
                                                imagetag +
                                            "</div>\r\n" +
                                           "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class=\"profile-bottom text-center\">\r\n" +
                                            "<div class=\"float-left col-xs-6 p-2\">\r\n" +
                                                "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).StateName + "," + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).Country.CountryName +
                                            "</div>\r\n" +
                                            "<div class=\"float-right col-xs-6\">\r\n" +
                                                "<a href = \".\\JobVacancyDetails.aspx?VacancyId=" + oVacancyList[i].VacancyID + "\"class=\"btn btn-success rounded-pill btn-sm\">\r\n" +
                                                "<i class=\"fas fa-user\"></i>View Vacancy</a>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n";
                        VacancyListLit.Text = VacancyListLit.Text + VacancyCard;
                    }
                }
                VacancyListLit.Text = VacancyListLit.Text + "</div>\r\n</asp:Panel>\r\n";

            }
            catch(Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }
        protected bool ShowVacancyListPage(int PageNo)
        {
            bool bRetValue = false;
            try
            {
                string imagetag = string.Empty;
                string cardColor = string.Empty;
                string VacancyStatus = string.Empty;


                int StartCardNo = PageNo * 9;
                int EndCardNo = (PageNo + 1) * 9;

                for (int i = StartCardNo; i < EndCardNo && i < oVacancyList.Count; i++)
                {

                    switch (Convert.ToInt32(oVacancyList[i].VacancyStatusTypeID))
                    {
                        case cVacancyStatus.Open:
                            cardColor = "border-info";
                            VacancyStatus = "Open Vacancies";
                            break;
                        case cVacancyStatus.InProcess:
                            cardColor = "border-success";
                            VacancyStatus = "Vacancies Under Scheduled Interview";
                            break;
                        case cVacancyStatus.Close:
                            cardColor = "border-primary";
                            VacancyStatus = "Close Vacancies After Final Selection";
                            break;
                        default: cardColor = "card-secondary"; break;
                    }

                    if (oVacancyList[i].VacancyStatusTypeID == VacancyLitstTabDDL.SelectedValue.ToString() || VacancyLitstTabDDL.SelectedValue.ToString() == "0")
                    {
                        string EmployerId = oVacancyList[i].UEEmployerID;
                        EmployerManagement.GetEmployerByID(EmployerId, ref oEmployer, true);

                        string EmployerBusinessLogo = string.Empty;
                        if (oEmployer.BusinessLogo != null)
                        {
                            EmployerBusinessLogo = Convert.ToBase64String(oEmployer.BusinessLogo);

                            imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";
                        }
                        else
                        {
                            imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                        }

                        string sVacancyDeatils = string.Empty;

                        if (oVacancyList[i].VacancyDetails.Length < 50)
                        {
                            sVacancyDeatils = oVacancyList[i].VacancyDetails;
                        }

                        else
                        {
                            sVacancyDeatils = oVacancyList[i].VacancyDetails.Substring(0, 50).ToString() + "...";

                        }

                        string VacancyCard = " <div class=\"col-xs-12 col-sm-6 col-md-4 col-lg-4 py-2\">\r\n" +
                                "<div class=\"card h-100 " + cardColor + "\">\r\n" +
                                    "<div class=\"card-body-u\">\r\n" +
                                        " <div class=\"col-sm-12\">\r\n" +
                                            "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + VacancyStatus + "</strong></i></h6>\r\n" +
                                            "<div class=\"col-xs-12 col-sm-6 col-md-8\">\r\n" +
                                                "<h2>" + oVacancyList[i].VacancyName + "</h2>\r\n" +
                                                "<p><strong>Vacancy Detail: </strong> " + sVacancyDeatils + " </p>\r\n" +
                                                "<ul class=\"list-unstyled\">\r\n" +
                                                    "<li> <strong>Employer Name:</strong>" + oEmployer.Name + "</li>\r\n" +
                                                "</ul>\r\n" +
                                            "</div>\r\n" +
                                            "<div class=\"col-xs-6 col-md-4 mb-4\">\r\n" +
                                                imagetag +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class=\"profile-bottom text-center\">\r\n" +
                                            "<div class=\"float-left p-2 col-xs-6\">\r\n" +
                                                "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).StateName + "," + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).Country.CountryName +
                                            "</div>\r\n" +
                                            "<div class=\"float-right col-xs-6\">\r\n" +
                                                "<a href = \".\\JobVacancyDetails.aspx?VacancyId=" + oVacancyList[i].VacancyID + "\"class=\"btn btn-success rounded-pill btn-sm\">\r\n" +
                                                "<i class=\"fas fa-user\"></i>View Vacancy</a>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n";
                        VacancyListLit.Text = VacancyListLit.Text + VacancyCard;
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
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }
        protected bool ShowVacancyListPage(int StartCardNo, int EndCardNo)
        {
            bool bRetValue = false;
            try
            {
                string imagetag = string.Empty;
                string cardColor = string.Empty;
                string VacancyStatus = string.Empty;

                for (int i = StartCardNo; i < EndCardNo; i++)
                {
                    switch (Convert.ToInt32(oVacancyList[i].VacancyStatusTypeID))
                    {
                        case cVacancyStatus.Open:
                            cardColor = "border-info";
                            VacancyStatus = "Open Vacancies";
                            break;
                        case cVacancyStatus.InProcess:
                            cardColor = "border-success";
                            VacancyStatus = "Vacancies Under Scheduled Interview";
                            break;
                        case cVacancyStatus.Close:
                            cardColor = "border-primary";
                            VacancyStatus = "Close Vacancies After Final Selection";
                            break;
                        default: cardColor = "card-secondary"; break;
                    }

                    if (oVacancyList[i].VacancyStatusTypeID == VacancyLitstTabDDL.SelectedValue.ToString() || VacancyLitstTabDDL.SelectedValue.ToString() == "0")
                    {
                        string EmployerId = oVacancyList[i].UEEmployerID;
                        EmployerManagement.GetEmployerByID(EmployerId, ref oEmployer, true);

                        string EmployerBusinessLogo = string.Empty;
                        if (oEmployer.BusinessLogo != null)
                        {
                            EmployerBusinessLogo = Convert.ToBase64String(oEmployer.BusinessLogo);

                            imagetag = "<img src = \"data:image/jpg; base64, " + EmployerBusinessLogo + " \" alt=\"\" class=\"img-circle img-fluid\">";
                        }
                        else
                        {
                            imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-circle img-fluid\">";
                        }

                        string VacancyCard = " <div class=\"col-xs-12 col-sm-6 col-md-4 col-lg-4 py-2\">\r\n" +
                                "<div class=\"card h-100 " + cardColor + "\">\r\n" +
                                    "<div class=\"card-body-u\">\r\n" +
                                        " <div class=\"col-sm-12\">\r\n" +
                                            "<h6 class=\"brief mb-4\" align=\"right\"><i><strong>" + VacancyStatus + "</strong></i></h6>\r\n" +
                                            "<div class=\"col-xs-12 col-sm-6 col-md-8\">\r\n" +
                                                "<h2>" + oVacancyList[i].VacancyName + "</h2>\r\n" +
                                                "<p><strong>Vacancy Detail: </strong> " + oVacancyList[i].VacancyDetails + " </p>\r\n" +
                                                "<ul class=\"list-unstyled\">\r\n" +
                                                    "<li> <strong>Employer Name:</strong>" + oEmployer.Name + "</li>\r\n" +
                                                "</ul>\r\n" +
                                            "</div>\r\n" +
                                            "<div class=\"col-xs-6 col-md-4 mb-4\">\r\n" +
                                                imagetag +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                        "<div class=\"profile-bottom text-center\">\r\n" +
                                            "<div class=\"float-left p-2 col-xs-6\">\r\n" +
                                                "<i class=\"fa fa-map-marker\"></i> <strong>Location:</strong> " + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).StateName + "," + LocationManagement.GetStateDetail(oVacancyList[i].PrimaryLocation).Country.CountryName +
                                            "</div>\r\n" +
                                            "<div class=\"float-right col-xs-6\">\r\n" +
                                                "<a href = \".\\JobVacancyDetails.aspx?VacancyId=" + oVacancyList[i].VacancyID + "\"class=\"btn btn-success rounded-pill btn-sm\">\r\n" +
                                                "<i class=\"fas fa-user\"></i>View Profile</a>\r\n" +
                                            "</div>\r\n" +
                                        "</div>\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n" +
                                "</div>\r\n";
                        VacancyListLit.Text = VacancyListLit.Text + VacancyCard;
                    }
                }
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }
        private void Button_Click1(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string buttonId = button.ID;
                VacancyListLit.Text = "";

                ShowVacancyListPage(Convert.ToInt32(buttonId));
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected void VacancyLitstTabDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetVacancyDetails();
                VacancyListLit.Text = "";
                ShowVacancyListPage(0);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected void VacancyListPrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(VacancyLitstTabDDL.SelectedValue))
                {
                    case 0:
                        Response.Redirect(@"~\PrintVacancyList.aspx?VacancyStatus=0", false);
                        break;
                    case cVacancyStatus.Open:
                        Response.Redirect(@"~\PrintVacancyList.aspx?VacancyStatus=1", false);
                        break;
                    case cVacancyStatus.InProcess:
                        Response.Redirect(@"~\PrintVacancyList.aspx?VacancyStatus=5", false);
                        break;
                    case cVacancyStatus.Close:
                        Response.Redirect(@"~\PrintVacancyList.aspx?VacancyStatus=4", false);
                        break;
                }
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected void EmployerLocationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sJobLocation = EmployerLocationDDL.SelectedValue;
                oVacancyList.Clear();
                VacancyManager.GetVacancyListByLocation(sJobLocation, ref oVacancyList, 0);
                VacancyListLit.Text = "";
                ShowVacancyListPage(0);
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            PlaceHolder1.Visible = true;
        }
    }
}