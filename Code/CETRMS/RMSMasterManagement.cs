using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CETRMS
{
    public static class RMSMasterManagement
    {
        /// <summary>
        /// Function to authenticate credentials of UEStaffMember
        /// </summary>
        /// <param name="_UserId">
        /// Userid of person trying to log in to the system.
        /// </param>
        /// <param name="_Password">
        /// Password of the person trying to log in to the system.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>User cannot be authenticated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Incorrect user-id</description>
        /// </item>
        /// <item>
        /// <term>-2</term>
        /// <description>Incorrect Password</description>
        /// </item> 
        /// <item>
        /// <term>1</term>
        /// <description>Authentic user.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int AuthenticateUEStaff(string _UserId, string _Password)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>> AuthenticateUEStaff(" + _UserId + ", " + _Password + ")");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AuthenticateUser";
                dbCommand.Parameters.AddWithValue("@UserId", _UserId);
                dbCommand.Parameters.AddWithValue("@Password", _Password);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                iRetValue = (int)dtData.Rows[0][0];
                if (iRetValue != 1)
                {
                    string ExceptionMessage = string.Empty;
                    switch (iRetValue)
                    {
                        case 0:
                            ExceptionMessage = "Incorrect userid";
                            break;
                        case -2:
                            ExceptionMessage = "Incorrect Password";       
                            break;
                    }
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "AuthenticateEmployer :: " + _UserId + " cannot login. Message: " + ExceptionMessage);
                    //throw new CETRMSExceptions(ExceptionMessage);   
                }
                else
                    logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "AuthenticateEmployer :: " + _UserId + " successfully loggedIn.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<AuthenticateEmployer :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to add new UEStaffMember. If function executes succesfully then it fills UEStaffMember.UserID with newly assigned userid. Default password for new staff member will be new userid.
        /// </summary>
        /// <param name="NewStaffMember">
        /// Details of the staff member, which is to be added into database. The parameter is passed as reference. 
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>New Staff Member cannot be added due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>New Staff Member cannot be added due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>New Staff Member added successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>          
        public static int AddNewUEStaff(ref UEStaffMember NewStaffMember)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>> AddNewUEStaff(" + new JavaScriptSerializer().Serialize(NewStaffMember).ToString() + ")");

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
                dbCommand.CommandText = "sp_AddNewStaffMember";
                dbCommand.Parameters.AddWithValue("@Name", NewStaffMember.Name);
                dbCommand.Parameters.AddWithValue("@Address", NewStaffMember.Address);
                dbCommand.Parameters.AddWithValue("@MobileNo", NewStaffMember.MobileNo);
                dbCommand.Parameters.AddWithValue("@Email", NewStaffMember.Email);
                dbCommand.Parameters.AddWithValue("@Designation", NewStaffMember.Designation);
                dbCommand.Parameters.AddWithValue("@TeamId", NewStaffMember.TeamId);
                dbCommand.Parameters.AddWithValue("@StaffPhoto", NewStaffMember.StaffPhoto);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                iRetValue = (int)dtData.Rows[0][0];
                NewStaffMember.UserId = dtData.Rows[0][0].ToString();
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "AddNewUEStaff :: New Staff Member " + NewStaffMember.Name + " added successfully.");

            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "AddNewUEStaff :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<AddNewUEStaff :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to reset UEStaff Member password using provided string UserID. After successfuly execution of the function. Staff member's user-id will be his/her new password.
        /// </summary>
        /// <param name="_UserId">
        /// User-id of Universal Education staff member, whose password is required.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Password cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Password cannot be updated due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Password updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        public static int ResetUEStaffPassword(string _UserId)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString
                );
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "update MISUser SET Password = @UserId WHERE UserId = @UserId";
                dbCommand.Parameters.AddWithValue("@UserId", _UserId);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to reset UEStaff Member password using provided UEStaffMember datatype. After successfuly execution of the function. Staff member's user-id will be his/her new password.
        /// </summary>
        /// <param name="StaffMemberDetails">
        /// Universal Education Staff Member details, whose password is required to be reset.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Password cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Password cannot be updated due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Password updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int ResetUEStaffPassword(UEStaffMember StaffMemberDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>ResetUEStaffPassword(" + new JavaScriptSerializer().Serialize(StaffMemberDetails) + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "update MISUser SET Password = @UserId WHERE UserId = @UserId";
                dbCommand.Parameters.AddWithValue("@UserId", StaffMemberDetails.UserId);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "ResetUEStaffPassword :: UE Staff member details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "ResetUEStaffPassword :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<ResetUEStaffPassword :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to update UEStaffMember details
        /// </summary>
        /// <param name="NewStaffMember">
        /// Universal Education Staff Member details, which is to be updated.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Staff Member details cannot be updated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Staff Member details cannot be updated due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Staff Member details updated successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>
        public static int UpdateUEStaff(UEStaffMember NewStaffMember)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>UpdateUEStaff(" + new JavaScriptSerializer().Serialize(NewStaffMember) + ")");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_UpdateStaffMemberDetails";
                dbCommand.Parameters.AddWithValue("@UserId", NewStaffMember.UserId);
                dbCommand.Parameters.AddWithValue("@Name", NewStaffMember.Name);
                dbCommand.Parameters.AddWithValue("@Address", NewStaffMember.Address);
                dbCommand.Parameters.AddWithValue("@MobileNo", NewStaffMember.MobileNo);
                dbCommand.Parameters.AddWithValue("@Email", NewStaffMember.Email);
                dbCommand.Parameters.AddWithValue("@Designation", NewStaffMember.Designation);
                dbCommand.Parameters.AddWithValue("@TeamId", NewStaffMember.TeamId);
                dbCommand.Parameters.AddWithValue("@UserStatus", NewStaffMember.UserStatus);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "UpdateUEStaff :: UE Staff member details updated successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "UpdateUEStaff :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<UpdateUEStaff :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Details of Staff Member from userid
        /// </summary>
        /// <param name="StaffMemberDetails">
        /// Universal Education Staff Member details, which is to be filled.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Staff Member details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Staff Member details cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Staff Member details fetched successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>
        public static int GetStaffMemberDetails(string userid, ref UEStaffMember StaffMemberDetails)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>GetStaffMemberDetails("+ userid+", ref UEStaffMember StaffMemberDetails)");
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            SqlDataAdapter dbAdapter = new SqlDataAdapter();
            DataTable dtData = new DataTable();
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetStaffDetails";
                dbCommand.Parameters.AddWithValue("@UserId", userid);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if(dtData.Rows.Count>0)
                {
                    logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "sp_GetStaffDetails:: fetched rows :: " + dtData.Rows.Count.ToString());
                    foreach (DataRow row in dtData.Rows)
                    {
                        StaffMemberDetails.UserId = row["userid"].ToString();
                        StaffMemberDetails.Name = row["Name"].ToString();
                        StaffMemberDetails.Address = row["Address"].ToString();
                        StaffMemberDetails.MobileNo = row["MobileNo"].ToString();
                        StaffMemberDetails.Email = row["Email"].ToString();
                        StaffMemberDetails.TeamId = row["TeamID"].ToString();
                        if (row["StaffPhoto"] != DBNull.Value)
                            StaffMemberDetails.StaffPhoto = (byte[])row["StaffPhoto"];
                        StaffMemberDetails.UserStatus = (int)row["UserStatus"];
                        StaffMemberDetails.Designation = row["Designation"].ToString();


                        // logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "sp_GetStaffDetails:: fetched row :: " + new JavaScriptSerializer().Serialize(StaffMemberDetails).ToString());
                    }
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "Staff details fetched successfully.");
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "GetStaffMemberDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<GetStaffMemberDetails :: " + iRetValue.ToString());
            return iRetValue;
        }

        /// <summary>
        /// Function to get Update Password Details.
        /// </summary>
        /// <param name="UserId">
        /// Userid of person trying to log in to the system.
        /// </param>
        /// <param name="Password">
        /// Updated Password of the person trying to log in to the system.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Old Password or UserId is incorrect.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>User cannot be authenticated due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>New Password Set.</description>
        /// </item> 
        /// </list>
        /// </returns>  
        public static int UpdatePassword(string UserId,string OldPassword, string NewPassword)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", ">>>UpdatePassword(" + UserId + ",string OldPassword, string NewPassword)");

            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_ChangePassword";
                dbCommand.Parameters.AddWithValue("@UserID", UserId);
                dbCommand.Parameters.AddWithValue("@OldPassword", OldPassword);
                dbCommand.Parameters.AddWithValue("@NewPassword", NewPassword);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                iRetValue = (int)dtData.Rows[0][0];
                
                string Message=string.Empty;
                if (iRetValue == 1)
                {
                    Message = "New Password Set Succefully";
                }
                else
                {
                    Message = "Old Password OR UserId is Incorrect";   // if iRetValue=-1 then 
                }
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CET_LOGIN, "", "UpdatePassword :: " + Message);
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "UpdatePassword :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CET_LOGIN, "", "<<<UpdatePassword :: " + iRetValue.ToString());
            return iRetValue;
        }
        public static int GetVisaTypeList(ref List<VisaType> VisaTypeList, string CountryName = "all")
        {
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
                dbCommand.CommandText = "sp_GetVisaTypeList";
                dbCommand.Parameters.AddWithValue("@VisaCountry", CountryName);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach(DataRow row in dtData.Rows)
                    {
                        VisaType visaType = new VisaType();
                        visaType.VisaTypeID = row["RecID"].ToString();
                        visaType.VisaTypeName = row["VisaTypeName"].ToString();
                        visaType.VisaCountryName = row["VisaCountry"].ToString();
                        visaType.VisaValidityYears = row["VisaValidity"].ToString();
                        visaType.VisaTypeDetails = row["VisaTypeDetails"].ToString();
                        visaType.VisaStateCode = row["VisaStateCode"].ToString();

                        VisaTypeList.Add(visaType);
                    }
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "UpdatePassword :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int GetVisaTypeDetails(string VisaTypeID, ref VisaType visaType)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "SELECT [RecID]" +
                                        ",[VisaTypeName]" +
                                        ",[VisaCountry]" +
                                        ",[VisaValidity]" +
                                        ",[VisaTypeDetails]" +
                                        ",[VisaStateCode] " +
                                        "FROM[dbo].[UEVisaType]" +
                                        " WHERE [RecID] = @RecID";
                dbCommand.Parameters.AddWithValue("@RecID", VisaTypeID);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        visaType.VisaTypeName = row["VisaTypeName"].ToString();
                        visaType.VisaCountryName = row["VisaCountry"].ToString();
                        visaType.VisaValidityYears = row["VisaValidity"].ToString();
                        visaType.VisaTypeDetails = row["VisaTypeDetails"].ToString();
                        visaType.VisaStateCode = row["VisaStateCode"].ToString();
                    }
                    iRetValue = 1;
                }
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "UpdatePassword :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int AddVisaTypeDetails(VisaType visaType)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "INSERT INTO [dbo].[UEVisaType]"+
                                        "([VisaTypeName]"+
                                        ",[VisaCountry]"+
                                        ",[VisaValidity]"+
                                        ",[VisaTypeDetails]"+
                                        ",[VisaStateCode])"+
                                        "VALUES"+
                                        "(@VisaTypeName"+
                                        ",@VisaCountry"+
                                        ",@VisaValidity"+
                                        ",@VisaTypeDetails"+
                                        ",@VisaStateCode)";
                dbCommand.Parameters.AddWithValue("@VisaTypeName", visaType.VisaTypeName);
                dbCommand.Parameters.AddWithValue("@VisaCountry", visaType.VisaCountryName);
                dbCommand.Parameters.AddWithValue("@VisaValidity", visaType.VisaValidityYears);
                dbCommand.Parameters.AddWithValue("@VisaTypeDetails", visaType.VisaTypeDetails);
                dbCommand.Parameters.AddWithValue("@VisaStateCode", visaType.VisaStateCode);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "AddVisaTypeDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int UpdateVisaTypeDetails(VisaType visaType)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE UEVisaType SET " +
                                        "[VisaTypeName] = @VisaTypeName" +
                                        ",[VisaCountry] = @VisaCountry" +
                                        ",[VisaValidity] = @VisaValidity" +
                                        ",[VisaTypeDetails] = @VisaTypeDetails" +
                                        ",[VisaStateCode] = @VisaStateCode" +
                                        " WHERE RecID = @VisaTypeID";
                dbCommand.Parameters.AddWithValue("@VisaTypeName", visaType.VisaTypeName);
                dbCommand.Parameters.AddWithValue("@VisaCountry", visaType.VisaCountryName);
                dbCommand.Parameters.AddWithValue("@VisaValidity", visaType.VisaValidityYears);
                dbCommand.Parameters.AddWithValue("@VisaTypeDetails", visaType.VisaTypeDetails);
                dbCommand.Parameters.AddWithValue("@VisaStateCode", visaType.VisaStateCode);
                dbCommand.Parameters.AddWithValue("@VisaTypeID", visaType.VisaTypeID);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CET_LOGIN, "", "AddVisaTypeDetails :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int GetCompanyDetails(ref CompanyProfile companyProfile)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();

                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetCompanyDetails";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);

                if(dtData.Rows.Count > 0)
                {
                    foreach(DataRow row in dtData.Rows)
                    {
                        companyProfile.CompanyBillingName = row["CompanyBillingName"].ToString();
                        companyProfile.BillingAddress = row["BillingAddress"].ToString();
                        companyProfile.BillingDistrict = row["BillingDistrict"].ToString();
                        companyProfile.BillingState = row["BillingState"].ToString();
                        companyProfile.BillingCountry = row["BillingCountry"].ToString();
                        companyProfile.GSTNumber = row["GSTNumber"].ToString();
                        companyProfile.WhatsaAppMobileNo = row["WhatsaAppMobileNo"].ToString();
                        companyProfile.CompanyWebURL = row["CompanyWebURL"].ToString();
                        companyProfile.SupportContactNumber = row["SupportContactNumber"].ToString();
                        companyProfile.NoReplyEmail = row["NoReplyEmail"].ToString();
                        companyProfile.SupportEmail = row["SupportEmail"].ToString();
                        companyProfile.NewsLetterEmail = row["NewsLetterEmail"].ToString();
                        companyProfile.AccountsEmail = row["AccountsEmail"].ToString();
                        companyProfile.InfoEmail = row["InfoEmail"].ToString();
                        companyProfile.ReferralRegistrationLink = row["ReferralRegistrationLink"].ToString();
                    }
                    iRetValue = RetValue.Success;
                }

            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int UpdateCompanyDetails(CompanyProfile companyProfile)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();

                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "UPDATE [dbo].[UECompanyDetails] " +
                                "SET" +
                                " [CompanyBillingName] = @CompanyBillingName" +
                                ",[BillingAddress] = @BillingAddress" +
                                ",[BillingDistrict] = @BillingDistrict" +
                                ",[BillingState] = @BillingState" +
                                ",[BillingCountry] = @BillingCountry" +
                                ",[GSTNumber] = @GSTNumber" +
                                ",[WhatsaAppMobileNo] = @WhatsaAppMobileNo" +
                                ",[SupportContactNumber] = @SupportContactNumber" +
                                ",[CompanyWebURL] = @CompanyWebURL" +
                                ",[NoReplyEmail] = @NoReplyEmail" +
                                ",[SupportEmail] = @SupportEmail" +
                                ",[NewsLetterEmail] = @NewsLetterEmail" +
                                ",[AccountsEmail] = @AccountsEmail" +
                                ",[ReferralRegistrationLink] = @ReferralRegistrationLink" +
                                ",[InfoEmail] = @InfoEmail";
                dbCommand.Parameters.AddWithValue("@CompanyBillingName", companyProfile.CompanyBillingName);
                dbCommand.Parameters.AddWithValue("@BillingAddress", companyProfile.BillingAddress);
                dbCommand.Parameters.AddWithValue("@BillingDistrict", companyProfile.BillingDistrict);
                dbCommand.Parameters.AddWithValue("@BillingState", companyProfile.BillingState);
                dbCommand.Parameters.AddWithValue("@BillingCountry", companyProfile.BillingCountry);
                dbCommand.Parameters.AddWithValue("@GSTNumber", companyProfile.GSTNumber);
                dbCommand.Parameters.AddWithValue("@WhatsaAppMobileNo", companyProfile.WhatsaAppMobileNo);
                dbCommand.Parameters.AddWithValue("@CompanyWebURL", companyProfile.CompanyWebURL);
                dbCommand.Parameters.AddWithValue("@SupportContactNumber", companyProfile.SupportContactNumber);
                dbCommand.Parameters.AddWithValue("@NoReplyEmail", companyProfile.NoReplyEmail);
                dbCommand.Parameters.AddWithValue("@SupportEmail", companyProfile.SupportEmail);
                dbCommand.Parameters.AddWithValue("@NewsLetterEmail", companyProfile.NewsLetterEmail);
                dbCommand.Parameters.AddWithValue("@AccountsEmail", companyProfile.AccountsEmail);
                dbCommand.Parameters.AddWithValue("@InfoEmail", companyProfile.InfoEmail);
                dbCommand.Parameters.AddWithValue("@ReferralRegistrationLink", companyProfile.ReferralRegistrationLink);
                dbCommand.ExecuteNonQuery();
                iRetValue = RetValue.Success;
            }
            catch (Exception ex)
            {
                iRetValue = RetValue.Error;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", ">>>UpdateCompanyDetails" + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }



        /// <summary>
        /// Function to Get Sign up Details of Candidate and Employer.
        /// </summary>
        /// <param name="CommonID>
        /// CommonID, whose list is required
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>NILL</term>
        /// <description>Sign Up details cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>sRetValue</term>
        /// <description>Sign Up Details</description>
        /// </item> 
        /// </list>
        /// </returns>  
        /// <exception cref="CETRMSExceptions">
        /// Custom CETRMSException to return user authentication 
        /// </exception>
        public static int GetSignUpDetails(string CommonID,ref List<UEClient> ListUEClient)             //Added By Krishna
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>GetCandidateMaskedList(" + CommonID + ", ref Candidate CandidateDetail)");
            int iRetValue = -1;
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString);
            try
            {
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetSignUpDetails";
                dbCommand.Parameters.AddWithValue("@ClientTypeID", CommonID);
                
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        UEClient oUEClient = new UEClient();
                      oUEClient.AuthenticationName  = row["AuthenticationName"].ToString();
                      oUEClient.AuthenticationProfileURL = row["AuthenticationProfileURL"].ToString();
                      oUEClient.SignedUpOn = (DateTime) row["SignedUpOn"];
                      oUEClient.AuthenticationId = row["AuthenticationID"].ToString();


                        ListUEClient.Add(oUEClient);
                        logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedList :: Data : ");
                    }
                }
                iRetValue = 1;
                logger.log(logger.LogSeverity.INF, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetCandidateMaskedList :: Executed successfully.");
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", "GetCandidateMaskedList :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<GetCandidateMaskedList :: " + iRetValue.ToString());
            return iRetValue;
        }
        /// <summary>
        /// Function to get Employer Dashboard Data.
        /// </summary>
        /// <param name="DashboardJSON">
        /// String Dashboard data in JSON format.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>-1</term>
        /// <description>Dashboard data cannot be fetched due to exception/error in code execution.</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>Dashboard data cannot be fetched due to mismatch in UserId.</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Dashboard data fetched  successfully.</description>
        /// </item> 
        /// </list>
        /// </returns>   
        public static int GetIndexPageParameters(ref string DashboardJSON)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MANAGEMENT, "", ">>>GetIndexPageParameters()");
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
                dbCommand.CommandText = "sp_GetIndexPageParameters";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, int> DashboardData = new Dictionary<string, int>();
                        DashboardData.Add("CandidatesOnBoarded", (int)row["CandidatesOnBoarded"]);
                        DashboardData.Add("EmployersOnBoarded", (int)row["EmployersOnBoarded"]);
                        DashboardData.Add("Locations", (int)row["Locations"]);
                        DashboardData.Add("VacanciesPublished", (int)row["VacanciesPublished"]);
                        DashboardData.Add("ScheduledInterviews", (int)row["ScheduledInterviews"]);
                        DashboardData.Add("JobApplication", (int)row["JobApplication"]);

                        DashboardJSON = JsonConvert.SerializeObject(DashboardData);

                        logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MANAGEMENT, "", "GetIndexPageParameters :: Successfully fetched Employer Dashboard Data.");
                    }
                }
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, "", "GetIndexPageParameters :: " + Message);
            }
            finally
            {
                dbConnection.Close();
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.WEBPAGE, "", "<<<GetIndexPageParameters :: " + iRetValue.ToString());
            return iRetValue;
        }
    }
}