<%@ Page Language="C#" MasterPageFile="~/LoginMasterPage.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="Createuser.aspx.cs" Inherits="Createuser" Title="Create new Account" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
        <tr>
            <td valign="top">        
                    
                    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto">
                        <%--<table border="0" cellspacing="0" cellpadding="0" class="middle-footer" style="width: 100%">--%>
                        <tr>
                            <td nowrap="nowrap">
                                Create Your Account
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                User ID
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Password
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Confirm Password
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                Email Address
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtEmail" runat="server" Width="190px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>                       
                        
                        <tr>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td nowrap="nowrap">
                                <asp:Button ID="btnCreateaccount" runat="server" Height="25px" OnClick="btnCreateaccount_Click"
                                    Text="Create Account" Width="110px" BackColor="#666666" ForeColor="#FFFF99" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                            </td>
                            <td nowrap="nowrap">
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" colspan="3">
                                <hr style="height: -12px; color: #FF0000; width: 283px;" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap" colspan="2">
                                &nbsp;
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
            </td>
            
        </tr>
    </table>
</asp:Content>
