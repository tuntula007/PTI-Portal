<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true" 
CodeFile="CourseRegistrationForm.aspx.cs" Inherits="CourseRegistrationForm" Title="Portal Pro+ : - Registration Form" 
MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .text-normal {
	    BORDER-RIGHT: #666666 1px solid;
	    BORDER-TOP: #666666 1px solid;
	    FONT-SIZE: 12px;
	    WORD-SPACING: normal;
	    VERTICAL-ALIGN:middle ; 
	    BORDER-LEFT: #666666 1px solid;
	    COLOR: #333333; BORDER-BOTTOM: #666666 1px solid; 
	    FONT-STYLE: normal;
	    FONT-FAMILY: Verdana, Arial;
	    LETTER-SPACING: normal; HEIGHT: 18px;
	    BACKGROUND-COLOR: #ffffff; 
	    TEXT-ALIGN: left
         }
        .tablesubject
        {
            width: 95%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
            
           
        }
        .style21
        {
            width: 99%;
        }
        .style2
        {
            width: 96%;
            font-family:Arial;
            
        }
        .style37
        {
            text-decoration: underline;
            color:#0A2A69;
        }
        .style41
        {
            height: 23px;
        }
        .style42
        {
            height: 18px;
        }
        </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    
                    <table align="center" class="style21">
                        <tr>
                            <td>
                    
                    <table align="center" class="style2">
                        <tr>
                            <td> &nbsp; </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-family:Times New Roman; font-size:15px">
                                &nbsp;</td>
                            <td style="text-align: left; font-family:Times New Roman; font-size:22px; border-bottom:dashed 1px gray">
                                <b>Course Registration Form</b></td>
                            <td align="right" >
                                <i>2011/2012 Session</i></td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td align="left" class="colorheader" colspan="2">
                                Important Notice</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2" class="smalltext">
                                1. Completed registration form must be printed and signatures of all appropriate 
                                University authorities must be obtained on 
                                <br />
