<%@ Page Language="C#" MasterPageFile="~/Records/RecordsMasterPage.master" AutoEventWireup="true" CodeFile="ModeOfStudys.aspx.cs" Inherits="Admin_State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <ajaxToolkit:TabContainer ID="TxtState" runat="server" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
                            AutoPostBack="true" ActiveTabIndex="0" Width="100%" 
                            Style="margin-bottom: 53px;">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel1">
                                <HeaderTemplate>
                                    Add  Mode of study</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel1" runat="server" Width="100%">
                                        <table cellpadding="2" cellspacing="0">
                                            <tr align="left">
                                                <td colspan="2">
                                                    Mode Of Study
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtModeStudy" runat="server" SkinID="mediumTxt"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: bottom">
                                                    <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" TabIndex="6"
                                                        Text="Submit" />
                                                </td>
                                                <td style="vertical-align: bottom">
                                                    <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="8" Text="Close" />
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: right; vertical-align: bottom">
                                                                &#160;&#160;
                                                            </td>
                                                            <td style="vertical-align: bottom; text-align: right">
                                                                &#160;&#160;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
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
                    <td style="width: 100%">
                        <table style="width: 20%">
                            <tr>
                                <td nowrap="nowrap">
                                    <asp:LinkButton ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return confirm('Are you sure of deleting this item?');"
                                        TabIndex="11" Text="Delete" />
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:LinkButton ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click" TabIndex="9"
                                        Text="Refresh Grid" />
                                </td>
                                <td nowrap="nowrap">
                                    <asp:LinkButton ID="BtnExit" runat="server" OnClick="BtnExit_Click" Text="Exit page" />
                                </td>
                                <%--<td style="width: 40%">
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100%">
                        <div id="hello1" style="overflow: auto; width: 100%; height: 100%; position: relative">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Contacts" NodeIndent="10" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                                Width="100%">
                                <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                                <HoverNodeStyle Font-Underline="False" />
                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                    NodeSpacing="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

