<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Newindex.aspx.cs" Inherits="CETRMS.Newindex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&family=Rasa&display=swap" rel="stylesheet"/>

    <title>Universal Abroad : home</title>
    <link rel="icon" type="text/x-html-insertion" href="IndexAssets/UeLogoWhitebg.png" />



    <%-- BOX ICON CSS LINK --%>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>

    <!-- Bootstrap core CSS -->
<%--    <link href="IndexAssets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />--%>

    <!-- Additional CSS Files -->
    <link rel="stylesheet" href="IndexAssets/css/fontawesome.css" /> 
    <link rel="stylesheet" href="IndexAssets/css/templatemo-scholar.css"/>
    <link rel="stylesheet" href="IndexAssets/css/owl.css" />
    <link rel="stylesheet" href="IndexAssets/css/animate.css" />
    <link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css" />
    <link rel="stylesheet" href="IndexAssets/Heroes.css" />
   <%-- <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="IndexAssets/bootstrap-4.0.0-dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="LandingPageAsset/loginModal.css" />
    <link rel="stylesheet" href="LandingPageAsset/Megamenu.css" />
    <link rel="stylesheet" href="IndexAssets/Termscondition.css"/>
    <link rel="stylesheet" href="IndexAssets/SidebarPanel.css"/>
    <link rel="stylesheet" href="IndexAssets/Joinchat.css"/>
  
    <style>
        *{
            font-family: 'Rasa', serif;

        }
        .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
            position: relative;
            width: 100%;
            min-height: 1px;           
        }

        .input-group-addon {
            color: rgb(255, 255, 255);
            /*background-color: #ffffff;*/
            padding:10px;
        }
        input#UserNameTXT.form-control .active{
            background:transparent;
        }
        .input-group > .custom-select:not(:first-child), .input-group > .form-control:not(:first-child) {
            border-top-left-radius: 0;
            border-bottom-left-radius: 0;
            border-color: transparent;
            color:#fff;
        }
        .input-group > .custom-file, .input-group > .custom-select, .input-group > .form-control {
            position: relative;
            -webkit-box-flex: 1;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            background: transparent;
        }


        @media (max-width: 992px) {
            .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
            padding-right:14px;
            padding-left:14px;
            }            
        }
        
        /*@media (max-width: 767px) {
            .header-area .main-nav {
                overflow: hidden;
            }
        }*/
       
        .row{
            margin-top:0;
            display:webkit-box;
            flex-wrap:wrap;
        }
          .bottom {
            position: absolute;
            bottom: 10px;
            margin-left:4.8rem;
        }
        .btn-group-sm > .btn, .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: .875rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }
          .btn-outline-success{
              color:#66acac;
              background-color:transparent;
              background-image:none;
              border-color:#66acac;
          }
          .card{
              position:initial;
              width: 100%;
              height:19rem;
          }
          .cardbetter{
              position:initial;
              width: 100%;
              height:15rem;
              margin-top:-50px;
          }
     
          .modal-content{
            position: relative;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            max-width:100vw;
            pointer-events: auto;
            background-color:#fff;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 0.3rem;
            padding:0px 16px 20px;
            outline: 0;
            z-index:1050;
            
          }
        .modal-content-login {
            position: relative;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            pointer-events: auto;
            background-color:transparent;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 0.3rem;
            outline: 0;
        }
        .modal-content-signup {
            position: relative;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            pointer-events: auto;
            background-color:transparent;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 0.3rem;
            outline: 0;         
        }
          .modal-footer{
              justify-content:center;
          }
          .img-fluid{
              width:65%;
          }
        .modal-dialog {
            position: relative;
            width: auto;
            /* margin: 0.5rem; */
            /* margin-left: 0.5rem; */
            /* margin-right: 0.5rem; */
            pointer-events: none;
        }
        .modal.right.fade .modal-dialog {
            right: -320px;
            transition: opacity 0.1s linear, right 0.1s ease-out;
        }

        .modal.right.fade.show .modal-dialog {
            right: 0;
        }
      

       /*COOKIE CONSENT STARTS*/

        .consentBox {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            align-items: center;
            /*flex-direction: column;*/
            /*border: 1px solid red;*/
            position: fixed;
            bottom: 9px;
            left: 20px;
            height: 30%;
            width: 48%;
            background: #fff;
            border-radius: 8px;
            padding: /*35px 25px*/ 17px 17px;
            transition: right 0.3s ease;
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
            z-index: 1051;
            animation: 5.3s fromRight;
            animation-iteration-count: 1;
            /*animation-delay: 0.8s;*/
        }

        @keyframes fromRight {
            from {
                left: -1070px;
            }

            to {
                left: 25px;
            }
        }


        .cookieTitle {
            color: black;
            display: flex;
            align-items: center;
            column-gap: 15px;
        }


            .cookieTitle i {
                color: black;
                font-size: 30px;
                margin-top: -0.5rem;
            }

            .cookieTitle h4 {
                color: black;
                font-weight: bold;
            }


        .contentText {
            margin-top: 2px;
            margin-bottom: 0.2rem;
        }

            .contentText p {
                color: #333;
                font-size: 16px;
            }

                .contentText p a {
                    color: #4070f4;
                    text-decoration: none;
                }


        .consentBox .btn {
            /*margin-top: 16px;*/
            width: 68%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .btn .acceptButton {
            border: none;
            color: #fff;
            padding: 8px 0;
            border-radius: 4px;
            background: /*#4070f4*/ black;
            cursor: pointer;
            /*width: calc(100% / 2 - 10px);*/
            width: 200px;
            margin: 3px;
            transition: all 0.2s ease;
        }

            .btn .acceptButton:hover {
                background-color: #034bf1;
            }


      

            @media only screen and (min-width:200px) and (max-width:480px){
                .consentBox{
                    height: 42%;
                    width: 60%;
                    font-size: 10%;
                }

                .consentBox .btn{
                    /*width*/: 100%;
                    margin-top: -1rem;
                }

                #settingBtn{
                    font-size: 13px;
                }

            }

             @media only screen and (min-width:485px) and (max-width:690px){
                .consentBox {
                   height: 42%;
                    width: 55%;
                }
             }

              @media only screen and (min-width: 695px) and (max-width:915px){
                  .consentBox {
                    height: 22%;
                    
                }
              }

               @media only screen and (min-width:920px) and (max-width:1030px){
                   .consentBox {
                    height: 37%;
                    width: 30%;
                }
               }

               @media only screen and (min-width:1035px) and (max-width:1280px){
                   .consentBox {
                    height: 42%;
                    width: 30%;
                }
               }

               @media only screen and (min-width:1285px){
                   .consentBox{
                       height: 38%;
                       width: 30%;
                       
                   }
                   .contentText p{
                       font-size: 18px;
                   }
               }

         
    </style>
    <script src="IndexAssets/vendor/jquery/jquery.min.js"></script>

