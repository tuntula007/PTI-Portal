<%@ Page Language="C#" MasterPageFile="~/Medical/MedicalMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Changepassword.aspx.cs" Inherits="Changepassword" Title="Change Password" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" class="middle-footer" style="width: 100%">
        <tr>
            <td valign="top">
            <fieldset>
                    <legend></legend>
    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto;">
    <tr>
        <td colspan="2" style="border-bottom-style: solid;border-bottom-width: thin;border-bottom-color: #FF0000" nowrap="nowrap">
            Change User Password</td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            UserID</td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            Old Password</td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtOldpassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            New Password</td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtNewpassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            Confirm New Password</td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtConfirmnewpassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            &nbsp;</td>
        <td nowrap="nowrap">
            <asp:Button ID="btnApply" runat="server" onclick="btnApply_Click" 
                Text="Apply" BackColor="#666666" ForeColor="#FFFF99" />
        </td>
    </tr>
</table>
</td>
            </fieldset>
            </tr>
            </table>

</asp:Content>

