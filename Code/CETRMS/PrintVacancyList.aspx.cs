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
    public partial class PrintVacancyList : System.Web.UI.Page
    {

        SqlConnection dbConnection = new SqlConnection();
        public string VacancyStatus;

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
                VacancyStatus = Request.QueryString.Get("VacancyStatus");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.VACANCY_MANAGEMENT, "", Message);
            }
        }
        private void CreatePDF()
        {
            try
            {
                //set Processing Mode of Report as Local   
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report   
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/VacancyListReport.rdlc");
                //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
                // ReportViewer1.LocalReport.DataSources.Add(rds);


                //creating object of DataSet dsMember and filling the DataSet using SQLDataAdapter   
                VacancyListDataSet VacancyListDS = new VacancyListDataSet();
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetVacancyListByLocation";
                dbCommand.Parameters.AddWithValue("@JobLocation", "all");
                dbCommand.Parameters.AddWithValue("@VacancyStatus", VacancyStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                dbConnection.Close();
                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("VacancyListDataSet", dbDataset.Tables[0]);
                ReportDataSource RDVacancyStatus = new ReportDataSource("VacancyStatusDataSet", dbDataset.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ////Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(RDVacancyStatus);

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