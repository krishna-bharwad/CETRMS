using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CETRMS
{
    public static class TestimonialManager
    {
        public static int AddNewTestimonial(Testimonial testimonial)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", ">>>AddNewTestimonial()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddTestimonial";
                dbCommand.Parameters.AddWithValue("@UEClientID", testimonial.UEClientId);
                dbCommand.Parameters.AddWithValue("@ResponseDate", testimonial.ResponseDate);
                dbCommand.Parameters.AddWithValue("@ResponseMessage", testimonial.ResponseMessage);
                dbCommand.Parameters.AddWithValue("@Rating", testimonial.Rating);
                dbCommand.ExecuteNonQuery();

                logger.log(logger.LogSeverity.INF, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "AddNewTestimonial :: Successfully add testimonial.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "AddNewTestimonial :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "<<<AddNewTestimonial :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetTestimonialByID(int TestimonialID, ref Testimonial testimonial)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", ">>>GetTestimonialByID()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "SELECT [RecID], [UEClientID],[ResponseDate],[Rating],[ResponseMessage],[IsShown] FROM [UETestimonials] Where TestimonialId = @TestimonialID";
                dbCommand.Parameters.AddWithValue("@TestimonialID", TestimonialID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach(DataRow row in dtData.Rows)
                    {
                        testimonial.TestimonialID = row["RecID"].ToString();
                        testimonial.UEClientId = row["UEClientID"].ToString();
                        testimonial.ResponseDate = Convert.ToDateTime(row["ResponseDate"]);
                        testimonial.ResponseMessage = row["ResponseMessage"].ToString();
                        testimonial.IsShown = (int)row["IsShown"];
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialByID :: Successfully add testimonial.");
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialByID :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "<<<GetTestimonialByID :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetAllTestimonialList(ref List<Testimonial> testimonials, int IsShown = -1)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", ">>>GetAllTestimonialList()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetTestimonialList";
                dbCommand.Parameters.AddWithValue("@IsShown", IsShown);
                dbCommand.Parameters.AddWithValue("@Rating", -1); // All Ratings
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Testimonial testimonial = new Testimonial();

                        testimonial.TestimonialID = row["RecID"].ToString();
                        testimonial.UEClientId = row["UEClientID"].ToString();
                        testimonial.ResponseDate = Convert.ToDateTime(row["ResponseDate"]);
                        testimonial.ResponseMessage = row["ResponseMessage"].ToString();
                        testimonial.IsShown = (int)row["IsShown"];
                        testimonial.Rating = (int)row["Rating"];
                        testimonials.Add(testimonial);
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetAllTestimonialList :: Successfully get testimonial.");
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetAllTestimonialList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "<<<GetAllTestimonialList :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetTestimonialsByRating(int Rating, ref List<Testimonial> testimonials)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", ">>>GetTestimonialsByRating()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetTestimonialList";
                dbCommand.Parameters.AddWithValue("@IsShown", -1);
                dbCommand.Parameters.AddWithValue("@Rating", Rating); // All Ratings
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Testimonial testimonial = new Testimonial();

                        testimonial.TestimonialID = row["RecID"].ToString();
                        testimonial.UEClientId = row["UEClientID"].ToString();
                        testimonial.ResponseDate = Convert.ToDateTime(row["ResponseDate"]);
                        testimonial.ResponseMessage = row["ResponseMessage"].ToString();
                        testimonial.IsShown = (int)row["IsShown"];
                        testimonials.Add(testimonial);
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialsByRating :: Successfully get testimonial.");
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialsByRating :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "<<<GetTestimonialsByRating :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int UpdateTestimonialStatus(string TestimonialID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", ">>>GetTestimonialByID()");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_ChangeTestimonialStatus";
                dbCommand.Parameters.AddWithValue("@TestimonialID", TestimonialID);
                dbCommand.ExecuteNonQuery();

                logger.log(logger.LogSeverity.INF, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialByID :: Successfully updated testimonial.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "GetTestimonialByID :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.TESTIMONIAL_MANAGEMENT, "", "<<<GetTestimonialByID :: " + iRetValue.ToString());
            return iRetValue;
        }
    }
}