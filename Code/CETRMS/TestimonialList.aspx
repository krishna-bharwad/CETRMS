<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="TestimonialList.aspx.cs" Inherits="CETRMS.TestimonialList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
     .GvHeader
     {
         background-color: #1ABB9C;
         color:white;
         font-size:medium;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
         border-radius: 10px;
     }
/*     .GvGrid:hover
        {
         background-color: #1ABB9C;
         color:white;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
        }*/
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="row">
        <div class="col-12 d-flex">
            <img src="https://img.icons8.com/external-flaticons-lineal-color-flat-icons/64/null/external-testimonial-night-club-flaticons-lineal-color-flat-icons-3.png" height="35" width="35"/>
            <h5 class="p-2">Testimonials</h5></div>
    </div>
    <hr />
    <div class="row">
        <div class="col-sm">
            Select Rating:
        </div>
        <div class="col-sm">
            <asp:ImageButton ID="OneStarIMB" runat="server" ImageUrl="~/images/one-star.png" Width="100px" Height="25px" />
        </div>
        <div class="col-sm">
            <asp:ImageButton ID="TwoStarIMB" runat="server" ImageUrl="~/images/two-star.png" Width="100px" Height="25px" />
        </div>
        <div class="col-sm">
            <asp:ImageButton ID="ThreeStarIMB" runat="server" ImageUrl="~/images/three-star.png" Width="100px" Height="25px" />
        </div>
        <div class="col-sm">
            <asp:ImageButton ID="FourStarIMB" runat="server" ImageUrl="~/images/four-star.png" Width="100px" Height="25px" />
        </div>
        <div class="col-sm">
            <asp:ImageButton ID="FiveStarIMB" runat="server" ImageUrl="~/images/five-star.png" Width="100px" Height="25px" />
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <asp:GridView ID="TestimonialsGV" runat="server" OnRowDataBound="TestimonialsGV_RowDataBound" border="0" CssClass="table table-responsive" RowStyle-CssClass="GvGrid" HeaderStyle-CssClass="GvHeader" >
            <Columns>
                <asp:TemplateField HeaderText="Rating">
                    <ItemTemplate>
                        <asp:image ID="RatingIMG" runat="server" Width="100px" Height="25px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Showing">
                    <ItemTemplate>
                        <asp:ImageButton ID="IsShowingImg" runat="server" Width="25px" Height="25px" OnClick="IsShowingImg_Click"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
