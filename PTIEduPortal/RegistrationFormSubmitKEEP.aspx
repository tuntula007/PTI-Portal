<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="RegistrationFormSubmit.aspx.cs" Inherits="RegistrationFormSubmit" Title="Portal Pro+ : - Course Registration Form"
    MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .text-normal
        {
            border-right: #666666 1px solid;
            border-top: #666666 1px solid;
            font-size: 12px;
            word-spacing: normal;
            vertical-align: middle;
            border-left: #666666 1px solid;
            color: #333333;
            border-bottom: #666666 1px solid;
            font-style: normal;
            font-family: Verdana, Arial;
            letter-spacing: normal;
            height: 18px;
            background-color: #ffffff;
            text-align: left;
        }
        .tablesubject
        {
            width: 95%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }
        .style21
        {
            width: 99%;
        }
        .style2
        {
            width: 96%;
            font-family: Arial;
        }
        .style22
        {
            font-size: small;
            font-style: italic;
        }
        .style23
        {
            width: 594px;
        }
        .style24
        {
            height: 22px;
        }
        .style25
        {
            font-family: arial;
            font-size: 13px;
            color: #0A2A69;
            font-weight: bold;
            text-align: left;
            height: 22px;
        }
        .style37
        {
            text-decoration: underline;
            color: #0A2A69;
        }
        .style38
        {
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table class="style1">
            <tr>
                <td>
                    <table align="center" class="style21">
                        <tr>
                            <td>
                                <table align="center" class="style2">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-family: Times New Roman; font-size: 15px">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; font-family: Times New Roman; font-size: 22px; border-bottom: dashed 1px gray"
                                            class="style23">
                                            <b>Course Registration </b>
                                            <asp:Label ID="lblname" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <i>
                                                <asp:Label ID="CurrentSession" runat="server" Text="2011/2012 Session"></asp:Label></i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2" align="right" class="style22">
                                            Semester: First
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <table align="center" class="tablecredential" border="2">
                                                <tr>
                                                    <td>
                                                        <b>Programme Admitted To</b>
                                                    </td>
                                                    <td>
                                                        <b>Course</b>
                                                    </td>
                                                    <td>
                                                        <b>Current Level</b>
                                                    </td>
                                                    <td>
                                                        <b>Matric/Reg No.</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblsch" runat="server" Text="school"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcourse" runat="server" Text="course"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblyear" runat="server" Text="year"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmatno" runat="server" Text="matno"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style24">
                                        </td>
                                        <td colspan="2" class="style25">
                                            <asp:Label ID="lblMess" runat="server" Text="lblMess"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="PanelCannotPrint" runat="server">
                                                <a href="StudentControlCenter.aspx">You may not be able to print if your passport is
                                                    not available</a>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelCanPrint" runat="server">
                                                <a href="javascript:openwin();">Print My Form</a>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr id="RegSemester" runat="server">
                                        <td>
                                       
                                            &nbsp;</td>
                                    </tr>
                                    <tr id="RegistrationForm" runat="server">
                                        <td>
                                        </td>
                                        <td colspan="2" align="left">
                                            <table class="tablegridcourse">
                                             <tr> <td class="style37">
                                                                      CURRENT SEMESTER COURSES:
                                                                    </td>
                                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        <asp:GridView ID="RegistrableCourseGridView" runat="server" AutoGenerateColumns="False"
                                                            DataSourceID="odsDeptCourses" Width="800px">
                                                            <Columns>
                                                                <asp:BoundField DataField="CourseCode" HeaderText="Course" SortExpression="CourseCode"
                                                                    ItemStyle-CssClass="gridcol1" />
                                                                <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" SortExpression="CourseTitle"
                                                                    ItemStyle-CssClass="gridcol2" />
                                                                <asp:BoundField DataField="CreditLoad" HeaderText="Credit Unit(s)" SortExpression="CreditLoad"
                                                                    ItemStyle-CssClass="gridcol3" />
                                                                <asp:BoundField DataField="CourseType" HeaderText="Status" SortExpression="CourseType"
                                                                    ItemStyle-CssClass="gridcol3" />
                                                                <asp:TemplateField HeaderText="Tick" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ckTickCourse" Checked="true"  Enabled ="false" runat="server" AutoPostBack="true" >  <%--OnCheckedChanged="ckTickCourse_CheckedChanged"--%>
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="cbTickAll" Checked ="true" enabled="false" Text="Tick All" runat="server" AutoPostBack="true" OnCheckedChanged="cbTickAll_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField Visible="false" ID="CreditLoadHiddenField" runat="server" Value='<%#Eval("CreditLoad") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <table align="center">
                                                                    <tr>
                                                                        <td class="gridcol1">
                                                                            Course
                                                                        </td>
                                                                        <td class="gridcol2">
                                                                            Course Title
                                                                        </td>
                                                                        <td class="gridcol3">
                                                                            Credit Unit(s)
                                                                        </td>
                                                                        <td>
                                                                            Status
                                                                        </td>
                                                                        <td>
                                                                            Sign. Of Lecturer
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        <asp:Panel ID="PanelCarryOver" runat="server" Visible="false">
                                                            <table class="style1">
                                                                <tr>
                                                                    <td class="style37">
                                                                      CARRY OVER COURSES:PLEASE SELECT YOUR CARRY OVERS FOR THE SEMESTER BELOW
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdCarryOver" runat="server" AutoGenerateColumns="False" DataSourceID="odsCarryOverCourses"
                                                                            Width="800px" Style="margin-top: 0px">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="CourseCode" HeaderText="Course" SortExpression="CourseCode"
                                                                                    ItemStyle-CssClass="gridcol1" />
                                                                                <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" SortExpression="CourseTitle"
                                                                                    ItemStyle-CssClass="gridcol2" />
                                                                                <asp:BoundField DataField="CreditLoad" HeaderText="Credit Unit(s)" SortExpression="CreditLoad"
                                                                                    ItemStyle-CssClass="gridcol3" />
                                                                                <asp:BoundField DataField="CourseType" HeaderText="Status" SortExpression="CourseType"
                                                                                    ItemStyle-CssClass="gridcol3" />
                                                                                <asp:TemplateField HeaderText="Ticked" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="ckTickedCourse"  AutoPostBack="true" OnCheckedChanged="ckTickedCourse_CheckedChanged"   Checked="false" Enabled="true"  runat="server" TextAlign="Left">
                                                                                        </asp:CheckBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                    <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField Visible="false" ID="CarryOverCreditLoadHiddenField" runat="server" Value='<%#Eval("CreditLoad") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td style="text-align: center">
                                                        <span class="style38">&nbsp;
                                                        </span>
                                                        <asp:Label ID="lblSuccess" runat="server" ForeColor ="Blue" 
                                                            style="text-align: center; font-size: x-large;" Text="  "></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td style="text-align: center">
                                                        <table class="style1">
                                                            <tr>
                                                                <td class="gridcol1">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="gridcol2" align="right">
                                                                     <b><font size="2px">GRAND TOTAL CREDITS:</font></b>
                                                                </td>
                                                                <td class="gridcol3">
                                                                    <asp:Label ID="lblTotCredit" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Label ID="lbltitleError" runat="server" Font-Bold="True" ForeColor="#CC3300"
                                                                        Text="lblerror" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Style="font-weight: 700"
                                                                        Text="Click here to Register" Font-Size="Large" />
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
                                        </td>
                                        <td colspan="2" align="left" class="colorheader">
                                            <asp:ObjectDataSource ID="odsDeptCourses" runat="server" SelectMethod="getSemesterCourses"
                                                TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="courseofstudyId" SessionField="CourseofStudyId"
                                                        Type="Int32" />
                                                    <asp:SessionParameter DefaultValue="" Name="level" SessionField="Level" Type="String" />
                                                    <asp:QueryStringParameter DefaultValue="First" Name="semester" QueryStringField="Semester"
                                                        Type="String" />
                                                    <asp:SessionParameter DefaultValue="0" Name="departmentId" SessionField="DepartmentId"
                                                        Type="Int32" />
                                                    <asp:SessionParameter DefaultValue="" Name="programme" SessionField="Programme" Type="String" />
                                                    <asp:SessionParameter Name="modeofstudy" SessionField="ModeOfStudy" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <asp:ObjectDataSource ID="odsCarryOverCourses" runat="server" SelectMethod="getSemesterCarryOver"
                                                TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="" Name="matricNumber" SessionField="MatricNumber"
                                                        Type="String" />
                                                    <asp:SessionParameter DefaultValue="First" Name="semester" SessionField="Semester"
                                                        Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2" align="left" class="colorheader">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="PanelCannotPrint2" runat="server">
                                                <a href="StudentControlCenter.aspx">You may not be able to print if your passport is
                                                    not available</a>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelCanPrint2" runat="server">
                                                <%--<a href="javascript:openwin();">Print My Form</a>--%>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        function openwin() {

            window.name = "testwindow";
            testwindow = window.open("PrintRegistrationForm.aspx?Semester=First", 'mywindow', 'location=no,status=no,toolbar=no,scrollbars=yes,width=1200,height=680,dependent=yes');
            testwindow.moveTo(100, 100);
            testwindow.focus();
        }
    </script>

</asp:Content>
