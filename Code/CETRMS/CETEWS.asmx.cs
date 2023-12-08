//#define SECURE
//#define SYSLOG


using Newtonsoft.Json;
using SoapBasedTokenAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace CETRMS
{
    /// <summary>
    /// Summary description for UEEWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CETEWS : System.Web.Services.WebService
    {
        public SecuredTokenWebService SoapHeader;
        public string Key = "fee6e21eb76c5f6c0d451f238d3c975b";

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UserAuthentication()
        {
            if (SoapHeader == null)
                return "Please provide username and password";
            if (string.IsNullOrEmpty(SoapHeader.UserName) || string.IsNullOrEmpty(SoapHeader.Password))
                return "Please provide username and password";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader.UserName, SoapHeader.Password))
                return "Invalid App Secret";

            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(
                token,
                SoapHeader.UserName,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(30),
                System.Web.Caching.CacheItemPriority.NotRemovable,
                null

                );
            return token;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string ValidateAPIKey()
        {
            if (SoapHeader == null)
                return "Please provide APIKey";
            if (string.IsNullOrEmpty(SoapHeader.APIKey))
                return "Please provide APIKey";
            if (!SoapHeader.IsValidEmployerAPIKey(SoapHeader.APIKey))
                return "Invalid App Key";
            
            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(
                token,
                SoapHeader.UserName,
                null,
                System.Web.Caching.Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(30),
                System.Web.Caching.CacheItemPriority.NotRemovable,
                null

                );
            return token;
        }


        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string HelloWorld()
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            return "Hello World";

        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string AuthenticateEmployer(string LoginID, string Password)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>AuthenticateEmployer()");
#endif

#if SECURE
            //if (SoapHeader == null)
            //    return "Please call UserAuthentication() or ValidateAPIKey() first";
            //if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
            //    return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif


            string sRetValue = "";
             //if (SoapHeader.IsValidEmployerAPIKey(Key))
             //{ 
                switch (EmployerManagement.AuthenticateEmployer(LoginID, Password))
                {
                    case 1:
                        sRetValue = "Authorized";
                        break;
                    case 0:
                        sRetValue = "Incorrect User-id";
                        break;
                    case -2:
                        sRetValue = "Incorrect Password";
                        break;
                    case -1:
                        sRetValue = "Cannot not validate due to technical error.";
                        break;
                }
             //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<AuthenticateEmployer(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetClientId(string AuthenticatorID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetClientId()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                string UEClientId = string.Empty;
                int RetValue = EmployerManagement.GetUEClientIdByAuthenticatorId(AuthenticatorID, ref UEClientId);
                switch (RetValue)
                {
                    case 1:
                        sRetValue = UEClientId;
                        break;
                    case 0:
                        sRetValue = "AuthenticatorID not registered";
                        break;
                    case -1:
                        sRetValue = "Error: Client Id cannot be fetched.";
                        break;
                }
          //  }

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetClientId(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetEmployerDetails(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetEmployerDetails()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Employer EmployerDetails = new Employer();
                int RetValue;
                RetValue = EmployerManagement.GetEmployerByID(EmployerID, ref EmployerDetails, true);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(EmployerDetails);
                        sRetValue = json;
                        break;
                    case 0:
                        sRetValue = "Incorrect ClientID";
                        break;
                    case -1:
                        sRetValue = "Error: Employer details cannot be fetched.";
                        break;
                }
            //}

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetEmployerDetails(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateEmployerDetails(string EmployerDetailsJSON)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>UpdateEmployerDetails()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Employer EmployerDetails = JsonConvert.DeserializeObject<Employer>(EmployerDetailsJSON);
                int RetValue;
                RetValue = EmployerManagement.UpdateEmployer(EmployerDetails);
                switch (RetValue)
                {
                    case 1:
                        sRetValue = "Employer Details updated successfully";
                        break;
                    case 0:
                        sRetValue = "Incorrect ClientID";
                        break;
                    case -1:
                        sRetValue = "Error: Employer details cannot be fetched.";
                        break;
                }
           // }

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<UpdateEmployerDetails(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string AddVacancy(string VacancyDetailsJSON)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>AddVacancy()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Vacancy VacancyDetail = JsonConvert.DeserializeObject<Vacancy>(VacancyDetailsJSON);
                int RetValue;
                RetValue = VacancyManager.AddVacancy(VacancyDetail);
                switch (RetValue)
                {
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy cannot be added.";
                        break;
                    default:
                        sRetValue = "Vacancy added successfully";
                        break;
                }
            //}

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<AddVacancy(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetVacancyDetails(string VacancyID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetVacancyDetails()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Vacancy VacancyDetail = new Vacancy();
                int RetValue;
                RetValue = VacancyManager.GetVacancyDetails(VacancyID, ref VacancyDetail);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyDetail);
                        sRetValue = json;
                        break;
                    case 0:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
           // }

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetVacancyDetails(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetAllVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetAllVacancyListByEmployer()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
           // }

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetAllVacancyListByEmployer(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetOpenVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetOpenVacancyListByEmployer()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList, cVacancyStatus.Open);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
            //}

            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetOpenVacancyListByEmployer(" + sRetValue + ")");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetClosedVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetClosedVacancyListByEmployer()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList, cVacancyStatus.Close);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetClosedVacancyListByEmployer()");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetPausedVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetPausedVacancyListByEmployer()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList, cVacancyStatus.Pause);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetPausedVacancyListByEmployer()");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetFilledVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetFilledVacancyListByEmployer()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList, cVacancyStatus.Filled);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetFilledVacancyListByEmployer()");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetInProcessVacancyListByEmployer(string EmployerID)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetInProcessVacancyListByEmployer()");
#endif

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Vacancy> VacancyList = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByEmployer(EmployerID, ref VacancyList, cVacancyStatus.InProcess);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(VacancyList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy details cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetInProcessVacancyListByEmployer()");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateVacancy(string VacancyDetailsJSON)
        {
#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>UpdateVacancy()");
#endif
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Vacancy VacancyDetail = JsonConvert.DeserializeObject<Vacancy>(VacancyDetailsJSON);
                int RetValue;
                RetValue = VacancyManager.UpdateVacancy(VacancyDetail);
                switch (RetValue)
                {
                    case 1:
                        sRetValue = "Vacancy updated successfully";
                        break;
                    case 0:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy cannot be updated.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

#if SYSLOG
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<UpdateVacancy()");
#endif
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateVacancyStatus(string VacancyID, string sVacancyStatus)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                int RetValue;
                RetValue = VacancyManager.UpdateVacancyStatus(VacancyID, Convert.ToInt32(sVacancyStatus));
                switch (RetValue)
                {
                    case 1:
                        sRetValue = "Vacancy status updated successfully";
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy status cannot be updated.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetJobApplicationDetails(string JobApplicationID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                int RetValue;
                JobApplication JobApplicationDetails = new JobApplication();
                RetValue = JobApplicationManager.GetJobApplicationDetails(JobApplicationID, ref JobApplicationDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Job Application ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Application status cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetNewJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.NewApplication);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetViewedJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.ApplicationViewed);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetInterviewScheduledJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.InterviewScheduled);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetSelectedJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.Selected);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetHiredJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.Hired);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetRejectedJobApplicationListByVacancy(string VacancyID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByVacancy(VacancyID, ref JobApplicationList, JobApplicationStatus.Rejected);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetNewJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.NewApplication);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetViewedJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.ApplicationViewed);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetInterviewScheduledJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.InterviewScheduled);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetSelectedJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.Selected);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetHiredJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.Hired);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
              }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetRejectedJobApplicationListByEmployer(string EmployerID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<JobApplication> JobApplicationList = new List<JobApplication>();
                int RetValue;
                RetValue = JobApplicationManager.GetJobApplicationListByEmployer(EmployerID, ref JobApplicationList, JobApplicationStatus.Rejected);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(JobApplicationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Employer ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Job Applications List cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateFullDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate CandidateDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidateFullDetails(CandidateID, ref CandidateDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(CandidateDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidatePersonalDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidatePersonalDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateContactDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidateContactlDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidatePassportDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidatePassportDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateVisaDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidateVisaDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateQualificationDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidateQualificationDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateOtherDetails(string CandidateID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Candidate PersonalDetails = new Candidate();
                int RetValue;
                RetValue = CandidateManagement.GetCandidateOtherDetails(CandidateID, ref PersonalDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(PersonalDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}

            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string ProposeNewInterview(string InterviewDetailsJSON)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Interview InterviewDetails = JsonConvert.DeserializeObject<Interview>(InterviewDetailsJSON);
                int RetValue;
                RetValue = InterviewManagement.ProposeNewInterview(InterviewDetails);
                switch (RetValue)
                {
                    case 1:
                        sRetValue = "Interview proposed successfully.";
                        break;
                    case -2:
                        sRetValue = "CandidateID ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Candidate cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetInterviewDetails(string InterviewID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                Interview InterviewDetails = new Interview();
                int RetValue;
                RetValue = InterviewManagement.GetInterviewDetails(InterviewID, ref InterviewDetails);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(InterviewDetails);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Interview ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Interview Details cannot be fetched.";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetNotificationSummary(string UEClientID)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                int RetValue;
                RetValue = NotificationManagement.GetNotificationStatistics(ref sRetValue, UEClientID);
                switch (RetValue)
                {
                    case 1:
                        break;
                    case -2:
                        sRetValue = "UE Client ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Can not fetch notification statistics";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetAllNotificaitonList(string UEClientID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Notification> notificationList = new List<Notification>();
                int RetValue;
                RetValue = NotificationManagement.GetNotificationList(ref notificationList, UEClientID, cNotificationType.AllNotifications, cNotificationStatus.AllNotifications);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(notificationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "UE Client ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Can not fetch notification statistics";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetUnreadNotificaitonList(string UEClientID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //if (SoapHeader != null && SoapHeader.IsValidEmployerAPIKey(Key))
            //{
                List<Notification> notificationList = new List<Notification>();
                int RetValue;
                RetValue = NotificationManagement.GetNotificationList(ref notificationList, UEClientID, cNotificationType.AllNotifications, cNotificationStatus.NotificationUnread);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(notificationList);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "UE Client ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Can not fetch notification statistics";
                        break;
                }
            //}
            //else
            //{
            //    sRetValue = "Invalid Key";
            //}
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string MobileAppDashboard(string EmployerID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>MobileAppDashboard()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string DashboardData = "";
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = EmployerManagement.GetEmployerMobileDashboardData(EmployerID, ref DashboardData);
            switch (RetValue)
            {
                case 1:
                    sRetValue = DashboardData;
                    break;
                case -2:
                    sRetValue = "Employer ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Employer dashboard details cannot be fetched.";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<MobileAppDashboard()");
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string MarkNotificationRead(string NotificationID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>MarkNotificationRead()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = NotificationManagement.MarkNotificationRead(NotificationID);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Notification status updated successfully";
                    break;
                case -2:
                    sRetValue = "Notification ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Notification status cannot be updated";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<MarkNotificationRead()");
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateMaskedDetails(string JobApplicationID)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            MaskedCandidate maskedCandidate = new MaskedCandidate();
            int RetValue;
            RetValue = CandidateManagement.GetCandidateMaskedDetails(JobApplicationID, ref maskedCandidate);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(maskedCandidate);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "JobApplicationID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Candidate cannot be fetched.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateMaskedList(string VacancyID, string CandidateStatus)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            List<MaskedCandidate> maskedCandidates = new List<MaskedCandidate>();
            int RetValue;
            RetValue = CandidateManagement.GetCandidateMaskedList(VacancyID, ref maskedCandidates, Convert.ToInt32(CandidateStatus));
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(maskedCandidates);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Vacancy ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Candidate List cannot be fetched.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCandidateMaskedListByEmployer(string EmployerID, string CandidateStatus)
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            List<MaskedCandidate> maskedCandidates = new List<MaskedCandidate>();
            int RetValue;
            RetValue = CandidateManagement.GetCandidateMaskedListByEmployer(EmployerID, ref maskedCandidates, Convert.ToInt32(CandidateStatus));
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(maskedCandidates);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Employer ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Candidate List cannot be fetched.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetCountryList()
        {

#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            //List<cCountry> countries = new List<cCountry>();
            int RetValue;
            //RetValue = LocationManagement.GetCountryList(ref countries);
            RetValue = LocationManagement.GetCountryList2(ref sRetValue);
            switch (RetValue)
            {
                case 1:
                    break;
                case -2:
                    sRetValue = "Country list cannot be fetched";
                    break;
                case -1:
                    sRetValue = "Error: country list cannot be fetched";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetStateList(string CountryCode)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            //List<cState> states = new List<cState>();
            int RetValue;
            //RetValue = LocationManagement.GetStateList(CountryCode, ref states);
            RetValue = LocationManagement.GetStateList2(CountryCode, ref sRetValue);
            switch (RetValue)
            {
                case 1:
                    //var json = new JavaScriptSerializer().Serialize(states);
                    //sRetValue = json;
                    break;
                case -2:
                    sRetValue = "state list cannot be fetched";
                    break;
                case -1:
                    sRetValue = "Error: state list cannot be fetched";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetMaskedInterviewListByEmployerId(string EmployerID, int InterviewStatus)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            List<cMaskedInterviewDetails> maskedInterviewLists = new List<cMaskedInterviewDetails>();
            int RetValue;
            RetValue = EmployerManagement.GetMaskedInterviewListByEmployerId(EmployerID, ref maskedInterviewLists, InterviewStatus);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(maskedInterviewLists);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Interview list cannot be fetched";
                    break;
                case -1:
                    sRetValue = "Error: Interview list cannot be fetched";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetMaskedInterviewDetails(string InreviewID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            cMaskedInterviewDetails MaskedInterviewDetails = new cMaskedInterviewDetails();
            int RetValue;
            RetValue = InterviewManagement.GetMaskedInterviewDetails(InreviewID, ref MaskedInterviewDetails);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(MaskedInterviewDetails);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Interview list cannot be fetched";
                    break;
                case -1:
                    sRetValue = "Error: Interview list cannot be fetched";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetDueRegistrationPaymentDetails(string EmployerID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            List<Payment> payments = new List<Payment>();
            int RetValue;
            RetValue = PaymentManagement.GetPaymentList(ref payments, EmployerID, cPaymentStatus.AllDuePayments, cPaymentType.EmployerRegistrationFee);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(payments);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Employer ID is incorrect payment list cannot be fetched";
                    break;
                case -1:
                    sRetValue = "Error: payment list cannot be fetched";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string AddPaymentLog(string PaymentLogJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            PaymentLog paymentLog = JsonConvert.DeserializeObject<PaymentLog>(PaymentLogJSON);
            int RetValue;
            RetValue = PaymentManagement.AddPaymentLog(paymentLog);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Payment Log added successfully.";
                    break;
                case -2:
                    sRetValue = "Payment ID is incorrect payment log cannot be added";
                    break;
                case -1:
                    sRetValue = "Error: payment log cannot be added";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdatePaymentStatus(string PaymentDetailsJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            Payment payment = JsonConvert.DeserializeObject<Payment>(PaymentDetailsJSON);
            int RetValue;
            RetValue = PaymentManagement.UpdatePaymentStatus(payment);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Payment status added successfully.";
                    break;
                case -2:
                    sRetValue = "Payment ID is incorrect payment status cannot be updated";
                    break;
                case -1:
                    sRetValue = "Error: payment status cannot be updated";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetJobApplicationStatistics(string VacancyID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetJobApplicationStatistics()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string StatisticsData = "";
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = VacancyManager.GetJobApplicationStatistics(VacancyID, ref StatisticsData);
            switch (RetValue)
            {
                case 1:
                    sRetValue = StatisticsData;
                    break;
                case -2:
                    sRetValue = "Vacancy ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: job applications statistics cannot be fetched.";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetJobApplicationStatistics()");
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetCurrencySymbol(string CountryCode)
        {
            string CurrencySymbol = string.Empty;
            string CurrencyName = string.Empty;
            string sRetValue = "";
            int RetValue;
            RetValue = LocationManagement.GetCurrencySymbol(CountryCode, ref CurrencySymbol, ref CurrencyName);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(CurrencySymbol);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "CountryCode does not match";
                    break;
                case -1:
                    sRetValue = "Error cannot generate currency symbol";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string SendWhatsappMessege(string Phoneno, string Messege)
        {
            
            string sRetValue = "";
            int RetValue;
            RetValue = WhatsAppManagement.SendMessage(Phoneno, Messege);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(Messege);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "MESSEGE DOES NOT RECEIVED";
                    break;
                case -1:
                    sRetValue = "Error";
                    break;
            }
            return sRetValue;
        }

        [WebMethod]
        [SoapHeader("SoapHeader")]

        public string GetCityList(string StateCode, string CityName)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>GetCityList()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = LocationManagement.GetMatchingCityList2(StateCode, CityName, ref sRetValue);
            switch (RetValue)
            {
                case 1:
                    break;
                case -2:
                    sRetValue = "State code does not match";
                    break;
                case -1:
                    sRetValue = "Error: city list  cannot be update.";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetCityList()");
            return sRetValue;
        }

        [WebMethod]
        [SoapHeader("SoapHeader")]

        public string AddCityName(string StateCode, string CityName)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", ">>>AddCityName()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = LocationManagement.AddCityName(StateCode, CityName);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "City added successfully";
                    break;
                case -2:
                    sRetValue = "State code does not match";
                    break;
                case -1:
                    sRetValue = "Error: city cannot be added.";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<AddCityName()");
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetPaymentLog(string UEClientID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            List<PaymentLog> paymentlog = new List<PaymentLog>();
            int RetValue;
            RetValue = PaymentManagement.GetPaymentLogByClientId(UEClientID, ref paymentlog);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(paymentlog);
                    sRetValue = json.Replace("},{",",").Replace("[","").Replace("]","");
                    break;
                case -2:
                    sRetValue = "UEClientId does not matched";
                    break;
                case -1:
                    sRetValue = "Error: Payment log Cannot be fetched";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "<<<GetPaymentLog()");
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string UpdateJobApplicationStatus(string JobApplicationID, int JobAplicationStatus)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            int RetValue;
            try
            {
                RetValue = JobApplicationManager.UpdateJobApplicationStatus(JobApplicationID, JobAplicationStatus);
                switch (RetValue)
                {
                    case 1:
                        sRetValue = "Job Application Status Changed Successfully";
                        break;
                    case -2:
                        sRetValue = "JobApplication Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Job Application Status Cannot be changed";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "UpdateJobApplicationStatus::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetPaymentLogInfoList(string PaymentRecId)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            int RetValue;
            string sRetValue = string.Empty;
            try
            {
                List<PaymentLogInfo> paymentlog = new List<PaymentLogInfo>();
                RetValue = PaymentManagement.GetPaymentLogInfoList(PaymentRecId, ref paymentlog);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(paymentlog);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "PaymentRecId Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Payment log list Cannot be fetched";
                        break;
                }
            }
            catch(Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetPaymentLogInfoList::"+ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetPaymentLogInfo(string PaymentLogId)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            int RetValue;
            string sRetValue = string.Empty;
            try
            {
                PaymentLogInfo paymentlog = new PaymentLogInfo();
                RetValue = PaymentManagement.GetPaymentLogInfo(PaymentLogId, ref paymentlog);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(paymentlog);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "PaymentRecId Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Payment log list Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetPaymentLogInfo::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetPaymentList(string UEClientID, int PaymentStatus, int PaymentType)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            int iRetValue;
            string sRetValue = string.Empty;
            try
            {
                List<Payment> payments = new List<Payment>();
                iRetValue = PaymentManagement.GetPaymentList(ref payments, UEClientID, PaymentStatus, PaymentType, true);
                switch (iRetValue)
                {
                    case RetValue.Success:
                        var json = new JavaScriptSerializer().Serialize(payments);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Client Id does not matched";
                        break;
                    case RetValue.Error:
                        sRetValue = "Error: Payment list Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetPaymentList::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetPaymentDetails(string PaymentID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            int iRetValue;
            string sRetValue = string.Empty;
            try
            {
                Payment payment = new Payment();
                iRetValue = PaymentManagement.GetPaymentDetails(PaymentID, ref payment, false, true);
                switch (iRetValue)
                {
                    case RetValue.Success:
                        var json = new JavaScriptSerializer().Serialize(payment);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Client Id does not matched";
                        break;
                    case RetValue.Error:
                        sRetValue = "Error: Payment details Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetPaymentDetails::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string SendVerificationEmail(string EmployerID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            try
            {
                sRetValue = Email.SendEmployerVerifyEmail(EmployerID);
                switch (sRetValue)
                {
                    case "Email sent successfully.":
                        sRetValue = "Email sent successfully.";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "SendVerificationEmail::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GenerateOfferLetter(string JobApplicationID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = string.Empty;
            int iRetValue = RetValue.NoRecord;
            try
            {
                iRetValue = JobApplicationManager.GenerateOfferLetter(JobApplicationID);
                switch (iRetValue)
                {
                    case RetValue.Success:
                        break;
                    case RetValue.NoRecord:
                        sRetValue = "Job Application ID could not match.";
                        break;
                    case RetValue.Error:
                        sRetValue = "Error: Job offer letter could not generated.";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GenerateOfferLetter::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetCityDetails(string StateCode, string CityCode)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            cCity city = new cCity();
            string sRetValue = string.Empty;
            try
            {
                city = LocationManagement.GetCityDetail(StateCode, CityCode);
                var json = new JavaScriptSerializer().Serialize(city);
                sRetValue = json;

            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GenerateOfferLetter::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetVisaTypeList(string CountryName)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            int RetValue;
            string sRetValue = string.Empty;
            try
            {
                List<VisaType> visaTypes = new List<VisaType>();
                RetValue = RMSMasterManagement.GetVisaTypeList(ref visaTypes, CountryName);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(visaTypes);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Visa Type Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Visa Type list Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetVisaTypeList::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string SendErrorReport(int LSeverity, string Message)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            bool RetValue = false;
            string sRetValue = string.Empty;
            try
            {
                List<VisaType> visaTypes = new List<VisaType>();
                switch(LSeverity)
                {
                    case 0:
                        RetValue = logger.log(logger.LogSeverity.DBG, logger.LogEvents.EMPLOYER_MOBILEAPP, "", Message);
                        break;
                    case 1:
                        RetValue = logger.log(logger.LogSeverity.INF, logger.LogEvents.EMPLOYER_MOBILEAPP, "", Message);
                        break;
                    case 2:
                        RetValue = logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_MOBILEAPP, "", Message);
                        break;
                }

                if (RetValue)
                    sRetValue = "Log record submitted";
                else
                    sRetValue = "Loc record cannot be submitted";
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetVisaTypeList::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
    }
}
