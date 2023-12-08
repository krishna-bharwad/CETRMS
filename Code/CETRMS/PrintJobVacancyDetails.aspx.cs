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
    public partial class PrintJobVacancyDetails : System.Web.UI.Page
    {
        public string VacancyId;
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
                    VacancyId = Request.QueryString.Get("VacancyID");
                    CreateJobVacancyDetailsPDF();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }

        private void CreateJobVacancyDetailsPDF()
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
                JobVacancyDetailsRV.ProcessingMode = ProcessingMode.Local;
                JobVacancyDetailsRV.LocalReport.ReportPath = Server.MapPath("~/JobVacancyDetailsReport.rdlc");

                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetVacancyDetailsForReport";
                dbCommand.Parameters.AddWithValue("@VacancyID", VacancyId);

                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);
                int rowc = dbDataset.Tables[0].Rows.Count;

                ReportDataSource JobVacancyDetailsRDS = new ReportDataSource("JobVacancyDetailsDataSet", dbDataset.Tables[0]);
                JobVacancyDetailsRV.LocalReport.DataSources.Clear();
                JobVacancyDetailsRV.LocalReport.DataSources.Add(JobVacancyDetailsRDS);
                byte[] bytes = JobVacancyDetailsRV.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamId, out warning);

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=JobVacancyDetails.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }

        }
    }
}