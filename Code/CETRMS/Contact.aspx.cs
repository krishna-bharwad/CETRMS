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
    public partial class Contact : System.Web.UI.Page
    {
        //string EmployerSignUpRedirection_url = "https://localhost:44342/EmployerSigUp.aspx";
        string EmployerSignUpRedirection_url = "https://localhost:44382/EmployerSignUp.aspx";
        string CandidateSignUpRedirection_url = "https://localhost:44382/CandidateSignUp.aspx";
        public static string CETClientID;
        public static int UEClientStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            EmployerSignUpRedirection_url = ConfigurationManager.AppSettings["EmpSignUpRedirectURL"].ToString();
            CandidateSignUpRedirection_url = ConfigurationManager.AppSettings["CandSignUpRedirectURL"].ToString();

        }
        protected void GoogleSignUp(object sender, EventArgs e)
        {
            if (loginProspectHF.Value == "Candidate")
                CandidateGoogleSignup();
            else
                EmployerGoogleSignup();
        }
        protected void FacebookSignUp(object sender, EventArgs e)
        {
            if (loginProspectHF.Value == "Candidate")
                CandidateFacebookSignup();
            else
                EmployerFacebookSignup();
        }
        protected void EmployerGoogleSignup()
        {
            string clientid = ConfigurationManager.AppSettings["Googleclientid"];
            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + EmployerSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
            Response.Redirect(url, false);
        }

        protected void EmployerFacebookSignup()
        {
            string clientId = ConfigurationManager.AppSettings["FacebookclientId"];
            string url = string.Format("https://www.facebook.com/v16.0/dialog/oauth?client_id={0}&redirect_uri={1}", clientId, EmployerSignUpRedirection_url);
            Response.Redirect(url, false);
        }
        protected void CandidateGoogleSignup()
        {
            string clientid = ConfigurationManager.AppSettings["Googleclientid"];
            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=profile&include_granted_scopes=true&redirect_uri=" + CandidateSignUpRedirection_url + "&response_type=code&client_id=" + clientid + "";
            Response.Redirect(url, false);
        }

        protected void CandidateFacebookSignup()
        {
            string clientId = ConfigurationManager.AppSettings["FacebookclientId"];
            string url = string.Format("https://www.facebook.com/v16.0/dialog/oauth?client_id={0}&redirect_uri={1}", clientId, CandidateSignUpRedirection_url);
            Response.Redirect(url,false);
        }
        protected void CandidatePersonalProfileSignup(object sender, EventArgs e)
        {
            PersonalProfileClass UserProfile = new PersonalProfileClass();
            UserProfile.ProfileName = CETRMS_CandidateUserNameTXT.Text.Trim(); // Enter ProfileName
            UserProfile.email = CETRMS_CandidateEmailTXT.Text.Trim(); // Enter Email
            UserProfile.Password = CETRMS_CandidatePasswordTXT.Text; //Enter Password
            CandidateManagement.CandidatePersonalProfileSignUp(UserProfile, ref CETClientID, ref UEClientStatus);
            Response.Redirect("./CandidateSignUp.aspx?Name=" + UserProfile.ProfileName, false);

        }
        protected void EmployerPersonalProfileSignup(object sender, EventArgs e)
        {
            PersonalProfileClass UserProfile = new PersonalProfileClass();
            UserProfile.ProfileName = CETRMS_EmployerUserNameTXT.Text.Trim(); // Enter ProfileName
            UserProfile.email = CETRMS_EmployerEmailTXT.Text.Trim(); // Enter Email
            UserProfile.Password = CETRMS_EmployerPasswordTXT.Text; //Enter Password
            EmployerManagement.EmployerPersonalProfileSignUp(UserProfile, ref CETClientID, ref UEClientStatus);
            Response.Redirect("./EmployerSignUp.aspx?Name=" + UserProfile.ProfileName, false);
        }

        protected void CETSigninLB_Click(object sender, EventArgs e)
        {
            int iRetValue = 0;
            string username;
            username = UserNameTXT.Text;
            string password;
            password = PasswordTXT.Text;
            iRetValue = RMSMasterManagement.AuthenticateUEStaff(username, password);
            CETLoginLBL.Visible = true;
            switch (iRetValue)
            {
                case 1:
                    Session["cetrms_username"] = UserNameTXT.Text;
                    Response.Cookies["cetrms_username"].Value = UserNameTXT.Text;
                    Response.Redirect("Dashboard.aspx", false);
                    break;
                case -2:
                    CETLoginLBL.Text = "Password is incorrect";
                    Response.Redirect("Newindex.aspx", false);
                    break;
                case -1:
                    CETLoginLBL.Text = "Username is incorrect";
                    Response.Redirect("Newindex.aspx", false);
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
    }

}