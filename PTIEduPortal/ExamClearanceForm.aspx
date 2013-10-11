<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamClearanceForm.aspx.cs"
    Inherits="ExamClearanceForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Registration</title>
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
        .style1
        {
            width: 80%;
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
        .style28
        {
            width: 400px;
            font-size: 16px;
        }
        .style35
        {
            font-size: 16px;
            height: 22px;
        }
        .style45
        {
            font-family: Tahoma;
            font-size: 15px;
            color: #0A2A69;
            font-weight: bold;
            text-align: left;
            text-decoration: underline;
            width: 514px;
        }
        .style46
        {
            width: 514px;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table class="style1">
            <tr>
                <td>
                    <table class="style1">
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
                                                        <img alt="UILogo" src="images/UILogo.png" />
                                                    </td>
                                                    <td width="90%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="thHeader">
                                                                    UNIVERSITY OF IBADAN<br />
                                                                    DISTANCE LEARNING CENTER
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4">
                                                                    <br />
                                                                    EXAM CLEARANCE FORM
                                                                    <br />
                                                                    <asp:Label ID="lblsession" runat="server" Font-Bold="True" Font-Size="14pt" Text="session"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:Image ID="imgPix" runat="server" Height="121px" Width="131px" ImageUrl="~/images/img.jpg" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelPersonalDetail" runat="server" Font-Size="16px">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" valign="top">
                                                <table class="tablepersonal" cellpadding="2" cellspacing="10">
                                                    <tr>
                                                        <td class="style45" align="center" colspan="2">
                                                            STUDENT PERSONAL INFORMATION
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style46" colspan="2">
                                                            MATRIC NO: <asp:Label ID="lblmatno" runat="server" Text="matno" Font-Bold="True" Font-Size="14pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style46" colspan="2">
                                                            NAME:
                                                            <asp:Label ID="name" runat="server" Text="fullname" Font-Bold="True" Font-Size="14pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            PROGRAMME: <asp:Label ID="lblcourse" runat="server" Text="programme" Font-Bold="True" Font-Size="14pt"></asp:Label>
                                                        </td>
                                                        <td class="style35">
                                                            LEVEL:
                                                            <asp:Label ID="lblacademiclevel" runat="server" Font-Bold="True" 
                                                                Font-Size="14pt" Text="courselevel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            EMAIL ADDRESS:
                                                            <asp:Label ID="email" runat="server" Font-Bold="True" Font-Size="14pt" Text="emailaddress"></asp:Label>
                                                        </td>
                                                        <td class="style35">
                                                            PHONE NUMBER: <asp:Label ID="lblphonenumber" runat="server" Text="phoneno" Font-Bold="True"
                                                                Font-Size="14pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            SEX: <asp:Label ID="lblsex" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                                Text="sex"></asp:Label>
                                                        </td>
                                                        <td class="style35">
                                                            MARITAL STATUS:
                                                            <asp:Label ID="lblmarital" runat="server" Font-Bold="True" Font-Size="14pt" 
                                                                Text="marital status"></asp:Label>
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
                                <asp:Panel ID="CourseForm1Panel" runat="server">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td align="left" class="colorheader">
                                                <asp:Label ID="FormHeader1" Text="COURSES REGISTERED FOR 2009/2010 SESSION" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top">
                                                <table class="tablegridcourse">
                                                    <tr valign="top">
                                                        <td>
                                                            <asp:GridView ID="CourseForm1GridView" runat="server" AutoGenerateColumns="False"
                                                                DataSourceID="odsDeptCourses1" Width="1000px" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px">
                                                                <RowStyle BorderStyle="Solid" BorderWidth="1px" Height="25px" Font-Bold="True" Font-Size="12pt" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="CourseCode" HeaderText="COURSE CODE" ItemStyle-CssClass="gridcol1"
                                                                        SortExpression="CourseCode">
                                                                        <ItemStyle CssClass="gridcol1" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CourseTitle" HeaderText="COURSE TITLE" ItemStyle-CssClass="gridcol2"
                                                                        SortExpression="CourseTitle">
                                                                        <ItemStyle CssClass="gridcol2" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CreditLoad" HeaderText="UNIT(S)" ItemStyle-CssClass="gridcol3"
                                                                        SortExpression="CreditLoad">
                                                                        <ItemStyle CssClass="gridcol3" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CourseType" HeaderText="STATUS" ItemStyle-CssClass="gridcol3"
                                                                        SortExpression="CourseType">
                                                                        <ItemStyle CssClass="gridcol3" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="APPROVAL STATUS">
                                                                        <ItemTemplate>
                                                                            <asp:Image ImageUrl='<%# "~/Images/" + Eval("ApprovalStatus") +".jpg" %>' ID="AprovStatus"
                                                                                runat="server" />
                                                                            <asp:Label ID="lblAprovalStatus" Text='<%# Eval("ApprovalStatus") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td class="gridcol1">
                                                                                COURSE
                                                                            </td>
                                                                            <td class="gridcol2">
                                                                                COURSE TITLE
                                                                            </td>
                                                                            <td class="gridcol3">
                                                                                CREDIT UNIT(S)
                                                                            </td>
                                                                            <td class="gridcol3">
                                                                                STATUS
                                                                            </td>
                                                                            <td>
                                                                                APPROVAL STATUS
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="10pt" />
                                                                <AlternatingRowStyle BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table class="blackheader">
                                                                <tr>
                                                                    <td class="gridcol1">
                                                                    </td>
                                                                    <td class="gridcol2" align="right">
                                                                        <b><font size="4px">Total Credit Units:</font></b>
                                                                    </td>
                                                                    <td class="gridcol3">
                                                                        <asp:Label ID="lblTotCredit1" runat="server" Font-Bold="True" Text="0.0" Font-Size="13"></asp:Label>
                                                                    </td>
                                                                    <td>
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
                                <asp:ObjectDataSource ID="odsDeptCourses1" runat="server" SelectMethod="getSessionRegisteredCourses"
                                    TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="" Name="matricNumber" SessionField="MatricNumber"
                                            Type="String" />
                                        <asp:SessionParameter DefaultValue="2010" Name="session" SessionField="CurrentSession"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
