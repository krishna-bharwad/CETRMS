<%@ Page Title="" Language="C#" MasterPageFile="~/UEStaff.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CETRMS.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
<style>
 
   @media screen and (max-device-width: 480px)
and (orientation: portrait){
        label {
            /*float: right;*/
            margin-top: -1rem;
            margin-right: -1rem;
            display: inline-block;
            margin-bottom: 0.2rem;
            font-size: 10px;
            position: absolute;
            bottom: 0;
            left: 0;
            top:4rem;
        }
        }



   .iconAnim-row{
       justify-content: space-evenly;
       align-items: stretch;
   }



        @media only screen 
and (min-device-width : 768px) 
and (max-device-width : 1024px) 
{
            label {
           /* float: right;*/
            margin-top: -2rem;
            margin-right: 3rem;
            display: inline-block;
            margin-bottom: 0.2rem;
            font-size: 14px;
            position: absolute;
            bottom: 0;
            left: 2.4rem;
        }
   
        }

        .DashboardBody
        {
            font-family: 'Rasa', serif;
            font-weight:400;
        }
        * {
            margin:0;
            padding:0;
            font-family: 'Rasa', serif;
            font-weight:400;
        }
        h2 {
            font-size: 0.8em;
        }

        #ValueLBL {
            font-size: 0.8em;
        }

        .mt-2 {
            font-size: 1.2em;
            font-weight:600;
        }  
        .caption{
            display:block;
            position:relative;
            
            font-size:small;
            left:auto;

        }
        .lead{
            line-height:1.0rem;
        }
        label{
            /*float:right;*/
            margin-top:-2rem;
            margin-right:5rem;
        }
        .nav-tabs .nav-item.show .nav-link1, .nav-tabs .nav-link.active {
            color: #495057;
            /* background-color: #fff; */
            /* border-color: #dee2e6 #dee2e6 #fff; */
        }


        .iconAnim-row{
            overflow: hidden;
        }

        .icon-anim{
   animation: fadeIn 0.8s ease-in;
   animation-iteration-count: 1;

}
 .zoom {
  transition: transform .3s;
 
}

.zoom:hover {
  -ms-transform: scale(1.05); /* IE 9 */
  -webkit-transform: scale(1.05); /* Safari 3-8 */
  transform: scale(1.05); 
  
}

@keyframes fadeIn{
    0%{
        opacity: 0;
        top: -55px;

    }

    100%{
        opacity: 1;
        top: 0px;                                     
    }

}



.dashboardAnim-text{
    /*border: 1px solid black;*/
    position: relative;
    animation: slide 1.8s ease-out;
    animation-iteration-count: 1;
}
@keyframes slide{
    0%{
        opacity: 0;
        right: -75px;
    }
    100%{
        opacity: 1;
        right: 0px;
    }
}




.dashboardAnim-text2{
    /*border: 1px solid black;*/
    position:relative;
    color: #1c94a4;
    font-size: 40px;
    font-weight: 800 ; 
}




