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
using AuditLogInfo;
using System.IO;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data.OleDb;



public partial class Admin_UploadAdmitted : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];


    private string ReceivedFolder = System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedFolder"];
    private string RawdataUpload = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedUploader"];

    //private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private string msg = "";


    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static string PixName = "NONE";
    private static string EditedPixName = "";
    private static bool EditON = false;
    private static bool varification = false;
    private string userFact = "";

    private static double Max = 0;
    private static double Min = 0;
    private static double TotalRows = 0;
    private static bool processstart = false;

    private static int GViewWidth = 0;

    //private static string PresentDept = "";
    //private static ArrayList AllFaculty = null;
    //private static ArrayList AllDept = null;

    private static string PresentDept = "";
    private static ArrayList AllFaculty = null;
    private static ArrayList AllDept = null;
    private static Hashtable FacultyID = null;
    private static Hashtable DepartmentID = null;
    private static Hashtable CentreCode = new Hashtable();
    private static Hashtable CourseOfStudyID = null;

    private static Hashtable PayitemID = null;


    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];



    private static CWritetoqueue rq = null;
    private static int TotalRec = 0;
    private static string IP = System.Configuration.ConfigurationManager.AppSettings["AdmittedFeedbackIP"];
    private string Group = "";
    private string ID = "";

    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogUI"];
    private AuditLogInfo.AuditInfo auditInfo = null;
    //AudilogUI

    protected void Page_Load(object sender, EventArgs e)
    {
        DDListSession.Enabled = false;
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

            }
        }
        if (!IsPostBack)
        {

            GridView1.DataSource = null;
            GridView1.DataBind();

            LoadProgm();
            loadModeStudy();
            LoadFaculty2();
            LoadLevel2();
            LoadSession();
            loadBatches();
            RadioButtonList1.SelectedIndex = -1;
            LoadEntryMode();
            LoadSemester();
            //userFact=   getuserinfo(ID);

        }
        //LinkButton1.Visible = false;
        //this.BtnUpload.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.LinkButton1, ""));
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
    private string getuserinfo(string ID)
    {
        string userFact1 = "";
        try
        {

            userFact = "";
            //userDept = "";
            //usercosstudy = "";
            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],DepartmentName,[CourseOfStudy] FROM [TUsers] where [userid]='" + ID + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    userFact1 = ds.Tables[0].Rows[jj][0].ToString();
                    //userDept = ds.Tables[0].Rows[jj][1].ToString();
                    //usercosstudy = ds.Tables[0].Rows[jj][2].ToString();
                }

            }

        }
        catch (Exception ex)
        {

        }

        return userFact1;
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

    private void loadBatches()
    {
        DDListBatch.Items.Clear();
        try
        {
            DataSet ds = new DataSet();
            string qry = "SELECT [Batch] FROM [AdmissionBatches] order by [Batch] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {

                    DDListBatch.Items.Add(ds.Tables[0].Rows[jj][0].ToString().ToUpper());
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
    protected void DDListDept2_Changed(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept2.Text.Trim(), DDListFac2.Text.Trim());
    }
    //DDListCourseOfStudy_Changed
    protected void DDListCourseOfStudy_Changed(object sender, EventArgs e)
    {
        LoadHons();
        //DDListCourseOfStudy.Text.Trim(), DDListDept2.Text.Trim(), DDListFac2.Text.Trim()
    }

    private void LoadHons()
    {
        try
        {

            TxtDuration.Text = "";
            TxtHons.Text = "";

            int cosid = 0;

            if (CourseOfStudyID.Count <= 0)
            {
                msg = "refres page, course of study id not available";
                showmassage(msg);
                return;
            }

            if (CourseOfStudyID.ContainsKey(DDListCourseOfStudy.Text.Trim()))
            {
                cosid = int.Parse(CourseOfStudyID[DDListCourseOfStudy.Text.Trim()].ToString());
            }


            DataSet ds = new DataSet();
            string qry = "SELECT [Duration],[Honours]  FROM [CourseOfStudy] where [CourseOfStudyID]= " + cosid + "";

            ds = SearchData(qry);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    TxtDuration.Text = ds.Tables[0].Rows[jj][0].ToString();
                    TxtHons.Text = ds.Tables[0].Rows[jj][1].ToString();
                }

            }
            else
            {
                TxtDuration.Text = "";
                TxtHons.Text = "";
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            showmassage(msg);
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
    protected void DDListFac2_Changed(object sender, EventArgs e)
    {
        loadDept2(DDListFac2.Text.Trim());

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
                // TxtCreditLoad.Text = "";
                LoadHons();
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
    private void populatetreeview()
    {
        try
        {
            //string Qry = "SELECT [EmpID],[Employee],[Amount],[PayItemName] as [PayItem Name],[PayItemType] as [PayItem Type],[TransactDate] as [Transaction Date],[Taxable] FROM [PayrollFixedPayItems] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "' order by [Employee]";

            //exportheader = "[EmpID],[Employee],[Amount],[PayItem Name],[PayItem Type],[Transaction Date],[Taxable]";
            //Exportfilename = DDListPayItemName.Text + "upload";
            //GridCaption = DDListPayItemName.Text + "upload";
            //GViewWidth = 700;
            //populategrdv(Qry);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    private void LogError(string strMsg, string SourceApp, string SourceMethod)
    {
        //log.Error(strMsg);
    }
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

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            LogError(msg, "Payroll", "");
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
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (processstart == true)
        {
            Shoprogress();
        }
    }
    private void Shoprogress()
    {

        try
        {
            ProgressBarFeedback pf = new ProgressBarFeedback();

            //get staff size
            string update = "";
            update = pf.Getfeedback(IP, "payroll", "TotalRows ");

            string[] item1 = update.Split(new char[] { '|' });
            int upp1 = item1.GetUpperBound(0);

            if (upp1 == 0)
            {
                TotalRows = double.Parse(item1[0].ToString());
                Max = TotalRows;
            }

            if (TotalRows > 1)
            {
                Shoprogress2(TotalRows);
            }
        }
        catch (Exception ex)
        {

        }


    }

    private void Shoprogress2(double max)
    {
        try
        {
            string percent = "";
            double current = 0;


            if (max > 1)
            {

                ProgressBarFeedback pf = new ProgressBarFeedback();

                string update = pf.Getfeedback(IP, "payroll", "current");

                string[] item = update.Split(new char[] { '|' });
                int upp = item.GetUpperBound(0);

                if (upp == 0)
                {
                    current = double.Parse(item[0].ToString());

                    if (current >= max)
                    {
                        processstart = false;
                    }

                    double per = (current * 300 / max);
                    percent = per.ToString("0");
                    double step = per / 3;
                    //string percent1 = "";
                    //double per1 = 0;
                    //per1 = per / 8;
                    //percent1 = per1.ToString();

                    if (per > 5.0)
                    {
                        lblProgress.Text = "complete (" + current.ToString() + " of " + max.ToString() + ")";
                        //lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width="+max+"><TR><TD bgcolor=#000066 width=" + percent1.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
                        lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width=300><TR><TD bgcolor=#000066 width=" + step.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
                        lblProgress.Visible = true;
                    }

                }
                else
                {
                    msg = "Returned status has error";
                    showmassage(msg);
                }
                //}
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            showmassage(msg);
        }
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
    protected void RadioButtonList1_Changed(object sender, EventArgs e)
    {
        string txt = RadioButtonList1.Text.Trim();
        if (txt == "Admitted Students")
        {
            DDListBatch.Enabled = true;
            DDListEntryMode.Enabled = true;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = false;

            DDListFac2.Enabled = true;
            DDListDept2.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
        }
        //admitted students(no app form)
        if (txt == "Admitted Students(Migrated)")
        {
            DDListBatch.Enabled = true;
            DDListEntryMode.Enabled = true;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = false;

            DDListFac2.Enabled = true;
            DDListDept2.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
        }
        if (txt == "Old Students(New to Portal)")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = false;

            DDListFac2.Enabled = true;
            DDListDept2.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
            DDListSemester.Enabled = false;
        }
        if (txt == "Old Students(Promoted To New Level)")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = false;

            DDListFac2.Enabled = true;
            DDListDept2.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
        }
        if (txt == "Graduated Students")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = false;
            DDListSemester.Enabled = false;


            DDListFac2.Enabled = true;
            DDListDept2.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
        }
        //
        if (txt == "Matric Numbers")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = false;
            DDListSemester.Enabled = false;


            DDListFac2.Enabled = false;
            DDListDept2.Enabled = false;
            DDListCourseOfStudy.Enabled = false;
        }
        //
        if (txt == "Summer Students")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = false;


            DDListFac2.Enabled = false;
            DDListDept2.Enabled = false;
            DDListCourseOfStudy.Enabled = false;
        }
        //
        if (txt == "Carry Over Courses")
        {
            DDListBatch.Enabled = false;
            DDListEntryMode.Enabled = false;
            DDListLevel2.Enabled = true;
            DDListSemester.Enabled = true;


            DDListFac2.Enabled = false;
            DDListDept2.Enabled = false;
            DDListCourseOfStudy.Enabled = false;
        }

    }
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        populatetreeview();
    }
    protected void Upload_Click(object sender, EventArgs e)
    {
        //LinkButton1_Click(this.BtnUpload, null);
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

        string Localpath1 = ReceivedFolder + "\\";
        string filepath = "";
        // string Taxation = "NO";
        string CurrentFile = "";
        string ext = "";


        ext = Path.GetExtension(fname);
        try
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            if (fname != "")
            {
                string Optype = "";
                String mess = ID;
                //msg = "Select Faculty, Department, Course of study, programme, duration, honour, session";
                //showmassage(mess);

                if (DDListFac2.Text.Trim() == "" || DDListDept2.Text.Trim() == "" || DDListCourseOfStudy.Text.Trim() == "" || DDListSession.Text.Trim() == "" || DDListProgramme.Text.Trim() == "" || TxtHons.Text.Trim() == "" || TxtDuration.Text.Trim() == "" || DDListStudyMode.Text.Trim() == "" || DDListLevel2.Text.Trim() == "" || DDListBatch.Text.Trim() == "" || DDListEntryMode.Text.Trim() == "" || DDListSemester.Text.Trim() == "")
                {
                    msg = "Select Faculty, Department, Course of study, programme, duration, honour, session, start level";
                    showmassage(msg);
                    return;
                }

                userFact = getuserinfo(ID);
                if (Group.ToUpper() == "FACULTY ADMIN")
                {
                    if (DDListFac2.Text.Trim().ToLower() != userFact.ToLower())
                    {
                        msg = "Permission not granted for this operation, contact your web master";
                        showmassage(msg);
                        return;
                    }
                }
                //Group.ToUpper() == "DEPARTMENT ADMIN"

                if (Group.ToUpper() == "DEPARTMENT ADMIN")
                {
                    msg = "Permission not granted for this operation, contact your web master";
                    showmassage(msg);
                    return;

                }
                //if (Group.ToUpper() != "WEB MASTER")
                //{
                //    if (Group.ToUpper() == "FACULTY ADMIN")
                //    {
                //        if (DDListFac2.Text.Trim().ToLower() != userFact.ToLower())
                //        {
                //            msg = "Permission not granted for this operation, contact your web master";
                //            showmassage(msg);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        msg = "Permission not granted for this operation, contact your web master";
                //        showmassage(msg);
                //        return;
                //    }
                //}


                if (RadioButtonList1.SelectedIndex < 0)
                {
                    msg = "Use option box to specify category of your upload";
                    showmassage(msg);
                    return;
                }

                string Faculty = "";
                string Dept = "";
                string CourseOfStudy = "";
                string Session = "";
                string Programme = DDListProgramme.Text.Trim();
                string Honours = TxtHons.Text.Trim();
                string Duration = TxtDuration.Text.Trim();
                string StudyMode = DDListStudyMode.Text.Trim();
                string AdmissionType = "";
                string StartLevel = DDListLevel2.Text.Trim();
                string batch = DDListBatch.Text.Trim();
                string entrymode = DDListEntryMode.Text.Trim();
                string semester = DDListSemester.Text.Trim();


                int DeptId = 0;
                int FactId = 0;
                int CostudyId = 0;
                string purpose = "";
                purpose = RadioButtonList1.Text.ToLower();
                //"AdmittedList";oldstudents

                Faculty = DDListFac2.Text.Trim();
                if (FacultyID.ContainsKey(Faculty))
                {
                    FactId = int.Parse(FacultyID[Faculty].ToString());
                }


                Dept = DDListDept2.Text.Trim();
                if (DepartmentID.ContainsKey(Dept))
                {
                    DeptId = int.Parse(DepartmentID[Dept].ToString());
                }
                CourseOfStudy = DDListCourseOfStudy.Text.Trim();

                if (CourseOfStudyID.ContainsKey(CourseOfStudy))
                {
                    CostudyId = int.Parse(CourseOfStudyID[CourseOfStudy].ToString());
                }
                Session = DDListSession.Text.Trim();

                string Uploader = "";
                Uploader = ID;

                if (!Directory.Exists(Localpath1))
                {
                    Directory.CreateDirectory(Localpath1);
                }

                CurrentFile = Localpath1 + fname;

                AdmissionType = "None";


                switch (ext.ToLower().Trim())
                {
                    case ".csv":
                        ////filepath = Server.MapPath("~/Received/" + rq.Logonpermit.Userid + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName);
                        ////filepath = Server.MapPath("~/Received/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName);
                        //fname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName;
                        ////FileUpload2.SaveAs(filepath);
                        //File.Copy(FileUpload2.PostedFile.FileName, Localpath1 + fname, true);
                        //TreatFileAdmitedCsv(Localpath1, fname, Session, Uploader);
                        break;
                    case ".xls":
                    case ".xlsx":
                        filepath = Server.MapPath("~/Received/" + fname);
                        FileUpload1.SaveAs(filepath);
                        File.Copy(filepath, Localpath1 + fname, true);
                        TreatFileAdmitedXls(Localpath1, fname, Session, Uploader, Faculty, Dept, CourseOfStudy, CostudyId, FactId, DeptId, Programme, Honours, Duration, StudyMode, AdmissionType, StartLevel, ext, batch, entrymode, filepath, purpose, semester);
                        break;

                    case ".txt":
                        ////filepath = Server.MapPath("~/Received/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName);
                        //fname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName;
                        ////FileUpload2.SaveAs(filepath);
                        //File.Copy(FileUpload2.PostedFile.FileName, Localpath1 + fname, true);
                        //TreatFileAdmitedTxt(Localpath1, fname, Session, Uploader);
                        break;
                    case ".mdb":
                        ////filepath = Server.MapPath("~/Received/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName);
                        //fname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName;
                        ////FileUpload2.SaveAs(filepath);
                        //File.Copy(FileUpload2.PostedFile.FileName, Localpath1 + fname, true);
                        //TreatFileMdb(Localpath1, fname, Session, Uploader);
                        break;
                    default:
                        msg = "Invalid File Format: Please load only CSV or TXT files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
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

    private void TreatFileAdmitedXls(string Localpath1, string fname, string Session, string Uploader, string Faculty, string Dept, string CourseOfStudy, int CostudyId, int FactId, int DeptId, string Programme, string Honours, string Duration, string StudyMode, string AdmissionType, string StartLevel, string ext, string batch, string entrymode, string filepath, string purpose, string semester)
    {
        try
        {
            if (CostudyId > 0 && FactId > 0 && DeptId > 0)
            {
               
            }
            else
            {
                msg = "Please, refresh page to upload again";
                showmassage(msg);
                return;
            }
            //if (CostudyId <= 0)
            //{
            //    msg = "Please, refresh page to upload again";
            //    showmassage(msg);
            //    return;
            //}
            string CurrentFile = Localpath1 + fname;

            if (ValidateAdmitedXLS(filepath, purpose))
            {
                UploadDataInfo datInfo = new UploadDataInfo();
                datInfo.CurrentFile = CurrentFile;
                datInfo.Filename = fname;
                datInfo.Session = Session;
                datInfo.FileTpye = ext;
                datInfo.Purpose = purpose;
                datInfo.Uploader = Uploader;
                datInfo.Faculty = Faculty;
                datInfo.Dept = Dept;
                datInfo.CourseOfStudy = CourseOfStudy;
                datInfo.DeptId = DeptId;
                datInfo.FacultyId = FactId;
                datInfo.CourseOfStudyId = CostudyId;
                datInfo.Programme = Programme;
                datInfo.Honours = Honours;
                datInfo.Duration = Duration;
                datInfo.ModeOfStudy = StudyMode;
                datInfo.AdmisssionType = AdmissionType;
                datInfo.StartLevel = StartLevel;
                datInfo.Batch = batch;
                datInfo.EntryMode = entrymode;
                datInfo.Semester = semester;
                //datInfo.OperationType = Optype;
                sendtoUploadloadQ(datInfo);


                ID = HttpContext.Current.User.Identity.Name;
                Group = (string)Cache[HttpContext.Current.User.Identity.Name];
                auditInfo = new AuditInfo();
                string msg1 = purpose.ToUpper() + ";" + "dept, level, course of study, session " + Dept + ";" + StartLevel + ";" + CourseOfStudy + ";" + Session;
                auditInfo.Action = "Admission Upload" + CurrentFile;
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;
                auditInfo.Msg = msg1;
                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;
                sendtoAuditQ(auditInfo);


                //msg = "Upload is successful, click refresh button for update";
                //showmassage(msg); 
            }
            else
            {
                //msg = "Upload is not successful";
                //showmassage(msg);

                foreach (string s in System.IO.Directory.GetFiles(Localpath1))
                {
                    if (s.EndsWith(fname))
                    {
                        System.IO.File.Delete(s);
                    }
                }
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
    private bool ValidateAdmitedXLS(string CurrentFile, string purpose)
    {
        bool succ = false;

        try
        {

            string Currsheet = "";
            int cnt = 0;
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file or cant open sheet";
                showmassage(msg);
                DisposeSheet();
                return succ;
            }
            else
            {

                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";


                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();


                OleDbDataAdapter dat = null;

                DataSet ds = new DataSet();

                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);

                DataTable item = ds.Tables[0];
                string colName1 = "";

                int valicolcnt = 0;
                //int cnt = 0;

                if (item.Rows.Count > 0)
                {
                    foreach (DataColumn col in item.Columns)
                    {
                        colName1 = col.ColumnName;
                        valicolcnt++;
                    }
                    //if (valicolcnt != 3 && purpose.ToLower() == "admitted students")
                    if (valicolcnt < 3 && purpose.ToLower() == "admitted students")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached admitted list template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    //Admitted Students(No App Form)
                    if (valicolcnt < 16 && purpose.ToLower() == "admitted students(migrated)")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached admitted list without application form template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    if (valicolcnt < 8 && purpose.ToLower() == "old students(new to portal)")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached  Old Students(New to portal) Template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }

                    if (valicolcnt < 3 && purpose.ToLower() == "old students(promoted to new level)")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached  Old Students(Promoted to new Level) Template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    //Graduated Students
                    if (valicolcnt < 3 && purpose.ToLower() == "graduated students")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached Graduated Students Template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();
                        return succ;
                    }
                    //Matric Numbers
                    if (valicolcnt < 3 && purpose.ToLower() == "matric numbers")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached Matric Number Template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    //
                    if (valicolcnt < 4 && purpose.ToLower() == "summer students")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached Summer Scholarship Students Template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    //Carry Over Courses
                    if (valicolcnt < 4 && purpose.ToLower() == "carry over courses")
                    {
                        msg = "Invalid excel sheet format, kindly download the attached Summer Scholarship Students Template.xls as sample format";
                        showmassage(msg);

                        objConn.Dispose();
                        objConn.Close();
                        DisposeSheet();

                        return succ;
                    }
                    //else
                    //{
                    foreach (DataRow row in item.Rows)
                    {
                        string regno = "";
                        if (row[0].ToString().Trim() != "")
                        {
                            cnt++;
                            regno = row[0].ToString();
                        }
                        else
                        {
                            break;
                        }

                    }

                    if (cnt > 0)
                    {

                        succ = true;

                        msg = "Upload successful, total number of data in this excel sheet is: " + " " + cnt.ToString();
                        showmassage(msg);
                        DisposeSheet();
                    }

                    //}
                }
                else
                {
                    msg = "You can not submit empty excel sheet";
                    showmassage(msg);
                    DisposeSheet();

                    return succ;
                }



                // Get the data table containg the schema guid.
                //dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                //columnsTable = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);

                //DataRow[] rows = columnsTable.Select(string.Format("[TABLE_NAME] LIKE '%{0}%'", Currsheet.Replace("‘", "")));




                //if (dt == null)
                //{
                //    return succ;
                //}



                ////if (valicolcnt != 3)
                ////{
                ////    msg = "Invalid excel sheet format, kindly download the attached .xls as sample format";
                ////    showmassage(msg);
                ////    DisposeSheet();

                ////    oDr1.Dispose();
                ////    oCmd1.Dispose();

                ////    return succ;
                ////}
                ////else
                ////{
                ////    succ = true;

                ////}
                //oDr1.Dispose();
                //oCmd1.Dispose();
                dat.Dispose();
                objConn.Dispose();
                objConn.Close();
                //String colName = dtBooks.Columns[3].ColumnName;

                ////string rws = "";
                ////string Names = "";
                ////string indigen = "";
                ////while (oDr1.Read())
                ////{

                ////    if (oDr1.IsDBNull(0))
                ////    {
                ////        break;
                ////    }
                ////    else
                ////    {
                ////        cnt++;
                ////        //rws = oDr1.GetValue(0).ToString();
                ////        //Names = oDr1.GetValue(1).ToString();
                ////        //indigen = oDr1.GetValue(2).ToString();
                ////    }

                ////}

                ////oDr1.Dispose();
                ////oCmd1.Dispose();
                ////objConn.Dispose();
                ////objConn.Close();

                ////if (cnt > 0)
                ////{

                ////    succ = true;
                ////}

                //msg = "Upload successful, total number of data in this excel sheet is: " + " " + cnt.ToString();
                //showmassage(msg);
                //DisposeSheet();
            }



        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "School Portal", "");
        }
        return succ;
    }
    private void sendtoUploadloadQ(UploadDataInfo datInfo)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(RawdataUpload))
            {
                mq = MessageQueue.Create(RawdataUpload);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(RawdataUpload);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(UploadDataInfo) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + datInfo.EntryMode + ";" + datInfo.Session;
            mq.Send(datInfo);
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
    //private void TreatFileXls(string Localpath1, string fname, string Payitem, string Payitemtype, string Taxation, string PayitemtCode, string FileTpye)
    //{
    //    string uploadby = this.User.Identity.Name;

    //    try
    //    {
    //        string CurrentFile = Localpath1 + fname;

    //        if (ValidateXLS(CurrentFile))
    //        {
    //            UploadPayItems datInfo = new UploadPayItems();
    //            datInfo.CurrentFile = CurrentFile;
    //            datInfo.Filename = fname;
    //            datInfo.Payitem = Payitem;
    //            datInfo.Payitemtype = Payitemtype;
    //            datInfo.PayitemtCode = PayitemtCode;
    //            datInfo.Taxation = Taxation;
    //            datInfo.Uploader = uploadby;
    //            datInfo.FileTpye = FileTpye;

    //            sendtoUploadloadQ(datInfo);


    //            ////rq.Logonpermit.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


    //            msg = "Upload is of " + " " + TotalRec.ToString() + " " + "records is in progress, click refresh grid button to see the feedback";
    //            showmassage(msg);

    //            msg = fname + " " + "uploaded for" + " " + Payitem + " " + "by " + " " + uploadby;
    //            //log.Info(msg);
    //            //CheckBoxTaxation.Checked = false;

    //            processstart = true;
    //        }
    //        else
    //        {
    //            msg = "Upload is not successful, check the file format and make sure only one sheet on the excel sheet";
    //            showmassage(msg);
    //            //delete file from received folder
    //            foreach (string s in System.IO.Directory.GetFiles(Localpath1))
    //            {
    //                if (s.EndsWith(fname))
    //                {
    //                    System.IO.File.Delete(s);
    //                }

    //            }
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        //log.Error(msg);
    //    }
    //}
    private void sendtoUploadloadQ(UploadPayItems datInfo)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(RawdataUpload))
            {
                mq = MessageQueue.Create(RawdataUpload);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(RawdataUpload);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(UploadPayItems) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + datInfo.Payitem + ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            mq.Send(datInfo);
            mq.Dispose();
            mq.Close();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //log.Error(msg);
        }
    }
    //private bool ValidateXLS(string CurrentFile)
    //{
    //    bool succ = false;
    //    try
    //    {

    //        ////xl = new ApplicationClass();

    //        ////wb = xl.Workbooks.Open(@CurrentFile, 0, true, 5, "", "", true, XlPlatform.xlWindows, "",
    //        ////false, false, 0, true, true, false);

    //        ////exsht = wb.Worksheets;

    //        string Currsheet = "";

    //        CSheetname sn = new CSheetname();
    //        Currsheet = sn.name(@CurrentFile);
    //        if (Currsheet == "")
    //        {
    //            msg = "Sheet1 empty in your excel file";
    //            showmassage(msg);
    //            DisposeSheet();
    //            return succ;
    //        }
    //        wks = (Worksheet)exsht.get_Item(Currsheet);

    //        exrange = (Range)wks.get_Range("A1", Type.Missing);
    //        exrange = exrange.get_End(XlDirection.xlToRight);
    //        exrange = exrange.get_End(XlDirection.xlDown);
    //        String downAddress = exrange.get_Address(false, false, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);

    //        exrange = wks.get_Range("A1", downAddress);
    //        if (exrange == null)
    //        {
    //            msg = "Empty Table";
    //            showmassage(msg);
    //            DisposeSheet();
    //            return succ;
    //        }

    //        object[,] values = (object[,])exrange.Value2;
    //        int Rows = values.GetLength(0);//.ToString();// rows,j
    //        int Cols = values.GetLength(1);//.ToString();//col,l


    //        //int exptdcol = 8;
    //        int colMax = 3;
    //        if (Cols != colMax)
    //        {
    //            msg = "Number of columns in the sheet is not correct";
    //            showmassage(msg);
    //            DisposeSheet();
    //            return succ;
    //        }

    //        //confirm if the number of cols in the excel sheet is correct
    //        for (int c = 1; c <= colMax; c++)
    //        {
    //            if (values[1, c].ToString().ToUpper() == "")
    //            {
    //                msg = "Number of columns in the excel sheet is not correct, make sure column headers are not empty";
    //                showmassage(msg);
    //                DisposeSheet();
    //                return succ;
    //            }

    //        }

    //        int cnt = 0;

    //        ////Check if file is epty or not
    //        //count the number of rows
    //        for (int m = 2; m <= Rows; m++)
    //        {

    //            cell = null;
    //            cell = (Range)wks.Cells[m, 1];
    //            if (null == cell.Value2)
    //            {
    //                break;
    //            }
    //            cnt++;
    //        }

    //        if (cnt > 0)
    //        {

    //            succ = true;
    //            TotalRec = cnt;
    //        }
    //        //msg = "Total number of data in this excel sheet is: " + " " + cnt.ToString();
    //        //showmassage(msg);
    //        DisposeSheet();


    //    }
    //    catch (Exception ex)
    //    {
    //        msg = ex.Message + "||" + ex.StackTrace.ToString();
    //        showmassage(msg);
    //        //log.Error(msg);
    //    }
    //    return succ;
    //}

    private void DisposeSheet()
    {
        try
        {
            //wb.Close(null, null, null);
            //xl.Workbooks.Close();
            //xl.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(exrange);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(xl);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(exsht);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);

            //exsht = null;
            //wb = null;
            //exrange = null;
            //xl = null;
        }
        catch (Exception ex)
        {

        }
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

    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (DDListPayItemType.Text.Trim() == "")
        //    {
        //        msg = "Select payitem type";
        //        showmassage(msg);
        //        return;
        //    }
        //    if (DDListPayItemName.Text.Trim() == "")
        //    {
        //        msg = "Select payitem";
        //        showmassage(msg);
        //        return;
        //    }

        //    string Qry = "";
        //    Qry = "Delete FROM [PayrollFixedPayItems] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "'";
        //    PerformDelete(Qry);

        //    Qry = "Delete FROM [PayItemsMap] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "'";
        //    PerformDelete(Qry);

        //    msg = "Delete successful";
        //    showmassage(msg);
        //    // LoadPayItems();
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    //log.Error(msg);
        //}
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
            LogError(msg, "Payroll", "");
            showmassage(msg);
        }
    }
    protected void BtnDeleteItem_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (DDListPayItemType.Text.Trim() == "")
        //    {
        //        msg = "Select payitem type";
        //        showmassage(msg);
        //        return;
        //    }
        //    if (DDListPayItemName.Text.Trim() == "")
        //    {
        //        msg = "Select payitem";
        //        showmassage(msg);
        //        return;
        //    }

        //    string Qry = "";
        //    Qry = "Delete FROM [PayrollFixedPayItems] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "'";
        //    PerformDelete(Qry);

        //    Qry = "Delete FROM [PayItemsMap] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "'";
        //    PerformDelete(Qry);

        //    Qry = "Delete FROM [PayItems] where [PayItemName]='" + DDListPayItemName.Text.Trim() + "' and [PayItemType]='" + DDListPayItemType.Text.Trim() + "'";
        //    PerformDelete(Qry);

        //    msg = "Delete successful";
        //    showmassage(msg);
        //    LoadPayItems();
        //    // LoadPayItems();
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace.ToString();
        //    showmassage(msg);
        //    //log.Error(msg);
        //}
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept2.Text.Trim(), DDListFac2.Text.Trim());
    }
}
