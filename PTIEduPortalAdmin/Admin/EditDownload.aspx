<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="EditDownload.aspx.cs" Inherits="EditDownload" Title="Edit Downloads Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            height: 514px;
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
                            DataKeyNames="DownloadId" DataSourceID="drscNews" ForeColor="#333333" 
                            PageSize="50" onselectedindexchanged="grdNews_SelectedIndexChanged">
                            <RowStyle BackColor="#E3EAEB" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField DataField="DownloadId" HeaderText="DownloadId" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="DownloadId" />
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                                    SortExpression="Active" />
                                <asp:BoundField DataField="UploadedBy" HeaderText="UploadedBy" 
                                    SortExpression="UploadedBy" />
                                <asp:BoundField DataField="UploadedOn" HeaderText="UploadedOn" 
                                    SortExpression="UploadedOn" />
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                No Downloads Available
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="drscNews" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>" 
                            SelectCommand="SELECT * FROM [Downloads]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            
        </asp:View>
         <asp:View ID ="vwDetails" runat="server">
            <table align="center" >
        <tr>
            <td>
                <asp:LinkButton ID="linkBack" runat="server" onclick="linkBack_Click">Back To Downloads List</asp:LinkButton>
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
            <td align="left" rowspan="6" valign="top">
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
                Type</td>
            <td align="left">
                <asp:DropDownList ID="cbType" runat="server" DataSourceID="drscDownloadType" 
                    DataTextField="Type" DataValueField="TypeId">
                </asp:DropDownList>
                <asp:SqlDataSource ID="drscDownloadType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>" 
                    SelectCommand="SELECT * FROM [DownloadTypes]"></asp:SqlDataSource>
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
                File</td>
            <td align="left">
                <asp:FileUpload ID="fupImage" runat="server" />
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
            <td class="style3">
                </td>
            <td class="style3">
                </td>
            <td class="style3">
                </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
        </asp:View>
    </asp:MultiView>
    
    
</asp:Content>

