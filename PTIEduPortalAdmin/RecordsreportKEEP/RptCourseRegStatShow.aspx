<%@ Page Title="" Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true" CodeFile="RptCourseRegStatShow.aspx.cs" Inherits="RptCourseRegStatShow" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" style="text-align: left">
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
            Font-Names="Tahoma" Font-Size="Small" NavigateUrl="~/Reporting/RptCourseRegistrationStat.aspx">Return 
        to Report Parameter Page</asp:HyperLink>
    <br />
    <br />
    <CR:CrystalReportViewer ID="CrvCourseRegistrationReq" runat="server" AutoDataBind="true" 
        DisplayGroupTree="False" DisplayToolbar="False" 
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
        style="text-align: left" />
</asp:Panel>
</asp:Content>
