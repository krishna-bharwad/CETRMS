<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="NewCandidateRegistrationList.aspx.cs" Inherits="CETRMS.NewCandidateRegistrationList" %>
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
       <%-- <img src="https://img.icons8.com/external-flaticons-lineal-color-flat-icons/90/null/external-receive-amount-online-money-service-flaticons-lineal-color-flat-icons.png" height="35" width="35"/>--%>
        <h5 class="p-2">List Of New Candidate Registration</h5></div>
    <%--<div class="col-1"><asp:ImageButton ID="PrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print" OnClientClick="SetTarget();" OnClick="PrintBTN_Click"/></div>--%>
</div>

    <div class="clearfix"></div>
<div class="row mb-4">
    <div class="col-12 table-responsive">
        <asp:GridView ID="NewCanRegListGV" runat="server" CssClass="table jambo_table bulk_action"  CellPadding ="3" CellSpacing ="3" RowStyle-CssClass="GvGrid" HeaderStyle-CssClass="GvHeader"  AllowPaging="true" PageSize="10" OnPageIndexChanging="NewCanRegListGV_PageIndexChanging">
           <%--<Columns>
               
              <asp:TemplateField>
                 
                  <ItemTemplate>
                      <asp:Image ID="CandidateImage"  runat="server" ImageUrl='<%# Eval("AuthenticationProfileURL") %>' />
                      <asp:Image Src="data:image/jpg;base64"  ID="image1" alt="" class="img-circle img-fluid " runat="server" ImageUrl='<%# Eval("AuthenticationProfileURL")%>'/>
                  </ItemTemplate>
              </asp:TemplateField>
           </Columns>--%>
            
        </asp:GridView>
    </div>
</div>
</asp:Content>
