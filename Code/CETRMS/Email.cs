//#define TESTING
#define GODADDY
using System;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Net;       //Add 

namespace CETRMS
{
    public class GenerateEmail : System.Web.UI.Page
    {
        static private Interview interview = new Interview();
        static private Employer employer = new Employer();
        static private Candidate candidate = new Candidate();
        static private Vacancy vacancy = new Vacancy();
        static private JobApplication jobApplication = new JobApplication();
        static private Payment payment = new Payment();
        static private CompanyProfile companyProfile = new CompanyProfile();


        //static private string EmailSubject;
        static private string TemplateEmailBody;
        static private string EmailBody;

        //static private string SupportEmailAddress;
        //static private string SupportContactNumber;


        //static private string CandidateName;
        public string CandidateEmail { get; set; }
        public string EmployerEmail { get; set; }
        //static private string CandidateRCode;


        public string NoReplyEmail { get; set; }
        public string SupportEmail { get; set; }
        public string NewLetterEmail { get; set; }
        public string AccountsEmail { get; set; }
        public string InfoEmail { get; set; }

        public string CustomerEmail { get; set; }

        public string SupportDashboardEmailReceipients;

        private static string StartUrlLink { get; set; }
        private static string HostUrlLink { get; set; }
        public string NewCandidateRegistrationLink { get; set; }

        public string ProfileName { get; set; }
        public string VerifyEmailLink { get; set; }
        public string ServiceTypeName { get; set; }
        public string BillingTo { get; set; }


