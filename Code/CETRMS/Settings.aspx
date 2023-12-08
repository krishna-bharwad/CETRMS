<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="CETRMS.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        nav {
    display: -ms-flexbox;
    display: flex;
    -ms-flex-wrap: wrap;
    flex-wrap: wrap;
    padding-left: 0;
    margin-bottom: 0;
    list-style: none;
}
   .nav-tabs1 .nav-item.show .nav-link, .nav-tabs .nav-link.active {
    color: #495057;
    /*background-color: #fff;
    border-color: #dee2e6 #dee2e6 #fff;*/
}
  .nav-tabs1 .nav-link {
    border: 1px solid transparent;
    border-top-left-radius: 0.25rem;
    border-top-right-radius: 0.25rem;
}
   
   ul.bar_tabs1 >li a {
    padding: 10px 17px;
    background: #F5F7FA;
    margin: 0;
    border-top-right-radius: 0;
}

   ul.bar_tabs1 {
    overflow: visible;
    background: #F5F7FA;
    height: 40px;
    margin: 21px 0 14px;
    padding-left:0px;
    position: relative;
    z-index: 1;
    width: 100%;
    border-bottom: 1px solid #E6E9ED;
}
   h2{
       font-weight:500;
       line-height:1.2;
   }

     .GvHeader
     {
         background-color: #1ABB9C;
         color:white;
         font-size:medium;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
         border-radius: 10px;
     }
     .GvGrid:hover
        {
         background-color: #1ABB9C;
         color:white;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
        }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="row">
        <div class="col-12">
            <h2><i class="fa fa-gear"></i> CETRMS Settings </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs1 bar_tabs1 mb-5" id="myTab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="CETRMSMasters-tab" data-toggle="tab" href="#CETRMSMasters" role="tab" aria-controls="CETRMSMasters" aria-selected="true">CETRMS Masters</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="fees-tab" data-toggle="tab" href="#fees" role="tab" aria-controls="fees" aria-selected="false">Fee / Charges</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="configuration-tab" data-toggle="tab" href="#configuration" role="tab" aria-controls="profile" aria-selected="false">Company Billing Details</a>
                </li>
            </ul>
            <div class="tab-content" id="myTabContent">
                <div class="tab-pane fade active show" id="CETRMSMasters" role="tabpanel" aria-labelledby="CETRMSMasters-tab">
                    <div class="row mb-3">
                        <div class="col-sm-12">
                            <h5>Location Management:</h5>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-2 mb-2">
                            <h6>Country:</h6>
                        </div>
                        <div class="col-sm-4 mb-2">
                            <asp:DropDownList ID="CountryDDL" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="CountryDDL_SelectedIndexChanged"></asp:DropDownList>
                            <p></p>
                            <div class="row mb-6">
                                <div class="col-6">
                                    <asp:LinkButton ID="EditCountryNameLB" runat="server" OnClick="EditCountryNameLB_Click">Edit Country Details</asp:LinkButton>
                                </div>
                                <div class="col-6 text-right">
                                    <asp:LinkButton ID="AddCountryLB" runat="server" OnClick="AddCountryLB_Click">+ Add New Country</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2 mb-2">
                            <h6>State:</h6>
                        </div>
                        <div class="col-sm-4 mb-2">
                            <asp:DropDownList ID="StateDDL" runat="server" CssClass="custom-select"></asp:DropDownList>
                            <p />
                            <div class="row">
                                <div class="col-6">
                                    <asp:LinkButton ID="EditStateNameLB" runat="server" OnClick="EditStateNameLB_Click">Edit State Name</asp:LinkButton>
                                </div>
                                <div class="col-6 text-right">
                                    <asp:LinkButton ID="AddStateLB" runat="server" OnClick="AddStateLB_Click">+ Add New State</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-10">
                            <h5>Visa Type</h5> <asp:Label ID="VisaTypeInfoLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="AddVisaBTN" runat="server" Text="Add Visa Type" CssClass="btn-sm btn-primary" OnClick="AddVisaBTN_Click" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <asp:GridView ID="VisaTypeListGV" runat="server" CssClass="table table-hover" HeaderStyle-CssClass="GvHeader" RowStyle-CssClass="GvGrid"></asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="configuration" role="tabpanel" aria-labelledby="configuration-tab">
                    <asp:UpdatePanel ID="ConfigurationUpdatePanel" runat="server">
                    <ContentTemplate>
                        <h2>
                            <b>Company Billing Details</b>
                        </h2>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    Invoice Name:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="InvoiceNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    Invoice Address:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="InvoiceAddressTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        City:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="InvoiceCityTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        District:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="InvoiceDistrictTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        State:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="InvoiceStateTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        Country:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="InvoiceCountryTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        GST No:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="GSTNoTXt" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                    <hr />
                        <h2 >
                            <b>Contact Details</b>
                        </h2>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        Email:
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="CompanyEmailTXT" runat="server" CssClass="form-control" Wrap="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        Web URL:
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="CompanyWebURLLBL" runat="server" CssClass="form-control" Wrap="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-2">
                                        Mobile No:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="CompanyMobileTXT" runat="server" CssClass="form-control" Wrap="true"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        Landline/IP No:
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="CompanyPhoneTXT" runat="server" CssClass="form-control" Wrap="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-3">
                                        Support Contact No.:
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="SupportContactNoTXT" runat="server" CssClass="form-control" Wrap="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-sm-4">
                                        Referral Registration Weblink:
                                    </div>
                                    <div class=" col-sm-8">
                                        <asp:TextBox ID="ReferralRegistrationLinkTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                    <hr />
                        <h2>
                            <b>Emails</b>
                        </h2>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    No Reply Email:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="NoReplyEmailTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    Support Email:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="SupportEmailTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    New Letter Email:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="NewsLetterEmailTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    Accounts Email:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="AccountsEmailTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-sm-2">
                                    Info Email:
                                </div>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="InfoEmailTXT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                    <div class="row mb-2 justify-content-center">
                        <div class="col-md-4">
                            <asp:Button ID="UpdateCompanyProfileBTN" runat="server" CssClass="btn btn-primary" Text="Update Details" OnClick="UpdateCompanyProfileBTN_Click" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <asp:Label ID="CompanyProfileInfoLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                <div class="tab-pane fade" id="fees" role="tabpanel" aria-labelledby="fees-tab">
                    <asp:UpdatePanel ID="FeeUpdatePanel" runat="server">
                    <ContentTemplate>
                    <div class="row mb-3">
                        <div class="col-sm-12">
                            <h5>Fees / Charges</h5> <asp:Label ID="FeeInfoLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3"><b>Fee Type</b></div>
                        <div class="col-1"><b>Currency</b></div>
                        <div class="col-3"><b>Payment Type</b></div>
                        <div class="col-1"><b>Amount / Percent</b></div>
                        <div class="col-1"><b>Tax (%)</b></div>
                        <div class="col-2"><b>Due Period in days</b></div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Employer Registration
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="EmpRegCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="EmpRegPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRegFeeAmountTXT" runat="server" TextMode="Number" min="0" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRegFeeTaxTXT" runat="server" TextMode="Number" min="0" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="EmpRegDuePeriodTXT" runat="server" TextMode="Number" min="0" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="UpdateEmpRegFeeBTN" runat="server" Text="update" OnClick="UpdateEmpRegFeeBTN_Click" OnClientClick="return ValidateEmpRegFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Employer Recruitment Fee
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="EmpRecFeeCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="EmpRecPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRecAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRecTaxTXT" runat="server" TextMode="Number" min="0" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="EmpRecDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="EmpRecUpdateBTN" runat="server" Text="update" OnClick="EmpRecUpdateBTN_Click"  OnClientClick="return ValidateEmpRecFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Employer Registration Renewal
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="EmpRegRenCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="EmpRegRenPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRegRenAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="EmpRegRenTaxTXT" runat="server" TextMode="Number" min="0" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="EmpRegRenDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="EmpRegRenUpdateBTN" runat="server" Text="update" OnClick="EmpRegRenUpdateBTN_Click"  OnClientClick="return ValidateEmpRegRenFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Candidate Registration Fee
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="CandRegCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="CandRegPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRegAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRegTaxTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="CandRegDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="CandRegUpdateBTN" runat="server" Text="update" OnClick="CandRegUpdateBTN_Click"  OnClientClick="return ValidateCandRegFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Candidate Recruitment Fee
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="CandRecCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="CandRecPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRecAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRecTaxTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="CandRecDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="CandRecUpdateBTN" runat="server" Text="update" OnClick="CandRecUpdateBTN_Click"  OnClientClick="return ValidateCandRecFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Candidate Registration Renewal
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="CandRegRenCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="CandRegRenPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRegRenAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="CandRegRenTaxTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="CandRegRenDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="CandRegRenUpdateBTN" runat="server" Text="update" OnClick="CandRegRenUpdateBTN_Click"  OnClientClick="return ValidateCandRegRenFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <h5>Candidate Referral Bonus</h5>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-3">
                            Candidate Referral Bonus
                        </div>
                        <div class="col-1">
                            <asp:DropDownList ID="CandRefBnCurrencyDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="USD">USD</asp:ListItem>
                                <asp:ListItem Value="INR">INR</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="CandRefBnPaymentTypeDDL" runat="server" CssClass="custom-select">
                                <asp:ListItem Value="amount">Amount</asp:ListItem>
                                <asp:ListItem Value="percent">Percent of Salary</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="CandRefBnAmountTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="CandRefBnDuePeriodTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Button ID="CandRefBnBTN" runat="server" Text="update" OnClick="CandRefBnBTN_Click" OnClientClick="return ValidateCandRefBnFeeControls();" CssClass="btn-sm btn-primary" />
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    

    <!-- Add Country Modal -->
    <div class="modal fade" id="AddNewCountry" tabindex="-1" role="dialog" aria-labelledby="AddNewCountrylabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="AddNewCountryTitle">Add New Country</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div class="row mb-4">
                    <div class="col-3">
                        New Country
                    </div>
                    <div class="col-9">
                        <asp:TextBox ID="NewCountryNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-2">
                        Currency Symbol
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="NewCurrencySymbolTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-2">
                        Currency Name
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="NewCurrencyNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-3">
                        New State
                    </div>
                    <div class="col-9">
                        <asp:TextBox ID="NewCountryStateNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4"></div>
                    <div class="col-4">
                        <asp:Button ID="NewCountryBTN" runat="server" Text="Add Country" CssClass="btn btn-primary" OnClick="NewCountryBTN_Click" OnClientClick="return ValidateNewCountryControls();"/>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Label ID="NewCountryLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

    <!-- Add State Modal -->
    <div class="modal fade" id="AddNewState" tabindex="-1" role="dialog" aria-labelledby="AddNewStatelabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="AddNewStateTitle">Add New State</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div class="row mb-4">
                    <div class="col-3">
                        Country:
                    </div>
                    <div class="col-9">
                        <asp:Label ID="NewStateCountryLBL" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-3">
                        New State
                    </div>
                    <div class="col-9">
                        <asp:TextBox ID="NewStateNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4"></div>
                    <div class="col-4">
                        <asp:Button ID="AddStateBTN" runat="server" Text="Add State" CssClass="btn btn-primary" OnClick="AddStateBTN_Click"  OnClientClick="return ValidateNewStateControls();"/>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Label ID="NewStateInfoLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

        <!-- Edit Country Modal -->
    <div class="modal fade" id="EditCountry" tabindex="-1" role="dialog" aria-labelledby="EditCountrylabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="EditCountryTitle">Edit Country</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div class="row mb-4">
                    <div class="col-3">
                        Country Name
                    </div>
                    <div class="col-9">
                        <asp:TextBox ID="EditCountryNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-2">
                        Currency Symbol
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="EditCurrencySymbolTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-2">
                        Currency Name
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="EditCurrencyNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4"></div>
                    <div class="col-4">
                        <asp:Button ID="EditCountryBTN" runat="server" Text="Update Country" CssClass="btn btn-primary" OnClick="EditCountryBTN_Click" OnClientClick="return ValidateEditCountryControls();"/>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Label ID="EditCountryLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

    <!-- Edit State Modal -->
    <div class="modal fade" id="EditState" tabindex="-1" role="dialog" aria-labelledby="EditStatelabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="EditStateTitle">Edit State</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div class="row mb-4">
                    <div class="col-3">
                        Country:
                    </div>
                    <div class="col-9">
                        <asp:Label ID="EditStateCountryNameLBL" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-3">
                        State Name
                    </div>
                    <div class="col-9">
                        <asp:TextBox ID="EditStateNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4"></div>
                    <div class="col-4">
                        <asp:Button ID="EditStateBTN" runat="server" Text="Update State" CssClass="btn btn-primary" OnClick="EditStateBTN_Click" OnClientClick="return ValidateEditStateControls();"/>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Label ID="EditStateLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

    <!-- Add Visa Type Modal -->
    <div class="modal fade" id="AddVisaType" tabindex="-1" role="dialog" aria-labelledby="AddVisaTypelabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
            <h5 class="modal-title" id="AddVisaTypeTitle">Add Visa Type</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
            </div>
            <div class="modal-body">
                <div class="row mb-4">
                    <div class="col-3">
                        Country:
                    </div>
                    <div class="col-9">
                        <asp:DropDownList ID="AddVisaTypeCountryDDL" runat="server" CssClass="custom-select">
                            <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                            <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                            <asp:ListItem Value="Australia">Australia</asp:ListItem>
                            <asp:ListItem Value="Austria">Austria</asp:ListItem>
                            <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                            <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                            <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                            <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                            <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                            <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                            <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                            <asp:ListItem Value="Belize">Belize</asp:ListItem>
                            <asp:ListItem Value="Benin">Benin</asp:ListItem>
                            <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                            <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                            <asp:ListItem Value="Bosnia and Herzegovina">Bosnia and Herzegovina</asp:ListItem>
                            <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                            <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                            <asp:ListItem Value="Brunei">Brunei</asp:ListItem>
                            <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                            <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                            <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                            <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                            <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                            <asp:ListItem Value="Canada">Canada</asp:ListItem>
                            <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                            <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                            <asp:ListItem Value="Chad">Chad</asp:ListItem>
                            <asp:ListItem Value="Chile">Chile</asp:ListItem>
                            <asp:ListItem Value="China">China</asp:ListItem>
                            <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                            <asp:ListItem Value="Congo">Congo</asp:ListItem>
                            <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                            <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                            <asp:ListItem Value="Croatia">Croatia</asp:ListItem>
                            <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                            <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                            <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                            <asp:ListItem Value="Democratic Republic of the Congo">Democratic Republic of the Congo</asp:ListItem>
                            <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                            <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                            <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                            <asp:ListItem Value="Dominique">Dominique</asp:ListItem>
                            <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
                            <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                            <asp:ListItem Value="Egytp">Egytp</asp:ListItem>
                            <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                            <asp:ListItem Value="Equatorial Guinea" >Equatorial Guinea </asp:ListItem>
                            <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                            <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                            <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                            <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                            <asp:ListItem Value="Finland">Finland</asp:ListItem>
                            <asp:ListItem Value="France">France</asp:ListItem>
                            <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                            <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                            <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                            <asp:ListItem Value="Germany">Germany</asp:ListItem>
                            <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                            <asp:ListItem Value="Greece">Greece</asp:ListItem>
                            <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                            <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                            <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                            <asp:ListItem Value="Guinea-Bissau">Guinea-Bissau</asp:ListItem>
                            <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                            <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                            <asp:ListItem Value="Holy See">Holy See</asp:ListItem>
                            <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                            <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                            <asp:ListItem Value="Iceland">Iceland</asp:ListItem>
                            <asp:ListItem Value="India">India</asp:ListItem>
                            <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                            <asp:ListItem Value="Iran">Iran</asp:ListItem>
                            <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                            <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                            <asp:ListItem Value="Israel">Israel</asp:ListItem>
                            <asp:ListItem Value="Italy">Italy</asp:ListItem>
                            <asp:ListItem Value="Ivory Coast">Ivory Coast</asp:ListItem>
                            <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                            <asp:ListItem Value="Japan">Japan</asp:ListItem>
                            <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                            <asp:ListItem Value="Kazakhstán">Kazakhstán</asp:ListItem>
                            <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                            <asp:ListItem Value="Kirgyzstan">Kirgyzstan</asp:ListItem>
                            <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                            <asp:ListItem Value="Korea">Korea</asp:ListItem>
                            <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                            <asp:ListItem Value="Laos">Laos</asp:ListItem>
                            <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                            <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                            <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                            <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                            <asp:ListItem Value="Libya">Libya</asp:ListItem>
                            <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                            <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                            <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                            <asp:ListItem Value="Macedonia" >Macedonia </asp:ListItem>
                            <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                            <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                            <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                            <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                            <asp:ListItem Value="Mali">Mali</asp:ListItem>
                            <asp:ListItem Value="Malta">Malta</asp:ListItem>
                            <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                            <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                            <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                            <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                            <asp:ListItem Value="Micronesia">Micronesia</asp:ListItem>
                            <asp:ListItem Value="Moldova">Moldova</asp:ListItem>
                            <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                            <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                            <asp:ListItem Value="Montenegro">Montenegro</asp:ListItem>
                            <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                            <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                            <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                            <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                            <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                            <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                            <asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
                            <asp:ListItem Value="New Zeland">New Zeland</asp:ListItem>
                            <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                            <asp:ListItem Value="Níger">Níger</asp:ListItem>
                            <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                            <asp:ListItem Value="North Korea">North Korea</asp:ListItem>
                            <asp:ListItem Value="Norway">Norway</asp:ListItem>
                            <asp:ListItem Value="Oman">Oman</asp:ListItem>
                            <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                            <asp:ListItem Value="Palau">Palau</asp:ListItem>
                            <asp:ListItem Value="Palestine">Palestine</asp:ListItem>
                            <asp:ListItem Value="Panama">Panama</asp:ListItem>
                            <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                            <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                            <asp:ListItem Value="Peru">Peru</asp:ListItem>
                            <asp:ListItem Value="Philippines">Philippines</asp:ListItem>
                            <asp:ListItem Value="Poland">Poland</asp:ListItem>
                            <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                            <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                            <asp:ListItem Value="Rumanía">Rumanía</asp:ListItem>
                            <asp:ListItem Value="Russia">Russia</asp:ListItem>
                            <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                            <asp:ListItem Value="Saint Kitts and Neviss">Saint Kitts and Neviss</asp:ListItem>
                            <asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
                            <asp:ListItem Value="Saint Vincent and the Grenadines">Saint Vincent and the Grenadines</asp:ListItem>
                            <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                            <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                            <asp:ListItem Value="Sao Tomé and Príncipe">Sao Tomé and Príncipe</asp:ListItem>
                            <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                            <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                            <asp:ListItem Value="Serbia">Serbia</asp:ListItem>
                            <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                            <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                            <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                            <asp:ListItem Value="Slovak Republic">Slovak Republic</asp:ListItem>
                            <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                            <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                            <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                            <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                            <asp:ListItem Value="South Sudan">South Sudan</asp:ListItem>
                            <asp:ListItem Value="Spain">Spain</asp:ListItem>
                            <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                            <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                            <asp:ListItem Value="Surinam">Surinam</asp:ListItem>
                            <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                            <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                            <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                            <asp:ListItem Value="Syria">Syria</asp:ListItem>
                            <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                            <asp:ListItem Value="Tanzania">Tanzania</asp:ListItem>
                            <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                            <asp:ListItem Value="The United States of America">The United States of America</asp:ListItem>
                            <asp:ListItem Value="Togo">Togo</asp:ListItem>
                            <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                            <asp:ListItem Value="Trinidad and Tobago">Trinidad and Tobago</asp:ListItem>
                            <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                            <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                            <asp:ListItem Value="Turkmeinetan">Turkmeinetan</asp:ListItem>
                            <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                            <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                            <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                            <asp:ListItem Value="Union of Comoros">Union of Comoros</asp:ListItem>
                            <asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
                            <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                            <asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
                            <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                            <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                            <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                            <asp:ListItem Value="Vietnam">Vietnam</asp:ListItem>
                            <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                            <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                            <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4">
                        Visa Type Name:
                    </div>
                    <div class="col-8">
                        <asp:TextBox ID="VisaTypeNameTXT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4">
                        Visa Validity (in years):
                    </div>
                    <div class="col-8">
                        <asp:TextBox ID="VisaValidityTXT" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        Visa Type Details:
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:TextBox ID="VisaTypeDetailsTXT" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-4"></div>
                    <div class="col-4">
                        <asp:Button ID="AddVisaTypeBTN" runat="server" Text="Add Visa Type" CssClass="btn-sm btn-primary" OnClick="AddVisaTypeBTN_Click" />
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-12">
                        <asp:Label ID="AddVisaTypeLBL" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>

        </div>
    </div>


    <script>
        function ValidateNewCountryControls() {
            var NewCountryNameTXT = document.getElementById('PageContent_NewCountryNameTXT');
            var NewCurrencySymbolNameTXT = document.getElementById('PageContent_NewCurrencySymbolTXT');
            var NewCurrencyNameTXT = document.getElementById('PageContent_NewCurrencyNameTXT');
            var NewCountryStateNameTXT = document.getElementById('PageContent_NewCountryStateNameTXT');

            let NewCountryNameText = NewCountryNameTXT.value;
            let NewCurrencySymbolNameText = NewCurrencySymbolNameTXT.value;
            let NewCurrencyNameText = NewCurrencyNameTXT.value;
            let NewCountryStateNameText = NewCountryStateNameTXT.value;

            if (NewCountryNameText.trim() == "") {
                alert("New Country Name cannot be blank.");
                return false;
            }
            if (NewCurrencySymbolNameText.trim() == "") {
                alert("New Currency Symbol Name cannot be blank.");
                return false;
            }
            if (NewCurrencyNameText.trim() == "") {
                alert("New Currency Name cannot be blank.");
                return false;
            }
            if (NewCountryStateNameText.trim() == "") {
                alert("New State Name cannot be blank.");
                return false;
            }
            if (NewCurrencySymbolNameText.trim().length != 1) {
                alert("Curreny Symbol cannot be of more then 1 character.");
                return false;
            }
        }
        function ValidateNewStateControls() {
            var NewStateNameTXT = document.getElementById('PageContent_NewStateNameTXT');

            let NewStateNameText = NewStateNameTXT.value;

            if (NewStateNameText.trim() == "") {
                alert("New State Name cannot be blank.");
                return false;
            }
        }
        function ValidateEditCountryControls() {
            var NewCountryNameTXT = document.getElementById('PageContent_EditCountryNameTXT');
            var NewCurrencySymbolNameTXT = document.getElementById('PageContent_EditCurrencySymbolTXT');
            var NewCurrencyNameTXT = document.getElementById('PageContent_EditCurrencyNameTXT');

            let NewCountryNameText = NewCountryNameTXT.value;
            let NewCurrencySymbolNameText = NewCurrencySymbolNameTXT.value;
            let NewCurrencyNameText = NewCurrencyNameTXT.value;

            if (NewCountryNameText.trim() == "") {
                alert("Country Name cannot be blank.");
                return false;
            }
            if (NewCurrencySymbolNameText.trim() == "") {
                alert("Currency Symbol Name cannot be blank.");
                return false;
            }
            if (NewCurrencyNameText.trim() == "") {
                alert("Currency Name cannot be blank.");
                return false;
            }
            if (NewCurrencySymbolNameText.trim().length > 7) {
                alert("Curreny Symbol cannot be of more then 7 character.");
                return false;
            }
        }
        function ValidateEditStateControls() {
            var NewStateNameTXT = document.getElementById('PageContent_EditStateNameTXT');

            let NewStateNameText = NewStateNameTXT.value;

            if (NewStateNameText.trim() == "") {
                alert("State Name cannot be blank.");
                return false;
            }
        }
        function ValidateEmpRegFeeControls() {
            var EmpRegFeeAmountTXT = document.getElementById('PageContent_EmpRegFeeAmountTXT');
            var EmpRegDuePeriodTXT = document.getElementById('PageContent_EmpRegDuePeriodTXT');

            let EmpRegFeeAmountText = EmpRegFeeAmountTXT.value;
            let EmpRegDuePeriodText = EmpRegDuePeriodTXT.value;

            if (EmpRegFeeAmountText.trim() == "") {
                alert("Employee Registration fee cannot be blank.");
                return false;
            }
            if (EmpRegDuePeriodText.trim() == "") {
                alert("Employee Registration fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(EmpRegFeeAmountText.trim()) < 0) {
                alert("Employee Registration fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(EmpRegDuePeriodText.trim()) < 1) {
                alert("Employee Registration fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateEmpRecFeeControls() {
            var EmpRecFeeAmountTXT = document.getElementById('PageContent_EmpRecAmountTXT');
            var EmpRecDuePeriodTXT = document.getElementById('PageContent_EmpRecDuePeriodTXT');

            let EmpRecFeeAmountText = EmpRecFeeAmountTXT.value;
            let EmpRecDuePeriodText = EmpRecDuePeriodTXT.value;

            if (EmpRecFeeAmountText.trim() == "") {
                alert("Employee Recruitment fee cannot be blank.");
                return false;
            }
            if (EmpRecDuePeriodText.trim() == "") {
                alert("Employee Recruitment fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(EmpRecFeeAmountText.trim()) < 0) {
                alert("Employee Recruitment fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(EmpRecDuePeriodText.trim()) < 1) {
                alert("Employee Recruitment fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateEmpRegRenFeeControls() {
            var EmpRegRenAmountTXT = document.getElementById('PageContent_EmpRegRenAmountTXT');
            var EmpRegRenDuePeriodTXT = document.getElementById('PageContent_EmpRegRenDuePeriodTXT');

            let EmpRegRenAmountText = EmpRegRenAmountTXT.value;
            let EmpRegRenDuePeriodText = EmpRegRenDuePeriodTXT.value;

            if (EmpRegRenAmountText.trim() == "") {
                alert("Employee Registration Renewal fee cannot be blank.");
                return false;
            }
            if (EmpRegRenDuePeriodText.trim() == "") {
                alert("Employee Registration Renewal fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(EmpRegRenAmountText.trim()) < 0) {
                alert("Employee Registration Renewal fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(EmpRegRenDuePeriodText.trim()) < 1) {
                alert("Employee Registration Renewal fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateCandRegFeeControls() {
            var CandRegAmountTXT = document.getElementById('PageContent_CandRegAmountTXT');
            var CandRegDuePeriodTXT = document.getElementById('PageContent_CandRegDuePeriodTXT');

            let CandRegAmountText = CandRegAmountTXT.value;
            let CandRegDuePeriodText = CandRegDuePeriodTXT.value;

            if (CandRegAmountText.trim() == "") {
                alert("Candidate Registration fee cannot be blank.");
                return false;
            }
            if (CandRegDuePeriodText.trim() == "") {
                alert("Candidate Registration fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(CandRegAmountText.trim()) < 0) {
                alert("Candidate Registration fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(CandRegDuePeriodText.trim()) < 1) {
                alert("Candidate Registration fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateCandRecFeeControls() {
            var CandRecAmountTXT = document.getElementById('PageContent_CandRecAmountTXT');
            var CandRecDuePeriodTXT = document.getElementById('PageContent_CandRecDuePeriodTXT');

            let CandRecAmountText = CandRecAmountTXT.value;
            let CandRecDuePeriodText = CandRecDuePeriodTXT.value;

            if (EmpRegFeeAmountText.trim() == "") {
                alert("Candidate Recruitment fee cannot be blank.");
                return false;
            }
            if (CandRecDuePeriodText.trim() == "") {
                alert("Candidate Recruitment fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(CandRecAmountText.trim()) < 0) {
                alert("Candidate Recruitment fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(CandRecDuePeriodText.trim()) < 1) {
                alert("Candidate Recruitment fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateCandRegRenFeeControls() {
            var CandRegRenAmountTXT = document.getElementById('PageContent_CandRegRenAmountTXT');
            var CandRegRenDuePeriodTXT = document.getElementById('PageContent_CandRegRenDuePeriodTXT');

            let CandRegRenAmountText = CandRegRenAmountTXT.value;
            let CandRegRenDuePeriodText = CandRegRenDuePeriodTXT.value;

            if (CandRegRenAmountText.trim() == "") {
                alert("Candidate Registration Renewal fee cannot be blank.");
                return false;
            }
            if (CandRegRenDuePeriodText.trim() == "") {
                alert("Candidate Registration Renewal fee due period cannot be blank.");
                return false;
            }
            if (parseFloat(CandRegRenAmountText.trim()) < 0) {
                alert("Candidate Registration Renewal  fee cannot be less then 0.");
                return false;
            }
            if (parseFloat(CandRegRenDuePeriodText.trim()) < 1) {
                alert("Candidate Registration Renewal  fee due period cannot be less then 1.");
                return false;
            }
        }
        function ValidateCandRefBnFeeControls() {
            var CandRefBnAmountTXT = document.getElementById('PageContent_CandRefBnAmountTXT');
            var CandRefBnDuePeriodTXT = document.getElementById('PageContent_CandRefBnDuePeriodTXT');

            let CandRefBnAmountText = CandRefBnAmountTXT.value;
            let CandRefBnDuePeriodText = CandRefBnDuePeriodTXT.value;

            if (CandRefBnAmountText.trim() == "") {
                alert("Candidate Referral Bonus amount cannot be blank.");
                return false;
            }
            if (CandRefBnDuePeriodText.trim() == "") {
                alert("Candidate Referral Bonus amount due period cannot be blank.");
                return false;
            }
            if (parseFloat(CandRefBnAmountText.trim()) < 0) {
                alert("Candidate Referral Bonus amount cannot be less then 0.");
                return false;
            }
            if (parseFloat(CandRefBnDuePeriodText.trim()) < 1) {
                alert("Candidate Referral Bonus amount due period cannot be less then 1.");
                return false;
            }
        }
    </script>
</asp:Content>
