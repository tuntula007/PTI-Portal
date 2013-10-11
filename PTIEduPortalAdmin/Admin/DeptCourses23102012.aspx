<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="DeptCourses.aspx.cs" Inherits="Admin_DeptCourses" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="always">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" AutoPostBack="true"
                            OnActiveTabChanged="TabContainer1_ActiveTabChanged" Width="100%" Style="margin-bottom: 53px;"
                            Font-Size="Small">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel1">
                                <HeaderTemplate>
                                    Add Registrable courses</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel" runat="server" HorizontalAlign="Left" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto">
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="PaymentTypePanel1" runat="server" GroupingText="Course Info" HorizontalAlign="Left"
                                                                    Width="100%">
                                                                    <table>
                                                                        <tr>
                                                                            <td nowrap="nowrap" style="border-right-color: Black; border-style: solid">
                                                                                <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto;
                                                                                    width: 250px">
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
                                                                                                    <td style="vertical-align: bottom">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Button ID="DisplayC" runat="server" OnClick="DisplayC_Click" Text="Display Courses"
                                                                                                            Width="105px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2" bgcolor="White">
                                                                                                        Move courses of the same credit load<br />
                                                                                                        and Course type &gt;&gt;
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto;
                                                                                                border-color: Blue; border-style: solid; border-width: thick">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <div id="Div4" style="overflow: auto; width: 250px; height: 200px; position: relative">
                                                                                                            <asp:CheckBoxList ID="ChkBoxListAvailCourses" runat="server" Width="250px">
                                                                                                            </asp:CheckBoxList>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td style="border-left-color: Red; border-left-width: thin; border-left-style: solid">
                                                                                                        <table cellspacing="3">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnSelectAll" runat="server" OnClick="Button3_Click" Text="Select All"
                                                                                                                        Width="80px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnDisselect" runat="server" OnClick="BtnDisselect_Click" Text="UnSelect All"
                                                                                                                        Width="80px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="AddLecturerToList" runat="server" OnClick="AddLecturerToList_Click"
                                                                                                                        Text="&gt;&gt;" Width="80px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td nowrap="nowrap" style="border-left-color: Black; border-style: solid">
                                                                                <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto;
                                                                                    width: 300px">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellspacing="2">
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        Mode Of Study:
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:DropDownList ID="DDListStudyMode" runat="server">
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
                                                                                                        Faculty:
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:DropDownList ID="DDListFac2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListFac2_Changed">
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
                                                                                                        <asp:DropDownList ID="DDListDept2" runat="server" OnSelectedIndexChanged="DDListDept2_Changed"
                                                                                                            AutoPostBack="True">
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
                                                                                                        <asp:DropDownList ID="DDListLevel2" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:DropDownList ID="DDListSemester2" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="vertical-align: bottom" class="style2">
                                                                                                        Session
                                                                                                    </td>
                                                                                                    <td class="style2">
                                                                                                        <asp:DropDownList ID="DDListSession" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        Course Type
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:DropDownList ID="DDListCourseType" runat="server">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        Credit Load
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <asp:TextBox ID="TxtCreditLoad" runat="server">0</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td nowrap="nowrap">
                                                                                                        Upload Course:
                                                                                                    </td>
                                                                                                    <td nowrap="nowrap">
                                                                                                        <table cellpadding="0" cellspacing="0" style="position: relative; z-index: auto;
                                                                                                            overflow: auto; float: left; table-layout: auto">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/CourseOfStudy100Lsemester1.xls">Download Template</asp:HyperLink>
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
                                                                                            <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto;
                                                                                                border-color: Blue; border-style: solid; border-width: thick">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <div id="Div1" style="overflow: auto; width: 300px; height: 200px; position: relative">
                                                                                                            <asp:ListBox ID="ListBoxCourses" runat="server" Font-Bold="True" Height="300px" Rows="1000"
                                                                                                                TabIndex="4" Width="300px"></asp:ListBox>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td style="border-left-color: Red; border-left-width: thin; border-left-style: solid">
                                                                                                        <table cellspacing="3">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnUpload2" runat="server" OnClick="BtnUpload2_Click" Text="Upload File" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnRemoveAll" runat="server" OnClick="BtnRemoveAll_Click" Text="&lt;&lt;"
                                                                                                                        Width="70px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnRemovSingle" runat="server" OnClick="BtnRemovSingle_Click" Text="&lt;"
                                                                                                                        Width="70px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="return confirm('Be sure that the right department and faculty is selected?');"
                                                                                                                        TabIndex="6" Text="Submit" Width="70px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="8" Text="Close"
                                                                                                                        Width="70px" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
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
                                </HeaderTemplate>
                                <ContentTemplate>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel2">
                                <HeaderTemplate>
                                    </HeaderTemplate>
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
                                                                                <asp:BoundField DataField="Course Code" HeaderText="Course Code" />
                                                                                <asp:BoundField DataField="Course Title" HeaderText="Course Title" />
                                                                                <asp:BoundField DataField="Programme of Study" 
                                                                                    HeaderText="Programme of Study" />
                                                                                <asp:TemplateField HeaderText="Level">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ObjectDataSource1"
                                                                                            DataTextField="AcademicLevel" DataValueField="AcademicLevel" SelectedValue='<%# Bind("Level") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLevel"
                                                                                            TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Level") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Semester" Visible="False">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ObjectDataSource2"
                                                                                            DataTextField="Semester" DataValueField="Semester" SelectedValue='<%# Bind("Semester") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetSemester"
                                                                                            TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Semester") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Course Type">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="ObjectDataSource4"
                                                                                            DataTextField="CourseType" DataValueField="CourseType" SelectedValue='<%# Bind("[Course Type]") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetCourseType"
                                                                                            TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("[Course Type]") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Study Mode">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="ObjectDataSource3"
                                                                                            DataTextField="ModeOfStudy" DataValueField="ModeOfStudy" SelectedValue='<%# Bind("[Study Mode]") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetStudyMode"
                                                                                            TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("[Study Mode]") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Credit Load" HeaderText="Credit Load" />
                                                                                <asp:BoundField DataField="Programme" HeaderText="Programme" />
                                                                                <asp:BoundField DataField="Session" HeaderText="Session" />
                                                                            </Columns>
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
