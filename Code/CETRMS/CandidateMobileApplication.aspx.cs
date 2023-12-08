using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class CandidateMobileApplication: System.Web.UI.Page
    {
        //string EmployerSignUpRedirection_url = "https://localhost:44342/EmployerSigUp.aspx";
        string EmployerSignUpRedirection_url = "https://localhost:44332/EmployerSignUp.aspx";
        string CandidateSignUpRedirection_url = "https://localhost:44332/CandidateSignUp.aspx";
        public static string UEClientID;
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
            Response.Redirect(url, false);
        }
        protected void CandidatePersonalProfileSignup(object sender, EventArgs e)
        {
            PersonalProfileClass UserProfile = new PersonalProfileClass();
            UserProfile.ProfileName = CETRMS_CandidateUserNameTXT.Text.Trim(); // Enter ProfileName
            UserProfile.email = CETRMS_CandidateEmailTXT.Text.Trim(); // Enter Email
            UserProfile.Password = CETRMS_CandidatePasswordTXT.Text; //Enter Password
            CandidateManagement.CandidatePersonalProfileSignUp(UserProfile, ref UEClientID, ref UEClientStatus);
            Response.Redirect("./CandidateSignUp.aspx?Name=" + UserProfile.ProfileName, false);

        }
        protected void EmployerPersonalProfileSignup(object sender, EventArgs e)
        {
            PersonalProfileClass UserProfile = new PersonalProfileClass();
            UserProfile.ProfileName = CETRMS_EmployerUserNameTXT.Text.Trim(); // Enter ProfileName
            UserProfile.email = CETRMS_EmployerEmailTXT.Text.Trim(); // Enter Email
            UserProfile.Password = CETRMS_EmployerPasswordTXT.Text; //Enter Password
            EmployerManagement.EmployerPersonalProfileSignUp(UserProfile, ref UEClientID, ref UEClientStatus);
            Response.Redirect("./EmployerSignUp.aspx?Name=" + UserProfile.ProfileName, false);
        }

        protected void UESigninLB_Click(object sender, EventArgs e)
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
        protected void EmployerAPKDownload_Click(object sender, EventArgs e)
        {
            string _Path = HttpContext.Current.Server.MapPath(@".\MobileApp\Employer\Employer.apk");
            System.IO.FileInfo file = new System.IO.FileInfo(_Path);
            Response.Write(_Path);

            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name + ";");
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/x-unknown";
                Response.TransmitFile(file.FullName);
                Response.Flush();
                Response.End();

            }
            else
            {
                Response.Write("This file does not exist.");
            }

        }

        protected void CandidateAPKDownload_Click(object sender, EventArgs e)
        {
            string _Path = HttpContext.Current.Server.MapPath(@".\MobileApp\Candidate\Candidate.apk");
            System.IO.FileInfo file = new System.IO.FileInfo(_Path);
            Response.Write(_Path);

            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name + ";");
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/x-unknown";
                Response.TransmitFile(file.FullName);
                Response.Flush();
                Response.End();

            }
            else
            {
                Response.Write("This file does not exist.");        
            }

        }
    }
}
