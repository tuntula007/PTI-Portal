<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="StudentControlCenter.aspx.cs" Inherits="StudentControlCenter" Title="Students Control Centre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="dropdown.js"></script>

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 18%;
            text-align: left;
        }
        a:link
        {
            text-decoration: none;
        }
        a
        {
            font-size: 11px;
            color: #ffffff;
        }
        .style3
        {
            width: 30px;
        }
        .style4
        {
            width: 136px;
        }
        .style5
        {
            width: 136px;
            height: 28px;
        }
        .style6
        {
            height: 28px;
        }
        .style7
        {
            height: 28px;
            width: 13px;
        }
        .style8
        {
            width: 13px;
        }
        .style9
        {
            text-decoration: underline;
            color: #CC0000;
        }
        .style10
        {
            width: 30px;
            height: 23px;
        }
        .style11
        {
            height: 23px;
        }
        a.sample_attach, a.sample_attach:visited, div.sample_attach
        {
            display: block;
            width: 150px;
            border: 1px solid black;
            padding: 2px 5px;
            background: #FFFFEE;
            text-decoration: none;
            font-family: Verdana, Sans-Sherif;
            font-weight: 900;
            font-size: 1.0em;
            color: #008000;
        }
        a.sample_attach, a.sample_attach:visited
        {
            border-bottom: none;
        }
        div#sample_attach_menu_child
        {
            border-bottom: 1px solid black;
        }
        input.sample_attach
        {
            margin: 1px 0px;
            width: 170px;
        }
        .sample_attach_div
        {
            position: absolute;
            visibility: hidden;
            border: 1px solid black;
            padding: 0px 5px 2px 5px;
            background: #FFFFEE;
        }
        .errorblock
        {
            display: block;
            width: 250px;
            border: 1px solid black;
            padding: 2px 5px;
            background: #FFFFEE;
            text-decoration: none;
            font-family: Verdana, Sans-Sherif;
            font-weight: 500;
            font-size: 1.0em;
            color: #B91111;
        }
        .style12
        {
            width: 136px;
            height: 21px;
        }
        .style13
        {
            width: 13px;
            height: 21px;
        }
        .style14
        {
            height: 21px;
        }
        .style15
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <table class="style1">
                <tr>
                    <td align="right" style="margin-right: 5px; font-style: italic; font-size: large;
                        font-weight: bold">
                        <asp:Label ID="lblCurrentSession" runat="server" Text=""></asp:Label>
                        <%--<asp:Label ID="CurrentSessionSemester" runat="server" Text=""></asp:Label>--%>
                    </td>
                </tr>
            </table>
            &nbsp;<table bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td valign="top" width="20%">
                        <table border="0" cellpadding="0" cellspacing="0" width="230">
                            <tr>
                                <td>
                                    <img alt="" height="27" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="204" class="controlcentermenu">
                                        <tr>
                                            <td id="Td1" background="images/button.gif" tooltip="Update your personal info" height="28"
                                                align="left" runat="server">
                                                <strong><a href="PersonalBiodata.aspx" style="margin-left: 5px;">Student Profile</a>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td2" background="images/button.gif" tooltip="Update your personal info" height="28"
                                                align="left" runat="server">
                                                <strong><a href="Changepassword.aspx" style="margin-left: 5px;">Change Password</a>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="PrintAdmissionLetter" background="images/button.gif" height="28" align="left"
                                                runat="server">
                                                <strong><a href="RptPrintAdmissionLetter.aspx" style="margin-left: 5px">Admission Letter</a>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="SemesterReg" runat="server" background="images/button.gif" align="left" class="style6">
                                                <strong><a href="RegistrationFormSubmit.aspx" onclick="return CheckCanRegister('<% = Server.HtmlEncode(this.CanRegister(1)) %>','1');"
                                                    style="margin-left: 5px">Course Registration</a></strong>
                                            </td>
                                        </tr>
                                        
                                        <%--<tr>
                                            <td background="images/button.gif" align="left" height="28" runat ="server" id ="linkPrintRegistration">
                                                <strong><a href="javascript:openwin();" runat ="server"   onclick="return CheckRegistered('<% = Server.HtmlEncode(this.isRegistered(1)) %>','1');"
                                                    style="margin-left: 5px">Print Registration Form</a></strong>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td background="images/button.gif" align="left" height="28" runat ="server" id ="linkPrintRegistration">
                                                <strong><a id="A1" href="javascript:openwin();" runat ="server"    
                                                    style="margin-left: 5px">Print Registration Form</a></strong>
                                            </td>
                                        </tr>
                                        
                                        
                                         <tr>
                                            <td background="images/button.gif" align="left" height="28" runat ="server" id ="Td3">
                                                <strong><a id="A2" href="HowToRegisterCourses.pdf" runat ="server"    
                                                    style="margin-left: 5px">How To Register Courses</a></strong>
                                            </td>
                                        </tr>
                                        
                                          <tr>
                                            <td background="images/button.gif" align="left" height="28" >
                                                <strong><a href="javascript:openwinFirst();" visible ="false" id="linkPrintFirstRegistration" runat="Server" onclick="PrintFirstRegistration"
                                                    style="margin-left: 5px">Print First Registration Form</a></strong>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td background="images/button.gif" align="left" height="28" >
                                                <strong><a href="javascript:openwinSecond();" visible ="false" id="linkPrintSecondRegistration" runat="Server" onclick="PrintFirstRegistration"
                                                    style="margin-left: 5px">Print Second Registration Form</a></strong>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td background="images/button.gif" align="left" height="28" >
                                                <strong><a href="javascript:openwinThird();" visible ="false" id="linkPrintThirdRegistration" runat="Server" onclick="PrintFirstRegistration"
                                                    style="margin-left: 5px">Print Third Registration Form</a></strong>
                                            </td>
                                        </tr>
                                        <tr id="trExamClearance" runat="server" visible="false">
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong>
                                                <a href="#" style="margin-left: 5px"></a>
                                                <%--<a href="ExamClearanceForm.aspx" style="margin-left: 5px">Exam Clearance Form</a>--%>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server" visible="True">
                                         <%--
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong><a href="MedicalStatus.aspx" style="margin-left: 5px">Medical Status Form</a>
                                                </strong>
                                            </td>
                                            --%> 
                                        </tr>
                                        <tr id="trMedicalForm" runat="server" visible="false">
                                             <%--
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong><a href="javascript:openwinMedical()" style="margin-left: 5px">Medical Registration
                                                    Authorisation</a> </strong>
                                            </td>
                                            --%>
                                            
                                        </tr>
                                        <tr id="trLibraryForm" runat="server" visible="false">
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong>
                                                <a href="#" style="margin-left: 5px"></a></strong>
                                                
                                                <%--<a href="javascript:openwinLibrary()" style="margin-left: 5px">Library Registration Authorisation</a></strong>--%>
                                            </td>
                                        </tr>
                                        <tr id="trStudentID" runat="server" visible="false">
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong>
                                                <a href="#" style="margin-left: 5px"></a></strong>
                                                <%--<a href="javascript:openwinStudentId();" style="margin-left: 5px">My ID Card</a></strong>--%>
                                            </td>
                                        </tr>
                                        <tr id="trCourseMaterial" runat="server" visible="True">
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong><a href="ContentGallery.aspx" style="margin-left: 5px"></a>
                                                
                                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                                    OnClick ="PrintFirstRegistration" Visible="False">
                                                
                                                <strong> </strong>LinkButton
                                                
                                                </asp:LinkButton>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                          <%--  <td background="images/button.gif" align="left" height="28">
                                                <strong><a href="#" style="margin-left: 5px">My Academic Records</a> </strong>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                         <%-- 
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong><a href="#" style="margin-left: 5px">My Lecturers</a></strong>
                                            </td>
                                          --%>  
                                        </tr>
                                        <tr id="trPayStatus" runat="server">
                                            <td background="images/button.gif" align="left" height="28">
                                                <strong>
                                                
                                                <a id="lnkPayStatus" runat="server" href="#" style="margin-left: 5px">
                                                    <asp:Label ID="lblPayStatus" Visible ="false" runat="server" Text="." /></a>
                                                    
                                                    </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img height="27" src="images/spacer.gif" width="1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="204">
                                        <tr>
                                            <td background="images/partner_t.gif" height="34">
                                                <img alt="" height="8" src="images/spacer.gif" width="27" /><strong>Events / News Alert</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="images/gr1.gif">
                                                <table border="0" cellpadding="0" cellspacing="0" width="204">
                                                    <tr>
                                                        <td width="25">
                                                            <img height="8" src="images/spacer.gif" width="20" />
                                                        </td>
                                                        <td width="185">
                                                            <table border="0" cellpadding="0" cellspacing="5" width="185">
                                                                <%--                                                                <tr>
                                                                    <td style="color: Maroon" align="left">
                                                                        Registration&nbsp; for 2009/2010 Second Semester is now open for DLC MODE Programme
                                                                        students
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        Part-Time Students Start Exam! Update of their biodata on registration form should
                                                                        continue.
                                                                    </td>
                                                                </tr>
