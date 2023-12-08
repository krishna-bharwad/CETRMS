<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="CETRMS.PaymentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
     .GvHeader
     {
         background-color: #66acac;
         color:white;
         font-size:medium;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
         border-radius: 10px;
     }
     .GvGrid:hover
        {
         background-color: #66acac;
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
    <div class="col-11 d-flex">
        <img src="https://img.icons8.com/external-flaticons-lineal-color-flat-icons/90/null/external-receive-amount-online-money-service-flaticons-lineal-color-flat-icons.png" height="35" width="35"/>
        <h5 class="p-2">Payments</h5></div>
    <div class="col-1"><asp:ImageButton ID="PrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print" OnClick="PrintBTN_Click" /></div>
</div>
    <hr />
<div class="row mb-4">
    <div class="col-sm-2"><label class="text-mowrap p-2">Payment Status:</label></div>
    <div class="col-sm-4">
        <asp:DropDownList ID="PaymentStatusDDL" runat="server" AutoPostBack="true" CssClass="custom-select form-control-border" OnSelectedIndexChanged="PaymentStatusDDL_SelectedIndexChanged">
            <asp:ListItem Value="-1">All</asp:ListItem>
            <asp:ListItem Value ="1">Payment Due Within Time</asp:ListItem>
            <asp:ListItem Value="2">Payment Due Out Of Time</asp:ListItem>
            <asp:ListItem Value="4">Payment Done</asp:ListItem>
            <asp:ListItem Value="5">Payment Cancelled</asp:ListItem>
            <asp:ListItem Value="7">Payment Failed</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="col-sm-2"><label class="text-mowrap p-2">Payment Type:</label></div>
    <div class="col-sm-4">
        <asp:DropDownList ID="PaymentTypeDDL" runat="server" AutoPostBack="true" CssClass="custom-select form-control-border" OnSelectedIndexChanged="PaymentTypeDDL_SelectedIndexChanged">
            <asp:ListItem Value="-1">All</asp:ListItem>
            <asp:ListItem Value ="1">Employer Registration Fee</asp:ListItem>
            <asp:ListItem Value="2">Candidate Registration Fee</asp:ListItem>
            <asp:ListItem Value="3">Employer Recruitment Fee</asp:ListItem>
            <asp:ListItem Value="4">Candidate Recruitment Fee</asp:ListItem>
            <asp:ListItem Value="5">Employer Registration Renewal</asp:ListItem>
            <asp:ListItem Value="6">Candidate Registration Renewal</asp:ListItem>
        </asp:DropDownList>    </div>
</div>
<div class="clearfix"></div>
<div class="row mb-4">
    <div class="col-12 table-responsive">
        <asp:GridView ID="PaymentListGV" runat="server" CssClass="table jambo_table bulk_action" RowStyle-CssClass="GvGrid" HeaderStyle-CssClass="GvHeader" OnRowDataBound="PaymentListGV_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Sr.No." />
                <asp:TemplateField HeaderText="Payment Order No">
                    <ItemTemplate>
                        <asp:LinkButton ID="PayOrderNoLB" runat="server" CausesValidation="false" CommandName="PayOrderNoLB" OnClick="LinkButton1_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>
</asp:Content>
