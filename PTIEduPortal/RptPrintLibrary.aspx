<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true" CodeFile="RptPrintLibrary.aspx.cs" Inherits="RptPrintLibrary" Title="DLCUI | Authorisation Letter" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" style="text-align: left">
    <CR:CrystalReportViewer ID="CrvAdmissionReq" runat="server" AutoDataBind="true" 
            DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" DisplayToolbar="False" />
</asp:Panel>
</asp:Content>

