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
using System.Text;



public partial class Admin_MinCredit : System.Web.UI.Page
{
    string str = System.Configuration.ConfigurationManager.AppSettings["conn"];
    string str2 = System.Configuration.ConfigurationManager.AppSettings["conn2"];

    private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string PinInfoUpdate = ".\\private$\\" + ConfigurationManager.AppSettings["PinInfoUpdate"];

    private static string msg = "";

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
    private static Hashtable CourseOfStudyID = null;

    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];
    //private static CPermit cp = null;
    //private static CWritetoqueue rq = null;
    private static string Group = "";
    private static string ID = "";
    private static string userDept = "";
    private static string userFact = "";
    private static string usercosstudy = "";
    public DataSet populategrddat = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.Width = 900;

        AllFaculty = null;
        DDListSemester.Visible = false;
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
            getuserinfo(ID);
            AllFaculty = null;

            LoadProgramme();
            LoadModeOfStudy();
            LoadFaculty();
            LoadLevels();
            LoadSemester();

            populategrd();
        }

        GridView1.Width = GViewWidth;
    }



    private void LoadLevels()
    {
        try
        {
            DDListLevel.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [AcademicLevel] FROM [Levels] order by [AcademicLevel] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListLevel.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
    private void LoadSemester()
    {
        try
        {
            DDListSemester.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [Semester] FROM [Semesters] order by [Semester] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListSemester.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
                //Loadcourses();
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
    private void getuserinfo(string ID)
    {
        try
        {

            userFact = "";
            userDept = "";
            usercosstudy = "";
            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],DepartmentName,[CourseOfStudy] FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    userFact = ds.Tables[0].Rows[jj][0].ToString();
                    userDept = ds.Tables[0].Rows[jj][1].ToString();
                    usercosstudy = ds.Tables[0].Rows[jj][2].ToString();
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
                    //LoadCouseofstudyId();
                    LoadProgramme();
                    LoadModeOfStudy();

                    LoadFaculty();

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

    protected void DDListDept_Changed(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept.Text.Trim(), DDListFaculty.Text.Trim());
    }
    private void LoadFaculty()
    {

        try
        {
            DDListFaculty.Items.Clear();
            //FacultyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],[FacultyID] FROM [Faculty] order by [FacultyName] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListFaculty.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    //FacultyID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                }
                loadDept(DDListFaculty.Text.Trim());
            }
            else
            {
                DDListFaculty.Items.Clear();
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

                    LoadCourseOfStudy(DDListDept.Text.Trim(), Fact);
                }
                else
                {
                    DDListCourseOfStudy.Items.Clear();
                    DDListDept.Items.Clear();
                }
                //TxtCourseOfStudy.Text = "";



                //Load dept id


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

    private void LoadCourseOfStudy(string Dept, string fact)
    {

        try
        {
            DDListCourseOfStudy.Items.Clear();


            CourseOfStudyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [CourseOfStudyName],[CourseOfStudyID]  FROM [CourseOfStudy] where [DepartmentName]= '" + Dept + "' and [FacultyName] = '" + fact + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "' order by [CourseOfStudyName] asc";

            //string qry = "SELECT [CourseOfStudyName],[CourseOfStudyID]  FROM [CourseOfStudy] where [DepartmentName]= '" + Dept + "' and [FacultyName] = '" + fact + "'";

            ds = SearchData(qry);
            string name = "";
            string Cosid = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                //DDListCourseOfStudy.Items.Add("All");
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    name = ds.Tables[0].Rows[jj][0].ToString();
                    Cosid = ds.Tables[0].Rows[jj][1].ToString();
                    DDListCourseOfStudy.Items.Add(name);
                    if (!CourseOfStudyID.ContainsKey(name.Trim()))
                    {
                        CourseOfStudyID.Add(name.Trim(), int.Parse(Cosid));
                    }
                }
                //TxtCreditLoad.Text = "";
            }
            else
            {
                DDListCourseOfStudy.Items.Clear();
                DDListCourseOfStudy.Items.Add("");
                msg = "Setup course of study for this department";
                showmassage(msg);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
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
        //if (Group.ToLower().Trim() == "web master" || Group.ToLower().Trim() == "faculty admin" || Group.ToLower().Trim() == "department admin")
        //{
        //    succ = true;
        //}
        succ = true;
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

            //query = "SELECT [CourseOfStudyName] as [Course Of Study],[Duration] as [Duration Of Study],[DepartmentName] as [Department],[FacultyName] as [Faculty],[Honours] as [Honor]  FROM [CourseOfStudy]";

            int valid = 0;
            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                //query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Course Of Study],[FacultyName] as [Faculty],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] order by DepartmentName";
                query = "select [Srn], [CourseOfStudy],[AcademicLevel] as [Level],[MaxCore] as [Max Core],[MinCore] as [MinCore],[MaxElective] as [Max Elective],[MinElective] as [Min Elective],[MaxCreditLoad] as [Max Credit Load],[MinCreditLoad] as [Min Credit Load],[Programme],[ModeOfStudy] as [Mode Of Study] FROM [CreditLoad]";
                
                valid++;
            }
            if (Group.ToUpper() == "FACULTY ADMIN")
            {
               // query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Course Of Study],[FacultyName] as [School],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] where FacultyName='" + userFact + "' order by DepartmentName";
                query = "select [Srn], [CourseOfStudy],[AcademicLevel] as [Level],[MaxCore] as [Max Core],[MinCore] as [MinCore],[MaxElective] as [Max Elective],[MinElective] as [Min Elective],[MaxCreditLoad] as [Max Credit Load],[MinCreditLoad] as [Min Credit Load],[Programme],[ModeOfStudy] as [Mode Of Study] FROM [CreditLoad]";
                
                valid++;
            }
            if (Group.ToUpper() == "DEPARTMENT ADMIN")
            {
                //query = "select [CourseOfStudyID] as [Id],[CourseOfStudyName] as [Course Of Study],[FacultyName] as [School],[DepartmentName] as [Department],[Duration] as [Duration Of Study],[Programme],[ModeOfStudy] as [Mode Of Study],[Honours],[MinCrdtEarned] as [Min Credit Earned],[MinCrdtRegistered] as [Min Credit Registered],[MinCGPA] as [Min CGPA] FROM [CourseOfStudy] where FacultyName='" + userFact + "' and DepartmentName ='" + userDept + "' order by DepartmentName";
                query = "select [Srn], [CourseOfStudy],[AcademicLevel] as [Level],[MaxCore] as [Max Core],[MinCore] as [MinCore],[MaxElective] as [Max Elective],[MinElective] as [Min Elective],[MaxCreditLoad] as [Max Credit Load],[MinCreditLoad] as [Min Credit Load],[Programme],[ModeOfStudy] as [Mode Of Study] FROM [CreditLoad]  where [CourseOfStudy]='" + usercosstudy + "'";
                
                valid++;
            }
            

            if (valid > 0)
            {
                exportheader = "[Srn], [CourseOfStudy],[AcademicLevel],[],[Max Core], [MinCore], [Max Elective], [Min Elective], [Max Credit Load], [Min Credit Load],[Programme],[Mode Of Study]";

                Exportfilename = "Credit Load";
                GridCaption = "Credit Load";
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
            PanelGrid.Visible = true;
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


            


            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = true;
            row.Cells[6].Enabled = true;
            row.Cells[7].Enabled = true;
            row.Cells[8].Enabled = true;
            row.Cells[9].Enabled = true;
            row.Cells[10].Enabled = true;
            row.Cells[11].Enabled = false;
           // row.Cells[12].Enabled = false;

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

            

            GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();

            GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[6].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[6].Controls[0])).Text.Trim();

            GridView1.Rows[e.RowIndex].Cells[7].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[7].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[8].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[8].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[9].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[9].Controls[0])).Text.Trim();
           
            //query = "select [Srn], [CourseOfStudy],[AcademicLevel],[Semester]
            //,[MaxCore],[MinCore],[MaxElective],[MinElective],[MaxCreditLoad],[MinCreditLoad],[Programme],[ModeOfStudy] as [Mode Of Study] FROM [CreditLoad]";
                

            string Srn = row.Cells[1].Text.Trim();
            int srn = int.Parse(Srn);
            string MaxCore1 = row.Cells[4].Text.Trim();
            string MinCore1 = row.Cells[5].Text.Trim();
            string MaxElective1 = row.Cells[6].Text.Trim();
            string MinElective1 = row.Cells[7].Text.Trim();
            string MaxCredit1 = row.Cells[8].Text.Trim();
            string MinCredit1 = row.Cells[9].Text.Trim();


            int MaxCore = 0;
            int MinCore = 0;
            int MaxElective = 0;
            int MinElective = 0;
            int MaxCredit = 0;
            int MinCredit = 0;

            if (int.TryParse(MaxCore1, out MaxCore))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                // TxtMaxCore.Focus();
                return;
            }

            //
            if (int.TryParse(MinCore1, out MinCore))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                // TxtMinCore.Focus();
                return;
            }

            //
            if (int.TryParse(MaxElective1, out MaxElective))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMaxElective.Focus();
                return;
            }

            if (int.TryParse(MinElective1, out MinElective))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMinElective.Focus();
                return;
            }

            if (int.TryParse(MaxCredit1, out MaxCredit))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMaxCredit.Focus();
                return;
            }
            //
            if (int.TryParse(MinCredit1, out MinCredit))
            {
            }
            else
            {
                msg = "Enter numeric value";
                showmassage(msg);
                //TxtMinCredit.Focus();
                return;
            }


            //Qry = ",[MaxCore],[MinCore],[MaxElective],[MinElective],[MaxCreditLoad],[MinCreditLoad],[Programme],[ModeOfStudy],[CourseOfStudy]) VALUES (" + CosStudyID + ",'" + DDListLevel.Text.Trim() + "','" + DDListSemester.Text.Trim() + "'," + MaxCore + "," + MinCore + "," + MaxElective + "," + MinElective + "," + MaxCredit + "," + MinCredit + ",'" + DDListProgramme.Text + "','" + DDListModeStudy.Text + "','" + DDListCourseOfStudy.Text + "')";

            GridView1.EditIndex = -1;

            string qry = "UPDATE [CreditLoad] SET MaxCore = " + MaxCore + ", MinCore=" + MinCore + ", MaxElective=" + MaxElective + ", MinElective=" + MinElective + ", MaxCreditLoad=" + MaxCredit + ", MinCreditLoad=" + MinCredit + " where [srn] = " + srn + "";
            PerformUpdate(qry);
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
            //row.Cells[12].Enabled = false;
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
            qry = "SELECT * FROM [Applicants] where [State] ='" + obj2 + "' or [State] ='" + obj3 + "'";

            if (!Existed(qry))
            {

                qry = "Delete from [State] where [StateID] = '" + obj3 + "'";

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
            string Setupby = "";

            Setupby = ID;

            if (ID == "")
            {
                msg = "Please, refresh page";
                showmassage(msg);
                return;
            }

            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {

            }
            if (Group.ToUpper() == "FACULTY ADMIN" && DDListFaculty.Text.Trim().ToLower() != userFact.ToLower())
            {
                msg = "you have no right to setup course of study for this faculty";
                showmassage(msg);
                return;
            }

            if (Group.ToUpper() == "DEPARTMENT ADMIN" && DDListFaculty.Text.Trim().ToLower() != userFact.ToLower() && DDListDept.Text.Trim().ToLower() != userDept.ToLower())
            {
                msg = "you have no right to setup course of study for this faculty";
                showmassage(msg);
                return;

            }



            if (DDListCourseOfStudy.Text != "")
            {
                DDListCourseOfStudy.Text = DDListCourseOfStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select course of study";
                showmassage(msg);
                return;
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
                return;
            }
            if (DDListLevel.Text != "")
            {
                DDListLevel.Text = DDListLevel.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Level";
                showmassage(msg);
                return;
            }

            if (DDListSemester.Text != "")
            {
                DDListSemester.Text = DDListSemester.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Semester";
                showmassage(msg);
                return;
            }

            if (DDListModeStudy.Text != "")
            {
                DDListModeStudy.Text = DDListModeStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select study mode";
                showmassage(msg);
                return;
            }
            //uniqItm = cos.CourseOfStudyName + "|" + cos.FacultyName + "|" + cos.DepartmentName + "|" + cos.Duration + "|" + cos.Honours + "|" + cos.Programme + "|" + cos.ModeOfStudy;
            //TxtMinCGPA.Text="";
            // TxtMinCrdtearned.Text="";
            //TxtMinCrdtReg.Text="";



            int MaxCore = 0;
            int MinCore = 0;
            int MaxElective = 0;
            int MinElective = 0;
            int MaxCredit = 0;
            int MinCredit = 0;

            if (TxtMaxCore.Text.Trim() != "")
            {
                if (int.TryParse(TxtMaxCore.Text.Trim(), out MaxCore))
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
                TxtMaxCore.Focus();
                return;
            }

            //
            if (TxtMinCore.Text.Trim() != "")
            {
                if (int.TryParse(TxtMinCore.Text.Trim(), out MinCore))
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
                TxtMinCore.Focus();
                return;
            }

            //
            if (TxtMaxElective.Text.Trim() != "")
            {
                if (int.TryParse(TxtMaxElective.Text.Trim(), out MaxElective))
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
                TxtMaxElective.Focus();
                return;
            }

            if (TxtMinElective.Text.Trim() != "")
            {
                if (int.TryParse(TxtMinElective.Text.Trim(), out MinElective))
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
                TxtMinElective.Focus();
                return;
            }

            if (TxtMaxCredit.Text.Trim() != "")
            {
                if (int.TryParse(TxtMaxCredit.Text.Trim(), out MaxCredit))
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
                TxtMaxCredit.Focus();
                return;
            }
            if (TxtMinCredit.Text.Trim() != "")
            {
                if (int.TryParse(TxtMinCredit.Text.Trim(), out MinCredit))
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
                TxtMinCredit.Focus();
                return;
            }
            string uniqItm = "";
            int DeptID = 0;
            int FacultId = 0;

            
            int CosStudyID = 0;
            if (CourseOfStudyID.ContainsKey(DDListCourseOfStudy.Text.Trim()))
            {
                CosStudyID = int.Parse(CourseOfStudyID[DDListCourseOfStudy.Text.Trim()].ToString());               
            }

            string Qry = "SELECT * FROM [CreditLoad] where [CourseOfStudyID] = " + CosStudyID + " and [AcademicLevel] = '" + DDListLevel.Text.Trim() + "' and [Semester] = '" + DDListSemester.Text.Trim() + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy] = '" + DDListModeStudy.Text.Trim() + "'";

            if (Existed(Qry))
            {
                msg = "Credit load existed before for the above parameters, try to edit";
                showmassage(msg);
                //TxtFacPrefix.Focus();
                return;
            }
            else
            {


                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                Qry = "INSERT INTO [CreditLoad]([CourseOfStudyID],[AcademicLevel],[Semester],[MaxCore],[MinCore],[MaxElective],[MinElective],[MaxCreditLoad],[MinCreditLoad],[Programme],[ModeOfStudy],[CourseOfStudy]) VALUES (" + CosStudyID + ",'" + DDListLevel.Text.Trim() + "','" + DDListSemester.Text.Trim() + "'," + MaxCore + "," + MinCore + "," + MaxElective + "," + MinElective + "," + MaxCredit + "," + MinCredit + ",'" + DDListProgramme.Text + "','" + DDListModeStudy.Text + "','" + DDListCourseOfStudy.Text + "')";
                PerformInsert(Qry);
                populategrd();                
                
                TxtMaxCore.Text = "0";
                TxtMinCore.Text = "0";
                TxtMaxElective.Text = "0";
                TxtMinElective.Text = "0";
                TxtMaxCredit.Text = "0";
                TxtMinCredit.Text = "0";

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

    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        string Fact = ddl.SelectedValue;//((DropDownList)sender).SelectedValue;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        DropDownList d2 = (DropDownList)row.FindControl("DropDownList7");

        DataSet ds = new DataSet();
        string qry = "SELECT distinct [DepartmentName] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

        ds = SearchData(qry);
        d2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
            {
                d2.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
            }

        }
        else
        {
            d2.Items.Add("");
        }

    }
}
