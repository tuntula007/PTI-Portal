using System;
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
using System.Messaging;
//using Cyberspace.ServiceBrocker;//.dll.refresh;
//using log4net;
//using log4net.Config;
//using log4net.Util;
using System.IO;
using System.Reflection;
using System.Text;


public partial class Setup_Faculty : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    //private string strCrudqueries = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Crudqueries"].Trim();
    //private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string msg = "";

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";

    private static int GViewWidth = 0;
    private static Hashtable schcodes = null;


    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];

    private static string ID = "";
    private static string Group = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.Width = 600;
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
        //


        if (!IsPostBack)
        {
            // populategrd();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1_ActiveTabChanged(TabContainer1, null);

            LoadSch();
            populategrd();
        }
        //if (!IsPostBack)
        //{

        //   populatetreeview();
        //   TabContainer1_ActiveTabChanged(TabContainer1, null);


        //}
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                LoadSch();
                populategrd();
            }
            //if (TabContainer1.ActiveTabIndex == 1)
            //{
            //    LoadFacultyEdit();
            //    populatetreeview();

            //}
            //if (TabContainer1.ActiveTabIndex == 2)
            //{
            //    populatetreeview();


            //}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }

    private void LoadSch()
    {
        try
        {
            schcodes = new Hashtable();
            DDListSchools.Items.Clear();
            DataSet ds = new DataSet();

            string Qry = "SELECT distinct [FacultyName],[FacultyID] FROM [Faculty]";
            ds = SearchData(Qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListSchools.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());

                    if (!schcodes.ContainsKey(ds.Tables[0].Rows[jj][0].ToString().ToUpper()))
                    {
                        schcodes.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper(), ds.Tables[0].Rows[jj][1].ToString().ToUpper());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }
    private void LoadFaculty()
    {
        //DDListFaculty.Items.Clear();
        //WebServ = new CybWebServices.Service1();
        //int col = 1;
        //object[] arr = null;

        //string qry = "SELECT [FacultyName] FROM [edup_Faculty]";
        //arr = WebServ.RetreiveFac(qry, col);

        //ArrayList fac = new ArrayList(arr);
        //if (fac.Count > 0)
        //{
        //    foreach (string facty in fac)
        //    {
        //        DDListFaculty.Items.Add(facty);
        //    }
        //}
    }
    protected void BtnAddnew_Click(object sender, EventArgs e)
    {
        //PaymentTypePanel1.Visible = true;       

        //populatetreeview();

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
    private void populategrd()
    {
        try
        {
            query = "SELECT FacultyID as [Faculty ID], [FacultyName] as [Faculty],[DepartmentName] as [Department],[DepartmentCode] as [Dept Code],[Hod] FROM [Departments] order by [DepartmentName]";
            exportheader = "[Faculty ID],[Faculty],[Dept Name],[Dept Code],[Hod]";

            //string query = "SELECT [FacultyName] as [Faculty Name], [DepartmentName] as [Department Name],[DepartmentCode] as [Department Code],[Hod] as [Head Of Department]  FROM [Departments]";
            ////query = "SELECT [AdmissionSession] as Session,[EntryMode],[RegNo],[Surname],[OtherNames],[Sex],[Age],[State],[Country],[AdmissionStatus],[CourseOfStudy],[TransactionDate] FROM [EntryTable] where [AdmissionSession] like '%" + TxtSearch + "%' or EntryMode like '%" + TxtSearch + "%' or RegNo like '%" + TxtSearch + "%' or Surname like '%" + TxtSearch + "%' or OtherNames like '%" + TxtSearch + "%' or Sex like '%" + TxtSearch + "%' or Age like '%" + TxtSearch + "%' or State like '%" + TxtSearch + "%' or Country like '%" + TxtSearch + "%' or AdmissionStatus like '%" + TxtSearch + "%' or CourseOfStudy like '%" + TxtSearch + "%' or [TransactionDate] like '%" + TxtSearch + "%' order by TransactionDate desc";
            //exportheader = "[FacultyName] as [Faculty Name], [DepartmentName] as [Department Name],[DepartmentCode] as [Department Code],[Hod] as [Head Of Department]";


            Exportfilename = "Depts";
            GridCaption = "Depts";
            GViewWidth = 900;
            populategrdv(query);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "Payroll", "");
        }
    }

    protected void grdViewStatustory_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        GridView1.EditIndex = -1;
        try
        {
            populategrd();

        }
        catch (Exception ex)
        {

            //logger.Error(ex.Message);
        }
    }
    protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;


            populategrd();// DisplayGrid();
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = true;
            row.Cells[4].Enabled = true;
            row.Cells[5].Enabled = true;
            //row.Cells[6].Enabled = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }

    protected void grdViewStatustory_OnRowUpdating(Object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView1.ToolTip = GridView1.Rows[e.RowIndex].Cells[1].Text;
            GridViewRow row = GridView1.Rows[e.RowIndex];
            GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();

            string Name = "";
            string Code = "";
            double amt = 0;
            //Qry = "SELECT * FROM [Payment_Plan] where [ShareHolderCategory] = '" + DDListCategory.Text.Trim() + "' and DurationPlan = '" + DDListDuration.Text.Trim() + "'";

            //string Ldays = GridView1.ToolTip.Trim();//.ToString();//.Text;
            //if (double.TryParse(Ldays, out amt))
            //{

            //}
            //else
            //{
            //    msg = "enter a numeric value";
            //    showmassage(msg);
            //    populategrd();
            //    return;
            //}

            //Name = row.Cells[1].Text.Trim();//.Controls[0])).Text.Trim();
            //Code = row.Cells[2].Text.Trim();



            string FacultyPrefix = row.Cells[1].Text.Trim();
            string FacultyName = row.Cells[2].Text.Trim();
            string DeptCode = row.Cells[4].Text.Trim();


            string DeptName = row.Cells[3].Text.Trim();


            string HOD = row.Cells[5].Text.Trim();
            string CreatedBy = ID;// rq.Logonpermit.Userid;
            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            GridView1.EditIndex = -1;
            //update
            string qry = "UPDATE [Departments] SET [DepartmentName] = '" + DeptName.ToUpper().Trim() + "', [Hod]='" + HOD.ToUpper().Trim() + "',CreatedDate='" + CreatedDate + "',CreatedBy='" + CreatedBy + "' where [FacultyName] = '" + FacultyName + "' and  [FacultyID]  = '" + FacultyPrefix + "' and [DepartmentCode]= '" + DeptCode + "'";
            PerformUpdate(qry);
            populategrd();
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = false;
            //row.Cells[5].Enabled = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
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
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void grdViewStatustory_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count <= 0)
            {
                e.Cancel = true;
                return;
            }


            string obj1 = "";
            string obj2 = "";
            string obj3 = "";
            string obj4 = "";

            obj1 = GridView1.Rows[e.RowIndex].Cells[1].Text;
            obj2 = GridView1.Rows[e.RowIndex].Cells[2].Text;
            obj3 = GridView1.Rows[e.RowIndex].Cells[3].Text;
            obj4 = GridView1.Rows[e.RowIndex].Cells[4].Text;

            int id = int.Parse(obj1);
            //string qry = "SELECT * FROM [LeaveTypes] where [LeaveType] ='" + TxtLeaveType.Text.Trim() + "'";
            string qry = "";

            qry = "SELECT * FROM [CourseOfStudy] where [DepartmentName] ='" + obj3 + "'";

            if (!Existed(qry))
            {

                 qry = "Delete from [Departments] where [FacultyID]  = " + id + " and [DepartmentCode]= '" + obj4 + "' and [DepartmentName]= '" + obj3 + "'";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                msg = "Department name has been used by students, it can not be deleted";
                showmassage(msg);
                return;
            }


            //if (obj1 != "" && obj2 != "" && obj3 != "" && obj4 != "")
            //{

            //    string qry = "Delete [Departments] where [FacultyName] = '" + obj2 + "' and  [FacultyID]  = '" + obj1 + "' and [DepartmentCode]= '" + obj4 + "' and [DepartmentName]= '" + obj3 + "'";

            //    PerformDelete(qry);
            //    populategrd();
            //}
            //else
            //{
            //    //state.InsertState(st);
            //}


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }
    private void populategrdv(string query1)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(query1, cnn);

            dat.Fill(ds);
            if (GViewWidth > 0)
            {
                GridView1.Width = GViewWidth;
            }

            GridView1.DataSource = ds;
            Session["ds2"] = ds;

            GridView1.DataBind();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    formatGridview();
            //}
            GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView1.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView1.CaptionAlign = TableCaptionAlign.Left;
            //ChequePanelGridv.Visible = true;
        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }

    }
    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        loadDDDeptnames();
    }
    protected void BankGridv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet ds3 = null;
            if (Session["ds2"] != null)
            {
                ds3 = (DataSet)Session["ds2"];
                GridView1.DataSource = ds3;
                GridView1.Width = GViewWidth;
                GridView1.PageIndex = e.NewPageIndex;
                //GrdVPerntwk.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
                GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds3.Tables[0].Rows.Count.ToString();
                GridView1.ToolTip = ds3.Tables[0].Rows.Count.ToString();
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    //formatGridView1();
                }

                GridView1.Visible = true;

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {


    }
    private void loadDDDeptnames()
    {

    }
    private void refreshscreen()
    {
        try
        {
            populategrd();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "Payroll", "");
        }
    }
    protected void DDlistPaymentType_OnSelectedIndexChanged(object sender, System.EventArgs e)
    {

        loadDeptnames();
    }

    private void loadDeptnames()
    {

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (DDListSchools.Text.Trim() == "")
            {
                msg = "Select school name";
                showmassage(msg);
                return;
            }

            if (TxtDept.Text != "")
            {
                TxtDept.Text = TxtDept.Text.ToUpper().Replace("'", "''").Replace(";", ":").Replace("&", "AND").Trim();
            }
            else
            {
                msg = "Enter department name";
                showmassage(msg);
                TxtDept.Focus();

                return;
            }

            if (TxtDeptCode.Text != "")
            {
                TxtDeptCode.Text = TxtDeptCode.Text.ToUpper().Replace("'", "''").Replace(";", ":").Replace("&", "AND").Trim();//.Replace("STATE", "").Trim();
            }
            else
            {
                msg = "Enter department prefix eg csc for computer science";
                showmassage(msg);
                TxtDeptCode.Focus();

                return;
            }

            if (TxtHod.Text != "")
            {
                TxtHod.Text = TxtHod.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
            }
            else
            {
                TxtHod.Text = "NONE";
            }

            //FacultyBusiness fb = new FacultyBusiness();

            //SELECT [FacultyName],[DepartmentName],[DepartmentCode],[Hod] FROM [edup_Departments]
            //string qry = "SELECT * from [edup_Faculty] where [FacultyPrefixCode]='" + TxtFacPrefix.Text.Trim() + "' or [FacultyName]='" + TxtFaculty.Text.Trim() + "'";

            DataSet ds = new DataSet();

            //Dura = new Hashtable();
            string Plan = "";
            int M = 0;
            string facCode = "";
            if (schcodes.Count > 0)
            {
                if (schcodes.ContainsKey(DDListSchools.Text.Trim()))
                {
                    facCode = schcodes[DDListSchools.Text.Trim()].ToString();
                }
            }
            else
            {
                msg = "School code empty, refresh page";
                showmassage(msg);
                return;
            }

            string Qry = "SELECT * FROM [Departments] where [FacultyName] = '" + DDListSchools.Text.Trim() + "' and  [FacultyID]  = '" + facCode + "' and [DepartmentCode]= '" + TxtDeptCode.Text.Trim() + "' and [DepartmentName]= '" + TxtDept.Text.Trim() + "'";

            if (Existed(Qry))
            {
                msg = "Dept with name or prefix existed before, try to edit";
                showmassage(msg);
                TxtDeptCode.Focus();
                return;
            }
            else
            {

                string DeptName = TxtDept.Text.Trim();
                string DeptPrefix = TxtDeptCode.Text.Trim();
                string HOD = TxtHod.Text.Trim();
                string CreatedBy = ID;// rq.Logonpermit.Userid;
                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                Qry = "INSERT INTO [Departments]([FacultyID],[FacultyName],[DepartmentName],[DepartmentCode],[Hod],[CreatedDate],[CreatedBy]) VALUES ('" + facCode + "','" + DDListSchools.Text.Trim() + "','" + DeptName + "','" + DeptPrefix + "','" + HOD + "','" + CreatedDate + "','" + CreatedBy + "')";
                PerformInsert(Qry);

                populategrd();
                TxtDept.Text = "";
                TxtDeptCode.Text = "";
                TxtHod.Text = "";
                TxtDept.Focus();
            }

            //populatetreeview();


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
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
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }

        return ret;
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
            //LogError(msg, "Payroll", "");
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
            //LogError(msg, "Payroll", "");
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
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
        //Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        //Page.Controls.Add(lbl);
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        GridView1.DataSource = null;
        GridView1.DataBind();

        TxtDept.Text = "";
        TxtDeptCode.Text = "";
        TxtHod.Text = "";
        BtnExport.Visible = false;
        BtnRefresh.Visible = false;
        LnkBtnPrin.Visible = false;
        TabContainer1.Dispose();
        TabContainer1.ActiveTabIndex = -1;
        //PaymentTypePanel1.Visible = false;

    }
    protected void BankGridv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnViewallbanks_Click(object sender, EventArgs e)
    {
        //populatetreeview();
        //PaymentTypePanel1.Visible = true;
        ////TxtPaymentType.Text = "";

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
        //try
        //{

        //    if (DDListFaculty.Text != "" && TxtFaculty.Text != "")
        //    {
        //        DDListFaculty.Text = DDListFaculty.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //        TxtFaculty.Text = TxtFaculty.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Enter faculty and faculty prefix";
        //        showmassage(msg);
        //        DDListFaculty.Focus();
        //        return;
        //    }


        //    CSingleAttribute cs = new CSingleAttribute();
        //    cs.ActionType = "Delete";
        //    cs.MethodName = "Delete";
        //    cs.SourceClass = "Faculty";
        //    cs.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    cs.SourceApplication = "EduPortal";
        //    cs.ActionBody = "Delete from [edup_Faculty] where [FacultyName]='" + DDListFaculty.Text.Trim() + "' and [FacultyPrefixCode]='" + TxtFaculty.Text.Trim() + "'";
        //    sendQuery(cs);

        //    log.Info("Delete faculty by:");
        //    populatetreeview();
        //    PaymentTypePanel1.Visible = true;
        //    DDListFaculty.Text = "";
        //    TxtFacPrefix.Text = "";
        //    TxtFaculty.Text = "";
        //    DDListFaculty.Focus();

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //}
        ////cmd = new SqlCommand(", cnn);

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        //BtnAddnew.Visible = false;
        //BtnDelete.Visible = false;
        ////BtnEdit.Visible = false;
        //BtnViewallbanks.Visible = false;
        //Server.Transfer("Default.aspx?re=0");
    }

    protected void LinkBtnExit_Click(object sender, EventArgs e)
    {
        //Server.Transfer("Clientwelcome.aspx?re=0");
    }
    protected void LinkBtnEdit_Click(object sender, EventArgs e)
    {
        //PaymentTypePanel1.Visible = true;
        LoadFacultyEdit();
        //populatetreeview();
        //MultiView1.ActiveViewIndex = 1;
    }

    private void LoadFacultyEdit()
    {
        //try
        //{
        //    DDListFacEdit.Items.Clear();

        //    ArrayList fac = new ArrayList();

        //    FacultyBusiness fb = new FacultyBusiness();
        //    fac = fb.GetFaculty();

        //    if (fac.Count > 0)
        //    {
        //        foreach (string facty in fac)
        //        {

        //            DDListFacEdit.Items.Add(facty);
        //        }


        //        retrievDat(DDListFacEdit.Text.Trim());

        //    }

        //    else
        //    {
        //        TxtDeanEdit.Text = "";
        //        TxtFacPrefixEdit.Text = "";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }

    private void retrievDat(string fact)
    {
        //try
        //{
        //    FacultyBusiness fb = new FacultyBusiness();
        //    DataSet ds00 = fb.GetFacultySchemaByID(fact.Replace("'", "''"));

        //    string Itm2 = "";
        //    if (ds00.Tables[0].Rows.Count > 0)
        //    {
        //        for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
        //        {
        //            Itm2 = ds00.Tables[0].Rows[jj][0].ToString().ToUpper();

        //            TxtFacPrefixEdit.Text = ds00.Tables[0].Rows[jj][1].ToString().ToUpper();
        //            TxtDeanEdit.Text = ds00.Tables[0].Rows[jj][2].ToString().ToUpper();

        //            // fac.Add(Itm2);
        //        }

        //        populategrdv(ds00);
        //    }
        //    else
        //    {
        //        TxtFacPrefixEdit.Text = "";
        //        TxtDeanEdit.Text = "";
        //    }

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        string header = exportheader;// exportheader;
        string filename = Exportfilename;// Exportfilename;
        ExportData(header, filename);
    }
    private void ExportData(string header, string filename)
    {
        StringBuilder stb = new StringBuilder();
        //cmd = new SqlDataAdapter("select MSISDN,Service_Name as CODE, Operator_ID as NETWORK, Log_Date as LOGDAY,Message_Body As [MESSAGE BODY], GUID from Mg_Transaction where Service_Name in " + shortcode + " and  Direction = 'IN' and Log_Date between '" + txtBeginDate.Text.Trim() + "' and '" + txtEndDate.Text.Trim() + "' order by Log_Date desc", cnn);
        //stb.Append("BANK NAME,BRANCH,ADDRESS,CONTACT PERSON,PHONE,MAIL");
        stb.Append(header);
        stb.AppendLine();
        DataSet ds = new DataSet();

        String msg;
        int j;
        int k;
        int i;
        int m;

        try
        {
            if (Session["ds2"] != null)
            {
                ds = (DataSet)Session["ds2"];
                j = ds.Tables[0].Columns.Count;
                //j = j - 1;
                k = ds.Tables[0].Rows.Count;

                for (i = 0; i < k; i++)
                {
                    for (m = 0; m < j; m++)
                    {
                        if (m == 0)
                        {
                            stb.Append(ds.Tables[0].Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                        }
                        else
                        {
                            stb.Append("," + ds.Tables[0].Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                        }

                    }
                    stb.AppendLine();
                }


                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + filename + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.csv";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Response.Write(stb.ToString());
                Response.End();
            }
            else
            {
                msg = "Specify what you want to Export";
                showmassage(msg);
                //TxtMsisdn.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
            return;
        }
    }
    protected void BtnSubmitEdit_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    if (DDListFacEdit.Text != "")
        //    {
        //        DDListFacEdit.Text = DDListFacEdit.Text;//.ToUpper().Replace("'", "''").Replace(";", ":");
        //        PresentDept = DDListFacEdit.Text.Trim().Replace("'", "''");
        //    }
        //    else
        //    {
        //        msg = "Select Faculty Name";
        //        showmassage(msg);
        //        DDListFacEdit.Focus();
        //        return;
        //    }



        //    if (TxtFacPrefixEdit.Text != "")
        //    {
        //        TxtFacPrefixEdit.Text = TxtFacPrefixEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
        //    }
        //    else
        //    {
        //        msg = "Enter faculty prefix eg SCN for science, ENG for engineering";
        //        showmassage(msg);
        //        TxtFacPrefixEdit.Focus();
        //        return;
        //    }

        //    if (TxtDeanEdit.Text != "")
        //    {
        //        TxtDeanEdit.Text = TxtDeanEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();

        //    }
        //    else
        //    {
        //        TxtDeanEdit.Text = "NONE";

        //    }

        //    FacultyBusiness fb = new FacultyBusiness();
        //    //
        //    if (fb.Existed(DDListFacEdit.Text.Trim().Replace("'", "''")))
        //    {
        //        Faculty facData = new Faculty();

        //        facData.FacultyName = DDListFacEdit.Text.Trim().Replace("'", "''");
        //        //facData.FacultyPrefix = TxtFacPrefix.Text.Trim();
        //        facData.DeanOfFaculty = TxtDeanEdit.Text.Trim().Replace("'", "''");
        //        facData.CreatedBy = "NONE";// rq.Logonpermit.Userid;
        //        facData.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        //        fb.UpdateFaculty(facData);

        //        msg = "Update successful";
        //        showmassage(msg);


        //        DataSet Ds = fb.GetFacultySchemaByID(DDListFacEdit.Text.Trim().Replace("'", "''"));
        //        populategrdv(Ds);
        //        //GViewWidth = 0;



        //        //LoadFacultyEdit();
        //        TxtFacPrefixEdit.Text = "";
        //        TxtDeanEdit.Text = "";
        //        TxtFacPrefixEdit.Focus();

        //    }
        //    else
        //    {

        //    }

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }

    //private bool confirmExistedB4(string qry)
    //{
    //    //bool succ = false;
    //    //try
    //    //{
    //    //    WebServ = new CybWebServices.Service1();

    //    //    DataSet ds = new DataSet();
    //    //    ds = WebServ.RetriveDat(qry);
    //    //    if (ds.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        succ = true;
    //    //    }
    //    //    //WebServ = new CybWebServices.Service1();
    //    //    //int col = 1;
    //    //    //object[] arr = null;
    //    //    //arr = WebServ.RetreiveFac(qry, col);

    //    //    //ArrayList fac = new ArrayList(arr);
    //    //    //if (fac.Count > 0)
    //    //    //{
    //    //    //    succ = true;
    //    //    //}

    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    msg = ex.Message + "||" + ex.StackTrace.ToString();
    //    //    showmassage(msg);
    //    //    LogError(msg, "Payroll", "");
    //    //}
    //    //return succ;
    //}
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    if (DDListFacEdit.Text != "")
        //    {
        //        //DDListFaculty.Text = DDListFaculty.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //        //TxtDeptCode.Text = TxtDeptCode.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Select faculty to delete";
        //        showmassage(msg);
        //        DDListFacEdit.Focus();
        //        return;
        //    }


        //    FacultyBusiness fb = new FacultyBusiness();
        //    Faculty facData = new Faculty();

        //    facData.FacultyName = DDListFacEdit.Text.Trim();

        //    fb.DeleteFaculty(facData);

        //    msg = "Delete successful";
        //    showmassage(msg);
        //    populatetreeview();

        //    LoadFacultyEdit();
        //    ////TxtDeanEdit.Text = "";
        //    ////TxtFacPrefixEdit.Text = "";
        //    TxtFacPrefixEdit.Focus();

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
        //cmd
    }
    protected void BtnCloseEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        GridView1.DataSource = null;
        GridView1.DataBind();

        TabContainer1.ActiveTabIndex = 0;

    }
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        populategrd();
        //populatetreeview();
    }
    protected void BtnPrintGrid_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["ds2"];
        Session["ds"] = ds;
        showwindow("PrintPages.aspx?Title=" + Exportfilename);
    }
    private void showwindow(string window)
    {
        Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open('payment.aspx','mywindow','location=0,status=0,scrollbars=0,width=600,height=600,dependent=yes' )</script>";
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ",'CustomPopUP','location=0,resizable=no,status=0,scrollbars=yes,toolbar=yes,menubar=yes,width=600,height=600,dependent=yes' )</script>";
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }
}
