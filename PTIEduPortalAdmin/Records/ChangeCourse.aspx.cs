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
using System.Text;
using AuditLogInfo;


public partial class Admin_ChangeCourse : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    //private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string msg = "";
    private static Hashtable EmployeeEvent = null;

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";

    private static int GViewWidth = 0;
    private static string PresentDept = "";
    private static ArrayList AllFaculty = null;
    private static ArrayList AllDept = null;
    private static Hashtable FacultyID = null;
    private static Hashtable DepartmentID = null;
    private static Hashtable CentreCode = new Hashtable();
    private static Hashtable CourseOfStudyID = null;

    private static int selectedStatus = 0;
    private static int SelectedPayrollStatus = 0;

    private string ID = "";// cp = null;
    private string Group = "";
    private string userDept = "";
    private string userFact = "";
    private string usercosstudy = "";


    private AuditLogInfo.AuditInfo auditInfo = null;
    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogPTI"];


    //WriteCrude rs = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.Width = 1100;
        ChkBoxListStaff.Width = 1000;
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

            GridView1.DataSource = null;
            GridView1.DataBind();
            PanelRations.Visible = false;

            LoadProgm();
            loadModeStudy();
            LoadLevel2();
            LoadSession();
            LoadFaculty2();
            LoadEntryMode();
            //populategrd();


        }
    }

    private void populategrd()
    {
        try
        {

            //query = "SELECT [CourseOfStudyName] as [Course Of Study],[Duration] as [Duration Of Study],[DepartmentName] as [Department],[FacultyName] as [Faculty],[Honours] as [Honor]  FROM [CourseOfStudy]";

            string qry = "";
            //if (Group.ToUpper() == "WEB MASTER")
            //{
            qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [Faculty],[FacultyNameOld] as [Old Faculty],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Programme Of Study],[CourseOfStudyNameOld] as [Old Programme Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status],EntryModeOld as [EntryMode Old],EntryMode as [EntryMode] FROM [ChangeOfCourse] where [ApproveStatus] = 0 or DisApprovedStatus = 1 order by CreatedDate desc";
            //}


            exportheader = "[MatNo],[Surname],[Other Names],[Session],[New Faculty],[Old Faculty],[New Department],[Old Department],[New Level] ,[Old Level],[New Programme Of Study],[Old Programme Of Study],[New Programme],[Old Programme],[New Duration],[Old Duration],[New Honours],[Old Honours],[New Mode Of Study],[Old Mode Of Study],[DisApproved],[EntryMode Old],[EntryMode]";

            Exportfilename = "Change of Courses ";
            GridCaption = "Change of Courses";
            GViewWidth = 1000;
            populategr(qry);
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            // LogError(msg, "Payroll", "");
        }
    }
    private void populategr(string query1)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

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
            // populategrddat.datasou
            GridView1.DataBind();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    formatGridview();
            //}
            GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
            GridView1.ToolTip = ds.Tables[0].Rows.Count.ToString();
            GridView1.CaptionAlign = TableCaptionAlign.Left;
            GridView1.Visible = true;
            //PanelGrid.Visible = true;
            //ChequePanelGridv.Visible = true;
        }
        catch (Exception ex)
        {

            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }

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
            TxtHons.Text = "";
            TxtDuration2.Text = "";


            CourseOfStudyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [CourseOfStudyName],[CourseOfStudyID],[Honours],[Duration]  FROM [CourseOfStudy] where [DepartmentName]= '" + Dept + "' and [FacultyName] = '" + fact + "' and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy] = '" + DDListStudyMode.Text.Trim() + "' order by [CourseOfStudyName] asc";

            ds = SearchData(qry);
            string name = "";
            string Cosid = "";

            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    name = ds.Tables[0].Rows[jj][0].ToString();
                    Cosid = ds.Tables[0].Rows[jj][1].ToString();
                    TxtHons.Text = ds.Tables[0].Rows[jj][2].ToString();
                    TxtDuration2.Text = ds.Tables[0].Rows[jj][3].ToString();

                    DDListCourseOfStudy.Items.Add(name);
                    if (!CourseOfStudyID.ContainsKey(name.Trim()))
                    {
                        CourseOfStudyID.Add(name.Trim(), int.Parse(Cosid));
                    }
                }

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
                DataSet ds = new DataSet();

                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
                BtnExport.Visible = false;

                ChkBoxListStaff.Width = 3000;
                PanelRations.Visible = false;

                LoadProgm();
                loadModeStudy();
                LoadLevel2();

                LoadSession();

                LoadFaculty2();
                LoadEntryMode();
                populategrd();
            }
            //3
            if (TabContainer1.ActiveTabIndex == 1)
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
                DisplayGrid();
                // loadDDDeptnames2();
                //PerformDisplay();

                GridView1.Visible = true;
                BtnExport.Visible = true;

            }
            //History
            if (TabContainer1.ActiveTabIndex == 2)
            {

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll Automation", "");
        }
    }

    private void PerformDisplay()
    {
        //if (DDListDeptSearch.Text.Trim() == "All")
        //{
        //    DisplayGrid();
        //}
        //else
        //{
        //    DisplayGridByDept(DDListDeptSearch.Text.Trim());
        //}
    }

    private void DisplayGrid()
    {
        LoadAwaitApproval();
    }
    private void LoadSalGrades(string SalaryStructureCode)
    {
        try
        {

            ////DDListGradeLev.Items.Clear();

            ////ArrayList fac = new ArrayList();

            ////BasicSalaryBusiness fb = new BasicSalaryBusiness();
            ////fac = fb.GetSalaryStructureGradeLevel(SalaryStructureCode);//.GetSalaryStructureCode();

            ////if (fac.Count > 0)
            ////{
            ////    foreach (string facty in fac)
            ////    {

            ////        DDListGradeLev.Items.Add(facty);
            ////        DDListGradeLev.Text = facty;
            ////    }


            ////    LoadSalSteps(SalaryStructureCode, DDListGradeLev.Text.Trim());

            ////}

            ////else
            ////{
            ////    DDListGradeLev.Items.Add("");
            ////    DDListStep.Items.Add("");

            ////}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }
    protected void DDListDept2_Changed(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept2.Text.Trim(), DDListFac2.Text.Trim());


    }
    protected void DDListFac2_Changed(object sender, EventArgs e)
    {
        loadDept2(DDListFac2.Text.Trim());

    }
    private void LoadSalSteps(string SalaryStructureCode, string SalGrade)
    {
        try
        {

            //DDListStep.Items.Clear();

            //ArrayList fac = new ArrayList();

            //BasicSalaryBusiness fb = new BasicSalaryBusiness();
            //fac = fb.GetSalaryStructureGradeLevelStep(SalaryStructureCode, SalGrade);//.GetSalaryStructureCode();

            //if (fac.Count > 0)
            //{
            //    foreach (string facty in fac)
            //    {

            //        DDListStep.Items.Add(facty);
            //    }


            //    //LoadSalSteps(DDListGradeLev.Text.Trim());

            //}

            //else
            //{
            //    DDListStep.Items.Add("");
            //    //DDListEmplyMode.Items.Add("");
            //}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }

    private void LoadSalaryCode()
    {
        try
        {

            //DDListSalStructure.Items.Clear();

            //ArrayList fac = new ArrayList();

            //BasicSalaryBusiness fb = new BasicSalaryBusiness();
            //fac = fb.GetSalaryStructureCode();

            //if (fac.Count > 0)
            //{
            //    foreach (string facty in fac)
            //    {

            //        DDListSalStructure.Items.Add(facty);
            //    }

            //    LoadSalGrades(DDListSalStructure.Text.Trim());
            //}

            //else
            //{
            //    DDListSalStructure.Items.Add("");
            //    DDListGradeLev.Items.Add("");
            //    DDListStep.Items.Add("");
            //    //DDListEmplyMode.Items.Add("");
            //}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }

    private void LoadDesignation()
    {
        try
        {

            //DDListDesignation.Items.Clear();

            //ArrayList fac = new ArrayList();


            //DesignationBusiness fb = new DesignationBusiness();
            //fac = fb.GetDesignations();

            //if (fac.Count > 0)
            //{
            //    foreach (string facty in fac)
            //    {
            //        DDListDesignation.Items.Add(facty);
            //    }

            //}

            //else
            //{
            //    DDListDesignation.Items.Add("");
            //}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }

    //private void LoadEmployeeEvent()
    //{
    //    try
    //    {
    //        DDListEvents.Items.Clear();
    //        string Qry = "";
    //        //query = "";
    //        EmployeeEvent = new Hashtable();

    //        DataSet ds = new DataSet();

    //        Qry = "SELECT [StatusCode],[StatusName]  FROM [EmployeeStatus]";

    //        ds = SearchData(Qry);
    //        int code = 0;
    //        string Sname = "";

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
    //            {
    //                code = int.Parse(ds.Tables[0].Rows[jj][0].ToString());
    //                Sname = ds.Tables[0].Rows[jj][1].ToString().ToUpper();
    //                DDListEvents.Items.Add(Sname);
    //                EmployeeEvent.Add(Sname, code);
    //            }
    //            LoadPaymentOption2(Sname);
    //        }
    //        else
    //        {
    //            DDListEvents.Items.Clear();
    //            TxtPaymetopion.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        LogError(msg, "Payroll", "");
    //    }
    //}

    //private void LoadPaymentOption2(string Sname)
    //{
    //    try
    //    {
    //        TxtPaymetopion.Text = "";
    //        int Status = 0;
    //        if (EmployeeEvent.ContainsKey(Sname))
    //        {
    //            Status = int.Parse(EmployeeEvent[Sname].ToString());
    //        }

    //        if (Status > 0)
    //        {
    //            selectedStatus = Status;
    //            DataSet ds = new DataSet();
    //            string Qry = "SELECT [PayrollEffect],[PayrollStatus]  FROM [EmployeeStatus] where [StatusCode]= " + Status + "";


    //            ds = SearchData(Qry);
    //            int code = 0;


    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
    //                {
    //                    TxtPaymetopion.Text = ds.Tables[0].Rows[jj][0].ToString();
    //                    SelectedPayrollStatus = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
    //                }

    //            }
    //            else
    //            {
    //                TxtPaymetopion.Text = "";
    //            }
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        LogError(msg, "Payroll", "");
    //    }
    //}
    private DataSet GetEmpInfo(string Qry)
    {

        DataSet ds = new DataSet();
        try
        {
            ds = SearchData(Qry);
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
        return ds;
    }
    private DataSet GetPayrollEffect(string eventname)
    {

        DataSet ds = new DataSet();
        try
        {
            int Status = 0;
            if (EmployeeEvent.ContainsKey(eventname))
            {
                Status = int.Parse(EmployeeEvent[eventname].ToString());
            }

            if (Status > 0)
            {
                selectedStatus = Status;

                string Qry = "SELECT [PayrollEffect],[PayrollStatus]  FROM [EmployeeStatus] where [StatusCode]= " + Status + "";


                ds = SearchData(Qry);
                //int code = 0;


                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                //    {
                //        TxtPaymetopion.Text = ds.Tables[0].Rows[jj][0].ToString();
                //        SelectedPayrollStatus = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                //    }

                //}
                //else
                //{
                //    TxtPaymetopion.Text = "";
                //}
            }



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
        return ds;
    }
    protected void SupplierGridv_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int i = 0;
        //i = SupplierGridv.SelectedIndex;
        //string inf = SupplierGridv.Rows[i].Cells[2].Text;
        //loadInfo(inf);
        //CheckBox checkbox = (CheckBox)sender;
        //GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        //Response.Write(row.Cells[0].Text); 
    }
    //private void GenerateEventNumb()
    //{
    //    SqlConnection cnn = new SqlConnection(str);
    //    cnn.Open();

    //    SqlCommand cmd = null;
    //    SqlDataReader dr = null;

    //    try
    //    {
    //        cmd = new SqlCommand("SELECT count(*)from [EmployeeEventNumbers]", cnn);
    //        int m = (int)cmd.ExecuteScalar();
    //        cmd.Dispose();

    //        int LastNumber = 0;
    //        string PONUMBER = "";

    //        int Status = 1;

    //        int IntPart = 0;
    //        int realID = 1;
    //        int Number = 0;
    //        Number = realID + m;

    //        //
    //        if (m == 0)
    //        {
    //            //if (POPref != "")
    //            //{
    //            //    PONUMBER = POPref + String.Format("{0:00}", Number);
    //            //}
    //            //else
    //            //{
    //            //    PONUMBER = String.Format("{0:00}", Number);
    //            //}
    //            //LabelPOnumber.Visible = true;
    //            //LabelPOnumber.Text = PONUMBER;
    //            LabelStatus.Text = Status.ToString();
    //            return;
    //        }
    //        else
    //        {
    //            cmd = new SqlCommand("SELECT Top(1)[EventNumbers] from [EmployeeEventNumbers] order by [srn] desc", cnn);
    //            dr = cmd.ExecuteReader();
    //            while (dr.Read())
    //            {
    //                LastNumber = int.Parse(dr.GetValue(0).ToString());
    //            }
    //            dr.Dispose();
    //            cmd.Dispose();

    //            if (LastNumber > 0)
    //            {
    //                Status = LastNumber + 1;
    //                LabelStatus.Text = Status.ToString();
    //                //realID = 0;

    //                //string pattern1 = ConfigurationManager.AppSettings["regexint"];
    //                //MatchCollection mc1 = Regex.Matches(LastNumber, pattern1);


    //                //for (int i = 0; i < mc1.Count; i++)
    //                //{
    //                //    IntPart = int.Parse(mc1[0].ToString());
    //                //}


    //                //realID = IntPart + 1;
    //                //Number = realID;

    //                //if (POPref != "")
    //                //{
    //                //    PONUMBER = POPref + String.Format("{0:00}", Number);
    //                //}
    //                //else
    //                //{
    //                //    PONUMBER = String.Format("{0:00}", Number);
    //                //}

    //                //LabelPOnumber.Visible = true;
    //                //LabelPOnumber.Text = PONUMBER;
    //                return;

    //            }
    //        }
    //        cnn.Dispose();
    //        cnn.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //    }
    //}

    //private void LoadPaymentOption()
    //{
    //    DDListPaymentOption.Items.Clear();
    //    DDListPaymentOption.Items.Add("With Payment");
    //    DDListPaymentOption.Items.Add("Without Payment");
    //}
    private void LogError(string strMsg, string SourceApp, string SourceMethod)
    {
        ////cp = new CPermit();
        ////cp.Direction = strMsg;
        ////cp.SourceApplication = SourceApp;
        ////cp.MethodName = SourceMethod;
        ////cp.MsgType = "ERROR";
        ////rq = new CWritetoqueue();
        ////rq.strPath = auditque;
        //////rq.Logonpermit.MsgType=;
        ////rq.Writeaudit(cp);
        //log.Error(strMsg);
    }
    private void LoadFaculty()
    {
        //try
        //{
        //    DDListPaymentOption.Items.Clear();

        //    FacultyBusiness fb = new FacultyBusiness();
        //    FacultyId = fb.GetFacultyNameAndId();

        //    if (FacultyId.Count > 0)
        //    {
        //        foreach (DictionaryEntry facty in FacultyId)
        //        {
        //            DDListPaymentOption.Items.Add(facty.Key.ToString());
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}
    }
    protected void BtnAddnew_Click(object sender, EventArgs e)
    {
        //PaymentTypePanel1.Visible = true;       

        //populatetreeview();

    }
    protected void LnkBtnPrint_Click(object sender, EventArgs e)
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
    private void populatetreeview()
    {
        try
        {
            if (TxtSearch3.Text.Trim() != "")
            {
                populategrdv2();
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }

    private void populategrdv2()
    {
        try
        {


            string qry = "";
            //qry = "SELECT [EmpID] as OGNO,[Title],[Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Faculty],[DepartmentSection] as [Department],[DepartmentUnit] as [Unit] ,[OldSalaryStructureGroup] as [Old Salary Group],[NewSalaryStructureGroup] as [New Salary Group],[OldDesignation] as [Old Designation],[NewDesignation] as [New Designation],[OldGradeLevel] as [Old Grade Level],[NewGradeLevel] as [New Grade Level],[OldStep] as [Old Step],[NewStep] as [New Step],[DateApproved] as [Date] from [Promotions] where [EmpID] like '%" + TxtSearch + "%' or [EmpID] like '%" + TxtSearch + "%' or [Title] like '%" + TxtSearch + "%' or [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MinistryFaculty] like '%" + TxtSearch + "%' or [DepartmentSection] like '%" + TxtSearch + "%' or [DepartmentUnit] like '%" + TxtSearch + "%' or [NewDesignation] like '%" + TxtSearch + "%' or [NewGradeLevel] like '%" + TxtSearch + "%' or [NewStep] like '%" + TxtSearch + "%' or [NewSalaryStructureGroup] like '%" + TxtSearch + "%' or [ApprovedBy] like '%" + TxtSearch + "%' or [ApprovedStatus] like '%" + TxtSearch + "%' or [DateApproved] like '%" + TxtSearch + "%' or [OldDesignation] like '%" + TxtSearch + "%' or [OldStep] like '%" + TxtSearch + "%' or [OldSalaryStructureGroup] like '%" + TxtSearch + "%'or [PromotionMode] like '%" + TxtSearch + "%'or [Email] like '%" + TxtSearch + "%'or [DisApprovedStatus] like '%" + TxtSearch + "%' and ApprovedStatus = 0 and [DisApprovedStatus] = 0";

            //// if (Group.ToUpper() == "WEB MASTER")
            //// {
            ////     qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [New School],[FacultyNameOld] as [Old School],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Course Of Study],[CourseOfStudyNameOld] as [Old Course Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status] FROM [ChangeOfCourse] where  [ApproveStatus] = 0 and [DisApprovedStatus] = 0 order by [CreatedDate] desc";
            //// }

            //// exportheader = "[MatNo],[Surname],[Other Names],[Session],[New School],[Old School],[New Department],[Old Department],[New Level] ,[Old Level],[New Course Of Study],[Old Course Of Study],[New Programme],[Old Programme],[New Duration],[Old Duration],[New Honours],[Old Honours],[New Mode Of Study],[Old Mode Of Study],[DisApproved]";

            //// Exportfilename = "Change of Courses ";
            //// GridCaption = "Change of Courses";
            //// GViewWidth = 1100;
            ////// populategr(qry);


            string TxtSearch = TxtSearch3.Text.Trim();
            qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [New School],[FacultyNameOld] as [Old School],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Course Of Study],[CourseOfStudyNameOld] as [Old Course Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status],[ApproveStatus] as [Approve Status] FROM [ChangeOfCourse] where [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MatricNumber] like '%" + TxtSearch + "%' or [PresentSession] like '%" + TxtSearch + "%' or [CreatedDate] like '%" + TxtSearch + "%' or [FacultyName] like '%" + TxtSearch + "%' or [DepartmentName] like '%" + TxtSearch + "%' or [AcademicLevel] like '%" + TxtSearch + "%' or [CourseOfStudyName] like '%" + TxtSearch + "%' or [Programme] like '%" + TxtSearch + "%' or [Duration] like '%" + TxtSearch + "%' or [Honours] like '%" + TxtSearch + "%'or [ModeOfStudy] like '%" + TxtSearch + "%' or [ApproveStatus] like '%" + TxtSearch + "%' or [DisApprovedStatus] like '%" + TxtSearch + "%' order by [CreatedDate] desc";

            //qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [New School],[FacultyNameOld] as [Old School],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Course Of Study],[CourseOfStudyNameOld] as [Old Course Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status] FROM [ChangeOfCourse] where  [ApproveStatus] = 0 and [DisApprovedStatus] = 0 order by [CreatedDate] desc";
            // = "SELECT [EmpID] as OGNO,[Title],[Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Faculty],[DepartmentSection] as [Department],[DepartmentUnit] as [Unit] ,[OldSalaryStructureGroup] as [Old Salary Group],[NewSalaryStructureGroup] as [New Salary Group],[OldDesignation] as [Old Designation],[NewDesignation] as [New Designation],[OldGradeLevel] as [Old Grade Level],[NewGradeLevel] as [New Grade Level],[OldStep] as [Old Step],[NewStep] as [New Step],[DateApproved] as [Date],[ApprovedBy] as [Approved By],[ApprovedStatus] as [Approved Status],[PromotionMode] as [Promotion Mode],[DisApprovedStatus]as [DisApproved Status] from [Promotions] where [EmpID] like '%" + TxtSearch.Text.Trim() + "%' or [EmpID] like '%" + TxtSearch.Text.Trim() + "%' or [Title] like '%" + TxtSearch.Text.Trim() + "%' or [Surname] like '%" + TxtSearch.Text.Trim() + "%' or [OtherNames] like '%" + TxtSearch.Text.Trim() + "%' or [MinistryFaculty] like '%" + TxtSearch.Text.Trim() + "%' or [DepartmentSection] like '%" + TxtSearch.Text.Trim() + "%' or [DepartmentUnit] like '%" + TxtSearch.Text.Trim() + "%' or [NewDesignation] like '%" + TxtSearch.Text.Trim() + "%' or [NewGradeLevel] like '%" + TxtSearch.Text.Trim() + "%' or [NewStep] like '%" + TxtSearch.Text.Trim() + "%' or [NewSalaryStructureGroup] like '%" + TxtSearch.Text.Trim() + "%' or [ApprovedBy] like '%" + TxtSearch.Text.Trim() + "%' or [ApprovedStatus] like '%" + TxtSearch.Text.Trim() + "%' or [DateApproved] like '%" + TxtSearch.Text.Trim() + "%' or [OldDesignation] like '%" + TxtSearch.Text.Trim() + "%' or [OldStep] like '%" + TxtSearch.Text.Trim() + "%' or [OldSalaryStructureGroup] like '%" + TxtSearch.Text.Trim() + "%'or [PromotionMode] like '%" + TxtSearch.Text.Trim() + "%'or [Email] like '%" + TxtSearch.Text.Trim() + "%'or [DisApprovedStatus] like '%" + TxtSearch.Text.Trim() + "%'";

            exportheader = "[MatNo],[Surname],[Other Names],[Session],[New School],[Old School],[New Department],[Old Department],[New Level] ,[Old Level],[New Course Of Study],[Old Course Of Study],[New Programme],[Old Programme],[New Duration],[Old Duration],[New Honours],[Old Honours],[New Mode Of Study],[Old Mode Of Study],[DisApproved],[Approve Status]";

            Exportfilename = "Change of Courses ";
            GridCaption = "Change of Courses";
            GViewWidth = 1500;

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);

            dat.Fill(ds);

            //GridViewRow gvr = GridView1.Rows;
            //CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
            //chk.Visible = false;  

            if (GViewWidth > 0)
            {
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    gvr.FindControl("CheckBoxGIN").Visible = false;
                    //CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN").Visible = false; ;
                    //chk.Visible = false;                   
                }

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

    //private void populategrdv(DataSet qr)
    //{
    //    throw new NotImplementedException();
    //}
    private void populategrdv(string query)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);

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
                    //formatGridview();
                }

                GridView1.Visible = true;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    //ntc = new NtwColor();
                    if (AllFaculty != null)
                    {
                        foreach (string factty in AllFaculty)
                        {
                            if (GridView1.Rows[i].Cells[1].Text == factty)
                            {

                                //GridView1.Rows[i].Cells[1].BackColor = System.Drawing.Color.DarkOrange;
                                GridView1.Rows[i].BackColor = System.Drawing.Color.LightSalmon;//.DarkOrange;
                                GridView1.Rows[i].BorderStyle = BorderStyle.Groove;//.Solid;
                                GridView1.Rows[i].BorderWidth = 5;
                                GridView1.Rows[i].Font.Bold = true;
                                GridView1.Rows[i].Font.Size = 15;

                            }

                        }
                    }
                    else
                    {
                        break;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
    }
    private void loadDDDeptnames()
    {

    }
    //private void refreshscreen()
    //{
    //    try
    //    {
    //        // populatetreeview();
    //        PaymentTypePanel1.Visible = true;
    //        //PaymentTypePanelGridv.Visible = true;
    //        //LabelselectPaymentType.Visible = true;
    //        //DDlistPaymentType.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace;
    //        LogError(msg, "Payroll", "");
    //        showmassage(msg);
    //    }
    //}

    protected void DDlistPaymentType_OnSelectedIndexChanged(object sender, System.EventArgs e)
    {

        loadDeptnames();
    }

    private void loadDeptnames()
    {

        //try
        //{

        //    //DDListDept.Items.Clear();
        //    DataSet ds = new DataSet();
        //    //string TxtSearch = TxtSearchstaff.Text.Trim();

        //    query = "SELECT distinct [DepartmentSection] FROM [Employees]";


        //    ds = SearchData(query);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
        //        {
        //            string Dept = ds.Tables[0].Rows[jj][0].ToString();//.ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][6].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper();
        //            DDListDept.Items.Add(Dept);

        //        }

        //        LoadStaffNames(DDListDept.Text.Trim());

        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string Qry = "";

        //    if (DDListPaymentOption.Text.Trim() != "")
        //    {
        //        DDListPaymentOption.Text = DDListPaymentOption.Text.Trim().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Chose payment Option";
        //        showmassage(msg);
        //        //DDListPaymentOption.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }

        //    if (TxtEventName.Text != "")
        //    {
        //        TxtEventName.Text = TxtEventName.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
        //    }
        //    else
        //    {
        //        msg = "Enter Event Name eg Sabatical Leave";
        //        showmassage(msg);
        //        TxtEventName.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }

        //    if (LabelStatus.Text == "")
        //    {
        //        msg = "Employee Status Code Not Populated";
        //        showmassage(msg);
        //        LabelStatus.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }

        //    //With Payment");
        //    //DDListPaymentOption.Items.Add("Without Payment

        //    int payrollstatus = 0;
        //    if (DDListPaymentOption.Text.Trim() == "With Payment")
        //    {
        //        payrollstatus = 1;
        //    }


        //    int statuscode = int.Parse(LabelStatus.Text.Trim());
        //    //Qry = "SELECT * FROM [EmployeeStatus] where [StatusCode]=" + statuscode + " or [StatusName]='" + TxtEventName.Text.Trim().ToUpper() + "' and [PayrollEffect]='" + DDListPaymentOption.Text.Trim() + "' ";
        //    Qry = "SELECT * FROM [EmployeeStatus] where [StatusCode]=" + statuscode + " or [StatusName]='" + TxtEventName.Text.Trim().ToUpper() + "'";
        //    if (Existed(Qry))
        //    {
        //        msg = "The information you are trying to enter exixted before";
        //        showmassage(msg);
        //        return;
        //    }
        //    else
        //    {
        //        Qry = "INSERT INTO [EmployeeStatus]([StatusCode],[StatusName],[PayrollEffect],[PayrollStatus]) VALUES(" + statuscode + ",'" + TxtEventName.Text.Trim().ToUpper() + "','" + DDListPaymentOption.Text.Trim() + "'," + payrollstatus + ")";
        //        PerformInsert(Qry);

        //        Qry = "INSERT INTO [EmployeeEventNumbers]([EventNumbers]) VALUES(" + statuscode + ")";
        //        PerformInsert(Qry);

        //        TxtEventName.Text = "";
        //        GenerateEventNumb();
        //    }


        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}

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
    private void PerformDel(string Qry)
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
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }

        return ret;
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

        //GridView1.DataSource = null;
        //GridView1.DataBind();

        //TxtEventName.Text = "";
        //TxtDepartmentName.Text = "";
        //TxtHOD.Text = "";
        BtnExport.Visible = false;
        //BtnRefresh.Visible = false;
        TabContainer1.ActiveTabIndex = -1;
        TabContainer1.Dispose();

    }
    protected void BankGridv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnViewallbanks_Click(object sender, EventArgs e)
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
        //try
        //{

        //    if (DDListPaymentOption.Text != "" && TxtDeptCode.Text != "")
        //    {
        //        DDListPaymentOption.Text = DDListPaymentOption.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //        TxtDeptCode.Text = TxtDeptCode.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Enter faculty and faculty prefix";
        //        showmassage(msg);
        //        DDListPaymentOption.Focus();
        //        return;
        //    }


        //    CSingleAttribute cs = new CSingleAttribute();
        //    cs.ActionType = "Delete";
        //    cs.MethodName = "Delete";
        //    cs.SourceClass = "Faculty";
        //    cs.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    cs.SourceApplication = "Payroll";
        //    cs.ActionBody = "Delete from [edup_Faculty] where [FacultyName]='" + DDListPaymentOption.Text.Trim() + "' and [FacultyPrefixCode]='" + TxtDeptCode.Text.Trim() + "'";
        //    sendQuery(cs);

        //    log.Info("Delete faculty by:");
        //    populatetreeview();
        //    PaymentTypePanel1.Visible = true;
        //    DDListPaymentOption.Text = "";
        //    TxtDepartmentName.Text = "";
        //    TxtDeptCode.Text = "";
        //    DDListPaymentOption.Focus();

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}
        ////cmd = new SqlCommand(", cnn);

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }

    protected void LinkBtnAddNew_Click(object sender, EventArgs e)
    {
        AllFaculty = null;
        //PaymentTypePanel1.Visible = true;
        LoadFaculty();
        //populatetreeview();
        //MultiView1.ActiveViewIndex = 0;


    }
    //DDListFacEdit_Changed
    protected void DDListFacEdit_Changed(object sender, EventArgs e)
    {
        //loadDept(DDListFacEdit.Text.Trim());
    }
    //DDListDeptEdit_Changed
    protected void DDListDeptEdit_Changed(object sender, EventArgs e)
    {
        //retrievDat(DDListDeptEdit.Text.Trim());
    }
    protected void LinkBtnExit_Click(object sender, EventArgs e)
    {
        AllFaculty = null;
        Server.Transfer("Clientwelcome.aspx?re=0");
    }
    protected void LinkBtnEdit_Click(object sender, EventArgs e)
    {
        //PaymentTypePanel1.Visible = true;
        //AllFaculty = null;
        //LoadFacultyEdit();
        //BtnDel.Enabled = false;
        //populatetreeview();
        //MultiView1.ActiveViewIndex = 1;
    }

    private void LoadEntryMode()
    {
        DDListEntryMode.Items.Clear();

        try
        {
            DataSet ds = new DataSet();
            string qry = "SELECT [EntryMode] FROM [EntryMode] order by [EntryMode] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListEntryMode.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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

    private void retrievDat(string Dept)
    {
        try
        {
            //TxtFaculty.Text = TreeView1.SelectedValue.ToString();

            //WebServ = new CybWebServices.Service1();
            //string item = "";
            ////int col = 2;
            ////object[] arr = null;
            //string qry = "SELECT [DepartmentName],[DepartmentCode],[Hod] FROM [edup_Departments] where DepartmentName='" + Dept + "' ";
            ////arr = WebServ.RetreiveFac(qry, col);
            ////ArrayList fac = new ArrayList(arr);

            //ArrayList fac = new ArrayList();
            //DepartmentsBusiness Db = new DepartmentsBusiness();

            //DataSet ds00 = Db.GetDepartmentSchemaByID(Dept);
            //string Itm2 = "";
            //if (ds00.Tables[0].Rows.Count > 0)
            //{
            //    for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
            //    {
            //        Itm2 = ds00.Tables[0].Rows[jj][0].ToString().ToUpper();

            //        TxtDeptNameEdit.Text = ds00.Tables[0].Rows[jj][2].ToString().ToUpper();
            //        TxtDeptCodeEdit.Text = ds00.Tables[0].Rows[jj][3].ToString().ToUpper();
            //        TxtHodEdit.Text = ds00.Tables[0].Rows[jj][4].ToString().ToUpper();

            //        fac.Add(Itm2);
            //    }
            //}

            //if (fac.Count > 0)
            //{

            //    //GViewWidth = 0;
            //    //query = "SELECT [FacultyName] as Faculty,[DepartmentName] as Department,[DepartmentCode] as [Department Code],[Hod] as [Head Of Department] FROM [edup_Departments] where DepartmentName='" + TxtDeptNameEdit.Text.Trim() + "'";
            //    ////query = "SELECT [AdmissionSession] as Session,[EntryMode],[RegNo],[Surname],[OtherNames],[Sex],[Age],[State],[Country],[AdmissionStatus],[CourseOfStudy],[TransactionDate] FROM [EntryTable] where [AdmissionSession] like '%" + TxtSearch.Text + "%' or EntryMode like '%" + TxtSearch.Text + "%' or RegNo like '%" + TxtSearch.Text + "%' or Surname like '%" + TxtSearch.Text + "%' or OtherNames like '%" + TxtSearch.Text + "%' or Sex like '%" + TxtSearch.Text + "%' or Age like '%" + TxtSearch.Text + "%' or State like '%" + TxtSearch.Text + "%' or Country like '%" + TxtSearch.Text + "%' or AdmissionStatus like '%" + TxtSearch.Text + "%' or CourseOfStudy like '%" + TxtSearch.Text + "%' or [TransactionDate] like '%" + TxtSearch.Text + "%' order by TransactionDate desc";
            //    //exportheader = "[FacultyName] as Faculty,[DepartmentName] as Department,[DepartmentCode] as [Department Code],[Hod] as [Head Of Department]";
            //    //Exportfilename = "Dept";
            //    //GridCaption = "Depts";
            //    //GViewWidth = 900;
            //    //populategrdv(query);

            //    GViewWidth = 0;

            //    exportheader = DepartmentsBusiness.exportheader;

            //    Exportfilename = DepartmentsBusiness.Exportfilename;
            //    GridCaption = DepartmentsBusiness.GridCaption;
            //    GViewWidth = DepartmentsBusiness.GViewWidth;

            //    DataSet ds = new DataSet();
            //    ds = Db.GetDepartmentSchemaByID(Dept);
            //    populategrdv(ds);
            //}
            //else
            //{
            //    TxtDeptNameEdit.Text = "";
            //    TxtDeptCodeEdit.Text = "";
            //    TxtHodEdit.Text = "";
            //}
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        //string header = DepartmentsBusiness.exportheader;// exportheader;
        //string filename = DepartmentsBusiness.Exportfilename;// Exportfilename;
        //ExportData(header, filename);

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
            LogError(msg, "Payroll", "");
            showmassage(msg);
            return;
        }
    }
    protected void BtnSubmitEdit_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    if (DDListFacEdit.Text != "")
        //    {
        //        DDListFacEdit.Text = DDListFacEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Select Faculty Name";
        //        showmassage(msg);
        //        DDListFacEdit.Focus();
        //        PaymentTypePanel1.Visible = true;
        //        return;
        //    }
        //    if (DDListDeptEdit.Text != "")
        //    {
        //        DDListDeptEdit.Text = DDListDeptEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Select Department Name";
        //        showmassage(msg);
        //        DDListDeptEdit.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }


        //    if (TxtDeptCodeEdit.Text != "")
        //    {
        //        TxtDeptCodeEdit.Text = TxtDeptCodeEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
        //    }
        //    else
        //    {
        //        msg = "Enter dept code eg CSC, PHY, CHM";
        //        showmassage(msg);
        //        TxtDeptCodeEdit.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }

        //    if (TxtDeptNameEdit.Text != "")
        //    {
        //        TxtDeptNameEdit.Text = TxtDeptNameEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
        //        PresentDept = TxtDeptNameEdit.Text.Trim();
        //    }
        //    else
        //    {
        //        msg = "Enter dept name";
        //        showmassage(msg);
        //        TxtDeptNameEdit.Focus();
        //        //PaymentTypePanel1.Visible = true;
        //        return;
        //    }

        //    if (TxtHodEdit.Text != "")
        //    {
        //        TxtHodEdit.Text = TxtHodEdit.Text.ToUpper().Replace("'", "''").Replace(";", ":");//.Replace("STATE", "").Trim();
        //    }
        //    else
        //    {
        //        TxtHodEdit.Text = "NONE";
        //    }


        //    Departments Dept = new Departments();
        //    Dept.DepartmentName = TxtDeptNameEdit.Text.Trim();
        //    Dept.DepartmentCode = TxtDeptCodeEdit.Text.Trim();
        //    Dept.Hod = TxtHodEdit.Text.Trim();
        //    Dept.OlddepartmentCode = TxtDeptCodeEdit.Text.Trim();

        //    DepartmentsBusiness Db = new DepartmentsBusiness();
        //    Db.UpdateDepartment(Dept);


        //    msg = "Update successful";
        //    showmassage(msg);



        //    GViewWidth = 0;

        //    exportheader = DepartmentsBusiness.exportheader;

        //    Exportfilename = DepartmentsBusiness.Exportfilename;
        //    GridCaption = DepartmentsBusiness.GridCaption;
        //    GViewWidth = DepartmentsBusiness.GViewWidth;

        //    DataSet ds = new DataSet();
        //    ds = Db.GetDepartmentSchemaByID(PresentDept);
        //    populategrdv(ds);



        //    TxtDeptCodeEdit.Text = "";
        //    TxtDeptNameEdit.Text = "";
        //    TxtHodEdit.Text = "";
        //    //LoadFacultyEdit();
        //    TxtDeptNameEdit.Focus();

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}
    }

    private bool confirmExistedB4(string qry)
    {
        bool succ = false;
        try
        {


            //ArrayList fac = new ArrayList();
            ////string qry = "SELECT [Level]  FROM [Levels]";
            //WebServ = new CybWebServices.Service1();
            //DataSet ds00 = WebServ.RetriveDat(qry);
            //string Itm2 = "";
            //if (ds00.Tables[0].Rows.Count > 0)
            //{
            //    succ = true;
            //    //for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
            //    //{
            //    //    Itm2 = ds00.Tables[0].Rows[jj][0].ToString().ToUpper();

            //    //    fac.Add(Itm2);
            //    //}
            //}

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
        return succ;
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    if (DDListFacEdit.Text != "" && DDListDeptEdit.Text != "")
        //    {
        //        //DDListFaculty.Text = DDListFaculty.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //        //TxtDeptCode.Text = TxtDeptCode.Text.Trim().ToUpper().Replace("'", "''").Replace(";", ":");
        //    }
        //    else
        //    {
        //        msg = "Select Department to delete";
        //        showmassage(msg);
        //        DDListDeptEdit.Focus();
        //        return;
        //    }

        //    Departments Dept = new Departments();
        //    Dept.DepartmentCode = TxtDeptCodeEdit.Text.Trim();

        //    DepartmentsBusiness Db = new DepartmentsBusiness();
        //    Db.DeleteDepartment(Dept);

        //    //CSingleAttribute cs = new CSingleAttribute();
        //    //cs.ActionType = "Delete";
        //    //cs.MethodName = "Delete";
        //    //cs.SourceClass = "Department";
        //    //cs.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    //cs.SourceApplication = "Payroll";
        //    //cs.ActionBody = "Delete from [edup_Departments] where [DepartmentCode]='" + TxtDeptCodeEdit.Text.Trim() + "'";
        //    //sendQuery(cs);

        //    //log.Info("Department Deleted by:");
        //    msg = "Delete successful";
        //    showmassage(msg);
        //    populatetreeview();
        //    //PaymentTypePanel1.Visible = true;

        //    TxtHodEdit.Text = "";
        //    TxtDeptNameEdit.Text = "";

        //    TxtDeptNameEdit.Text = "";
        //    LoadFacultyEdit();
        //    TxtDeptNameEdit.Focus();

        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}
        //cmd
    }
    protected void BtnCloseEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        //GridView1.DataSource = null;
        //GridView1.DataBind();

        //TxtDeptCodeEdit.Text = "";
        //TxtDeptNameEdit.Text = "";
        //TxtHodEdit.Text = "";
        TabContainer1.ActiveTabIndex = 0;
        //MultiView1.ActiveViewIndex = -1;
    }
    //protected void LinkBtnSearch_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();

    //    //GridView1.DataSource = null;
    //    //GridView1.DataBind();
    //    //MultiView1.ActiveViewIndex = 2;
    //    TxtSearch.Focus();
    //}
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        //populatetreeview();
    }
    //DDListDeptSearch_Changed
    protected void DDListDeptSearch_Changed(object sender, EventArgs e)
    {
        PerformDisplay();
        //DisplayGridByDept(DDListDeptSearch.Text.Trim());
    }
    //CheckBoxSummary_Check
    protected void CheckBoxSummary_Check(object sender, EventArgs e)
    {
        //if (CheckBoxSummary.Checked)
        //{
        //    loadDDDeptnames2();
        //    DDListDeptSearch.Visible = true;

        //}
        //else
        //{
        //    DDListDeptSearch.Items.Clear();
        //    DDListDeptSearch.Visible = false;
        //    DataSet ds = null;
        //    GridView1.DataSource = ds;
        //    GridView1.DataBind();
        //    DisplayGrid();
        //}

    }

    private void loadDDDeptnames2()
    {
        //try
        //{
        //    DDListDeptSearch.Items.Clear();
        //    DataSet ds = new DataSet();
        //    //string TxtSearch = TxtSearchstaff.Text.Trim();

        //    query = "SELECT distinct [DepartmentSection] FROM [Promotions]";


        //    ds = SearchData(query);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
        //        {
        //            string Dept = ds.Tables[0].Rows[jj][0].ToString();//.ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][6].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper();
        //            DDListDeptSearch.Items.Add(Dept);

        //        }
        //        DDListDeptSearch.Items.Add("All");
        //        DisplayGridByDept(DDListDeptSearch.Text.Trim());
        //        //LoadStaffNames(DDListDept.Text.Trim());

        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }

    private void DisplayGridByDept(string TxtSearch)
    {
        DataSet ds = new DataSet();
        try
        {
            string qry = "";
            //qry = "SELECT [EmpID] as OGNO,[Title],[Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Faculty],[DepartmentSection] as [Department],[DepartmentUnit] as [Unit] ,[OldSalaryStructureGroup] as [Old Salary Group],[NewSalaryStructureGroup] as [New Salary Group],[OldDesignation] as [Old Designation],[NewDesignation] as [New Designation],[OldGradeLevel] as [Old Grade Level],[NewGradeLevel] as [New Grade Level],[OldStep] as [Old Step],[NewStep] as [New Step],[DateApproved] as [Date] from [Promotions] where [EmpID] like '%" + TxtSearch + "%' or [EmpID] like '%" + TxtSearch + "%' or [Title] like '%" + TxtSearch + "%' or [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MinistryFaculty] like '%" + TxtSearch + "%' or [DepartmentSection] like '%" + TxtSearch + "%' or [DepartmentUnit] like '%" + TxtSearch + "%' or [NewDesignation] like '%" + TxtSearch + "%' or [NewGradeLevel] like '%" + TxtSearch + "%' or [NewStep] like '%" + TxtSearch + "%' or [NewSalaryStructureGroup] like '%" + TxtSearch + "%' or [ApprovedBy] like '%" + TxtSearch + "%' or [ApprovedStatus] like '%" + TxtSearch + "%' or [DateApproved] like '%" + TxtSearch + "%' or [OldDesignation] like '%" + TxtSearch + "%' or [OldStep] like '%" + TxtSearch + "%' or [OldSalaryStructureGroup] like '%" + TxtSearch + "%'or [PromotionMode] like '%" + TxtSearch + "%'or [Email] like '%" + TxtSearch + "%'or [DisApprovedStatus] like '%" + TxtSearch + "%' and ApprovedStatus = 0 and [DisApprovedStatus] = 0";

            if (Group.ToUpper() == "WEB MASTER")
            {
                qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [New School],[FacultyNameOld] as [Old School],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Course Of Study],[CourseOfStudyNameOld] as [Old Course Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status] FROM [ChangeOfCourse] where [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MatricNumber] like '%" + TxtSearch + "%' or [PresentSession] like '%" + TxtSearch + "%' or [CreatedDate] like '%" + TxtSearch + "%' or [FacultyName] like '%" + TxtSearch + "%' or [DepartmentName] like '%" + TxtSearch + "%' or [AcademicLevel] like '%" + TxtSearch + "%' or [CourseOfStudyName] like '%" + TxtSearch + "%' or [Programme] like '%" + TxtSearch + "%' or [Duration] like '%" + TxtSearch + "%' or [Honours] like '%" + TxtSearch + "%'or [ModeOfStudy] like '%" + TxtSearch + "%' or [ApproveStatus] like '%" + TxtSearch + "%'  and  [ApproveStatus] = 0 and [DisApprovedStatus] = 0 order by [CreatedDate] desc";
            }

            exportheader = "[MatNo],[Surname],[Other Names],[Session],[New School],[Old School],[New Department],[Old Department],[New Level] ,[Old Level],[New Course Of Study],[Old Course Of Study],[New Programme],[Old Programme],[New Duration],[Old Duration],[New Honours],[Old Honours],[New Mode Of Study],[Old Mode Of Study],[DisApproved]";

            Exportfilename = "Change of Courses ";
            GridCaption = "Change of Courses";
            GViewWidth = 1100;
            populategr(qry);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }



    //private void Perform()
    //{

    //    DataSet ds2 = null;

    //    try
    //    {
    //        exportheader = DepartmentsBusiness.exportheader;
    //        Exportfilename = DepartmentsBusiness.Exportfilename;
    //        GridCaption = DepartmentsBusiness.GridCaption;
    //        GViewWidth = DepartmentsBusiness.GViewWidth;

    //        DepartmentsBusiness Db = new DepartmentsBusiness();
    //        ds2 = Db.SearchDeptSummery();

    //        ArrayList fac = DepartmentsBusiness.AllFaculty;


    //        Session["ds2"] = ds2;
    //        GridView1.DataSource = ds2;
    //        GridView1.DataBind();

    //        if (GViewWidth > 0)
    //        {
    //            GridView1.Width = GViewWidth;
    //        }

    //        int TotalDept = ds2.Tables[0].Rows.Count - fac.Count;

    //        GridView1.SelectedRowStyle.Wrap = true;
    //        GridView1.Caption = GridCaption + ":" + " " + "Total = " + " " + TotalDept.ToString();
    //        GridView1.ToolTip = TotalDept.ToString();
    //        GridView1.CaptionAlign = TableCaptionAlign.Left;

    //        //Color mycolr= new Color();
    //        for (int i = 0; i < GridView1.Rows.Count; i++)
    //        {
    //            //ntc = new NtwColor();

    //            foreach (string factty in fac)
    //            {
    //                if (GridView1.Rows[i].Cells[1].Text == factty)
    //                {

    //                    //GridView1.Rows[i].Cells[1].BackColor = System.Drawing.Color.DarkOrange;
    //                    //GridView1.Rows[i].BackColor = System.Drawing.Color.LightSalmon;//.DarkOrange;
    //                    GridView1.Rows[i].BorderStyle = BorderStyle.Groove;//.Solid;
    //                    GridView1.Rows[i].BorderWidth = 5;
    //                    GridView1.Rows[i].Cells[1].Font.Bold = true;
    //                    GridView1.Rows[i].Cells[1].Font.Size = 15;
    //                    //GridView1.Rows[i].Cells[1].Font.Bold = false;
    //                    //GridView1.Rows[i].Cells[1].Font.Size = 8;


    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace;
    //        LogError(msg, "Payroll", "");
    //        showmassage(msg);
    //    }
    //}
    //


    private void LoadAwaitApproval()
    {
        DataSet ds = new DataSet();
        try
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string qry = "";
            //qry = "SELECT [EmpID] as OGNO,[Title],[Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Faculty],[DepartmentSection] as [Department],[DepartmentUnit] as [Unit] ,[OldSalaryStructureGroup] as [Old Salary Group],[NewSalaryStructureGroup] as [New Salary Group],[OldDesignation] as [Old Designation],[NewDesignation] as [New Designation],[OldGradeLevel] as [Old Grade Level],[NewGradeLevel] as [New Grade Level],[OldStep] as [Old Step],[NewStep] as [New Step],[DateApproved] as [Date] from [Promotions] where [EmpID] like '%" + TxtSearch + "%' or [EmpID] like '%" + TxtSearch + "%' or [Title] like '%" + TxtSearch + "%' or [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MinistryFaculty] like '%" + TxtSearch + "%' or [DepartmentSection] like '%" + TxtSearch + "%' or [DepartmentUnit] like '%" + TxtSearch + "%' or [NewDesignation] like '%" + TxtSearch + "%' or [NewGradeLevel] like '%" + TxtSearch + "%' or [NewStep] like '%" + TxtSearch + "%' or [NewSalaryStructureGroup] like '%" + TxtSearch + "%' or [ApprovedBy] like '%" + TxtSearch + "%' or [ApprovedStatus] like '%" + TxtSearch + "%' or [DateApproved] like '%" + TxtSearch + "%' or [OldDesignation] like '%" + TxtSearch + "%' or [OldStep] like '%" + TxtSearch + "%' or [OldSalaryStructureGroup] like '%" + TxtSearch + "%'or [PromotionMode] like '%" + TxtSearch + "%'or [Email] like '%" + TxtSearch + "%'or [DisApprovedStatus] like '%" + TxtSearch + "%' and ApprovedStatus = 0 and [DisApprovedStatus] = 0";

            //if (Group.ToUpper() == "WEB MASTER")
            //{
            qry = "select [MatricNumber] as [MatNo],[Surname],[OtherNames] as [Other Names],[PresentSession] as [Session],[FacultyName] as [New School],[FacultyNameOld] as [Old School],[DepartmentName] as [New Department],[DepartmentNameOld] as [Old Department],[AcademicLevel] as [New Level] ,[AcademicLevelOld] as [Old Level],[CourseOfStudyName] as [New Course Of Study],[CourseOfStudyNameOld] as [Old Course Of Study],[Programme] as [New Programme],[ProgrammeOld]  as [Old Programme],[Duration] as [New Duration],[DurationOld] as [Old Duration],[Honours] as [New Honours],[HonoursOld] as [Old Honours],[ModeOfStudy] as [New Mode Of Study],[ModeOfStudyOld] as [Old Mode Of Study],[DisApprovedStatus] as [DisApproved Status],EntryModeOld as [EntryMode Old],EntryMode as [EntryMode] FROM [ChangeOfCourse] where  [ApproveStatus] = 0 and [DisApprovedStatus] = 0 order by [CreatedDate] desc";
            //}

            exportheader = "[MatNo],[Surname],[Other Names],[Session],[New School],[Old School],[New Department],[Old Department],[New Level] ,[Old Level],[New Course Of Study],[Old Course Of Study],[New Programme],[Old Programme],[New Duration],[Old Duration],[New Honours],[Old Honours],[New Mode Of Study],[Old Mode Of Study],[DisApproved],[EntryMode Old],[EntryMode]";

            Exportfilename = "Change of Courses ";
            GridCaption = "Change of Courses";
            GViewWidth = 1000;
            populategr(qry);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }

    }
    protected void EditSelected_Changed(object sender, GridViewEditEventArgs e)
    {
        //try
        //{
        //    msg = "Please, be sure you exit this profile module by clicking END EXIT BUTTON on any of the pages";
        //    showmassage(msg);
        //    log.Info(msg);
        //    int j = GridViewEmployee.HeaderRow.Cells.Count;//.Columns.Count;
        //    int Sid = 0;
        //    int RefNO = 0;
        //    int StaffStatus = 2;

        //    for (int m = 0; m < j; m++)
        //    {
        //        if (GridViewEmployee.HeaderRow.Cells[m].Text.ToUpper() == "STAFF ID")
        //        {
        //            Sid = m;
        //        }
        //        if (GridViewEmployee.HeaderRow.Cells[m].Text.ToUpper() == "Reference No")
        //        {
        //            RefNO = m;
        //        }
        //        //if (GridView1.HeaderRow.Cells[m].Text.ToUpper() == "EXPIREDSTATUS")
        //        //{
        //        //    ExpStatus = m;
        //        //}

        //    }
        //    //pick col headers

        //    //Check if the person is 4 d company
        //    int i = 0;

        //    //i = GridView1.SelectedIndex;//.EditIndex;//.SelectedIndex;
        //    i = e.NewEditIndex;
        //    string StaffId = GridViewEmployee.Rows[i].Cells[Sid].Text;
        //    string RefNumber = GridViewEmployee.Rows[i].Cells[RefNO].Text;

        //    int status = int.Parse(GridViewEmployee.Rows[i].Cells[StaffStatus].Text);

        //    if (StaffId != "" && status > 0)
        //    {
        //        RtrieveStaffInfo(StaffId);
        //    }
        //    else
        //    {
        //        msg = "Check the status of this person and make sure the staff id is ok";
        //        showmassage(msg);
        //        return;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }
    protected void ChkBoxListStaff_Changed(object sender, EventArgs e)
    {
        string item = ChkBoxListStaff.Text;

        if (item != "")
        {
            LoadStaff(item);
        }
        else
        {
            PanelRations.Visible = false;
        }


    }

    private void LoadStaff(string item)
    {
        try
        {
            string body = item;
            string StaffId = "";
            string[] item2 = body.Split(new char[] { '|' });
            int upp = item2.GetUpperBound(0);
            StaffId = item2[0].ToString();

            if (StaffId != "")
            {
                getStaffInfo(StaffId);

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }

    private void getStaffInfo(string StaffId)
    {


        try
        {
            //string Qry = "";
            //query = "";

            DataSet ds = new DataSet();
            //string TxtSearch = TxtSearchstaff.Text.Trim();
            //Qry = "SELECT [EmpID] as [Staff Id], [EmployeeStatus] as [Status], [Surname],[OtherNames] as [Other Names],[DateOfBirth] as [Date Of Birth],[Sex],[MaritalStatus] as [Marital Status],[StateOfOrigin] as [State],[LgaOfOrigin] as [LGA],[ResidentialAddress] as [Address],[Nationality],[Religion],[Phone],[Email],[DateOfEmployment] as [Employment Date],[DateOfResumption] as [Resumption Date],[MinistryFaculty] as [Ministry/Faculty],[DepartmentSection] as [Department/Section],[Designation],[GradeLevel] as [Grade Level],[Step],[SalaryStructureGroup] as [Salary Structure],[PayGroupCode] as [Pay Group Code],[EmploymentType] as [Employment Type],[EmploymentMode] as [Employment Mode],[BankName] as [Bank],[BankBranch] as [Bank Branch],[AccountNumber] as [Account Number],[BursaryReferenceNo] as [Reference No],[NextOfKinName] as [Next Of Kin],[NextOfKinAddress] as [Kin Address],[NextOfKinRelationship] as [Kin Relationship],[NextOfKinPhone] as [Kin Phone],[CompanyBranch] as [Company Branch],[confirmationStatus] as [Confirmation Status],[probationPeriod] as [Probation Period] FROM [Employees] where [EmpID] like '%" + TxtSearch + "%' or [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [DateOfBirth] like '%" + TxtSearch + "%' or [Sex] like '%" + TxtSearch + "%' or [MaritalStatus] like '%" + TxtSearch + "%' or [StateOfOrigin] like '%" + TxtSearch + "%' or [LgaOfOrigin] like '%" + TxtSearch + "%' or [ResidentialAddress] like '%" + TxtSearch + "%' or [Nationality] like '%" + TxtSearch + "%' or [Religion] like '%" + TxtSearch + "%' or [Phone] like '%" + TxtSearch + "%' or [Email] like '%" + TxtSearch + "%' or [DateOfEmployment] like '%" + TxtSearch + "%' or [DateOfResumption] like '%" + TxtSearch + "%' or [MinistryFaculty] like '%" + TxtSearch + "%' or [DepartmentSection] like '%" + TxtSearch + "%' or [Designation] like '%" + TxtSearch + "%' or [GradeLevel] like '%" + TxtSearch + "%' or [Step] like '%" + TxtSearch + "%' or [SalaryStructureGroup] like '%" + TxtSearch + "%' or [PayGroupCode] like '%" + TxtSearch + "%' or [EmploymentType] like '%" + TxtSearch + "%' or [EmploymentMode] like '%" + TxtSearch + "%' or [BankName] like '%" + TxtSearch + "%' or [BankBranch] like '%" + TxtSearch + "%' or [AccountNumber] like '%" + TxtSearch + "%' or [BursaryReferenceNo] like '%" + TxtSearch + "%' or [NextOfKinName] like '%" + TxtSearch + "%' or [NextOfKinAddress] like '%" + TxtSearch + "%' or [NextOfKinRelationship] like '%" + TxtSearch + "%' or [NextOfKinPhone] like '%" + TxtSearch + "%' or [EmployeePassportText] like '%" + TxtSearch + "%' or [CompanyBranch] like '%" + TxtSearch + "%' or [confirmationStatus] like '%" + TxtSearch + "%' or [probationPeriod] like '%" + TxtSearch + "%' or [EmployeeStatus] like '%" + TxtSearch + "%'";
            query = "select (select distinct B.[FacultyName] from [CourseOfStudy] B where A.[FacultyID] = B.[FacultyID]) as School, (select distinct B.[DepartmentName] from [CourseOfStudy] B where A.[DepartmentID] = B.[DepartmentID]) as dept, A.[CourseOfStudyName],A.[Programme],A.[Duration],A.[Honours],A.[ModeOfStudy],A.[AcademicLevel],A.[EntryMode] FROM [Students] A where A.[MatricNumber] = '" + StaffId + "'";


            ds = SearchData(query);

            TxtSchool.Text = "";
            TxtDept.Text = "";
            TxtCourseStudy.Text = "";
            TxtLevel.Text = "";
            TxtProg.Text = "";
            TxtDuration.Text = "";
            TxtEntryMode.Text = "";
            PanelRations.Visible = true;
            //A.[Programme],A.[Duration],A.[Honours],A.[ModeOfStudy]
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    TxtSchool.Text = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    TxtDept.Text = ds.Tables[0].Rows[jj][1].ToString().ToUpper();
                    TxtCourseStudy.Text = ds.Tables[0].Rows[jj][2].ToString().ToUpper();
                    TxtProg.Text = ds.Tables[0].Rows[jj][3].ToString().ToUpper();
                    TxtDuration.Text = ds.Tables[0].Rows[jj][4].ToString().ToUpper();
                    TxtHonor.Text = ds.Tables[0].Rows[jj][5].ToString().ToUpper();
                    TxtStudyMode.Text = ds.Tables[0].Rows[jj][6].ToString().ToUpper();
                    TxtLevel.Text = ds.Tables[0].Rows[jj][7].ToString().ToUpper();
                   TxtEntryMode.Text = ds.Tables[0].Rows[jj][8].ToString().ToUpper();
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
    }


    private void LoadStaffNames()
    {
        PanelRations.Visible = false;
        ChkBoxListStaff.Items.Clear();
        try
        {
            //string Qry = "";
            //query = "";

            DataSet ds = new DataSet();
            string TxtSearch = TxtSearchstaff.Text.Trim();

            query = "SELECT [MatricNumber] as [Matric Number],[Surname],[OtherNames] as [Other Names] FROM [Students] where [AdmissionStatus] = 'ADMITTED' and [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [MaidenName] like '%" + TxtSearch + "%' or [MatricNumber] like '%" + TxtSearch + "%' or [Email] like '%" + TxtSearch + "%' or [PhoneNumber] like '%" + TxtSearch + "%' or [AcademicLevel] like '%" + TxtSearch + "%' or [CourseOfStudyName] like '%" + TxtSearch + "%' or [Programme] like '%" + TxtSearch + "%' or [ModeOfStudy] like '%" + TxtSearch + "%'";

            ds = SearchData(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    string Staff = ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][1].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][5].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][6].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    ChkBoxListStaff.Items.Add(Staff);
                    //ListBoxCourses.Items.Add(Itm2);
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
    private void LoadStaffNames(string Dept)
    {
        //PanelRations.Visible = false;
        //ChkBoxListStaff.Items.Clear();
        //try
        //{
        //    //string Qry = "";
        //    //query = "";

        //    DataSet ds = new DataSet();
        //    string TxtSearch = TxtSearchstaff.Text.Trim();

        //    //query = "SELECT [EmpID] as [Staff Id], [EmployeeStatus] as [Status], [Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Ministry/Faculty],[DepartmentSection] as [Department/Section],[Designation],[GradeLevel] as [Grade Level],[Step],[SalaryStructureGroup] as [Salary Structure],[EmploymentType] as [Employment Type] FROM [Employees] where [DepartmentSection] = '" + Dept + "' order by Surname Asc";// 
        //    query = "SELECT [EmpID] as [Staff Id], [EmployeeStatus] as [Status], [Surname],[OtherNames] as [Other Names],[MinistryFaculty] as [Ministry/Faculty],[DepartmentSection] as [Department/Section],[Designation],[GradeLevel] as [Grade Level],[Step],[SalaryStructureGroup] as [Salary Structure],[EmploymentType] as [Employment Type] FROM [Employees] where [EmpID] like '%" + TxtSearch + "%' or [Surname] like '%" + TxtSearch + "%' or [OtherNames] like '%" + TxtSearch + "%' or [DateOfBirth] like '%" + TxtSearch + "%' or [Sex] like '%" + TxtSearch + "%' or [MaritalStatus] like '%" + TxtSearch + "%' or [StateOfOrigin] like '%" + TxtSearch + "%' or [LgaOfOrigin] like '%" + TxtSearch + "%' or [ResidentialAddress] like '%" + TxtSearch + "%' or [Nationality] like '%" + TxtSearch + "%' or [Religion] like '%" + TxtSearch + "%' or [Phone] like '%" + TxtSearch + "%' or [Email] like '%" + TxtSearch + "%' or [DateOfEmployment] like '%" + TxtSearch + "%' or [DateOfResumption] like '%" + TxtSearch + "%' or [MinistryFaculty] like '%" + TxtSearch + "%' or [DepartmentSection] like '%" + TxtSearch + "%' or [Designation] like '%" + TxtSearch + "%' or [GradeLevel] like '%" + TxtSearch + "%' or [Step] like '%" + TxtSearch + "%' or [SalaryStructureGroup] like '%" + TxtSearch + "%' or [PayGroupCode] like '%" + TxtSearch + "%' or [EmploymentType] like '%" + TxtSearch + "%' or [EmploymentMode] like '%" + TxtSearch + "%' or [BankName] like '%" + TxtSearch + "%' or [BankBranch] like '%" + TxtSearch + "%' or [AccountNumber] like '%" + TxtSearch + "%' or [BursaryReferenceNo] like '%" + TxtSearch + "%' or [NextOfKinName] like '%" + TxtSearch + "%' or [NextOfKinAddress] like '%" + TxtSearch + "%' or [NextOfKinRelationship] like '%" + TxtSearch + "%' or [NextOfKinPhone] like '%" + TxtSearch + "%' or [EmployeePassportText] like '%" + TxtSearch + "%' or [CompanyBranch] like '%" + TxtSearch + "%' or [confirmationStatus] like '%" + TxtSearch + "%' or [probationPeriod] like '%" + TxtSearch + "%' or [EmployeeStatus] like '%" + TxtSearch + "%'";


        //    ds = SearchData(query);


        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
        //        {
        //            string Staff = ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][6].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][0].ToString().ToUpper();
        //            ChkBoxListStaff.Items.Add(Staff);
        //            //ListBoxCourses.Items.Add(Itm2);
        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    LogError(msg, "Payroll", "");
        //}
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        populatetreeview();


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
    protected void TxtEventNumber_Click(object sender, EventArgs e)
    {
        //GenerateEventNumb();
    }

    protected void BtnDisselect_Click(object sender, EventArgs e)
    {
        if (ChkBoxListStaff.Items.Count > 0)
        {
            for (int k = 0; k < ChkBoxListStaff.Items.Count; k++)
            {
                ChkBoxListStaff.Items[k].Selected = false;
            }
        }
    }
    protected void BtnSelectAll_Click(object sender, EventArgs e)
    {
        if (ChkBoxListStaff.Items.Count > 0)
        {
            for (int k = 0; k < ChkBoxListStaff.Items.Count; k++)
            {
                ChkBoxListStaff.Items[k].Selected = true;
            }
        }
    }
    protected void BtnAssign02_Click(object sender, EventArgs e)
    {
        try
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            Hashtable SelectedStaff = new Hashtable();
            ArrayList Indexes = new ArrayList();



            //int k = ChkBoxListStaff.Items.Count;
            //if (k > 0)
            //{
            //    for (int kk = 0; kk < k; kk++)
            //    {
            //        if (ChkBoxListStaff.Items[kk].Selected == true)
            //        {
            //            SelectedStaff.Add(ChkBoxListStaff.Items[kk].Text, kk);
            //        }
            //    }
            //}
            //else
            //{
            //    msg = "Select staff to promote";
            //    showmassage(msg);
            //    //log.Info(msg);
            //    return;
            //}

            string item = "";
            item = ChkBoxListStaff.Text;
            if (item != "")
            {
                SelectedStaff.Add(ChkBoxListStaff.Text, 1);
            }
            else
            {
                msg = "Select student";
                showmassage(msg);
                return;
            }


            string Matno = "";
            string Surname = "";
            string OtherNames = "";

            string PresentSession = "";
            int FacultID = 0;
            string FacultyName = "";
            int DepartID = 0;
            string DepartmentName = "";
            string AcademicLevel = "";
            int CourseStudyID = 0;
            string CourseOfStudyName = "";
            string Programme = "";
            string Duration = "";
            string Honours = "";
            string ModeOfStudy = "";

            string FacultyNameOld = "";
            string DepartmentNameOld = "";
            string AcademicLevelOld = "";
            string CourseOfStudyNameOld = "";
            string ProgrammeOld = "";
            string DurationOld = "";
            string HonoursOld = "";
            string ModeOfStudyOld = "";
            string EntryMode = "";
            string EntryModeOld = "";

            int FacultyIDOld = 0;
            int DepartmentIDOld = 0;
            int CourseOfStudyIDOld = 0;

            //string Email = "";
            if (DDListFac2.Text != "")
            {
                DDListFac2.Text = DDListFac2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select the new school name";
                showmassage(msg);

                return;
            }


            if (DDListDept2.Text != "")
            {
                DDListDept2.Text = DDListDept2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select the new departmental name";
                showmassage(msg);
                DDListDept2.Focus();
                //PanelCourse.Visible = true;
                return;
            }

            if (DDListCourseOfStudy.Text != "")
            {


                DDListCourseOfStudy.Text = DDListCourseOfStudy.Text.ToUpper().Replace("'", "''").Replace(";", ":");

            }
            else
            {
                msg = "Select new course of study";
                showmassage(msg);

                return;
            }



            if (DDListLevel2.Text != "")
            {
                DDListLevel2.Text = DDListLevel2.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select the new academic level";
                showmassage(msg);
                DDListLevel2.Focus();
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
            if (TxtHons.Text == "")
            {

                msg = "Select course honor";
                showmassage(msg);
                return;
            }

            if (DDListEntryMode.Text != "")
            {
              EntryMode = DDListEntryMode.Text.ToUpper().Replace("'", "''").Replace(";", ":");
            }
            else
            {
                msg = "Select the new entry mode";
                showmassage(msg);
                return;
            }

            if (TxtDuration2.Text == "")
            {

                msg = "Select course duration";
                showmassage(msg);
                return;
            }


            // DeptCos.DepartmentName = DDListDept2.Text.Trim();
            if (DepartmentID.ContainsKey(DDListDept2.Text.Trim()))
            {
                DepartID = int.Parse(DepartmentID[DDListDept2.Text.Trim()].ToString());
            }
            //DeptCos.FacultyName = DDListFac2.Text.Trim();
            if (FacultyID.ContainsKey(DDListFac2.Text.Trim()))
            {
                FacultID = int.Parse(FacultyID[DDListFac2.Text.Trim()].ToString());
            }

            if (CourseOfStudyID.ContainsKey(DDListCourseOfStudy.Text.ToString()))
            {
                CourseStudyID = int.Parse(CourseOfStudyID[DDListCourseOfStudy.Text.ToString()].ToString());
                //DeptCos.CourseOfStudy = CosOfstudy.Key.ToString();
            }


            int ApproveStatus = 0;
            int DisApprovedStatus = 0;


            string ApprovedBy = ID;
            string ApprovedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //string body = "";
            //

            DataSet ds = null;//
            //ds = new DataSet();
            //


            int cnt = 0;
            foreach (DictionaryEntry Staffs in SelectedStaff)
            {
                string body = "";
                int indx = 0;

                body = Staffs.Key.ToString();
                indx = int.Parse(Staffs.Value.ToString());
                Indexes.Add(indx.ToString());

                string[] item2 = body.Split(new char[] { '|' });
                int upp = item2.GetUpperBound(0);
                if (upp == 2)
                {

                    Matno = item2[0].ToString();
                    Surname = item2[1].ToString().Replace("'", "''");
                    OtherNames = item2[2].ToString().Replace("'", "''");

                    ds = new DataSet();
                    //Get old valus
                    string qry = "SELECT  (select distinct B.[FacultyName] from [CourseOfStudy] B where A.[FacultyID] = B.[FacultyID]) as school, (select distinct B.[DepartmentName] from [CourseOfStudy] B where A.[DepartmentID] = B.[DepartmentID]) as dept,A.[AcademicLevel],A.[CourseOfStudyName],A.[Programme],A.[Duration],A.[Honours],A.[ModeOfStudy],A.[FacultyID],A.[DepartmentID],A.[CourseOfStudyID],A.[EntryMode] FROM [Students] A where A.[MatricNumber] = '" + Matno + "'";
                    ds = SearchData(qry);
                    //ds = GetEmpInfo(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                        {
                            //[FacultyName],[DepartmentName],[AcademicLevel],[CourseOfStudyName],[Programme],
                            //  [Duration],[Honours],[ModeOfStudy],[FacultyID],[DepartmentID],[CourseOfStudyID]
                            FacultyNameOld = ds.Tables[0].Rows[jj][0].ToString().ToUpper().Replace("'", "''");
                            DepartmentNameOld = ds.Tables[0].Rows[jj][1].ToString().ToUpper().Replace("'", "''");
                            AcademicLevelOld = ds.Tables[0].Rows[jj][2].ToString().Replace("'", "''");
                            CourseOfStudyNameOld = ds.Tables[0].Rows[jj][3].ToString().Replace("'", "''");
                            ProgrammeOld = ds.Tables[0].Rows[jj][4].ToString().ToUpper();
                            DurationOld = ds.Tables[0].Rows[jj][5].ToString().ToUpper();
                            HonoursOld = ds.Tables[0].Rows[jj][6].ToString().Replace("'", "''");
                            ModeOfStudyOld = ds.Tables[0].Rows[jj][7].ToString().Replace("'", "''");
                            FacultyIDOld = int.Parse(ds.Tables[0].Rows[jj][8].ToString());
                            DepartmentIDOld = int.Parse(ds.Tables[0].Rows[jj][9].ToString());
                            CourseOfStudyIDOld = int.Parse(ds.Tables[0].Rows[jj][10].ToString());
                            EntryModeOld = ds.Tables[0].Rows[jj][11].ToString().Replace("'", "''");
                        }

                        string Qry = "";
                        // delete if existed before
                        Qry = "Delete from [ChangeOfCourse] where [MatricNumber] ='" + Matno + "' and [ApproveStatus] = 0";//
                        PerformDel(Qry);


                        Qry = "INSERT INTO [ChangeOfCourse]([Surname],[OtherNames],[MatricNumber],[PresentSession],[CreatedDate],[CreatedBy],[ApproveStatus],[DisApprovedStatus],[FacultyID],[FacultyName],[DepartmentID],[DepartmentName],[AcademicLevel],[CourseOfStudyID],[CourseOfStudyName],[Programme],[Duration],[Honours],[ModeOfStudy],[FacultyNameOld],[FacultyIDOld],[DepartmentIDOld],[CourseOfStudyIDOld],[DepartmentNameOld],[AcademicLevelOld],[CourseOfStudyNameOld],[ProgrammeOld],[DurationOld],[HonoursOld],[ModeOfStudyOld],EntryModeOld,EntryMode) VALUES ('" + Surname + "','" + OtherNames + "','" + Matno + "','" + DDListSession.Text.Trim() + "','" + ApprovedDate + "','" + ApprovedBy + "'," + ApproveStatus + "," + DisApprovedStatus + "," + FacultID + ",'" + DDListFac2.Text + "'," + DepartID + ",'" + DDListDept2.Text + "','" + DDListLevel2.Text + "'," + CourseStudyID + ",'" + DDListCourseOfStudy.Text + "','" + DDListProgramme.Text + "','" + TxtDuration2.Text + "','" + TxtHons.Text + "','" + DDListStudyMode.Text.Trim() + "','" + FacultyNameOld + "'," + FacultyIDOld + "," + DepartmentIDOld + "," + CourseOfStudyIDOld + ",'" + DepartmentNameOld + "','" + AcademicLevelOld + "','" + CourseOfStudyNameOld + "','" + ProgrammeOld + "','" + DurationOld + "','" + HonoursOld + "','" + ModeOfStudyOld + "','" + EntryModeOld + "','" + EntryMode + "')";
                        PerformInsert(Qry);

                        //Qry = "Update [Employees] set [EmployeeStatus] = " + StatusCode + ",[PayrollStatus] = " + PayrollEffect + " where EmpID ='" + EmpId + "'";
                        //PerformUpdate(Qry);
                        ID = HttpContext.Current.User.Identity.Name;
                        Group = (string)Cache[HttpContext.Current.User.Identity.Name];
                        auditInfo = new AuditInfo();
                        string msg1 = Qry;
                        auditInfo.Action = "Changed course/Level request for " + Matno;
                        auditInfo.Usergroup = Group;
                        auditInfo.Userid = ID;
                        auditInfo.Msg = msg1;
                        auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        auditInfo.Computer = User.Identity.Name;
                        auditInfo.Hostname = Request.UserHostName;
                        auditInfo.IPAddress = Request.UserHostAddress;
                        sendtoAuditQ(auditInfo);

                        cnt++;
                    }

                }
            }

            ////if (SelectedStaff.Count == k)
            ////{
            ////    ChkBoxListStaff.Items.Clear();
            ////}
            ////else
            ////{
            ////    foreach (string ind in Indexes)
            ////    {

            ////        int indd = int.Parse(ind);
            ////        if (ChkBoxListStaff.Items.Count < indd)
            ////        {
            ////            indd = indd - 1;
            ////        }
            ////        ChkBoxListStaff.Items.RemoveAt(indd);
            ////    }
            ////}
            //
            ChkBoxListStaff.Items.Clear();//.SelectedIndex = -1;
            msg = cnt.ToString() + " " + "Students information changed successfully and await approval";
            showmassage(msg);
            //log.Info(msg);


            //query = "SELECT [EmpId] as [Staff Id],[Surname],[OtherNames] as [Other Names],[StatusName] as[Event Name],[PayrollEffect] as [Payroll Status],[MinistryFaculty] as [Faculty],[DepartmentSection] as [Department],[SalaryStructureGroup] as [Salary Structure],[Designation],[GradeLevel] as [Grade Level],[Step],[ApproveStatus] as [Approval],[StatusCode] as [Event Code], [TransactDate] as [Transaction Date] FROM [EmployeeStatusHisory] where ApproveStatus = 0 order by TransactDate";
            //exportheader = "[Staff Id],[Surname],[Other Names],[Event Name],[Payroll Status],[Faculty],[Department],[Salary Structure],[Designation],[Grade Level],[Step],[Approval],[Event Code],[Transaction Date]";

            //Exportfilename = "Staffstatus";
            //GridCaption = "Staff with changed status";
            //GViewWidth = 1200;
            //TxtPaymetopion.Text = "";
            PanelRations.Visible = false;
            populategrd();
            ////populatetreeview();


        }
        catch (Exception ex)
        {
            msg = ex.Message;
            LogError(msg, "Payroll", "");
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
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void BtnExitpg02_Click(object sender, EventArgs e)
    {

    }
    protected void BtnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList ApprvedOGNO = new ArrayList();

            // Department = "";
            //collect all the ogno
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
                if (chk.Checked)
                {
                    string OGNO = gvr.Cells[1].Text;
                    //Department = gvr.Cells[3].Text;
                    ApprvedOGNO.Add(OGNO);
                    //Grd += (chk.Text + "<br />");
                }
            }



            string query = "";

            string ApprovedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int cnt = 0;
            DataSet ds = null;


            int FacultID = 0;
            int DepartID = 0;
            int CourseStudyID = 0;
            string CourseOfStudyName = "";
            string AcademicLevel = "";
            string Programme = "";
            string Duration = "";
            string Honours = "";
            string ModeOfStudy = "";

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string Approveby = ID;
            auditInfo = new AuditInfo();


            auditInfo.Usergroup = Group;
            auditInfo.Userid = ID;

            auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            auditInfo.Computer = User.Identity.Name;
            auditInfo.Hostname = Request.UserHostName;
            auditInfo.IPAddress = Request.UserHostAddress;

            if (ApprvedOGNO.Count > 0)
            {
                foreach (string Ogno in ApprvedOGNO)
                {
                    FacultID = 0;
                    DepartID = 0;
                    CourseStudyID = 0;
                    CourseOfStudyName = "";
                    Programme = "";
                    Duration = "";
                    Honours = "";
                    ModeOfStudy = "";
                    AcademicLevel = "";
                    string EntryMode = "";

                    ds = new DataSet();

                    string qry = "select [FacultyID],[DepartmentID],[CourseOfStudyID],[CourseOfStudyName],[Programme],[Duration],[Honours],[ModeOfStudy],[AcademicLevel],EntryMode FROM [ChangeOfCourse]  where [MatricNumber] = '" + Ogno + "'";
                    ds = SearchData(qry);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                        {
                            //[Programme],[Duration],[Honours],[ModeOfStudy],[AcademicLevel]
                            FacultID = int.Parse(ds.Tables[0].Rows[jj][0].ToString());
                            DepartID = int.Parse(ds.Tables[0].Rows[jj][1].ToString());
                            CourseStudyID = int.Parse(ds.Tables[0].Rows[jj][2].ToString());
                            CourseOfStudyName = ds.Tables[0].Rows[jj][3].ToString().Replace("'", "''");
                            Programme = ds.Tables[0].Rows[jj][4].ToString().ToUpper();
                            Duration = ds.Tables[0].Rows[jj][5].ToString().ToUpper();
                            Honours = ds.Tables[0].Rows[jj][6].ToString().Replace("'", "''");
                            ModeOfStudy = ds.Tables[0].Rows[jj][7].ToString().Replace("'", "''");
                            AcademicLevel = ds.Tables[0].Rows[jj][8].ToString().Replace("'", "''");
                            EntryMode = ds.Tables[0].Rows[jj][9].ToString().Replace("'", "''");


                        }
                    }
                    //[CreatedDate]  ,[CreatedBy]                   

                    auditInfo.Action = "Changed course/Level Approval for " + Ogno;
                    query = "UPDATE [Students]  SET [FacultyID] =" + FacultID + ", [DepartmentID] = " + DepartID + ", [CourseOfStudyID] = " + CourseStudyID + " , [CourseOfStudyName] = '" + CourseOfStudyName + "', [Programme] = '" + Programme + "',[Duration] = '" + Duration + "',[Honours] = '" + Honours + "',[ModeOfStudy] = '" + ModeOfStudy + "',[AcademicLevel] = '" + AcademicLevel + "',[EntryMode] = '" + EntryMode + "' where [MatricNumber] = '" + Ogno + "'";

                    PerformUpdate(query);
                    auditInfo.Msg = query;
                    sendtoAuditQ(auditInfo);

                    query = "UPDATE [ChangeOfCourse]  SET [ApproveStatus] = 1, [CreatedBy] = '" + ID + "', [CreatedDate] = '" + ApprovedDate + "' where MatricNumber = '" + Ogno + "'";

                    PerformUpdate(query);
                    auditInfo.Msg = query;
                    sendtoAuditQ(auditInfo);

                    cnt++;


                    //auditInfo.Action = "Changed course/Level Approval for " + Ogno;
                    
                }

                msg = cnt.ToString() + " " + "students request have been approved";// +"";
                showmassage(msg);
            }
            else
            {
                msg = "Select what to approve";// +"";
                showmassage(msg);
                return;
            }

            DisplayGrid();

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
            ArrayList ApprvedOGNO = new ArrayList();

            // Department = "";
            //collect all the ogno
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("CheckBoxGIN");
                if (chk.Checked)
                {
                    string OGNO = gvr.Cells[1].Text;
                    //Department = gvr.Cells[3].Text;
                    ApprvedOGNO.Add(OGNO);
                    //Grd += (chk.Text + "<br />");
                }
            }



            string ApprovedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];

            string Approveby = ID;
            auditInfo = new AuditInfo();


            auditInfo.Usergroup = Group;
            auditInfo.Userid = ID;

            auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            auditInfo.Computer = User.Identity.Name;
            auditInfo.Hostname = Request.UserHostName;
            auditInfo.IPAddress = Request.UserHostAddress;

            int cnt = ApprvedOGNO.Count;
            if (cnt > 0)
            {
                foreach (string Ogno in ApprvedOGNO)
                {
                    //[ApproveStatus]      ,[DisApprovedStatus]
                    query = "UPDATE [ChangeOfCourse]  SET [DisApprovedStatus] =1, [CreatedBy] = '" + Approveby + "', [CreatedDate] = '" + ApprovedDate + "' where [MatricNumber] = '" + Ogno + "' and [ApproveStatus]=0 and [DisApprovedStatus]=0";

                    PerformUpdate(query);


                    auditInfo.Action = "Changed course/Level Disapproval for " + Ogno;
                    auditInfo.Msg = query;
                    sendtoAuditQ(auditInfo);
                }

                msg = cnt.ToString() + " " + "students request have been disapproved";// +"";
                showmassage(msg);
            }
            else
            {
                msg = "Select what to disapprove";// +"";
                showmassage(msg);
                return;
            }

            //PerformDisplay();//
            DisplayGrid();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void BtnSearch1_Click(object sender, EventArgs e)
    {
        if (TxtSearchstaff.Text.Trim() != "")
        {
            LoadStaffNames();
        }
        else
        {
            msg = "Enter value to search";
            showmassage(msg);
            return;
        }
    }
    protected void BtnSearch2_Click(object sender, EventArgs e)
    {
        if (TxtSearch1.Text.Trim() != "")
        {

            DisplayGridByDept(TxtSearch1.Text.Trim());
        }
        else
        {
            DisplayGrid();
        }
    }
}
