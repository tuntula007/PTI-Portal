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

public partial class Admin_StudentPaymentStatus : System.Web.UI.Page
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
        
        string thisQuery = "select st.surname + ' ' + st.othernames [Name],sp.MatricNumber [Matric Number],st.regno [Form Number], st.courseofstudyname [Program of Study], sp.paymenttype [Payment Type],Cast(sp.pinvalue as varchar) [Amount paid],[Session]";
        thisQuery += "from studentpayment sp join students st on sp.matricnumber=st.matricnumber where st.matricnumber='" + txtSearchParameter.Text.Trim() + "'OR st.[RegNo]='" + txtSearchParameter.Text.Trim() + "'";
        DataSet dtRecord = SearchData(thisQuery);
        if (dtRecord ==null | dtRecord.Tables[0].Rows.Count < 1)
        {
            msg = "The Form/Matric Number Entered does not have any payment records.";
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
