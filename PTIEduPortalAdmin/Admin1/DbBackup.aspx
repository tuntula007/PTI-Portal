<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="DbBackup.aspx.cs" Inherits="Tools_DbBackup" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="400" OnTick="Timer1_Tick">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
            <asp:PostBackTrigger ControlID="BtnExport"></asp:PostBackTrigger>
        </Triggers>
        <ContentTemplate>
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblProgress" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="PaymentTypePanel2" runat="server" HorizontalAlign="Left">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td colspan='3'>
                                <table>
                                    <tr>
                                        <td style="width: auto">
                                            <asp:Panel ID="PanelList" runat="server" Height="150px" Width="230px" BorderStyle="Solid"
                                                ScrollBars="Both">
                                                <asp:RadioButtonList ID="ChkBoxListStaff" OnSelectedIndexChanged="ChkBoxListStaff_Changed"
                                                    AutoPostBack="true" runat="server">
                                                </asp:RadioButtonList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="return confirm('Are you sure of running payroll database backup?');"
                                    TabIndex="3" Text="Backup Db" />
                            </td>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnRestore" runat="server" OnClick="BtnBtnRestore_Click" TabIndex="4"
                                    Text="Restore Db" OnClientClick="return confirm('Are you sure of restoring last backup payroll database?');" />
                            </td>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" TabIndex="4"
                                    Text="Delete Backup" OnClientClick="return confirm('Are you sure of deleting the selected backup?');" />
                            </td>
                        </tr>
                    </table>
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
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateSelectButton="false"
                                            BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" CaptionAlign="Left"
                                            CellPadding="1" Font-Size="Small" GridLines="Horizontal" HorizontalAlign="Left"
                                            OnPageIndexChanging="BankGridv_PageIndexChanging" SkinID="gridviewSkin" Width="100%">
                                            <PagerSettings Position="TopAndBottom" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="CheckBoxGIN" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxGIN_Changed" />
                                                        <%--<asp:CheckBox ID="CheckBoxGIN" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBoxGIN_Changed" />--%>
                                                        <%--<asp:CheckBox ID="CheckBoxGIN" runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Left" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <EmptyDataTemplate>
                                                Zero record found
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
