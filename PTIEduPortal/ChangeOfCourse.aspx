<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="ChangeOfCourse.aspx.cs" Inherits="ChangeOfCourse" Title="Change Of Course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 405;
        }
        .style3
        {
            width: 112px;
            height: 37px;
        }
        .style4
        {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelParam" runat="server" Font-Names="Tahoma" Font-Size="Small" Style="text-align: left">
        <table class="style1">
            <tr>
                <td>
                </td>
                <td>
                    <h2>
                        Change Of Programme</h2>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style4">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#000066" Text="You did not meet the minimum requirement for Programme of Study you originally applied for. &lt;br /&gt; Alternative Programme(s) you can change to are listed below. &lt;br /&gt;Select based on your preference and click on Accept button to continue with your admission."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Application Form Number:
                </td>
                <td>
                    <asp:Label ID="lblFormNumber" runat="server">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Full Name:
                </td>
                <td>
                    <asp:Label ID="lblFullname" runat="server">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Programme Of Study Applied For:
                </td>
                <td>
                    <asp:Label ID="lblProgramOfStudy" runat="server">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Change Programme Of Study To:
                </td>
                <td>
                    <asp:DropDownList ID="CmbCourse" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td>
                    I hereby certify to change to programme of study selected above.<br />
                    I also understand that I may not be able to change back thereafter.<br />
                    <asp:CheckBox ID="chkAgree" runat="server" Text="I agree" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                </td>
                <td>
                    <asp:Button ID="BtnViewAdmitted" runat="server" Text="Accept" OnClick="BtnViewAdmitted_Click"
                        Width="162px" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" NavigateUrl="~/ViewAdmittedStudentsSpecial.aspx">Return to Admission Notification Page 
                    Page</asp:HyperLink>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
