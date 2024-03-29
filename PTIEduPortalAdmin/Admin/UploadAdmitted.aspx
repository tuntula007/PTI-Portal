﻿<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="UploadAdmitted.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Admin_UploadAdmitted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            height: 28px;
        }
        .style3
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Timer ID="Timer1" runat="server" Interval="7000" OnTick="Timer1_Tick">
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
                                    <asp:ListItem>Admitted Students</asp:ListItem>
                                    <asp:ListItem>Admitted Students(Migrated)</asp:ListItem>
                                    <asp:ListItem>Old Students(New to Portal)</asp:ListItem>
                                    <asp:ListItem>Old Students(Promoted To New Level)</asp:ListItem>
                                    <asp:ListItem>Graduated Students</asp:ListItem>
                                    <asp:ListItem>Carry Over Courses</asp:ListItem>
                                    <asp:ListItem Value="Spill Over Student Courses">Spill Over Student Courses</asp:ListItem>
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
                                Faculty:
                            </td>
                            <td nowrap="nowrap" colspan="3">
                                <asp:DropDownList ID="DDListFac2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListFac2_Changed">
                                </asp:DropDownList>
                            </td>
                            <td>
                        </tr>
                        <tr>
                            <td>
                                Department:
                            </td>
                            <td nowrap="nowrap" colspan="3">
                                <asp:DropDownList ID="DDListDept2" runat="server" OnSelectedIndexChanged="DDListDept2_Changed"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                        </tr>
                        <tr>
                            <td>
                                Programme of Study:
                            </td>
                            <td nowrap="nowrap" colspan="3">
                                <asp:DropDownList ID="DDListCourseOfStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListCourseOfStudy_Changed">
                                </asp:DropDownList>
                            </td>
                            <td>
                        </tr>
                        <tr>
                            <td>
                                Batch:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListBatch" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Entry Mode:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListEntryMode" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Entry Level:
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListLevel2" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Session
                            </td>
                            <td>
                                <asp:DropDownList ID="DDListSession" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                               
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="DDListSemester" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Refresh Info</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Honor
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="TxtHons" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            Course Duration
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="TxtDuration" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
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
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/AdmissionListsample.xls">Download Admtted List Template</asp:HyperLink><br />
                                            <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Admin/UIAdmissionListMigrate2.xls">Download Admtted List(Migrated) Template</asp:HyperLink><br />
                                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/OldStudentNewPortal.xls">Download Old Students(New to portal) Template</asp:HyperLink><br />
                                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/OldStudentPromoted.xls">Download Old Students(Promoted to new Level) Template</asp:HyperLink><br />
                                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/OldStudentPromoted.xls">Download Graduated Students Template</asp:HyperLink><br />
                                            <%--<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Admin/MatriculationSample.xls">Download Matric Number Template</asp:HyperLink><br />
                                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Admin/SummerScholarship.xls">Download Summer Scholarship Students Template</asp:HyperLink><br />--%>
                                            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Admin/Carryoversample.xls">Download Carry over Template</asp:HyperLink>                                           
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
