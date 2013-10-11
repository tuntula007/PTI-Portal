<%@ Page Language="C#" MasterPageFile="~/Bursary/BursaryMasterPage.master" AutoEventWireup="true" CodeFile="Fees.aspx.cs" Inherits="Admin_Fees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                            OnActiveTabChanged="TabContainer1_ActiveTabChanged" Width="100%" Style="margin-bottom: 53px;">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel1">
                                <HeaderTemplate>
                                    <span><span>Setup Plan Cost</span></span></HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel1" runat="server" GroupingText="Pay Group Setup" HorizontalAlign="Left"
                                        Width="100%">
                                        <table cellspacing="1" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PanelRations" runat="server">
                                                        <table style="z-index: auto; position: relative; float: left; overflow: auto; table-layout: auto;
                                                            top: 0px; left: 0px;">
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Share Holder Category
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDListCategory" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Duration Plan
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DDListDuration" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListDuration_Changed">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Months
                                                                </td>
                                                                <td nowrap="nowrap">
                                                                    <asp:TextBox ID="TxtMonths" runat="server" SkinID="mediumTxt" ReadOnly="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Cost
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtCost" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap="nowrap">
                                                                    Description
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtDesc" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="BtnClose" runat="server" Text="Close" OnClick="BtnClose_Click" />
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
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="" ID="TabPanel2">
                                <HeaderTemplate>
                                    <</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" GroupingText="" HorizontalAlign="Left"
                                        Width="100%">
                                    </asp:Panel>
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
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" PageSize="40" AutoGenerateDeleteButton="True"
                                                AutoGenerateEditButton="True" OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                                OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" OnRowDeleting="grdViewStatustory_OnRowDeleting">
                                                <PagerSettings Position="TopAndBottom" />
                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <RowStyle BackColor="White" ForeColor="#330099" />
                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                            </asp:GridView>
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
    <%--<asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>--%>
</asp:Content>

