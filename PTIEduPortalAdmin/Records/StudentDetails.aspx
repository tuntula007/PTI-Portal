<%@ Page Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentDetails.aspx.cs" Title="Admission Status" Inherits="Admin_StudentDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <h2>
                            CHECK STUDENT DETAILS</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Enter Form/Matric Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchParameter" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchParam" runat="server" OnClick="btnSearchParam_Click" Text="Display">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                            Width="1000px" ScrollBars="Both">
                            <table width="75%" align="center" id="tbDetails" runat="server" visible ="false">
                                <tr>
                                    <td width="50%">
                                    </td>
                                    <td width="50%">
                                        <asp:Image ID="passPort" runat="server" Height="120px" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DetailsView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" Font-Size="Small"
                                            ForeColor="Black" GridLines="Vertical" HorizontalAlign="Left" Width="80%">
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                VerticalAlign="Middle" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                        </asp:DetailsView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearchParam" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
