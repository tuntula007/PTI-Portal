<%@ Page Language="C#" MasterPageFile="~/LoginMasterPage.master" AutoEventWireup="true" CodeFile="RecoverPassword.aspx.cs" Inherits="ProfileManager_RecoverPassword" Title="Recover password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="left" cellpadding="0" cellspacing="0" class="style1" 
        style="font-family: verdana;font-size: 11px">
        <tr>
            <td class="style6">
                Password Recovery Tool</td>
            <td class="style9">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Enter your email address</td>
            <td class="style9">
                <asp:TextBox ID="TextBox1" runat="server" Width="320px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnReset" OnClick="btnReset_Click"   runat="server" 
                    Height="22px" Text="Recover Password" 
                    Width="114px" />
            </td>
        </tr>
        <tr title=" ">
            <td class="style7">
            </td>
            <td class="style10">
            </td>
            <td class="style8">
                </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

