using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class VerifyCandidateEmail : System.Web.UI.Page
    {
        public string CandidateID;
        public static Candidate candidate = new Candidate();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string code = Request.QueryString["code"].ToString();

                CandidateID = EncDec.Decrypt(code, ConfigurationManager.AppSettings["EncryptionPassword"]);
                CandidateManagement.GetCandidatePersonalDetails(CandidateID, ref candidate);

                if (candidate.PersonalProfile.VerifyEmail == "1")
                {
                    VerificationStatusLBL.Text = "Your email is already verified.";
                }
                else
                {
                    if (EmployerManagement.VerifyEmployerEmail(CandidateID, RetValue.Success) == RetValue.Success)
                    {
                        VerificationStatusLBL.Text = "Email successfully verified.";
                    }
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
    }
}