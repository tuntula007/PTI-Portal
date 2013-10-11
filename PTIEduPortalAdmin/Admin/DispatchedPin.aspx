<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="DispatchedPin.aspx.cs" Inherits="Admin_DispatchedPin" %>

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
                                    Allocate Application Pin Batches</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel" runat="server" HorizontalAlign="Left" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="4">
                                                                <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto">
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Study Center:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListCentre" runat="server">
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
                                                                            Programme
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListProg" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListProg_Changed">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            Pin Price
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:TextBox ID="TxtCost" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:Button ID="BtnAllocate" runat="server" OnClick="BtnAllocate_Click" Text="Allocate Batch"
                                                                                OnClientClick="return confirm('Please, make sure you select the right session, study center and programme are ok?');" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <table align="left" cellpadding="0" cellspacing="0" class="style2">
                                                            <tr>
                                                                <td align="left" style="width: 90%">
                                                                    <div id="hello1" style="overflow: auto; width: 100%; height: 100%; position: relative">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="White"
                                                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CaptionAlign="Left"
                                                            CellPadding="3" Font-Size="Small" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="GridView1_OnPageIndexChanging"
                                                            PageSize="40">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBoxGIN2" runat="server" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel3">
                                <HeaderTemplate>
                                    Application Pin Batch Approve</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="4">
                                                                <table style="float: left; overflow: auto; position: relative; z-index: auto; table-layout: auto">
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap="nowrap">
                                                                            <asp:Button ID="BtnApprove" runat="server" OnClick="BtnApprove_Click" Text="Approve"
                                                                                OnClientClick="return confirm('Please, be very sure session, study center and programme are correct before approving ?');" />
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:Button ID="BtnDisapprove" runat="server" OnClick="BtnDisapprove_Click" Text="Disapprove" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <table align="left" cellpadding="0" cellspacing="0" class="style2">
                                                            <tr>
                                                                <td align="left" style="width: 90%">
                                                                    <div id="Div1" style="overflow: auto; width: 100%; height: 100%; position: relative">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &#160;&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" BackColor="White"
                                                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CaptionAlign="Left"
                                                            CellPadding="3" Font-Size="Small" ForeColor="Black" GridLines="Vertical" OnPageIndexChanging="GridView3_OnPageIndexChanging"
                                                            PageSize="40">
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBoxGIN3" runat="server" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel2">
                                <HeaderTemplate>
                                    Print Application Pins</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PanelPartPay" runat="server" Width="100%">
                                        <table>
                                            <tr valign="top" style="float: left">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Print Option
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DDListPrintOption" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <div id="Div2" style="overflow: auto; width: 100%; height: 100%; position: relative">
                                                            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#CC9966"
                                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" PageSize="40" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                                <PagerSettings Position="TopAndBottom" />
                                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" HorizontalAlign="Left"
                                                                    VerticalAlign="Middle" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBoxGIN" runat="server" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="Btndelete" runat="server" Text="Remove Printing" OnClick="BtndeleteClick"
                                                                                OnClientClick="return confirm('Are you sure of removing this batch from printing next time?');">
                                                                            </asp:Button></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField AccessibleHeaderText="Action" ButtonType="Button" SelectText="Print/Export Pins"
                                                                        ShowSelectButton="True">
                                                                        <ItemStyle ForeColor="#FF3300" />
                                                                    </asp:CommandField>
                                                                </Columns>
                                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="Menu3" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
