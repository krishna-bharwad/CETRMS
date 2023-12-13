using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
namespace CETRMS
{
    public partial class PrintPaymentInvoice : System.Web.UI.Page
    {
        public string PaymentId;
        SqlConnection dbConnection = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["cetrms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (!IsPostBack)
                {
                    PaymentId = Request.QueryString.Get("PaymentId");
                    CreatePaymentInvoicePDF();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        private void CreatePaymentInvoicePDF()
        {
            string deviceInfo = "";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            try
            {
                Payment payment = new Payment();
                if (PaymentManagement.GetPaymentDetailsForReceipt(PaymentId, ref payment) == RetValue.Success)
                {
                    //ReportViewer ReportViewer1 = new ReportViewer();
                    PaymentInvoiceRV.ProcessingMode = ProcessingMode.Local;
                    PaymentInvoiceRV.LocalReport.ReportPath =Server.MapPath("PaymentInvoice.rdlc");
                    logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "Local Report file path: " + PaymentInvoiceRV.LocalReport.ReportPath);

                    PaymentInvoiceRV.LocalReport.DataSources.Clear();

                    dbConnection.Open();
                    using (SqlCommand dbCommand = new SqlCommand())
                    {
                        SqlDataAdapter dbAdapter = new SqlDataAdapter();
                        DataSet dbDataset = new DataSet();
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "sp_GetPaymentDetails";
                        dbCommand.Parameters.AddWithValue("@PaymentRecID", PaymentId);
                        dbAdapter.SelectCommand = dbCommand;
                        dbAdapter.Fill(dbDataset);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentReceipt>>sp_GetPaymentDetails return rows: " + dbDataset.Tables[0].Rows.Count.ToString());
                        ReportDataSource datasource = new ReportDataSource("PaymentDetails", dbDataset.Tables[0]);
                        PaymentInvoiceRV.LocalReport.DataSources.Add(datasource);
                    }
                    using (SqlCommand dbCommand = new SqlCommand())
                    {
                        SqlDataAdapter dbAdapter = new SqlDataAdapter();
                        DataSet dbDataset = new DataSet();
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "sp_GetClientDetailsForInvoice";
                        dbCommand.Parameters.AddWithValue("@CETClientID", payment.CETClientID);
                        dbAdapter.SelectCommand = dbCommand;
                        dbAdapter.Fill(dbDataset);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentReceipt>>sp_GetClientDetailsForInvoice return rows: " + dbDataset.Tables[0].Rows.Count.ToString());
                        ReportDataSource datasource = new ReportDataSource("ClientDetails", dbDataset.Tables[0]);
                        PaymentInvoiceRV.LocalReport.DataSources.Add(datasource);
                    }
                    using (SqlCommand dbCommand = new SqlCommand())
                    {
                        SqlDataAdapter dbAdapter = new SqlDataAdapter();
                        DataSet dbDataset = new DataSet();
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "sp_GetCompanyDetails";
                        dbAdapter.SelectCommand = dbCommand;
                        dbAdapter.Fill(dbDataset);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentReceipt>>sp_GetCompanyDetails return rows: " + dbDataset.Tables[0].Rows.Count.ToString());
                        ReportDataSource datasource = new ReportDataSource("CompanyDetailsDS", dbDataset.Tables[0]);
                        PaymentInvoiceRV.LocalReport.DataSources.Add(datasource);
                    }



                    byte[] bytes = PaymentInvoiceRV.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=PaymentInvoice_" + payment.PaymentOrderNo + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes); // create the file
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            finally
            {
                dbConnection.Close();
            }

        }
    }
}