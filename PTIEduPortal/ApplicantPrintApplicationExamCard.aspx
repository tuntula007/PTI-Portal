<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicantPrintApplicationExamCard.aspx.cs"
    Inherits="ApplicantPrintApplicationExamCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Application Form</title>
    <link rel="Stylesheet" type="text/css" href="css/style1.css" />
    <style type="text/css">
        @media print
        {
            html
            {
                margin: 0;
            }
            body
            {
                color: black;
                background: white;
                font-size: 12px;
                font-family: Arial;
                margin: 0;
                text-align: center;
            }
            .tablecredential
            {
                width: 90%;
                border-style: dashed;
                border-width: 1px;
                border-collapse: collapse;
                font-family: arial;
                font-size: 12px;
            }
            .tablepersonal
            {
                width: 98%;
                border-style: solid;
                border-width: 0;
                border-collapse: collapse;
                font-family: arial;
                font-size: 11px;
                text-align: left;
                margin: 0px;
            }
            .pagebreaker
            {
                page-break-before: avoid;
            }
            .nopagebreak
            {
                page-break-before: avoid;
            }
            .printbutton
            {
                display: none;
            }
        }
        @media screen
        {
            body
            {
                color: black;
                background: white;
                font-size: 12px;
                font-family: Arial;
                text-align: center;
            }
            html
            {
                margin: 10px;
                border: 1px solid;
            }
            .printbutton
            {
                color: Blue;
            }
        }
        .HeaderTable
        {
            width: 100%;
            text-align: left;
        }
        .style1
        {
            width: 96%;
            margin-left: 0px;
            height: 15px;
        }
        .thHeader
        {
            font-size: 25px;
            font-weight: 900;
            letter-spacing: 3px;
        }
        .subheader
        {
            text-align: left;
            font-family: Times New Roman;
            font-size: 22px;
            border-bottom: dashed 1px gray;
            font-weight: bold;
        }
        .style63
        {
            font-size: 12px;
            text-align: left;
        }
        .style65
        {
            font-size: 12px;
            text-align: left;
            width: 307px;
        }
        .style66
        {
            font-size: 12px;
            text-align: left;
            width: 213px;
        }
        .style67
        {
            font-size: 12px;
            text-align: left;
            }
        .style69
        {
            width: 73px;
        }
        .style70
        {
            font-size: 12px;
            text-align: left;
        }
        .style71
        {
            text-align: right;
        }
        .style72
        {
            font-size: 12px;
            text-align: left;
            width: 186px;
        }
        .style73
        {
            font-size: 12px;
            text-align: left;
        }
        .style74
        {
            font-size: 12px;
            text-align: left;
            font-weight: normal;
        }
        .style75
        {
            font-size: 16px;
            text-align: center;
        }
        .style76
        {
            font-size: 12px;
            text-align: left;
            width: 307px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlForm" runat="server" Width="80%">
            <table class="style1" align="center">
                <tr>
                    <td>
                        <table class="style1">
                            <tr valign="top" id="trPreviousInfo" runat="server">
                                <td>
                                    <asp:Panel ID="PanelCourseSelected" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td align="left" valign="top">
                                                    <a class="printbutton" href="javascript:window.print()">Print</a></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <asp:LinkButton ID="lnkApplication" runat="server" 
                                            PostBackUrl="~/ApplicantControlCenter.aspx" 
                                            style="text-align: right; color: #FF0000; font-weight: 700;">
                                            &lt;&lt;&lt; Back To MyPortaHome Page</asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a class="printbutton" href="javascript:window.print()">Print</a></asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="4" class="style1">
                                        <tr>
                                            <td class="style71">
                                                <table border="1" class="style1">
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style69" style="font-size: 9px">
                                                                        <img alt="" src="images/Logo.png" style="height: 95px; width: 101px" /></td>
                                                                    <td class="style63">
                                                                        PETROLEUM TRAINING INSTITUTE, PRIVATE MAIL BAG 20,EFFURUN,<br />
                                                                        DELTA STATE - NIGERIA</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style65">
                                                                        ENTRY FORM SERIAL NO:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="ApplicationForm" runat="server" Font-Bold="True" 
                                                                            Font-Names="Tahoma" Font-Size="13pt" Font-Underline="False" ForeColor="#0A2A69" 
                                                                            style="font-size: 12px"></asp:Label>
                                                                    </td>
                                                                    <td rowspan="6">
                                                                        <asp:Image ID="imgPix" runat="server" Height="92px" ImageUrl="/picx/blank.png" 
                                                                            Width="99px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style65">
                                                                        CANDIDATE EXAM NO:</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style76">
                                                                        CHOICE OF PROGRAMME: </td>
                                                                    <td style="text-align: left">
                                                                        <font size="5">
                                                                        <asp:Label ID="lblProgramme" runat="server" Font-Bold="True" 
                                                                            style="font-size: 12px; text-align: left;"></asp:Label>
                                                                        </font>
                                                                        <b><font>:</font></b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style65">
                                                                        (<font size="5"><asp:Label ID="lblsession" runat="server" Font-Bold="True" 
                                                                            style="font-size: 12px" Text="2013/2014 SESSION"></asp:Label></font>)</td>
                                                                    <td style="text-align: left; vertical-align: top;" rowspan="3">
                                                                        <asp:Label ID="lblDLCFirstChoice" runat="server" style="font-size: 9px; font-weight: 700;" 
                                                                            Text="Label"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style65">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style65">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style66">
                                                                        SURNAME:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="surname" runat="server" style="font-size: 12px" Text="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style66">
                                                                        OTHER NAME(S):</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="othernames" runat="server" style="font-size: 12px" Text="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style66">
                                                                        DATE OF BIRTH:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="dob" runat="server" style="font-size: 12px" Text="Label"></asp:Label>
                                                                    </td>
                                                                    <td class="style63">
                                                                        SEX:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="sex" runat="server" style="font-size: 12px" Text="Label"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style66">
                                                                        Address of candidate:</td>
                                                                    <td colspan="3" style="text-align: left">
                                                                        <asp:Label ID="homeadd" runat="server" style="font-size: 10px" Text="Label"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style66">
                                                                        Signature:</td>
                                                                    <td class="style70" colspan="3">
                                                                        __________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style66">
                                                                        CHOICE OF EXAM CENTER:</td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="ExamTown" runat="server" style="font-size: 12px" Text="Label"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style75">
                                                            CERTIFICATE OF WITNESS</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style63">
                                                            I certify that the photograph is a true resemblance of&nbsp; the above named person</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style67">
                                                                        Signature of Witness:</td>
                                                                    <td class="style70">
                                                                        ____________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style67">
                                                                        Full Name:</td>
                                                                    <td class="style63">
                                                                        ____________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style67">
                                                                        Profession:</td>
                                                                    <td class="style63">
                                                                        ____________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style67">
                                                                        Office Addres:</td>
                                                                    <td class="style63">
                                                                        ____________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style67">
                                                                        Home Address:</td>
                                                                    <td class="style63">
                                                                        ____________________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style67">
                                                                        Date:</td>
                                                                    <td class="style63">
                                                                        ____________________________</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style75">
                                                            ENTRANCE EXAM SUBJECTS</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style72">
                                                                        Part I:</td>
                                                                    <td class="style70">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style72">
                                                                        General Paper</td>
                                                                    <td class="style70">
                                                                        <asp:TextBox ID="txtGenpaper" runat="server" style="font-weight: 700" 
                                                                            Width="172px">ENGLISH LANGUAGE</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style72">
                                                                        &nbsp;</td>
                                                                    <td class="style63">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style72">
                                                                        Part II: Subjects Combination</td>
                                                                    <td class="style63">
                                                                        <asp:TextBox ID="txtOthersubjects" runat="server" Enabled="False" 
                                                                            style="font-weight: 700" Width="298px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style75">
                                                            DECLARATION BY CANDIDATE</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style74" colspan="2">
                                                                        I make this entry according to the provisions of Petroleum Training Institute 
                                                                        (PTI) regulations and solenmly declare that I Shall abide by all rules governing 
                                                                        the proper conduct of the (PTI Examinations)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style73">
                                                                        Signature of Candidate</td>
                                                                    <td class="style63">
                                                                        _____________________</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style73">
                                                                        Date:</td>
                                                                    <td class="style63">
                                                                        _____________________</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
            <asp:Label ID="lblEMes" runat="server"></asp:Label>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
