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
            string Title = "PETROLEUM TRAINING INSTITUTE, WARRI, NIGERIA";

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
                else if (ReportType == "Applicants")
                {

                    //RptPath = Server.MapPath("ApplicantExamCards.rpt");
                    RptPath = Server.MapPath("ApplicantsExamCards.rpt");
                    AdmDt = GetApplicantExamCard(Yearr, Programme, StudyMode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                   
                    //RptDoc.DataDefinition.FormulaFields["Session"].Text = "'" + Yearr  + "'";
                    //RptDoc.DataDefinition.FormulaFields["Programme"].Text = "'" + Programme.ToUpper() + "'";
                   
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

                else if (ReportType == "Exam Cards")
                {
                    RptPath = Server.MapPath("ExamCards.rpt");
                    AdmDt = GetExamCards(Yearr, Programme, StudyMode, CourseCode);
                    RptDoc.Load(RptPath);
                    RptDoc.SetDataSource(AdmDt);

                    Title2 = "Programme: " + Programme.ToUpper() + ", " + " Study Mode: " + StudyMode.ToUpper();

                    //if (StartDate != "" && EndDate != "")
                    //{
                    //    daterange = "Summer School Registration between the period of" + " " + StartDate + " " + "To" + " " + EndDate + "." + " " + Yearr + " " + "academic session";
                    //}
                    //else
                    //{
                    //    daterange = "Summer School Registration for " + Yearr + " " + "academic session";
                    //}
                    //ReportTitle = "PaySlip For " + MonthhTxt + ", " + Yearr;

                    daterange = "Paid and registered students for " + CourseCode + " ," + Yearr + " " + "academic session";

                    RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
                    RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
                    RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + daterange + "'";

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

    private DataTable GetExamCards(string Yearr, string Programme, string StudyMode, string CourseCode)
    {
        DataSet ExamCards = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            //string query = "SELECT D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department FROM [Students] A, PictureFile B, CourseRegistration D, SchoolFeesPin E  where D.[MatricNumber] = A.[RegNo] and D.[MatricNumber] = B.[PicKey] and D.[MatricNumber] = E.[UsedBy] and D.[SessionName] = E.[SessionName] and D.[SessionName] = '" + Yearr + "' and D.[CourseCode] = '" + CourseCode + "' and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "'";

            string query = "SELECT D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[CourseCode] = '" + CourseCode + "' and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "'";


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

    private DataTable GetApplicantExamCard(string session, string programme, string studyMode)
    {
        var  applicantExamCards = new DataSet( ) ;
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            string query = "";

            var cmd = cnn.CreateCommand();
          
            query = "SELECT * FROM vwApplicants "
                + " WHERE  [Session] = '" + session + "' and [Programme]= '" +
                programme + "' and [ModeOfStudy]= '" + studyMode + "' Order By [Names]";
            cmd.CommandText = query;        

            //var dat = new SqlDataAdapter(query, cnn);
            var dat = new SqlDataAdapter(cmd);
            dat.Fill(applicantExamCards);
            cnn.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
        }
        return applicantExamCards.Tables[0];
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
