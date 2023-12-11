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
    public partial class PrintEmployerList : System.Web.UI.Page
    {
        SqlConnection dbConnection = new SqlConnection();
        public string EmployerStatus;
        List<Employer> oEmployerList = new List<Employer>();

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
                EmployerStatus = Request.QueryString.Get("EmployerStatus");
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }
        private void CreatePDF()
        {
            try
            {
               // EmployerManagement.GetEmployerListByLocation(ref oEmployerList, "all");
                //for (int i = 0; i < oEmployerList.Count; i++)
                //{

              
                //set Processing Mode of Report as Local   
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report   
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/EmployerListReport.rdlc");
                    //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
                    // ReportViewer1.LocalReport.DataSources.Add(rds);


                    //creating object of DataSet dsMember and filling the DataSet using SQLDataAdapter   
                   // EmployerListDataSet EmployerListDS = new EmployerListDataSet();
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetEmployerListByLocation";
                dbCommand.Parameters.AddWithValue("@BusinessLocation", "all");
                dbCommand.Parameters.AddWithValue("@EmployerStatus", EmployerStatus);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                dbConnection.Close();
                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("EmployerListDataSet", dbDataset.Tables[0]);
                ReportDataSource RDEmployerStatus = new ReportDataSource("EmployerStatusDataSet", dbDataset.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                //Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(RDEmployerStatus);

              //  }
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


