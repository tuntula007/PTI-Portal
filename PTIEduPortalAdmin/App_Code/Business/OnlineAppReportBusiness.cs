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
/// <summary>
/// Summary description for OnlineAppReportBusiness
/// </summary>
public class OnlineAppReportBusiness
{
	public OnlineAppReportBusiness()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet getAddmissionCourses(string pcode)
    {
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter("@ProgrammeTag", pcode));
        DataSet ds;
        ds = db.ExecuteDataSet("getAddmissionCourses");
        db.Dispose();
        return ds;
    }
    public static DataSet getAddmissionDepartment(string programme)
    {
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter("@Programme", programme));
        DataSet ds;
        ds = db.ExecuteDataSet("getAddmissionDepartment");
        db.Dispose();
        return ds;
    }
    public static DataSet  getCourseBySchool(int schid)
    {

        string key = "getCourseBySchool" +  schid.ToString(); 
   
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        db.AddParameter(new SqlParameter("@FacId", schid));
        ds = db.ExecuteDataSet("getCourseBySchool");
        return ds;
    }


    public static DataTable getEntranceExamCard(string Programme)
    {
        string key = "getEntranceExamCard";
        DBAccess db = new DBAccess(key);
        
        DataSet ds = new DataSet("key");
      
        DataTable AdmRow = new DataTable();
        try
        {
        //    db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@Programme", Programme));
            ds = db.ExecuteDataSet("getEntranceExamCard");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {
            string s = ex.Message;
        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }


    public static DataTable NDApplicants(string Studymode, int facultyid, string prog, int course)
    {
        string key = "NDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog; 
        DBAccess db = new DBAccess(key);
        
        DataSet ds = new DataSet("key");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            ds = db.ExecuteDataSet("Reports_NDApplicants");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {
 
        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable NDApplicants(string Studymode, int facultyid, string prog, int course, string rptType)
    {
        string key = "NDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog + rptType;
        DBAccess db = new DBAccess(key);

        DataSet ds = new DataSet("key");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            if (rptType == "2")
            {

                ds = db.ExecuteDataSet("Reports_NDApplicants_NonIndegene");
            }
            else
            {
                ds = db.ExecuteDataSet("Reports_NDApplicants_Indegene");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
  
    public static DataTable HNDApplicants(string Studymode, int facultyid,string prog, int course)
    {

        string key = "HNDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            ds = db.ExecuteDataSet("Reports_HNDApplicants");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable HNDApplicants(string Studymode, int facultyid, string prog, int course, string rptType)
    {

        string key = "HNDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog + rptType;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            if (rptType == "2")
            {
                ds = db.ExecuteDataSet("Reports_HNDApplicants_NonIndegene");
            }
            else
            {
                ds = db.ExecuteDataSet("Reports_HNDApplicants_Indegene");
            }
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }

    public static DataTable PreNDApplicants(string Studymode, int facultyid, string prog, int course)
    {

        string key = "PNDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            ds = db.ExecuteDataSet("Reports_PreNDApplicants");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable PreNDApplicants(string Studymode, int facultyid, string prog, int course, string rptType)
    {

        string key = "PNDApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog + rptType;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course));
            ds = db.ExecuteDataSet("Reports_PreNDApplicants");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }

    public static DataTable Others(string Studymode, int facultyid, string prog, int course)
    {
        DBAccess db = new DBAccess();
        string key = "OtherApplicants" + Studymode + facultyid.ToString() + course.ToString() + prog;
        DataSet ds = new DataSet(key);
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));
            db.AddParameter(new SqlParameter("@courseid", course ));
            ds = db.ExecuteDataSet("Reports_PreNDApplicants");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    //[Reports_NDApplicants_All]
    public static DataTable NDApplicantsAll(string Studymode)
    {

        string key = "NDApplicantsAll" + Studymode;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));

            ds = db.ExecuteDataSet("Reports_NDApplicants_All");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable HNDApplicantsAll(string Studymode)
    {

        string key = "HNDApplicantsAll" + Studymode;
        DBAccess db = new DBAccess(key);
        DataSet ds = new DataSet("k");
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Studymode));

            ds = db.ExecuteDataSet("Reports_HNDApplicants_All");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;

    }
    public static DataTable InterviewList(string Mode, string Programme,string Course)
    {
        DBAccess db = new DBAccess();
        string key = "InterviewList" + Mode + Programme+ Course ;
        DataSet ds = new DataSet(key);
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Mode));
            db.AddParameter(new SqlParameter("@Programme", Programme));
            db.AddParameter(new SqlParameter ("@FirstDepartment",Course ));
            ds = db.ExecuteDataSet("Reports_InterviewList");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;
    }
    public static DataTable InterviewListOlevel(string Mode, string Programme, string Course)
    {
        DBAccess db = new DBAccess();
        string key = "InterviewListOlevel" + Mode + Programme + Course;
        DataSet ds = new DataSet(key);
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Modeofstudy", Mode));
            db.AddParameter(new SqlParameter("@Programme", Programme));
            db.AddParameter(new SqlParameter("@FirstDepartment", Course));
            ds = db.ExecuteDataSet("Reports_InterviewListOlevel");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;
    }

    public static DataTable ApplicationStat(string prog)
    {
        DBAccess db = new DBAccess();
        string key = "ApplicationStat" +  prog;
        DataSet ds = new DataSet(key);
        //string Qstring = "";
        DataTable AdmRow = new DataTable();
        try
        {
            db.AddParameter(new SqlParameter("@Programme", prog));
            ds = db.ExecuteDataSet("Reports_ApplicationStat");
            AdmRow = ds.Tables[0];
        }
        catch (Exception ex)
        {

        }
        ds.Dispose();
        db.Dispose();
        return AdmRow;
    }
}