.iconLabel{
    position: relative;
    top: 2.2em;
    left: 0;
}




 @media screen and (max-width: 480px){
    .container-fluid{
        overflow: hidden;
        width: 100%;
        box-shadow: 0 0 5px 3px #c1c0c0;
        margin-bottom: 0.8em;
    }

    .board-inner{
        border: 1px solid red;
        
    }

    #myTab li{
        display: flex;
        justify-content: space-evenly;
        align-items: center;
    }

    .round-tabs{
        border: white !important;
    }

    .iconLabel{
        /*border: 1px solid black;*/
        font-weight: bold;
        font-size: 11px;
        top:-1.5em;
        margin-left: 6px;
    }

 }

  @media only screen and (min-width:1285px){

     .iconAnim-row img{
         /*margin-left: 1.78rem;*/
     }
     .textAnim{
         
         padding-top: 0.7rem;
     }
     
 }
















    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageSubTitle" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageContent" runat="server">
    <div class="DashboardBody">
    <div class="row">
    <div class="col-md-12 position-sticky">   

 <%--<section>--%>


     <%-- ICON-NAVBAR STARTS --%>

     <div class="container-fluid">
     <div class="row">
         <div class="board">
             <div class="board-inner bg-white">
                 
                         <ul class="nav nav-tabs" id="myTab" role="tablist" >

                             <li>
                                 <a class="nav-link1 iconLink text-success" id="Candidate-tab" data-toggle="tab"  href="#Candidate" role="tab" title="Candidate" aria-controls="Candidate" aria-selected="true">
                                     <span class="round-tabs one">
                                         <img class="iconImg" src="LandingPageAsset/img/1.png" />
                                         <%--                                <span class="caption">Candidate</span>--%>
                                       
                                     </span><br />

                                     <label class="text-dark iconLabel">Candidate</label>
                                 </a>                                
                             </li>

                             <li>
                                 <a class="nav-link1 iconLink" id="Employer-tab" data-toggle="tab"  href="#Employer" role="tab" title="Employer" aria-controls="Employer" aria-selected="false" >
                                     <span class="round-tabs two">
                                         <img class="iconImg" src="LandingPageAsset/img/2.png" />
                                        
                                     </span><br />
                                      <label class="text-dark iconLabel">Employer</label>
                                 </a>
                                 
                             </li>

                             <li>
                                 <a class="nav-link1 iconLink" id="Vacancies-tab" data-toggle="tab"  href="#Vacancies" role="tab" title="Vacancies" aria-controls="Vacancies" aria-selected="false">
                                     <span class="round-tabs three">
                                         <img class="iconImg" src="LandingPageAsset/img/3.png" />
                                         
                                     </span><br />
                                     <label class="text-dark iconLabel">Vacancies</label>
                                 </a>
                                 
                             </li>

                             <li>
                                 <a class="nav-link1 iconLink" id="Payments-tab" data-toggle="tab"  href="#Payments" role="tab" title="Payments" aria-controls="Payments" aria-selected="false">
                                     <span class="round-tabs four">
                                         <img class="iconImg" src="LandingPageAsset/img/4.png" />
                                         
                                     </span><br />
                                     <label class="text-dark iconLabel">Payments</label>
                                 </a>
                                 
                             </li>

                             <li>
                                 <a class="nav-link1 iconLink" id="Interview-tab" data-toggle="tab"  href="#Interview" role="tab" title="Interview" aria-controls="Interview" aria-selected="false">
                                     <span class="round-tabs five">
                                         <img class="iconImg" src="LandingPageAsset/img/05.png" width="40" />
                                         
                                     </span><br />
                                     <label class="text-dark iconLabel">Interview</label>
                                 </a>
                                 
                             </li>

                         </ul>
                   
             </div>

            

         </div>
     </div>
    </div>
<%--</section>--%>

        <%-- ICON-NAVBAR ENDS --%>













      </div>
