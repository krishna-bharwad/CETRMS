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
    public partial class PrintReferralPaidList : System.Web.UI.Page
    {
        SqlConnection dbConnection = new SqlConnection();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            
            if (!IsPostBack)
            {
                CreatePDF();
            }
        }
        private void CreatePDF()
        {
            try
            {
                //set Processing Mode of Report as Local   
                ReferralPaidRv.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report   
                ReferralPaidRv.LocalReport.ReportPath = Server.MapPath("~/ReferralPaidListReport.rdlc");
                //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
             //    ReferralPaidRv.LocalReport.DataSources.Add(rds);


                
               
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetReferralPaidListForReport";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                dbConnection.Close();
                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("ReferralPaidListDataSets", dbDataset.Tables[0]);
                ReferralPaidRv.LocalReport.DataSources.Clear();
                ////Add ReportDataSource   
                ReferralPaidRv.LocalReport.DataSources.Add(rds);
               

            }
            catch (Exception ex)
            {
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}