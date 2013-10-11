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
/// <summary>
/// Summary description for HelpDeskEnquiry
/// </summary>
public class HelpDeskEnquiry
{
	public HelpDeskEnquiry()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string getPinDescription(string encryptPin)
    {
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter("@Pin", encryptPin));
        SqlDataReader dr;
        try
        {
            dr = (SqlDataReader)db.ExecuteReader("getPinDescription");
            if (dr.HasRows)
            {
                dr.Read();
                string desc = "";
                desc = dr.GetString(dr.GetOrdinal("PinDescription"));
                dr.Dispose();
                db.Dispose();
                return desc;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string ReleasePinDescription(string encryptPin)
    {
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter("@Pin", encryptPin));
        //SqlDataReader dr;
        try
        {
            int ii = db.ExecuteNonQuery("ReleasePin");
            if (ii>0)
            {
                string desc = "PIN Successfully released!";
                
                db.Dispose();
                return desc;
            }
            else
            {
                return "Release NOT Successful";
            }
        }
        catch (Exception ex)
        {
            return "Release Not Successful";
        }
    }

  
    public static List<Students> getStudentEnquiry(string searchStr)
    {
        List<Students> result = new List<Students>();
        //
        DBAccess db = new DBAccess();
        db.AddParameter(new SqlParameter("@searchStr", searchStr));
        SqlDataReader dr;
        try
        {
            dr = (SqlDataReader)db.ExecuteReader("getStudentEnquiry");
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    Students student = new Students();
                    student.AcademicLevel = dr.GetString(dr.GetOrdinal("AcademicLevel"));

                    student.isEvening = dr.IsDBNull(dr.GetOrdinal("isEvening")) ? "" : dr.GetString(dr.GetOrdinal("isEvening"));
                    student.IsIndigene = dr.IsDBNull(dr.GetOrdinal("isIndigene")) ? "" : dr.GetString(dr.GetOrdinal("isIndigene"));
                    student.MatricNumber = dr.GetString(dr.GetOrdinal("MatricNumber"));
                    student.RegNo = dr.GetString(dr.GetOrdinal("RegNo"));
                    student.Sex = dr.GetString(dr.GetOrdinal("Sex"));
                    student.Programme = dr.GetString(dr.GetOrdinal("Programme"));
                    student.Surname = dr.GetString(dr.GetOrdinal("Surname"));
                    student.OtherNames = dr.GetString(dr.GetOrdinal("othernames"));
                    student.StudentId = dr.GetInt32(dr.GetOrdinal("studentid"));
                    student.AcademicLevel = dr.GetString(dr.GetOrdinal("AcademicLevel"));
                    result.Add(student);
                }
            }
        }
        catch (Exception ex)
        { }
        
        return result;
    }
    public static void updateStudentStatus(Students student)
    {
        DBAccess db = new DBAccess();
        try
        {
            db.AddParameter(new SqlParameter("@studentid", student.StudentId));
            db.AddParameter(new SqlParameter("@IsEvening", student.isEvening));
            db.AddParameter(new SqlParameter("@IsIndigene", student.IsIndigene));
            db.AddParameter(new SqlParameter("@AcademicLevel", student.AcademicLevel));
            db.ExecuteNonQuery("updateStudentEveningStatus");
        }
        catch (Exception ex)
        { }

        return;
    }
}
