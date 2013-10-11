using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Configuration;
using log4net;
using log4net.Config;




/// <summary>
/// Summary description for UploadCases
/// </summary>
public class UploadCases
{
    public static StringBuilder regerrors = null;
    public static string RegNotFound = "";
    public static string msg = "";
    public static string msgs = "";
    private static string CompletedFile = ConfigurationManager.AppSettings.Get("ProcessedFile");
    private static string ErrorFilePath = ConfigurationManager.AppSettings.Get("ErrorFile");
    private static string str = ConfigurationManager.AppSettings.Get("conn");
    private static SqlConnection cnn = null;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private static int Gencnt = 0;
    private static int TotalRows = 0;
    private static string AddmissionSucc = "Admitted";
    private static string RegInt = "\\d+";



    public static bool TreatXLSEntranceExam(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();

        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following applicants could not be uploaded from the submitted Entrance Exam Result list:");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted admission list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (Directory.Exists(path1))
        {
            destinationPath = Path.Combine(path1, fileN);
        }
        else
        {
            Directory.CreateDirectory(path1);
            destinationPath = Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (Directory.Exists(path2))
        {
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }
        else
        {
            Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;
            string lastmatno = "";
            int MatConsum = 0;
            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";


                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";
                //ring strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);


                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }

                    Gencnt++;
                    stdat = new Students
                    {
                        //FormNumber
                        RegNo = oDr1.GetValue(0).ToString(),
                        //General Paper
                        AdmittedSession = oDr1.GetValue(1).ToString(),
                        //General Paper Score
                        AdmissionStatus = oDr1.GetValue(2).ToString(),
                        //Subject 1
                        Faculty = oDr1.GetValue(3).ToString(),
                        //Subject 1 Score
                        Department = oDr1.GetValue(4).ToString(),
                        //Subject 2
                        CourseOfStudy = oDr1.GetValue(5).ToString(),
                        //Subject 2 Score
                        OperationType = oDr1.GetValue(6).ToString(),

                       
                    };

                     //TotalScore
                    var total = int.Parse(stdat.AdmissionStatus) + int.Parse(stdat.Department) +
                                int.Parse(stdat.OperationType);
                    stdat.AcademicLevel = total.ToString();

                   var qry = "INSERT INTO ENTRANCERAWSCORES (FormNumber,EntranceSubject1,EntranceSubject1Score,EntranceSubject2" + 
                               ",EntranceSubject2Score,EntranceSubject3,EntranceSubject3Score,TotalScore) VALUES('" + stdat .RegNo + "','" +
                               stdat.AdmittedSession + "','" + stdat.AdmissionStatus + "','" + stdat.Faculty + "','" + stdat.Department
                               + "','" + stdat.CourseOfStudy + "','" + stdat.OperationType + "','" + stdat.AcademicLevel + "')";

                    if (!PerformInsert2(qry))
                    {
                        msgs = msgs + stdat.RegNo + " Result Failed To Upload" + Environment .NewLine ;
                        continue;
                    }

                    cnt++;
                    MatConsum++;
                 
                    Gencnt++;
                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {
                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of data uploaded in submitted Entrance Exam Result list: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            
            log.Info(msg);
           


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSAmitted(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();

        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following applicants could not be uploaded from the submitted admission list:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Entry mode = " + datInfo.EntryMode.ToUpper() + " , Course of study = " + datInfo.CourseOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Session =" + datInfo.Session + " ,Level =" + datInfo.StartLevel + " ,Uploaded by = " + datInfo.Uploader.ToUpper());
        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted admission list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (Directory.Exists(path2))
        {
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }
        else
        {
            Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;
            string lastmatno = "";
            int MatConsum = 0;
            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";


                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";
                //ring strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);


                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }

                    Gencnt++;
                    stdat = new Students
                                {
                                    AdmittedSession = datInfo.Session,
                                    AdmissionStatus = AddmissionSucc.ToUpper().Replace("'", "''"),
                                    CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''"),
                                    Faculty = datInfo.Faculty,
                                    Department = datInfo.Dept,
                                    CourseOfStudy = datInfo.CourseOfStudy,
                                    OperationType = datInfo.OperationType,
                                    DepartmentID = datInfo.DeptId,
                                    FacultyID = datInfo.FacultyId,
                                    CourseOfStudyID = datInfo.CourseOfStudyId,
                                    Programme = datInfo.Programme,
                                    Honours = datInfo.Honours,
                                    Duration = datInfo.Duration,
                                    ModeOfStudy = datInfo.ModeOfStudy,
                                    AdmissionType = datInfo.AdmisssionType,
                                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                    Batch = datInfo.Batch,
                                    EntryMode = datInfo.EntryMode,
                                    AdmittedLevel = datInfo.StartLevel,
                                    RegNo = oDr1.GetValue(0).ToString()
                                };

                    stdat.MatricNumber = stdat.RegNo;
                    stdat.IsIndigene = oDr1.IsDBNull(2) ? "NO" : oDr1.GetValue(1).ToString();
                    stdat.IsIndigene = stdat.IsIndigene.ToLower().Contains("yes") ? "1" : "0";

                    if (!UpdateDBAdmitted(stdat)) continue;
                    cnt++;
                    MatConsum++;
                    //lastmatno = stdat.MatricNumber ;
                    Gencnt++;
                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {
                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of data uploaded in submitted admitted list: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of data uploaded in submitted admitted list: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Admitted student upload reports";
                string Heading = "Admitted student upload reports";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSMatNo(UploadDataInfo datInfo)
    {
        bool succ = false;


        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following students with matric numbers could not be uploaded:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Uploaded by = " + datInfo.Uploader.ToUpper());
        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (Directory.Exists(path1))
        {
            destinationPath = Path.Combine(path1, fileN);
        }
        else
        {
            Directory.CreateDirectory(path1);
            destinationPath = Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (Directory.Exists(path2))
        {
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }
        else
        {
            Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;

            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

            OleDbConnection objConn = null;


            objConn = new OleDbConnection(strConn);
            objConn.Open();

            Console.WriteLine("Xls file opened successfully");
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
                msg = "Empty table submitted " + datInfo.CurrentFile;
                log.Error(msg);
                return succ;
            }

            //String[] excelSheets = new String[dt.Rows.Count];
            int Rows = 0;
            Rows = dt.Rows.Count;
            //String[] excelSheets1 = new String[dt.Columns.Count];
            int col = dt.Columns.Count;


            Gencnt = 0;
            //TotalRows++;
            TotalRows = 0;
            TotalRows = Rows;
            while (oDr1 != null && oDr1.Read())
            {
                if (oDr1.IsDBNull(0))
                {
                    break;
                }
                Gencnt++;
                var stdat = new Students
                                {
                                    AdmittedSession = datInfo.Session,
                                    AdmissionStatus = AddmissionSucc.ToUpper(),
                                    CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''"),
                                    Faculty = datInfo.Faculty,
                                    Department = datInfo.Dept,
                                    CourseOfStudy = datInfo.CourseOfStudy,
                                    OperationType = datInfo.OperationType,
                                    DepartmentID = datInfo.DeptId,
                                    FacultyID = datInfo.FacultyId,
                                    CourseOfStudyID = datInfo.CourseOfStudyId,
                                    Programme = datInfo.Programme,
                                    Honours = datInfo.Honours,
                                    Duration = datInfo.Duration,
                                    ModeOfStudy = datInfo.ModeOfStudy,
                                    AdmissionType = datInfo.AdmisssionType,
                                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                    AdmittedLevel = datInfo.StartLevel
                                };

                if (oDr1.IsDBNull(0))
                {
                    //break;
                }
                else
                {
                    stdat.RegNo = oDr1.GetValue(0).ToString();
                }

                if (oDr1.IsDBNull(1))
                {
                    //break;
                }
                else
                {
                    stdat.MatricNumber = oDr1.GetValue(1).ToString();
                }

                stdat.Surname = oDr1.IsDBNull(1) ? "NONE" : oDr1.GetValue(2).ToString();

                if (!UpdateDBMatNo(stdat)) continue;
                cnt++;

                Gencnt++;
            }

            if (oDr1 != null) oDr1.Dispose();
            oCmd1.Dispose();
            objConn.Dispose();
            objConn.Close();

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {

                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of matric numbers uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of matric numbers uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Matriculation Numbers";
                string Heading = "Matriculation Numbers";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;

    }

    public static bool TreatXLSAmittedNoApp(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following migrated students could not be uploaded from the submitted admission list:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Entry mode = " + datInfo.EntryMode.ToUpper() + " , Course of study = " + datInfo.CourseOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Session = " + datInfo.Session + " ,Level =" + datInfo.StartLevel + " ,Uploaded by = " + datInfo.Uploader.ToUpper());

        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted admission list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;
            string lastmatno = "";
            int MatConsum = 0;
            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";


                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";
                //ring strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @CurrentFile + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);


                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        //stdat.AdmissionStatus = AddmissionSucc.ToUpper().Replace("'", "''");
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        //stdat.MatricNo =;// "NONE" + TmMatno[MatConsum].ToString();
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        stdat.Batch = datInfo.Batch;
                        stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;

                        stdat.RegNo = oDr1.GetValue(0).ToString().Trim().Replace("'", "''"); ;
                        stdat.Surname = oDr1.GetValue(1).ToString().Replace("'", "''"); ;
                        stdat.OtherNames = oDr1.GetValue(2).ToString().Replace("'", "''") + " " + oDr1.GetValue(3).ToString().Replace("'", "''");
                        stdat.Sex = oDr1.GetValue(4).ToString().Replace("'", "''");
                        //  marital status
                        stdat.MaritalStatus = oDr1.GetValue(5).ToString().Replace("'", "''");
                        //email
                        stdat.Email = oDr1.GetValue(6).ToString().Replace("'", "''"); ;
                        //phone
                        stdat.PhoneNumber = oDr1.GetValue(7).ToString().Replace("'", "''"); ;


                        //Nationality
                        stdat.Nationality = oDr1.GetValue(8).ToString().Replace("'", "''"); ;
                        //State
                        stdat.State = oDr1.GetValue(9).ToString().Replace("'", "''"); ;
                        //Lga
                        stdat.LocalGovernmentArea = oDr1.GetValue(10).ToString().Trim().Replace("'", "''"); ;
                        stdat.HomeAddress = oDr1.GetValue(11).ToString().Replace("'", "''"); ;
                        stdat.TeachingSubject = oDr1.GetValue(12).ToString().Replace("'", "''"); ;
                        //remarks
                        stdat.AdmissionStatus = oDr1.GetValue(13).ToString().Replace("'", "''"); ;
                        stdat.Remark = oDr1.GetValue(14).ToString().Replace("'", "''"); ;
                        stdat.DateOfBirth = oDr1.GetValue(15).ToString().Replace("'", "''"); ;


                        stdat.MatricNumber = stdat.RegNo.Trim().Replace("'", "''"); ;



                        if (UpdateDBAdmittedNoAppFm(stdat))
                        {
                            cnt++;
                            MatConsum++;
                            lastmatno = stdat.MatricNumber;
                            Gencnt++;
                        }
                        else
                        {
                            //File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                            //DisposeSheet();
                            //return succ;
                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {
                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of data uploaded in submitted admitted list: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of data uploaded in submitted admitted list: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Admitted student upload reports";
                string Heading = "Admitted student upload reports";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSCarryOvers(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following carry over students with matric numbers could not be uploaded:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Semester = " + datInfo.Semester.ToUpper() + " , Level = " + datInfo.StartLevel.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Uploaded by = " + datInfo.Uploader.ToUpper());
        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;

            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                //ArrayList TmMatno = new ArrayList();
                //TmMatno = GeneratTempMatNo(Rows);

                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;

                string CosCode = "";


                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {


                        CosCode = "";
                        Gencnt++; ////// not correct
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = AddmissionSucc.ToUpper();// "GRADUATED";
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //stdat.Batch = datInfo.Batch;
                        //stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;
                        //rws = oDr1.GetValue(0).ToString();
                        //Names = oDr1.GetValue(1).ToString();
                        //indigen = oDr1.GetValue(2).ToString();
                        // "NONE" + TmMatno[MatConsum].ToString();//matno

                        if (oDr1.IsDBNull(0))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.MatricNumber = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {

                        }
                        else
                        {
                            CosCode = oDr1.GetValue(2).ToString();
                        }




                        if (UpdateDBCarryOver(stdat, CosCode, datInfo.Semester))
                        {
                            cnt++;
                            //MatConsum++;
                            //lastmatno = stdat.RegNo;
                            Gencnt++;
                        }

                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {

                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of carry over students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of carry over students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Carry Over Students";
                string Heading = "Carry Over Students";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following carry over course students could not be uploaded:" + " " + RegNotFound + Environment.NewLine;
                    msgs = msgbody;
                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString() + Environment.NewLine);
                    msgs += "Successfull upload is " + cnt.ToString() + "," + " out of total records " +
                            TotalRows.ToString() + Environment.NewLine;
                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);
                }
            }
        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSGraduatedStudents(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following Graduated students could not be uploaded:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Course of study = " + datInfo.CourseOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Session =" + datInfo.Session + " ,Uploaded by = " + datInfo.Uploader.ToUpper());

        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;

            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
                OleDbCommand oCmd1 = null;
                oCmd1 = new OleDbCommand("select * from [" + Currsheet + "]", objConn);
                OleDbDataReader oDr1 = oCmd1.ExecuteReader();
                //DataTable dt = new DataTable();
                //// Get the data table containg the schema guid.
                //dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                OleDbDataAdapter dat = null;
                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);
                dt = ds.Tables[0];

                if (dt == null)
                {
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);



                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = "GRADUATED";
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.MatricNumber = oDr1.GetValue(0).ToString();
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //stdat.Batch = datInfo.Batch;
                        //stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;
                        //rws = oDr1.GetValue(0).ToString();
                        //Names = oDr1.GetValue(1).ToString();
                        //indigen = oDr1.GetValue(2).ToString();
                        stdat.RegNo = stdat.MatricNumber;// "NONE" + TmMatno[MatConsum].ToString();//matno
                        if (oDr1.IsDBNull(1))
                        {
                            stdat.Surname = "NONE";
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {
                            stdat.OtherNames = "NONE";
                        }
                        else
                        {
                            stdat.OtherNames = oDr1.GetValue(2).ToString();
                        }


                        if (UpdateDBGraduated(stdat))
                        {
                            cnt++;
                            //MatConsum++;
                            //lastmatno = stdat.RegNo;
                            Gencnt++;
                        }
                        else
                        {

                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {

                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of graduated students data uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of graduated students data uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Graduated Students";
                string Heading = "Graduated Students";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSOldstudentspromotedtonewlevel(UploadDataInfo datInfo)
    {
        //Students to new Level
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following old students could not be moved to thier respective new Levels:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Course of study = " + datInfo.CourseOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Session =" + datInfo.Session + " ,Level =" + datInfo.StartLevel + " ,Uploaded by = " + datInfo.Uploader.ToUpper());

        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;
            string lastmatno = "";
            int MatConsum = 0;
            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
                OleDbCommand oCmd1 = null;
                oCmd1 = new OleDbCommand("select * from [" + Currsheet + "]", objConn);
                OleDbDataReader oDr1 = oCmd1.ExecuteReader();
                ////DataTable dt = new DataTable();
                ////// Get the data table containg the schema guid.
                ////dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                OleDbDataAdapter dat = null;
                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);
                dt = ds.Tables[0];

                if (dt == null)
                {
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);

                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = AddmissionSucc.ToUpper().Replace("'", "''");
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.MatricNumber = oDr1.GetValue(0).ToString();
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //stdat.Batch = datInfo.Batch;
                        //stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;
                        //rws = oDr1.GetValue(0).ToString();
                        //Names = oDr1.GetValue(1).ToString();
                        //indigen = oDr1.GetValue(2).ToString();
                        stdat.RegNo = stdat.MatricNumber;// "NONE" + TmMatno[MatConsum].ToString();//matno
                        if (oDr1.IsDBNull(1))
                        {
                            stdat.Surname = "NONE";
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {
                            stdat.OtherNames = "NONE";
                        }
                        else
                        {
                            stdat.OtherNames = oDr1.GetValue(2).ToString();
                        }


                        if (UpdateDBOdStudentsLevel(stdat))
                        {
                            cnt++;
                            //MatConsum++;
                            //lastmatno = stdat.RegNo;
                            Gencnt++;
                        }
                        else
                        {

                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {

                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();
                succ = true;
            }

            msg = "Total number of old students data uploaded for new academic Level: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of old students data uploaded for new academic Level: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Old students new level";
                string Heading = "Old students new level";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }
            // succ = true;

        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSOldstudentsnewtoportal(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following old students could not be uploaded from the submitted list:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Course of study = " + datInfo.CourseOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Session =" + datInfo.Session + " ,Level =" + datInfo.StartLevel + " ,Uploaded by = " + datInfo.Uploader.ToUpper());

        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;
            string lastmatno = "";
            int MatConsum = 0;
            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
                OleDbCommand oCmd1 = null;
                oCmd1 = new OleDbCommand("select * from [" + Currsheet + "]", objConn);
                OleDbDataReader oDr1 = oCmd1.ExecuteReader();
                ////DataTable dt = new DataTable();
                ////// Get the data table containg the schema guid.
                ////dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                OleDbDataAdapter dat = null;
                dat = new OleDbDataAdapter("select * from [" + Currsheet + "]", objConn);
                dat.Fill(ds);
                dt = ds.Tables[0];

                if (dt == null)
                {
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                ArrayList TmMatno = new ArrayList();
                TmMatno = GeneratTempMatNo(Rows);


                string email = "";
                string Phone = "";

                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                while (oDr1.Read())
                {

                    email = "";
                    Phone = "";


                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = AddmissionSucc.ToUpper().Replace("'", "''");
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.MatricNumber = oDr1.GetValue(0).ToString();
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //stdat.Batch = datInfo.Batch;
                        //stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;
                        //rws = oDr1.GetValue(0).ToString();
                        //Names = oDr1.GetValue(1).ToString();
                        //indigen = oDr1.GetValue(2).ToString();
                        stdat.RegNo = stdat.MatricNumber;// "NONE" + TmMatno[MatConsum].ToString();//matno
                        if (oDr1.IsDBNull(1))
                        {
                            stdat.Surname = "NONE";
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {
                            stdat.OtherNames = "NONE";
                        }
                        else
                        {
                            stdat.OtherNames = oDr1.GetValue(2).ToString();
                        }

                        if (oDr1.IsDBNull(3))
                        {
                            stdat.Sex = "NONE";
                        }
                        else
                        {
                            stdat.Sex = oDr1.GetValue(3).ToString();
                        }
                        if (oDr1.IsDBNull(4))
                        {
                            stdat.Country = "NONE";
                        }
                        else
                        {
                            stdat.Country = oDr1.GetValue(4).ToString();
                        }
                        if (oDr1.IsDBNull(5))
                        {
                            stdat.State = "NONE";
                        }
                        else
                        {
                            stdat.State = oDr1.GetValue(5).ToString();
                        }

                        if (oDr1.IsDBNull(6))
                        {
                            email = "NONE";
                        }
                        else
                        {
                            email = oDr1.GetValue(6).ToString();
                        }

                        if (oDr1.IsDBNull(7))
                        {
                            Phone = "NONE";
                        }
                        else
                        {
                            Phone = oDr1.GetValue(7).ToString();
                        }

                        //if name != null
                        if (UpdateDBOdStudents(stdat, email, Phone))
                        {
                            cnt++;
                            MatConsum++;
                            //lastmatno = stdat.RegNo;
                            Gencnt++;
                        }
                        else
                        {
                            //File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                            //DisposeSheet();
                            //return succ;
                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {

                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();
                succ = true;
            }

            msg = "Total number of old student data uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            //msgs = "Total number of old student data uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs += Environment.NewLine + "Total number of old student data uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();


            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Old students upload reports";
                string Heading = "Old students upload reports";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;
                    msgs += Environment.NewLine + "Please, be informed that the following matriculation numbers could not be uploaded:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }
            //succ = true;

        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSsummerStudents(UploadDataInfo datInfo)
    {
        bool succ = false;


        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following summer students with matric numbers could not be uploaded:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Uploaded by = " + datInfo.Uploader.ToUpper());
        regerrors.AppendLine("");


        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {

            string Currsheet = "";
            int cnt = 0;

            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;


                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;


                Students stdat = null;
                //ArrayList TmMatno = new ArrayList();
                //TmMatno = GeneratTempMatNo(Rows);

                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;
                int isscholarship = 0;
                string scholarship = "No";

                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {

                        isscholarship = 0;
                        scholarship = "No";

                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = AddmissionSucc.ToUpper();// "GRADUATED";
                        stdat.CreatedBy = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //stdat.Batch = datInfo.Batch;
                        //stdat.EntryMode = datInfo.EntryMode;
                        stdat.AdmittedLevel = datInfo.StartLevel;
                        //rws = oDr1.GetValue(0).ToString();
                        //Names = oDr1.GetValue(1).ToString();
                        //indigen = oDr1.GetValue(2).ToString();
                        // "NONE" + TmMatno[MatConsum].ToString();//matno

                        if (oDr1.IsDBNull(0))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.RegNo = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.MatricNumber = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {
                            stdat.Surname = "NONE";
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(2).ToString();
                        }

                        if (oDr1.IsDBNull(3))
                        {
                            // scholarship = "No";// stdat.Surname = "NONE";
                        }
                        else
                        {
                            scholarship = oDr1.GetValue(3).ToString();

                        }

                        if (scholarship.Trim().ToLower() == "yes")
                        {
                            isscholarship = 1;
                        }


                        if (UpdateDBScholarship(stdat, isscholarship))
                        {
                            cnt++;
                            //MatConsum++;
                            //lastmatno = stdat.RegNo;
                            Gencnt++;
                        }
                        else
                        {

                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {
                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of summer students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of summer students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Summer Students";
                string Heading = "Summer Students";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following students could not be uploaded for summer:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following students could not be uploaded for summer:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);

                }

            }


        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
        }
        return succ;
    }

    public static bool TreatXLSspillOvers(UploadDataInfo datInfo)
    {
        bool succ = false;

        RegNotFound = "";
        regerrors = new StringBuilder();
        //regerrors.AppendFormat(
        regerrors.AppendLine("");
        regerrors.AppendLine("Please, be informed that the following split over students with matric numbers could not be uploaded:");
        regerrors.AppendLine("");
        regerrors.AppendLine("Programme = " + datInfo.Programme.ToUpper() + " , Mode of study = " + datInfo.ModeOfStudy.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Semester = " + datInfo.Semester.ToUpper() + " , Level = " + datInfo.StartLevel.ToUpper());
        regerrors.AppendLine("");
        regerrors.AppendLine("Uploaded by = " + datInfo.Uploader.ToUpper());
        regerrors.AppendLine("");

        if (datInfo.CurrentFile == "")
        {
            msg = "File name not specified in submitted old students list";
            log.Error(msg);
            return succ;
        }

        string path1 = CompletedFile + "\\";
        string destinationPath = "";

        string fileN = datInfo.Filename;

        if (System.IO.Directory.Exists(path1))
        {
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path1);
            destinationPath = System.IO.Path.Combine(path1, fileN);
        }

        //Error file

        string path2 = ErrorFilePath + "\\";
        string ErrorFiledestinationPath = "";

        string ErrorfileN = datInfo.Filename;
        if (System.IO.Directory.Exists(path2))
        {
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }
        else
        {
            System.IO.Directory.CreateDirectory(path2);
            ErrorFiledestinationPath = System.IO.Path.Combine(path2, ErrorfileN);
        }

        try
        {
            string Currsheet = "";
            int cnt = 0;

            Hashtable AdminAmails = new Hashtable();
            AdminAmails = getmails();
            CSheetname sn = new CSheetname();
            Currsheet = sn.name(@datInfo.CurrentFile);
            if (Currsheet == "")
            {
                msg = "Sheet1 empty in your excel file " + datInfo.CurrentFile;
                log.Error(msg);
                DisposeSheet();
                return succ;
            }
            else
            {
                //string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES\"";
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @datInfo.CurrentFile + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";

                OleDbConnection objConn = null;

                objConn = new OleDbConnection(strConn);
                objConn.Open();

                Console.WriteLine("Xls file opened successfully");
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
                    msg = "Empty table submitted " + datInfo.CurrentFile;
                    log.Error(msg);
                    return succ;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int Rows = 0;
                Rows = dt.Rows.Count;
                String[] excelSheets1 = new String[dt.Columns.Count];
                int col = dt.Columns.Count;

                Students stdat = null;
                //ArrayList TmMatno = new ArrayList();
                //TmMatno = GeneratTempMatNo(Rows);

                Gencnt = 0;
                //TotalRows++;
                TotalRows = 0;
                TotalRows = Rows;

                string CosCode = "";

                while (oDr1.Read())
                {
                    if (oDr1.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        CosCode = "";
                        Gencnt++;
                        stdat = new Students();
                        stdat.AdmittedSession = datInfo.Session;
                        stdat.AdmissionStatus = AddmissionSucc.ToUpper();// "GRADUATED";
                        stdat.CreatedDate = datInfo.Uploader.ToUpper().Replace("'", "''");
                        stdat.Faculty = datInfo.Faculty;
                        stdat.Department = datInfo.Dept;
                        stdat.CourseOfStudy = datInfo.CourseOfStudy;
                        stdat.OperationType = datInfo.OperationType;
                        stdat.DepartmentID = datInfo.DeptId;
                        stdat.FacultyID = datInfo.FacultyId;
                        stdat.CourseOfStudyID = datInfo.CourseOfStudyId;
                        stdat.Programme = datInfo.Programme;
                        stdat.Honours = datInfo.Honours;
                        stdat.Duration = datInfo.Duration;
                        stdat.ModeOfStudy = datInfo.ModeOfStudy;
                        stdat.AdmissionType = datInfo.AdmisssionType;
                        stdat.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                        stdat.AdmittedLevel = datInfo.StartLevel;

                        if (oDr1.IsDBNull(0))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.MatricNumber = oDr1.GetValue(0).ToString();
                        }

                        if (oDr1.IsDBNull(1))
                        {
                            //break;
                        }
                        else
                        {
                            stdat.Surname = oDr1.GetValue(1).ToString();
                        }

                        if (oDr1.IsDBNull(2))
                        {

                        }
                        else
                        {
                            CosCode = oDr1.GetValue(2).ToString();
                        }

                        if (UpdateDBSpillOver(stdat, CosCode, datInfo.Semester))
                        {
                            cnt++;
                            Gencnt++;
                        }
                    }

                }

                oDr1.Dispose();
                oCmd1.Dispose();
                objConn.Dispose();
                objConn.Close();
            }

            if (cnt == 0)
            {
                File.Delete(ErrorFiledestinationPath);
                File.Move(datInfo.CurrentFile, ErrorFiledestinationPath);
                DisposeSheet();
            }
            else
            {
                File.Delete(destinationPath);
                File.Move(datInfo.CurrentFile, destinationPath);
                DisposeSheet();

                succ = true;
            }

            msg = "Total number of spill over students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();
            msgs = "Total number of spill over students uploaded: " + " " + datInfo.Filename + " " + " on " + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "=" + " " + cnt.ToString();

            log.Info(msg);

            if (RegNotFound != "")
            {
                string Subject = "Splill Over Students";
                string Heading = "Splill Over Students";
                string Attached = "";

                foreach (DictionaryEntry mail in AdminAmails)
                {
                    string email = mail.Key.ToString();
                    string name = mail.Value.ToString();

                    //String Mheader = "Reg numbers not found";
                    String msgbody = "Please, be informed that the following splill over students could not be uploaded:" + " " + RegNotFound;
                    msgs = "Please, be informed that the following splill over students could not be uploaded:" + " " + RegNotFound;

                    regerrors.AppendLine("");
                    regerrors.AppendLine("Successfull upload is " + cnt.ToString() + "," + " out of total records " + TotalRows.ToString());

                    sendGenMail(email, regerrors.ToString(), Subject, Heading, Attached, name);
                }
            }
        }
        catch (Exception ex)
        {
            DisposeSheet();
            msg = ex.Message;
            Console.WriteLine(msg);
            log.Error(msg);
            //Logmsg(msg);
            throw;
        }
        return succ;
    }

    private static bool UpdateDBScholarship(Students stdat, int isscholarship)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                string qry = "";
                //if (stdat.RegNo != "" && stdat.MatricNo != "")
                //{
                //
                qry = "SELECT [Email],[PhoneNumber] FROM [Students] WHERE [RegNo]='" + stdat.RegNo + "' or [MatricNumber] = '" + stdat.MatricNumber + "' and [AdmissionStatus]='ADMITTED' and [Programme] = '" + stdat.Programme + "' and [ModeOfStudy]='" + stdat.ModeOfStudy + "' and [AcademicLevel] ='" + stdat.AdmittedLevel + "'";
                cmd = new SqlCommand(qry, cnn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr.IsDBNull(0) == false)
                    {
                        email = dr.GetString(0);
                    }
                    else
                    {
                        email = "None";
                    }

                    if (dr.IsDBNull(1) == false)
                    {
                        phone = dr.GetString(1);
                    }
                    else
                    {
                        phone = "None";
                    }

                    dr.Dispose();
                    cmd.Dispose();

                    qry = "Delete FROM [SummerSchool] where [RegNo]='" + stdat.RegNo + "' or [MatricNumber] = '" + stdat.MatricNumber + "' and [SessionName] ='" + stdat.AdmittedSession + "'";
                    PerformDelete(qry);

                    qry = "INSERT INTO [SummerSchool]([MatricNo],[RegNo],[SessionName],[AcademicLevel],[HasRegistered],[Scholarship],[CreateDate]) VALUES ('" + stdat.MatricNumber + "','" + stdat.RegNo + "','" + stdat.AdmittedSession + "','" +
                        stdat.AdmittedLevel + "',0," + isscholarship + ",'" + stdat.CreatedDate + "')";
                    PerformInsert(qry);

                    Subject = "Petroleum Training Institute Summer School Notice";
                    Heading = "Petroleum Training Institute Summer School Notice:";
                    Attached = "";
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendLine("");
                    sb2.AppendLine("This is to bring to your notice that you will be part taking in" + " " + stdat.AdmittedSession + " academic session's summer school,");
                    sb2.AppendLine("");
                    sb2.AppendLine("Kindly go to the school portal to register your summer courses");
                    sb2.AppendLine("");


                    string name = stdat.Surname + " " + stdat.OtherNames;
                    sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                    rtn = true;

                }
                else
                {
                    dr.Dispose();
                    cmd.Dispose();
                    msg1 = "Student by name " + stdat.Surname + "," + "with  matno" + " " + stdat.MatricNumber + " " + "could not be found";
                }
                //}
                //else
                //{
                //    msg1 = "Incomplete data for student with matno " + stdat.MatricNo;
                //}

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
            }

        }
        return rtn;
    }

    private static bool UpdateDBOdStudents(Students stdat, string email, string Phone)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";
        string temppassword = "";


        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {

                StringBuilder sb = new StringBuilder();

                if (stdat.Surname.ToLower() != "none")
                {
                    string qry = "";
                    qry = "SELECT * FROM [Students] WHERE [MatricNumber]='" + stdat.MatricNumber + "'";
                    if (!Existed(qry))
                    {
                        sb.Append("INSERT INTO  [Students] ");
                        sb.Append("([Surname]");//1
                        sb.Append(" ,[OtherNames]");//2
                        sb.Append(" ,[MaidenName]");//3
                        sb.Append(",[Sex]");//4
                        sb.Append(",[DateOfBirth]");//5
                        sb.Append(",[MaritalStatus]");//6
                        sb.Append(",[Nationality]");//7
                        sb.Append(",[State]");//8
                        sb.Append(",[LocalGovernmentArea]");//9
                        sb.Append(",[MatricNumber]");//10
                        sb.Append(",[DepartmentID]");//11
                        sb.Append(",[PlaceOfBirth]");//12
                        sb.Append(",[HomeAddress]");//13
                        sb.Append(",[ResidentialAddress]");//14
                        sb.Append(",[RoomNo]");//15
                        sb.Append(" ,[Country]");//16
                        sb.Append(",[Email]");//17
                        sb.Append(",[PhoneNumber]");//18
                        sb.Append(",[SponsorName]");//19
                        sb.Append(",[SponsorAddress]");//20
                        sb.Append(",[SponsorPhone]");//21
                        sb.Append(" ,[AdmissionStatus]");//22
                        sb.Append(" ,[AcademicLevel]");//23
                        sb.Append(",[RegNo]");//24
                        sb.Append(",[FacultyID]");//25
                        sb.Append(",[CreatedDate]");//26
                        sb.Append(",[CreatedBy]");//27
                        sb.Append(",[CourseOfStudyID]");//28
                        sb.Append(",[PassportFile]");//29
                        sb.Append(",[LocalPassportFile]");//30
                        sb.Append(",[AdmittedSession]");//31
                        sb.Append(",[Programme]");//32
                        sb.Append(",[Duration]");//33
                        sb.Append(",[Honours]");//34
                        sb.Append(",[ModeOfStudy]");//35
                        sb.Append(",[PresentSession]");//36
                        sb.Append(",[AdmittedLevel]");//37
                        sb.Append(",[Title]");//38
                        sb.Append(",[Repeating]");//39
                        sb.Append(",[IsIndigene]");//40
                        sb.Append(",[isNewStudent]");//41
                        sb.Append(",[isEvening]");//42
                        sb.Append(",[CanRegister]");//43
                        sb.Append(",[CanRegister2]");//44
                        sb.Append(",[TempEmailPwd]");//45
                        sb.Append(",[MatricSerial]");//46
                        sb.Append(",[Center]");//47
                        sb.Append(",[CourseOfStudyName]");//48
                        sb.Append(",[EntryMode]");//49                            
                        sb.Append(",[CanChangePassport])");//50
                        sb.Append("VALUES");
                        sb.Append("('" + stdat.Surname + "'");//1
                        sb.Append(",'" + stdat.OtherNames + "'");//2
                        sb.Append(",'None'");//3
                        sb.Append(",'" + stdat.Sex + "'");//4
                        sb.Append(",'None'");//5
                        sb.Append(",'None'");//6
                        sb.Append(",'" + stdat.Country + "'");//7
                        sb.Append(",'" + stdat.State + "'");//8
                        sb.Append(",'None'");//9
                        sb.Append(",'" + stdat.MatricNumber + "'");//10
                        sb.Append("," + stdat.DepartmentID);//11
                        sb.Append(",'None'");//12
                        sb.Append(",'None'");//13
                        sb.Append(",'None'");//14//ResidentialAddress
                        sb.Append(",'None'");//rm number//15
                        sb.Append(",'" + stdat.Country + "'");//16
                        sb.Append(",'" + email + "'");//17
                        sb.Append(",'" + Phone + "'");//18
                        sb.Append(",'None'");//19
                        sb.Append(",'None'");//20
                        sb.Append(",'None'");//21
                        sb.Append(",'" + stdat.AdmissionStatus + "'");//22
                        sb.Append(",'" + stdat.AdmittedLevel + "'");//23
                        sb.Append(",'" + stdat.MatricNumber + "'");//24
                        sb.Append(",'" + stdat.FacultyID + "'");//25
                        sb.Append(",'" + stdat.CreatedDate + "'");//26
                        sb.Append(",'" + stdat.CreatedBy + "'");//27
                        sb.Append(",'" + stdat.CourseOfStudyID + "'");//28
                        sb.Append(",'" + stdat.RegNo + "'");//29//temp matno//pasport
                        sb.Append(",'" + stdat.RegNo + "'");//30
                        sb.Append(",'" + stdat.AdmittedSession + "'");//31
                        sb.Append(",'" + stdat.Programme + "'");//32
                        sb.Append(",'" + stdat.Duration + "'");//33
                        sb.Append(",'" + stdat.Honours + "'");//34
                        sb.Append(",'" + stdat.ModeOfStudy + "'");//35
                        sb.Append(",'" + stdat.AdmittedSession + "'");//36
                        sb.Append(",'" + stdat.AdmittedLevel + "'");//37
                        sb.Append(",'None'");//38
                        sb.Append(",'No'");//repeating//39
                        sb.Append(",'No'");//40 stdat.Indigen
                        sb.Append(",'0'");//new student//41
                        sb.Append(",'0'");//evening//42
                        sb.Append(",'0'");//can reg1//43
                        sb.Append(",'0'");//can reg2//44
                        sb.Append(",'None'");//temp email passw//45
                        //string serail = stdat.RegNo.Trim().Replace("NONE", "").Trim();
                        int srn = 1;// int.Parse(serail);
                        sb.Append("," + srn + "");//46//matserial
                        sb.Append(",'None'");//47
                        sb.Append(",'" + stdat.CourseOfStudy + "'");//48
                        sb.Append(",'None'");//49
                        sb.Append("," + 1 + ")");//can change passport//50

                        temppassword = "default";// +srn.ToString();
                        qry = "SELECT * FROM [StudentSignOn] where [RegNo] = '" + stdat.MatricNumber +
                            "' or [MatricNumber] = '" + stdat.MatricNumber + "'";

                        if (!Existed(qry))
                        {

                            PerformInsert(sb.ToString());

                            //insert into student signon
                            qry = "INSERT INTO [StudentSignOn]([MatricNumber],[RegNo],[Password],[Email],[Phone],Status) VALUES ('" +
                                stdat.MatricNumber + "','" + stdat.MatricNumber + "','" + temppassword + "','" + email + "','" + Phone + "',1)";
                            PerformInsert(qry);


                            Subject = "Petroleum Training Institute Old Students upload";
                            Heading = "Petroleum Training Institute Students upload: " + stdat.AdmittedSession + " " + "academic session";
                            Attached = "";
                            StringBuilder sb2 = new StringBuilder();
                            sb2.AppendLine("");
                            sb2.AppendLine("This is to inform you that your data have been uploaded into Petroleum Training Institute edu-portal:");
                            sb2.AppendLine("");
                            //sb2.Append("\n");
                            sb2.AppendLine("College:" + " " + stdat.Faculty);
                            sb2.AppendLine("");
                            sb2.AppendLine("Department:" + " " + stdat.Department);
                            sb2.AppendLine("");
                            sb2.AppendLine("Course of study:" + " " + stdat.CourseOfStudy);
                            sb2.AppendLine("");
                            sb2.AppendLine("Level:" + " " + stdat.AdmittedLevel);
                            sb2.AppendLine("");
                            sb2.AppendLine("Course duration:" + " " + stdat.Duration);
                            sb2.AppendLine("");
                            sb2.AppendLine("Course Honor:" + " " + stdat.Honours);
                            sb2.AppendLine("");
                            sb2.AppendLine("Study Mode:" + " " + stdat.ModeOfStudy);
                            sb2.AppendLine("");
                            sb2.AppendLine("Programme :" + " " + stdat.Programme);
                            sb2.AppendLine("");
                            sb2.AppendLine("");
                            sb2.AppendLine("*********");
                            sb2.AppendLine("");
                            //sb2.AppendLine("You can login to the portal as an applicant with your old credentials to check your admission status and print admission letter:");
                            //sb2.AppendLine("");
                            sb2.AppendLine("Meanwhile, you can login as a student with your login credentials below or sign up on the portal and start performing student activities.");
                            sb2.AppendLine("");
                            sb2.AppendLine("Username = " + " " + stdat.MatricNumber);
                            sb2.AppendLine("");
                            sb2.AppendLine("Password = " + " " + temppassword);

                            // messg = "This is to remind you that birthday of " + " " + StaffName + " " + " of " + " " + Department + " " + "department comes up on:" + " " + dayname + ", " + bday.Value.ToString();

                            string name = stdat.Surname + " " + stdat.OtherNames;
                            sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                            rtn = true;

                        }
                        else
                        {
                            msg1 = "Student by name " + stdat.Surname + " " + stdat.Faculty + " " + "with  mat no" + " " + stdat.MatricNumber + " " + "has already signon as student";
                        }

                    }
                    else
                    {
                        msg1 = "Student by name " + stdat.Surname + " " + stdat.Faculty + " " + "with  reg no" + " " + stdat.MatricNumber + " " + "already existed as student";
                    }
                }
                else
                {
                    msg1 = "No name for student with matno " + stdat.MatricNumber + ", " + stdat.Faculty;
                }
                msgs = msg1;
                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBOdStudentsLevel(Students stdat)
    {
        bool rtn = false;
        //DataSet ds = null;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";


        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                string qry = "";
                if (stdat.Surname.ToLower() != "none")
                {
                    //,,,,,
                    qry = "SELECT [Email],[PhoneNumber] FROM [Students] WHERE [MatricNumber]='" + stdat.MatricNumber
                        + "' and [AdmissionStatus]='ADMITTED' and [FacultyID] =" + stdat.FacultyID +
                        " and [DepartmentID]=" + stdat.DepartmentID + " and [CourseOfStudyID] =" +
                        stdat.CourseOfStudyID + " and [Programme] = '" + stdat.Programme + "' and [Duration] = '" +
                        stdat.Duration + "' and [Honours]= '" + stdat.Honours + "' and [ModeOfStudy]='" +
                        stdat.ModeOfStudy + "'";
                    cmd = new SqlCommand(qry, cnn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.IsDBNull(0) == false)
                        {
                            email = dr.GetString(0);
                        }
                        else
                        {
                            email = "None";
                        }

                        if (dr.IsDBNull(1) == false)
                        {
                            phone = dr.GetString(1);
                        }
                        else
                        {
                            phone = "None";
                        }

                        dr.Dispose();
                        cmd.Dispose();

                        qry = "Update [Students] set [PresentSession] ='" + stdat.AdmittedSession +
                            "',[AcademicLevel] = '" + stdat.AdmittedLevel + "' WHERE [MatricNumber]='" +
                            stdat.MatricNumber + "' and [AdmissionStatus]='ADMITTED' ";
                        PerformUpdate(qry);

                        Subject = "Petroleum Training Institute New Academic Level";
                        Heading = "Petroleum Training Institute New Academic Level: " +
                            stdat.AdmittedSession + " " + "academic session";
                        Attached = "";
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine("");
                        sb2.AppendLine("This is to inform you of your new academic level:");
                        sb2.AppendLine("");
                        //sb2.Append("\n");
                        sb2.AppendLine("College:" + " " + stdat.Faculty);
                        sb2.AppendLine("");
                        sb2.AppendLine("Department:" + " " + stdat.Department);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course of study:" + " " + stdat.CourseOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Level:" + " " + stdat.AdmittedLevel);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course duration:" + " " + stdat.Duration);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course Honor:" + " " + stdat.Honours);
                        sb2.AppendLine("");
                        sb2.AppendLine("Study Mode:" + " " + stdat.ModeOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Programme :" + " " + stdat.Programme);
                        sb2.AppendLine("");
                        sb2.AppendLine("");
                        sb2.AppendLine("*********");
                        sb2.AppendLine("");

                        string name = stdat.Surname + " " + stdat.OtherNames;
                        sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                        rtn = true;

                    }
                    else
                    {
                        dr.Dispose();
                        cmd.Dispose();
                        msg1 = "Student by name " + stdat.Surname + " " + stdat.Faculty + " " +
                            "with  matno" + " " + stdat.MatricNumber + " " + "could not be found";
                    }
                }
                else
                {
                    msg1 = "No name for student with matno " + stdat.MatricNumber + ", " + stdat.Faculty;
                }

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBGraduated(Students stdat)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";


        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                string qry = "";
                if (stdat.Surname.ToLower() != "none")
                {
                    //,,,,,
                    qry = "SELECT [Email],[PhoneNumber] FROM [Students] WHERE [MatricNumber]='" +
                        stdat.MatricNumber + "' and [AdmissionStatus]='ADMITTED' and [FacultyID] =" + stdat.FacultyID
                        + " and [DepartmentID]=" + stdat.DepartmentID + " and [CourseOfStudyID] =" +
                        stdat.CourseOfStudyID
                        + " and [Programme] = '" + stdat.Programme + "' and [Duration] = '" +
                        stdat.Duration + "' and [Honours]= '" + stdat.Honours + "' and [ModeOfStudy]='"
                        + stdat.ModeOfStudy + "'";
                    cmd = new SqlCommand(qry, cnn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.IsDBNull(0) == false)
                        {
                            email = dr.GetString(0);
                        }
                        else
                        {
                            email = "None";
                        }

                        if (dr.IsDBNull(1) == false)
                        {
                            phone = dr.GetString(1);
                        }
                        else
                        {
                            phone = "None";
                        }

                        dr.Dispose();
                        cmd.Dispose();

                        qry = "Update [Students] set [PresentSession] ='" + stdat.AdmittedSession +
                            "',[AdmissionStatus] = '" + stdat.AdmissionStatus + "' WHERE [MatricNumber]='" +
                            stdat.MatricNumber + "'";
                        PerformUpdate(qry);

                        Subject = "Petroleum Training Institute Graduation Notice";
                        Heading = "Petroleum Training Institute Graduation Notice: " + stdat.AdmittedSession + " " +
                            "academic session";
                        Attached = "";
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine("");
                        sb2.AppendLine("Congratulation, this is to inform you that your data have been upload as graduated student:");
                        sb2.AppendLine("");
                        //sb2.Append("\n");
                        sb2.AppendLine("College:" + " " + stdat.Faculty);
                        sb2.AppendLine("");
                        sb2.AppendLine("Department:" + " " + stdat.Department);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course of study:" + " " + stdat.CourseOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Level:" + " " + stdat.Department);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course duration:" + " " + stdat.Duration);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course Honor:" + " " + stdat.Honours);
                        sb2.AppendLine("");
                        sb2.AppendLine("Study Mode:" + " " + stdat.ModeOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Programme :" + " " + stdat.Programme);
                        sb2.AppendLine("");
                        sb2.AppendLine("");
                        sb2.AppendLine("*********");
                        sb2.AppendLine("");
                        sb2.AppendLine("Meanwhile, there is need for you to make sure all the school procedures for graduating students are met ");
                        sb2.AppendLine("");

                        string name = stdat.Surname + " " + stdat.OtherNames;
                        sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                        rtn = true;

                    }
                    else
                    {
                        dr.Dispose();
                        cmd.Dispose();
                        msg1 = "Student by name " + stdat.Surname + " " + stdat.Faculty + " " + "with  matno" + " " + stdat.MatricNumber + " " + "could not be found";
                    }
                }
                else
                {
                    msg1 = "No name for student with matno " + stdat.MatricNumber + ", " + stdat.Faculty;
                }

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBCarryOver(Students stdat, string CosCode, string semester)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                int CosofStudy = 0;
                string qry = "";
                if (CosCode != "")
                {
                    qry = "SELECT * FROM [DeptCourses] WHERE [CourseCode]='" + CosCode + "'";
                    if (Existed(qry))
                    {
                        qry = "SELECT [Email],[PhoneNumber],[CourseOfStudyID] FROM [Students] WHERE [RegNo]='" + stdat.MatricNumber + "' or [MatricNumber] = '" + stdat.MatricNumber + "' and [AdmissionStatus]='ADMITTED' and [Programme] = '" + stdat.Programme + "' and [ModeOfStudy]='" + stdat.ModeOfStudy + "'";
                        cmd = new SqlCommand(qry, cnn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (dr.IsDBNull(0) == false)
                            {
                                email = dr.GetString(0);
                            }
                            else
                            {
                                email = "None";
                            }

                            if (dr.IsDBNull(1) == false)
                            {
                                phone = dr.GetString(1);
                            }
                            else
                            {
                                phone = "None";
                            }

                            if (dr.IsDBNull(2) == false)
                            {
                                CosofStudy = int.Parse(dr.GetValue(2).ToString());
                            }

                            dr.Dispose();
                            cmd.Dispose();

                            qry = "Delete FROM [CarryOver] where [MatricNo] = '" + stdat.MatricNumber + "' and [AcademicLevel] ='" + stdat.AdmittedLevel + "' and [CourseCode] ='" + CosCode + "'";//and [Semester] ='" + semester + "'
                            PerformDelete(qry);

                            qry = "INSERT INTO [CarryOver]([MatricNo],[FullName],[CourseCode],[Semester],[AcademicLevel],[ModeOfStudy],[CourseOfStudyID],[Passed]) VALUES ('" + stdat.MatricNumber + "','" + stdat.Surname + "','" + CosCode + "','" + semester + "','" + stdat.AdmittedLevel + "','" + stdat.ModeOfStudy + "'," + CosofStudy + ",0)";
                            PerformInsert(qry);

                            Subject = "PTI Carry Over course Notice";
                            Heading = "PTI Carry Over course Notice:";
                            Attached = "";
                            StringBuilder sb2 = new StringBuilder();
                            sb2.AppendLine("");
                            sb2.AppendLine("This is to bring to your notice that you have carry over in this course" + " " + CosCode + " , for" + stdat.AdmittedSession + " academic session");
                            sb2.AppendLine("");



                            string name = stdat.Surname + " " + stdat.OtherNames;
                            sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                            rtn = true;

                        }
                        else
                        {
                            dr.Dispose();
                            cmd.Dispose();
                            msg1 = "Student by name " + stdat.Surname + "," + "with  matno" + " " + stdat.MatricNumber + " " + "could not be found";
                        }
                    }
                    else
                    {
                        msg1 = CosCode + " " + "course Code not found";

                    }
                }
                else
                {
                    msg1 = "Incomplete data for student with matno " + stdat.MatricNumber;
                }

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBSpillOver(Students stdat, string CosCode, string semester)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                int CosofStudy = 0;
                string qry = "";
                if (CosCode != "")
                {
                    qry = "SELECT [Email],[PhoneNumber],[CourseOfStudyID] FROM [Students] WHERE [RegNo]='" +
                        stdat.MatricNumber + "' or [MatricNumber] = '" + stdat.MatricNumber + "' and [Programme] = '" +
                        stdat.Programme + "' and [ModeOfStudy]='" + stdat.ModeOfStudy + "'";
                    cmd = new SqlCommand(qry, cnn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.IsDBNull(0) == false)
                        {
                            email = dr.GetString(0);
                        }
                        else
                        {
                            email = "None";
                        }

                        if (dr.IsDBNull(1) == false)
                        {
                            phone = dr.GetString(1);
                        }
                        else
                        {
                            phone = "None";
                        }

                        if (dr.IsDBNull(2) == false)
                        {
                            CosofStudy = int.Parse(dr.GetValue(2).ToString());
                        }

                        dr.Dispose();
                        cmd.Dispose();

                        qry = "Delete FROM [CarryOver] where [MatricNo] = '" + stdat.MatricNumber +
                            "' and [AcademicLevel] ='" + stdat.AdmittedLevel + "' and [CourseCode] ='" +
                            CosCode + "'";//and [Semester] ='" + semester + "'
                        PerformDelete(qry);

                        qry = "INSERT INTO [CarryOver]([MatricNo],[FullName],[CourseCode],[Semester],[AcademicLevel],[ModeOfStudy],[CourseOfStudyID],[Passed]) VALUES ('" + stdat.MatricNumber +
                            "','" + stdat.Surname + "','" + CosCode + "','" + semester + "','" + stdat.AdmittedLevel +
                            "','" + stdat.ModeOfStudy + "'," + CosofStudy + ",0)";
                        PerformInsert(qry);

                        Subject = "PTI Splill Over course Notice";
                        Heading = "PTI Splill Over course Notice:";
                        Attached = "";
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine("");
                        sb2.AppendLine("This is to bring to your notice that you have carry over in this course" + " " + CosCode + " , for" +
                            stdat.AdmittedLevel + " academic session");
                        sb2.AppendLine("");

                        string name = stdat.Surname + " " + stdat.OtherNames;
                        sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);

                        rtn = true;
                    }
                    else
                    {
                        dr.Dispose();
                        cmd.Dispose();
                        msg1 = "Student by name " + stdat.Surname + "," + "with  matno" + " " + stdat.MatricNumber +
                            " " + "could not be found";
                    }
                }
                else
                {
                    msg1 = "Incomplete data for student with matno " + stdat.MatricNumber;
                }

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBMatNo(Students stdat)
    {
        bool rtn = false;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;
                StringBuilder sb = new StringBuilder();
                string email = "";
                string phone = "";
                string qry = "";
                if (stdat.RegNo != "" && stdat.MatricNumber != "")
                {
                    //
                    qry = "SELECT [Email],[PhoneNumber] FROM [Students] WHERE [RegNo]='" + stdat.RegNo + "' and [AdmissionStatus]='ADMITTED' and [Programme] = '" + stdat.Programme + "' and [ModeOfStudy]='" + stdat.ModeOfStudy + "'";
                    cmd = new SqlCommand(qry, cnn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.IsDBNull(0) == false)
                        {
                            email = dr.GetString(0);
                        }
                        else
                        {
                            email = "None";
                        }

                        if (dr.IsDBNull(1) == false)
                        {
                            phone = dr.GetString(1);
                        }
                        else
                        {
                            phone = "None";
                        }

                        dr.Dispose();
                        cmd.Dispose();

                        qry = "Update [Students] set [MatricNumber] = '" + stdat.MatricNumber + "' WHERE [RegNo]='" + stdat.RegNo + "'";
                        PerformUpdate(qry);

                        qry = "Update [StudentSignOn] set [MatricNumber] = '" + stdat.MatricNumber + "' WHERE [RegNo]='" + stdat.RegNo + "'";
                        PerformUpdate(qry);

                        Subject = "Petroleum Training Institute Matriculation Number Notice";
                        Heading = "Petroleum Training Institute Matriculation Number Notice:";
                        Attached = "";
                        StringBuilder sb2 = new StringBuilder();
                        sb2.AppendLine("");
                        sb2.AppendLine("Below is your matriculation number:");
                        sb2.AppendLine("");
                        //sb2.Append("\n");
                        sb2.AppendLine("MatNo" + " " + stdat.MatricNumber);
                        sb2.AppendLine("");


                        string name = stdat.Surname + " " + stdat.OtherNames;
                        sendGenMail(email, sb2.ToString(), Subject, Heading, Attached, name);


                        rtn = true;

                    }
                    else
                    {
                        dr.Dispose();
                        cmd.Dispose();
                        msg1 = "Student by name " + stdat.Surname + "," + "with  matno" + " " +
                            stdat.MatricNumber + " " + "could not be found";
                    }
                }
                else
                {
                    msg1 = "Incomplete data for student with matno " + stdat.MatricNumber;
                }

                //
                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBAdmitted(Students stdat)
    {
        bool rtn = false;
        DataSet ds = null;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";

        //string message = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;

                cmd = new SqlCommand("SELECT [Surname],[OtherNames],[MaidenName],[Sex],[DateOfBirth],[MaritalStatus],[Nationality],[State],[LocalGovernmentArea],[PlaceOfBirth],[HomeAddress],[PostalAddress],[Country],[Email],[PhoneNumber],[SponsorName],[SponsorAddress],[SponsorPhone],[PassportFile],[LocalPassportFile],[Title],[Center] FROM [Applicants] WHERE [RegNo]='" + stdat.RegNo + "' and [PresentSession]= '" + stdat.AdmittedSession + "'", cnn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Students student = new Students();

                    ////CybSoft.EduPortal.Data.PostJambApplicant Pj = new CybSoft.EduPortal.Data.PostJambApplicant();
                    student.Surname = dr.GetString(0);
                    student.Firstname = dr.GetString(1);
                    if (dr.IsDBNull(1) == false)
                    {
                        student.Middlename = dr.GetString(2);
                    }
                    else
                    {
                        student.Middlename = "";
                    }

                    student.Sex = dr.GetString(3);
                    student.DateOfBirth = dr.GetString(4);//.GetDateTime(4);//.ToString();//.GetString(4);
                    student.MaritalStatus = dr.GetString(5);
                    if (dr.IsDBNull(6) == false)
                    {
                        student.Nationality = dr.GetString(6);
                    }
                    else
                    {
                        student.Nationality = "None";
                    }

                    if (dr.IsDBNull(7) == false)
                    {
                        student.State = dr.GetString(7);
                    }
                    else
                    {
                        student.State = "None";
                    }
                    if (dr.IsDBNull(8) == false)
                    {
                        student.LocalGovernmentArea = dr.GetString(8);
                    }
                    else
                    {
                        student.LocalGovernmentArea = "None";
                    }

                    if (dr.IsDBNull(9) == false)
                    {
                        student.PlaceOfBirth = dr.GetString(9);
                    }
                    else
                    {
                        student.PlaceOfBirth = "None";
                    }

                    //Pj.CourseOfStudy = "";//,[CourseOfStudyApplied]
                    student.HomeAddress = dr.GetString(10);
                    if (dr.IsDBNull(11) == false)
                    {
                        student.PostalAddress = dr.GetString(11);
                    }
                    else
                    {
                        student.PostalAddress = "None";
                    }


                    if (dr.IsDBNull(12) == false)
                    {
                        student.Country = dr.GetString(12);
                    }
                    else
                    {
                        student.Country = "None";
                    }
                    student.Email = dr.GetString(13);
                    student.PhoneNumber = dr.GetString(14);

                    if (dr.IsDBNull(15) == false)
                    {
                        student.ParentGuardianName = dr.GetString(15);
                    }
                    else
                    {
                        student.ParentGuardianName = "None";
                    }

                    if (dr.IsDBNull(16) == false)
                    {
                        student.ParentGuardianAddress = dr.GetString(16);
                    }
                    else
                    {
                        student.ParentGuardianAddress = "None";
                    }

                    if (dr.IsDBNull(17) == false)
                    {
                        student.ParentGuardianPhone = dr.GetString(17);
                    }
                    else
                    {
                        student.ParentGuardianPhone = "None";
                    }

                    student.PassportFile = dr.GetString(18);
                    student.LocalPassportFile = dr.GetString(19);
                    student.Title = dr.GetString(20);

                    if (dr.IsDBNull(21) == false)
                    {
                        student.Center = dr.GetString(21);
                    }
                    else
                    {
                        student.Center = "None";
                    }

                    dr.Dispose();
                    cmd.Dispose();
                    StringBuilder sb = new StringBuilder();
                    string qry = "";
                    qry = "SELECT * FROM [Students] WHERE [RegNo]='" + stdat.RegNo + "'";
                    if (!Existed(qry))
                    {
                        sb.Append("INSERT INTO  [Students] ");
                        sb.Append("([Surname]");//1
                        sb.Append(" ,[OtherNames]");//2
                        sb.Append(" ,[MaidenName]");//3
                        sb.Append(",[Sex]");//4
                        sb.Append(",[DateOfBirth]");//5
                        sb.Append(",[MaritalStatus]");//6
                        sb.Append(",[Nationality]");//6
                        sb.Append(",[State]");//7
                        sb.Append(",[LocalGovernmentArea]");//8
                        sb.Append(",[MatricNumber]");//9
                        sb.Append(",[DepartmentID]");//10
                        sb.Append(",[PlaceOfBirth]");//11
                        sb.Append(",[HomeAddress]");//12
                        sb.Append(",[ResidentialAddress]");//13
                        sb.Append(",[RoomNo]");//14
                        sb.Append(" ,[Country]");//15
                        sb.Append(",[Email]");//16
                        sb.Append(",[PhoneNumber]");//17
                        sb.Append(",[SponsorName]");//18
                        sb.Append(",[SponsorAddress]");//19
                        sb.Append(",[SponsorPhone]");//20
                        sb.Append(" ,[AdmissionStatus]");//21
                        sb.Append(" ,[AcademicLevel]");//22
                        sb.Append(",[RegNo]");//23
                        sb.Append(",[FacultyID]");//24
                        sb.Append(",[CreatedDate]");//25
                        sb.Append(",[CreatedBy]");//26
                        sb.Append(",[CourseOfStudyID]");//27
                        sb.Append(",[PassportFile]");//28
                        sb.Append(",[LocalPassportFile]");//29
                        sb.Append(",[AdmittedSession]");//30
                        sb.Append(",[Programme]");//31
                        sb.Append(",[Duration]");//32
                        sb.Append(",[Honours]");//33
                        sb.Append(",[ModeOfStudy]");//34
                        sb.Append(",[PresentSession]");//35
                        sb.Append(",[AdmittedLevel]");//36
                        sb.Append(",[Title]");//37
                        sb.Append(",[Repeating]");//38
                        sb.Append(",[IsIndigene]");//39
                        sb.Append(",[isNewStudent]");//40
                        sb.Append(",[isEvening]");//41
                        sb.Append(",[CanRegister]");//42
                        sb.Append(",[CanRegister2]");//43
                        sb.Append(",[TempEmailPwd]");//44
                        sb.Append(",[MatricSerial]");//45
                        sb.Append(",[Center]");//46
                        sb.Append(",[CourseOfStudyName]");//46
                        sb.Append(",[EntryMode]");//47                            
                        sb.Append(",[CanChangePassport])");//48//
                        sb.Append("VALUES");
                        sb.Append("('" + student.Surname + "'");//1
                        sb.Append(",'" + student.Firstname + "'");//2
                        sb.Append(",'" + student.Middlename + "'");//3
                        sb.Append(",'" + student.Sex + "'");//4
                        sb.Append(",'" + student.DateOfBirth + "'");//5
                        sb.Append(",'" + student.MaritalStatus + "'");//6
                        sb.Append(",'" + student.Nationality + "'");//7
                        sb.Append(",'" + student.State + "'");//8
                        sb.Append(",'" + student.LocalGovernmentArea + "'");//9
                        sb.Append(",'" + stdat.RegNo + "'");//10
                        sb.Append("," + stdat.DepartmentID);//11
                        sb.Append(",'" + student.PlaceOfBirth + "'");//12
                        sb.Append(",'" + student.HomeAddress + "'");//13
                        sb.Append(",'" + student.HomeAddress + "'");//14//ResidentialAddress
                        sb.Append(",'None'");//rm number//15
                        sb.Append(",'" + student.Country + "'");//16
                        sb.Append(",'" + student.Email + "'");//17
                        sb.Append(",'" + student.PhoneNumber + "'");//18
                        sb.Append(",'" + student.ParentGuardianName + "'");//19
                        sb.Append(",'" + student.ParentGuardianAddress + "'");//20
                        sb.Append(",'" + student.ParentGuardianPhone + "'");//21
                        sb.Append(",'" + stdat.AdmissionStatus + "'");//22
                        sb.Append(",'" + stdat.AdmittedLevel + "'");//23
                        sb.Append(",'" + stdat.RegNo + "'");//24
                        sb.Append(",'" + stdat.FacultyID + "'");//25
                        sb.Append(",'" + stdat.CreatedDate + "'");//26
                        sb.Append(",'" + stdat.CreatedBy + "'");//27
                        sb.Append(",'" + stdat.CourseOfStudyID + "'");//28
                        sb.Append(",'" + student.PassportFile + "'");//29
                        sb.Append(",'" + student.LocalPassportFile + "'");//30
                        sb.Append(",'" + stdat.AdmittedSession + "'");//31
                        sb.Append(",'" + stdat.Programme + "'");//32
                        sb.Append(",'" + stdat.Duration + "'");//33
                        sb.Append(",'" + stdat.Honours + "'");//34
                        sb.Append(",'" + stdat.ModeOfStudy + "'");//35
                        sb.Append(",'" + stdat.AdmittedSession + "'");//36
                        sb.Append(",'" + stdat.AdmittedLevel + "'");//37
                        sb.Append(",'" + student.Title + "'");//38
                        sb.Append(",'No'");//repeating//39
                        sb.Append(",'" + stdat.IsIndigene + "'");//40
                        sb.Append(",'1'");//new student//41
                        sb.Append(",'0'");//evening//42
                        sb.Append(",'0'");//can reg1//43
                        sb.Append(",'0'");//can reg2//44
                        sb.Append(",'None'");//temp email passw//45
                        //string serail = stdat.MatricNo.Trim().Replace("NONE", "").Trim();
                        int srn = 1;// int.Parse(serail);
                        sb.Append("," + srn + "");//46//matserial
                        sb.Append(",'" + student.Center + "'");//47
                        sb.Append(",'" + stdat.CourseOfStudy + "'");//48
                        sb.Append(",'" + stdat.EntryMode + "'");//49
                        sb.Append("," + 0 + ")");//can change passport//50


                        qry = qry = "SELECT [Password] FROM [ApplicantSignOn] WHERE [FormNumber]='" + stdat.RegNo + "'";
                        ds = new DataSet();
                        ds = SearchData(qry);

                        string passw = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                            {
                                passw = ds.Tables[0].Rows[jj][0].ToString();
                            }

                            qry = "SELECT * FROM [StudentSignOn] where [RegNo] = '" + stdat.RegNo + "'";

                            if (!Existed(qry))
                            {

                                qry = "SELECT * FROM [AdmissionList] where [RegNo] = '" + stdat.RegNo + "'";

                                if (!Existed(qry))
                                {
                                    //insert into students

                                    PerformInsert(sb.ToString());

                                    //insert into student signon
                                    qry = "INSERT INTO [StudentSignOn]([MatricNumber],[RegNo],[Password],[Email],[Phone],Status) VALUES ('" + stdat.RegNo + "','" + stdat.RegNo + "','" + passw + "','" + student.Email + "','" + student.PhoneNumber + "',0)";
                                    PerformInsert(qry);
                                    //insert into admission list
                                    qry = "INSERT INTO [AdmissionList]([Surname],[OtherNames],[Sex],[DateOfBirth],[LocalGovernmentArea],[Address],[AcademicLevel],[RegNo],[FacultyID],[AdmittedSession],[Programme],[Duration],[ModeOfStudy],[AdmittedLevel],[IsIndigene],[CourseOfStudy],[BeginDate],[Faculty],[Department],[CourseOfStudyID],[Batch],[EntryMode]) VALUES ('" + student.Surname + "','" + student.Firstname + "','" + student.Sex + "','" + student.DateOfBirth + "','" + student.LocalGovernmentArea + "','" + student.HomeAddress + "','" + stdat.AdmittedLevel + "','" + stdat.RegNo + "','" + stdat.FacultyID + "','" + stdat.AdmittedSession + "','" + stdat.Programme + "','" + stdat.Duration + "','" + stdat.ModeOfStudy + "','" + stdat.AdmittedLevel + "','" + stdat.IsIndigene
                                        + "' ,'" + stdat.CourseOfStudy + "','" + student.CreatedDate + "','" + stdat.Faculty + "','" + stdat.Department + "','" + stdat.CourseOfStudyID + "','" + stdat.Batch + "','" + stdat.EntryMode + "')";
                                    PerformInsert(qry);
                                    //update applicants
                                    qry = "Update [Applicants] set [AdmissionStatus] = '" + stdat.AdmissionStatus + "', [AcademicLevel]='" + stdat.AdmittedLevel + "', [AdmittedSession]='" + stdat.AdmittedSession + "', [AdmittedLevel]='" + stdat.AdmittedLevel + "', [IsIndigene]='" + stdat.IsIndigene + "' where [RegNo]='" + stdat.RegNo + "' and [PresentSession] = '" + stdat.AdmittedSession + "'";
                                    PerformUpdate(qry);


                                    rtn = true;
                                }
                                else
                                {
                                    msg1 = "Student by name " + student.Surname + " " + student.Faculty + " " + "with  reg no" + " " + stdat.RegNo + " " + "is already admmited as a student";
                                }

                            }
                            else
                            {
                                msg1 = "Student by name " + student.Surname + " " + student.Faculty + " " + "with  reg no" + " " + stdat.RegNo + " " + "has already signon as student";
                            }
                        }
                        else
                        {
                            msg1 = "Student by name " + student.Surname + " " + student.Faculty + " " + "with  reg no" + " " + stdat.RegNo + " " + "does not have applicant signo credentials";
                        }

                    }
                    else
                    {
                        msg1 = "Student by name " + student.Surname + " " + student.Faculty + " " + "with  reg no" + " " + stdat.RegNo + " " + "already existed as student";
                    }

                }
                else
                {
                    msg1 = "Student with registration number " + stdat.RegNo + " " + "is not found as an applicant";

                }
                dr.Dispose();
                cmd.Dispose();

                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool UpdateDBAdmittedNoAppFm(Students stdat)
    {
        bool rtn = false;
        DataSet ds = null;
        string Subject = "";
        string Heading = "";
        string Attached = "";
        string msg1 = "";
        string qry = "";
        string CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
        string message = "";
        //string message = "";

        cnn = GetConnection(cnn);
        if (cnn.State == ConnectionState.Open)
        {
            try
            {
                StringBuilder sb2 = new StringBuilder();
                int IsAdmitted = 0;
                if (stdat.AdmissionStatus.ToLower() == "a")
                {
                    IsAdmitted = 1;
                }

                qry = "Select * from [Students] where RegNo= '" + stdat.RegNo + "'";
                if (Existed(qry))
                {

                    msg1 = "Candidate with registration number " + stdat.RegNo + " " + "existed already as a student";
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                    rtn = false;
                    return rtn;
                }

                qry = "Select * from [AdmissionList] where RegNo= '" + stdat.RegNo + "'";
                if (Existed(qry))
                {
                    qry = "Delete from [AdmissionList] where RegNo= '" + stdat.RegNo + "'";
                    PerformDel(qry);
                }


                qry = "INSERT INTO [AdmissionList]([Surname],[OtherNames],[Sex],[DateOfBirth],[LocalGovernmentArea],[Address],[AcademicLevel],[RegNo],[FacultyID],[AdmittedSession],[Programme],[Duration],[ModeOfStudy],[AdmittedLevel],[IsIndigene],[CourseOfStudy],[BeginDate],[Faculty],[Department],[CourseOfStudyID],[Batch],[EntryMode],[State],[MaritalStatus],[Nationality],[IsAdmitted],[AdmittedBy],[AdmittedStatus],[Remarks],[Email],[Phone],TeachingSubject) VALUES ('" + stdat.Surname + "','" + stdat.OtherNames + "','" + stdat.Sex + "','" + stdat.DateOfBirth + "','" + stdat.LocalGovernmentArea + "','" + stdat.HomeAddress + "','" + stdat.AdmittedLevel + "','" + stdat.RegNo + "','" + stdat.FacultyID + "','" + stdat.AdmittedSession + "','" + stdat.Programme + "','" + stdat.Duration + "','" + stdat.ModeOfStudy + "','" + stdat.AdmittedLevel + "','" + stdat.IsIndigene + "' ,'" + stdat.CourseOfStudy + "','" + CreatedDate + "','" + stdat.Faculty + "','" + stdat.Department + "','" + stdat.CourseOfStudyID + "','" + stdat.Batch + "','" + stdat.EntryMode + "','" + stdat.State + "','" + stdat.MaritalStatus + "','" + stdat.Nationality + "'," + IsAdmitted + ",'" + stdat.CreatedBy + "','" + stdat.AdmissionStatus + "','" + stdat.Remark + "','" + stdat.Email + "','" + stdat.PhoneNumber + "','" + stdat.TeachingSubject + "')";
                if (PerformInsert2(qry))
                {
                    Subject = "Petroleum Training Institute";
                    Heading = "Petroleum Training Institute Admission: " + stdat.AdmittedSession + " " + "academic session";
                    Attached = "";

                    UploadDataInfosms datInfo = null;

                    string name = stdat.Surname + " " + stdat.OtherNames;
                    if (stdat.AdmissionStatus.ToLower() == "a")
                    {
                        message = "This is to inform you that you have been admitted into Petroleum Training Institute, Effurun for " + stdat.AdmittedSession + " academic session, visit the school portal";

                        //sb2.AppendLine("Please, disregard any previous message regarding University of Ibadan Distance learning Admission if received before.");
                        //sb2.AppendLine("");
                        sb2.AppendLine("");
                        sb2.AppendLine("This is to inform you that you have been admitted into Petroleum Training Institute, Effurun:");
                        sb2.AppendLine("");
                        //sb2.Append("\n");
                        sb2.AppendLine("Faculty:" + " " + stdat.Faculty);
                        sb2.AppendLine("");
                        sb2.AppendLine("Department:" + " " + stdat.Department);
                        sb2.AppendLine("");
                        sb2.AppendLine("Programme of study:" + " " + stdat.CourseOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Level:" + " " + stdat.AdmittedLevel);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course duration:" + " " + stdat.Duration);
                        sb2.AppendLine("");
                        sb2.AppendLine("Course Honor:" + " " + stdat.Honours);
                        sb2.AppendLine("");
                        sb2.AppendLine("Study Mode:" + " " + stdat.ModeOfStudy);
                        sb2.AppendLine("");
                        sb2.AppendLine("Programme :" + " " + stdat.Programme);
                        sb2.AppendLine("");
                        sb2.AppendLine("Teaching Subject :" + " " + stdat.TeachingSubject);
                        sb2.AppendLine("");
                        sb2.AppendLine("*********");
                        sb2.AppendLine("Meanwhile, you can go to the portal to view admission and print out your admission noticification letter.");
                        sb2.AppendLine("");
                        sb2.AppendLine("");
                        sb2.AppendLine("Fees breakdown are reflected in the admission noticification letter, visit the designated banks to pay and collect Transaction refrerence for you sign up");
                        sb2.AppendLine("");
                        //sb2.AppendLine("Meanwhile, you won't be able to login as a student with your login credentials below till after printing your admission letter.");
                        sb2.AppendLine("");
                        sb2.AppendLine("Application Form Number = " + " " + stdat.RegNo);

                        sb2.AppendLine("");
                        sb2.AppendLine("Please, disregard any previous message regarding Change of Course from Petroleum Training Institute Admission if received before.");


                        //sb2.AppendLine("");
                        //sb2.AppendLine("Password = " + " " + passw);
                        sendGenMail(stdat.Email, sb2.ToString(), Subject, Heading, Attached, name);


                    }
                    else
                    {

                        message = "You have been adviced to visit Petroleum Training Institute Learning portal to chose another Programme of Study for " + stdat.AdmittedSession + " academic session";



                        sb2.AppendLine("");
                        sb2.AppendLine("This is to inform you that you have been adviced to chose from the Programme of Study below since your requirements could not meet the initial Programme of Study applied for:");
                        sb2.AppendLine("");
                        //sb2.Append("\n");
                        sb2.AppendLine("Suggested Programme of Study:" + " " + stdat.Remark);
                        sb2.AppendLine("");

                        sb2.AppendLine("*********");
                        sb2.AppendLine("Meanwhile, you can go to the portal to view admission, accept your new programme of study and print out your admission noticification letter.");
                        sb2.AppendLine("");
                        sb2.AppendLine("");
                        sb2.AppendLine("Fees breakdown are reflected in the admission noticification letter, visit the designated banks to pay and collect Transaction refrerence for you sign up");
                        sb2.AppendLine("");

                        sb2.AppendLine("");
                        sb2.AppendLine("Application Form Number = " + " " + stdat.RegNo);

                        sb2.AppendLine("");
                        sb2.AppendLine("Please, disregard any previous message regarding Change of Course from Petroleum Training Institute Admission if received before.");


                        sendGenMail(stdat.Email, sb2.ToString(), Subject, Heading, Attached, name);
                    }

                    string MsisdnType = "individual";
                    datInfo = new UploadDataInfosms();

                    datInfo.CurrentFile = "";
                    datInfo.Filename = "";
                    datInfo.Sdate = "";
                    datInfo.Stime = "";
                    datInfo.Uploader = stdat.CreatedBy;
                    datInfo.FileTpye = "";
                    datInfo.Messagebody = message;
                    datInfo.Sourceaddress = "PTI";
                    datInfo.MsgType = "common";
                    datInfo.Template = "";
                    datInfo.MsisdnType = MsisdnType;
                    datInfo.Msisdn = stdat.PhoneNumber;
                    datInfo.SubGroup = "";
                    datInfo.Group = "";
                    datInfo.SubGroupCategory = "";
                    //sendtoUploadloadQ(datInfo);

                    rtn = true;
                }
                else
                {
                }

                //update applicants
                //qry = "Update [Applicants] set [AdmissionStatus] = '" + stdat.AdmissionStatus + "', [AcademicLevel]='" + stdat.StartLevel + "', [AdmittedSession]='" + stdat.AdmissionSession + "', [AdmittedLevel]='" + stdat.StartLevel + "', [IsIndigene]='" + stdat.Indigen + "' where [RegNo]='" + stdat.RegNo + "' and [PresentSession] = '" + stdat.AdmissionSession + "'";
                //PerformUpdate(qry);



                if (msg1 != "")
                {
                    regerrors.AppendLine("");
                    regerrors.AppendLine(msg1);
                    if (RegNotFound == "")
                    {
                        RegNotFound = msg1 + ",";
                    }
                    else
                    {
                        RegNotFound = RegNotFound + msg1 + ",";
                    }
                }
            }
            catch (Exception ex)
            {   //
                msg = ex.Message;
                // log.Error(msg);
                //Logmsg(msg);
                throw;
            }

        }
        return rtn;
    }

    private static bool PerformInsert2(string Qry)
    {
        bool ret = false;
        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlCommand cmd = null;

                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                ret = true;
                cmd.Dispose();
            }


            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            //throw;
        }
        return ret;
    }

    private static void PerformDelete(string Qry)
    {
        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            throw;
        }
    }

