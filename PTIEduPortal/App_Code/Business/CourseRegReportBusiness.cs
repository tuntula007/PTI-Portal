using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
/// <summary>
/// Summary description for ReportBusiness
/// </summary>
public class CourseRegReportBusiness
{
	public CourseRegReportBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable ByCourseAlone(string cCode, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseCode", cCode));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByCourseAlone");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseAloneNot(string cCode, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseCode", cCode));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByCourseAloneNot");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseAloneDetails(string cCode, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseCode", cCode));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationDetails_ByCourseAlone");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StudentsListCourseAlone(string cCode, string aSession, string Mode, string StudCategory)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@CourseCode", cCode));
            db.AddParameter(new SqlParameter("@StudCategory", StudCategory));
            ds = db.ExecuteDataSet("StudentsList_ByCourseAlone");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable PinUsageCourseAlone(string TableNamme, string cCode, string aSession, string Mode, string StudGroup)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseCode", cCode));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            if (TableNamme == "SchoolFeesPin")
            {
                db.AddParameter(new SqlParameter("@StudGroup", StudGroup));
                ds = db.ExecuteDataSet("SchoolFeePinReport_ByCourseAlone");
            }
            else if (TableNamme == "StudioFeesPin")
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("StudioFeePinReport_ByCourseAlone");
            }
            else
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("AdmissionLetterFeePinReport_ByCourseAlone");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByLevelAlone(string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByLevelAlone");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        AdmRow = ds.Tables[0];
        return AdmRow;

    }
    public static DataTable ByLevelAloneNot(string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try{
        db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
        db.AddParameter(new SqlParameter("@AcadSession", aSession));
        db.AddParameter(new SqlParameter("@Mode", Mode));
        db.AddParameter(new SqlParameter("@Semester", Semester));
        ds = db.ExecuteDataSet("RegistrationReport_ByLevelAloneNot");
        AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByLevelAloneDetails(string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationDetails_ByLevelAlone");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StudentsListLevelAlone(string AcadLevel, string aSession, string Mode, string StudCategory)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@StudCategory", StudCategory));
            ds = db.ExecuteDataSet("StudentsList_ByLevelAlone");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable PinUsageLevelAlone(string TableNamme, string AcadLevel, string aSession, string Mode, string StudGroup)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            if (TableNamme == "SchoolFeesPin")
            {
                db.AddParameter(new SqlParameter("@StudGroup", StudGroup));
                ds = db.ExecuteDataSet("SchoolFeePinReport_ByLevelAlone");
            }
            else if (TableNamme == "StudioFeesPin")
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("StudioFeePinReport_ByLevelAlone");
            }
            else
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("AdmissionLetterFeePinReport_ByLevelAlone");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByAll(string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByAllNot(string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByAllNot");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByAllDetails(string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationDetails_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StudentsListAll(string aSession, string Mode, string StudCategory)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@StudCategory", StudCategory));
            ds = db.ExecuteDataSet("StudentsList_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable PinUsageByAll(string TableNamme, string aSession, string Mode,string StudGroup)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            if (TableNamme == "SchoolFeesPin")
            {
                db.AddParameter(new SqlParameter("@StudGroup", StudGroup));
                ds = db.ExecuteDataSet("SchoolFeePinReport_ByAll");

            }
            else if (TableNamme == "StudioFeesPin")
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("StudioFeePinReport_ByAll");
            }
            else if (TableNamme == "AdmissionLetterFeePin")
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("AdmissionLetterFeePinReport_ByAll");
            }
            else
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("AllFeePinReport_ByAll");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseLevel(string Ccode, string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@Ccode", Ccode));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByCourseLevel");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseLevelNot(string Ccode, string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@CourseCode", Ccode));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("RegistrationReport_ByCourseLevelNot");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable ByCourseLevelDetails(string Ccode, string AcadLevel, string aSession, string Mode, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@CourseCode", Ccode));

            ds = db.ExecuteDataSet("RegistrationDetails_ByCourseLevel");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StudentsListCourseLevel(string Ccode, string AcadLevel, string aSession, string Mode,string StudCategory)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            db.AddParameter(new SqlParameter("@CourseCode", Ccode));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@StudCategory", StudCategory));
            ds = db.ExecuteDataSet("StudentsList_ByCourseLevel");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable PinUsageByCourseLevel(string TableNamme, string Ccode, string AcadLevel, string aSession, string Mode, string StudGroup)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseCode", Ccode));
            db.AddParameter(new SqlParameter("@AcadLevel", AcadLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Mode", Mode));
            if (TableNamme == "SchoolFeesPin")
            {
                db.AddParameter(new SqlParameter("@StudGroup", StudGroup));
                ds = db.ExecuteDataSet("SchoolFeePinReport_ByCourseLevel");
            }
            else if (TableNamme == "StudioFeesPin")
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("StudioFeePinReport_ByCourseLevel");
            }
            else
            {
                string PassG = "0";
                if (StudGroup == "Evening")
                {
                    PassG = "1";
                }
                db.AddParameter(new SqlParameter("@StudGroup", PassG));
                ds = db.ExecuteDataSet("AdmissionLetterFeePinReport_ByCourseLevel");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatisticalRegByAll(string aSession, string Semester)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            ds = db.ExecuteDataSet("StatisticalRegReport_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatisticalStudListByAll(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("StatisticalStudListReport_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatAdmissionLetterPaymentByAll(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("StatisticalAdmissionLetterPayment_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatSchoolFeePaymentByAllM(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("StatisticalSchoolFeePayment-M_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatSchoolFeePaymentByAllE(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("StatisticalSchoolFeePayment-E_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable StatStudioFeePaymentByAll(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourseAll");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("StatisticalStudioFeePayment_ByAll");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }

    //public static DataTable ByAllCourses(string aSession, string Semester)
    //{
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByAllCourses");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    db.AddParameter(new SqlParameter("@Semester", Semester));
    //    ds = db.ExecuteDataSet("RegistrationReport_ByAllCourses");

    //    return AdmRow;

    //}

    //public static DataTable ByClass(int cStudyId, string aLevel, string aSession)
    //{
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByClass");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId  ));
    //    db.AddParameter(new SqlParameter("@AcademicLevel",aLevel  ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByClass");

    //    return AdmRow;

    //}
    //public static DataTable ByStudentClass(int cStudyId, string aLevel, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByClass");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId));
    //    db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByStudentClass");

    //    return AdmRow;

    //}
    //public static DataTable ByStudent( string mNumber, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByStudent");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

        
    //    db.AddParameter(new SqlParameter("@MatricNumber", mNumber ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_ByStudent");

    //    return AdmRow;

    //}
    
    //public static DataTable YetToRegister( int cStudyId , string aLevel, string aSession)
    //{
        
    //    DBAccess db = new DBAccess();

    //    DataSet ds = new DataSet("ByStudent");
    //    string Qstring = "";
    //    DataTable AdmRow = new DataTable();

    //    db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId ));
    //    db.AddParameter(new SqlParameter("@AcademicLevel", aLevel  ));
    //    db.AddParameter(new SqlParameter("@AcadSession", aSession));
    //    ds = db.ExecuteDataSet("Report_Registration_YetToRegister");

    //    return AdmRow;

    //}
    public bool SaveStudentPassPort(string MatricNo, string PixPath)
    {
        bool RetValue = false;
        DBAccess db = new DBAccess();
        try
        {
            FileStream fs = null;
            SqlParameter param = null;
            fs = new FileStream(PixPath, FileMode.Open, FileAccess.Read);
            Byte[] blob = new Byte[fs.Length];
            fs.Read(blob, 0, blob.Length);
            fs.Close();
            param = new SqlParameter("@Picture", SqlDbType.VarBinary, blob.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blob);
            db.Parameters.Add(new SqlParameter("@MatricNo", MatricNo));
            db.Parameters.Add(param);
            int rett = db.ExecuteNonQuery("InsertStudentsPassport");
            if (rett != -1)
            {
                RetValue = true;
            }
            else
            {
                RetValue = false;
            }
            db.Dispose();

        }
        catch (Exception ex)
        {
            string ss = ex.Message;
        }
        db.Dispose();
        return RetValue;
    }
}
