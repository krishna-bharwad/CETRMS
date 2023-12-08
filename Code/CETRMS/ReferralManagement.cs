using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CETRMS;

namespace CETRMS
{
    public static class ReferralManagement
    {
        public static int GetCandidateReferralCode(string CandidateId, ref string ReferralCode)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            dbConnection.Open();
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "Select ReferralCode from UECandidate where CandidateId =" + CandidateId;
                dbCommand.ExecuteScalar();
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dt);
                ReferralCode = "Please check or use my referral code for signIn " + ConfigurationManager.AppSettings["AppURL"] + "/ReferredCandidateSignUp?ReferralCode=";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ReferralCode += row["ReferralCode"].ToString();
                        iRetValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int InsertReferralDetails(string UECandidateId, string ReferralCode)
        {
            int IRetValue = 0;
            string ReferralID = string.Empty;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_InsertReferralDetails";
                dbCommand.Parameters.AddWithValue("@UECandidateId", UECandidateId);
                dbCommand.Parameters.AddWithValue("@ReferralCode", ReferralCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                foreach (DataRow row in dtData.Rows)
                {
                    ReferralID = row["ReferralID"].ToString();
                    IRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
        public static int GetCandidateIDByReferralCode(string ReferralCode, ref string CandidateID)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "Select CandidateId from UECandidate where ReferralCode='" + ReferralCode + "'";
                dbCommand.ExecuteScalar();
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CandidateID = row["CandidateId"].ToString();
                        IRetValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
        public static int GetReferralDetailsByStatus(ref List<Referral> ReferralDetail, int ReferralStatus = -1)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "sp_GetReferralDetailsByStatus";
                dbCommand.Parameters.AddWithValue("@ReferralStatus", ReferralStatus);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count >= 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Referral referr = new Referral();
                        referr.ReferralID = row["ReferralID"].ToString();
                        referr.ReferralStatus = row["ReferralStatus"].ToString();
                        referr.UECandidateID = row["UECandidateID"].ToString();          // New Candidate Id
                        referr.ReferralCode = row["ReferralCode"].ToString();           //Referral  code  is of  Old Candidate who referred new one.
                        ReferralDetail.Add(referr);
                    }
                    IRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
     
        public static int GetCandidateReferralListByCandidateID(string candidateId, ref List<Referral> ReferralDetail)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetReferralDetailsByList";
                dbCommand.Parameters.AddWithValue("@UECandidateId", candidateId);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count >= 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Referral referr = new Referral();
                        referr.ReferralID = row["ReferralID"].ToString();
                        referr.ReferralStatus = row["ReferralStatus"].ToString();
                        referr.UECandidateID = row["UECandidateID"].ToString();          // New Candidate Id
                        referr.ReferralCode = row["ReferralCode"].ToString();           //Referral  code  is of  Old Candidate who referred new one.
                        ReferralDetail.Add(referr);
                    }
                    IRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
        public static int UpdateCandidateReferralStatus(string ReferralID, int ReferralStatus)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = "Update UEReferral set ReferralStatus ="+ ReferralStatus+ "where ReferralID =" + ReferralID;
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        }
        public static int GetReferralDetailsByReferralId(string ReferralId, ref Referral Referraldetails)
        {
            int IRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetReferralDetails";
                dbCommand.Parameters.AddWithValue("@ReferralID", ReferralId);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count >= 1)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Referraldetails.ReferralID = row["ReferralID"].ToString();
                        Referraldetails.ReferralStatus = row["ReferralStatus"].ToString();
                        Referraldetails.UECandidateID = row["UECandidateID"].ToString();
                        Referraldetails.ReferralCode = row["ReferralCode"].ToString();           
                    }
                    IRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                IRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return IRetValue;
        
        }
    }
}