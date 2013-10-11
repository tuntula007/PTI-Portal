<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="AdmittedStudents.aspx.cs" Inherits="Admin_AdmittedStudents" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 20px;
            width: 148px;
        }
        .style4
        {
            height: 20px;
            width: 135px;
        }
        .style5
        {
            width: 135px;
        }
        .style6
        {
            width: 604px;
        }
        .style8
        {
            width: 148px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="90%">
        <table style="border-color: Gray; border-style: solid">
            <tr>
                <td colspan="3">
                    <table>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" 
                    style="border-color:Black; border-style: solid; border-width: thick" >
                    <table>
                        <tr>
                            <td class="style8">
                                Session</td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlAcademicSession" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="style6" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style8">
                                Mode of Study:</td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlModeOfStudy" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlModeOfStudy_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="style6" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Programme:</td>
                            <td nowrap="nowrap" colspan="2" class="style4">
                                <asp:DropDownList ID="ddlProgramme" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="style6" >
                                &nbsp;</td>
                            <tr>
                                <td class="style8">
                                    Faculty:
                                </td>
                                <td class="style5" colspan="2" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack="True" onselectedindexchanged="ddlFaculty_SelectedIndexChanged" 
                                        >
                                    </asp:DropDownList>
                                </td>
                                <td class="style6">
                                    &nbsp;</td>
                                <tr>
                                    <td class="style8">
                                        Course of Study:
                                    </td>
                                    <td class="style5" colspan="2" nowrap="nowrap">
                                        <asp:DropDownList ID="ddlCourseOfStudy" runat="server" AutoPostBack="True" 
                                            >
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style6">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style8">
                                        Entry Level:
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="ddlAcademicLevel" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style6" colspan="2">
                                        &nbsp;</td>
                                </tr>
                            </tr>
                            <tr>
                                <td class="style8">
                                    Reg Number</td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="txtRegNo" runat="server"></asp:TextBox>
                                </td>
                                <td class="style6" colspan="2">
                                    &nbsp;</td>
                        </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;</td>
                                <td nowrap="nowrap">
                                    <asp:Button ID="BtnShowStudents" runat="server" onclick="BtnShowStudents_Click" 
                                        Text="Display Students" Width="119px" />
                                </td>
                                <td class="style6" colspan="2">
                                    &nbsp;</td>
                            </tr>
                        <tr>
                            <td class="style8">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td class="style6" colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table class="style9">
        <tr>
            <td><asp:Label ID="lblMessage" runat="server" Style="position: static" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" Width="755px" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" PageSize="25" AllowPaging="True" AllowSorting="true"
            onrowcommand="GridView1_RowCommand" onrowdeleting="GridView1_RowDeleting" 
             DataKeyNames="RegNo" onpageindexchanging="GridView1_PageIndexChanging" >
        <RowStyle BackColor="White" ForeColor="#003399" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"RegNo") %>'
                        CommandName="Edit">Edit</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                  <ItemTemplate>
                    <asp:LinkButton runat="server" ID="DeleteButton" 
                      CommandName="delete"  
                      OnClientClick="if (!window.confirm('Are you sure you want to delete this item?')) return false;" >Delete</asp:LinkButton>
                  </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reg Number">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("RegNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Names">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Surname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course of Study">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CourseofStudy") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Programme">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Programme") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Faculty">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Faculty") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
            HorizontalAlign="Left" />
    </asp:GridView></td>
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
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    </asp:UpdatePanel>
</asp:Content>

