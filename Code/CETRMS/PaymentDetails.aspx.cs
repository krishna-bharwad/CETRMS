using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class PaymentDetails : System.Web.UI.Page
    {
        public static string PaymentID;
        public static int ClientType;
        public static Payment payment = new Payment();
        public static UEClient uEClient = new UEClient();
        public static Employer employer = new Employer();
        public static Candidate candidate = new Candidate();
        public static CompanyProfile companyProfile = new CompanyProfile();
        protected static List<PaymentLogInfo> paymentLogs = new List<PaymentLogInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                PaymentID = Request.QueryString["PaymentID"].ToString();
                if (!IsPostBack)
                {
                    GetPaymentDetails();
                    GetPaymentLog();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }
        protected void ShowInvoice()
        {
            try
            {
                GetClientDetails();
                FillOrderDetailsGV();
                RMSMasterManagement.GetCompanyDetails(ref companyProfile);
                BillingAddressLBL.Text = companyProfile.BillingAddress;
                BillingRegionLBL.Text = companyProfile.BillingDistrict + ", " + companyProfile.BillingState + ", " + companyProfile.BillingCountry;
                CompanyContactLBL.Text = companyProfile.SupportContactNumber;
                InvoiceLBL.Text = payment.InvoiceNo;
                PaymentDateLBL.Text = payment.Stripe.StripePaymentDate.ToString("d");
                PaymentOrderNoLBL.Text = payment.PaymentOrderNo;
                InvoicePanel.Visible = true;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }

        protected void GetClientDetails()
        {
            try
            {
                switch (payment.PaymentType)
                {
                    case cPaymentType.EmployerRecruitmentFee:
                    case cPaymentType.EmployerRegistrationFee:
                    case cPaymentType.EmployerRegistrationRenewal:
                        NameLBL.Text = employer.Name;
                        BusinessNameLBL.Text = employer.BusinessName;
                        AddressLBL.Text = LocationManagement.GetCityDetail(employer.LocationStateCode, employer.LocationCityCode).CityName;
                        StateLBL.Text = LocationManagement.GetStateDetail(employer.LocationStateCode).StateName;
                        ClientType = cClientType.Employer;
                        break;
                    case cPaymentType.CandidateRecruitmentFee:
                    case cPaymentType.CandidateRegistrationFee:
                    case cPaymentType.CandidateRegistrationRenewal:
                        
                        ClientType = cClientType.Candidate;
                        NameLBL.Text = candidate.PersonalProfile.Name;
                        BusinessNameLBL.Text = "";
                        AddressLBL.Text = candidate.ContactDetails.CurrentAddress + ", " + LocationManagement.GetCityDetail(candidate.ContactDetails.CurrentStateCode, candidate.ContactDetails.CurrentCityCode).CityName;
                        StateLBL.Text = LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).StateName;
                        break;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }
        protected void GetPaymentDetails()
        {
            try
            {
                PaymentManagement.GetPaymentDetails(PaymentID, ref payment);
                PaymentIdLBL.Text = payment.PaymentRecID;
                PayOrderNoLBL.Text = payment.PaymentOrderNo;
                AmountLBL.Text = payment.Currency +" "+ payment.Amount.ToString();
                TaxAmountLBL.Text = payment.Currency + " " + payment.TaxAmount.ToString();
                TotalAmountLBL.Text = payment.Currency + " " + (payment.Amount + payment.TaxAmount).ToString();
                DueDateLBL.Text = payment.DueDate.ToString("d");

                switch (payment.PaymentType)
                {
                    case cPaymentType.CandidateRecruitmentFee:
                        PaymentTypeLBL.Text = "Candidate Recruitment Fee";
                        CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                        ClientNameLBL.Text = candidate.PersonalProfile.Name;
                        break;
                    case cPaymentType.EmployerRecruitmentFee:
                        PaymentTypeLBL.Text = "Employment Recruitment Fee";
                        EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                        ClientNameLBL.Text = employer.BusinessName;
                        break;
                    case cPaymentType.CandidateRegistrationFee:
                        PaymentTypeLBL.Text = "Candidate Registration Fee";
                        CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                        ClientNameLBL.Text = candidate.PersonalProfile.Name;
                        break;
                    case cPaymentType.EmployerRegistrationFee:
                        PaymentTypeLBL.Text = "Employment Registration Fee";
                        EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                        ClientNameLBL.Text = employer.BusinessName;
                        break;
                    case cPaymentType.CandidateRegistrationRenewal:
                        PaymentTypeLBL.Text = "Candidate Registration Renewal Fee";
                        CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                        ClientNameLBL.Text = candidate.PersonalProfile.Name;
                        break;
                    case cPaymentType.EmployerRegistrationRenewal:
                        PaymentTypeLBL.Text = "Employer Registration Renewal Fee";
                        EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                        ClientNameLBL.Text = employer.BusinessName;
                        break;
                }

                
                switch (payment.PaymentStatus)
                {
                    case cPaymentStatus.PaymentDueOutOfTime:
                        PaymentStatusLBL.Text = "Payment Due Out of time";
                        PaymentLogInfoPanel.Visible = true;
                        break;
                    case cPaymentStatus.PaymentDueWithinTime:
                        PaymentStatusLBL.Text = "Payment Due Within time";
                        PaymentLogInfoPanel.Visible = true;
                        break;
                    case cPaymentStatus.PaymentInitiated:
                        PaymentStatusLBL.Text = "Payment Initiated";
                        PaymentLogInfoPanel.Visible = true;
                        break;
                    case cPaymentStatus.PaymentDone:
                        PaymentStatusLBL.Text = "Payment Completed";
                        PaymentLogInfoPanel.Visible = true;

                        break;
                    case cPaymentStatus.PaymentFailed:
                        PaymentStatusLBL.Text = "Payment Failed";
                        PaymentLogInfoPanel.Visible = true;
                        break;
                    case cPaymentStatus.PaymentCancelled:
                        PaymentStatusLBL.Text = "Payment Cancelled";
                        PaymentLogInfoPanel.Visible = true;
                        break;
                    case cPaymentStatus.PaymentDiscounted:
                        PaymentStatusLBL.Text = "Payment Discounted";
                        PaymentLogInfoPanel.Visible = false;
                        break;
                }

                if (payment.PaymentStatus == cPaymentStatus.PaymentDone)
                {
                    ShowInvoice();
                }
                else
                {
                    InvoicePanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }

        protected void DownloadpdfBTN_Click(object sender, EventArgs e)
        {
            try
            {
                //byte[] InvoicePDFData = null;
                //if (ReceiptManagement.GetPaymentReceipt(PaymentID, ref InvoicePDFData) == 1)
                //{
                //    Response.Buffer = true;
                //    Response.Clear();
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-disposition", "attachment; filename=PaymentInvoice.pdf");
                //    Response.BinaryWrite(InvoicePDFData); // create the file    
                //    Response.Flush();
                //}
                Response.Redirect(@"./PrintPaymentInvoice.aspx?PaymentId=" + PaymentID, false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }
        protected void FillOrderDetailsGV()
        {
            try
            {
                DataTable PaymentTable = new DataTable();
                string PaymentType = string.Empty;

                PaymentTable.Columns.Add("Sr. No.");
                PaymentTable.Columns.Add("Service");
                PaymentTable.Columns.Add("Currency");
                PaymentTable.Columns.Add("Amount");
                int cCount = PaymentTable.Columns.Count;

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
                    "1",
                    PaymentType + "\r\nOrder No.: "+payment.PaymentOrderNo,
                    payment.Currency,
                    payment.Amount);

                PaymentTable.Rows.Add(
                    "2",
                    "Tax",
                    payment.Currency,
                    payment.TaxAmount.ToString());

                PaymentTable.Rows.Add(
                    "3",
                    "Total Amount",
                    payment.Currency,
                    (payment.Amount + payment.TaxAmount).ToString());

                PaymentDetailsGV.DataSource = PaymentTable;
                PaymentDetailsGV.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }

        protected void PaymentDetailsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GetPaymentLog()
        {
            paymentLogs.Clear();
            PaymentManagement.GetPaymentLogInfoList(payment.PaymentRecID, ref paymentLogs);
            DataTable PaymentLog = new DataTable();

            try
            {
                string PaymentStatus = string.Empty;
                string PaymentType = string.Empty;
                int nCount = 1;

                PaymentLog.Columns.Add("Sr. No.");
                PaymentLog.Columns.Add("Created At");
                PaymentLog.Columns.Add("Status");
                PaymentLog.Columns.Add("Amount");
                PaymentLog.Columns.Add("payment method");
                PaymentLog.Columns.Add("TXN id");
                PaymentLog.Columns.Add("Payment Receipt");
                PaymentLog.Columns.Add("StripeReceipt");
                PaymentLog.Columns.Add("Detail Log");
                int cCount = PaymentLog.Columns.Count;

                foreach (PaymentLogInfo paymentLogInfo in paymentLogs)
                {
                    PaymentLog.Rows.Add(
                        (nCount++).ToString(),
                        paymentLogInfo.CreatedAt.ToString("F"),
                        paymentLogInfo.status,
                        paymentLogInfo.Amount,
                        paymentLogInfo.payment_method,
                        paymentLogInfo.id,
                        "Stripe Receipt",
                        paymentLogInfo.remarks,
                        paymentLogInfo.StripePaymentLog);
                }

                PaymentLogInfoGV.DataSource = null;
                PaymentLogInfoGV.DataBind();
                PaymentLogInfoGV.DataSource = PaymentLog;
                PaymentLogInfoGV.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            finally
            {

            }

        }
        protected void PaymentLogInfoGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string StripeReceiptURL = e.Row.Cells[7].Text;

                    if (StripeReceiptURL.Trim() != "" && StripeReceiptURL.Trim() != "&nbsp;")
                    {
                        HyperLink StripReceiptHL = new HyperLink();
                        StripReceiptHL.Text = "Stripe Receipt";
                        StripReceiptHL.NavigateUrl = StripeReceiptURL;
                        StripReceiptHL.Target = "_blank";
                        e.Row.Cells[6].Controls.Add(StripReceiptHL);
                    }
                    else
                        e.Row.Cells[6].Text = "";
                }
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
        }
    }
}