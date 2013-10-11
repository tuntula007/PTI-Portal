<%@ Page Language="C#" MasterPageFile="~/Bursary/BursaryMasterPage.master" AutoEventWireup="true"
    CodeFile="SignUpReversal.aspx.cs" Title="SignUp Reversal" Inherits="Admin_SignUpReversal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <h2>
                            SIGNUP REVERSAL</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Wrong Form Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWrongFormNumber" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Correct Form Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCorrectFormNumber" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchParam" runat="server" OnClick="btnSearchParam_Click" Text="Check Status">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr><td colspan="2"><asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label></td></tr>
                <tr>
                    <td style="width: 100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                                        Width="100%" ScrollBars="Vertical">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td>
                                                    <h3>
                                                        WRONG APPLICANT DETAILS</h3>
                                                </td>
                                                <td>
                                                    <h3>
                                                        CORRECT APPLICANT DETAILS</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <asp:DetailsView ID="DetailsView1" runat="server" CellPadding="4" PageSize="40" Font-Size="Small"
                                                        ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Left" Width="100%">
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <FieldHeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="14" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                    </asp:DetailsView>
                                                </td>
                                                <td width="50%">
                                                    <asp:DetailsView ID="DetailsView2" runat="server" CellPadding="4" PageSize="40" Font-Size="Small"
                                                        ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Left" Width="100%">
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <FieldHeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="14" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                    </asp:DetailsView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnReverseSignUp" runat="server" Text="Reverse Signup" 
                                                        onclick="btnReverseSignUp_Click" />
                                                    <asp:Button ID="btnRollBackSignUp"
                                                        runat="server" Text="Rollback Signup" onclick="btnRollBackSignUp_Click" />
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearchParam" />
            <asp:PostBackTrigger ControlID="btnReverseSignUp" />
            <asp:PostBackTrigger ControlID="btnRollBackSignUp" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
