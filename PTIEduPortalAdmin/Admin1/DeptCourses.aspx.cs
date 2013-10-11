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


public partial class Admin_DeptCourses : System.Web.UI.Page
{
    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "ResetScrollPosition();", true);


    string str = System.Configuration.ConfigurationManager.AppSettings["conn"];
    string str2 = System.Configuration.ConfigurationManager.AppSettings["conn2"];

    private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string PinInfoUpdate = ".\\private$\\" + ConfigurationManager.AppSettings["PinInfoUpdate"];

    private  string msg = "";

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


    //private static string PresentDept = "";
    //private static ArrayList AllFaculty = null;
    //private static ArrayList AllDept = null;
    //private static Hashtable FacultyID = null;
    //private static Hashtable DeptID = null;


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

        DDListSemester.Visible=false;
        DDListSemester2.Visible = false;

       // GridView1.Width = 1000;
        //ListBoxCostudy.Width = 3000;
        //ListBoxCostudy.Height = 300;
        ListBoxCourses.Width = 1000;
        ChkBoxListAvailCourses.Width = 1000;
        AllFaculty = null;
        //Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), Page.ClientID, "restoreScroll();", true);
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
            //populategrd();//

            //
            ChkBoxListAvailCourses.Items.Clear();
            ListBoxCourses.Items.Clear();
            TxtCreditLoad.Text = "";
            AllFaculty = null;

            LoadFaculty();

            LoadProgm();
            loadModeStudy();
            LoadLevel2();

            LoadSemester2();
            LoadSession();
            LoadCourseType();
            LoadFaculty2();
            TxtCreditLoad.Focus();
            getuserinfo(ID);
            populategrd();



        }
        this.BtnUpload2.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.LinkButton2, ""));

        GridView1.Width = GViewWidth;
       

    }
    
    //public void SetFocus(Page sPage)
    //{
    //    string[] sCtrl = null;
    //    string sCtrlId = null;
    //    Control sCtrlFound = default(Control);
    //    string sCtrlClientId = null;
    //    string sScript = null;

    //    sCtrl = sPage.Request.Form.GetValues("__EVENTTARGET");
    //    if ((sCtrl != null))
    //    {
    //        sCtrlId = sCtrl[0];
    //        sCtrlFound = sPage.FindControl(sCtrlId);
    //        if ((sCtrlFound != null))
    //        {
    //            sCtrlClientId = sCtrlFound.ClientID;
    //            sScript = "<SCRIPT language='javascript'>document.getElementById('" + sCtrlClientId + "').focus(); if (document.getElementById('" + sCtrlClientId + "').scrollIntoView(false)) {document.getElementById('" + sCtrlClientId + "').scrollIntoView(true)} </SCRIPT>";
    //            sPage.ClientScript.RegisterStartupScript(typeof(string), "controlFocus", sScript);
    //        }
    //    }
    //}

    private void LoadCourseType()
    {
        DDListCourseType.Items.Clear();

        try
        {

            DataSet ds = new DataSet();
            string qry = "SELECT [CourseType]  FROM [CourseTypes] order by [CourseType] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListCourseType.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
    private void LoadSession()
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

    private void loadModeStudy()
    {
        try
        {
            DDListStudyMode.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [ModeOfStudy] FROM [ModeOfStudy] order by [ModeOfStudy] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListStudyMode.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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

    private void LoadProgm()
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
    private void LoadSemester2()
    {
        try
        {
            DDListSemester2.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [Semester] FROM [Semesters] order by [Semester] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListSemester2.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
    private void LoadLevel2()
    {
        try
        {
            DDListLevel2.Items.Clear();

            DataSet ds = new DataSet();
            string qry = "SELECT [AcademicLevel] FROM [Levels] order by [AcademicLevel] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListLevel2.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
                    LoadLevels();
                    LoadSemester();
                    //populategrd();//

                    //
                    ChkBoxListAvailCourses.Items.Clear();
                    ListBoxCourses.Items.Clear();
                    TxtCreditLoad.Text = "";
                    AllFaculty = null;

                    LoadFaculty();
                    LoadProgm();
                    loadModeStudy();
                    LoadLevel2();
                    LoadSemester2();
                    LoadSession();
                    LoadCourseType();
                    LoadFaculty2();
                    TxtCreditLoad.Focus();
                    getuserinfo(ID);
                    populategrd();


                }
                else
                {

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
            //DepartmentID = new Hashtable();



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
                        //DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                    }

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
    private void LoadFaculty2()
    {
        try
        {


            DDListFac2.Items.Clear();

            FacultyID = new Hashtable();
            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],[FacultyID] FROM [Faculty] order by [FacultyName] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListFac2.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    FacultyID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                }
                loadDept2(DDListFac2.Text.Trim());
            }
            else
            {
                DDListDept2.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            // LogError(msg, "School Portal", "");
            showmassage(msg);
        }
    }

    private void loadDept2(string Fact)
    {
        try
        {

            DDListDept2.Items.Clear();
            DepartmentID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT distinct [DepartmentName],[DepartmentId] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListDept2.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                }
                LoadCourseOfStudy(DDListDept2.Text.Trim(), Fact);
            }
            else
            {
                DDListDept2.Items.Clear();
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
        }
    }

    private void LoadCourseOfStudy(string Dept, string fact)
    {

        try
        {
            DDListCourseOfStudy.Items.Clear();


            CourseOfStudyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [CourseOfStudyName],[CourseOfStudyID]  FROM [CourseOfStudy] where [DepartmentName]= '" + Dept + "' and [FacultyName] = '" + fact + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy] = '" + DDListStudyMode.Text.Trim() + "' order by [CourseOfStudyName] asc";

            ds = SearchData(qry);
            string name = "";
            string Cosid = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                DDListCourseOfStudy.Items.Add("All");
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
                TxtCreditLoad.Text = "";
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
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID);

            int valid = 0;

            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                // query = "SELECT Srn,[CourseTitle] as [Course Title],[CourseCode] as [Course Code],[PassMark] as [Pass Mark], [FacultyName] as [Faculty],[DepartmentName] as [Department],[AcademicLevel] as [Academic Level],[Semester] FROM [Courses] where [FacultyName] ='" + DDListFaculty.Text.Trim() + "' and DepartmentName='" + DDListDept.Text.Trim() + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]='" + DDListSemester.Text.Trim() + "'";
                query = "SELECT [Srn],[CourseCode] as [Course Code],[CourseTitle] as [Course Title],[CourseOfStudy] as [Programme of Study],[AcademicLevel] as [Level],[Semester],[SessionName] as [Session],[CourseType] as [Course Type],[CreditLoad] as [Credit Load],[Programme],[ModeOfStudy] as [Study Mode] FROM [DeptCourses] where FacultyName = '" + DDListFac2.Text.Trim() + "' and DepartmentName = '" + DDListDept2.Text.Trim() + "' and Programme  = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy]  = '" + DDListStudyMode.Text.Trim() + "' and [SessionName]  = '" + DDListSession.Text.Trim() + "' and [Semester]  = '" + DDListSemester2.Text.Trim() + "' and [AcademicLevel]  = '" + DDListLevel2.Text.Trim() + "'";
                valid++;
            }

            if (Group.ToUpper() == "FACULTY ADMIN")
            {
                query = "SELECT [Srn],[CourseCode] as [Course Code],[CourseTitle] as [Course Title],[CourseOfStudy] as [Programme of Study],[AcademicLevel] as [Level],[Semester],[SessionName] as [Session],[CourseType] as [Course Type],[CreditLoad] as [Credit Load],[Programme],[ModeOfStudy] as [Study Mode] FROM [DeptCourses] where FacultyName = '" + userFact + "' and Programme  = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy]  = '" + DDListStudyMode.Text.Trim() + "' and [SessionName]  = '" + DDListSession.Text.Trim() + "' and [Semester]  = '" + DDListSemester2.Text.Trim() + "' and [AcademicLevel]  = '" + DDListLevel2.Text.Trim() + "'";
                valid++;
            }
            if (Group.ToUpper() == "DEPARTMENT ADMIN")
            {
                query = "SELECT [Srn],[CourseCode] as [Course Code],[CourseTitle] as [Course Title],[CourseOfStudy] as [Programme of Study],[AcademicLevel] as [Level],[Semester],[SessionName] as [Session],[CourseType] as [Course Type],[CreditLoad] as [Credit Load],[Programme],[ModeOfStudy] as [Study Mode] FROM [DeptCourses] where FacultyName = '" + userFact + "' and CourseOfStudy = '" + usercosstudy + "' and Programme  = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy]  = '" + DDListStudyMode.Text.Trim() + "' and [SessionName]  = '" + DDListSession.Text.Trim() + "' and [Semester]  = '" + DDListSemester2.Text.Trim() + "' and [AcademicLevel]  = '" + DDListLevel2.Text.Trim() + "'";
                valid++;

            }
            //////
            if (valid > 0)
            {
                exportheader = "[Srn],[Course Code],[Course Title],[Programme of Study],[Level],[Semester],[Session],[Course Type],[Credit Load],[Programme],[Mode Of Study]";

                Exportfilename = "DepartmentCourses";
                GridCaption = "DepartmentCourses";
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
            
            GridView1.DataBind();
            
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



            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = true;
            row.Cells[6].Enabled = true;
            row.Cells[7].Enabled = true;
            row.Cells[8].Enabled = true;
            row.Cells[9].Enabled = true;
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

            string Level = ((DropDownList)row.FindControl("DropDownList1")).SelectedItem.Value;
            string Semester = DDListSemester2.Text;// ((DropDownList)row.FindControl("DropDownList2")).SelectedItem.Value;
            string CourseType = ((DropDownList)row.FindControl("DropDownList4")).SelectedItem.Value;
            string StudyMode = ((DropDownList)row.FindControl("DropDownList3")).SelectedItem.Value;


            GridView1.Rows[e.RowIndex].Cells[9].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[9].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[10].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[10].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[11].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[11].Controls[0])).Text.Trim();

            int crd = 0;


            //string Symbol = row.Cells[1].Text.Trim();
            //string Desc = row.Cells[2].Text.Trim();
            //string Code = row.Cells[3].Text.Trim();

            int srn = int.Parse(row.Cells[1].Text.Trim());
            string qry = "";

            int Passmark = 0;

            if (int.TryParse(row.Cells[9].Text.Trim(), out crd))
            {

            }
            else
            {
                msg = "Enter a credit load";
                showmassage(msg);
                //TxtPassmark.Focus();
                return;
            }

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string CreatedBy = ID;// rq.Logonpermit.Userid;
            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            GridView1.EditIndex = -1;
            //[Id],[Course Of Study],[Faculty],[Department],[Duration Of Study],[Programme],[Mode Of Study],[Honours],[Min Credit Earned],[Min Credit Registered],[Min CGPA]";
            //[AcademicLevel],[Semester],[CourseType],[CreditLoad],[CourseTitle],[ModeOfStudy]
            //update
            qry = "UPDATE [DeptCourses] SET AcademicLevel = '" + Level + "', Semester='" + Semester + "', CourseType='" + CourseType + "', CreditLoad=" + crd + ", ModeOfStudy='" + StudyMode + "' where [Srn] = " + srn + "";
            PerformUpdate(qry);
            populategrd();

            auditInfo = new AuditInfo();
            
            auditInfo.Action = "Registrable Course Update";
            auditInfo.Usergroup = Group;
            auditInfo.Userid = ID;

            auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            auditInfo.Computer = User.Identity.Name;
            auditInfo.Hostname = Request.UserHostName;
            auditInfo.IPAddress = Request.UserHostAddress;
            auditInfo.Msg = qry;
            sendtoAuditQ(auditInfo);


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
            qry = "SELECT * FROM [CourseRegistration] where [CourseCode] ='" + obj2 + "'";

            if (!Existed(qry))
            {

                qry = "Delete from [DeptCourses] where [Srn] = " + srn + "";

                PerformDelete(qry);
                populategrd();
            }
            else
            {
                msg = "Course code used already, try to remove it from registrable courses";
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
                Loadcourses();
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

    protected void DisplayC_Click(object sender, EventArgs e)
    {
        Loadcourses();

    }
    protected void DDListFac2_Changed(object sender, EventArgs e)
    {
        loadDept2(DDListFac2.Text.Trim());

    }
    //DDListFact3_Changed
    protected void DDListFact3_Changed(object sender, EventArgs e)
    {
        // loadDept3(DDListFact3.Text.Trim());

    }
    protected void DDListDept2_Changed(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept2.Text.Trim(), DDListFac2.Text.Trim());


    }
    //DDListDept3_Changed
    protected void DDListDept3_Changed(object sender, EventArgs e)
    {
        //LoadCourseOfStudy2(DDListDept3.Text.Trim());
    }
    private void Loadcourses()
    {
        if (DDListFaculty.Text.Trim() != "" && DDListDept.Text.Trim() != "" && DDListLevel.Text.Trim() != "" && DDListSemester.Text.Trim() != "")
        {

            ChkBoxListAvailCourses.Items.Clear();
            string query = "";

            query = "SELECT [CourseCodeName]  FROM [Courses] where [FacultyName]='" + DDListFaculty.Text.Trim() + "' and [DepartmentName]='" + DDListDept.Text.Trim() + "' and [AcademicLevel]='" + DDListLevel.Text.Trim() + "' and [Semester]= '" + DDListSemester.Text.Trim() + "'";

            DataSet ds = new DataSet();
            ds = SearchData(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    ChkBoxListAvailCourses.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
                }
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ChkBoxListAvailCourses.Items.Count > 0)
        {
            for (int k = 0; k < ChkBoxListAvailCourses.Items.Count; k++)
            {
                ChkBoxListAvailCourses.Items[k].Selected = true;
            }
        }
    }
    protected void BtnDisselect_Click(object sender, EventArgs e)
    {
        ChkBoxListAvailCourses.SelectedIndex = -1;
        //if (ChkBoxListAvailCourses.Items.Count > 0)
        //{
        //    for (int k = 0; k < ChkBoxListAvailCourses.Items.Count; k++)
        //    {
        //        ChkBoxListAvailCourses.Items[k].Selected = false;
        //    }
        //}
    }
    protected void BtnRemoveAll_Click(object sender, EventArgs e)
    {
        ListBoxCourses.Items.Clear();
    }
    protected void BtnRemovSingle_Click(object sender, EventArgs e)
    {
        try
        {

            if (ListBoxCourses.SelectedIndex >= 0)
            {
                int h = ListBoxCourses.SelectedIndex;
                if (h >= 0)
                {
                    ListBoxCourses.Items.RemoveAt(h);

                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
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
            if (FileUpload1.HasFile)
            {
                msg = "Please, click the file upload button";
                showmassage(msg);
                return;
            }

            int valid = 0;
            if (Group.ToUpper() != "FACULTY ADMIN" && Group.ToUpper() != "DEPARTMENT ADMIN")
            {
                valid++;
            }
            if (Group.ToUpper() == "FACULTY ADMIN" && DDListFac2.Text.Trim().ToLower() == userFact.ToLower())
            {
                valid++;
            }


            if (Group.ToUpper() == "DEPARTMENT ADMIN" && DDListFac2.Text.Trim().ToLower() == userFact.ToLower() && DDListDept2.Text.Trim().ToLower() == userDept.ToLower() && DDListCourseOfStudy.Text.Trim().ToLower() == usercosstudy.ToLower())
            {
                valid++;

            }

            if (valid == 0)
            {
                msg = "you have no right to setup course of study for this faculty";
                showmassage(msg);
                return;
            }
            if (DDListFac2.Text != "")
            {
                DDListFac2.Text = DDListFac2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Faculty Name";
                showmassage(msg);
                DDListFac2.Focus();
                //PanelCourse.Visible = true;
                return;
            }


            if (DDListDept2.Text != "")
            {
                DDListDept2.Text = DDListDept2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Departmental Name";
                showmassage(msg);
                DDListDept2.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            if (DDListCourseOfStudy.Text != "")
            {
                if (DDListCourseOfStudy.Text.ToUpper() == "ALL")
                {
                }
                else
                {

                    DDListCourseOfStudy.Text = DDListCourseOfStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
            }
            else
            {
                msg = "Select course of study";
                showmassage(msg);
                DDListCourseOfStudy.Focus();
                return;
            }

            if (DDListSemester2.Text != "")
            {
                DDListSemester2.Text = DDListSemester2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Semester";
                showmassage(msg);
                DDListSemester2.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            if (DDListLevel2.Text != "")
            {
                DDListLevel2.Text = DDListLevel2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Level";
                showmassage(msg);
                DDListLevel2.Focus();
                //PanelCourse.Visible = true;
                return;
            }


            if (DDListCourseType.Text != "")
            {
                DDListCourseType.Text = DDListCourseType.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Course Type";
                showmassage(msg);
                DDListCourseType.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            if (DDListSession.Text != "")
            {
                DDListSession.Text = DDListSession.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select Session";
                showmassage(msg);
                DDListSession.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            if (DDListProgramme.Text != "")
            {
                DDListProgramme.Text = DDListProgramme.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select school programme";
                showmassage(msg);
                DDListProgramme.Focus();
                return;
            }

            if (DDListStudyMode.Text != "")
            {
                DDListStudyMode.Text = DDListStudyMode.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select study mode";
                showmassage(msg);
                DDListStudyMode.Focus();
                return;
            }

            int Cdt = 0;
            if (TxtCreditLoad.Text != "")
            {
                if (int.TryParse(TxtCreditLoad.Text.Trim(), out Cdt))
                {
                }
                else
                {
                    msg = "Enter a numeric value for the credit load";
                    showmassage(msg);
                    TxtCreditLoad.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }

            }
            else
            {
                msg = "Enter a numeric value for the credit load";
                showmassage(msg);
                TxtCreditLoad.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            //if (DDListCourseGroup.Text.Trim() == "")
            //{
            //    string Cgroup = "NONE";
            //}


            int k = ListBoxCourses.Items.Count;
            string body = "";

            string UniqueItem = "";
            string ExistedCodes = "";

            //CSingleAttribute cs = null;
            //DeptCoursesBusiness Dcb = null;// new DeptCoursesBusiness();
            DeptCourses DeptCos = null;
            string Qry = "";
            int cnt = 0;
            if (k > 0)
            {
                auditInfo = new AuditInfo();
                string msg1 = Qry;
                auditInfo.Action = "Registrable Course setup, Direct input";
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;

                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;

                if (DDListCourseOfStudy.Text.Trim().ToUpper() == "ALL")
                {
                    foreach (DictionaryEntry CosOfstudy in CourseOfStudyID)
                    {
                        if (CosOfstudy.Key.ToString() != "ALL")
                        {
                            for (int j = 0; j < k; j++)
                            {
                                string cousecode = "";
                                string coursename = "";

                                body = ListBoxCourses.Items[j].Text.Trim();
                                string[] item2 = body.Split(new char[] { '=' });
                                int upp = item2.GetUpperBound(0);
                                if (upp > 0)
                                {
                                    cousecode = item2[0].ToString().Trim();
                                    coursename = item2[1].ToString().Trim();
                                }

                                // Dcb = new DeptCoursesBusiness();

                                UniqueItem = body + "//" + DDListDept2.Text.Trim() + "//" + CosOfstudy.Key.ToString() + "//" + DDListSession.Text.Trim() + "//" + DDListLevel2.Text.Trim() + "//" + DDListStudyMode.Text.Trim() + "//" + DDListProgramme.Text.Trim();// +"//" + DDListCourseGroup.Text.Trim();

                                string qry = "SELECT * from [DeptCourses] where [UniqueItem]='" + UniqueItem + "'";
                                if (Existed(qry))
                                {

                                    if (ExistedCodes == "")
                                    {
                                        ExistedCodes = ExistedCodes + body;
                                    }
                                    else
                                    {
                                        ExistedCodes = ExistedCodes + "," + body;
                                    }
                                }
                                else
                                {
                                    DeptCos = new DeptCourses();

                                    DeptCos.StaffNumber = "NONE";

                                    DeptCos.DepartmentName = DDListDept2.Text.Trim();
                                    if (DepartmentID.ContainsKey(DDListDept2.Text.Trim()))
                                    {
                                        DeptCos.DepartmentID = int.Parse(DepartmentID[DDListDept2.Text.Trim()].ToString());
                                    }
                                    DeptCos.FacultyName = DDListFac2.Text.Trim();
                                    if (FacultyID.ContainsKey(DDListFac2.Text.Trim()))
                                    {
                                        DeptCos.FacultyID = int.Parse(FacultyID[DDListFac2.Text.Trim()].ToString());
                                    }

                                    if (CourseOfStudyID.ContainsKey(CosOfstudy.Key.ToString()))
                                    {
                                        DeptCos.CourseOfStudyID = int.Parse(CourseOfStudyID[CosOfstudy.Key.ToString()].ToString());
                                        DeptCos.CourseOfStudy = CosOfstudy.Key.ToString();
                                    }

                                    DeptCos.CourseCodeName = body;
                                    DeptCos.Course = coursename;
                                    DeptCos.CourseCode = cousecode;
                                    DeptCos.CourseTitle = coursename;
                                    DeptCos.CourseType = DDListCourseType.Text.Trim();
                                    DeptCos.CourseGroup = "None";// DDListCourseGroup.Text.Trim();
                                    DeptCos.ModeOfStudy = DDListStudyMode.Text.Trim();
                                    DeptCos.Programme = DDListProgramme.Text.Trim();
                                    DeptCos.CreatedBy = Setupby;
                                    DeptCos.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                    DeptCos.CreditLoad = Cdt;
                                    DeptCos.Semester = DDListSemester2.Text.Trim();
                                    DeptCos.SessionName = DDListSession.Text.Trim();
                                    DeptCos.UniqueItem = UniqueItem;
                                    DeptCos.AcademicLevel = DDListLevel2.Text.Trim();
                                    DeptCos.AssignedLecturer = "NONE";

                                    Qry = "INSERT INTO [DeptCourses]([FacultyID],[DepartmentID],[AcademicLevel],[Semester],[SessionName],[CourseType],[CreditLoad],[AssignedLecturer],[CreatedBy],[CreatedDate],[CourseTitle],[CourseCode],[CourseOfStudyID],[UniqueItem],[CourseCodeName],[Course],[DepartmentName],[FacultyName],[CourseOfStudy],[StaffNumber],[ModeOfStudy],[Programme]) VALUES (" + DeptCos.FacultyID + "," + DeptCos.DepartmentID + ",'" + DeptCos.AcademicLevel + "','" + DeptCos.Semester + "','" + DeptCos.SessionName + "','" + DeptCos.CourseType + "'," + DeptCos.CreditLoad + ",'" + DeptCos.AssignedLecturer + "','" + DeptCos.CreatedBy + "','" + DeptCos.CreatedDate + "','" + DeptCos.CourseTitle + "','" + DeptCos.CourseCode + "'," + DeptCos.CourseOfStudyID + ",'" + DeptCos.UniqueItem + "','" + DeptCos.CourseCodeName + "','" + DeptCos.Course + "','" + DeptCos.DepartmentName + "','" + DeptCos.FacultyName + "','" + DeptCos.CourseOfStudy + "','" + DeptCos.StaffNumber + "','" + DeptCos.ModeOfStudy + "','" + DeptCos.Programme + "')";
                                    PerformInsert(Qry);
                                    //Dcb.InsertDepartmentCourses(DeptCos);
                                    cnt++;
                                    TxtCreditLoad.Text = "";

                                    
                                    auditInfo.Msg = Qry;
                                    sendtoAuditQ(auditInfo);

                                }
                            }
                        }

                    }
                }
                else
                {

                    for (int j = 0; j < k; j++)
                    {
                        string cousecode = "";
                        string coursename = "";

                        body = ListBoxCourses.Items[j].Text.Trim();
                        string[] item2 = body.Split(new char[] { '=' });
                        int upp = item2.GetUpperBound(0);
                        if (upp > 0)
                        {
                            cousecode = item2[0].ToString().Trim();
                            coursename = item2[1].ToString().Trim();
                        }

                        // Dcb = new DeptCoursesBusiness();

                        UniqueItem = body + "//" + DDListDept2.Text.Trim() + "//" + DDListCourseOfStudy.Text.Trim() + "//" + DDListSession.Text.Trim() + "//" + DDListLevel2.Text.Trim() + "//" + DDListStudyMode.Text.Trim() + "//" + DDListProgramme.Text.Trim();// +"//" + DDListCourseGroup.Text.Trim();

                        string qry = "SELECT * from [DeptCourses] where [UniqueItem]='" + UniqueItem + "'";

                        if (Existed(qry))
                        {

                            if (ExistedCodes == "")
                            {
                                ExistedCodes = ExistedCodes + body;
                            }
                            else
                            {
                                ExistedCodes = ExistedCodes + "," + body;
                            }
                        }
                        else
                        {
                            DeptCos = new DeptCourses();

                            DeptCos.StaffNumber = "NONE";

                            DeptCos.DepartmentName = DDListDept2.Text.Trim();
                            if (DepartmentID.ContainsKey(DDListDept2.Text.Trim()))
                            {
                                DeptCos.DepartmentID = int.Parse(DepartmentID[DDListDept2.Text.Trim()].ToString());
                            }
                            DeptCos.FacultyName = DDListFac2.Text.Trim();
                            if (FacultyID.ContainsKey(DDListFac2.Text.Trim()))
                            {
                                DeptCos.FacultyID = int.Parse(FacultyID[DDListFac2.Text.Trim()].ToString());
                            }

                            if (CourseOfStudyID.ContainsKey(DDListCourseOfStudy.Text.Trim()))
                            {

                                DeptCos.CourseOfStudyID = int.Parse(CourseOfStudyID[DDListCourseOfStudy.Text.Trim()].ToString());
                                DeptCos.CourseOfStudy = DDListCourseOfStudy.Text.Trim();
                            }

                            DeptCos.CourseCodeName = body;
                            DeptCos.Course = coursename;
                            DeptCos.CourseCode = cousecode;
                            DeptCos.CourseTitle = coursename;
                            DeptCos.CourseType = DDListCourseType.Text.Trim();
                            DeptCos.CourseGroup = "None";// DDListCourseGroup.Text.Trim();
                            DeptCos.ModeOfStudy = DDListStudyMode.Text.Trim();
                            DeptCos.Programme = DDListProgramme.Text.Trim();
                            DeptCos.CreatedBy = Setupby;
                            DeptCos.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            DeptCos.CreditLoad = Cdt;
                            DeptCos.Semester = DDListSemester2.Text.Trim();
                            DeptCos.SessionName = DDListSession.Text.Trim();
                            DeptCos.UniqueItem = UniqueItem;
                            DeptCos.AcademicLevel = DDListLevel2.Text.Trim();
                            DeptCos.AssignedLecturer = "NONE";

                            Qry = "INSERT INTO [DeptCourses]([FacultyID],[DepartmentID],[AcademicLevel],[Semester],[SessionName],[CourseType],[CreditLoad],[AssignedLecturer],[CreatedBy],[CreatedDate],[CourseTitle],[CourseCode],[CourseOfStudyID],[UniqueItem],[CourseCodeName],[DepartmentName],[FacultyName],[CourseOfStudy],[StaffNumber],[ModeOfStudy],[Programme]) VALUES (" + DeptCos.FacultyID + "," + DeptCos.DepartmentID + ",'" + DeptCos.AcademicLevel + "','" + DeptCos.Semester + "','" + DeptCos.SessionName + "','" + DeptCos.CourseType + "'," + DeptCos.CreditLoad + ",'" + DeptCos.AssignedLecturer + "','" + DeptCos.CreatedBy + "','" + DeptCos.CreatedDate + "','" + DeptCos.CourseTitle + "','" + DeptCos.CourseCode + "'," + DeptCos.CourseOfStudyID + ",'" + DeptCos.UniqueItem + "','" + DeptCos.CourseCodeName + "','" + DeptCos.DepartmentName + "','" + DeptCos.FacultyName + "','" + DeptCos.CourseOfStudy + "','" + DeptCos.StaffNumber + "','" + DeptCos.ModeOfStudy + "','" + DeptCos.Programme + "')";
                            PerformInsert(Qry);
                            //Dcb.InsertDepartmentCourses(DeptCos);
                            cnt++;
                            TxtCreditLoad.Text = "";

                            auditInfo.Msg = Qry;
                            sendtoAuditQ(auditInfo);

                        }
                    }
                }

            }

            ListBoxCourses.Items.Clear();
            //log.Info(cnt.ToString() + " " + " Departmental courses created by:" + " " + Setupby + " " + " for" + " " + DDListDept2.Text);

            if (ExistedCodes == "")
            {
                msg = cnt.ToString() + " " + "Departmental courses successfully created for " + " " + DDListDept2.Text;
            }
            else
            {
                msg = cnt.ToString() + " " + "Departmental courses successfully created for " + " " + DDListDept2.Text + " " + " but," + "the following courses existed before for this department and session, hence could not be recreated" + ":" + ExistedCodes;
            }

            showmassage(msg);
            ListBoxCourses.Items.Clear();
            TxtCreditLoad.Text = "0";
            TxtCreditLoad.Focus();

            populategrd();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
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
        if (Group.ToUpper() == "FACULTY ADMIN" && DDListFac2.Text.Trim().ToLower() == userFact.ToLower())
        {
            valid++;
        }


        if (Group.ToUpper() == "DEPARTMENT ADMIN" && DDListFac2.Text.Trim().ToLower() == userFact.ToLower() && DDListDept2.Text.Trim().ToLower() == userDept.ToLower() && DDListCourseOfStudy.Text.Trim().ToLower() == usercosstudy.ToLower())
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
        string courseOfStudy = "";
        string studymode = "";
        string session = "";
        string Programme = "";

        ext = Path.GetExtension(fname);

        try
        {
            if (fname != "")
            {
                string Optype = "";
                String mess = ID;


                ////if (DepartmentID.ContainsKey(DDListDept.Text.Trim()))
                ////{
                ////    DeptID = int.Parse(DepartmentID[DDListDept.Text.Trim()].ToString());
                ////}

                ////if (FacultyID.ContainsKey(DDListFaculty.Text.Trim()))
                ////{
                ////    FacultId = int.Parse(FacultyID[DDListFaculty.Text.Trim()].ToString());
                ////}



                //
                if (DDListFac2.Text != "")
                {
                    Faculty = DDListFac2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Faculty Name";
                    showmassage(msg);
                    DDListFac2.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }


                if (DDListDept2.Text != "")
                {
                    Dept = DDListDept2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Departmental Name";
                    showmassage(msg);
                    DDListDept2.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }

                if (DDListCourseOfStudy.Text != "")
                {

                    courseOfStudy = DDListCourseOfStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");

                }
                else
                {
                    msg = "Select course of study";
                    showmassage(msg);

                    return;
                }

                if (DDListSemester2.Text != "")
                {
                    semester = DDListSemester2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Semester";
                    showmassage(msg);
                    DDListSemester2.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }

                if (DDListLevel2.Text != "")
                {
                    Level = DDListLevel2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Level";
                    showmassage(msg);
                    DDListLevel2.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }


                if (DDListSession.Text != "")
                {
                    session = DDListSession.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select Session";
                    showmassage(msg);
                    DDListSession.Focus();
                    //PanelCourse.Visible = true;
                    return;
                }

                if (DDListProgramme.Text != "")
                {
                    Programme = DDListProgramme.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select school programme";
                    showmassage(msg);
                    DDListProgramme.Focus();
                    return;
                }

                if (DDListStudyMode.Text != "")
                {
                    studymode = DDListStudyMode.Text.ToUpper().Replace("'", "''").Replace(";", ":");
                }
                else
                {
                    msg = "Select study mode";
                    showmassage(msg);
                    return;
                }
                //
                int CourseStudyID = 0;
                if (DepartmentID.ContainsKey(DDListDept2.Text.Trim()))
                {
                    DeptID = int.Parse(DepartmentID[DDListDept2.Text.Trim()].ToString());
                }

                if (FacultyID.ContainsKey(DDListFac2.Text.Trim()))
                {
                    FacultId = int.Parse(FacultyID[DDListFac2.Text.Trim()].ToString());
                }

                if (CourseOfStudyID.ContainsKey(DDListCourseOfStudy.Text.Trim()))
                {

                    CourseStudyID = int.Parse(CourseOfStudyID[DDListCourseOfStudy.Text.Trim()].ToString());

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

                        TreatFileAdmitedXls(fname, filepath, Faculty, Dept, FacultId, DeptID, courseOfStudy, CourseStudyID, semester, Level, Programme, session, studymode);
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

    private void TreatFileAdmitedXls(string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string courseOfStudy, int CourseStudyID, string semester, string Level, string Programme, string session, string studymode)
    {
        //fname, filepath, Faculty, Dept, FacultId, DeptID, courseOfStudy, CourseStudyID, semester, Level, Programme, session, studymode

        ID = HttpContext.Current.User.Identity.Name;
        Group = (string)Cache[HttpContext.Current.User.Identity.Name];

        //userFact = getuserFact(ID);
        //userDept = getuserDept(ID); ;
        //usercosstudy = getuserCostudy(ID);
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


                OleDbCommand oCmd1 = null;
                oCmd1 = new OleDbCommand("select * from [" + Currsheet + "]", objConn);
                OleDbDataReader oDr1 = oCmd1.ExecuteReader();


                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                OleDbDataAdapter dat = null;
                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);
                dt = ds.Tables[0];

                //DataTable item = ds.Tables[0];
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


                    if (valicolcnt >= 4)
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
                int crd = 0;
                string status = "";
                string coursestatus = "";
                string UniqueItem = "";


                auditInfo = new AuditInfo();
                //string msg1 = Qry;
                auditInfo.Action = "Registrable Course setup, uploaded";
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
                        coscode = oDr1.GetValue(0).ToString().ToUpper();
                    }

                    if (oDr1.IsDBNull(1))
                    {
                        course = "None";
                    }
                    else
                    {
                        course = oDr1.GetValue(1).ToString().Replace("&", "AND").Trim().ToUpper();
                    }

                    if (oDr1.IsDBNull(2))
                    {

                    }
                    else
                    {

                        if (int.TryParse(oDr1.GetValue(2).ToString(), out crd))
                        {
                        }
                        else
                        {
                            //msg = "Enter a numeric value for the credit load";

                        }
                    }


                    if (oDr1.IsDBNull(3))
                    {

                    }
                    else
                    {
                        status = oDr1.GetValue(3).ToString();//.Replace("&", "AND").Trim();
                    }

                    if (status.ToLower().StartsWith("c"))
                    {
                        coursestatus = "COMPULSORY";
                    }
                    if (status.ToLower().StartsWith("e"))
                    {
                        coursestatus = "ELECTIVE";
                    }
                    if (status.ToLower().StartsWith("r"))
                    {
                        coursestatus = "REQUIRED";
                    }

                    if (coscode != "" && course != "" && crd > 0 && coursestatus != "")
                    {
                        string qry = "";
                        string coscodename = coscode + "=" + course;
                        qry = "SELECT * from [Courses] where [CourseCode]='" + coscode.Trim() + "'";

                        if (Existed(qry))
                        {
                            UniqueItem = coscodename + "//" + Dept + "//" + courseOfStudy + "//" + session + "//" + Level + "//" + studymode + "//" + Programme;

                            qry = "SELECT * from [DeptCourses] where [UniqueItem]='" + UniqueItem + "'";


                            if (!Existed(qry) && coscode.ToLower() != "none")
                            {
                                string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                                qry = "INSERT INTO [DeptCourses]([FacultyID],[DepartmentID],[AcademicLevel],[Semester],[SessionName],[CourseType],[CreditLoad],[AssignedLecturer],[CreatedBy],[CreatedDate],[CourseTitle],[CourseCode],[CourseOfStudyID],[UniqueItem],[CourseCodeName],[DepartmentName],[FacultyName],[CourseOfStudy],[StaffNumber],[ModeOfStudy],[Programme]) VALUES (" + FacultId + "," + DeptID + ",'" + Level + "','" + semester + "','" + session + "','" + coursestatus + "'," + crd + ",'NONE','" + ID + "','" + CreatedDate + "','" + course + "','" + coscode + "'," + CourseStudyID + ",'" + UniqueItem + "','" + coscodename + "','" + Dept + "','" + Faculty + "','" + courseOfStudy + "','NONE','" + studymode + "','" + Programme + "')";
                                PerformInsert(qry);
                                cnt++;

                                auditInfo.Msg = qry;
                                sendtoAuditQ(auditInfo);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }




                    }


                    //}
                    rc++;
                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
                populategrd();

                msg = "Total number of uploaded data is: " + " " + cnt.ToString() + " , out of total records of  " + dt.Rows.Count.ToString() + " ,see if course codes existed in departmental courses table";
                showmassage(msg);

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }
    }


    protected void BtnClose_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        GridView1.DataSource = null;
        GridView1.DataBind();
        TabContainer1.ActiveTabIndex = -1;
    }
    protected void AddLecturerToList_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList arr = new ArrayList();
            ArrayList SelectCourses = new ArrayList();

            if (ChkBoxListAvailCourses.Items.Count > 0)
            {
                for (int kk = 0; kk < ChkBoxListAvailCourses.Items.Count; kk++)
                {
                    if (ChkBoxListAvailCourses.Items[kk].Selected == true)
                    {
                        SelectCourses.Add(ChkBoxListAvailCourses.Items[kk].Text);
                    }
                }
            }

            //

            int k = ListBoxCourses.Items.Count;
            if (k > 0)
            {
                for (int i = 0; i < k; i++)
                {
                    arr.Add(ListBoxCourses.Items[i].Text);
                }
                //

                string cousecode = "";

                foreach (string courses in SelectCourses)
                {
                    int confr = 0;

                    foreach (string names in arr)
                    {
                        if (names.ToUpper().Trim() == courses.ToUpper().Trim())
                        {
                            confr++;
                            break;
                        }

                    }

                    if (confr == 0)
                    {
                        arr.Add(courses);
                    }
                }

                ListBoxCourses.Items.Clear();
                foreach (string s in arr)
                {
                    ListBoxCourses.Items.Add(s);
                }

            }
            else
            {
                //arr.Add();   
                foreach (string courses in SelectCourses)
                {
                    ListBoxCourses.Items.Add(courses);
                }

            }

            PaymentTypePanel1.Visible = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
        }
    }
    protected void BtnUpload2_Click(object sender, EventArgs e)
    {

    }
    
}
