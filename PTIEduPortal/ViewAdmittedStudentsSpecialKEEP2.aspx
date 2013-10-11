<%@ Page Language="C#" MasterPageFile="~/MasterPageApplicant.master" AutoEventWireup="true"
    CodeFile="ViewAdmittedStudentsSpecial.aspx.cs" Inherits="ViewAdmittedStudentsSpecial"
    Title="Checking Admission Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style5
        {
            color: #333399;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelParam" runat="server" Font-Names="Tahoma" Font-Size="Small" Style="text-align: left">
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#000066" Style="font-size: small"
                        
                        Text="Newly admitted students can print their notification letter here.<br><u><b>STEPS:</b></u><ul><li>Enter your Application Form Number Or Enter your Full Name (i.e.)Surname/Firstname/Middle Name in the field provided.</li><li>If you have been admitted, you will be able to print your Admission Notification Letter.</li><li>Instructions on the notification letter will guide you to the next line of action to be taken by you.</li></ul>" 
                        Visible="False"></asp:Label>
                    <asp:ObjectDataSource ID="ObjGrid" runat="server" SelectMethod="GetAdmittedStudentsByFormNo"
                        TypeName="CybSoft.EduPortal.Business.ApplicantsBusiness">
                        <FilterParameters>
                            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="newparameter" PropertyName="Text" />
                        </FilterParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtSearch" Name="Param" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="PanelGrid" runat="server" Font-Names="Tahoma" Font-Size="Small" Height="1147px"
                        Style="text-align: left" Width="90%">
                        <table id="SimpleSearch" runat="server">
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" ForeColor="#333399" Text=" ADMISSION INFORMATION  :"
                                        Style="font-weight: 700"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSearch" runat="server" Width="305px" Enabled="False"></asp:TextBox>
                                    &nbsp;<asp:Button ID="BtnSearch" runat="server" Text="AdmissionStatus" 
                                        Width="112px" OnClick="BtnSearch_Click" Visible="False" />
                                    <br />
                                    <asp:LinkButton ID="lkbtnAdvanceSearch" runat="server" Text="Advance Search" 
                                        OnClick="lkbtnAdvanceSearch_Click" Visible="False"></asp:LinkButton>
                                    
                                    <asp:LinkButton ID="LinkBtnAdmision" runat="server" 
                                        Text="PRINT ADMISSION LETTER" OnClick="LnkShoww_Click" Visible="False"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table id="AdvanceSearch" runat="server" visible="false">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label3" runat="server" ForeColor="#333399" Text="If you have Forgotten your Application Form Number <br/>Enter your Full Name, Email or Phone Number and click on Search Button below:"
                                        Style="font-weight: 700"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" ForeColor="#333399" Text="Surname/Family Name/Last Name:"
                                        Style="font-weight: 700"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="SurnameText" runat="server" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" ForeColor="#333399" Text="First Name/Given Name:"
                                        Style="font-weight: 700"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="SecondNameText" runat="server" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" ForeColor="#333399" Text="Email Address:" Style="font-weight: 700"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailText" runat="server" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" ForeColor="#333399" Text="Phone Number:" Style="font-weight: 700"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="PhoneText" runat="server" Width="305px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="BtnSearchBiodata" runat="server" Text="Search" Width="112px" OnClick="BtnSearchBiodata_Click" /><asp:LinkButton
                                        ID="lkbtnSimpleSearch" runat="server" Text="Simple Search" OnClick="lkbtnSimpleSearch_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" DataSourceID="ObjGrid" Font-Names="Tahoma" Font-Size="Small"
                            ForeColor="#333333" GridLines="None" Height="10px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
                            PageSize="50" Style="font-size: x-small" Width="100%">
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="S/N">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex  + 1  %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Form No">
                                    <ItemTemplate>
                                            <asp:LinkButton ID="LnkShoww" runat="server" CausesValidation="False" CommandName="Select"
                                            OnClick="LnkShoww_Click" Text="Print Your Admission Letter" ToolTip="YOU MUST HAVE PAID FOR A =N=1000 ADMISSION LETTER PIN AT THE BANK BEFORE YOU CAN PRINT YOUR ADMISSION LETTER"></asp:LinkButton>
                                        <%-- 
                                         <asp:LinkButton ID="LnkShoww" runat="server" CausesValidation="False" CommandName="Select"
                                             Text='<%# Bind("ApplicationNo") %>'></asp:LinkButton>
                                          --%>  
                                        <asp:HiddenField ID="IsAdmitted" runat="server" Value='<%# Eval("IsAdmitted") %>' />
                                        <asp:HiddenField ID="AdmittedStatus" runat="server" Value='<%# Eval("AdmittedStatus") %>' />
                                        <asp:HiddenField ID="Remarks" runat="server" Value='<%# Eval("Remarks") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Surname" HeaderText="Applicants Name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                
                                 <asp:TemplateField HeaderText="Sch Fees">
                                    <ItemTemplate>
                                            <asp:LinkButton ID="LnkShowFees" runat="server" CausesValidation="False" CommandName="Select"
                                            OnClick="LnkShowFees_Click" Text="Download Fees"></asp:LinkButton>
                                     </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                
                                
                                
                                
                               <%-- <asp:BoundField DataField="Sex" HeaderText="Sex">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Department" HeaderText="Department">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CourseOfStudy" HeaderText="Programme of Study">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView> 
                        <br />
                        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="#FF0066" 
                            Text="Label" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="LblStud" runat="server" Font-Bold="True" ForeColor="#000066" 
                            Text="Label" Visible="False"></asp:Label>
                       <br />
           
                    </asp:Panel>
                </td>
            </tr>
           
        </table>
    </asp:Panel>
</asp:Content>
