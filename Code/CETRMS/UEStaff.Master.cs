using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace CETRMS
{
    public partial class UEStaff : System.Web.UI.MasterPage
    {
        public static UEStaffMember uEStaffMember = new UEStaffMember();
        SqlConnection dbConnection = new SqlConnection();
        public static List<Notification> notifications = new List<Notification>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                if (!IsPostBack)
                {
                    if (Session["cetrms_username"] == null)
                    {
                        Response.Redirect("~/NewIndex.aspx", false);
                    }
                    DateLBL.Text = System.DateTime.Now.ToString("dddd, dd-MMM-yyyy");
                    GetStaffDetails();
                    GetNotificationSummary();
                    GetNotificationList();
                    GetVersionNumber();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }
        protected void UpdatePasswordBTN_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Session["cetrms_username"].ToString();
                string Oldpassword = OldPasswordTXT.Text;
                string Newpassword = NewPasswordTXT.Text;
                if (RMSMasterManagement.UpdatePassword(username, Oldpassword, Newpassword) == 1)
                {
                    ChangePasswordInfoLBL.Visible = true;
                    ChangePasswordInfoLBL.Text = "Password updated successfully";
                }

                ShowModel("changePassword");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }

        protected void SignOutBTN_Click(object sender, EventArgs e)
        {

            Session["cetrms_username"] = null;
            Response.Cookies["cetrms_username"].Value = "";
            Response.Cookies["uerms_password"].Value = "";
            Response.Cookies["cetrms_username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["uerms_password"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("NewIndex.aspx", false);
        }
        protected void UpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                uEStaffMember.Name = NameTXT.Text.Trim();
                uEStaffMember.Address = AddressTXT.Text.Trim();
                uEStaffMember.MobileNo = MobileNoTXT.Text.Trim();
                uEStaffMember.Email = EmailIdTXT.Text.Trim();
                uEStaffMember.Designation = DesignationTXT.Text.Trim();
                uEStaffMember.TeamId = StaffTeamDDL.SelectedValue;
                uEStaffMember.UserStatus = Convert.ToInt32(UserStatusDDL.SelectedValue);
                if (RMSMasterManagement.UpdateUEStaff(uEStaffMember) == 1)
                {
                    UpdateUserDataLBL.Text = "Details update successfully.";
                }
                GetStaffDetails();
                ShowModel("UpdateInfo-modal");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }

        protected void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }
        protected void GetStaffDetails()
        {
            try
            {
                if (RMSMasterManagement.GetStaffMemberDetails(Session["cetrms_username"].ToString(), ref uEStaffMember) == RetValue.Success)
                {
                    MemberIdLBL.Text = uEStaffMember.UserId;
                    NameTXT.Text = uEStaffMember.Name;
                    AddressTXT.Text = uEStaffMember.Address;
                    MobileNoTXT.Text = uEStaffMember.MobileNo;
                    EmailIdTXT.Text = uEStaffMember.Email;
                    DesignationTXT.Text = uEStaffMember.Designation;
                    for (int i = 0; i < StaffTeamDDL.Items.Count; i++)
                    {
                        if (StaffTeamDDL.Items[i].Value == uEStaffMember.TeamId)
                        {
                            StaffTeamDDL.Items[i].Selected = true;
                        }
                        else
                        {
                            StaffTeamDDL.Items[i].Selected = false;
                        }
                    }
                    if (uEStaffMember.UserStatus == 0)
                    {
                        UserStatusDDL.Items[0].Selected = true;
                        UserStatusDDL.Items[1].Selected = false;
                    }
                    else
                    {
                        UserStatusDDL.Items[0].Selected = false;
                        UserStatusDDL.Items[1].Selected = true;
                    }
                    if (uEStaffMember.StaffPhoto != null)
                    {
                        Byte[] CandidatePhotoData = (Byte[])(uEStaffMember.StaffPhoto);
                        string CandidatePhotoMem = Convert.ToBase64String(CandidatePhotoData);
                        StaffPhotoIMG.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoMem);
                        StaffPhotoIMG.Width = 200;
                        StaffPhotoIMG.Height = 240;

                        ProfilePicImg.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoMem);
                        ProfilePicImg.Width = 50;
                        ProfilePicImg.Height = 50;

                        profilepic2img.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoMem);
                        profilepic2img.Width = 50;
                        profilepic2img.Height = 50;
                    }

                    UserNameLBL.Text = uEStaffMember.Name;
                    DesignationLBL.Text = uEStaffMember.Designation;
                }
                else
                {
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "","Not able to fetch staff details");
                }
            }
            
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
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

        protected void PhotoUploadBTN_Click(object sender, EventArgs e)
        {
            try
            {
                FileUpload img = (FileUpload)PhotoUpload;
                Byte[] imgByte = null;

                if (img.HasFile && img.PostedFile != null)
                {
                    // To create a PostedFile
                    HttpPostedFile File = PhotoUpload.PostedFile;

                    // Create byte Array with file len
                    imgByte = new Byte[File.ContentLength];

                    // Force the control to load data in array
                    File.InputStream.Read(imgByte, 0, File.ContentLength);


                    string CandidatePhotoMem = Convert.ToBase64String(imgByte);
                    StaffPhotoIMG.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoMem);
                    StaffPhotoIMG.Width = 200;
                    StaffPhotoIMG.Height = 240;


                    string query = "UPDATE UEStaff SET StaffPhoto = @eimg where userid = '" + Session["cetrms_username"].ToString() + "'";
                    SqlCommand cmd = new SqlCommand(query, dbConnection);

                    cmd.Parameters.AddWithValue("@eimg", imgByte);
                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();

                    ShowModel("UpdateInfo-modal");
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }

        protected void GetNotificationSummary()
        {
            try
            {
                string DataJson = string.Empty;
                NotificationManagement.GetNotificationStatistics(ref DataJson, "-1", cNotificationType.AdminNotification);
                if (!String.IsNullOrEmpty(DataJson))
                {
                    dynamic NotificationStatisticsData = JsonConvert.DeserializeObject(DataJson);
                    UnreadNotificationCountLBL.Text = NotificationStatisticsData.NotificationUnread;
                }
                else
                {
                    UnreadNotificationCountLBL.Text = "0";
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }
        protected void GetNotificationList()
        {
            try
            {
                notifications.Clear();

                NotificationManagement.GetNotificationList(ref notifications, "-1", -1, cNotificationType.AdminNotification, cNotificationStatus.NotificationUnread);
                if (notifications.Count > 0)
                {
                    string notificationList = "<ul class=\"dropdown-menu list-unstyled msg_list\" role=\"menu\" aria-labelledby=\"navbarDropdown1\">\r\n";
                    foreach (Notification notification in notifications)
                    {
                        notificationList = notificationList + "<li class=\"nav-item\">\r\n" +
                          "<a class=\"dropdown-item\" href=\"" + notification.hyperlink + "&NotificationID=" + notification.NotificationID + "\">\r\n" +
                            "<span>\r\n" +
                              "<span>UE Admin</span>\r\n" +
                              "<span class=\"time\">" + notification.NotificationTime.ToString() + "</span>\r\n" +
                            "</span>\r\n" +
                            "<span class=\"message\">\r\n" +
                              notification.NotificationMessage +
                            "</span>\r\n" +
                          "</a>\r\n" +
                        "</li>\r\n";
                    }
                    notificationList = notificationList + "<li class=\"nav-item\">\r\n" +
                      "<div class=\"text-center\">\r\n" +
                        "<a class=\"dropdown-item\" href=\"AllNotifications.aspx\">\r\n" +
                          "<strong>See All Alerts</strong>\r\n" +
                          "<i class=\"fa fa-angle-right\"></i>\r\n" +
                        "</a>\r\n" +
                      "</div>\r\n" +
                    "</li>\r\n" +
                  "</ul>\r\n";
                    NotficationListLit.Text = notificationList;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
        }

        protected void GetVersionNumber()
        {
            //VersionLBL.Text = "Version: " + common.GetSoftwareVersion();
            PublishDateLBL.Text = "Published On: " + common.GetPublishDate();
        }
    }   
}
    
