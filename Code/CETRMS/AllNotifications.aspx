<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="AllNotifications.aspx.cs" Inherits="CETRMS.AllNotifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
    All Notifications
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
<div class="table-responsive">
    <asp:GridView ID="NotificationListGV" runat="server" CssClass="table table-striped jambo_table bulk_action" ShowHeader="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="NotificationListGV_PageIndexChanging" OnRowDataBound="NotificationListGV_RowDataBound">
        <HeaderStyle BackColor="#1ABB9C" ForeColor="White" Font-Bold="true"/>
            <Columns>
                <asp:BoundField HeaderText="Notification ID" />
                <asp:TemplateField HeaderText="Notification ID">
                    <ItemTemplate>
                        <asp:LinkButton ID="NotificationIDLB" runat="server" CausesValidation="false" CommandName="NotificationIDLB" OnClick="NotificationIDLB_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    </asp:GridView>
</div>
</asp:Content>
