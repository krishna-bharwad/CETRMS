<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="UpcomingInterviews.aspx.cs" Inherits="CETRMS.UpcomingInterviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
    <style>
        .InterviewListPage
        {
            font-family: 'Poppins', sans-serif;
        }
        .InterviewListTable
        {
            border:none;
            border-collapse: collapse;
            border-color: white;
            border-width: 0;
        }
        .InterviewListRows
        {
            height: 50px;
        }
        .InterviewListRows:hover
        {
            box-shadow: 10px 2px 52px 7px rgba(102,172,172,0.83);
            -webkit-box-shadow: 10px 2px 52px 7px rgba(102,172,172,0.83);
            -moz-box-shadow: 10px 2px 52px 7px rgba(102,172,172,0.83);
        }
        #calendar{
            max-height:33rem;
            justify-content:space-evenly;
            display:block;
        }
        #PageContent_InterviewListPanel{
            max-height:33rem;
        }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
<div class="InterviewListPage">
    <div class="row">
        <div class="col-11 d-flex">
            <img src="images/ProposeInterview.png" alt="ProposedInterview" width="35" height="35" />      
            <h2><b>Interview Calender</b></h2>
        </div>
    </div>
    <div class="clearfix"></div>
    <hr />
    <div class="row mb-4">
            <div class="col-sm-2">
                <asp:Label ID="EmployerName_lbl"  runat ="server"><h6>Employer Name:</h6></asp:Label>
            </div>
       
        <div class="col-sm-4">
            <asp:DropDownList ID="EmployerDDL" runat="server" AutoPostBack="True" CssClass="custom-select form-control-border" OnSelectedIndexChanged="Refreshinterview">
            </asp:DropDownList>
                </div>
            <div class="col-sm-2">
                <asp:Label ID="VacancyName_lbl" runat="server"><h6>Interview Status:</h6></asp:Label>
            </div>
            
            <div class="col-sm-4">
            <asp:DropDownList ID="InterviewStatus" runat="server" AutoPostBack="True" CssClass="custom-select form-control-border" OnSelectedIndexChanged="Refreshinterview">
                <asp:ListItem Value="-1">All</asp:ListItem>
                
                <asp:ListItem Value="2">Interview Scheduled</asp:ListItem>
                <asp:ListItem Value="3">Interview Started</asp:ListItem>
                <asp:ListItem Value="4">Interview Completed</asp:ListItem>
                <asp:ListItem Value="5">Interview Dropped</asp:ListItem>
                <asp:ListItem Value="6">Interview Cancelled</asp:ListItem>
                <asp:ListItem Value="7">Interview Rejected</asp:ListItem>
            </asp:DropDownList>
            </div>
        </div>
    <div class="row ">
        <div class="col-md-4">
            <asp:Panel ID="InterviewListPanel" runat="server" ScrollBars="Vertical">
            <asp:GridView ID="gridService" runat="server" AutoGenerateColumns="false" CssClass="table InterviewListTable" RowStyle-CssClass="InterviewListRows" cellspacing="0" cellpadding="0" OnRowCommand="gridService_OnRowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="True" HeaderText="Scheduled Zoom Meetings">
                        <ItemTemplate>
                                    <asp:LinkButton ID="Button_Text" runat="server" CausesValidation="false" CommandName="InterviewDetails" CommandArgument='<%# Eval("InterviewID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <div class="text-center mt-4">
                <h4><asp:Label runat="server" ID="InterviewNotScheduled_txt"></asp:Label></h4>
            </div>
            </asp:Panel>
        </div>


        <div class="col-md-8">
            <div class="row">
                <div class="col-md-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>Calendar Events <small>Sessions</small></h2>
                        </div>
                        <div class="x_content">
                            <div id='calendar' class="table-responsive"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
