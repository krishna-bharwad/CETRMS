<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="CETRMS.PaymentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
    <style>
        .PaymentDetailsPage
        {
            font-family: 'Poppins', sans-serif;
            font-size:medium;
        }
        .InvoiceTableHeader{
            background-color:blue;
            color:white;
        }
     .GvHeader
     {
         background-color: #66acac;
         color:white;
         font-size:medium;
         box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
         border-radius: 10px;
     }
     .GvGrid
        {
         white-space: normal;
         word-break: break-all;
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
    Payment Details
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="PaymentDetailsPage">
    <hr />
    <div class="row mb-2">
            <div class="col-sm-2">
                <h6>Client :</h6>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="ClientNameLBL" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row mb-2">
            <div class="col-sm-2">
                <h6>Payment ID:</h6>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="PaymentIdLBL" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2">
                <h6>Payment Order No.:</h6>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="PayOrderNoLBL" runat="server"></asp:Label>
            </div>
        </div>
    <div class="row mb-2">
        <div class="col-sm-2">
            <h6>Payment Type:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="PaymentTypeLBL" runat="server"></asp:Label>
        </div>
        <div class="col-sm-2">
            <h6>Payment Status:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="PaymentStatusLBL" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-sm-2">
            <h6>Amount:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="AmountLBL" runat="server"></asp:Label>
        </div>
        <div class="col-sm-2">
            <h6>Tax:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="TaxAmountLBL" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row mb-2">
        <div class="col-sm-2">
            <h6>Total Amount:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="TotalAmountLBL" runat="server"></asp:Label>
        </div>
        <div class="col-sm-2">
            <h6>Due Date:</h6>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="DueDateLBL" runat="server"></asp:Label>
        </div>
    </div>
<%--    <div class="row mb-2">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
            <asp:Button ID="GenerateInvoicePDFBTN" runat="server" CssClass="btn btn-primary" Text="Download Invoice PDF" OnClick="DownloadpdfBTN_Click" />
        </div>
    </div>--%>
    <hr />
    <asp:Panel ID="PaymentLogInfoPanel" runat="server">
        <div class="row">
            <div class="col-12">
                Payment Log
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:GridView ID="PaymentLogInfoGV" runat="server" OnRowDataBound="PaymentLogInfoGV_RowDataBound" CssClass="table" RowStyle-CssClass="GvGrid" HeaderStyle-CssClass="GvHeader"></asp:GridView>
            </div>
        </div>
    </asp:Panel>
    <hr />
    <asp:Panel ID="InvoicePanel" runat="server">
        <div class="row mb-2">
            <div class="col-6">
                <asp:Image ID="UELogoIMG" runat="server" ImageUrl="~/IndexAssets/images/UE-LOGO-2.png" Width="180px" Height="60px" />
            </div>
            <div class="col-6 text-right">
                <h3>
                    INVOICE
                </h3>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-sm">
                <asp:Label ID="BillingAddressLBL" runat="server"></asp:Label>
            </div>
            <div class="col-sm"></div>
        </div>
        <div class="row mb-2">
            <div class="col-sm">
                <asp:Label ID="BillingRegionLBL" runat="server"></asp:Label>
            </div>
            <div class="col-sm"></div>
        </div>
        <div class="row mb-2">
            <div class="col-sm-5 mb-2">
                <asp:Label ID="CompanyContactLBL" runat="server"></asp:Label>
            </div>
            <div class="col-sm-1 mb-2"></div>
            <div class="col-sm-6 mb-2">
                <div class="row">
                    <div class="col-6 text-center InvoiceTableHeader">Invoice #</div>
                    <div class="col-6 text-center InvoiceTableHeader">Date</div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-sm-5 mb-2"></div>
            <div class="col-sm-1 mb-2"></div>
            <div class="col-sm-6 mb-2">
                <div class="row">
                    <div class="col-6 text-center">
                        <asp:Label ID="UEInvoiceNoLBL" runat="server"><asp:Label ID="InvoiceLBL" runat="server"></asp:Label></asp:Label>
                    </div>
                    <div class="col-6 text-center">
                        <asp:Label ID="UEInvoiceDateLBL" runat="server"><asp:Label ID="PaymentDateLBL" runat="server"></asp:Label></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-sm-5">
                Bill To
            </div>
            <div class="col-sm-1"></div>
            <div class="col-sm-6">
                <div class="row">
                    <div class="col-6 text-center InvoiceTableHeader">Payment Order No</div>
                    <div class="col-6 text-center InvoiceTableHeader">Terms</div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <asp:Label ID="NameLBL" runat="server"></asp:Label>
            </div>
            <div class="col-1"></div>
            <div class="col-6">
                <div class="row">
                    <div class="col-6 text-center"><asp:Label ID="PaymentOrderNoLBL" runat="server"></asp:Label></div>
                    <div class="col-6 text-center">Payment Receipt</div>
                </div>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <asp:Label ID="BusinessNameLBL" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <asp:Label ID="AddressLBL" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-5">
                <asp:Label ID="StateLBL" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-12">
                <asp:GridView ID="PaymentDetailsGV" runat="server" CssClass="table" HeaderStyle-CssClass="InvoiceTableHeader" OnRowDataBound="PaymentDetailsGV_RowDataBound">
                </asp:GridView>
            </div>
        </div>
        <section class="content invoice">
            <div class="row no-print justify-content-center">
                <div class="col-6 col-sm-4">
                    <asp:LinkButton ID="GeneratePDFLB" runat="server" CssClass="btn btn-primary btn-block text-nowrap" Text="<i class='fa fa-download'></i>Generate PDF" OnClick="DownloadpdfBTN_Click"></asp:LinkButton>
                </div>
            </div>
            
        </section>
    </asp:Panel>

</asp:Content>
