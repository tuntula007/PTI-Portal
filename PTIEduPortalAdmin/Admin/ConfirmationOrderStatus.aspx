<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConfirmationOrderStatus.aspx.cs" Title="Payment Status" Inherits="Admin_ConfirmationOrderStatus" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <h2>
                            CHECK CONFIRMATION ORDER STATUS</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Enter Confirmation Order Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchParameter" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Confirmation Order Type:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbtncontype" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="SchoolFeesPin">School Fees</asp:ListItem>
                                        <asp:ListItem Value="ApplicationFeesPin">Application Form</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchParam" runat="server" OnClick="btnSearchParam_Click" Text="Check">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                                        Width="1000px" ScrollBars="Both">
                                        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" Font-Size="Small"
                                            ForeColor="Black" GridLines="Vertical" HorizontalAlign="Left" Width="80%">
                                            <PagerSettings Position="TopAndBottom" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                VerticalAlign="Middle" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearchParam" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
