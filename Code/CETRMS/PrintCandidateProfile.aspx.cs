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
    public partial class PrintCandidateProfile : System.Web.UI.Page
    {
        public string CandidateId;
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
                    CandidateId = Request.QueryString.Get("CandidateID");
                    CreateCandidateProfilePDF();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        private void CreateCandidateProfilePDF()
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
               CandidateProfileRV.ProcessingMode = ProcessingMode.Local;
               CandidateProfileRV.LocalReport.ReportPath = Server.MapPath("~/CandidateProfileReport.rdlc");
         
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetCandidateFullDetailsForReport";
                dbCommand.Parameters.AddWithValue("@CandidateID", CandidateId);
                
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);
                int rowc = dbDataset.Tables[0].Rows.Count;

                ReportDataSource CandidateProfileRDS = new ReportDataSource("CandidateProfileDataSet", dbDataset.Tables[0]);
                CandidateProfileRV.LocalReport.DataSources.Clear();
                CandidateProfileRV.LocalReport.DataSources.Add(CandidateProfileRDS);
                byte[] bytes = CandidateProfileRV.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamId, out warning);


                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=CandidateProfile.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes); // create the file
                Response.Flush();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            finally
            {
                dbConnection.Close();
            }

        }
    }
}