<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployerSignUp.aspx.cs" Inherits="CETRMS.Sample" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employer Signup</title>

      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="description" content="Bootstrap App Landing Template">
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=5.0">
      <meta name="author" content="Themefisher">
      <meta name="generator" content="Themefisher Small Apps Template v1.0">
  
      <!-- theme meta -->
      <meta name="theme-name" content="small-apps" />
      <link href="https://fonts.googleapis.com/css2?family=Open+Sans&family=Rasa&display=swap" rel="stylesheet"/>
      <!-- Favicon -->
      <link rel="shortcut icon" type="SignUpPageResources/image/x-icon" href="SignUpPageResources/images/favicon.png" />
  
      <!-- PLUGINS CSS STYLE -->
      <link rel="stylesheet" href="SignUpPageResources/plugins/bootstrap/bootstrap.min.css">
      <link rel="stylesheet" href="SignUpPageResources/plugins/themify-icons/themify-icons.css">
      <link rel="stylesheet" href="SignUpPageResources/plugins/slick/slick.css">
      <link rel="stylesheet" href="SignUpPageResources/plugins/slick/slick-theme.css">
      <link rel="stylesheet" href="SignUpPageResources/plugins/fancybox/jquery.fancybox.min.css">
      <link rel="stylesheet" href="SignUpPageResources/plugins/aos/aos.css">


      <!-- CUSTOM CSS -->
      <link href="SignUpPageResources/ProfileCard.css" rel="stylesheet"/>
      <link href="SignUpPageResources/css/Employerstyle.css" rel="stylesheet"/>
      <link href="SignUpPageResources/assets/css/main.css" rel="stylesheet"/>
      <link rel="stylesheet" href="IndexAssets/css/templatemo-scholar.css"/>
       
    <link rel="stylesheet" href="IndexAssets/css/fontawesome.css" /> 
    <link rel="stylesheet" href="IndexAssets/css/templatemo-scholar.css"/>
    <%--<link rel="stylesheet" href="IndexAssets/css/owl.css" />--%>
    <link rel="stylesheet" href="IndexAssets/css/animate.css" />
    <%--<link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css" />--%>
    <link rel="stylesheet" href="IndexAssets/Heroes.css" />
    <link rel="stylesheet" href="IndexAssets/bootstrap-4.0.0-dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="LandingPageAsset/loginModal.css" />
    <link rel="stylesheet" href="LandingPageAsset/Megamenu.css" />
    <link rel="stylesheet" href="IndexAssets/Termscondition.css"/>
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&family=Rasa&display=swap" rel="stylesheet"/>
    <script src="IndexAssets/vendor/jquery/jquery.min.js"></script>
    <style>
        *{
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
        .header-area  {
            background: linear-gradient(to bottom, #66acac 0%, #ffffff 100%);
            border-radius: 0px 0px 25px 25px;
            /*height: 80px !important;*/
            position: fixed!important;
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
        form {
            display: block;
            margin-top: 0em;
            margin-block-end: 0em;
        }
        footer {
           /* margin-top: 163px;*/
            position: relative;
            width: 100%;
            background-color: #66acac;
            vertical-align: middle;
            min-height: 110px;
            border-radius: 80px 80px 0px 0px;
            padding: 10px;
        }
         
    </style>




</head>
<body  class="body-wrapper" data-spy="scroll" data-target=".privacy-nav">
    <form id="form1" runat="server">
        <div>
                
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

                                    <%--<ul class="nav">
                                        <li class="scroll-to-section"><a href="https://www.apple.com/in/app-store/" class="active">Candidate</a></li>
                                        <li class="scroll-to-section">
                                            <a href="https://www.apple.com/in/app-store/" aria-haspopup="true" aria-expanded="false">Employer</a>                                            
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
                                    </ul>--%>

                                    <a class='menu-trigger'>
                                        <span>Menu</span>
                                    </a>
                                    <!-- ***** Menu End ***** -->
                                </nav>
                            </div>

                        </div>
                    </div>
                </header>               
         
            <div class="section">
            <div class="container-fluid">
<%--            <nav class="navbar main-nav navbar-expand-lg px-2 px-sm-0 py-2 py-lg-0">
              <div class="container">
                <a class="navbar-brand" href="index.html"><img src="images/universal.png" alt="logo" height="100px" width="200px"></a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                  aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                  <span class="ti-menu"></span>
                </button>
              </div>
            </nav>--%>
        <!--====================================
        =            Hero Section            =
        =====================================-->
        <section class="section gradient-banner">
            <div id="particles-js"></div>
	        <div class="shapes-container">
		        <div class="shape" data-aos="fade-down-left" data-aos-duration="1500" data-aos-delay="100"></div>
		        <div class="shape" data-aos="fade-down" data-aos-duration="1000" data-aos-delay="100"></div>
		        <div class="shape" data-aos="fade-up-right" data-aos-duration="1000" data-aos-delay="200"></div>
		        <div class="shape" data-aos="fade-up" data-aos-duration="1000" data-aos-delay="200"></div>
		        <div class="shape" data-aos="fade-down-left" data-aos-duration="1000" data-aos-delay="100"></div>
		        <div class="shape" data-aos="fade-down-left" data-aos-duration="1000" data-aos-delay="100"></div>
		        <div class="shape" data-aos="zoom-in" data-aos-duration="1000" data-aos-delay="300"></div>
		        <div class="shape" data-aos="fade-down-right" data-aos-duration="500" data-aos-delay="200"></div>
		        <div class="shape" data-aos="fade-down-right" data-aos-duration="500" data-aos-delay="100"></div>
		        <div class="shape" data-aos="zoom-out" data-aos-duration="2000" data-aos-delay="500"></div>
		        <div class="shape" data-aos="fade-up-right" data-aos-duration="500" data-aos-delay="200"></div>
		        <div class="shape" data-aos="fade-down-left" data-aos-duration="500" data-aos-delay="100"></div>
		        <div class="shape" data-aos="fade-up" data-aos-duration="500" data-aos-delay="0"></div>
		        <div class="shape" data-aos="fade-down" data-aos-duration="500" data-aos-delay="0"></div>
		        <div class="shape" data-aos="fade-up-right" data-aos-duration="500" data-aos-delay="100"></div>
		        <div class="shape" data-aos="fade-down-left" data-aos-duration="500" data-aos-delay="0"></div>
	        </div>
	        <div class="container">
		        <div class="row align-items-center">
			        <div class="col-md-4 text-center order-1 order-md-1">
				        <img class="img-fluid" src="LandingPageAsset/img/UE-Iphone-X.png" width="250" alt="screenshot"/>
			        </div>
                    
                    <div class="col-md-4 order-2 order-md-2 text-center text-md-left">
				        <h1 class="text-white font-weight-bold mb-4">Hire the best employee</h1>
				        <p class="text-white mb-5">Waypoint for selecting from the best applicants around the world.</p>
				        <a href="FAQ.html" class="btn btn-outline-light">Download Now</a>
			        </div>

			        <div class="col-md-4 text-center order-1 order-md-2">
                      <div class="Profilegrid-7 element-animation">
                        <div class="Profilecard Profilecolor-card-2">
                          <asp:Image ID="imgprofile" runat="server" CssClass="profile" />
                          <h1 class="Profiletitle-2">Hi <asp:Label ID="lblname" runat="server"></asp:Label> !!!</h1>
                          <p class="Profilejob-title"> Congratulations</p>
                          <div class="Profiledesc Profiletop">
                            <p>Welcome to the new world of opportunities, now is the time to fulfil your dream of working abroad..</p>
                          </div>
                        </div>
                      </div>
                    </div>
			        </div>
		        </div>
            </section>
	        
        <!--====  End of Hero Section  ====-->
        <section class="section pt-0 position-relative pull-top">
	        <div class="container">
		        <div class="rounded shadow p-5 bg-white">
			        <div class="row">
				        <div class="col-lg-4 col-md-6 mt-5 mt-md-0 text-center">
					        <i class="fa-solid fa-briefcase fa-2xl p-4"></i>
					        <h3 class="mt-4 text-capitalize h5 ">Opportunities</h3>
					        <p class="regular text-muted">Pick from thousands of opportunities that are specifically sorted for your area of expertise.</p>
				        </div>
				        <div class="col-lg-4 col-md-6 mt-5 mt-md-0 text-center">
					        <i class="fa fa-user fa-2xl p-4"></i>
					        <h3 class="mt-4 text-capitalize h5 ">Profile</h3>
					        <p class="regular text-muted">Keep your profile current and present the best version of yourself to potential employers.</p>
				        </div>
				        <div class="col-lg-4 col-md-12 mt-5 mt-lg-0 text-center">
					       <i class="fa fa-calendar fa-2xl p-4"></i>
					        <h3 class="mt-4 text-capitalize h5 ">Schedules</h3>
					        <p class="regular text-muted">View your interview schedules as accurately as possible,get notified with latest updates on your job applications.</p>
				        </div>
			        </div>
		        </div>
	        </div>
        </section>
        </div>
            </div>
        <!--============================
        =            Footer            =
        =============================-->
        <footer>
                    <div class="container py-vh-4 text-white fw-lighter">
                        <div class="row">
                            <div class="col-sm text-center text-lg-start">
                                <a class="navbar-brand pe-md-4 fs-4 col-12 col-md-auto text-center" href="Newindex.aspx">
                                    <img src="LandingPageAsset/img/UE-LOGO-2.png" width="224" height="100" fill="currentColor" class="bi bi-stack img-fluid" viewbox="0 0 16 16"/>
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
                                    <li class="nav-item">
                                      <span class="text-white"> EMAIL ID:-</span> <a href="mailto:Universal.abroadjobs23@gmail.com" class="text-white text-wrap" style="word-break:break-all;">Universal.abroadjobs23@gmail.com</a>
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
             
        </div>
    </form>
<%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
<script defer src="https://use.fontawesome.com/releases/v5.0.8/js/all.js"></script>
<link href="https://fonts.googleapis.com/css?family=Roboto:100,300,400" rel="stylesheet"/>--%>

<script src="SignUpPageResources/assets/js/plugins/particles.js-master/particles.js-master/particles.min.js"></script>

<script src="SignUpPageResources/assets/js/particales-script.js"></script>

<script src="SignUpPageResources/assets/js/main.js"></script>

 <!-- Bootstrap core JavaScript -->
    <script src="IndexAssets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://assets.codepen.io/7773162/vanilla-tilt.min.js"></script>
    <script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="IndexAssets/vendor/jquery/jquery.min.js"></script>
    <script src="IndexAssets/js/isotope.min.js"></script>
    <script src="IndexAssets/js/owl-carousel.js"></script>
    <script src="IndexAssets/js/counter.js"></script>
    <script src="IndexAssets/js/custom.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="IndexAssets/bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <script src="LandingPageAsset/Megamenu.js"></script>
</body>
</html>

