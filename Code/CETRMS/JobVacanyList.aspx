<%@ Page Title="" Language="C#" MasterPageFile="~/LandingPage.Master" AutoEventWireup="true" CodeBehind="JobVacanyList.aspx.cs" Inherits="CETRMS.JobVacanyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="LandingPageAsset/JobVacancyList.css"/>
    
    
    <div class="container mb-4">
  
    <div class="card shadow" style="width:50rem;">
        <div class="card-body">
            <div class="row flex">
                <div class="col">
                    <asp:Label ID="JobTitleLBL" runat="server" CssClass="text-start text-secondary">Job Title/Role</asp:Label>
                     <br/>
                    <asp:Label ID="JobLocationLBL" runat="server" CssClass="text-start text-secondary">Job Location</asp:Label>
                </div>
                <div class="col">
                  <div class="position-relative">
                      <asp:Button ID="ApplyNowBTN" runat="server" type="button" CssClass="btn btn-primary position-absolute top-0 end-0" Text="Apply Now"/>
                    </div>
                </div>           
            </div>
            <div class="row flex">
                <div class="col">
                    <asp:Label ID="RequiredExperienceLBL" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Person.png" width="20" height="20"></span>&nbsp; Required Experience</asp:Label>
                </div>
                <div class="col">
                    <asp:Label ID="EducationLevelLBL" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Education.png" width="20" height="20"></span>&nbsp; Education Level</asp:Label>
                </div>
                <div class="col">
                    <asp:Label ID="SalaryRangeLBL" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Salary.png" width="20" height="20"></span>&nbsp; Salary Range.</asp:Label>
                </div>
            </div>
           
        </div>
    </div>
      
    <div class="card shadow" style="width:50rem;">
        <div class="card-body">
            <div class="row flex">
                <div class="col">
                    <asp:Label ID="JobTitleLBL1" runat="server" CssClass="text-start text-secondary">Job Title/Role</asp:Label>
                     <br/>
                    <asp:Label ID="JobLocationLBL2" runat="server" CssClass="text-start text-secondary">Job Location</asp:Label>
                </div>
                <div class="col">
                  <div class="position-relative">
                      <asp:Button ID="ApplyNowBTN1" runat="server" type="button" CssClass="btn btn-primary position-absolute top-0 end-0" Text="Apply Now"/>
                    </div>
                </div>           
            </div>
            <div class="row flex">
                <div class="col">
                    <asp:Label ID="RequiredExperienceLBL1" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Person.png" width="20" height="20"></span>&nbsp; Required Experience</asp:Label>
                </div>
                <div class="col">
                    <asp:Label ID="EducationLevelLBL1" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Education.png" width="20" height="20"></span>&nbsp; Education Level</asp:Label>
                </div>
                <div class="col">
                    <asp:Label ID="SalaryRangeLBL1" runat="server" CssClass="fs-5 text-secondary"><span><img src="LandingPageAsset/img/Salary.png" width="20" height="20"></span>&nbsp; Salary Range.</asp:Label>
                </div>
            </div>
           
        </div>
    </div>
      
    </div>
</asp:Content>
