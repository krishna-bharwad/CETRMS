<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CandidateMobileApplication.aspx.cs" Inherits="CETRMS.CandidateMobileApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="images/Logo.ico" type="image/ico" />
    <title>Universal Education</title>
    <link rel="stylesheet" href="IndexAssets/css/fontawesome.css" />
    <link rel="stylesheet" href="IndexAssets/css/templatemo-scholar.css" />
    <%--<link rel="stylesheet" href="IndexAssets/css/owl.css" />--%>
    <link rel="stylesheet" href="IndexAssets/css/animate.css" />
    <%--<link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css" />--%>
    <link rel="stylesheet" href="IndexAssets/Heroes.css" />
    <link rel="stylesheet" href="IndexAssets/bootstrap-4.0.0-dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="LandingPageAsset/loginModal.css" />
    <link rel="stylesheet" href="LandingPageAsset/Megamenu.css" />
    <link rel="stylesheet" href="IndexAssets/Termscondition.css" />

    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&family=Rasa&display=swap" rel="stylesheet" />
    <script src="IndexAssets/vendor/jquery/jquery.min.js"></script>

    <link href="MobileApp.css" rel="stylesheet" />

    <style>
        * {
            font-family: 'Rasa', serif;
        }

        .col, .col-1, .col-10, .col-11, .col-12, .col-2, .col-3, .col-4, .col-5, .col-6, .col-7, .col-8, .col-9, .col-auto, .col-lg, .col-lg-1, .col-lg-10, .col-lg-11, .col-lg-12, .col-lg-2, .col-lg-3, .col-lg-4, .col-lg-5, .col-lg-6, .col-lg-7, .col-lg-8, .col-lg-9, .col-lg-auto, .col-md, .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-3, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9, .col-md-auto, .col-sm, .col-sm-1, .col-sm-10, .col-sm-11, .col-sm-12, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-auto, .col-xl, .col-xl-1, .col-xl-10, .col-xl-11, .col-xl-12, .col-xl-2, .col-xl-3, .col-xl-4, .col-xl-5, .col-xl-6, .col-xl-7, .col-xl-8, .col-xl-9, .col-xl-auto {
            position: relative;
            width: 100%;
            min-height: 1px;
        }
        /* .header-area {
            position: absolute;
            background-color: transparent;
            top: 15px;
            left: 0;
            right: 0;
            z-index: 100;
            -webkit-transition: all .5s ease 0s;
            -moz-transition: all .5s ease 0s;
            -o-transition: all .5s ease 0s;
            transition: all .5s ease 0s;
        }*/
        .header-area .main-nav ul.nav {
            border-radius: 0px 0px 25px 25px;
            flex-basis: 100%;
            margin-right: 0px;
            justify-content: right;
            -webkit-transition: all 0.3s ease 0s;
            -moz-transition: all 0.3s ease 0s;
            -o-transition: all 0.3s ease 0s;
            transition: all 0.3s ease 0s;
            position: relative;
            z-index: 999;
            margin-top: 10px;
        }

        .header-area {
            background: linear-gradient(to bottom, #66acac 0%, #ffffff 100%);
            border-radius: 0px 0px 25px 25px;
            /*height: 80px !important;*/
            position: fixed !important;
            top: 0 !important;
            left: 0;
            right: 0;
            box-shadow: 0px 0px 10px rgba(0,0,0,0.15) !important;
            -webkit-transition: all .5s ease 0s;
            -moz-transition: all .5s ease 0s;
            -o-transition: all .5s ease 0s;
            transition: all .5s ease 0s;
        }

        .input-group-addon {
            color: rgb(255, 255, 255);
            /*background-color: #ffffff;*/
            padding: 10px;
        }

        input#UserNameTXT.form-control .active {
            background: transparent;
        }

        .input-group > .custom-select:not(:first-child), .input-group > .form-control:not(:first-child) {
            border-top-left-radius: 0;
            border-bottom-left-radius: 0;
            border-color: transparent;
            color: #fff;
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
                padding-right: 14px;
                padding-left: 14px;
            }
        }

        /*@media (max-width: 767px) {
            .header-area .main-nav {
                overflow: hidden;
            }
        }*/

        .row {
            margin-top: 0;
            display: webkit-box;
            flex-wrap: wrap;
        }

        .bottom {
            position: absolute;
            bottom: 10px;
            margin-left: 4.8rem;
        }

        .btn-group-sm > .btn, .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: .875rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }

        .btn-outline-success {
            color: #66acac;
            background-color: transparent;
            background-image: none;
            border-color: #66acac;
        }

        .card {
            position: initial;
            width: 100%;
            height: auto;
        }

        .cardbetter {
            position: initial;
            width: 100%;
            height: 15rem;
            margin-top: -50px;
        }

        .modal-content {
            position: relative;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 100%;
            max-width: 100vw;
            pointer-events: auto;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 0.3rem;
            padding: 0px 16px 20px;
            outline: 0;
            z-index: 1050;
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
            background-color: transparent;
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
            background-color: transparent;
            background-clip: padding-box;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 0.3rem;
            outline: 0;
        }

        .modal-footer {
            justify-content: center;
        }

        .img-fluid {
            width: 65%;
        }

        .modal-dialog {
            position: relative;
            width: auto;
            /* margin: 0.5rem; */
            /* margin-left: 0.5rem; */
            /* margin-right: 0.5rem; */
            pointer-events: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <header class="header-area header-sticky background-header">
                        <div class="container">
                            <div class="row">
                                <div class="col-12">
                                    <nav class="main-nav">
                                        <!-- ***** Logo Start ***** -->
                                        <a href="Newindex.aspx" class="logo">
                                            <img src="IndexAssets/images/UE-LOGO-2.png" alt="UE-LOGO" style="width: 175px;" />
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
                                                <asp:LinkButton ID="LoginModalLB" runat="server" type="button" title="Log In" data-toggle="modal" data-target="#LoginModalCenter" OnClick="LoginModalLB_Click">
                                              Log In
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
                     <div class="row textContentRow">
        <div class="col span_1_of_2">
          <div class="hero-description">
            <h1>
              Universal Abroad Jobs <br />
            </h1>
            <p>
              With <span class="bold-text">CETRMS </span>, Get your dream job and better placement!
              Download Universal Abroad App
              Dream Job!!!
            </p>

            <%--<a href="app/CETRMS_Employer.apk" class="btn"><span class="download-btn"> Employer Download Now</span></a>--%>
              <asp:Button  ID="EmployerAPKDownload" runat="server" CssClass="download-btn btn"  Text="Employer Download Now" OnClick="EmployerAPKDownload_Click"/>
            <br><br><br>

              <asp:Button  ID="CandidateAPKDownload" runat="server" CssClass="download-btn btn"  Text="Candidate Download Now" OnClick="CandidateAPKDownload_Click"/>
           <%-- <a href="app/CETRMS_Candidate.apk" class="btn"><span class="download-btn"> Candidate Download Now</span></a>--%>

          </div>
        </div>
        <div class="col span_1_of_2">
          <img class="hero-img" src="images/UEMobile.png" alt="" />
        </div>
      </div>


                </div>
            </div>


            <div class="clearfix">
                <br />
                <br />
                <br />
                <br />
            </div>


            <section id="features">
                <div class="row">
                    <div class="col span_1_of_2">
                        <div class="features-images">
                            <img class="features_1" src="images/Apple iPhone SE Screenshot 1.png" alt="" />
                        </div>
                    </div>
                    <div class="col span_1_of_2">
                        <div class="section-description features-description col1">
                            <h2 class="stylish_heading">For Candidate <span class="red_dot">.</span>
                            </h2>
                            <p class="little-description">
                                *Post a Job takes less than 5 minutes and Register via OTP

            *Pay registration fees 50$ Yearly.

            *Get verified our team will get in touch
                            </p>
                        </div>
                    </div>
                </div>
                <div class="row second-features-row">
                    <div class="col span_1_of_2">
                        <div class="section-description features-description">
                            <h2 class="stylish_heading">Get calls. Hire. <span class="red_dot">.</span>
                            </h2>
                            <p class="little-description">
                                You will get calls from relevant candidates within one hour or call them directly from our candidate
            database.
                            </p>
                        </div>
                    </div>
                    <div class="col span_1_of_2">
                        <div class="features-images">
                            <img class="features_3" src="images/Apple iPhone SE Screenshot 3.png" alt="" />
                        </div>
                    </div>
                </div>
            </section>

            <!-- How To Section -->
            <section id="howto">
                <div class="row">
                    <div class="col span_1_of_3 first-col">
                        <div class="first-item">
                            <div class="item-description">
                                <h4>Simple Hiring</h4>
                                <p>
                                    Recieve calls from qualified candidates in under an hour of posting a job
                                </p>
                            </div>
                            <div class="item-icon1">
                                <svg width="2.5rem" height="2.5rem" viewBox="0 0 16 16" class="bi bi-chat-dots" fill="currentColor"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M2.678 11.894a1 1 0 0 1 .287.801 10.97 10.97 0 0 1-.398 2c1.395-.323 2.247-.697 2.634-.893a1 1 0 0 1 .71-.074A8.06 8.06 0 0 0 8 14c3.996 0 7-2.807 7-6 0-3.192-3.004-6-7-6S1 4.808 1 8c0 1.468.617 2.83 1.678 3.894zm-.493 3.905a21.682 21.682 0 0 1-.713.129c-.2.032-.352-.176-.273-.362a9.68 9.68 0 0 0 .244-.637l.003-.01c.248-.72.45-1.548.524-2.319C.743 11.37 0 9.76 0 8c0-3.866 3.582-7 8-7s8 3.134 8 7-3.582 7-8 7a9.06 9.06 0 0 1-2.347-.306c-.52.263-1.639.742-3.468 1.105z" />
                                    <path
                                        d="M5 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm4 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                                </svg>
                            </div>
                        </div>
                        <div class="first-item">
                            <div class="item-description">
                                <h4>Intelligent Recommendations</h4>
                                <p>
                                    Only the best candidates are recommended by our ML as per your requirement
                                </p>
                            </div>
                            <div class="item-icon1">
                                <svg width="2.5rem" height="2.5rem" viewBox="0 0 16 16" class="bi bi-telephone-outbound" fill="currentColor"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M3.654 1.328a.678.678 0 0 0-1.015-.063L1.605 2.3c-.483.484-.661 1.169-.45 1.77a17.568 17.568 0 0 0 4.168 6.608 17.569 17.569 0 0 0 6.608 4.168c.601.211 1.286.033 1.77-.45l1.034-1.034a.678.678 0 0 0-.063-1.015l-2.307-1.794a.678.678 0 0 0-.58-.122l-2.19.547a1.745 1.745 0 0 1-1.657-.459L5.482 8.062a1.745 1.745 0 0 1-.46-1.657l.548-2.19a.678.678 0 0 0-.122-.58L3.654 1.328zM1.884.511a1.745 1.745 0 0 1 2.612.163L6.29 2.98c.329.423.445.974.315 1.494l-.547 2.19a.678.678 0 0 0 .178.643l2.457 2.457a.678.678 0 0 0 .644.178l2.189-.547a1.745 1.745 0 0 1 1.494.315l2.306 1.794c.829.645.905 1.87.163 2.611l-1.034 1.034c-.74.74-1.846 1.065-2.877.702a18.634 18.634 0 0 1-7.01-4.42 18.634 18.634 0 0 1-4.42-7.009c-.362-1.03-.037-2.137.703-2.877L1.885.511zM11 .5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 .5.5v4a.5.5 0 0 1-1 0V1.707l-4.146 4.147a.5.5 0 0 1-.708-.708L14.293 1H11.5a.5.5 0 0 1-.5-.5z" />
                                </svg>
                            </div>
                        </div>
                    </div>
                    <div class="col span_1_of_3 second-col">
                        <img class="big_image" src="images/Apple iPhone SE Screenshot 4.png" alt="" />
                    </div>
                    <div class="col span_1_of_3 third-col">
                        <div class="last-item">
                            <div class="item-icon2">
                                <svg width="2.5rem" height="2.5rem" viewBox="0 0 16 16" class="bi bi-camera" fill="currentColor"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M15 12V6a1 1 0 0 0-1-1h-1.172a3 3 0 0 1-2.12-.879l-.83-.828A1 1 0 0 0 9.173 3H6.828a1 1 0 0 0-.707.293l-.828.828A3 3 0 0 1 3.172 5H2a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1zM2 4a2 2 0 0 0-2 2v6a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2h-1.172a2 2 0 0 1-1.414-.586l-.828-.828A2 2 0 0 0 9.172 2H6.828a2 2 0 0 0-1.414.586l-.828.828A2 2 0 0 1 3.172 4H2z" />
                                    <path fill-rule="evenodd"
                                        d="M8 11a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5zm0 1a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z" />
                                    <path d="M3 6.5a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0z" />
                                </svg>
                            </div>
                            <div class="item-description">
                                <h4>Priority customer support</h4>
                                <p>
                                    Prioritized customer support for the paid plan users
                                </p>
                            </div>
                        </div>
                        <div class="last-item">
                            <div class="item-icon2">
                                <svg width="2.5rem" height="2.5rem" viewBox="0 0 16 16" class="bi bi-camera-video" fill="currentColor"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M0 5a2 2 0 0 1 2-2h7.5a2 2 0 0 1 1.983 1.738l3.11-1.382A1 1 0 0 1 16 4.269v7.462a1 1 0 0 1-1.406.913l-3.111-1.382A2 2 0 0 1 9.5 13H2a2 2 0 0 1-2-2V5zm11.5 5.175l3.5 1.556V4.269l-3.5 1.556v4.35zM2 4a1 1 0 0 0-1 1v6a1 1 0 0 0 1 1h7.5a1 1 0 0 0 1-1V5a1 1 0 0 0-1-1H2z" />
                                </svg>
                            </div>
                            <div class="item-description">
                                <h4>Send your video</h4>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
              eiusmod tempor incididunt ut
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- End: How To Section -->



            <!-- Subscribe Section -->
            <section id="subscribe">
                <div class="row">
                    <h2 class="stylish_heading">Subscribe to our newsletter <span class="red_dot">.</span>
                    </h2>
                    <p class="">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
        eiusmod tempor incididunt ut labore et dolore magna.
                    </p>
                </div>

            </section>
            <!-- End: Subscribe Section -->


            <!-- Download The App Section -->
            <section id="download">
                <div class="row d-flex">
                    <button class="appDownloadBtn" onclick="">
                        <img src="images/PlayStore.jpg" alt="" /></button>
                    <button class="appDownloadBtn" onclick="">
                        <img src="images/AppStore.jpg" alt="" /></button>

                </div>
            </section>
            <!-- End: Download The App Section -->



            <footer>
                <div class="container py-vh-4 text-white fw-lighter">
                    <div class="row">
                        <div class="col-sm text-center text-lg-start">
                            <a class="navbar-brand pe-md-4 fs-4 col-12 col-md-auto text-center" href="Newindex.aspx">
                                <img src="LandingPageAsset/img/UE-LOGO-2.png" width="224" height="100" fill="currentColor" class="bi bi-stack img-fluid" viewbox="0 0 16 16">
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
                                <li class="nav-item">
                                    <a href="#LoginModalCenter" data-toggle="modal" data-target="#LoginModalCenter" class="text-white">LOGIN</a>
                                </li>
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
                                <li class="nav-item">EMAIL ID:- <a href="mailto:Universal.abroadjobs23@gmail.com" class="text-white text-wrap" style="word-break: break-all;">Universal.abroadjobs23@gmail.com</a>
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
                                <ul class="footerSocialList">
                                    <li>
                                        <a href="https://www.facebook.com/people/universalabroadjobs23/100091846080129/?mibextid=ZbWKwL" target="_blank">
                                            <img class="footer-social-icon" src="IndexAssets/images/facebook.png"  alt="ZURB Foundation Facebook" /></a>
                                    </li>
                                    <p></p>
                                    <li>
                                        <a href="https://instagram.com/universal.abroadjob23?igshid=YmMyMTA2M2Y=" target="_blank">
                                            <img class="footer-social-icon" src="IndexAssets/images/instagram.png" width="30" height="30" alt="ZURB Foundation Twitter"></a>
                                    </li>
                                    <p></p>
                                    <li>
                                        <a href="https://twitter.com/abroadjob23?t=paG2CECMX5pBsNTSL8x7ew&amp;s=08" target="_blank">
                                            <img class="footer-social-icon" src="IndexAssets/images/twitter.png" width="30" height="30" alt="ZURB Foundation Youtube" /></a>
                                    </li>
                                    <p></p>
                                    <li>
                                        <a href="https://www.linkedin.com/in/universal-abroad-jobs-02916b271/" target="_blank">
                                            <img class="footer-social-icon" src="IndexAssets/images/linkedin.png" width="30" height="30" alt="Get Involved with Zurb Foundation" /></a>
                                    </li>
                                    <p></p>
                                    <li>
                                        <a href="https://www.youtube.com/@universal.abroadjobs23" target="_blank">
                                            <img class="footer-social-icon" src="IndexAssets/images/youtube.png" width="30" height="30" alt="Get Involved with Zurb Foundation" /></a>
                                    </li>
                                </ul>
                            </div>

                        </div>
                        <div class="col-6 text-right float-right p-2">
                            <label class="text-white h6" style="text-decoration: none;" target="_blank"><span>Copyright © 2023</span> UniversalEducation. All Rights Reserved.</label>
                        </div>
                    </div>
                </div>
            </footer>

        </div>

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
                                        <asp:CheckBox ID="RememberCHK" runat="server" />
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
                                <asp:Button ID="UESigninLB" runat="server" Text="Sign In" CssClass="btn btn--pill btn--signin" OnClick="UESigninLB_Click" />
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
                            <center>
                                <img src="IndexAssets/images/UE-LOGO-2.png" style="width: 12rem;" /></center>
                            <div class="clearfix">
                                <br />
                            </div>
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
                                    <br />
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

    </form>
    <!-- Scripts -->
    <!-- Bootstrap core JavaScript -->

    <script src="IndexAssets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://assets.codepen.io/7773162/vanilla-tilt.min.js"></script>
    <%--<script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>--%>

    <!--Remember Me-->
    <script>
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
                localStorage.username = UserName.value;
                localStorage.checkbox = rmCheck.value;
            } else {
                localStorage.username = "";
                localStorage.checkbox = "";
            }
        }
    </script>


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
    <script src="IndexAssets/js/isotope.min.js"></script>
    <script src="IndexAssets/js/owl-carousel.js"></script>
    <script src="IndexAssets/js/counter.js"></script>
    <script src="IndexAssets/js/custom.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="IndexAssets/bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <script src="MobileAppJS/main.js"></script>
</body>
</html>
