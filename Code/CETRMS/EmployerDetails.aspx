<%@ Page Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="EmployerDetails.aspx.cs" Inherits="CETRMS.EmployerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }   
        
        /*TABLE CSS STARTS*/
        .tableCard {
            overflow: hidden;
        }
        .tableContainer {
            padding: 1em;
            margin-top: .4em;
        }
        .employerTable {
            border: none !important;
        }
            .employerTable tr {
                background: white !important;
                border-bottom: 1px solid #1c94a4;
                transition: 0.2s ease-out;
            }
                .employerTable tr:hover {
                    background: #f0faf8 !important;
                    /*border: 1px solid #f0faf8;*/
                    box-shadow: -6px -2px 3px #d8d8d8, 6px 2px 3px #d8d8d8;
                }

            .employerTable th {
                background: #66acac;
                font-size: 16px;
                font-weight: 300;
                color: white;
                text-align: center;
                padding-top: 10px;
            }

            .employerTable td {
                font-size: 15px;
                padding: 10px;
                text-align: center;
                border: white;
            }
        .tableName {
            font-size: 20px;
        }
        /* 
        ---------------------------------------------
        Facts Style
        --------------------------------------------- 
        */

        .fun-facts {
            position: relative;
            padding: 10px 0px 10px 0px;
            overflow: hidden;
            background: #66acac;
            border-radius: 100px;
            width: 95%;
            margin-left:.5rem;
            margin-top:.5rem;
        }

        .fun-facts:before {
          position: absolute;
          top: 0;
          bottom: 0;
          left: 0;
          z-index: -1;
          width: 95%;
          height: 100%;
          background-color: #7a6ad8;
          content: '';
          border-top-right-radius: 500px;
          border-bottom-right-radius: 500px;
        }

/*        .fun-facts:after {
          background: url(../images/contact-dec-01.png);
          position: absolute;
          left: 15%;
          opacity: 0.5;
          top: 0;
          width: 318px;
          height: 50px;
          content: '';
          z-index: 2;
        }*/

        .fun-facts .counterBlock {
          text-align: center;
          margin-bottom: 10px;
        }

        .fun-facts h2 {
          color: #fff;
          font-size: 42px;
          font-weight: 500;
        }

