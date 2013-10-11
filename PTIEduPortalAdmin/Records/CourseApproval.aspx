<%@ Page Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseApproval.aspx.cs" Inherits="Records_CourseApproval" Title="Untitled Page" %>

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
                            AutoPostBack="true" ActiveTabIndex="0" Width="100%" Style="margin-bottom: 53px;">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel1">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel1" runat="server" GroupingText="Registered Courses"
                                        HorizontalAlign="Left" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <asp:Panel ID="Panel6" runat="server" GroupingText="Search for student with Matno,FormNo etc">
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
                                                <td>
                                                    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto">
                                                        <tr>
                                                            <td>
                                                                <table>
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
                                                                        <td>
                                                                            Mode Of Study:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListModeStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListModeStudy_Changed">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
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
                                                                        <td>
                                                                            Level:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListLevels" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Programmme of Study:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListCourseStudy" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="vertical-align: bottom">
                                                                                        <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="return confirm('Be sure that your inputs are correct?');"
                                                                                            TabIndex="3" Text="Display Data" />
                                                                                    </td>
                                                                                    <td style="vertical-align: bottom">
                                                                                        <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="4" Text="Close" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnApprove" runat="server" OnClick="BtnApprove_Click" Text="Approve" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="BtnDisApprove" runat="server" OnClick="BtnDisApprove_Click" Text="DisApprove" />
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
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--<tr>
                    <td>
                        <hr style="background-color: #c40000; height: 1px; border-top-color: Red; border-style: solid" />
                    </td>
                </tr>-->
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
                                            <asp:LinkButton ID="LnkBtnPrin" runat="server" OnClick="BtnPrintGrid_Click" TabIndex="9"
                                                Text="Print" />
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
                                            <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                                                Width="1000px" ScrollBars="Both">
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="#CCCCCC"
                                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CaptionAlign="Left"
                                                    CellPadding="4" Font-Size="Small" HorizontalAlign="Left" OnPageIndexChanging="BankGridv_PageIndexChanging"
                                                    PageSize="50" SkinID="gridviewSkin" Width="100%" CellSpacing="2" ForeColor="Black">
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <RowStyle BackColor="White" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%--<asp:CheckBox ID="CheckBoxGIN" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxGIN_Changed" />--%>
                                                                <asp:CheckBox ID="CheckBoxGIN" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
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
</asp:Content>
