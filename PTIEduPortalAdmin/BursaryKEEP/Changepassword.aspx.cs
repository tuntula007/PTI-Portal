using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Messaging;
//using Cyberspace.ServiceBrocker;


public partial class Changepassword : System.Web.UI.Page
{
   
    private string str = ConfigurationManager.AppSettings["conn"];
    private static string msg = "";
    private static string ID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Page.User.Identity.IsAuthenticated == true)
        {

            if (Cache[HttpContext.Current.User.Identity.Name] != null)
            {
                string userGroup = (string)Cache[HttpContext.Current.User.Identity.Name];
                //if (userGroup.ToLower().Contains("users") == true)
                //{
                ID = HttpContext.Current.User.Identity.Name;
                txtUserid.Text = ID;
                txtUserid.Enabled = false;
                //}
            }

            //if (!IsPostBack)
            //{
            //    TabContainer1_ActiveTabChanged(TabContainer1, null);
            //    ChkBoxListStaff.Width = 5000;
            //    TabContainer1.ActiveTabIndex = 0;
            //}
        }
    }
    private void showmassage(string message)
    {        
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ")</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
    private void LogError(string strMsg, string SourceApp, string SourceMethod)
    {
        //cp = new CPermit();
        //cp.Direction = strMsg;
        //cp.SourceApplication = SourceApp;
        //cp.MethodName = SourceMethod;
        //cp.MsgType = "ERROR";
        //rq = new CWritetoqueue();
        //rq.strPath = auditque;
        ////rq.Logonpermit.MsgType=;
        //rq.Writeaudit(cp);
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {

        // For non admin


        //rq = (CWritetoqueue)Session["user"];

        if (ID != null)
        {

            if (txtUserid.Text.Trim().ToUpper() != ID.ToUpper())
            {
                MessageBox("You can only change your own password!");
                txtUserid.Focus();
                return;
            }
        }
        else
        {
            MessageBox("Sorry You must relogin to perform this operation");
            Server.Transfer("loginaccess.aspx");
            return;
        }
        
        if (txtUserid.Text.Trim() == "")
        {
            MessageBox("You must supply a valid user id!");
            txtUserid.Focus();
            return;

        }

        if (txtOldpassword.Text.Trim() == "")
        {
            MessageBox("Old password cannot be an empty string!");
            txtOldpassword.Focus();
            return;


        }
        if (txtNewpassword.Text.Trim() == "")
        {
            MessageBox("New password cannot be an empty string!");
            txtNewpassword.Focus();
            return;


        }

        if (txtNewpassword.Text.Trim() != txtConfirmnewpassword.Text.Trim())
        {
            MessageBox("The new passwords does not match!");
            txtNewpassword.Focus();
            return;

        }
        try
        {
            string qry = "Select UserID,passw from TUsers where UserID = '" + txtUserid.Text.Trim() + "'";

            //WebServ = new CybWebServices.Service1();
            DataSet ds00 = SearchData(qry);// WebServ.RetriveDat(qry);

            string passw = "";
            string Uid = "";
            //CPermit cp = new CPermit();
            if (ds00.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
                {
                    Uid = ds00.Tables[0].Rows[jj][0].ToString();
                    passw = ds00.Tables[0].Rows[jj][1].ToString();

                    if (passw != txtOldpassword.Text.Trim())
                    {
                        MessageBox("Sorry the old password is not correct!");
                        return;
                    }
                    else
                    {
                        string Qry = "Update TUsers set passw='" + txtNewpassword.Text.Trim().Replace("'", "''") + "' where Userid ='" + txtUserid.Text.Trim().Replace("'", "''") + "'";
                        PerformUpdate(Qry);
                        MessageBox("Password successfully changed, you must reloging to proceed your operations");



                        HttpContext.Current.ApplicationInstance.Context.Cache.Remove(ID);
                        
                        Response.Redirect("~/loginaccess.aspx");

                        txtUserid.Text = "";
                        txtConfirmnewpassword.Text = "";
                        txtNewpassword.Text = "";
                        txtOldpassword.Text = "";
                    }
                }
            }                        

            //    txtUserid.Text = "";
            //}
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }
    }
    private void PerformUpdate(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(str);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "ZR", "");
            showmassage(msg);
        }
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