        public bool ReadMailBody(string TemplateFileName)
        {
            bool bRetValue = false;
            try
            {
                string filepath = Server.MapPath(TemplateFileName);
                StringBuilder sb = new StringBuilder();
                foreach (string line in System.IO.File.ReadLines(filepath))
                {
                    sb.Append(line);
                }

                TemplateEmailBody = sb.ToString();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            return bRetValue;
        }

        public bool GetNotificationDetails(string _InterviewID)
        {
            bool bRetValue = false;
            try
            {
                if (InterviewManagement.GetInterviewDetails(_InterviewID, ref interview) == 1)
                {
                    if (JobApplicationManager.GetJobApplicationDetails(interview.JobApplicationID, ref jobApplication) == 1)
                    {
                        if (VacancyManager.GetVacancyDetails(jobApplication.VacancyID, ref vacancy) == 1)
                        {
                            if (EmployerManagement.GetEmployerByID(vacancy.UEEmployerID, ref employer) == 1)
                            {
                                EmployerEmail = employer.email;
                                if (CandidateManagement.GetCandidatePersonalDetails(jobApplication.CandidateID, ref candidate) == 1)
                                {
                                    CandidateEmail = candidate.PersonalProfile.CandidateEmail;
                                    bRetValue = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            return bRetValue;
        }
        public bool GetProfileDetail(string _UEClientID, int ClientType)
        {
            bool bRetValue = false;
            try
            {
                if (ClientType == cClientType.Employer)
                {
                    if (EmployerManagement.GetEmployerByID(_UEClientID, ref employer) == 1)
                    {
                        ProfileName = employer.BusinessName;
                        EmployerEmail = employer.email;
                        VerifyEmailLink = ConfigurationManager.AppSettings["AppURL"] + @"VerifyEmployerEmail.aspx?code=" + EncDec.Encrypt(System.DateTime.Now.ToString("ddMMyyyy") + "-" + employer.EmployerID, ConfigurationManager.AppSettings["EncryptionPassword"]);
                    }
                }
                else
                {
                    if (CandidateManagement.GetCandidatePersonalDetails(_UEClientID, ref candidate) == 1)
                    {
                        ProfileName = candidate.PersonalProfile.Name;
                        CandidateEmail = candidate.PersonalProfile.CandidateEmail;
                        VerifyEmailLink = ConfigurationManager.AppSettings["AppURL"] + @"VerifyCandidateEmail.aspx?code=" + EncDec.Encrypt(System.DateTime.Now.ToString("ddMMyyyy") + "-" + candidate.CandidateID, ConfigurationManager.AppSettings["EncryptionPassword"]);
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            return bRetValue;
        }
        public bool GetInvoiceDetail(string _PaymentRecID)
        {
            bool bRetValue = false;
            try
            {
                RMSMasterManagement.GetCompanyDetails(ref companyProfile);
                if (PaymentManagement.GetPaymentDetails(_PaymentRecID, ref payment, false, true) == 1)
                {
                    switch(payment.PaymentType)
                    {
                        case cPaymentType.EmployerRegistrationFee:
                            ServiceTypeName = "Employer Registration Fee";
                            EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                            BillingTo = employer.BusinessName + "\r\n";
                            BillingTo = BillingTo + employer.address + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(employer.LocationStateCode).StateName + ", " + LocationManagement.GetCountryDetail(employer.LocationStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + employer.email + "\r\n";
                            break;
                        case cPaymentType.EmployerRecruitmentFee:
                            ServiceTypeName = "Employer Recruitment Fee";
                            EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                            BillingTo = employer.BusinessName + "\r\n";
                            BillingTo = BillingTo + employer.address + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(employer.LocationStateCode).StateName + ", " + LocationManagement.GetCountryDetail(employer.LocationStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + employer.email + "\r\n";
                            break;
                        case cPaymentType.EmployerRegistrationRenewal:
                            ServiceTypeName = "Employer Registration Renewal Fee";
                            EmployerManagement.GetEmployerByID(payment.UEClientID, ref employer);
                            BillingTo = employer.BusinessName + "\r\n";
                            BillingTo = BillingTo + employer.address + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(employer.LocationStateCode).StateName + ", " + LocationManagement.GetCountryDetail(employer.LocationStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + employer.email + "\r\n";
                            break;
                        case cPaymentType.CandidateRegistrationFee:
                            ServiceTypeName = "Candidate Registration Fee";
                            CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                            BillingTo = candidate.PersonalProfile.Name + "\r\n";
                            BillingTo = BillingTo + candidate.ContactDetails.CurrentAddress + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).StateName + ", " + LocationManagement.GetCountryDetail(candidate.ContactDetails.CurrentStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + candidate.PersonalProfile.CandidateEmail + "\r\n";
                            break;
                        case cPaymentType.CandidateRecruitmentFee:
                            ServiceTypeName = "Candidate Recruitment Fee";
                            CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                            BillingTo = candidate.PersonalProfile.Name + "\r\n";
                            BillingTo = BillingTo + candidate.ContactDetails.CurrentAddress + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).StateName + ", " + LocationManagement.GetCountryDetail(candidate.ContactDetails.CurrentStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + candidate.PersonalProfile.CandidateEmail + "\r\n";
                            break;
                        case cPaymentType.CandidateRegistrationRenewal:
                            ServiceTypeName = "Candidate Registration Renewal Fee";
                            CandidateManagement.GetCandidateFullDetails(payment.UEClientID, ref candidate);
                            BillingTo = candidate.PersonalProfile.Name + "\r\n";
                            BillingTo = BillingTo + candidate.ContactDetails.CurrentAddress + "\r\n";
                            BillingTo = BillingTo + LocationManagement.GetStateDetail(candidate.ContactDetails.CurrentStateCode).StateName + ", " + LocationManagement.GetCountryDetail(candidate.ContactDetails.CurrentStateCode).CountryName + "\r\n";
                            BillingTo = BillingTo + candidate.PersonalProfile.CandidateEmail + "\r\n";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }
            return bRetValue;
        }

        public bool PrepareEmailBody()
        {
            bool bRetValue = false;

            try
            {
                EmailBody = TemplateEmailBody;

                EmailBody = EmailBody.Replace("#CANDIDATENAME#", candidate.PersonalProfile.Name);
                EmailBody = EmailBody.Replace("#VACANCYNAME#", vacancy.VacancyName);
                EmailBody = EmailBody.Replace("#EMPLOYERNAME#", employer.BusinessName);
                EmailBody = EmailBody.Replace("#TOPIC#", interview.ZoomVCMeetingResponse.topic);
                EmailBody = EmailBody.Replace("#STARTDATETIME#", interview.ZoomVCMeetingResponse.start_time);
                EmailBody = EmailBody.Replace("#TIMEZONE#", interview.ZoomVCMeetingResponse.timezone);
                EmailBody = EmailBody.Replace("#PASSWORD#", interview.ZoomVCMeetingResponse.password);

                if(interview.ZoomVCMeetingResponse.start_url != null)
                    interview.ZoomVCMeetingResponse.start_url = interview.ZoomVCMeetingResponse.start_url.Replace("\\", "/");
                EmailBody = EmailBody.Replace("#HOSTURL#", interview.ZoomVCMeetingResponse.start_url);
                
                if (interview.ZoomVCMeetingResponse.join_url != null)
                    interview.ZoomVCMeetingResponse.join_url = interview.ZoomVCMeetingResponse.join_url.Replace("\\", "/");

                EmailBody = EmailBody.Replace("#STARTURL#", interview.ZoomVCMeetingResponse.join_url);

                EmailBody = EmailBody.Replace("#PROFILENAME#", ProfileName);
                EmailBody = EmailBody.Replace("#VerificationLink#", VerifyEmailLink);

                EmailBody = EmailBody.Replace("#BUSINESSADDRESS#", companyProfile.BillingAddress);
                EmailBody = EmailBody.Replace("#BUSINESSLOCATION#", companyProfile.BillingDistrict+", "+companyProfile.BillingState+", "+companyProfile.BillingCountry);
                EmailBody = EmailBody.Replace("#BUSINESSPHONE#", companyProfile.SupportContactNumber);
                EmailBody = EmailBody.Replace("#BUSINESSEMAIL#", companyProfile.SupportEmail);
                EmailBody = EmailBody.Replace("#BILLTO#", VerifyEmailLink);
                EmailBody = EmailBody.Replace("#INVOICENO#", payment.InvoiceNo);
                EmailBody = EmailBody.Replace("#PAYMENTDATE#", payment.Stripe.StripePaymentDate.ToString("d"));
                EmailBody = EmailBody.Replace("#ORDERNO#", payment.PaymentOrderNo);
                EmailBody = EmailBody.Replace("#TERMS#", "Payment Receipt");
                EmailBody = EmailBody.Replace("#UESERVICENAME#", ServiceTypeName);
                EmailBody = EmailBody.Replace("#AMOUNT#", payment.Amount.ToString());
                EmailBody = EmailBody.Replace("#TAXDETAILS#", "TAX");
                EmailBody = EmailBody.Replace("#TAX#", payment.TaxAmount.ToString());
                EmailBody = EmailBody.Replace("#TOTALAMOUNT#", (payment.Amount + payment.TaxAmount).ToString());
                EmailBody = EmailBody.Replace("#SUPPORTEMAIL#", companyProfile.SupportEmail);
                EmailBody = EmailBody.Replace("#SUPPORTNUMBER#", companyProfile.SupportContactNumber);
                bRetValue = true;
            }
            catch (Exception ex)
            {
                bRetValue = false;
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, Session["uerms_username"].ToString(), Message);
            }

            return bRetValue;
        }
        public string SendEmail(string _EmailSubject, string FromEmail, string ToMail = "NULL")
        {
            string bRetValue = "";

            if (ConfigurationManager.AppSettings["EnableEmailNotificaiton"] == "0")
                return "Email notification Disabled";

            string to = string.Empty;

            if (ToMail == "NULL")
                to = CustomerEmail; //To address    
            else
                to = ToMail;
#if GODADDY
            string from = FromEmail; //From address
#elif TESTING
            string from = FromEmail;
#endif

            if (to != null && to != string.Empty)
            {
                MailMessage message = new MailMessage(from, to);

                message.Subject = _EmailSubject;
                message.Body = EmailBody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
#if GODADDY
                SmtpClient client = new SmtpClient(); //Gmail smtp
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("pateluttam2012@gmail.com", "piorzhpitwqyfwiq");

#elif TESTING
            SmtpClient client = new SmtpClient("smtp-relay.sendinblue.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("developer.cet22@gmail.com", "hvRDCsz9P28XQcAW");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
#endif
                try
                {
                    client.Send(message);
                    bRetValue = "Email sent successfully.";
                }

                catch (Exception ex)
                {
                    string Message = "Error: " + ex.Message + "\r\n";
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Message = Message + t.ToString();
                    logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
                    return ex.Message;
                }
            }
            else
                bRetValue = "Mail cannot be sent. Customer email is blank.";
            return bRetValue;
        }
    }

    static public class Email
    {
        public static string SendCandidateInterviewNotification(string _InterviewID)
        {
            string sRetValue = string.Empty;
            try
            {
                GenerateEmail newEmail = new GenerateEmail();
                newEmail.ReadMailBody("~/EmailTemplate/candidateinterviewnotification.html");
                newEmail.GetNotificationDetails(_InterviewID);
                newEmail.PrepareEmailBody();
                sRetValue = newEmail.SendEmail("Interview Call Link by Universal Abroad", ConfigurationManager.AppSettings["ClientEmail"], newEmail.CandidateEmail);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return sRetValue;
        }
        public static string SendEmployerInterviewNotification(string _InterviewID)
        {
            string sRetValue = string.Empty;
            try
            {
                GenerateEmail newEmail = new GenerateEmail();
                newEmail.ReadMailBody("~/EmailTemplate/employerinterviewnotification.html");
                newEmail.GetNotificationDetails(_InterviewID);
                newEmail.PrepareEmailBody();
                sRetValue = newEmail.SendEmail("Interview Call Link by Universal Abroad", ConfigurationManager.AppSettings["ClientEmail"], newEmail.EmployerEmail);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return sRetValue;
        }
        public static string SendEmployerVerifyEmail(string _EmployerID)
        {
            string sRetValue = string.Empty;
            try
            {
                GenerateEmail newEmail = new GenerateEmail();
                newEmail.ReadMailBody("~/EmailTemplate/VerifyEmail.html");
                newEmail.GetProfileDetail(_EmployerID, cClientType.Employer);
                newEmail.PrepareEmailBody();
                sRetValue = newEmail.SendEmail("Universal Education Email Verification Link", ConfigurationManager.AppSettings["ClientEmail"], newEmail.EmployerEmail);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return sRetValue;
        }
        public static string SendCandidateVerifyEmail(string _CandidateID)
        {
            string sRetValue = string.Empty;
            try
            {
                GenerateEmail newEmail = new GenerateEmail();
                newEmail.ReadMailBody("~/EmailTemplate/VerifyEmail.html");
                newEmail.GetProfileDetail(_CandidateID, cClientType.Candidate);
                newEmail.PrepareEmailBody();
                sRetValue = newEmail.SendEmail("Universal abroad Email Verification Link", ConfigurationManager.AppSettings["ClientEmail"], newEmail.CandidateEmail);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return sRetValue;
        }
        public static string SendInvoiceEmail(string PaymentRecID)
        {
            string sRetValue = string.Empty;
            try
            {
                GenerateEmail newEmail = new GenerateEmail();
                newEmail.ReadMailBody("~/EmailTemplate/invoicetemplate.html");
                newEmail.GetInvoiceDetail(PaymentRecID);
                newEmail.PrepareEmailBody();
                sRetValue = newEmail.SendEmail("Universal abroad service invoice", ConfigurationManager.AppSettings["ClientEmail"], newEmail.CandidateEmail);
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.NOTIFICATION_MANAGEMENT, "", Message);
            }
            return sRetValue;
        }
    }
}