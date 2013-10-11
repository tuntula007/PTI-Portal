<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" Title="Reset Password" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" class="middle-footer" style="width: 100%">
        <tr>
            <td valign="top">
            <fieldset>
                    <legend></legend>
    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto;">
    <tr>
        <td colspan="2" style="border-bottom-style: solid;border-bottom-width: thin;border-bottom-color: #FF0000" nowrap="nowrap">
            Reset User Password</td>
    </tr>
    <tr>
        <td nowrap="nowrap" colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            Matric Number</td>
        <td nowrap="nowrap">
            <asp:TextBox ID="txtUserid" runat="server" Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMatricNo" runat="server" 
                ControlToValidate="txtUserid" ErrorMessage="Matric Number Is Required">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            &nbsp;</td>
        <td nowrap="nowrap">
            <asp:Button ID="btnApply" runat="server" onclick="btnApply_Click" 
                Text="Reset" BackColor="#666666" ForeColor="#FFFF99" Width="100px" />
        </td>
    </tr>
</table>
</td>
            </fieldset>
            </tr>
            </table>

</asp:Content>

