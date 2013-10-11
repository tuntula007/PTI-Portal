using System;
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
//using CybSoft.Payroll.Business;
//using CybSoft.Payroll.Data;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;


public partial class Tools_DbBackup : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["conn"];
    //private string strCrudqueries = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Crudqueries"].Trim();
    //PayrollDBbackup
    private static string Backuppath = ConfigurationManager.AppSettings["UIDBbackup"];
    private static string PayrollBackupQueue = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["UIBackupQueue"];

    private static string sqldatapath = ConfigurationManager.AppSettings["SQLDATAPATH"];
    //private string Log4netName = System.Configuration.ConfigurationManager.AppSettings["Log4net"].Trim();
    private static string msg = "";
    

    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private static double backupprocessstage = 0;
    private static string processMsg = "";
    private static string Optypes = "";
    private static string IP = System.Configuration.ConfigurationManager.AppSettings["FeedbackIP"];
    private static double Max = 0;
    private static double Min = 0;
    private static double TotalRows = 0;
    private static bool processstart = false;

    private static int GViewWidth = 0;

    private static string PresentDept = "";

    private string auditque = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Auditlog"];
    private static CPermit cp = null;
   // private static CWritetoqueue rq = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        GridView1.Width = 600;
        ChkBoxListStaff.Width = 1000;

        if (!IsPostBack)
        {
            query = "SELECT [BackupDbName] as [Backup Database Name],[BackupDate] as [Backup Date],[BackupBy] as [Backup By],[RestoredCount] as [Restored Count],[RestoredBy] as [Restored By] FROM [DbBackup] order by BackupDate desc";

            exportheader = "[Backup Database Name],[Backup Date],[Backup By],[Restored Count],[Restored By] ";
            Exportfilename = "DbBackup";
            GridCaption = "DbBackup";
            GViewWidth = 600;
            processstart = false;
            populategrdv(query);
            LoadList();
        }
    }

    private void LoadList()
    {
        ChkBoxListStaff.Items.Clear();
        string Localpath1 = Backuppath + "\\";

        if (!Directory.Exists(Localpath1))
        {
            Directory.CreateDirectory(Localpath1);
        }

        string[] files = System.IO.Directory.GetFiles(Localpath1);
        int cnt = 0;
        foreach (string s in files)
        {
            string fileName = System.IO.Path.GetFileName(s);
            if (!fileName.ToLower().Contains("master") && !fileName.ToLower().Contains("_log"))
            {
                ChkBoxListStaff.Items.Add(fileName);
            }          

        }
    }
    protected void ChkBoxListStaff_Changed(object sender, EventArgs e)
    {
        foreach (GridViewRow oldrow in GridView1.Rows)
        {
            ((RadioButton)oldrow.FindControl("CheckBoxGIN")).Checked = false;
        }
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        try
        {
            //if (TabContainer1.ActiveTabIndex == 0)
            //{

            //}

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }
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
    private void populatetreeview()
    {
        try
        {
            //GViewWidth = 0;
            //exportheader = FacultyBusiness.exportheader;

            //Exportfilename = FacultyBusiness.Exportfilename;
            //GridCaption = FacultyBusiness.GridCaption;
            //GViewWidth = FacultyBusiness.GViewWidth;


            //FacultyBusiness fb = new FacultyBusiness();
            //DataSet qr = fb.GetFacultySchema();

            //populategrdv(qr);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
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
        //loadDDDeptnames();
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

    private void refreshscreen()
    {
        try
        {
            populatetreeview();
            //PaymentTypePanel1.Visible = true;
            //PaymentTypePanelGridv.Visible = true;
            //LabelselectPaymentType.Visible = true;
            //DDlistPaymentType.Visible = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
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
        string DbName = "";
        try
        {
            Optypes = "Backup";

            if (dbbackup())
            {


                query = "SELECT [BackupDbName] as [Backup Database Name],[BackupDate] as [Backup Date],[BackupBy] as [Backup By],[RestoredCount] as [Restored Count] FROM [DbBackup] order by BackupDate desc";
                exportheader = "[Backup Database Name],[Backup Date],[Backup By],[Restored Count]";
                Exportfilename = "DbBackup";
                GridCaption = "DbBackup";
                GViewWidth = 600;

                msg = "Monitor the progess bar to know when backup is completed";

            }
            else
            {
                msg = "database could not be backup, contact your administrator";
            }


            showmassage(msg);

            populatetreeview();

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
        }

    }

    private bool dbbackup()
    {
        bool succ = false;
        string user = this.User.Identity.Name;
        string op = "backup" + "|" + user;
        sendtoBackup(op);
        succ = true;
        processstart = true;



        ////lblProgress.Text = "";
        ////string Localpath1 = Backuppath + "\\";
        ////if (!Directory.Exists(Localpath1))
        ////{
        ////    Directory.CreateDirectory(Localpath1);
        ////}
        ////string date = DateTime.Now.ToString("yyyy-MM-dd");

        ////string user = this.User.Identity.Name;

        ////string Qry = "";
        //////string databaseName = "PayrollDB";
        ////try
        ////{

        ////    using (SqlConnection cnn = new SqlConnection(str))
        ////    {
        ////        cnn.Open();
        ////        string dbName = cnn.Database.ToString();

        ////        string backupname = dbName+date; 
        ////        ServerConnection sc = new ServerConnection(cnn);
        ////        Server sv = new Server(sc);

        ////        //BackupDeviceItem bdi = default(BackupDeviceItem);bdi = new BackupDeviceItem(@"L:\ACM\Temp\Vinu\" + backupName + ".bak", DeviceType.File);
        ////        // Create backup device item for the backup 
        ////        BackupDeviceItem bdi = new BackupDeviceItem(Localpath1 + backupname + ".bak", DeviceType.File);

        ////        // Create the backup informaton 
        ////        Microsoft.SqlServer.Management.Smo.Backup bk = new Backup();
        ////        processMsg = "Full database backup in progess";

        ////        backupprocessstage = 0;
        ////        bk.PercentComplete += new PercentCompleteEventHandler(percentComplete);
        ////        bk.Complete += new Microsoft.SqlServer.Management.Common.ServerMessageEventHandler(backup_Complete);

        ////        bk.Devices.Add(bdi);
        ////        bk.Action = BackupActionType.Database;
        ////        bk.PercentCompleteNotification = 1;
        ////        bk.BackupSetDescription = dbName;
        ////        bk.BackupSetName = dbName;
        ////        bk.Database = dbName;
        ////        //bk.ExpirationDate = DateTime.Now.AddDays(30); 
        ////        bk.LogTruncation = BackupTruncateLogType.Truncate;
        ////        bk.FormatMedia = false;
        ////        bk.Initialize = true;
        ////        bk.Checksum = true;
        ////        bk.ContinueAfterError = true;
        ////        bk.Incremental = false;

        ////        // Run the backup 
        ////        bk.SqlBackup(sv);//Exception 

        ////        if (LogBackup(Localpath1))
        ////        {
        ////            Qry = "Delete FROM [DbBackup] where [BackupDbName]='" + dbName + "' and BackupDate='" + date + "'";
        ////            PerformDelete(Qry);

        ////            Qry = "INSERT INTO [DbBackup]([BackupDbName],[BackupDate],[BackupBy],[RestoredCount]) VALUES ('" + backupname + "','" + date + "','" + user + "',0)";
        ////            PerformInsert(Qry);

        ////            msg = "Payroll backup successful, try to copy it from the folder" + ": " + backupname;
        ////            succ = true;
        ////        }                
        ////    }
        ////    //succ = true;
        ////}
        ////catch (Exception ex)
        ////{
        ////    msg = ex.Message + "||" + ex.StackTrace;
        ////    LogError(msg, "Payroll", "");
        ////    showmassage(msg);
        ////}
        return succ;
    }

    private bool LogBackup(string Localpath1)
    {
        //lblProgress.Text = "";
        ////backupprocessstage = 0;
        bool succ = false;
        ////processMsg = "Databsae log backup in progress";
        //////string Localpath1 = Backuppath + "\\";
        ////if (!Directory.Exists(Localpath1))
        ////{
        ////    Directory.CreateDirectory(Localpath1);
        ////}
        ////string date = DateTime.Now.ToString("yyyy-MM-dd");

        //////string databaseName = "PayrollDB";
        ////try
        ////{

        ////    using (SqlConnection cnn = new SqlConnection(str))
        ////    {
        ////        cnn.Open();
        ////        string dbName = cnn.Database.ToString();

        ////        string backupname = dbName+date +"_Log";// +date; 
        ////        ServerConnection sc = new ServerConnection(cnn);
        ////        Server sv = new Server(sc);

        ////        BackupDeviceItem bdi = new BackupDeviceItem(Localpath1 + backupname + ".bak", DeviceType.File);

        ////        // Create the backup informaton 
        ////        Microsoft.SqlServer.Management.Smo.Backup bk = new Backup();
        ////        bk.PercentComplete += new PercentCompleteEventHandler(percentComplete);
        ////        bk.Complete += new Microsoft.SqlServer.Management.Common.ServerMessageEventHandler(backup_Complete);

        ////        bk.Devices.Add(bdi);
        ////        bk.Action = BackupActionType.Log;
        ////        //bk.PercentCompleteNotification = 1;
        ////        bk.BackupSetDescription = dbName;
        ////        bk.BackupSetName = dbName;
        ////        bk.Database = dbName;
        ////        //bk.ExpirationDate = DateTime.Now.AddDays(30); 
        ////        bk.LogTruncation = BackupTruncateLogType.Truncate;
        ////        bk.FormatMedia = false;
        ////        bk.Initialize = true;
        ////        bk.Checksum = true;
        ////        bk.ContinueAfterError = true;
        ////        bk.Incremental = false;

        ////        // Run the backup 
        ////        bk.SqlBackup(sv);//Exception                 
        ////        succ = true;
        ////    }

        ////}
        ////catch (Exception ex)
        ////{
        ////    msg = ex.Message + "||" + ex.StackTrace;
        ////    LogError(msg, "Payroll", "");
        ////    showmassage(msg);
        ////}
        return succ;
    }


    //static void backup_Complete(object sender, Microsoft.SqlServer.Management.Common.ServerMessageEventArgs e)
    //{
    //    // WriteToLogAndConsole(e.ToString() + "% Complete");
    //}
    //static void percentComplete(object sender, PercentCompleteEventArgs e)
    //{
    //    backupprocessstage = double.Parse(e.Percent.ToString());

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
            LogError(msg, "Payroll", "");
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
            LogError(msg, "Payroll", "");
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
            LogError(msg, "Payroll", "");
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
        DataSet ds = new DataSet();

        GridView1.DataSource = null;
        GridView1.DataBind();

        //TxtDbname.Text = "";
        //TxtFacPrefix.Text = "";
        //TxtDean.Text = "";
        BtnExport.Visible = false;
        BtnRefresh.Visible = false;
        //LnkBtnPrin.Visible = false;
        //TabContainer1.Dispose();
        //TabContainer1.ActiveTabIndex = -1;
        //PaymentTypePanel1.Visible = false;

    }
    protected void BankGridv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBoxGIN_Changed(object sender, EventArgs e)
    {
        //int i = 0;

        int Prow = 0;

        //clear all option boxes
        foreach (GridViewRow oldrow in GridView1.Rows)
        {
            ((RadioButton)oldrow.FindControl("CheckBoxGIN")).Checked = false;
        }

        RadioButton rb = (RadioButton)sender;
        GridViewRow row = (GridViewRow)rb.NamingContainer;
        ((RadioButton)row.FindControl("CheckBoxGIN")).Checked = true;
        GridView1.Visible = true;

        if (ChkBoxListStaff.Items.Count > 0)
        {
            for (int k = 0; k < ChkBoxListStaff.Items.Count; k++)
            {
                ChkBoxListStaff.Items[k].Selected = false;
            }
        }


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

        try
        {
            int Prow = 0;
            string backupfilename = "";
            string backupfilename2 = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string user = this.User.Identity.Name;

            //clear all option boxes
            foreach (GridViewRow oldrow in GridView1.Rows)
            {
                if (((RadioButton)oldrow.FindControl("CheckBoxGIN")).Checked == true)
                {
                    backupfilename = oldrow.Cells[1].Text.ToString() + ".bak";
                    backupfilename2 = oldrow.Cells[1].Text.ToString();
                    break;
                }
            }


            string Localpath1 = Backuppath;

            if (Directory.Exists(Localpath1))
            {
                System.IO.File.Delete(Localpath1 + "\\" + backupfilename);
                System.IO.File.Delete(Localpath1 + "\\" + backupfilename2 + "_Log.bak");
                //payrolldb2010-08-10_Log.bak
                string Qry = "Delete FROM [DbBackup] where [BackupDbName]='" + backupfilename.Replace(".bak", "").Trim() + "'";
                PerformDelete(Qry);

                msg = "Delete successful";
                showmassage(msg);

                query = "SELECT [BackupDbName] as [Backup Database Name],[BackupDate] as [Backup Date],[BackupBy] as [Backup By],[RestoredCount] as [Restored Count],[RestoredBy] as [Restored By] FROM [DbBackup] order by BackupDate desc";

                exportheader = "[Backup Database Name],[Backup Date],[Backup By],[Restored Count],[Restored By] ";
                Exportfilename = "DbBackup";
                GridCaption = "DbBackup";
                GViewWidth = 600;
                processstart = false;
                populategrdv(query);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "Payroll", "");
        }


    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        //BtnAddnew.Visible = false;
        //BtnDelete.Visible = false;
        ////BtnEdit.Visible = false;
        //BtnViewallbanks.Visible = false;
        //Server.Transfer("Default.aspx?re=0");
    }
   
    protected void LinkBtnAddNew_Click(object sender, EventArgs e)
    {
        //PaymentTypePanel1.Visible = true;
        //LoadFaculty();
        populatetreeview();
        //MultiView1.ActiveViewIndex = 0;


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
    protected void Timer1_Tick(object sender, EventArgs e)
    {

        //double max = 100;
        //double current = backupprocessstage;
        //double per = (current * 100 / 100);
        //string percent = per.ToString("0");
        //double step = per / 1;

        //if (per > 5.0)
        //{
        //    lblProgress.Text =processMsg+"; "+" "+  "complete (" + current.ToString() + " of " + max.ToString() + ")";
        //    //lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width="+max+"><TR><TD bgcolor=#000066 width=" + percent1.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
        //    lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width=100><TR><TD bgcolor=#000066 width=" + step.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
        //    lblProgress.Visible = true;
        //}

        if (processstart == true)
        {
            Shoprogress2(100);
        }
    }
    //private void Shoprogress()
    //{

    //    try
    //    {
    //        ProgressBarFeedback pf = new ProgressBarFeedback();

    //        //get staff size
    //        string update = "";
    //        update = pf.Getfeedback(IP, "payroll", "TotalRows ");

    //        string[] item1 = update.Split(new char[] { '|' });
    //        int upp1 = item1.GetUpperBound(0);

    //        if (upp1 == 0)
    //        {
    //            TotalRows = double.Parse(item1[0].ToString());
    //            Max = TotalRows;
    //        }

    //        if (TotalRows > 1)
    //        {
    //            Shoprogress2(TotalRows);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }


    //}

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

                    double per = (current * 100 / max);
                    percent = per.ToString("0");
                    double step = per / 1;
                    //string percent1 = "";
                    //double per1 = 0;
                    //per1 = per / 8;
                    //percent1 = per1.ToString();

                    if (per > 5.0)
                    {
                        lblProgress.Text = Optypes+" "+ "complete (" + current.ToString() + " of " + max.ToString() + ")";
                        //lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width="+max+"><TR><TD bgcolor=#000066 width=" + percent1.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
                        lblProgress.Text = lblProgress.Text + "<TABLE cellspacing=0 cellpadding=0 border=1 width=100><TR><TD bgcolor=#000066 width=" + step.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>&nbsp;</TD></TR></TABLE>";
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
    
    

   

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        query = "SELECT [BackupDbName] as [Backup Database Name],[BackupDate] as [Backup Date],[BackupBy] as [Backup By],[RestoredCount] as [Restored Count],[RestoredBy] as [Restored By] FROM [DbBackup] order by BackupDate desc";
        exportheader = "[Backup Database Name],[Backup Date],[Backup By],[Restored Count],Restored By";
        Exportfilename = "DbBackup";
        GridCaption = "DbBackup";
        GViewWidth = 600;

        populategrdv(query);
        LoadList();
    }
    //protected void BtnPrintGrid_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    ds = (DataSet)Session["ds2"];
    //    Session["ds"] = ds;
    //    showwindow("PrintPages.aspx?Title=" + Exportfilename);
    //}
    private void showwindow(string window)
    {
        Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open('payment.aspx','mywindow','location=0,status=0,scrollbars=0,width=600,height=600,dependent=yes' )</script>";
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ",'CustomPopUP','location=0,resizable=no,status=0,scrollbars=yes,toolbar=yes,menubar=yes,width=600,height=600,dependent=yes' )</script>";
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }
    protected void BtnBtnRestore_Click(object sender, EventArgs e)
    {

        msg = "Please, contact your vendor for database restoration, it is a critical transaction";
        showmassage(msg);
        return;

        int Prow = 0;
        string backupfilename = "";
        //clear all option boxes
        Optypes = "Db Restore";
        if (ChkBoxListStaff.Items.Count > 0)
        {
            backupfilename = ChkBoxListStaff.Text.Trim();
            if (backupfilename == "")
            {
                foreach (GridViewRow oldrow in GridView1.Rows)
                {
                    if (((RadioButton)oldrow.FindControl("CheckBoxGIN")).Checked == true)
                    {
                        backupfilename = oldrow.Cells[1].Text.ToString() + ".bak";
                        break;
                    }
                }
            }
            //else
            //{
            //    backupfilename = backupfilename;// +".bak";
            //}

        }
        else
        {
            foreach (GridViewRow oldrow in GridView1.Rows)
            {
                if (((RadioButton)oldrow.FindControl("CheckBoxGIN")).Checked == true)
                {
                    backupfilename = oldrow.Cells[1].Text.ToString() + ".bak";
                    break;
                }
            }
        }


        //string Localpath1 = "C:\\PayrollDBbackup\\";

        string Localpath1 = Backuppath + "\\";

        //Backuppath + "\\";
        if (!Directory.Exists(Localpath1))
        {
            Directory.CreateDirectory(Localpath1);
        }

        string[] files = System.IO.Directory.GetFiles(Localpath1);
        int cnt = 0;
        foreach (string s in files)
        {

            string fileName = System.IO.Path.GetFileName(s);
            if (fileName.ToUpper().Trim() == backupfilename.ToUpper().Trim())
            {
                cnt++;
                break;
            }

        }

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        string user = this.User.Identity.Name;

        if (cnt == 0)
        {
            msg = "Selected file not found in backup folder";
            showmassage(msg);
            return;
        }
        else
        {
            if (backupfilename != "")
            {
                string messg = backupfilename + "|" + user + "|" + "restore";

                sendtoBackup(messg);

                msg = "Restore of " + ": " + backupfilename + " " + "in progress, please restart your computer after the processing to refresh the database ";
                processstart = true;
                showmassage(msg);
            }
            //send to q
        }
        //if file exist




        //string Qry = "";
        ////string databaseName = "PayrollDB";
        //try
        //{

        //    using (SqlConnection cnn = new SqlConnection(str))
        //    {

        //        backupprocessstage = 0;
        //        lblProgress.Text = "";
        //        cnn.Open();
        //        string dbName = cnn.Database.ToString();


        //        string backupname = backupfilename; //dbName +date;// +date; 
        //        ServerConnection sc = new ServerConnection(cnn);
        //        Server myServer = new Server(sc);

        //        //sqldatapath;//
        //        string DatabaseFolder = @sqldatapath;//"C:\\Program Files\\Microsoft SQL Server\\MSSQL.2\\MSSQL\\Data";
        //        //string DatabaseFileName = "";



        //        //Server myServer = GetServer();
        //        Restore myRestore = new Restore();
        //        myRestore.Database = dbName;
        //        Database currentDb = myServer.Databases[dbName];
        //        if (currentDb != null)
        //            //myServer.KillAllProcesses(dbName);
        //            myServer.KillAllProcesses(dbName);

        //       // myRestore.Devices.AddDevice(Localpath1 + backupname + ".bak", DeviceType.File);
        //        myRestore.Devices.AddDevice(Localpath1 + backupname, DeviceType.File);
        //        //myRestore.Devices.AddDevice(Localpath1 + backupname+"Log" + ".bak", DeviceType.File);

        //        string DataFileLocation = DatabaseFolder + "\\" + dbName + ".mdf";
        //        //string LogFileLocation = DatabaseFolder + "\\" + dbName + "_log.ldf";
        //        // RelocateFile rf = new RelocateFile(dbName, DataFileLocation);

        //        myRestore.RelocateFiles.Add(new RelocateFile(dbName, DataFileLocation));
        //        //myRestore.RelocateFiles.Add(new RelocateFile(dbName, LogFileLocation));
        //        myRestore.ReplaceDatabase = true;
        //        processMsg = "Full database restoration in progess";
        //        //myRestore.PercentCompleteNotification = 10;
        //        myRestore.PercentComplete += new PercentCompleteEventHandler(percentComplete);
        //        myRestore.Complete += new ServerMessageEventHandler(backup_Complete);
        //        //WriteToLogAndConsole("Restoring:{0}", destinationDatabaseName);
        //        myRestore.SqlRestore(myServer);
        //        currentDb = myServer.Databases[dbName];

        //        currentDb.SetOnline();                              

        //        msg = "Restore of " + ": " + backupname + " " + "in progress, please restart your computer to refresh the database ";
        //        //showmassage();
        //        showmassage(msg);

        //        //myServer.Refresh();

        //        BtnSubmit.Enabled = false;
        //        BtnRestore.Enabled = false;
        //        BtnExport.Enabled = false;
        //        BtnRefresh.Enabled = false;

        //    }
        //    //succ = true;
        //}
        //catch (Exception ex)
        //{
        //    msg = ex.Message + "||" + ex.StackTrace;
        //    LogError(msg, "Payroll", "");
        //    showmassage(msg);
        //}
    }
    private void sendtoBackup(string instruct)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(PayrollBackupQueue))
            {
                mq = MessageQueue.Create(PayrollBackupQueue);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(PayrollBackupQueue);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                mq.DefaultPropertiesToSend = dfp;
            }

            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            mq.Send(instruct);
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
}
