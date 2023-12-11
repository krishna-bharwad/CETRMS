using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class AllNotifications : System.Web.UI.Page
    {
        public static List<Notification> notifications = new List<Notification>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (!IsPostBack)
                {
                    ShowNotificationList();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void ShowNotificationList()
        {
            try
            {
                notifications.Clear();
                DataTable NotificationTable = new DataTable();
                if (NotificationManagement.GetNotificationList(ref notifications, "-1" /*-1:admin*/, cNotificationType.AdminNotification, cNotificationStatus.AllNotifications) == 1)
                {
                    string NotificationStatus = string.Empty;
                    NotificationTable.Columns.Add("Notification ID");
                    NotificationTable.Columns.Add("Notification Date-Time");
                    NotificationTable.Columns.Add("Notification Message");
                    NotificationTable.Columns.Add("Notification Status");
                    NotificationTable.Columns.Add("");
                    int cCount = NotificationTable.Columns.Count;

                    foreach (Notification notification in notifications)
                    {
                        switch (notification.NotificationStatus)
                        {
                            case cNotificationStatus.NotificationDrafted:
                                NotificationStatus = "Drafted";
                                break;
                            case cNotificationStatus.NotificationSent:
                                NotificationStatus = "Sent";
                                break;
                            case cNotificationStatus.NotificationUnread:
                                NotificationStatus = "Unread";
                                break;
                            case cNotificationStatus.NotificationRead:
                                NotificationStatus = "Read";
                                break;
                            case cNotificationStatus.NotificationDropped:
                                NotificationStatus = "Dropped";
                                break;
                        }
                        NotificationTable.Rows.Add(
                            notification.NotificationID,
                            notification.NotificationTime,
                            notification.NotificationMessage,
                            NotificationStatus,
                            notification.hyperlink
                            );
                    }
                    NotificationListGV.DataSource = NotificationTable;
                    NotificationListGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void NotificationListGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                NotificationListGV.PageIndex = e.NewPageIndex;
                ShowNotificationList();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void NotificationListGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string hyperlinkURL = e.Row.Cells[6].Text;
                    LinkButton lb = (LinkButton)e.Row.FindControl("NotificationIDLB");
                    lb.CommandArgument = hyperlinkURL + "&NotificationID=" + e.Row.Cells[2].Text;
                    lb.Text = e.Row.Cells[2].Text;
                }
                if (e.Row.Cells.Count >= 6)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[6].Visible = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void NotificationIDLB_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lb = (LinkButton)sender;
                string url = lb.CommandArgument;
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
    }
}