using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class PaymentList : System.Web.UI.Page
    {
        public static string PaymentStatus;
        public static List<Employer> Employers = new List<Employer>();
        public static List<Payment> Payments = new List<Payment>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (Request.QueryString["PaymentStatus"] != null)
                {
                    PaymentStatus = Request.QueryString["PaymentStatus"].ToString();
                }
                if (!IsPostBack)
                {
                    SetPaymentStatusDDL();
                    FillPaymentListGridView();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "Page_Load :: " + Message);
            }
        }
        protected void SetPaymentStatusDDL()
        {
            try
            {
                for (int i = 0; i < PaymentStatusDDL.Items.Count; i++)
                {
                    if (PaymentStatusDDL.Items[i].Value == PaymentStatus)
                        PaymentStatusDDL.Items[i].Selected = true;
                    else
                        PaymentStatusDDL.Items[i].Selected = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "SetPaymentStatusDDL :: " + Message);
            }
        }
        protected void PaymentStatusDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPaymentListGridView();
        }

        protected void EmployerListDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPaymentListGridView();
        }
        protected void FillPaymentListGridView()
        {
            try
            {
                Payments.Clear();
                DataTable PaymentTable = new DataTable();
                if (PaymentManagement.GetPaymentList(ref Payments, "-1", Convert.ToInt32(PaymentStatusDDL.SelectedValue), Convert.ToInt32(PaymentTypeDDL.SelectedValue)) == 1)
                {
                    string PaymentStatus = string.Empty;
                    string PaymentType = string.Empty;
                    int nCount = 1;

                    PaymentTable.Columns.Add("PaymentID");
                    PaymentTable.Columns.Add("Sr. No.");
                    PaymentTable.Columns.Add("PaymentOrderNo");
                    PaymentTable.Columns.Add("Payment Type");
                    PaymentTable.Columns.Add("Payment Due Date");
                    PaymentTable.Columns.Add("Currency");
                    PaymentTable.Columns.Add("Amount");
                    PaymentTable.Columns.Add("Payment Status");
                    int cCount = PaymentTable.Columns.Count;

                    foreach (Payment payment in Payments)
                    {
                        switch (payment.PaymentStatus)
                        {
                            case cPaymentStatus.PaymentDueWithinTime:
                                PaymentStatus = "Payment Due Within Time";
                                break;
                            case cPaymentStatus.PaymentDueOutOfTime:
                                PaymentStatus = "Payment Due OutOf Time";
                                break;
                            case cPaymentStatus.PaymentDone:
                                PaymentStatus = "Payment Done";
                                break;
                            case cPaymentStatus.PaymentCancelled:
                                PaymentStatus = "Payment Cancelled";
                                break;
                            case cPaymentStatus.PaymentDiscounted:
                                PaymentStatus = "Payment Discounted";
                                break;
                        }

                        switch (payment.PaymentType)
                        {
                            case cPaymentType.EmployerRecruitmentFee:
                                PaymentType = "Employer Recruitment Fee";
                                break;
                            case cPaymentType.CandidateRecruitmentFee:
                                PaymentType = "Candidate Recruitment Fee";
                                break;
                            case cPaymentType.EmployerRegistrationFee:
                                PaymentType = "Employer Registration Fee";
                                break;
                            case cPaymentType.CandidateRegistrationFee:
                                PaymentType = "Candidate Registration Fee";
                                break;
                            case cPaymentType.EmployerRegistrationRenewal:
                                PaymentType = "Employer Registration Renewal";
                                break;
                            case cPaymentType.CandidateRegistrationRenewal:
                                PaymentType = "Candidate Registration Renewal";
                                break;
                        }

                        PaymentTable.Rows.Add(
                            payment.PaymentRecID,
                            (nCount++).ToString(),
                            payment.PaymentOrderNo,
                            PaymentType,
                            payment.DueDate.ToString("dd-MM-yyyy"),
                            payment.Currency,
                            payment.Amount,
                            PaymentStatus); ;
                    }
                    PaymentListGV.DataSource = PaymentTable;
                    PaymentListGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "FillPaymentListGridView :: " + Message);
            }
        }
        protected void PaymentTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPaymentListGridView();
        }

        protected void PaymentListGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string PaymentOrderNo = e.Row.Cells[4].Text;
                    string PaymentID = e.Row.Cells[2].Text;
                    LinkButton lb = (LinkButton)e.Row.FindControl("PayOrderNoLB");
                    lb.CommandArgument = PaymentID;
                    lb.Text = PaymentOrderNo;

                    e.Row.Cells[0].Text = e.Row.Cells[3].Text;
                }
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "PaymentListGV_RowDataBound :: " + Message);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton PayOrderNoLB = (LinkButton)sender;
                string PayID = PayOrderNoLB.CommandArgument.ToString();
                Response.Redirect(URLs.PaymentDetailsURL + PayID, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "LinkButton1_Click :: " + Message);
            }
        }

        protected void PrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(PaymentStatusDDL.SelectedValue))
                {
                    case cPaymentStatus.All:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=-1", false);
                        break;
                    case cPaymentStatus.PaymentDueWithinTime:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=1", false);
                        break;
                    case cPaymentStatus.PaymentDueOutOfTime:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=2", false);
                        break;
                    case cPaymentStatus.PaymentDone:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=3", false);
                        break;
                    case cPaymentStatus.PaymentCancelled:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=4", false);
                        break;
                    case cPaymentStatus.PaymentDiscounted:
                        Response.Redirect(@"~\PrintPaymentList.aspx?PaymentStatus=5", false);
                        break;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", "PrintBTN_Click :: " + Message);
            }
        }
    }
}