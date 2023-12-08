<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintJobVacancyDetails.aspx.cs" Inherits="CETRMS.PrintJobVacancyDetails" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            
             <rsweb:ReportViewer ID="JobVacancyDetailsRV" runat="server" Width="100%">
                 
             </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
