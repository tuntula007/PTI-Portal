﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptPrintAdmissionLetter.aspx.cs" Inherits="RptPrintAdmissionLetter" Title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admission Letter</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <CR:CrystalReportViewer ID="CrvAdmissionReq" runat="server" AutoDataBind="true" 
            DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" />
    
    </div>
    </form>
</body>
</html>