<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="CourseMat.aspx.cs" Inherits="CourseMat" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td colspan="2" bgcolor="#FFFBD6">
                <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" BackColor="#FFFBD6">
                </asp:Menu>
            </td>
        </tr>
        <tr align="left">
            <td bgcolor="#FFFBD6">
                <asp:Menu ID="Menu1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium"
                    Orientation="Horizontal" StaticSubMenuIndent="10px" BackColor="#FFFBD6" DynamicHorizontalOffset="2"
                    ForeColor="#990000">
                    <StaticSelectedStyle BackColor="#FFCC66" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                    <Items>
                        <asp:MenuItem NavigateUrl="~/ContentGallery.aspx" Text="&lt;&lt; Download Center"
                            Value="&lt;&lt; Download Center"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
            <td align="right" valign="bottom">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="border-style: solid; border-color: #FF0000;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Download and install Adobe Acrobat Reader if not already install in this system
                <asp:HyperLink ID="HyperLink3" runat="server" Font-Bold="True" Font-Italic="False"
                    ForeColor="Red" NavigateUrl="http://ardownload.adobe.com/pub/adobe/reader/win/9.x/9.2/enu/AdbeRdr920_en_US.exe">
                                                            Click Here
                </asp:HyperLink>
            </td>
        </tr>
         <tr>
            <td colspan="2">
                <hr style="border-style: solid; border-color: #FF0000;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlFileInfo" runat="server">
                    <div>
                        <p>
                            <asp:Label ID="lblName" runat="server"></asp:Label></p>
                    </div>
                    <div>
                        <p>
                            <asp:Label ID="lblCategory" runat="server"></asp:Label></p>
                    </div>
                    <div>
                        <p>
                            <asp:Label ID="lblContentID" runat="server"></asp:Label></p>
                    </div>
                    <div>
                        <p>
                            <asp:Label ID="lblDatePublished" runat="server"></asp:Label></p>
                    </div>
                    <div>
                        <p>
                            <asp:Label ID="lblNote" runat="server"></asp:Label></p>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="border-style: solid; border-color: #FF0000;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList1" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image1" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload1" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel2" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList2" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image2" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload2" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel3" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList3" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image3" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload3" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel4" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList4" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image4" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload4" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel5" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList5" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image5" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload5" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel6" runat="server" Font-Names="Calibri" Font-Size="10pt" GroupingText="Quick Details"
                    Visible="False">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <asp:BulletedList ID="BulletedList6" runat="server">
                                </asp:BulletedList>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Image ID="Image6" runat="server" Height="80px" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td width="60%">
                                <asp:LinkButton ID="btnDownload6" runat="server" OnClick="btnDownload1_Click">Download</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border-style: solid; border-color: #FF0000;" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
