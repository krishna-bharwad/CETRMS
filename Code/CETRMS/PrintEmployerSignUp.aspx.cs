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
    public partial class PrintEmployerSignUp : System.Web.UI.Page
    {
        SqlConnection dbConnection = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;

            if(!IsPostBack)
            {
                GetEmplSignupList();
            }

        }

        protected void GetEmplSignupList()
        {

            try
            {

                //set Processing Mode of Report as Local   
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report   
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/EmployerSignUpReport.rdlc");
                //ReportDataSource rds = new ReportDataSource("CandidateListDataSet");
                // ReportViewer1.LocalReport.DataSources.Add(rds);


                //creating object of DataSet dsMember and filling the DataSet using SQLDataAdapter   
               // CandidateSignUp oCandidateSignup = new CandidateSignUp();
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataSet dbDataset = new DataSet();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetSignUpDetails";
                dbCommand.Parameters.AddWithValue("@ClientTypeID", 1);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dbDataset);

                int rowc = dbDataset.Tables[0].Rows.Count;

                dbConnection.Close();
                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("EmployerSignUpDataSet", dbDataset.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ////Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);


            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
            }
            finally
            {
                dbConnection.Close();
            }


        }
    }
}