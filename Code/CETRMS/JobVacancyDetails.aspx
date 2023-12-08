<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="JobVacancyDetails.aspx.cs" Inherits="CETRMS.JobVacancyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
    <style>
        .CandidateDetailsPage
        {
            font-family: 'Poppins', sans-serif;
        }
        h3{

            font-size:16px;
        }
        h5{
            font-size:16px;
        }
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
        




      /* SIDECARD CSS STARTS*/

         .vacancyMainRow{
             height: 100vh !important;
             
         }



        .sideCard {
            /*border: 1px solid black;*/
            height: 60vh;
            margin: 0rem 0 0rem 0.5rem;
            position: relative;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-wrap: wrap;
        }

            .sideCard .cardBox {
               
                position: relative;
                width: 30rem;
                height: 30rem;
                margin: 3rem;
            }
                .sideCard .cardBox:hover .imgBox {
                    transform: translate(/*-3.5rem, -1.9rem*/ );
                    height: 18rem;
                    padding: 14px 14px 0px 14px !important;
                }
                .sideCard .cardBox:hover .contentBox {
                    transform: translate(-1.2rem, 13.5rem);
                    height: 20rem;
                    line-height: 0.8rem;
                    width: 100%;
                    overflow: visible;
                    margin-top: 1.5rem;
                    border-radius: 20px;
                }


        .imgBox {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            box-shadow: 0 0 8px 5px #b6b5b5;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 20rem;
            padding: 14px;
            z-index: 2;
            transition: all 0.5s ease-in-out;
            background-color: white;
            border-radius: 15px;
        }
        .profile-user-img {
            width: 70%;
            height: 7rem;
            object-fit: cover;
            resize: both;
            border-radius: 13px;
            margin-top: -3em;
        }

        .profile-username{
            font-size: 1.3rem;
        }

        .contentBox {
            position: absolute;
            overflow: hidden;
            top: 0;
            left: 0;
            width: 100%;
            height: 20rem;
            padding: 0.95rem;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            background-color: #F5F8F9 /*#f0faf8*/ /* #66acac*/;
            z-index: 1;
            align-items: flex-end;
            flex-wrap: wrap;
            text-align: center;
            transition: 0.5s ease-in-out;
            /*box-shadow: 0px 0px 9px 3px*/ /*#e6e6e6*/ #66acac inset;
            /*border: 2px solid #e6e6e6;*/
        }
        .employerDetails {
            display: flex;
            align-items: center;
            flex-direction: row;
            flex-wrap: nowrap;
            justify-content: space-evenly;
            color: /*#1c94a4 #60968c*/ #10535c!important;
            font-size: 15px;
            padding-left: 9px;
        }
        .EmployerStatus{
            color: #1c94a4 !important;
        }

         .dateData .employerDetails{
            padding-left: 1.5rem;
        }

        #employerAddress{

            /*border: 1px solid black;*/
        }
    


        /*.innerContentBox > h4{
             border-bottom: 1px solid grey;
        }*/
        .listItem{
            /*border-bottom: 1px solid darkgrey;*/
            list-style: none;
            padding: 6px;
            display: flex;
            flex-wrap: wrap;
            flex-direction: row;
            justify-content: space-between;
        }
        .innerContentBox {
            display: flex;
            flex-wrap: wrap;
            flex-direction: column;
            justify-content: flex-start;
            align-items: center;
            /*background-color: white;*/
            /*backdrop-filter: blur(50px);*/
            /*box-shadow: 0 0 4px 2px #66acac;*/
            height: 90%;
            margin-top: 10%;
            width: 100%;
            overflow-x: scroll;
            /*padding: 16px;*/
        }
        .contentBox h4 {
            display: block;
            font-size: 15px;
            text-align: initial;
            color: #111 /*#d5fcf4*/;
            font-weight: bold;
            line-height: 1rem;
            /*letter-spacing: 1px;*/
        }
        .contentBox span {
            color: /*#1c94a4*/ #111;
            font-weight:;
            text-align: justify-all;
        }
        .descList {
            height: 100%;
            background-color: black !important;
        }
        ul.LowerCardContent {
            padding: 0;
            list-style: none;
        }

        /*ul.LowerCardContent li .message_wrapper {
            margin-left: 10px;
            margin-right: 10px;
            align-content:flex-start;
           display: flex;
           justify-content: flex-start;
           align-content: center;
           flex-direction: row;
           flex-wrap: wrap;
        }*/

        .message_wrapper{
            display: flex;
           justify-content: space-around;
           align-content: center;
           flex-direction: row;
           /*font-size: 12px;*/
        }

        .message{
            /*border: 1px solid red;*/
            display: flex;
            align-items: center;
            justify-content: flex-start;
            padding-left: 2px;
        }

        .dateData{
            display: flex;
            flex-direction: column;

        }


           .message_wrapper h4.heading {
                font-weight: 600;
                font-size: 15px;
                margin: 0;
                cursor: pointer;
                margin-bottom: 10px;
                line-height: 100%;
                color: #10535c;
            }

            .dateData h4.heading{
                /*border: 1px solid black;*/
                margin-bottom: 0rem;
            }


            ul.LowerCardContent li .message_wrapper blockquote {
                padding: 0px 0px;
                margin: 0;
                /*border-left: 5px solid #eee;*/
            }
        /*485-690 695-915 920-1280*/

