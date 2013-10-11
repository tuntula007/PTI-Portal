<%@ Page Language="C#" MasterPageFile="~/MasterPageform.master" AutoEventWireup="true"
    CodeFile="MedicalStatus.aspx.cs" Inherits="MedicalStatus" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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
        .style43
        {
            font-weight: normal;
            width: 174px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td align="left">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LabelWelcome" runat="server" Font-Bold="True" Font-Size="Large" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LabelAppNo" runat="server" Font-Bold="True" Font-Size="Large" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Emergency Contact Person :
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Contact person:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="text-normal" Width="289px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        Address Of Contact:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtContactAddress" runat="server" CssClass="text-normal" Width="376px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        Phone of Contact
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhoneContact" runat="server" CssClass="text-normal" Width="376px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Health Status
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDListHealthStatus" runat="server">                                            
                                            <asp:ListItem>Good</asp:ListItem>
                                            <asp:ListItem>Fair</asp:ListItem>
                                            <asp:ListItem>Poor</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Ever Been Admitted as an in-patient in Hospital?
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDListAdmitted" runat="server">
                                            <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        If so, state reason for admission, name of hospital and date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdmitted" runat="server" CssClass="text-normal" Width="600px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Are you on any medication(s)?
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDListMedication" runat="server" AppendDataBoundItems="True">
                                           <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        If so, state drug and dosage
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtMedication" runat="server" CssClass="text-normal" Width="550px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                border-bottom-style: solid; border-bottom-color: Black; border-bottom-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Do you suffer from or have suffered from any of the following?
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    A. Tuberculosis
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease1" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    F. Diabeties
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease6" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B. Schistosomaisis
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease2" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    G. Any disease of digestive system
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease7" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    C. Any respiratory disease
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease3" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    H. Any disease of the heart
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease8" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    D. Cickle cell disease
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease4" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    I. Any genitor-uninary system
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease9" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    E. Allergies
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease5" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    J. Nervous disease
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListDesease10" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        If the answer to the above is yes, please give details
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDeseas" runat="server" CssClass="text-normal" Width="550px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            If there is any other relevant details of your medical history not covered by the
                            questions, please give
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TxtOtherDetails" runat="server" CssClass="text-normal" Width="700px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            6. Travel history with date
                        </td>
                        <td>
                            <asp:TextBox ID="TxtTravelHistory" runat="server" CssClass="text-normal" Width="550px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Is your family a healthy one
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDlistFamilyHealthy" runat="server">
                                            <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        Has any one of your family member suffered from any of the following?
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    A. Tuberculosis
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListFamilyDesease1" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    C. Hypertension
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListFamilyDesease3" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B. Diabeties
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListFamilyDesease2" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    D. Mental illness
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListFamilyDesease4" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43" nowrap="nowrap">
                                        Do you react to any drug(s)?
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDListDrugReact" runat="server">
                                            <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        If yes, state drugs
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDrugReact" runat="server" CssClass="text-normal" Width="376px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="border-top-style: solid; border-top-color: Black; border-top-width: thin;
                                width: 100%">
                                <tr>
                                    <td class="style43">
                                        Have you been immunized against any of the following?
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    A. Hepatitis
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListImunization1" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    C. Yellow fever
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListImunization3" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B. Tetanus
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListImunization2" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    D. Cerebro spinal meningitis
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDListImunization4" runat="server">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style43">
                            &nbsp;
                        </td>
                        <td>
                        <table>
                        <tr>
                        <td>
                         <asp:Button ID="BtnBiodataSave" runat="server" Text="Save" Width="50px" OnClick="BtnBiodataSave_Click" />
                        </td>
                        <td>
                         <asp:Button ID="BtnRetrieve" runat="server" Text="Retriev Data" Width="100px" 
                                OnClick="BtnRetrieve_Click" />
                        </td>
                        <td>
                         <asp:Button ID="BtnPrint" runat="server" Text="Print Form" Width="100px" 
                                OnClick="BtnPrint_Click" />
                        </td>
                        </tr>
                        </table>
                        </td>
                           
                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
