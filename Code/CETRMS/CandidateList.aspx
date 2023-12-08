<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="CandidateList.aspx.cs" Inherits="CETRMS.CandidateList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .button-25 {
                border: none;
                padding: 11px 15px 12px;
                background:white;
                color:#66acac;
                font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                font-size:medium;
            }
        .button-active-25 {
                border: none;
                padding: 11px 15px 12px;
                background:#66acac;
                color:white;
                font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
                font-size:medium;
                border-radius: 50%;
            }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <section class="content">

    <div class="row">
    <div class="col-11"><h5>Candidate List</h5></div>
    <div class="col-1"><asp:ImageButton ID="PrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print"  OnClientClick="SetTarget();"  OnClick="PrintBTN_Click" /></div>
    </div>
<hr />


      <!-- Main content -->
    <div class="row">
        <div class="col-sm-2">
            <label class="text-nowrap p-2">Candidate Status:</label>
        </div>
        <div class="col-sm-4">
            <asp:DropDownList ID="CandidateListTabDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="True" OnSelectedIndexChanged="CandidateListTabDDL_SelectedIndexChanged">
            <%--<asp:ListItem  Value="1">New Registration</asp:ListItem>--%>
<%--            <asp:ListItem  Value="0">Candidate Completed Details</asp:ListItem>
            <asp:ListItem  Value="3">Registration Fee Paid</asp:ListItem>
            <asp:ListItem  Value="4">Applied to any Vacancy</asp:ListItem>
            <asp:ListItem  Value="5">Candidate Under Selection Process</asp:ListItem>
            <asp:ListItem  Value="6">Interview Appeared</asp:ListItem>
            <asp:ListItem  Value="14">Candidate Recruitment Fee Due</asp:ListItem>
            <asp:ListItem  Value="8">Candidate Final Selected</asp:ListItem>
            <asp:ListItem  Value="9">Candidate Rejected</asp:ListItem>
            <asp:ListItem  Value="11">Registration Renewal Pending</asp:ListItem>
            <asp:ListItem  Value="12">Registration Renewal Fee Paid</asp:ListItem>--%> 
            </asp:DropDownList>     
        </div>
        <div class="col-sm-2">
            <label class="p-2">Location:</label>
        </div>
        <div class="col-sm-4">
            <asp:DropDownList ID="LocationDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="True" OnSelectedIndexChanged="LocationDDL_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
<asp:UpdatePanel ID="CardsPanel" runat="server">
    <ContentTemplate>
    <div class="card-body pb-0">
      <div class="row">
          <asp:Literal ID="CandidateListLit" runat="server">

          </asp:Literal>
      </div>
    </div>
    <div class="text-center">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        </asp:PlaceHolder>
    </div>
        </ContentTemplate>
</asp:UpdatePanel>
</section>
</asp:Content>
