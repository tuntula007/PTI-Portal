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

public partial class Admin_ConfirmationOrderStatus : System.Web.UI.Page
{
    string msg = "";
    private static string Key = ConfigurationManager.AppSettings["CyberKey"];

    private static string str = ConfigurationManager.AppSettings["conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Role Here

    }
    protected void btnSearchParam_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchParameter.Text))
        {
            msg = "Please enter a confirmation order number to Search.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        if (rdbtncontype.SelectedIndex < 0)
        {
            msg = "Please the fee type (School Fees or Application Form).";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }

        string thisQuery = "SELECT [PinNumber],[PinStatus],[UsedBy],[UsedDate],[SessionName] ,[AcademicLevel],[Programme],[Amount],[Faculty] FROM [" + rdbtncontype.SelectedValue + "] where [PinNumber]='";
        byte[] buffer = CyberEncryptor.encypt(txtSearchParameter.Text.Trim(), Key);


        string encrypPin = Convert.ToBase64String(buffer);
        thisQuery += encrypPin + "'";

        DataSet dtRecord = SearchData(thisQuery);
        if (dtRecord == null | dtRecord.Tables[0].Rows.Count < 1)
        {
            msg = "The Confirmation Order Number Entered does not exist.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }

        if (dtRecord.Tables[0].Rows[0][1].ToString() == "0")
        {
            msg = "The Confirmation Order Number Entered exist and have not been used.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        if (dtRecord.Tables[0].Rows[0][1].ToString() == "1")
        {
            msg = (dtRecord.Tables[0].Rows[0][2].ToString().ToLower().Contains("zenith")) ?
                "The Confirmation Order Number Entered exist and but is blocked!" :
                "The Confirmation Order Number was used by " + dtRecord.Tables[0].Rows[0][2].ToString() + " on " + dtRecord.Tables[0].Rows[0][3].ToString();
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
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
