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

public partial class Reports : System.Web.UI.Page
{

    public static string PtCode = "";
    private static string str = ConfigurationManager.AppSettings["conn"];

    private static string msg = "";
    private static string query = "";
    private static Hashtable sess = null;
    private static string Group = "";
    private static string ID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        PanelNames.Visible = false;
        ChkBoxListStaff.Width = 5000;
        LabelSdate.Visible = true;
        LabelEdate.Visible = true;
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

        if (!Page.IsPostBack)
        {

            
            //DDListReportType.Items.Add("Applicants");
            DDListReportType.Items.Add("Admitted Students");
            //DDListReportType.Items.Add("School fees Ref Summary");
            DDListReportType.Items.Add("Registered Students");//Date will specify late reg
            DDListReportType.Items.Add("Exam Cards");
            

            LoadSession();
            loadModeStudy();
            LoadProgm();
            LoadLevels();

        }

    }
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
    private void LoadSession()
    {
        try
        {
            sess = new Hashtable();
            DDListSession.Items.Clear();

            DataSet ds = new DataSet();

            string Qry = "SELECT [SessionName] FROM [Session] order by [ActiveStatus] desc";
            ds = SearchData(Qry);
            int cnt = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    DDListSession.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    if (!sess.ContainsKey(ds.Tables[0].Rows[jj][0].ToString()))
                    {
                        sess.Add(ds.Tables[0].Rows[jj][0].ToString(), cnt.ToString());
                        cnt++;
                    }
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
    private string getmonthName(int mnt)
    {

        switch (mnt)
        {
            case 1: return ("January");
            case 2: return ("February");
            case 3: return ("March");
            case 4: return ("April");
            case 5: return ("May");
            case 6: return ("June");
            case 7: return ("July");
            case 8: return ("August");
            case 9: return ("September");
            case 10: return ("October");
            case 11: return ("November");
            case 12: return ("December");
            default: return ("Illegal month");
        }


    }
    private void LoadYrs()
    {
        DataSet ds = new DataSet();

        try
        {
            DDListSession.Items.Clear();
            string Qry = "SELECT [Year] FROM [Years]";
            ds = SearchData(Qry);
            string item = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    item = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    DDListSession.Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "School Portal", "");
            //showmassage(msg);
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
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime SDate;
            if (TxtStartdate.Text != "")
            {
                if (DateTime.TryParse(TxtStartdate.Text, out SDate))
                {

                }
                else
                {
                    TxtStartdate.Text = "";
                }
            }

            DateTime EDate;
            if (TxtEnddate.Text != "")
            {
                if (DateTime.TryParse(TxtEnddate.Text, out EDate))
                {

                }
                else
                {
                    TxtEnddate.Text = "";
                }
            }
        }
        catch (Exception ex)
        {

        }


        string body = ChkBoxListStaff.Text;
        string id = "";
        string[] item = body.Split(new char[] { '|' });
        int upp = item.GetUpperBound(0);
        if (upp > 0)
        {
            id = item[1].ToString();
        }
        ////DDListReportType.Items.Add("Applicants");
        ////DDListReportType.Items.Add("Admitted Students");
        ////DDListReportType.Items.Add("School fees Ref Summary");
        ////DDListReportType.Items.Add("Student Registration");//Date will specify late reg
        ////DDListReportType.Items.Add("Exam Cards");       

        if (DDListReportType.Text.Trim() == "Applicants" || DDListReportType.Text.Trim() == "Admitted Students" || DDListReportType.Text.Trim() == "School fees Pin Summary" || DDListReportType.Text.Trim() == "Registered Students" || DDListReportType.Text.Trim() == "Exam Cards")
        {
            //if (Group.ToLower() != "web master" && Group.ToLower() != "bursary admin")
            //{
            //    msg = "You have no right to view this report";
            //    showmassage(msg);
            //    return;
            //}
        }

        if (DDListReportType.Text.Trim().ToUpper() == "EXAM CARDS")
        {
            if (TxtCourseCode.Text.Trim() == "")
            {
                msg = "Enter course code";
                showmassage(msg);
                return;
            }
        }

        ////Session["CourseCode"] = TxtCourseCode.Text.Trim().ToUpper();
        ////Session["Staff"] = id;
        ////Session["RptType"] = DDListReportType.Text.Trim();
        ////Session["Yearr"] = DDListSession.Text.Trim();

        ////Session["Date1"] = TxtStartdate.Text.Trim();
        ////Session["Date2"] = TxtEnddate.Text.Trim();

        ////Session["Programme"] = DDListProgramme.Text.Trim();
        ////Session["StudyMode"] = DDListStudyMode.Text.Trim();
        ////Session["Level"] = DDListLevels.Text.Trim();

        string CourseCode = TxtCourseCode.Text.Trim().ToUpper();
        string StudID = id;
        string RptType = DDListReportType.Text.Trim();
        string Yearr = DDListSession.Text.Trim();

        string Date1 = TxtStartdate.Text.Trim();
        string Date2 = TxtEnddate.Text.Trim();

        string Programme = DDListProgramme.Text.Trim();
        string StudyMode = DDListStudyMode.Text.Trim();
        string Level= DDListLevels.Text.Trim();


        //Response.Redirect("~/Admin/ReportShow.aspx");

       // showwindow("JobEdit.aspx?JobId=" + JobId + "&UserId=" + UserID);
        showwindow("ReportShow.aspx?CourseCode=" + CourseCode + "&StudID=" + StudID + "&RptType=" + RptType + "&Yearr=" + Yearr + "&Date1=" + Date1 + "&Date2=" + Date2 + "&Programme=" + Programme + "&StudyMode=" + StudyMode + "&Level=" + Level);

       // showwindow("ReportShow.aspx?CourseCode=" + CourseCode + "&StudID=" + StudID + "&RptType=" + RptType + "&Yearr=" + Yearr + "&Date1=" + Date1 + "&Date2=" + Date2 + "&Programme=" + Programme + "&StudyMode=" + StudyMode + "&Level=" + Level);        
        
        
        //Server.Transfer("ReportShow.aspx");

    }
    private void showwindow(string window)
    {
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }
    protected void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Individual Id Card
        if (DDListReportType.Text.Trim() == "Individual Id Card")
        {
            PanelNames.Visible = true;
            LoadNames();
        }
        else
        {
            ChkBoxListStaff.Items.Clear();
            PanelNames.Visible = false;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        ChkBoxListStaff.Items.Clear();
        try
        {
            DataSet ds = new DataSet();
            string TxtSearch = TxtSearchstaff.Text.Trim();
            query = "select ([Surname] + ' '+ [OtherNames]) as [Names],[RegNo],[CourseOfStudyName] as [Course Of Study] FROM [Students] where [Surname]  like '%" + TxtSearch + "%' or [OtherNames]  like '%" + TxtSearch + "%' or [MaritalStatus]  like '%" + TxtSearch + "%' or [Nationality]  like '%" + TxtSearch + "%' or [State]  like '%" + TxtSearch + "%' or [LocalGovernmentArea]  like '%" + TxtSearch + "%' or [MatricNumber]  like '%" + TxtSearch + "%' or [Country]  like '%" + TxtSearch + "%' or [Email]  like '%" + TxtSearch + "%' or [PhoneNumber]  like '%" + TxtSearch + "%' or [AcademicLevel]  like '%" + TxtSearch + "%' or [RegNo]  like '%" + TxtSearch + "%' or [CourseOfStudyName] like '%" + TxtSearch + "%' or [AdmittedSession] like '%" + TxtSearch + "%' or [Programme] like '%" + TxtSearch + "%' or [Duration] like '%" + TxtSearch + "%' or [Honours] like '%" + TxtSearch + "%' or [ModeOfStudy] like '%" + TxtSearch + "%' or [PresentSession] like '%" + TxtSearch + "%' or [Title] like '%" + TxtSearch + "%'";

            // query = "select [Surname] + ' '+ [OtherNames]) as [Names],[RegNo],[CourseOfStudyName] as [Course Of Study] FROM [Students] where Status = 1";

            ds = SearchData(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    // [StaffName],[StaffId],[Department],[Designation] 
                    string Staff = ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][1].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper();
                    ChkBoxListStaff.Items.Add(Staff);
                }
                ChkBoxListStaff.Visible = true;
                //LoadGroups();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);

        }
    }
    private void LoadNames()
    {
        ChkBoxListStaff.Items.Clear();
        try
        {

            DataSet ds = new DataSet();

            //query = "select [Name],[BorrowerID],[Faculty],[Department] FROM [Borrowers] where Status = 1";
            query = "select ([Surname] + ' '+ [OtherNames]) as [Names],[RegNo],[CourseOfStudyName] as [Course Of Study] FROM [Students]";

            ds = SearchData(query);


            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    string Staff = ds.Tables[0].Rows[jj][0].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][1].ToString().ToUpper() + "|" + ds.Tables[0].Rows[jj][2].ToString().ToUpper();// +"|" + ds.Tables[0].Rows[jj][3].ToString().ToUpper();
                    ChkBoxListStaff.Items.Add(Staff);
                }
                ChkBoxListStaff.Visible = true;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }
    }
}
