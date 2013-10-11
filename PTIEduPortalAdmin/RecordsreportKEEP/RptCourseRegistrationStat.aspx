<%@ Page Title="" Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master"
    AutoEventWireup="true" CodeFile="RptCourseRegistrationStat.aspx.cs" Inherits="RptCourseRegistrationStat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table style="width: 90%" align="center">
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                </td>
                <td style="text-align: left">
                    COURSE REGISTRATION DISTRIBUTION REPORTS
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Scope:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbRscope" runat="server">
                        <asp:ListItem Value="1">All Students</asp:ListItem>
                        <asp:ListItem Value="2">New Students</asp:ListItem>
                        <asp:ListItem Value="3">Old Students</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style10" style="text-align: left; width: 161px;">
                    Report Type
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="CmbRtype" runat="server">
                        <asp:ListItem Value="1">Distribution by Faculty</asp:ListItem>
                        <asp:ListItem Value="2">Distribution by Department</asp:ListItem>
                        <asp:ListItem Value="3">Distribution by Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="4">Distribution by Programme Category</asp:ListItem>
                        <asp:ListItem Value="5">Distribution by Overall</asp:ListItem>
                        <asp:ListItem Value="6">By Programme Of Study vs Faculty</asp:ListItem>
                        <asp:ListItem Value="7">By Programme Category vs Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="8">By Programme Category vs Faculty</asp:ListItem>
                        <asp:ListItem Value="9">By Department vs Faculty</asp:ListItem>
                        <asp:ListItem Value="10">By Department vs Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="11">By Department vs Programme Category</asp:ListItem>
                        <asp:ListItem Value="12">By Faculty vs Programme Of Study</asp:ListItem>
                        <asp:ListItem Value="13">By Faculty vs Programme Category</asp:ListItem>
                    </asp:DropDownList>
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
                    Filter By:
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="cmbStdFaculty" runat="server" DataSourceID="ObjFaculty"
                        DataTextField="FacultyName" DataValueField="FacultyName">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkAllFaculty" runat="server" AutoPostBack="True"
                        Text="All" />
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
                    <asp:ObjectDataSource ID="ObjSessions" runat="server" SelectMethod="GetSessionsRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjFaculty" runat="server" SelectMethod="GetFacultyRegistered"
                        TypeName="CourseRegReportBusiness"></asp:ObjectDataSource>
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
