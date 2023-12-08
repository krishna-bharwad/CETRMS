<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="ZoomMeetings.aspx.cs" Inherits="CETRMS.ZoomMeetings" %>

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
    <div class="ZoomMeetingsPage">
    <%--<table>
        <tbody>
            <tr>
                <th>ZoomMeetings Details</th>
            </tr>
        </tbody>
    </table>--%>
     <div class="table-responsive">
    <asp:GridView ID="gridService" runat="server" AutoGenerateColumns="false" CellPadding="15" CellSpacing="15" HeaderStyle-CssClass="GvHeader" OnRowDataBound="ZoomMeetingsGV_RowDataBound" CssClass="table jambo_table bulk_action">
        <Columns>
            <asp:BoundField ItemStyle-Width="400px" DataField="InterviewID" HeaderText="InterviewID" />
            <asp:BoundField ItemStyle-Width="400px" DataField="Sr no" HeaderText="Sr no " />
            <asp:BoundField ItemStyle-Width="400px" DataField="ZoomMeetingId" HeaderText="ZoomMeetingID " />
            <asp:TemplateField HeaderText="ZoomMeeting ID">
                <ItemTemplate>
                    <asp:LinkButton ID="ZoomMeetingId" runat="server" CausesValidation="false" CommandName="ZoomMeetingId" OnClick="ZoomMeetingLink_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="1000px" DataField="Topic" HeaderText="Topic" />
            <asp:BoundField ItemStyle-Width="500px" DataField="StartTime" HeaderText="Start Time" />
            <asp:BoundField ItemStyle-Width="500px" DataField="TimeZone" HeaderText="TimeZone" />
            <asp:BoundField ItemStyle-Width="500px" DataField="InterviewStatus" HeaderText="Interview Status" />

        </Columns>
    </asp:GridView>
         </div>
</div>
</asp:Content>