    private static ArrayList GeneratTempMatNo(int Rows)
    {
        ArrayList MatNo = new ArrayList();

        SqlConnection cnn = new SqlConnection(str);
        cnn.Open();

        SqlCommand cmd = null;
        SqlDataReader dr = null;

        try
        {
            cmd = new SqlCommand("SELECT count(*)from [TempMatno]", cnn);
            int m = (int)cmd.ExecuteScalar();
            cmd.Dispose();

            string LastNumber = "";
            string TempNUMBER = "NONE";

            int IntPart = 0;
            int realID = 1;
            int Number = 0;
            Number = realID + m;
            //
            int Count = 0;
            if (m == 0)
            {
                for (int kk = 1; kk <= Rows; kk++)
                {
                    MatNo.Add(kk.ToString());
                }
                //return MatNo;
            }
            else
            {
                cmd = new SqlCommand("SELECT Top(1)[TMatno] from [TempMatno] order by [srn] desc", cnn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LastNumber = dr.GetString(0);
                }
                dr.Dispose();
                cmd.Dispose();

                if (LastNumber != "")
                {
                    realID = 0;

                    string pattern1 = RegInt;//
                    MatchCollection mc1 = Regex.Matches(LastNumber, pattern1);


                    for (int i = 0; i < mc1.Count; i++)
                    {
                        IntPart = int.Parse(mc1[0].ToString());
                    }

                    realID = IntPart + 1;
                    Number = realID;

                    for (int k = Number; k <= Rows + Number; k++)
                    {

                        MatNo.Add(k.ToString());
                    }

                }
            }
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace.ToString();
            //log.Error(msg);
            //Logmsg(msg);                
        }
        return MatNo;
    }

