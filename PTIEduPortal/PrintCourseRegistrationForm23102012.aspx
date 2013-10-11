<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCourseRegistrationForm.aspx.cs"
    Inherits="PrintCourseRegistrationForm" %>

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
            width: 100%;
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
        .style28_8
        {
            width: 100%;
            font-size: 16px;
        }
        .style35
        {
            font-size: 16px;
            height: 22px;
        }
        .style41
        {
            width: 471px;
            font-size: 16px;
        }
        .style44
        {
            font-size: 16px;
        }
        .style45
        {
            font-size: small;
        }
        .style46
        {
            font-size: 11pt;
            text-align: left;
        }
        .style48
        {
            width: 50%;
            text-align: left;
            font-size: 11pt;
            font-weight: normal;
        }
        .style49
        {
            font-weight: normal;
        }
        .style50
        {
            text-align: left;
        }
        .style51
        {
            text-align: justify;
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
                                                    <td width="5">
                                                        <img alt="UILogo" src="images/Logo.png" style="height: 105px; width: 99px" />
                                                    </td>
                                                    <td width="95%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="thHeader">
                                                                    PETROLEUM TRAINING INSTITUTE<br />
                                                                    <span class="style45">PMB 20,
                                                                    EFFURUN-DELTA STATE, NIGERIA</span></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4">
                                                                    <br />
                                                                    STUDENT REGISTRATION FORM FOR COURSES</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <span>( To be printed in Quadruplicate ).</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelPersonalDetail" runat="server" Font-Size="16px">
                                    <table class="style1">
                                        <tr>
                                            <td align="left" valign="top">
                                                <table class="tablepersonal" cellpadding="2" cellspacing="10">
                                                    <tr>
                                                        <td class="blackheader" align="center" colspan="2">
                                                            <br />
                                                            STUDENT PERSONAL INFORMATION<br />
                                                            <span></span>
                                                        </td>
                                                        <td align="center" rowspan="3" valign="top">
                                                            <asp:Image ID="imgPix" runat="server" Height="121px" Width="131px" ImageUrl="~/images/img.jpg" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            MATRIC NO:<asp:Label ID="lblmatno" runat="server" Text="matno" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                        <td class="style44">
                                                            SURNAME:<asp:Label ID="surname" runat="server" Text="Label" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28" colspan="2">
                                                            OTHER NAMES:
                                                            <asp:Label ID="othernames" runat="server" Font-Bold="True" Font-Size="14pt" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28_8" colspan="3">
                                                            <table class="style28_8" width="100%">
                                                                <tr align="left">
                                                                    <td width="20%" align="left">
                                                                        SEX:<asp:Label ID="lblsex" runat="server" Text="sex" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                                    <td width="40%" align="left">
                                                                        DATE OF BIRTH:<asp:Label ID="lbldob" runat="server" Text="dob" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                                    <td width="40%" align="left">
                                                                        STATE OF ORIGIN:<asp:Label ID="lblstate" runat="server" Text="stateoforigin" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            EMAIL ADDRESS:
                                                            <asp:Label ID="email" runat="server" Font-Bold="True" Font-Size="14pt"
                                                                Text="emailaddress"></asp:Label>
                                                        </td>
                                                        <td colspan="2" class="style35">
                                                            PHONE NUMBER:<asp:Label ID="lblphonenumber" runat="server" Text="phoneno" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            PROGRAMME:<asp:Label ID="lblprogram" runat="server" Text="programme" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                        <td colspan="2" class="style35">
                                                            :<asp:Label ID="lblyear" runat="server" Text="year" Font-Bold="True"
                                                                Font-Size="14pt"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                          <asp:Label ID="lbldegree" runat="server" Text="degree" Font-Bold="True" Font-Size="14pt"></asp:Label></td>
                                                        <td class="style44" colspan="2">
                                                            COURSE OF STUDY:
                                                            <asp:Label ID="lblcourse" runat="server" Text="programme" Font-Bold="True" Font-Size="14pt"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            DIRECTORATE:
                                                            <asp:Label ID="lblsch" runat="server" Font-Bold="True" Font-Size="14pt" Text="school"></asp:Label>
                                                        </td>
                                                        <td class="style41" colspan="2">
                                                        
                                                         <!--   TEACHING SUBJECT/COMBINATION: -->
                                                            
                                                            &nbsp;</td>
                                                    </tr>
                                                    <!--
                                                    <tr>
                                                        <td class="style28" colspan="3">
                                                            SESSION OF SUSPENSION (IF APPLICABLE)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28" colspan="3">
                                                            SESSION OF SUSPENSION (IF APPLICABLE)
                                                        </td>
                                                    </tr>
                                                    -->
                                                    <tr>
                                                        <td class="style28">
                                                            SESSION OF REGISTRATION:
                                                            <asp:Label ID="lblsession" runat="server" Font-Bold="True" Font-Size="14pt" Text="session"></asp:Label>
                                                        </td>
                                                        <td colspan="2" class="style35">
                                                            YearOfStudy/Class Code:
                                                            <asp:Label ID="lblacademiclevel" runat="server" Text="courselevel" Font-Bold="True"
                                                                Font-Size="14pt"></asp:Label>
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
                                                <asp:Label ID="FormHeader1" Text="CURRENT COURSES :"
                                                    runat="server"></asp:Label>
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
                                                                            <asp:Image ImageUrl='<%# "~/Images/" + Eval("ApprovalStatus") +".jpg" %>' ID="AprovStatus" runat="server" />
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
                                                                        <b><font size="4px">Semester Total Credit Units:</font></b>
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
                                        
                                       <tr> <td class="style50"> 
                                           <asp:Label ID="lblCarryOver" runat="server" Text="CARRY OVER COURSES :"></asp:Label>
                                           :</td></tr>  
                                        
                                        <table class="thHeader"  id="carryTable" runat ="server"  >
                                            <tr valign="top">
                                                <td>
                                                    
                                                    
                                                    <asp:GridView ID="grdCarryOver" runat="server" AutoGenerateColumns="False" DataSourceID="odsCarryOverCourses"
                                                                                                            Width="1000px" BorderColor="Black" BorderStyle="Solid"
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
                                                                            <asp:Image ImageUrl='<%# "~/Images/" + Eval("ApprovalStatus") +".jpg" %>' ID="AprovStatusCO" runat="server" />
                                                                            <asp:Label ID="lblAprovalStatusCO" Text='<%# Eval("ApprovalStatus") %>' runat="server"></asp:Label>
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
                                                    <table class="blackheader" id ="totals" runat ="server">
                                                        <tr>
                                                            <td class="gridcol1">
                                                            </td>
                                                            <td align="right" class="gridcol2">
                                                                <b><font size="4px">Carry Over Total Credit Units:</font></b>
                                                            </td>
                                                            <td class="gridcol3">
                                                                <asp:Label ID="lblTotCarriedUnits" runat="server" Font-Bold="True" Font-Size="13" 
                                                                    Text="0.0"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        
                                                         <tr>
                                                            <td class="gridcol1">
                                                            </td>
                                                            <td align="right" class="gridcol2">
                                                                <b><font size="4px"></font></b>
                                                            </td>
                                                            <td class="gridcol3">
                                                                
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        
                                                         <tr>
                                                            <td class="gridcol1">
                                                            </td>
                                                            <td align="right" class="gridcol2">
                                                                <b><font size="4px">Grand Total Credit Units:</font></b>
                                                            </td>
                                                            <td class="gridcol3">
                                                                <asp:Label ID="lblGrandTotalCreditUnits" runat="server" Font-Bold="True" Font-Size="13" 
                                                                    Text="0.0"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              
                                <asp:ObjectDataSource ID="odsCarryOverCourses" runat="server" SelectMethod="getSemesterRegisteredCarryOver"
                                                TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="" Name="matricNumber" SessionField="MatricNumber"
                                                        Type="String" />
                                                    <asp:SessionParameter DefaultValue="First" Name="semester" SessionField="Semester"
                                                        Type="String" />
                                                         <asp:SessionParameter DefaultValue="2012" Name="session" SessionField="CurrentSession" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                              
                                
                                <asp:ObjectDataSource ID="odsDeptCourses1" runat="server" SelectMethod="getSemesterRegisteredCourses"
                                    TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness" 
                                    OldValuesParameterFormatString="original_{0}">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="" Name="matricNumber" SessionField="MatricNumber"
                                            Type="String" />
                                        <asp:SessionParameter DefaultValue="2012" Name="session" SessionField="CurrentSession" Type="String" />
                                       <asp:SessionParameter DefaultValue="First" Name="semester" SessionField="Semester"
                                        Type="String" />
                                        
                                        
                                                 <%-- <asp:SessionParameter DefaultValue="001" Name="matricnumber" SessionField="MatricNumber"
                                                        Type="String" />
                                               
                                                        <asp:SessionParameter DefaultValue="First" Name="semester" SessionField="Semester"
                                                        Type="String" />
                                                    <asp:SessionParameter DefaultValue="2012/2013" Name="session" SessionField="SessionName"
                                                        Type="String" />--%>
                                        
                                        
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            
                            
                             <td>
                                 &nbsp;</td>
                        </tr>
                        <tr valign="bottom">
                            <td>
                                <table cellspacing="5" class="style1">
                                    <tr>
                                        <td class="style51">
                                            STUDENT&#39;S SIGNATURE / DATE:</td>
                                        <td>
                                                            
                                                            <asp:Label ID="lblteachingsubject" runat="server" Text="teachingsubject"
                                                                Font-Bold="True" Font-Size="14pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                     </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left">
                                           _________________________________________
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left">
                                        </td>
                                        <td style="width: 50%; text-align: center; vertical-align: top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            DISTRIBUTION:</td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style48">
                                            &nbsp; EXAMINATIONS &amp; RECORDS DIVISION</td>
                                        <td style="width: 50%; text-align: center">
                                            ________________________________________________________
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top; font-size: 11pt;">
                                            &nbsp;<span class="style49"> STUDENT AFFAIRS OFFICER</span></td>
                                        <td style="width: 50%; text-align: center; vertical-align: top">
                                            HEAD OF DEPARTMENT&#39;S SIGNATURE / DATE</td>
                                    </tr>
                                    <tr>
                                        <td class="style46">
                                            &nbsp;<span class="style49"> DEPT. CONCERNED</span></td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left; font-size: 11pt;">
                                            &nbsp;<span class="style49"> STUDENT CONCERNED</span></td>
                                        <td style="width: 50%; text-align: center">
                                            ________________________________________________________
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; text-align: left; vertical-align: top; font-size: 9pt;">
                                            <span lang="en-us">&nbsp;NOTE: </span>
                                        </td>
                                        <td style="width: 50%; text-align: center; vertical-align: top">
                                            REGISTRAR&#39;S
                                            SIGNATURE / DATE
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <span lang="en-us">&nbsp;<span class="style49">To be printed in quadruplicate by 
                                            every student at the begining of every semester</span></span></td>
                                        <td align="right">
                                            <a href="javascript:window.print()" class="printbutton">Print>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                </table>
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
