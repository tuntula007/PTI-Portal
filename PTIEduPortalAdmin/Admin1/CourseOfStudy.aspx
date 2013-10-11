<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="CourseOfStudy.aspx.cs" Inherits="Admin_CourseOfStudy" %>

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
                                    Add Programme Of Study</HeaderTemplate>
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
                                                                            <asp:DropDownList ID="DDListDept" runat="server">
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
                                                                            <asp:TextBox ID="TxtCourseOfStudy" runat="server" SkinID="mediumTxt" Width="400px"></asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Programme Duration:
                                                                        </td>
                                                                        <td nowrap="nowrap" id="DD">
                                                                            <asp:DropDownList ID="DDListDuration" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Honor:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListHonour" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
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
                                                                            Min Credit Earned:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinCrdtearned" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Min Credit Registered:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinCrdtReg" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Min CGPA:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtMinCGPA" runat="server" SkinID="mediumTxt">0</asp:TextBox>
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
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:LinkButton ID="BtnExport" runat="server" OnClick="BtnExport_Click" TabIndex="9"
                                        Text="Export" />
                                </td>
                                <td align="left">
                                    <asp:LinkButton ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click" TabIndex="9"
                                        Text="Refresh Grid" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" GroupingText="Info" HorizontalAlign="Left"
                            Width="1000px" ScrollBars="Both">
                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="50" AutoGenerateDeleteButton="True"
                                AutoGenerateEditButton="True" OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" OnRowDeleting="grdViewStatustory_OnRowDeleting"
                                Font-Size="Small" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False"
                                DataKeyNames="Id">
                                <PagerSettings Position="TopAndBottom" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="Programme Of Study" HeaderText="Programme Of Study" />
                                    <asp:BoundField DataField="Faculty" HeaderText="Faculty" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" />
                                    <asp:TemplateField HeaderText="Duration Of Study">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Duration Of Study]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="ObjectDuration"
                                                DataTextField="Duration" DataValueField="Duration" SelectedValue='<%# Bind("[Duration Of Study]") %>'>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDuration" runat="server" SelectMethod="GetDuration"
                                                TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Programme">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Programme") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="ObjectProg" DataTextField="Programme"
                                                DataValueField="Programme" SelectedValue='<%# Bind("Programme") %>'>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectProg" runat="server" SelectMethod="GetProg" TypeName="FacultyBusiness">
                                            </asp:ObjectDataSource>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mode Of Study">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Mode Of Study]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="ObjectMode" DataTextField="ModeOfStudy"
                                                DataValueField="ModeOfStudy" SelectedValue='<%# Bind("[Mode Of Study]") %>'>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectMode" runat="server" SelectMethod="GetStudyMode"
                                                TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Honours">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Honours") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="ObjectHons" DataTextField="Honour"
                                                DataValueField="Honour" SelectedValue='<%# Bind("Honours") %>'>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectHons" runat="server" SelectMethod="GetHonor" TypeName="FacultyBusiness">
                                            </asp:ObjectDataSource>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Min Credit Earned" HeaderText="Min Credit Earned" Visible="False" />
                                    <asp:BoundField DataField="Min Credit Registered" HeaderText="Min Credit Registered"
                                        Visible="False" />
                                    <asp:BoundField DataField="Min CGPA" HeaderText="Min CGPA" Visible="False" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                    VerticalAlign="Middle" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <EmptyDataTemplate>
                                    Zero record found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
