//using Microsoft.Reporting.Map.WebForms.BingMaps;
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
    public partial class PrintEmployerDetails : System.Web.UI.Page
    {

        public string EmployerId;
        SqlConnection dbConnection = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (!IsPostBack)
                {
                    EmployerId = Request.QueryString.Get("EmployerID");
                    CreateEmployerDetailsPDF();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }

        private void CreateEmployerDetailsPDF()
        {
            try
            {
                string deviceInfo = "";
                string[] streamId;
                Warning[] warning;

                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;



                // ReportViewer CandidateProfileRV = new ReportViewer();
                EmployerDetailsRV.ProcessingMode = ProcessingMode.Local;
                EmployerDetailsRV.LocalReport.ReportPath = Server.MapPath("~/EmployerDetailsReport.rdlc");

                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetEmployerDetailsForReport";
                dbCommand.Parameters.AddWithValue("@EmployerID", EmployerId);

                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);
                int rowc = dbDataset.Tables[0].Rows.Count;

                ReportDataSource EmployerDetailsRDS = new ReportDataSource("EmployerDetailsDataSet", dbDataset.Tables[0]);
                EmployerDetailsRV.LocalReport.DataSources.Clear();
                EmployerDetailsRV.LocalReport.DataSources.Add(EmployerDetailsRDS);
                byte[] bytes = EmployerDetailsRV.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamId, out warning);

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=EmployerDetails.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            finally
            {
                dbConnection.Close();
            }

        }
    }
}