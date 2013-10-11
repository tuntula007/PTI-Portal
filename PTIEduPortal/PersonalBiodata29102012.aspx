<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="PersonalBiodata.aspx.cs" Inherits="PersonalBiodata" Title="PTI Portal  - Personal Data Form"
    MaintainScrollPositionOnPostback="true" %>

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
        .style31
        {
            width: 65px;
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
                                        <td style="text-align: left; font-family: Times New Roman; font-size: 22px; border-bottom: dashed 1px gray">
                                            <b>Personal Data Form</b>
                                        </td>
                                        <td align="right">
                                            <i>
                                                <asp:Label ID="Sessions" runat="server" Text="2011/2012 Session"></asp:Label></i>
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
                                        <td align="left" class="colorheader" colspan="2">
                                            Important Notice
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2" class="smalltext">
                                            1. This page is to enable you update some of your personal information.<br />
                                            2. Only information fields required are enable for entry.
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style41">
                                        </td>
                                        <td colspan="2" class="style41">
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
                                                        <b>FACULTY</b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>PROGRAMME OF STUDY</b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>LEVEL</b>
                                                    </td>
                                                    <td class="style42">
                                                        <b>MATRIC NO.</b>
                                                    </td>
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
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2" class="colorheader">
                                            Make sure you&nbsp; click on UPDATE Button below to finish the update
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="PanelPersonalDetail" runat="server">
                                                <table class="style1">
                                                    <tr>
                                                        <td class="blackheader">
                                                            Personal Detail
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
                                                                            <asp:ListItem>Engr.</asp:ListItem>
                                                                            <asp:ListItem>Prof.</asp:ListItem>
                                                                            <asp:ListItem>Rev.</asp:ListItem>
                                                                            <asp:ListItem>Rev. Mrs.</asp:ListItem>
                                                                            <asp:ListItem>St</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Surname
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtSurname" runat="server" CssClass="text-normal" Width="153px"
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Othernames
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtOthernames" runat="server" CssClass="text-normal" Width="153px"
                                                                            Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Maiden name (If married Female)
                                                                    </td>
                                                                    <td colspan="3" class="style27">
                                                                        <asp:TextBox ID="txtMaiden" runat="server" CssClass="text-normal" Width="153px" Enabled="False"></asp:TextBox>
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
                                                                        <asp:DropDownList ID="cmbMonth" runat="server">
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
                                                                    <td class="style31">
                                                                        Sex
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbSex" runat="server">
                                                                            <asp:ListItem></asp:ListItem>
                                                                            <asp:ListItem>Male</asp:ListItem>
                                                                            <asp:ListItem>Female</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Nationality
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:DropDownList ID="cmbNation" runat="server" Enabled="False">
                                                                            <asp:ListItem>Nigerian</asp:ListItem>
                                                                            <asp:ListItem>Non-Nigerian</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style29">
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
                                                                </tr>
                                                                <tr>
                                                                    <td class="style29">
                                                                        State
                                                                    </td>
                                                                    <td colspan="3" class="style30">
                                                                        <asp:DropDownList ID="cmbState" runat="server" Enabled="True" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="cmbState_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                        Permanent Home Address (Not Postal Address)
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="text-normal" Width="243px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style34">
                                                                        Email Address
                                                                    </td>
                                                                    <td colspan="3" class="style35">
                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="text-normal" Width="127px" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Phone Number
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap="nowrap">
                                                                        <span><span>Next of Kin Name:</span></span><span><span> </span></span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="TxtNextKinName" runat="server" SkinID="mediumTxt"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap="nowrap">
                                                                        <span><span>Next of Kin Address:</span></span><span><span> </span></span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="TxtNextOfKinAddres" runat="server" SkinID="mediumTxt" TextMode="MultiLine"
                                                                            TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap="nowrap">
                                                                        <span><span>Next of Kin Phone:</span></span><span><span> </span></span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="TxtNextOfKinPhone" runat="server" SkinID="mediumTxt" TabIndex="2"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap="nowrap">
                                                                        <span><span>Next of Kin Relationship:</span></span><span><span> </span></span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="TxtNextOfKinRela" runat="server" SkinID="mediumTxt" TabIndex="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap="nowrap">
                                                                        <span><span>Next of Kin Email:</span></span><span><span> </span></span>
                                                                    </td>
                                                                    <td nowrap="nowrap">
                                                                        <asp:TextBox ID="TxtNextmail" runat="server" SkinID="mediumTxt" TabIndex="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        Hall of Residence/Hostel
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHall" runat="server" CssClass="text-normal" Width="127px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="style31">
                                                                        Room No
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRoom" runat="server" CssClass="text-normal" Width="75px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td colspan="3">
                                                                        (Write &quot;L.O.&quot; if living outside)
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <asp:Panel ID="PanelProgramDetail" runat="server">
                                                <table class="style1">
                                                    <tr>
                                                        <td class="blackheader">
                                                            Program Detail
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <table class="tablepersonal" cellpadding="2" cellspacing="2">
                                                                <tr>
                                                                    <td class="style28">
                                                                        Please Choose One:</td>
                                                                    <td colspan="3">
                                                                        <asp:RadioButtonList ID="rdbTeachingCenter" runat="server" Width="427px" 
                                                                            RepeatDirection="Horizontal" Height="26px">
                                                                            <%--<asp:ListItem Value="1">Achiever’s University Owo</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Nigerian</asp:ListItem>
                                                                            <asp:ListItem Value="3">Non-Nigerian</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trExamCenter" runat="server">
                                                                    <td class="style28">
                                                                        Please Choose One:</td>
                                                                    <td colspan="3">
                                                                        <asp:RadioButtonList ID="rdbExamCenter" runat="server" Width="427px" RepeatDirection="Horizontal">
                                                                            <%--<asp:ListItem Value="1">Achiever’s University Owo</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Born in Delta State</asp:ListItem>
                                                                            <asp:ListItem Value="3">Not Born in Delta State</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lbltitleError" runat="server" Font-Bold="True" ForeColor="#CC3300"
                                                Text="lblerror" Visible="False"></asp:Label>
                                            &nbsp;<asp:Label ID="lblEntryError1" runat="server" Font-Bold="True" ForeColor="#CC3300"
                                                Text="lblerror" Visible="False"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Style="font-weight: 700"
                                                Text="Click here to Update" Font-Size="Large" />
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
</asp:Content>