</head>
<body ondragstart="return false;" ondrop="return false;">
    <form id="form1" runat="server">
        <div>
            <!-- ***** Preloader Start ***** -->
            

            <div class="container-lg">
            <!-- ***** Preloader End ***** -->
           <%-- <div class="mydiv1">--%>
                <!-- ***** Header Area Start ***** -->
                <header class="header-area header-sticky background-header">
                    <div class="container">
                        <div class="row">
                            <div class="col-12">
                                <nav class="main-nav">
                                    <!-- ***** Logo Start ***** -->
                                    <a href="Newindex.aspx" class="logo">
                                        <img src="IndexAssets/images/UELogo.png" alt="UE-LOGO" style="width: 175px;" />
                                    </a>
                                    <!-- ***** Logo End ***** -->
                                    <!-- ***** Serach Start ***** -->

                                    <!-- ***** Serach Start ***** -->
                                    <!-- ***** Menu Start ***** -->

                                    <ul class="nav">
                                        <li class="scroll-to-section"><a href="CandidateMobileApplication.aspx" class="active">Candidate</a></li>
                                        <li class="scroll-to-section">
                                            <a href="CandidateMobileApplication.aspx" aria-haspopup="true" aria-expanded="false">Employer</a>                                            
                                        </li>
                                                                                                                  
                                        <li class="scroll-to-section"><a href="Aboutus.aspx">Company</a></li>
                                        <li class="scroll-to-section">
                                            <asp:LinkButton ID="LoginModalLB" runat="server" type="button" title="Log In" OnClick="LoginModalLB_Click">
                                              MIS SignIn
                                            </asp:LinkButton>
                                        </li>
                                        <li class="scroll-to-section">
                                            <asp:LinkButton ID="SignupModalLB" runat="server" type="button" title="Sign Up" data-toggle="modal" data-target="#TermsconditionValidation">
                                             Sign Up
                                            </asp:LinkButton>
                                        </li>
                                    </ul>

                                    <a class='menu-trigger'>
                                        <span>Menu</span>
                                    </a>
                                    <!-- ***** Menu End ***** -->
                                </nav>
                            </div>

                        </div>
                    </div>
                </header>
                <!-- ***** Header Area End ***** -->
           

           

                <div class="main-banner" id="#main">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="item item-1">
                                    <div class="header-text">
                                        <%--<span class="category">Hire Now!</span>--%>
                                        <h2>Universal Abroad Jobs</h2>
                                        <p>Get your dream job and better placement!
                                            <br />
                                            Download Universal Abroad App<br />
                                            Dream Job!!!</p>
                                        <div class="buttons">
                                            <div class="main-button">
                                                <a href="SearchVacancyList.aspx">Get a Job</a>
                                            </div>
                                            <div class="main-button">
                                              <a href="SearchCandidateList.aspx">Hire now</a>
                                            </div>                                            
                                        </div>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           <%-- </div>--%>

            <div class="services section" id="#Services">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon1">
                                    <img src="IndexAssets/images/Qualified-Candidates-1681117735171.png" alt="online degrees"/>
                                </div>
                                <div class="main-content card text-center h-100">
                                    <div class="clearfix"><br/></div>
                                    <h4>Qualified Candidates</h4>
                                    <span><strong style="font-size:50px;color: #3e7474;"><asp:Label ID="CandidateCountLBL" runat="server"></asp:Label></strong></span>                                   
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon1">
                                    <img src="IndexAssets/images/Interviews-1681117748393.png" alt="short courses"/>
                                </div>
                                <div class="main-content card text-center h-100">
                                    <div class="clearfix"><br/></div>
                                    <h4>On board Employers</h4>
                                     <span><strong style="font-size:50px;color: #3e7474;"><asp:Label ID="EmployerCountLBL" runat="server"></asp:Label></strong></span>                                  
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon1">
                                    <img src="IndexAssets/images/Across-World.png" alt="web experts"/>
                                </div>
                                <div class="main-content card text-center h-100">
                                    <div class="clearfix"><br/></div>
                                    <h4>Locations</h4>
                                     <span><strong style="font-size:50px;color: #3e7474;"><asp:Label ID="LocationsLBL" runat="server"></asp:Label></strong></span>                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="services section" id="services">
                <div class="container">
                    <h1>Get Started in 3 easy steps</h1>
                    <div class="row mt-5">
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon">
                                    <img src="IndexAssets/images/Employer-1681117817999.png" alt="online degrees" />
                                </div>
                                <div class="main-content card">
                                    <div class="clearfix"><br/></div>
                                    <h4>For Empolyer</h4>
                                    <p>*Post a Job takes less than 5 minutes and Register via OTP</p>
                                    <p>*Pay registration fees 50$ Yearly.</p>
                                    <p>*Get verified our team will get in touch</p>
                                    <%--<div class="main-button bottom">
                                        <a href="#">Read More</a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon">
                                    <img src="IndexAssets/images/Job-Seekers-1681117733444.png" alt="short courses" />
                                </div>
                                <div class="main-content card">
                                    <div class="clearfix"><br/></div>
                                    <h4>For Job Seekers</h4>
                                    <p>*Register via OTP</p>
                                    <p>*Pay registration fees 50$ Yearly.</p>
                                    <p>*Get verified our team will get in touch</p>
                                    <%--<div class="main-button bottom">
                                        <a href="#">Read More</a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">
                                <div class="icon">
                                    <img src="IndexAssets/images/Get-Calls-1681117754183.png" alt="web experts" />
                                </div>
                                <div class="main-content card">
                                    <div class="clearfix"><br/></div>
                                    <h4>Get calls. Hire.</h4>
                                    <p>You will get calls from relevant candidates within one hour or call them directly from our candidate database.</p>
                                    <%--<div class="main-button bottom">
                                        <a href="#">Read More</a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="services section" id="#">
                <div class="container">
                    <h1>What makes Universal Abroad better ?</h1>
                    <div class="row">
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">

                                <div class="main-content cardbetter justify-content-center">
                                    <h4>Simple Hiring</h4>
                                    <p>Recieve calls from qualified candidates in under an hour of posting a job  </p>
                                    <div class="main-button">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">

                                <div class="main-content cardbetter justify-content-center">
                                    <h4>Intelligent Recommendations</h4>
                                    <p>Only the best candidates are recommended by our ML as per your requirement </p>
                                    <div class="main-button">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="service-item">

                                <div class="main-content cardbetter justify-content-center">

                                    <h4>Priority customer support</h4>
                                    <p>Prioritized customer support for the paid plan users</p>
                                    <div class="main-button">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="section about-us">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6 offset-lg-1">
                            <div class="accordion" id="accordionExample">
                                <h3 class="text-white text-center mb-4">FAQ</h3>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingOne">
                                        <button class="accordion-button" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            How do I create an employer account with universal-abroadjobs.com? 
                                        </button>
                                    </h2>
                                    <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                           Creating an account with universal-abroadjobs.com is simple. You can start by posting your first job and authenticate your Email Id.
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingTwo">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                            How do I start hiring from universal-abroadjobs.com?
                                        </button>
                                    </h2>
                                    <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            Hiring from universal-abroadjobs.com is easy and quick.<br/>
                                            Just follow these steps:<br/>
                                            •	Post a Job (takes less than 5 minutes) and Register via Email Verification <br/>
                                            •	Pay registration fees (50$) Yearly.<br/>
                                            •	Get verified (our team will get in touch).<br/>
                                            Candidates will start contacting you immediately after successful verification.
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingThree">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                            Can I post a job for free?
                                        </button>
                                    </h2>
                                    <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            No, You have to pay 50$ yearly, where you can post a job for a year and there is no limit for job posting.
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingFour">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                            How does universal-abroadjobs.com ensure that only candidates matching the job criteria contact me?
                                        </button>
                                    </h2>
                                    <div id="collapseFour" class="accordion-collapse collapse" aria-labelledby="headingFour" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            Our matching algorithm filters through our candidate database to display the job post only to the Candidates matching your job requirement, making the whole process quick, easy, and convenient.
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingFive">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                            When will I start receiving candidate response?
                                        </button>
                                    </h2>
                                    <div id="collapseFive" class="accordion-collapse collapse" aria-labelledby="headingFive" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            A job posting shall start receiving responses once it’s been approved by our Verification Team. We have seen some job postings getting approved in as fast as 30 minutes.  </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingSix">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                           What types of payment do you accept?
                                        </button>
                                    </h2>
                                    <div id="collapseSix" class="accordion-collapse collapse" aria-labelledby="headingSix" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            You can the pay the yearly fees of 50$ via paypal payment method.                                        
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingSeven">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">
                                           Do I get the invoice for the registration fees from Universal-Abroadjobs.com?
                                        </button>
                                    </h2>
                                    <div id="collapseSeven" class="accordion-collapse collapse" aria-labelledby="headingSeven" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                            Yes, Once your registration get successful and if your email is verified, you will get the invoice in your email or else you can download from payment reports."                                
                                        </div>
                                    </div>
                                </div>
                                <div class="accordion-item">
                                    <h2 class="accordion-header" id="headingEight">
                                        <button class="accordion-button collapsed" type="button" data-toggle="collapse" data-target="#collapseEight" aria-expanded="false" aria-controls="collapseEight">
                                            Do I get the refund on the registration fees?
                                        </button>
                                    </h2>
                                    <div id="collapseEight" class="accordion-collapse collapse" aria-labelledby="headingEight" data-parent="#accordionExample">
                                        <div class="accordion-body">
                                             No, We don't have refund policy against the registration fees, fur futher you can refer our <a href="PrivacyPolicy.aspx">Refund Policy.</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5 align-self-center">
                            <div class="section-heading">
                                <h6>About Us</h6>
                                <h4>UNIVERSAL-ABROADJOBS.COM</h4>
                                <p>
                                    Here we provide employment services.
                                    Recruitment staff for all positions as per your company criteria and requirements.
                                    Sourcing candidates who fit your business, who values and devote themselves to your company for the long term.
                                </p>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--<section class="section courses" id="#">
    <div class="container">
      <div class="row">
        <div class="col-lg-12 text-center">
          <div class="section-heading">
            <h6>Latest Courses</h6>
            <h2>Latest Courses</h2>
          </div>
        </div>
      </div>
      <ul class="event_filter">
        <li>
          <a class="is_active" href="#!" data-filter="*">Show All</a>
        </li>
        <li>
          <a href="#!" data-filter=".design">Webdesign</a>
        </li>
        <li>
          <a href="#!" data-filter=".development">Development</a>
        </li>
        <li>
          <a href="#!" data-filter=".wordpress">Wordpress</a>
        </li>
      </ul>
      <div class="row event_box">
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6 design">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-01.jpg" alt=""></a>
              <span class="category">Webdesign</span>
              <span class="price"><h6><em>$</em>160</h6></span>
            </div>
            <div class="down-content">
              <span class="author">Stella Blair</span>
              <h4>Learn Web Design</h4>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6  development">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-02.jpg" alt=""></a>
              <span class="category">Development</span>
              <span class="price"><h6><em>$</em>340</h6></span>
            </div>
            <div class="down-content">
              <span class="author">Cindy Walker</span>
              <h4>Web Development Tips</h4>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6 design wordpress">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-03.jpg" alt=""></a>
              <span class="category">Wordpress</span>
              <span class="price"><h6><em>$</em>640</h6></span>
            </div>
            <div class="down-content">
              <span class="author">David Hutson</span>
              <h4>Latest Web Trends</h4>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6 development">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-04.jpg" alt=""></a>
              <span class="category">Development</span>
              <span class="price"><h6><em>$</em>450</h6></span>
            </div>
            <div class="down-content">
              <span class="author">Stella Blair</span>
              <h4>Online Learning Steps</h4>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6 wordpress development">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-05.jpg" alt=""></a>
              <span class="category">Wordpress</span>
              <span class="price"><h6><em>$</em>320</h6></span>
            </div>
            <div class="down-content">
              <span class="author">Sophia Rose</span>
              <h4>Be a WordPress Master</h4>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6 align-self-center mb-30 event_outer col-md-6 wordpress design">
          <div class="events_item">
            <div class="thumb">
              <a href="#"><img src="IndexAssets/images/course-06.jpg" alt=""></a>
              <span class="category">Webdesign</span>
              <span class="price"><h6><em>$</em>240</h6></span>
            </div>
            <div class="down-content">
              <span class="author">David Hutson</span>
              <h4>Full Stack Developer</h4>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>--%>

            <div class="section fun-facts">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="wrapper">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6">
                                        <div class="counter">
                                            <h2><asp:Label ID="VacanciesPublishedLBL" runat="server"></asp:Label></h2>
                                            <p class="count-text ">Vacancies Published</p>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <div class="counter">
                                            <h2><asp:Label ID="ScheduledInterviewsLBL" runat="server"></asp:Label></h2>
                                            <p class="count-text ">Scheduled Interviews</p>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-6">
                                        <div class="counter">
                                            <h2><asp:Label ID="JobApplicationLBL" runat="server"></asp:Label></h2>
                                            <p class="count-text ">Job Applications</p>
                                        </div>
                                    </div>
