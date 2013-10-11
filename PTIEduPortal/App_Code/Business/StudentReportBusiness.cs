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
/// <summary>
/// Summary description for StudentReportBusiness
/// </summary>
public class StudentReportBusiness
{
	public StudentReportBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable ByCourse(int cStudyId, string aLevel, string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByCourse");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId));
            db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("Report_Student_ByCourseLevel");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }

    
    public static DataTable ByLga(int cStudyId, string aLevel,string lga, string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("ByLga");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@CourseOfStudyId", cStudyId));
            db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
            db.AddParameter(new SqlParameter("@Lga", lga));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("Report_Student_ByLga");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable GlobalList(string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("Global");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("Report_Student_GlobalList");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable GlobalList(string aLevel, string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("Global");
        string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));
            ds = db.ExecuteDataSet("Report_Student_GlobalListByLevel");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    //
    public static DataTable ListByStateLevel(string aLevel, string aSession)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("Global");

        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@AcademicLevel", aLevel));
            db.AddParameter(new SqlParameter("@AcadSession", aSession));

            ds = db.ExecuteDataSet("Report_Student_ListByStateLevel");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable DistributionByLga(string state)
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("Global");

        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@State", state));
            ds = db.ExecuteDataSet("Report_Student_DistributionByLga");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable DistributionByState()
    {
        DBAccess db = new DBAccess();

        DataSet ds = new DataSet("Global");
        DataTable AdmRow = new DataTable();
        try
        {
            ds = db.ExecuteDataSet("Report_Student_DistributionByState");
        }
        catch (Exception ex)
        { }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }

}
