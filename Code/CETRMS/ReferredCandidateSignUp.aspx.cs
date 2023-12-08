using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CETRMS
{
    public partial class ReferredCandidateSignUp : System.Web.UI.Page
    {
        string RefCode = string.Empty;
        string CandidateId = string.Empty;
        string RTextCode;
        public static string UEClientID;
        public static int UEClientStatus;
        public static string ClientId;
        public static int ClientStatus;
        string CandidateSignUpRedirection_url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RefCode = Request.QueryString["ReferralCode"];
                RCode.Text = RefCode.Substring(RefCode.IndexOf('=') + 1);
                RTextCode = RCode.Text;
                CandidateSignUpRedirection_url = ConfigurationManager.AppSettings["CandSignUpRedirectURL"] + "?ReferralCode=" + RefCode;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        protected void RCode_TextChanged(object sender, EventArgs e)
        {
            RTextCode = RCode.Text;
        }
        protected void CandidateSignUpPage(object sender, EventArgs e)
        {
            try
            {
                if (ReferralManagement.GetCandidateIDByReferralCode(RTextCode, ref CandidateId) == 1)
                {

                    PersonalProfileClass UserProfile = new PersonalProfileClass();
                    UserProfile.ProfileName = UserName.Text.Trim(); // Enter ProfileName
                    UserProfile.email = Email.Text.Trim(); // Enter Email
                    UserProfile.Password = Password.Text; //Enter Password
                    CandidateManagement.CandidatePersonalProfileSignUp(UserProfile, ref UEClientID, ref UEClientStatus);
                    ReferralManagement.InsertReferralDetails(UEClientID, RTextCode);
                    Response.Redirect("./CandidateSignUp.aspx?Name=" + UserProfile.ProfileName, false);
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
        protected void GoogleSignUp(object sender, EventArgs e)
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        protected void FacebookSignUp(object sender, EventArgs e)
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }      
    }
}