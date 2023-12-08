<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchVacancyList.aspx.cs" Inherits="CETRMS.SearchVacancyList" %>

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
    <%--<div class="section-heading mb-5">
            <div class="row mb-4">
                <div class="col-12 text-center"><h2>Search Candidate</h2></div>
            </div>
            <div class="row mb-4">
                <div class="col-2">
                    <label class="text-nowrap p-2">Country:</label> 
                    </div>
                <div class="col-4">
                    <asp:DropDownList ID="CountryDDL" runat="server" CssClass="custom-select form-control" AutoPostBack="true" OnSelectedIndexChanged="CountryDDL_SelectedIndexChanged"></asp:DropDownList>
                    
                    </div>
                <div class="col-2">
                    <label class="text-nowrap p-2">State:</label> 
                    </div>
                <div class="col-4">
                    <asp:DropDownList ID="StateDDL" runat="server" AutoPostBack="true" CssClass="custom-select form-control" OnSelectedIndexChanged="StateDDL_SelectedIndexChanged" ></asp:DropDownList>
                    </div>
                </div>
           

            <div class="card-body pb-0">
                <div class="row">
                    <asp:Literal ID="CandidateListLit" runat="server"></asp:Literal>
                </div>
            </div>
           
              <div class="x_content">
                                                                
               </div>
        </div>--%>

    <div class="x_content">
        <section class="content">
            <!-- Main content -->
            <div class="section-heading mb-3">
             <div class="row md-4">
                <div class="col-12 text-center">
                   <h2> Search Vacancy </h2>
                </div>
                </div>
                <div class="row md-4">
                    <div class="col-2">
                        Select Country:
                    </div>
                    <div class="col-4">
                        <asp:DropDownList ID="CountryDDL" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="CountryDDL_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-2">
                        Select State:
                    </div>
                    <div class="col-4">
                        <asp:DropDownList ID="StateDDL" runat="server" CssClass="custom-select" AutoPostBack="true" OnSelectedIndexChanged="StateDDL_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

               <div class="card-body pb-0">
                <div class="row">
                    <asp:Literal ID="VacancyListLit" runat="server"></asp:Literal>
                </div>   
            </div>      
               </div>
        </section>
    </div>
   </asp:content>
