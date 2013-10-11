<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="ApplicantCredentials.aspx.cs" Inherits="ApplicantCredentials" Title="  Online ApplicantCredentials" %>

<script runat="server">

    
</script>

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
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Add Department" ID="TabPanel1">
                                
                                
                                <HeaderTemplate>
                                     Edit Active Application Records
                                    </HeaderTemplate>
                                    
                                    
                                <ContentTemplate>
                                    <asp:Panel ID="PaymentTypePanel1" runat="server" HorizontalAlign="Left"
                                        Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto">
                                                        <tr>
                                                            <td colspan="2">
                                                                School:
                                                            </td>
                                                            <td>                                                           
                                                                
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDListSchools" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td rowspan="4">
                                                                &nbsp;</td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td colspan="2">
                                                                Applicant Form No
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtDept" runat="server" SkinID="mediumTxt"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click" 
                                                                    style="text-align: left" TabIndex="9" Text="VIEW RECORD"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span class="style2">:</span>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtDeptCode" runat="server" SkinID="mediumTxt" TabIndex="1" 
                                                                    Visible="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" class="style3">
                                                                &nbsp;</td>
                                                            <td class="style3">
                                                                &nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHod" runat="server" SkinID="mediumTxt" TabIndex="2" 
                                                                    Visible="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" OnClientClick="return confirm('Be sure that your inputs are correct?');"
                                                                    TabIndex="3" Text="Submit" Visible="False" />
                                                            </td>
                                                            <td style="vertical-align: bottom">
                                                                <asp:Button ID="BtnClose" runat="server" OnClick="BtnClose_Click" TabIndex="4" Text="Close" />
                                                            </td>
                                                            <td style="vertical-align: bottom">
                                                                &nbsp;</td>
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
                                                Text="Export" Visible="False" />
                                        </td>
                                        <td align="left">
                                            &nbsp;</td>
                                        <td align="left">
                                            <asp:LinkButton ID="LnkBtnPrin" runat="server" OnClick="BtnPrintGrid_Click" TabIndex="9"
                                                Text="Print" Visible="False" />
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
                                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="2" AutoGenerateDeleteButton="false"
                                                 
                                                OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                                                OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" 
                                                OnRowDeleting="grdViewStatustory_OnRowDeleting" 
                                                Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                                                HorizontalAlign="Left">
                                                <PagerSettings Position="TopAndBottom" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
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
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style2
        {
            color: #FFFFFF;
        }
        .style3
        {
            color: #FFFF99;
        }
        </style>

</asp:Content>

