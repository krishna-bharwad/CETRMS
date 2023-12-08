<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="InterviewDetails.aspx.cs" Inherits="CETRMS.GetInterviewDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>
    <style>
        .InterviewDetailsPage
        {
            font-family: 'Poppins', sans-serif;
            font-size:medium;
        }
        .InterviewDetailsPageCard
        {
            vertical-align:text-top;
        }
        .InterviewDetailsPageCard .a
        {
            font-size:x-large;
            color:#66acac;
        }
        ul.interview-overview {
            border-bottom: 1px solid #e8e8e8;
            padding-bottom: 10px;
            margin-bottom: 10px
        }

        ul.interview-overview li {
            display: inline-block;
            text-align: center;
            padding: 0 15px;
            width: 30%;
            font-size: 14px;
            border-right: 1px solid #e8e8e8
        }

        ul.interview-overview li:last-child {
            border-right: 0
        }

        ul.interview-overview li .name {
            font-size: 12px
        }

        ul.interview-overview li .value {
            font-size: 14px;
            font-weight: bold;
            display: block;
            color: #66acac;
        }

        ul.stats-overview li:first-child {
            padding-left: 0
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="InterviewDetailsPage">
    <%--<div class="x_title">--%>
        <div class="row">
            <div class="col-sm-4">
            <h2><b>Interview Details:</b></h2>
            </div>
            <div class="col-sm-2">
            </div>
            <div class="col-sm-4">
                <h2><b>Interview Status: </b></h2>
                
            </div>
            <div class="col-sm-2">
                <asp:Label ID="InterviewStatusLBL" runat="server"> </asp:Label>
            </div>
        </div>
    <%--</div>--%>
    <div class="clearfix"></div>
    <hr />
    <div class="row">
        <%--<div class="col-4">
            <div class="card h-100 InterviewDetailsPageCard">
                <div class="card-body-u">
                    <div class="col-sm-12">
                        <h6 align="right"><i><strong><asp:Label ID="EmployerStatusLBL" runat="server"></asp:Label></strong></i></h6>
                        <h2><asp:HyperLink ID="EmployerNameHL" runat="server"></asp:HyperLink></h2>
                        <div class="left col-md-7 col-sm-7">
                            <ul class="list-unstyled">
                                <li><i class="fa fa-phone"></i> <asp:Label ID="EmployerContactNoLBL" runat="server"></asp:Label></li>
                            </ul>
                            <p><strong>Email: </strong><asp:Label ID="EmployerEmailLBL" runat="server"></asp:Label></p>
                        </div>
                        <div class="right col-md-5 col-sm-5 text-left">
                            <asp:Image ID="BusinessLogo_IMG" runat="server" Width="80" Height="80" CssClass="profile-user-img img-fluid img-circle" />
                        </div>
                    </div>
                </div>
                <div class="profile-bottom">
                    <div class="float-left p-2 col-xs-12">
                        <i class="fa fa-map-marker"></i> <asp:Label ID="EmployerLocationLBL" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 py-2">
            <div class="card h-100 border-info">
                <div class="card-body-u">
                    <div class="col-sm-12">
                        <h6 class="brief mb-4" align="right"><i><strong><asp:Label ID="EmployerStatusLBL" runat="server"></asp:Label></strong></i></h6>
                        <h2><asp:HyperLink ID="EmployerNameHL" runat="server"></asp:HyperLink></h2>
                        <div class="col-xs-12 col-sm-6 col-md-8">
                            
                            <ul class="list-unstyled">
                                <li><i class="fa fa-phone"></i> <asp:Label ID="EmployerContactNoLBL" runat="server"></asp:Label></li>
                            </ul>
                            <p></p>
                        </div>
                        <div class="col-xs-6 col-md-4 mb-4">
                             <asp:Image ID="BusinessLogo_IMG" runat="server" Width="80" Height="80" CssClass="profile-user-img img-fluid img-circle" />
                        </div>
                    </div>
                    <div class="profile-bottom text-center">
                        <div class="float-left p-2 col-xs-6">
                            <i class="fa fa-envelope" aria-hidden="true"></i><strong>&nbsp;<asp:Label ID="EmployerEmailLBL" runat="server"></asp:Label></strong> 
                        </div>  
                        <div class="float-left p-2 col-xs-6">
                            <i class="fa fa-map-marker" aria-hidden="true"></i><strong>&nbsp;<asp:Label ID="EmployerLocationLBL" runat="server"></asp:Label></strong> 
                        </div>                      
                    </div>
                </div>
            </div>
        </div>



        <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 py-2">
            <div class="card h-100 border-info">
                <div class="card-body-u">
                    <div class="col-sm-12">
                        <h6 class="brief mb-4" align="right"><i><strong><asp:Label ID="VacancyStatusLBL" runat="server"></asp:Label></strong></i></h6>
                        <h2><asp:HyperLink ID="VacanyNameHL" runat="server"></asp:HyperLink></h2>
                        <div class="col-xs-12 col-sm-12 col-md-12">
                            
                            
                            <p><strong>Vacancy Detail: </strong><asp:Label ID="VacancyDetailsLBL" runat="server"></asp:Label></p>
                        </div>
<%--                        <div class="col-xs-6 col-md-4 mb-4">
                             <asp:Image ID="Image1" runat="server" Width="80" Height="80" CssClass="profile-user-img img-fluid img-circle" />
                        </div>--%>
                    </div>
                    <div class="profile-bottom text-center">
                        <div class="float-left p-2 col-xs-6">
                            <i class="fa fa-map-marker" aria-hidden="true"></i><strong>&nbsp;<asp:Label ID="VacancyLocationLBL" runat="server"></asp:Label></strong> 
                        </div>                      
                    </div>
                </div>
            </div>
        </div>
       
        
        <div class="col-xs-12 col-sm-6 col-md-4 col-lg-4 py-2">
            <div class="card h-100 border-info">
                <div class="card-body-u">
                    <div class="col-sm-12">
                        <h6 class="brief mb-4" align="right"><i><strong><asp:Label ID="CandidateStatusLBL" runat="server"></asp:Label></strong></i></h6>
                        <h2><asp:HyperLink ID="CandidateNameHL" runat="server"></asp:HyperLink></h2>
                        <div class="col-xs-12 col-sm-6 col-md-8">
                            <p><i class="fa fa-phone"></i>: <asp:Label ID="CandidatePhoneLBL" runat="server"></asp:Label></p>                            
                        </div>
                        <div class="col-xs-6 col-md-4 mb-4">
                             <asp:Image ID="Candidate_IMG" runat="server" Width="80" Height="80" CssClass="profile-user-img img-fluid img-circle" />
                        </div>
                    </div>
                    <div class="profile-bottom text-center">
<%--                        <div class="float-left p-2 col-xs-6">
                            <p><strong>Application Status: </strong><asp:Label ID="JobApplicationStatusLBL" runat="server"> </asp:Label></p>
                        </div>--%>
                        <div class="float-left p-2 col-xs-6">
                            <i class="fa fa-envelope" aria-hidden="true"></i>&nbsp;<asp:Label ID="CandidateEmailLBL" runat="server"></asp:Label></p>
                        </div>
                        <div class="float-left p-2 col-xs-6">
                            <i class="fa fa-map-marker" aria-hidden="true"></i><strong>&nbsp;<asp:Label ID="CandidateLocationLBL" runat="server"></asp:Label></strong> 
                        </div>                      
                    </div>
                </div>
            </div>
        </div>              
        </div>
        <div class="clearfix"></div>
        <br />
        <div class="row">
        <div class="col-md-12 mb-4">
            <div class="card card-primary card-outline">
                <div class="card-body box-profile">
                    <div class="row justify-content-Left accordion">
                        <div class="col-5">
                        <h4><strong>Proposed Interview ID: </strong>
                            <asp:Label ID="InterviewIDLBL" runat="server"> </asp:Label>

                        </h4>
                        </div>
                    </div>

                    <div class="interview-overview">
                        <div class="row">
                            <div class="col-sm mb-2">
                                <span class="name">Duration In Minutes: </span>
                                <span class="value ">
                                    <asp:Label ID="DurationInMinutesLBL" CssClass="pt-5" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="col-sm mb-2">
                                <span class="name">Preferred Start Time: </span>
                                <span class="value ">
                                    <asp:Label ID="PreferredDateTimeLBL" CssClass="pt-5" runat="server"></asp:Label>
                                </span>
                            </div>
                            <div class="col-sm mb-2">
                                <span class="name">Chosen TimeZone: </span>
                                <span class="value ">
                                    <asp:Label ID="ChosenTimeZoneLBL" CssClass="pt-5" runat="server"></asp:Label>
                                </span>
                            </div>
                        </div>
                    </div>
                       
                    <asp:Panel ID="ScheduleMeetingPanel" runat="server">
                    <div class="row">
                        <div class="col-sm mb-2">
                            <asp:TextBox ID="MeetingStartTimeTXT" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>
                        <div class="col-sm mb-2">
                            <asp:LinkButton ID="ScheduleInterviewLB" CssClass="btn btn-md btn-success rounded-pill" OnClick="ScheduleInterview_btn" runat="server">Schedule Interview</asp:LinkButton>
                            <asp:LinkButton ID="RescheduleInterviewLB" CssClass="btn btn-md btn-success rounded-pill" OnClick="RescheduleInterview_btn" runat="server">ReSchedule Interview</asp:LinkButton>
                        </div>
                        <div class="col-sm mb-2">
                            <asp:LinkButton ID="CancelInterview" CssClass="btn btn-md btn-success rounded-pill" OnClick="CancelInterview_btn" runat="server" Enabled="false">Cancel Interview</asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    </asp:Panel>
                    <hr />
                    <div class="row">
                        <div class="col-5">
                            <h4><strong>Zoom VC -  </strong> <asp:Label ID="MeetingIDLBL" runat="server"></asp:Label> </h4>
                               
                        </div>
                        <div class="col-7 text-right">
                            <h4><strong>Zoom VC Status - </strong>  <asp:Label runat="server" ID="VcStatus"></asp:Label></h4>
                        </div>
                    </div>
                    <asp:Panel ID="MeetingProposedStatusPanel" runat="server" Visible="false">
                    <div class="interview-overview">
                        <div class="row">
                            <div class="col-sm mb-2">
                                  <ul class="stats-overview">
                                    <li>
                                      <span class="name"> Scheduled Start Time </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="ZVSchduledStartTimeLBL"></asp:Label> </span>
                                    </li>
                                    <li class="hidden-phone">
                                      <span class="name"> TimeZone </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="ZVTimeZoneLBL"></asp:Label> </span>
                                    </li>
                                  </ul>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="MeetingCompletionStatusPanel" runat="server" Visible="false">
                    <div class="interview-overview">
                        <div class="row">
                            <div class="col-sm mb-2">
                                  <ul class="stats-overview">
                                    <li>
                                      <span class="name"> Started Time </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="ZVStartTime"></asp:Label> </span>
                                    </li>
                                    <li class="hidden-phone">
                                      <span class="name"> End Time </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="EndTime"></asp:Label> </span>
                                    </li>
                                  </ul>
                            </div>
                        </div>
                    </div>
                    <div class="interview-overview">
                        <div class="row">
                            <div class="col-sm mb-2">
                                  <ul class="stats-overview">
                                    <li>
                                      <span class="name"> No Of Participants </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="ParticipantCount"></asp:Label> </span>
                                    </li>
                                    <li class="hidden-phone">
                                      <span class="name"> Total Minute </span>
                                      <span class="value text-success"> <asp:Label runat="server" ID="TotalMinute"></asp:Label> </span>
                                    </li>
                                  </ul>
                            </div>
                        </div>
                    </div>
                    </asp:Panel>

                    <div class="row">

                        <div class="col-5  accordion pt-3">
                            <h4 class="text-nowrap"><strong>Zoom VC Details</strong></h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Topic</h4>
                                            <blockquote class="message">
                                                <asp:Label runat="server" ID="Topic"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Employer URL</h4>
                                            <blockquote class="message">
                                                <asp:HyperLink ID="StartURLHL" runat="server" Target="_blank"></asp:HyperLink>
                                                <%--<asp:Label runat="server" ID="StartURL"></asp:Label>--%>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Candidate URL</h4>
                                            <blockquote class="message">
                                                <asp:Label runat="server" ID="HostURL"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                                </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">Encrypted Password</h4>
                                            <blockquote class="message">
                                                <asp:Label runat="server" ID="Password"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                               </ul>
                        </div>
                        <div class="col-6">
                                <ul class="messages">
                                    <li>
                                        <div class="message_wrapper">
                                            <h4 class="heading">App Password</h4>
                                            <blockquote class="message">
                                                <asp:Label runat="server" ID="AppPassword"></asp:Label>
                                            </blockquote>
                                        </div>
                                    </li>
                               </ul>
                        </div>
                    </div>
                    <asp:Panel ID="NotificationPanel" runat="server">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <p class="text-muted text-left ml-5 mt-4">
                                    <asp:CheckBox ID ="Mail_CheckBox" runat="server"></asp:CheckBox>
                                    <asp:Label runat="server" CssClass="pt-2" ID="Mail">Mail</asp:Label>
                                    <%--<asp:CheckBox runat="server" CssClass="pl-2" ID="WhatsApp"></asp:CheckBox>
                                    <asp:Label runat="server" CssClass="pt-2">WhatsApp</asp:Label>--%>
                                    <span class="pl-2">
                                        <asp:LinkButton ID="SendNotification" CssClass="btn btn-sm btn-success rounded-pill" runat="server"  OnClick="SendEmailToIndivisuals">Send Notification</asp:LinkButton>
                                    </span>
                                </p><br />
                                <asp:Label runat="server" ID="MailCandidate_Lbl"></asp:Label>
                                <asp:Label runat="server" ID="MailEmp_lbl"></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                    </div>

                </div>
                <!-- /.card-body -->
            </div>
        </div>
    </div>
</asp:Content>
