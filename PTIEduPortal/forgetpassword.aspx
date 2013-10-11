<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="forgetpassword.aspx.cs" Inherits="forgetpassword" Title="Retrieve Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="colorheader">
        <br />
        Enter your Matric Number and click Submit button. Your password will be sent to
        your mail box
    </div>
    <br />
    <br />
    <table width="90%" align="center">
        <tr>
            <td align="right">
                Matric Number:
            </td>
            <td align="left">
                <asp:TextBox ID="txtMno" runat="server">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rqdMno" runat="server" Display="Dynamic" ControlToValidate="txtMno"
                    ErrorMessage="Matric Number can't be empty"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <div>
        <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
    </div>
</asp:Content>
