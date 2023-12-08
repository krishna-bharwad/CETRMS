<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="ReferralDetails.aspx.cs" Inherits="CETRMS.ReferralDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="row d-flex">
    <div class="col">
        <h2>Referral Details</h2>
        </div>
        <div class="col pr-4 text-right float-right">
            <asp:ImageButton ID="ReferralPaidListPrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print"  OnClientClick="SetTarget();" OnClick="ReferralPaidList_btn"/>
        </div>
    </div>
    <hr />
    <div class="row mb-4">
        <div class="col-sm-2">
            <asp:Label ID="Status_lbl" runat="server"><h6>Status:</h6></asp:Label>
        </div>
        <div class="col-sm-4">
            <asp:DropDownList ID="ReferralStatusDDL" runat="server" AutoPostBack="True" CssClass="custom-select form-control-border" OnSelectedIndexChanged="ReferralStatusChanged">
                <asp:ListItem Value="-1">All</asp:ListItem>
                <asp:ListItem Value="1">New Candidate Signup</asp:ListItem>
                <asp:ListItem Value="2">Profile Completed</asp:ListItem>
                <asp:ListItem Value="3">Registration Fees Paid</asp:ListItem>
                <asp:ListItem Value="4">Referral Payment Completed</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="table-responsive">
        <asp:GridView ID="ReferralDetailsGV" runat="server" AutoGenerateColumns="false" CellPadding="15" CellSpacing="15" HeaderStyle-CssClass="GvHeader" OnRowDataBound="ReferralDetailsGv_RowDataBound" CssClass="table jambo_table bulk_action">
            <Columns>
                <%-- <asp:BoundField ItemStyle-Width="150px" DataField="ReferralID" HeaderText="Interview Id" />--%>

                <asp:BoundField ItemStyle-Width="150px" DataField="ReferralID" HeaderText="Referral ID" />
                <%--<asp:BoundField ItemStyle-Width="150px" DataField="CandidateName" HeaderText="Candidate Name" />--%>
                <asp:BoundField ItemStyle-Width="150px" DataField="CandidateName" HeaderText="Referred Candidate" />
                <asp:BoundField ItemStyle-Width="150px" DataField="ReferralCode" HeaderText="Referral Code" />
                <asp:BoundField ItemStyle-Width="150px" DataField="OCandidateName" HeaderText="Candidate Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="ReferralStatus" HeaderText="Referral Status" />
                <asp:TemplateField ShowHeader="True" ItemStyle-Width="150px" HeaderText="Payment Status">
                    <ItemTemplate>
                        <asp:Button ID="btn" runat="server" CausesValidation="false"  OnClientClick="hideEditFields('false');" CssClass="btn btn-sm btn-success rounded-pill" data-target="#ReferralPaymentModal" CommandArgument='<%# Eval("ReferralID") %>' OnClick="PaymentDetails_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </div>

    <!-- Modal -->
    <div class="modal fade" id="ReferralPaymentModal" tabindex="-1" role="dialog" aria-labelledby="ReferralPaymentModal" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Referral Payment</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <asp:Label runat="server" Text="Bank Account Number:" CausesValidation="false"></asp:Label>
                            <span>
                                <asp:Label runat="server" ID="BankAccountNo" CssClass="text-success" CausesValidation="false"></asp:Label>
                            </span>
                        
                        <div class="Clearfix">
                           
                                <asp:Label runat="server" Text="IFSC Code:" CausesValidation="false"></asp:Label>
                                <span>
                                    <asp:Label runat="server" ID="IFSC_lbl" CssClass="text-success" CausesValidation="false"></asp:Label>
                                </span>
                         
                        </div>
                    
                    
                        <div class="Clearfix">
                           
                                <asp:Label runat="server" Text="Referral ID:" CausesValidation="false"></asp:Label>
                                <span>
                                    <asp:Label runat="server" ID="ReferralIDlbl" CssClass="text-success" CausesValidation="false"></asp:Label>
                                </span>
                             <span>
                                    <asp:Label runat="server" ID="OldCandidateId" CssClass="text-success" CausesValidation="false" Visible="false"></asp:Label>
                                </span>
                            
                        </div>
                        </div>
                        <div class="col-6 text-right">
                               <asp:Button ID ="btn" runat = "server" CssClass="btn btn-sm btn-success rounded-pill ml-lg-5" Text ="Get Candidate Details" OnClick="GetCandidateDetails_btn"/>
                        </div>
                    </div>
                </div>
                <div class="modal-footer-referral">
                    <div class="row">
                        <div class="col-8">
                            <asp:Label runat="server" Text="Are you sure you want to transfer Referral Amount ?"  CausesValidation="false"></asp:Label>
                        </div>
                        <div class="col-4 text-right">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                            <asp:Button runat="server" Id="ReferralPaid_btn" OnClick="PaymentDone_Click" CssClass="btn btn-primary" Text="Yes"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

