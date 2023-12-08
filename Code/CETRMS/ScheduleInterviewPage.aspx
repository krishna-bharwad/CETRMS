<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="ScheduleInterviewPage.aspx.cs" Inherits="CETRMS.ScheduleInterviewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
    <style>
        .ProposeInterviewListPage
        {
            font-family: 'Poppins', sans-serif;
            font-size: medium;
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
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="ProposeInterviewListPage">
    <div class="row">
        <div class="col-11 d-flex">
            <img src="images/ProposeInterview.png" alt="ProposedInterview" width="35" height="35" />   
            <h5 class="p-2">Proposed Interview</h5>
        </div>
    </div>
    <div class="clearfix"></div>
    <hr />
    <div class="row mb-4">
            <div class="col-sm-2 p-2">
                <asp:Label ID="EmployerName_lbl"  runat ="server">Employer Name:</asp:Label>
            </div>
       
        <div class="col-sm-4">
            <asp:DropDownList ID="EmployerDDL" runat="server" AutoPostBack="True" CssClass="custom-select form-control-border" OnSelectedIndexChanged="EmployerDDL_SelectedIndexChanged">
            </asp:DropDownList>
                </div>
            <div class="col-sm-2 p-2">
                <asp:Label ID="VacancyName_lbl" runat="server">Vacancy Name:</asp:Label>
            </div>
            
            <div class="col-sm-4">
                <asp:DropDownList ID="VacancyDDL" runat="server" AutoPostBack="True" CssClass="custom-select form-control-border" OnSelectedIndexChanged="VacancyDDL_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

    <div class="table-responsive">
        <asp:GridView ID="ProposedInterviewsGV" runat="server" AutoGenerateColumns="false" CellPadding="15" CellSpacing="15" OnRowDataBound="ScheduleInterviewGv_RowDataBound" HeaderStyle-CssClass="GvHeader" CssClass="table jambo_table bulk_action" >
            <Columns>
                <asp:BoundField ItemStyle-Width="150px" DataField="InterviewID" HeaderText="Interview Id" />
                <asp:TemplateField ShowHeader="True" ItemStyle-Width="150px" HeaderText="Interview ID">
                    <ItemTemplate>
                        <asp:LinkButton ID="btn" runat ="server" CausesValidation="false" CommandName="InterviewID" OnClick="InterviewID_OnClick"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="150px" DataField="EmployerName" HeaderText="Employer Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="CandidateName" HeaderText="Candidate Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="VacancyName" HeaderText="Vacancy Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="DurationInMinutes" HeaderText="Duration In Minutes" />
                <asp:BoundField ItemStyle-Width="150px" DataField="ChosenTimeZone" HeaderText="Chosen Time Zone" />

                
            </Columns>
        </asp:GridView>
         <div class="text-center mt-4">
            <h4><asp:Label runat="server" id ="InterviewNotProposed_txt"></asp:Label></h4>
            </div>
    </div>
</div>
</asp:Content>

