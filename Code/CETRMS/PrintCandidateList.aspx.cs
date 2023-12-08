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
    public partial class PrintCandidateList : System.Web.UI.Page
    {
        List<Candidate> oCandidateList = new List<Candidate>();
          SqlConnection dbConnection = new SqlConnection();
          public string CandidateStatus;
        Candidate oCandidate = new Candidate();
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
                CandidateStatus = Request.QueryString.Get("CandidateStatus");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
        }
        private void CreatePDF()
        {
            try
            {
            //    string DisplayCandidateStatus = string.Empty;
            //       switch (CandidateStatus)
            //        {
            //        case "1":
            //            DisplayCandidateStatus = "New Registration";
            //            CandidateStatusLB.Text = DisplayCandidateStatus;
            //            break;
            //        case "2":
            //            DisplayCandidateStatus = "Candidate Completed Details";
            //            CandidateStatusLB.Text = DisplayCandidateStatus;
            //            break;
            //        case "3":
            //            DisplayCandidateStatus = "Candidate Under Selection Process";
            //            CandidateStatusLB.Text = DisplayCandidateStatus;
            //            break;
            //        case "5":
            //            DisplayCandidateStatus = "Candidate Final Selected";
            //            CandidateStatusLB.Text = DisplayCandidateStatus;
            //            break;
            //        case "6":
            //            DisplayCandidateStatus = "Candidate Rejected";
            //            CandidateStatusLB.Text = DisplayCandidateStatus;
            //            break;
             //}
                   
                    //set Processing Mode of Report as Local   
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    //set path of the Local report   
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/CandidateListReport.rdlc");
                    //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
                    // ReportViewer1.LocalReport.DataSources.Add(rds);


                    //creating object of DataSet dsMember and filling the DataSet using SQLDataAdapter   
                    CandidateListDataSet CandidateListDS = new CandidateListDataSet();
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataSet dbDataset = new DataSet();
                    dbCommand.Connection = dbConnection;
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "sp_GeCandidateListByLocation2";
                    dbCommand.Parameters.AddWithValue("@JobLocation", "all");
                    dbCommand.Parameters.AddWithValue("@CandidateStatus", CandidateStatus);
                    dbAdapter.SelectCommand = dbCommand;
                    dbAdapter.Fill(dbDataset);

                    int rowc = dbDataset.Tables[0].Rows.Count;

                    dbConnection.Close();
                    //Providing DataSource for the Report   
                    ReportDataSource rds = new ReportDataSource("CandidateListDataSet", dbDataset.Tables[0]);
                    ReportDataSource RDCandidateStatus = new ReportDataSource("CandidateStatusDataSet", dbDataset.Tables[0]);
                    ReportDataSource RDCandidateLocation = new ReportDataSource("CandidateStatusDataSet", dbDataset.Tables[0]);



                ReportViewer1.LocalReport.DataSources.Clear();
                    ////Add ReportDataSource   
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DataSources.Add(RDCandidateStatus);
                    ReportViewer1.LocalReport.DataSources.Add(RDCandidateLocation);


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
    
