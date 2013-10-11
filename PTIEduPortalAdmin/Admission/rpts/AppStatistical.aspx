<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master" AutoEventWireup="true" CodeFile="AppStatistical.aspx.cs" Inherits="AppStatistical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 700px" align="center">
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    E-APPLICATION FOR UTME REPORTS</td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Display Applicants By:</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbProgramme" runat="server" 
                        onselectedindexchanged="CmbProgramme_SelectedIndexChanged" 
                        style="font-family: Tahoma; font-size: small" Height="22px" Width="461px">
                         
                         <asp:ListItem>Faculty of Agriculture and Life Sciences</asp:ListItem>
                        <asp:ListItem>Faculty of Pure and Applied Sciences</asp:ListItem>
                        <asp:ListItem>Faculty of Humanities Management and Social Sciences</asp:ListItem>
                        <asp:ListItem>All Applicants</asp:ListItem>
                     <%--
                       <asp:ListItem>Faculties</asp:ListItem>
                         <asp:ListItem>1st Choice Applicants</asp:ListItem>
                        <asp:ListItem>Departments</asp:ListItem>
                        <asp:ListItem>Programmes</asp:ListItem>
                        <asp:ListItem>2nd Choice Applicants</asp:ListItem>
                        <asp:ListItem>Neither 1st Nor 2nd Choice</asp:ListItem>
                        <asp:ListItem>1st And 2nd Choice Applicants</asp:ListItem>--%>
                       
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbMode" runat="server" 
                        onselectedindexchanged="CmbMode_SelectedIndexChanged" Visible="False">
                        <asp:ListItem>Full-Time</asp:ListItem>
                        <asp:ListItem>Part-Time</asp:ListItem>
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
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

