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
    public static class LocationManagement
    {
        /// <summary>
        /// Function to fetch State Details.
        /// </summary>
        /// <param name="LocationCode">
        /// Location Code of the State
        /// </param>
        /// <returns>
        /// Object of type cState, containing StateCode, StateName, CountryCode, CountryName
        /// </returns> 
        /// <exception cref="CETRMSExceptions">
        /// State details not found based on Location Code.
        /// </exception>
        public static cState GetStateDetail(string LocationCode)
        {
            cState StateDetails = new cState();
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                if (LocationCode == null)
                {
                    StateDetails.StateName = "Not Available";
                    StateDetails.StateCode = "-1";
                    StateDetails.Country.CountryName = "Not Available";
                    StateDetails.Country.CountryCode = "-1";
                }
                else
                {

                    dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                    dbConnection.Open();
                    SqlCommand dbCommand = new SqlCommand();
                    SqlDataAdapter dbAdapter = new SqlDataAdapter();
                    DataTable dtData = new DataTable();
                    dbCommand.Connection = dbConnection;//krishna
                    dbCommand.CommandType = CommandType.Text; //Krishna
                    dbCommand.CommandText = "select StateName, CountryName, CurrencyName, CurrencySymbol from CETStates where StateCode = @StateCode"; // Add s in UEStates
                    dbCommand.Parameters.AddWithValue("@StateCode", LocationCode);
                    dbAdapter.SelectCommand = dbCommand;
                    dbAdapter.Fill(dtData);
                    if (dtData.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtData.Rows)
                        {
                            StateDetails.Country.CountryName = row["CountryName"].ToString();
                            StateDetails.Country.CurrencyName = row["CurrencyName"].ToString();
                            StateDetails.Country.CurrencySymbol = row["CurrencySymbol"].ToString();
                            StateDetails.Country.CountryCode = LocationCode.Substring(0, 3);
                            StateDetails.StateName = row["StateName"].ToString();
                            StateDetails.StateCode = LocationCode;
                        }
                    }
                    else
                    {
                        StateDetails.StateName = "Not Available";
                        StateDetails.StateCode = "-1";
                        StateDetails.Country.CountryName = "Not Available";
                        StateDetails.Country.CountryCode = "-1";
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }

            return StateDetails;
        }
        /// <summary>
        /// Function to fetch Country Details.
        /// </summary>
        /// <param name="LocationCode">
        /// Location Code of the State
        /// </param>
        /// <returns>
        /// Object of type cCountry, containing CountryCode, CountryName
        /// </returns> 
        /// <exception cref="CETRMSExceptions">
        /// Country details not found based on state Code.
        /// </exception>/// 
        public static cCountry GetCountryDetail(string LocationCode)
        {
            cCountry CountryDetails = new cCountry();
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;//krishna
                dbCommand.CommandType = CommandType.Text; //Krishna
                dbCommand.CommandText = "select distinct CountryName, CurrencyName, CurrencySymbol from CETStates where StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@StateCode", LocationCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CountryDetails.CountryName = row["CountryName"].ToString();
                        CountryDetails.CountryCode = LocationCode.Substring(0, 3);
                        CountryDetails.CurrencyName = row["CurrencyName"].ToString();
                        CountryDetails.CurrencySymbol = row["CurrencySymbol"].ToString();
                    }
                }
                else
                    throw new CETRMSExceptions("Country details not found based on Country Code.");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return CountryDetails;
        }
        /// <summary>
        /// Function to fetch City Details.
        /// </summary>
        /// <param name="CityCode">
        /// Location Code of the City
        /// </param>
        /// <returns>
        /// Object of type cCity, containing CityCode, CityName, StateCode, StateName, CountryCode, CountryName
        /// </returns> 
        /// <exception cref="CETRMSExceptions">
        /// Country details not found based on City Code.
        /// </exception> 
        public static cCity GetCityDetail(string StateCode, string CityCode)
        {
            cCity CityDetails = new cCity();
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;//krishna
                dbCommand.CommandType = CommandType.Text; //Krishna
                dbCommand.CommandText = "SELECT [CityCode], [CityName], CETCity.[StateCode], CETStates.StateName, CETStates.CountryName, CETStates.CurrencySymbol, CETStates.CurrencyName"
                             + " FROM [CETCity] "
                             + "inner join CETStates on CETStates.StateCode = CETCity.StateCode where CityCode = @CityCode and CETCity.StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@CityCode", CityCode);
                dbCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CityDetails.CityCode = CityCode;
                        CityDetails.CityName = row["CityName"].ToString();
                        CityDetails.State.StateCode = row["StateCode"].ToString();
                        CityDetails.State.StateName = row["StateName"].ToString();
                        CityDetails.State.Country.CountryName = row["CountryName"].ToString();
                        CityDetails.State.Country.CountryCode = CityDetails.State.StateCode.Substring(0, 3);
                        CityDetails.State.Country.CurrencySymbol = row["CurrencySymbol"].ToString();
                        CityDetails.State.Country.CurrencyName = row["CurrencyName"].ToString();
                    }
                }
                else
                {
                    // throw new CETRMSExceptions("City details not found based on city Code.");
                    CityDetails.CityName = "Not Available";
                    CityDetails.CityCode = "-1";
                    CityDetails.State.StateName = "Not Available";
                    CityDetails.State.StateCode = "-1";
                    CityDetails.State.Country.CountryName = "Not Available";
                    CityDetails.State.Country.CountryCode = "-1";
                }
            }
            catch(Exception ex)
            {
                CityDetails.CityName = "Not Available";
                CityDetails.CityCode = "-1";
                CityDetails.State.StateName = "Not Available";
                CityDetails.State.StateCode = "-1";
                CityDetails.State.Country.CountryName = "Not Available";
                CityDetails.State.Country.CountryCode = "-1";
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return CityDetails;
        }
        /// <summary>
        /// Function to Add New City Details.
        /// </summary>
        /// <param name="StateCode">
        /// Location Code of the State in which city is to be added
        /// </param>
        /// <param name="NewCityName">
        /// Name of new City
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>NewCityCode</term>
        /// <description>City Successfully Added</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: City cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddCityName(string StateCode, string NewCityName)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddNewCity";
                dbCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbCommand.Parameters.AddWithValue("@CityName", NewCityName);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["NewCityCode"];
                    }
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of cities with matching entered parameter.
        /// </summary>
        /// <param name="StateCode">
        /// Location Code of the State in which city is to be searched
        /// </param>
        /// <param name="CityName">
        /// Letters of the name of the city, which will be matched
        /// </param>
        /// <param name="CityList">
        /// Object of type cCity List, which will be filled with fetched data.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>City List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching city name found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: City List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetMatchingCityList(string StateCode, string CityName, ref List<cCity> CityList)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.CommandText = "SELECT [CityCode], [CityName], UECity.[StateCode], CETStates.StateName, CETStates.CountryName, CETStates.CurrencySymbol, CETStates.CurrencyName"
                    + " FROM [CETCity] "
                    + "inner join CETStates on CETStates.StateCode = CETCity.StateCode where CityName LIKE @CityName + '%' and CETCity.StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@CityName", CityName);
                dbCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cCity CityDetails = new cCity();

                        CityDetails.CityCode = row["CityCode"].ToString();
                        CityDetails.CityName = row["CityName"].ToString();
                        CityDetails.State.StateCode = row["StateCode"].ToString();
                        CityDetails.State.StateName = row["StateName"].ToString();
                        CityDetails.State.Country.CountryName = row["CountryName"].ToString();
                        CityDetails.State.Country.CountryCode = CityDetails.State.StateCode.Substring(0, 3);
                        CityDetails.State.Country.CurrencySymbol = row["CurrencySymbol"].ToString();
                        CityDetails.State.Country.CurrencyName = row["CurrencyName"].ToString();

                        CityList.Add(CityDetails);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of cities in given statecode.
        /// </summary>
        /// <param name="StateCode">
        /// Location Code of the State in which city is to be searched
        /// </param>
        /// <param name="CityList">
        /// Object of type cCity List, which will be filled with fetched data.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>City List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching State code found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: City List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetCitylist(string StateCode, ref List<cCity> CityList)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.CommandText = "SELECT [CityCode], [CityName], CETCity.[StateCode], CETStates.StateName, CETStates.CountryName, CETStates.CurrencySymbol, CETStates.CurrencyName"
                    + " FROM [CETCity] "
                    + "inner join UEStates on CETStates.StateCode = CETCity.StateCode where CETCity.StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cCity CityDetails = new cCity();

                        CityDetails.CityCode = row["CityCode"].ToString();
                        CityDetails.CityName = row["CityName"].ToString();
                        CityDetails.State.StateCode = row["StateCode"].ToString();
                        CityDetails.State.StateName = row["StateName"].ToString();
                        CityDetails.State.Country.CountryName = row["CountryName"].ToString();
                        CityDetails.State.Country.CountryCode = CityDetails.State.StateCode.Substring(0, 3);
                        CityDetails.State.Country.CurrencySymbol = row["CurrencySymbol"].ToString();
                        CityDetails.State.Country.CurrencyName = row["CurrencyName"].ToString();


                        CityList.Add(CityDetails);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of states in given country code.
        /// </summary>
        /// <param name="CountryCode">
        /// Location Code of the Country of which state list is required
        /// </param>
        /// <param name="StateList">
        /// Object of type cState List, which will be filled with fetched data.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>State List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching Country code found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: State List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetStateList(string CountryCode, ref List<cState> StateList)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetStateList";
                dbCommand.Parameters.AddWithValue("@CountryCode", CountryCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cState StateDetail = new cState();
                        StateDetail.StateCode = row["StateCode"].ToString();
                        StateDetail.StateName = row["StateName"].ToString();
                        StateDetail.Country.CountryCode = row["CountryCode"].ToString();
                        StateDetail.Country.CountryName = row["CountryName"].ToString();
                        StateDetail.Country.CurrencyName = row["CurrencyName"].ToString();
                        StateDetail.Country.CurrencySymbol = row["CurrencySymbol"].ToString();
                        StateList.Add(StateDetail);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of country.
        /// </summary>
        /// <param name="CountryList">
        /// Object of type cCountry List.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>Country List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching Country list found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: Country List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetCountryList(ref List<cCountry> CountryList)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetCountryList";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cCountry CountryDetails = new cCountry();
                        CountryDetails.CountryName = row["CountryName"].ToString();
                        CountryDetails.CountryCode = row["CountryCode"].ToString();
                        CountryDetails.CurrencyName = row["CurrencyName"].ToString();
                        CountryDetails.CurrencySymbol = row["CurrencySymbol"].ToString();

                        CountryList.Add(CountryDetails);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of timezone.
        /// </summary>
        /// <param name="TimeZoneList">
        /// Object of type cTimeZone List.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>TimeZone List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching TimeZone list found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: TimeZone List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetTimeZoneList(ref List<cTimeZone> TimeZoneList)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.CommandText = "SELECT [ZTZName],[UTCTZName],[UTCTZDiff] FROM [ZoomTimeZone]";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        cTimeZone TimeZoneDetail = new cTimeZone();

                        TimeZoneDetail.TimeZoneName = row["ZTZName"].ToString();
                        TimeZoneDetail.UTCName= row["UTCTZName"].ToString();
                        TimeZoneDetail.UTCTimeDiff = (double)row["UTCTZDiff"];

                        TimeZoneList.Add(TimeZoneDetail);
                    }
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to fetch TimeZone Details.
        /// </summary>
        /// <param name="TimeZoneName">
        /// Location Code of the State
        /// </param>
        /// <returns>
        /// Object of type cCountry, containing CountryCode, CountryName
        /// </returns> 
        /// <exception cref="CETRMSExceptions">
        /// Country details not found based on state Code.
        /// </exception>/// 
        public static cTimeZone GetTimeZoneDetail(string TimeZoneName)
        {
            cTimeZone TimeZoneDetail = new cTimeZone();
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.CommandText = "SELECT [ZTZName],[UTCTZName],[UTCTZDiff] FROM [ZoomTimeZone] where ZTZName = @ZTZName";
                dbCommand.Parameters.AddWithValue("@ZTZName", TimeZoneName);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        TimeZoneDetail.TimeZoneName = row["ZTZName"].ToString();
                        TimeZoneDetail.UTCName = row["UTCTZName"].ToString();
                        TimeZoneDetail.UTCTimeDiff = (double)row["UTCTZDiff"];
                    }
                }
                else
                    throw new CETRMSExceptions("Time Zone details not found based on Time Zone name.");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return TimeZoneDetail;
        }
        /// <summary>
        /// Function to get list of country.
        /// </summary>
        /// <param name="CountryList">
        /// Object of type cCountry List.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>Country List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching Country list found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: Country List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetCountryList2(ref string CountryListJSON)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetCountryList";
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    List<Dictionary<string, string>> lCountryList = new List<Dictionary<string, string>>();
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, string> CountryData = new Dictionary<string, string>();
                        CountryData.Add(row["CountryName"].ToString().Trim(), row["CountryCode"].ToString().Trim());

                        lCountryList.Add(CountryData);
                    }
                    CountryListJSON = new JavaScriptSerializer().Serialize(lCountryList);
                    CountryListJSON = CountryListJSON.Replace("{", "");
                    CountryListJSON = CountryListJSON.Replace("[", "");
                    CountryListJSON = CountryListJSON.Replace("]", "");
                    CountryListJSON = CountryListJSON.Replace("}", "");
                    CountryListJSON = "{" + CountryListJSON + "}";
                    iRetValue = 1;

                }
                else

                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of states in given country code.
        /// </summary>
        /// <param name="CountryCode">
        /// Location Code of the Country of which state list is required
        /// </param>
        /// <param name="StateList">
        /// Object of type cState List, which will be filled with fetched data.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>State List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching Country code found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: State List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetStateList2(string CountryCode, ref string StateListJSON)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            string CountryName = string.Empty;
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetStateList";
                dbCommand.Parameters.AddWithValue("@CountryCode", CountryCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    Dictionary<string, string> lStateListJSON = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lStateList = new List<Dictionary<string, string>>();

                    foreach (DataRow row in dtData.Rows)
                    {
                        CountryName = row["CountryName"].ToString();
                        Dictionary<string, string> StateData = new Dictionary<string, string>();
                        StateData.Add(row["StateName"].ToString().Trim().ToUpper(), row["StateCode"].ToString().Trim());
                        lStateList.Add(StateData);
                    }
                    lStateListJSON.Add(CountryName, new JavaScriptSerializer().Serialize(lStateList));
                    StateListJSON = new JavaScriptSerializer().Serialize(lStateListJSON);
                    StateListJSON = StateListJSON.Replace("{", "");
                    StateListJSON = StateListJSON.Replace("}", "");
                    StateListJSON = StateListJSON.Replace("[", "");
                    StateListJSON = StateListJSON.Replace("]", "");
                    StateListJSON = StateListJSON.Replace("\\", "");
                    StateListJSON = StateListJSON.Replace(CountryName, "");
                    StateListJSON = "{" + StateListJSON + "}";
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to get list of cities with matching entered parameter.
        /// </summary>
        /// <param name="StateCode">
        /// Location Code of the State in which city is to be searched
        /// </param>
        /// <param name="CityName">
        /// Letters of the name of the city, which will be matched
        /// </param>
        /// <param name="CityList">
        /// Object of type cCity List, which will be filled with fetched data.
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>City List Successfully fetched</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>No matching city name found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: City List cannot be fetched due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int GetMatchingCityList2(string StateCode, string CityName, ref string CityNameListJSON)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandText = "SELECT [CityCode], [CityName], CETCity.[StateCode], CETStates.StateName, CETStates.CountryName"
                    + " FROM [CETCity] "
                    + "inner join CETStates on CETStates.StateCode = CETCity.StateCode where CityName LIKE @CityName + '%' and CETCity.StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@CityName", CityName);
                dbCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {

                    Dictionary<string, string> lCityListJSON = new Dictionary<string, string>();
                    List<Dictionary<string, string>> lCityList = new List<Dictionary<string, string>>();
                    foreach (DataRow row in dtData.Rows)
                    {
                        Dictionary<string, string> CityData = new Dictionary<string, string>();
                        CityData.Add(row["CityName"].ToString().Trim(), row["CityCode"].ToString().Trim());
                        lCityList.Add(CityData);

                        //cCity CityDetails = new cCity();

                        //CityDetails.CityCode = row["CityCode"].ToString();
                        //CityDetails.CityName = row["CityName"].ToString();
                        //CityDetails.State.StateCode = row["StateCode"].ToString();
                        //CityDetails.State.StateName = row["StateName"].ToString();
                        //CityDetails.State.Country.CountryName = row["CountryName"].ToString();
                        //CityDetails.State.Country.CountryCode = CityDetails.State.StateCode.Substring(0, 3);

                        //CityList.Add(CityDetails);
                    }
                    CityNameListJSON = new JavaScriptSerializer().Serialize(lCityList);
                    CityNameListJSON = CityNameListJSON.Replace("{", "");
                    CityNameListJSON = CityNameListJSON.Replace("}", "");
                    CityNameListJSON = CityNameListJSON.Replace("[", "");
                    CityNameListJSON = CityNameListJSON.Replace("]", "");
                    CityNameListJSON = CityNameListJSON.Replace("\\", "");
                    CityNameListJSON = "{" + CityNameListJSON + "}";
                    iRetValue = 1;
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Add New Country Details.
        /// </summary>
        /// <param name="NewCountryName">
        /// Location Code of the State in which city is to be added
        /// </param>
        /// <param name="NewStateName">
        /// Name of new City
        /// </param>
        /// <param name="CurrencyName">
        /// Name of new City
        /// </param>
        /// <param name="CurrencySymbol">
        /// Name of new City
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>NewCountryCode</term>
        /// <description>Country Successfully Added</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: country cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddCountryName(string NewStateName, string NewCountryName, string CurrencyName, string CurrencySymbol)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddNewCountry";
                dbCommand.Parameters.AddWithValue("@CountryName", NewCountryName);
                dbCommand.Parameters.AddWithValue("@StateName", NewStateName);
                dbCommand.Parameters.AddWithValue("@CurrencyName", CurrencyName);
                dbCommand.Parameters.AddWithValue("@CurrencySymbol", CurrencySymbol);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        iRetValue = (int)row["Code"];
                    }
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Add New Country Details.
        /// </summary>
        /// <param name="state">
        /// Object of type state, which hold definition of new country and new state to be added
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>Country Successfully Added</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: country cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddCountryName(ref cState state)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddNewCountry";
                dbCommand.Parameters.AddWithValue("@CountryName", state.Country.CountryName);
                dbCommand.Parameters.AddWithValue("@StateName", state.StateName);
                dbCommand.Parameters.AddWithValue("@CurrencyName", state.Country.CurrencyName);
                dbCommand.Parameters.AddWithValue("@CurrencySymbol", state.Country.CurrencySymbol);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        state.StateCode = row["Code"] + "001";
                        iRetValue = 1;
                    }
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Add New state Details.
        /// </summary>
        /// <param name="state">
        /// Object of type state, which hold definition of new state to be added
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>state Successfully Added</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: state cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddNewState(ref cState state)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddNewState";
                dbCommand.Parameters.AddWithValue("@CountryName", state.Country.CountryName);
                dbCommand.Parameters.AddWithValue("@StateName", state.StateName);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        state.StateCode = row["Code"].ToString();
                        iRetValue = 1;
                    }
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Add New state Details.
        /// </summary>
        /// <param name="NewStateName">
        /// Name of new state to be added.
        /// </param>
        /// <returns>
        /// <param name="CountryName">
        /// Name of Country in which state is to be added
        /// </param>
        /// <param name="StateCode">
        /// Return object in which new state value will be shown
        /// </param>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>state Successfully Added</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: state cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int AddNewState(string NewStateName, string CountryName, ref string StateCode)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_AddNewState";
                dbCommand.Parameters.AddWithValue("@CountryName", CountryName);
                dbCommand.Parameters.AddWithValue("@StateName", NewStateName);
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        StateCode = row["Code"].ToString();
                        iRetValue = 1;
                    }
                }
                else
                    iRetValue = 0;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        public static int GetCurrencySymbol(string CountryCode, ref string CurrencySymbol, ref string CurrencyName)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {

                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                SqlDataAdapter dbAdapter = new SqlDataAdapter();
                DataTable dtData = new DataTable();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_GetStateList";
                dbCommand.Parameters.AddWithValue("@CountryCode", CountryCode); 
                dbAdapter.SelectCommand = dbCommand;
                dbAdapter.Fill(dtData);
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtData.Rows)
                    {
                        CurrencySymbol = row["CurrencySymbol"].ToString();
                        CurrencyName = row["CurrencyName"].ToString();
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Edit Country Details.
        /// </summary>
        /// <param name="country">
        /// Object of type cCountry, which hold definition of country details
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>Country Successfully updated</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: country cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int EditCountry(ref cCountry country)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "sp_UpdateCountryDetails";
                dbCommand.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                dbCommand.Parameters.AddWithValue("@CountryName", country.CountryName);
                dbCommand.Parameters.AddWithValue("@CurrencyName", country.CurrencyName);
                dbCommand.Parameters.AddWithValue("@CurrencySymbol", country.CurrencySymbol);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
        /// <summary>
        /// Function to Edit state Details.
        /// </summary>
        /// <param name="state">
        /// Object of type cState, which hold definition of state details
        /// </param>
        /// <returns>
        /// <list type="bullet|number|table">
        /// <listheader>
        /// <term>Return Code</term>
        /// <description>description</description>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <description>State Successfully updated</description>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <description>StateCode Not found</description>
        /// </item>
        /// <item>
        /// <term>-1</term>
        /// <description>Error: state cannot be added due to code execution error</description>
        /// </item> 
        /// </list>
        /// </returns> 
        public static int EditStateName(ref cState state)
        {
            int iRetValue = 0;
            SqlConnection dbConnection = new SqlConnection();
            try
            {
                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
                dbConnection.Open();
                SqlCommand dbCommand = new SqlCommand();
                dbCommand.Connection = dbConnection;
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = "update CETStates SET StateName = @StateName where StateCode = @StateCode";
                dbCommand.Parameters.AddWithValue("@StateName", state.StateName);
                dbCommand.Parameters.AddWithValue("@StateCode", state.StateCode);
                dbCommand.ExecuteNonQuery();
                iRetValue = 1;
            }
            catch (Exception ex)
            {
                iRetValue = -1;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.LOCATION_MANAGEMENT, "", Message);
            }
            finally
            {
                dbConnection.Close();
            }
            return iRetValue;
        }
    }
}