/* 540,  768, 912, 1204-1280*/





        @media only screen and (min-width:250px) and (max-width: 480px) {

            .vacancyMainRow{
                height: auto !important;
            }
            
            .tableCard {
                margin-top: 4rem;
            }
            .sideCard {
                margin-top: 4rem;
                padding: 0;
                margin-bottom: 7rem;
            }
            .contentBox {
                top: 50px;
                height: 5rem;
            }

            .innerContentBox {
                width: 100%;
                padding: 2px;
                height: 85%;
                margin-top: 51px;
                overflow-x: scroll;
            }
            .listItem{
                padding: 3px;
            }
            .cardBox {
                /*border: 1px solid grey;*/
                width: 100% !important;
            }
            .sideCard .cardBox:hover .contentBox {
                transform: translate(-0.1rem, 3.6rem);
                width: 120%;
                height: ;
                margin-left: -1.2rem;
            }
            .sideCard .cardBox:hover .imgBox {
                transform: translate(0, -6.5rem);
            }
            .imgBox {
                height: 18rem;
            }
            .profile-user-img {
                height: 6rem !important;
                margin-top: -1em;
                width: 62%;
            }
            .tableCard {
                margin-top: 7.4rem;
            }
        }

        @media only screen and (min-width:485px) and (max-width:690px){
            .tableCard {
                margin-top: 11rem ;
            }
            .vacancyMainRow{
                height: auto !important;
            }
            .innerContentBox{
                overflow-x: scroll;
            }
            .profile-user-img {
                width: 42%;
                height: 7rem;
                margin-top: -1em;
            }
            .imgBox{
                height:18rem;
                
            }
            .sideCard .cardBox:hover .imgBox {
                transform: translate(-1.9rem, -2.5rem);
            }

            .listItem{
                font-size: 18px;
            }

            .employerDetails{
                font-size: 19px;
            }

            .contentBox{
                height: 18rem; 
            }

            .sideCard .cardBox:hover .contentBox {
                transform: translate(2.9rem, 13.4rem);
                width: 100%;
                height: ;
                /*margin-left: -1.2rem;*/
            }

            .sideCard{
                margin-bottom: 4rem;
            }

        }
        @media only screen and (min-width: 695px) and (max-width:915px){
            
            .sideCard{
                /*border: 1px solid black;*/
                padding:0;
            }

            .cardBox{
                /*border: 1px solid black;*/
                width: 100%;
                margin: 0 !important;
                padding: 0;
            }

            .profile-user-img {
                height: 5.5rem !important;
            }
            .sideCard .cardBox:hover .contentBox {
                height: 20rem !important;
                transform: translate(0, 9.4rem);
                width: 117%;
                margin-left: -1.1rem;
            }
            .sideCard .cardBox:hover .imgBox {

                transform: translate(0, -5.5rem);
            }
            .contentBox{
                height: 19rem !important;
            }
            .innerContentBox {
                width: 100%;
                padding: 2px;
                /*overflow-x: scroll;*/
                /*height: 100%;*/
            }
            .listItem {
                display: flex;
                flex-wrap: wrap;
                flex-direction: column;
                font-size: 15px;
                margin: 0.2rem 0;
            }

           
              

        }
      
        
        @media only screen and (min-width:920px) and (max-width:1030px) {             /*NEST HUB*/
            .sideCard .cardBox:hover .contentBox {
                width: 126% !important;
                transform: translate(-0.1rem, 13.5rem); 
                margin-left: -0.6rem;
                 height: 20rem;
                    width: 110% !important;
                    overflow: visible;
                    padding: 8.8px;
            }

            .imgBox{
                padding: 11px;
                left: -17px;
                width: 113%;
            }

            .sideCard .cardBox:hover .imgBox {
                /*transform: translate(-2rem, -2.19rem);*/
                transform: translate(0rem, -1.7rem);
                height: 18rem;
                    width: 113%;
                    /*margin-left: -2rem;*/
            }
            
            .sideCard{
                /*border: 1px solid black;*/
            }

            .cardBox{
                /*border: 1px solid black;*/
                width: 100%;
                margin: 0 !important;
                padding: 0;
            }
           

            .profile-user-img {
                height: 7rem !important;
                width: 74%;
                margin-top: -2rem;
            }
            
            .innerContentBox{
                 padding: 0px;
                 overflow-x: scroll;
            }

           

            .listItem{
                display: flex;
            flex-wrap: wrap;
            flex-direction: row;
            }
        }


          @media only screen and (min-width:1035px) /*and (max-width:1285px)*/{                      /*NEST HUB MAX*/

            .sideCard{
                /*border: 1px solid black;*/
                
            }

            .cardBox{
                /*border: 1px solid black;*/
                width: 100%;
                margin: 0 !important;
                padding: 0;
            }

            .sideCard .cardBox:hover .imgBox {
                    /*transform: translate(-2rem, -2.19rem);*/
                    transform: translate(0rem, 0rem);
                    height: 18rem;
                    width: 100%;
                    /*margin-left: -2rem;*/
                }


            .contentBox{
                left: -17px;
            }


            .sideCard .cardBox:hover .contentBox {
                    /*transform: translate(-0.8rem, 11.5rem);*/
                    transform: translate(0rem, 13.5rem); 
                    height: 20rem;
                    width: 102% !important;
                    overflow: visible;
                    padding: 8.8px;
             }

            .profile-user-img {
                height: 7rem !important;
                width: 74%;
                margin-top: -2rem;
            }
            
            .innerContentBox{
                 padding: 0px;
                 /*overflow-x: scroll;*/
                 margin-top: 3.5rem;
                 margin-left: -4rem;
            }

            .imgBox{
                left: -17px;
                width: 100%;
            }

            .listItem{
                display: flex;
            flex-wrap: wrap;
            flex-direction: row;
            }
        }


        /* SIDECARD CSS ENDS*/






        </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="VacancyDetailsPage">
    <div class="row">
    <div class="col-11"><h4>Job Vacancy Details</h4></div>
    <div class="col-1"><asp:ImageButton ID="JobVacancyDetailsPrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" height="35px" width="35px" AlternateText="Print" OnClientClick="SerTarget();" OnClick="JobVacancyDetailsPrintBTN_Click1"/></div>
    </div>

    <hr />
    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <div class="row vacancyMainRow">
          <div class="col-md-3">

                    <%-- Dusra Sidecard suru--%>
                    <div class="container sideCard">
                        <div class="cardBox">
                            <div class="imgBox">
                                <asp:Image ID="BusinessLogo" AlternateText="Employer Business Logo" runat="server" CssClass="profile-user-img img-fluid img-circle" />
                                <h3 class="profile-username text-center">
                                    <asp:Label ID="EmployerBusinessNameLBL" runat="server"></asp:Label></h3>
                                <br />
                                <div class="EmployerStatus text-center">
                                    <asp:Label ID="EmployerStatusLBL" runat="server"></asp:Label>
                                </div>
                                <br />
                                <p class="viewDetails">View Details</p>
                            </div>
                            <div class="contentBox container-fluid">
                                <div class="row innerContentBox">
                                    <div class="col-md-12">
                                        <ul>
                                            <li class="listItem">
                                                <%--<ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                           <%-- <h4 class="heading">Contact Person</h4>
                                                            <blockquote class="message">--%>
                                                             <span><i class="fa-solid fa-user" style="color: #10535c;"></i></span>
                                                                <asp:Label ID="ContactPersonNameLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                           <%-- </blockquote>--%>
                                                        </div>
                                                    <%--</li>
                                                </ul>--%>
                                            </li>                             

                                            <li class="listItem">
                                                <%--<ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                            <%--<h4 class="heading">Contact Number</h4>
                                                            <blockquote class="message">--%>
                                                                <span><i class="fa-solid fa-phone" style="color: #10535c;"></i></span>  
                                                                <asp:Label ID="WhatsAppNumberLBL" CssClass="employerDetails" runat="server"> </asp:Label>
                                                           <%-- </blockquote>--%>
                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                            </li>
                                            <li class="listItem">
                                               <%-- <ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                            <%--<h4 class="heading">Address</h4>
                                                            <blockquote class="message">--%>
                                                                <span><i class="fa-sharp fa-solid fa-location-dot" style="color: #10535c;"></i></span>
                                                                <asp:Label ID="EmployerAddressLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                           <%-- </blockquote>--%>
                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                            </li>
                                            <li class="listItem">
                                                <%--<ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                            <%--<h4 class="heading">Email</h4>
                                                            <blockquote class="message">--%>
                                                             <span><i class="fa-solid fa-envelope" style="color: #10535c;"></i> </span>
                                                                <asp:Label ID="EmployerEmailLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            <%--</blockquote>--%>
                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                            </li>
                                            <li class="listItem">
                                                <ul class="LowerCardContent">
                                                    <li>
                                                        <div class="message_wrapper">
                                                            <h4 class="heading">Registered On</h4>
                                                            <blockquote class="message">
                                                                <asp:Label ID="AppliedOnLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            </blockquote>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li class="listItem">
                                                <ul class="LowerCardContent">
                                                    <li>
                                                        <div class="message_wrapper">
                                                            <h4 class="heading">Updated On</h4>
                                                            <blockquote class="message">
                                                                <asp:Label ID="ProfileUpdatedOnLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            </blockquote>
                                                        </div>
                                                    </li>
                                                </ul>
                                           </li>
                                        </ul>
                                    </div>
                                    <%-- <ul class="list-group list-group-unbordered descList mb-3">
                                        <li class="list-group-item descListItem">
                                            <b>Registerd On:</b> <a class="float-right">
                                                <asp:Label ID="AppliedOnLBL" runat="server"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item descListItem">
                                            <b>Profile Updated On:</b> <a class="float-right">
                                                <asp:Label ID="ProfileUpdatedOnLBL" runat="server"></asp:Label></a>
                                        </li>
                                    </ul>--%>
                                </div>
                            </div>
                        </div>
                    </div>

            <!-- Profile Image -->
            <%--<div class="card card-primary card-outline">
              <div class="card-body box-profile">
                <div class="text-center">
                  <asp:Image ID="BusinessLogo" AlternateText="Employer Business Logo" runat="server" CssClass="profile-user-img img-fluid img-circle shadow" />
                </div>

                <h3 class="profile-username text-center"><asp:Label ID="EmployerFullNameLBL" runat="server"></asp:Label></h3>
                  <div class="message_wrapper">
                                      <h4 class="heading">Employer Business Name:</h4>
                                      <blockquote class="message"><asp:Label ID="EmployerFullNameLBL" runat="server"></asp:Label></blockquote>
                                    </div>

                <p class="text-muted text-center"><asp:Label ID="CurrentProfileLBL" runat="server"></asp:Label></p>
                    <b></b> <%--<a class="center"><asp:Label ID="CandidateStatusLBL" runat="server"></asp:Label></a>
                    <div class="clearfix"></div>
                    <br/>

                  
                
                  <br/>
               
              </div>
                  
              <!-- /.card-body -->
            </div>--%>
            
          </div>
          <!-- /.col -->
          <div class="col-md-9">
            <div class="card tableCard">
             
              <div class="card-body">
                <div class="tab-content">
                  <div class="active tab-pane" id="PersonalProfile">
                    <div class="post">
                       
                        <div class="row">
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Employer Name</h4>
                                      <blockquote class="message"><asp:Label ID="EmployerNameLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Vacancy Name</h4>
                                      <blockquote class="message"><asp:Label ID="VacancyNameLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                        </div>
                    </div>
                    <hr/>
                    <div class="post">
                        <div class="row">
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Job Location</h4>
                                      <blockquote class="message"><asp:Label ID="JobLocationLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Job Type</h4>
                                      <blockquote class="message"><asp:Label ID="JobTypeLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Candidate Required</h4>
                                      <blockquote class="message"><asp:Label ID="CandidatesRequiredLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                            <div class="col-sm-6">
                             <ul class="messages">
                                <li>
                                    <div class="message_wrapper">
                                      <h4 class="heading">Employement Status</h4>
                                      <blockquote class="message"><asp:Label ID="EmployementStatusLBL" runat="server"></asp:Label></blockquote>
                                    </div>
                                    </li>
                                 </ul>
                            </div>
                        </div>
                      <p>
                      </p>
                    </div>
                    <hr/>
                    <div class="post">
                      <!-- /.user-block -->
                        <div class="row">
                            <div class="col-sm-6">

                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Required Minimum Experience</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="RequiredMinExpLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-sm-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Required Minimum Qualification</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="RequiredMinQualificationLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Salary Offered</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="SalaryOfferedLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-sm-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Status</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="VacancyStatusLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                         <div class="row">
                            <%--<div class="col-sm-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Vacancy Details</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="VacancyDetailsLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>--%>
                             <div class="col-sm-12">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Salary Cycle</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="SalaryCycleLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            
                        </div>
                        <div class="row">

                            <div class="col-sm-12">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Vacancy Details</h4>
                                            <blockquote class="message">
                                                <asp:Label ID="VacancyDetailsLBL" runat="server"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                            </div>

                        </div>
                        </div>
                  </div>
                 

                          </div>            
                    </div>                  
                  <!-- /.tab-pane -->
                </div>
                <!-- /.tab-content -->
              </div><!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>                                     
    </section>   
    </div>    
</asp:Content>
