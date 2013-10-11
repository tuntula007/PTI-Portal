using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Threading;


public partial class Admin_CourseMatUpload : System.Web.UI.Page
{
    private string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["conn"];

    private string msg = "";
    private string strFileGalleryFolder = "Received";// System.Configuration.ConfigurationManager.AppSettings["FileGalleryFolder"];
    //CoursematerialPath
    private string coursematerialPath = System.Configuration.ConfigurationManager.AppSettings["CoursematerialPath"];

    //private static string strConnectionString = ConfigurationManager.AppSettings["conn"];
    //private string ReceivedFolder = System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedFolder"];
    //private string RawdataUpload = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["WesleyAdmittedUploader"];




    private bool m_isApplicationTerminating = false;
    private ArrayList filePaths;
    private ArrayList allowedExtensions;
    private int m_threadCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            //BulletedList1.Items.Add("Document#: " + DateTime.Now.ToString("1yyMMddHHmmssfff"));
            //LoadCategory();
            LoadLevels();
            LoadFaculty();
        }
    }

    private void LoadCategory()
    {
        DDListCourseCode.Items.Clear();

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select distinct [ContentCategory]  from  [Content_Category] order by [ContentCategory]", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                while (dr.Read() == true)
                {
                    DDListCourseCode.Items.Add(dr.GetString(0).ToLower());
                }

            }

            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //logger.Info(ex.Message);
        }

    }
    protected void lnkAdd1_Click(object sender, EventArgs e)
    {
        lnkAdd2.Visible = true;
        lnkAdd1.Visible = false;
        FileUpload2.Visible = true;
    }
    protected void lnkAdd2_Click(object sender, EventArgs e)
    {
        lnkAdd3.Visible = true;
        lnkAdd2.Visible = false;
        FileUpload3.Visible = true;
    }
    protected void lnkAdd3_Click(object sender, EventArgs e)
    {
        lnkAdd4.Visible = true;
        lnkAdd3.Visible = false;
        FileUpload4.Visible = true;
    }
    protected void lnkAdd4_Click(object sender, EventArgs e)
    {
        lnkAdd5.Visible = true;
        lnkAdd4.Visible = false;
        FileUpload5.Visible = true;
    }
    protected void lnkAdd5_Click(object sender, EventArgs e)
    {
        //lnkAdd6.Visible = true;
        lnkAdd5.Visible = false;
        FileUpload6.Visible = true;
    }
    protected void cmdSubmitContent_Click(object sender, EventArgs e)
    {
        //bulletError.Items.Clear();
        m_threadCount = 0;
        try
        {
            filePaths = new ArrayList();

            bool shouldInsert = false;
            int m_threads = 0;
            bool m_iscritical = false;

            int fileCount = 0;

            Thread upload;
            CFileUploading cf;
            DateTime currentTime = DateTime.Now;
            string DocumentID = DateTime.Now.ToString("1yyMMddHHmmssfff");// BulletedList1.Items[0].Text.Replace("Document#:", "").Trim();
            //allowedExtensions = GetAllowedExtentions();


            Hashtable allfiles = new Hashtable();

            string Localpath1 = coursematerialPath;// +"\\";
            string filepath2 = "";
            // string Taxation = "NO";
            string CurrentFile = "";
            string ext = "";
            int FileSize = 0;
            string ID = HttpContext.Current.User.Identity.Name;
            string Sql = "";

            if (!Directory.Exists(Localpath1))
            {
                Directory.CreateDirectory(Localpath1);
            }

            //CurrentFile = Localpath1 + fname;


            FileInfo finfo = null;
            long magesize = 0;
            long maxsizeMB = 0;
            long FileInKB = 0;
            long FileInMB = 0;

            string fname = "";
            string contentType = "application/pdf";
            long maxsize = 20;

            if (FileUpload1.HasFile == true)
            {

                fname = FileUpload1.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":
                        cf = new CFileUploading();
                        cf.FUpload = FileUpload1;
                        cf.ControlNumber = 1;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload1.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);

                        finfo = new FileInfo(filepath2);
                         magesize = finfo.Length;
                         maxsizeMB = 2;
                         FileInKB = finfo.Length / 1024;
                         FileInMB = FileInKB / 1024;
                         if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload1";
                            showmassage(msg);
                            return;
                        }


                        //////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                        //////upload.Start(cf);
                        //////Interlocked.Increment(ref m_threads);

                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }

                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }


                //SaveFile(FileUpload1, ticketID,1);

            }

            if (FileUpload2.HasFile == true)
            {
                ////cf = new CFileUploading();
                ////cf.FUpload = FileUpload2;
                ////cf.ControlNumber = 2;
                ////cf.TicketID = DocumentID;
                ////m_threadCount++;
                ////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                ////upload.Start(cf);
                ////Interlocked.Increment(ref m_threads);
                ////shouldInsert = true;
                //////SaveFile(FileUpload2, ticketID,2);
                ////fileCount++;

                fname = FileUpload2.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":
                        cf = new CFileUploading();
                        cf.FUpload = FileUpload2;
                        cf.ControlNumber = 2;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload2.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);

                        finfo = new FileInfo(filepath2);
                        magesize = finfo.Length;
                        maxsizeMB = 2;
                        FileInKB = finfo.Length / 1024;
                        FileInMB = FileInKB / 1024;
                        if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload2";
                            showmassage(msg);
                            return;
                        }

                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }
                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }

            }

            if (FileUpload3.HasFile == true)
            {
                ////cf = new CFileUploading();
                ////cf.FUpload = FileUpload3;
                ////cf.ControlNumber = 3;
                ////cf.TicketID = DocumentID;
                ////m_threadCount++;
                ////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                ////upload.Start(cf);
                ////Interlocked.Increment(ref m_threads);
                ////shouldInsert = true;
                ////fileCount++;
                //SaveFile(FileUpload2, ticketID,3);

                fname = FileUpload3.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":
                        cf = new CFileUploading();
                        cf.FUpload = FileUpload3;
                        cf.ControlNumber = 3;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload3.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);

                        finfo = new FileInfo(filepath2);
                        magesize = finfo.Length;
                        maxsizeMB = 2;
                        FileInKB = finfo.Length / 1024;
                        FileInMB = FileInKB / 1024;
                        if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload3";
                            showmassage(msg);
                            return;
                        }

                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }
                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }
            }

            if (FileUpload4.HasFile == true)
            {
                ////cf = new CFileUploading();
                ////cf.FUpload = FileUpload4;
                ////cf.ControlNumber = 4;
                ////cf.TicketID = DocumentID;
                ////m_threadCount++;
                ////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                ////upload.Start(cf);
                ////Interlocked.Increment(ref m_threads);
                ////shouldInsert = true;
                ////fileCount++;
                //SaveFile(FileUpload2, ticketID,3);

                fname = FileUpload4.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":
                        cf = new CFileUploading();
                        cf.FUpload = FileUpload4;
                        cf.ControlNumber = 1;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload4.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);

                        finfo = new FileInfo(filepath2);
                        magesize = finfo.Length;
                        maxsizeMB = 2;
                        FileInKB = finfo.Length / 1024;
                        FileInMB = FileInKB / 1024;
                        if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload4";
                            showmassage(msg);
                            return;
                        }


                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }
                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }
            }


            if (FileUpload5.HasFile == true)
            {
                ////cf = new CFileUploading();
                ////cf.FUpload = FileUpload5;
                ////cf.ControlNumber = 5;
                ////cf.TicketID = DocumentID;
                ////m_threadCount++;
                ////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                ////upload.Start(cf);
                ////Interlocked.Increment(ref m_threads);
                ////shouldInsert = true;
                ////fileCount++;
                //SaveFile(FileUpload2, ticketID,3);

                fname = FileUpload1.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":

                        cf = new CFileUploading();
                        cf.FUpload = FileUpload5;
                        cf.ControlNumber = 5;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload5.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);

                        finfo = new FileInfo(filepath2);
                        magesize = finfo.Length;
                        maxsizeMB = 2;
                        FileInKB = finfo.Length / 1024;
                        FileInMB = FileInKB / 1024;
                        if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload5";
                            showmassage(msg);
                            return;
                        }
                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }
                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }
            }

            if (FileUpload6.HasFile == true)
            {
                ////cf = new CFileUploading();
                ////cf.FUpload = FileUpload6;
                ////cf.ControlNumber = 6;
                ////cf.TicketID = DocumentID;
                ////m_threadCount++;
                ////upload = new Thread(new ParameterizedThreadStart(UploadHandler));
                ////upload.Start(cf);
                ////Interlocked.Increment(ref m_threads);
                ////shouldInsert = true;
                ////fileCount++;
                //SaveFile(FileUpload2, ticketID,3);

                fname = FileUpload1.FileName;
                ext = Path.GetExtension(fname);
                switch (ext.ToLower().Trim())
                {
                    case ".pdf":
                    case ".mp3":
                    case ".mpga":
                    case ".wav":
                    case ".wma":
                    case ".mid":
                    case ".midi":
                    case ".rm":
                    case ".ram":
                        cf = new CFileUploading();
                        cf.FUpload = FileUpload6;
                        cf.ControlNumber = 6;
                        cf.TicketID = DocumentID;
                        //m_threadCount++;

                        filepath2 = Server.MapPath("~/Received/" + fname);
                        FileUpload6.SaveAs(filepath2);
                        File.Copy(filepath2, Localpath1 + fname, true);


                        finfo = new FileInfo(filepath2);
                        magesize = finfo.Length;
                        maxsizeMB = 2;
                        FileInKB = finfo.Length / 1024;
                        FileInMB = FileInKB / 1024;
                        if (FileInMB > maxsize || FileInMB < 0)
                        {

                            msg = "Document size can not be more than 4MB for upload6";
                            showmassage(msg);
                            return;
                        }

                        if (!allfiles.Contains(fname))
                        {
                            allfiles.Add(fname, FileUpload1.PostedFile.ContentLength);
                        }
                        shouldInsert = true;
                        fileCount++;

                        break;

                    default:
                        msg = "Invalid File Format: Please load only pdf files only";
                        showmassage(msg);
                        //FileUpload2.Focus();
                        return;
                }
            }

            if (shouldInsert == true)
            {
                if ((txtTitle.Text.Trim().Length + txtContentBody.Text.Trim().Length) > 0)
                {
                    // Create a new Document for this guy

                    SqlConnection cnn = new SqlConnection(strConnectionString);
                    cnn.Open();

                    int xpirationMode = 0;
                    if (chkExpireMode.Checked == true)
                    {
                        xpirationMode = 1;
                    }

                    int publishedStatus = 0;
                    if (chkPublish.Checked == true)
                    {
                        publishedStatus = 1;
                    }
                    int restrictDownload = 0;
                    if (chkDownloadRestricted.Checked == true)
                    {
                        restrictDownload = 1;
                    }
                    DateTime expiration1 = DateTime.Now;
                    DateTime expiration = DateTime.Now;
                    txtExpireDate.Text = txtExpireDate.Text.Trim().Replace("'", "''");
                    if (txtExpireDate.Text.Trim().Length > 0)
                    {
                        if (DateTime.TryParse(txtExpireDate.Text, out expiration) == true)
                        {
                            // DateTime is accepted
                        }
                        else
                        {
                            expiration = currentTime;

                        }
                    }

                    expiration1 = expiration;

                    //   // Create the Ticket
                    
                    int downloadcnt = 0;
                    int cnt=0;
                    foreach (DictionaryEntry fn in allfiles)
                    {
                        string sfile = fn.Key.ToString();
                        FileSize = int.Parse(fn.Value.ToString());
                        string destpath = Localpath1 + sfile;
                        //getcontent type from file ext.
                        ext = Path.GetExtension(sfile);

                        Sql = "INSERT INTO [CourseMaterial]" +
                                                               "([DocumentID]" +
                                                               ",[DocumentCategory]" +
                                                               ",[DocumentName]" +
                                                               ",[ExpirationMode]" +
                                                               ",[PostedDate]" +
                                                               ",[ExpirationDate]" +
                                                               ",[ContentCreatedByUser]" +
                                                               ",[PublishedStatus]" +
                                                               ",[RestrictDownload]" +
                                                               ",[AssociatedDocumentID]" +
                                                               ",[LastModifiedDate]" +
                                                               ",[ContentDescription]" +
                                                               ",[Quantity]" +
                                                               ",[AcademicLevel]" +
                                                               ",[FileSize]" +
                                                               ",[FilePath]" +
                                                               ",[ContentType]" +
                                                               ",[DownLoadCnt]" +
                                                                ") VALUES" +
                                                                "('" + DocumentID +
                                                                "','" + DDListCourseCode.Text.Trim().Replace("'", "''") +
                                                                "','" + txtTitle.Text.Trim().Replace("'", "''").Replace("&", "and") +
                                                                "'," + xpirationMode +
                                                                ",'" + currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                                                                "','" + expiration1.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                                                                "','" + ID +
                                                                "'," + publishedStatus +
                                                                "," + restrictDownload +
                                                                ",'" + DocumentID + "1" +
                                                                "','" + currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                                                                "','" + txtContentBody.Text.Trim().Replace("'", "''").Replace("&", "and") +
                                                                "'," + fileCount +
                                                                ",'" + DDListLevel.Text +
                                                                "'," + FileSize +
                                                                ",'" + destpath +
                                                                "','" + contentType +
                                                                "'," + downloadcnt + ")";
                        PerformInsert(Sql);
                        cnt++;

                        //DocumentID=DocumentID + cnt.ToString();

                        
                    }



                    ////SqlCommand cmd = new SqlCommand("INSERT INTO [CourseMaterial]" +
                    ////                                           "([DocumentID]" +
                    ////                                           ",[DocumentCategory]" +
                    ////                                           ",[DocumentName]" +
                    ////                                           ",[ExpirationMode]" +
                    ////                                           ",[PostedDate]" +
                    ////                                           ",[ExpirationDate]" +
                    ////                                           ",[ContentCreatedByUser]" +
                    ////                                           ",[PublishedStatus]" +
                    ////                                           ",[RestrictDownload]" +
                    ////                                           ",[AssociatedDocumentID]" +
                    ////                                           ",[LastModifiedDate]" +
                    ////                                           ",[ContentDescription]" +
                    ////                                           ",[Quantity]" +                                                               
                    ////                                           ",[AcademicLevel]" +
                    ////                                           ",[FileSize]" +
                    ////                                           ",[FilePath]" +
                    ////                                            ") VALUES" +
                    ////                                            "('" + DocumentID +
                    ////                                            "','" + DDListCourseCode.Text.Trim().Replace("'", "''") +
                    ////                                            "','" + txtTitle.Text.Trim().Replace("'", "''") +
                    ////                                            "'," + xpirationMode +
                    ////                                            ",'" + currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                    ////                                            "','" + expiration1.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                    ////                                            "','" + ID +
                    ////                                            "'," + publishedStatus +
                    ////                                            "," + restrictDownload +
                    ////                                            ",'" + DocumentID + "1" +
                    ////                                            "','" + currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") +
                    ////                                            "','" + txtContentBody.Text.Trim().Replace("'", "''") +
                    ////                                            "'," + fileCount + 
                    ////                                            ",'"+DDListLevel.Text+
                    ////                                            "'," + FileSize + 
                    ////                                            ",'"+Localpath1+ "')", cnn);

                    ////cmd.ExecuteNonQuery();
                    ////cmd.Dispose();

                    //delay a litle
                    ////while (m_threadCount > 0)
                    ////{
                    ////    Thread.Sleep(1000);
                    ////}
                    ////foreach (CAttachments att in filePaths)
                    ////{
                    ////    cmd = new SqlCommand("INSERT INTO  [Content_Posts] ([DocumentID],[FilePath],[FileSize] ,[FileExtension] ,[ContentType],[DownloadCount],[Uploaded]) VALUES('" + DocumentID + "','" + att.FilePath + "'," + att.FileSize + ",'" + att.FileExtention + "','" + att.ContentType + "',0,'" + currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')", cnn);
                    ////    cmd.ExecuteNonQuery();
                    ////    cmd.Dispose();
                    ////}

                    ////cnn.Dispose();
                    ////cnn.Close();                    
                    txtContentBody.Text = "";
                    txtTitle.Text = "";


                }

                populategrd();

                msg = fileCount.ToString() + " Files uploaded successfully";
                showmassage(msg);
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void PerformInsert(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

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
        //message = message.Replace("'", " ").Replace("\r\n", "");
        //MasterPage master = (MasterPage)this.Master;
        //master.ClientMessage(this.Page, message);
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        Page.Controls.Add(lbl);
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
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
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
    private void LoadFaculty()
    {
        try
        {

            DDListFaculty.Items.Clear();
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
            msg = ex.Message + "||" + ex.StackTrace;
            // LogError(msg, "School Portal", "");
            showmassage(msg);
        }
    }


    protected void DDListLecturerFaculty_Changed(object sender, EventArgs e)
    {
        loadDept(DDListFaculty.Text.Trim());
    }
    //DDListDept_Changed
    protected void DDListDept_Changed(object sender, EventArgs e)
    {
        //loadDept();
        DDListCourseCode.Items.Clear();
        LoadCouseCodes(DDListFaculty.Text.Trim(), DDListDept.Text, DDListLevel.Text);
    }
    //DDListCourseCode_Changed
    protected void DDListCourseCode_Changed(object sender, EventArgs e)
    {
        populategrd();
    }

    private void populategrd()
    {
        try
        {

            GridView1.DataSource = null;
            GridView1.DataBind();

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            string query1 = "select [Srn], [DocumentCategory] as [Course code],[DocumentName] as [Title],[PostedDate] as [Posted Date],[DownLoadCnt] as [DownLoad Qty],[ContentDescription] as [Description],[FilePath] FROM [CourseMaterial] where DocumentCategory='" + DDListCourseCode.Text.Trim() + "'";
            SqlDataAdapter dat = new SqlDataAdapter(query1, cnn);

            dat.Fill(ds);

            GridView1.DataSource = ds;
            Session["ds2"] = ds;

            GridView1.DataBind();
            GridView1.Width = 1000;
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    formatGridview();
            //}
            GridView1.Caption = "Total = " + " " + ds.Tables[0].Rows.Count.ToString();
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
    private void PerformUpdate(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

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
    protected void grdViewStatustory_OnRowEditing(Object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;


            populategrd();// DisplayGrid();
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = true;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = false;
            row.Cells[6].Enabled = true;
            row.Cells[7].Enabled = false;

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
            GridView1.ToolTip = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[1].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[1].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[2].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[2].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[3].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[3].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[4].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[4].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[5].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[5].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[6].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[6].Controls[0])).Text.Trim();
            GridView1.Rows[e.RowIndex].Cells[7].Text = ((System.Web.UI.WebControls.TextBox)(row.Cells[7].Controls[0])).Text.Trim();



            int srn = int.Parse(row.Cells[1].Text.Trim());
            string Title = row.Cells[3].Text.Trim().Replace("'", "''").Replace("&", "and"); ;
            string Description = row.Cells[6].Text.Trim().Replace("'", "''").Replace("&", "and");


            GridView1.EditIndex = -1;
            //update
            string qry = "UPDATE [CourseMaterial] SET [DocumentName] = '" + Title + "', [ContentDescription]='" + Description + "' where [srn] = " + srn + "";
            PerformUpdate(qry);
            populategrd();
            row.Cells[1].Enabled = false;
            row.Cells[2].Enabled = false;
            row.Cells[3].Enabled = false;
            row.Cells[4].Enabled = false;
            row.Cells[5].Enabled = false;
            row.Cells[6].Enabled = false;
            row.Cells[7].Enabled = false;
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
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
            int srn = int.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text);
            obj1 = GridView1.Rows[e.RowIndex].Cells[7].Text;//path
            //obj2 = GridView1.Rows[e.RowIndex].Cells[2].Text;

            string qry = "";


            qry = "Delete from [CourseMaterial] where [Srn] = " + srn + "";

            PerformUpdate(qry);
            System.IO.File.Delete(obj1);
            populategrd();
            


        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            showmassage(msg);
            //LogError(msg, "Payroll", "");
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
                    LoadCouseCodes(Fact, DDListDept.Text, DDListLevel.Text);
                }
                else
                {
                    DDListDept.Items.Clear();
                    DDListCourseCode.Items.Clear();
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

    private void LoadCouseCodes(string Fact, string Dept, string Level)
    {
        try
        {
            DDListCourseCode.Items.Clear();
            //DepartmentID = new Hashtable();

            try
            {
                DataSet ds = new DataSet();
                string qry = "SELECT distinct [CourseCode] FROM [Courses] where [FacultyName]='" + Fact + "' and [DepartmentName] ='" + Dept + "' and [AcademicLevel] ='" + Level + "' order by [CourseCode] asc";

                ds = SearchData(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                    {
                        DDListCourseCode.Items.Add(ds.Tables[0].Rows[jj][0].ToString());
                    }
                    populategrd();
                }
                else
                {
                    DDListCourseCode.Items.Clear();
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
    private void UploadHandler(object data)
    {

        CFileUploading fupl = (CFileUploading)data;
        SaveFile(fupl.FUpload, fupl.TicketID, fupl.ControlNumber);

        while (m_isApplicationTerminating == false)
        {
            Thread.Sleep(2000);
        }
    }
    private void SaveFile(FileUpload fileupload, string ticketID, int controlNumber)
    {


        ////try
        ////{
        ////    CAttachments att = new CAttachments();
        ////    Boolean fileOK = false;
        ////    String path = Server.MapPath("~/" + strFileGalleryFolder.Replace("/", "") + "/");
        ////    if (fileupload.HasFile)
        ////    {


        ////        String fileExtension =
        ////            System.IO.Path.GetExtension(fileupload.FileName).ToLower();
        ////        att.FileExtention = fileExtension;
        ////        if (allowedExtensions.Contains(fileExtension))
        ////        {

        ////            fileOK = true;

        ////        }
        ////        else
        ////        {
        ////            bulletError.Items.Add("Files with extention '" + fileExtension + "' is not configured to be allowed.");
        ////            m_threadCount--;
        ////        }


        ////    }

        ////    if (fileOK)
        ////    {
        ////        try
        ////        {

        ////            string finalFilePath = Path.Combine(path, "case" + ticketID + fileupload.FileName);


        ////            att.FilePath = finalFilePath; // Store the file attributes
        ////            att.FileSize = fileupload.PostedFile.ContentLength;
        ////            att.ContentType = fileupload.PostedFile.ContentType;

        ////            filePaths.Add(att);
        ////            m_threadCount--;
        ////            fileupload.PostedFile.SaveAs(finalFilePath);

        ////            bulletError.Items.Add("Sccessfully posted " + fileupload.FileName + "  [" + fileupload.PostedFile.ContentLength.ToString("#,###,###") + " bytes");

        ////        }
        ////        catch (Exception ex)
        ////        {

        ////            bulletError.Items.Add("Error with FileUpload# " + controlNumber.ToString() + ":" + ex.Message);

        ////        }
        ////    }



        ////    //    }


        ////    //}
        ////}
        ////catch (Exception ex)
        ////{

        ////    //logger.Info(ex.Message);
        ////}

    }

    private ArrayList GetAllowedExtentions()
    {
        ArrayList ar = new ArrayList();

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select  distinct [Extention]  from  [Content_FileExtentions]", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                while (dr.Read() == true)
                {
                    ar.Add(dr.GetString(0).ToLower());
                }

            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //logger.Info(ex.Message);
        }
        return ar;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DDListCourseCode.Items.Clear();
        LoadCouseCodes(DDListFaculty.Text.Trim(), DDListDept.Text, DDListLevel.Text);
    }
}
