<%@ Page Title="" Language="C#" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="true" CodeFile="RptAccountSummary.aspx.cs" Inherits="AccountSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Panel ID="Panel1" runat="server" Width="100%" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 95%" align="center">
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;
                </td>
                <td style="text-align: left; text-decoration: underline;">
                    ACCOUNT SUMMARY REPORTS
                </td>
            </tr>
            <tr><td colspan="2"></td></tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Programme Category:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbProgrammeCategory" runat="server">
                        <asp:ListItem>All Category</asp:ListItem>
                        <asp:ListItem>DEGREE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Faculty:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="Faculty" runat="server" DataSourceID="ObjFaculty" DataTextField="FacultyName"
                        DataValueField="FacultyName" AutoPostBack="True" OnSelectedIndexChanged="Faculty_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllFaculty" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllFaculty_CheckedChanged"
                        Text="All Faculty" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Department:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbDepartment" DataSourceID="ObjectDepartmentByCategoryAndFaculty"
                        DataTextField="DepartmentName" DataValueField="DepartmentName" runat="server"
                        OnSelectedIndexChanged="CmbDepartment_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllDepartment" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllDepartment_CheckedChanged"
                        Text="All Department" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Programme Of Study:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbCourse" runat="server" DataSourceID="ObjCoursesByCategoryFacultyAndDepartment"
                        DataTextField="ProgrammeName" DataValueField="ProgrammeCode">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllProg" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllProg_CheckedChanged"
                        Text="All Programme Of Study" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Student Level:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbStudentLevel" runat="server">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>300</asp:ListItem>
                        <asp:ListItem>400</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                        <asp:ListItem>600</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllLevel" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllLevel_CheckedChanged"
                        Text="All Level" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Academic Session:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbSession" runat="server" DataSourceID="ObjSessions" DataTextField="SessionName"
                        DataValueField="SessionName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Date From (yyyy/mm/dd):
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Date To (yyyy/mm/dd):
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: right; width: 161px; vertical-align: top;
                    font-size: medium;">
                    Report Format:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbReportOption" runat="server" Font-Size="15px">
                        <asp:ListItem Value="PDF" Selected="True">PDF Format </asp:ListItem>
                        <asp:ListItem Value="Excel">Excel Format</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Button ID="BtnShow" runat="server" OnClick="BtnShow_Click" Text="Show Report"
                        Width="101px" />
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 161px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:ObjectDataSource ID="ObjCourses" runat="server" SelectMethod="GetProgrammeRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjCoursesByCategory" runat="server" SelectMethod="GetProgrammeRegistered"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjCoursesByCategoryAndFaculty" runat="server" SelectMethod="GetProgrammeRegistered"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                            <asp:ControlParameter ControlID="Faculty" Name="ByFaculty" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjCoursesByCategoryFacultyAndDepartment" runat="server"
                        SelectMethod="GetProgrammeRegistered" TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                            <asp:ControlParameter ControlID="Faculty" Name="ByFaculty" PropertyName="Text" />
                            <asp:ControlParameter ControlID="CmbDepartment" Name="ByDepartment" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjFaculty" runat="server" SelectMethod="GetFacultyRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjFacultyByCategory" runat="server" SelectMethod="GetFacultyRegistered"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDepartment" runat="server" SelectMethod="GetDepartmentsRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDepartmentByCategory" runat="server" SelectMethod="GetDepartmentsRegistered"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDepartmentByCategoryAndFaculty" runat="server" SelectMethod="GetDepartmentsRegistered"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="Text" />
                            <asp:ControlParameter ControlID="Faculty" Name="ByFaculty" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjSessions" runat="server" SelectMethod="GetSessionsRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom"
                        Format="yyyy-MM-dd" Enabled="True">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo"
                        Format="yyyy-MM-dd" Enabled="True">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

