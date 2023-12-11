using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class TestimonialList : System.Web.UI.Page
    {
        List<Testimonial> Testimonials = new List<Testimonial>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (!IsPostBack)
                    FillTestimonialGV();
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        protected void FillTestimonialGV()
        {
            try
            {
                Testimonials.Clear();
                DataTable TestimonialTable = new DataTable();
                if (TestimonialManager.GetAllTestimonialList(ref Testimonials) == 1)
                {
                    string PaymentStatus = string.Empty;
                    string PaymentType = string.Empty;
                    int nCount = 1;

                    TestimonialTable.Columns.Add("TestimonialID");
                    TestimonialTable.Columns.Add("Sr. No.");
                    TestimonialTable.Columns.Add("Date");
                    TestimonialTable.Columns.Add("Testimonial By");
                    TestimonialTable.Columns.Add("Message");
                    TestimonialTable.Columns.Add("RatingsStar");
                    TestimonialTable.Columns.Add("IsVisible");

                    int cCount = TestimonialTable.Columns.Count;

                    foreach (Testimonial testimonial in Testimonials)
                    {
                        Candidate candidate = new Candidate();
                        CandidateManagement.GetCandidatePersonalDetails(testimonial.CETClientID, ref candidate);

                        TestimonialTable.Rows.Add(
                            testimonial.TestimonialID,
                            (nCount++).ToString(),
                            testimonial.ResponseDate,
                            candidate.PersonalProfile.Name,
                            testimonial.ResponseMessage,
                            testimonial.Rating,
                            testimonial.IsShown);
                    }
                    TestimonialsGV.DataSource = TestimonialTable;
                    TestimonialsGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }

        }

        protected void TestimonialsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string TestimonialID = e.Row.Cells[2].Text.Trim();
                    int iRatingGrade = Convert.ToInt32(e.Row.Cells[7].Text);
                    int IsShowing = Convert.ToInt32(e.Row.Cells[8].Text);

                    Image RatingImg = (Image)e.Row.FindControl("RatingIMG");
                    ImageButton IsShowingImg = (ImageButton)e.Row.FindControl("IsShowingImg");

                    switch (iRatingGrade)
                    {
                        case 1:
                            RatingImg.ImageUrl = "images\\one-star.png";
                            break;
                        case 2:
                            RatingImg.ImageUrl = "images\\two-star.png";
                            break;
                        case 3:
                            RatingImg.ImageUrl = "images\\three-star.png";
                            break;
                        case 4:
                            RatingImg.ImageUrl = "images\\four-star.png";
                            break;
                        case 5:
                            RatingImg.ImageUrl = "images\\five-star.png";
                            break;
                    }

                    if (IsShowing == 1)
                    {
                        IsShowingImg.ImageUrl = "https://img.icons8.com/color/48/null/checked-checkbox.png";
                        IsShowingImg.CommandArgument = TestimonialID;
                        IsShowingImg.ToolTip = "Click here to change testimonial status (Showing/Not Showing) on home page.";
                    }
                    else
                    {
                        IsShowingImg.ImageUrl = "https://img.icons8.com/offices/30/null/cancel.png";
                        IsShowingImg.CommandArgument = TestimonialID;
                        IsShowingImg.ToolTip = "Click here to change testimonial status (Showing/Not Showing) on home page.";
                    }
                }
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }

        }

        protected void IsShowingImg_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton temp = (ImageButton)sender;
                string tID = temp.CommandArgument;

                TestimonialManager.UpdateTestimonialStatus(tID);

                //TestimonialsGV.Dispose();
                FillTestimonialGV();
                //TestimonialsGV.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }

        }
    }
}