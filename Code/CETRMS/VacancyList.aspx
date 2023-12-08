<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="VacancyList.aspx.cs" Inherits="CETRMS.VacancyList" %>
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
    <div class="col-11"><h5>Vacancy List</h5></div>
    <div class="col-1"><asp:ImageButton ID="VacancyListPrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print"  OnClientClick="SetTarget();" OnClick="VacancyListPrintBTN_Click"/></div>
    </div>
<hr />
      <!-- Main content -->
    <div class="row">
        <div class="col-sm-2">
          <label class="text-nowrap p-2">Vacancy Status:</label>
        </div>
            <div class="col-sm-4">
                
                <asp:DropDownList ID="VacancyLitstTabDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="True" OnSelectedIndexChanged="VacancyLitstTabDDL_SelectedIndexChanged">
                    <asp:ListItem Value="0">Total Vacancies</asp:ListItem>
                    <asp:ListItem Value="1">Open Vacancies</asp:ListItem>
                    <asp:ListItem Value="5">Vacancies Under Scheduled Interview</asp:ListItem>
                    <asp:ListItem Value="4">Close Vacancies After Final Selection</asp:ListItem>
                </asp:DropDownList>
            </div>
        
        <div class="col-sm-2">
        <label class="p-2">Location:</label>     
        </div>
            <div class="col-sm-4">              
                <asp:DropDownList ID="EmployerLocationDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="True" OnSelectedIndexChanged="EmployerLocationDDL_SelectedIndexChanged">
                </asp:DropDownList>                         
            </div>
       
    </div>
<asp:UpdatePanel ID="CardsPanel" runat="server">
    <ContentTemplate>
    <div class="card-body pb-0">
<%--        <asp:Panel ID="testPanel" runat="server" Visible="false">
        <div class="row">
            <div class="col-6">
                Test
            </div>
            <div class="col-6">
                Test01
            </div>
        </div>
        </asp:Panel>--%>
      <div class="row">
          <asp:Literal ID="VacancyListLit" runat="server">

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
