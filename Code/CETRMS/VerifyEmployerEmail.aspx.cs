using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class VerifyEmployerEmail : System.Web.UI.Page
    {
        public string EmployerID;
        public static Employer employer = new Employer();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string code = Request.QueryString["code"].ToString();

                code = EncDec.Decrypt(code, ConfigurationManager.AppSettings["EncryptionPassword"]);

                EmployerID = code.Substring(code.IndexOf("-") + 1);
                EmployerManagement.GetEmployerByID(EmployerID, ref employer);

                if (employer.VerifyEmail == "1")
                {
                    VerificationStatusLBL.Text = "Your email is already verified.";
                }
                else
                {
                    if (EmployerManagement.VerifyEmployerEmail(EmployerID, RetValue.Success) == RetValue.Success)
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, "", Message);
            }
        }
    }
}