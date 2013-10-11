<%@ Page Language="C#" MasterPageFile="~/Admission/AdmissionMasterPage.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeFile="Createuser.aspx.cs" Inherits="Createuser" Title="Create new Account" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
        <tr>
            <td valign="top">
                <table style="position: relative; z-index: auto; overflow: auto; float: left; table-layout: auto">
                    <%--<table border="0" cellspacing="0" cellpadding="0" class="middle-footer" style="width: 100%">--%>
                    <tr>
                        <td nowrap="nowrap">
                            Create Account
                        </td>
                        <td nowrap="nowrap">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            User ID
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Password
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Confirm Password
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Email Address
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtEmail" runat="server" Width="190px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Select User Group
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="cboUsergroup" runat="server" OnSelectedIndexChanged="cboUsergroup_Changed" Font-Names="Verdana" Font-Size="11px"
                                ForeColor="#0099FF" Height="21px" Width="197px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Faculty:
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="DDListFaculty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDListFaculty_Changed">
                            </asp:DropDownList>
                        </td>
                        <td nowrap="nowrap">
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Department:
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="DDListDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDListDept_Changed">
                            </asp:DropDownList>
                        </td>
                        <td nowrap="nowrap">
                        </td>
                    </tr>
                    <tr>
                            <td>
                                Programme of Study:
                            </td>
                            <td nowrap="nowrap" colspan="3">
                                <asp:DropDownList ID="DDListCourseOfStudy" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                        </tr>
                    <tr>
                        <td nowrap="nowrap">
                            &nbsp;
                        </td>
                        <td nowrap="nowrap">
                            <asp:Button ID="btnCreateaccount" runat="server" Height="25px" OnClick="btnCreateaccount_Click"
                                Text="Create Account" Width="110px" BackColor="#666666" ForeColor="#FFFF99" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                        </td>
                        <td nowrap="nowrap">
                        </td>
                        <td nowrap="nowrap">
                            &nbsp;
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                <asp:Panel ID="Panel6" runat="server" GroupingText="Search for editable User" BorderStyle="None">
                    <table style="position: relative; z-index: 0; border-style: solid">
                        <tr>
                            <td nowrap="nowrap">
                                Value
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="TxtSearchstaff" runat="server" Width="300px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <asp:Button ID="BtnSearchEmplyee" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                            </td>
                        </tr>
                        <tr>
                                                                            <td>
                                                                                <span><span>&#160;&#160;</span></span><span><span> </span></span>
                                                                            </td>
                                                                            <td>
                                                                                <span><span>&#160;&#160;</span></span><span><span> </span></span>
                                                                            </td>
                                                                            <td>
                                                                                <span><span>&#160;&#160;</span></span><span><span> </span></span>
                                                                            </td>
                                                                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 39%; position: relative; overflow: auto;
                    float: left; top: 0px; left: 0px; height: 20px;">
                    <tr>
                        <td nowrap="nowrap">
                            <asp:LinkButton ID="BtnExport" runat="server" OnClick="BtnExport_Click" TabIndex="9"
                                Text="Export" />
                        </td>
                        <td nowrap="nowrap">
                            &nbsp;
                        </td>
                        <td nowrap="nowrap">
                            &nbsp;
                        </td>
                        <td style="width: 52%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr style="background-color: #c40000; height: 1px; border-top-color: Red; border-style: solid" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelGrid" runat="server" GroupingText="Info" HorizontalAlign="Left"
                    Width="1000px" ScrollBars="Both">
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" PageSize="40" AutoGenerateDeleteButton="True"
                        AutoGenerateEditButton="True" 
                        OnRowEditing="grdViewStatustory_OnRowEditing" OnRowUpdating="grdViewStatustory_OnRowUpdating"
                        OnRowCancelingEdit="grdViewStatustory_OnRowCancelingEdit" OnRowDeleting="grdViewStatustory_OnRowDeleting"
                        Font-Size="Small" ForeColor="Black" GridLines="Vertical" 
                        HorizontalAlign="Left" AutoGenerateColumns="False">
                        <PagerSettings Position="TopAndBottom" />
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" />
                            <asp:BoundField DataField="User Name" HeaderText="User Name" />
                            <asp:TemplateField HeaderText="User Group">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("[User Group]") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" 
                                        DataSourceID="ObjectDataSource1" DataTextField="Usergroup" 
                                        DataValueField="Usergroup">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                        SelectMethod="GetGroups" TypeName="FacultyBusiness"></asp:ObjectDataSource>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Mail" HeaderText="Mail" />
                        </Columns>
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
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>
