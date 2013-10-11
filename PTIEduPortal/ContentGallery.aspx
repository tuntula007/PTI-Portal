<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="ContentGallery.aspx.cs" Inherits="Contents_AdminContentGallery" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td colspan="2">
                        <hr style="border-style: solid; border-color: #FF0000;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel1" runat="server" Font-Size="7pt" GroupingText="Contents" Width="100%">
                            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanging="GridView1_OnSelectedIndexChanging"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" Style="font-size: x-small; font-family: Calibri"
                                Width="100%" AutoGenerateColumns="False">
                                <RowStyle BackColor="#F7F7DE" />
                                <Columns>
                                    <asp:CommandField AccessibleHeaderText="Select" SelectText="" ShowSelectButton="True"
                                        ButtonType="Image" HeaderText="Download" SelectImageUrl="~/Images/icon_downloads_over.gif">
                                        <HeaderStyle ForeColor="Red" />
                                        <ItemStyle Font-Bold="True" Font-Size="11pt" ForeColor="#0000CC" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Course Code" HeaderText="Course Code" />
                                    <asp:BoundField DataField="Course Title" HeaderText="Course Title" />
                                    <asp:BoundField DataField="Course Description" 
                                        HeaderText="Course Description" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="10pt" ForeColor="Red" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
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
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