<%--                                    <div class="col-lg-3 col-md-6">
                                        <div class="counter end">
                                            <h2 class="timer count-title count-number" data-to="15" data-speed="1000"><asp:Label ID="YearsExperience" runat="server"></asp:Label></h2>
                                            <p class="count-text ">Years Experience</p>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="team section" id="team">
                <div class="container">
                    <%--<div class="row">
                        <div class="col-lg-3 col-md-6">
                            <div class="team-member">
                                <div class="main-content">
                                    <img src="IndexAssets/images/img4.png" alt=""/>
                                    <span class="category">UX Teacher</span>
                                    <h4>Sophia Rose</h4>
                                    <ul class="social-icons">
                                        <li><a href="#"><i class="fab fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fab fa-linkedin"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="team-member">
                                <div class="main-content">
                                    <img src="IndexAssets/images/img3.png" alt=""/>
                                    <span class="category">Graphic Teacher</span>
                                    <h4>Cindy Walker</h4>
                                    <ul class="social-icons">
                                        <li><a href="#"><i class="fab fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fab fa-linkedin"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="team-member">
                                <div class="main-content">
                                    <img src="IndexAssets/images/img2.png" alt=""/>
                                    <span class="category">Full Stack Master</span>
                                    <h4>David Hutson</h4>
                                    <ul class="social-icons">
                                        <li><a href="#"><i class="fab fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fab fa-linkedin"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="team-member">
                                <div class="main-content">
                                    <img src="IndexAssets/images/img1.png" alt=""/>
                                    <span class="category">Digital Animator</span>
                                    <h4>Stella Blair</h4>
                                    <ul class="social-icons">
                                        <li><a href="#"><i class="fab fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fab fa-linkedin"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>

            <div class="section testimonials">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-7">
                            <div class="owl-carousel owl-testimonials">
                                <asp:Literal ID="TestimonialLit" runat="server">                                    
                                </asp:Literal>                              
