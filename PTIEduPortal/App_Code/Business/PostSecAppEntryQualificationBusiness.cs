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
using CybSoft.EduPortal.Data;

/// <summary>
/// Summary description for EntryQualificationBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{
    public class PostSecAppEntryQualificationBusiness
    {
        public PostSecAppEntryQualificationBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool SaveRecord(PostSecAppEntryQualification eq)
        {
            DBAccess db = new DBAccess();


            db.Parameters.Add(new SqlParameter("@RegNo", eq.RegNo));
            db.Parameters.Add(new SqlParameter("@UserName", eq.UserName));
            db.Parameters.Add(new SqlParameter("@ExamName", eq.ExamName));
            db.Parameters.Add(new SqlParameter("@ExamRegNo", eq.ExamRegNo));
            db.Parameters.Add(new SqlParameter("@ExamDate", eq.ExamDate));
            db.Parameters.Add(new SqlParameter("@NameOfSchool",eq.NameOfSchool ));
            db.Parameters.Add(new SqlParameter("@SubjectName1", eq.SubjectName1));
            db.Parameters.Add(new SqlParameter("@SubjectGrade1", eq.SubjectGrade1));
            db.Parameters.Add(new SqlParameter("@SubjectName2", eq.SubjectName2));
            db.Parameters.Add(new SqlParameter("@SubjectGrade2", eq.SubjectGrade2));
            db.Parameters.Add(new SqlParameter("@SubjectName3", eq.SubjectName3));
            db.Parameters.Add(new SqlParameter("@SubjectGrade3", eq.SubjectGrade3));
            db.Parameters.Add(new SqlParameter("@SubjectName4", eq.SubjectName4));
            db.Parameters.Add(new SqlParameter("@SubjectGrade4", eq.SubjectGrade4));
            db.Parameters.Add(new SqlParameter("@SubjectName5", eq.SubjectName5));
            db.Parameters.Add(new SqlParameter("@SubjectGrade5", eq.SubjectGrade5));
            db.Parameters.Add(new SqlParameter("@SubjectName6", eq.SubjectName6));
            db.Parameters.Add(new SqlParameter("@SubjectGrade6", eq.SubjectGrade6));
            

            int i = db.ExecuteNonQuery("PostSecApplicantEntryQualification_Insert");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;
        }

        public static PostSecAppEntryQualification getRecord(string formNumber, string sitting)
        {
            DBAccess db = new DBAccess();
            DataTable NowTable = null;
            DataRow nr = null;
            PostSecAppEntryQualification eq = new PostSecAppEntryQualification();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", formNumber));
               
                DataSet ds = new DataSet("Enq");
                ds = db.ExecuteDataSet("getPostSecEntryQualByFormNumber");
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                eq.ExamName = nr["ExamName"].ToString();
                eq.ExamDate = nr["ExamDate"].ToString();
                eq.ExamRegNo = nr["ExamRegNo"].ToString();
                eq.NameOfSchool = nr["NameOfSchool"].ToString();
                eq.SubjectName1 = nr["SubjectName1"].ToString();
                eq.SubjectName2 = nr["SubjectName2"].ToString();
                eq.SubjectName3 = nr["SubjectName3"].ToString();
                eq.SubjectName4 = nr["SubjectName4"].ToString();
                eq.SubjectName5 = nr["SubjectName5"].ToString();
                eq.SubjectName6 = nr["SubjectName6"].ToString();
                eq.SubjectName7 = nr["SubjectName7"].ToString();
                eq.SubjectGrade1 = nr["SubjectGrade1"].ToString();
                eq.SubjectGrade2 = nr["SubjectGrade2"].ToString();
                eq.SubjectGrade3 = nr["SubjectGrade3"].ToString();
                eq.SubjectGrade4 = nr["SubjectGrade4"].ToString();
                eq.SubjectGrade5 = nr["SubjectGrade5"].ToString();
                eq.SubjectGrade6 = nr["SubjectGrade6"].ToString();
                eq.SubjectGrade7 = nr["SubjectGrade7"].ToString();
                



            }
            catch (Exception exx)
            {

            }

            db.Dispose();
            return eq;

        }

        public static bool SavePostRecord(ApplicantPostQualification eq)
        {
            DBAccess db = new DBAccess();


            db.Parameters.Add(new SqlParameter("@RegNo", eq.RegNo));
            db.Parameters.Add(new SqlParameter("@PostMatric", eq.PostMatric));
            db.Parameters.Add(new SqlParameter("@CourseGrade", eq.CourseGrade));
            db.Parameters.Add(new SqlParameter("@CourseName", eq.CourseName));
            db.Parameters.Add(new SqlParameter("@Institution", eq.Institution));
            db.Parameters.Add(new SqlParameter("@QualyType", eq.QualyType));
            db.Parameters.Add(new SqlParameter("@QualYear", eq.QualYear));

            try
            {
                int i = db.ExecuteNonQuery("PostApplicantEntryQualification_Insert");
                db.Dispose();
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static ApplicantPostQualification getRecord(string formNumber)
        {
            DBAccess db = new DBAccess();
            DataTable NowTable = null;
            DataRow nr = null;
            ApplicantPostQualification eq = new ApplicantPostQualification();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", formNumber));

                DataSet ds = new DataSet("Enq");
                ds = db.ExecuteDataSet("getPostQualificationEntryByFormNumber");
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                eq.CourseGrade = (nr["CourseGrade"] != null) ? nr["CourseGrade"].ToString() : "";
                eq.CourseName = (nr["CourseName"] != null) ? nr["CourseName"].ToString() : "";
                eq.Institution = (nr["Institution"] != null) ? nr["Institution"].ToString() : "";
                eq.PostMatric = (nr["PostMatric"] != null) ? nr["PostMatric"].ToString() : "";
                eq.QualyType = (nr["QualyType"] != null) ? nr["QualyType"].ToString() : "";
                eq.QualYear = (nr["QualYear"] != null) ? nr["QualYear"].ToString() : "";
                eq.RegNo = (nr["RegNo"] != null) ? nr["RegNo"].ToString() : "";

            }
            catch (Exception exx)
            {

            }

            db.Dispose();
            return eq;

        }

    }
}
