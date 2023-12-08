using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CETRMS
{

    public static class ReceiptManagement
    {
        public static int GetPaymentReceipt(Payment payment, ref byte[] ReceiptPDF)
        {
            int iRetValue = 0;
            try
            {
                ReportViewer ReportViewer1 = new ReportViewer();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = "~/PaymentInvoice.rdlc";

                ReportViewer1.LocalReport.DataSources.Clear();

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReceiptPDF = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            return iRetValue;
        }
        public static int GetPaymentReceipt(string PaymentID, ref byte[] ReceiptPDF)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", ">>>GetPaymentReceipt("+ PaymentID+", ref byte[] ReceiptPDF)");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                Payment payment = new Payment();
                if (PaymentManagement.GetPaymentDetailsForReceipt(PaymentID, ref payment) == RetValue.Success)
                {
                    ReportViewer ReportViewer1 = new ReportViewer();
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/PaymentInvoice.rdlc");
                    logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "Local Report file path: " + ReportViewer1.LocalReport.ReportPath);

                    ReportViewer1.LocalReport.DataSources.Clear();

                    dbConnection.Open();
                    using (SqlCommand dbCommand = new SqlCommand())
                    {
                        SqlDataAdapter dbAdapter = new SqlDataAdapter();
                        DataSet dbDataset = new DataSet();
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "sp_GetPaymentDetails";
                        dbCommand.Parameters.AddWithValue("@PaymentRecID", PaymentID);
                        dbAdapter.SelectCommand = dbCommand;
                        dbAdapter.Fill(dbDataset);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentReceipt>>sp_GetPaymentDetails return rows: " + dbDataset.Tables[0].Rows.Count.ToString());
                        ReportDataSource datasource = new ReportDataSource("PaymentDetails", dbDataset.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                    using (SqlCommand dbCommand = new SqlCommand())
                    {
                        SqlDataAdapter dbAdapter = new SqlDataAdapter();
                        DataSet dbDataset = new DataSet();
                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = "sp_GetClientDetailsForInvoice";
                        dbCommand.Parameters.AddWithValue("@UEClientId", payment.UEClientID);
                        dbAdapter.SelectCommand = dbCommand;
                        dbAdapter.Fill(dbDataset);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "GetPaymentReceipt>>sp_GetClientDetailsForInvoice return rows: " + dbDataset.Tables[0].Rows.Count.ToString());
                        ReportDataSource datasource = new ReportDataSource("ClientDetails", dbDataset.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
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
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    ReceiptPDF = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.PAYMENT_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.PAYMENT_MANAGEMENT, "", "<<<GetPaymentReceipt(" + iRetValue.ToString() + ")");

            return iRetValue;
        }
    }
}