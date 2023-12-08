using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SoapBasedTokenAuthentication
{
    public class SecuredTokenWebService : System.Web.Services.Protocols.SoapHeader
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
        public string AuthenticationToken { get; set; }

        public bool IsEmployerAppUserCredentialsValid(string UserName, String Password)
        {
            if (UserName == ConfigurationManager.AppSettings["EmployerAPIUUID"] && Password == ConfigurationManager.AppSettings["EmployerAPIPassword"])
                return true;
            else
                return false;
        }
        public bool IsCandidateAppUserCredentialsValid(string UserName, String Password)
        {
            if (UserName == ConfigurationManager.AppSettings["CandidateAPIUUID"] && Password == ConfigurationManager.AppSettings["CandidateAPIPassword"])
                return true;
            else
                return false;
        }

        public bool IsValidEmployerAPIKey(string APIKey)
        {
            bool bRetValue = false;
            if (APIKey == ConfigurationManager.AppSettings["_EmployerAPIKey"])
                bRetValue = true;
            return bRetValue;
        }
        public bool IsValidCandidateAPIKey(string APIKey)
        {
            bool bRetValue = false;
            if (APIKey == ConfigurationManager.AppSettings["_CandidateAPIKey"])
                bRetValue = true;
            return bRetValue;
        }
        public bool IsEmployerAppUserCredentialsValid(SecuredTokenWebService SoapHeader)
        {
            if (SoapHeader == null)
                return false;
            
            // checks the token exist in cache
            if (!string.IsNullOrEmpty(SoapHeader.AuthenticationToken))
                return (HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

            return false;
        }
        public bool IsCandidateAppUserCredentialsValid(SecuredTokenWebService SoapHeader)
        {
            if (SoapHeader == null)
                return false;

            // checks the token exist in cache
            if (!string.IsNullOrEmpty(SoapHeader.AuthenticationToken))
                return (HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

            return false;
        }
    }
}