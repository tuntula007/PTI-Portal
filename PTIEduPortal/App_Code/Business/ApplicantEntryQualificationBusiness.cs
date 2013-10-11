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
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Summary description for EntryQualificationBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{
    public class ApplicantEntryQualificationBusiness
    {
        public ApplicantEntryQualificationBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool SaveRecord(ApplicantEntryQualification eq)
        {
            DBAccess db = new DBAccess();


            db.Parameters.Add(new SqlParameter("@RegNo", eq.RegNo));
            db.Parameters.Add(new SqlParameter("@UserName", eq.UserName));
            db.Parameters.Add(new SqlParameter("@ExamName", eq.ExamName));
            db.Parameters.Add(new SqlParameter("@ExamRegNo", eq.ExamRegNo));
            db.Parameters.Add(new SqlParameter("@ExamDate", eq.ExamDate));
            db.Parameters.Add(new SqlParameter("@Sitting", int.Parse(eq.Sitting)));
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
            db.Parameters.Add(new SqlParameter("@SubjectName7", eq.SubjectName7));
            db.Parameters.Add(new SqlParameter("@SubjectGrade7", eq.SubjectGrade7));
            db.Parameters.Add(new SqlParameter("@SubjectName8", eq.SubjectName8));
            db.Parameters.Add(new SqlParameter("@SubjectGrade8", eq.SubjectGrade8));
            db.Parameters.Add(new SqlParameter("@SubjectName9", eq.SubjectName9));
            db.Parameters.Add(new SqlParameter("@SubjectGrade9", eq.SubjectGrade9));
            db.Parameters.Add(new SqlParameter("@SubjectName10", eq.SubjectName10));
            db.Parameters.Add(new SqlParameter("@SubjectGrade10", eq.SubjectGrade10));
            db.Parameters.Add(new SqlParameter("@ModeOfStudy", eq.ModeOfStudy));
            //db.Parameters.Add(new SqlParameter("@ScannedCopy", eq.ScannedResult));

            int i = db.ExecuteNonQuery("ApplicantEntryQualification_Insert");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;
        }

        public static ApplicantEntryQualification getRecord(string formNumber, string sitting)
        {
            DBAccess db = new DBAccess();
            DataTable NowTable = null;
            DataRow nr = null;
            ApplicantEntryQualification eq = new ApplicantEntryQualification();
            
            
            
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", formNumber));
                db.Parameters.Add(new SqlParameter("@Sitting", sitting));
                DataSet ds = new DataSet("Enq");
                ds = db.ExecuteDataSet("getEntryQualByFormNumber");
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                eq.RegNo = nr["RegNo"].ToString();
                eq.ExamName = nr["ExamName"].ToString();
                eq.ExamDate = nr["ExamDate"].ToString();
                eq.ExamRegNo = nr["ExamRegNo"].ToString();
                eq.SubjectName1 = nr["SubjectName1"].ToString();
                eq.SubjectName2 = nr["SubjectName2"].ToString();
                eq.SubjectName3 = nr["SubjectName3"].ToString();
                eq.SubjectName4 = nr["SubjectName4"].ToString();
                eq.SubjectName5 = nr["SubjectName5"].ToString();
                eq.SubjectName6 = nr["SubjectName6"].ToString();
                eq.SubjectName7 = nr["SubjectName7"].ToString();
                eq.SubjectName8 = nr["SubjectName8"].ToString();
                eq.SubjectName9 = nr["SubjectName9"].ToString();
                eq.SubjectName10 = nr["SubjectName10"].ToString();

                eq.SubjectGrade1 = nr["SubjectGrade1"].ToString();
                eq.SubjectGrade2 = nr["SubjectGrade2"].ToString();
                eq.SubjectGrade3 = nr["SubjectGrade3"].ToString();
                eq.SubjectGrade4 = nr["SubjectGrade4"].ToString();
                eq.SubjectGrade5 = nr["SubjectGrade5"].ToString();
                eq.SubjectGrade6 = nr["SubjectGrade6"].ToString();
                eq.SubjectGrade7 = nr["SubjectGrade7"].ToString();
                eq.SubjectGrade8 = nr["SubjectGrade8"].ToString();
                eq.SubjectGrade9 = nr["SubjectGrade9"].ToString();
                eq.SubjectGrade10 = nr["SubjectGrade10"].ToString();
                //eq.ScannedResult =(byte[]) (nr["ScannedCopy"].ToString());
                eq.Sitting = nr["Sitting"].ToString();



            }
            catch (Exception exx)
            {

            }

            db.Dispose();
            return eq;

        }

        public static DataTable getRecord(string formNumber)
        {
            DBAccess db = new DBAccess();
            DataTable NowTable = null;
            DataTable dtRecordList = new DataTable();
            List<ApplicantEntryQualification> qRecordsList = new List<ApplicantEntryQualification>();
            DataRow nr = null;
            

            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", formNumber));
                DataSet ds = new DataSet("Enq");
                ds = db.ExecuteDataSet("LoadEntryQualByFormNumber");
                NowTable = ds.Tables[0];
                for (int i = 0; i < NowTable.Rows.Count; i++)
                {
                    nr = NowTable.Rows[i];
                    ApplicantEntryQualification eq = new ApplicantEntryQualification();
                    eq.ExamName = nr["ExamName"].ToString();
                    eq.ExamDate = nr["ExamDate"].ToString();
                    eq.ExamRegNo = nr["ExamRegNo"].ToString();
                    eq.SubjectName1 = nr["SubjectName1"].ToString();
                    eq.SubjectName2 = nr["SubjectName2"].ToString();
                    eq.SubjectName3 = nr["SubjectName3"].ToString();
                    eq.SubjectName4 = nr["SubjectName4"].ToString();
                    eq.SubjectName5 = nr["SubjectName5"].ToString();
                    eq.SubjectName6 = nr["SubjectName6"].ToString();
                    eq.SubjectName7 = nr["SubjectName7"].ToString();
                    eq.SubjectName8 = nr["SubjectName8"].ToString();
                    eq.SubjectName9 = nr["SubjectName9"].ToString();
                    eq.SubjectName10 = nr["SubjectName10"].ToString();
                    eq.SubjectGrade1 = nr["SubjectGrade1"].ToString();
                    eq.SubjectGrade2 = nr["SubjectGrade2"].ToString();
                    eq.SubjectGrade3 = nr["SubjectGrade3"].ToString();
                    eq.SubjectGrade4 = nr["SubjectGrade4"].ToString();
                    eq.SubjectGrade5 = nr["SubjectGrade5"].ToString();
                    eq.SubjectGrade6 = nr["SubjectGrade6"].ToString();
                    eq.SubjectGrade7 = nr["SubjectGrade7"].ToString();
                    eq.SubjectGrade8 = nr["SubjectGrade8"].ToString();
                    eq.SubjectGrade9 = nr["SubjectGrade9"].ToString();
                    eq.SubjectGrade10 = nr["SubjectGrade10"].ToString();
                    eq.Sitting = nr["Sitting"].ToString();
                    qRecordsList.Add(eq);
                }
                dtRecordList = Utility.ConvertTo<ApplicantEntryQualification>(qRecordsList);
            }
            catch (Exception exx)
            {
            }

            db.Dispose();
            return dtRecordList;
        }

        public static DataTable getEntryExams()
        {
            Utility db = new Utility();
            DataSet ds;
            DataTable dt = new DataTable();
            ds = db.SelectQuery("select id, name from entryexam");
            dt = ds.Tables[0];
            return dt;

        }

        public static DataTable getGrades(string ExamId)
        {
            Utility db = new Utility();
            DataSet ds;
            DataTable dt = new DataTable();
            ds = db.SelectQuery("select grade from entryexamgrade where entryexamid=" + ExamId);
            dt = ds.Tables[0];
            return dt;

        }

        public void DeleteExams(string ExamRegNo, string formNumber)
        {
            Utility db = new Utility();
            db.PerformQuery("delete applicantentryqualification where regno='" + formNumber
                + "' and examregno='" + ExamRegNo
                + "'; update applicants set EducationStatus=0 where regno='" + formNumber + "'");
        }

        public static DataTable getSubjects()
        {
            Utility db = new Utility();
            DataSet ds;
            DataTable dt = new DataTable();
            ds = db.SelectQuery("select [subject] from subjects");
            dt = ds.Tables[0];
            return dt;

        }

        //public static bool CompareOlevels()
        //{
        //    ArrayList arrSubjects = new ArrayList();
        //    string subjects = "", grades = "", subject = "", courseStudiedOrExamNumber = "";
        //    int count = 0;
        //    for (int i = 1; i <= 9; ++i)
        //    {
        //        ddlSubject = (DropDownList)FindControl("ddlSubject" + (i).ToString().Trim());
        //        ddlGrade = (DropDownList)FindControl("ddlGrade" + (i).ToString().Trim());
        //        if (ddlSubject.SelectedIndex != 0 && ddlGrade.SelectedIndex != 0)
        //        {
        //            subject = ddlSubject.SelectedValue.Trim();
        //            if (arrSubjects.Contains(subject))
        //            {
        //                WebUtility.Message(MessageTypeEnum.warning, "You selected a subject more than once.", Page.Master);
        //                //lblMessage.Text = "You seleted a subject more than once.";
        //                break;
        //            }
        //            arrSubjects.Add(subject);
        //            subjects += subject.Length.ToString().PadLeft(2, '0') + subject;
        //            grades += ddlGrade.SelectedValue.Trim().Length.ToString().PadLeft(2, '0') + ddlGrade.SelectedValue.Trim();
        //            count++;
        //        }
        //    }

        //}
    }
}
