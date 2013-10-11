<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="UploadExamResults.aspx.cs" Inherits="Admin_UploadExamResults" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellspacing="3">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Upload File</td>
            <td>
                <asp:FileUpload ID="fupUpload" runat="server" />
            </td>
            <td>
                <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
                    Text="Upload" />
            </td>
            <td>
                                            <asp:HyperLink ID="hypExamTemplate" runat="server" 
                                                NavigateUrl="~/Admin/EntranceResult.xlsx">Download Entrance Exam Results Template</asp:HyperLink>
                                            </td>
        </tr>
    </table>
</asp:Content>

