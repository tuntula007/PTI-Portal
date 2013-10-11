<%@ Page Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true"
    CodeFile="Courses.aspx.cs" Inherits="Admin_Courses" %>

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
                                    Add Course</HeaderTemplate>
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
                                                                            Level:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListLevel" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListSemester" runat="server">
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
                                                                            Course Name:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtCourseName" runat="server" SkinID="mediumTxt" Width="400px"></asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Course Code:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtCourseCode" runat="server" SkinID="mediumTxt"></asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Pass Mark:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtPassmark" runat="server" SkinID="mediumTxt">0</asp:TextBox>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        Upload File:
                                                                                    </td>
                                                                                    <td>
                                                                                        <table cellpadding="0" cellspacing="0" style="position: relative; z-index: auto;
                                                                                            overflow: auto; float: left; table-layout: auto">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/AppliedBiology100Lsemester1.xls">Download Template</asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Button ID="AddLecturerToList" runat="server" OnClick="AddLecturerToList_Click"
                                                                                Text="Add To List" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="BtnUpload2" runat="server" OnClick="BtnUpload2_Click" Text="Upload" />
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
                                                                    <td align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                    <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left" Width="1000px" ScrollBars="Both">
                                                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" AutoGenerateDeleteButton="True"
                                                                            OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                                                            OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" OnRowDeleting="grdViewStatustory_OnRowDeleting"
                                                                            Font-Size="Small" ForeColor="Black" GridLines="Vertical" AutoGenerateEditButton="True"
                                                                            AutoGenerateColumns="False" DataKeyNames="Srn">
                                                                            <PagerSettings Position="TopAndBottom" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Srn" HeaderText="Srn" />
                                                                                <asp:BoundField DataField="Course Title" HeaderText="Course Title" />
                                                                                <asp:BoundField DataField="Course Code" HeaderText="Course Code" />
                                                                                <asp:BoundField DataField="Pass Mark" HeaderText="Pass Mark" />
                                                                                <asp:BoundField DataField="Faculty" HeaderText="Faculty" />
                                                                                <asp:BoundField DataField="Department" HeaderText="Department" />
                                                                                <asp:TemplateField HeaderText="Academic Level">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("[Academic Level]") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1"
                                                                                            DataTextField="AcademicLevel" DataValueField="AcademicLevel" SelectedValue='<%# Bind("[Academic Level]") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLevel"
                                                                                            TypeName="FacultyBusiness" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" />
                                                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                                                VerticalAlign="Middle" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                                                        </td>
                                                    </tr>
                                                </td>
                                            </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExport" />
        </Triggers>
    </asp:UpdatePanel>    
    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" OnClick="LinkButton1_Click"
        OnClientClick="Make sure you select .jpg file of good size">LinkButton</asp:LinkButton>
</asp:Content>
