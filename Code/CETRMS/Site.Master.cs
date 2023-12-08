using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CETRMS;

namespace CETRMS
{
    public partial class SiteMaster : MasterPage
    {
        string EmployerSignUpRedirection_url = "https://localhost:44332/EmployerSignUp.aspx";
        string CandidateSignUpRedirection_url = "https://localhost:44332/CandidateSignUp.aspx";
        public static string UEClientID;
        public static int UEClientStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EmployerSignUpRedirection_url = ConfigurationManager.AppSettings["EmpSignUpRedirectURL"].ToString();
                CandidateSignUpRedirection_url = ConfigurationManager.AppSettings["CandSignUpRedirectURL"].ToString();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void EmployerGoogleSignup()
        {
            try
            {
                string clientid = ConfigurationManager.AppSettings["Googleclientid"];
                string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + EmployerSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void CandidateGoogleSignup()
        {
            try
            {
                string clientid = ConfigurationManager.AppSettings["Googleclientid"];
                string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + CandidateSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                CandidateManagement.CandidatePersonalProfileSignUp(UserProfile, ref UEClientID, ref UEClientStatus);
                Response.Redirect("./CandidateSignUp.aspx?Name=" + UserProfile.ProfileName, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                EmployerManagement.EmployerPersonalProfileSignUp(UserProfile, ref UEClientID, ref UEClientStatus);
                Response.Redirect("./EmployerSignUp.aspx?Name=" + UserProfile.ProfileName, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void UESigninLB_Click(object sender, EventArgs e)
        {
            try
            {
                int iRetValue = 0;
                string username;
                username = UserNameTXT.Text;
                string password;
                password = PasswordTXT.Text;
                iRetValue = RMSMasterManagement.AuthenticateUEStaff(username, password);
                UELoginLBL.Visible = true;
                switch (iRetValue)
                {
                    case 1:
                        Session["uerms_username"] = UserNameTXT.Text;
                        Response.Cookies["uerms_username"].Value = UserNameTXT.Text;
                        Response.Redirect("Dashboard.aspx", false);
                        break;
                    case -2:
                        UELoginLBL.Text = "Password is incorrect";
                        break;
                    case -1:
                        UELoginLBL.Text = "Username is incorrect";
                        break;
                }

                string msg = "$(document).ready(function() {\r\n";
                msg += "$('#exampleModal').modal('show');\r\n";
                msg += "});";

                String csname1 = "PopupScript";
                Type cstype = this.GetType();

                ClientScriptManager cs = Page.ClientScript;

                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    cs.RegisterStartupScript(cstype, csname1, msg, true);
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
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
                if (Request.Cookies["uerms_username"] != null && Request.Cookies["uerms_password"] != null)
                {
                    UserNameTXT.Text = Request.Cookies["uerms_username"].Value;
                    PasswordTXT.Attributes["Value"] = Request.Cookies["uerms_password"].Value;

                    int iRetValue = 0;
                    string username;
                    username = UserNameTXT.Text;
                    string password;
                    password = PasswordTXT.Text;
                    iRetValue = RMSMasterManagement.AuthenticateUEStaff(username, password);
                    UELoginLBL.Visible = true;

                    if (iRetValue == 1)
                    {
                        Session["uerms_username"] = UserNameTXT.Text;
                        Response.Cookies["uerms_username"].Value = UserNameTXT.Text;
                        Response.Redirect("Dashboard.aspx", false);
                    }

                }

                else
                {
                    ShowModel("LoginModalCenter");
                }
            }

            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.UE_LOGIN, Session["uerms_username"].ToString(), Message);
            }
        }
    }
}