<%@ Page Title="" Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master"
    AutoEventWireup="true" CodeFile="RptCourseRegistrationByCode.aspx.cs" Inherits="RptCourseRegistrationByCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 90%" align="center">
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                </td>
                <td style="text-align: left">
                    <span style="font-size: 14px">COURSE REGISTRATION BY COURSE CODE REPORT</span>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: center; font-size: 12px;" colspan="2">
                    Pick Report Parameters below:
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Programme:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbProgrammeCategory" runat="server">
                        <asp:ListItem>All Category</asp:ListItem>
                        <asp:ListItem>DEGREE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Faculty:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbFaculty" runat="server" DataSourceID="ObjFaculty" DataTextField="FacultyName"
                        DataValueField="FacultyName" 
                        onselectedindexchanged="CmbFaculty_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllFaculty" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllFaculty_CheckedChanged"
                        Text="All Faculty" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Department:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbDepartment" DataSourceID="ObjectDepartmentByCategoryAndFaculty" DataTextField="DepartmentName"
                        DataValueField="DepartmentName" runat="server" OnSelectedIndexChanged="CmbDepartment_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllDepartment" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllDepartment_CheckedChanged"
                        Text="All Department" Visible="True" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Programme Of Study:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbCourse" runat="server" DataSourceID="ObjCoursesByCategoryFacultyAndDepartment" DataTextField="ProgrammeName"
                        DataValueField="ProgrammeCode" OnSelectedIndexChanged="CmbCourse_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllProg" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllProg_CheckedChanged"
                        Text="All Programme" Visible="True" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Level:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbStudentLevel" runat="server" Visible="False">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>300</asp:ListItem>
                        <asp:ListItem>400</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                        <asp:ListItem>600</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllLevel" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllLevel_CheckedChanged"
                        Text="All Level" Visible="False" />
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px;">
                    Academic Session:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbSession" runat="server" DataSourceID="ObjSessions" DataTextField="SessionName"
                        DataValueField="SessionName">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px; vertical-align: top">
                    Registrable Course Code:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbCode" runat="server" DataSourceID="objCourseCode" DataTextField="CourseCode"
                        DataValueField="CourseCode" OnDataBound="cmbCode_DataBound">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px; vertical-align: top">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 190px; vertical-align: top">
                    Columns To Display:
                </td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="CheckBoxDisplayColumn" runat="server" RepeatColumns="2">
                        <asp:ListItem Enabled="false" Selected="True">Name</asp:ListItem>
                        <asp:ListItem Enabled="false" Selected="True">Matric Number</asp:ListItem>
                        <asp:ListItem Value="S.Programme">Programme Category</asp:ListItem>
                        <asp:ListItem Value="CS.FacultyName">Faculty</asp:ListItem>
                        <asp:ListItem Value="CS.DepartmentName">Department</asp:ListItem>
                        <asp:ListItem Value="S.CourseofStudyName">Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="S.Sex">Sex</asp:ListItem>
                        <asp:ListItem Value="S.State">State</asp:ListItem>
                        <asp:ListItem Value="ISNULL(S.phonenumber,'')">Phone Number</asp:ListItem>
                        <asp:ListItem Value=" Cast(isnull(S.AcademicLevel,'') as nvarchar(10))">Current Level</asp:ListItem>
                        <asp:ListItem Value=" isnull(S.Email,'')">Personal Email</asp:ListItem>
                        <asp:ListItem Value=" isnull(S.DLCMail,'')">DLC Mail</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: right; width: 190px; vertical-align: top;
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
                <td class="style11" style="text-align: left; width: 190px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 190px;">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Button ID="BtnShow" runat="server" OnClick="BtnShow_Click" Text="Show Report"
                        Width="101px" />
                </td>
            </tr>
            <tr>
                <td class="style11" style="text-align: left; width: 190px;">
                </td>
                <td style="text-align: left">
                    <asp:ObjectDataSource ID="ObjCourses" runat="server" SelectMethod="GetProgrammeRegisteredFromCourseReg"
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
                            <asp:ControlParameter ControlID="CmbFaculty" Name="ByFaculty" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjCoursesByCategoryFacultyAndDepartment" runat="server"
                        SelectMethod="GetProgrammeRegistered" TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbDepartment" Name="ByDepartment" PropertyName="SelectedValue"
                                Type="String" />
                                <asp:ControlParameter ControlID="CmbFaculty" Name="ByFaculty" PropertyName="SelectedValue"
                                Type="String" />
                                <asp:ControlParameter ControlID="CmbProgrammeCategory" Name="ByCategory" PropertyName="SelectedValue"
                                Type="String" />
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
                            <asp:ControlParameter ControlID="CmbFaculty" Name="ByFaculty" PropertyName="Text" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjSessions" runat="server" SelectMethod="GetSessionsRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="objCourseCode" runat="server" SelectMethod="GetRegisteredCourseCodeByCourse"
                        TypeName="CourseRegReportBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbCourse" Name="ByProgramOfStudy" PropertyName="SelectedValue"
                                Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
