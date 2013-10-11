<%@ Page Language="C#" MasterPageFile="~/MasterPageApplicant.master" AutoEventWireup="true"
    CodeFile="ApplicantControlCenter.aspx.cs" Inherits="ApplicantControlCenter" Title="Applicant Control Center" %>

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
        .style16
        {
            font-weight: bold;
            text-decoration: underline;
        }
        .style17
        {
            color: #009900;
        }
        .style18
        {
            color: red;
            font-weight: bold;
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <center>
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style18">
                        PLEASE CHECK BACK LATER, THE PORTAL IS CURRENT LY BEING UPDATED.......</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            &nbsp;<table bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" width="95%">
                <tr>
                    <td valign="top" width="20%">
                        <table border="0" cellpadding="0" cellspacing="0" width="230">
                            <tr>
                                <td>
                                    <img height="27" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="204" class="controlcentermenu">

                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="ApplicationForm.aspx">Fill Application Form</a></strong>
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="ApplicantPrintApplicationExamCard.aspx">
                                                    <asp:Label ID="Label1" runat="server" Text="Print Exam Card" 
                                                    Visible="true"></asp:Label></a>
                                                </strong>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="ApplicantPrintApplicationForm.aspx">Print Application Form</a></strong>
                                            </td>
                                        </tr>
                                        
                                        
                                                                                
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                
                                                <hr />
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="#">Screening Exam Clearance</a></strong>
                                            </td>
                                        </tr>

                                        
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="#">Manage Profile</a> </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="RptPrintNotification.aspx">
                                                    <asp:Label ID="AdmissionLabel" runat="server" Text="Check Admission Status" 
                                                    Visible="False"></asp:Label></a>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="images/button.gif" class="style1" height="28">
                                                <strong><a href="#">Academic Calendar</a></strong>
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
                                                <img height="8" src="images/spacer.gif" width="27" /><strong>Events / News Alert</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td background="images/gr1.gif">
                                                <table border="0" cellpadding="0" cellspacing="0" width="204">
                                                    <tr>
                                                        <td width="25">
                                                            <img height="8" src="images/spacer.gif" width="20" />
                                                        </td>
                                                        <td width="175">
                                                            <table border="0" cellpadding="0" cellspacing="5" width="175">
                                                                <tr>
                                                                    <td class="style2">
                                                                        Application for Admission is open now
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        Update your biodata on your Application form
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style2">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
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
                                                <img height="7" src="images/part_bottom.gif" width="204" />
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
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <img height="20" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style15" valign="top">
                                    <asp:Image ID="imgPix" runat="server" ImageUrl="~/images/img.jpg" AlternateText="Upload Passport"
                                        Height="114px" Width="119px" />
                                </td>
                                <td align="left" valign="top">
                                    <p align="left" style="font-family: Arial; font-size: 13px">
                                        <b><span class="style17">P T I&nbsp; Portal</span><br></br>
                                        <span class="style16">Prospective Students Control Center                                         </span>
                                    </p>
                                    <p align="left" style="font-family: Arial; font-size: 12px">
                                        This Control Center guides you through any operations you wish to perform online.
                                        <span class="style9">Make sure you upload your passport before you start registration</span>..</p>
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
                                                <td id="PassportUpload" runat="server">
                                                    <div class="sample_attach" id="sample_attach_src_parent">
                                                        Upload Passport</div>
                                                    <div class="sample_attach_div" id="sample_attach_src_child">
                                                        <b>Browse your passport, and click upload button below</b><br />
                                                        <asp:FileUpload ID="FileUploadPassport" runat="server" class="sample_attach_input" />
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
                                                    <img alt="Login information" src="images/new.gif" style="height: 25px; width: 25px" />
                                                </td>
                                                <td align="left">
                                                    <a href="ApplicationForm.aspx" style="color: Blue; font-size: 14px">Uploaded your passport?
                                                        Fill your form here</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style10">
                                                </td>
                                                <td align="left" class="style11">
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
                                    Prospective Student Personal Information
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <img alt="" height="10" src="images/spacer.gif" width="1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" cellpadding="2" cellspacing="2" width="90%" class="controlcentertext">
                                        <tr>
                                            <td class="style5" align="left">
                                                Application Form No:
                                            </td>
                                            <td class="style7">
                                            </td>
                                            <td class="style6" align="left">
                                                <asp:Label ID="lblMatno" runat="server" Text="matno"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style12" align="left">
                                                Surname:
                                            </td>
                                            <td class="style13">
                                            </td>
                                            <td align="left" class="style14">
                                                <asp:Label ID="lblSurname" runat="server" Text="surname"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                Other Names:
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
                                                Programme:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblprogcat" runat="server" Text="program"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                &nbsp;</td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblModeOfStudy" runat="server" Text="studymode" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                Entry Mode:
                                            </td>
                                            <td class="style8">
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblEntryMode" runat="server" Text="EntryMode"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4" align="left">
                                                Application Session:
                                            </td>
                                            <td class="style8">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblpresentsession" runat="server" Text="Session"></asp:Label>
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
</asp:Content>
