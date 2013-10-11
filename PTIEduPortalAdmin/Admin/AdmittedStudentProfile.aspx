<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="AdmittedStudentProfile.aspx.cs" Inherits="Admin_AdmittedStudentProfile" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style =" text-align : left; padding-left  : 45px;">
    <label id="lblVoln" runat ="server" ></label>
    <table class="style9">
         <tr>
             <td style="width: 163px">
                 &nbsp;</td>
             <td colspan="4">
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 &nbsp;</td>
             <td colspan="4">
                 <asp:Label ID="lblMessage" runat="server"></asp:Label>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 <label for ="Title" class ="fieldinput">Surname</label> 
             </td>
             <td colspan="4">
        <asp:TextBox ID="txtSurname" runat ="server" Width ="250px"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Other Names</label> 
             </td>
             <td colspan="4">
        <asp:TextBox ID="txtOtherNames" runat ="server" Width ="250px"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Programme</label> 
             </td>
             <td colspan="4">
        <asp:DropDownList ID="ddlProgramme" runat ="server"> 
        </asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px" class ="fieldinput">Course of Study</td>
             <td colspan="4">
        <asp:DropDownList ID ="ddlCourseOfStudy" runat ="server">
        </asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Academic Level</label> 
             </td>
             <td colspan="4">
        <asp:DropDownList ID="ddlAcademicLevel" runat ="server"></asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
    <p class ="fieldinput">Date of Birth</p> 
             </td>
             <td colspan="4">
                 <asp:DropDownList ID="ddlDay" runat="server">
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlMonth" runat="server">
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlYear" runat="server">
                 </asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Faculty</label> 
             </td>
             <td colspan="4">
        <asp:DropDownList ID="ddlFaculty" runat ="server" >     
        </asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>        
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Mode Of Study</label> 
             </td>
             <td colspan="4">
        <asp:DropDownList ID="ddlModeOfStudy" runat ="server" >     
        </asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         
         <tr>
             <td style="width: 163px">
                 <label for ="Title">Academic Session</label>  
             </td>
             <td colspan="4">
        <asp:DropDownList ID="ddlAcademicSession" runat ="server"></asp:DropDownList>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         
         <tr>
             <td style="width: 163px">
                 Email Address</td>
             <td colspan="4">
                 <asp:TextBox ID="txtEmailAdd" runat="server" Width ="250px"></asp:TextBox>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         
         <tr>
             <td style="width: 163px">
                 &nbsp;</td>
             <td style="width: 26px">
                 <asp:Button ID="btnCancel" runat ="server" Text ="Cancel" 
                     onclick="btnCancel_Click" />
             </td>
             <td>
                 &nbsp;</td>
             <td>
        <asp:Button ID="btnUpdate" runat ="server" Text ="Update" 
                onclick="btnUpdate_Click" />
             </td>
             <td>
                 &nbsp;</td>
             <td>
                 &nbsp;</td>
         </tr>
         </table>

    </p>     
        <hr /> 
    <span><label id="lblMess" runat ="server" ></label> </span>
        
        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>                
</div>
</asp:Content>

