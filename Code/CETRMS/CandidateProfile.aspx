<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="CandidateProfile.aspx.cs" Inherits="CETRMS.CandidateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet' />




    <%-- FONTAWESOMW SCRIPT --%>
    <script src="https://kit.fontawesome.com/98b7f3ee2e.js" crossorigin="anonymous"></script>

    <%-- bootstrap link --%>
    <link rel="stylesheet" src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" />

   



    <style>
        body {
            min-height: 100vh;
        }

        .CandidateDetailsPage {
            font-family: 'Poppins', sans-serif;
        }

        p {
            color: #28a745 !important;
        }

        h3 {
            font-size: 16px;
        }

        h5 {
            font-size: 16px;
        }

        .GvHeader {
            background-color: #66acac;
            color: white;
            font-size: medium;
            box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
            border-radius: 10px;
        }

        .GvGrid:hover {
            background-color: #66acac;
            color: white;
            box-shadow: rgba(0, 0, 0, 0.45) 0px 25px 20px -20px;
        }


        .tabNavPill {
            display: flex;
            flex-wrap: nowrap;
            align-items: center;
        }

        @media screen and (max-width: 450px) {
            .tabNavPill {
                flex-wrap: wrap;
            }
        }



        /*CARD CSS STARTS*/



        .resumeBtn {
            /*border: 1px solid red;*/
            background-color: white;
            color: white;
            border-radius: 5px;
            box-shadow: 0px 0px 5px 3px #fff !important;
            margin-top: 3px;
        }

        .resumeDownload {
            /*border: 1px solid black;*/
            top: 0rem;
            position: relative !important;
            text-align: center;
            margin-top: 0.1rem;
            /*background-color: #e4e1e1;*/
        }

        article:hover .resumeBtn {
            color: black !important;
            /*background-color: #66acac;*/
        }

        .imgIcon {
            height: 100%;
            width: 45%;
        }

        .iconButton, .iconButtonPassport {
            /*border: 1px solid grey;*/
            height: 100%;
            width: 55%;
            margin-top: 1rem;
        }

            .iconButtonPassport .downloadBtns {
                /*border: 1px solid grey;*/
                height: 100%;
                width: 60%;
                transform: scale(var(--img-scale));
            }

            .iconButton .downloadBtns {
                height: 100%;
                width: 40%;
                transform: scale(var(--img-scale));
            }

        .downloadBtns:hover /*, :focus)*/ {
            --img-scale: 1.1;
            --title-color: #28666e;
            --link-icon-translate: 0;
            --link-icon-opacity: 1;
            box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;
        }

        /* .cardImg{
          height: 30%;
          width: 100%;

      }*/

        .card-title {
            font-size: 22px;
            font-weight: bold;
        }

        .other-title {
            font-size: 16px;
        }

        .titleSection {
            text-align: center;
        }

        .card-text {
            font-size: 15px;
        }

        .textSection {
            /*border: 1px solid black;*/
            padding: 0px 2px 9px 2px;
        }

        .candidateHeadingSection {
            /*border: 1px solid red;*/
            padding-bottom: 2px;
            text-align: center;
        }

        .candidateHeading {
            font-size: 15px;
            font-weight: 600;
            font-style: italic;
        }

        .detailsContainer {
            border: 1px solid #e9e7e7;
            border-radius: 13px;
            background-color: #f5f5f5;
        }

        .detailsContainer2 {
            border: 1px solid #e9e7e7;
            align-items: center;
            margin: 0.5rem 0 -2.5rem 0;
            border-radius: 13px;
            background-color: #f0faf8;
        }

        .profile-user-img {
            height: 100%;
            width: 100% !important;
            /*object-fit: cover;*/
            box-shadow: 0 0 8px 8px rgba(0, 0, 0, 0.16);
        }

        .registereDetails {
            padding: 0.5rem;
            display: flex;
            flex-wrap: wrap;
            color: #66acac !important;
        }


        .detailsContainer li {
            /*border: 1px solid red;*/
            list-style: none !important;
        }

        .detailsContainer ul {
            padding: 6px 0 6px 12px;
        }

        .sampleCard {
            width: 100%;
            padding: 0 !important;
            border: 1px solid #e4e1e1;
        }
        /*CARD CSS ENDS*/

        .nav-pills .nav-link.active, .nav-pills .show > .nav-link {
            background-color: transparent !important;
            color: #599696;
            border-bottom: 3px solid #66acac;
            /*font-size: 18px;*/
            font-weight: bold;
        }

        .nav-item a {
            color: black;
        }

        .detailsHeading .accent-blue {
            /* display: flex;
            justify-content: center;
            align-items: center;
           margin: 1rem;*/
            font-size: 19px !important;
            font-weight: bold !important;
        }

        .detailsHeading {
            padding: 13px;
            margin: 1rem 0 0.5rem 0;
            display: flex;
            justify-content: center;
            align-items: center;
            /* border: 1px solid blue;*/
            font-size: 17px !important;
            font-weight: bold !important;
        }

            .detailsHeading h6 {
                font-size: 14px;
            }

        .sub-heading {
            font-weight: 600 !important;
            font-size: 17px !important;
        }

        .sub-heading1 {
            font-weight: 600 !important;
            font-size: 16px !important;
        }

        .mainHeading h4 {
            font-size: 18px;
            font-weight: 600;
        }

        .details {
            color: black;
            font-style: italic;
        }

        ul.stats-overview li .name {
            font-size: 14px;
            font-weigh: bold;
            border: none !important;
        }

        .visaContainer {
            margin-top: 5rem;
        }






        /*****************************************/


        article {
            width: 100%;
            box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px !important;
            --img-scale: 1.001;
            --title-color: black;
            --link-icon-translate: -20px;
            --link-icon-opacity: 0;
            position: relative;
            border-radius: 16px;
            box-shadow: none;
            background: #fff;
            transform-origin: center;
            transition: all 0.4s ease-in-out;
            overflow: hidden;
            margin-bottom: 1rem;
        }

            /* article a::after {
                position: absolute;
                inset-block: 0;
                inset-inline: 0;
                cursor: pointer;
                content: "";
            }*/

            /* basic article elements styling */
            article h2 {
                margin: 0 0 18px 0;
                font-family: Calibri;
                font-size: 15px;
                letter-spacing: 0.06em;
                color: var(--title-color);
                transition: color 0.3s ease-out;
            }

        figure {
            margin: 0;
            padding: 0;
            aspect-ratio: 1 / 1;
            /*overflow: hidden;*/
        }

        article img {
            max-width: 100%;
            transform-origin: center;
            transform: scale(var(--img-scale));
            transition: transform 0.4s ease-in-out;
        }

        .article-body {
            padding: 24px;
        }

        article a {
            display: inline-flex;
            align-items: center;
            text-decoration: none;
            color: #28666e;
        }

            article a:focus {
                outline: 1px dotted #28666e;
            }

            article a .icon {
                min-width: 24px;
                width: 24px;
                height: 24px;
                margin-left: 5px;
                transform: translateX(var(--link-icon-translate));
                opacity: var(--link-icon-opacity);
                transition: all 0.3s;
            }

        /* using the has() relational pseudo selector to update our custom properties */
        article:has(:hover, :focus) {
            --img-scale: 1.1;
            --title-color: #28666e;
            --link-icon-translate: 0;
            --link-icon-opacity: 1;
            box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;
        }




        *,
        *::before,
        *::after {
            box-sizing: border-box;
        }

        /*body {
  margin: 0;
  padding: 48px 0;
  font-family: "Figtree", sans-serif;
  font-size: 1.2rem;
  line-height: 1.6rem;
  min-height: 100vh;
}*/

        .articles {
            /*display: grid;*/
            max-width: 100%;
            margin-inline: auto;
            padding-inline: 24px;
            grid-template-columns: repeat(auto-fill, minmax(320px, 1fr));
            gap: 24px;
        }

        .sr-only:not(:focus):not(:active) {
            clip: rect(0 0 0 0);
            clip-path: inset(50%);
            height: 1px;
            overflow: hidden;
            position: absolute;
            white-space: nowrap;
            width: 1px;
        }


        .detailsOfVisa {
            font-weight: 600;
            font-size: 15px;
            margin-top: 0.5rem;
            height: auto;
        }

        #visaDetails {
            text-align: center;
        }




        @container card (min-width: 380px) {
            .article-wrapper {
                display: grid;
                grid-template-columns: 100px 1fr;
                gap: 16px;
            }

            .article-body {
                padding-left: 0;
            }

            figure {
                width: 100%;
                height: 100%;
                overflow: hidden;
            }

                figure img {
                    height: 100%;
                    aspect-ratio: 1;
                    object-fit: cover;
                }
        }

        @media screen and (max-width: 960px) {
            /*article {
                container: card/inline-size;
              
            }*/

            .article-body p {
                display: none;
            }
        }

        @media screen and (min-width: 728px) and (max-width: 960px) {
            article {
                margin-left: -2rem;
                width: 149%;
            }
        }

        @media screen and (min-width: 965px) and (max-width: 1090px) {
            article {
                margin-left: -1rem;
                width: 138%;
            }
        }

        .selectbox {
            visibility: hidden;
        }





        /*tabs style starts*/


        .selfIntro {
            width: 95%;
            max-height: 100%;
            /*border: 1px solid black;*/
            overflow-y: scroll;
        }

        .selfDetails, .selfIntro {
            height: auto;
        }

        .leftColumn {
            /*border: 1px solid black;*/
        }

        .dataRow {
            line-height: 15px;
            height: auto;
            text-align: center;
            padding: 1rem;
        }

        .introRow {
            height: 7rem;
            text-align: justify;
            padding: 0.5rem;
            border-radius: 10px;
        }

        .infoImgIcon {
            margin: -1rem;
            height: 40px;
            width: 50px;
        }


        @media screen and (max-width: 450px) {
            .selfIntro, .selfDetails {
                overflow-y: scroll;
            }

            .imgIcon {
                height: 100%;
                width: 38%;
            }
        }

        @media screen and (min-width: 500px) and (max-width: 690px) {
            .imgIcon {
                height: 100%;
                width: 30%;
            }
        }

        @media screen and (min-width: 1290px) {
            .imgIcon {
                height: 100%;
                width: 30%;
            }
        }


        .detailsMainCard{
            /*border: 1px solid red;*/
            height: 100vh;
        }



    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="server">
    <div class="CandidateDetailsPage">
        <div class="row">
            <div class="col-11 mainHeading">
                <h4>Candidate Profile</h4>
            </div>
            <div class="col-1">
                <asp:ImageButton ID="CandidateProfilePrintBTN" runat="server" ImageUrl="https://img.icons8.com/external-filled-line-andi-nur-abdillah/64/null/external-Print-graphic-design-(filled-line)-filled-line-andi-nur-abdillah.png" Height="35px" Width="35px" AlternateText="Print" OnClientClick="SetTarget();" OnClick="CandidateProfilePrintBTN_Click" />
            </div>
        </div>

        <hr />
        <!-- Main content -->
        <section class="content">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">

                        <div class="articles">
                            <%-- <div class="container articleContainer">--%>
                            <article>
                                <div class="article-wrapper">

                                    <figure>
                                        <%--<img src="https://picsum.photos/id/1011/800/450" alt="" />--%>
                                        <asp:Image ID="CandidiatePhotoImg" AlternateText="User profile picture" runat="server" CssClass="profile-user-img card-img-top" />
                                    </figure>



                                </div>



                                <div class="article-body">

                                    <div class="titleSection">
                                        <asp:Label CssClass="card-title" ID="CandidateFullNameLBL" runat="server"></asp:Label><br />
                                    </div>

                                    <div class="textSection">

                                        <asp:Label CssClass="card-text" ID="CandidateProfileLBL" runat="server"></asp:Label><br />
                                    </div>

                                    <div class="candidateHeadingSection">
                                        <a class="center">
                                            <asp:Label ID="CandidateStatusLBL" CssClass=" candidateHeading" runat="server"></asp:Label></a>
                                    </div>

                                    <div class="container detailsContainer">
                                        <div class="row">
                                            <ul>
                                                <li>
                                                    <div class="block">
                                                        <div class="block_content">
                                                            <h2 class="title">
                                                                <b>Registered On:</b>
                                                            </h2>
                                                            <div class="byline" style="margin-top: -9px;">
                                                                <%-- <span>value comes here</span>--%>
                                                                <asp:Label ID="AppliedOnLBL" CssClass="registereDetails" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="row">
                                            <ul>
                                                <li>
                                                    <div class="block">
                                                        <div class="block_content">
                                                            <h2 class="title">
                                                                <b>Updated On:</b>
                                                            </h2>
                                                            <div class="byline" style="margin-top: -9px;">
                                                                <%--<span>value comes here</span>--%>
                                                                <asp:Label ID="ProfileUpdatedOnLBL" CssClass="registereDetails" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>


                                    <div class="row resumeDownload ">
                                        <asp:LinkButton ID="CandidateResumeDownloadLB" runat="server" CssClass="btn btn-sm resumeBtn dropdown-item shadow" OnClick="CandidateResumeDownloadLB_Click1">
                                           <span style="font-size:17px; color:#fff; font-weight:bold;"><%--<i class="fa fa-long-arrow-down"></i> Resume Download--%><img class="imgIcon" src="images/Resume5.png" alt="icon"/></span>
                                        </asp:LinkButton>
                                    </div>


                                </div>
                                <%--</div>--%>
                            </article>
                        </div>
                    </div>





                    <%-- *************************************** --%>



                    <!-- /.card-body -->



                    <!-- /.col -->
                    <div class="col-md-9">
                        <div class="card detailsMainCard">
                            <div class="card-header p-2">
                                <ul class="nav nav-pills tabNavPill justify-content-center">
                                    <li class="nav-item"><a class="nav-link active" href="#PersonalProfile" data-toggle="tab">Personal Profile</a></li>
                                    <li class="nav-item"><a class="nav-link" href="#Passport" data-toggle="tab">Passport & Visa Details</a></li>
                                    <li class="nav-item"><a class="nav-link" href="#Qualification" data-toggle="tab">Qualification & Experience Details</a></li>
                                    <li class="nav-item"><a class="nav-link" href="#OtherDetails" data-toggle="tab">History</a></li>
                                </ul>

                            </div>


                            <%-- mobile number --%>





                            <div class="card-body">
                                <div class="tab-content">

                                    <%-- PERSONAL PROFILE STARTS --%>

                                    <div class="active tab-pane" id="PersonalProfile">
                                        <div class="post">
                                            <div class="row introRow justify-content-center">
                                                <%-- <ul class="messages">--%>
                                                <%--<li>--%>
                                                <div class="selfIntro">
                                                    <h4 class="sub-heading">Self Introduction:</h4>
                                                    <blockquote class="message byline">
                                                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label ID="ProfileBriefLBL" CssClass="details selfDetails" runat="server"></asp:Label>
                                                    </blockquote>
                                                </div>
                                                <%-- </li>
                                            </ul>--%>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="post">
                                            <div class="row justify-content-">

                                                <%-- LEFT COLUMN STARTS --%>
                                                <div class="col-md-6 leftColumn">

                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-solid fa-location-dot infoIcon" style="color: #000000;"></i>&nbsp Current Address: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="CurrentAddressLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>

                                                    <%--<ul class="messages">
                                                        <li>--%>
                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-solid fa-phone infoIcon" style="color: #000000;"></i>&nbsp Mobile: &nbsp&nbsp </h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="MobileLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>
                                                    <%--  </li>
                                                    </ul>--%>

                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-sharp fa-solid fa-at infoIcon" style="color: #000000;"></i>&nbsp Email: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="EmailLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>

                                                    <%-- <ul class="messages">
                                                        <li>--%>
                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-solid fa-cake-candles infoIcon" style="color: #000000;"></i>&nbsp Date of Birth (age): &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="DateOfBirthLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>
                                                    <%-- </li>
                                                    </ul>--%>
                                                </div>
                                                <%-- LEFT COLUMN ENDS --%>


                                                <%-- RIGHT COLUMN STARTS --%>

                                                <div class="col-md-6">

                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-solid fa-house-user infoIcon" style="color: #000000;"></i>&nbsp Permanent Address: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="PermanentAddressLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>

                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-sharp fa-solid fa-check-to-slot infoIcon" style="color: #000000;"></i>&nbsp Candidate Cast: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="CandidateCastLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>

                                                    <%--<ul class="messages">
                                                        <li>--%>
                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><i class="fa-solid fa-person-circle-question infoIcon" style="color: #000000;"></i>&nbsp Marital Status: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="MaritalStatusLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>
                                                    <%--  </li>
                                                    </ul>--%>

                                                    <div class="dataRow row">
                                                        <h4 class="sub-heading"><%--<i class="fa-sharp fa-regular fa-location-pen" style="color: #000000;"></i>--%><img src="images/Location-Globe.svg" class="infoImgIcon" alt="icon" />&nbsp Nationality: &nbsp&nbsp</h4>
                                                        <blockquote class="message byline">
                                                            <asp:Label ID="NationalityLBL" CssClass="details" runat="server"></asp:Label>
                                                        </blockquote>
                                                    </div>


                                                </div>

                                                <%-- RIGHT COLUMN ENDS --%>
                                            </div>
                                            <%-- DETAILS ROW ENDS --%>
                                        </div>
                                    </div>


                                    <%-- PERSONAL PROFILE ENDS --%>


                                    <%-- PASSPORT DETAILS STARTS --%>

                                    <div class="tab-pane" id="Passport">
                                        <div class="post">
                                            <div class="user-block">
                                                <div class="row">
                                                    <div class="col-sm-10 detailsHeading">
                                                        <h5><b>
                                                            <label class="accent-blue">Passport Details</label></b></h5>
                                                    </div>
                                                    <div class="col-sm-2 iconButtonPassport">
                                                        <asp:ImageButton ID="PassportDownloadIB" runat="server" ImageUrl="~/images/PassportFile-NEW (1).png" CssClass="downloadBtns" OnClick="PassportDownloadLB_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="sub-heading1">Name</h4>
                                                                <blockquote class="message byline">
                                                                    <asp:Label ID="GivenNameLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-4">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="sub-heading1">Legal Guardian Name</h4>
                                                                <blockquote class="message byline">
                                                                    <asp:Label ID="LegalGuardianNameLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-4">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="sub-heading1">Surname</h4>
                                                                <blockquote class="message byline">
                                                                    <asp:Label ID="SurnameLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <%-- </div>

                                            <div class="row">--%>
                                                <div class="col-sm-6">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="sub-heading1">Mother Name</h4>
                                                                <blockquote class="message byline">
                                                                    <asp:Label ID="MotherNameLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-sm-6">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="sub-heading1">Spouse Name</h4>
                                                                <blockquote class="message byline">
                                                                    <asp:Label ID="SpouseNameLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="row justify-content-center detailsContainer2">
                                                <div class="col-sm-12">
                                                    <ul class="stats-overview">
                                                        <li>
                                                            <b class="name">Passport Issue Date </b>
                                                            <span class="value ">
                                                                <asp:Label ID="PassportIssueDateLBL" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                        <li>
                                                            <b class="name">Passport Expiry Date </b>
                                                            <span class="value ">
                                                                <asp:Label ID="PassportExpiryDateLBL" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <%-- </div>
                                            <div class="row">--%>
                                                <div class="col-sm-12">
                                                    <ul class="stats-overview">
                                                        <li>
                                                            <b class="name">Passport Number </b>
                                                            <span class="value ">
                                                                <asp:Label ID="PassportNumberLBL" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                        <li class="hidden-phone">
                                                            <b class="name">Passport Issue Location </b>
                                                            <span class="value">
                                                                <asp:Label ID="PassportIssueLocationLBL" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <%-- row ends --%>

                                            <p>
                                            </p>
                                        </div>
                                        <div class="post visaContainer">
                                            <div class="user-block">
                                                <div class="row">
                                                    <div class="col-sm-10 detailsHeading">
                                                        <h5>
                                                            <label class="accent-blue">Visa Details</label></h5>

                                                    </div>
                                                    <div class="col-sm-2 iconButton">
                                                        <asp:ImageButton ID="VisaDownloadIB" runat="server" ImageUrl="~/images/VisaFile-NEW.png" CssClass="downloadBtns" OnClick="VisaDownloadLB_Click" />
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row ">
                                                <div class="col-sm-12">
                                                    <ul class="messages">
                                                        <li>
                                                            <div class="message_wrapper">
                                                                <h4 class="detailsOfVisa">Details Of Visa</h4>
                                                                <blockquote id="visaDetails" class="message">
                                                                    <asp:Label ID="DetailsOfVisaLBL" CssClass="details" runat="server"></asp:Label>
                                                                </blockquote>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>





                                            <div class="row justify-content-center detailsContainer2">
                                                <%--<div class="row">--%>
                                                <div class="col-sm-12">
                                                    <ul class="stats-overview">
                                                        <li>
                                                            <b class="name">Visa Type Name </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaTypeNameLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                        <li class="hidden-phone">
                                                            <b class="name">Visa Valid Upto </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaValidUptoLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <%--</div>
                                            <div class="row">--%>
                                                <div class="col-sm-12">
                                                    <ul class="stats-overview">
                                                        <li>
                                                            <b class="name">Visa Country Name </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaCountryNameLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                        <li class="hidden-phone">
                                                            <b class="name">Visa Validity Years </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaValidityYearsLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <%-- </div>
                                            <div class="row">--%>
                                                <div class="col-sm-12">
                                                    <ul class="stats-overview">
                                                        <li>
                                                            <b class="name">Visa Type Details </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaTypeDetailsLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                        <li class="hidden-phone">
                                                            <b class="name">Visa State Code </b>
                                                            <span class="value">
                                                                <asp:Label ID="VisaStateCodeLBL" CssClass="details" runat="server"></asp:Label>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <%--</div>--%>
                                            </div>
                                            <%-- row ends --%>
                                        </div>

                                    </div>

                                    <%-- PASSPORT DETAILS ENDS --%>




                                    <%-- QUALIFICATION STARTS --%>

                                    <div class="tab-pane" id="Qualification">
                                        <div class="post">
                                            <div class="user-block">
                                                <h5>
                                                    <label class="accent-blue detailsHeading" style="font-size: 17px; font-weight: bold; text-align: center;">Qualification Details</label></h5>
                                            </div>
                                            <!-- /.user-block -->
                                            <div class="row mt-4">
                                                <div class="col-sm-6 " style="font-size: 15px;">
                                                    <b>Highest Qualification:</b><%--</div>
                                                <div class="col-sm-3 ">--%>
                                                    <asp:Label ID="HighestQualificationLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="font-size: 15px;">
                                                    <b>University Name:</b><%--</div>
                                                <div class="col-sm-3">--%>
                                                    <asp:Label ID="UniversityNameLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                                </div>
                                            <div class="row mb-5 mt-3">
                                                <div class="col-sm-6" style="font-size: 15px;">
                                                    <b>University Location:</b><%--</div>
                                                <div class="col-sm-3 text-success">--%>
                                                    <asp:Label ID="UniversityLocationLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6" style="font-size: 15px;">
                                                    <b>Qualification Year:</b><%--</div>
                                                <div class="col-sm-3">--%>
                                                    <asp:Label ID="QualificationYearLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="post">
                                            <div class="user-block">
                                                <h5>
                                                    <label class="accent-blue detailsHeading" style="font-size: 17px; font-weight: bold; text-align: center;">Other Details</label></h5>
                                            </div>
                                            <p>
                                                <%-- <asp:LinkButton ID="ResumeFileTypeLB" runat="server"></asp:LinkButton>--%>
                                            </p>
                                            <p>
                                            </p>
                                        </div>
                                        <div class="post">
                                            <div class="user-block">
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-9">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-start">
                                                    <label style="color: black">Total Work Experience :</label>
                                                    <asp:Label ID="TotalExperienceMonthLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 text-start">
                                                    <label style="color: black">Notice Period:</label>
                                                    <asp:Label ID="NoticePeriodLBL" CssClass="details" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <p>
                                            </p>
                                        </div>
                                    </div>

                                    <%-- QUALIFICATION ENDS --%>


                                    <%-- HISTORY STARTS --%>

                                    <div class="tab-pane" id="OtherDetails">
                                        <div class="post">
                                            <h5 class="card-title other-title"><b>Job Application</b></h5>
                                            <div class="x_content">
                                                <asp:UpdatePanel ID="UpdatePanel1" CssClass="details" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="JobApplicationGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" HeaderStyle-CssClass="GvHeader" RowStyle-CssClass="GvGrid" CssClass="table" CellPadding="3" CellSpacing="3" PageSize="5" OnPageIndexChanging="JobApplicationGV_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="JobApplicationID" HeaderText="JobApplication ID" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="VacancyID" HeaderText="Vacancy Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CandidateID" HeaderText="Candidate Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ApplicationStatus" HeaderText="Application Status" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CompanyRemarks" HeaderText="Candidate Status " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="AppliedOn" HeaderText="Applied On " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RemarksOn" HeaderText="Remarks On " />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="post">
                                            <h5 class="card-title other-title"><b>Interview</b></h5>
                                            <div class="x_content">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="CandidateInterviewGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" HeaderStyle-CssClass="GvHeader" RowStyle-CssClass="GvGrid" CssClass="table" CellPadding="3" CellSpacing="3" PageSize="5" OnPageIndexChanging="CandidateInterviewGV_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="JobApplicationID" HeaderText="JobApplication ID" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="InterviewStatus" HeaderText="Interview Status" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CETRemarks" HeaderText="Remarks" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="EmployerRemarks" HeaderText="Employer Remarks" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CandidateRemarks" HeaderText="Candidate Remarks " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ChosenTimeZone" HeaderText="ChosenTimeZone " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="DurationInMinutes" HeaderText="DurationInMinutes " />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="post">
                                            <h5 class="card-title other-title"><b>Rejection</b></h5>
                                            <div class="x_content">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="CandidateRejectionGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" HeaderStyle-CssClass="GvHeader" RowStyle-CssClass="GvGrid" CssClass="table" CellPadding="3" CellSpacing="3" PageSize="5" OnPageIndexChanging="CandidateRejectionGV_PageIndexChanging">

                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="JobApplicationID" HeaderText="JobApplication ID" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="VacancyID" HeaderText="Vacancy Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CandidateID" HeaderText="Candidate Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ApplicationStatus" HeaderText="Application Status" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CompanyRemarks" HeaderText="Company Remarks " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="AppliedOn" HeaderText="Applied On " />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="RemarksOn" HeaderText="Remarks On " />

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- HISTORY STARTS --%>
                                </div>
                                <!-- /.tab-content -->
                            </div>
                            <!-- /.card-body -->

                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /.COL-MD-9 -->
                </div>
                <!-- /.ROW -->
            </div>
            <!-- /.CONTAINER -->
        </section>
    </div>

    <!--CandidateDetails DIV -->

    <%--</div>--%>

    <%-- BOOTSTRAP SCRIPT --%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>

    <%-- <script language="JavaScript">
        function resumedownload() {
            console.log("dropdown button clicked");


            var dotbutton = document.getElementById('dotbutton');
            var selectBox = document.getElementById('selectBox');

            selectBox.style.visibility = "visible";

        }
    </script>--%>
</asp:Content>
