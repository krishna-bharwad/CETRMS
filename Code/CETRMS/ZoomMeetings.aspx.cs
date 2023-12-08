using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CETRMS
{
    public partial class ZoomMeetings : System.Web.UI.Page
    {
       List<Interview> interview = new List<Interview>();
       string InterviewStatus = string.Empty;
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
                    InterviewManagement.GetInterviewListByStatus(ref interview);
                    GetAllZoomMeetings();
                }
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        protected void GetAllZoomMeetings()
        {
            try
            {
                DataTable dt = new DataTable();
                int count = 1;
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("InterviewID", typeof(string)),
                new DataColumn("Sr no", typeof(string)),
                new DataColumn("ZoomMeetingId", typeof(string)),
                new DataColumn("Topic", typeof(string)),
                new DataColumn("StartTime", typeof(string)),
                new DataColumn("TimeZone", typeof(string)),
                new DataColumn("InterviewStatus", typeof(string)) });

                for (int i = 0; i < interview.Count; ++i)
                {
                    Interview InterviewDetails = interview[i];

                    ZoomVideoCallMeetingManagment.GetZoomVCStatus(ref InterviewDetails);
                    if (InterviewDetails.ZoomVCMeetingResponse.id != null)
                    {
                        switch (InterviewDetails.InterviewStatus)
                        {
                            case InterviewCallStatus.InterviewProposed:
                                InterviewStatus = "Interview Proposed";
                                break;
                            case InterviewCallStatus.InterviewScheduled:
                                InterviewStatus = "Interview Scheduled";
                                break;
                            case InterviewCallStatus.InterviewStarted:
                                InterviewStatus = "Interview Started";
                                break;
                            case InterviewCallStatus.InterviewCompleted:
                                InterviewStatus = "Interview Completed";
                                break;
                            case InterviewCallStatus.InterviewDropped:
                                InterviewStatus = "Interview Dropped";
                                break;
                            case InterviewCallStatus.InterviewCancelled:
                                InterviewStatus = "Interview Cancelled";
                                break;
                            case InterviewCallStatus.InterviewRejected:
                                InterviewStatus = "Interview Rejected";
                                break;
                        }
                        dt.Rows.Add(InterviewDetails.InterviewID,
                            (count++).ToString(),
                            InterviewDetails.ZoomVCMeetingResponse.id,
                            InterviewDetails.ZoomVCMeetingResponse.topic,
                            InterviewDetails.ZoomVCMeetingResponse.start_time,
                            InterviewDetails.ChosenTimeZone,
                            InterviewStatus);
                    }
                }
                gridService.DataSource = dt;
                gridService.DataBind();
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }

        }
        protected void ZoomMeetingsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string ZoomMeetingID = e.Row.Cells[2].Text;
                    string InterviewID = e.Row.Cells[0].Text;
                    LinkButton lb = (LinkButton)e.Row.FindControl("ZoomMeetingId");
                    lb.CommandArgument = InterviewID;
                    lb.Text = ZoomMeetingID;
                }
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        protected void ZoomMeetingLink_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ZoomIdLb = (LinkButton)sender;
                string IntID = ZoomIdLb.CommandArgument.ToString();
                Response.Redirect("InterviewDetails.aspx?InterviewID=" + IntID, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
    }
}