using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class AllEmployerList : System.Web.UI.Page
    {
        public List<UEClient> oUEClient = new List<UEClient>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (!IsPostBack)
                {
                    GetEmployerList();
                    GetNewEmpRegList();
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

        protected void GetEmployerList()
        {
            try
            {
                oUEClient.Clear();
                RMSMasterManagement.GetSignUpDetails(1.ToString(), ref oUEClient);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GetNewEmpRegList()
        {
            try
            {


                DataTable NewCandidateList = new DataTable();

                NewCandidateList.Columns.Add("Employer Name");
                NewCandidateList.Columns.Add("Email ID");
                NewCandidateList.Columns.Add("SignedUpOn");
                // NewCandidateList.Columns.Add("Picture");

                for (int i = 0; i < oUEClient.Count; i++)
                {
                    //string CandidatePhoto = string.Empty;
                    // string imagetag = string.Empty;

                    //CandidatePhoto = oUEClient[i].AuthenticationProfileURL;
                    // CandidateImage.ImageUrl = String.Format("data:image/jpeg;base64,{0}", CandidatePhoto);

                    // CandidatePhoto = oUEClient[i].AuthenticationProfileURL;

                    // imagetag = "<img src = \"data:image/jpg; base64, " + CandidatePhoto + " \" alt=\"\" class=\"img-circle img-fluid\">";


                    NewCandidateList.Rows.Add(oUEClient[i].AuthenticationName,
                                              oUEClient[i].AuthenticationId,
                                              oUEClient[i].SignedUpOn
                                            );

                }

                NewEmployerGV.DataSource = NewCandidateList;
                NewEmployerGV.DataBind();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void NewEmployerGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                NewEmployerGV.PageIndex = e.NewPageIndex;
                GetEmployerList();
                GetNewEmpRegList();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void PrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"~\PrintEmployerSignUp.aspx", false);
        }
    }
}