

#region using statements

using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Messaging;
using System.Data.SqlClient;


#endregion

#region class Faculty
[Serializable]
public class FacultyBusiness
{
    private static string str = System.Configuration.ConfigurationManager.AppSettings["conn"];

    #region Get All Name and Id
    #endregion
    public static DataSet GetFacultys()
    {
        DataSet ds = new DataSet();
        try
        {
            string qry = "SELECT [FacultyName],[FacultyID] FROM [Faculty]";
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
    public static DataSet GetDept(string factid)
    {
        DataSet ds = new DataSet();
        try
        {
            string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
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
    //
    public static DataSet GetDuration()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            string qry = "SELECT [Duration] FROM [CourseDuration]";
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
    public static DataSet GetProg()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            string qry = "SELECT [Programme] FROM [SchProgramme]";
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
    
    public static DataSet GetHonor()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            //string qry = "SELECT [Duration] FROM [CourseDuration] order by [Duration] asc";
            string qry = "SELECT [Honour] FROM [CourseHonour]";
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
    public static DataSet GetStudyMode()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            //string qry = "SELECT [Duration] FROM [CourseDuration] order by [Duration] asc";
            string qry = "SELECT [ModeOfStudy] FROM [ModeOfStudy]";
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
    public static DataSet GetLevel()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            string qry = "SELECT [AcademicLevel] FROM [Levels] order by [AcademicLevel] asc";
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
    public static DataSet GetSemester()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            string qry = "SELECT [Semester] FROM [Semesters] order by [Semester] asc";
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
    public static DataSet GetCourseType()
    {
        DataSet ds = new DataSet();
        try
        {
            //string qry = "SELECT [DepartmentName] FROM [Departments] where [FacultyID]='" + factid + "'";
            string qry = "SELECT [CourseType] FROM [CourseTypes] order by [CourseType] asc";
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
}
#endregion




