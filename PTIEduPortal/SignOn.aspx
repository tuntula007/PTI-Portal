<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOn.aspx.cs" Inherits="SignOn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="css/style1.css" />
    <title> Portal  - Sign up</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        td img
        {
            display: block;
        }
    </style>
    <!--Fireworks CS3 Dreamweaver CS3 target.  Created Mon Nov 23 10:08:57 GMT+0100 2009-->
</head>
<body bgcolor="#ffffff">
    <form runat="server">
    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="941">
            <!-- fwtable fwsrc="template2.png" fwpage="Page 1" fwbase="template2.gif" fwstyle="Dreamweaver" fwdocid = "1559416576" fwnested="0" -->
            <tr>
                <td>
                    <img src="images/spacer.gif" width="7" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="30" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="50" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="44" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="253" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="11" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="472" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="57" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="11" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="6" height="1" border="0" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="1" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td colspan="10">
                    <img name="template2_r1_c1" src="images/template2_r1_c1.gif" width="941" height="7"
                        border="0" id="template2_r1_c1" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="7" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td rowspan="4">
                    <img name="template2_r2_c1" src="images/template2_r2_c1.gif" width="7" height="555"
                        border="0" id="template2_r2_c1" alt="" />
                </td>
                <td colspan="8" class="topmenubg">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="3%" height="26">
                                &nbsp;
                            </td>
                            <td width="38%">
                                &nbsp;
                            </td>
                            <td width="58%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" id="topmainmenu">
                                    <tr>
                                        <td width="34%">
                                            <a href="StudentLogin.aspx">My DLC UI Portal</a>
                                        </td>
                                        <td width="42%">
                                            <a href="feedbackform.aspx">Need Help? Contact Us</a>
                                        </td>
                                        <td width="18%">
                                            <a href="#">FAQ</a>
                                        </td>
                                        <td width="6%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="1%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td rowspan="4">
                    <img name="template2_r2_c10" src="images/template2_r2_c10.gif" width="6" height="555"
                        border="0" id="template2_r2_c10" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="65" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <img name="middleleftLinelong" src="images/middleleftLinelong.gif" width="30" height="400"
                        border="0" id="middleleftLinelong" alt="" />
                </td>
                <td colspan="3" class="logobg">
                    &nbsp;
                </td>
                <td>
                    <img name="template2_r3_c6" src="images/template2_r3_c6.gif" width="11" height="400"
                        border="0" id="template2_r3_c6" alt="" />
                </td>
                <td colspan="2" valign="top" class="loginfieldbg">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="45">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="61">
                                <table width="98%" border="0" cellspacing="0" cellpadding="0" class="hearderborder">
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="7%">
                                                        <img src="images/login_infoicon.png" width="34" height="35" alt="Login information" />
                                                    </td>
                                                    <td width="93%">
                                                        <p>
                                                            <span style="color: #0000FF">For Newly Admtted Student new to the Portal, Enter your details below to Sign Up</span><br />
                                                            <br />
                                                            <%--For Old/Returning Student who have not sign on before, Sign Up <a href="SignOn.aspx">
                                                                Here</a><br />--%>
                                                            If you have have already sign up before, Please Login <a href="StudentLogin.aspx">Here</a><br />
                                                            If you have an existing accout and you can't logon, you can click here to find a
                                                            solution <a href="#">FAQS</a></p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="51">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="loginborder">
                                    <tr>
                                        <td height="29" colspan="5" class="loginTDhead">
                                            New Student Sign up
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="17%" height="41" style="font-weight: bold">
                                            Form Number
                                        </td>
                                        <td width="32%" height="41">
                                            <asp:TextBox ID="txtMatricNo" CssClass="shorttextBox" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="2%" rowspan="4">
                                            &nbsp;
                                        </td>
                                        <td colspan="2" width="49%" style="font-weight: bold">
                                            Enter Application Form Number
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="17%" height="41" style="font-weight: bold">
                                            Order Number
                                        </td>
                                        <td width="32%" height="41">
                                            <asp:TextBox ID="txtPin" CssClass="shorttextBox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td colspan="3" width="51%" style="font-weight: bold">
                                            Enter Bank Confirmation Order Number
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35" style="font-weight: bold">
                                            Password
                                        </td>
                                        <td height="35">
                                            <asp:TextBox ID="txtPassWord" CssClass="shorttextBox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td height="35" style="font-weight: bold">
                                            Confirm password
                                        </td>
                                        <td height="35">
                                            <asp:TextBox ID="txtPasswordC" CssClass="shorttextBox" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="29" style="font-weight: bold">
                                            Email
                                        </td>
                                        <td height="29">
                                            <asp:TextBox ID="txtEmail" CssClass="shorttextBox" runat="server"></asp:TextBox>
                                        </td>
                                        <td height="29" style="font-weight: bold">
                                            Phone number
                                        </td>
                                        <td height="29">
                                            <asp:TextBox ID="txtPhone" CssClass="shorttextBox" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="15">
                                            &nbsp;
                                        </td>
                                        <td height="15" colspan="4" align="center">
                                            <asp:Panel ID="PanelError" runat="server" BackColor="White" Visible="False" Width="301px">
                                                <label><asp:Label ID="LblError" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red"></asp:Label><br />
                                                    <span>Possible Cause: <asp:Label ID="LblErrorCause" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                            ForeColor="Red"></asp:Label></span><br />
                                                    <label><asp:HyperLink ID="LnkLogin" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                            NavigateUrl="~/StudentLogin.aspx" Visible="False">HyperLink</asp:HyperLink></label></label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="29">
                                            &nbsp;
                                        </td>
                                        <td height="29" colspan="4" align="center">
                                            <label>
                                                &nbsp;<asp:Button ID="BtnSignOn" runat="server" Text="Click Here to Sign up" OnClick="BtnSignOn_Click"
                                                    Style="height: 26px" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img name="template2_r3_c9" src="images/template2_r3_c9.gif" width="11" height="400"
                        border="0" id="template2_r3_c9" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="400" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td colspan="8" class="bottombarbg">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="3%" height="29">
                                &nbsp;
                            </td>
                            <td width="97%">
                                For technical queries, send Email to itsupport@dlc.ui.edu.ng
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="40" border="0" alt="" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img name="LeftbottomCurve" src="images/LeftbottomCurve.gif" width="80" height="50"
                        border="0" id="LeftbottomCurve" alt="" />
                </td>
                <td>
                    <img name="centerbottombarshort" src="images/centerbottombarshort.gif" width="44"
                        height="50" border="0" id="centerbottombarshort" alt="" />
                </td>
                <td colspan="3">
                    <img name="centerbottombarLong" src="images/centerbottombarLong.gif" width="736"
                        height="50" border="0" id="centerbottombarLong" alt="" />
                </td>
                <td colspan="2">
                    <img name="rightbottomCurve" src="images/rightbottomCurve.gif" width="68" height="50"
                        border="0" id="rightbottomCurve" alt="" />
                </td>
                <td>
                    <img src="images/spacer.gif" width="1" height="50" border="0" alt="" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