    private static Hashtable getmails()
    {
        Hashtable Mails = new Hashtable();

        try
        {
            string Qry = "SELECT [Email],[StaffName] FROM [MailList]";

            DataSet ds = SearchData(Qry);

            string Id = "";
            string email = "";

            DateTime dt;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    email = ds.Tables[0].Rows[jj][0].ToString().ToUpper();
                    Id = ds.Tables[0].Rows[jj][1].ToString().ToLower();

                    if (!Mails.ContainsKey(Id))
                    {
                        Mails.Add(email, Id);
                    }
                }

            }
        }
        catch (Exception ex)
        {
        }
        return Mails;
    }

    private static DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);
                dat.Fill(ds);
                dat.Dispose();
            }



            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
        }
        return ds;
    }

    private static SqlConnection GetConnection(SqlConnection cnn)
    {
        if (cnn == null)
        {
            try
            {
                cnn = new SqlConnection(str);
                cnn.Open();


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //log.Error(msg);
                //Logmsg(msg);
                //displaymsg(exception.Message);
                //PrintError(exception);
            }
        }
        else
        {
            if (cnn.State != System.Data.ConnectionState.Open)
            {
                cnn.Dispose();
                cnn.Close();
                cnn = null;
                cnn = GetConnection(cnn);
                //TimeSpan tsp = PTI_Bg.Properties.Settings.Default.DataBaseReconnectionInterval;
                //Thread.Sleep(tsp);
            }
        }
        return cnn;
    }

    private static bool Existed(string qry)
    {
        bool ret = false;

        try
        {

            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
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
            }

            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
        }

        return ret;
    }

    private static void PerformInsert(string Qry)
    {

        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlCommand cmd = null;
                //SqlDataReader dr = null;


                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }


            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            throw;
        }
    }
    private static void PerformDel(string Qry)
    {

        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlCommand cmd = null;
                //SqlDataReader dr = null;


                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }


            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            throw;
        }
    }
    private static void PerformUpdate(string Qry)
    {

        try
        {
            cnn = GetConnection(cnn);
            if (cnn.State == ConnectionState.Open)
            {
                SqlCommand cmd = null;
                //SqlDataReader dr = null;


                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }


            //cnn.Dispose();
            //cnn.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message + "||" + ex.StackTrace;
            //logger.Error(error);
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            throw;
        }
    }

    private static void DisposeSheet()
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

    private static void sendGenMail(string maillist, string messg, string subject, string Heading, string attachedFile, string StaffName)
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
            //sb.AppendLine(Heading.ToUpper());
            //sb.AppendLine("-------------------------------");
            sb.AppendLine();
            sb.AppendLine(messg);
            sb.AppendLine();
            sb.AppendLine("Best Regards,");
            sb.AppendLine("");
            sb.AppendLine("======================================================================================");
            sb.AppendLine("PTI");

            cm.Body = sb.ToString();
            cm.BCCEmail.Add("cybsoft@cybaaspace.ng");
            //cm.ReplyTo.Add("admissions@");
            cm.ReplyTo.Add("itsupport@pti.edu.ng");
            cm.DisplayName = subject;
            cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm.SourceApplication = "PTI";


            // sendtoMailQueue(cm);   to do later
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {


        }

        // }
    }



}
