using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CETRMS;

namespace CETRMS
{
    public partial class Newindex : System.Web.UI.Page
    {
        //string EmployerSignUpRedirection_url = "https://localhost:44342/EmployerSigUp.aspx";
        string EmployerSignUpRedirection_url = "https://localhost:44382/EmployerSignUp.aspx";
        string CandidateSignUpRedirection_url = "https://localhost:44382/CandidateSignUp.aspx";
        public static string CETClientID;
        public static int UEClientStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EmployerSignUpRedirection_url = ConfigurationManager.AppSettings["EmpSignUpRedirectURL"].ToString();
                CandidateSignUpRedirection_url = ConfigurationManager.AppSettings["CandSignUpRedirectURL"].ToString();

                if (!IsPostBack)
                {
                    //if (Request.Cookies["cetrms_username"] != null && Request.Cookies["uerms_password"] != null)
                    //{
                    //    UserNameTXT.Text = Request.Cookies["cetrms_username"].Value;
                    //    PasswordTXT.Attributes["Value"] = Request.Cookies["uerms_password"].Value;
                    //}

                }
                ShowTestimonials();
                ShowIndexParameter();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }

        }
        protected void GoogleSignUp(object sender, EventArgs e)
        {
            try
            {
                if (loginProspectHF.Value == "Candidate")
                    CandidateGoogleSignup();
                else
                    EmployerGoogleSignup();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void FacebookSignUp(object sender, EventArgs e)
        {
            try
            {
                if (loginProspectHF.Value == "Candidate")
                    CandidateFacebookSignup();
                else
                    EmployerFacebookSignup();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void EmployerGoogleSignup()
        {
            try
            {
                string clientid = ConfigurationManager.AppSettings["Googleclientid"];
                string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile email&include_granted_scopes=true&redirect_uri=" + EmployerSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }

        protected void EmployerFacebookSignup()
        {
            try
            {
                string clientId = ConfigurationManager.AppSettings["FacebookclientId"];
                string url = string.Format("https://www.facebook.com/v16.0/dialog/oauth?client_id={0}&redirect_uri={1}", clientId, EmployerSignUpRedirection_url);
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void CandidateGoogleSignup()
        {
            try
            {
                string clientid = ConfigurationManager.AppSettings["Googleclientid"];
                string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile email&include_granted_scopes=true&redirect_uri=" + CandidateSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }

        protected void CandidateFacebookSignup()
        {
            try
            {
                string clientId = ConfigurationManager.AppSettings["FacebookclientId"];
                string url = string.Format("https://www.facebook.com/v16.0/dialog/oauth?client_id={0}&redirect_uri={1}", clientId, CandidateSignUpRedirection_url);
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void CandidatePersonalProfileSignup(object sender, EventArgs e)
        {
            try
            {
                PersonalProfileClass UserProfile = new PersonalProfileClass();
                UserProfile.ProfileName = CETRMS_CandidateUserNameTXT.Text.Trim(); // Enter ProfileName
                UserProfile.email = CETRMS_CandidateEmailTXT.Text.Trim(); // Enter Email
                UserProfile.Password = CETRMS_CandidatePasswordTXT.Text; //Enter Password
                CandidateManagement.CandidatePersonalProfileSignUp(UserProfile, ref CETClientID, ref UEClientStatus);
                Response.Redirect("./CandidateSignUp.aspx?Name=" + UserProfile.ProfileName, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }

        }
        protected void EmployerPersonalProfileSignup(object sender, EventArgs e)
        {
            try
            {
                PersonalProfileClass UserProfile = new PersonalProfileClass();
                UserProfile.ProfileName = CETRMS_EmployerUserNameTXT.Text.Trim(); // Enter ProfileName
                UserProfile.email = CETRMS_EmployerEmailTXT.Text.Trim(); // Enter Email
                UserProfile.Password = CETRMS_EmployerPasswordTXT.Text; //Enter Password
                EmployerManagement.EmployerPersonalProfileSignUp(UserProfile, ref CETClientID, ref UEClientStatus);
                Response.Redirect("./EmployerSignUp.aspx?Name=" + UserProfile.ProfileName, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }

        protected void CETSigninLB_Click(object sender, EventArgs e)
        {
            try
            {
                int iRetValue = 0;
                string username;
                username = UserNameTXT.Text;
                string password;
                password = PasswordTXT.Text;
                iRetValue = RMSMasterManagement.AuthenticateUEStaff(username, password);
                CETLoginLBL.Visible = true;

                if (RememberCHK.Checked)
                {
                    Response.Cookies["cetrms_username"].Value = UserNameTXT.Text;
                    Response.Cookies["uerms_password"].Value = PasswordTXT.Text;
                    Response.Cookies["cetrms_username"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["uerms_password"].Expires = DateTime.Now.AddDays(30);

                }

                else
                {
                    Response.Cookies["cetrms_username"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["uerms_password"].Expires = DateTime.Now.AddDays(-1);
                }

                switch (iRetValue)
                {
                    case 1:
                        Session["cetrms_username"] = UserNameTXT.Text;
                        Response.Cookies["cetrms_username"].Value = UserNameTXT.Text;
                        Response.Redirect("Dashboard.aspx", false);
                        break;
                    case -1:
                        CETLoginLBL.Text = "Password is incorrect";
                        //Response.Redirect("Newindex.aspx");
                        break;
                    case -2:
                        CETLoginLBL.Text = "Username is incorrect";
                        //Response.Redirect("Newindex.aspx");
                        break;
                }
                ShowModel("LoginModalCenter");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void ShowTestimonials()
        {
            try
            {
                List<Testimonial> testimonials = new List<Testimonial>();
                TestimonialManager.GetAllTestimonialList(ref testimonials, 1);

                if (testimonials.Count > 0)
                {
                    TestimonialLit.Text = "";
                    foreach (Testimonial testimonial in testimonials)
                    {
                        Candidate candidate = new Candidate();
                        CandidateManagement.GetCandidateFullDetails(testimonial.CETClientID, ref candidate);
                        cState state = new cState();
                        state = LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode);
                        string Location = state.StateName + ", " + state.Country.CountryName;
                        string CandidatePhotoMem = string.Empty;
                        string imagetag = string.Empty;
                        string StarRatingImage = string.Empty;
                        string ColorTag = string.Empty;

                        if (candidate.PersonalProfile.Photo != null)
                        {
                            CandidatePhotoMem = Convert.ToBase64String(candidate.PersonalProfile.Photo);
                            imagetag = "<img src = \"data:image/jpg; base64, " + CandidatePhotoMem + " \" alt=\"\" class=\"img-fluid\">";
                        }
                        else
                        {
                            imagetag = "<img src=\"images/user.png\" alt=\"\" class=\"img-fluid\">";
                        }
                        if (candidate.PersonalProfile.Photo != null)
                            CandidatePhotoMem = Convert.ToBase64String(candidate.PersonalProfile.Photo);

                        switch (testimonial.Rating)
                        {
                            case 1:
                                StarRatingImage = "one-star.png";
                                break;
                            case 2:
                                StarRatingImage = "two-star.png";
                                break;
                            case 3:
                                StarRatingImage = "three-star.png";
                                break;
                            case 4:
                                StarRatingImage = "four-star.png";
                                break;
                            case 5:
                                StarRatingImage = "five-star.png";
                                break;
                        }

                        string TestimonialMessage = "<div class=\"item\">\r\n" +
                                    "<p>“" + testimonial.ResponseMessage + "”</p>\r\n" +
                                    "<div class=\"author\">\r\n" +
                                    imagetag +
                                    "<h4>" + candidate.PersonalProfile.Name + "</h4>\r\n" +
                                    "<div class=\"row\">\r\n" +
                                    "<div class=\"col\">\r\n" +
                                    "<span class=\"category\">" + Location + "</span>\r\n" +
                                    "</div>\r\n" +
                                    "<div class=\"col\">\r\n" +
                                    "<img id =\"RatingIMG\" src=\"images/" + StarRatingImage + "\" style=\"height:25px;width:100px;\">\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n" +
                                    "</div>\r\n";

                        TestimonialLit.Text = TestimonialLit.Text + TestimonialMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        public void ShowModel(string ModelName)
        {
            string msg = "$(document).ready(function() {\r\n";
            msg += "$('#" + ModelName + "').modal('show');\r\n";
            msg += "});";
            String csname1 = "PopupScript";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                cs.RegisterStartupScript(cstype, csname1, msg, true);
            }
        }

        protected void LoginModalLB_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cetrms_username"] != null && Request.Cookies["uerms_password"] != null)
                {
                    UserNameTXT.Text = Request.Cookies["cetrms_username"].Value;
                    PasswordTXT.Attributes["Value"] = Request.Cookies["uerms_password"].Value;

                    int iRetValue = 0;
                    string username;
                    username = UserNameTXT.Text;
                    string password;
                    password = PasswordTXT.Text;
                    iRetValue = RMSMasterManagement.AuthenticateUEStaff(username, password);
                    CETLoginLBL.Visible = true;

                    if (iRetValue == 1)
                    {
                        Session["cetrms_username"] = UserNameTXT.Text;
                        Response.Cookies["cetrms_username"].Value = UserNameTXT.Text;
                        Response.Redirect("Dashboard.aspx", false);
                    }

                }

                else
                {
                    ShowModel("LoginModalCenter");
                }
            }
            catch (ThreadAbortException ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", Message);

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
        protected void ShowIndexParameter()
        {
            try
            {
                string IndexJSON = string.Empty;
                RMSMasterManagement.GetIndexPageParameters(ref IndexJSON);
                dynamic CandidateDashboardData = JsonConvert.DeserializeObject(IndexJSON);
                CandidateCountLBL.Text = CandidateDashboardData.CandidatesOnBoarded;
                EmployerCountLBL.Text = CandidateDashboardData.EmployersOnBoarded;
                LocationsLBL.Text = CandidateDashboardData.Locations;
                VacanciesPublishedLBL.Text = CandidateDashboardData.VacanciesPublished;
                ScheduledInterviewsLBL.Text = CandidateDashboardData.ScheduledInterviews;
                JobApplicationLBL.Text = CandidateDashboardData.JobApplication;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
        }
    }
}