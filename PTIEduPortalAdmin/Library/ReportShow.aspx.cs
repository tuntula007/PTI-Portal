using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Text;
using System.Globalization;

public partial class ReportShow : System.Web.UI.Page
{

    ReportDocument RptDoc = new ReportDocument();
    public static string PtCode = "";
    public static string ReportTypeGlobal = "";

    private static string str = ConfigurationManager.AppSettings["conn"];
    private static string msg = "";
    private static int Maxdays = 0;
    private static int UsedDays = 0;
    private static int avaidays = 0;

    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LabelError.Visible = false;
            string OrgName = "Cyberspace Network limited";
            string Addr = "33, Saka Tinubu, VI, Lagos";
            DataTable AdmDt = new DataTable();
            DataTable AdmDt2 = new DataTable();


            string ReportTitle = "";


            // showwindow("~/Admin/ReportShow.aspx?CourseCode=" + CourseCode + "&StudID=" + StudID + "&RptType=" + RptType + "&Yearr=" + Yearr + "&Date1=" + Date1 + "&Date2=" + Date2 + "&Programme=" + Programme + "&StudyMode=" + StudyMode + "&Level=" + Level);
            ////string ReportType = (string)Session["RptType"];
            ////string Staff = (string)Session["Staff"];
            ////string Yearr = (string)Session["Yearr"];
            ////string StartDate = (string)Session["Date1"];
            ////string EndDate = (string)Session["Date2"];
            ////string Programme = (string)Session["Programme"];
            ////string StudyMode = (string)Session["StudyMode"];
            ////string CourseCode = (string)Session["CourseCode"];
            ////string Level = (string)Session["Level"];


            string ReportType = Request.QueryString["RptType"];
            string Staff = Request.QueryString["StudID"];
            string Yearr = Request.QueryString["Yearr"];
            string StartDate = Request.QueryString["Date1"];
            string EndDate = Request.QueryString["Date2"];
            string Programme = Request.QueryString["Programme"];
            string StudyMode = Request.QueryString["StudyMode"];
            string CourseCode = Request.QueryString["CourseCode"];
            string Level = Request.QueryString["Level"];

            //JobID = Request.QueryString["JobId"];//.ToString;
            //UserId = Request.QueryString["UserId"];

            string daterange = "";

            string Title2 = "";
            string Title = "PETROLEUM TRAINING INSTITUTE, EFFURUN";

            string RptPath = "";

