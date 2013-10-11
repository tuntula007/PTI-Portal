<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PinEntry.aspx.cs" Inherits="PinEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="css/style1.css" />
    <title>Registration Portal  - School Fees Pin Verification</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        td img
        {
            display: block;
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 96px;
        }
        .style3
        {
            width: 16px;
        }
        .style4
        {
            width: 96px;
            font-weight: bold;
        }
        .style5
        {
            color: #000;
            background: #E1E7F0;
            font-size: 12px;
            font-weight: bold;
            text-align: left;
            font-family: Tahoma;
            text-decoration: underline;
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
                                        <td width="24%">
                                            <a href="StudentControlCenter.aspx">Website Home</a>
                                        </td>
                                        <td width="32%">
                                            <a href="feedbackform.aspx">Need Help? Contact Us</a>
                                        </td>
                                        <td width="20%">
                                            <a href="#">FAQ</a>
                                        </td>
                                        <td width="24%">
                                            <a href="LogOut.aspx">Log Out</a>
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
                                                        <asp:Label ID="LblInfo" runat="server" Font-Names="Tahoma" ForeColor="#000066"></asp:Label>
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
                                        <td height="29" colspan="2" class="style5">
                                            User Ref Number Authentication
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" height="41" colspan="2" style="width: 100%">
                                            <asp:Panel ID="PanelSkuFee" runat="server" Font-Names="Tahoma" Visible="False">
                                                <table class="style1">
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>Note that you can make a FULL or PART payment by selecting Payment Type (All 
                                                            payment oustanding must be completed by the begining of 2nd Semester)</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:DropDownList ID="PaymentMode" runat="server" CssClass="logintextBox">
                                                                <asp:ListItem Text="Full Payment" Value="Full Payment"></asp:ListItem>
                                                                <asp:ListItem Text="First Installment" Value="First Installment"></asp:ListItem>
                                                                <asp:ListItem Text="Second Installment" Value="Second Installment"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            Sch Fees PIN
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSkuFeePin" runat="server" CssClass="logintextBox" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelError" runat="server" Font-Names="Tahoma" Visible="False" BackColor="#EAFEFF">
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style4">
                                                            Error:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblError" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="Red">Invalid Ref. Number</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4">
                                                            Possible Cause:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblErrorCause" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                                                ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                            </td>
                        </tr>
                        <tr>
                            <td height="15" class="style3">
                                &nbsp;
                            </td>
                            <td height="15">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="29" class="style3">
                                &nbsp;
                            </td>
                            <td height="29">
                                <label>
                                    &nbsp;<asp:Button ID="BtnContinue" runat="server" Text="Continue" OnClick="BtnLogin_Click" />
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
                            For technical queries, send Email to itsupport@pti.edu.ng       </tr>
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
