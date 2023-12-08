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
    public partial class PrintInterviewList : System.Web.UI.Page
    {

        SqlConnection dbConnection = new SqlConnection();
        string InterviewStatus = string.Empty;
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
                InterviewStatus = Request.QueryString.Get("InterviewStatus");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, "", Message);
            }
        }
        private void CreatePDF()
        {
            try
            {
                //set Processing Mode of Report as Local   
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report   
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/InterviewListReport.rdlc");
                //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
                // ReportViewer1.LocalReport.DataSources.Add(rds);


                //creating object of DataSet dsMember and filling the DataSet using SQLDataAdapter   
                //InterviewListDataSet interviewListDataSet = new InterviewListDataSet();
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetInterviewListByStatusForReport";
                dbCommand.Parameters.AddWithValue("@InterviewStatus", InterviewStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                dbConnection.Close();
                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("InterviewListDataSet", dbDataset.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ////Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);
                

            }
            catch (Exception ex)
            {
                string Messege = ex.Message;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.INTERVIEW_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}