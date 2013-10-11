<%@ Page Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Font-Names="Tahoma" Font-Size="Small">
        <table>
            <tr>
                <td valign="bottom">
                    Date Range
                </td>
                <td>
                    <table>
                        <tr>
                            <td nowrap="nowrap">
                                <asp:Label ID="LabelSdate" runat="server" Text="Start Date"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:Label ID="LabelEdate" runat="server" Text="End Date"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TxtStartdate" runat="server" Style="z-index: 1" SkinID="mediumTxt"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TxtEnddate" runat="server" Style="z-index: 1" SkinID="mediumTxt"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    Report Type:
                </td>
                <td style="width: 350px">
                    <asp:DropDownList ID="DDListReportType" runat="server" AutoPostBack="false" OnSelectedIndexChanged="CmbReportType_SelectedIndexChanged"
                        Style="height: 22px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Session
                </td>
                <td>
                    <asp:DropDownList ID="DDListSession" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Mode Of Study:
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DDListStudyMode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Programme:
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DDListProgramme" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Level:
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="DDListLevels" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    Course Code:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="TxtCourseCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table>
                        <tr>
                            <td nowrap="nowrap">
                                <asp:Panel ID="PanelNames" runat="server" GroupingText="Use Search to display students names">
                                    <table style="position: relative; z-index: 1; border-color: Red; border-style: solid">
                                        <tr>
                                            <td nowrap="nowrap">
                                            </td>
                                            <td nowrap="nowrap">
                                            </td>
                                            <td nowrap="nowrap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td nowrap="nowrap">
                                                            Value
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <asp:TextBox ID="TxtSearchstaff" runat="server" Width="270px"></asp:TextBox>
                                                        </td>
                                                        <td nowrap="nowrap">
                                                            <asp:Button ID="BtnSearchEmplyee" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel7" runat="server" Height="200px" Width="300px" BorderStyle="Solid"
                                                                ScrollBars="Both">
                                                                Staff List:
                                                                <asp:RadioButtonList ID="ChkBoxListStaff" runat="server" Width="300px">
                                                                </asp:RadioButtonList>
                                                            </asp:Panel>
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
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnSave" runat="server" Text="Show" Width="85px" OnClick="BtnSave_Click" />
                </td>
                <td>
                    &#160;
                </td>
                <td>
                    &#160;
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                        TargetControlID="TxtStartdate" Format="yyyy-MM-dd" PopupPosition="TopLeft">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                        TargetControlID="TxtEnddate" Format="yyyy-MM-dd" PopupPosition="TopLeft">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    &#160;
                </td>
                <td>
                    &#160;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
