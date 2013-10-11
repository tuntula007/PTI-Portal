<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true" CodeFile="ViewAdmittedStudents.aspx.cs" Inherits="ViewAdmittedStudents" Title="View Admission List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 112px;
        }
        .style3
        {
            height: 37px;
        }
        .style5
        {
            font-size: x-large;
            color: #000066;
            text-decoration: underline;
        }
        .style6
        {
            width: 112px;
            font-weight: bold;
            color:Black ;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="PanelParam" runat="server" Font-Names="Tahoma" Font-Size="Small" 
        style="text-align: left">
        <table class="style1">
            <tr>
                <td colspan="2" height="8">
                    </td>
            </tr>
            <tr>
                <td class="style5" colspan="2">
                    <b>2012/2013 Provisional Admission List <br /></b></td>
            </tr>
            <tr>
                <td class="style3" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#000066" 
                        
                        Text="To view admission list, select the Programme, Course of Study and Mode of Study of your application, then click &quot;View Admitted Student&quot; Button. &lt;br&gt; This list contains  Full Admission List, kindly check for your name among the list. &lt;br&gt; If your name is found in the list, take note of your Reg No. &lt;br&gt;"></asp:Label>
                    <!--
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="True" 
                        Font-Italic="False" ForeColor="Red" NavigateUrl="~/AdmissionLetterParam.aspx"> 
                    &nbsp;Click Here</asp:HyperLink>
                    -->
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    Click
                    <asp:LinkButton ID="lnkHere" runat="server" onclick="lnkHere_Click">Here</asp:LinkButton>
                    &nbsp;for PreDegree Procedure</td>
            </tr>
            <tr>
                <td class="style6">
                    Session</td>
                <td>
                    <asp:DropDownList ID="CmbSession" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="CmbProgramme_SelectedIndexChanged">
                        
                        <asp:ListItem Selected ="True" >2012/2013</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Mode of Study:</td>
                <td>
                    <asp:DropDownList ID="CmbMode" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="CmbMode_SelectedIndexChanged">
                        <asp:ListItem>Full-Time</asp:ListItem>
                        <asp:ListItem>Part-Time</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Programme:</td>
                <td>
                    <asp:DropDownList ID="CmbProgramme" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="CmbProgramme_SelectedIndexChanged">
                        <asp:ListItem Selected="True"></asp:ListItem>
                        <asp:ListItem>CERTIFICATE</asp:ListItem>
                        <asp:ListItem>HND</asp:ListItem>
                        <asp:ListItem>ND</asp:ListItem>
                        <asp:ListItem>PRE-ND</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Course Of Study:</td>
                <td>
                    <asp:DropDownList ID="CmbCourse" runat="server" DataSourceID="ObjCourses" 
                        DataTextField="CourseOfStudy" DataValueField="CourseOfStudy">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="BtnViewAdmitted" runat="server" Text="View Admitted Student" 
                        onclick="BtnViewAdmitted_Click" Width="177px" />
                    <asp:ObjectDataSource ID="ObjCourses" runat="server" 
                        SelectMethod="getAdmittedCoursesByProg" 
                        TypeName="CybSoft.EduPortal.Business.ApplicantsBusiness">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="CmbProgramme" Name="Programme" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjGrid" runat="server" 
                        FilterExpression="[surname] like '%{0}%' or [othernames] like '%{0}%'">
                        <FilterParameters>
                            <asp:ControlParameter ControlID="txtSearch" DefaultValue="" Name="newparameter" 
                                PropertyName="Text" />
                        </FilterParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td style="text-align: right">
                    <!--
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
                        NavigateUrl="~/AdmissionLetterParam.aspx">Return to Admission Letter 
                    Printing</asp:HyperLink>
                    -->
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="PanelGrid" runat="server" Font-Names="Tahoma" Font-Size="Small" 
                        Height="2530px" style="text-align: left" Visible="False" Width="600px">
                        <asp:Label ID="Label2" runat="server" ForeColor="#333399" 
                            Text="To search for your name among the list, click Seach button below"></asp:Label>
                        <br />
                        <asp:Label ID="lbl" runat="server">Search For Your Name Among The List Below:</asp:Label>
                        <asp:TextBox ID="txtSearch" runat="server" Width="186px"></asp:TextBox>
                        &nbsp;<asp:Button ID="Button1" runat="server" Text="Search" 
                             />
                        <br />
                        <br />
                        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="#FF0066" 
                            Text="Label" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="LblStud" runat="server" Font-Bold="True" ForeColor="#FF0066" 
                            Text="Label" Visible="False"></asp:Label>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="ObjGrid" Height="109px" PageSize="100" 
                            Width="550px" AllowPaging="True" CellPadding="4" ForeColor="#333333" 
                            GridLines="None" onselectedindexchanged="GridView2_SelectedIndexChanged">
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <Columns>
                            <asp:TemplateField HeaderText="Srn">
                             <ItemTemplate >
                             <%# Container.DataItemIndex  + 1  %>
                             </ItemTemplate>
                             </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reg No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkShoww" runat="server" CausesValidation="False" 
                                            CommandName="Select" onclick="LnkShoww_Click" Text='<%# Bind("RegNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Surname" HeaderText="Applicants Name">
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
                    </asp:Panel>
                </td>
            </tr>
	<tr><td colspan="2">

<script type="text/javascript"><!--
google_ad_client = "ca-pub-9852760838257514";
/* nplAdd1 */
google_ad_slot = "9600945965";
google_ad_width = 728;
google_ad_height = 90;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
 </td></tr>
        </table>
    </asp:Panel>
                </asp:Content>

