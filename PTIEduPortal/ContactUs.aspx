<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" Title="Contact Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .topfont
        {
            padding-top: 0px;
            font-family: tahoma;
            font-size: 12px;
            color: #060;
            font-weight: normal;
        }
        .redfont
        {
            padding-top: 0px;
            font-family: tahoma;
            font-size: 12px;
            color: #F00;
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="span12">
        <h2 class="alert-success lead">
            &nbsp; Contact Details
        </h2>
        
        <ul>
            
             <li>
             
                <ul >
                     <b>Address </b>
                     <li>Tel: 08032640578</li>
                      <li>Email: <a href="mailto:info@pti.edu.ng">info@pti.edu.ng </a></li>
                </ul>
         </li>
           
         <li>
             
                <ul >
                     <b>Genral Enquiries; </b>
                     <li>Tel: 08032640578</li>
                      <li>Email: <a href="mailto:info@pti.edu.ng">info@pti.edu.ng </a></li>
                </ul>
         </li>
          <br/>
            <li>
                <ul >
                     <b>For Admission Enquiries; </b>
                     <li>Tel: 08032640578</li>
                       <li>Email: <a href="mailto:admission@pti.edu.ng">admission@pti.edu.ng </a></li>
                     
                </ul>
               
            
            </li>
              <br/>
             <li>
                <ul >
                     <b>For Portal Complaints; </b>
                     <li>Tel: 08126971127, 08052742813</li>
                     <li>Email: <a href="mailto:itsupport@pti.edu.ng">itsupport@pti.edu.ng </a></li>
                    
                </ul>
               
            
            </li>
             <br/>
           <li>
                All Calls must be between:  Mon-Fri,
            9am-4pm.
           </li>
           
        </ul>
        <asp:SqlDataSource ID="drscNews" runat="server" ConnectionString="<%$ ConnectionStrings:PTIEduportalConnectionString %>"
            SelectCommand="SELECT * FROM [NewsItems] WHERE ([NewsId] = @NewsId)">
            <SelectParameters>
                <asp:QueryStringParameter Name="NewsId" QueryStringField="NewsId" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
