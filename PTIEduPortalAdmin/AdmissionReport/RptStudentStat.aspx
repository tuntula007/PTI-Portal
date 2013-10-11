<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master" AutoEventWireup="true" CodeFile="RptStudentStat.aspx.cs" Inherits="RptStudentStat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 700px" align="center">
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                   STUDENTS DISTRIBUTION/STATISTICS REPORTS</td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Scope:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbRscope" runat="server">
                        <asp:ListItem Value="1">Paid Students</asp:ListItem>
                        <asp:ListItem Value="2">Unpaid Students</asp:ListItem>
                        <asp:ListItem Value="3">All Students</asp:ListItem>
                        <asp:ListItem Value="4">Admitted and Accepted</asp:ListItem>
                        <asp:ListItem Value="5">Admitted But Not Accepted</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Type</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbRtype" runat="server">
                        <asp:ListItem Value="1">Statistics by Programme Category</asp:ListItem>
                        <asp:ListItem Value="2">Statistics by Faculty</asp:ListItem>
                        <asp:ListItem Value="3">Statistics by Department</asp:ListItem>
                        <asp:ListItem Value="4">Statistics by Programme Of Study</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Academic Session:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbSession" runat="server" DataSourceID="ObjSessions" 
                        DataTextField="PresentSession" DataValueField="PresentSession">
                    </asp:DropDownList>
                </td>
            </tr>
                        <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Format:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbReportOption" runat="server" Font-Size="15px">
                    <asp:ListItem Value ="PDF" Selected=True>PDF Format </asp:ListItem>
                    <asp:ListItem Value ="Excel">Excel Format</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:Button ID="BtnShow" runat="server" onclick="BtnShow_Click" 
                        Text="Show Report" Width="101px" />
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:ObjectDataSource ID="ObjSessions" runat="server" 
                        SelectMethod="GetSessions" TypeName="StudentReportBusiness">
                    </asp:ObjectDataSource>
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

