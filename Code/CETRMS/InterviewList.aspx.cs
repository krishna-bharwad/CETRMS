using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class InterviewList : System.Web.UI.Page
    {
        public static List<Interview> oInterview = new List<Interview>();
        public static string iInterviewStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                iInterviewStatus = Request.QueryString.Get("InterviewStatus");
                if (!IsPostBack)
                {
                    FnInterviewStatus();
                    GetInterviewDetails();
                    GetInterviewListGV();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void FnInterviewStatus()
        {
            try
            {
                for (int i = 0; i < InterviewStatus.Items.Count; i++)
                {
                    if (InterviewStatus.Items[i].Value == iInterviewStatus)
                        InterviewStatus.Items[i].Selected = true;
                    else
                        InterviewStatus.Items[i].Selected = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GetInterviewDetails()
        {
            try
            {
                oInterview.Clear();
                InterviewManagement.GetInterviewListByStatus(ref oInterview, Convert.ToInt32(InterviewStatus.SelectedValue));
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GetInterviewListGV()
        {
            try
            {
                DataTable InterviewList = new DataTable();
                //oInterview.Clear();
                //InterviewManagement.GetInterviewListByStatus(ref oInterview, Convert.ToInt32(iInterviewStatus));

                //InterviewList.Columns.AddRange(new DataColumn[7]{
                //                                new DataColumn("InterviewID",typeof(string)),
                //                                new DataColumn("InterviewStatus",typeof(string)),
                //                                new DataColumn("CandidateRemarks",typeof(string)),
                //                                new DataColumn("ChosenTimeZone",typeof(string)),
                //                                new DataColumn("EmployerRemarks",typeof(string)),
                //                                new DataColumn("PreferredDateTime",typeof(DateTime)),
                //                                new DataColumn("DurationInMinutes",typeof(int)),

                // });

                InterviewList.Columns.Add("Interview ID");
                InterviewList.Columns.Add("InterviewID");
                InterviewList.Columns.Add("InterviewStatus");
                InterviewList.Columns.Add("CandidateRemarks");
                InterviewList.Columns.Add("ChosenTimeZone");
                InterviewList.Columns.Add("EmployerRemarks");
                InterviewList.Columns.Add("PreferredDateTime");
                InterviewList.Columns.Add("DurationInMinutes");





                for (int i = 0; i < oInterview.Count;i++)
                {

                    string AllInterviewStatus = string.Empty;
                    switch (oInterview[i].InterviewStatus)
                    {
                        case InterviewCallStatus.InterviewProposed:
                            AllInterviewStatus = "Interview Proposed";
                            break;
                        case InterviewCallStatus.InterviewScheduled:
                            AllInterviewStatus = "Interview Scheduled";
                            break;
                        case InterviewCallStatus.InterviewStarted:
                            AllInterviewStatus = "InterviewStarted";
                            break;
                        case InterviewCallStatus.InterviewDropped:
                            AllInterviewStatus = "Interview Dropped";
                            break;
                        case InterviewCallStatus.InterviewCancelled:
                            AllInterviewStatus = "Interview Cancelled";
                            break;
                        case InterviewCallStatus.InterviewRejected:
                            AllInterviewStatus = "Interview Rejected";
                            break;
                        case InterviewCallStatus.InterviewCompleted:
                            AllInterviewStatus = "Interview Completed";
                            break;
                    }

                    InterviewList.Rows.Add(oInterview[i].InterviewID,
                                            oInterview[i].InterviewID,
                                            AllInterviewStatus,
                                            oInterview[i].CandidateRemarks,
                                            oInterview[i].ChosenTimeZone,
                                            oInterview[i].EmployerRemarks,
                                            oInterview[i].PreferredDateTime,
                                            oInterview[i].DurationInMinutes);
                }

                GVInterviewList.DataSource = InterviewList;
                GVInterviewList.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void InterviewListPrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(InterviewStatus.SelectedValue))
                {
                    case InterviewCallStatus.InterviewProposed:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewProposed, false);
                        break;
                    case InterviewCallStatus.InterviewScheduled:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewScheduled, false);
                        break;
                    case InterviewCallStatus.InterviewStarted:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewStarted, false);
                        break;
                    case InterviewCallStatus.InterviewCompleted:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewCompleted, false);
                        break;
                    case InterviewCallStatus.InterviewDropped:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewDropped, false);
                        break;
                    case InterviewCallStatus.InterviewCancelled:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewCancelled, false);
                        break;
                    case InterviewCallStatus.InterviewRejected:
                        Response.Redirect(@"~\PrintInterviewList.aspx?InterviewStatus=" + InterviewCallStatus.InterviewRejected, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void InterviewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInterviewDetails();
            GetInterviewListGV();

        }

      
        private void ILB_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton olinkButton = (LinkButton)sender;
                string IntID = olinkButton.CommandArgument.ToString();
                Response.Redirect(URLs.InterviewDetailsURL + IntID, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void InterviewListLB_Click(object sender, EventArgs e)
        {

        }

        protected void InterviewID_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton olinkButton = (LinkButton)sender;
                string IntID = olinkButton.CommandArgument.ToString();
                Response.Redirect("InterviewDetails.aspx?InterviewID=" + IntID, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GVInterviewList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
              
                 if (e.Row.RowType == DataControlRowType.DataRow)
                {

                        string sInterviewId = e.Row.Cells[0].Text;

                        HyperLink InterviewDetailHL = new HyperLink();
                        InterviewDetailHL.Text = sInterviewId;
                        InterviewDetailHL.NavigateUrl = ResolveUrl(URLs.InterviewDetailsURL + sInterviewId);


                        e.Row.Cells[0].Controls.Add(InterviewDetailHL);


                }
                 if(e.Row.Cells.Count>2)
                e.Row.Cells[1].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void GVInterviewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVInterviewList.PageIndex = e.NewPageIndex;
                GetInterviewListGV();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
    }
}