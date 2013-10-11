<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HomePageNews.aspx.cs" Inherits="HomePageNews" Title="campus News" %>

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
      <asp:Repeater ID="rptNews" runat="server" DataSourceID="drscNews"
       >
        <HeaderTemplate>
            <ul class="alert-success lead" >
               &nbsp;  Campus News !!!
                 </ul>
            <ul>
        </HeaderTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
        <ItemTemplate>
              <h2 >
                 <%# Eval("Title") %>
                 </h2>
            <li>
                <%# Eval("Body") %>
               
            </li>
        </ItemTemplate>
    </asp:Repeater>
   
    <asp:SqlDataSource ID="drscNews" runat="server" ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>"
        SelectCommand="SELECT * FROM [NewsItems] WHERE ([NewsId] = @NewsId)">
        <SelectParameters>
            <asp:QueryStringParameter Name="NewsId" QueryStringField="NewsId" 
                Type="Int32" />
        </SelectParameters>
      </asp:SqlDataSource>
</div>
  
</asp:Content>
