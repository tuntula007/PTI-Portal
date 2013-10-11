<%@ Page Language="C#" MasterPageFile="~/Medical/MedicalMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportShow.aspx.cs" Inherits="ReportShow" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Tab 1">
                        <HeaderTemplate>
                            Detailed Reports</HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
                                <CR:CrystalReportViewer ID="CrvAdmissionReq" runat="server" AutoDataBind="True" DisplayGroupTree="False"
                                    EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" Style="text-align: left" />
                                <asp:Label ID="LabelError" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                            
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
