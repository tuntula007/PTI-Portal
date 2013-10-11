<%@ Page Language="C#" MasterPageFile="~/LoginMasterPage.master" AutoEventWireup="true"
    CodeFile="loginaccess.aspx.cs" Inherits="loginaccess" Title="Login Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        
        .innertext input[type='text'] 
        {
        	width:200px;
        	height:20px;
        	border:solid 1px black;
        	font-size:larger;
        	}
        	
         .innertext input[type='password'] 
        {
        	width:200px;
        	height:20px;
        	border:solid 1px black;
        	font-size:larger;
        	}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="1" cellspacing="1" class="style2" style="top: 400px">
        <tr valign="top" align="left">
            <td>
                <asp:Label ID="LabRelogin" runat="server" Text="" Visible="false">
                </asp:Label>
            </td>
        </tr>
        <tr align="left" valign="middle">
            <td>
                <table>
                    <tr valign="top" style="top:200">
                        <td>
                            <asp:Login ID="Login1" runat="server" CssClass="innertext" BackColor="#F7F7DE" 
                                BorderColor="#30587B" BorderStyle="Solid"
                                BorderWidth="2px" Font-Names="Verdana" Font-Size="Larger" OnAuthenticate="Login1_Authenticate"
                                Width="100%" RememberMeSet="False">
                                <TitleTextStyle BackColor="#30587B" Font-Bold="True" ForeColor="#FFFFFF" />
                            </asp:Login>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td>
                &nbsp;</td>
        </tr>        
    </table>
</asp:Content>
