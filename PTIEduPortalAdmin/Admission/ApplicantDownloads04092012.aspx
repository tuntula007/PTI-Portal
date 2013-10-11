<%@ Page Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master" AutoEventWireup="true"
    CodeFile="ApplicantDownloads.aspx.cs" Inherits="Admin_ApplicantDownloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
    .style2
    {
        width: 719px;
    }
</style>
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
                                    <asp:Panel ID="PaymentTypePanel1" runat="server" GroupingText="DownLoads" HorizontalAlign="Left"
                                        Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto; top: 0px; left: 0px; width: 906px;">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            Session
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="DDListSession" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            &nbsp;</td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListModeStudy" runat="server" AutoPostBack="True" 
                                                                                OnSelectedIndexChanged="DDListModeStudy_Changed" Visible="False">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            Student Type:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListType_Changed">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
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
                                                                        <td colspan="2">
                                                                            Entry Mode:
                                                                        </td>
                                                                        <td nowrap="nowrap">
                                                                            <asp:DropDownList ID="DDListEntryMode" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
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
                                                                        <td colspan="2">
                                                                            Start Date:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TxtStartDate" runat="server" SkinID="mediumTxt" TabIndex="1" AutoPostBack="True"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            End Date:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="TxtEndDate" runat="server" SkinID="mediumTxt" TabIndex="2"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <table>
                                                                    <tr valign="top">
                                                                        <td class="style2">
                                                                            Programme of Studies
                                                                            <asp:Panel ID="Panel10" runat="server" Height="359px" Width="762px" BorderStyle="Solid"
                                                                                ScrollBars="Both">
                                                                                <asp:RadioButtonList ID="ChkboxShareType2" runat="server" Font-Bold="True">
                                                                                </asp:RadioButtonList>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td nowrap="nowrap" valign="middle">
                                                                            <table cellspacing="3">
                                                                                <tr>
                                                                                    <td style="text-align: left">
                                                                                        &nbsp;
                                                                                        <asp:Button ID="btnprintExamcard" runat="server" 
                                                                                            onclick="btnprintExamcard_Click" Text="...." 
                                                                                            ToolTip="proceed to print exam cards" Width="92px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: bottom; text-align: right;">
                                                                                        <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="return confirm('Be sure that your inputs are correct?');"
                                                                                            TabIndex="3" Text="Display Data" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="vertical-align: bottom; text-align: center;">
                                                                                        <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="4" 
                                                                                            Text="Close" Width="49px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtStartDate"
                                                                    Format="yyyy-MM-dd" Enabled="True">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtEndDate"
                                                                    Format="yyyy-MM-dd" Enabled="True">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Edit Faculty" ID="TabPanel2">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ContentTemplate>
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
                                        <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left" Width="1000px" ScrollBars="Both">
                                            
                                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="50" AutoGenerateDeleteButton="False"
                                                Font-Size="Small" ForeColor="Black" GridLines="Vertical" AutoGenerateEditButton="False">
                                                <PagerSettings Position="TopAndBottom" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
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
