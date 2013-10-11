<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="ViewNews.aspx.cs" Inherits="NewsItem_ViewNews" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="mvView" runat="server">
        <asp:View ID="vwBrowse" runat="server">
            <table class="style2">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdNews" runat="server" AllowPaging="True" 
                            AllowSorting="True" PageSize="100">
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:View>
          <asp:View ID="vwDetails" runat="server">
              <table align="center" cellspacing="3">
                  <tr>
                      <td align="right">
                          Title</td>
                      <td align="left">
                          <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                      </td>
                      <td align="left" rowspan="7" valign="top">
                          <asp:Image ID="imgNewsImage" runat="server" Width="22px" />
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
        </asp:View>
    </asp:MultiView>
</asp:Content>

