<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UploadPins.aspx.cs" Inherits="Admin_UploadPins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick">
    </asp:Timer>
    <asp:Panel ID="PaymentTypePanel1" runat="server" HorizontalAlign="Left" Width="100%">
        <table style="border-color: Gray; border-style: solid">
            <tr>
                <td colspan="5">
                    <table>
                        <tr>
                            <td valign="middle" style="font-size: medium; color: Blue; font-style: italic;  border-right-color: Black;
                                border-right-style: solid">
                                Select Upload Options
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
                                    OnSelectedIndexChanged="RadioButtonList1_Changed" RepeatColumns="2" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Application Pins</asp:ListItem>
                                    <asp:ListItem>Acceptance Pins</asp:ListItem>
                                    <asp:ListItem>School Fees Pins</asp:ListItem>
                                    <asp:ListItem>Summer Pins</asp:ListItem>                                    
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="border-color:Black; border-style: solid; border-width: thick" >
                    <table>
                        <tr>
                            <td>
                                Mode Of Study:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListStudyMode" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Programme:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListProgramme" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Faculty Name:
                            </td>
                            <td nowrap="nowrap" colspan="3">
                                <asp:DropDownList ID="DDListFac2" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                        </tr>                        
                        
                        <tr>                            
                            <td>
                                Session
                            </td>
                            <td>
                                <asp:DropDownList ID="DDListSession" runat="server">
                                </asp:DropDownList>
                            </td>
                            
                            <td>
                                Payment type
                            </td>
                            <td>
                                <asp:DropDownList ID="DDListPaymentType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>                            
                            <td>
                                Fees Amount
                            </td>
                            <td>
                                
                                <asp:TextBox ID="TxtAmt" runat="server"></asp:TextBox>
                                
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <table>
                        <tr>
                            <td style="border-top-color: Green; border-top-style: solid; border-top-width: thick;
                                border-right-color: Black; border-right-style: solid">
                                Upload File:
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="position: relative; z-index: auto;
                                    overflow: auto; float: left; table-layout: auto">
                                    <tr>
                                        <td style="border-top-color: Green; border-top-style: solid; border-top-width: thick">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </td>
                                        <td style="border-top-color: Green; border-top-style: solid; border-top-width: thick">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/TestPins.xls">Download Pin Template</asp:HyperLink><br />                                                                                       
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    &nbsp;
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnUpload" runat="server" OnClick="Upload_Click" TabIndex="6" Text="Upload" />
                            </td>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" TabIndex="6"
                                    Text="Delete Upload Values" OnClientClick="return confirm('Are you sure of deleting this payitem from fixed upload?');" />
                            </td>
                            <td style="vertical-align: bottom">
                                &nbsp;
                            </td>
                            <td style="vertical-align: bottom">
                                <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="8" Text="Close" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblProgress" runat="server" Font-Bold="True"></span></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="PanelGrid" runat="server">
        <table>
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="BankGridv_PageIndexChanging"
                        PageSize="40">
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
    </asp:Panel>
</asp:Content>

