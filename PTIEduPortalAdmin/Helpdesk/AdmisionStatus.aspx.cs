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
using System.Data.SqlClient;
//using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;
//using System.IO;
//using log4net;
//using log4net.Config;  

public partial class Admin_AdmissionStatus : System.Web.UI.Page
{
    //SignOnBusiness sb = new SignOnBusiness();
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
            msg = "Please enter Applicant's Form Number to Search.";
            showmassage(msg);
            GridView1.Visible = false;
            return;            
        }
        //lblMessage.Text = "";
        string thisQuery = "SELECT Al.[RegNo] [Form Number] ,Al.[Surname] +' ' +Al.[OtherNames] [Name],Al.[Sex] ,Al.[DateOfBirth] [DOB],Al.[Address],";
        thisQuery += "Al.[AcademicLevel] [Level] ,Al.[AdmittedSession] [Session],Al.[Programme],Al.[Faculty],Al.[Department],Al.[CourseOfStudy] [Program Of Study] ,Al.[Duration],Al.[ModeOfStudy] [Mode Of Study]";
        thisQuery += ",Al.[EntryMode] [Entry Mode],Al.[Email] ,Al.[Phone],case ltrim(rtrim(isnull(Al.[TeachingSubject],''))) when '' then 'N/A' else ltrim(rtrim(isnull(Al.[TeachingSubject],''))) end [Teaching Subject]";
        thisQuery += ",Case Al.[IsAdmitted] when 1 then 'Admitted' else 'PENDING' end [Admission Status] ,Al.[BeginDate] [Admitted On]";
        thisQuery += ",Al.[AdmittedBy] [Uploaded By],Al.[Batch], Case rtrim(ltrim((Isnull(St.[MatricNumber],'')))) When '' THEN 'FALSE' ELSE 'TRUE' END [Offer Accepted]";
        thisQuery += "FROM [AdmissionList] Al  left join [Students] st on Al.regno=st.regno WHERE Al.[RegNo]='" + txtSearchParameter.Text.Trim() + "'";
        // Join Students St On Al.RegNo=St.RegNo 
        DataSet dtAdmitted = SearchData(thisQuery);// sb.SelectQuery(thisQuery);
        if (dtAdmitted.Tables[0].Rows.Count < 1)
        {
            msg = "The Student does not have any admission records.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        GridView1.Visible = true;
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
