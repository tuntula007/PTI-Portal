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
using System.Text.RegularExpressions;
//using Cyberspace.ServiceBrocker;
//using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;

public partial class Createuser : System.Web.UI.Page
{

    //private static string strConn = ConfigurationManager.AppSettings["conn"];
    private static string str = ConfigurationManager.AppSettings["conn"];//.ConnectionString; 


    private static string msg = "";

    private static string ID = "";// cp = null;

    private static string Group = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            //ID = HttpContext.Current.User.Identity.Name;
            //Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            
        }

        if (Page.IsPostBack == false)
        {
            //if (!LoadGroup())
            //{
            //    btnCreateaccount.Enabled = false;
            //}
            //loadShareTypes();
            //btnCreateaccount.Enabled = false;
        }



    }
    //private void loadShareTypes()
    //{
    //    try
    //    {

    //        DDListShareType.Items.Clear();
    //        string Qry = "";

    //        DataSet ds = new DataSet();

    //        Qry = "select distinct [ShareTypeName] FROM [Shares_Types]";

    //        ds = SearchData(Qry);

    //        string Sname = "";

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
    //            {
    //                Sname = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
    //                DDListShareType.Items.Add(Sname);
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        //LogError(msg, "Payroll", "");
    //    }
    //}

    private void showmassage(string message)
    {
        //message = message.Replace("'", " ").Replace("\r\n", "");
        //Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        //Page.Controls.Add(lbl);
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
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
    //private bool LoadGroup()
    //{
    //    bool ret = false;
    //    try
    //    {
    //        cboUsergroup.Items.Clear();
    //        string qry = "Select Distinct Usergroup from [TUsergroups]";

    //        //WebServ = new CybWebServices.Service1();
    //        DataSet ds00 = SearchData(qry);// WebServ.RetriveDat(qry);
    //        string Grp = "";
    //        //CPermit cp = new CPermit();
    //        if (ds00.Tables[0].Rows.Count > 0)
    //        {
    //            for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
    //            {
    //                Grp = ds00.Tables[0].Rows[jj][0].ToString().ToUpper();

    //                cboUsergroup.Items.Add(Grp);
    //                ret = true;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string exx = ex.Message;
    //    }

    //    return ret;
    //}

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
    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ")</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
    //protected void cboUsergroup_Changed(object sender, EventArgs e)
    //{
    //    if (cboUsergroup.Text.Trim().ToLower() == "companysecretary")
    //    {
    //        DDListShareType.Enabled = true;
    //        //DDListShareType.Items.Clear();
    //        loadShareTypes();
    //    }
    //    else
    //    {
    //        DDListShareType.Items.Clear();
    //        DDListShareType.Enabled = false;
    //    }
    //}
    protected void btnCreateaccount_Click(object sender, EventArgs e)
    {
        
        if (txtUserid.Text.Trim() == "")
        {
            MessageBox("You must provide a valid UserId!");
            txtUserid.Focus();
            return;
        }
        if (txtPassword.Text.Trim() == "")
        {
            MessageBox("Password cannot be empty!");
            txtPassword.Focus();
            return;
        }

        if (txtPassword.Text.Trim() != txtConfirmpassword.Text.Trim())
        {
            MessageBox("Password fields does not match!");
            txtPassword.Focus();
            return;
        }
        try
        {



            if (txtEmail.Text.Trim() != "" && txtEmail.Text != "NONE")
            {
                string mail = "";
                string patternStrict = ConfigurationManager.AppSettings["regexmail"];

                MatchCollection mc = Regex.Matches(txtEmail.Text, patternStrict);

                for (int i = 0; i < mc.Count; i++)
                {
                    mail = mc[0].ToString();
                }

                if (mc.Count > 0)
                {
                    txtEmail.Text = mail.ToLower();
                }
                else
                {
                    msg = "Enter a valid mail address";
                    showmassage(msg);
                    txtEmail.Focus();
                }
            }
            else
            {
                msg = "Enter a valid mail address";
                showmassage(msg);
                txtEmail.Focus();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }


        try
        {
            string qry = "Select * from TUsers where emailaddress ='" + txtEmail.Text.Trim() + "'";
            if (Existed(qry))
            {
                MessageBox("Email already exist!");
                txtEmail.Focus();
                return;
            }

            qry = "Select Userid from TUsers where userid ='" + txtUserid.Text.Trim() + "'";
            if (Existed(qry))
            {
                MessageBox("The user id you entered already exist!");
            }
            else
            {
                //qry = "INSERT INTO  [TUsers] ([userid] ,[passw],[usergroup],[emailaddress],[ShareTpyes]) VALUES ('" + txtUserid.Text.Trim().Replace("'", "''") + "','" + txtPassword.Text.Trim().Replace("'", "''") + "','client','" + txtEmail.Text.Trim().ToLower().Replace("'", "''") + "','" + ShareType + "')";
                qry = "INSERT INTO  [TUsers] ([userid] ,[passw],[usergroup],[emailaddress],[UserStatus]) VALUES ('" + txtUserid.Text.Trim().Replace("'", "''") + "','" + txtPassword.Text.Trim().Replace("'", "''") + "','Client','" + txtEmail.Text.Trim().ToLower().Replace("'", "''") + "',1)";

                PerformInsert(qry);

                txtUserid.Text = "";
                txtEmail.Text = "";
                showmassage("User Account successfully created!");

            }

        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }

    }
    private void PerformInsert(string Qry)
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
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    private bool Existed(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(str);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;

            cmd = new SqlCommand(qry, cnn);

            dr = cmd.ExecuteReader();//
            if (dr.HasRows)
            {
                ret = true;
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Zenith Registrar", "");
            showmassage(msg);
        }

        return ret;
    }
    //private bool IdExisted(string qry)
    //{
    //    bool rtn = false;

    //    WebServ = new CybWebServices.Service1();
    //    DataSet ds00 = WebServ.RetriveDat(qry);

    //    if (ds00.Tables[0].Rows.Count > 0)
    //    {
    //        rtn = true;
    //    }

    //    return rtn;
    //}
}
