using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Messaging;
using System.Text.RegularExpressions;
using AuditLogInfo;
//using Cyberspace.ServiceBrocker;
//using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;

public partial class Createuser : System.Web.UI.Page
{

    //private static string strConn = ConfigurationManager.AppSettings["conn"];
    private static string str = ConfigurationManager.AppSettings["conn"];//.ConnectionString; 
    // private static string ZRMailQ = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Outgoingmails"];
    private static string EmailPackagePath = ".\\private$\\" + ConfigurationManager.AppSettings["EmailQ"];    //AudilogUI
    private static string AudilogUIQ = ".\\private$\\" + ConfigurationManager.AppSettings["AudilogUI"];
    //AudilogUI

    private static int GViewWidth = 0;

    private string msg = "";

    private string ID = "";// cp = null;
    private string Group = "";
    private string userDept = "";
    private string userFact = "";
    private string usercosstudy = "";


    private static string exportheader = "";
    private static string Exportfilename = "";
    private static string query = "";
    private static string GridCaption = "";
    private StringBuilder sbmsgbody = null;

    private AuditLogInfo.AuditInfo auditInfo = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //DDListShareType.Visible = false;
        //DDListShareType.Enabled = false;
        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            //if (userGroup.ToLower().Contains("users") == true)
            //{
            //    ID = HttpContext.Current.User.Identity.Name;
            //}
        }

        if (Page.IsPostBack == false)
        {
            if (!LoadGroup())
            {
                btnCreateaccount.Enabled = false;

            }
            else
            {
                LoadFaculty();
                DDListDept.Items.Add("None");
                DDListFaculty.Items.Add("None");
                DDListCourseOfStudy.Items.Add("None");
                DDListDept.Text = "None";
                DDListFaculty.Text = "None";
                DDListCourseOfStudy.Text = "None";
                //getuserinfo(ID);
            }
            // loadShareTypes();
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

    protected void DDListDept_Changed(object sender, EventArgs e)
    {
        LoadCourseOfStudy(DDListDept.Text.Trim(), DDListFaculty.Text.Trim());
    }
    private void showmassage(string message)
    {

        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    private void LoadCourseOfStudy(string Dept, string fact)
    {
        try
        {
            DDListCourseOfStudy.Items.Clear();




            DataSet ds = new DataSet();
            string qry = "SELECT [CourseOfStudyName],[CourseOfStudyID]  FROM [CourseOfStudy] where [DepartmentName]= '" + Dept + "' and [FacultyName] = '" + fact + "' order by [CourseOfStudyName] asc";// and [Programme] = '" + DDListProgramme.Text.Trim() + "' and [ModeOfStudy] = '" + DDListStudyMode.Text.Trim() + "'

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
    }
    private bool LoadGroup()
    {
        bool ret = false;
        try
        {
            cboUsergroup.Items.Clear();
            string qry = "Select Distinct Usergroup from [TUsergroups]";

            //WebServ = new CybWebServices.Service1();
            DataSet ds00 = SearchData(qry);// WebServ.RetriveDat(qry);
            string Grp = "";
            //CPermit cp = new CPermit();
            if (ds00.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds00.Tables[0].Rows.Count; jj++)
                {
                    Grp = ds00.Tables[0].Rows[jj][0].ToString().ToUpper();

                    cboUsergroup.Items.Add(Grp);
                    ret = true;
                }

                LoadGrid(cboUsergroup.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            string exx = ex.Message;
        }

        return ret;
    }
    private void LoadGrid(string GroupSel)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];


            userFact = getuserFact(ID);
            userDept = getuserDept(ID); ;
            usercosstudy = getuserCostudy(ID); ;

            if (Group.ToLower().Trim() == "web master")//|| //
            {
                query = "SELECT [Srn] as [Id],[userid] as [User Name],[usergroup] as [User Group],[emailaddress] as [Mail] FROM [TUsers] where UserStatus = 1 and [usergroup]='" + GroupSel + "' ";
            }
            else
            {
                query = "SELECT [Srn] as [Id],[userid] as [User Name],[usergroup] as [User Group],[emailaddress] as [Mail] FROM [TUsers] where UserStatus = 1 and [usergroup]='" + Group + "' ";
            }

            exportheader = "[Id],[User Name],[User Group],[Mail]";

            Exportfilename = "UserGroup";
            GridCaption = "UserGroup";
            GViewWidth = 1000;

            populategrdv(query);

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            LogError(msg, "UI", "");
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
    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ")</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
    protected void cboUsergroup_Changed(object sender, EventArgs e)
    {
        DDListDept.Items.Clear();//.Text = "None";
        DDListFaculty.Items.Clear();
        if (cboUsergroup.Text.Trim().ToLower() == "web master" || cboUsergroup.Text.Trim().ToLower() == "bursary admin" || cboUsergroup.Text.Trim().ToLower() == "bursary operation" || cboUsergroup.Text.Trim().ToLower() == "records" || cboUsergroup.Text.Trim().ToLower() == "admission" || cboUsergroup.Text.Trim().ToLower() == "audit" || cboUsergroup.Text.Trim().ToLower() == "help desk" || cboUsergroup.Text.Trim().ToLower() == "medical" || cboUsergroup.Text.Trim().ToLower() == "library" || cboUsergroup.Text.Trim().ToLower() == "editorial")//editorial 
        {
            DDListDept.Enabled = false;
            DDListFaculty.Enabled = false;
            DDListCourseOfStudy.Enabled = false;
        }
        else
        {
            LoadFaculty();
            DDListDept.Enabled = true;
            DDListFaculty.Enabled = true;
            DDListCourseOfStudy.Enabled = true;
        }

        LoadGrid(cboUsergroup.Text.ToLower().Trim());
    }
    private void LoadFaculty()
    {

        try
        {
            DDListFaculty.Items.Clear();
            // FacultyID = new Hashtable();

            DataSet ds = new DataSet();
            string qry = "SELECT [FacultyName],[FacultyID] FROM [Faculty] order by [FacultyName] asc";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //DDListFaculty.Items.Add("None");
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
            // DepartmentID = new Hashtable();



            try
            {
                DataSet ds = null;
                string qry = "";
                qry = "SELECT distinct [DepartmentName],[DepartmentId] FROM [Departments] where [FacultyName]='" + Fact + "' order by [DepartmentName] asc";

                ds = new DataSet();
                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DDListDept.Items.Add("None");
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        DDListDept.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                        //DepartmentID.Add(ds.Tables[0].Rows[jj][0].ToString(), ds.Tables[0].Rows[jj][1].ToString());
                    }
                    LoadCourseOfStudy(DDListDept.Text.Trim(), Fact);
                }
                else
                {
                    DDListDept.Items.Clear();
                    DDListCourseOfStudy.Items.Clear();
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
    protected void DDListFaculty_Changed(object sender, EventArgs e)
    {
        loadDept(DDListFaculty.Text.Trim());
    }
    protected void btnCreateaccount_Click(object sender, EventArgs e)
    {
        string k = Group.ToLower().Trim();
        string fact = "";
        string dept = "";
        string CosOfStudy = "";
        bool validuser = false;



        ID = HttpContext.Current.User.Identity.Name;
        Group = (string)Cache[HttpContext.Current.User.Identity.Name];


        userFact = getuserFact(ID);
        userDept = getuserDept(ID); ;
        usercosstudy = getuserCostudy(ID); ;

        if (Group.ToUpper() == "WEB MASTER")
        {
            if (cboUsergroup.Text.Trim().ToUpper() == "WEB MASTER")
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;
            }
            //if (cboUsergroup.Text.Trim().ToUpper() == "BURSARY OPERATION" || cboUsergroup.Text.Trim().ToUpper() == "AUDIT" || cboUsergroup.Text.Trim().ToUpper() == "HELP DESK" || cboUsergroup.Text.Trim().ToUpper() == "HELP DESK")
            //{
            //    fact = "None";
            //    dept = "None";
            //    CosOfStudy = "None";
            //    validuser = true;
            //}
            if (cboUsergroup.Text.Trim().ToUpper() == "BURSARY ADMIN" || cboUsergroup.Text.Trim().ToUpper() == "BURSARY OPERATION" || cboUsergroup.Text.Trim().ToUpper() == "AUDIT" || cboUsergroup.Text.Trim().ToUpper() == "HELP DESK" || cboUsergroup.Text.Trim().ToUpper() == "ADMISSION" || cboUsergroup.Text.Trim().ToUpper() == "RECORDS" || cboUsergroup.Text.Trim().ToUpper() == "MEDICAL" || cboUsergroup.Text.Trim().ToUpper() == "LIBRARY" || cboUsergroup.Text.Trim().ToUpper() == "EDITORIAL")//editorial
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;
            }

            
            if (cboUsergroup.Text.Trim().ToUpper() == "FACULTY ADMIN")
            {
                fact = DDListFaculty.Text.Trim();
                dept = "None";
                CosOfStudy = "None";
                validuser = true;
            }
            if (cboUsergroup.Text.Trim().ToUpper() == "DEPARTMENT ADMIN" && DDListCourseOfStudy.Text.ToLower().Trim() != "")
            {
                fact = DDListFaculty.Text.Trim();
                dept = DDListDept.Text.Trim();
                CosOfStudy = DDListCourseOfStudy.Text.Trim();
                validuser = true;
            }

        }
        //
        if (Group.ToUpper() == "BURSARY ADMIN")
        {
            if (cboUsergroup.Text.Trim().ToUpper() == "BURSARY ADMIN" || cboUsergroup.Text.Trim().ToUpper() == "BURSARY OPERATION" || cboUsergroup.Text.Trim().ToUpper() == "AUDIT")
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;

            }
            else
            {
                msg = "you have no right to create user for this group";
                showmassage(msg);
                validuser = false;
                return;
            }
            //if (cboUsergroup.Text.Trim().ToUpper() != "BURSARY ADMIN")
            //{
            //    msg = "you have no right to create user for this group";
            //    showmassage(msg);
            //    validuser = false;
            //    return;

            //}
            //else
            //{
            //    fact = "None";
            //    dept = "None";
            //    CosOfStudy = "None";
            //    validuser = true;
            //}
        }

        //cboUsergroup.Text.Trim().ToUpper() == "ADMISSION" || cboUsergroup.Text.Trim().ToUpper() == "RECORDS"

        if (Group.ToUpper() == "ADMISSION")
        {
            if (cboUsergroup.Text.Trim().ToUpper() == "ADMISSION")
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;

            }
            else
            {
                msg = "you have no right to create user for this group";
                showmassage(msg);
                validuser = false;
                return;
            }
        }

        if (Group.ToUpper() == "RECORDS")
        {
            if (cboUsergroup.Text.Trim().ToUpper() == "RECORDS")
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;

            }
            else
            {
                msg = "you have no right to create user for this group";
                showmassage(msg);
                validuser = false;
                return;
            }
        }

        if (Group.ToUpper() == "EDITORIAL")
        {
            if (cboUsergroup.Text.Trim().ToUpper() == "EDITORIAL")
            {
                fact = "None";
                dept = "None";
                CosOfStudy = "None";
                validuser = true;

            }
            else
            {
                msg = "you have no right to create user for this group";
                showmassage(msg);
                validuser = false;
                return;
            }
        }

        if (Group.ToUpper() == "FACULTY ADMIN")
        {
            if (DDListFaculty.Text.Trim().ToLower() != userFact.ToLower())
            {
                msg = "you have no right to create user for this faculty";
                showmassage(msg);
                validuser = false;
                return;
            }
            else
            {
                if (cboUsergroup.Text.Trim().ToUpper() == "FACULTY ADMIN")
                {
                    fact = DDListFaculty.Text.Trim();
                    dept = "None";
                    CosOfStudy = "None";
                    validuser = true;
                }
                else
                {
                    if (cboUsergroup.Text.Trim().ToUpper() == "DEPARTMENT ADMIN" && DDListCourseOfStudy.Text.ToLower().Trim() != "")
                    {
                        fact = DDListFaculty.Text.Trim();
                        dept = DDListDept.Text.Trim();
                        CosOfStudy = DDListCourseOfStudy.Text.Trim();
                        validuser = true;
                    }
                    else
                    {
                        msg = "you have no right to create user for this group";
                        showmassage(msg);
                        validuser = false;
                        return;
                    }
                }
            }
        }


        if (Group.ToUpper() == "DEPARTMENT ADMIN")
        {
            if (DDListDept.Text.Trim().ToLower() != userDept.ToLower() && DDListCourseOfStudy.Text.ToLower().Trim() != usercosstudy.ToLower())
            {
                msg = "you have no right to create user for this department and course of study";
                showmassage(msg);
                validuser = false;
                return;
            }
            else
            {

                if (cboUsergroup.Text.Trim().ToUpper() == "DEPARTMENT ADMIN" && DDListCourseOfStudy.Text.ToLower().Trim() != "")
                {
                    fact = DDListFaculty.Text.Trim();
                    dept = DDListDept.Text.Trim();
                    CosOfStudy = DDListCourseOfStudy.Text.Trim();
                    validuser = true;
                }
                else
                {
                    msg = "you have no right to create user for this group";
                    showmassage(msg);
                    validuser = false;
                    return;
                }
            }
        }


        if (txtUserid.Text.Trim() == "")
        {
            MessageBox("You must provide a valid UserId!");
            txtUserid.Focus();
            return;
        }
        if (txtPassword.Text.Trim() == "")
        {
            MessageBox("Password cannot be empty!");
            txtPassword.Focus();
            return;
        }

        if (txtPassword.Text.Trim() != txtConfirmpassword.Text.Trim())
        {
            MessageBox("Password fields does not match!");
            txtPassword.Focus();
            return;
        }
        try
        {



            if (txtEmail.Text.Trim() != "" && txtEmail.Text != "NONE")
            {
                string mail = "";
                string patternStrict = ConfigurationManager.AppSettings["regexmail"];

                MatchCollection mc = Regex.Matches(txtEmail.Text, patternStrict);

                for (int i = 0; i < mc.Count; i++)
                {
                    mail = mc[0].ToString();
                }

                if (mc.Count > 0)
                {
                    txtEmail.Text = mail.ToLower();
                }
                else
                {
                    msg = "Enter a valid mail address";
                    showmassage(msg);
                    txtEmail.Focus();
                    return;
                }
            }
            else
            {
                msg = "Enter a valid mail address";
                showmassage(msg);
                txtEmail.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
        }


        try
        {
            string qry = "Select * from TUsers where emailaddress ='" + txtEmail.Text.Trim() + "'";
            if (Existed(qry))
            {
                MessageBox("Email already exists!");
                txtEmail.Focus();
                return;
            }

            qry = "Select Userid from TUsers where userid ='" + txtUserid.Text.Trim() + "'";
            if (Existed(qry))
            {
                MessageBox("The user id you entered already exists!");
                validuser = false;
                return;
            }
            else
            {
                if (validuser == true)
                {
                    qry = "INSERT INTO  [TUsers] ([userid] ,[passw],[usergroup],[emailaddress],[UserStatus],[FacultyName],[DepartmentName],CourseOfStudy,SetupBy) VALUES ('" + txtUserid.Text.Trim().Replace("'", "''") + "','" + txtPassword.Text.Trim().Replace("'", "''") + "','" + cboUsergroup.Text.ToLower() + "','" + txtEmail.Text.Trim().ToLower().Replace("'", "''") + "',1,'" + fact + "','" + dept + "','" + CosOfStudy + "','" + ID + "')";

                    PerformInsert(qry);

                    //loginfo

                    auditInfo = new AuditInfo();

                    auditInfo.Action = "Create User";
                    auditInfo.Usergroup = Group;
                    auditInfo.Userid = ID;
                    auditInfo.Msg = qry;
                    auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    auditInfo.Computer = User.Identity.Name;
                    auditInfo.Hostname = Request.UserHostName;
                    auditInfo.IPAddress = Request.UserHostAddress;
                    sendtoAuditQ(auditInfo);


                    string Subject = "Login Credentials";
                    string Heading = "PETROLEUM TRAINING INSTITUTE, WARRI, NIGERIA - admin portal Login Credentials";
                    string Attached = "";
                    string email = txtEmail.Text.Trim();
                    string name = txtUserid.Text.Trim();


                    //String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

                    sbmsgbody = new StringBuilder();

                    sbmsgbody.AppendLine("");

                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Below are your login credentials for PTI Admin portal:");
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Username = " + name);
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("Password = " + txtPassword.Text.Trim());
                    sbmsgbody.AppendLine("");
                    sbmsgbody.AppendLine("User Group = " + cboUsergroup.Text.Trim());
                    sbmsgbody.AppendLine("");

                    sendGenMail(email, sbmsgbody.ToString(), Subject, Heading, Attached, name);

                    txtUserid.Text = "";
                    txtEmail.Text = "";
                    showmassage("User Account successfully created!");
                    validuser = true;




                }

            }

        }
        catch (Exception ex)
        {
            string exx = ex.Message;
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
            sb.AppendLine("PETROLEUM TRAINING INSTITUTE, WARRI, NIGERIA");

            cm.Body = sb.ToString();
            
            cm.BCCEmail.Add("cybsoft@cybaaspace.net");
            cm.ReplyTo.Add("itsupport@dlc.ui.edu.ng");
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
    protected void grdViewStatustory_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count <= 0)
            {
                e.Cancel = true;
                return;
            }


            if (Group.ToLower().Trim() != "superadmin" && Group.ToLower().Trim() != "admin")//|| //
            {
                msg = "You have no right to create user";
                showmassage(msg);
                return;
            }

            int id = 0;

            string userno = GridView1.Rows[e.RowIndex].Cells[1].Text; ;

            //Name = row.Cells[2].Text.Trim();

            if (int.TryParse(userno, out id))
            {
            }
            else
            {
                msg = "Id not in right format";
                showmassage(msg);
                return;
            }

            string qry = "Delete [TUsers] where [Srn] = " + userno + "";
            LoadGrid(cboUsergroup.Text.Trim());



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Library", "");
        }

    }
    protected void grdViewStatustory_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        GridView1.EditIndex = -1;
        try
        {
            //populategrd();
            LoadGrid(cboUsergroup.Text.Trim());

        }
        catch (Exception ex)
        {

            //logger.Error(ex.Message);
        }
    }
    protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;

            LoadGrid(cboUsergroup.Text.Trim());
            //populategrd();// DisplayGrid();
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = true;
            row.Cells[3].Enabled = true;
            row.Cells[4].Enabled = true;

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Library", "");
        }

    }

    protected void grdViewStatustory_OnRowUpdating(Object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView1.ToolTip = GridView1.Rows[e.RowIndex].Cells[1].Text;
            GridViewRow row = GridView1.Rows[e.RowIndex];
            GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
            string Usergruop = ((DropDownList)row.FindControl("DropDownList1")).SelectedItem.Value;
            //GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            //GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();

            string k = Usergruop;


            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];


            userFact = getuserFact(ID);
            userDept = getuserDept(ID); 
            usercosstudy = getuserCostudy(ID);


            if (Group.ToUpper().Trim() != "WEB MASTER")//|| //
            {
                msg = "You have no right to edit user, contact web master";
                showmassage(msg);
                return;
            }



            string Name = "";
            int id = 0;
            string email = "";


            string userno = row.Cells[1].Text.Trim();
            Name = row.Cells[2].Text.Trim();

            if (int.TryParse(userno, out id))
            {
            }
            else
            {
                msg = "Id not in right format";
                showmassage(msg);
                return;
            }

            email = row.Cells[4].Text.Trim();

            if (email == "")
            {
                msg = "Enter a valid mail";
                showmassage(msg);
                return;
            }
            else
            {
                string mail = "";
                string patternStrict = ConfigurationManager.AppSettings["regexmail"];

                MatchCollection mc = Regex.Matches(email, patternStrict);

                for (int i = 0; i < mc.Count; i++)
                {
                    mail = mc[0].ToString();
                }

                if (mc.Count > 0)
                {
                    email = mail.ToLower();
                }
                else
                {
                    msg = "Enter a valid mail address";
                    showmassage(msg);
                    return;
                }
            }



            GridView1.EditIndex = -1;
            //update
            string qry = "UPDATE TUsers SET [usergroup] = '" + Usergruop + "', [emailaddress]='" + email + "',[userid]='" + Name + "',SetupBy ='" + ID + "' where [Srn] = " + userno + "";
            PerformUpdate(qry);

            string Subject = "Login Credentials";
            string Heading = "PTI - admin portal updated Login Credentials";
            string Attached = "";




            //String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

            sbmsgbody = new StringBuilder();
            sbmsgbody.AppendLine("");

            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("Below are your updated login credentials for admin portal access:");
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("Username = " + Name);
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("email = " + email);
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("User Group = " + Usergruop);
            sbmsgbody.AppendLine("");

            sendGenMail(email, sbmsgbody.ToString(), Subject, Heading, Attached, Name);





            LoadGrid(cboUsergroup.Text.Trim());// populatetreeview();
            //populategrd();
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            //row.Cells[5].Enabled = false;
            //row.Cells[6].Enabled = false;
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
            //LogError(msg, "Library", "");
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
            LogError(msg, "ZR", "");
            showmassage(msg);
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string TxtSearch = TxtSearchstaff.Text.Trim();
        try
        {
            if (TxtSearch != "")
            {
                if (Group.ToLower().Trim() == "web master")//|| //
                {
                    query = "SELECT [Srn] as [Id],[userid] as [User Name],[usergroup] as [User Group],[emailaddress] as [Mail] FROM [TUsers] where [userid] like '%" + TxtSearch + "%' or [passw] like '%" + TxtSearch + "%' or [usergroup] like '%" + TxtSearch + "%' or [emailaddress] like '%" + TxtSearch + "%' or [UserStatus] like '%" + TxtSearch + "%' or [FacultyName] like '%" + TxtSearch + "%' or [DepartmentName] like '%" + TxtSearch + "%' or [CourseOfStudy] like '%" + TxtSearch + "%' and UserStatus = 1";
                }
                else
                {
                    query = "SELECT [Srn] as [Id],[userid] as [User Name],[usergroup] as [User Group],[emailaddress] as [Mail] FROM [TUsers] where [userid] like '%" + TxtSearch + "%' or [passw] like '%" + TxtSearch + "%' or [usergroup] like '%" + TxtSearch + "%' or [emailaddress] like '%" + TxtSearch + "%' or [UserStatus] like '%" + TxtSearch + "%' or [FacultyName] like '%" + TxtSearch + "%' or [DepartmentName] like '%" + TxtSearch + "%' or [CourseOfStudy] like '%" + TxtSearch + "%' and UserStatus = 1 and [usergroup]='" + Group + "' ";
                }

                exportheader = "[Id],[User Name],[User Group],[Mail]";

                Exportfilename = "UserGroup";
                GridCaption = "UserGroup";
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
            LogError(msg, "UI", "");
        }
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
            LogError(msg, "Zenith Registrar", "");
            showmassage(msg);
        }

        return ret;
    }
    //private bool IdExisted(string qry)
    //{
    //    bool rtn = false;

    //    WebServ = new CybWebServices.Service1();
    //    DataSet ds00 = WebServ.RetriveDat(qry);

    //    if (ds00.Tables[0].Rows.Count > 0)
    //    {
    //        rtn = true;
    //    }

    //    return rtn;
    //}
}
