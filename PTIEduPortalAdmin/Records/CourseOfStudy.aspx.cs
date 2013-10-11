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
using System.Drawing;
using AuditLogInfo;


public partial class Admin_CourseOfStudy : System.Web.UI.Page
{
    string str = System.Configuration.ConfigurationManager.AppSettings["conn"];
    string str2 = System.Configuration.ConfigurationManager.AppSettings["conn2"];

    private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string PinInfoUpdate = ".\\private$\\" + ConfigurationManager.AppSettings["PinInfoUpdate"];

    private string msg = "";

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static string PixName = "NONE";
    private static string EditedPixName = "";

    private static int GViewWidth = 0;

    private static string PresentDept = "";
    private static ArrayList AllFaculty = null;
    private static ArrayList AllDept = null;

    private static Hashtable FacultyID = null;

    private static Hashtable DepartmentID = null;
    private static Hashtable CentreCode = new Hashtable();

    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];
    //private static CPermit cp = null;
    //private static CWritetoqueue rq = null;
    private string ID = "";// cp = null;
    private string Group = "";
    private string userDept = "";
    private string userFact = "";
    private string usercosstudy = "";
    public DataSet populategrddat = null;

    private AuditLogInfo.AuditInfo auditInfo = null;
    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogPTI"];
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.Width = 900;
        //ListBoxCostudy.Width = 3000;
        //ListBoxCostudy.Height = 300;

        //ListBoxCourses.Width = 3000;
        AllFaculty = null;

        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            if (Group.ToLower().Trim() == null)//!Group.ToLower().Contains("web master") == true
            {
                //msg = "You have no right to this page";
                //showmassage(msg);
                return;

            }
            else
            {
                //getuserinfo(ID);
            }
        }

        if (!IsPostBack)
        {
            TabContainer1.ActiveTabIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();
           // getuserinfo(ID);
            AllFaculty = null;
            LoadFaculty();
            LoadDuration();
            LoadHons();
            LoadProgramme();
            LoadModeOfStudy();
            populategrd();
        }

        GridView1.Width = GViewWidth;
    }
    private string getuserCostudy(string ID)
    {
        usercosstudy = "";

        try
        {


            DataSet ds = new DataSet();
            //string qry = "SELECT [FacultyName],DepartmentName,[CourseOfStudy] FROM [TUsers] where [userid]='" + ID + "'";
            string qry = "SELECT CourseOfStudy FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    usercosstudy = ds.Tables[0].Rows[jj][0].ToString();

                }

            }

        }
        catch (Exception ex)
        {

        }

        return usercosstudy;
    }

    private string getuserDept(string ID)
    {
        userDept = "";

        try
        {


            DataSet ds = new DataSet();
            //string qry = "SELECT [FacultyName],DepartmentName,[CourseOfStudy] FROM [TUsers] where [userid]='" + ID + "'";
            string qry = "SELECT DepartmentName FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    userDept = ds.Tables[0].Rows[jj][0].ToString();

                }

            }

        }
        catch (Exception ex)
        {

        }

        return userDept;

    }

    private string getuserFact(string ID)
    {
        userFact = "";

        try
        {


            DataSet ds = new DataSet();
            //string qry = "SELECT [FacultyName],DepartmentName,[CourseOfStudy] FROM [TUsers] where [userid]='" + ID + "'";
            string qry = "SELECT FacultyName FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    userFact = ds.Tables[0].Rows[jj][0].ToString();

                }

            }

        }
        catch (Exception ex)
        {

        }

        return usercosstudy;
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
    private void getuserinfo(string ID)
    {
        try
        {
            userDept = "";
            userFact = "";

            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],[DepartmentName] FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    userFact = ds.Tables[0].Rows[jj][0].ToString();
                    userDept = ds.Tables[0].Rows[jj][1].ToString();
                }

            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                if (permitedGroup())
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    AllFaculty = null;
                    LoadFaculty();
                    LoadDuration();
                    LoadHons();
                    LoadProgramme();
                    LoadModeOfStudy();
                    populategrd();
                }
                else
                {
                    //msg = "You have no right to this page";
                    //showmassage(msg);
                    //return;
                }

            }
            if (TabContainer1.ActiveTabIndex == 1)
            {
                if (Group.ToLower().Trim() != "")//|| //
                {
                    //BtnApprove.Visible = false;
                    //BtnDisapprove.Visible = false;
                    //msg = "You have no right to access this tab";
                    //showmassage(msg);
                    //return;
                }
                //BtnApprove.Visible = true;
                //BtnDisapprove.Visible = true;
                //GridView3.DataSource = null;
                //GridView3.DataBind();
                //LoadApprovedReservedBooks2();
            }

            if (TabContainer1.ActiveTabIndex == 2)
            {
                //GridView1.DataSource = null;
                //GridView1.DataBind();


                //GridView2.DataSource = null;
                //GridView2.DataBind();


                //if (permitedGroup())
                //{

                //    Loadprintoption();
                //    loadPrintables();
                //}
                //else
                //{
                //    msg = "You have no right to this page";
                //    showmassage(msg);
                //    return;
                //}
            }



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);

        }
    }
    private void LoadFaculty()
    {

        try
        {
            DDListFaculty.Items.Clear();
            FacultyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],[FacultyID] FROM [Faculty] order by [FacultyName] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListFaculty.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    FacultyID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                }
                loadDept(DDListFaculty.Text.Trim());
            }
            else
            {
                DDListDept.Items.Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void loadDept(string Fact)
    {
        try
        {
            DDListDept.Items.Clear();
            DepartmentID = new Hashtable();



            try
            {
                DataSet ds = null;
                string qry = "";
                qry = "SELECT distinct [DepartmentName],[DepartmentId] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

                ds = new DataSet();
                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        DDListDept.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                        //DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                    }

                }
                else
                {
                    DDListDept.Items.Clear();
                }
                TxtCourseOfStudy.Text = "";



                //Load dept id
                qry = "SELECT distinct [DepartmentName],[DepartmentId] FROM [Departments] order by [DepartmentName] asc";

                ds = new DataSet();
                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        if (!DepartmentID.ContainsKey(ds.Tables[0].Rows[jj][0].ToString()))
                        {
                            DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                        }
                    }

                }
                else
                {
                    DepartmentID.Clear();
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
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
            //return;
        }

    }
    private void LoadHons()
    {
        try
        {

            DDListHonour.Items.Clear();


            DataSet ds = new DataSet();
            string qry = "SELECT [Honour],[Description] FROM [CourseHonour] order by [Honour] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListHonour.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
    private void LoadDuration()
    {

        DDListDuration.Items.Clear();

        try
        {

            DataSet ds = new DataSet();
            string qry = "SELECT [Duration] FROM [CourseDuration] order by [Duration] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListDuration.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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

    private void LoadProgramme()
    {

        DDListProgramme.Items.Clear();

        //ArrayList fac = new ArrayList();

        try
        {
            // DDListModeStudy.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [Programme],[Description] FROM [SchProgramme] order by [Programme] asc";

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
    private bool permitedGroup()
    {
        bool succ = false;
        if (Group.ToLower().Trim() == "web master" || Group.ToLower().Trim() == "faculty admin" || Group.ToLower().Trim() == "department admin")
        {
            succ = true;
        }
        return succ;
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
    protected void DDListLecturerFaculty_Changed(object sender, EventArgs e)
    {
        loadDept(DDListFaculty.Text.Trim());
    }
    private void populategrd()
    {
        try
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID);

            //query = "SELECT [CourseOfStudyName] as [Course Of Study],[Duration] as [Duration Of Study],[DepartmentName] as [Department],[FacultyName] as [Faculty],[Honours] as [Honor]  FROM [CourseOfStudy]";
            int valid = 0;

            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                //query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Course Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] order by DepartmentName";
                query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Programme Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] order by DepartmentName";
                valid++;
            }
            if (Group.ToUpper() == "FACULTY ADMIN")
            {
                query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Programme Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] where FacultyName='" + userFact + "' order by DepartmentName";
                valid++;
            }
            if (Group.ToUpper() == "DEPARTMENT ADMIN")
            {
                query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Programme Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] where FacultyName='" + userFact + "' and DepartmentName ='" + userDept + "' order by DepartmentName";
                valid++;
            }
            //

            if (valid > 0)
            {
                exportheader = "[Id],[Course Of Study],[School],[Department],[Duration Of Study],[Programme],[Mode Of Study],[Honours],[Min Credit Earned],[Min Credit Registered],[Min CGPA]";

                Exportfilename = "CourseOfStudy";
                GridCaption = "CourseOfStudy";
                GViewWidth = 1000;
                populategrdv(query);
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "Payroll", "");
        }
    }
    private void populategrdv(string query1)
    {
        try
        {
            populategrddat = new DataSet();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(query1, cnn);

            dat.Fill(ds);
            dat.Fill(populategrddat);
            if (GViewWidth > 0)
            {
                GridView1.Width = GViewWidth;
            }

            GridView1.DataSource = ds;
            Session["ds2"] = ds;
            // populategrddat.datasou
            GridView1.DataBind();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    formatGridview();
            //}
            GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView1.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView1.CaptionAlign = TableCaptionAlign.Left;
            //PanelGrid.Visible = true;
            //ChequePanelGridv.Visible = true;
        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }

    }
    public DataSet PopulateControls()
    {

        DataSet ds = new DataSet();
        ds = populategrddat;
        return ds;
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

    protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;


            populategrd();// DisplayGrid();

            GridViewRow row = GridView1.Rows[e.NewEditIndex];
           //// GridView1.Rows[e.NewEditIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();

           ////string serial = row.Cells[1].Text;
           ////int srn = int.Parse(serial);


           ////DropDownList d2 = (DropDownList)row.FindControl("DropDownList7");


            ////DropDownList ddlControl = new DropDownList();
            ////ddlControl = (DropDownList)row.FindControl("DropDownList1");
            ////string Fact = ddlControl.SelectedValue;

            ////DropDownList d2 = (DropDownList)row.FindControl("DropDownList7");

            ////DataSet ds = new DataSet();
            ////string qry = "SELECT distinct [DepartmentName] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

            ////ds = SearchData(qry);
            ////d2.Items.Clear();
            ////if (ds.Tables[0].Rows.Count > 0)
            ////{
            ////    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
            ////    {
            ////        d2.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
            ////    }

            ////}
            //query = "[Id],[CourseOfStudyName],[FacultyName],[DepartmentName],[Duration Of Study]
            //,[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] order by DepartmentName";
            //[MinCrdtEarned],[MinCrdtRegistered],[MinCGPA]

            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = true;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = true;
            row.Cells[6].Enabled = false;
            row.Cells[7].Enabled = false;
            row.Cells[8].Enabled = true;
            row.Cells[9].Enabled = true;
            row.Cells[10].Enabled = true;
            row.Cells[11].Enabled = true;
            //row.Cells[8].Enabled = true;
            //row.Cells[9].Enabled = true;
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
            GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();

            GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();


            //string Grdvfaculty = ((DropDownList)row.FindControl("DropDownList1")).SelectedItem.Value;
            //string Grdvdept = ((DropDownList)row.FindControl("DropDownList7")).SelectedItem.Value;

            string Duration = ((DropDownList)row.FindControl("DropDownList3")).SelectedItem.Value;
            string Honour = ((DropDownList)row.FindControl("DropDownList4")).SelectedItem.Value;
            string ModeOfStudy = ((DropDownList)row.FindControl("DropDownList6")).SelectedItem.Value;
            string Programme = ((DropDownList)row.FindControl("DropDownList5")).SelectedItem.Value;
            GridView1.Rows[e.RowIndex].Cells[9].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[9].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[10].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[10].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[11].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[11].Controls[0])).Text.Trim();

            string Grdvfaculty = row.Cells[3].Text.Trim();
            string Grdvdept = row.Cells[4].Text.Trim();

            string CourseOfStudy = row.Cells[2].Text.Trim().Replace("&", "AND").Trim();
            string MinCrE = "0";// row.Cells[9].Text.Trim();
            string MinCrReg = "0";// row.Cells[10].Text.Trim();
            string MinCG = "0";// row.Cells[11].Text.Trim();
            //query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Course Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] order by DepartmentName";
            //[MinCrdtEarned],[MinCrdtRegistered],[MinCGPA]
            float MinCGPA = 0;
            int MinCrdtearned = 0;
            int MinCrdtReg = 0;
            int id = int.Parse(GridView1.ToolTip);
            if (MinCrE != "")
            {
                if (int.TryParse(MinCrE, out MinCrdtearned))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMinCrdtearned.Focus();
                return;
            }

            //
            if (MinCrReg != "")
            {
                if (int.TryParse(MinCrReg, out MinCrdtReg))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMinCrdtReg.Focus();
                return;
            }

            //
            if (MinCG != "")
            {
                if (float.TryParse(MinCG, out MinCGPA))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMinCGPA.Focus();
                return;
            }


            GridView1.EditIndex = -1;

            string uniqItm = CourseOfStudy + "|" + Grdvfaculty + "|" + Grdvdept + "|" + Duration + "|" + Honour + "|" + Programme + "|" + ModeOfStudy;
            //DDListDept
            string Qry = "SELECT * FROM [CourseOfStudy] where [UniqueItem] = '" + uniqItm + "' and CourseOfStudyID != " + id + "";


            auditInfo = new AuditInfo();
           
            auditInfo.Action = "Update Course of study";
            auditInfo.Usergroup = Group;
            auditInfo.Userid = ID;
            
            auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            auditInfo.Computer = User.Identity.Name;
            auditInfo.Hostname = Request.UserHostName;
            auditInfo.IPAddress = Request.UserHostAddress;
            




            if (Existed(Qry))
            {
                msg = "Data existed before";
                showmassage(msg);
                return;
            }

            string qry = "SELECT * FROM [Students] where [CourseOfStudyID] =" + id + "";
            //[CourseOfStudyID],[CourseOfStudyName],[Duration],[DepartmentID],[Programme],[ModeOfStudy],[DepartmentName],[FacultyName],[Honours],[MinCrdtEarned],[MinCrdtRegistered],[MinCGPA],[FacultyID],[UniqueItem]
            if (!Existed(qry))
            {
                qry = "UPDATE [CourseOfStudy] SET CourseOfStudyName = '" + CourseOfStudy + "', [Duration]='" + Duration + "',[Programme]='" + Programme + "',[ModeOfStudy] ='" + ModeOfStudy + "',[Honours] ='" + Honour + "',[MinCrdtEarned] = " + MinCrdtearned + ",[MinCrdtRegistered] = " + MinCrdtReg + ",[MinCGPA] = " + MinCGPA + " where [CourseOfStudyID] = " + id + "";
                PerformUpdate(qry);
                auditInfo.Msg = qry;
                sendtoAuditQ(auditInfo);
            }
            else
            {
                //[MinCrdtEarned],[MinCrdtRegistered],[MinCGPA]
                qry = "UPDATE [CourseOfStudy] SET [MinCrdtEarned] = " + MinCrdtearned + ",[MinCrdtRegistered] = " + MinCrdtReg + ",[MinCGPA] = " + MinCGPA + " where [CourseOfStudyID] = " + id + "";
                PerformUpdate(qry);
                auditInfo.Msg = qry;
                sendtoAuditQ(auditInfo);

                qry = "UPDATE [Students] SET CourseOfStudyName = '" + CourseOfStudy + "', [Duration]='" + Duration + "',[Honours] ='" + Honour + "' where [CourseOfStudyID] = " + id + " and [Programme]='" + Programme + "' and [ModeOfStudy] ='" + ModeOfStudy + "'";
                PerformUpdate(qry);
                auditInfo.Msg = qry;
                sendtoAuditQ(auditInfo);

                msg = "Please, try to update departmental course for this course of study";
                showmassage(msg);
                // return;
            }

            
            populategrd();

            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = false;
            row.Cells[6].Enabled = false;
            row.Cells[7].Enabled = false;
            row.Cells[8].Enabled = false;
            row.Cells[9].Enabled = false;
            row.Cells[10].Enabled = false;
            row.Cells[11].Enabled = false;
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
    private void ClearControls(Control control)
    {
        for (int i = control.Controls.Count - 1; i >= 0; i--)
        {
            ClearControls(control.Controls[i]);
        }

        if (!(control is TableCell))
        {
            if (control.GetType().GetProperty("SelectedItem") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                try
                {
                    literal.Text =
                        (string)control.GetType().GetProperty("SelectedItem").
                            GetValue(control, null);
                }
                catch
                { }
                control.Parent.Controls.Remove(control);
            }
            else if (control.GetType().GetProperty("Text") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                literal.Text =
                    (string)control.GetType().GetProperty("Text").
                        GetValue(control, null);
                control.Parent.Controls.Remove(control);
            }
        }
        return;
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        try
        {
            switch (e.Item.Text.Trim())
            {
                case "Export":


                    //showgrid(false);// ShowShareType(false);
                    string filename = String.Format("Results_{0}_{1}_{2}.xls",
                      DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("dd"), DateTime.Now.ToString("HHmmss"));

                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.Charset = "";

                    // SetCacheability doesn't seem to make a difference (see update)
                    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

                    Response.ContentType = "application/ms-excel";

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

                    // Replace all gridview controls with literals
                    ClearControls(GridView1);

                    // Throws exception: Control 'ComputerGrid' of type 'GridView'
                    // must be placed inside a form tag with runat=server.
                    // ComputerGrid.RenderControl(htmlWrite);

                    // Alternate to ComputerGrid.RenderControl above
                    System.Web.UI.HtmlControls.HtmlForm form
                        = new System.Web.UI.HtmlControls.HtmlForm();
                    Controls.Add(form);
                    form.Controls.Add(GridView1);
                    form.RenderControl(htmlWriter);

                    Response.Write(stringWriter.ToString());
                    Response.End();

                    // ShowShareType(true);
                    //showgrid2(true);
                    break;

                //case "Edit":

                //    Display();

                //    break;
                case "Cancel Edit":
                    ////editinfo2 = false;
                    ////SupId2 = "";
                    ////BtnUpdate2.Enabled = false;

                    ////BtnSubmit2.Enabled = true;
                    ////screenclear();
                    ////ChkboxShareType2.Items.Clear();
                    ////LoadGrid();
                    ////LoadGridview();
                    populategrd();

                    break;


            }
        }
        catch (Exception ex)
        {

            throw;
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

            //[SYMBOL],[State],[StateID]
            obj3 = GridView1.Rows[e.RowIndex].Cells[3].Text;
            obj2 = GridView1.Rows[e.RowIndex].Cells[2].Text;
            obj1 = GridView1.Rows[e.RowIndex].Cells[1].Text;
            //[Id],[Course Of Study],[Faculty],[Department],[Duration Of Study],[Programme],[Mode Of Study],[Honours],[Min Credit Earned],[Min Credit Registered],[Min CGPA]";

            string qry = "";
            qry = "SELECT * FROM [Students] where [CourseOfStudyID] =" + obj1 + "";

            if (!Existed(qry))
            {

                qry = "Delete from [CourseOfStudy] where [CourseOfStudyID] = " + obj1 + "";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                msg = "State name has been used by student, it can not be deleted";
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
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    protected void AddLecturerToList_Click(object sender, EventArgs e)
    {
        try
        {


            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID);


            string Setupby = "";

            Setupby = ID;

            if (ID == "")
            {
                msg = "Please, refresh page";
                showmassage(msg);
                return;
            }
            int valid = 0;

            //if (Group.ToUpper() == "WEB MASTER")
            //{
            //    valid++;
            //}

            if (Group.ToUpper() != "FACULTY ADMIN")
            {
                valid++;
            }
            if (Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                valid++;
            }



            if (Group.ToUpper() == "FACULTY ADMIN" && DDListFaculty.Text.Trim().ToLower() == userFact.ToLower())
            {
                valid++;
            }
            

            if (Group.ToUpper() == "DEPARTMENT ADMIN" && DDListFaculty.Text.Trim().ToLower() == userFact.ToLower() && DDListDept.Text.Trim().ToLower() == userDept.ToLower())
            {
                valid++;

            }
            

            if( valid == 0)
            {
                msg = "you have no right to setup course of study for this faculty";
                showmassage(msg);
                return;
            }

            if (TxtCourseOfStudy.Text == "")
            {
                msg = "Enter course of study";
                showmassage(msg);
                TxtCourseOfStudy.Focus();
                return;
            }
            else
            {
                TxtCourseOfStudy.Text = TxtCourseOfStudy.Text.Trim().ToUpper().Replace("'", "''").Replace("=", ":").Replace("&", "AND").Trim();
            }

            if (DDListFaculty.Text != "")
            {
                DDListFaculty.Text = DDListFaculty.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Faculty Name";
                showmassage(msg);
                DDListFaculty.Focus();
                //PaymentTypePanel1.Visible = true;
                return;
            }

            if (DDListDept.Text != "")
            {
                DDListDept.Text = DDListDept.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Departmental Name";
                showmassage(msg);
                DDListDept.Focus();
                //PaymentTypePanel1.Visible = true;
                return;
            }

            if (DDListModeStudy.Text != "")
            {
                DDListModeStudy.Text = DDListModeStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Mode oF Study";
                showmassage(msg);
                DDListModeStudy.Focus();
                //PaymentTypePanel1.Visible = true;
                return;
            }

            if (DDListProgramme.Text != "")
            {
                DDListProgramme.Text = DDListProgramme.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Programme";
                showmassage(msg);
                DDListProgramme.Focus();
                //PaymentTypePanel1.Visible = true;
                return;
            }

            if (DDListDuration.Text != "")
            {
                DDListDuration.Text = DDListDuration.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select course duration";
                showmassage(msg);
                DDListDuration.Focus();

                return;
            }

            if (DDListHonour.Text != "")
            {
                DDListHonour.Text = DDListHonour.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Level";
                showmassage(msg);
                DDListHonour.Focus();
                //PaymentTypePanel1.Visible = true;
                return;
            }

            //uniqItm = cos.CourseOfStudyName + "|" + cos.FacultyName + "|" + cos.DepartmentName + "|" + cos.Duration + "|" + cos.Honours + "|" + cos.Programme + "|" + cos.ModeOfStudy;
            //TxtMinCGPA.Text="";
            // TxtMinCrdtearned.Text="";
            //TxtMinCrdtReg.Text="";

            float MinCGPA = 0;
            int MinCrdtearned = 0;
            int MinCrdtReg = 0;

            if (TxtMinCrdtearned.Text.Trim() != "")
            {
                if (int.TryParse(TxtMinCrdtearned.Text.Trim(), out MinCrdtearned))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                TxtMinCrdtearned.Focus();
                return;
            }

            //
            if (TxtMinCrdtReg.Text.Trim() != "")
            {
                if (int.TryParse(TxtMinCrdtReg.Text.Trim(), out MinCrdtReg))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                TxtMinCrdtReg.Focus();
                return;
            }

            //
            if (TxtMinCGPA.Text.Trim() != "")
            {
                if (float.TryParse(TxtMinCGPA.Text.Trim(), out MinCGPA))
                {
                }
                else
                {

                }
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                TxtMinCGPA.Focus();
                return;
            }
            string uniqItm = "";
            int DeptID = 0;
            int FacultId = 0;


            if (DepartmentID.ContainsKey(DDListDept.Text.Trim()))
            {
                DeptID = int.Parse(DepartmentID[DDListDept.Text.Trim()].ToString());
            }

            if (FacultyID.ContainsKey(DDListFaculty.Text.Trim()))
            {
                FacultId = int.Parse(FacultyID[DDListFaculty.Text.Trim()].ToString());
            }
            uniqItm = TxtCourseOfStudy.Text.Trim() + "|" + DDListFaculty.Text.Trim() + "|" + DDListDept.Text.Trim() + "|" + DDListDuration.Text.Trim() + "|" + DDListHonour.Text.Trim() + "|" + DDListProgramme.Text.Trim() + "|" + DDListModeStudy.Text.Trim();
            //DDListDept
            string Qry = "SELECT * FROM [CourseOfStudy] where [UniqueItem] = '" + uniqItm + "'";

            if (Existed(Qry))
            {
                msg = "Course of study existed befor for this programme and study mode , try to edit";
                showmassage(msg);
                //TxtFacPrefix.Focus();
                return;
            }
            else
            {


                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                Qry = "INSERT INTO [CourseOfStudy]([CourseOfStudyName],[Duration],[Programme],[ModeOfStudy],[DepartmentName],[FacultyName],[Honours],[MinCrdtEarned],[MinCrdtRegistered],[MinCGPA],[FacultyID],[DepartmentID],[UniqueItem]) VALUES ('" + TxtCourseOfStudy.Text.Trim() + "','" + DDListDuration.Text.Trim() + "','" + DDListProgramme.Text.Trim() + "','" + DDListModeStudy.Text.Trim() + "','" + DDListDept.Text.Trim() + "','" + DDListFaculty.Text.Trim() + "','" + DDListHonour.Text.Trim() + "'," + MinCrdtearned + "," + MinCrdtReg + "," + MinCGPA + "," + FacultId + "," + DeptID + ",'" + uniqItm + "')";
                PerformInsert(Qry);
                populategrd();


                //ID = HttpContext.Current.User.Identity.Name;
                //Group = (string)Cache[HttpContext.Current.User.Identity.Name];
                auditInfo = new AuditInfo();
                string msg1 = Qry;
                auditInfo.Action = "Course of study setup";
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;
                auditInfo.Msg = msg1;
                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;                
                sendtoAuditQ(auditInfo);


                TxtCourseOfStudy.Text = "";
                TxtMinCGPA.Text = "0";
                TxtMinCrdtearned.Text = "0";
                TxtMinCrdtReg.Text = "0";

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
    protected void BtnRemoveAll_Click(object sender, EventArgs e)
    {

    }
    protected void BtnRemovSingle_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        string header = exportheader;
        string filename = Exportfilename;
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
            msg = ex.Message + "||" + ex.StackTrace;
            //log.Error(msg);
            showmassage(msg);
            return;
        }
    }
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        populategrd();
        //populategrd();

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {



        ////DropDownList ddl = (DropDownList)sender;
        ////string Fact = ddl.SelectedValue;//((DropDownList)sender).SelectedValue;
        ////GridViewRow row = (GridViewRow)ddl.NamingContainer;
        ////row.EnableViewState = true;
        ////DropDownList d2 = (DropDownList)row.FindControl("DropDownList7");

        ////DataSet ds = new DataSet();
        ////string qry = "SELECT distinct [DepartmentName] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

        ////ds = SearchData(qry);
        ////d2.Items.Clear();
        ////if (ds.Tables[0].Rows.Count > 0)
        ////{
        ////    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
        ////    {
        ////        d2.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
        ////    }

        ////}
        ////else
        ////{
        ////    d2.Items.Add("");
        ////}





    }
}
