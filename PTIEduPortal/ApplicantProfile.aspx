<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageApplicant.master" AutoEventWireup="true" CodeFile="ApplicantProfile.aspx.cs" Inherits="ApplicantProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  
   
   
    <div style =" text-align : left; padding-left  : 45px;">
    <label id="lblVoln" runat ="server" ></label>
 
        <table class="style1">
            <tr>
                <td>
                    <label for ="Title">Surname</label></td>
                <td>
        <asp:TextBox ID="txtSurname" runat ="server" Width ="250px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Other Names</td>
                <td>
        <asp:TextBox ID="txtOthernames" runat ="server" Width ="250px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label for ="Title">Email Address</label> 
                </td>
                <td>
        <asp:TextBox ID="txtEmailAdd" runat ="server" Width ="250px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label for ="Title">State</label> 
                </td>
                <td>
        <asp:DropDownList ID="ddlState" runat ="server" AutoPostBack="True" 
            onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                                <asp:ListItem>ABIA</asp:ListItem>
                                                                <asp:ListItem>ADAMAWA</asp:ListItem>
                                                                <asp:ListItem>ANAMBRA</asp:ListItem>
                                                                <asp:ListItem>AKWA IBOM</asp:ListItem>
                                                                <asp:ListItem>BAUCHI</asp:ListItem>
                                                                <asp:ListItem>BAYELSA</asp:ListItem>
                                                                <asp:ListItem>BENUE</asp:ListItem>
                                                                <asp:ListItem>BORNO</asp:ListItem>
                                                                <asp:ListItem>CROSS RIVER</asp:ListItem>
                                                                <asp:ListItem>DELTA</asp:ListItem>
                                                                <asp:ListItem>EBONYI</asp:ListItem>
                                                                <asp:ListItem>EDO</asp:ListItem>
                                                                <asp:ListItem>EKITI</asp:ListItem>
                                                                <asp:ListItem>ENUGU</asp:ListItem>
                                                                <asp:ListItem>GOMBE</asp:ListItem>
                                                                <asp:ListItem>IMO</asp:ListItem>
                                                                <asp:ListItem>JIGAWA</asp:ListItem>
                                                                <asp:ListItem>KADUNA</asp:ListItem>
                                                                <asp:ListItem>KANO</asp:ListItem>
                                                                <asp:ListItem>KATSINA</asp:ListItem>
                                                                <asp:ListItem>KEBBI</asp:ListItem>
                                                                <asp:ListItem Selected="True">KOGI</asp:ListItem>
                                                                <asp:ListItem>KWARA</asp:ListItem>
                                                                <asp:ListItem>LAGOS</asp:ListItem>
                                                                <asp:ListItem>NASSARAWA</asp:ListItem>
                                                                <asp:ListItem>NIGER</asp:ListItem>
                                                                <asp:ListItem>OGUN</asp:ListItem>
                                                                <asp:ListItem>ONDO</asp:ListItem>
                                                                <asp:ListItem>OSUN</asp:ListItem>
                                                                <asp:ListItem>OYO</asp:ListItem>
                                                                <asp:ListItem>PLATEAU</asp:ListItem>
                                                                <asp:ListItem>RIVERS</asp:ListItem>
                                                                <asp:ListItem>SOKOTO</asp:ListItem>
                                                                <asp:ListItem>TARABA</asp:ListItem>
                                                                <asp:ListItem>YOBE</asp:ListItem>
                                                                <asp:ListItem>ZAMFARA</asp:ListItem>
                                                                <asp:ListItem>ABUJA</asp:ListItem>        
        </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label for ="Title">Local Government Area</label> 
                </td>
                <td>
        <asp:DropDownList ID="ddlLga" runat ="server"></asp:DropDownList>
                </td>
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
                <td>
                    &nbsp;</td>
            </tr>
        </table>
<br /> 
   
      
        <hr /> 
    <span><label id="lblMess" runat ="server" ></label> </span>
        
        <p><asp:Button ID="btnCancel" runat ="server" Text ="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      
        <asp:Button ID="btnSubmit" runat ="server" Text ="Submit" 
                onclick="btnSubmit_Click" /></p>                
</div>
</asp:Content>

