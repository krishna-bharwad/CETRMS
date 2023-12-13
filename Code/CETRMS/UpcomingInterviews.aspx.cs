using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class UpcomingInterviews : System.Web.UI.Page
    {
        protected List<Employer> employers = new List<Employer>();
        
        protected List<Interview> interview = new List<Interview>();
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
                    FillEmployerDDL(); //Prashant
                    LoadPageData();
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
        protected void FillEmployerDDL() //Prashant
        {
            try
            {
                employers.Add(new Employer { BusinessName = "All", EmployerID = "-1" });

                EmployerManagement.GetEmployerList(ref employers, EmployerStatus.InProcessVacancy);

                EmployerDDL.DataSource = employers;
                EmployerDDL.DataTextField = "BusinessName";
                EmployerDDL.DataValueField = "EmployerId";

                EmployerDDL.DataBind();
                EmployerDDL.SelectedValue = "-1";

                interview.Clear();
                InterviewManagement.GetInterviewListByEmployer(EmployerDDL.SelectedValue, ref interview);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        protected void Refreshinterview(object sender, EventArgs e)
        {
            LoadPageData();
        }
        protected void LoadPageData()
        {
            string getStatus = InterviewStatus.SelectedValue;
            int interviewStatus = Convert.ToInt32(getStatus);
            try
            {
                interview.Clear();
                InterviewManagement.GetInterviewListByEmployer(EmployerDDL.SelectedValue, ref interview, interviewStatus);
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[1] { new DataColumn("InterviewID", typeof(string)) });

                //if (EmployerDDL.SelectedValue == "0")
                //{
                //    InterviewStatus.SelectedValue = "-1";

                //}
                foreach (var interview in interview)
                {
                    dt.Rows.Add(interview.InterviewID);
                }

                gridService.DataSource = dt;
                gridService.DataBind();


                if (dt.Rows.Count != 0 || EmployerDDL.SelectedValue == "-1")
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        InterviewNotScheduled_txt.Text = string.Empty;
                        LinkButton btn = (LinkButton)gridService.Rows[i].Cells[0].FindControl("Button_Text");
                        btn.Text = interview[i].ZoomVCMeetingResponse.topic;
                        switch (interview[i].InterviewStatus)
                        {
                            case InterviewCallStatus.InterviewProposed:
                                btn.Style.Add("color", "rgba(243,156,18,0.88)");
                                break;

                            case InterviewCallStatus.InterviewScheduled:
                                btn.Style.Add("color", "rgba(52,152,219,0.88)");
                                break;

                            case InterviewCallStatus.InterviewStarted:
                                btn.Style.Add("color", "rgba(38,185,154,0.88)");
                                break;

                            case InterviewCallStatus.InterviewCompleted:
                                btn.Style.Add("color", "rgba(243,156,18,0.88)");
                                break;

                            case InterviewCallStatus.InterviewDropped:
                                btn.Style.Add("color", "rgba(243,156,18,0.88)");
                                break;

                            case InterviewCallStatus.InterviewRejected:
                                btn.Style.Add("color", "rgba(243,156,18,0.88)");
                                break;

                            case InterviewCallStatus.InterviewCancelled:
                                btn.Style.Add("color", "rgba(231,76,60,0.88)");
                                break;
                        }
                    }

                    

                    InterviewNotScheduled_txt.Text = "";
                }
                else
                {
                    InterviewNotScheduled_txt.Text = "No Interview Scheduled";
                }
                if (dt.Rows.Count == 0)
                {
                    InterviewListPanel.Visible = false;
                }
                else
                {
                    InterviewListPanel.Visible = true;
                }
                CalenderLoad();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        protected void gridService_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "InterviewDetails") return;
                int InterviewId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("InterviewDetails.aspx?InterviewID=" + InterviewId, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }

        }
        protected void CalenderLoad()
        {
            try
            {
                string EventScript = string.Empty;
                PrepareEventList(ref EventScript);

                string msg = "<script language=\"javascript\">\r\n";
                msg += "$(function() {\r\n";
                msg += "function ini_events(ele) {\r\n";

                msg += "ele.each(function() {\r\n";

                msg += " var eventObject = {\r\n";
                msg += "title: $.trim($(this).text())}\r\n"; // use the element's text as the event title
                msg += "      $(this).data('eventObject', eventObject)\r\n";
                msg += "        $(this).draggable({   zIndex: 1070,  revert: true, \r\n";// will cause the event to go back to its
                msg += "         revertDuration: 0 \r\n"; //  original position after the drag
                msg += "       }) }) }\r\n";

                msg += "ini_events($('#external-events div.external-event'))\r\n";
                msg += " var date = new Date()\r\n";
                msg += "  var d = date.getDate(),\r\n";
                msg += "  m = date.getMonth(),\r\n";
                msg += "  y = date.getFullYear()\r\n";
                msg += "var Calendar = FullCalendar.Calendar;\r\n";
                msg += " var Draggable = FullCalendar.Draggable;\r\n";
                msg += "var containerEl = document.getElementById('external-events');\r\n";
                msg += "var checkbox = document.getElementById('drop-remove');\r\n";
                msg += "var calendarEl = document.getElementById('calendar');\r\n";
                msg += "var calendar = new Calendar(calendarEl, {\r\n";
                msg += "headerToolbar: {\r\n";
                msg += "left: 'prev,next today',\r\n";
                msg += "center: 'title',\r\n";
                msg += "right: 'dayGridMonth,timeGridWeek,timeGridDay'\r\n";
                msg += "},\r\n";
                msg += "    themeSystem: 'bootstrap',\r\n";
                msg += "     events:\r\n";
                msg += "[\r\n";
                msg += EventScript;
                msg += "      ],\r\n";
                msg += "      editable: true,\r\n";
                msg += "      droppable: true, \r\n";
                msg += "      eventDragStop: true,  \r\n";
                msg += "      drop: function(info) {\r\n";
                //msg += "    if (checkbox.checked) {\r\n";
                //msg += "            info.draggedEl.parentNode.removeChild(info.draggedEl);\r\n";
                //msg += "        }\r\n";
                msg += "       },\r\n";



                msg += "eventClick: function(info){\r\n";
                msg += " var varInterviewId = info.event.title.substring(info.event.title.indexOf('-')+1)\r\n";
                msg += "window.location.assign('InterviewDetails.aspx?InterviewID='+varInterviewId);\r\n";
                //msg += "alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);";
                //msg += "alert('View: ' + info.view.type);";
                msg += "},\r\n";
                msg += "eventMouseEnter: function(info) {\r\n";
                msg += "tooltip = '<div class=\"tooltiptopicevent\" style=\"width:auto;height:auto;background:#feb811;position:fixed;z-index:10001;padding:10px 10px 10px 10px ;  line-height: 200%;\">' + 'title: ' + ': ' + info.event.title.substring(info.event.title.indexOf('-')+1) + '</br>' + 'start: ' + ': ' + info.event.start + '</div>';\r\n";

                msg += "$(\"body\").append(tooltip);\r\n";
                msg += "$(this).mouseover(function (e) {\r\n";
                msg += "$(this).css('z-index', 10000);\r\n";
                msg += "$('.tooltiptopicevent').fadeIn('500');\r\n";
                msg += "$('.tooltiptopicevent').fadeTo('10', 1.9);\r\n";
                msg += "}).mousemove(function (e) {\r\n";
                msg += "$('.tooltiptopicevent').css('top', e.pageY + 10);\r\n";
                msg += "$('.tooltiptopicevent').css('left', e.pageX + 20);});\r\n";

                msg += "},\r\n";
                msg += "eventMouseLeave: function(info) {\r\n";
                msg += "$(this).css('z-index', 8);\r\n";
                msg += "$('.tooltiptopicevent').remove();\r\n";
                msg += "}\r\n";
                msg += "});\r\n";
                msg += "calendar.render();\r\n";
                msg += " $('#calendar').fullCalendar()\r\n";

                /* ADDING EVENTS */
                msg += "var currColor = '#3c8dbc' \r\n";

                // Color chooser button
                msg += "    $('#color-chooser > li > a').click(function(e) {\r\n";
                msg += "    e.preventDefault()\r\n";

                // Save color
                msg += "     currColor = $(this).css('color')\r\n";

                // Add color effect to button
                msg += "      $('#add-new-event').css({\r\n";
                msg += "        'background-color': currColor,\r\n";
                msg += "        'border-color'    : currColor\r\n";
                msg += "      })\r\n";
                msg += "    })\r\n";
                msg += "    $('#add-new-event').click(function(e) {\r\n";
                msg += "    e.preventDefault()\r\n";

                // Get value and make sure it is not null
                msg += "     var val = $('#new-event').val()\r\n";
                msg += "     if (val.length == 0)\r\n";
                msg += "   {\r\n";
                msg += "       return\r\n";
                msg += "      }\r\n";

                // Create events
                msg += "    var event = $('<div />')\r\n";
                msg += "      event.css({\r\n";
                msg += "        'background-color': currColor,\r\n";
                msg += "       'border-color'    : currColor,\r\n";
                msg += "        'color'           : '#fff'\r\n";
                msg += "     }).addClass('external-event')\r\n";
                msg += "      event.text(val)\r\n";
                msg += "     $('#external-events').prepend(event)\r\n";

                // Add draggable funtionality
                // msg += "     ini_events(event)\r\n";


                // Remove event from text input
                msg += "      $('#new-event').val('')\r\n";
                msg += "    })\r\n";
                msg += "  });\r\n";
                msg += "</script>\r\n";

                // Define the name and type of the client scripts on the page.
                String csname1 = "PopupScript";
                Type cstype = this.GetType();

                // Get a ClientScriptManager reference from the Page class.
                ClientScriptManager cs = Page.ClientScript;

                // Check to see if the startup script is already registered.
                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    cs.RegisterStartupScript(cstype, csname1, msg);
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        protected bool PrepareEventList(ref string _EventScript)
        {
            bool bRetValue = false;
            try
            {
                string cardColor = string.Empty;
                string EventScript = string.Empty;
                for (int i = 0; i < interview.Count; i++)
                {
                    switch (interview[i].InterviewStatus)
                    {
                        case 2: cardColor = "green"; break; // Interview Scheduled
                        case 3: cardColor = "grey"; break; // Interview Completed
                        case 4: cardColor = "red"; break; // Interview Cancelled
                        case 5: cardColor = "yellow"; break; // Interview on-hold
                        default: cardColor = "blue"; break; // Candidate Selected
                    }

                    string yy = interview[i].PreferredDateTime.Year.ToString();
                    string mm = (interview[i].PreferredDateTime.Month - 1).ToString();
                    string dd = interview[i].PreferredDateTime.Day.ToString();
                    string StartHH = interview[i].PreferredDateTime.Hour.ToString();
                    string StartMM = interview[i].PreferredDateTime.Minute.ToString();
                    string EndHH = interview[i].PreferredDateTime.Hour.ToString();
                    string EndMM = (interview[i].PreferredDateTime.Minute + interview[i].ZoomVCMeetingResponse.duration).ToString();

                    string InterviewStartTime = yy + ", " + mm + ", " + dd + "," + StartHH + "," + StartMM;
                    string InterviewEndTime = yy + ", " + mm + ", " + dd + "," + StartHH + "," + StartMM;
                    string InterviewID = interview[i].InterviewID.ToString();
                    string msg = "{\r\n";
                    msg += "interviewId: '" + InterviewID + "',\r\n";
                    msg += "title: '" + interview[i].ZoomVCMeetingResponse.topic + "-" + InterviewID + "',\r\n";
                    msg += "          start: new Date(" + InterviewStartTime + "),\r\n";
                    msg += "          end: new Date(" + InterviewEndTime + "),\r\n";
                    //msg += "          url: 'https://www.google.com/',\r\n";
                    msg += "          backgroundColor: '" + cardColor + "', \r\n";
                    msg += "          borderColor: '" + cardColor + "', \r\n";
                    msg += "          eventColor: '" + cardColor + "', \r\n";
                    msg += "          eventDisplay: 'block' \r\n";

                    msg += "        },\r\n";

                    EventScript = EventScript + msg;
                }
                _EventScript = EventScript;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
            return bRetValue;
        }
    }
}
    