/*        .fun-facts h2::after {
          content: '+';
          margin-left: 5px;
        }*/

        .fun-facts p {
          color: #fff;
          font-size: 16px;
          font-weight: 500;
          margin-top: 5px;
        }





        @media only screen and  (max-width: 375px) {

            .fun-facts {
                width: 66%;
                height: 48vh;
                margin-left: 2.8rem;
            }

            .counterBlock {
                padding-top: 13px;
            }
        }


        @media only screen and (min-width:380px) and (max-width: 480px) {

            .fun-facts {
                width: 68%;
                height: 47vh;
                margin-left: 2.8rem
            }

            .counterBlock {
                padding-top: 40px;
            }
        }


         @media only screen and (min-width:485px) and (max-width:690px){
             .fun-facts{
                 height: 53vh;
                 width: 56%;
                 margin-left: 6rem;
             }
             .counterBlock {
                padding-top: 38px;
            }

         }
        /*  */

            @media only screen and (min-width: 695px) and (max-width:915px) {
               
                .fun-facts{
                 
                 margin-left: 1.2rem;
             }
            
            }

        @media only screen and (min-width:1035px) /*and (max-width:1285px)*/ {
             
             .fun-facts{
                 margin-left: 1.2rem;
             }
        
        
        }

        @media only screen and (min-width:920px) and (max-width:1030px) {
        
            .fun-facts{
                 margin-left: 0.9rem;
                 padding: 0 8px;
             }
        
        }

        @media only screen and (max-width: 480px) {
            .tableContainer {
                padding: 0em;
            }
            .smallTable {
                overflow-x: scroll;
            }
            .employerTable td, .employerTable th {
                font-size: 13px;
                padding: 15px;
            }
            .card-desc {
                height: 36vh;
                overflow-y: scroll;
            }
            .descList {
                padding: 0px;
                margin-top: -1em;
            }
        }
         @media only screen and (min-width:485px) and (max-width:690px){
            .smallTable {
                overflow-x: scroll;
            }
        }
        @media only screen and (min-width: 695px) and (max-width:915px) {
             .smallTable {
                overflow-x: scroll;
            }
        }
         @media only screen and (min-width:920px) and (max-width:1280px){
             .smallTable {
                overflow-x: scroll;
            }
        }

         /*sample comment*/

        /*TABLE CSS ENDS*/




        /* SIDECARD CSS STARTS*/

        .sideCard {
            /*border: 1px solid black;*/
            height: 60vh;
            margin: 35px 0 0 3px;
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
                    transform: translate(-4.5rem, -4.9rem);
                    height: 18rem;
                    padding: 14px 14px 0px 14px !important;
                }
                .sideCard .cardBox:hover .contentBox {
                    transform: translate(-1.2rem, 9.5rem);
                    height: 18rem;
                    line-height: 0.8rem;
                    width: 134%;
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
            list-style: none
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
            .tableCard {
                margin-top: 4rem;
            }
            .sideCard {
                margin-top: 4rem;
                padding: 0;
            }
            .contentBox {
                top: 50px;
                height: 5rem;
            }

            .innerContentBox {
                width: 100%;
                padding: 2px;
                height: 85%;
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
                transform: translate(-0.1rem, 9.4rem);
                width: 120%;
                height: ;
                margin-left: -1.2rem;
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
                overflow-x: scroll;
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
        @media only screen and (min-width:1035px) and (max-width:1280px){                      /*NEST HUB MAX*/

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
                    transform: translate(0rem, -2.1rem);
                    height: 18rem;
                    width: 100%;
                    
                }

            .sideCard .cardBox:hover .contentBox {
                    /*transform: translate(-0.8rem, 11.5rem);*/
                    transform: translate(-1.1rem, 13.5rem); 
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
                 overflow-x: scroll;
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
                transform: translate(0rem, -2.1rem);
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

@media only screen and (min-width:1290px) /*and (max-width:1990px)*/ {

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
                transform: translate(0rem, -2.1rem);
                height: 18rem;
                width: 100%;

            }

        .sideCard .cardBox:hover .contentBox {
                /*transform: translate(-0.8rem, 11.5rem);*/
                transform: translate(-1.1rem, 13.5rem); 
                height: 20rem;
                width: 100% !important;
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
             overflow-x: scroll;
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

    <div class="row">
        <div class="col-11">
            <h5>Employer Details: &nbsp;<asp:Label ID="EmployerDetailLBL" runat="server"></asp:Label></h5>
        </div>
        <div class="col-1">
            <asp:ImageButton ID="EmployerDetailsPrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" Height="35px" Width="35px" AlternateText="Print" OnClientClick="SetTarget();" OnClick="EmployerDetailsPrintBTN_Click" /></div>
    </div>
    <hr />
    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-3">


                    <%-- Sidecard Starts--%>
                    <div class="container sideCard">
                        <div class="cardBox">
                            <div class="imgBox container-fluid">
                                <asp:Image ID="BusinessLogo" AlternateText="Employer Business Logo" runat="server" CssClass="profile-user-img img-fluid <%--img-circle--%>" />
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
                                               <%-- <ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                            <%--<h4 class="heading">Contact Person</h4>
                                                            <blockquote class="message">--%>
                                                             <span><i class="fa-solid fa-user" style="color: #10535c;"></i> <%--<i class="fa-regular fa-user" style="color: #60968c;"></i>--%> </span> 
                                                            <asp:Label ID="EmployerNameLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            <%--</blockquote>--%>
                                                        </div>
                                                    <%--</li>
                                                </ul>--%>
                                              
                                            </li>
                                            <li class="listItem">
                                               <%-- <ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                            <%--<h4 class="heading">Contact Number</h4>
                                                            <blockquote class="message">
                                                                
                                                            </blockquote>--%>
                                                            <span><i class="fa-solid fa-phone" style="color: #10535c;"></i></span>                                      
                                                            <asp:Label ID="WhatsAppNumberLBL" CssClass="employerDetails" runat="server"></asp:Label>

                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                                
                                            </li>
                                            <li class="listItem">
                                               <%-- <ul class="LowerCardContent">
                                                    <li>--%>
                                                        <div id="employerAddress" class="message_wrapper">
                                                            <%--<h4 class="heading">Address</h4>
                                                            <blockquote class="message">                                                              
                                                            </blockquote>--%>
                                                            
                                                            <span><i class="fa-sharp fa-solid fa-location-dot" style="color: #10535c;"></i></span>
                                                            <asp:Label ID="EmployerAddressLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                               <%-- <h4>Address: 
                                                   <span>
                                                       <i class="fa-sharp fa-solid fa-location-dot" style="color: #b2bfd7;"></i> <asp:Label ID="EmployerAddressLBL" CssClass="employerDetails" runat="server"></asp:Label> 
                                                   </span>
                                                </h4>--%>
                                            </li>
                                            <li class="listItem">
                                               <%-- <ul class="lowercardcontent">
                                                    <li>--%>
                                                        <div class="message_wrapper">
                                                           <%-- <h4 class="heading">email</h4>
                                                            <blockquote class="message">                                                               
                                                            </blockquote> #60968c--%>

                                                            <span><i class="fa-solid fa-envelope" style="color: #10535c;"></i> </span>
                                                            <asp:label ID="EmployerEmailLBL" CssClass="employerDetails" runat="server"></asp:label>

                                                        </div>
                                                   <%-- </li>
                                                </ul>--%>
                                                <%--<h4>Email: 
                                                   
                                                       <span><i class="fa-solid fa-envelope" style="color: #b2bfd7;"></i> </span>
                                                       <asp:Label ID="EmployerEmailLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                   
                                                </h4>--%>
                                            </li>
                                            <li class="listItem">
                                                <ul class="LowerCardContent">
                                                    <li>
                                                        <div class="message_wrapper dateData">
                                                            <h4 class="heading">Registered On:</h4>
                                                            <blockquote class="message">
                                                                <asp:Label ID="AppliedOnLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            </blockquote>
                                                        </div>
                                                    </li>
                                                </ul>
<%--                                                <h4>Registered On: 
                                                   <span class="AppliedOn">
                                                       <br />
                                                       
                                                   </span>
                                                </h4>--%>
                                            </li>
                                            <li class="listItem">
                                                <ul class="LowerCardContent">
                                                    <li>
                                                        <div class="message_wrapper dateData">
                                                            <h4 class="heading">Updated On:</h4>
                                                            <blockquote class="message">
                                                                <asp:Label ID="ProfileUpdatedOnLBL" CssClass="employerDetails" runat="server"></asp:Label>
                                                            </blockquote>
                                                        </div>
                                                    </li>
                                                </ul>
<%--                                                <h4>Profile Updated On: 
                                                  <span class="ProfileUpdatedOn">
                                                      <br />
                                                      
                                                  </span>
                                               </h4>--%>
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
                    <%-- Sidecard Ends--%>







                    <!-- Profile Image -->
                    <!-- /.card -->
                </div>
                <!-- About Me Box -->
                <div class="col-md-9">
                    <div class="card tableCard">
                        <!-- /.card-header -->
                        <div class="card-body">
                            <div class="tab-content">
                                <div class="active tab-pane" id="EmployerDetails">
                                    <!-- Post -->
                                    <div class="post">
                                        <div class="section justify-content-center fun-facts">
                                            <div class="container counterBlock">

                                                            <div class="row">
                                                                <div class="col-lg-4 col-md-4">
                                                                    <div class="counter">
                                                                        <h2><asp:Label ID="VacanciesPublishedLBL" runat="server"></asp:Label></h2>
                                                                        <p class="count-text ">Vacancies Published</p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4">
                                                                    <div class="counter">
                                                                        <h2><asp:Label ID="ScheduledInterviewsLBL" runat="server"></asp:Label></h2>
                                                                        <p class="count-text ">Scheduled Interviews</p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-4">
                                                                    <div class="counter">
                                                                        <h2><asp:Label ID="JobApplicationLBL" runat="server"></asp:Label></h2>
                                                                        <p class="count-text ">Job Applications</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                        </div>
                                        <p>
                                        </p>
                                    </div>
                                    <div class="post tableContainer">
                                        <div class="user-block">
                                            <h5><label class="accent-blue tableName"><b>Vacancies</b></label></h5></div>
                                        <!-- /.user-block -->
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 smallTable">
                                                <asp:GridView ID="EmployerVacancyHistoryGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" CssClass="mt-3 pt-1 table employerTable table-striped jambo_table bulk_action" CellPadding="5" CellSpacing="5" PageSize="5" OnPageIndexChanging="EmployerVacancyHistoryGV_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="VacancyName" HeaderText="Vacancy Name" />
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="PrimaryLocation" HeaderText="Primary Location" />
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="VacancyDetails" HeaderText="Vacancy Details" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="post tableContainer">
                                        <div class="user-block"><h5><label class="accent-blue tableName"><b>Interviews</b></label></h5></div>
                                        <!-- /.user-block -->
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 smallTable">
                                                <asp:GridView ID="EmployerInterviewHistoryGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" CssClass="mt-3 pt-1 table employerTable table-striped jambo_table bulk_action" CellPadding="5" CellSpacing="5" PageSize="5" OnPageIndexChanging="EmployerInterviewHistoryGV_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="InterviewID" HeaderText="Interview ID" />
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="InterviewDate" HeaderText="Interview Date" />
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="Topic" HeaderText="Topic" />
                                                        <asp:BoundField ItemStyle-Width="150px" ControlStyle-CssClass="tableHead" DataField="InterviewStatus" HeaderText="Interview Status" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.post -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>

    </section>
    <!-- /.content -->

</asp:Content>
