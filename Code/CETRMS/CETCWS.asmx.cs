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
    /// Summary description for UECWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CETCWS : System.Web.Services.WebService
    {

        public SecuredTokenWebService SoapHeader;

        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UserAuthentication()
        {
            if (SoapHeader == null)
                return "Please provide username and password";
            if (string.IsNullOrEmpty(SoapHeader.UserName) || string.IsNullOrEmpty(SoapHeader.Password))
                return "Please provide username and password";
            if (!SoapHeader.IsCandidateAppUserCredentialsValid(SoapHeader.UserName, SoapHeader.Password))
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
            if (!SoapHeader.IsValidCandidateAPIKey(SoapHeader.APIKey))
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
        public string AuthenticateCandidate(string LoginID, string Password)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            switch (CandidateManagement.AuthenticateCandidate(LoginID, Password))
            {
                case 0:
                    sRetValue = "Incorrect User-id";
                    break;
                case -2:
                    sRetValue = "Incorrect Password";
                    break;
                case -1:
                    sRetValue = "Cannot not validate due to technical error.";
                    break;
                default:
                    sRetValue = "Authorized";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetClientId(string AuthenticatorID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            string UEClientId = string.Empty;
            int RetValue = CandidateManagement.GetUEClientIdByAuthenticatorId(AuthenticatorID, ref UEClientId);
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
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string SendJobApplication(string JobApplicationJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            JobApplication JobApplicationDetails = JsonConvert.DeserializeObject<JobApplication>(JobApplicationJSON);
            int RetValue;
            RetValue = CandidateManagement.SendJobApplication(JobApplicationDetails);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Job Application inserted successfully";
                    break;
                case 0:
                    sRetValue = "Job Application cannot be validated";
                    break;
                case -1:
                    sRetValue = "Error: Job Application cannot be inserted.";
                    break;
            }
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
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetVacancyDetails(string VacancyID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Vacancy VacancyDetail = new Vacancy();
            int RetValue;
            RetValue = VacancyManager.GetVacancyDetails(VacancyID, ref VacancyDetail);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(VacancyDetail);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "Vacancy ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Vacancy details cannot be fetched.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdatePersonalProfile(string CandidateID, string PersonalProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Personal PersonalProfile = JsonConvert.DeserializeObject<Candidate.Personal>(PersonalProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidatePersonalProfile(CandidateID, PersonalProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Personal Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Personal Profile details cannot be updated.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateContactProfile(string CandidateID, string ContactProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Contact ContactProfile = JsonConvert.DeserializeObject<Candidate.Contact>(ContactProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidateContactDetails(CandidateID, ContactProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Contact Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Contact Profile details cannot be updated.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateBankAccountDetails(string CandidateID, string BankAccountDetailsJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.BankAccount BankAccount = JsonConvert.DeserializeObject<Candidate.BankAccount>(BankAccountDetailsJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateBankAccountDetails(CandidateID, BankAccount);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Bank Account Details Successfully Updated.";
                    break;
                case -2:
                    sRetValue = "Candidate Id does not match";
                    break;
                case -1:
                    sRetValue = "Bank Account Details cannot be updated.";
                    break;
            }
            return sRetValue;
        }


        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdatePassportProfile(string CandidateID, string PassportProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Passport PassportProfile = JsonConvert.DeserializeObject<Candidate.Passport>(PassportProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidatePassportDetails(CandidateID, PassportProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Passport Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Passport Profile details cannot be updated.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateVisaProfile(string CandidateID, string VisaProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Visa VisaProfile = JsonConvert.DeserializeObject<Candidate.Visa>(VisaProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidateVisaDetails(CandidateID, VisaProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Visa Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Visa Profile details cannot be updated.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateQualificationProfile(string CandidateID, string QualificationProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Qualification QualificationProfile = JsonConvert.DeserializeObject<Candidate.Qualification>(QualificationProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidateQualificationDetails(CandidateID, QualificationProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Qualification Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Qualification Profile details cannot be updated.";
                    break;
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string UpdateOtherProfile(string CandidateID, string OtherProfileJSON)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string sRetValue = "";
            Candidate.Other OtherProfile = JsonConvert.DeserializeObject<Candidate.Other>(OtherProfileJSON);
            int RetValue;
            RetValue = CandidateManagement.UpdateCandidateOtherDetails(CandidateID, OtherProfile);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Other Profile details updated.";
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Other Profile details cannot be updated.";
                    break;
            }
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
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetCandidateReferralLink(string CandidateId)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string ReferralLink = string.Empty;
            string sRetValue = "";
            int RetValue;
            RetValue = ReferralManagement.GetCandidateReferralCode(CandidateId, ref ReferralLink);
            // RCode.Text = RefferalCode.Substring(ReferralCode.IndexOf('=') + 1);
            switch (RetValue)
            {
                case 1:
                    var json = new JavaScriptSerializer().Serialize(ReferralLink);
                    sRetValue = json;
                    break;
                case -2:
                    sRetValue = "UE CandidateId does not match";
                    break;
                case -1:
                    sRetValue = "Error cannot generate Referrel Link";
                    break;
            }
            return sRetValue;

        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string AddTestimonial(string TestimonialJSON)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            int RetValue;
            Testimonial testimonial = JsonConvert.DeserializeObject<Testimonial>(TestimonialJSON);
            RetValue = TestimonialManager.AddNewTestimonial(testimonial);
            switch (RetValue)
            {
                case 1:
                    sRetValue = "Testimonial Submitted succesfully";
                    break;
                case -2:
                    sRetValue = "UE client ID does not match";
                    break;
                case -1:
                    sRetValue = "Error cannot add testimonial";
                    break;
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
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.EMPLOYER_WEBSERVICE, "", "GetPaymentLogInfoList::" + ex.Message);
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
                    sRetValue = json.Replace("},{", ",").Replace("[", "").Replace("]", "");
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
        public string GetCurrencySymbol(string CountryCode)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
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
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetDueRegistrationPaymentDetails(string CandidateID)
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
            RetValue = PaymentManagement.GetPaymentList(ref payments, CandidateID, cPaymentStatus.AllDuePayments, cPaymentType.CandidateRegistrationFee);
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
        public string GetMaskedInterviewDetails(string InreviewID)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
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
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewDetails::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetMaskedInterviewListByCandidateId(string CandidateId, int InterviewStatus)
        {
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
                List<cMaskedInterviewDetails> maskedInterviewLists = new List<cMaskedInterviewDetails>();
                int RetValue;
                RetValue = CandidateManagement.GetMaskedInterviewListByCandidateId(CandidateId, ref maskedInterviewLists, InterviewStatus);
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
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetMaskedInterviewListByCandidateId::" + ex.Message);
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
            int RetValue;
            string sRetValue = string.Empty;
            try
            {
                List<Payment> payments = new List<Payment>();
                RetValue = PaymentManagement.GetPaymentList(ref payments, UEClientID, PaymentStatus, PaymentType);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(payments);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Client Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Payment list Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetPaymentList::" + ex.Message);
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
            int RetValue;
            string sRetValue = string.Empty;
            try
            {
                Payment payment = new Payment();
                RetValue = PaymentManagement.GetPaymentDetails(PaymentID, ref payment);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(payment);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Client Id does not matched";
                        break;
                    case -1:
                        sRetValue = "Error: Payment details Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetPaymentDetails::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }



        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string MobileAppDashboard(string CandidateID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>MobileAppDashboard()");
#if SECURE            
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif

            string DashboardData = "";
            string sRetValue = string.Empty;
            int RetValue;
            RetValue = CandidateManagement.GetCandidateMobileDashboardData(CandidateID, ref DashboardData);
            switch (RetValue)
            {
                case 1:
                    sRetValue = DashboardData;
                    break;
                case -2:
                    sRetValue = "Candidate ID does not match";
                    break;
                case -1:
                    sRetValue = "Error: Candidate dashboard details cannot be fetched.";
                    break;
            }
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<MobileAppDashboard()");
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string SendVerificationEmail(string CandidateID)
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
                sRetValue = Email.SendCandidateVerifyEmail(CandidateID);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "SendVerificationEmail::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetVacancyListByLocation(string Location)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
                List<Vacancy> vacancies = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetOpenVacancyListByLocation(Location, ref vacancies, cVacancyStatus.Open);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(vacancies);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Location ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy list cannot be fetched.";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetVacancyListByLocation::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetVacancyListByStatus(string VacancyStatus)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
                List<Vacancy> vacancies = new List<Vacancy>();
                int RetValue;
                RetValue = VacancyManager.GetVacancyListByLocation("all", ref vacancies, Convert.ToInt32(VacancyStatus));
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(vacancies);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Vacancy status does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Vacancy list cannot be fetched.";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetVacancyListByStatus::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetJobApplicationList(string CandidateID)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
                int RetValue;
                List<JobApplication> jobApplications = new List<JobApplication>();
                RetValue = JobApplicationManager.GetJobApplicationListByCandidate(CandidateID, ref jobApplications);
                switch (RetValue)
                {
                    case 1:
                        var json = new JavaScriptSerializer().Serialize(jobApplications);
                        sRetValue = json;
                        break;
                    case -2:
                        sRetValue = "Candidate ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Application list cannot be fetched.";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetJobApplicationList::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string GetLocationList(string VacancyStatus)
        {
#if SECURE
            if (SoapHeader == null)
                return "Please call UserAuthentication() or ValidateAPIKey() first";
            if (!SoapHeader.IsEmployerAppUserCredentialsValid(SoapHeader))
                return "Please call UserAuthentication() or ValidateAPIKey() first";
#endif
            string sRetValue = "";
            try
            {
                int RetValue;
                List<JobApplication> jobApplications = new List<JobApplication>();
                RetValue = VacancyManager.GetLocationList(VacancyStatus, ref sRetValue);
                switch (RetValue)
                {
                    case 1:
                        break;
                    case -2:
                        sRetValue = "Vacancy Status ID does not match";
                        break;
                    case -1:
                        sRetValue = "Error: Location list cannot be fetched.";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetLocationList::" + ex.Message);
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
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetVisaTypeList::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [SoapHeader("SoapHeader")]
        public string GetVisaTypeDetails(string VisaTypeID)
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
                VisaType visaTypes = new VisaType();
                RetValue = RMSMasterManagement.GetVisaTypeDetails(VisaTypeID, ref visaTypes);
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
                        sRetValue = "Error: Visa Type details Cannot be fetched";
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANIDATE_WEBSERVICE, "", "GetVisaTypeDetails::" + ex.Message);
                sRetValue = "Passed parameter cannot be parsed.";
            }
            return sRetValue;
        }
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("SoapHeader")]
        public string MarkNotificationRead(string NotificationID)
        {
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", ">>>MarkNotificationRead()");
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
            logger.log(logger.LogSeverity.DBG, logger.LogEvents.CANIDATE_WEBSERVICE, "", "<<<MarkNotificationRead()");
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
                switch (LSeverity)
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
