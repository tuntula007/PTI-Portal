<%@ Page Title="Application Form" Language="C#" MasterPageFile="~/MasterPageApplicant.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="ApplicationForm.aspx.cs"
    Inherits="ApplicationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
   <style type="text/css">  
   table.radioWithProperWrap input  
   { 
   float: left;  }   
   table.radioWithProperWrap label  
   {
    margin-left: 25px;       
    display: block;  } 
   </style> 

   
   
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .text-normal
        {
            border: 1px solid #666666;
            font-size: 12px;
                word-spacing: normal;
                vertical-align: middle;
                color: #333333;
                font-style: normal;
                font-family: Verdana, Arial;
                letter-spacing: normal;
                background-color: #ffffff;
                text-align: left;
            margin-right: 9px;
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
        .style18
        {
            width: 96%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }
        .style19
        {
            height: 21px;
        }
        .style27
        {
            height: 32px;
        }
        .style28
        {
        }
        .style29
        {
            width: 248px;
            height: 24px;
        }
        .style30
        {
            height: 24px;
        }
        .style34
        {
            width: 248px;
            height: 22px;
        }
        .style35
        {
            height: 22px;
        }
        .style42
        {
            height: 18px;
        }
    </style>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 86%;
        }
        .style2
        {
            width: 100%;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
        }
        .style43
        {
            height: 39px;
        }
        .style44
        {
            background-color: #C0C0C0;
        }
        .style45
        {
            width: 93%;
        }
        .style46
        {
            width: 123px;
        }
        .style47
        {
            height: 21px;
            width: 165px;
        }
        .style48
        {
            height: 21px;
            width: 159px;
        }
        .style49
        {
            width: 75%;
        }
        .style50
        {
            width: 168px;
        }
        .style51
        {
            width: 180px;
        }
        .style52
        {
            width: 128px;
        }
        .style53
        {
            width: 146px;
        }
        .style54
        {
        }
    .style55
    {
        font-family: Verdana;
    }
    .radioWithProperWrap
    {
        font-family: Verdana;
    }
    .style56
    {
        height: 298px;
    }
        .style57
        {
            font-size: 12px;
        }
        .style58
        {
            width: 180px;
            font-size: 12px;
        }
        .style60
        {
            width: 128px;
            font-size: 12px;
            height: 12px;
            font-weight: normal;
        }
        .style61
        {
            width: 168px;
            height: 12px;
            font-size: 12px;
        }
        .style62
        {
            width: 180px;
            height: 12px;
        }
        .style63
        {
            width: 146px;
            height: 12px;
        }
        .style64
        {
            font-size: 12px;
            height: 12px;
            font-weight: normal;
        }
        .style65
        {
            height: 12px;
        }
        .style66
        {
            font-size: 12px;
        }
        .style67
        {
            font-weight: normal;
        }
        .style68
        {
            width: 146px;
            font-size: 12px;
            font-weight: normal;
        }
        .style69
        {
            font-size: 14px;
        }
        .style70
        {
            font-family: Verdana;
            color: #999999;
        }
        .style71
        {
            color: #FF0000;
        }
        .style72
        {
            font-family: Verdana;
            color: #FF0000;
        }
        .style73
        {
            color: #CC6600;
        }
        .style74
        {
            font-family: Verdana;
            color: #CC0000;
        }
        .style75
        {
            font-size: 8px;
            text-align: center;
        }
        .style76
        {
            color: #990000;
        }
        .style81
        {
            font-size: 8px;
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
                                        <td style="text-align: center; font-family: Times New Roman; font-size: 15px">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; font-family: Times New Roman; font-size: 22px; border-bottom: dashed 1px gray">
                                            <b style="font-size: 14px">Application Form For Admission</b>
                                        </td>
                                        <td style="text-align: left; font-family: Times New Roman; font-size: 22px; border-bottom: dashed 1px gray">
                                            <asp:Label ID="lblSession" runat="server" Text="2012/2013 " 
                                                style="font-size: 14px"></asp:Label>
                                            <span class="style69">SESSION</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                                        <asp:Label ID="lblcourse" runat="server" Text="course" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left" class="colorheader" colspan="2">
                                            Important Notice
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2" class="smalltext">
                                            <ol>
                                                <li>All information required must be carefully filled in this application form.</li><li>
                                                    Applicants should make sure he/she update his/her biodata in the section labelled
                                                    Personal Biodata below.</li><li>Applicants should update their Educational Qualification.</li>
                                            </ol>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <table align="center" class="tablecredential">
                                                <tr>
                                                    <td class="style42">
                                                        <b>Programme</b>
                                                    </td>
                                                    <td class="style42">
                                                        &nbsp;</td>
                                                    <td class="style42">
                                                        <b>Mode of Entry</b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>Application Form Number</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblsch" runat="server" Text="school"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:DropDownList ID="ModeOfEntry" runat="server" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblmatno" runat="server" Text="matno"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan="2" class="style73">
                                            click on each of these links below to enter the different categories of&nbsp; 
                                            your application information :</td>
                                    </tr>
                                    <tr>
                                        <td class="style43">
                                            &nbsp;
                                        </td>
                                        <td colspan="2" class="style43">
                                            <table align="center" class="tablecredentialLarge">
                                                <tr>
                                                    <td class="style42">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lnkpersonInfo" Width="100%" 
                                                            OnClick="lnkpersonInfo_Click" style="background-color: #C0C0C0">PERSONAL INFORMATION</asp:LinkButton></b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lnkchoiceProg" Width="100%" 
                                                            OnClick="lnkchoiceProg_Click" style="background-color: #C0C0C0" 
                                                            ToolTip="click here to enter your choice of programmes"><b>CHOOSE PROGRAMMES</b><b></asp:LinkButton></b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lnkeduinfo" Width="100%" 
                                                            OnClick="lnkeduinfo_Click" style="background-color: #C0C0C0" 
                                                            ToolTip="click here to enter information on your previous education"><b>YOUR EDUCATION HISTORY</b></asp:LinkButton></b>
                                                    </td>
                                                    <td id="trPostEdu" runat="server" visible="false">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lnkposteduinfo" Width="100%" 
                                                            OnClick="lnkposteduinfo_Click" style="background-color: #C0C0C0" 
                                                            ToolTip="click here to enter information on your previous education"><b><b>OTHER EDUCATION HISTORY</b></b></asp:LinkButton></b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>
                                                            <asp:LinkButton runat="server" ID="lnkattestation" OnClientClick="alert('You can only complete this application if you have fill all neccesary details')"
                                                                Width="100%" OnClick="lnkattestation_Click" Height="38px" 
                                                            style="background-color: #C0C0C0" 
                                                            ToolTip="click here to enter your declaration"><b>DECLARATION</b></asp:LinkButton></b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="mtvApplicationForm" runat="server">
            <asp:View ID="vwPersonalDetails" runat="server">
                <table align="center" class="style2">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Panel ID="PanelPersonalDetail" runat="server">
                                <table class="style1">
                                    <tr>
                                        <td class="blackheader">
                                            PERSONAL INFORMATION:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td class="style28">
                                                        Title
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="cmbTitle" runat="server">
                                                            <asp:ListItem> </asp:ListItem>
                                                            <asp:ListItem>Mr.</asp:ListItem>
                                                            <asp:ListItem>Mrs.</asp:ListItem>
                                                            <asp:ListItem>Miss</asp:ListItem>
                                                            <asp:ListItem>Chief</asp:ListItem>
                                                            <asp:ListItem>Dr.</asp:ListItem>
                                                            <asp:ListItem>Dr. Mrs.</asp:ListItem>
                                                            <asp:ListItem>Engr.</asp:ListItem>
                                                            <asp:ListItem>Prof.</asp:ListItem>
                                                            <asp:ListItem>Rev.</asp:ListItem>
                                                            <asp:ListItem>Rev. Mrs.</asp:ListItem>
                                                            <asp:ListItem>Sir</asp:ListItem>
                                                            <asp:ListItem>Lady</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Surname
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSurname" runat="server" CssClass="text-normal" Width="289px"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Other Names
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtOthernames" runat="server" CssClass="text-normal" Width="376px"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Date of Birth
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="cmbDay" runat="server">
                                                            <asp:ListItem>Day</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                            <asp:ListItem>19</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>21</asp:ListItem>
                                                            <asp:ListItem>22</asp:ListItem>
                                                            <asp:ListItem>23</asp:ListItem>
                                                            <asp:ListItem>24</asp:ListItem>
                                                            <asp:ListItem>25</asp:ListItem>
                                                            <asp:ListItem>26</asp:ListItem>
                                                            <asp:ListItem>27</asp:ListItem>
                                                            <asp:ListItem>28</asp:ListItem>
                                                            <asp:ListItem>29</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>31</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="cmdMonth" runat="server">
                                                            <asp:ListItem>Month</asp:ListItem>
                                                            <asp:ListItem Value="01">Jan</asp:ListItem>
                                                            <asp:ListItem Value="02">Feb</asp:ListItem>
                                                            <asp:ListItem Value="03">Mar</asp:ListItem>
                                                            <asp:ListItem Value="04">Apr</asp:ListItem>
                                                            <asp:ListItem Value="05">May</asp:ListItem>
                                                            <asp:ListItem Value="06">Jun</asp:ListItem>
                                                            <asp:ListItem Value="07">Jul</asp:ListItem>
                                                            <asp:ListItem Value="08">Aug</asp:ListItem>
                                                            <asp:ListItem Value="09">Sep</asp:ListItem>
                                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="cmbYear" runat="server">
                                                            <asp:ListItem>Year</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Gender
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="cmbSex" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Marital Status
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbMarital" runat="server">
                                                            <asp:ListItem>Single</asp:ListItem>
                                                            <asp:ListItem>Married</asp:ListItem>
                                                            <asp:ListItem>Widowed</asp:ListItem>
                                                            <asp:ListItem>Divorced</asp:ListItem>
                                                            <asp:ListItem>Separated</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Religion
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbReligion" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>Christian</asp:ListItem>
                                                            <asp:ListItem>Muslim</asp:ListItem>
                                                            <asp:ListItem>Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Physical Challenges
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbDisability" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="None">None</asp:ListItem>
                                                            <asp:ListItem Value="Deaf">Deaf</asp:ListItem>
                                                            <asp:ListItem Value="Dumb">Dumb</asp:ListItem>
                                                            <asp:ListItem Value="Blind">Blind</asp:ListItem>
                                                            <asp:ListItem Value="Maimd">Maimed</asp:ListItem>
                                                            <asp:ListItem Value="Others">Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <%--                                                <tr>
                                                    <td class="style28">
                                                        Blood Group
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbBG" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>A</asp:ListItem>
                                                            <asp:ListItem>B</asp:ListItem>
                                                            <asp:ListItem>B-</asp:ListItem>
                                                            <asp:ListItem>B+</asp:ListItem>
                                                            <asp:ListItem>O</asp:ListItem>
                                                            <asp:ListItem>AB</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
--%>
                                                <tr>
                                                    <td class="style28">
                                                        Maiden Name (If married Female)
                                                    </td>
                                                    <td colspan="3" class="style27">
                                                        <asp:TextBox ID="txtMaiden" runat="server" CssClass="text-normal" Width="153px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Nationality
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="cmbNation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbNation_SelectedIndexChanged">
                                                            <asp:ListItem>Nigerian</asp:ListItem>
                                                            <asp:ListItem>Non-Nigerian</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="trForNonNigerian" runat="server" visible="false">
                                                    <td>
                                                        Please Enter your Nationality
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtForNonNigerian" runat="server" CssClass="text-normal" Width="289px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr id="trForNigerianState" runat="server">
                                                    <td class="style29">
                                                        State
                                                    </td>
                                                    <td colspan="3" class="style30">
                                                        <asp:DropDownList ID="cmbState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbState_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="trForNigerianLGAs" runat="server">
                                                    <td class="style28">
                                                        Local Govt. Area
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="cmbLGA" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Permanent Home Address &amp; Home Town<br />
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="text-normal" Width="243px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style34">
                                                        Postal address</td>
                                                    <td class="style35" colspan="3">
                                                        <asp:TextBox ID="txtCorAddress" runat="server" CssClass="text-normal" Width="243px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style34">
                                                        Email Address
                                                    </td>
                                                    <td colspan="3" class="style35">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="text-normal" Width="127px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Phone Number
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="text-normal" Width="127px" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="colorheader" colspan="4">
                                                        Details of Next of Kin (Person to call in case of emergency):
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Next Of Kin Name
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSponsorName" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Next Of Kin Address
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSponsorAdd" runat="server" CssClass="text-normal" Width="243px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Next of Kin Phone Number
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSponsorPhone" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Next Of Kin Email
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSponsorEmail" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Relationship to Next of Kin
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtSponsorRelationship" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtNkinOccupation" runat="server" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        My Sponsor:</td>
                                                    <td colspan="3">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style75" 
                                                        
                                                        title="Enter any of the following:  Parents, or Company, or Self, or Others (if none of the three is your sponsor)" 
                                                        style="vertical-align: top;">
                                                        <span class="style81">&nbsp;</span><span class="style81"><span class="style55">NAME&nbsp; 
                                                        AND ADDRESS OF SPONSOR :<br />
                                                        &nbsp;IF YOU HAVE BEEN AWARDED ANY SCHOLARSHIP, BURSARY AND SPONSORED BY AN 
                                                        ORGANIZATIONS FOR THE COURSE PROPOSED, STATE THE NAME OF THE AWARDING OR 
                                                        SPONSORING AUTHORITY AND THE VALUE OF THE SCHOLARSHIP OR BURSARY.
                                                        <br />
                                                        </span></span><span class="style55"><span class="style76"><span class="style81">
                                                        ATTARCH PHOTOSTART COPY OF RELEVANT DOCUMENT<br />
                                                        TO YOUR APPLICATION FORM PRINT OUT</span></span></span></td>
                                                    <td colspan="3" class="style75">
                                                           <asp:TextBox ID="txtsponsor" runat="server" 
                                                               
                                                               
                                                               ToolTip="Enter any of the following:  Parents, or Company, or Self, or Others (if none of the three is your sponsor) / State clearly Your  Sponsor's Address" 
                                                               CssClass="text-normal" Width="402px" Height="101px" TextMode="MultiLine"></asp:TextBox>
                                                           &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style28" 
                                                        title="Enter any of the following:  Parents, or Company, or Self, or Others (if none of the three is your sponsor)">
                                                        &nbsp;</td>
                                                    <td colspan="3" 
                                                        style="font-style: italic; font-family: 'Times New Roman', Times, serif">
                                                        (Parents, Company, Self, Others)</td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:Button ID="btnSaveContinue" runat="server" Text="Save & Continue" OnClick="btnSaveContinue_Click" /><asp:Button
                                                            ID="btnContinuelater" runat="server" Text="Continue Later" OnClick="btnContinuelater_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td colspan="3">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwProgramOfChoice" runat="server">
                <table align="center" class="style2">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Panel ID="pnlCourseOfStudy" runat="server" Visible="True">
                                <table class="style1">
                                    <tr>
                                        <td class="blackheader">
                                            CHOICE OF PROGRAMME:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td class="style28" colspan="3">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        First Course of Choice
                                                    </td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:DropDownList ID="cmb1stChoice" runat="server" Width="430px">
                                                            <asp:ListItem Value="1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Second Course of Choice
                                                    </td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:DropDownList ID="cmb2ndChoice" runat="server" Width="427px">
                                                            <asp:ListItem Value="1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td class="colorheader" colspan="2">
                                                        Select two preferred Teaching Subject (Select [Not Applicable] if not applying to
                                                        Faculty of Education)
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        First Teaching Subject Of Choice
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbTeachingSub1" runat="server" Width="427px">
                                                            <asp:ListItem Value="1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                        Second Teaching Subject Of Choice
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbTeachingSub2" runat="server" Width="427px">
                                                            <asp:ListItem Value="1">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td class="style28">
                                                        Third Course of Choice</td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:DropDownList ID="cmb3rdChoice" runat="server" Width="427px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="colorheader">
                                                        <asp:DropDownList ID="cmbIsPrevious" runat="server" AutoPostBack="True" 
                                                            Height="16px" OnSelectedIndexChanged="cmbIsPrevious_SelectedIndexChanged" 
                                                            style="text-align: right" Width="16px" Visible="False">
                                                            <asp:ListItem>No</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="colorheader">
                                                        &nbsp;</td>
                                                    <td style="text-align: right">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr ID="trPreviousInfo" runat="server" visible="false">
                                                    <td colspan="3">
                                                        <table cellpadding="2" cellspacing="2" class="tablepersonal">
                                                            <tr>
                                                                <td class="style28">
                                                                    Previous Matric Number
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPreviousRegNo" runat="server" CssClass="text-normal" 
                                                                        Width="127px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style28">
                                                                    Previous Programme
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cmbPreviousCourse" runat="server" Width="430px">
                                                                        <asp:ListItem Value="1">Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style28" colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td width="25%">
                                                                                Period Attended
                                                                            </td>
                                                                            <td width="35%">
                                                                                <asp:DropDownList ID="cmbPreviousYearFrom" runat="server" Width="243px">
                                                                                    <asp:ListItem Value="1">From</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="5%">
                                                                            </td>
                                                                            <td width="35%">
                                                                                <asp:DropDownList ID="cmbPreviousYearTo" runat="server" Width="243px">
                                                                                    <asp:ListItem Value="1">To</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td class="style28">
                                                        Pick Location For Lecture:
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdbTeachingCenter" runat="server" Width="427px" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Achiever’s University Owo</asp:ListItem>
                                                            <asp:ListItem Value="2">Ibadan</asp:ListItem>
                                                            <asp:ListItem Value="3">Lagos</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>--%>
                                                <tr id="t" runat="server">
                                                    <td class="style1">
                                                        &nbsp;</td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                    
                                                       
                                                         &nbsp;</td>
                                                </tr>
                                                <tr ID="trExamsubj" runat="server">
                                                    <td class="style1">
                                                        Choose Entrance Exam Subjects:</td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                        <b>Preferred Entrance Examination Centre : </b>
                                                    </td>
                                                </tr>
                                                <tr ID="trExamCenter" runat="server">
                                                    <td class="style56" style="vertical-align: top; text-align: left;">
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:DropDownList ID="cmbEntranceExamSubj" runat="server">
                                                                                                                                         </asp:DropDownList>
                                                                </td>
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
                                                            <tr title="choose the subjects relevant to your choice of programme as stated in these 4 categories">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">1.</span><span class="style44"><span 
                                                                        style="font-size: 7.0pt; line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">Mathematics 
                                                                    and Physics</span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">:</span></span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><br />
                                                                    -</span><span 
                                                                        
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; " 
                                                                        class="style72">to be choosen by candidates for Petroleum Engineering, 
                                                                    Mineral Resources Engineering, Mechanical Engineering, Electrical/Electronic 
                                                                    Engineering , Welding and Fabrication, under Water Operations Engineering 
                                                                    Programmes</span><span 
                                                                        class="style70" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; ">
                                                                    </span><span class="style55" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; color: #006633;">
                                                                    <br />
                                                                    <br />
                                                                    </span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA" class="style55">
                                                                    2.</span></span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA" class="style55"><span class="style44">Mathematics and Chemistry:</span></span></span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"> </span><span 
                                                                        
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; color: #006633;">
                                                                    <br class="style55" />
                                                                    </span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA" class="style55">-</span></span><span class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style71" style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style72" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; ">to 
                                                                    be choosen by candidates</span> for <span style="mso-spacerun:yes">&nbsp;</span>Petroleum 
                                                                    and Natural Gas Processing Technology, Science Laboratory Technology and 
                                                                    Industrial safety progammes </span>
                                                                    </span>
                                                                    <span class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">
                                                                    <br />
                                                                    <br />
                                                                    3.</span></span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style44">Mathematics and Business Studies:</span></span></span><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><br class="style55" />
                                                                    </span>
                                                                    <span class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">-</span><span class="style72" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style72" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; ">to 
                                                                    be choosen by candidates</span> for Petroleum Marketing and Business Studies</span><span 
                                                                        class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">
                                                                    <br />
                                                                    <br />
                                                                    </span>
                                                                    <span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span class="style55" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">4.</span></span><span 
                                                                        style="font-size: 7.0pt; line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                        class="style55" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                        class="style44">Mathematics and Physics:</span></span></span><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><br class="style55" />
                                                                    </span>
                                                                    <span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><span style="font-size: 7.0pt; line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                                                                    <span class="style55" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">
                                                                    -</span></span></span><span style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA; font-family: Tahoma, sans-serif;"><span 
                                                                        style="font-size: 7.0pt; line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                        class="style72" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                        class="style72" 
                                                                        style="font-size: 7.0pt; line-height: 115%; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA; ">to 
                                                                    be choosen by candidates</span> for </span></span></span>
                                                                    <span class="style72" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">General Welding<br />
                                                                    <br />
                                                                    </span>
                                                                    <span class="style74" style="font-size:7.0pt;line-height:115%;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">Any candidate who fails to answer questions in accordance with his or her choice of discipline as stated above will 
                                                                    automatically be disqualified.</span><span style="font-size:7.0pt;line-height:115%;
font-family:&quot;Tahoma&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;mso-fareast-theme-font:
minor-latin;color:#006633;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA"><br />
                                                                    </span></td>
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
                                                                    <table class="style45">
                                                                        <tr>
                                                                            <td class="style46">
                                                                                G<span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA"><span 
                                                                                    style="line-height: 115%; font-family: &quot;Tahoma&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: Calibri; mso-fareast-theme-font: minor-latin; color: #006633; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">eneral 
                                                                                Paper:</span></span></span></span></span></span></span></td>
                                                                            <td>
                                                                                <asp:Label ID="lblGebSubj" runat="server" Text="ENGLISH LANGUAGE" 
                                                                                    ToolTip="The General Paper ( English, is compulsary for all candidates)"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
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
                                                    </td>
                                                    
                                                    <td class="style56">
                                                        </td>
                                                    <td class="style56">
                                                        <asp:RadioButtonList ID="rdbExamCenter" runat="server" 
                                                            CssClass="radioWithProperWrap" Height="218px" 
                                                            ToolTip="Choose One Exam Center For: DIPLOMA, PRE-DIPLOMA, GENERAL WELDING CERTIFICATE" 
                                                            Width="413px">
                                                            <asp:ListItem Value="1">PTI Effurun, Warri</asp:ListItem>
                                                            <asp:ListItem Value="2">Nigeria Navy Sec Sch,Ojo, Lagos</asp:ListItem>
                                                            <asp:ListItem Value="3">NNPC Staff Sch(within NNPC Housing Complex)Kadunna</asp:ListItem>
                                                            <asp:ListItem Value="4">Army Day Sec. Sch,Bori Camp, PortHarcourt</asp:ListItem>
                                                            <asp:ListItem Value="5">Command Day Sec. Sch, Enugu</asp:ListItem>
                                                            <asp:ListItem Value="6">Goverment Sec. Sch, Tudun Wada, Wuse Zone 4, Abuja</asp:ListItem>
                                                            <asp:ListItem Value="7">Ramat Polytechnic, Maiduguri</asp:ListItem>
                                                            <asp:ListItem Value="8">Adamawa State Polytechnic, Yola</asp:ListItem>
                                                            <asp:ListItem Value="9">Sokoto Teachers College (STC), Sokoto</asp:ListItem>
                                                            <asp:ListItem Value="10">I am HND Applicant.Only Interview is Applicable</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style28">
                                                    </td>
                                                    <td class="style28">
                                                        &nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnChoiceSaveContinue" runat="server" 
                                                            OnClick="btnChoiceSaveContinue_Click" Text="Save &amp; Continue" />
                                                        <asp:Button ID="btnChoiceContinueLater" runat="server" 
                                                            OnClick="btnChoiceContinueLater_Click" Text="Continue Later" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwEducationalDetails" runat="server">
                <table align="center" class="style2">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Panel ID="PanelEntryQual" runat="server">
                                <table class="style1">
                                    <tr>
                                        <td class="blackheader">
                                            EDUCATIONAL BACKGROUND:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <table class="tablepersonal">
                                                <tr>
                                                    <td>
                                                        <table cellpadding="2" cellspacing="2" class="tablepersonal">
                                                            <tr>
                                                                <td class="style28" align="center">
                                                                    <asp:GridView ID="grdvwEducation" Width="100%" runat="server" BackColor="#CCCCCC"
                                                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                                                                        ForeColor="Black" AutoGenerateColumns="False" OnRowCommand="grdvwEducation_RowCommand"
                                                                        OnRowEditing="grdvwEducation_RowEditing">
                                                                        <RowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Sitting" HeaderText="Seating" />
                                                                            <asp:BoundField DataField="ExamName" HeaderText="Exam Name" />
                                                                            <asp:BoundField DataField="ExamRegNo" HeaderText="Exam Number" />
                                                                            <asp:BoundField DataField="ExamDate" HeaderText="Exam Year" />
                                                                            <asp:ButtonField CommandName="Edit" Text="Edit" />
                                                                            <asp:ButtonField CommandName="Delete" Text="Delete" />
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#CCCCCC" />
                                                                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="blue" Font-Bold="True" ForeColor="White" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Select your Qualifications Below to Add
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table class="style18" enableviewstate="true">
                                                            <tr valign="top">
                                                                <td colspan="4" style="text-align: center">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style47">
                                                                    Exam Name
                                                                </td>
                                                                <td class="style19" colspan="2">
                                                                    <asp:DropDownList ID="cmbExam" runat="server" CssClass="text-normal" Width="184px"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="cmbExam_SelectedIndexChanged" 
                                                                        Height="16px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style19">
                                                                    Exam Year
                                                                    <asp:DropDownList ID="cmbExamDate" runat="server" CssClass="text-normal" Width="75px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style19" colspan="2">
                                                                    Candidate's Exam No
                                                                </td>
                                                                <td class="style48">
                                                                    <asp:TextBox ID="txtExamNo" runat="server" CssClass="text-normal" Width="126px"></asp:TextBox>
                                                                </td>
                                                                <td class="style19">
                                                                    Seating Type<asp:DropDownList ID="cmbSeating" runat="server" CssClass="text-normal"
                                                                        Width="75px" AutoPostBack="True" OnSelectedIndexChanged="cmbSeating_SelectedIndexChanged">
                                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">First</asp:ListItem>
                                                                        <asp:ListItem Value="2">Second</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trEntrySubjects" runat="server">
                                                                <td colspan="4">
                                                                    <table class="style18" enableviewstate="true">
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <b>Subject</b>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <b>Grade</b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center" enableviewstate="true">
                                                                                <asp:DropDownList ID="CmbSittingSubj1" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center" enableviewstate="true">
                                                                                <asp:DropDownList ID="CmbSittingGrade1" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center" enableviewstate="true">
                                                                                <asp:DropDownList ID="CmbSittingSubj2" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center" enableviewstate="true">
                                                                                <asp:DropDownList ID="CmbSittingGrade2" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj3" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade3" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj4" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade4" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj5" runat="server" Width="250px">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade5" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj6" runat="server" Width="250px">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade6" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj7" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade7" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj8" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade8" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj9" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade9" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingSubj10" runat="server" Width="250px">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:DropDownList ID="CmbSittingGrade10" runat="server">
                                                                                    <asp:ListItem> </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td style="text-align: center" colspan="2">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td width="45%" align="right">
                                                                                            Upload Certificate
                                                                                        </td>
                                                                                        <td width="55%" align="left">
                                                                                            <asp:FileUpload ID="fupResulst" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                <asp:TextBox ID="txtOtherqual" runat="server" Width="239px" 
                                                                                    ToolTip="other qualifications" Visible="False"></asp:TextBox>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                            <td style="text-align: center">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="text-align: center">
                                                        <asp:Button ID="btnAddnew" Text="Save &amp; Add New" runat="server" 
                                                            OnClick="btnAddnew_Click" Visible="False" />
                                                        <asp:Button ID="btnAddContinue" runat="server" Text="Save &amp; Continue" OnClick="btnAddContinue_Click" />
                                                        <asp:Button ID="btnAddContinueLater" runat="server" Text="Save &amp; Continue Later"
                                                            OnClick="btnAddContinueLater_Click" />
                                                    </td>
                                                    <td valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style28">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwPostEducationDetails" runat="server">
                <table align="center" class="style2">
                    <tr>
                        <td>
                            <table cellpadding="2" cellspacing="2" class="tablepersonal">
                                <tr>
                                    <td class="style28">
                                        Qualification
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPostQual" runat="server">
                                            <asp:ListItem>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:ListItem>
                                            <asp:ListItem>DIPLOMA</asp:ListItem>
                                            <asp:ListItem>CERTIFCATE</asp:ListItem>
                                            <asp:ListItem>ORDINARY NATIONAL DIPLOMA</asp:ListItem>
                                            <asp:ListItem>BACHELORS DEGREE</asp:ListItem>
                                            <asp:ListItem>HIGHER NATIONAL DIPLOMA</asp:ListItem>
                                            <asp:ListItem>MBA</asp:ListItem>
                                            <asp:ListItem>MASTERS (Msc, Mphil..)</asp:ListItem>
                                            <asp:ListItem>DOCTORATE/PHD</asp:ListItem>
                                            <asp:ListItem>OTHERS</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        School
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostschool" runat="server" CssClass="text-normal" Width="445px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        Month/Year
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostYear" runat="server" CssClass="text-normal" Width="205px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        Exam./Matric No.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostMatNo" runat="server" CssClass="text-normal" Width="205px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        Discipline
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostCourse" runat="server" CssClass="text-normal" Width="205px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        Grade:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPostGrade" runat="server" CssClass="text-normal" Width="205px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddPostContinue" runat="server" OnClick="btnAddPostContinue_Click"
                                Text="Save &amp; Continue" />
                            <asp:Button ID="btnAddPostContinueLater" runat="server" OnClick="btnAddPostContinueLater_Click"
                                Text="Save &amp; Continue Later" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwAttestation" runat="server">
                <table align="center" class="style2">
                    <tr>
                        <td width="35%" align="left" colspan="2" style="width: 100%">
                            <table id = "tabthreeReferees" runat ="server" visible="false" class="style49">
                                <tr>
                                    <td class="style66" colspan="2">
                                        Work Experience</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style54">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style61">
                                        <span class="style67">Present Employer</span>:</td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtpresEmployer" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style60">
                                        Position:</td>
                                    <td class="style63">
                                        <asp:TextBox ID="txtposition" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style64">
                                        Date Apointed:</td>
                                    <td class="style65">
                                        <asp:TextBox ID="txtDateEmployed" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style66" colspan="2">
                                        Three Referees</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style54">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style58">
                                        1<span class="style67">.Name Of Ref1</span></td>
                                    <td class="style52">
                                        <asp:TextBox ID="txtName1" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style68">
                                        Ref1 Address</td>
                                    <td class="style54" colspan="2">
                                        <asp:TextBox ID="txtAddress1" runat="server" Width="378px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style58">
                                        2<span class="style67">.Name Of Ref2</span></td>
                                    <td class="style52">
                                        <asp:TextBox ID="txtName2" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style68">
                                        Ref2 Address</td>
                                    <td class="style54" colspan="2">
                                        <asp:TextBox ID="txtAddress2" runat="server" Width="378px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style58">
                                        3.<span class="style67">Name Of Ref3</span></td>
                                    <td class="style52">
                                        <asp:TextBox ID="txtName3" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="style68">
                                        Ref3 Address</td>
                                    <td class="style54" colspan="2">
                                        <asp:TextBox ID="txtAddress3" runat="server" Width="379px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style50">
                                        &nbsp;</td>
                                    <td class="style51">
                                        &nbsp;</td>
                                    <td class="style52">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style54">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="35%">
                            <span class="style67">Referral: (How did you hear about us?</span><span 
                                class="style67">) </span>
                        </td>
                        <td align="left" width="65%">
                            <asp:DropDownList ID="cmbRefAll" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="cmbRefAll_SelectedIndexChanged">
                                <asp:ListItem>Please Select</asp:ListItem>
                                <asp:ListItem>SMS Adverts</asp:ListItem>
                                <asp:ListItem>Email Alerts</asp:ListItem>
                                <asp:ListItem>Print Media</asp:ListItem>
                                <asp:ListItem>Radio/TV Adverts</asp:ListItem>
                                <asp:ListItem>PTI Website</asp:ListItem>
                                <asp:ListItem>Social Networking Websites(FaceBook, Twitters etc.)</asp:ListItem>
                                <asp:ListItem>Partners</asp:ListItem>
                                <asp:ListItem>Not Specified</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trForPartners" runat="server" visible="false">
                        <td width="35%" align="left">
                            <span class="style67">Which of the Partners?</span>
                        </td>
                        <td width="65%" align="left">
                            <asp:DropDownList ID="cmbRefPartners" runat="server" Width="100%" AutoPostBack="True"
                                OnSelectedIndexChanged="cmbRefPartners_SelectedIndexChanged">
                                <asp:ListItem>Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trPartnersDetail" runat="server" visible="false">
                        <td colspan="2">
                            <asp:Label ID="lblPartnerDetails" runat="server">Details of Selected Partners</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="colorheader">
                            DECLARATION:
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            I <span class="style67">herby declare:<ul>
                                <li class="style57">That the information supplied in this Form is to the best of my 
                                    knowledge and belief, accurate in every detail; and</li>
                                <li class="style57">if any time it is discovered that the information I have given 
                                    is false or incorrect, I will be </li>
                                <li class="style57">required to withdraw or liable to prosecution or both; and</li>
                                <li class="style57">that if I am admitted, I shall keep the&nbsp; rules and regulations 
                                    of the Petroleum Training&nbsp; Institute</li>
                            </ul>
                            </span></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="I agree" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Style="font-weight: 700"
                                Text="Click here to Apply" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        <table align="center" class="style2">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lbltitleError" runat="server" Font-Bold="True" ForeColor="#CC3300"
                        Text="lblerror" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblEntryError1"
                            runat="server" Font-Bold="True" ForeColor="#CC3300" Text="lblerror" Visible="False"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
