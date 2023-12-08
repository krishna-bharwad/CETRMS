<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivacyPolicy.aspx.cs" Inherits="CETRMS.PrivacyPolicy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    

    <title>Universal Education : Privacy Policy</title>


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
      
         
    </style>
</head>
<body ondragstart="return false;" ondrop="return false;">
    <form id="form1" runat="server">
        <div>

           <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
               <header class="header-area background-header header-sticky">
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
            </div>
        </div>
                <!-- ***** Header Area End ***** -->
            <div class="clearfix"><br/><br/><br/><br/></div>

            <div class="section">
                <div class="container">
                    <h2>PERSONAL IDENTIFICATION INFORMATION:</h2>
                    <p>We may collect personal identification information from Users in a variety of ways, including, but not limited to, when Users visit our site, register on the site, post requirement, subscribe to the newsletter, respond to a survey, fill out a form, and in connection with other activities, services, features or resources we make available on our Site. Users may be asked for, as appropriate, name, email address, qualification certificate copy, passport copy, mailing address, phone number, credit and debit card information. Users may, however, visit our Site anonymously. We will collect personal identification information from Users only if they voluntarily submit such information to us. Users can always refuse to supply personally identification information, except that it may prevent them from engaging in certain Site related activities.</p>
                
                    <h2>NON-PERSONAL IDENTIFICATION INFORMATION:</h2>
                    <p>We may collect non-personal identification information about Users whenever they interact with our Site. Non-personal identification information may include the browser name, the type of computer and technical information about Users means of connection to our Site, such as the operating system and the Internet service providers utilized including IP address and other similar information.</p>  
                  
                    <h2>WEB BROWSER COOKIES:</h2>
                    <p>Our Site may use "cookies" to enhance User experience. User's web browser places cookies on their hard drive for record-keeping purposes and sometimes to track information about them. User may choose to set their web browser to refuse cookies, or to alert you when cookies are being sent. If they do so, note that some parts of the Site may not function properly.</p>
                    
                    <h2>HOW WE USE COLLECTED INFORMATION?</h2>
                    <p>Universal-abroadjobs.com collects and uses Users personal information for the following purposes:<br/>
                       •To personalize user experience: We may use information in the aggregate to understand how our Users as a group use the services and resources provided on our Site<br/>
                       •To improve our Site: We continually strive to improve our website offerings based on the information and feedback we receive from you.<br/>
                       •To improve customer service: Your information helps us to more effectively respond to your customer service requests and support needs.<br/>
                       •To process transactions: We may use the information Users provide about themselves when placing an order only to provide service to that order. We do not share this information with outside parties except to the extent necessary to provide the service by our registered and premium members.<br/>
                       •To administer content, promotion, survey or other Site feature: To send Users information they agreed to receive about topics we think will be of interest to them.<br/>
                       •To send periodic emails: The email address Users provide for registering with Universal-abroadjobs.com, will only be used to send them information and updates pertaining to their order. It may also be used to respond to their inquiries, and/or other requests or questions. If User decides to opt-in to our mailing list, they will receive emails that may include company news, updates, offers, related product or service information, etc.<br/> 
                        If at any time the User would like to unsubscribe from receiving future emails, they can submit unsubscribe request. We've included unsubscribe option at the bottom of each email.                    
                    </p>
                    
                    <h2>HOW WE PROTECT YOUR INFORMATION?</h2>
                    <p>
                        We adopt appropriate data collection, storage and processing practices and security measures to protect against unauthorized access, alteration, disclosure or destruction of your personal information, username, password, transaction information and data stored on our Site. We have physical, electronic, and procedural safeguards that comply with the laws prevalent in India to protect personal information about you. We seek to ensure compliance with the requirements of the Information Technology Act, 2000 as amended and rules made thereunder to ensure the protection and preservation of your privacy.
                        UNLESS YOU REQUEST THAT WE DELETE CERTAIN INFORMATION, WE RETAIN THE INFORMATION WE COLLECT FOR AT LEAST 5 YEARS AND MAY RETAIN THE INFORMATION FOR AS LONG AS NEEDED FOR OUR BUSINESS AND LEGAL PURPOSES.
                    </p>
                    <h2>SHARING YOUR PERSONAL INFORMATION</h2>
                    <p>
                        We do not sell, trade, or rent personal identification information about you with other persons (save with your consent) or non-affiliated companies except to provide services you have requested, when we have your permission or under the following circumstances.<br/>
                        •We provide the information to trusted partners who work on behalf of or with us under confidentiality agreements. These companies may use your personal information to help us communicate with you about our offers and our marketing partners. However, these companies do not have any independent right to share this information.<br/>
                        •We transfer information about you if we are acquired by or merged with another company. In this event, we will notify you before information about you is transferred and becomes subject to a different privacy policy.<br/>

                        We may share generic aggregated demographic information not linked to any personal identification information regarding visitors and users with our business partners, trusted affiliates and advertisers for the purposes outlined above.<br/>
                        We share sensitive personal information to any third party without obtaining the prior consent of the User<br/>
                        •When it is requested or required by law or by any court or governmental agency or authority to disclose, for the purpose of verification of identity, or for the prevention, detection, investigation including cyber incidents, or for prosecution and punishment of offences.<br/>
                        •When we respond to subpoenas, court orders, or legal process, or to establish or exercise our legal rights or defend against legal claims.<br/>
                        •When we believe it is necessary to share information in order to investigate, prevent, or take action regarding illegal activities, suspected fraud, situations involving potential threats to the physical safety of any person, violations of our terms of use, or as otherwise required by law.<br/>
                        These disclosures are made in good faith and belief that such disclosure is reasonably necessary for enforcing the Privacy Policy and for complying with the applicable laws and regulations.
                    </p>
                    
                    <h2>ADVERTISING</h2>
                    <p>
                        Advertisements appearing on our site may be delivered to Users by advertising partners, who may set cookies. These cookies allow the ad server to recognize your computer each time they send you an online advertisement to compile non-personal identification information about you or others who use your computer. This information allows ad networks to, among other things, deliver targeted advertisements that they believe will be of most interest to you. This privacy policy does not cover the use of cookies by any advertisers.
                    </p>
                    
                    <h2>GOOGLE ADSENSE</h2>
                    <p>Some of the ads may be served by Google. Google's use of the DART cookie enables it to serve ads to Users based on their visit to our Site and other sites on the Internet. DART uses "non personally identifiable information" and does NOT track personal information about you, such as your name, email address, physical address, etc. You may opt out of the use of the DART cookie by visiting the Google ad and content network privacy policy at www.google.com/privacy_ads.html</p>
                    
                    <h2>CHANGES TO THIS PRIVACY POLICY</h2>
                    <p>Universal-abroadjobs.com has the discretion to update this privacy policy at any time. We encourage Users to frequently check this page for any changes to stay informed about how we are helping to protect the personal information we collect. You acknowledge and agree that it is your responsibility to review this privacy policy periodically and become aware of modifications.</p>
                    
                    <h2>YOUR ACCEPTANCE OF THESE TERMS</h2>
                    <p>By using this Site, you signify your acceptance of this policy. If you do not agree to this policy, please do not use our Site. Your continued use of the Site following the posting of changes to this policy will be deemed your acceptance of those changes.</p>
                
                    <h2>CHANGING YOUR INFORMATION OR CLOSING YOUR ACCOUNT</h2>
                    <p>Upon request Universal-abroadjobs.com will provide you with information about whether we hold any of your personal information. You are responsible for maintaining the accuracy of the information you submit to us, such as your contact information. You may access, correct, or request deletion of your personal information by making updates to that information or by contacting us through your online account. If you request to access all personal information you’ve submitted, we will respond to your request to access within 30 days. If you completely delete all such information, then your account may become deactivated. If your account is deactivated or you ask to close your account, you will no longer be able to use the Service. If you would like us to delete your account in our system, you can do so through the your account (once you logged in, visit settings/ user settings, and then click on the close my account link).</p>
                     
                    <p>We will use commercially reasonable efforts to honor your request; however, certain information will actively persist on the Service even if you close your account, including information in your Work Diaries and messages you posted to the Service. In addition, your personal information may remain in our archives and information you update or delete, or information within a closed account, may persist internally or for our administrative purposes. It is not always possible to completely remove or delete information from our databases. In addition, we typically will not remove information you posted publicly through or on the Service. Bear in mind that neither you nor we can delete all copies of information that has been previously shared with others on the Service. CHOICE/OPT OUTS</p>
                    
                    <p>We give you the choice regarding the collection and usage of your personally identifiable information. During registration for “joining our mailing list,” we request for contact information in order to send bulletins and for advertising purposes. You may therefore choose to opt out of providing such information. We may send you marketing messages by SMS, Whatsapp, Email, RCS or by any other medium.</p>
                  
                    <p>Further, once you are registered with us, you will have the option at any stage to inform us that you no longer wish to receive future e-mails and you may “unsubscribe” by contacting on unsubscribe link. Further, as per Rule 5(7) of the Information Technology (Reasonable security practices and procedures and sensitive personal data or information) Rules, 2011 of Information Technology Act, 2000 amended through Information Technology Amendment Act, 2008, you have an option to withdraw your consent for use of your sensitive personal data given earlier to us. Such withdrawal of consent shall be sent in writing to our registered address.</p>
                    
                    <h2>REASONABLE SECURITY PRACTICES AS PER INFORMATION TECHNOLOGY ACT, 2000 AND ITS RULES</h2>
                    <p>We have implemented reasonable security practices as per Rule 8 of the Information Technology (Reasonable security practices and procedures and sensitive personal data or information) Rules, 2011 of Information Technology Act, 2000 amended through Information Technology Amendment Act, 2008. Access to your personal account online is password protected. We will not release your account password to any person. In the event that you forget your password, you may generate an on-line request for your password to be sent to you by e-mail at the e-mail address used during registration.</p>
                    <p>We have implemented stringent, internationally acceptable standards of technology, managerial security, technical security, operational security and physical security in order to protect personally identifiable information from loss, misuse, disclosure, alteration or destruction. The data resides behind a firewall, with access restricted to our authorized personnel. We have implemented “Reasonable Security Practices” as required by the Information Technology Act, 2000 rules including any amendment in the said Act and its rules. By complying with such provisions, we assure you proper care and control over our Information Technology and Security operations under sections 43, 43A, 45, 66,72A & 85 of I.T. Act, 2000 and I.T.A.A, 2008 including related rules and therefore you agree that we shall not be held responsible for any activity in your account which results from your failure to keep your own password/mobile secured.</p>
                    <p>By using this service you agree that we shall not be held responsible for any uncontrollable security attacks and in such cases you agree that we shall not be held responsible for any type of financial losses, loss of opportunity, legal cost, business losses, reputation loss, direct and indirect losses that may occur to you as per the Provisions of Section 43, 43A and 45 of Information Technology Act, 2000 including any amendments in the said Act and any other laws of India for the time being in force. You further agree that our management shall not be held responsible directly or indirectly for any cybercrime related criminal liabilities under I.T. Act, 2000 relating to your information as you have agreed and acknowledged that our management complies with due diligence (care & controls) requirements of I.T. Act, 2000 including its rules and amendments.</p>
                    <p>AWS provides complete list of information security controls of server on which our service is hosted. Further, you also agree and acknowledge that our management shall never be held responsible regarding privacy of your sensitive personal data in case of sharing your sensitive personal data to any authorized cyber investigation agency of appropriate government authorities under sections 67C, 69, 69A, 69B, 70B, 79 and 80 of I.T.Act,2000 including its amendments and rules.</p>
                
                    <h2>GRIEVANCE REDRESSAL</h2>
                    <p>If you have any questions or grievances regarding the privacy statement, practices of the site, or any other transaction issue, please contact our grievance officer on [insert details]. We have appointed our grievance officer as per Rule 5 (9) of the Information Technology of Information Technology Act, 2000 amended through Information Technology Amendment Act, 2008. Any grievance or complaint, in relation to processing of information, should be sent to us in writing with a thorough description, to following contact email ID. Grievance shall be redressed as expeditiously as possible and you can contact him on below details</p>
                    <p>
                        The details of the Grievance Officer are as follows:

                        Uttam patel
                        Email: universal.abroadjobs23@gmail.com
                        Address - 4th floor, shagun complex, navrangpura, Ahmedabad, Gujarat-380001, india.
                        We request you to please provide the following information in your complaint:<br/>
                        •Identification of the information provided by you<br/>
                        •Clear statement as to whether the information is personal information or sensitive personal information<br/>
                        •Your address, telephone number or e-mail address.<br/>
                        •A statement that you have a good-faith belief that the information has been processed incorrectly or disclosed without authorization, as the case may be.<br/>
                        •A statement, under penalty of perjury, that the information in the notice is accurate, and that the information being complained about belongs to you.<br/>
                    </p>
                    <h2>CONTACT US</h2>
                    <p>
                        If you have any questions about this Privacy Policy, the practices of this site, or your dealings with this site, please contact us at: universal.abroadjobs23@gmail.com
                    </p>
                    <h2>SECURITY</h2>
                    <p>
                        Universal-abroadjobs.com takes industry standard protocols and technology to protect registered user information. Universal-abroadjobs.com also protects registered user information offline. All registered user information is restricted within our offices. Servers used to store personally identifiable information are housed in a secure, supervised environment. In addition, only our employees who need specific information to perform a task are granted access to personally identifiable information. Universal-abroadjobs.com takes commercially reasonable steps to help protect and secure the information it collects and stores about our Users.
                    </p>
                    <p>
                        All access to the Site is encrypted using industry-standard transport layer security technology (TLS). When you enter sensitive information (such as tax identification number), we encrypt the transmission of that information using secure socket layer technology (SSL). We also use HTTP strict transport security to add an additional layer of protection for our Users. But remember that no method of transmission over the Internet, or method of electronic storage, is 100% secure. Thus, while we strive to protect your personal data, we cannot ensure and do not warrant the security of any information you transmit to us. We maintain physical, electronic and procedural safeguards in connection with the collection, storage and disclosure of personal information (including sensitive personal information). Our security procedures mean that we may occasionally request proof of identity before we disclose personal information to you. It is important for you to protect against unauthorised access to your password and to your computer. Be sure to sign off when you finish using a shared computer. Click here for more information on how to sign off.
                    </p>
                </div>
            </div>
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
                                                <img class="footer-social-icon" src="IndexAssets/images/facebook.png" width="30" height="30" alt="ZURB Foundation Facebook"/></a>
                                        </li>
                                        <p></p>
                                        <li>
                                            <a href="https://instagram.com/universal.abroadjob23?igshid=YmMyMTA2M2Y=" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/instagram.png" width="30" height="30" alt="ZURB Foundation Twitter"/></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://twitter.com/abroadjob23?t=paG2CECMX5pBsNTSL8x7ew&amp;s=08" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/twitter.png" width="30" height="30" alt="ZURB Foundation Youtube"/></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://www.linkedin.com/in/universal-abroad-jobs-02916b271/" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/linkedin.png" width="30" height="30" alt="Get Involved with Zurb Foundation"/></a>
                                        </li>
                                         <p></p>
                                        <li>
                                            <a href="https://www.youtube.com/@universal.abroadjobs23" target="_blank">
                                                <img class="footer-social-icon" src="IndexAssets/images/youtube.png" width="30" height="30" alt="Get Involved with Zurb Foundation"/></a>
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
                                            <button class="nav-link active" id="pills-Employer-tab" data-toggle="pill" data-target="#pills-Employer" type="button" role="tab" aria-controls="pills-Employer" aria-selected="true">Employer</button>
                                        </li>
                                        <li class="nav-item" role="presentation">
                                            <button class="nav-link" id="pills-Candidate-tab" data-toggle="pill" data-target="#pills-Candidate" type="button" role="tab" aria-controls="pills-Candidate" aria-selected="false">Candidate</button>
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
                                            <asp:LinkButton ID="CETRMS_EmployerSingUpLB" runat="server" type="submit" OnClick="EmployerPersonalProfileSignup" CssClass="btn btn--pill btn--signin">Employer Signup</asp:LinkButton>
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
                                            <asp:LinkButton ID="CETRMS_CandidateSignUpLB" runat="server" type="submit" OnClick="CandidatePersonalProfileSignup" CssClass="btn btn--pill btn--signin">Candidate Signup</asp:LinkButton>
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
                            <div class="row p-2 bd-highlight">
                                <div class="col-sm">
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <div class="flex--item Google-Signup" data-ga="[&quot;sign up&quot;,&quot;Sign Up Started - Google&quot;,&quot;New Post&quot;,null,null]">
                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="GoogleSignUp">
                                    <svg aria-hidden="true" class="native svg-icon iconGoogle" width="40" height="40"  viewBox="0 0 18 18"><path fill="#4285F4" d="M16.51 8H8.98v3h4.3c-.18 1-.74 1.48-1.6 2.04v2.01h2.6a7.8 7.8 0 0 0 2.38-5.88c0-.57-.05-.66-.15-1.18Z"/><path fill="#34A853" d="M8.98 17c2.16 0 3.97-.72 5.3-1.94l-2.6-2a4.8 4.8 0 0 1-7.18-2.54H1.83v2.07A8 8 0 0 0 8.98 17Z"/><path fill="#FBBC05" d="M4.5 10.52a4.8 4.8 0 0 1 0-3.04V5.41H1.83a8 8 0 0 0 0 7.18l2.67-2.07Z"/><path fill="#EA4335" d="M8.98 4.18c1.17 0 2.23.4 3.06 1.2l2.3-2.3A8 8 0 0 0 1.83 5.4L4.5 7.49a4.77 4.77 0 0 1 4.48-3.3Z"/></svg> 
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm">
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


</body>
</html>
