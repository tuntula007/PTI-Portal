using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;


public partial class Admin_EntryGrade : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    private static string msg = "";
    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];

    private static string Group = "";
    private string ID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //private static string Group = "";
        //private static string ID = "";
        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            //if (Group.ToLower().Contains("web master"))
            //{

            //}
            //else
            //{
            //    if (Group.ToLower().Contains("faculty admin"))
            //    {
            //    }
            //    else
            //    {
            //        // || !Group.ToLower().Contains("faculty admin")
            //        msg = "You have no right to access this page";
            //        showmassage(msg);
            //        Response.Redirect("Home.aspx");

            //        return;
            //    }

            //}
        }

        if (!IsPostBack)
        {
            populatetreeview();
            TabContainer1_ActiveTabChanged(TabContainer1, null);

        }


    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
               TxtGrade.Text = "";
               TxtGrade.Focus();
                populatetreeview();
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "ZR", "");
        }
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
        //log.Error(strMsg);
    }
    private void populatetreeview()
    {

        try
        {

            TreeView1.Nodes.Clear();
            TreeNode parent = new TreeNode();
            TreeNode tn = null;// new TreeNode();
            TreeNodeCollection child = new TreeNodeCollection();

            int Count = 0;


            string qry = "SELECT Grade FROM [EntryGradeList] order by [Grade] asc";

            DataSet ds = new DataSet();
            ds = SearchData(qry);


            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    string data = ds.Tables[0].Rows[jj][0].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][10].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    tn = new TreeNode(data.ToUpper());
                    child.Add(tn);
                }

            }


            foreach (TreeNode tn1 in child)
            {
                parent.ChildNodes.Add(tn1);
            }
            parent.Text = "Grades";
            parent.ToolTip = parent.ChildNodes.Count.ToString();

            TreeView1.Nodes.Add(parent);
            TreeView1.ExpandAll();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Cyb Hr", "");
        }

    }
    //protected void BtnEdit_Click(object sender, EventArgs e)
    //{
    //    loadDDDeptnames();
    //}
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (TreeView1.SelectedNode.Depth > 0)
        {
            TxtGrade.Text = TreeView1.SelectedValue.ToString();
            PaymentTypePanel1.Visible = true;
        }
        //PaymentTypePanelGridv.Visible = true;
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
            LogError(msg, "ZR", "");
            showmassage(msg);
        }
    }

    private void PerformDelete(string Qry)
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
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }

        return ret;
    }
    private void refreshscreen()
    {
        try
        {
            populatetreeview();
            PaymentTypePanel1.Visible = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Zr", "");
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtGrade.Text != "")
            {
                TxtGrade.Text = TxtGrade.Text.ToUpper().Replace("'", "''").Replace("=", "").Trim(); //.Replace("STATE", "").Trim();
            }
            else
            {
                msg = "Enter Grade";
                showmassage(msg);
                TabContainer1.Focus();
                return;
            }
            //SELECT [Usergroup] FROM [TUsergroups] order by [Usergroup]

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string Qry = "SELECT * FROM [EntryGradeList] where [Grade] = '" + TxtGrade.Text.Trim() + "'";

            if (Existed(Qry))
            {
                msg = "Grade existed before";
                showmassage(msg);
                TxtGrade.Focus();
                return;
            }
            else
            {


                Qry = "INSERT INTO [EntryGradeList]([Grade]) VALUES ('" + TxtGrade.Text.Trim() + "')";
                PerformInsert(Qry);

            }

            populatetreeview();

            TxtGrade.Text = "";
            TxtGrade.Focus();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }

    }



    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }


    protected void BtnClose_Click(object sender, EventArgs e)
    {
        BtnDelete.Visible = false;
        BtnRefresh.Visible = false;
        BtnExit.Visible = false;
        TreeView1.Visible = false;
        TabContainer1.Dispose();
        TabContainer1.ActiveTabIndex = -1;
    }
    protected void BankGridv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void Confirm(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.confirm('" + strMsg + "')</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {

            if (TxtGrade.Text != "")
            {
                TxtGrade.Text = TxtGrade.Text.Trim().ToUpper().Replace("'", "''");
            }
            else
            {
                msg = "Enter Grade";
                showmassage(msg);
                TabContainer1.Focus();
                return;
            }


            string qry = "";
            qry = "SELECT * FROM [EntryExamGrade] where [Grade] = '" + TxtGrade.Text.Trim() + "'";

             if (Existed(qry))
            {
                msg = "Grade has been assigned to exam body, you can not delete";
                showmassage(msg);
                TxtGrade.Focus();
                return;
            }

             qry = "Delete from [EntryGradeList] where [Grade] ='" + TxtGrade.Text.Trim() + "'";

            PerformDelete(qry);

            populatetreeview();

            TxtGrade.Text = "";
            TxtGrade.Focus();



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "ZR", "");
        }

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        //BtnAddnew.Visible = false;
        BtnDelete.Visible = false;
        BtnRefresh.Visible = false;
        BtnExit.Visible = false;
        TreeView1.Visible = false;
        TabContainer1.ActiveTabIndex = -1;
        TabContainer1.Dispose();
        ///Server.Transfer("Clientwelcome.aspx?re=0");
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        populatetreeview();
    }
}