<!-- <a href="https://www.youtube.com/channel/UC7hSS_eujjZOEQrjsda76gA/videos" target="_blank" id="ytd-url">My YouTube Channel</a> -->
        
    </div>
  </div>
        
  <div class="row">
      
        <div class="col-md-12">
                
            <div class="tab-content" id="myTabContent">
                <div class="tab-pane fade show active " id="Candidate" role="tabpanel" aria-labelledby="Candidate-tab">


                    <div class="row mb-2">
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-md-4">
                                        <img src="LandingPageAsset/img/1.2.png"/>                                        
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Candidate Completed Details</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="CandidateCompletedDetailsLBL" runat="server"></asp:Label></h3>

                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="CandidateCompletedDetailsValueLBL" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL1" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                      <asp:LinkButton ID="CandidateCompletedDetailsLB" runat="server" CssClass="stretched-link" OnClick="CandidateCompletedDetailsLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/1.3.png"/>
                                            
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Candidate Under Selection Process</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="CandidateUnderSelectionProcessLBL" runat="server"></asp:Label></h3>

                                            
                                           <%-- <p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL2" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuaterLBL2" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                         <asp:LinkButton ID="CandidateUnderSelectionProcessLB" runat="server" CssClass="stretched-link" OnClick="CandidateUnderSelectionProcessLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class= "icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/1.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Candidate Final Selected</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="CandidateFinalSelectedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL3" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL3" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="CandidateFinalSelectedLB" runat="server" CssClass="stretched-link" OnClick="CandidateFinalSelectedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    <%--</div>--%>



                   <%-- <div class="row mb-2">--%>
                        <div class="col-lg-4 col-md-6 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/1.5.png"/>
                                            
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Candidate Rejected</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="CandidateRejectedLBL" runat="server"></asp:Label></h3>
                                           
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL4" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL4" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="CandidateRejectedLB" runat="server" CssClass="stretched-link" OnClick="CandidateRejectedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div> 
                        
                    </div>   
                </div>



                <div class="tab-pane fade" id="Employer"  role="tabpanel" aria-labelledby="Employer-tab" >
                    <div class="row mb-2">
                         <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/2.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8 ">
                                            <div class="mt-2 dashboardAnim-text lead">Employers On-board</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="EmployerOnBoardLBL" runat="server"></asp:Label></h3>
                                        </div>
                                        <asp:LinkButton ID="EmployerOnBoardLB" runat="server" CssClass="stretched-link" OnClick="EmployerOnBoardLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>                       
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/2.2.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Active Employers With Open Vacancy</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="ActiveEmployersWithOpenVacancyLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL7" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL7" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="ActiveEmployersWithOpenVacancyLB" runat="server" CssClass="stretched-link" OnClick="ActiveEmployersWithOpenVacancy_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/2.3.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Inactive Employers With Filled Vacacny</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InactiveEmployerWithFilledVacacnyLBL" runat="server"></asp:Label></h3>
                                           
                                          <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL8" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL8" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InactiveEmployersWithFilledVacacnyLB" runat="server" CssClass="stretched-link" OnClick="InactiveEmployersWithFilledVacacnyLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row mb-2">

                       
                         
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/2.5.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">InProcess Vacancies</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="EmployerInProcessVacancies" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL7" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL7" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="LinkButton8" runat="server" CssClass="stretched-link" OnClick="EmployersInprocessVacancies_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/2.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Employers With No Vacancy</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="EmployersWithNoVacancyLBL" runat="server"></asp:Label></h3>
                                        </div>
                                        <asp:LinkButton ID="EmployersWithNoVacancyLB" runat="server" CssClass="stretched-link" OnClick="EmployersWithNoVacancyLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                                                
                    </div>
                </div>
            
                <div class="tab-pane fade" id="Vacancies"  role="tabpanel" aria-labelledby="Vacancies-tab" >
                    <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/3.1.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Vacancies</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalVacanciesLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL11" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL11" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="TotalVacanciesLB" runat="server" CssClass="stretched-link" OnClick="TotalVacanciesLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/3.2.png"/>
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Open Vacancies</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="OpenVacanciesLBL" runat="server"></asp:Label></h3>
                                           
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL12" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL12" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="OpenVacanciesLB" runat="server" CssClass="stretched-link" OnClick="OpenVacanciesLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="https://img.icons8.com/fluency/80/null/timetable.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Vacancies Under Scheduled Interview</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="VacanciesUnderScheduledInterviewLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL13" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL13" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="VacanciesUnderScheduledInterviewLB" runat="server" CssClass="stretched-link" OnClick="VacanciesUnderScheduledInterviewLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-2">

                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/3.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Close Vacancies After Final Selection</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="CloseVacanciesAfterFinalSelectionLBL" runat="server"></asp:Label></h3>
                                           
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL14" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL14" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="CloseVacanciesAfterFinalSelectionLB" runat="server" CssClass="stretched-link" OnClick="CloseVacanciesAfterFinalSelectionLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        

                    </div>
                </div>
               
                <div class="tab-pane fade" id="Payments"  role="tabpanel" aria-labelledby="Payments-tab">
                    <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="https://img.icons8.com/fluency/80/null/request-money.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Payment Amount Requested</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalPaymentRequestedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL16" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL16" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                         <asp:LinkButton ID="TotalPaymentRequestedLB" runat="server" CssClass="stretched-link" OnClick="TotalPaymentRequestedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.2.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Payment Recieved</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalPaymentReceivedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL17" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL17" runat="server">Since last quarter</asp:Label>
                                                </span>
                                            </p>--%>
                                        </div>
                                         <asp:LinkButton ID="TotalPaymentReceivedLB" runat="server" CssClass="stretched-link" OnClick="TotalPaymentReceivedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.3.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">

                                            <div class="mt-2 dashboardAnim-text lead">Total Outstanding Payment Within Time Limit</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalOutstandingPaymentWithinTimeLimitLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL18" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL18" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="OutstandingPaymentWithinTimeLB" runat="server" CssClass="stretched-link" OnClick="OutstandingPaymentWithinTimeLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Outstanding Payment Out Of Time Limit</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalOutstandingPaymentOutOfTimeLimitLBL" runat="server"></asp:Label></h3>
                                       
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL19" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL19" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="OutstandingPaymentOutofTimeLB" runat="server" CssClass="stretched-link" OnClick="OutstandingPaymentOutofTimeLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="https://img.icons8.com/fluency/80/null/external-link.png"/>                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Payment Notificaiton Generated</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalPaymentNotificationGeneratedLBL" runat="server"></asp:Label></h3>
                                           
                                           <%-- <p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL20" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL20" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="TotalPaymentNotificaitonGeneratedLB" runat="server" CssClass="stretched-link" OnClick="TotalPaymentNotificaitonGeneratedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.6.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Payment Transactions Done</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalPaymentTransactionDoneLBL" runat="server"></asp:Label></h3>
                                           
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL21" runat="server">27.4%</asp:Label><br/>
                                               <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL21" runat="server">Since last quarter</asp:Label>
                                                </span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="TotalPaymentTransactionDoneLB" runat="server" CssClass="stretched-link" OnClick="TotalPaymentTransactionDoneLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.7.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Payment Transactions Over Due</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalPaymentTransactionOverDueLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL22" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="Quarter22" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="TotalPaymentTransactionOverDueLB" runat="server" CssClass="stretched-link" OnClick="TotalPaymentTransactionOverDueLB_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/4.2.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Total Referral Payment </div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="TotalTransferReferralDetailsLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL22" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="Quarter22" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="TotalTransferReferralDetailsLB" runat="server" CssClass="stretched-link" OnClick="TotalTransferReferralDetailsLB_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>

                </div>

                <div class="tab-pane fade" id="Interview"  role="tabpanel" aria-labelledby="Employer-tab">
                    <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Proposed</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewProposedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL6" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL6" runat="server">Since last quarter</asp:Label></span>-
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewProposedLB" runat="server" CssClass="stretched-link" OnClick="InterviewProposedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.2.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Scheduled</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewScheduledLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL7" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL7" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewScheduledLB" runat="server" CssClass="stretched-link" OnClick="InterviewScheduledLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.3.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Started</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewStartedLBL" runat="server"></asp:Label></h3>
                                           
                                          <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL8" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL8" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewStartedLB" runat="server" CssClass="stretched-link" OnClick="InterviewStartedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                      <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.4.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Dropped</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewDroppedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL6" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL6" runat="server">Since last quarter</asp:Label></span>-
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewDroppedLB" runat="server" CssClass="stretched-link" OnClick="InterviewDroppedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.5.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Cancelled</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewCancelledLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL7" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL7" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewCancelledLB" runat="server" CssClass="stretched-link" OnClick="InterviewCancelledLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.6.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Rejected</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewRejectedLBL" runat="server"></asp:Label></h3>
                                           
                                          <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL8" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL8" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewRejectedLB" runat="server" CssClass="stretched-link" OnClick="InterviewRejectedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                     <div class="row mb-2">
                        <div class="col-sm-4 mb-4">
                            <div class="card zoom shadow p-1 h-100 bg-body rounded">
                                <div class="card-body">
                                    <div class="row iconAnim-row g-2">
                                        <div class="icon-anim col-sm-4">
                                            <img src="LandingPageAsset/img/5.7.png"/>
                                        
                                        </div>
                                        <div class="textAnim col-sm-8">
                                            <div class="mt-2 dashboardAnim-text lead">Interview Completed</div>
                                            <h3 class="card-title dashboardAnim-text2" >
                                                <asp:Label ID="InterviewCompletedLBL" runat="server"></asp:Label></h3>
                                            
                                            <%--<p class="text-success">
                                                <i class="fa fa-angle-up"></i>
                                                <asp:Label ID="ValueLBL9" runat="server">27.4%</asp:Label><br/>
                                                <span style="font-size: 0.9em" class="text-muted">
                                                    <asp:Label ID="QuarterLBL9" runat="server">Since last quarter</asp:Label></span>
                                            </p>--%>
                                        </div>
                                        <asp:LinkButton ID="InterviewCompletedLB" runat="server" CssClass="stretched-link" OnClick="InterviewCompletedLB_Click"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>                                                
                    </div>
                </div>
            </div>
         </div>
      </div>
</div>    


</asp:Content>
