<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageApplicant.master" AutoEventWireup="true"
    CodeFile="ApplicantForgetPassword.aspx.cs" Inherits="ApplicantForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="colorheader">
        Enter your form number or username and Click submit. Your password will be sent
        to your mail box
    </div><br />
    <table>
        <tr align="left">
            <td class="fieldname">
                User Name or Form Number:
            </td>
            <td>
                <asp:TextBox ID="txtMno" runat="server" Width="200px">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="rqdMno" runat="server" Display="Dynamic" ControlToValidate="txtMno"
                    ErrorMessage="Form Number or User Name can't be empty"></asp:RequiredFieldValidator>
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
