<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="EditNews.aspx.cs" Inherits="EditNews" Title="Edit News Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView runat="server"  ID="mvView" ActiveViewIndex="0">
        <asp:View runat="server" ID ="vwBrowse">
            
            <table class="style2">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdNews" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                            DataKeyNames="NewsId" DataSourceID="drscNews" ForeColor="#333333" 
                            PageSize="50" onselectedindexchanged="grdNews_SelectedIndexChanged">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="NewsId" HeaderText="NewsId" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="NewsId" />
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                <asp:BoundField DataField="Caption" HeaderText="Caption" 
                                    SortExpression="Caption" />
                                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                <asp:BoundField DataField="ImagePath" HeaderText="ImagePath" 
                                    SortExpression="ImagePath" />
                                <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                                    SortExpression="Active" />
                                <asp:BoundField DataField="UploadedBy" HeaderText="UploadedBy" 
                                    SortExpression="UploadedBy" />
                                <asp:BoundField DataField="UploadedOn" HeaderText="UploadedOn" 
                                    SortExpression="UploadedOn" />
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="drscNews" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>" 
                            SelectCommand="SELECT * FROM [NewsItems]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            
        </asp:View>
         <asp:View ID ="vwDetails" runat="server">
            <table align="center" >
        <tr>
            <td>
                <asp:LinkButton ID="linkBack" runat="server" onclick="linkBack_Click">Back To News List</asp:LinkButton>
            </td>
        </tr>
         <tr>
            <td>
                <table align="center" cellspacing="3">
        <tr>
            <td align="right">
                <asp:HiddenField ID="hdfNewsId" runat="server" />
            </td>
            <td align="left">
                &nbsp;</td>
            <td align="left" rowspan="8" valign="top">
                <asp:Image ID="imgNewsImage" runat="server" Width="22px" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Title</td>
            <td align="left">
                <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Caption</td>
            <td align="left">
                <asp:TextBox ID="txtCaption" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Type</td>
            <td align="left">
                <asp:DropDownList ID="cbType" runat="server">
                    <asp:ListItem>Home Page News</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Active</td>
            <td align="left">
                <asp:CheckBox ID="ckActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Image</td>
            <td align="left">
                <asp:FileUpload ID="fupImage" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Body</td>
            <td align="left">
                <asp:TextBox ID="txtBody" runat="server" Height="150px" TextMode="MultiLine" 
                    Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="cmdSave" runat="server" onclick="cmdSave_Click" Text="Edit" 
                    Width="100px" />
                &nbsp;<asp:Button ID="cmdDelete" runat="server" onclick="cmdDelete_Click" 
                    Text="Delete" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
        </asp:View>
    </asp:MultiView>
    
    
</asp:Content>

