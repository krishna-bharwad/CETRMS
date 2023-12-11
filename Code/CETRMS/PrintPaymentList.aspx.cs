using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class PrintPaymentList : System.Web.UI.Page
    {
        SqlConnection dbConnection = new SqlConnection();
        public string sPaymentStatus;
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
                sPaymentStatus = Request.QueryString.Get("PaymentStatus");
                if (!IsPostBack)
                {
                    CreatePDF();

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

        private void CreatePDF()
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PaymentListReport.rdlc");

                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetPaymentList";
                dbCommand.Parameters.AddWithValue("@CETClientID", "-1");
                dbCommand.Parameters.AddWithValue("@PaymentStatus", sPaymentStatus);
                dbCommand.Parameters.AddWithValue("@PaymentType", "-1");
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                ReportDataSource rds = new ReportDataSource("PaymentListDataSet", dbDataset.Tables[0]);
                //ReportDataSource RDPaymentStatus = new ReportDataSource("PaymentStatusDataSet", dbDataset.Tables[0]);
                



                ReportViewer1.LocalReport.DataSources.Clear();
                ////Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);
                //ReportViewer1.LocalReport.DataSources.Add(RDPaymentStatus);
               


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
                dbConnection.Close();

            }
        }
    }
}