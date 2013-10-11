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
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data.OleDb;

public partial class Admin_UploadPins : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    private string ReceivedFolder = System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedFolder"];
    private string RawdataUpload = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedUploader"];
    private static string Key = ConfigurationManager.AppSettings["CyberKey"];
    //private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string msg = "";

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static string PixName = "NONE";
    private static string EditedPixName = "";
    private static bool EditON = false;
    private static bool varification = false;

    private static double Max = 0;
    private static double Min = 0;
    private static double TotalRows = 0;
    private static bool processstart = false;

    private static int GViewWidth = 0;

    
    private static Hashtable FacultyID = null;
    private static Hashtable DepartmentID = null;
    private static Hashtable CentreCode = new Hashtable();
    private static Hashtable CourseOfStudyID = null;

    private static Hashtable PayitemID = null;    

    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];
    private static CWritetoqueue rq = null;
    private static int TotalRec = 0;
    private static string IP = System.Configuration.ConfigurationManager.AppSettings["AdmittedFeedbackIP"];
    private static string Group = "";
    private static string ID = "";
    //private static int  TotalRows = 0;
    private static int CurrentCount = 0;
    //DataTable dt = null;
    //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        DDListSession.Enabled = false;

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

            LoadSession();
            LoadPayType();
            //loadBatches();
            RadioButtonList1.SelectedIndex = -1;
            //LoadEntryMode();
            //LoadSemester();

        }
        //LinkButton1.Visible = false;
        //this.BtnUpload.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.LinkButton1, ""));
    }

    private void LoadPayType()
    {
        DDListPaymentType.Items.Clear();
        DDListPaymentType.Items.Add("Full Payment");
        DDListPaymentType.Items.Add("First Installment");
        DDListPaymentType.Items.Add("Second Installment");
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

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            // LogError(msg, "School Portal", "");
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

                current = double.Parse(CurrentCount.ToString());

                if (current >= max)
                {
                    processstart = false;
                }

                double per = (current * 300 / max);
                percent = per.ToString("0");
                double step = per / 3;


                if (per > 5.0)
                {
                    lblProgress.Text = "complete (" + current.ToString() + " of " + max.ToString() + ")";
                    //lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width="+max+"><TR><TD bgcolor=#000066 width=" + percent1.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
                    lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width=300><TR><TD bgcolor=#000066 width=" + step.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
                    lblProgress.Visible = true;
                }
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

        //<asp:ListItem>Application Pins</asp:ListItem>
        //                            <asp:ListItem>Acceptance Pins</asp:ListItem>
        //                            <asp:ListItem>School Fees Pins</asp:ListItem>
        //                            <asp:ListItem>Summer Pins</asp:ListItem>   
        if (txt == "Application Pins")
        {
            DDListFac2.Enabled = false;
            DDListProgramme.Enabled = true;
            DDListPaymentType.Enabled = false;
            TxtAmt.Text = "";
            TxtAmt.Text = "5500";

        }
        if (txt == "Acceptance Pins")
        {
            DDListFac2.Enabled = false;
            DDListProgramme.Enabled = false;
            DDListPaymentType.Enabled = false;
            TxtAmt.Text = "";
            TxtAmt.Text = "20000";
        }
        if (txt == "School Fees Pins")
        {
            DDListFac2.Enabled = true;
            DDListProgramme.Enabled = true;
            DDListPaymentType.Enabled = true;

        }
        if (txt == "Summer Pins")
        {
            DDListFac2.Enabled = true;
            DDListProgramme.Enabled = true;
            DDListPaymentType.Enabled = false;
        }
        //


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

        if (Group.ToUpper() != "WEB MASTER")
        {
            msg = "Permission not granted for this operation, contact your web master";
            showmassage(msg);
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
            if (fname != "")
            {
                string Optype = "";
                String mess = ID;
                //msg = "Select Faculty, Department, Course of study, programme, duration, honour, session";
                //showmassage(mess);

                if (DDListFac2.Text.Trim() == "" || DDListSession.Text.Trim() == "" || DDListProgramme.Text.Trim() == "" || TxtAmt.Text.Trim() == "" || DDListStudyMode.Text.Trim() == "" || DDListPaymentType.Text.Trim() == "")
                {
                    msg = "Select Faculty, programme, session, payment type";
                    showmassage(msg);
                    return;
                }

                if (RadioButtonList1.SelectedIndex < 0)
                {
                    msg = "Use option box to specify category of your upload";
                    showmassage(msg);
                    return;
                }

                double cash = 0;
                if (double.TryParse(TxtAmt.Text.Trim(), out cash))
                {

                }
                else
                {
                    msg = "Enter Amount";
                    showmassage(msg);
                    return;
                }

                string Faculty = "";                
                string Session = "";
                string Programme = DDListProgramme.Text.Trim();

                string StudyMode = DDListStudyMode.Text.Trim();
                string PaymentType = "";

                PaymentType = DDListPaymentType.Text.Trim().ToUpper();



                
                int FactId = 0;
                
                string purpose = "";
                purpose = RadioButtonList1.Text.Trim();
                //"AdmittedList";oldstudents

                Faculty = DDListFac2.Text.Trim();
                if (FacultyID.ContainsKey(Faculty))
                {
                    FactId = int.Parse(FacultyID[Faculty].ToString());
                }


                Session = DDListSession.Text.Trim();

                string Uploader = "";
                Uploader = ID;



                if (!Directory.Exists(Localpath1))
                {
                    Directory.CreateDirectory(Localpath1);
                }

                CurrentFile = Localpath1 + fname;

                switch (ext.ToLower().Trim())
                {
                    case ".csv":

                        break;
                    case ".xls":
                    case ".xlsx":


                        filepath = Server.MapPath("~/Received/" + fname);
                        FileUpload1.SaveAs(filepath);
                        File.Copy(filepath, Localpath1 + fname, true);

                        TreatFileAdmitedXls(filepath, fname, Session, Uploader, Faculty, Programme, StudyMode, PaymentType, ext, purpose, cash);
                        break;

                    case ".txt":

                        break;
                    case ".mdb":

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


    private void TreatFileAdmitedXls(string filepath, string fname, string Session, string Uploader, string Faculty, string Programme, string StudyMode, string PaymentType, string ext, string purpose, double cash)
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


                int rc = 0;

                string pinserial = "";
                string pin = "";
                string encrypPin = "";

                TotalRows = dt.Rows.Count;
                //1
                if (purpose == "Application Pins")
                {
                    while (oDr1.Read())
                    {
                        processstart = true;

                        if (oDr1.IsDBNull(0))
                        {
                            break;
                        }
                        else
                        {
                            pinserial = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            break;
                        }
                        else
                        {
                            pin = oDr1.GetValue(1).ToString();
                        }

                        byte[] buffer = CyberEncryptor.encypt(pin, Key);


                        encrypPin = Convert.ToBase64String(buffer);


                        string qry = "";

                        qry = "SELECT * FROM [ApplicationFeesPin] where [PinNumber] = '" + encrypPin + "'";

                        if (!Existed(qry))
                        {
                            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            //string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string semester, string Level
                            qry = "INSERT INTO [ApplicationFeesPin] ([PinNumber],[PinSerialNumber],[Amount],[PinStatus],[SessionName],[Programme],[ModeOfStudy],[UsedBy],[UsedDate]) VALUES ('" + encrypPin + "','" + pinserial + "'," + cash + ",0,'" + Session + "','" + Programme + "','" + StudyMode + "','NONE','" + CreatedDate + "')";
                            PerformInsert(qry);

                            cnt++;
                            CurrentCount = cnt;
                        }

                        // }
                        rc++;
                    }


                }
                //<asp:ListItem>Acceptance Pins</asp:ListItem>
        //                            <asp:ListItem>School Fees Pins</asp:ListItem>
        //                            <asp:ListItem>Summer Pins</asp:ListItem>  
                //2
                if (purpose == "Acceptance Pins")
                {
                    while (oDr1.Read())
                    {
                        processstart = true;

                        if (oDr1.IsDBNull(0))
                        {
                            break;
                        }
                        else
                        {
                            pinserial = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            break;
                        }
                        else
                        {
                            pin = oDr1.GetValue(1).ToString();
                        }

                        byte[] buffer = CyberEncryptor.encypt(pin, Key);


                        encrypPin = Convert.ToBase64String(buffer);


                        string qry = "";

                        qry = "SELECT * FROM [AdmissionLetterFeePin] where [PinNumber] = '" + encrypPin + "'";

                        if (!Existed(qry))
                        {
                            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            //string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string semester, string Level
                            qry = "INSERT INTO [AdmissionLetterFeePin] ([PinNumber],[PinSerialNumber],[Amount],[PinStatus],[SessionName],[UsedBy],[DateUsed] ,[ModeOfStudy]) VALUES ('" + encrypPin + "','" + pinserial + "'," + cash + ",0,'" + Session + "','NONE','" + CreatedDate + "','" + StudyMode + "')";
                            PerformInsert(qry);

                            cnt++;
                            CurrentCount = cnt;
                        }

                        // }
                        rc++;
                    }


                }
                //
                //3
                if (purpose == "School Fees Pins")
                {
                    while (oDr1.Read())
                    {
                        processstart = true;

                        if (oDr1.IsDBNull(0))
                        {
                            break;
                        }
                        else
                        {
                            pinserial = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            break;
                        }
                        else
                        {
                            pin = oDr1.GetValue(1).ToString();
                        }

                        byte[] buffer = CyberEncryptor.encypt(pin, Key);


                        encrypPin = Convert.ToBase64String(buffer);


                        string qry = "";

                        qry = "SELECT * FROM [SchoolFeesPin] where [PinNumber] = '" + encrypPin + "'";

                        if (!Existed(qry))
                        {
                            string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            //string fname, string filepath, string Faculty, string Dept, int FacultId, int DeptID, string semester, string Level
                            qry = "INSERT INTO [SchoolFeesPin] ([PinNumber],[PinSerialNumber],[Amount],[PinStatus],[SessionName],[UsedBy],[UsedDate],[ModeOfStudy],[Programme],[AcademicLevel],[Faculty],[PaymentType],[DateUploaded],[UploadBy],[IsIndigene]) VALUES ('" + encrypPin + "','" + pinserial + "'," + cash + ",0,'" + Session + "','NONE','" + CreatedDate + "','" + StudyMode + "','" + Programme + "','All Levels','" + Faculty + "','" + PaymentType + "','" + CreatedDate + "','" + ID + "','No')";
                            PerformInsert(qry);

                            cnt++;
                            CurrentCount = cnt;
                        }

                        // }
                        rc++;
                    }


                }



                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
                populategrd();


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

    private void populategrd()
    {

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
        //LoadCourseOfStudy(DDListDept2.Text.Trim(), DDListFac2.Text.Trim());
    }
}
