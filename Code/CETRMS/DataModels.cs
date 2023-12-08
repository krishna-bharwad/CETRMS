using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CETRMS
{
    public class DataModels
    {
    }

    public static class URLs
    {
        public const string CandidateDetailsURL = "CandidateProfile.aspx?CandidateID=";
        public const string EmployerDetailsURL = "EmployerDetails.aspx?EmployerId=";
        public const string VacancyDetailsURL = "JobVacancyDetails.aspx?VacancyId=";
        public const string InterviewDetailsURL = "InterviewDetails.aspx?InterviewID=";
        public const string PaymentDetailsURL = "PaymentDetails.aspx?PaymentID=";
        public const string JobApplicationDetails = "";
    }
    public static class RetValue
    {
        public const int Success = 1;
        public const int NoRecord = 0;
        public const int Error = -1;
    }
    public class CompanyProfile
    {
        public string CompanyBillingName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingDistrict { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string GSTNumber { get; set; }
        public string WhatsaAppMobileNo { get; set; }
        public string CompanyWebURL { get; set; }
        public string SupportContactNumber { get; set; }
        public string NoReplyEmail { get; set; }
        public string SupportEmail { get; set; }
        public string NewsLetterEmail { get; set; }
        public string AccountsEmail { get; set; }
        public string InfoEmail { get; set; }
        public string ReferralRegistrationLink { get; set; }
    }
    public class PersonalProfileClass
    {
        public string ProfileName { get; set; }
        public string Password { get; set; }
        public string email { get; set; }
    }
    public class GoogleTokenclass
    {
        public string access_token
        {
            get;
            set;
        }
        public string token_type
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
        public string refresh_token
        {
            get;
            set;
        }
    }
    public class GoogleUserclass
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string given_name
        {
            get;
            set;
        }
        public string family_name
        {
            get;
            set;
        }
        public string picture
        {
            get;
            set;
        }
        public string gender
        {
            get;
            set;
        }
        public string locale
        {
            get;
            set;
        }
        public string email { get; set; }
        public string verified_email { get; set; }
    }
    public class FacebookTokenclass
    {
        public string access_token
        {
            get;
            set;
        }
        public string token_type
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
    }
    public class FacebookUserclass
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string email
        {
            get;
            set;
        }
        public string imageurl
        {
            get;
            set;
        }
    }
    public class UEStaffMember
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string LocationCode { get; set; }
        public string Designation { get; set; }
        public string TeamId { get; set; }
        public byte[] StaffPhoto { get; set; }
        public int UserStatus { get; set; }
        public UEStaffMember()
        {
            StaffPhoto = null;
        }
    }
    public static class AuthenticationType
    {
        public const int PersonalProfile = 1;
        public const int Google = 2;
        public const int Facebook = 3;
    }
    static public class cClientType
    {
        public const int Employer = 1;
        public const int Candidate = 2;
    }
    public class UEClient
    {
        public string UEClientId { get; set; }
        public string ClientType { get; set; }
        public string ClientStatus { get; set; }
        public string UEAdminId { get; set; }
        public string LastReceiptId { get; set; }
        public string Password { get; set; }
        public int AuthenticationType { get; set; }
        public string AuthenticationId { get; set; }
        public string AuthenticationName { get; set; }
        public string AuthenticationProfileURL { get; set; }
        public DateTime SignedUpOn { get; set; }                //Add By Krishna
    }
    public class Employer
    {
        public string EmployerID { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string WhatsAppNumber { get; set; }
        public byte[] BusinessLogo { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string LocationStateCode { get; set; }
        public string LocationCityCode { get; set; }
        public string EmployerStatus { get; set; }              //Edited by Durgesh 
        public DateTime RegisteredOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string TempImageURL { get; set; }
        public string VerifyEmail { get; set; }
        public Employer()
        {
            EmployerID = string.Empty;
            Name = string.Empty;
            BusinessName = string.Empty;
            BusinessLogo = null;
            email = string.Empty;
            address = string.Empty;
        }
    }
    public static class EmployerStatus
    {
        public const int AllEmployers = -1;
        public const int NewRegistration = 1;
        public const int RegistrationComplete = 2;
        public const int RegistrationFeePaid = 3;
        public const int OpenVacancy = 4;
        public const int InProcessVacancy = 5;
        public const int RecruitmentFeePaid = 6;
        public const int FilledVacancy = 7;
        public const int RegistrationRenewalPending = 8;
        public const int RegistrationRenewalFeePaid = 9;
        public const int InActive = 10;
        public static string GetEmployerStatus(int EmployerStatusCode)
        {
            string sRetValue = string.Empty;
            switch(EmployerStatusCode)
            {
                case AllEmployers:
                    sRetValue = "All Employer";
                    break;
                case NewRegistration:
                    sRetValue = "New Registration";
                    break;
                case RegistrationComplete:
                    sRetValue = "Registration Fee Due";
                    break;
                case RegistrationFeePaid:
                    sRetValue = "Registration Fee Paid";
                    break;
                case OpenVacancy:
                    sRetValue = "Open Vacancy";
                    break;
                case InProcessVacancy:
                    sRetValue = "In process vacancy";
                    break;
                case RecruitmentFeePaid:
                    sRetValue = "Recruitment Fee Paid";
                    break;
                case FilledVacancy:
                    sRetValue = "Filled Vacancy";
                    break;
                case RegistrationRenewalPending:
                    sRetValue = "Registration Renewal Pending";
                    break;
                case RegistrationRenewalFeePaid:
                    sRetValue = "Registration Renewal Fee Paid";
                    break;
                case InActive:
                    sRetValue = "Inactive";
                    break;
            }
            return sRetValue;
        }
        public static int GetEmployerStatusList(ref List<Tuple<int, string, bool>> EmployerStatusList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                EmployerStatusList.Add(new Tuple<int, string, bool>(AllEmployers, GetEmployerStatus(AllEmployers), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(NewRegistration, GetEmployerStatus(NewRegistration), false));
                EmployerStatusList.Add(new Tuple<int, string, bool>(RegistrationComplete, GetEmployerStatus(RegistrationComplete), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(RegistrationFeePaid, GetEmployerStatus(RegistrationFeePaid), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(OpenVacancy, GetEmployerStatus(OpenVacancy), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(InProcessVacancy, GetEmployerStatus(InProcessVacancy), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(RecruitmentFeePaid, GetEmployerStatus(RecruitmentFeePaid), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(FilledVacancy, GetEmployerStatus(FilledVacancy), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(RegistrationRenewalPending, GetEmployerStatus(RegistrationRenewalPending), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(RegistrationRenewalFeePaid, GetEmployerStatus(RegistrationRenewalFeePaid), true));
                EmployerStatusList.Add(new Tuple<int, string, bool>(InActive, GetEmployerStatus(InActive), false));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }
    public class Candidate
    {
        public string CandidateID { get; set; }
        public string CanidateBriefProfile { get; set; }
        public string CandidatePhotoImageURL { get; set; }
        public class Personal
        {
            public string Name { get; set; }
            public string CandidateEmail { get; set; }
            public string CandidateIntro { get; set; }
            public DateTime DateOfbirth { get; set; }
            public string Gender { get; set; }
            public byte[] Photo { get; set; }
            public string MaritalStatus { get; set; }
            public string Nationality { get; set; }
            public string CandidateCast { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string LastUpdatedOn { get; set; }
            public string Status { get; set; }
            public string ValidationOTP { get; set; }
            public string ReferralCode { get; set; }
            public string ReferralCandidate { get; set; }
            public string VerifyEmail { get; set; }
            public string CandidatePhotoImageURL { get; set; }

            public Personal()
            {
                Name = string.Empty;
                CandidateEmail = string.Empty;
                CandidateIntro = string.Empty;
                DateOfbirth = System.DateTime.Now;
                Gender = string.Empty;
                Photo = null;
                MaritalStatus = string.Empty;
                Nationality = string.Empty;
                CandidateCast = string.Empty;
                RegistrationDate = System.DateTime.Now;
                Status = string.Empty;
                ValidationOTP = string.Empty;
                ReferralCode = string.Empty;
                ReferralCandidate = string.Empty;
            }
        }
        public class Passport
        {
            public string GivenName { get; set; }
            public string LegalGuardianName { get; set; }
            public string Surname { get; set; }
            public string MotherName { get; set; }
            public string SpouseName { get; set; }
            public DateTime PassportIssueDate { get; set; }
            public DateTime PassportExpiryDate { get; set; }
            public string PassportNumber { get; set; }
            public string PassportIssueLocation { get; set; }
            public string PassportFileType { get; set; }
            public string PassportFileName { get; set; }
            public byte[] PassportData { get; set; }
            public string PassportFileURL { get; set; }
            public Passport() 
            {
                GivenName = string.Empty;
                LegalGuardianName = string.Empty;
                Surname = string.Empty;
                MotherName = string.Empty;
                SpouseName = string.Empty;
                PassportIssueDate = System.DateTime.Now;
                PassportExpiryDate = System.DateTime.Now;
                PassportNumber = string.Empty;
                PassportIssueLocation = "001012";
                PassportFileType = string.Empty;
                PassportFileName = string.Empty;
                PassportFileURL = string.Empty;
                PassportData = null; 
            }
        }
        public class Visa
        {
            public string VisaTypeID { get; set; }
            public string DetailsOfVisa { get; set; }
            public DateTime VisaValidUpto { get; set; }
            public string VisaFileType { get; set; }
            public string VisaFileName { get; set; }
            public byte[] VisaFileData { get; set; }
            public string VisaFileURL { get; set; }
            public Visa() 
            {
                VisaTypeID = string.Empty;
                DetailsOfVisa = string.Empty;
                VisaValidUpto = System.DateTime.Now;
                VisaFileType = string.Empty;
                VisaFileName = string.Empty;
                VisaFileURL = string.Empty;
                VisaFileData = null; 
            }
        }
        public class Contact
        {
            public string ContactNumberCountryCode { get; set; }
            public string ContactNumber { get; set; }
            public string PermanentAddress { get; set; }
            public string PermanentStateCode { get; set; }
            public string PermanentCityCode { get; set; }
            public string CurrentAddress { get; set; }
            public string CurrentStateCode { get; set; }
            public string CurrentCityCode { get; set; }
            public Contact()
            {
                ContactNumberCountryCode = string.Empty;
                ContactNumber = string.Empty;
                PermanentAddress = string.Empty;
                PermanentStateCode = string.Empty;
                PermanentCityCode = string.Empty;
                CurrentAddress = string.Empty;
                CurrentStateCode = string.Empty;
                CurrentCityCode = string.Empty;
            }
        }
        public class Qualification
        {
            public string HighestQualification { get; set; }
            public string UniversityName { get; set; }
            public string UniversityLocation { get; set; }
            public string QualificationYear { get; set; }
            public Qualification()
            {
                HighestQualification = string.Empty;
                UniversityName = string.Empty;
                UniversityLocation = string.Empty;
                QualificationYear = string.Empty;
            }
        }
        public class Other
        {
            public string ResumeFileType { get; set; }
            public string ResumeFileName { get; set; }
            public byte[] ResumeData { get; set; }
            public int TotalExperienceMonth { get; set; }
            public int NoticePeriod { get; set; }
            public string ResumeFileURL { get; set; }
            public Other()
            {
                ResumeFileName = string.Empty;
                ResumeFileName = string.Empty;
                ResumeData = null;
                TotalExperienceMonth = 0;
                NoticePeriod = 0;
                ResumeFileURL = string.Empty;
            }
        }
        public class BankAccount
        {
            public string BankAccountNumber { get; set; }
            public string IFSCCode { get; set; }
            public BankAccount()
            {
                BankAccountNumber = string.Empty;
                IFSCCode = string.Empty;
            }
        }

        public Personal PersonalProfile;
        public Passport PassportDetails;
        public Visa VisaDetails;
        public Contact ContactDetails;
        public Qualification QualificationDetails;
        public Other OtherDetails;
        public BankAccount BankAccountDetails;
        public Candidate()
        {
            CandidateID = string.Empty;
            CanidateBriefProfile = string.Empty;
            CandidatePhotoImageURL = string.Empty;
            PersonalProfile = new Personal();
            PassportDetails = new Passport();
            VisaDetails = new Visa();
            ContactDetails = new Contact();
            QualificationDetails = new Qualification();
            OtherDetails = new Other();
            BankAccountDetails = new BankAccount();
        }
    }

    public class MaskedCandidate
    {
        public int CandidateID { get; set; }
        public string CanidateBriefProfile { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
       // public string Name { get; set; }                  //Added By Krishna
        public string Location { get; set; }
        public byte[] Photo { get; set; }
        public string VacancyName { get; set; }
        public int JobApplicationID { get; set; }
        public string CandidateProfileImgURL { get; set; }
        public int InterviewStatus { get; set; }
        public int JobApplicationStatus { get; set; }
    }
    public static class cVacancyStatus
    {
        public const int All = -1;
        public const int Open = 1;
        public const int Filled = 2;
        public const int Pause = 3;
        public const int Close = 4;
        public const int InProcess = 5;

        public static string GetVacancyStatus(int VacancyStatusCode)
        {
            string sRetValue = string.Empty;
            switch(VacancyStatusCode)
            {
                case Open:
                    sRetValue = "Open";
                    break;
                case Filled:
                    sRetValue = "Filled";
                    break;
                case Pause:
                    sRetValue = "Pause";
                    break;
                case Close:
                    sRetValue = "Close";
                    break;
                case InProcess:
                    sRetValue = "In proces";
                    break;
            }
            return sRetValue;
        }
    }   
    public static class CandidateStatus
    {
        public const int AllCandidates = 0;
        public const int NewRegistration = 1;
        public const int ProfileCreated = 2;
        public const int RegistrationFeePaid = 3;
        public const int AppliedtoVacancy = 4;
        public const int InterviewScheduled = 5;
        public const int InterviewAppeared = 6;
        public const int RecruitmentFeePaid = 7;
        public const int Hired = 8;
        public const int Rejected = 9;
        public const int AvailableForOpening = 10;
        public const int RegistrationRenewalPending = 11;
        public const int RegistrationRenewalFeePaid = 12;
        public const int InterviewDropped = 13;
        public const int RecruitementFeeDue = 14;
        public const int InActive = 15;

        public static string GetCandidateStatus(int StatusCode)
        {
            string sRetValue = string.Empty;
            switch (StatusCode)
            {
                case CandidateStatus.AllCandidates:
                    sRetValue = "All Candidates";
                    break;
                case CandidateStatus.NewRegistration:
                    sRetValue = "New Registration";
                    break;
                case CandidateStatus.ProfileCreated:
                    sRetValue = "Profile Created";
                    break;
                case CandidateStatus.RegistrationFeePaid:
                    sRetValue = "Registration Fee Paid";
                    break;
                case CandidateStatus.AppliedtoVacancy:
                    sRetValue = "Applied to Vacancy";
                    break;
                case CandidateStatus.InterviewScheduled:
                    sRetValue = "Interview Scheduled";
                    break;
                case CandidateStatus.InterviewAppeared:
                    sRetValue = "Interview Appeared";
                    break;
                case CandidateStatus.RecruitmentFeePaid:
                    sRetValue = "Recruitment Fee Paid";
                    break;
                case CandidateStatus.Hired:
                    sRetValue = "Hired";
                    break;
                case CandidateStatus.Rejected:
                    sRetValue = "Rejected";
                    break;
                case CandidateStatus.AvailableForOpening:
                    sRetValue = "Available for Opening";
                    break;
                case CandidateStatus.RegistrationRenewalPending:
                    sRetValue = "Registration Renewal Fee Pending";
                    break;
                case CandidateStatus.RegistrationRenewalFeePaid:
                    sRetValue = "Registration Renewal Fee Paid";
                    break;
                case CandidateStatus.InActive:
                    sRetValue = "In Active";
                    break;
                case CandidateStatus.RecruitementFeeDue:
                    sRetValue = "Recruitment Fee Due";
                    break;
            }
            return sRetValue;
        }
        public static int GetCandidateStatusList(ref List<Tuple<int, string, bool>> CandidateStatusList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                CandidateStatusList.Add(new Tuple<int, string, bool>(AllCandidates, GetCandidateStatus(AllCandidates), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(NewRegistration, GetCandidateStatus(NewRegistration), false));
                CandidateStatusList.Add(new Tuple<int, string, bool>(ProfileCreated, GetCandidateStatus(ProfileCreated), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(RegistrationFeePaid, GetCandidateStatus(RegistrationFeePaid), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(AppliedtoVacancy, GetCandidateStatus(AppliedtoVacancy), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(InterviewScheduled, GetCandidateStatus(InterviewScheduled), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(InterviewAppeared, GetCandidateStatus(InterviewAppeared), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(RecruitmentFeePaid, GetCandidateStatus(RecruitmentFeePaid), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(Hired, GetCandidateStatus(Hired), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(Rejected, GetCandidateStatus(Rejected), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(AvailableForOpening, GetCandidateStatus(AvailableForOpening), false));
                CandidateStatusList.Add(new Tuple<int, string, bool>(RegistrationRenewalPending, GetCandidateStatus(RegistrationRenewalPending), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(RegistrationRenewalFeePaid, GetCandidateStatus(RegistrationRenewalFeePaid), true));
                CandidateStatusList.Add(new Tuple<int, string, bool>(InActive, GetCandidateStatus(InActive), false));
                CandidateStatusList.Add(new Tuple<int, string, bool>(RecruitementFeeDue, GetCandidateStatus(RecruitementFeeDue), true));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }

    public class Vacancy
    {
        public string VacancyID { get; set; }
        public string UEEmployerID { get; set; }
        public string VacancyStatusTypeID { get; set; }
        public string VacancyName { get; set; }
        public string VacancyCode { get; set; }
        public string PrimaryLocation { get; set; }
        public string JobType { get; set; }
        public string EmployementStatus { get; set; }
        public int CandidatesRequired { get; set; }
        public int RequiredMinExp { get; set; }
        public string RequiredMinQualification { get; set; }
        public DateTime PostingDate { get; set; }
        public string VacancyDetails { get; set; }
        public double SalaryOffered { get; set; }
        public string VacancyStatusTypeName { get; set; }
        public int SalaryCycle { get; set; }

        public string StateName { get; set; }           //Add By Krishna

        public string CurrencySymbol { get; set; }      //Add By Krishna
        public Vacancy()
        {
            VacancyID = "0";
            UEEmployerID = "0";
            VacancyStatusTypeID = "0";
            VacancyName = string.Empty;
            VacancyCode = string.Empty;
            PrimaryLocation = string.Empty;
            JobType = string.Empty;
            EmployementStatus = string.Empty;
            CandidatesRequired = 0;
            RequiredMinExp = 0;
            RequiredMinQualification = string.Empty;
            PostingDate = System.DateTime.Now;
            VacancyDetails = string.Empty;
            SalaryOffered = 0.0;
        }
    }
    public static class SalaryCycle
    {
        public const int All = 0;
        public const int Week = 1;
        public const int month = 2;
        public const int year = 3;
    }
    public static class JobApplicationStatus
    {
        public const int AllApplications = -1;
        public const int NewApplication = 1;
        public const int ApplicationViewed = 2;
        public const int InterviewRequested = 3;
        public const int InterviewScheduled = 4;
        public const int Hired = 5;
        public const int Rejected = 6;
        public const int InterviewCancelled = 7;
        public const int Selected = 8;
        public const int InterviewCompleted = 9;
        public const int InterviewDropped = 10;
        public const int OfferLetterIssued = 11;
        public const int EmployerRecruitmentFeeDue = 12;
        public const int EmployerRecruitmentFeePaid = 13;
        public const int CandidateRecruitmentFeeDue = 14;
        public const int CandidateRecruitmentFeePaid = 15;
        public static string GetJobApplicationStatus(int JobApplicationStatusCode)
        {
            string sRetValue = string.Empty;
            switch(JobApplicationStatusCode)
            {
                case AllApplications:
                    sRetValue = "All Application";
                    break;
                case NewApplication:
                    sRetValue = "New Application";
                    break;
                case ApplicationViewed:
                    sRetValue = "Application Viewed";
                    break;
                case InterviewRequested:
                    sRetValue = "Interview Requested";
                    break;
                case InterviewScheduled:
                    sRetValue = "Interview Scheduled";
                    break;
                case Hired:
                    sRetValue = "Hired";
                    break;
                case Rejected:
                    sRetValue = "Rejected";
                    break;
                case InterviewCancelled:
                    sRetValue = "Interview Cancelled";
                    break;
                case Selected:
                    sRetValue = "Selected";
                    break;
                case InterviewCompleted:
                    sRetValue = "Interview Completed";
                    break;
                case InterviewDropped:
                    sRetValue = "Interview Dropped";
                    break;
                case OfferLetterIssued:
                    sRetValue = "Offer Letter Issued";
                    break;
                case EmployerRecruitmentFeeDue:
                    sRetValue = "Employer Recruitment Fee Due";
                    break;
                case EmployerRecruitmentFeePaid:
                    sRetValue = "Employer Recruitemet Fee Paid";
                    break;
                case CandidateRecruitmentFeeDue:
                    sRetValue = "Candidate Recruitment Fee Due";
                    break;
                case CandidateRecruitmentFeePaid:
                    sRetValue = "Candidate Recruitment Fee Paid";
                    break;
            }
            return sRetValue;
        }
        public static int GetJobApplicationStatusList(ref List<Tuple<int, string, bool>> JobApplicationStatusList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(AllApplications, GetJobApplicationStatus(AllApplications), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(NewApplication, GetJobApplicationStatus(NewApplication), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(ApplicationViewed, GetJobApplicationStatus(ApplicationViewed), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(InterviewRequested, GetJobApplicationStatus(InterviewRequested), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(InterviewScheduled, GetJobApplicationStatus(InterviewScheduled), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(Hired, GetJobApplicationStatus(Hired), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(Rejected, GetJobApplicationStatus(Rejected), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(InterviewCancelled, GetJobApplicationStatus(InterviewCancelled), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(Selected, GetJobApplicationStatus(Selected), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(InterviewCompleted, GetJobApplicationStatus(InterviewCompleted), true));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(InterviewDropped, GetJobApplicationStatus(InterviewDropped), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(OfferLetterIssued, GetJobApplicationStatus(OfferLetterIssued), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(EmployerRecruitmentFeeDue, GetJobApplicationStatus(EmployerRecruitmentFeeDue), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(EmployerRecruitmentFeePaid, GetJobApplicationStatus(EmployerRecruitmentFeePaid), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(CandidateRecruitmentFeeDue, GetJobApplicationStatus(CandidateRecruitmentFeeDue), false));
                JobApplicationStatusList.Add(new Tuple<int, string, bool>(CandidateRecruitmentFeePaid, GetJobApplicationStatus(CandidateRecruitmentFeePaid), false));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }
    public class JobApplication
    {
        public string JobApplicationID { get; set; }
        public string VacancyID { get; set; }
        public string CandidateID { get; set; }
        public int ApplicationStatus { get; set; }
        public string CompanyRemarks { get; set; }
        public DateTime AppliedOn { get; set; }
        public DateTime RemarksOn { get; set; }
        public string Reserve1 { get; set; }
        public string OfferLetterURL { get; set; }

        public JobApplication()
        {
            Reserve1 = string.Empty;
            OfferLetterURL = string.Empty;
        }
    }
    public class OfferLetterDetails
    {
        public string EmployerName { get; set; }
        public string EmployerBusinessName { get; set; }
        public string EmployerAddress { get; set; }
        public string EmployerEmail { get; set; }
        public string EmployerPhone { get; set; }
        public byte[] EmployerBusinessLogo { get; set; }
        public DateTime OfferLetterDate { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public string Salary { get; set; }
        public string OfferLetterURL { get; set; }
        public OfferLetterDetails()
        {
            EmployerBusinessLogo = null;
        }
    }
    public static class CReferralStatus
    {
        public const int All = -1;
        public const int NewSignUp = 1;
        public const int ProfileCompleted = 2;
        public const int ReferralPaymentDue = 3;
        public const int ReferralPaid = 4;
    }
    public class Referral
    {
        public string ReferralID { get; set; }
        public string UECandidateID { get; set; }
        public string ReferralStatus { get; set; }
        public string ReferralCode { get; set; }

        public Referral()
        {

        }
    }
    public static class InterviewCallStatus
    {
        public const int InterviewProposed = 1;
        public const int InterviewScheduled = 2;
        public const int InterviewStarted = 3;
        public const int InterviewCompleted = 4;
        public const int InterviewDropped = 5;
        public const int InterviewCancelled = 6;
        public const int InterviewRejected = 7;
        public static string GetInterviewStatus(int InterviewStatusCode)
        {
            string sRetValue = string.Empty;
            switch(InterviewStatusCode)
            {
                case InterviewProposed:
                    sRetValue = "Interview Proposed";
                    break;
                case InterviewScheduled:
                    sRetValue = "Interview Scheduled";
                    break;
                case InterviewStarted:
                    sRetValue = "Interview Started";
                    break;
                case InterviewCompleted:
                    sRetValue = "Interview Completed";
                    break;
                case InterviewDropped:
                    sRetValue = "Interview Dropped";
                    break;
                case InterviewCancelled:
                    sRetValue = "Interview Cancelled";
                    break;
                case InterviewRejected:
                    sRetValue = "Interview Rejected";
                    break;
            }
            return sRetValue;
        }
        public static int GetInterviewStatusList(ref List<Tuple<int, string, bool>> InterviewStatusList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewProposed, GetInterviewStatus(InterviewProposed), true));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewScheduled, GetInterviewStatus(InterviewScheduled), false));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewStarted, GetInterviewStatus(InterviewStarted), true));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewCompleted, GetInterviewStatus(InterviewCompleted), true));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewDropped, GetInterviewStatus(InterviewDropped), true));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewCancelled, GetInterviewStatus(InterviewCancelled), true));
                InterviewStatusList.Add(new Tuple<int, string, bool>(InterviewRejected, GetInterviewStatus(InterviewRejected), true));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }
    public class Interview
    {
        public string InterviewID { get; set; }
        public string JobApplicationID { get; set; }
        public DateTime PreferredDateTime { get; set; }
        public int InterviewStatus { get; set; }
        public string EmployerRemarks { get; set; }
        public string CandidateRemarks { get; set; }
        public string UERemarks { get; set; }
        public string ChosenTimeZone { get; set; }
        public int DurationInMinutes { get; set; }
        public class ZoomVideoCallMeetingRequest
        {
            public string agenda { get; set; }
            //public bool default_password { get; set; }
            public int duration { get; set; }
            //public string password { get; set; }
            //public bool pre_schedule { get; set; }
            //public class cRecurrence
            //{
            //    public string end_date_time { get; set; }
            //    public string end_times { get; set; }
            //    public string monthly_day { get; set; }
            //    public string monthly_week { get; set; }
            //    public string monthly_week_day { get; set; }
            //    public string repeat_interval { get; set; }
            //    public string type { get; set; }
            //    public string weekly_days { get; set; }

            //    public cRecurrence()
            //    {
            //        end_date_time = string.Empty;
            //        end_times = string.Empty;
            //        monthly_day = string.Empty;
            //        monthly_week = string.Empty;
            //        monthly_week_day = string.Empty;
            //        repeat_interval = string.Empty;
            //        type = string.Empty;
            //        weekly_days = string.Empty;
            //    }
            //}
            //public cRecurrence recurrence = new cRecurrence();
            //public string schedule_for { get; set; }
            //public class cSettings
            //{
            //    public string[] additional_data_center_regions { get; set; }
            //    public bool allow_multiple_devices { get; set; }
            //    public string alternative_hosts { get; set; }
            //    public bool alternative_hosts_email_notification { get; set; }
            //    public int approval_type { get; set; }
            //    public class cApproved_or_denied_countries_or_regions
            //    {
            //        public string[] approved_list { get; set; }
            //        public string[] denied_list { get; set; }
            //        public bool enable { get; set; }
            //        public string method { get; set; }
            //        public cApproved_or_denied_countries_or_regions() {
            //            enable = false;
            //            method = string.Empty;
            //        }
            //    }
            //    public cApproved_or_denied_countries_or_regions approved_or_denied_countries_or_regions = new cApproved_or_denied_countries_or_regions();
            //    public string audio { get; set; }
            //    public string authentication_domains { get; set; }
            //    public class cAuthentication_exception
            //    {
            //        public string email { get; set; }
            //        public string name { get; set; }
            //        public cAuthentication_exception() {
            //            email = string.Empty;
            //            name = string.Empty;
            //        }
            //    }
            //    public cAuthentication_exception authentication_exception = new cAuthentication_exception();
            //    public string authentication_option { get; set; }
            //    public string auto_recording { get; set; }
            //    public class cBreakout_room
            //    {
            //        public bool enable { get; set; }
            //        public class cRooms
            //        {
            //            public string name { get; set; }
            //            public string[] participants { get; set; }
            //            public cRooms()
            //            {
            //                name = string.Empty;
            //            }
            //        }

            //        public cRooms rooms = new cRooms();
            //        public cBreakout_room()
            //        {
            //            enable = false;
            //        }
            //    }
            //    public cBreakout_room breakout_room = new cBreakout_room();
            //    public int calendar_type { get; set; }
            //    public bool close_registration { get; set; }
            //    public string contact_email { get; set; }
            //    public string contact_name { get; set; }
            //    public bool email_notification { get; set; }
            //    public string encryption_type { get; set; }
            //    public bool focus_mode { get; set; }
            //    public string[] global_dial_in_countries { get; set; }
            //    public bool host_video { get; set; }
            //    public int jbh_time { get; set; }
            //    public bool join_before_host { get; set; }
            //    public class cLanguage_interpretation
            //    {
            //        public bool enable { get; set; }
            //        public class cInterpreters
            //        {
            //            public string email { get; set; }
            //            public string languages { get; set; }
            //            public cInterpreters()
            //            {
            //                email = string.Empty;
            //                languages = string.Empty;
            //            }
            //        }
            //        public cInterpreters interpreters = new cInterpreters();

            //        public cLanguage_interpretation()
            //        {
            //            enable = false;
            //        }
            //    }
            //    public bool meeting_authentication { get; set; }
            //    public class cMeeting_invitees
            //    {
            //        public string email { get; set; }
            //        public cMeeting_invitees()
            //        {
            //            email = string.Empty;
            //        }
            //    }
            //    public cMeeting_invitees[] meeting_invitees;
            //    public bool mute_upon_entry { get; set; }
            //    public bool participant_video { get; set; }
            //    public bool private_meeting { get; set; }
            //    public bool registrants_confirmation_email { get; set; }
            //    public bool registrants_email_notification { get; set; }
            //    public int registration_type { get; set; }
            //    public bool show_share_button { get; set; }
            //    public bool use_pmi { get; set; }
            //    public bool waiting_room { get; set; }
            //    public bool watermark { get; set; }
            //    public bool host_save_video_order { get; set; }
            //    public bool alternative_host_update_polls { get; set; }
            //    public cSettings() 
            //    {
            //        mute_upon_entry = false;
            //        participant_video = false;
            //        private_meeting = false;
            //        registrants_confirmation_email = false;
            //        registrants_email_notification = false;
            //        registration_type = false;
            //        show_share_button = false;
            //    }
            //}
            //public cSettings settings = new cSettings();
            public string start_time { get; set; }
            //public string template_id { get; set; }
            public string timezone { get; set; }
            public string topic { get; set; }
            public string host_email { get; set; }
            //public class cTracking_fields
            //{
            //    public string field { get; set; }
            //    public string value { get; set; }
            //}
            //public cTracking_fields[] tracking_fields;
            public int type { get; set; }
            public ZoomVideoCallMeetingRequest()
            {
                agenda = string.Empty;
                duration = 1;
                start_time = string.Empty;
                topic = string.Empty;
                type = 2;
            }

        }
        public class ZoomVideoCallMeetingResponse
        {
            public string uuid { get; set; }
            public string id { get; set; }
            public string host_id { get; set; }
            public string host_email { get; set; }
            public string topic { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public string start_time { get; set; }
            public string duration { get; set; }
            public string timezone { get; set; }
            public string created_at { get; set; }
            public string start_url { get; set; }
            public string join_url { get; set; }
            public string password { get; set; }
            public string h323_password { get; set; }
            public string pstn_password { get; set; }
            public string encrypted_password { get; set; }
            public class cSettings
            {
                public string host_video { get; set; }
                public string participant_video { get; set; }
                public string cn_meeting { get; set; }
                public string in_meeting { get; set; }
                public string join_before_host { get; set; }
                public string jbh_time { get; set; }
                public string mute_upon_entry { get; set; }
                public string watermark { get; set; }
                public string use_pmi { get; set; }
                public string approval_type { get; set; }
                public string audio { get; set; }
                public string auto_recording { get; set; }
                public string enforce_login { get; set; }
                public string enforce_login_domains { get; set; }
                public string alternative_hosts { get; set; }
                public string alternative_host_update_polls { get; set; }
                public string close_registration { get; set; }
                public string show_share_button { get; set; }
                public string allow_multiple_devices { get; set; }
                public string registrants_confirmation_email { get; set; }
                public string waiting_room { get; set; }
                public string request_permission_to_unmute_participants { get; set; }
                public string registrants_email_notification { get; set; }
                public string meeting_authentication { get; set; }
                public string encryption_type { get; set; }
                public string approved_or_denied_countries_or_regions { get; set; }
                public string breakout_room { get; set; }
                public string alternative_hosts_email_notification { get; set; }
                public string device_testing { get; set; }
                public string focus_mode { get; set; }
                public string enable_dedicated_group_chat { get; set; }
                public string private_meeting { get; set; }
                public string email_notification { get; set; }
                public string host_save_video_order { get; set; }
            }
            public cSettings settings = new cSettings();
            public string pre_schedule { get; set; }
        }
        public class ZoomVideoCallMeetingStatus
        {
            public string id { get; set; }
            public string uuid { get; set; }
            public string duration { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string host_id { get; set; }
            public string dept { get; set; }
            public string participants_count { get; set; }
            public string source { get; set; }
            public string topic { get; set; }
            public string total_minutes { get; set; }
            public string type { get; set; }
            public string user_email { get; set; }
            public string user_name { get; set; }
            public string Status { get; set; }
        }
        public ZoomVideoCallMeetingStatus ZoomVCMeetingStatus;
        public ZoomVideoCallMeetingRequest ZoomVCMeetingRequest;
        public ZoomVideoCallMeetingResponse ZoomVCMeetingResponse;
        public Interview() {
            ZoomVCMeetingRequest = new ZoomVideoCallMeetingRequest();
            ZoomVCMeetingResponse = new ZoomVideoCallMeetingResponse();
            ZoomVCMeetingStatus = new ZoomVideoCallMeetingStatus();
        }
    }
    public class cCountry
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyName { get; set; }
        public cCountry() { }
    }
    public class cState
    {
        public string StateCode { get; set; }
        public cCountry Country { get; set; }
        public string StateName { get; set; }
        public string CurrencyName { get; set; }                   
        public string CurrencySymbol { get; set; }          
        public cState()
        {
            StateCode = string.Empty;
            StateName = string.Empty;
            Country = new cCountry();
        }
    }
    public class cCity
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public cState State { get; set; }
        public cCity()
        {
            State = new cState();
        }

    }
    public class cTimeZone
    {
        public string TimeZoneName { get; set; }
        public string UTCName { get; set; }
        public double UTCTimeDiff { get; set; }
    }
    public static class cPaymentType
    {
        public const int EmployerRegistrationFee = 1;
        public const int CandidateRegistrationFee = 2;
        public const int EmployerRecruitmentFee = 3;
        public const int CandidateRecruitmentFee = 4;
        public const int EmployerRegistrationRenewal = 5;
        public const int CandidateRegistrationRenewal = 6;
        public static string GetPaymentType(int PaymentTypeCode)
        {
            string sRetValue = string.Empty;
            switch(PaymentTypeCode)
            {
                case EmployerRegistrationFee:
                    sRetValue = "Employer Registration Fee";
                    break;
                case CandidateRegistrationFee:
                    sRetValue = "Candidate REgistration Fee";
                    break;
                case EmployerRecruitmentFee:
                    sRetValue = "Employer Recruitment Fee";
                    break;
                case CandidateRecruitmentFee:
                    sRetValue = "Candidate Recruitment Fee";
                    break;
                case EmployerRegistrationRenewal:
                    sRetValue = "Employer Registration Renewal";
                    break;
                case CandidateRegistrationRenewal:
                    sRetValue = "Candidate Registration Renewal";
                    break;
            }
            return sRetValue;
        }
        public static int GetPaymentTypeList(ref List<Tuple<int,string, bool>> PaymentTypeList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                PaymentTypeList.Add(new Tuple<int, string, bool>(EmployerRegistrationFee, GetPaymentType(EmployerRegistrationFee), true));
                PaymentTypeList.Add(new Tuple<int, string, bool>(CandidateRegistrationFee, GetPaymentType(CandidateRegistrationFee), true));
                PaymentTypeList.Add(new Tuple<int, string, bool>(EmployerRecruitmentFee, GetPaymentType(EmployerRecruitmentFee), true));
                PaymentTypeList.Add(new Tuple<int, string, bool>(CandidateRecruitmentFee, GetPaymentType(CandidateRecruitmentFee), true));
                PaymentTypeList.Add(new Tuple<int, string, bool>(EmployerRegistrationRenewal, GetPaymentType(EmployerRegistrationRenewal), true));
                PaymentTypeList.Add(new Tuple<int, string, bool>(CandidateRegistrationRenewal, GetPaymentType(CandidateRegistrationRenewal), true));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }
    public static class cPaymentStatus
    {
        public const int All = -1;
        public const int PaymentDueWithinTime = 1;
        public const int PaymentDueOutOfTime = 2;
        public const int PaymentInitiated = 3;
        public const int PaymentDone = 4;
        public const int PaymentCancelled = 5;
        public const int PaymentDiscounted = 6;
        public const int PaymentFailed = 7;
        public const int AllDuePayments = 8;
        public static string GetPaymentStatus(int PaymentStatusCode)
        {
            string sRetValue = string.Empty;
            switch(PaymentStatusCode)
            {
                case All:
                    sRetValue = "All";
                    break;
                case PaymentDueWithinTime:
                    sRetValue = "Payment Due With in time limite";
                    break;
                case PaymentDueOutOfTime:
                    sRetValue = "Payment Due Out of time";
                    break;
                case PaymentInitiated:
                    sRetValue = "Payment Initiated";
                    break;
                case PaymentDone:
                    sRetValue = "Payment done";
                    break;
                case PaymentCancelled:
                    sRetValue = "Payment Cancelled";
                    break;
                case PaymentDiscounted:
                    sRetValue = "Payment Discounted";
                    break;
                case PaymentFailed:
                    sRetValue = "Payment Failed";
                    break;
                case AllDuePayments:
                    sRetValue = "All Due Payments";
                    break;
            }
            return sRetValue;
        }
        public static int GetPaymentStatusList(ref List<Tuple<int, string, bool>> PaymentStatusList)
        {
            int iRetValue = RetValue.NoRecord;
            try
            {
                PaymentStatusList.Add(new Tuple<int, string, bool>(All, GetPaymentStatus(All), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentDueWithinTime, GetPaymentStatus(PaymentDueWithinTime), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentDueOutOfTime, GetPaymentStatus(PaymentDueOutOfTime), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentInitiated, GetPaymentStatus(PaymentInitiated), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentDone, GetPaymentStatus(PaymentDone), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentCancelled, GetPaymentStatus(PaymentCancelled), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentDiscounted, GetPaymentStatus(PaymentDiscounted), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(PaymentFailed, GetPaymentStatus(PaymentFailed), true));
                PaymentStatusList.Add(new Tuple<int, string, bool>(AllDuePayments, GetPaymentStatus(AllDuePayments), true));
                iRetValue = RetValue.Success;
            }
            catch
            {
                iRetValue = RetValue.Error;
            }
            return iRetValue;
        }
    }
    public class PaymentLog
    {
        public string LogId { get; set; }
        public string PaymentRecID { get; set; }
        public DateTime PaymentLogTime { get; set; }
        public int PaymentStatus { get; set; }
        public string PaymentLogMessage { get; set; }
        public PaymentLog()
        {
            LogId = string.Empty;
            PaymentRecID = string.Empty;
            PaymentLogMessage = string.Empty;
        }
    }
    public class PaymentTypeDetails
    {
        public int PaymentType { get; set; }
        public double Amount { get; set; }
        public double Tax { get; set; }
        public string Currency { get; set; }
        public string CalculationMode { get; set; } // AMOUNT : PERCENT
        public int DueDays { get; set; }
    }
    public class Payment
    {
        public string PaymentRecID { get; set; }
        public string PaymentOrderNo { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public double TaxAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string UEClientID { get; set; }
        public int PaymentStatus { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceURL { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public class cStripe
        {
            public string StripePaymentID { get; set; }
            public DateTime StripePaymentDate { get; set; }
            public string StripePaymentStatus { get; set; }
            public string StripePaymentMessage { get; set; }
            public string StripePaymentReceiptURL { get; set; }
            public string StripePaymentMethod { get; set; }
            public cStripe()
            {
                StripePaymentID = string.Empty;
                StripePaymentStatus = string.Empty;
                StripePaymentMessage = string.Empty;
                StripePaymentReceiptURL = string.Empty;
                StripePaymentMethod = string.Empty;
            }
        }
        public cStripe Stripe = new cStripe();
        public int NotificationID { get; set; }
        public int NotificationType { get; set; }

        public Payment()
        {
            PaymentRecID = string.Empty;
            PaymentOrderNo = string.Empty;
            Currency = string.Empty;
            Amount = 0.00;
            TaxAmount = 0.00;
            DueDate = System.DateTime.Now;
            TransactionType = string.Empty;
            UEClientID = string.Empty;
            InvoiceNo = string.Empty;
            InvoiceURL = string.Empty;
            Reserve1 = string.Empty;
            Reserve2 = string.Empty;
        }
        //public class cStripePaymentIntentReq
        //{
        //    public string amount { get; set; }
        //    public string currency { get; set; }
        //    public class cautomatic_payment_methods
        //    {
        //        public string enabled { get; set; }
        //    }
        //    //public cautomatic_payment_methods automatic_payment_methods = new cautomatic_payment_methods();
        //    public bool confirm { get; set; }
        //    public string customer { get; set; }
        //    public string description { get; set; }
        //    public bool off_session { get; set; }
        //    public string payment_method { get; set; }
        //    public string receipt_email { get; set; }
        //    public class cShipping
        //    {
        //        public class cAddress
        //        {
        //            public string city { get; set; }
        //            public string country { get; set; }
        //            public string line1 { get; set; }
        //            public string line2 { get; set; }
        //            public string state { get; set; }
        //        }
        //        public cAddress address = new cAddress();
        //        public string name { get; set; }
        //        public string carrier { get; set; }
        //        public string phone { get; set; }
        //        public string tracking_number { get; set; }
        //    }
        //    public cShipping shipping = new cShipping();
        //    public string statement_descriptor { get; set; }
        //    public string statement_descriptor_suffix { get; set; }
        //}
        //public class cStripePaymentSuccessStatus
        //{
        //    public class cObject
        //    {
        //        public string id { get; set; }
        //        public string sObject { get; set; }
        //        public string amount { get; set; }
        //        public string amount_captured { get; set; }
        //        public string amount_refunded { get; set; }
        //        public string application { get; set; }
        //        public string application_fee { get; set; }
        //        public string application_fee_amount { get; set; }
        //        public string balance_transaction { get; set; }
        //        public class cBilling_details
        //        {
        //            public class cAddress
        //            {
        //                public string city { get; set; }
        //                public string country { get; set; }
        //                public string line1 { get; set; }
        //                public string line2 { get; set; }
        //                public string postal_code { get; set; }
        //                public string state { get; set; }
        //            }
        //            public cAddress address = new cAddress();
        //            public string email { get; set; }
        //            public string name { get; set; }
        //            public string phone { get; set; }
        //        }
        //        public cBilling_details billing_details = new cBilling_details();
        //        public string calculated_statement_descriptor { get; set; }
        //        public string captured { get; set; }
        //        public string created { get; set; }
        //        public string currency { get; set; }
        //        public string customer { get; set; }
        //        public string description { get; set; }
        //        public string destination { get; set; }
        //        public string dispute { get; set; }
        //        public string disputed { get; set; }
        //        public string failure_balance_transaction { get; set; }
        //        public string failure_code { get; set; }
        //        public string failure_message { get; set; }
        //        public class cFraud_details
        //        {
        //        }
        //        public cFraud_details fraud_details = new cFraud_details();
        //        public string invoice { get; set; }
        //        public string livemode { get; set; }
        //        public class cMetadata
        //        {
        //        }
        //        public cMetadata metadata = new cMetadata();
        //        public string on_behalf_of { get; set; }
        //        public string order { get; set; }
        //        public class cOutcome
        //        {
        //            public string network_status { get; set; }
        //            public string reason { get; set; }
        //            public string risk_level { get; set; }
        //            public int risk_score { get; set; }
        //            public string seller_message { get; set; }
        //            public string type { get; set; }
        //        }
        //        public cOutcome outcome = new cOutcome();
        //        public bool paid { get; set; }
        //        public string payment_intent { get; set; }
        //        public string payment_method { get; set; }
        //        public class cPayment_method_details
        //        {
        //            public class cCard
        //            {
        //                public string brand { get; set; }
        //                public class cChecks
        //                {
        //                    public string address_line1_check { get; set; }
        //                    public string address_postal_code_check { get; set; }
        //                    public string cvc_check { get; set; }
        //                }
        //                public cChecks checks = new cChecks();
        //                public string country { get; set; }
        //                public int exp_month { get; set; }
        //                public int exp_year { get; set; }
        //                public string fingerprint { get; set; }
        //                public string funding { get; set; }
        //                public string installments { get; set; }
        //                public string last4 { get; set; }
        //                public string mandate { get; set; }
        //                public string network { get; set; }
        //                public string network_token { get; set; }
        //                public class cThree_d_secure
        //                {
        //                    public string authentication_flow { get; set; }
        //                    public string result { get; set; }
        //                    public string result_reason { get; set; }
        //                    public string version { get; set; }
        //                }
        //                public cThree_d_secure three_d_secure = new cThree_d_secure();
        //                public string wallet { get; set; }
        //            }
        //            public cCard card = new cCard();
        //            public string type { get; set; }
        //        }
        //        public cPayment_method_details payment_method_details = new cPayment_method_details();
        //        public string receipt_email { get; set; }
        //        public string receipt_number { get; set; }
        //        public string receipt_url { get; set; }
        //        public bool refunded { get; set; }
        //        public string review { get; set; }
        //        public string shipping { get; set; }
        //        public string source { get; set; }
        //        public string source_transfer { get; set; }
        //        public string statement_descriptor { get; set; }
        //        public string statement_descriptor_suffix { get; set; }
        //        public string status { get; set; }
        //        public string transfer_data { get; set; }
        //        public string transfer_group { get; set; }
        //    }
        //}
        //public cStripePaymentIntentReq StripePaymentIntentReq = new cStripePaymentIntentReq();
        //public cStripePaymentIntentObj stripePaymentIntentObj = new cStripePaymentIntentObj();
        //public cStripePaymentSuccessStatus stripePaymentSuccessStatus = new cStripePaymentSuccessStatus();
    }
    public class PaymentLogInfo
    {
        public string PaymentRecID { get; set; }
        public string id { get; set; }
        public string Amount { get; set; }
        public string payment_method { get; set; }
        public DateTime CreatedAt { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string StripePaymentLog { get; set; }
        public PaymentLogInfo()
        {
            PaymentRecID = string.Empty;
            id = string.Empty;
            Amount = string.Empty;
            payment_method = string.Empty;
            status = string.Empty;
            remarks = string.Empty;
            StripePaymentLog = string.Empty;
        }
    }
    public class Receipt
    {
        public string ReceiptId { get; set; }
        Receipt() {
            ReceiptId = string.Empty;
        }
    }
    public static class cNotificationType
    {
        public const int AllNotifications = -1;
        public const int BroadCastNotification = 1;
        public const int PersonalisedNotification = 2;
        public const int AdminNotification = 3;
    }
    public static class cNotificationStatus
    {
        public const int AllNotifications = -1;
        public const int NotificationDrafted = 1;
        public const int NotificationSent = 2;
        public const int NotificationUnread = 3;
        public const int NotificationRead = 4;
        public const int NotificationDropped = 5;
    }
    public class Notification
    {
        public string NotificationID { get; set; }
        public DateTime NotificationTime { get; set; }
        public int NotificationType { get; set; }
        public string NotificationMessage { get; set; }
        public string UEClientID { get; set; }
        public int NotificationStatus { get; set; }
        public string hyperlink { get; set; }
    }
    public class WhatsAppAcount
    {
        private int AccountId { get; set; }
        public string PhoneNumber { get; set; }
    }
    public static class WhatsAppMessageType
    {
        public static string WhatsAppText = "text";
        public static string WhatsAppTemplate = "template";
        public static string WhatsAppReaction = "reaction";
        public static string WhatsAppAudio = "audio";
        public static string WhatsAppDocument = "document";
        public static string WhatsAppImage = "image";
        public static string WhatsAppSticker = "sticker";
        public static string WhatsAppVideo = "video";
        public static string WhatsAppLocation = "location";
        public static string WhatsAppContact = "contact";
        public static string WhatsAppInteractive = "interactive";
    }
    public class WhatsAppMessage
    {
        public class cRequest
        {
            public string messaging_product { get; set; }
            public string recipient_type { get; set; }
            public string to { get; set; }
            public string type { get; set; }
            public class ctext
            {
                public string preview_url { get; set; }
                public string body { get; set; }
                public ctext() { }
            }
            public ctext text = new ctext();
            public class ctemplate
            {
                public string name { get; set; }
                public class clanguage 
                { 
                    public string code { get; set; }
                    public clanguage() { }
                }
                public clanguage language = new clanguage();
                public ctemplate() { }
            }
            public ctemplate template = new ctemplate();
            public class cReaction
            {
                public string message_id { set; get; }
                public string emoji { get; set; }
            }
            public cReaction reaction = new cReaction();
            public class cImage
            {
                public string id { get; set; }
                public string link { get; set; }
                public cImage() { }
            }
            public cImage image = new cImage();
            public class cAudio
            {
                public string id { get; set; }
                public string link { get; set; }
                public cAudio() { }
            }
            public cAudio audio = new cAudio();
            public class cDocument
            {
                public string id { get; set; }
                public string link { get; set; }
                public cDocument() { }
            }
            public cDocument document = new cDocument();
            public class cSticker
            {
                public string id { get; set; }
                public string link { get; set; }
                public cSticker() { }
            }
            public cSticker sticker = new cSticker();
            public class cVideo
            {
                public string id { get; set; }
                public string link { get; set; }
                public cVideo() { }
            }
            public cVideo video = new cVideo();
            public class cLocation
            {
                public long longitude { get; set; }
                public long latitude { get; set; }
                public string name { get; set; }
                public string address { get; set; }
                public cLocation() { }
            }
            public cLocation location = new cLocation();
            public class cContact
            {
                public class caddresse
                {
                    public string street { get; set; }
                    public string city { get; set; }
                    public string state { get; set; }
                    public string zip { get; set; }
                    public string country { get; set; }
                    public string country_code { get; set; }
                    public string type { get; set; } //"HOME" "WORK"
                }
                public List<caddresse> addresses = new List<caddresse>();
                public string birthday { get; set; }
                public class cEmail
                {
                    public string email { get; set; }
                    public string type { get; set; } //"HOME" "WORK"
                }
                public List<cEmail> emails = new List<cEmail>();
                public class cName
                {
                    public string formatted_name { get; set; }
                    public string first_name { get; set; }
                    public string last_name { get; set; }
                    public string middle_name { get; set; }
                    public string suffix { get; set; }
                    public string prefix { get; set; }
                }
                public cName name = new cName();
                public class cOrg
                {
                    public string company { get; set; }
                    public string department { get; set; }
                    public string title { get; set; }
                }
                public cOrg org = new cOrg();
                public class cPhone
                {
                    public string phone { get; set; }
                    public string type { get; set; } //"HOME" "WORK"
                    public string wa_id { get; set; }
                }
                public List<cPhone> phones = new List<cPhone>();
                public class cUrl
                {
                    public string url { get; set; }
                    public string type { get; set; } //"HOME" "WORK"
                }
                public List<cUrl> urls = new List<cUrl>();
                public cContact() { }
            }
            public List<cContact> contacts = new List<cContact>();
            public class cInteractive
            {
                public string type { get; set; }
                public class cHeader
                {
                    public string type { get; set; }
                    public string text { get; set; }
                }
                public cHeader header = new cHeader();
                public class cBody
                {
                    public string text { get; set; }
                }
                public cBody body = new cBody();
                public class cFooter
                {
                    public string text { get; set; }
                }
                public class cAction
                {
                    public string button { get; set; }
                    public class cSection
                    {
                        public string title { get; set; }
                        public class cRow
                        {
                            public string id { get; set; }
                            public string title { get; set; }
                            public string description { get; set; }
                        }
                        public List<cRow> rows = new List<cRow>();
                    }
                    public class cBTN
                    {
                        public string type { get; set; }
                        public class cReply
                        {
                            public string id { get; set; }
                            public string title { get; set; }
                        }
                        public cReply reply = new cReply();
                    }
                    public List<cBTN> buttons = new List<cBTN>();
                }
            }
            public cRequest() { }
        }
        public class cResponse
        { 
            public string messaging_product { get; set; }
            public class cContacts
            {
                public string input { get; set; }
                public string wa_id { get; set; }
            }
            public cContacts contacts = new cContacts();
            public class cMessage
            {
                public string id { get; set; }
            }
            public List<cMessage> messages = new List<cMessage>();

            public cResponse() { }
        }

        public cRequest Request = new cRequest();
        public cResponse Response = new cResponse();
    }

    public class cMaskedInterviewDetails
    {
        public string VacancyName { get; set; }
        public int CandidateID { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string CandidateLocation { get; set; }
        public byte[] Photo { get; set; }
        public string Status { get; set; }
        public string BriefProfile { get; set; }
        public int JobApplicationID { get; set; }
        public string InterviewID { get; set; }
        public DateTime InterviewScheduleDate { get; set; }
        public string InteviewTimeZone { get; set; }
        public string ZoomMeetingId { get; set; }
        public string ZoomStartUrl { get; set; }
        public string ZoomMeetingStatus { get; set; }
        public string CandidateProfileImgURL { get; set; }

    }
    public class Testimonial
    {
        public string TestimonialID { get; set; }
        public string UEClientId { get; set; }
        public DateTime ResponseDate { get; set; }
        public int Rating { get; set; }
        public string ResponseMessage { get; set; }
        public int IsShown { get; set; }
    }
    public class VisaType
    {
        public string VisaTypeID { get; set; }
        public string VisaCountryName { get; set; }
        public string VisaValidityYears { get; set; }
        public string VisaTypeName { get; set; }
        public string VisaTypeDetails { get; set; }
        public string VisaStateCode { get; set; }
    }
}