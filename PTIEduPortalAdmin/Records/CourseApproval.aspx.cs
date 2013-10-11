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
using System.IO;
using System.Reflection;
using System.Text;
using AuditLogInfo;
using System.Net;



public partial class Records_CourseApproval : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    
    private string msg = "";
    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static int GViewWidth = 0;
    private static bool Gensearch = false;

    //private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];

    private string ID = "";// cp = null;
    private string Group = "";
    
    private static Hashtable CourseOfStudyID = null;
    private AuditLogInfo.AuditInfo auditInfo = null;
    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogPTI"];
    private StringBuilder sbmsgbody = null;
    private static string EmailPackagePath = ".\\private$\\" + ConfigurationManager.AppSettings["EmailQ"]; 

    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.Width = 600;
        //ChkboxShareType2.Width = 1000;
        //private static string Group = "";
        //private static string ID = "";
        //PaymentTypePanel1.Visible = true;


        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            if (!Group.ToLower().Contains("web master") == true)
            {

                //msg = "You have no right to access this page";
                //showmassage(msg);
                //Response.Redirect("Home.aspx");

               // return;
            }
            else
            {

            }
        }

        if (!IsPostBack)
        {
            populategrd();
            //LoadProgm();
            LoadModeOfStudy();
            //LoadCourseOfStudy();
            loadsession();
            LoadLevels();
            //LoadType();
            LoadProgramme();
            //LoadEntryMode();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1_ActiveTabChanged(TabContainer1, null);

            BtnDisApprove.Enabled = false;
            BtnApprove.Enabled = false;

            DisplayGrid();
        }
        //this.TxtStartDate.TextChanged += new EventHandler(TxtStartDate_TextChanged);

    }
    //private void LoadEntryMode()
    //{
    //    DDListEntryMode.Items.Clear();

    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        string qry = "SELECT [EntryMode] FROM [EntryMode] order by [EntryMode] asc";

    //        ds = SearchData(qry);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
    //            {

    //                DDListEntryMode.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace;
    //        //LogError(msg, "School Portal", "");
    //        showmassage(msg);
    //        //return;
    //    }
    //}
    private void LoadLevels()
    {
        try
        {
            DDListLevels.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [AcademicLevel] FROM [Levels] order by [AcademicLevel] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDListLevels.Items.Add("None");
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListLevels.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
            //return;
        }
    }


    private void LoadModeOfStudy()
    {
        try
        {
            DDListModeStudy.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [ModeOfStudy] FROM [ModeOfStudy] order by [ModeOfStudy] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListModeStudy.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
                LoadCourseOfStudy(DDListModeStudy.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
            //return;
        }
    }
    //DDListType_Changed

    protected void DDListType_Changed(object sender, System.EventArgs e)
    {
        confirmapp();

    }

    private void confirmapp()
    {
        //if (DDListType.Text.Trim() == "Applicants")
        //{
        //    DDListLevels.Enabled = false;
        //}
        //else
        //{
        //    DDListLevels.Enabled = true;
        //}
    }
    protected void DDListModeStudy_Changed(object sender, System.EventArgs e)
    {
        //ChkboxShareType2.Items.Clear();
        LoadCourseOfStudy(DDListModeStudy.Text.Trim());

    }
    private void LoadCourseOfStudy(string SupId2)
    {
        DDListCourseStudy.Items.Clear();

        try
        {
            // DDListModeStudy.Items.Clear();

            CourseOfStudyID = new Hashtable();
            DataSet ds = new DataSet();
            string qry = "";
            int depID = 53; //use this to store id based on user login
            depID = getidforGroup(ID);
            qry = "SELECT distinct [CourseOfStudyName],[CourseOfStudyID] FROM [CourseOfStudy]  where [ModeOfStudy] ='" + DDListModeStudy.Text.Trim() + "' and departmentID =" + depID;

            ds = SearchData(qry);

            string name = "";
            string Cosid = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDListCourseStudy.Items.Add("All");
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    name = ds.Tables[0].Rows[jj][0].ToString();
                    Cosid = ds.Tables[0].Rows[jj][1].ToString();
                    DDListCourseStudy.Items.Add(name);

                    if (!CourseOfStudyID.ContainsKey(name.Trim()))
                    {
                        CourseOfStudyID.Add(name.Trim(), int.Parse(Cosid));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;

            showmassage(msg);
            //return;
        }
    }

    private int getidforGroup(string Group)
    {
        int did = 0;
        try
        {
            // DDListModeStudy.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [departmentid] FROM [Tusers] where userid ='"+ Group +"'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                   int.TryParse( ds.Tables[0].Rows[jj][0].ToString(), out did);
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;

            showmassage(msg);
            //return;
        }
        return did;
    }

    private void loadsession()
    {
        DDListSession.Items.Clear();

        try
        {
            // DDListModeStudy.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [SessionName] FROM [Session] order by [ActiveStatus] desc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListSession.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;

            showmassage(msg);
            //return;
        }
    }

    //private void LoadProgm()
    //{
    //    ChkboxShareType2.Items.Clear();

    //    try
    //    {
    //        // DDListModeStudy.Items.Clear();

    //        DataSet ds = new DataSet();
    //        string qry = "SELECT [Programme],[Description] FROM [SchProgramme] order by [Programme] asc";

    //        ds = SearchData(qry);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
    //            {
    //                ChkboxShareType2.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace;

    //        showmassage(msg);
    //        //return;
    //    }
    //}
    //void TxtStartDate_TextChanged(object sender, EventArgs e)
    //{
    //    ValidateDate();
    //}
    //private void ValidateDate()
    //{

    //    try
    //    {
    //        DateTime DateB;
    //        DateTime dt = new DateTime();// = DateB.AddDays(days);
    //        DateTime dt2 = new DateTime();
    //        int days = 30;

    //        if (DateTime.TryParse(TxtStartDate.Text, out DateB))
    //        {
    //            // int yr = DateB.Year;
    //            //// int day = DateB
    //            // if (DateTime.IsLeapYear(yr))
    //            // {
    //            //     days = 366;
    //            // }
    //            // else
    //            // {
    //            //     days = 365;
    //            // }

    //            dt2 = DateB.AddDays(days);

    //            TxtEndDate.Text = dt2.ToString("yyyy-MM-dd");
    //        }
    //        else
    //        {
    //            msg = "Wrong date format";
    //            showmassage(msg);
    //            TxtStartDate.Focus();
    //            return;
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                ////populategrd();
                //////LoadProgm();
                ////LoadModeOfStudy();
                //////LoadCourseOfStudy();
                ////loadsession();
                ////LoadLevels();
                //////LoadType();
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
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
            //query = "SELECT [Srn],[Programme],[Session],[StartDate] as [Start Date],[Enddate] as [End Date] FROM [ApplicationSales] order by Session";
            ////query = "SELECT [AdmissionSession] as Session,[EntryMode],[RegNo],[Surname],[OtherNames],[Sex],[Age],[State],[Country],[AdmissionStatus],[CourseOfStudy],[TransactionDate] FROM [EntryTable] where [AdmissionSession] like '%" + TxtSearch.Text + "%' or EntryMode like '%" + TxtSearch.Text + "%' or RegNo like '%" + TxtSearch.Text + "%' or Surname like '%" + TxtSearch.Text + "%' or OtherNames like '%" + TxtSearch.Text + "%' or Sex like '%" + TxtSearch.Text + "%' or Age like '%" + TxtSearch.Text + "%' or State like '%" + TxtSearch.Text + "%' or Country like '%" + TxtSearch.Text + "%' or AdmissionStatus like '%" + TxtSearch.Text + "%' or CourseOfStudy like '%" + TxtSearch.Text + "%' or [TransactionDate] like '%" + TxtSearch.Text + "%' order by TransactionDate desc";
            //exportheader = "[Srn],[Programme],[Session],[Start Date],[End Date]";

            //Exportfilename = "Application Sales";
            //GridCaption = "Application Sales";
            //GViewWidth = 700;
            //populategrdv(query);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "Payroll", "");
        }
    }

    //// protected void grdViewStatustory_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
    // {
    //     e.Cancel = true;
    //     GridView1.EditIndex = -1;
    //     try
    //     {
    //         populategrd();

    //     }
    //     catch (Exception ex)
    //     {

    //         //logger.Error(ex.Message);
    //     }
    // }
    //protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    //{
    //    try
    //    {
    //        GridView1.EditIndex = e.NewEditIndex;
    //        populategrd();// DisplayGrid();
    //        GridViewRow row = GridView1.Rows[e.NewEditIndex];
    //        row.Cells[1].Enabled = false;
    //        row.Cells[2].Enabled = false;
    //        row.Cells[3].Enabled = true;
    //        row.Cells[4].Enabled = true;
    //        row.Cells[5].Enabled = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        //LogError(msg, "Payroll", "");
    //    }

    //}
    private void LoadProgramme()
    {

        DDListProgramme.Items.Clear();

        //ArrayList fac = new ArrayList();

        try
        {
            // DDListModeStudy.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [Programme],[Description] FROM [SchProgramme]  order by [Programme] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListProgramme.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
            //return;
        }
    }
    //protected void grdViewStatustory_OnRowUpdating(Object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
    //        GridView1.ToolTip = GridView1.Rows[e.RowIndex].Cells[1].Text;
    //        GridViewRow row = GridView1.Rows[e.RowIndex];
    //        GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
    //        GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
    //        GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
    //        GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
    //        GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
    //        GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();

    //        string obj1 = "";
    //        string obj2 = "";
    //        string obj3 = "";
    //        string obj4 = "";
    //        string obj5 = "";

    //        obj1 = row.Cells[1].Text.Trim();
    //        obj2 = row.Cells[2].Text.Trim();
    //        obj3 = row.Cells[3].Text.Trim();
    //        obj4 = row.Cells[4].Text.Trim();
    //        obj5 = row.Cells[5].Text.Trim();

    //        int srn = 0;
    //        srn = int.Parse(obj1);

    //        GridView1.EditIndex = -1;
    //        //update

    //        DateTime DateB;
    //        DateTime dt;
    //        string EndDate = "";
    //        string StartDate = "";

    //        if (DateTime.TryParse(obj4, out DateB))
    //        {
    //            StartDate = DateB.ToString("yyyy-MM-dd");
    //        }
    //        else
    //        {
    //            msg = "Wrong start date format";
    //            showmassage(msg);
    //            TxtStartDate.Focus();
    //            return;
    //        }
    //        if (DateTime.TryParse(obj5, out dt))
    //        {
    //            EndDate = dt.ToString("yyyy-MM-dd");
    //        }
    //        else
    //        {
    //            msg = "Wrong end date format";
    //            showmassage(msg);
    //            //TxtEndDate.Focus();
    //            return;
    //        }
    //        string qry = "";
    //        qry = "SELECT * FROM [ApplicationSales] where [Session] = '" + obj3 + "' and [Programme] = '" + obj2 + "' and Srn != " + srn + "";

    //        if (!Existed(qry))
    //        {
    //            qry = "UPDATE [ApplicationSales] SET [Programme] = '" + obj2.Trim() + "', Session='" + obj3.Trim() + "',StartDate='" + StartDate + "',Enddate='" + EndDate + "' where [Srn] = " + srn + "";
    //            PerformUpdate(qry);

    //            populategrd();
    //            row.Cells[1].Enabled = false;
    //            row.Cells[2].Enabled = false;
    //            row.Cells[3].Enabled = true;
    //            row.Cells[4].Enabled = true;
    //            row.Cells[5].Enabled = true;
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

    //protected void grdViewStatustory_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        if (GridView1.Rows.Count <= 0)
    //        {
    //            e.Cancel = true;
    //            return;
    //        }


    //        string obj1 = "";
    //        string obj2 = "";
    //        string obj3 = "";
    //        string obj4 = "";
    //        string obj5 = "";

    //        obj1 = GridView1.Rows[e.RowIndex].Cells[1].Text;
    //        obj3 = GridView1.Rows[e.RowIndex].Cells[3].Text;


    //        int srn = 0;
    //        srn = int.Parse(obj1);


    //        string qry = "";
    //        qry = "SELECT * FROM [Applicants] where [PresentSession] ='" + obj1 + "'";

    //        if (!Existed(qry))
    //        {

    //            qry = "Delete from [ApplicationSales] where [Srn] = " + srn + "";

    //            PerformDelete(qry);
    //            populategrd();
    //        }
    //        else
    //        {
    //            msg = "Faculty name has been used to setup department, it can not be deleted";
    //            showmassage(msg);
    //            return;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        //LogError(msg, "Payroll", "");
    //    }

    //}
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                //formatGridview();
            }
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
    private void formatGridview()
    {

        try
        {
            String datef = "";
            DateTime datty;
            double dd = 0.0;
            int j = GridView1.HeaderRow.Cells.Count;//.Columns.Count;
            int Amt = 0;
            int Date1 = 0;
            int Date2 = 0;

            int Duedate = 0;
            //[StartDate] as [Start Date],[Enddate] as [End Date] FROM [ApplicationSales] order by Session";

            for (int m = 0; m < j; m++)
            {
                if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "AMOUNT")
                {
                    Amt = m;
                }
                if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "START DATE")
                {
                    Date1 = m;
                }
                if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "END DATE")
                {
                    Date2 = m;
                }

            }

            Double Total = 0.0;
            string dat = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (Amt != 0)
                {
                    dat = GridView1.Rows[i].Cells[Amt].Text;
                    if (dat != "")
                    {
                        dd = Double.Parse(GridView1.Rows[i].Cells[Amt].Text);
                        GridView1.Rows[i].Cells[Amt].Text = String.Format("{0:N}", dd);
                        Total = Total + dd;
                    }
                }
                if (Date1 != 0)
                {
                    dat = GridView1.Rows[i].Cells[Date1].Text;
                    if (dat != "")
                    {
                        datty = Convert.ToDateTime(GridView1.Rows[i].Cells[Date1].Text);
                        datef = datty.ToString("yyyy-MM-dd");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                        GridView1.Rows[i].Cells[Date1].Text = datef;
                    }
                }
                if (Date2 != 0)
                {
                    dat = GridView1.Rows[i].Cells[Date2].Text;
                    if (dat != "")
                    {
                        datty = Convert.ToDateTime(GridView1.Rows[i].Cells[Date2].Text);
                        datef = datty.ToString("yyyy-MM-dd");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                        GridView1.Rows[i].Cells[Date2].Text = datef;
                    }
                }
                //////
                ////if (Duedate != 0)
                ////{
                ////    datty = Convert.ToDateTime(GridView1.Rows[i].Cells[Duedate].Text);
                ////    datef = datty.ToString("yyyy-MM-dd");//(GridView1.Rows[i].Cells[6].Text).ToString("yyyy-MM-dd");
                ////    GridView1.Rows[i].Cells[Duedate].Text = datef;
                ////}

            }

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
    private void sendtoAuditQ(AuditInfo auditInfo)
    {

        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(AudilogUIQ))
            {
                mq = MessageQueue.Create(AudilogUIQ);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(AudilogUIQ);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(AuditInfo) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + auditInfo.Action + ";" + auditInfo.Logintime;
            mq.Send(auditInfo);
            mq.Dispose();
            mq.Close();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "School Portal", "");
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;       
        GridView1.DataBind();

        DisplayGrid();
        Gensearch = false;

    }

    private void DisplayGrid()
    {
        try
        {

            string prog = DDListCourseStudy.Text.Trim();
            string SessionName = "";
            //string StartDate = "";
            //string EndDate = "";
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            string CreatedBy = ID;
            string EntryMode = "";

            if (DDListSession.Text.Trim() != "")
            {
                SessionName = DDListSession.Text.Trim();
            }
            else
            {
                msg = "Enter Session name";
                showmassage(msg);

                return;
            }
            string Programme = "";
            if (DDListProgramme.Text.Trim() != "")
            {
                Programme = DDListProgramme.Text.Trim();
            }
            else
            {
                msg = "Enter school programme name";
                showmassage(msg);
                //TxtTxtSession.Focus();

                return;
            }
            
            DataTable tb = new DataTable();
            //string Year = Yearr;
            try
            {


                int cnt = 1;

                DataRow drow = null;

                tb.Columns.Add("Srn", typeof(string));
                tb.Columns.Add("Matno", typeof(string));
                tb.Columns.Add("Names", typeof(string));
                tb.Columns.Add("Sex", typeof(string));
                tb.Columns.Add("Level", typeof(string));
                tb.Columns.Add("Phone", typeof(string));
                tb.Columns.Add("Email", typeof(string));
                tb.Columns.Add("Status", typeof(string));
                tb.Columns.Add("CosCode1", typeof(string));
                tb.Columns.Add("CosCode2", typeof(string));
                tb.Columns.Add("CosCode3", typeof(string));
                tb.Columns.Add("CosCode4", typeof(string));
                tb.Columns.Add("CosCode5", typeof(string));
                tb.Columns.Add("CosCode6", typeof(string));
                tb.Columns.Add("CosCode7", typeof(string));
                tb.Columns.Add("CosCode8", typeof(string));
                tb.Columns.Add("CosCode9", typeof(string));
                tb.Columns.Add("CosCode10", typeof(string));
                tb.Columns.Add("CosCode11", typeof(string));
                tb.Columns.Add("CosCode12", typeof(string));
                tb.Columns.Add("CosCode13", typeof(string));
                tb.Columns.Add("CosCode14", typeof(string));
                tb.Columns.Add("CosCode15", typeof(string));
                tb.Columns.Add("CosCode16", typeof(string));
                tb.Columns.Add("CosCode17", typeof(string));
                tb.Columns.Add("CosCode18", typeof(string));
                tb.Columns.Add("CosCode19", typeof(string));
                tb.Columns.Add("CosCode20", typeof(string));
                tb.Columns.Add("Progid", typeof(string));
                tb.Columns.Add("Session", typeof(string));//ModeOfStudy
                tb.Columns.Add("ModeOfStudy", typeof(string));//Programme
                tb.Columns.Add("Programme", typeof(string));



                //foreach (string dep in PfaADmin)
                //{

                Hashtable Regstudents = new Hashtable();
                string sql = "";
                int cosstudyid = 0;
                if (prog == "All")
                {
                    sql = "select distinct [MatricNumber] from [CourseRegistration] where [SessionName]='" + DDListSession.Text.Trim() + "' and [AcademicLevel] ='" + DDListLevels.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [IsApproved] = 0";

                }
                else
                {
                    if (CourseOfStudyID.ContainsKey(DDListCourseStudy.Text.Trim()))
                    {
                        cosstudyid = int.Parse(CourseOfStudyID[DDListCourseStudy.Text.Trim()].ToString());
                        sql = "select distinct [MatricNumber] from [CourseRegistration] where [SessionName]='" + DDListSession.Text.Trim() + "' and [AcademicLevel] ='" + DDListLevels.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [CourseOfStudyID] = '" + cosstudyid + "' and [IsApproved] = 0";

                    }
                    else
                    {
                        msg = "Course of study id not available";
                        showmassage(msg);
                        return;
                    }

                }

                DataSet ds = null;
                ds = new DataSet();
                ds = SearchData(sql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        Regstudents.Add(ds.Tables[0].Rows[jj][0].ToString(), jj);
                    }
                }



                //BasicSal = 0;
                //TotalAllawi = 0;


                cnt = 1;

                if (Regstudents.Count > 0)
                {
                    foreach (DictionaryEntry stud in Regstudents)
                    {
                        string Surname = "";
                        string OtherNames = "";
                        string Sex = "";
                        string Email = "";
                        string PhoneNumber = "";
                        string DateOfBirth = "";
                        string State = "";


                        string regid = stud.Key.ToString();

                        sql = "select [Surname],[OtherNames],[Sex],[Email],[PhoneNumber],[DateOfBirth],[State] from [Students] where [PresentSession]='" + DDListSession.Text.Trim() + "' and [AcademicLevel] ='" + DDListLevels.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [MatricNumber]='" + regid + "'";

                        ds = new DataSet();
                        ds = SearchData(sql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                            {
                                Surname = ds.Tables[0].Rows[jj][0].ToString();
                                OtherNames = ds.Tables[0].Rows[jj][1].ToString();
                                Surname = Surname + " " + OtherNames;

                                Sex = ds.Tables[0].Rows[jj][2].ToString();
                                Email = ds.Tables[0].Rows[jj][3].ToString();
                                PhoneNumber = ds.Tables[0].Rows[jj][4].ToString();
                                DateOfBirth = ds.Tables[0].Rows[jj][5].ToString();
                                State = ds.Tables[0].Rows[jj][6].ToString();


                                drow = tb.NewRow();
                                drow["Srn"] = cnt.ToString();
                                drow["Matno"] = regid;
                                drow["Names"] = Surname.Replace("'", "''");
                                drow["Sex"] = Sex;
                                drow["Level"] = DDListLevels.Text.Trim();/// 2;
                                drow["Phone"] = PhoneNumber;// / 2;
                                drow["Email"] = Email;



                                drow["Session"] = DDListSession.Text.Trim();
                                drow["ModeOfStudy"] = DDListModeStudy.Text.Trim();
                                drow["Programme"] = DDListProgramme.Text.Trim();
                                //drow["Status"] = "";

                                //tb.Rows.Add(drow);

                            }


                            sql = "select [CourseCode],IsApproved,[CourseOfStudyID] from [CourseRegistration] where [SessionName]='" + DDListSession.Text.Trim() + "' and [AcademicLevel] ='" + DDListLevels.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [IsApproved] = 0 and [MatricNumber]='" + regid + "'";
                            ds = new DataSet();
                            ds = SearchData(sql);
                            string codes = "";
                            int status = 0;
                            int Cosid = 0;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                                {
                                    codes = ds.Tables[0].Rows[jj][0].ToString();
                                    status = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                                    Cosid = int.Parse(ds.Tables[0].Rows[jj][2].ToString());

                                    drow["Status"] = status.ToString();
                                    drow["Progid"] = Cosid.ToString();
                                    //tb.Columns.Add("Progid", typeof(string));

                                    switch (jj)
                                    {
                                        case 0:
                                            drow["CosCode1"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 1:
                                            drow["CosCode2"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 2:
                                            drow["CosCode3"] = ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 3:
                                            drow["CosCode4"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 4:
                                            drow["CosCode5"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 5:
                                            drow["CosCode6"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 6:
                                            drow["CosCode7"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 7:
                                            drow["CosCode8"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 8:
                                            drow["CosCode9"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 9:
                                            drow["CosCode10"] = ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 10:
                                            drow["CosCode11"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 11:
                                            drow["CosCode12"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 12:
                                            drow["CosCode13"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 13:
                                            drow["CosCode14"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 14:
                                            drow["CosCode15"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 15:
                                            drow["CosCode16"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 16:
                                            drow["CosCode17"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 17:
                                            drow["CosCode18"] = ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 18:
                                            drow["CosCode19"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;
                                        case 19:
                                            drow["CosCode20"] =  ds.Tables[0].Rows[jj][0].ToString();
                                            break;

                                    }



                                }
                                tb.Rows.Add(drow);
                                cnt++;
                            }
                        }

                    }


                    //drow["Session"] = DDListSession.Text.Trim();Session,ModeOfStudy,Programme
                    //drow["ModeOfStudy"] = DDListModeStudy.Text.Trim();
                    //drow["Programme"] = DDListProgramme.Text.Trim();
                    exportheader = "Srn,Matno,Names,Sex,Level,Phone,Email,Status,CosCode1,CosCode2,CosCode3,CosCode4,CosCode5,CosCode6,CosCode7,CosCode8,CosCode9,CosCode10,CosCode11,CosCode12,CosCode13,CosCode14,CosCode15,CosCode16,CosCode17,CosCode18,CosCode19,CosCode20,Session,ModeOfStudy,Programme";

                    Exportfilename = DDListCourseStudy.Text + " " + DDListSession.Text.Trim();
                    GridCaption = DDListCourseStudy.Text + " " + DDListSession.Text.Trim();
                    GViewWidth = 1000;


                    DataSet ds2 = new DataSet();
                    ds2.Tables.Add(tb);
                    if (GViewWidth > 0)
                    {
                        GridView1.Width = GViewWidth;
                    }

                    GridView1.DataSource = ds2;
                    Session["ds2"] = ds2;
                    GridView1.DataBind();

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        BtnDisApprove.Enabled = true;
                        BtnApprove.Enabled = true;
                    }
                    else
                    {
                        BtnDisApprove.Enabled = false;
                        BtnApprove.Enabled = false;
                    }
                    GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds2.Tables[0].Rows.Count.ToString();
                    GridView1.ToolTip = ds2.Tables[0].Rows.Count.ToString();
                    GridView1.CaptionAlign = TableCaptionAlign.Left;
                }
            }
            catch (Exception ex)
            {
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }

    //private void ValidateDate2()
    //{
    //    try
    //    {


    //        if (TxtStartDate.Text.Trim() != "" && TxtEndDate.Text.Trim() != "")
    //        {
    //            DateTime DateB;
    //            DateTime dt;// = new DateTime();// = DateB.AddDays(days);


    //            if (DateTime.TryParse(TxtStartDate.Text, out DateB))
    //            {

    //            }
    //            else
    //            {
    //                msg = "Wrong start date format";
    //                showmassage(msg);
    //                TxtStartDate.Focus();
    //                return;
    //            }
    //            if (DateTime.TryParse(TxtEndDate.Text, out dt))
    //            {

    //            }
    //            else
    //            {
    //                msg = "Wrong end date format";
    //                showmassage(msg);
    //                TxtEndDate.Focus();
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            msg = "Wrong date formats";
    //            showmassage(msg);
    //            TxtStartDate.Focus();
    //            return;
    //        }



    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

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
        // LoadFacultyEdit();
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
    protected void BtnSelectAll3_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    if (ChkboxShareType2.Items.Count > 0)
        //    {
        //        for (int k = 0; k < ChkboxShareType2.Items.Count; k++)
        //        {
        //            ChkboxShareType2.Items[k].Selected = true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{

        //    msg = ex.Message;
        //    showmassage(msg);
        //}
    }
    protected void BtnDisselect3_Click(object sender, EventArgs e)
    {
        //ChkboxShareType2.SelectedIndex = -1;
        //if (ChkboxShareType2.Items.Count > 0)
        //{
        //    for (int k = 0; k < ChkboxShareType2.Items.Count; k++)
        //    {
        //        ChkboxShareType2.Items[k].Selected = false;
        //    }
        //}
    }
    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        
        if (TxtSearchstaff.Text.Trim() != "")
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            DisplayGridStud(TxtSearchstaff.Text.Trim());
            Gensearch = true;
        }
        else
        {
            msg = "Enter value to search";
            showmassage(msg);
            return;
        }
    }

    private void DisplayGridStud(string TxtSearch)
    {
        try
        {


            string CreatedBy = ID;
            string EntryMode = ID;


            DataTable tb = new DataTable();
            //string Year = Yearr;
            try
            {


                int cnt = 1;

                DataRow drow = null;

                tb.Columns.Add("Srn", typeof(string));
                tb.Columns.Add("Matno", typeof(string));
                tb.Columns.Add("Names", typeof(string));
                tb.Columns.Add("Sex", typeof(string));
                tb.Columns.Add("Level", typeof(string));
                tb.Columns.Add("Phone", typeof(string));
                tb.Columns.Add("Email", typeof(string));
                tb.Columns.Add("Status", typeof(string));
                tb.Columns.Add("CosCode1", typeof(string));
                tb.Columns.Add("CosCode2", typeof(string));
                tb.Columns.Add("CosCode3", typeof(string));
                tb.Columns.Add("CosCode4", typeof(string));
                tb.Columns.Add("CosCode5", typeof(string));
                tb.Columns.Add("CosCode6", typeof(string));
                tb.Columns.Add("CosCode7", typeof(string));
                tb.Columns.Add("CosCode8", typeof(string));
                tb.Columns.Add("CosCode9", typeof(string));
                tb.Columns.Add("CosCode10", typeof(string));
                tb.Columns.Add("CosCode11", typeof(string));
                tb.Columns.Add("CosCode12", typeof(string));
                tb.Columns.Add("CosCode13", typeof(string));
                tb.Columns.Add("CosCode14", typeof(string));
                tb.Columns.Add("CosCode15", typeof(string));
                tb.Columns.Add("CosCode16", typeof(string));
                tb.Columns.Add("CosCode17", typeof(string));
                tb.Columns.Add("CosCode18", typeof(string));
                tb.Columns.Add("CosCode19", typeof(string));
                tb.Columns.Add("CosCode20", typeof(string));
                tb.Columns.Add("Progid", typeof(string));
                tb.Columns.Add("Session", typeof(string));//ModeOfStudy
                tb.Columns.Add("ModeOfStudy", typeof(string));//Programme
                tb.Columns.Add("Programme", typeof(string));



                //foreach (string dep in PfaADmin)
                //{

                Hashtable Regstudents = new Hashtable();
                string sql = "";
                int cosstudyid = 0;


                sql = "select distinct [MatricNumber] from [CourseRegistration] where [SessionName] like '%" + TxtSearch + "%' or [AcademicLevel] like '%" + TxtSearch + "%' or [ModeOfStudy] like '%" + TxtSearch + "%' or [Programme] like '%" + TxtSearch + "%' or [RegNo] like '%" + TxtSearch + "%' or [MatricNumber] like '%" + TxtSearch + "%' or [IsApproved] like '%" + TxtSearch + "%' or [CourseCode] like '%" + TxtSearch + "%' or [CourseOfStudyID] like '%" + TxtSearch + "%'";


                DataSet ds = null;
                ds = new DataSet();
                ds = SearchData(sql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        Regstudents.Add(ds.Tables[0].Rows[jj][0].ToString(), jj);
                    }
                }
                

                cnt = 1;

                if (Regstudents.Count > 0)
                {
                    foreach (DictionaryEntry stud in Regstudents)
                    {
                        string Surname = "";
                        string OtherNames = "";
                        string Sex = "";
                        string Email = "";
                        string PhoneNumber = "";
                        string DateOfBirth = "";
                        string State = "";


                        string regid = stud.Key.ToString();

                        sql = "select [Surname],[OtherNames],[Sex],[Email],[PhoneNumber],[DateOfBirth],[State] from [Students] where [MatricNumber]='" + regid + "'";

                        ds = new DataSet();
                        ds = SearchData(sql);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                            {
                                Surname = ds.Tables[0].Rows[jj][0].ToString();
                                OtherNames = ds.Tables[0].Rows[jj][1].ToString();
                                Surname = Surname + " " + OtherNames;

                                Sex = ds.Tables[0].Rows[jj][2].ToString();
                                Email = ds.Tables[0].Rows[jj][3].ToString();
                                PhoneNumber = ds.Tables[0].Rows[jj][4].ToString();
                                DateOfBirth = ds.Tables[0].Rows[jj][5].ToString();
                                State = ds.Tables[0].Rows[jj][6].ToString();

                            }

                            sql = "select distinct [SessionName] from [CourseRegistration] where  [MatricNumber] = '" + regid + "'";

                            DataSet ds3 = null;
                            ds3 = new DataSet();
                            ds3 = SearchData(sql);
                            ArrayList regs = new ArrayList();
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                for (int jj = 0; jj < ds3.Tables[0].Rows.Count; jj++)
                                {
                                    regs.Add(ds3.Tables[0].Rows[jj][0].ToString());
                                }

                                //
                                foreach (string sess in regs)
                                {

                                    sql = "select [CourseCode],IsApproved,[CourseOfStudyID],[AcademicLevel],[ModeOfStudy],[Programme] from [CourseRegistration] where [MatricNumber]='" + regid + "' and [SessionName] ='" + sess + "'";
                                    ds = new DataSet();
                                    ds = SearchData(sql);

                                    string codes = "";
                                    int status = 0;
                                    int Cosid = 0;
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        drow = tb.NewRow();
                                        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                                        {
                                            codes = ds.Tables[0].Rows[jj][0].ToString();
                                            status = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                                            Cosid = int.Parse(ds.Tables[0].Rows[jj][2].ToString());

                                            
                                            drow["Srn"] = cnt.ToString();
                                            drow["Matno"] = regid;
                                            drow["Names"] = Surname.Replace("'", "''");
                                            drow["Sex"] = Sex;

                                            drow["Phone"] = PhoneNumber;// / 2;
                                            drow["Email"] = Email;

                                            drow["Level"] = ds.Tables[0].Rows[jj][3].ToString();/// 2;
                                            drow["Session"] = sess;
                                            drow["ModeOfStudy"] = ds.Tables[0].Rows[jj][4].ToString();
                                            drow["Programme"] = ds.Tables[0].Rows[jj][5].ToString();

                                            drow["Status"] = status.ToString();
                                            drow["Progid"] = Cosid.ToString();
                                            //tb.Columns.Add("Progid", typeof(string));

                                            switch (jj)
                                            {
                                                case 0:
                                                    drow["CosCode1"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 1:
                                                    drow["CosCode2"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 2:
                                                    drow["CosCode3"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 3:
                                                    drow["CosCode4"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 4:
                                                    drow["CosCode5"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 5:
                                                    drow["CosCode6"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 6:
                                                    drow["CosCode7"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 7:
                                                    drow["CosCode8"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 8:
                                                    drow["CosCode9"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 9:
                                                    drow["CosCode10"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 10:
                                                    drow["CosCode11"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 11:
                                                    drow["CosCode12"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 12:
                                                    drow["CosCode13"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 13:
                                                    drow["CosCode14"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 14:
                                                    drow["CosCode15"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 15:
                                                    drow["CosCode16"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 16:
                                                    drow["CosCode17"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 17:
                                                    drow["CosCode18"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 18:
                                                    drow["CosCode19"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;
                                                case 19:
                                                    drow["CosCode20"] =  ds.Tables[0].Rows[jj][0].ToString();
                                                    break;

                                            }

                                            
                                        }
                                        tb.Rows.Add(drow);
                                        cnt++;
                                    }
                                    
                                }
                            }

                        }

                    }

                                       
                    exportheader = "Srn,Matno,Names,Sex,Level,Phone,Email,Status,CosCode1,CosCode2,CosCode3,CosCode4,CosCode5,CosCode6,CosCode7,CosCode8,CosCode9,CosCode10,CosCode11,CosCode12,CosCode13,CosCode14,CosCode15,CosCode16,CosCode17,CosCode18,CosCode19,CosCode20,Session,ModeOfStudy,Programme";

                    Exportfilename = DDListCourseStudy.Text + " " + DDListSession.Text.Trim();
                    GridCaption = DDListCourseStudy.Text + " " + DDListSession.Text.Trim();
                    GViewWidth = 1000;


                    DataSet ds2 = new DataSet();
                    ds2.Tables.Add(tb);
                    if (GViewWidth > 0)
                    {
                        GridView1.Width = GViewWidth;
                    }

                    GridView1.DataSource = ds2;
                    Session["ds2"] = ds2;
                    GridView1.DataBind();

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        BtnDisApprove.Enabled = true;
                        BtnApprove.Enabled = true;
                    }
                    else
                    {
                        BtnDisApprove.Enabled = false;
                        BtnApprove.Enabled = false;
                    }
                    GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds2.Tables[0].Rows.Count.ToString();
                    GridView1.ToolTip = ds2.Tables[0].Rows.Count.ToString();
                    GridView1.CaptionAlign = TableCaptionAlign.Left;
                }
            }
            catch (Exception ex)
            {
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }
    protected void BtnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int Courseid = 0;
            string AcademicLevel = "";
            string Programme = "";

            string ModeOfStudy = "";
            string session = "";

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string Approveby = ID;
            string OGNO = "";

            string query = "";
            string id = "";
            string ApprovedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int cnt = 0;
            DataSet ds = null;
            string mail = "";
            string Name = "";

            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
                if (chk.Checked)
                {
                    OGNO = gvr.Cells[2].Text;
                    Name = gvr.Cells[3].Text;
                    AcademicLevel = gvr.Cells[5].Text;
                    mail = gvr.Cells[7].Text;
                    
                    id = gvr.Cells[29].Text;
                    Courseid = int.Parse(id);
                    session = gvr.Cells[30].Text;
                    ModeOfStudy = gvr.Cells[31].Text;
                    Programme = gvr.Cells[32].Text;

                    //query = "UPDATE [CourseRegistration]  SET [IsApproved] = 1 where [SessionName]='" + session + "' and [AcademicLevel] ='" + AcademicLevel + "' and [ModeOfStudy] = '" + ModeOfStudy + "' and [Programme] = '" + Programme + "' and [CourseOfStudyID] = " + Courseid + " and [IsApproved] = 0 and [MatricNumber]='" + OGNO + "'";

                    
                    query = "UPDATE [CourseRegistration]  SET [IsApproved] = 1 where [SessionName]='" + session + "' and [AcademicLevel] ='" + AcademicLevel + "' and [ModeOfStudy] = '" + ModeOfStudy + "' and [Programme] = '" + Programme + "' and [CourseOfStudyID] = " + Courseid + " and [MatricNumber]='" + OGNO + "'";

                    PerformUpdate(query);

                    auditInfo = new AuditInfo();
                    string msg1 = query;
                    auditInfo.Action = "Course Approval";
                    auditInfo.Usergroup = Group;
                    auditInfo.Userid = ID;
                    auditInfo.Msg = msg1;
                    auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    auditInfo.Computer = User.Identity.Name;
                    auditInfo.Hostname = Request.UserHostName;
                    auditInfo.IPAddress = Request.UserHostAddress;
                    sendtoAuditQ(auditInfo);
                    cnt++;


                    string Subject = "Course Registration Approval";
                    string Heading = "PETROLEUM TRAINING INSTITUTE, EFFURUN admin portal Login Credentials";
                    string Attached = "";
                    string email = mail;
                    string name = Name;

                    sbmsgbody = new StringBuilder();

                    sbmsgbody.AppendLine("");

                    sbmsgbody.AppendLine("Current Academic Session: " + session);
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Current Academic Level: " + AcademicLevel);
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Congratulations, your registerred courses are approved");
                    sbmsgbody.AppendLine("");

                    sendGenMail(email, sbmsgbody.ToString(), Subject, Heading, Attached, name);
                }
            }

            msg = cnt.ToString() + " " + "students course registration approved";// +"";
            showmassage(msg);

            if (Gensearch == true)
            {
                DisplayGridStud(TxtSearchstaff.Text.Trim());
            }
            else
            {
                DisplayGrid();
            }



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void BtnDisApprove_Click(object sender, EventArgs e)
    {
        try
        {

            int Courseid = 0;
            string AcademicLevel = "";
            string Programme = "";

            string ModeOfStudy = "";
            string session = "";

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string Approveby = ID;
            string OGNO = "";

            string query = "";
            string id = "";
            string ApprovedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int cnt = 0;
            DataSet ds = null;
            string mail = "";
            string Name = "";
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
                if (chk.Checked)
                {
                    OGNO = gvr.Cells[2].Text;
                    Name = gvr.Cells[3].Text;
                    AcademicLevel = gvr.Cells[5].Text;
                    mail = gvr.Cells[7].Text;


                    id = gvr.Cells[29].Text;
                    Courseid = int.Parse(id);
                    session = gvr.Cells[30].Text;
                    ModeOfStudy = gvr.Cells[31].Text;
                    Programme = gvr.Cells[32].Text;

                    //query = "UPDATE [CourseRegistration]  SET [IsApproved] = 2 where [SessionName]='" + session + "' and [AcademicLevel] ='" + AcademicLevel + "' and [ModeOfStudy] = '" + ModeOfStudy + "' and [Programme] = '" + Programme + "' and [CourseOfStudyID] = " + Courseid + " and [IsApproved] = 0 and [MatricNumber]='" + OGNO + "'";

                    query = "UPDATE [CourseRegistration]  SET [IsApproved] = 2 where [SessionName]='" + session + "' and [AcademicLevel] ='" + AcademicLevel + "' and [ModeOfStudy] = '" + ModeOfStudy + "' and [Programme] = '" + Programme + "' and [CourseOfStudyID] = " + Courseid + " and [MatricNumber]='" + OGNO + "'";

                    PerformUpdate(query);

                    auditInfo = new AuditInfo();
                    string msg1 = query;
                    auditInfo.Action = "Course Disapproval";
                    auditInfo.Usergroup = Group;
                    auditInfo.Userid = ID;
                    auditInfo.Msg = msg1;
                    auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    auditInfo.Computer = User.Identity.Name;
                    auditInfo.Hostname = Dns.GetHostName().ToUpper();
                    auditInfo.IPAddress = Request.UserHostAddress;
                    sendtoAuditQ(auditInfo);
                    cnt++;


                    string Subject = "Course Registration Disapproval";
                    string Heading = "PETROLEUM TRAINING INSTITUTE, EFFURUN admin portal Login Credentials";
                    string Attached = "";
                    string email = mail;
                    string name = Name;


                    //String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

                    sbmsgbody = new StringBuilder();

                    sbmsgbody.AppendLine("");

                    sbmsgbody.AppendLine("Current Academic Session: " + session);
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Current Academic Level: " + AcademicLevel);
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("We advice you send mail to records@dlc.ui.edu.ng or call 08077593563, 08077593564 on courses to register for your current academic session");
                    sbmsgbody.AppendLine("");                    

                    sendGenMail(email, sbmsgbody.ToString(), Subject, Heading, Attached, name);

                }
            }

            msg = cnt.ToString() + " " + "students course registration disapproved";// +"";
            showmassage(msg);

            if (Gensearch == true)
            {
                DisplayGridStud(TxtSearchstaff.Text.Trim());
            }
            else
            {
                DisplayGrid();
            }


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "UI", "");
            showmassage(msg);
        }
    }
    private void sendGenMail(string maillist, string messg, string subject, string Heading, string attachedFile, string StaffName)
    {
        try
        {
            Cyberspace.Emailpackage.CMail cm = null;
            //foreach (DictionaryEntry em in maillist)
            //{
            string mlist = maillist;
            string staff = StaffName;

            cm = new Cyberspace.Emailpackage.CMail();
            cm.Subject = subject;
            cm.ToEmail.Add(mlist);
            cm.AttachedFile = attachedFile;
            //cm.FromEmail.Add();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear" + " " + staff + ",");
            //sb.AppendLine();
            sb.AppendLine(Heading.ToUpper());
            //sb.AppendLine("-------------------------------");
            sb.AppendLine();
            sb.AppendLine(messg);
            sb.AppendLine();
            sb.AppendLine("Best Regards,");
            sb.AppendLine("");
            sb.AppendLine("======================================================================================");
            sb.AppendLine("PETROLEUM TRAINING INSTITUTE, EFFURUN");

            cm.Body = sb.ToString();
            cm.BCCEmail.Add("cybsoft@cybaaspace.net");
            cm.ReplyTo.Add("records@dlc.ui.edu.ng");
            cm.DisplayName = subject;
            cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm.SourceApplication = "UI";


            sendtoMailQueue(cm);
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {


        }

        // }
    }


    private void sendtoMailQueue(Cyberspace.Emailpackage.CMail cm)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(EmailPackagePath))
            {
                mq = MessageQueue.Create(EmailPackagePath);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(EmailPackagePath);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cyberspace.Emailpackage.CMail) });
                mq.DefaultPropertiesToSend = dfp;
            }


            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            mq.Send(cm);
            mq.Dispose();
            mq.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + "||" + ex.StackTrace);
        }
    }
}
