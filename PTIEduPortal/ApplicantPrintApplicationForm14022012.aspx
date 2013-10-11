<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicantPrintApplicationForm.aspx.cs"
    Inherits="ApplicantPrintApplicationForm" %>

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
            width: 100%;
        }
        .thHeader
        {
            font-size: 25px;
            font-weight: 900;
            letter-spacing: 3px;
        }
        .style3
        {
            height: 6px;
            text-align: right;
        }
        .subheader
        {
            text-align: left;
            font-family: Times New Roman;
            font-size: 22px;
            border-bottom: dashed 1px gray;
            font-weight: bold;
        }
        .style4
        {
            text-align: center;
            font-family: Times New Roman;
            font-size: 18px;
            border-bottom: dashed 1px gray;
            font-weight: bold;
        }
        .style28
        {
        }
        .style34
        {
            height: 22px;
        }
        .style33
        {
            height: 17px;
        }
        .style41
        {
            width: 471px;
        }
        .style18
        {
            width: 100%;
            border-collapse: collapse;
            border-style: dashed;
            border-width: 1px;
        }
        .style48
        {
            width: 106px;
            height: 17px;
        }
        .style49
        {
            width: 106px;
            height: 17px;
            font-weight: bold;
        }
        .style50
        {
            text-decoration: underline;
        }
        .style55
        {
        }
        .style57
        {
        }
        .style60
        {
            height: 21px;
        }
        .style61
        {
            height: 23px;
        }
        .style62
        {
            text-decoration: underline;
            height: 19px;
        }
        .style63
        {
            font-family: Tahoma;
            font-size: 15px;
            text-decoration: underline;
        }
        .style64
        {
            width: 235px;
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
                            <tr>
                                <td>
                                    <table class="HeaderTable">
                                        <tr>
                                            <td class="thHeader" align="center">
                                                PETROLEUM TRAINING INSTITUTE NIGERIA</td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center">
                                                <table width="100%" align="center">
                                                    <tr>
                                                        <td rowspan="3" width="90">
                                                            <img alt="" src="images/Logo.png" style="height: 113px; width: 114px" />
                                                        </td>
                                                        <td align="center">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" width="131px" rowspan="3" valign="bottom">
                                                            <asp:Image ID="imgPix" runat="server" Height="140px" ImageUrl="/picx/blank.png" Width="131px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4" align="center">
                                                            <font size="5">
                                                            <asp:Label ID="lblProgramme" runat="server" Font-Bold="True"></asp:Label>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4">
                                                            <font size="5">
                                                            <asp:Label ID="lblsession" runat="server" Font-Bold="True" 
                                                                Text="2012/2013 APPLICATION FORM"></asp:Label>
                                                            </font>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            
                                                <asp:LinkButton ID="lnkApplication" runat="server" 
                                                                PostBackUrl="~/ApplicantControlCenter.aspx" 
                                                    style="text-align: right; font-weight: 700; color: #FF0000;">
                                                              &lt;&lt;&lt; Back To MyPortaHome Page</asp:LinkButton>
                                           </td>
                                           
                                             <td class="style3">
                                                <a href="javascript:window.print()" class="printbutton">Print</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelPersonalDetail" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <span class="blackheader">PERSONAL PARTICULARS:</span><asp:Label ID="ApplicationForm"
                                                        runat="server" Font-Bold="True" Font-Size="13pt" ForeColor="#0A2A69" Font-Names="Tahoma"
                                                        Font-Underline="False"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td class="style28">
                                                                Surname:&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="surname" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                Other Names:
                                                                <asp:Label ID="othernames" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                Title:&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            Religion:
                                                                            <asp:Label ID="lblReligious" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td width="50%">
                                                                            Gender:
                                                                            <asp:Label ID="sex" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                GSM No:
                                                                <asp:Label ID="phone" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            Email:
                                                                            <asp:Label ID="email" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td width="50%">
                                                                            <%--Blood Group:&nbsp;
                                                                            <asp:Label ID="lblBG" runat="server" Text="Label"></asp:Label>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                Marital Status:
                                                                <asp:Label ID="lblMaritalStatus" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            Date of Birth:
                                                                            <asp:Label ID="dob" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td width="50%">
                                                                            &nbsp;Nationality:<asp:Label ID="lblNationality" runat="server" Text="Label"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                LGA:
                                                                <asp:Label ID="lblLGA" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            State:
                                                                            <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td width="50%">
                                                                            Country:&nbsp;
                                                                            <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style41" colspan="2">
                                                                Contact Address:&nbsp;
                                                                <asp:Label ID="homeadd" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                Next Of Kin Name:
                                                                <asp:Label ID="lblNextOfKinName" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="50%">
                                                                            Next Of Kin Email:
                                                                            <asp:Label ID="lblNextOfKinEmail" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td width="50%">
                                                                            Next Of Kin Phone:&nbsp;
                                                                            <asp:Label ID="lblNextOfKinPhone" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style28">
                                                                Next Of Kin Address:&nbsp;
                                                                <asp:Label ID="lblNextOfKinAddress" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style41">
                                                                Relationship to Next Of Kin:<asp:Label ID="lblNextOfKinRelationship" runat="server"
                                                                    Text="Label"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelEntryQual" runat="server" Height="100%" Visible="True">
                                        <table class="style1">
                                            <tr>
                                                <td align="left" class="blackheader" colspan="2">
                                                    O-LEVEL RESULTS:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" colspan="2">
                                                    <table class="tablepersonal">
                                                        <tr>
                                                            <td valign="top">
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td style="width: 50%; border-right-color: Black; border-right-style: solid" valign="top">
                                                                            <table class="style18" cellspacing="10">
                                                                                <tr>
                                                                                    <td colspan="4" style="text-align: left; width: 100%">
                                                                                        <table>
                                                                                            <tr valign="top">
                                                                                                <td colspan="3">
                                                                                                    1. <i>First Sitting</i>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style49">
                                                                                                    Exam Type:
                                                                                                </td>
                                                                                                <td class="style33" colspan="2">
                                                                                                    <asp:Label ID="exam1" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td class="style33">
                                                                                                    <b>Exam Date</b> :
                                                                                                    <asp:Label ID="date1" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <b>Exam No:</b>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="center1" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr align="left">
                                                                                    <td colspan="4" style="text-align: left; width: 100%">
                                                                                        <table cellspacing="10" width="100%">
                                                                                            <tr>
                                                                                                <td class="style50">
                                                                                                    <b>Subject</b>
                                                                                                </td>
                                                                                                <td class="style50">
                                                                                                    <b>Grade</b>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style33">
                                                                                                    English Language
                                                                                                </td>
                                                                                                <td class="style33">
                                                                                                    <asp:Label ID="G11" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Mathematics
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G12" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S13" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G13" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S14" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G14" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S15" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G15" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S16" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G16" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S17" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G17" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S18" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G18" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S19" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G19" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S10" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G10" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td id="trSecondSeating" runat="server" valign="top">
                                                                            <table class="style18" cellspacing="10">
                                                                                <tr>
                                                                                    <td colspan="4" style="text-align: left; width: 100%">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    2. <i>Second Sitting</i>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style48">
                                                                                                    <b>Exam Type: </b>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="exam2" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <b>Exam Date</b> :
                                                                                                    <asp:Label ID="date2" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <b>Exam No:</b>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="center2" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="text-align: left; width: 100%">
                                                                                        <table cellspacing="10" width="100%">
                                                                                            <tr>
                                                                                                <td class="style62">
                                                                                                    <b>Subject</b>
                                                                                                </td>
                                                                                                <td class="style62">
                                                                                                    <b>Grade</b>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style33">
                                                                                                    <asp:Label ID="S21" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td class="style33">
                                                                                                    <asp:Label ID="G21" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style33">
                                                                                                    <asp:Label ID="S22" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                                <td class="style33">
                                                                                                    <asp:Label ID="G22" runat="server" Text="Label"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S23" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G23" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S24" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G24" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S25" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G25" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S26" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G26" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S27" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G27" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S28" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G28" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S29" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G29" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="S20" runat="server"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="G20" runat="server"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center">
                                                                            <b>(Original/Photocopies of Credentials &amp; Bank Teller will be presented for Verification)</b>
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelWesleyChoice" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td class="blackheader" align="center">
                                                    PREFFERED PROGRAM OF STUDIES
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td class="style57">
                                                                First Choice
                                                            </td>
                                                            <td class="style55">
                                                                Second Choice
                                                            </td>
                                                            <td class="style55">
                                                                Third Choice</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style55">
                                                                <asp:Label ID="lblDLCFirstChoice" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style55">
                                                                <asp:Label ID="lblDLCSecondChoice" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style55">
                                                                <asp:Label ID="lblDLCThirdChoice" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style55" colspan="2">
                                                                &nbsp;Preferred Examination Town:
                                                                <asp:Label ID="ExamTown" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style55">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style55" colspan="3">
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style55" colspan="3">
                                                                <table class="style1">
                                                                    <tr>
                                                                        <td class="style63" colspan="2">
                                                                            <b>PREVIOUS EDUCATION HISTORY</b></td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            Qualification</td>
                                                                        <td>
                                                                            <asp:Label ID="prevQual" runat="server">Label</asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            School</td>
                                                                        <td>
                                                                            <asp:Label ID="prvSch" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            Month/Year</td>
                                                                        <td>
                                                                            <asp:Label ID="prvmnthyr" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            Exam/MatricNumber</td>
                                                                        <td>
                                                                            <asp:Label ID="prvExmatno" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            Decipline</td>
                                                                        <td>
                                                                            <asp:Label ID="pvrDec" runat="server" Text="Label"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style64">
                                                                            Grade</td>
                                                                        <td>
                                                                            <asp:Label ID="prvGrade" runat="server" Text="Label"></asp:Label>
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
                                                            <td class="style55" colspan="2">
                                                                &nbsp;</td>
                                                            <td class="style55">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr valign="top" id="trPreviousInfo" runat="server">
                                <td>
                                    <asp:Panel ID="PanelCourseSelected" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td class="blackheader">
                                                    Previous Student Info:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td class="style57">
                                                                Previous Matric Number
                                                            </td>
                                                            <td class="style55" colspan="2">
                                                                Previous Program of Study
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style61" style="border-bottom-style: solid; border-bottom-width: 2px">
                                                                <asp:Label ID="lblPreviousMatric" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style61" colspan="2" style="border-bottom-style: solid; border-bottom-width: 2px">
                                                                <asp:Label ID="lblPreviousCourse" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style57">
                                                                Start Date
                                                            </td>
                                                            <td class="style55" colspan="2">
                                                                End Date
                                                            </td>
                                                        </tr>
                                                        <tr style="border-top-style: solid; border-top-color: Black; border-width: thick">
                                                            <td class="style60">
                                                                <asp:Label ID="lblPreviousStartDate" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td class="style60" colspan="2">
                                                                <asp:Label ID="lblPreviousEndDate" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="20%">
                                                                How you heard about us:</td>
                                                            <td>
                                                                <asp:Label ID="lblRef" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="blackheader">
                                                    DECLARATION:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <table cellpadding="2" cellspacing="2" class="tablepersonal">
                                                        <tr>
                                                            <td class="style28" colspan="3">
                                                                I&nbsp; herby declare:<ul>
                                                                    <li class="style57">That the information supplied in this Form is to the best of my 
                                                                        knowledge and belief, accurate in every detail; and</li><li class="style57">if 
                                                                        any time it is discovered that the information I have given is false or 
                                                                        incorrect, I will be </li><li class="style57">required to withdraw or liable to 
                                                                        prosecution or both; and</li><li class="style57">that if I am admitted, I shall 
                                                                        keep the&nbsp; rules and regulations of the Petroleum Training&nbsp; Institute</li>
                                                                </ul>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td class="style34">
                                                                __________________________________
                                                            </td>
                                                            <td class="style34">
                                                                ___________________________________
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td class="style34">
                                                                Student's Signature/Date
                                                            </td>
                                                            <td class="style34">
                                                                Guarantor's Name/Signature/Date
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style34" colspan="2">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="4" class="style1">
                                        <tr>
                                            <td align="right">
                                                <a href="javascript:window.print()" class="printbutton">Print</a>
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
