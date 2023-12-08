<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerifyEmployerEmail.aspx.cs" Inherits="CETRMS.VerifyEmployerEmail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <style>
        
        .section-heading {
            margin-top: 3rem;
        }

        .vr {
            height: 240px;
            border: 1px solid #808080;
        }

        .mb-4 {
            margin-bottom: 1.0rem !important;
        }
         .border-success {
             border-color: #66acac !important;
         }
         .card {
             position: relative;
             display: -webkit-box;
             display: -ms-flexbox;
             display: flex;
             -webkit-box-orient: vertical;
             -webkit-box-direction: normal;
             -ms-flex-direction: column;
             flex-direction: column;
             min-width: 0;
             word-wrap: break-word;
             background-color: #fff;
             background-clip: border-box;
             border: 1px solid rgba(0,0,0,.125);
             border-radius: 0.25rem;
             max-width: 22rem;
         }
        
    </style>
    <div class="x_content">
        <section class="content">
            <!-- Main content -->
            <div class="section-heading mb-3">
             <div class="row md-4">
                <div class="col-12 text-center">
                   <h2> <asp:Label ID="VerificationStatusLBL" runat="server"></asp:Label> </h2>
                </div>
              </div>      
            </div>
        </section>
    </div>
   </asp:content>
