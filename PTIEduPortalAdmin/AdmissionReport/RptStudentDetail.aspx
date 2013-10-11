<%@ Page Title="" Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master"
    AutoEventWireup="true" CodeFile="RptStudentDetail.aspx.cs" Inherits="RptStudentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 90%" align="center">
            <tr>
                <td colspan="2" style="text-align: left">
                    STUDENT DETAILED REPORTS
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Scope:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbRscope" runat="server">
                        <asp:ListItem Value="1">Paid Students</asp:ListItem>
                        <asp:ListItem Value="2">Unpaid Students</asp:ListItem>
                        <asp:ListItem Value="3">All Students</asp:ListItem>
                        <asp:ListItem Value="4">Admitted and Accepted</asp:ListItem>
                        <asp:ListItem Value="5">Admitted But Not Accepted</asp:ListItem>
                        <asp:ListItem Value="6">Paid and Registered Students</asp:ListItem>
                        <asp:ListItem Value="7">Paid But Not Registered Students</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
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
                <td class="style10" style="text-align: left; width: 161px; vertical-align: top">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px; vertical-align: top">
                    Columns To Display:
                </td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="CheckBoxDisplayColumn" runat="server" RepeatColumns="2">
                        <asp:ListItem Enabled="false" Selected="True">Name</asp:ListItem>
                        <asp:ListItem Enabled="false" Selected="True">Matric Number</asp:ListItem>
                        <asp:ListItem Value="S.Programme">Programme Category</asp:ListItem>
                        <asp:ListItem Value="CS.FacultyName">Faculty</asp:ListItem>
                        <asp:ListItem Value="CS.DepartmentName">Department</asp:ListItem>
                        <asp:ListItem Value="CS.CourseOfStudyName">Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="S.Sex">Sex</asp:ListItem>
                        <asp:ListItem Value="S.State">State</asp:ListItem>
                        <asp:ListItem Value="ISNULL(phonenumber,'')">Phone Number</asp:ListItem>
                        <asp:ListItem Value=" Cast(isnull(S.AcademicLevel,'') as nvarchar(10))">Current Level</asp:ListItem>
                    </asp:CheckBoxList>
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
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
