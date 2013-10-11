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
using System.Data.SqlClient;

/// <summary>
/// Summary description for EntryQualificationBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{
    public class EntryQualificationBusiness
    {
        public EntryQualificationBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool SaveRecord(EntryQualification eq, string modeofstudy)
        {
            string mykey = "EntryQualification_getEntryQualByMatricNumber_" + eq.MatricNumber + eq.Sitting;
            DBAccess db = new DBAccess(mykey);

            db.Parameters.Add(new SqlParameter("@MatricNumber",  eq.MatricNumber));
            db.Parameters.Add(new SqlParameter("@RegNo",  eq.RegNo));
            db.Parameters.Add(new SqlParameter("@ExamName",  eq.ExamName));
            db.Parameters.Add(new SqlParameter("@ExamRegNo",  eq.ExamRegNo));
            db.Parameters.Add(new SqlParameter("@ExamDate",  eq.ExamDate));
            db.Parameters.Add(new SqlParameter("@Sitting",  int.Parse(eq.Sitting)));
            db.Parameters.Add(new SqlParameter("@SubjectName1",  eq.SubjectName1));
            db.Parameters.Add(new SqlParameter("@SubjectGrade1",  eq.SubjectGrade1));
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
            db.Parameters.Add(new SqlParameter("@ModeOfStudy", modeofstudy));

            int i = db.ExecuteNonQuery("EntryQualification_Insert");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;
        }

        public static EntryQualification getRecord(string matno, string sitting)
        {
            string mykey = "EntryQualification_getEntryQualByMatricNumber_" + matno + sitting;
            DBAccess db = new DBAccess(mykey);
            DataTable NowTable = null;
            DataRow nr = null;
            EntryQualification eq = new EntryQualification();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", matno));
                db.Parameters.Add(new SqlParameter("@Sitting", sitting));
                DataSet ds = new DataSet("Enq");
                ds = db.ExecuteDataSet("getEntryQualByMatricNumber");
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
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
                eq.SubjectGrade1 = nr["SubjectGrade1"].ToString();
                eq.SubjectGrade2 = nr["SubjectGrade2"].ToString();
                eq.SubjectGrade3 = nr["SubjectGrade3"].ToString();
                eq.SubjectGrade4 = nr["SubjectGrade4"].ToString();
                eq.SubjectGrade5 = nr["SubjectGrade5"].ToString();
                eq.SubjectGrade6 = nr["SubjectGrade6"].ToString();
                eq.SubjectGrade7 = nr["SubjectGrade7"].ToString();
                eq.Sitting = nr["Sitting"].ToString();

                ds.Dispose();

            }
            catch (Exception exx)
            {

            }
            
            db.Dispose();
            return eq;

        }
    }
}
