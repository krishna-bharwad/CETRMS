using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using RestSharp;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace CETRMS
{
    public partial class ReferralDetails : System.Web.UI.Page
    {
        protected List<Referral> referr = new List<Referral>();

        string oCandidateId = string.Empty;
        string ReferralCode = string.Empty;
        string ReferralID = string.Empty;
        string CandidateIdForBankDetails = string.Empty;
        public string ReferralStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            ReferralStatus = Request.QueryString.Get("ReferaalStatus");
            if (!IsPostBack)
            {
                GetReferralStatusDDL();
                LoadPageData();
            }
        }

        protected void GetReferralStatusDDL()
        {
            try
            {
                for(int i=0;i < ReferralStatusDDL.Items.Count; i++)
                {
                    if (ReferralStatusDDL.Items[i].Value == ReferralStatus)
                        ReferralStatusDDL.Items[i].Selected = true;
                    else
                        ReferralStatusDDL.Items[i].Selected = false;
                }

            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }

        }

        protected void ReferralStatusChanged(object send, EventArgs e)
        {
            LoadPageData();
        }
        protected void LoadPageData()
        {
            try
            {
                referr.Clear();
                ReferralManagement.GetReferralDetailsByStatus(ref referr, Convert.ToInt32(ReferralStatusDDL.SelectedValue));
                DataTable dt = new DataTable();

                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ReferralID", typeof(string))
                                                       ,new DataColumn("ReferralCode",  typeof(string))
                                                       ,new DataColumn("CandidateName", typeof(string))
                                                       ,new DataColumn("OCandidateName",typeof(string))
                                                       ,new DataColumn("ReferralStatus",typeof(string))});
                foreach (var referr in referr)
                {
                    Candidate candidate = new Candidate();
                    Candidate oCandidate = new Candidate();
                    CandidateManagement.GetCandidateFullDetails(referr.CETCandidateID, ref candidate);
                    ReferralManagement.GetCandidateIDByReferralCode(referr.ReferralCode, ref oCandidateId);
                    CandidateManagement.GetCandidateFullDetails(oCandidateId, ref oCandidate);
                    dt.Rows.Add(referr.ReferralID, referr.ReferralCode, candidate.PersonalProfile.Name, oCandidate.PersonalProfile.Name, referr.ReferralStatus);
                }
                ReferralDetailsGV.DataSource = dt;
                ReferralDetailsGV.DataBind();

                if (Convert.ToInt32(ReferralStatusDDL.SelectedValue) != 4)
                {
                    ReferralPaidListPrintBTN.Visible = false;
                }
                else
                {
                    ReferralPaidListPrintBTN.Visible = true;
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
        protected void ReferralDetailsGv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Button btn = (Button)e.Row.Cells[5].FindControl("btn");
                    btn.Text = "Pay Now";
                    string RefStatus = e.Row.Cells[4].Text;
                    int ReferralStatus = Convert.ToInt32(RefStatus);

                    if (ReferralStatus == CReferralStatus.ReferralPaymentDue)
                    {
                        btn.Enabled = true;
                    }
                    else if (ReferralStatus == CReferralStatus.ReferralPaid)
                    {
                        btn.Enabled = false;
                        btn.Text = "Referral Paid";
                    }
                    else
                    {
                        btn.Enabled = true;
                    }
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
        protected void PaymentDetails_Click(object sender, EventArgs e)
        {
            try
            {
                var Ue = Master as UEStaff;
                Ue.ShowModel("ReferralPaymentModal");
                Referral refer = new Referral();
                Candidate oCandidate = new Candidate();
                Button btn = (Button)sender;
                ReferralID = btn.CommandArgument.ToString();
                ReferralManagement.GetReferralDetailsByReferralId(ReferralID, ref refer);
                ReferralManagement.GetCandidateIDByReferralCode(refer.ReferralCode, ref CandidateIdForBankDetails);
                CandidateManagement.GetCandidateFullDetails(CandidateIdForBankDetails, ref oCandidate);
                OldCandidateId.Text = CandidateIdForBankDetails;
                int IRetValue = 1;
                string BankAcNumber = oCandidate.BankAccountDetails.BankAccountNumber.ToString();
                string IFSCCode = oCandidate.BankAccountDetails.IFSCCode.ToString();
                ReferralIDlbl.Text = ReferralID;
                if (BankAcNumber != null && BankAcNumber != "")
                {
                    BankAccountNo.Text = oCandidate.BankAccountDetails.BankAccountNumber.ToString();
                }
                else
                {
                    BankAccountNo.Text = "Please Add Bank Account Number";
                    IRetValue = 0;
                }
                if (IFSCCode != null && IFSCCode != "")
                {
                    IFSC_lbl.Text = oCandidate.BankAccountDetails.IFSCCode.ToString();
                }
                else
                {
                    IFSC_lbl.Text = "Please Add IFSC Code";
                    IRetValue = 0;
                }
                if (IRetValue == 0)
                    ReferralPaid_btn.Enabled = false;
                else
                    ReferralPaid_btn.Enabled = true;

                //var client = new RestClient("https://ifsc.razorpay.com/" + IFSC_lbl.Text);
                //var request = new RestRequest();
                //request.Method = Method.Get;
                //request.RequestFormat = DataFormat.None;
                //RestResponse restResponse = client.Execute(request);
                //HttpStatusCode statusCode = restResponse.StatusCode;
                //int numericStatusCode = (int)statusCode;
                //if (numericStatusCode == 200)
                //{
                //    Candidate candidate = new Candidate();
                //    var response = JObject.Parse(restResponse.Content);

                //    //We can get required fields
                //    candidate.bankdetails.BANKCODE = (string)response["BANKCODE"];
                //    candidate.bankdetails.BRANCH = (string)response["BRANCH"];
                //}
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        protected void PaymentDone_Click(object sender, EventArgs e)
        {
            try
            {
                // Change Referral Status to ReferralPaid
                ReferralManagement.UpdateCandidateReferralStatus(ReferralIDlbl.Text, CReferralStatus.ReferralPaid);
                Response.Redirect("~/ReferralDetails.aspx",false);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
        }
        protected void GetCandidateDetails_btn(object sender, EventArgs e)
        {
            Response.Redirect(URLs.CandidateDetailsURL + OldCandidateId.Text);
        }
        protected void ReferralPaidList_btn(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/PrintReferralPaidList.aspx");
        }
    } 
}