&nbsp;&nbsp;&nbsp; them.<br />
                                2. Completely signed form should be returned to the respective School 
                                Administrative Offices.<br />
                                3. Students should make sure he/she update his/her biodata in&nbsp; using the link 
                                called Personal Data Form in Student Control Center<br />
                                4. New Students should update their Entry Qualification Subjects on Personal 
                                Data Form using the link stated in number 3 above.</td>
                        </tr>
                        <tr>
                            <td class="style41" >
                                </td>
                            <td colspan="2" class="style41">
                                </td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2">
                                <table align="center" class="tablecredential">
                                    <tr>
                                        <td class="style42">
                                            <b>School to which Admitted</b></td>
                                        <td class="style42">
                                            <b>Course</b></td>
                                        <td class="style42">
                                            <b>Year of Course</b></td>
                                        <td class="style42">
                                            <b>Enrolment No</b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblsch" runat="server" Text="school"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcourse" runat="server" Text="course"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblyear" runat="server" Text="year"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmatno" runat="server" Text="matno"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                                                              </td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2" class="colorheader">
                                Make sure you&nbsp; click on REGISTER Button below to finish your registration</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >
                                &nbsp;</td>
                            <td colspan="2" align="left" class="colorheader">
                                Subjects for First Semester 2011/2012 Session</td>
                        </tr>
                        
                        <tr>
                            <td>
                                </td>
                            <td colspan="2" align="left">
                                <table class="tablegridcourse">
                                    <tr valign="top">
                                        <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="odsDeptCourses" Width="700px">
                                    <Columns>
                                        <asp:BoundField DataField="CourseCode" HeaderText="Course Code" 
                                            SortExpression="CourseCode" ItemStyle-CssClass="gridcol1" />
                                        <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" 
                                            SortExpression="CourseTitle" ItemStyle-CssClass="gridcol2" />
                                        <asp:BoundField DataField="CreditLoad" HeaderText="Credit Load" 
                                            SortExpression="CreditLoad"  ItemStyle-CssClass="gridcol3"/>
                                        <asp:TemplateField HeaderText="HOD Signature">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate >
                                    <table align="center">
                                    <tr>
                                        <td class="gridcol1">
                                            Course Code</td>
                                        <td class="gridcol2">
                                            Course Name</td>
                                        <td class="gridcol3">
                                            Credit Unit</td>
                                        <td>
                                            HOD<br />
                                            Signature</td>
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
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <asp:Panel ID="PanelCarryOver" runat="server" Visible=false>
                                            <table class="style1">
                                                <tr>
                                                    <td class="style37">
                                                        Carry Over Courses</td>
                                                                                                  </tr>
                                                                                                  <tr>
                                                                                                      <td>
                                <asp:GridView ID="grdCarryOver" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="odsCarryOverCourses" Width="700px" style="margin-top: 0px">
                                    
                                    <Columns>
                                    
                                    
                                        <asp:BoundField DataField="CourseCode" HeaderText="Course Code" 
                                            SortExpression="CourseCode" ItemStyle-CssClass="gridcol1" />
                                        <asp:BoundField DataField="CourseTitle" HeaderText="Course Title" 
                                            SortExpression="CourseTitle" ItemStyle-CssClass="gridcol2" />
                                        <asp:BoundField DataField="CreditLoad" HeaderText="Credit Load" 
                                            SortExpression="CreditLoad" ItemStyle-CssClass="gridcol3" />
                                        <asp:TemplateField HeaderText="HOD Signature">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   
                                </asp:GridView>
                                                                                                      </td>
                                                                                                  </tr>
                                                                                                  
                                                                                              </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                                                                                          &nbsp;</td>
                                    </tr>
                                    <tr valign="top">
                                        <td style="text-align: center">
                                                                                                          <table class="style1">
                                                                                                              <tr>
                                                                                                                  <td class="gridcol1">
                                                                                                                      &nbsp;</td>
                                                                                                                  <td class="gridcol2" align="right">
                                                                                                                      <b><font size=2px>Total Credit Units:</font></b></td>
                                                                                                                  <td class="gridcol3">
                                                                                                                      <asp:Label ID="lblTotCredit" runat="server" Font-Bold="True" Text="0.0"></asp:Label>
                                                                                                                  </td>
                                                                                                                  <td>
                                                                                                                      &nbsp;</td>
                                                                                                              </tr>
                                                                                                          </table>
                                                                                                      </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ObjectDataSource ID="odsDeptCourses" runat="server" 
                                    SelectMethod="getSemesterCourses" 
                                    TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="courseofstudyId" 
                                            SessionField="CourseofStudyId" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="level" SessionField="Level" 
                                            Type="String" />
                                        <asp:QueryStringParameter DefaultValue="First" Name="semester" 
                                            QueryStringField="Semester" Type="String" />
                                        <asp:SessionParameter DefaultValue="0" Name="departmentId" 
                                            SessionField="DepartmentId" Type="Int32" />
                                        <asp:SessionParameter DefaultValue="" Name="programme" SessionField="Programme" 
                                            Type="String" />
                                        <asp:SessionParameter Name="modeofstudy" SessionField="ModeOfStudy" 
                                            Type="String" />
                                    </SelectParameters>
                                    
                               
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ObjectDataSource ID="odsCarryOverCourses" runat="server" 
                                    SelectMethod="getSemesterCarryOver" 
                                    TypeName="CybSoft.EduPortal.Business.DeptCoursesBusiness">
                                      <SelectParameters>
                                          <asp:SessionParameter DefaultValue="" Name="matricNumber" 
                                              SessionField="MatricNumber" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lbltitleError" runat="server" Font-Bold="True" 
                                    ForeColor="#CC3300" Text="lblerror" Visible="False"></asp:Label>
                                     &nbsp;<asp:Label ID="lblEntryError1" runat="server" Font-Bold="True" ForeColor="#CC3300" Text="lblerror" 
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnRegister" runat="server" onclick="btnRegister_Click" 
                                    style="font-weight: 700" Text="Click here to Register" Font-Size="Large" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                    </table>
                    
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
    
    </div>
</asp:Content>


