<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="MinCredit.aspx.cs" Inherits="Admin_MinCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                            OnActiveTabChanged="TabContainer1_ActiveTabChanged" Width="100%" Style="margin-bottom: 53px;"
                            Font-Size="Small">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel1">
                                <HeaderTemplate>
                                    Add Min Crd</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel" runat="server" HorizontalAlign="Left" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Programme:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListProgramme" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Mode Of Study:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListModeStudy" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Faculty:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListFaculty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListLecturerFaculty_Changed">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Department:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListDept_Changed">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Programme of Study:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListCourseOfStudy" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Level:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListLevel" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListSemester" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Max Core:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMaxCore" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Min Core:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinCore" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Max Elective:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMaxElective" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Min Elective:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinElective" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Max Credit Load:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMaxCredit" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Min Credit Load:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinCredit" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Button ID="Submit" runat="server" OnClick="AddLecturerToList_Click" Text="Submit" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
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
                                                    <asp:Panel ID="PanelGrid" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" nowrap="nowrap" valign="bottom">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" DynamicHorizontalOffset="2"
                                                                                                            Font-Names="Verdana" Font-Size="0.9em" ForeColor="#990000" OnMenuItemClick="Menu1_MenuItemClick"
                                                                                                            Orientation="Horizontal" StaticSubMenuIndent="10px">
                                                                                                            <StaticSelectedStyle BackColor="#FFCC66" />
                                                                                                            <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                                                                                                            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                                                                                            <DynamicMenuStyle BackColor="#FFFBD6" />
                                                                                                            <DynamicSelectedStyle BackColor="#FFCC66" />
                                                                                                            <Items>
                                                                                                                <asp:MenuItem Text="Export" Value="Export"></asp:MenuItem>
                                                                                                                <asp:MenuItem Text="Cancel Edit" Value="Cancel Edit"></asp:MenuItem>
                                                                                                            </Items>
                                                                                                            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                                                                                                            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                                                                                        </asp:Menu>
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
                                                                            <asp:Panel ID="Panel1" runat="server" GroupingText="Info" HorizontalAlign="Left" Width="1000px" ScrollBars="Both">
                                                                                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" AutoGenerateDeleteButton="True"
                                                                                    OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                                                                    OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" OnRowDeleting="grdViewStatustory_OnRowDeleting"
                                                                                    Font-Size="Small" ForeColor="Black" GridLines="Vertical" AutoGenerateEditButton="True">
                                                                                    <PagerSettings Position="TopAndBottom" />
                                                                                    <FooterStyle BackColor="#CCCCCC" />
                                                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                                                        VerticalAlign="Middle" />
                                                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                                </asp:GridView>
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
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel3">
                                <HeaderTemplate>
                                    Edit</HeaderTemplate>
                                <ContentTemplate>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel2">
                                <HeaderTemplate>
                                    Search</HeaderTemplate>
                                <ContentTemplate>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