<%--                                <div class="item">
                                    <p>“Please tell your friends or collegues about TemplateMo website. Anyone can access the website to download free templates. Thank you for visiting.”</p>
                                    <div class="author">
                                        <img src="IndexAssets/images/testimonial-author.jpg" alt=""/>
                                        <span class="category">Full Stack Master</span>
                                        <h4>Claude David</h4>
                                    </div>
                                </div>
                                <div class="item">
                                    <p>“Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Quis ipsum suspendisse ultrices gravid.”</p>
                                    <div class="author">
                                        <img src="IndexAssets/images/testimonial-author.jpg" alt=""/>
                                        <span class="category">UI Expert</span>
                                        <h4>Thomas Jefferson</h4>
                                    </div>
                                </div>
                                <div class="item">
                                    <p>“Scholar is free website template provided by TemplateMo for educational related websites. This CSS layout is based on Bootstrap v5.3.0 framework.”</p>
                                    <div class="author">
                                        <img src="IndexAssets/images/testimonial-author.jpg" alt=""/>
                                        <span class="category">Digital Animator</span>
                                        <h4>Stella Blair</h4>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-lg-5 align-self-center">
                            <div class="section-heading">
                                <h6>Candidate Review</h6>
                                <h2>What they say about us?</h2>
                                <p>Universal education is platform to develop skills and show the telent by get hire and hiring. Very easy to get hiried and Hiring system.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="section events" id="#">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <div class="section-heading">
                                <h2>Hire from 50+ Job Categories</h2>
                            </div>
                        </div>
                        <div class="col-lg-12 text-left">
                            <div class="section-heading">
                                <div class="main-button">
                                    <asp:LinkButton ID="PostLB1"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Accounts</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB2"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Back Office</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB3"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Beautician</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB4"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Driver</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB5"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Field Sales</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB6"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Hardware Engineer</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB7"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Lab Technician</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB8"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Machine Operator</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB9"   runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Marketing</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB10"  runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Office Boy Peon</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB11"  runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Security Guard</asp:LinkButton>
                                    <asp:LinkButton ID="PostLB12"  runat="server" href="SearchVacancyList.aspx" CssClass="btn-green btn-sm mb-4">Technician</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <div class="section-heading">
                                <h2>Hire from 750+ Cities</h2>
                            </div>
                        </div>
                        <div class="col-lg-12 text-left">
                            <div class="section-heading">
                                <div class="main-button">
                                    <asp:LinkButton ID="CityLB1" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="New York"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB2" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="Los Angeles"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB3" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="Chicago"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB4" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="Houston"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB5" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="San Antonio"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB6" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="San Diego"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB7" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="Dallas"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB8" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="San Jose"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB9" runat="server"  href="SearchCandidateList.aspx" CssClass="btn-green btn-sm mb-4" Text="Fort Worth"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB10" runat="server" href="SearchCandidateList.aspx" CssClass="btn-green  btn-sm mb-4" Text="Columbus"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB11" runat="server" href="SearchCandidateList.aspx" CssClass="btn-green  btn-sm mb-4" Text="Indianapolis"></asp:LinkButton>
                                    <asp:LinkButton ID="CityLB12" runat="server" href="SearchCandidateList.aspx" CssClass="btn-green  btn-sm mb-4" Text="Other 715 cities"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="contact-us section" id="#">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6  align-self-center">
                            <div class="section-heading">
                                <h3>Download Universal Abroad for Recruiters App</h3>
                                <p>Thank you for choosing Universal Abroad Application.
                                    <br />
                                    <em><strong>Please Scan QR Code</strong></em></p>
                                <div class="special-offer">
                                    <img src="IndexAssets/images/QR-Code.png" class="img-fluid" />                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="contact-us-content">
                                <img src="IndexAssets/images/UEMobile.png" class="img-fluid"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
     
                <footer>
                    <div class="container py-vh-4 text-white fw-lighter">
                        <div class="row">
                            <div class="col-sm text-center text-lg-start">
                                <a class="navbar-brand pe-md-4 fs-4 col-12 col-md-auto text-center" href="Newindex.aspx">
                                    <img src="IndexAssets/images/UELogo.png" width="224" height="100" fill="currentColor" class="bi bi-stack img-fluid" viewbox="0 0 16 16"/>
                                    <path d="m14.12 10.163 1.715.858c.22.11.22.424 0 .534L8.267 15.34a.598.598 0 0 1-.534 0L.165 11.555a.299.299 0 0 1 0-.534l1.716-.858 5.317 2.659c.505.252 1.1.252 1.604 0l5.317-2.66zM7.733.063a.598.598 0 0 1 .534 0l7.568 3.784a.3.3 0 0 1 0 .535L8.267 8.165a.598.598 0 0 1-.534 0L.165 4.382a.299.299 0 0 1 0-.535L7.733.063z">
                                        <path d="m14.12 6.576 1.715.858c.22.11.22.424 0 .534l-7.568 3.784a.598.598 0 0 1-.534 0L.165 7.968a.299.299 0 0 1 0-.534l1.716-.858 5.317 2.659c.505.252 1.1.252 1.604 0l5.317-2.659z">
                                        </path>
                                    </path>
                                </a>

                            </div>
                            <div class="col-6 col-md-4 border-end border-light mb-2">
                                <span class="text-white text-nowrap font-weight-bold h5">UNIVERSAL ABROAD</span>
                                <ul class="nav flex-column">
                                    <li class="nav-item">
                                        <a id="HomeHL" href="Newindex.aspx" class="text-white">HOME</a>
                                    </li>
