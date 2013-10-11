<%@ Page Language="C#" MasterPageFile="~/Editorial/EditorialMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseMatUpload.aspx.cs" Inherits="Admin_CourseMatUpload" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <%--<td colspan="2" bgcolor="#FFFBD6">
                <asp:Menu ID="Menu1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em"
                    Orientation="Horizontal" StaticSubMenuIndent="10px" BackColor="#FFFBD6" DynamicHorizontalOffset="2"
                    ForeColor="#990000">
                    <StaticSelectedStyle BackColor="#FFCC66" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/icon_directory_over.gif" NavigateUrl="~/Contents/AdminContentGallery.aspx"
                            Text="|Gallery" Value="|Gallery"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/htPDIcon.gif" NavigateUrl="~/Contents/SearchGallery.aspx"
                            Text="|Search Gallery" Value="|Search Gallery"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>--%>
        </tr>
        <tr>
            <%--<td colspan="2" bgcolor="#FFFBD6">
                <asp:Menu ID="Menu1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em"
                    Orientation="Horizontal" StaticSubMenuIndent="10px" BackColor="#FFFBD6" DynamicHorizontalOffset="2"
                    ForeColor="#990000">
                    <StaticSelectedStyle BackColor="#FFCC66" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/icon_directory_over.gif" NavigateUrl="~/Contents/AdminContentGallery.aspx"
                            Text="|Gallery" Value="|Gallery"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/htPDIcon.gif" NavigateUrl="~/Contents/SearchGallery.aspx"
                            Text="|Search Gallery" Value="|Search Gallery"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>--%>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="border-style: solid; border-color: #FF0000;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlFileAttributes" runat="server" Font-Size="8pt" GroupingText="Configure content attributes">
                    <table cellspacing="1" class="style1">
                        <tr>
                            <td colspan="3" valign="top">
                                <asp:Label ID="Label1" runat="server" Text="Ducument Title: Name or Caption"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:TextBox ID="txtTitle" runat="server" Width="98%" BackColor="#FFCCFF"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" width="50%">
                                <asp:BulletedList ID="bulletError" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
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
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap">
                                            Faculty:
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:DropDownList ID="DDListFaculty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListLecturerFaculty_Changed">
                                            </asp:DropDownList>
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
                                    </tr>   
                                    <tr>
                                    <td colspan="2">
                                    
                                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Refresh Codes</asp:LinkButton>
                                    
                                    </td>
                                    </tr>                                
                                    <tr>
                                    <td>
                                            <asp:Label ID="Label2" runat="server" Text="Course Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDListCourseCode" runat="server" Width="100%" 
                                                BackColor="#FFCCFF" AutoPostBack="True" OnSelectedIndexChanged="DDListCourseCode_Changed">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel6" runat="server" Width="100%">
                                                <table align="left" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkExpireMode" runat="server" Text="This content should expire" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Expire Date"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtExpireDate" runat="server" Width="99%" BackColor="#FFCCFF"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel3" runat="server" GroupingText="Document Security" Wrap="False">
                                                <asp:CheckBox ID="chkDownloadRestricted" runat="server" Checked="True" Text="Content can be Downloaded?" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ajaxToolkit:CalendarExtender ID="calenderext" Format="MMMM dd, yyyy" TargetControlID="txtExpireDate"
                                                runat="server" PopupPosition="TopRight">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" colspan="2">
                                <asp:Panel ID="pnlUpload" runat="server" Font-Size="7pt" GroupingText="File Attachment">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="#1" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="lnkAdd1" runat="server" OnClick="lnkAdd1_Click">Add More...</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="#2" Visible="False" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="lnkAdd2" runat="server" OnClick="lnkAdd2_Click" Visible="False">Add More...</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload3" runat="server" ToolTip="#3" Visible="False" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="lnkAdd3" runat="server" OnClick="lnkAdd3_Click" Visible="False">Add More...</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload4" runat="server" ToolTip="#4" Visible="False" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="lnkAdd4" runat="server" OnClick="lnkAdd4_Click" Visible="False">Add More...</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload5" runat="server" ToolTip="#5" Visible="False" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="lnkAdd5" runat="server" OnClick="lnkAdd5_Click" Visible="False">Add More...</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="60%">
                                                <asp:FileUpload ID="FileUpload6" runat="server" ToolTip="#6" Visible="False" />
                                            </td>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top" width="50%">
                <asp:Panel ID="Panel4" runat="server" Font-Size="8pt" GroupingText="Document Note: Brief overview or description of content"
                    Width="100%">
                    <asp:TextBox ID="txtContentBody" runat="server" Rows="5" TextMode="MultiLine" Width="98%"></asp:TextBox>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel5" runat="server">
                    <table cellspacing="1" class="style1">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkPublish" runat="server" Text="Publish Content" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmdSubmitContent" runat="server" Text="Submit Content" OnClick="cmdSubmitContent_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
                            <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server" GroupingText="Info" HorizontalAlign="Left"
                            Width="1000px" ScrollBars="Both">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" AutoGenerateDeleteButton="True"
                                                AutoGenerateEditButton="True" 
                                                OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                                OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" 
                                                OnRowDeleting="grdViewStatustory_OnRowDeleting" 
                                                Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                                                HorizontalAlign="Left">
                                                <PagerSettings Position="TopAndBottom" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            </td>
                        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
