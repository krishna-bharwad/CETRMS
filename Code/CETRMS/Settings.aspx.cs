using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CETRMS
{
    public partial class Settings : System.Web.UI.Page
    {
        List<cCountry> countries = new List<cCountry>();
        List<cState> states = new List<cState>();
        public static CompanyProfile companyProfile = new CompanyProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["uerms_username"] == null)
                {
                    Response.Redirect("~/NewIndex.aspx", false);
                }
                if (!IsPostBack)
                {
                    FillCountryDDL();
                    FillStateList(CountryDDL.SelectedValue);
                    LoadPaymentData();
                    GetVisaTypeList();
                    GetCompanyDetails();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void FillCountryDDL()
        {
            try
            {
                LocationManagement.GetCountryList(ref countries);

                CountryDDL.DataSource = countries;
                CountryDDL.DataTextField = "CountryName";
                CountryDDL.DataValueField = "CountryCode";
                CountryDDL.SelectedIndex = 0;
                CountryDDL.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void FillStateList(string CountryCode)
        {
            try
            {
                LocationManagement.GetStateList(CountryCode, ref states);

                StateDDL.DataSource = states;
                StateDDL.DataTextField = "StateName";
                StateDDL.DataValueField = "StateCode";
                StateDDL.SelectedIndex = 0;
                StateDDL.DataBind();
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void CountryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStateList(CountryDDL.SelectedValue);
        }

        protected void AddCountryLB_Click(object sender, EventArgs e)
        {
            ShowModel("AddNewCountry");
        }
        protected void AddStateLB_Click(object sender, EventArgs e)
        {
            try
            {
                cCountry country = LocationManagement.GetCountryDetail(StateDDL.SelectedValue);
                NewStateCountryLBL.Text = country.CountryName;
                ShowModel("AddNewState");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EditCountryNameLB_Click(object sender, EventArgs e)
        {
            try
            {
                cCountry country = LocationManagement.GetCountryDetail(StateDDL.SelectedValue);
                EditCountryNameTXT.Text = country.CountryName;
                EditCurrencyNameTXT.Text = country.CurrencyName;
                EditCurrencySymbolTXT.Text = country.CurrencySymbol;
                ShowModel("EditCountry");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EditStateNameLB_Click(object sender, EventArgs e)
        {
            try
            {
                cState state = LocationManagement.GetStateDetail(StateDDL.SelectedValue);
                EditStateCountryNameLBL.Text = state.Country.CountryName.Trim();
                EditStateNameTXT.Text = state.StateName.Trim();
                ShowModel("EditState");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void AddStateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                cState state = new cState();
                state.Country.CountryCode = CountryDDL.SelectedValue;
                state.Country.CountryName = CountryDDL.SelectedItem.Text;
                state.StateName = NewStateNameTXT.Text.Trim();
                if (LocationManagement.AddNewState(ref state) == RetValue.Success)
                    NewStateInfoLBL.Text = "New State Added successfully. New State Code: " + state.StateCode;
                else
                    NewStateInfoLBL.Text = "New state cannot be added.";

                ShowModel("AddNewState");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void NewCountryBTN_Click(object sender, EventArgs e)
        {
            try
            {
                cState state = new cState();
                state.Country.CountryName = NewCountryNameTXT.Text.Trim();
                state.Country.CurrencyName = NewCurrencyNameTXT.Text.Trim();
                state.Country.CurrencySymbol = NewCurrencySymbolTXT.Text.Trim();
                state.StateName = NewCountryStateNameTXT.Text.Trim();
                if (LocationManagement.AddCountryName(ref state) == RetValue.Success)
                    NewCountryLBL.Text = "New Country Added. Country Code: " + state.StateCode;
                else
                    NewCountryLBL.Text = "New Country cannot be added.";
                ShowModel("AddNewCountry");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EditCountryBTN_Click(object sender, EventArgs e)
        {
            try
            {
                cCountry country = new cCountry();
                country.CountryCode = CountryDDL.SelectedValue.Substring(0, 3);
                country.CountryName = EditCountryNameTXT.Text.Trim();
                country.CurrencySymbol = EditCurrencySymbolTXT.Text.Trim();
                country.CurrencyName = EditCurrencyNameTXT.Text.Trim();
                if (LocationManagement.EditCountry(ref country) == RetValue.Success)
                    EditCountryLBL.Text = "Country details updated successfully.";
                else
                    EditCountryLBL.Text = "Country details cannot be updated.";
                ShowModel("EditCountry");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void EditStateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                cState state = new cState();
                state.StateCode = StateDDL.SelectedValue;
                state.StateName = EditStateNameTXT.Text.Trim();
                if (LocationManagement.EditStateName(ref state) == RetValue.Success)
                    EditStateLBL.Text = "State Name update successfully.";
                else
                    EditStateLBL.Text = "State Name cannot be update.";
                ShowModel("EditState");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        public void ShowModel(string ModelName)
        {
            string msg = "$(document).ready(function() {\r\n";
            msg += "$('#" + ModelName + "').modal('show');\r\n";
            msg += "});";
            String csname1 = "PopupScript";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                cs.RegisterStartupScript(cstype, csname1, msg, true);
            }
        }
        protected void LoadPaymentData()
        {
            try
            {
                DataTable PaymentMasterData = new DataTable();
                if (PaymentManagement.GetPaymentMasterData(ref PaymentMasterData, "CR") == RetValue.Success)
                {
                    foreach (DataRow row in PaymentMasterData.Rows)
                    {
                        switch (row["PaymentTypeName"].ToString())
                        {
                            case "EmployerRegistrationFee":
                                for (int i = 0; i < EmpRegCurrencyDDL.Items.Count; i++)
                                {
                                    if (EmpRegCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        EmpRegCurrencyDDL.Items[i].Selected = true;
                                    else
                                        EmpRegCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < EmpRegPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (EmpRegPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        EmpRegPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        EmpRegPaymentTypeDDL.Items[i].Selected = false;
                                }

                                EmpRegFeeAmountTXT.Text = row["Amount"].ToString();
                                EmpRegFeeTaxTXT.Text = row["Tax"].ToString();
                                EmpRegDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                            case "CandidateRegistrationFee":
                                for (int i = 0; i < CandRegCurrencyDDL.Items.Count; i++)
                                {
                                    if (CandRegCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        CandRegCurrencyDDL.Items[i].Selected = true;
                                    else
                                        CandRegCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < CandRegPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (CandRegPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        CandRegPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        CandRegPaymentTypeDDL.Items[i].Selected = false;
                                }

                                CandRegAmountTXT.Text = row["Amount"].ToString();
                                CandRegTaxTXT.Text = row["Tax"].ToString();
                                CandRegDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                            case "EmployerRecruitmentFee":
                                for (int i = 0; i < EmpRecFeeCurrencyDDL.Items.Count; i++)
                                {
                                    if (EmpRecFeeCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        EmpRecFeeCurrencyDDL.Items[i].Selected = true;
                                    else
                                        EmpRecFeeCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < EmpRecPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (EmpRecPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        EmpRecPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        EmpRecPaymentTypeDDL.Items[i].Selected = false;
                                }

                                EmpRecAmountTXT.Text = row["Amount"].ToString();
                                EmpRecTaxTXT.Text = row["Tax"].ToString();
                                EmpRecDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                            case "CandidateRecruitmentFee":
                                for (int i = 0; i < CandRecCurrencyDDL.Items.Count; i++)
                                {
                                    if (CandRecCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        CandRecCurrencyDDL.Items[i].Selected = true;
                                    else
                                        CandRecCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < CandRecPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (CandRecPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        CandRecPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        CandRecPaymentTypeDDL.Items[i].Selected = false;
                                }

                                CandRecAmountTXT.Text = row["Amount"].ToString();
                                CandRecTaxTXT.Text = row["Tax"].ToString();
                                CandRecDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                            case "EmployerRegistrationRenewal":
                                for (int i = 0; i < EmpRegRenCurrencyDDL.Items.Count; i++)
                                {
                                    if (EmpRegRenCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        EmpRegRenCurrencyDDL.Items[i].Selected = true;
                                    else
                                        EmpRegRenCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < EmpRegRenPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (EmpRegRenPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        EmpRegRenPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        EmpRegRenPaymentTypeDDL.Items[i].Selected = false;
                                }

                                EmpRegRenAmountTXT.Text = row["Amount"].ToString();
                                EmpRegRenTaxTXT.Text = row["Tax"].ToString();
                                EmpRegRenDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                            case "CandidateRegistrationRenewal":
                                for (int i = 0; i < CandRegRenCurrencyDDL.Items.Count; i++)
                                {
                                    if (CandRegRenCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        CandRegRenCurrencyDDL.Items[i].Selected = true;
                                    else
                                        CandRegRenCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < CandRegRenPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (CandRegRenPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        CandRegRenPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        CandRegRenPaymentTypeDDL.Items[i].Selected = false;
                                }

                                CandRegRenAmountTXT.Text = row["Amount"].ToString();
                                CandRegRenTaxTXT.Text = row["Tax"].ToString();
                                CandRegRenDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                        }
                    }
                }

                if (PaymentManagement.GetPaymentMasterData(ref PaymentMasterData, "DR") == RetValue.Success)
                {
                    foreach (DataRow row in PaymentMasterData.Rows)
                    {
                        switch (row["PaymentTypeName"].ToString())
                        {
                            case "CandidateReferralBonus":
                                for (int i = 0; i < CandRefBnCurrencyDDL.Items.Count; i++)
                                {
                                    if (CandRefBnCurrencyDDL.Items[i].Value == row["Currency"].ToString().Trim())
                                        CandRefBnCurrencyDDL.Items[i].Selected = true;
                                    else
                                        CandRefBnCurrencyDDL.Items[i].Selected = false;
                                }

                                for (int i = 0; i < EmpRegPaymentTypeDDL.Items.Count; i++)
                                {
                                    if (CandRefBnPaymentTypeDDL.Items[i].Value == row["AmountType"].ToString().Trim())
                                        CandRefBnPaymentTypeDDL.Items[i].Selected = true;
                                    else
                                        CandRefBnPaymentTypeDDL.Items[i].Selected = false;
                                }

                                CandRefBnAmountTXT.Text = row["Amount"].ToString();
                                CandRefBnDuePeriodTXT.Text = row["DueDays"].ToString();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void UpdateEmpRegFeeBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("EmployerRegistrationFee",
                    EmpRegFeeAmountTXT.Text.Trim(),
                    EmpRegFeeTaxTXT.Text.Trim(),
                    EmpRegPaymentTypeDDL.SelectedValue,
                    EmpRegDuePeriodTXT.Text.Trim(),
                    EmpRegCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Employer Registration Fee is updated successfully.";
                else
                    FeeInfoLBL.Text = "Employer Registration Fee cannot be updated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EmpRecUpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("EmployerRecruitmentFee",
                    EmpRecAmountTXT.Text.Trim(),
                    EmpRecTaxTXT.Text.Trim(),
                    EmpRecPaymentTypeDDL.SelectedValue,
                    EmpRecDuePeriodTXT.Text.Trim(),
                    EmpRecFeeCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Employer Recruitment Fee is updated successfully.";
                else
                    FeeInfoLBL.Text = "Emplpyer Recruitment Fee cannot be updated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void EmpRegRenUpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("EmployerRegistrationRenewal",
                    EmpRegRenAmountTXT.Text.Trim(),
                    EmpRegRenTaxTXT.Text.Trim(),
                    EmpRegRenPaymentTypeDDL.SelectedValue,
                    EmpRegRenDuePeriodTXT.Text.Trim(),
                    EmpRegRenCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Employer Registration Renewal fee updated successfully.";
                else
                    FeeInfoLBL.Text = "Employer Registration Renewal fee cannot be updated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void CandRegUpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("CandidateRegistrationFee",
                    CandRegAmountTXT.Text.Trim(),
                    CandRegTaxTXT.Text.Trim(),
                    CandRegPaymentTypeDDL.SelectedValue,
                    CandRegDuePeriodTXT.Text.Trim(),
                    CandRegCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Candidate Registration Fee updated successfully.";
                else
                    FeeInfoLBL.Text = "Candidate Registration Fee cannot be updated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void CandRecUpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("CandidateRecruitmentFee",
                    CandRecAmountTXT.Text.Trim(),
                    CandRecTaxTXT.Text.Trim(),
                    CandRecPaymentTypeDDL.SelectedValue,
                    CandRecDuePeriodTXT.Text.Trim(),
                    CandRecCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Candidate Recruitment Fee updated successfully.";
                else
                    FeeInfoLBL.Text = "Candidate Recruitment Fee cannot be updated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void CandRegRenUpdateBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("CandidateRegistrationRenewal",
                    CandRegRenAmountTXT.Text.Trim(),
                    CandRegRenTaxTXT.Text.Trim(),
                    CandRegRenPaymentTypeDDL.SelectedValue,
                    CandRegRenDuePeriodTXT.Text.Trim(),
                    CandRegRenCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Candidate Registration Renewal Fee update successfully.";
                else
                    FeeInfoLBL.Text = "Candidate Registration Renewal Fee cannot be udpated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void CandRefBnBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (PaymentManagement.UpdatePaymentMaster("CandidateReferralBonus",
                    CandRefBnAmountTXT.Text.Trim(),
                    "0",
                    CandRefBnPaymentTypeDDL.SelectedValue,
                    CandRefBnDuePeriodTXT.Text.Trim(),
                    CandRefBnCurrencyDDL.SelectedValue) == RetValue.Success)
                    FeeInfoLBL.Text = "Candidate Referral Bonus Amount update successfully.";
                else
                    FeeInfoLBL.Text = "Candidate Referral Bonus Amount cannot be udpated.";
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }

        protected void AddVisaBTN_Click(object sender, EventArgs e)
        {
            ShowModel("AddVisaType");
        }

        protected void AddVisaTypeBTN_Click(object sender, EventArgs e)
        {
            try
            {
                VisaType visaType = new VisaType();
                visaType.VisaTypeName = VisaTypeNameTXT.Text.Trim();
                visaType.VisaCountryName = AddVisaTypeCountryDDL.SelectedValue;
                visaType.VisaValidityYears = VisaValidityTXT.Text.Trim();
                visaType.VisaTypeDetails = VisaTypeDetailsTXT.Text.Trim();
                visaType.VisaStateCode = "-1";
                if (RMSMasterManagement.AddVisaTypeDetails(visaType) == RetValue.Success)
                {
                    AddVisaTypeLBL.Text = "Visa Type added successfully.";
                    GetVisaTypeList();
                }
                else
                {
                    AddVisaTypeLBL.Text = "Visa Type cannot be added";
                }
                ShowModel("AddVisaType");
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void GetVisaTypeList()
        {
            try
            {
                List<VisaType> visaTypes = new List<VisaType>();
                if (RMSMasterManagement.GetVisaTypeList(ref visaTypes) == RetValue.Success)
                {
                    DataTable VisaTypeTable = new DataTable();
                    int nCount = 1;

                    VisaTypeTable.Columns.Add("Sr. No.");
                    VisaTypeTable.Columns.Add("Visa Type Name");
                    VisaTypeTable.Columns.Add("Visa Country");
                    VisaTypeTable.Columns.Add("Visa Validity in years");

                    int cCount = VisaTypeTable.Columns.Count;

                    foreach (VisaType visaType in visaTypes)
                    {

                        VisaTypeTable.Rows.Add(
                            (nCount++).ToString(),
                            visaType.VisaTypeName,
                            visaType.VisaCountryName,
                            visaType.VisaValidityYears);
                    }
                    VisaTypeListGV.DataSource = VisaTypeTable;
                    VisaTypeListGV.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void GetCompanyDetails()
        {
            try
            {
                if (RMSMasterManagement.GetCompanyDetails(ref companyProfile) == RetValue.Success)
                {
                    CompanyEmailTXT.Text = companyProfile.SupportEmail;
                    CompanyPhoneTXT.Text = companyProfile.SupportContactNumber;
                    InvoiceNameTXT.Text = companyProfile.CompanyBillingName;
                    InvoiceAddressTXT.Text = companyProfile.BillingAddress;
                    InvoiceDistrictTXT.Text = companyProfile.BillingDistrict;
                    InvoiceStateTXT.Text = companyProfile.BillingState;
                    InvoiceCountryTXT.Text = companyProfile.BillingCountry;
                    GSTNoTXt.Text = companyProfile.GSTNumber;
                    CompanyMobileTXT.Text = companyProfile.WhatsaAppMobileNo;
                    CompanyWebURLLBL.Text = companyProfile.CompanyWebURL;

                    SupportContactNoTXT.Text = companyProfile.SupportContactNumber;
                    NoReplyEmailTXT.Text = companyProfile.NoReplyEmail;
                    SupportEmailTXT.Text = companyProfile.SupportEmail;
                    NewsLetterEmailTXT.Text = companyProfile.NewsLetterEmail;
                    AccountsEmailTXT.Text = companyProfile.AccountsEmail;
                    InfoEmailTXT.Text = companyProfile.InfoEmail;
                    ReferralRegistrationLinkTXT.Text = companyProfile.ReferralRegistrationLink;
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
        protected void UpdateCompanyProfileBTN_Click(object sender, EventArgs e)
        {
            try
            {
                companyProfile.SupportEmail = CompanyEmailTXT.Text;
                companyProfile.SupportContactNumber = CompanyPhoneTXT.Text;
                companyProfile.CompanyBillingName = InvoiceNameTXT.Text;
                companyProfile.BillingAddress = InvoiceAddressTXT.Text;
                companyProfile.BillingDistrict = InvoiceDistrictTXT.Text;
                companyProfile.BillingState = InvoiceStateTXT.Text;
                companyProfile.BillingCountry = InvoiceCountryTXT.Text;
                companyProfile.GSTNumber = GSTNoTXt.Text;
                companyProfile.WhatsaAppMobileNo = CompanyMobileTXT.Text;
                companyProfile.CompanyWebURL = CompanyWebURLLBL.Text;

                companyProfile.SupportContactNumber = SupportContactNoTXT.Text;
                companyProfile.NoReplyEmail = NoReplyEmailTXT.Text;
                companyProfile.SupportEmail = SupportEmailTXT.Text;
                companyProfile.NewsLetterEmail = NewsLetterEmailTXT.Text;
                companyProfile.AccountsEmail = AccountsEmailTXT.Text;
                companyProfile.InfoEmail = InfoEmailTXT.Text;
                companyProfile.ReferralRegistrationLink = ReferralRegistrationLinkTXT.Text;
                if (RMSMasterManagement.UpdateCompanyDetails(companyProfile) == RetValue.Success)
                {
                    CompanyProfileInfoLBL.Text = "Company Profile updated successfully.";
                }
            }
            catch (Exception ex)
            {
                string Message = "Error: " + ex.Message + "\r\n";
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                Message = Message + t.ToString();
                logger.log(logger.LogSeverity.ERR, logger.LogEvents.WEBPAGE, Session["uerms_username"].ToString(), Message);
            }
        }
    }
}