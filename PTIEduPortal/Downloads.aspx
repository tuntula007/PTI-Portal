<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Downloads.aspx.cs" Inherits="Downloads" Title="Downloads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .topfont
        {
            padding-top: 0px;
            font-family: tahoma;
            font-size: 12px;
            color: #060;
            font-weight: normal;
        }
        .redfont
        {
            padding-top: 0px;
            font-family: tahoma;
            font-size: 12px;
            color: #F00;
            font-weight: normal;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="span12">
      <asp:Repeater ID="rptDownloadTypes" runat="server" DataSourceID="drscDownloadTypes"
        OnItemDataBound="rptDownloadTypes_ItemDataBound">
        <HeaderTemplate>
            <h2 >
                Download List</h2>
            <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
        <ItemTemplate>
            <li><b>
                <%# Eval("Type") %></b>
                <asp:HiddenField ID="hdfTypeId" Value='<%#  Eval("TypeId") %>' runat="server" />
                <asp:Repeater ID="rptDownloads" runat="server" DataSourceID="drscDownloads">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                    <ItemTemplate>
                        <li><a href='<%#  Eval("DocPath") %>'><b>
                            <%# Eval("Title") %>
                        </b></a></li>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="drscDownloads" runat="server" ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>"
                    SelectCommand="SELECT * FROM [Downloads] WHERE ([TypeId] = @TypeId)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hdfTypeId" Name="TypeId" PropertyName="Value" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="hdfDownloadTypeId" runat="server" />
    <asp:SqlDataSource ID="drscDownloadTypes" runat="server" ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>"
        SelectCommand="SELECT * FROM [DownloadTypes]"></asp:SqlDataSource>
</div>
  
</asp:Content>
