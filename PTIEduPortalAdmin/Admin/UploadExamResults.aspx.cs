using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using AuditLogInfo;


public partial class Admin_UploadExamResults : System.Web.UI.Page
{
    private string ReceivedFolder = ConfigurationManager.AppSettings["WesleyAdmittedFolder"];
    private static string str = ConfigurationManager.AppSettings["conn"];

    private string Group = "";
    private string userFact = "";
    private string msg = "";
    private AuditLogInfo.AuditInfo auditInfo = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fupUpload.HasFile)
        {
            string PixFname = fupUpload.FileName;
            LoadFile(PixFname);
        }
        else
        {
            msg = "Select file to upload";
            showmassage(msg);
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

    private void LoadFile(string fname)
    {
        if (fname == "")
        {
            msg = "Please Pick a File To Upload";
            showmassage(msg);
        }

        string Localpath1 = ReceivedFolder + "\\";
        string filepath = "";
       
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
             
                userFact = getuserinfo(ID);
                               
                
                if (!Directory.Exists(Localpath1))
                {
                    Directory.CreateDirectory(Localpath1);
                }

                CurrentFile = Localpath1 + fname;
                ext = Path.GetExtension(fname);

                switch (ext.ToLower().Trim())
                {
                  
                    case ".xls":
                    case ".xlsx":
                        filepath = Server.MapPath("~/Received/");
                        filepath = filepath  + fname;
                        fupUpload.SaveAs( filepath);
                        
                        File.Copy(filepath, CurrentFile, true);
                        TreatFileEntranceResultXls(Localpath1, fname, filepath,ext );
                        break;
                 
                    default:
                        msg = "Invalid File Format: Please Download Excel Template";
                        showmassage(msg);
                       
                        return;
                }

            }
            else
            {
                msg = "Upload document with accepted format";
                showmassage(msg);
            }

        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "School Portal", "");
        }


    }

    private void TreatFileEntranceResultXls(string Localpath1, string fname, string filepath, string ext)
    {
        try
        {

            string CurrentFile = Localpath1 + fname;

            if (ValidateAdmitedXLS(filepath))
            {
                UploadDataInfo datInfo = new UploadDataInfo();
                datInfo.CurrentFile = CurrentFile;
                datInfo.Filename = fname;
                datInfo.FileTpye = ext;

                if (UploadFile(datInfo))
                {

                    //showmassage(UploadCases.msgs);
                }

                showmassage(UploadCases.msgs + Environment .NewLine +  UploadCases.msgs );


                ID = HttpContext.Current.User.Identity.Name;
                Group = (string)Cache[HttpContext.Current.User.Identity.Name];
                auditInfo = new AuditInfo();
                auditInfo.Action = "Entrance Exam Result " + CurrentFile;
                auditInfo.Usergroup = Group;
                auditInfo.Userid = ID;
                //auditInfo.Msg = msg1;
                auditInfo.Logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                auditInfo.Computer = User.Identity.Name;
                auditInfo.Hostname = Request.UserHostName;
                auditInfo.IPAddress = Request.UserHostAddress;

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
                        File.Delete(s);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "School Portal", "");
        }
    }

    private bool ValidateAdmitedXLS(string CurrentFile)
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
                    if (valicolcnt < 7)
                    {
                        msg = "Invalid excel sheet format, kindly download the attached admitted list template.xls as sample format";
                        showmassage(msg);
                        objConn.Dispose();
                        objConn.Close();

                        return succ;
                    }



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

                        //msg = "Upload successful, total number of data in this excel sheet is: " + " " + cnt.ToString();
                        //oUploadCases = new UploadCases();

                        showmassage(UploadCases.msg);

                    }

                    //}
                }
                else
                {
                    msg = "You can not submit empty excel sheet";
                    showmassage(msg);

                    return succ;
                }

            }



        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "School Portal", "");
        }
        return succ;
    }

    private static bool UploadFile(UploadDataInfo datInfo)
    {
        bool succ = false;

        switch (datInfo.FileTpye.ToLower())
        {
            case ".xls":
            case ".xlsx":
                if (UploadCases.TreatXLSEntranceExam(datInfo))
                {
                    succ = true;

                }

                
                break;
        }


        return succ;
    }

    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessageLong(this.Page, message);
        //master.ClientMessage(this.Page, message);
        //Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        //Page.Controls.Add(lbl);
    }

}