--%>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" height="7" src="images/part_bottom.gif" width="204" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" width="60%">
                        <table align="left" border="0" cellpadding="0" cellspacing="0" width="498">
                            <tr>
                                <td class="controlcenterwelcome" colspan="2">
                                    &nbsp;
                                    <asp:Label ID="lblWelcom" runat="server" Text="Welcome!"></asp:Label>
                                    <input id="haspass" type="hidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <img alt="" height="20" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style15" valign="top">
                                    <asp:Image ID="imgPix" runat="server" ImageUrl="~/images/img.jpg" AlternateText="Upload Passport"
                                        Height="114px" Width="119px" style="margin-right: 0px" />
                                </td>
                                <td align="left" valign="top">
                                    <p align="left" style="font-family: Arial; font-size: 13px">
                                        PTI<b> Portal - Students Control Center </b>
                                    </p>
                                    <p align="left" style="font-family: Arial; font-size: 12px">
                                        This Control Center guides you through any operations you wish to perform online.
                                        <span class="style9">Make sure you upload your passport and update your personal biodata
                                            before you print your course registration form</span>.</p>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Panel ID="PanelUploadPass" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td class="style3">
                                                    <img alt="Login information" src="images/login_infoicon.png" style="height: 22px;
                                                        width: 22px" />
                                                </td>
                                                <td id="PassportUpdate" runat="server">
                                                    <div class="sample_attach" id="sample_attach_src_parent">
                                                        Update Passport</div>
                                                    <div class="sample_attach_div" id="sample_attach_src_child">
                                                        <b>Browse your passport, and click upload button below</b><br />
                                                        <br />
                                                        <asp:FileUpload ID="FileUploadPassport" runat="server" class="sample_attach_input"
                                                            Height="22px" Width="217px" />
                                                        <br />
                                                        <b>Passport can only be in (PNG, JPEG, GIF) formats.<br />
                                                            Maximum Size Allowed is 15kb.</b>
                                                        <br />
                                                        <asp:Button ID="btnUploadPassport" runat="server" Text="Upload" OnClick="btnUploadPassport_Click" />
                                                    </div>

                                                    <script type="text/javascript">
                                                        at_attach("sample_attach_src_parent", "sample_attach_src_child", "hover", "x", "pointer");
                                                    </script>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="PanelRegister" runat="server">
                                        <table class="style1">
                                            <tr>
                                                <td class="style3">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Panel ID="Panel4First" runat="server">
                                                        <asp:Panel ID="PanelRegStatus" runat="server">
                                                            <a href="javascript:openwin();" style="color: #008000; font-size: 14px">Print Your Registration
                                                                Form </a>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style3">
                                                    <img alt="Login information" src="images/new.gif" style="height: 25px; width: 25px" />
                                                </td>
                                                <td align="left">
                                                    <asp:Panel ID="Panel4Second" runat="server">
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                </td>
                                                <td align="left" class="style11">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                    <asp:Panel ID="panelError" runat="server" Visible="False">
                                        <center>
                                            <div class="errorblock">
                                                <asp:Label ID="lblerror" runat="server" Text="lblerror" ForeColor="#CC3300"></asp:Label></div>
                                        </center>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="controlcenterheader" colspan="2">
                                    Student Personal Information
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <img height="10" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" cellpadding="2" cellspacing="2" width="90%" class="controlcentertext">
                                        <tr>
                                            <td class="style5" align="left">
                                                MATRIC NUMBER:
                                            </td>
                                            <td class="style7">
                                            </td>
                                            <td class="style6" align="left">
                                                <asp:Label ID="lblMatno" runat="server" Text="matno"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style12" align="left">
                                                SURNAME:
                                            </td>
                                            <td class="style13">
                                            </td>
                                            <td align="left" class="style14">
                                                <asp:Label ID="lblSurname" runat="server" Text="surname"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                OTHERNAMES:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblOthernames" runat="server" Text="othernames"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                DIRECTORATE:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblSchool" runat="server" Text="school"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                DEPARTMENT:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblDepartment" runat="server" Text="department"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                PROGRAMME OF STUDY:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblCourseOfStudy" runat="server" Text="courseofstudy"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                LEVEL:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblLevel" runat="server" Text="level"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                STUDY MODE:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblModeOfStudy" runat="server" Text="studymode"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                E-MAIL:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lbldlcmail" runat="server" Text=":"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </div>

    <script type="text/javascript" language="javascript">
    function HasRegistered()
    {    
      return true;
      //version 1 method of accessing code behind - value access from javascript
        <% testIsRegistered("First"); %>
       
        var retval="<%= StatMessage %>"
       
        if (retval=="T")
        {
          // alert ("Since you have registered before, registration for 2009/2010 First Semester is CLOSED for you as DLC MODE Student. For more information, contact your ICT Director");
          //return false;
          return true;
        } else
        {     
           return true;
        }
    }
    
    function CheckRegistered(va1,va2)
    {
        // version 2 method of accessing code behind - value access from HTML
         if (va1=="T")
         {
           return true;
         }else
         {
           var sem="";
           if (va2=="1")
           {
             sem="First";
           } else
           {
             sem ="Second";
           }
           alert ("You have NOT registered for the curent session courses, so Printing is disallowed! Click on Course Registration link to Register.");
           return false;
         }
     
    }


    function CheckCanRegister(va1,va2)
    {
        // version 2 method of accessing code behind - value access from HTML
         if (va1=="T")
         {
           return true;
         }else
         {
           var sem="";
           if (va2=="1")
           {
             sem="First";
           } else
           {
             sem ="Second";
           }           
           alert ("Course Registration for the current session is NOT YET available! Contact the ICT Director for more information.");
           return false;
         }
     
    }
    
     function openwin()
     {
     window.location="PrintCourseRegistrationForm.aspx?Semester=First&isFromCenter=1";
     }
     
      function openwinFirst()
     {
     window.location="PrintCourseRegistrationForm.aspx?Semester=First&isFromCenter=1";
     }
       function openwinSecond()
     {
     window.location="PrintCourseRegistrationForm.aspx?Semester=Second&isFromCenter=1";
     }
      function openwinThird()
     {
     window.location="PrintCourseRegistrationForm.aspx?Semester=Third&isFromCenter=1";
     }
     
     
     
      function openwinStudentId() {

     window.name = "testwindow";
     testwindow = window.open("StudentIdCrystal.aspx", 'mywindow', 'location=no,status=no,toolbar=no,scrollbars=yes,width=800,height=680,dependent=yes');
     testwindow.moveTo(100, 100);
     testwindow.focus();
 }
      function openwinLibrary() {

     window.name = "testwindow";
     testwindow = window.open("RptPrintLibrary.aspx", 'mywindow', 'location=no,status=no,toolbar=no,scrollbars=yes,width=800,height=680,dependent=yes');
     testwindow.moveTo(100, 100);
     testwindow.focus();
 }
       function openwinMedical() {

     window.name = "testwindow";
     testwindow = window.open("RptPrintMedical.aspx", 'mywindow', 'location=no,status=no,toolbar=no,scrollbars=yes,width=800,height=680,dependent=yes');
     testwindow.moveTo(100, 100);
     testwindow.focus();
 }
    </script>

</asp:Content>
