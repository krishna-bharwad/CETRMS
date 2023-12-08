<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="InterviewList.aspx.cs" Inherits="CETRMS.InterviewList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
     <style>
        .ZoomMeetingsPage
        {
            font-family: 'Poppins', sans-serif;
            font-size:medium;
        }
     .GvHeader
     {
         background-color: #1ABB9C;
         color:white;
         font-size:medium;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 20px 10px -20px;
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
    <section class="content">
        <div class="row">
    <div class="col-11"><h5>Interview List</h5></div>
   
   <div class="col-1"><asp:ImageButton ID="InterviewListPrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print"  OnClientClick="SetTarget();" OnClick="InterviewListPrintBTN_Click"/></div>
    </div>
        <div class="row">
            <div class="col-6">
                
            <label class="text-nowrap p-2">Interview Status:</label>
       
            </div>
            <div class="col-6">
                <asp:DropDownList ID="InterviewStatus" runat="server" AutoPostBack="true" CssClass="custom-select form-control-border" 
                    OnSelectedIndexChanged="InterviewStatus_SelectedIndexChanged">
                   <%-- <asp:ListItem Value="-1">All</asp:ListItem>--%>
                
                <asp:ListItem Value="1">Interview Proposed</asp:ListItem>
                <asp:ListItem Value="2">Interview Scheduled</asp:ListItem>
                <asp:ListItem Value="3">Interview Started</asp:ListItem>
                <asp:ListItem Value="5">Interview Dropped</asp:ListItem>
                <asp:ListItem Value="6">Interview Cancelled</asp:ListItem>
                <asp:ListItem Value="7">Interview Rejected</asp:ListItem>
                <asp:ListItem Value="4">Interview Completed</asp:ListItem>

                </asp:DropDownList>
            </div>
            <div class="table-responsive">
                <%--<asp:GridView ID="InterviewListGV" runat="server" AutoGenerateColumns="True" CellPadding="15" CellSpacing="15" HeaderStyle-CssClass="GvHeader"  
                    CssClass="table jambo_table bulk_action" AllowPaging="true"  OnRowDataBound="InterviewListGV_RowDataBound">--%>
                   <%-- <Columns>
                        <asp:BoundField ItemStyle-Width="150px" DataField="InterviewID" HeaderText="Interview ID" />
                            <asp:TemplateField  HeaderText="Interview ID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="InterviewID" runat="server" CausesValidation="false" CommandName="InterviewID" OnClick="InterviewID_Click"></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                         
                        <asp:BoundField ItemStyle-Width="150px" DataField="InterviewStatus" HeaderText="Interview Status" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="CandidateRemarks" HeaderText="Candidate Remarks" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="ChosenTimeZone" HeaderText="Chosen TimeZone" />
                         <asp:BoundField ItemStyle-Width="150px" DataField="EmployerRemarks" HeaderText="Employer Remarks" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="PreferredDateTime" HeaderText="Preferred Date Time" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="DurationInMinutes" HeaderText="Duration InMinutes" />
                    </Columns>--%>
               <%-- </asp:GridView>--%>

                <asp:GridView ID="GVInterviewList" runat="server" AutoGenerateColumns="true" CellPadding="15" CellSpacing="15" HeaderStyle-CssClass="GvHeader"
                    CssClass="table jambo_table bulk_action" OnRowDataBound="GVInterviewList_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVInterviewList_PageIndexChanging"></asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>
