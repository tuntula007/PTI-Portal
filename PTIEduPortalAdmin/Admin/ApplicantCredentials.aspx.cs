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


public partial class ApplicantCredentials   : System.Web.UI.Page
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
            if (Group.ToLower().Contains("web master"))
            {

            }
            else
            {
                if (Group.ToLower().Contains("faculty admin"))
                {
                }
                else
                {
                    // || !Group.ToLower().Contains("faculty admin")
                    msg = "You have no right to access this page";
                    showmassage(msg);
                    Response.Redirect("Home.aspx");

                    return;
                }

            }
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
            query = "SELECT FormNumber,Surname,OtherNames,ModeOfStudy,Programme,UserName,[Password],Email,Phone  FROM [ApplicantSignOn] where [FormNumber] ='" + this.TxtDept.Text.ToString().Trim() + "'";
            exportheader = "FormNumber,Surname,OtherNames,ModeOfStudy,Programme,UserName,[Password],Email,Phone ";
 
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
              

            //string FacultyPrefix = row.Cells[1].Text.Trim();
            //string FacultyName = row.Cells[2].Text.Trim();
            //string DeptCode = row.Cells[4].Text.Trim(); 
            //string DeptName = row.Cells[3].Text.Trim();  
            //string HOD = row.Cells[5].Text.Trim();
            //string CreatedBy = ID;// rq.Logonpermit.Userid;
            //string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


            string strSurname = row.Cells[1].Text.Trim();
            string strOtherNames = row.Cells[2].Text.Trim();
            string strEntryMode= row.Cells[3].Text.Trim();
            string strProg = row.Cells[4].Text.Trim();
            string  strStatus = row.Cells[5].Text.Trim().ToString ();
            string CreatedBy = ID;
            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            GridView1.EditIndex = -1;
            //update
            string qry1 = "UPDATE [ApplicantSignOn] SET [ModeOfStudy] = '" + strEntryMode.ToUpper().Trim() + "', [Programme]='" + strProg.ToUpper().Trim() + "' where [FormNumber] = '" + TxtDept.Text.ToString().Trim() + "'";
            string qry2 = "UPDATE ApplicantsHISTORY SET [Surname] = '" + strSurname.ToUpper().Trim() + "', [OtherNames]='" + strOtherNames.ToUpper().Trim() + "',[EntryMode] ='" + strEntryMode + "',[Programme]='" + strProg + "',[PrintStatus]='" + int.Parse(strStatus.ToString()) + "' where [FormNumber] = '" + TxtDept.Text.ToString().Trim() + "'";

            PerformUpdate(qry1);
            PerformUpdate(qry2);
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
             string qry = "";

            ////////////////////////////////////////////////////////qry = "SELECT * FROM [CourseOfStudy] where [DepartmentName] ='" + obj3 + "'";

            if (!Existed(qry))
            {

               ////////////////////////////////////////////////////  qry = "Delete from [Departments] where [FacultyID]  = " + id + " and [DepartmentCode]= '" + obj4 + "' and [DepartmentName]= '" + obj3 + "'";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                msg = "The record has been used by students, it can not be deleted";
                showmassage(msg);
                return;
            }

             
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
     
            GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView1.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView1.CaptionAlign = TableCaptionAlign.Left;
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

            ///////////////////////////////////////////////string Qry = "SELECT * FROM [Departments] where [FacultyName] = '" + DDListSchools.Text.Trim() + "' and  [FacultyID]  = '" + facCode + "' and [DepartmentCode]= '" + TxtDeptCode.Text.Trim() + "' and [DepartmentName]= '" + TxtDept.Text.Trim() + "'";
            string Qry = "";
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

                ////////////////////////////////////////////Qry = "INSERT INTO [Departments]([FacultyID],[FacultyName],[DepartmentName],[DepartmentCode],[Hod],[CreatedDate],[CreatedBy]) VALUES ('" + facCode + "','" + DDListSchools.Text.Trim() + "','" + DeptName + "','" + DeptPrefix + "','" + HOD + "','" + CreatedDate + "','" + CreatedBy + "')";
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
        
    }

    private void retrievDat(string fact)
    {
         
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
        
 
    }

   
    protected void BtnDel_Click(object sender, EventArgs e)
    {

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
