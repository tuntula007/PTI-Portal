<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayClearance.aspx.cs" Inherits="PayClearance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Receipts</title>
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
                font-size: 16px;
                font-weight: bold;
                font-family: Arial;
                margin: 0;
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
            .tablepersonalnew
            {
                width: 100%;
                border-style: solid;
                border-width: 0;
                border-collapse: collapse;
                font-family: arial;
                font-size: 11px;
                text-align: center;
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
                font-size: 17px;
                font-weight: bold;
                font-family: Arial;
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
        .thHeader
        {
            font-size: 20px;
            font-weight: 900;
            text-align: center;
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
            font-size: 20px;
            border-bottom: dashed 1px gray;
            font-weight: bold;
        }
        .style4
        {
            text-align: center;
            font-family: Times New Roman;
            font-size: 22px;
            border-bottom: dashed 1px gray;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table width="75%">
            <tr>
                <td>
                    <table class="HeaderTable">
                        <tr>
                            <td class="style3">
                                <a href="javascript:window.print()" class="printbutton">Print</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="thHeader">
                                <table width="100%">
                                    <tr>
                                        <td width="5%">
                                            <img alt="UILogo" src="images/Logo.png" style="height: 97px; width: 104px" />
                                        </td>
                                        <td width="95%">
                                            <table width="100%">
                                                <tr>
                                                    <td class="thHeader">
                                                       PTI EFFURUN<br />
                                                        DELTA STATE, NIGERIA
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        <br />
                                                        STUDENT RECEIPTS
                                                        <br />
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
            <tr align="center">
                <td align="center">
                    <asp:Panel ID="PanelPersonalDetail" runat="server" Font-Size="16px" Width="100%">
                        <table width="100%" align="center">
                            <tr>
                                <td align="right" width="100%">
                                    Date:
                                    <asp:Label ID="paydate" runat="server" Font-Bold="True" Font-Size="14pt" Text="21/07/2011"></asp:Label>
                                    <%--                                    <br />
                                    Receipt No:
                                    <asp:Label ID="receiptno" runat="server" Font-Bold="True" Font-Size="14pt" Text="DL000000"></asp:Label>
--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" width="100%">
                                    <table width="100%" align="center">
                                        <tr>
                                            <td width="40%" align="center">
                                                <table width="100%" align="center">
                                                    <tr>
                                                        <td width="50%" align="left">
                                                            MATRIC NO:
                                                        </td>
                                                        <td width="60%" align="left">
                                                            <asp:Label ID="lblmatno" runat="server" Font-Bold="True" Font-Size="14pt" Text="matno"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            RECEIVED FROM:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="name" runat="server" Font-Bold="True" Font-Size="14pt" Text="fullname"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            FACULTY:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="faculty" runat="server" Font-Bold="True" Font-Size="14pt" Text="faculty"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            COURSE:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblcourse" runat="server" Font-Bold="True" Font-Size="14pt" Text="programme"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            PROGRAMME:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="programme" runat="server" Font-Bold="True" Font-Size="14pt" Text="programme"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            SESSION:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblsession" runat="server" Font-Bold="True" Font-Size="14pt" Text="session"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            LEVEL:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblacademiclevel" runat="server" Font-Bold="True" Font-Size="14pt"
                                                                Text="courselevel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            BANK:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="bank" runat="server" Font-Bold="True" Font-Size="14pt" Text="ZENITH"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            PAYMENT ID:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="paytype" runat="server" Font-Bold="True" Font-Size="14pt" Text="FULL PAYMENT"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            AMOUNT:
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="amount" runat="server" Font-Bold="True" Font-Size="14pt" Text="0, 000.00"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center" width="50%" valign="top">
                                                <asp:GridView ID="FeesDescriptionGridView" ShowFooter="true" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="FeesDescriptionGridView_RowDataBound">
                                                    <RowStyle BorderStyle="Solid" BorderWidth="1px" Height="15px" Font-Bold="True" Font-Size="12pt" />
                                                    <HeaderStyle Font-Bold="True" Font-Size="10pt" />
                                                    <FooterStyle Font-Bold="true" Font-Size="12pt" />
                                                    <AlternatingRowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>
                                                        <asp:BoundField DataField="FeeName" HeaderText="FEE DESCRIPTION">
                                                            <ItemStyle Width="65%" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Amount" HeaderText="AMOUNT (N)">
                                                            <ItemStyle Width="35%" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
