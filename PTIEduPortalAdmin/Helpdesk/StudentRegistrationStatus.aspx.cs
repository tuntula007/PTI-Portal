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

public partial class Admin_StudentRegistrationStatus : System.Web.UI.Page
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
            msg = "Please enter Form or Matric Number to Search.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        
        string thisQuery = "select ExamRegCode [Exam Code],matricnumber [Matric Number],regno [Form Number],[sessionname] [Session], count(regno) [No of Courses],";
        thisQuery += "sum(cast(courseunit as int)) [Total Unit Load] from courseregistration where matricnumber='" + txtSearchParameter.Text.Trim() + "'OR [RegNo]='" + txtSearchParameter.Text.Trim() + "'";
        thisQuery += "group by [sessionname],ExamRegCode,regno, matricnumber order by [sessionname]";
        DataSet dtRecord = SearchData(thisQuery);
        if (dtRecord ==null | dtRecord.Tables[0].Rows.Count < 1)
        {
            msg = "The Form/Matric Number Entered does not have any registration records.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        GridView1.Visible = true;
        GridView1.Width = 1000;
        GridView1.DataSource = dtRecord.Tables[0];
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