<%--                                    <li class="nav-item">
                                        <a href="#LoginModalCenter" data-toggle="modal" data-target="#LoginModalCenter" class="text-white">LOGIN</a>
                                    </li>--%>
                                    <li class="nav-item">
                                        <a href="TermsCondition.aspx" class="text-white">TERMS &amp; CONDITION</a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="PrivacyPolicy.aspx" class="text-white">PRIVACY POLICY</a>
                                    </li>
                                </ul>
                            </div>
                            
                            <div class="col-6 col-md-4 mb-2">
                                <span class="text-white font-weight-bold h5">SUPPORT</span>
                                <ul class="nav flex-column">
                                    <li class="nav-item">
                                       EMAIL ID:- <a href="mailto:Universal.abroadjobs23@gmail.com" class="text-white text-wrap" style="word-break:break-all;">Universal.abroadjobs23@gmail.com</a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="Aboutus.aspx" class="text-white">ABOUT US</a>
                                    </li>
                                    <li class="nav-item">
                                        <a href="Contact.aspx" class="text-white">CONTACT US</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="container text-center small py-vh-2 border-top border-light">
                        <div class="row">
                            <div class="col-6 p-1">
                                <div class="d-flex footer-nav-left">
                                    <ul class="d-flex">
                                        <li>
                                            <a href="https://www.facebook.com/people/universalabroadjobs23/100091846080129/?mibextid=ZbWKwL" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/facebook.png" width="30" height="30" alt="ZURB Foundation Facebook"></a>
                                        </li>
                                        <p></p>
                                        <li>
                                            <a href="https://instagram.com/universal.abroadjob23?igshid=YmMyMTA2M2Y=" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/instagram.png" width="30" height="30" alt="ZURB Foundation Twitter"></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://twitter.com/abroadjob23?t=paG2CECMX5pBsNTSL8x7ew&amp;s=08" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/twitter.png" width="30" height="30" alt="ZURB Foundation Youtube"></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://www.linkedin.com/in/universal-abroad-jobs-02916b271/" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/linkedin.png" width="30" height="30" alt="Get Involved with Zurb Foundation"></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://www.youtube.com/@universal.abroadjobs23" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/youtube.png" width="30" height="30" alt="Get Involved with Zurb Foundation"></a>
                                        </li>
                                    </ul></div>
                                
                            </div>
                            <div class="col-6 text-right float-right p-2">
                                <label class="text-white h6" style="text-decoration: none;" target="_blank"><span>Copyright © 2023</span> UniversalEducation. All Rights Reserved.</label>
                            </div>
                        </div>
                    </div>
                </footer>
       
     
        <div class="modal fade" id="LoginModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content-login">
                    <div class="login-box box">
                        <span></span>
                        <span></span>
                        <span></span>
                        <span></span>
                        <div class="modal-header">
                            <div class="modal-title">
                                <img src="LandingPageAsset/img/UE-LOGO-2.png" alt="UE-LOGO" style="width: 200px; height: 70px;" />
                            </div>
                        </div>
                        <div class="modal-body">

                            <div class="title">MIS Login</div>
                            <div class="input-group">
                               <div class="input-group-addon">
                                    <i class="far fa-envelope" id="Email"></i>
                                </div>
                                <asp:TextBox ID="UserNameTXT" runat="server" CssClass="form-control" placeholder="Your user-name*"></asp:TextBox>
                            </div>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="far fa-eye" id="togglePassword"></i>
                                </div>
                                <asp:TextBox ID="PasswordTXT" runat="server" TextMode="Password" CssClass="form-control" placeholder="Your password*"></asp:TextBox>
                            </div>
                            <div class="row p-3">
                                <div class="col-xs-2">
                                    <div class="checkbox">
                                        <asp:CheckBox ID="RememberCHK" runat="server"/>
                                    </div>
                                </div>
                                <div class="col-xs-5">
                                    <label class="text-nowrap">Remember me</label>
                                </div>
                            </div>
                                       
                                    <%--<asp:CheckBox ID="RememberCHK"  runat ="server" Text="Remember me"/>  
                                    <asp:CheckBox ID="TermsConditionsCHK" runat="server" Text="I agree to the terms and conditions and the privacy policy"/>
                            --%>       
                            <div class="p-t-10">
                                <asp:Button ID="UESigninLB" runat="server" Text="Sign In" CssClass="btn btn--pill btn--signin"  OnClick="UESigninLB_Click" />             
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="UELoginLBL" runat="server" CssClass="text-nowrap" Font-Italic="true" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                            </div>    
                           
                        </div>
                        <div class="modal-footer">

                        </div>
                    </div>
                </div>
            </div>
        </div>
       
        
        <div class="modal fade bs-example-modal-lg" id="TermsconditionValidation" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
                <div class="modal-content ">
                    <div class="section">
                        <div class="container">
                            <center><img src="IndexAssets/images/UE-LOGO-2.png" style="width:12rem;"/></center>
                            <div class="clearfix"><br/></div>
                            <h5 class="text-center">TERMS AND CONDITIONS</h5>
                            <p>
                                <strong>You must take the time to read and understand the Terms and Conditions before registering for our services. By registering, you accept that you are entering into a contract with us based on these Terms and Conditions.
                Visitors to universal-abroadjobs.com who do not register to become a Member (Employer or Employee) similarly affirm that they are bound by these Terms and Conditions each time they access the universal-abroadjobs.com.
                If you do not accept the Terms and Conditions stated below, please refrain from using universal-abroadjobs.com.
                                </strong>
                                <br />                               
                                1.	The use of the Website by an Employer or Employee shall be deemed acceptance of and agreement to these terms only.<br />
                                2.	The Website has been established to allow Employer to contact Employees and to view their profiles detailing an Employee's experience. We do not issue any experience certificate to our registered members.<br />
                                3.	Any profile created showing political or illegal material would not be accepted under any circumstances.<br />
                                4.	universal-abroadjobs.com will take all reasonable precautions to keep the details of Employers and Employees secure but will not be liable for unauthorized access to the information provided by any party whatsoever.<br />
                                5.	The Members warrant that their e-mail and other contact addresses are valid and up to date when using the Website.<br />
                                6.	Members agree not to impersonate any other person or entity or to use a false name or a name that they have no authority to use.<br />
                                7.	Members acknowledge that universal-abroadjobs.com is not liable for any form of loss or damage that may be suffered by a Member through the use of the Website including loss of data or information or any kind of financial or physical loss or damage.<br />
                                8.	universal-abroadjobs.com privacy policy forms part of these Terms and Conditions, and by agreeing to these Terms and Conditions, you also give your consent to the manner in which we may handle your personal data as detailed in that policy.<br />
                                9.	The management reserves the right to modify the Terms and Conditions at any time without any prior notification.<br />
                                10.	These Terms will be subject to Indian Law and the jurisdiction of Indian Courts.<br />
                                11.	We do not cater to Placement Agencies and consultancies. Any payments made by Placement Agencies/ Consultancies will not be refunded under any situation.<br />
                                12.	universal-abroadjobs.com is not responsible if any candidate has committed crime/illegal activity at employer's premises. Background verification of candidates who are/will be hired is a responsibility of respective recruiter/recruiter's company.
                            </p>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-sm">
                                        <asp:LinkButton ID="but1" runat="server" value="I DO accept the terms and conditions" CssClass="btn  btn-success btn-block text-white" data-toggle="modal" data-dismiss="modal" data-target="#SignupModalCenter">I DO accept the terms and conditions</asp:LinkButton>
                                    </div>
                                    <br/>
                                    <div class="col-sm">
                                        <asp:LinkButton ID="but2" runat="server" value="I DO NOT accept the terms and conditions" CssClass="btn btn-success btn-block text-white">I DO NOT accept the terms and conditions</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                      </div>
                    </div>
                 </div>
            </div>
         </div>
    


        <div class="modal fade" id="SignupModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content-signup">
                    <div class="login-box box">
                        <span></span>
                        <span></span>
                        <span></span>
                        <span></span>
                        <div class="modal-header">
                            <div class="modal-title">
                                <img src="LandingPageAsset/img/UE-LOGO-2.png" alt="UE-LOGO" style="width: 200px; height: 70px;" />
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="title">Sign Up</div>
                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link active" id="pills-Employer-tab" data-bs-toggle="pill" data-bs-target="#pills-Employer" type="button" role="tab" aria-controls="pills-Employer" aria-selected="true">Employer</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link" id="pills-Candidate-tab" data-bs-toggle="pill" data-bs-target="#pills-Candidate" type="button" role="tab" aria-controls="pills-Candidate" aria-selected="false">Candidate</button>
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="pills-tabContent">
                                        <div class="tab-pane fade show active" id="pills-Employer" role="tabpanel" aria-labelledby="pills-Employer-tab">
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_EmployerUserNameTXT" runat="server" CssClass="input--style-3" placeholder="Employer Username"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_EmployerEmailTXT" runat="server" CssClass="input--style-3" placeholder="Employer Email"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_EmployerPasswordTXT" runat="server" TextMode="Password" CssClass="input--style-3" placeholder="Employer Password"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_EmployerEmployerRePasswordTXT" runat="server" TextMode="Password" CssClass="input--style-3" placeholder="Employer Confirm Password"></asp:TextBox>
                                            </div>
                                            <asp:LinkButton ID="CETRMS_EmployerSingUpLB" runat="server" type="submit" OnClick="EmployerPersonalProfileSignup" OnClientClick="return ValidateEmployerSignUp();" CssClass="btn btn--pill btn--signin">Employer Signup</asp:LinkButton>
                                        </div>
                                        <div class="tab-pane fade" id="pills-Candidate" role="tabpanel" aria-labelledby="pills-Candidate-tab">
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_CandidateUserNameTXT" runat="server" CssClass="input--style-3" placeholder="Candidate Username"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_CandidateEmailTXT" runat="server" CssClass="input--style-3" placeholder="Candidate Email"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_CandidatePasswordTXT" runat="server" TextMode="Password" CssClass="input--style-3" placeholder="Candidate Password"></asp:TextBox>
                                            </div>
                                            <div class="input-group">
                                                <asp:TextBox ID="CETRMS_CandidateRePasswordTXT" runat="server" TextMode="Password" CssClass="input--style-3" placeholder="Candidate Confirm Password"></asp:TextBox>
                                            </div>
                                            <asp:LinkButton ID="CETRMS_CandidateSignUpLB" runat="server" type="submit" OnClick="CandidatePersonalProfileSignup" OnClientClick="return ValidateCandidateSignUp();" CssClass="btn btn--pill btn--signin">Candidate Signup</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 justify-content-center" style="color: #339dad;">
                                    OR Sign-up by
                            <asp:HiddenField ID="loginProspectHF" runat="server" />
                                </div>
                            </div>
                            <div class="row d-flex">
                                <div class="col">
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <div class="flex--item Google-Signup" data-ga="[&quot;sign up&quot;,&quot;Sign Up Started - Google&quot;,&quot;New Post&quot;,null,null]">
                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="GoogleSignUp">
                                    <svg aria-hidden="true" class="native svg-icon iconGoogle" width="40" height="40"  viewBox="0 0 18 18"><path fill="#4285F4" d="M16.51 8H8.98v3h4.3c-.18 1-.74 1.48-1.6 2.04v2.01h2.6a7.8 7.8 0 0 0 2.38-5.88c0-.57-.05-.66-.15-1.18Z"/><path fill="#34A853" d="M8.98 17c2.16 0 3.97-.72 5.3-1.94l-2.6-2a4.8 4.8 0 0 1-7.18-2.54H1.83v2.07A8 8 0 0 0 8.98 17Z"/><path fill="#FBBC05" d="M4.5 10.52a4.8 4.8 0 0 1 0-3.04V5.41H1.83a8 8 0 0 0 0 7.18l2.67-2.07Z"/><path fill="#EA4335" d="M8.98 4.18c1.17 0 2.23.4 3.06 1.2l2.3-2.3A8 8 0 0 0 1.83 5.4L4.5 7.49a4.77 4.77 0 0 1 4.48-3.3Z"/></svg> 
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col">
                                    <div class="flex--item Facebook-Signup" data-ga="[&quot;sign up&quot;,&quot;Sign Up Started - Facebook&quot;,&quot;New Post&quot;,null,null]">
                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="FacebookSignUp">
                                    <svg aria-hidden="true" class="svg-icon iconFacebook" width="40" height="40"  viewBox="0 0 18 18"><path fill="#4167B2" d="M3 1a2 2 0 0 0-2 2v12c0 1.1.9 2 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2H3Zm6.55 16v-6.2H7.46V8.4h2.09V6.61c0-2.07 1.26-3.2 3.1-3.2.88 0 1.64.07 1.87.1v2.16h-1.29c-1 0-1.19.48-1.19 1.18V8.4h2.39l-.31 2.42h-2.08V17h-2.5Z"/></svg>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
 

                <div class="action main-content1" onclick="actionToggle();">
                    <div class="row">
                        <div class="col-lg-12">
                            <span>
                                <img src="IndexAssets/images/hourglass.gif" class="img-fluid"/> 
                            </span>

                            <ul>
                                <li>
                                    <div class="container">

                                        <div class="row mb-4">
                                            <div class="col">
                                                <center>
                                                    <img src="LandingPageAsset/img/UE-LOGO_1.png" class="img-fluid" /></center>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <h5 class="text-center">Install our
                                    <br />
                                                    Mobile Application
                                    <br />
                                                    &<br />
                                                    Get Hire Now</h5>
                                            </div>
                                        </div>
                                        <div class="row d-flex">
                                            <div class="col">
                                                <a href="CandidateMobileApplication.aspx">
                                                    <img src="IndexAssets/images/google-playstore.svg" width="100" /></a>
                                            </div>
                                            <div class="col">
                                                <a href="CandidateMobileApplication.aspx">
                                                    <img src="IndexAssets/images/app-store.svg" width="100" /></a>
                                            </div>
                                        </div>

                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="action main-content">
                    <div class="joinchat joinchat--right joinchat--show joinchat--tooltip" data-settings="{&quot;telephone&quot;:&quot;918000269423&quot;,&quot;mobile_only&quot;:false,&quot;button_delay&quot;:3,&quot;whatsapp_web&quot;:true,&quot;message_views&quot;:2,&quot;message_delay&quot;:10,&quot;message_badge&quot;:false,&quot;message_send&quot;:&quot;&quot;,&quot;message_hash&quot;:&quot;&quot;}">
                        <div class="joinchat__button">
                            <div class="joinchat__button__open"></div>
                            <div class="joinchat__button__sendtext">Open chat</div>
                        </div>
                        <svg height="0" width="0">
                            <defs>
                                <clipPath id="joinchat__message__peak">
                                    <path d="M17 25V0C17 12.877 6.082 14.9 1.031 15.91c-1.559.31-1.179 2.272.004 2.272C9.609 18.182 17 18.088 17 25z"></path>
                                </clipPath>
                            </defs></svg>
                    </div>
                </div>

                <%-- COOKIE CONSENT STARTS--%>
                <div id="consentBox" class="consentBox" >
                    <div class="cookieTitle ">
                        <i class="bx bx-cookie"></i>
                        <h4>Cookies consent</h4>
                    </div>
                    <div class="contentSection">
                        <div class="contentText">
                            <p>This website use cookies to help you have a superior and more relevant browsing experience on the website. By accepting all cookies you agree to our  <a href="#">Privacy Policy</a></p>
                        </div>
                    </div>
                    <div class="btn">
                        <button class="acceptButton" id="acceptBtn" onclick="acceptCookies(); return false">Accept</button>
                        <button class="acceptButton" id="declineBtn" onclick="acceptCookies(); return false">Decline</button>
                        <%--<button class="acceptButton" id="settingBtn" onclick="acceptCookies(); return false">Cookies Setting</button>--%>
                    </div>
                </div>

            </div>
        </div>
    </form>
    <!-- Scripts -->
    <!-- Bootstrap core JavaScript --> 

    <script src="IndexAssets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://assets.codepen.io/7773162/vanilla-tilt.min.js"></script>

    <%-- BOX ICON JS --%>
    <script src="https://unpkg.com/boxicons@2.1.4/dist/boxicons.js"></script>

    <%--<script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>--%>

    <!--Remember Me-->
    <%--<script>
        const rmCheck = document.getElementById("RememberCHK"),
            UserName = document.getElementById("UserNameTXT");

        if (localStorage.checkbox && localStorage.checkbox !== "") {
            rmCheck.setAttribute("checked", "checked");
            UserName.value = localStorage.username;
        } else {
            rmCheck.removeAttribute("checked");
            UserName.value = "";
        }

        function UESigninLB_Click() {
            if (rmCheck.checked && UserName.value !== "") {
                localStorage.username = UserNameTXT.value;
                localStorage.checkbox = rmCheck.value;
            } else {
                localStorage.username = "";
                localStorage.checkbox = "";
            }
        }
    </script>--%>


    <script>
        const togglePassword = document.querySelector('#togglePassword');
        const password = document.querySelector('#PasswordTXT');

        togglePassword.addEventListener('click', function (e) {
            // toggle the type attribute
            const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
            password.setAttribute('type', type);
            // toggle the eye slash icon
            this.classList.toggle('fa-eye-slash');
        });
    </script>




    <script>
        VanillaTilt.init(document.querySelectorAll(".box"), {
            max: 1,
            speed: 300,
            easing: "cubic-bezier(.03,.98,.52,.99)",
            scale: 1.05,
        });
    </script>
      <script>
          function myFunction() {
              alert("Page is loaded");
          }
          function Redirect() {
              window.location = "#SignupModalCenter";
          }
     </script>
    

    <%--Side panel script--%>
    <script type="text/javascript">
        function actionToggle() {
            var action = document.querySelector('.action');
            action.classList.toggle('active');
        }

        function ValidateEmployerSignUp() {
            var EmployerUserNameTXT = document.getElementById('CETRMS_EmployerUserNameTXT');
            var EmployerEmailTXT = document.getElementById('CETRMS_EmployerEmailTXT');
            var EmployerPasswordTXT = document.getElementById('CETRMS_EmployerPasswordTXT');
            var EmployerEmployerRePasswordTXT = document.getElementById('CETRMS_EmployerEmployerRePasswordTXT');

            let EmployerUserNameText = EmployerUserNameTXT.value;
            let EmployerEmailText = EmployerEmailTXT.value;
            let EmployerPasswordText = EmployerPasswordTXT.value;
            let EmployerEmployerRePasswordText = EmployerEmployerRePasswordTXT.value;

            if (EmployerUserNameText.trim() == "") {
                alert("Employer user name cannot be blank.");
                return false;
            }
            if (EmployerEmailText.trim() == "") {
                alert("Employer email cannot be blank.");
                return false;
            }
            if (EmployerPasswordText.trim() == "") {
                alert("Employer password cannot be blank.");
                return false;
            }
            if (EmployerEmployerRePasswordText.trim() == "") {
                alert("Employer re-password cannot be blank.");
                return false;
            }
            if (EmployerEmployerRePasswordText.trim() != EmployerPasswordText.trim()) {
                alert("Retyped password is not matching.");
                return false;
            }
        }
        function ValidateCandidateSignUp() {
            var CandidateUserNameTXT = document.getElementById('CETRMS_CandidateUserNameTXT');
            var CandidateEmailTXT = document.getElementById('CETRMS_CandidateEmailTXT');
            var CandidatePasswordTXT = document.getElementById('CETRMS_CandidatePasswordTXT');
            var CandidateRePasswordTXT = document.getElementById('CETRMS_CandidateRePasswordTXT');

            let CandidateUserNameText = CandidateUserNameTXT.value;
            let CandidateEmailText = CandidateEmailTXT.value;
            let CandidatePasswordText = CandidatePasswordTXT.value;
            let CandidateRePasswordText = CandidateRePasswordTXT.value;

            if (CandidateUserNameText.trim() == "") {
                alert("Candidate user name cannot be blank.");
                return false;
            }
            if (CandidateEmailText.trim() == "") {
                alert("Candidate email cannot be blank.");
                return false;
            }
            if (CandidatePasswordText.trim() == "") {
                alert("Candidate password cannot be blank.");
                return false;
            }
            if (CandidateRePasswordText.trim() == "") {
                alert("Candidate re-password cannot be blank.");
                return false;
            }
            if (CandidateRePasswordText.trim() != CandidatePasswordText.trim()) {
                alert("Retyped password is not matching.");
                return false;
            }
        }
    </script>

    <script>
        function myFunction() {
            var x = document.getElementById("PasswordTXT");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
    <script>
        var input = document.getElementById("LoginModalCenter");
        input.addEventListener("keypress", function (event) {
            if (event.key === "Enter") {
                event.preventDefault();
                document.getElementById("UESigninLB").click();
            }
        });
    </script>

  <%-- COOKIE CONSENT JS--%>
    <script>
        let consentBox = document.getElementById('consentBox');;
        function acceptCookies() {
            consentBox.style.display = "none";
        }
    </script>

    <script src="IndexAssets/js/isotope.min.js"></script>
    <script src="IndexAssets/js/owl-carousel.js"></script>
    <script src="IndexAssets/js/counter.js"></script>
    <script src="IndexAssets/js/custom.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="IndexAssets/bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <script src="LandingPageAsset/Megamenu.js"></script>
    <script src="IndexAssets/Joinchat.js"></script>
</body>
</html>
