
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
    public partial class CandidateProfile : System.Web.UI.Page
    {
        SqlConnection sqlco = new SqlConnection();
        public  string CandidateId;
        public  string JobApplicationIdd;
        public List<Candidate> oCandidateList = new List<Candidate>();
        public   List<JobApplication> oJobApplication = new List<JobApplication>();
        public   Vacancy oVacancy = new Vacancy();
        public List<Interview> oInterview = new List<Interview>();
        public Candidate _Candidate = new Candidate();
        public JobApplication JobApp = new JobApplication();
        public VisaType  oVisaType = new VisaType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cetrms_username"] == null)
            {
                Response.Redirect("~/NewIndex.aspx", false);
            }
            if (Request.QueryString["NotificationID"] != null)
            {
                NotificationManagement.MarkNotificationRead(Request.QueryString["NotificationID"].ToString());
            }
            CandidateId = Request.QueryString.Get("CandidateID").ToString();
            
            sqlco.ConnectionString = ConfigurationManager.ConnectionStrings["CETRMSDB"].ConnectionString;
            if (!IsPostBack)
            {
                
                GetCandidateDetail();
                ShowCandidateDetail();
                FillJobApplicationGV();
                FillCandidateInterviewGV();
                FillCandidateRejectionGV();
            }

        }
        protected void GetCandidateDetail()
        {
            CandidateManagement.GetCandidateFullDetails(CandidateId,ref _Candidate, true);

        }
        protected void ShowCandidateDetail()
        {
            string CandidatePhotoImg = string.Empty;
            string imagetag = string.Empty;

            if (_Candidate.PersonalProfile.Photo != null)
            {
                CandidatePhotoImg = Convert.ToBase64String(_Candidate.PersonalProfile.Photo);

                imagetag = "<img src = \"data:image/jpg; base64, " + CandidatePhotoImg + " \" alt=\"\">";

                CandidiatePhotoImg.ImageUrl = String.Format("data:image/jpg;base64,{0}", CandidatePhotoImg);
            }
            else
            {
                imagetag = "<img src=\"images/user.png\" alt=\"\" >";


            }
            string CandStatus = string.Empty;
            CandStatus = CandidateStatus.GetCandidateStatus(Convert.ToInt32(_Candidate.PersonalProfile.Status));
            CandidateStatusLBL.Text = CandStatus;
            CandidateFullNameLBL.Text = _Candidate.PersonalProfile.Name;
            AppliedOnLBL.Text=_Candidate.PersonalProfile.RegistrationDate.ToString();
            ProfileUpdatedOnLBL.Text = _Candidate.PersonalProfile.LastUpdatedOn;
            ProfileBriefLBL.Text = _Candidate.PersonalProfile.CandidateIntro;

            string CandidateID = _Candidate.CandidateID;
            CandidateManagement.GetCandidateBriefProfile(CandidateID,ref _Candidate);
            CandidateProfileLBL.Text = _Candidate.CanidateBriefProfile;

             DateTime DoB;
            if (DateTime.TryParse(_Candidate.PersonalProfile.DateOfbirth.ToString(), out DoB))
            {
                DateTime Today = DateTime.Now;

                TimeSpan Span = Today - DoB;

                DateTime Age = DateTime.MinValue + Span;

                // note: MinValue is 1/1/1 so we have to subtract...
                int Years = Age.Year - 1;
                int Months = Age.Month - 1;
                int Days = Age.Day - 1;

                DateOfBirthLBL.Text = DoB.Date.ToString("dd-MMM-yyyy") + "<p/> (" + Years.ToString() + " Years, " + Months.ToString() + " Months)";
            }
            NationalityLBL.Text = _Candidate.PersonalProfile.Nationality;
            PermanentAddressLBL.Text = _Candidate.ContactDetails.PermanentAddress +", "+ LocationManagement.GetCityDetail(_Candidate.ContactDetails.PermanentStateCode, _Candidate.ContactDetails.PermanentCityCode).CityName + ", " + LocationManagement.GetStateDetail(_Candidate.ContactDetails.PermanentStateCode).StateName;

            CurrentAddressLBL.Text = _Candidate.ContactDetails.CurrentAddress + ", " + LocationManagement.GetCityDetail(_Candidate.ContactDetails.CurrentStateCode, _Candidate.ContactDetails.CurrentCityCode).CityName + ", " +LocationManagement.GetStateDetail(_Candidate.ContactDetails.CurrentStateCode).StateName;
            MobileLBL.Text = "+" + "("+ _Candidate.ContactDetails.ContactNumberCountryCode + ")" + "-"  + " " + _Candidate.ContactDetails.ContactNumber;
            EmailLBL.Text = _Candidate.PersonalProfile.CandidateEmail;
            CandidateCastLBL.Text = _Candidate.PersonalProfile.CandidateCast;
            GivenNameLBL.Text = _Candidate.PassportDetails.GivenName;
            LegalGuardianNameLBL.Text = _Candidate.PassportDetails.LegalGuardianName;
            SurnameLBL.Text = _Candidate.PassportDetails.Surname;
            MotherNameLBL.Text = _Candidate.PassportDetails.MotherName;
            SpouseNameLBL.Text = _Candidate.PassportDetails.SpouseName;
            PassportIssueDateLBL.Text = _Candidate.PassportDetails.PassportIssueDate.ToString("dd/MM/yyyy");
            PassportExpiryDateLBL.Text = _Candidate.PassportDetails.PassportExpiryDate.ToString("dd/MM/yyyy");
            PassportNumberLBL.Text = _Candidate.PassportDetails.PassportNumber;

            PassportIssueLocationLBL.Text = _Candidate.PassportDetails.PassportIssueLocation +","+ LocationManagement.GetStateDetail(_Candidate.PassportDetails.PassportIssueLocation).StateName + "," + LocationManagement.GetStateDetail(_Candidate.PassportDetails.PassportIssueLocation).Country.CountryName;

            VisaTypeNameLBL.Text = _Candidate.VisaDetails.VisaTypeID ;
            RMSMasterManagement.GetVisaTypeDetails(_Candidate.VisaDetails.VisaTypeID,ref oVisaType);
            VisaTypeNameLBL.Text = oVisaType.VisaTypeName;
            VisaCountryNameLBL.Text = oVisaType.VisaCountryName;
            VisaValidityYearsLBL.Text = oVisaType.VisaValidityYears;
            VisaTypeDetailsLBL.Text = oVisaType.VisaTypeDetails;
            VisaStateCodeLBL.Text = oVisaType.VisaStateCode;
            DetailsOfVisaLBL.Text = _Candidate.VisaDetails.DetailsOfVisa;
            VisaValidUptoLBL.Text = _Candidate.VisaDetails.VisaValidUpto.ToString("dd/MM/yyyy");
            HighestQualificationLBL.Text = _Candidate.QualificationDetails.HighestQualification;
            UniversityNameLBL.Text = _Candidate.QualificationDetails.UniversityName;

            UniversityLocationLBL.Text = _Candidate.QualificationDetails.UniversityLocation + ", " + LocationManagement.GetStateDetail(_Candidate.QualificationDetails.UniversityLocation).StateName + ", " + LocationManagement.GetStateDetail(_Candidate.QualificationDetails.UniversityLocation).Country.CountryName; 
            QualificationYearLBL.Text = _Candidate.QualificationDetails.QualificationYear;

            TotalExperienceMonthLBL.Text = _Candidate.OtherDetails.TotalExperienceMonth.ToString();
            NoticePeriodLBL.Text = _Candidate.OtherDetails.NoticePeriod.ToString();
         }

        protected void PassportDownloadLB_Click(object sender, EventArgs e)
        {
            try
            {
                sqlco.Open();
                SqlCommand com = new SqlCommand("select PassportFileName,PassportFileType,PassportData from UECandidate where @CandidateID = CandidateID", sqlco);
                com.Parameters.AddWithValue("CandidateID", CandidateId);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = dr["PassportFileType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dr["PassportFileName"].ToString()); // to open file prompt Box open or Save file  
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite((byte[])dr["PassportData"]);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            finally
            {
                sqlco.Close();
            }
        }

        protected void VisaDownloadLB_Click(object sender, EventArgs e)
        {
            try
            {
                sqlco.Open();
                SqlCommand com = new SqlCommand("select VisaFileName,VisaFileType,VisaData from UECandidate where @CandidateID = CandidateID", sqlco);
                com.Parameters.AddWithValue("CandidateID", CandidateId);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = dr["VisaFileType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dr["VisaFileName"].ToString()); // to open file prompt Box open or Save file  
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite((byte[])dr["VisaData"]);
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);

            }
            finally
            {
                sqlco.Close();
            }
        }

        protected void CandidateResumeDownloadLB_Click(object sender, EventArgs e)
        {
            try
            {
                sqlco.Open();
                 SqlCommand com = new SqlCommand("select ResumeFileName,ResumeFileType,ResumeData from UECandidate where @CandidateID = CandidateID", sqlco);
                 com.Parameters.AddWithValue("CandidateID", CandidateId);
                 SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = dr["ResumeFileType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dr["ResumeFileName"].ToString()); // to open file prompt Box open or Save file  
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite((byte[])dr["ResumeData"]);
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();

                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            finally
            {
                sqlco.Close();
            }
         }


        protected void FillJobApplicationGV()
        {
            try
            {

                DataTable CandidateJobApp = new DataTable();
                CandidateManagement.GetCandidateFullDetails(CandidateId, ref _Candidate, true);
                JobApplicationManager.GetJobApplicationListByCandidate(_Candidate.CandidateID, ref oJobApplication, -1);
                CandidateJobApp.Columns.AddRange(new DataColumn[7]{
                                                new DataColumn("JobApplicationID",typeof(string)),
                                                new DataColumn("VacancyID",typeof(string)),
                                                new DataColumn("CandidateID",typeof(string)),
                                                new DataColumn("ApplicationStatus",typeof(string)),
                                                new DataColumn("CompanyRemarks",typeof(string)),
                                                new DataColumn("AppliedOn",typeof(DateTime)),
                                                new DataColumn("RemarksOn",typeof(DateTime)),

                });

                for (int i = 0; i < oJobApplication.Count; i++)
                {
                    string AppStatus = string.Empty;
                    AppStatus = JobApplicationStatus.GetJobApplicationStatus(oJobApplication[i].ApplicationStatus);

                    string sCandidate = oJobApplication[i].CandidateID;
                    CandidateManagement.GetCandidateFullDetails(sCandidate, ref _Candidate);
                    
                    string sVacancyName = oJobApplication[i].VacancyID;
                    VacancyManager.GetVacancyDetails(sVacancyName, ref oVacancy);
                    CandidateJobApp.Rows.Add(oJobApplication[i].JobApplicationID,
                                             oVacancy.VacancyName,
                                             _Candidate.PersonalProfile.Name,
                                             AppStatus,
                                            CandidateStatus.GetCandidateStatus(Convert.ToInt32(_Candidate.PersonalProfile.Status)),
                                             oJobApplication[i].AppliedOn,
                                             oJobApplication[i].RemarksOn);

                    
                }

               
                JobApplicationGV.DataSource = CandidateJobApp;
                JobApplicationGV.DataBind();
               

            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }


        }


        protected void FillCandidateInterviewGV()
        {
            try
            {

                DataTable CandidateInterView = new DataTable();
                CandidateManagement.GetCandidateFullDetails(CandidateId, ref _Candidate, true);
                InterviewManagement.GetInterviewListByCandidate(_Candidate.CandidateID,ref oInterview,-1);
                CandidateInterView.Columns.AddRange(new DataColumn[7]{
                                                new DataColumn("JobApplicationID",typeof(string)),
                                                new DataColumn("InterviewStatus",typeof(string)),
                                                new DataColumn("CETRemarks",typeof(string)),
                                                new DataColumn("EmployerRemarks",typeof(string)),
                                                new DataColumn("CandidateRemarks",typeof(string)),
                                                new DataColumn("ChosenTimeZone",typeof(string)),
                                                new DataColumn("DurationInMinutes",typeof(int)),

                });

                for (int i = 0; i < oInterview.Count; i++)
                {
                    string CandInterViewSta = string.Empty;

                    CandInterViewSta = InterviewCallStatus.GetInterviewStatus(oInterview[i].InterviewStatus);
                   
                    CandidateInterView.Rows.Add(oInterview[i].JobApplicationID,
                                             CandInterViewSta,
                                             oInterview[i].CETRemarks,
                                             oInterview[i].EmployerRemarks,
                                             oInterview[i].CandidateRemarks,
                                             oInterview[i].ChosenTimeZone,
                                             oInterview[i].DurationInMinutes);

                }

                CandidateInterviewGV.DataSource = CandidateInterView;
                CandidateInterviewGV.DataBind();

            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }


        }

        protected void FillCandidateRejectionGV()
        {
            try
            {
                DataTable CandidateRejection = new DataTable();
                string CandidateRejectionID = _Candidate.CandidateID;
                CandidateRejection.Columns.AddRange(new DataColumn[7]{
                                                new DataColumn("JobApplicationID",typeof(string)),
                                                new DataColumn("VacancyID",typeof(string)),
                                                new DataColumn("CandidateID",typeof(string)),
                                                new DataColumn("ApplicationStatus",typeof(string)),
                                                new DataColumn("CompanyRemarks",typeof(string)),
                                                new DataColumn("AppliedOn",typeof(DateTime)),
                                                new DataColumn("RemarksOn",typeof(DateTime)),

                });
                for (int i = 0; i < oJobApplication.Count; i++)
                {
                    if (oJobApplication[i].ApplicationStatus == 6)
                    {
                        string sCandidate = _Candidate.CandidateID;
                        CandidateManagement.GetCandidateFullDetails(sCandidate, ref _Candidate);
                        string sVacancyName = oJobApplication[i].VacancyID;
                        VacancyManager.GetVacancyDetails(sVacancyName, ref oVacancy);

                        CandidateRejection.Rows.Add(oJobApplication[i].JobApplicationID,
                                                 oVacancy.VacancyName,
                                                 _Candidate.PersonalProfile.Name,
                                                 "Rejected",
                                                 oJobApplication[i].CompanyRemarks,
                                                 oJobApplication[i].AppliedOn,
                                                 oJobApplication[i].RemarksOn);

                        

                    }
                    

                }
                CandidateRejectionGV.DataSource = CandidateRejection;
                CandidateRejectionGV.DataBind();
            }
            catch(Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
        }

        protected void CandidateProfilePrintBTN_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"~\PrintCandidateProfile.aspx?CandidateID=" + CandidateId, false);

        }

        protected void JobApplicationGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            JobApplicationGV.PageIndex = e.NewPageIndex;
            JobApplicationGV.SelectedIndex = -1;
            FillJobApplicationGV();
        }

        protected void CandidateInterviewGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CandidateInterviewGV.PageIndex = e.NewPageIndex;
            FillCandidateInterviewGV();
        }

        protected void CandidateRejectionGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CandidateRejectionGV.PageIndex = e.NewPageIndex;
            FillCandidateRejectionGV();
        }

        protected void CandidateResumeDownloadLB_Click1(object sender, EventArgs e)
        {
            try
            {
                sqlco.Open();
                SqlCommand com = new SqlCommand("select ResumeFileName,ResumeFileType,ResumeData from UECandidate where @CandidateID = CandidateID", sqlco);
                com.Parameters.AddWithValue("CandidateID", CandidateId);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = dr["ResumeFileType"].ToString();
                    Response.AddHeader("content-disposition", "attachment;filename=" + dr["ResumeFileName"].ToString()); // to open file prompt Box open or Save file  
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite((byte[])dr["ResumeData"]);
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();

                logger.log(logger.LogSeverity.ERR, logger.LogEvents.CANDIDATE_MANAGEMENT, Session["cetrms_username"].ToString(), Message);
            }
            finally
            {
                sqlco.Close();
            }
        }

        
    }
}