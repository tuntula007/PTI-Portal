<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageApplicant.master" AutoEventWireup="true"
    CodeFile="ApplicationFormSubmit.aspx.cs" Inherits="ApplicationFormSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .text-normal
        {
            border-right: #666666 1px solid;
            border-top: #666666 1px solid;
            font-size: 12px;
            word-spacing: normal;
            vertical-align: middle;
            border-left: #666666 1px solid;
            color: #333333;
            border-bottom: #666666 1px solid;
            font-style: normal;
            font-family: Verdana, Arial;
            letter-spacing: normal;
            height: 18px;
            background-color: #ffffff;
            text-align: left;
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
            font-family: Arial;
        }
        .style22
        {
            font-size: small;
            font-style: italic;
        }
        .style23
        {
            width: 594px;
        }
        .style24
        {
            height: 22px;
        }
        .style25
        {
            font-family: arial;
            font-size: 13px;
            color: #0A2A69;
            font-weight: bold;
            text-align: left;
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table class="style1">
            <tr>
                <td>
                    <table align="center" class="style21">
                        <tr>
                            <td>
                                <table align="center" class="style2">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-family: Times New Roman; font-size: 15px">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; font-family: Times New Roman; font-size: 22px; border-bottom: dashed 1px gray"
                                            class="style23">
                                            <b>Application Form </b>
                                            <asp:Label ID="lblname" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <i><asp:Label ID="lblCurrentSession" runat="server" Text="2009/2010 Session"></asp:Label></i></td></tr><tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2" align="right" class="style22">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <table align="center" class="tablecredential">
                                                <tr>
                                                    <td>
                                                        <b>Programme </b>
                                                    </td>
                                                    <td>
                                                        <b>Entry Mode</b>
                                                    </td>                                                    <td>
                                                        <b>Mode of Study</b>
                                                    </td>
                                                    <td>
                                                        <b>&nbsp;First Choice Course</b>
                                                    </td>
                                                    <td>
                                                        <b>Second Choice Course</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblsch" runat="server" Text="school"></asp:Label></td><td>
                                                        <asp:Label ID="lblEntryMode" runat="server" Text="EntryMode"></asp:Label></td><td>
                                                        <asp:Label ID="lblcourse" runat="server" Text="course"></asp:Label></td><td>
                                                        <asp:Label ID="lblyear" runat="server" Text="second choice"></asp:Label></td><td>
                                                        <asp:Label ID="lblschoice" runat="server" Text="Firstchoice"></asp:Label></td></tr><tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style24">
                                        </td>
                                        <td colspan="2" class="style25">
                                            <asp:Label ID="lblMess" runat="server" Text="lblMess"></asp:Label></td></tr><tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2" align="left" class="colorheader">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a href="ApplicationForm.aspx"><< REVIEW</a>
                                                    </td>
                                                    <td><span width="50px"></span></td>
                                                    <td>
                                                        <a href="javascript:openwin();">Print Application Form</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        function openwin() {

            window.name = "testwindow";
            testwindow = window.open("ApplicantPrintApplicationForm.aspx", 'mywindow', 'location=no,status=no,toolbar=no,scrollbars=yes,width=1000,height=680,dependent=yes');
            testwindow.moveTo(100, 100);
            testwindow.focus();
        }
     
    
    </script>

</asp:Content>
