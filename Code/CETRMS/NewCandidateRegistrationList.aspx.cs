using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class NewCandidateRegistrationList : System.Web.UI.Page
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
                    GetCandidateList();
                    GetNewCanRegList();
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

        protected void GetCandidateList()
        {
            try
            {
                oUEClient.Clear();
                RMSMasterManagement.GetSignUpDetails(2.ToString(), ref oUEClient);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }

        }

        protected void GetNewCanRegList()
        {
            try
            {


                DataTable NewCandidateList = new DataTable();

                NewCandidateList.Columns.Add("Candidate Name");
                NewCandidateList.Columns.Add("Email ID");
                NewCandidateList.Columns.Add("SignedUp Time");
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

                NewCanRegListGV.DataSource = NewCandidateList;
                NewCanRegListGV.DataBind();
                
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }

        protected void NewCanRegListGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                NewCanRegListGV.PageIndex = e.NewPageIndex;
                NewCanRegListGV.SelectedIndex = -1;
                GetCandidateList();
                GetNewCanRegList();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }

        protected void PrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"~\PrintCandidateSignup.aspx", false);
        }
    }
}