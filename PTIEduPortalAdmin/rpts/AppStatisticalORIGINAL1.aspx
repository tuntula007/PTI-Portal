<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master" AutoEventWireup="true" CodeFile="AppStatisticalORIGINAL.aspx.cs" Inherits="AppStatisticalORIGINAL" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 700px" align="center">
            <tr>
                <td class="style4" style="text-align: left; ">
                    </td>
                <td style="text-align: left" class="style5">
                    </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    MULTIPLE EXAM CARDS PRINT</td>
            </tr>
            <tr>
                <td class="style6" style="text-align: left; ">
                </td>
                <td class="style7" style="text-align: left">
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbProgramme" runat="server" 
                        onselectedindexchanged="CmbProgramme_SelectedIndexChanged" 
                        style="font-family: Tahoma; font-size: small">
                        <asp:ListItem>ND</asp:ListItem>
                        <asp:ListItem>HND Applicants</asp:ListItem>
                        <asp:ListItem>All Applicants</asp:ListItem>
                      
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2" style="text-align: left; ">
                    </td>
                <td style="text-align: left" class="style3">
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
                        Text="Display Cards" Width="101px" />
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

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style2
        {
            width: 161px;
            height: 26px;
        }
        .style3
        {
            height: 26px;
        }
        .style4
        {
            width: 161px;
            height: 84px;
        }
        .style5
        {
            height: 84px;
        }
        .style6
        {
            width: 161px;
            height: 32px;
        }
        .style7
        {
            height: 32px;
        }
    </style>

</asp:Content>


