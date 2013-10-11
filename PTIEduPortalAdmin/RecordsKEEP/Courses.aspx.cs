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
using System.Data.OleDb;
using AuditLogInfo;



public partial class Admin_Courses : System.Web.UI.Page
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

    private static int CurrentCount = 0;

    private AuditLogInfo.AuditInfo auditInfo = null;
    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogUI"];
    


    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.Width = 900;
        DDListSemester.Visible = false;
        AllFaculty = null;

        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            if (Group.ToLower().Trim() == null)
            {               
                return;
            }
            else
            {

            }
        }

        if (!IsPostBack)
        {
            TabContainer1.ActiveTabIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();

            AllFaculty = null;
            LoadFaculty();
            LoadLevels();
            LoadSemester();
            //getuserinfo(ID);
            populategrd();
        }
        this.BtnUpload2.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.LinkButton2, ""));

        GridView1.Width = GViewWidth;
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

                    LoadLevels();
                    LoadSemester();
                    LoadFaculty();

                    //populategrd();
                    TxtCourseName.Text = "";
                    TxtCourseCode.Text = "";
                    TxtPassmark.Text = "0";

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
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        //LoadPix();
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
                DataSet ds = new DataSet();
                string qry = "SELECT distinct [DepartmentName],[DepartmentId] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        DDListDept.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                        DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                    }
                    populategrd();
                }
                else
                {
                    DDListDept.Items.Clear();
                }
                //TxtCourseOfStudy.Text = "";
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
    //
    protected void DDListDept_Changed(object sender, EventArgs e)
    {
        populategrd();
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
    private void populategrd()
    {
        try
        {

            //query = "SELECT [CourseOfStudyName] as [Course Of Study],[Duration] as [Duration Of Study],[DepartmentName] as [Department],[FacultyName] as [Faculty],[Honours] as [Honor]  FROM [CourseOfStudy]";

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID);



            int valid = 0;
            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                query = "SELECT Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],[PassMark] as [Pass Mark], [FacultyName] as [Faculty],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level] FROM [Courses] where [FacultyName] ='" + DDListFaculty.Text.Trim() + "' and DepartmentName='" + DDListDept.Text.Trim() + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]='" + DDListSemester.Text.Trim() + "'";
                valid++;
            }

            if (Group.ToUpper() == "FACULTY ADMIN")
            {
                query = "SELECT Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],[PassMark] as [Pass Mark], [FacultyName] as [Faculty],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level] FROM [Courses] where [FacultyName] ='" + userFact + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]='" + DDListSemester.Text.Trim() + "'";
                valid++;

            }
            if (Group.ToUpper() == "DEPARTMENT ADMIN")
            {
                query = "SELECT Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],[PassMark] as [Pass Mark], [FacultyName] as [Faculty],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level] FROM [Courses] where [FacultyName] ='" + userFact + "' and DepartmentName='" + userDept + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]='" + DDListSemester.Text.Trim() + "'";
                valid++;

            }
            //


            if (valid > 0)
            {
                //query = "SELECT Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],[PassMark] as [Pass Mark], [FacultyName] as [Faculty],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level] FROM [Courses] where [FacultyName] ='" + DDListFaculty.Text.Trim() + "' and DepartmentName='" + DDListDept.Text.Trim() + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]='" + DDListSemester.Text.Trim() + "'";

                exportheader = "Srn,[Course Title],[Course Code],[Pass Mark], [Faculty],[Department],[Academic Level]";

                Exportfilename = "Courses" + DDListLevel.Text.Trim();
                GridCaption = "Courses for " + " " + "for" + " " + DDListLevel.Text.Trim();
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

            //// //Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],
            //// //[PassMark] as [Pass Mark], [FacultyName] as [School],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level],[Semester]

            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = true;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = true;
            row.Cells[5].Enabled = false;
            row.Cells[6].Enabled = false;
            row.Cells[7].Enabled = true;
            //row.Cells[8].Enabled = true;




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
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[6].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[6].Controls[0])).Text.Trim();
            string level = ((DropDownList)row.FindControl("DropDownList1")).SelectedItem.Value;
            string semester = DDListSemester.Text.Trim();// ((DropDownList)row.FindControl("DropDownList2")).SelectedItem.Value;



            //// //Srn,[CourseTitle],[CourseCode],
            //// //[PassMark],[FacultyName],[DepartmentName],[AcademicLevel],[Semester]



            string obj1 = row.Cells[1].Text.Trim();//srn
            string obj2 = row.Cells[2].Text.Trim();//costitle
            string obj3 = row.Cells[3].Text.Trim();//coscode
            string obj4 = row.Cells[4].Text.Trim();//passmark
            string obj5 = row.Cells[5].Text.Trim();
            string obj6 = row.Cells[6].Text.Trim();
            string obj7 = level;// row.Cells[7].Text.Trim();//level
            string obj8 = semester;// row.Cells[8].Text.Trim();//semester

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            if (obj2 != "" && obj4 != "" && obj7 != "" && obj8 != "")
            {

                string CreatedBy = ID;// rq.Logonpermit.Userid;
                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                GridView1.EditIndex = -1;
                int srn = int.Parse(obj1);
                string qry = "";

                int Passmark = 0;

                if (int.TryParse(obj4, out Passmark))
                {

                }
                else
                {
                    msg = "Enter a valid pass mark for this course";
                    showmassage(msg);
                    //TxtPassmark.Focus();
                    return;
                }



                string coscodename = obj3 + "=" + obj2;
                // uniqItm = TxtCourseOfStudy.Text.Trim() + "|" + DDListFaculty.Text.Trim() + "|" + DDListDept.Text.Trim() + "|" + DDListDuration.Text.Trim() + "|" + DDListHonour.Text.Trim() + "|" + DDListProgramme.Text.Trim() + "|" + DDListModeStudy.Text.Trim();
                //DDListDept
                qry = "SELECT * FROM [DeptCourses] where [CourseCode] ='" + obj3 + "'";

                if (!Existed(qry))
                {

                    qry = "UPDATE [Courses] SET [PassMark] = " + Passmark + ", [AcademicLevel]='" + obj7 + "',[Semester]='" + obj8 + "',[CourseTitle] ='" + obj2 + "',[CourseCodeName] ='" + coscodename + "' where [Srn] = " + srn + "";
                    PerformUpdate(qry);

                }
                else
                {
                    qry = "UPDATE [Courses] SET [PassMark] = " + Passmark + " where [Srn] = " + srn + "";
                    PerformUpdate(qry);

                    msg = "Only Pass mark updated successfully";
                    showmassage(msg);
                    // return;
                }


                auditInfo = new AuditInfo();

                auditInfo.Action = "Department Course Update,";
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;

                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;
                auditInfo.Msg = qry;
                sendtoAuditQ(auditInfo);
                populategrd();


            }

            //if (!Existed(qry))
            //{


            //}
            //else
            //{
            //    msg = "Course code used already, try to remove it from department courses";
            //    showmassage(msg);
            //    return;
            //}









            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            //row.Cells[4].Enabled = false;
            //row.Cells[5].Enabled = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string PixFname = FileUpload1.FileName;
            LoadFile(PixFname);
        }
        else
        {
            msg = "Select file to upload";
            showmassage(msg);
            return;
        }


    }

    private void LoadFile(string fname)
    {
        if (fname == "")
        {
            return;
        }

        ID = HttpContext.Current.User.Identity.Name;
        Group = (string)Cache[HttpContext.Current.User.Identity.Name];

        userFact = getuserFact(ID);
        userDept = getuserDept(ID); ;
        usercosstudy = getuserCostudy(ID);

        int valid = 0;
        if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
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
        
        if (valid == 0)
        {
            msg = "you have no right to setup course of study for this faculty";
            showmassage(msg);
            return;
        }

        string CurrentFile = "";
        string ext = "";


        int DeptID = 0;
        int FacultId = 0;


        string Faculty = "";
        string Dept = "";
        string Level = "";
        string semester = "";

        ext = Path.GetExtension(fname);

        try
        {
            if (fname != "")
            {
                string Optype = "";
                String mess = ID;
                if (DDListFaculty.Text != "")
                {
                    Faculty = DDListFaculty.Text.ToUpper().Replace("'", "''").Replace(";", ":");
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
                    Dept = DDListDept.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Departmental Name";
                    showmassage(msg);
                    DDListDept.Focus();
                    //PaymentTypePanel1.Visible = true;
                    return;
                }

                if (DepartmentID.ContainsKey(DDListDept.Text.Trim()))
                {
                    DeptID = int.Parse(DepartmentID[DDListDept.Text.Trim()].ToString());
                }

                if (FacultyID.ContainsKey(DDListFaculty.Text.Trim()))
                {
                    FacultId = int.Parse(FacultyID[DDListFaculty.Text.Trim()].ToString());
                }

                if (DDListLevel.Text != "")
                {
                    Level = DDListLevel.Text.ToUpper();
                }
                else
                {
                    msg = "Select Mode oF Study";
                    showmassage(msg);
                    DDListLevel.Focus();
                    return;
                }

                if (DDListSemester.Text != "")
                {
                    semester = DDListSemester.Text.ToUpper();
                }
                else
                {
                    msg = "Select Programme";
                    showmassage(msg);
                    DDListSemester.Focus();
                    return;
                }


                string filepath = "";
                switch (ext.ToLower().Trim())
                {
                    case ".csv":

                        break;
                    case ".xls":
                    case ".xlsx":

                        filepath = Server.MapPath("~/Received/" + fname);
                        FileUpload1.SaveAs(filepath);

                        TreatFileAdmitedXls(fname, filepath, Faculty, Dept, FacultId, DeptID, semester, Level);
                        break;

                    case ".txt":

                        break;
                    case ".mdb":

                        break;
                    default:
                        msg = "Invalid File Format: Please load only CSV or TXT files only";
                        showmassage(msg);

                        return;
                }

            }
            else
            {
                msg = "Upload document with accepted format";
                showmassage(msg);
                return;
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "School Portal", "");
        }
    }

    private void TreatFileAdmitedXls(string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string semester, string Level)
    {
        string Setupby = "";
        Setupby = ID;
       
        try
        {

            string Currsheet = "";
            int cnt = 0;
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@filepath);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file or cant open sheet";
                showmassage(msg);
                return;
            }
            else
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @filepath + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                //Console.WriteLine("Xls file opened successfully");
                OleDbCommand oCmd1 = null;
                oCmd1 = new OleDbCommand("select * from [" + Currsheet + "]", objConn);
                OleDbDataReader oDr1 = oCmd1.ExecuteReader();


                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                OleDbDataAdapter dat = null;
                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);
                dt = ds.Tables[0];

                if (dt == null)
                {
                    msg = "Empty table submitted " + filepath;
                    showmassage(msg);
                    oDr1.Dispose();
                    oCmd1.Dispose();
                    objConn.Dispose();
                    objConn.Close();
                    return;

                }

                string colName1 = "";
                int valicolcnt = 0;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        colName1 = col.ColumnName;
                        valicolcnt++;
                    }

                    if (valicolcnt >= 2)
                    {

                    }
                    else
                    {
                        msg = "Invalid excel sheet format, kindly download the attached admitted list template.xls as sample format";
                        showmassage(msg);

                        oDr1.Dispose();
                        oCmd1.Dispose();
                        objConn.Dispose();
                        objConn.Close();
                        return;
                    }
                }

                //String[] excelSheets = new String[dt.Rows.Count];
                //int Rows = 0;
                //Rows = dt.Rows.Count;
                //String[] excelSheets1 = new String[dt.Columns.Count];
                //int col = dt.Columns.Count;



                int rc = 0;
                string course = "";
                string coscode = "";


                ID = HttpContext.Current.User.Identity.Name;
                Group = (string)Cache[HttpContext.Current.User.Identity.Name];

                auditInfo = new AuditInfo();
                //string msg1 = Qry;
                auditInfo.Action = "Department Course setup, uploaded";
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;
                
                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;



                while (oDr1.Read())
                {
                    //if (rc != 0)
                    //{
                        if (oDr1.IsDBNull(0))
                        {
                            coscode = "None";
                        }
                        else
                        {
                            coscode = oDr1.GetValue(0).ToString().Replace("'", "''").Trim();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            course = "None";
                        }
                        else
                        {
                            course = oDr1.GetValue(1).ToString().Replace("&", "AND").Trim();
                        }

                        string qry = "";
                        string coscodename = coscode + "=" + course;
                        qry = "SELECT * FROM [Courses] where [CourseCode] = '" + coscode + "'";




                        if (!Existed(qry) && coscode.ToLower() != "none")
                        {
                            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            //string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string semester, string Level
                            qry = "INSERT INTO [Courses] ([FacultyID],[FacultyName],[DepartmentID],[DepartmentName],[AcademicLevel],[Semester],[CourseTitle],[CourseCode],[CreatedDate],[CreatedBy],[CourseCodeName],PassMark) VALUES (" + FacultId + ",'" + Faculty + "'," + DeptID + ",'" + Dept + "','" + Level + "','" + semester + "','" + course + "','" + coscode + "','" + CreatedDate + "','" + ID + "','" + coscodename + "',0)";
                            PerformInsert(qry);

                            cnt++;


                            auditInfo.Msg = qry;
                            sendtoAuditQ(auditInfo);
                        }

                   // }
                    rc++;
                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
                populategrd();
                
                //msg = "Upload successful, total number of data in this excel sheet is: " + " " + cnt.ToString();
                //showmassage(msg);
                msg = "Total number of uploaded data in this excel sheet is: " + " " + cnt.ToString() + " , out of total records of  " + dt.Rows.Count.ToString();
                showmassage(msg);

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowState == DataControlRowState.Edit)//e.Row.RowType == DataControlRowType.DataRow
        {

            //string ConnectionString = “Data Source=192.168.1.23;Initial Catalog=Purchase;User ID=sa;Password=abc;”;

            //SqlConnection objConn = new SqlConnection(ConnectionString);

            ////Find an dropdownlist control in Gridview

            DataRowView drv = e.Row.DataItem as DataRowView;

            if (GridView1.EditIndex == e.Row.RowIndex)
            {
                DropDownList dl = (DropDownList)e.Row.FindControl("DDLIstFact");

                string Qry = "";

                DataSet ds = new DataSet();

                Qry = "select [FacultyName] FROM [Faculty]";

                ds = SearchData(Qry);
                ////dl.DataSource = ds;
                ////dl.DataBind();
                ////dl.Visible = true;
                string Sname = "";
                //GridView1.EditIndex = -1;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        Sname = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                        dl.Items.Add(Sname);
                    }
                    dl.Visible = true;

                }
                //dl.DataValueField = "Srn";
                //dl.DataTextField = "Faculty";
                dl.SelectedValue = drv[3].ToString();

            }
            //dl.DataSource = ds;
            //dl.DataBind();  
            // populategrd();
            //break;
            ////Getting an CategoryID assign from Datakeynames property in Gridview control

            ////<asp:GridView ID=”GridView1″ runat=”server” AutoGenerateColumns=”False”

            ////Font-Names=”Verdana” Font-Size=”9pt” DataKeyNames=”CategoryID” >

            //int catid=int.Parse(GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

            //string query = “select *from tblSubcategory where CategoryID=” +catid;

            //SqlCommand objCmd = new SqlCommand(query, objConn);

            //objCmd.CommandType = CommandType.Text;

            //DataSet objDs = new DataSet();

            //SqlDataAdapter objDa = new SqlDataAdapter(objCmd);

            //objConn.Open();

            //objDa.Fill(objDs);

            //objCmd.ExecuteNonQuery();

            //ddlSubCategory.DataTextField = “SubCategory”;

            //ddlSubCategory.DataValueField = “SubCategoryID”;

            //ddlSubCategory.DataSource = objDs;

            //ddlSubCategory.DataBind();

            //objConn.Close();

            //}

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

            //// //Srn,[CourseTitle],[CourseCode] as [Course Code],
            //// //[PassMark] as [Pass Mark], [FacultyName] as [School],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level],[Semester]

            obj3 = GridView1.Rows[e.RowIndex].Cells[3].Text;
            obj2 = GridView1.Rows[e.RowIndex].Cells[2].Text;
            obj1 = GridView1.Rows[e.RowIndex].Cells[1].Text;
            int srn = int.Parse(obj1);
            string qry = "";
            qry = "SELECT * FROM [DeptCourses] where [CourseCode] ='" + obj3 + "'";

            if (!Existed(qry))
            {

                qry = "Delete from [Courses] where [Srn] = " + srn + "";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                msg = "Course code used already, try to remove it from department courses";
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
    protected void AddLecturerToList_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                msg = "Please, click the file upload button";
                showmassage(msg);
                return;
            }

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID);

            string Setupby = "";
            Setupby = ID;
            string uniqItm = "";
            int DeptID = 0;
            int FacultId = 0;


            if (ID == "")
            {
                msg = "Please, refresh page";
                showmassage(msg);
                return;
            }

            int valid = 0;
            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
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


            if (valid == 0)
            {
                msg = "you have no right to setup course of study for this faculty";
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

            if (DepartmentID.ContainsKey(DDListDept.Text.Trim()))
            {
                DeptID = int.Parse(DepartmentID[DDListDept.Text.Trim()].ToString());
            }

            if (FacultyID.ContainsKey(DDListFaculty.Text.Trim()))
            {
                FacultId = int.Parse(FacultyID[DDListFaculty.Text.Trim()].ToString());
            }

            if (DDListLevel.Text != "")
            {
                DDListLevel.Text = DDListLevel.Text.ToUpper();
            }
            else
            {
                msg = "Select Mode oF Study";
                showmassage(msg);
                DDListLevel.Focus();
                return;
            }

            if (DDListSemester.Text != "")
            {
                DDListSemester.Text = DDListSemester.Text.ToUpper();
            }
            else
            {
                msg = "Select Programme";
                showmassage(msg);
                DDListSemester.Focus();
                return;
            }
            int Passmark = 0;

            if (int.TryParse(TxtPassmark.Text.Trim(), out Passmark))
            {

            }
            else
            {
                //msg = "Enter a valid pass mark for this course";
                //showmassage(msg);
                //TxtPassmark.Focus();
                //return;
            }

            if (TxtCourseCode.Text.Trim() != "" && TxtCourseCode.Text.Trim().ToLower() != "none")
            {

            }
            else
            {
                msg = "Enter course code";
                showmassage(msg);
                TxtCourseCode.Focus();
                return;
            }

            if (TxtCourseName.Text.Trim() != "")
            {
                TxtCourseName.Text = TxtCourseName.Text.Replace("&", "AND").Trim();
            }
            else
            {
                msg = "Enter course title";
                showmassage(msg);
                TxtCourseName.Focus();
                return;
            }

            string coscodename = TxtCourseCode.Text.Trim() + "=" + TxtCourseName.Text.Trim();


            // uniqItm = TxtCourseOfStudy.Text.Trim() + "|" + DDListFaculty.Text.Trim() + "|" + DDListDept.Text.Trim() + "|" + DDListDuration.Text.Trim() + "|" + DDListHonour.Text.Trim() + "|" + DDListProgramme.Text.Trim() + "|" + DDListModeStudy.Text.Trim();
            //DDListDept



            string Qry = "SELECT * FROM [Courses] where [CourseCode] = '" + TxtCourseCode.Text.Trim() + "'";

            if (Existed(Qry))
            {
                msg = "Course code existed before , try to edit";
                showmassage(msg);
                //TxtFacPrefix.Focus();
                return;
            }
            else
            {


                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                Qry = "INSERT INTO [Courses] ([FacultyID],[FacultyName],[DepartmentID],[DepartmentName],[AcademicLevel],[Semester],[CourseTitle],[CourseCode],[CreatedDate],[CreatedBy],[CourseCodeName],PassMark) VALUES (" + FacultId + ",'" + DDListFaculty.Text + "'," + DeptID + ",'" + DDListDept.Text + "','" + DDListLevel.Text + "','" + DDListSemester.Text + "','" + TxtCourseName.Text + "','" + TxtCourseCode.Text + "','" + CreatedDate + "','" + ID + "','" + coscodename + "'," + Passmark + ")";
                PerformInsert(Qry);
                populategrd();


                auditInfo = new AuditInfo();
                string msg1 = Qry;
                auditInfo.Action = "Department Course setup, Direct input";
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;
                auditInfo.Msg = msg1;
                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;
                sendtoAuditQ(auditInfo);


                TxtPassmark.Text = "0";
                TxtCourseName.Text = "";
                TxtCourseCode.Text = "";
                

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
    protected void BtnUpload2_Click(object sender, EventArgs e)
    {
        // LoadPix();
    }
}
