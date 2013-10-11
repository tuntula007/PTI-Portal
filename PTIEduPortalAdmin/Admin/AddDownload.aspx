<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="AddDownload.aspx.cs" Inherits="AddDownload" Title="Add News Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" cellspacing="3">
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                &nbsp;</td>
            <td align="left" rowspan="6" valign="top">
                &nbsp;</td>
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
                <asp:Button ID="cmdSave" runat="server" onclick="cmdSave_Click" Text="Save" />
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
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
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
</asp:Content>