            ////DDListReportType.Items.Add("Applicants");
            ////DDListReportType.Items.Add("Admitted Students");
            ////DDListReportType.Items.Add("School fees Ref Summary");
            ////DDListReportType.Items.Add("Registered Students");//Date will specify late reg
            ////DDListReportType.Items.Add("Exam Cards"); 
            if (ReportType != "")
            {
                if (ReportType == "Application Pin Summary")
                {
                    RptPath = Server.MapPath("ApplicationPin2.rpt");
                    AdmDt = GetApplicationPinUsage(StartDate, EndDate, Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                    Title2 = "Programme = " + Programme.ToUpper() + " ," + " Study Mode = " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "Application pins used between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "Application pins used for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }
                else if (ReportType == "Admitted Students")
                {
                    RptPath = Server.MapPath("Addmittedstudents.rpt");
                    AdmDt = GetAddmittedStudents(StartDate, EndDate, Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);
                    Title2 = "Programme: " + Programme.ToUpper() + " ," + " Study Mode: " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "Addmitted Students between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "Addmitted Students for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }
                else if (ReportType == "School fees Ref Summary")
                {
                    RptPath = Server.MapPath("SchoolFees.rpt");
                    AdmDt = GetSchoolFeesPin(StartDate, EndDate, Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);
                    Title2 = "Programme: " + Programme.ToUpper() + " ," + " Study Mode: " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "School fees reference used between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "School fees reference used for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }

                else if (ReportType == "First Semester Registration")
                {
                    RptPath = Server.MapPath("Semesterreg.rpt");
                    AdmDt = GetSemesterReg1(StartDate, EndDate, Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                    Title2 = "Programme = " + Programme.ToUpper() + " ," + " Study Mode = " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "First Semester Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "First Semester Registration for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }
                else if (ReportType == "Registered Students")
                {

                    RptPath = Server.MapPath("Semesterreg.rpt");
                    AdmDt = GetSemesterReg2(StartDate, EndDate, Yearr, Programme, StudyMode, Level);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                    Title2 = "Programme: " + Programme.ToUpper() + " ," + " Study Mode: " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "Course Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "Course Registration for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }
                else if (ReportType == "Summer School Registration")
                {
                    RptPath = Server.MapPath("Semesterreg.rpt");
                    AdmDt = GetSemesterReg3(StartDate, EndDate, Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                    Title2 = "Programme = " + Programme.ToUpper() + " ," + " Study Mode = " + StudyMode.ToUpper();

                    if (StartDate != "" && EndDate != "")
                    {
                        daterange = "Summer School Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    }
                    else
                    {
                        daterange = "Summer School Registration for " + Yearr + " " + "academic session";
                    }
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

                }

                else if (ReportType == "All Library")
                {
                    try
                    {
                        RptPath = Server.MapPath("ExamCards.rpt");
                        AdmDt = GetExamCards2(Yearr, Programme, StudyMode, CourseCode, Level,StartDate, EndDate);                       
                        
                        RptDoc.Load(RptPath);
                        RptDoc.SetDataSource(AdmDt);

                        Title2 = "Programme: " + Programme.ToUpper() + ", " + " Study Mode: " + StudyMode.ToUpper();

                        if (StartDate != "" && EndDate != "")
                        {
                            daterange = "CLeared Students for Library Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                        }
                        else
                        {
                            daterange = "CLeared Students for Library Registration for " + Yearr + " " + "academic session";

                        }
                        
                        //daterange = "CLeared Students for Medical Registration for "  + Yearr + " " + "academic session";

                        RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                        RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                        RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";
                    }
                    catch (Exception   ex)
                    {                        
                       
                    }

                }
                else if (ReportType == "Individual Library")
                {
                    try
                    {
                        RptPath = Server.MapPath("ExamCards.rpt");
                        AdmDt = GetExamCards(Yearr, Programme, StudyMode, CourseCode, Level, Staff);                       

                        RptDoc.Load(RptPath);
                        RptDoc.SetDataSource(AdmDt);

                        Title2 = "Programme: " + Programme.ToUpper() + ", " + " Study Mode: " + StudyMode.ToUpper();

                        //if (StartDate != "" && EndDate != "")
                        //{
                        //    //daterange = "CLeared Students for Medical Registration for " + Yearr + " " + "academic session";

                        //    daterange = "CLeared Students for Medical Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                        //}
                        //else
                        //{
                        //    daterange = "CLeared Students for Medical Registration for " + Yearr + " " + "academic session";

                        //}
                        //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;
                        daterange = "CLeared Students for Library Registration for " + Yearr + " " + "academic session";

                        
                        RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                        RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                        RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";
                    }
                    catch (Exception ex)
                    {


                    }

                }

                if (AdmDt.Rows.Count > 0)
                {
                    ReportTypeGlobal = ReportType;
                    CrvAdmissionReq.ReportSource = RptDoc;
                    PrintToPdf();
                }
                else
                {
                    LabelError.Text = "NO RECORD TO DISPLAY";
                    LabelError.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataTable GetExamCards2(string Yearr, string Programme, string StudyMode, string CourseCode, string Level, string StartDate, string EndDate)
    {
        DataSet ExamCards = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query ="";// "SELECT distinct Top(100) D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department,(select C.[CourseOfStudyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as CourseOfStudyName FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[IsApproved] = 1 and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "' and D.[AcademicLevel] = '" + Level + "'";
            //string query = "SELECT distinct D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department,(select C.[CourseOfStudyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as CourseOfStudyName FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[IsApproved] = 1 and D.[MatricNumber] = '" + Staff + "'";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT distinct D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department,(select C.[CourseOfStudyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as CourseOfStudyName FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[IsApproved] = 1 and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "' and D.[AcademicLevel] = '" + Level + "' and D.[RegDate]  >= '" + StartDate + "' and  D.[RegDate] <= '" + EndDate + "'";
               // query = "SELECT distinct A.[MatricNumber], (select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[RegDate]  >= '" + StartDate + "' and  A.[RegDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and A.IsSummer = 1 and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "'";
            }
            else
            {
                ////query = "SELECT distinct A.[MatricNumber],A.[CourseOfStudy],A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";
                //query = "SELECT distinct A.[MatricNumber],(select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.IsSummer = 1 and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "'";
                query = "SELECT distinct D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department,(select C.[CourseOfStudyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as CourseOfStudyName FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[IsApproved] = 1 and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "' and D.[AcademicLevel] = '" + Level + "'";

            }
            
            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(ExamCards, "Students");

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return ExamCards.Tables[0];
    }

    private DataTable GetExamCards(string Yearr, string Programme, string StudyMode, string CourseCode, string Level, string Staff)
    {
        DataSet ExamCards = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department FROM [Students] A, PictureFile B, CourseRegistration D, SchoolFeesPin E  where D.[MatricNumber] = A.[RegNo] and D.[MatricNumber] = B.[PicKey] and D.[MatricNumber] = E.[UsedBy] and D.[SessionName] = E.[SessionName] and D.[SessionName] = '" + Yearr + "' and D.[CourseCode] = '" + CourseCode + "' and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "'";

            string query = "SELECT distinct D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department,(select C.[CourseOfStudyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as CourseOfStudyName FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[IsApproved] = 1 and D.[MatricNumber] = '" + Staff + "'";
            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(ExamCards, "Students");

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return ExamCards.Tables[0];
    }

    

    private DataTable GetSchoolFeesPin(string StartDate, string EndDate, string Yearr, string Programme, string StudyMode)
    {
        DataSet SchFeessPin = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";
            // ,[Programme]      ,[ModeOfStudy]
            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT A.[PinSerialNumber],A.[PinNumber],A.[UsedBy],A.[UsedDate],A.[AcademicLevel],(select C.[PaymentType] from [StudentPayment] C where A.[PinNumber] = C.[Pin] and A.[UsedBy] = C.[MatricNumber]) as [PaymentType],A.[Amount],A.[Faculty],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[UsedBy] = B.[MatricNumber]) as [Names],(select B.[CourseOfStudyName] from [Students] B where A.[UsedBy] = B.[MatricNumber]) as CourseOfStudy FROM [SchoolFeesPin] A where A.[PinStatus]=1 and A.[UsedDate]  >= '" + StartDate + "' and  A.[UsedDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and A.[Programme]= '" + Programme + "' and A.[ModeOfStudy]= '" + StudyMode + "' order by A.UsedDate desc";

            }
            else
            {
                query = "SELECT A.[PinSerialNumber],A.[PinNumber],A.[UsedBy],A.[UsedDate],A.[AcademicLevel],(select C.[PaymentType] from [StudentPayment] C where A.[PinNumber] = C.[Pin] and A.[UsedBy] = C.[MatricNumber]) as [PaymentType],A.[Amount],A.[Faculty],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[UsedBy] = B.[MatricNumber]) as [Names],(select B.[CourseOfStudyName] from [Students] B where A.[UsedBy] = B.[MatricNumber]) as CourseOfStudy FROM [SchoolFeesPin] A where A.[PinStatus]=1 and A.[SessionName] = '" + Yearr + "' and A.[Programme]= '" + Programme + "' and A.[ModeOfStudy]= '" + StudyMode + "' order by A.UsedDate desc";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(SchFeessPin, "SchoolFeesPin");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return SchFeessPin.Tables[0];
    }

    private DataTable GetSemesterReg2(string StartDate, string EndDate, string Yearr, string Programme, string StudyMode, string Level)
    {
        DataSet Semesterregistration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";
            //string Level = "100";

            //select C.[FacultyPrefix] from [Faculty] C where C.[FacultyID] = A.[FacultyID]) as Facultys
            if (StartDate != "" && EndDate != "")
            {
                // query = "SELECT distinct A.[MatricNumber],(select distinct B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select distinct B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[RegDate]  >= '" + StartDate + "' and  A.[RegDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "'";
                query = "SELECT distinct A.[MatricNumber],(select distinct B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Programme],A.[ModeOfStudy],CONVERT(VARCHAR,A.[RegDate],101) as RegDate,(select distinct B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[RegDate]  >= '" + StartDate + "' and  A.[RegDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "' and A.[AcademicLevel]='" + Level + "'";

            }
            else
            {
                //query = "SELECT distinct A.[MatricNumber],A.[CourseOfStudy],A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";
                query = "SELECT distinct A.[MatricNumber],(select distinct B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Programme],A.[ModeOfStudy],CONVERT(VARCHAR,A.[RegDate],101) as RegDate  ,(select distinct B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "' and A.[AcademicLevel]='" + Level + "'";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Semesterregistration, "CourseRegistration");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return Semesterregistration.Tables[0];
    }

    private DataTable GetSemesterReg3(string StartDate, string EndDate, string Yearr, string Programme, string StudyMode)
    {
        DataSet Semesterregistration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";
            //select C.[FacultyPrefix] from [Faculty] C where C.[FacultyID] = A.[FacultyID]) as Facultys
            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT distinct A.[MatricNumber], (select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[RegDate]  >= '" + StartDate + "' and  A.[RegDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and A.IsSummer = 1 and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "'";
            }
            else
            {
                //query = "SELECT distinct A.[MatricNumber],A.[CourseOfStudy],A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";
                query = "SELECT distinct A.[MatricNumber],(select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.IsSummer = 1 and [Programme]= '" + Programme + "' and [ModeOfStudy]= '" + StudyMode + "'";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Semesterregistration, "CourseRegistration");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return Semesterregistration.Tables[0];
    }

    private DataTable GetSemesterReg1(string StartDate, string EndDate, string Yearr, string prog, string StudyMode)
    {
        DataSet Semesterregistration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();
            //string prog = "DEGREE";
            //string StudyMode = "Full-Time";

            string query = "";
            //select C.[FacultyPrefix] from [Faculty] C where C.[FacultyID] = A.[FacultyID]) as Facultys
            if (StartDate != "" && EndDate != "")
            {
                //[CourseOfStudy],[AcademicLevel],[Semester],[Programme],[ModeOfStudy],[MatricNumber],[RegNo]
                //query = "SELECT A.[PinNumber],A.[PinSerialNumber],A.[Amount],A.[DateUsed],A.[ModeOfStudy],A.[UsedBy],(select B.[Surname] + ' ' + B.[OtherNames] from [Applicants] B where A.[UsedBy] = B.[FormNumber]) as [Names] FROM [AdmissionLetterFeePin] A where A.[DateUsed]  >= '" + StartDate + "' and  A.[DateUsed] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and A.[PinStatus]=1 order by A.[DateUsed] desc";
                query = "SELECT distinct A.[MatricNumber], (select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[RegDate]  >= '" + StartDate + "' and  A.[RegDate] <= '" + EndDate + "' and A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";
            }
            else
            {
                //query = "SELECT distinct A.[MatricNumber],A.[CourseOfStudy],A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";
                query = "SELECT distinct A.[MatricNumber],(select B.[CourseOfStudyName] from [Students] B where A.[CourseOfStudyID] = B.[CourseOfStudyID] and A.[MatricNumber] = B.[MatricNumber]) as CourseOfStudy,A.[AcademicLevel],A.[Semester],A.[Programme],A.[ModeOfStudy],A.[RegDate],(select B.[Surname] + ' ' + B.[OtherNames] from [Students] B where A.[MatricNumber] = B.[MatricNumber]) as [Names] FROM [CourseRegistration] A where A.[SessionName] = '" + Yearr + "' and A.Semester='First' and [Programme]= '" + prog + "' and [ModeOfStudy]= '" + StudyMode + "'";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Semesterregistration, "CourseRegistration");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return Semesterregistration.Tables[0];
    }

    private DataTable GetAddmittedStudents(string StartDate, string EndDate, string Yearr, string Programme, string StudyMode)
    {
        DataSet AdmittedStudents = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";
            //select C.[FacultyPrefix] from [Faculty] C where C.[FacultyID] = A.[FacultyID]) as Facultys
            if (StartDate != "" && EndDate != "")
            {
                query = "Select * FROM [AdmissionList] A where A.[BeginDate]  >= '" + StartDate + "' and  A.[BeginDate] <= '" + EndDate + "' and A.[AdmittedSession] = '" + Yearr + "' and A.[ModeOfStudy] = '" + StudyMode + "' and A.[Programme] = '" + Programme + "' order by A.[BeginDate] desc";
            }
            else
            {
                query = "Select * FROM [AdmissionList] A where  A.[AdmittedSession] = '" + Yearr + "' and A.[ModeOfStudy] = '" + StudyMode + "' and A.[Programme] = '" + Programme + "' order by A.[BeginDate] desc";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(AdmittedStudents, "AdmissionList");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return AdmittedStudents.Tables[0];
    }

    private DataTable GetApplicationPinUsage(string StartDate, string EndDate, string Yearr, string Programme, string StudyMode)
    {
        DataSet ApplicationPin = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";
            // ,[Programme]      ,[ModeOfStudy]
            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [ApplicationFeesPin] where [UsedDate]  >= '" + StartDate + "' and  [UsedDate] <= '" + EndDate + "' and [SessionName] = '" + Yearr + "' and [PinStatus]=1 and [Programme] = '" + Programme + "' and [ModeOfStudy] = '" + StudyMode + "' order by UsedDate desc";
            }
            else
            {
                query = "SELECT * FROM [ApplicationFeesPin] where  [SessionName] = '" + Yearr + "' and [PinStatus] = 1 and [Programme] = '" + Programme + "' and [ModeOfStudy] = '" + StudyMode + "' order by UsedDate desc";

            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(ApplicationPin, "ApplicationFeesPin");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return ApplicationPin.Tables[0];
    }

    private DataTable GetBookSummary(string StartDate, string EndDate, string Yearr)
    {
        DataSet AvailBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [BookDetails] where [Datesetup]  >= '" + StartDate + "' and  [Datesetup] <= '" + EndDate + "' order by Datesetup desc";
            }
            else
            {
                query = "SELECT * FROM [BookDetails] order by Datesetup desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(AvailBooks, "BookDetails");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return AvailBooks.Tables[0];
    }

    private DataTable GetIsolatedBk(string StartDate, string EndDate, string Yearr)
    {
        DataSet IsolatedBk = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT A.[BookStatus],A.[StatusCode], B.[Title], B.[Author], B.[Publisher], B.[ISBN], B.[ACCNO] , B.[Vol], B.[No], B.[Edition] ,B.[Cost],B.[Availability] FROM [BookIsolation] A, [BookDetails] B where B.[Availability] = A.[StatusCode] and B.[Availability] != 1 and B.[Availability] != 0 and B.[Datesetup]  >= '" + StartDate + "' and  B.[Datesetup] <= '" + EndDate + "' order by B. Datesetup desc";
            }
            else
            {
                query = "SELECT A.[BookStatus],A.[StatusCode], B.[Title], B.[Author], B.[Publisher], B.[ISBN], B.[ACCNO] , B.[Vol], B.[No], B.[Edition] ,B.[Cost],B.[Availability] FROM [BookIsolation] A, [BookDetails] B where B.[Availability] = A.[StatusCode] and B.[Availability] != 1 and B.[Availability] != 0 order by Datesetup desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(IsolatedBk, "BookDetails");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return IsolatedBk.Tables[0];
    }

    private DataTable GetOrderedBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet OrderedBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [BookAquisitions] where [Datesetup]  >= '" + StartDate + "' and  [Datesetup] <= '" + EndDate + "' order by Datesetup desc";
            }
            else
            {
                query = "SELECT * FROM [BookAquisitions] order by Datesetup desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(OrderedBooks, "BookAquisitions");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return OrderedBooks.Tables[0];
    }

    private DataTable GetAvailBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet AvailBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [BookDetails] where [Availability]= 1 and [Datesetup]  >= '" + StartDate + "' and  [Datesetup] <= '" + EndDate + "' order by Datesetup desc";
            }
            else
            {
                query = "SELECT * FROM [BookDetails] where [Availability]= 1 order by Datesetup desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(AvailBooks, "BookDetails");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return AvailBooks.Tables[0];
    }

    private DataTable GetReceivedBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet BookReceived = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [BookRecievedDetails] where  [Datesetup]  >= '" + StartDate + "' and  [Datesetup] <= '" + EndDate + "' order by Datesetup desc";
            }
            else
            {
                query = "SELECT * FROM [BookRecievedDetails] order by Datesetup desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(BookReceived, "BookRecievedDetails");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return BookReceived.Tables[0];
    }

    private DataTable GetReGstudent(string StartDate, string EndDate, string Yearr)
    {
        DataSet Registration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [Borrowers] where [Status]= 1 and RenewedStatus = 0 and [DateRegistered]  >= '" + StartDate + "' and  [DateRegistered] <= '" + EndDate + "' order by DateRegistered desc";
            }
            else
            {
                query = "SELECT * FROM [Borrowers] where RenewedStatus = 0 and [Status] = 1 and Session = '" + Yearr + "' order by DateRegistered desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Registration, "Borrowers");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return Registration.Tables[0];
    }

    private DataTable GetConsultedBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet OverDueBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [Loan] where [DateBorrowed] >= '" + StartDate + "' and [DateBorrowed] <= '" + EndDate + "' order by DateBorrowed desc";
            }
            else
            {
                query = "SELECT * FROM [Loan]";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(OverDueBooks, "Loan");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return OverDueBooks.Tables[0];
    }

    private DataTable GetRenewBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet Registration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [Borrowers] where RenewedStatus = 1  and [RenewedDate]  >= '" + StartDate + "' and  [RenewedDate] <= '" + EndDate + "' order by RenewedDate desc";
            }
            else
            {
                query = "SELECT * FROM [Borrowers] where RenewedStatus = 1 and Session = '" + Yearr + "' order by RenewedDate desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Registration, "Borrowers");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return Registration.Tables[0];
    }

    private DataTable GetOverdueBooks2(string StartDate, string EndDate, string Yearr)
    {
        DataSet OverDueBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [Loan] where RetStatus = 0  and [DateBorrowed] >= '" + StartDate + "' and [DateBorrowed] <= '" + EndDate + "' order by DateBorrowed desc";
            }
            else
            {
                query = "SELECT * FROM [Loan] where RetStatus = 0 order by DateBorrowed desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(OverDueBooks, "Loan");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return OverDueBooks.Tables[0];
    }

    private DataTable GetOverdueBooks(string StartDate, string EndDate, string Yearr)
    {
        DataSet OverDueBooks = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            if (StartDate != "" && EndDate != "")
            {
                query = "SELECT * FROM [Loan] where RetStatus = 0  and [ExpectedRetDate] < '" + StartDate + "' or [ExpectedRetDate] < '" + EndDate + "' order by ExpectedRetDate desc";
            }
            else
            {
                query = "SELECT * FROM [Loan] where RetStatus = 0 and [ExpectedRetDate] < '" + DateTime.Now.ToString("yyyy-MM-dd") + "'order by ExpectedRetDate desc";
            }

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(OverDueBooks, "Loan");
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return OverDueBooks.Tables[0];
    }

    private DataTable GetReg2(string Yearr, string Staff)
    {
        DataSet Registration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =0";
            string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[BorrowerID]= '" + Staff + "'  and A.[Status] = 1 and A.[RenewedStatus] =1";
            //string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =1";


            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Registration, "Borrowers");

            cnn.Dispose();
            cnn.Close();



        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return Registration.Tables[0];
    }

    private DataTable GetReg(string Yearr, string Staff)
    {
        DataSet Registration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =0";
            string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[BorrowerID]= '" + Staff + "'  and A.[Status] = 1 and A.[RenewedStatus] =0";


            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Registration, "Borrowers");

            cnn.Dispose();
            cnn.Close();



        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return Registration.Tables[0];
    }

    private DataTable GetRenewals(string Yearr)
    {
        DataSet Registration = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =0";
            string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =1";

            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(Registration, "Borrowers");

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return Registration.Tables[0];
    }

    private DataTable GetReg(string Yearr)
    {

        DataSet IdCard = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT B.[Passport],A.[BorrowerID],A.[Name],A.[Email],A.[Borrowertype],A.[Faculty],A.[Department],A.[DateRegistered],A.[Status],A.[password],A.[Session],A.[ExpDate],A.[RenewedDate],A.[RenewedStatus]  FROM [Borrowers] A, Pictures B where A.[BorrowerID]= B.[StudentId] and A.[Session] = '" + Yearr + "' and A.[Status] = 1 and A.[RenewedStatus] =0";
            string query = "SELECT B.[Picture],A.[MatricNumber],(A.[Surname] + ' ' + A.[OtherNames]) as [Names], A.[AcademicLevel],A.[ModeOfStudy],A.[Programme],(select C.[FacultyPrefix] from [Faculty] C where C.[FacultyID] = A.[FacultyID]) as Facultys,A.[CourseOfStudyName],A.[PresentSession],A.[NextKin],A.[NextkinAddress]  FROM [Students] A, PictureFile B where A.[RegNo]= B.[PicKey] and A.[PresentSession] = '" + Yearr + "'";


            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(IdCard, "Students");

            cnn.Dispose();
            cnn.Close();



        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return IdCard.Tables[0];
    }

    private DataTable GetMonthlyLeave(string Yearr, string monthint, string monthTxt)
    {
        string startmonth = "";
        string endmonth = "";

        int yr = int.Parse(Yearr);
        int mnt = int.Parse(monthint);

        int monthlastday = DateTime.DaysInMonth(yr, mnt);

        startmonth = Yearr + "-" + mnt.ToString("00") + "-" + "01";
        endmonth = Yearr + "-" + mnt.ToString("00") + "-" + monthlastday.ToString("00");



        DataSet monthlyleave = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "SELECT * from [Leave] where [StartDate] >= '" + startmonth + "' and [EndDate] <= '" + endmonth + "' order by [StartDate]";


            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(monthlyleave, "Leave");

            cnn.Dispose();
            cnn.Close();



        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return monthlyleave.Tables[0];
    }

    //private DataTable GetOrg(string Yearr)
    //{

    //}

    //private DataSet getpayslipall(string month, string yr)
    //{
    //    SqlConnection cnn = new SqlConnection(str);
    //    cnn.Open();
    //    DataTable dt = new DataTable();
    //    DataTable dt2 = new DataTable();
    //    SqlDataAdapter dat = null;
    //    DataSet PayslipAllawance = new DataSet();
    //    DataSet PayslipDeductions = new DataSet();
    //    DataSet IndPayslipSumery = new DataSet();

    //    try
    //    {
    //        ////dat = new SqlDataAdapter("SELECT * FROM [PaySlip] where [EmpID] ='" + staffid + "' and [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and [MinistryFaculty] ='" + fact + "' and [DepartmentSection] ='" + dept + "' and lower(PayItemType) = 'allowance'", cnn);
    //        //////dat.Fill(IndPayslipAllawi);
    //        ////dat.Fill(IndPayslipAllawi, "PaySlip");

    //        ////dat = new SqlDataAdapter("SELECT * FROM [PaySlipDeductionTemp]", cnn);
    //        //////dat.Fill(IndPayslipDeduct2);
    //        ////dat.Fill(IndPayslipDeduct2, "PaySlipDeductionTemp");

    //        ////dat = new SqlDataAdapter("SELECT * FROM [PayrollSummary] where [EmpID] ='" + staffid + "' and [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and [MinistryFaculty] ='" + fact + "' and [DepartmentSection] ='" + dept + "'", cnn);
    //        //////dat.Fill(IndPayslipSumery);
    //        ////dat.Fill(IndPayslipSumery, "PayrollSummary1");

    //        string Sqlq = "SELECT * FROM [PaySlip] where [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and Amount > 0 ";
    //        dat = new SqlDataAdapter(Sqlq, cnn);
    //        //dat.Fill(IndPayslipAllawi);
    //        dat.Fill(PayslipAllawance, "PaySlip1");

    //        ////Sqlq = "SELECT * FROM [PaySlip] where [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and lower(PayItemType) = 'deduction' and Amount > 0 ";

    //        ////dat = new SqlDataAdapter(Sqlq, cnn);

    //        ////dat.Fill(PayslipAllawance, "PaySlipDeduct");

    //        //dat = new SqlDataAdapter("SELECT * FROM [PaySlip] where [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and lower(PayItemType) = 'allowance'", cnn);

    //        //dat.Fill(dt);
    //        //dat = new SqlDataAdapter("SELECT * FROM [PaySlip] where [PayMonth] ='" + month + "' and [PayYear] ='" + yr + "' and lower(PayItemType) = 'deduction'", cnn);

    //        //dat.Fill(dt2);

    //        //PayslipAllawance.Tables.Add(dt);
    //        //PayslipAllawance.Tables.Add(dt2);


    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //    return PayslipAllawance;
    //}


    private DataTable GetSuspendedstaff(string Monthh, string Yearr)
    {

        SqlConnection cnn = new SqlConnection(str);
        cnn.Open();


        DataSet ds = new DataSet();
        DataTable AdmRow = new DataTable();

        try
        {
            //string qry = "select  * from  PaySlip where (PayMonth = '" + Monthh + "' and PayYear = '" + Yearr + "' ) or (PayMonth = '" + LastPeriodMonthh + "' and PayYear = '" + LastPeriodYear + "')";

            string qry = "SELECT StatusCode,StatusName,PayrollEffect,EmpId,Surname,OtherNames,MinistryFaculty,DepartmentSection,SalaryStructureGroup,Designation,GradeLevel,Step from  UnprocessedStaff  where [StatusCode] = 8 and [PayMonth]='" + Monthh + "' and [PayYear]='" + Yearr + "'";
            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);

            dat.Fill(ds);
            int m = ds.Tables[0].Rows.Count;
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {
            //string ffg = ex.Message;
            // logger.Error(ex.Message);
        }


        return AdmRow;
    }

    private DataTable GetComparativeJournal(string Monthh, string Yearr, string LastPeriodMonthh, string LastPeriodYear)
    {
        SqlConnection cnn = new SqlConnection(str);
        cnn.Open();


        DataSet ds = new DataSet();
        DataTable AdmRow = new DataTable();

        try
        {
            string qry = "select  * from  PaySlip where (PayMonth = '" + Monthh + "' and PayYear = '" + Yearr + "' ) or (PayMonth = '" + LastPeriodMonthh + "' and PayYear = '" + LastPeriodYear + "')";


            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);

            dat.Fill(ds);
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {
            //string ffg = ex.Message;
            // logger.Error(ex.Message);
        }


        return AdmRow;
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

    private void getLeaveInfo(string EmpId, string LeaveType, string empgroup, string Yr)
    {
        try
        {
            DataSet ds = null;// new DataSet();            

            //get max leave days
            string qry = "SELECT [LeaveDays] FROM [LeaveDaysgroup] where [LeaveType] = '" + LeaveType + "' and LeaveGroup = '" + empgroup + "'";

            ds = SearchData(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    Maxdays = int.Parse(ds.Tables[0].Rows[jj][0].ToString());
                    //TxtEntledDays.Text = Maxdays.ToString();
                }
            }
            else
            {
                Maxdays = 0;
            }


            //string yr = DateTime.Now.Year.ToString();
            //get leave history
            qry = "SELECT sum(DaysApplied) as n FROM [LeaveHistory] where [LeaveType] = '" + LeaveType + "' and Staffid = '" + EmpId + "' and year = '" + Yr + "'";

            ds = SearchData(qry);

            int spentdays = 0;
            //int avaidays = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
                {
                    string val = ds.Tables[0].Rows[jj][0].ToString();
                    if (val != "")
                    {
                        spentdays = int.Parse(ds.Tables[0].Rows[jj][0].ToString());

                    }
                    else
                    {
                        spentdays = 0;
                    }
                }

            }
            else
            {
                spentdays = 0;
            }

            UsedDays = spentdays;//.ToString();
            avaidays = Maxdays - spentdays;

        }
        catch (Exception ex)
        {

        }

    }
    private void showrpt2(DataTable tb, string Monthh, string Year)
    {
        string header = "[Transaction Reference],[OGNO],[Surname],[Other Names],[NetPay],[Customer Debit Account No],[Payment Code],[Beneficiary Code],[Beneficiary Account No],[Routing Bank Code],[Bank],[Customer Debit Account No],[Branch Name]";

        string filename = "BankSchedule for" + Monthh + "" + Year;
        StringBuilder stb = new StringBuilder();

        stb.Append(header);
        stb.AppendLine();

        String msg;
        int j;
        int k;
        int i;
        int m;

        try
        {
            if (tb != null)
            {

                k = tb.Rows.Count;
                j = tb.Columns.Count;
                //string rw = tb.Rows[1][2].ToString();
                //j = ds.Tables[0].Columns.Count;
                ////j = j - 1;
                //k = ds.Tables[0].Rows.Count;

                for (i = 0; i < k; i++)
                {
                    for (m = 0; m < j; m++)
                    {
                        if (m == 0)
                        {
                            stb.Append(tb.Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                            //stb.Append(ds.Tables[0].Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
                        }
                        else
                        {
                            stb.Append("," + tb.Rows[i][m].ToString().Replace(",", ";").ToLower().Replace("\r\n", ""));
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
            //msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            //showmassage(msg);
            //return;
        }
    }

    protected void PrintToPdf()
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "";
            if (ReportTypeGlobal == "Earning Analysis" || ReportTypeGlobal == "Deduction Analysis")
            {
                FilNamm = ReportTypeGlobal + " Excel Report";
                RptDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, FilNamm);
            }
            else
            {
                FilNamm = ReportTypeGlobal + " PDF Report";
                RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ex = null;
        }
        finally
        {
            RptDoc.Close();
            RptDoc.Dispose();
        }

    }
}
