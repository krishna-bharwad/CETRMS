<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="EmployerList.aspx.cs" Inherits="CETRMS.EmployerList" %>
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
            <div class="col-11">
                <h5>Employer List</h5>
            </div>
            <div class="col-1">
                <asp:ImageButton ID="PrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print" OnClientClick="SetTarget()" OnClick="PrintBTN_Click"/>

            </div>
            </div>
        <hr />
        <div class="row">
            <div class="col-sm-2">
                <label class="text-nowrap p-2">Employer Status:</label>
            </div>
            <div class="col-sm-4">
                <asp:DropDownList ID="EmployerListTabDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="true" OnSelectedIndexChanged="EmployerListTabDDL_SelectedIndexChanged">
                   
<%--                    <asp:ListItem Value="-1">Employer On Boarded</asp:ListItem>
                    <asp:ListItem Value="2">Registration Fee Due</asp:ListItem>
                    <asp:ListItem Value="3">Employers With No Vacancy</asp:ListItem>
                    <asp:ListItem Value="4">Active Employer With Open Vacancy</asp:ListItem>
                    <asp:ListItem Value="5">InProcess Vacancies</asp:ListItem>
                    <asp:ListItem Value="6">Recruitment Fee Paid</asp:ListItem>
                    <asp:ListItem Value="7">Inactive Employer With Filled Vacacny</asp:ListItem>
                    <asp:ListItem Value="8">Registration Renewal Fee Due</asp:ListItem>
                    <asp:ListItem Value="9">Registration Renewed</asp:ListItem>--%>
                    
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <label class="text-nowrap p-2">Location:</label>
            </div>
            <div class="col-sm-4">
                <asp:DropDownList ID="LocationDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="true" OnSelectedIndexChanged="LocationDDL_SelectedIndexChanged">

                </asp:DropDownList>
            </div>
        </div>
        <asp:UpdatePanel ID="CardsPanel" runat="server">
            <ContentTemplate>
            <div class="card-body pb-0">
            <div class="row">

                <asp:Literal ID="EmployerListLiteral" runat="server">

                </asp:Literal>
            </div>

        </div>
        <div class="text-center">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </section>
</asp:Content>
