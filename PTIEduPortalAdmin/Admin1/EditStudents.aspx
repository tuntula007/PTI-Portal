<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="EditStudents.aspx.cs" Inherits="Admin_EditStudents" Title="Untitled Page" %>

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
                        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
                            ActiveTabIndex="0" Width="100%" Style="margin-bottom: 53px;">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Apply Events" ID="TabPanel5">
                                <HeaderTemplate>
                                    Edit Student information</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PanelPartPay" runat="server" Width="100%">
                                        <table cellpadding="0" cellspacing="0" style="border-color: Black; border-style: solid;
                                            text-align: left; width: 100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <asp:Panel ID="Panel6" runat="server" GroupingText="Search with form number or surname">
                                                        <table style="position: relative; z-index: 0; border-color: Red; border-style: solid">
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
                                                                    Value
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtSearchstaff" runat="server" Width="300px"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:Button ID="BtnSearchEmplyee1" runat="server" OnClick="BtnSearch1_Click" Text="Search" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table cellspacing="1px" style="position: relative; top: 0px; left: 0px">
                                                            <tr valign="top">
                                                                <td>
                                                                    <asp:Panel ID="Panel10" runat="server" Height="210px" Width="300px" BorderStyle="Solid"
                                                                        ScrollBars="Both">
                                                                        Student List:
                                                                        <asp:RadioButtonList ID="ChkBoxListStaff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChkBoxListStaff_Changed">
                                                                        </asp:RadioButtonList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <table cellspacing="3" style="border-color: Black; border-style: solid">
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="BtnSelectAll" runat="server" OnClick="BtnSelectAll_Click" Text="Select All"
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
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <table style="position: relative; z-index: auto; border-color: Black; border-style: solid;
                                                                        text-align: left;">
                                                                        <caption>
                                                                            Enter new values
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellspacing="2">
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Surname:
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:TextBox ID="TxtSurname" runat="server" Width="190px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Other Names
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:TextBox ID="TxtOtherNames" runat="server" Width="190px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Email
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:TextBox ID="txtEmail" runat="server" Width="190px"></asp:TextBox>
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                                                                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Phone
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:TextBox ID="TxtPhone" runat="server" Width="190px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Date of Birth
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:TextBox ID="TxtStartDate" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                Sex
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:DropDownList ID="DDListSex" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:Button ID="BtnAssign02" runat="server" OnClick="BtnAssign02_Click" OnClientClick="return confirm('Please, be very sure of your changes');"
                                                                                                    Text="Apply Changes" />
                                                                                            </td>
                                                                                            <td nowrap="nowrap">
                                                                                                <asp:Button ID="BtnExitpg02" runat="server" OnClick="BtnExitpg02_Click" Text="Close" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </caption>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <asp:Panel ID="PanelRations" runat="server" Height="170px">
                                                        <table style="z-index: auto; position: relative; float: left; overflow: auto; table-layout: auto;
                                                            top: 0px; left: 0px;">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    School
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="TxtSchool" runat="server" ReadOnly="True" SkinID="mediumTxt" Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Department
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="TxtDept" runat="server" ReadOnly="True" SkinID="mediumTxt" Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Course of study
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="TxtCourseStudy" runat="server" ReadOnly="True" SkinID="mediumTxt"
                                                                        Width="350px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Programme
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtProg" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    Study mode
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtStudyMode" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    MatNo
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtMatno" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Level
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtLevel" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    Honor
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtHonor" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    Duration
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtDuration" runat="server" ReadOnly="True" SkinID="mediumTxt"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtStartDate"
                                                                        Format="yyyy-MM-dd" Enabled="True" PopupPosition="TopLeft">
                                                                    </ajaxToolkit:CalendarExtender>
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
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Approve Events" ID="TabPanel4">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ContentTemplate>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <span>
                            
                        </span>
                    </td>
                    </span>
                </tr>--%>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:LinkButton ID="BtnExport" runat="server" OnClick="BtnExport_Click" TabIndex="9"
                                        Text="Export" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100%">
                        <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                            Width="1000px" ScrollBars="Both">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="#CCCCCC"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CaptionAlign="Left"
                                CellPadding="4" Font-Size="Small" HorizontalAlign="Left" OnPageIndexChanging="BankGridv_PageIndexChanging"
                                PageSize="50" SkinID="gridviewSkin" Width="100%" CellSpacing="2" ForeColor="Black">
                                <PagerSettings Position="TopAndBottom" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <RowStyle BackColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
