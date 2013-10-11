using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;
using System.IO;
using System.Data.SqlClient;
//using log4net;
//using log4net.Config;  

public partial class Admin_StudentDetails : System.Web.UI.Page
{
    string msg = "";
    private static string str = ConfigurationManager.AppSettings["conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Role Here

    }
    protected void btnSearchParam_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchParameter.Text))
        {
            
            msg = "Please enter Form or Matric Number to Search."; ;
            showmassage(msg);
            tbDetails.Visible = false;
            return;
        }
        //lblMessage.Text = "";
        string thisQuery = "SELECT [MatricNumber] [Matric No],[Surname] + ' ' +[OtherNames] [Name] ,[Sex] ,[DateOfBirth] [DOB],isnull([MaritalStatus],'') [Marital],isnull([Nationality],'') [Nationality]";
        thisQuery += ",isnull([State],'') [State]  ,isnull([LocalGovernmentArea],'') [LGA]  ,isnull([HomeAddress],'') [Address]";
        thisQuery += ",[Email] ,[PhoneNumber],[AcademicLevel] [Level],[RegNo] [Form Number],[CourseOfStudyName] [Program] ,[AdmittedSession] [Admitted Session]";
        thisQuery += ",[Programme],[Duration] ,isnull([Honours],'') [Honours] ,[ModeOfStudy] [Mode of Study],[PresentSession] [Current Session]";
        thisQuery += ",isnull([Title],'') [Title] ,Case [isNewStudent] when '1' then 'YES' else 'NO' end [New Student] ,[EntryMode] [Entry Mode]";//, Case rtrim(ltrim((Isnull(St.[MatricNumber],'')))) When '' THEN 'FALSE' ELSE 'TRUE' END [Offer Accepted]
        thisQuery += ",[TeachingSubject] [Teaching Subject]  FROM [Students] where matricnumber='" + txtSearchParameter.Text.Trim() + "' OR [RegNo]='"+ txtSearchParameter.Text.Trim() + "'";
        // Join Students St On Al.RegNo=St.RegNo 
        DataSet dtAdmitted = SearchData(thisQuery);

        if (dtAdmitted ==null | dtAdmitted.Tables[0].Rows.Count < 1)
        {
            msg = "The Form/Matric Number Enter does not exist.";
            tbDetails.Visible = false;
            showmassage(msg);
            return;
        }
        string pQuery = "Select case isnull(Passportfile,'') when '' then 'http://www.portal.dlc.ui.edu.ng/picx/blank.png' else Passportfile end as PassportFile From Students where matricnumber='" + txtSearchParameter.Text.Trim() + "' OR [RegNo]='" + txtSearchParameter.Text.Trim() + "'";
        passPort.ImageUrl = SearchData(pQuery).Tables[0].Rows[0][0].ToString();

        tbDetails.Visible = true;
        GridView1.Width = 1000;
        GridView1.DataSource = dtAdmitted.Tables[0];
        GridView1.DataBind();
    }
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
        //Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        //Page.Controls.Add(lbl);
    }
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);
            dat.Fill(ds);

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception e)
        {
            string ex = e.Message;
        }
        return ds;
    }
}
