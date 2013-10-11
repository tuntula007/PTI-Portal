
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for DeptCoursesBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{
    public class DeptCoursesBusiness : BaseBusiness 
    {
        public DeptCoursesBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static bool getSemesterRegistrationStatus(string matno,string semester, string session, ref int IsApproved)
        {
            string mykey = "Registration_getSemesterRegistrationStatus_" + matno + session + semester;
            DBAccess db = new DBAccess();
            bool retval = false;
            IsApproved = 0;
            try
            {
                db.AddParameter(new SqlParameter("@MatricNumber", matno));
                db.AddParameter(new SqlParameter("@Semester", semester));
                db.AddParameter(new SqlParameter("@SessionName", session));
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSemesterRegistrationStatus");
                retval = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    IsApproved = Convert.ToInt32(dr["isapproved"].ToString());
                    retval = true;
                }
                else
                    retval = false;
            }
            catch (Exception ex)
            {
                retval = false;
            }
            db.Dispose();
            return retval;

        }






        public static bool getRegistrationStatus(string matno, string session, ref int IsApproved)
        {
            string mykey = "Registration_getRegistrationStatus_" + matno + session;
            DBAccess db = new DBAccess();
            bool retval = false;
            IsApproved = 0;
            try
            {
                db.AddParameter(new SqlParameter("@MatricNumber", matno));
                //db.AddParameter(new SqlParameter("@Semester", semester));
                db.AddParameter(new SqlParameter("@SessionName", session));
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getRegistrationStatus");
                retval = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    IsApproved = Convert.ToInt32(dr["isapproved"].ToString());
                    retval = true;
                }
                else
                    retval = false;
            }
            catch (Exception ex)
            {
                retval = false;
            }
            db.Dispose();
            return retval;

        }
        public static List<DeptCourses> getSemesterCourses(int courseofstudyId, string level, string semester, int departmentId, string programme, string modeofstudy)
        {

             string mykey = "Registration_getSemesterCourses_" + courseofstudyId.ToString() + level + semester + departmentId.ToString() + programme + modeofstudy;
             DBAccess db = new DBAccess();
             db.AddParameter(new SqlParameter("@AcademicLevel", level));
             db.AddParameter(new SqlParameter("@Semester", semester));
             db.AddParameter(new SqlParameter("@DepartmentId", departmentId));
             db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
             db.AddParameter(new SqlParameter("@Programme", programme));
             db.AddParameter(new SqlParameter("@ModeofStudy", modeofstudy));

                SqlDataReader dr ;
                List<DeptCourses> result = new List<DeptCourses>();

                if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
                {
                    result = (List<DeptCourses>)BizObject.Cache[mykey];
                }
                else
                {
                    dr = (SqlDataReader)db.ExecuteReader("getSemesterCourses");
                   
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            DeptCourses deptCourse = new DeptCourses();
                            //
                            deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                            deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                            deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                            deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                            deptCourse.Semester = dr.GetString(dr.GetOrdinal("Semester"));
                            //
                            result.Add(deptCourse);
                        }
                        BaseBusiness.CacheData(mykey, result);
                    }
                    
                }
                db.Dispose();
            return result;
        }
        public static List<DeptCourses> getSessionCourses(int courseofstudyId, string level, int departmentId, string programme, string modeofstudy)
        {

            string mykey = "Registration_getSessionCourses_" + courseofstudyId.ToString() + level + departmentId.ToString() + programme + modeofstudy;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@DepartmentId", departmentId));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@Programme", programme));
            db.AddParameter(new SqlParameter("@ModeofStudy", modeofstudy));

            SqlDataReader dr;
            List<DeptCourses> result = new List<DeptCourses>();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (List<DeptCourses>)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSessionCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                        deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                        deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                        deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                        deptCourse.Semester  = dr.GetString(dr.GetOrdinal("Semester"));
                        //
                        result.Add(deptCourse);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static List<DeptCourses> getSemesterRegisteredCourses1(int courseofstudyId, string level, string semester, int departmentId, string programme, string modeofstudy)
        {
            string mykey = "Registration_getSemesterRegisteredCourses_" + courseofstudyId.ToString() + level + semester + departmentId.ToString() + programme + modeofstudy;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@DepartmentId", departmentId));
            
            db.AddParameter(new SqlParameter("@Programme", programme));
            db.AddParameter(new SqlParameter("@ModeofStudy", modeofstudy));

            SqlDataReader dr;
            List<DeptCourses> result = new List<DeptCourses>();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (List<DeptCourses>)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSemesterRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                        deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                        deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                        //
                        result.Add(deptCourse);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static int getSemesterCoursesTotalCredit(int courseofstudyId, string level, string semester, int departmentId, string programme, string modeofstudy)
        {

            string mykey = "Registration_getSemesterCoursesTotalCredit_" + courseofstudyId.ToString() + level + semester + departmentId.ToString() + programme + modeofstudy; 
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@DepartmentId", departmentId));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@Programme", programme));
            db.AddParameter(new SqlParameter("@ModeofStudy", modeofstudy));

            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSemesterCoursesTotalCredit");
            db.Dispose();string t ="0";
              //t = (string)totSemesterCredit;
              //if (string.IsNullOrEmpty(t))
              //{
              //    return Convert.ToInt32(t); 
              //}
              //else
              { return Convert.ToInt32(totSemesterCredit); }
            
        }
        public static int getSessionCoursesTotalCredit(int courseofstudyId, string level, int departmentId, string programme, string modeofstudy)
        {

            string mykey = "Registration_getSemesterCoursesTotalCredit_" + courseofstudyId.ToString() + level + departmentId.ToString() + programme + modeofstudy;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@DepartmentId", departmentId));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@Programme", programme));
            db.AddParameter(new SqlParameter("@ModeofStudy", modeofstudy));

            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesTotalCredit");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);

        }
        public static List<DeptCourses> getSemesterRegisteredCourses(string matricnumber, string semester, string session)
        {
            string mykey = "Registration_getSemesterRegisteredCourses_" + matricnumber + semester + session;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@matricNumber", matricnumber));
            db.AddParameter(new SqlParameter("@Session", session));

            SqlDataReader dr;
            List<DeptCourses> result = new List<DeptCourses>();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (List<DeptCourses>)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSemesterRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                        deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                        deptCourse.CreditLoad = double.Parse(dr["CourseUnit"].ToString());
                        deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                        //
                        result.Add(deptCourse);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static int getTotalSemesterRegisteredCourses(string matricnumber, string semester, string session)
        {
            string mykey = "Registration_getSemesterRegisteredCourses_" + matricnumber + semester + session;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@matricNumber", matricnumber));
            db.AddParameter(new SqlParameter("@Session", session));

            SqlDataReader dr;
            int result = new int();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (int)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSemesterRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        //
                        result = result + Convert.ToInt32(dr["CourseUnit"]);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static int getTotalSessionRegisteredCourses(string matricnumber, string session)
        {
            string mykey = "Registration_getSessionRegisteredCourses_" + matricnumber + session;
            DBAccess db = new DBAccess();
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@matricNumber", matricnumber));
            db.AddParameter(new SqlParameter("@Session", session));

            SqlDataReader dr;
            int result = new int();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (int)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSessionRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        //
                        result = result + Convert.ToInt32(dr["CourseUnit"]);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static List<DeptCourses> getSessionRegisteredCourses(string matricnumber, string session)
        {
            string mykey = "Registration_getSummerRegisteredCourses_" + matricnumber + session;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricnumber));
            db.AddParameter(new SqlParameter("@Session", session));

            SqlDataReader dr;
            List<DeptCourses> result = new List<DeptCourses>();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (List<DeptCourses>)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSessionRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                        deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                        deptCourse.CreditLoad = double.Parse(dr["CourseUnit"].ToString());
                        deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                        deptCourse.ApprovalStatus = dr.GetString(dr.GetOrdinal("ApprovalStatus"));
                        //deptCourse.Semester = dr.GetString(dr.GetOrdinal("Semester"));
                        //
                        result.Add(deptCourse);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        public static int getTotalSummerRegisteredCourses(string matricnumber, string session)
        {
            string mykey = "Registration_getSummerRegisteredCourses_" + matricnumber + session;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricnumber));
            db.AddParameter(new SqlParameter("@Session", session));

            SqlDataReader dr;
            int result = new int();

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (int)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSummerRegisteredCourses");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        DeptCourses deptCourse = new DeptCourses();
                        //
                        //
                        result = result + Convert.ToInt32(dr["CourseUnit"]);
                    }
                    BaseBusiness.CacheData(mykey, result);
                }

            }
            db.Dispose();
            return result;
        }
        //Check Summer Registration Status
        public static bool getRegistrationStatus(string matno, string session)
        {
            string mykey = "Registration_getRegistrationStatus_" + matno + session;
            DBAccess db = new DBAccess();
            bool retval = false;
            try
            {
                db.AddParameter(new SqlParameter("@MatricNumber", matno));
                db.AddParameter(new SqlParameter("@SessionName", session));
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSummerRegistrationStatus");
                retval = false;
                if (dr.HasRows)
                    retval = true;
                else
                    retval = false;
            }
            catch (Exception ex)
            {
                retval = false;
            }
            db.Dispose();
            return retval;

        }
        //Get Summer Student Details
        public static SummerStudent getSummerStudent(string matno, string session)
        {
            SummerStudent retStudent = new SummerStudent();
            string mykey = "Registration_getSummerRegistrationStatus_" + matno + session;
            DBAccess db = new DBAccess();
            try
            {
                db.AddParameter(new SqlParameter("@MatricNumber", matno));
                db.AddParameter(new SqlParameter("@SessionName", session));
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSummerRegistrationStatus");
                if (dr.HasRows)
                {
                    dr.Read();
                    retStudent.AcademicLevel = dr["AcademicLevel"].ToString();
                    retStudent.HasRegistered = Convert.ToInt32(dr["HasRegistered"]);
                    //retStudent.IsActive = Convert.ToInt32(dr["IsActive"]);
                    retStudent.MatricNo = dr["MatricNo"].ToString();
                    retStudent.RegNo = dr["RegNo"].ToString();
                    retStudent.Scholarship = Convert.ToInt32(dr["Scholarship"]);
                    retStudent.SessionName = dr["SessionName"].ToString();
                }
                else
                    retStudent = null;
                //retunMessage = "Your Matric/Reg Number does not exist for summer class";

            }
            catch (Exception ex)
            {
                retStudent = null;
                //retunMessage = "There was a technical problem checking your status";
            }
            db.Dispose();
            return retStudent;

        }

        public static int getSemesterCoursesMaxCredit(int courseofstudyId, string level, string semester, string modeofstudy, string programme)
        {
            int zeroed = 2;
            string mykey = "Registration_getSemesterCoursesMaxCredit_" + courseofstudyId.ToString() + level + semester;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));

            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSemesterCoursesMaxCredit");
        
            db.Dispose();

            return Convert.ToInt32(totSemesterCredit);
            if (totSemesterCredit == null) { return zeroed; }
        }
        public static int getSessionCoursesMaxCredit(int courseofstudyId, string level, string modeofstudy, string programme)
        {
            int zeroed = 0;
            string mykey = "Registration_getSessionCoursesMaxCredit_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));

            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMaxCredit");
        
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);
            if (totSemesterCredit == null) { return zeroed ; }

        }
        public static int getSessionCoursesMaxCore(int courseofstudyId, string level, string modeofstudy, string programme)
        {

            string mykey = "Registration_getSemesterCoursesMaxCore_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));

            object totSemesterCredit;

            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMaxCore");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);



        }
        public static int getSessionCoursesMaxElective(int courseofstudyId, string level, string modeofstudy, string programme)
        {

            string mykey = "Registration_getSessionCoursesMaxElective_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));

            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMaxElective");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);

        }

        public static int getSemesterCoursesMinCredit(int courseofstudyId, string level, string semester, string modeofstudy, string programme)
        {
            int zeroed=2;
            string mykey = "Registration_getSemesterCoursesMinCredit_" + courseofstudyId.ToString() + level + semester;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSemesterCoursesMinCredit");
            db.Dispose();
            if (totSemesterCredit == null) { return zeroed; }
            return Convert.ToInt32(totSemesterCredit);

        }
        public static int getSessionCoursesMinCredit(int courseofstudyId, string level, string modeofstudy, string programme)
        {

            string mykey = "Registration_getSessionCoursesMinCredit_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMinCredit");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);

        }
        public static int getSessionCoursesMinCore(int courseofstudyId, string level, string modeofstudy, string programme)
        {

            string mykey = "Registration_getSessionCoursesMinCore_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMinCore");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);

        }
        public static int getSessionCoursesMinElective(int courseofstudyId, string level, string modeofstudy, string programme)
        {

            string mykey = "Registration_getSessionCoursesMinElective_" + courseofstudyId.ToString() + level;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@AcademicLevel", level));
            //db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", courseofstudyId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            object totSemesterCredit;
            totSemesterCredit = (object)db.ExecuteScalar("getSessionCoursesMinElective");
            db.Dispose();
            return Convert.ToInt32(totSemesterCredit);

        }
        public static string getExamClearanceCode()
        {

            string mykey = "Registration_getClearanceCode_" + DateTime.Now.ToString();
            DBAccess db = new DBAccess(mykey);
            object examCode;
            examCode = (object)db.ExecuteScalar("getClearanceCode");
            db.Dispose();
            return Convert.ToString(examCode);

        } 
         public static List<DeptCourses> getSemesterRegisteredCarryOver(string matricNumber, string semester,string session)
         {
             string mykey = "Registration_getSemesterCarryOver_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@Session", session));
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSemesterRegisteredCarryOver");
            List<DeptCourses> result = new List<DeptCourses>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeptCourses deptCourse = new DeptCourses();
                    //
                    deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                    deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                    deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                    deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                    deptCourse.ApprovalStatus = dr.GetString(dr.GetOrdinal("ApprovalStatus"));
                    //
                    result.Add(deptCourse);
                }

            }
            db.Dispose();
            return result;
        }



        public static List<DeptCourses> getSemesterCarryOver(string matricNumber, string semester)
        {
            string mykey = "Registration_getSemesterCarryOver_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", semester));

            SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSemesterCarryOver");
            List<DeptCourses> result = new List<DeptCourses>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeptCourses deptCourse = new DeptCourses();
                    //
                    deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                    deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                    deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                    deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                    
                    //
                    result.Add(deptCourse);
                }

            }
            db.Dispose();
            return result;
        }
        public static List<DeptCourses> getSessionCarryOver(string matricNumber)
        {
            string mykey = "Registration_getSemesterCarryOver_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            //db.AddParameter(new SqlParameter("@Semester", semester));

            SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSessionCarryOver");
            List<DeptCourses> result = new List<DeptCourses>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeptCourses deptCourse = new DeptCourses();
                    //
                    deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                    deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                    deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                    deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                    deptCourse.ApprovalStatus = dr.GetString(dr.GetOrdinal("ApprovalStatus"));

                    //
                    result.Add(deptCourse);
                }

            }
            db.Dispose();
            return result;
        }

        //SummerCourses
        public static List<DeptCourses> getSemesterCarryOver(string MatricNumber)
        {
            string mykey = "Registration_getSummerCourses_" + MatricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", MatricNumber));
            //db.AddParameter(new SqlParameter("@Semester", SessionName));

            SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSummerCourses");
            List<DeptCourses> result = new List<DeptCourses>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeptCourses deptCourse = new DeptCourses();
                    //
                    deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                    deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                    deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                    deptCourse.CourseType = dr.GetString(dr.GetOrdinal("CourseType"));
                    deptCourse.Semester = dr.GetString(dr.GetOrdinal("Semester"));
                    //
                    result.Add(deptCourse);
                }

            }
            db.Dispose();
            return result;
        }


        public static int getSemesterRegisteredCarryOverTotalCredit(string matricNumber, string semester, string session, string AcademicLevel, int CourseOfStudyId, int DepartmentId, string ModeOfStudy, string Programme)
        { 

            string mykey = "Registration_getSemesterRegisteredCarryOverTotalCredit_" + matricNumber + semester + session;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", semester));
            db.AddParameter(new SqlParameter("@Session", session));

            db.AddParameter(new SqlParameter("@AcademicLevel", AcademicLevel));
            db.AddParameter(new SqlParameter("@CourseOfStudyId", CourseOfStudyId));
            db.AddParameter(new SqlParameter("@DepartmentId", DepartmentId));
            db.AddParameter(new SqlParameter("@ModeOfStudy", ModeOfStudy));
            db.AddParameter(new SqlParameter("@Programme", Programme));
            object totCarryOverCredit;
            totCarryOverCredit = (object)db.ExecuteScalar("getSemesterRegisteredCarryOverTotalCredit");
            db.Dispose();
            if (totCarryOverCredit == null)
            { return 0; }
            return Convert.ToInt32(totCarryOverCredit);

        }
        public static int getSemesterCarryOverTotalCredit(string matricNumber, string semester)
        {

            string mykey = "Registration_getSemesterCarryOverTotalCredit_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", semester));

            object totCarryOverCredit;
            totCarryOverCredit = (object)db.ExecuteScalar("getSemesterCarryOverTotalCredit");
            db.Dispose();
            return Convert.ToInt32(totCarryOverCredit);

        }
        public static int getSessionCarryOverTotalCredit(string matricNumber)
        {

            string mykey = "Registration_getSessionCarryOverTotalCredit_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            //db.AddParameter(new SqlParameter("@Semester", semester));

            object totCarryOverCredit;
            totCarryOverCredit = (object)db.ExecuteScalar("getSessionCarryOverTotalCredit");
            db.Dispose();
            return Convert.ToInt32(totCarryOverCredit);

        }
        public static int getSummerTotalCredit(string matricNumber)
        {

            string mykey = "Registration_getSummerTotalCredit_" + matricNumber;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));

            object totSummerCredit;
            totSummerCredit = (object)db.ExecuteScalar("getSummerTotalCredit");
            db.Dispose();
            return Convert.ToInt32(totSummerCredit);

        }
        public static List<DeptCourses> getSemesterCarryOver2(string matricNumber, string Semester)
        {

            string mykey = "Registration_getSemesterCarryOver_" + matricNumber + Semester;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", Semester));


            SqlDataReader dr = (SqlDataReader)db.ExecuteReader("getSemesterCarryOver");
            List<DeptCourses> result = new List<DeptCourses>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DeptCourses deptCourse = new DeptCourses();
                    //
                    deptCourse.CourseCode = dr.GetString(dr.GetOrdinal("CourseCode"));
                    deptCourse.CourseTitle = dr.GetString(dr.GetOrdinal("CourseTitle"));
                    deptCourse.CreditLoad = double.Parse(dr["CreditLoad"].ToString());
                    //
                    result.Add(deptCourse);
                }

            }
            db.Dispose();
            return result;
        }
        public static double getSemesterCarryOverTotalCredit2(string matricNumber, string Semester)
        {

            string mykey = "Registration_getSemesterCarryOverTotalCredit_" + matricNumber + Semester;
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@matricNumber", matricNumber));
            db.AddParameter(new SqlParameter("@Semester", Semester));
            object totCarryOverCredit;
            totCarryOverCredit = (object)db.ExecuteScalar("getSemesterCarryOverTotalCredit");
            db.Dispose();
            return Convert.ToDouble(totCarryOverCredit);

        }
        public static bool Register(string matricno, int courseofstudyId, string level, string semester, int departmentId, string programme, string modeofstudy,string center)
        {
            DBAccess db = new DBAccess();
            
            db.Parameters.Add(new SqlParameter("@MatricNumber",matricno));
            db.Parameters.Add(new SqlParameter("@Programme",programme));
            db.Parameters.Add(new SqlParameter("@CourseOfStudyID",courseofstudyId));
            db.Parameters.Add(new SqlParameter("@AcademicLevel",level));
            db.Parameters.Add(new SqlParameter("@Semester",semester));
            db.Parameters.Add(new SqlParameter("@ModeOfStudy",modeofstudy));
            db.Parameters.Add(new SqlParameter("@DepartmentId",departmentId));
            db.Parameters.Add(new SqlParameter("@Center", center));
            
            int i = db.ExecuteNonQuery("CourseRegistration_InsertNew");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;

        }
        public static bool Register(string matricno, string coursecode, string coursetype, string courseunit, string session,string semester, string examCode)
        {
            DBAccess db = new DBAccess();

            db.Parameters.Add(new SqlParameter("@MatricNumber", matricno));
            db.Parameters.Add(new SqlParameter("@CourseCode", coursecode));
            db.Parameters.Add(new SqlParameter("@CourseUnit", courseunit));
            db.Parameters.Add(new SqlParameter("@Session", session));
            db.Parameters.Add(new SqlParameter("@CourseType", coursetype));
            db.Parameters.Add(new SqlParameter("@ExamCode", examCode));
            db.Parameters.Add(new SqlParameter("@Semester", semester));
            int i = db.ExecuteNonQuery("CourseRegistration_AddNew");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;

        }
        public static bool Register(string matricno, string coursecode, string coursetype, string courseunit, string session, string examCode)
        {
            DBAccess db = new DBAccess();

            db.Parameters.Add(new SqlParameter("@MatricNumber", matricno));
            db.Parameters.Add(new SqlParameter("@CourseCode", coursecode));
            db.Parameters.Add(new SqlParameter("@CourseUnit", courseunit));
            db.Parameters.Add(new SqlParameter("@Session", session));
            db.Parameters.Add(new SqlParameter("@CourseType", coursetype));
            db.Parameters.Add(new SqlParameter("@ExamCode", examCode));
            int i = db.ExecuteNonQuery("CourseRegistration_AddNew");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;

        }
        public static bool DeRegister(string matricno, string session)
        {
            DBAccess db = new DBAccess();

            db.Parameters.Add(new SqlParameter("@MatricNumber", matricno));
            db.Parameters.Add(new SqlParameter("@Session", session));
            int i = db.ExecuteNonQuery("CourseRegistration_Remove");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;

        }
        public static bool DeptCoursesAvailable(int CourseOfStudyID, string Semester, string AcademicLevel)
        {

            string mykey = "DeptCoursesAvailable_" + CourseOfStudyID.ToString() + Semester + AcademicLevel;
            DBAccess db = new DBAccess();
            bool retval = false;
            try
            {
                db.Parameters.Add(new SqlParameter("@CourseofStudyID", CourseOfStudyID));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Semester", Semester));

                DataSet ds = db.ExecuteDataSet("DeptCoursesAvailable");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    retval = true;
                }
                else
                {
                    retval = false;
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {

                string ffg = ec.Message;
                retval = false;
            }
            db.Dispose();
            return retval;
        }
        public static bool Register(string matricno, string semester, string coursecode, string session)
        {
            DBAccess db = new DBAccess();

            db.Parameters.Add(new SqlParameter("@MatricNumber", matricno));
            db.Parameters.Add(new SqlParameter("@Semester", semester));
            db.Parameters.Add(new SqlParameter("@CourseCode", coursecode));
            db.Parameters.Add(new SqlParameter("@Session", session));
            //db.Parameters.Add(new SqlParameter("@VerifiedPIN", verifiedPIN));
            int i = db.ExecuteNonQuery("CourseRegistration_AddSummer");
            db.Dispose();
            if (i > 0)
                return true;
            else
                return false;

        }
        public static SummerPrices getSummerPrice(string faculty, string modeofstudy, string programme)
        {
            SummerPrices retSummerPrices = new SummerPrices();
            string mykey = "Registration_getSummerPrices_" + faculty + "_" + modeofstudy + "_" + programme;
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@Faculty", faculty));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            SqlDataReader dr;
            SummerPrices result = new SummerPrices();
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                retSummerPrices = (SummerPrices)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSummerPrices");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        //
                        result.UpperQty = dr.GetInt32(dr.GetOrdinal("UpperQty"));
                        result.Uploader = dr.GetString(dr.GetOrdinal("Uploader"));
                        result.Programme = dr.GetString(dr.GetOrdinal("Programme"));
                        result.Prices = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("Prices")));
                        result.ModeOfStudy = dr.GetString(dr.GetOrdinal("ModeOfStudy"));
                        result.LowerQty = dr.GetInt32(dr.GetOrdinal("LowerQty"));
                        result.Faculty = dr.GetString(dr.GetOrdinal("Faculty"));
                        //result.CreateDate = dr.GetString(dr.GetOrdinal("CreateDate"));
                        //
                        retSummerPrices = result;
                    }
                    BaseBusiness.CacheData(mykey, retSummerPrices);
                }

            }
            db.Dispose();
            return retSummerPrices;

        }
        public static bool Register(GridView grdCarryOver, Students st, string Semester, string CurrentSes, string summerPin)
        {
            string coursecode = "", courseunit = "";
            bool isRegistered = false;
            if (grdCarryOver.Rows.Count > 0)
            {
                foreach (GridViewRow r in grdCarryOver.Rows)
                {
                    CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
                    //creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                    if (chkrow.Checked)
                    {
                        coursecode = r.Cells[0].Text;
                        courseunit = r.Cells[2].Text;
                        Semester = (r.Cells[4].Text == "1") ? "FIRST" : "SECOND";
                        //register student
                        isRegistered = Register(st.MatricNumber, Semester, coursecode, CurrentSes);
                    }
                }
            }
            if (isRegistered==true)
            {
                new SignOnBusiness().PerformQuery("UPDATE SummerFeesPin SET UsedBy='" + st.MatricNumber + "',UsedDate=GETDATE(),PinStatus =1,SessionName ='" + CurrentSes + "' WHERE '" + summerPin + "'<>'waived' AND PinNumber ='" + summerPin + "'");
                new SignOnBusiness().PerformQuery("UPDATE SummerSchool SET HasRegistered =1 WHERE ([MatricNo] ='" + st.MatricNumber + "'or [RegNo] ='" + st.MatricNumber + "') AND [SessionName]='" + CurrentSes + "'");
            }
            return isRegistered;
        }
        public static SummerPrices getSummerPrice(int countCourse, string faculty, string modeofstudy, string programme)
        {
            SummerPrices retSummerPrices = getSummerPrice(faculty, modeofstudy, programme);
            string mykey = "Registration_getSummerPrices_" + faculty + "_" + modeofstudy + "_" + programme + "_" + countCourse.ToString();
            DBAccess db = new DBAccess(mykey);
            db.AddParameter(new SqlParameter("@Faculty", faculty));
            db.AddParameter(new SqlParameter("@countCourse", countCourse));
            db.AddParameter(new SqlParameter("@ModeOfStudy", modeofstudy));
            db.AddParameter(new SqlParameter("@Programme", programme));
            SqlDataReader dr;
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                retSummerPrices = (SummerPrices)BizObject.Cache[mykey];
            }
            else
            {
                dr = (SqlDataReader)db.ExecuteReader("getSummerPricesByCount");

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        SummerPrices result = new SummerPrices();
                        //
                        result.UpperQty = dr.GetInt32(dr.GetOrdinal("UpperQty"));
                        result.Uploader = dr.GetString(dr.GetOrdinal("Uploader"));
                        result.Programme = dr.GetString(dr.GetOrdinal("Programme"));
                        result.Prices = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("Prices")));
                        result.ModeOfStudy = dr.GetString(dr.GetOrdinal("ModeOfStudy"));
                        result.LowerQty = dr.GetInt32(dr.GetOrdinal("LowerQty"));
                        result.Faculty = dr.GetString(dr.GetOrdinal("Faculty"));
                        //result.CreateDate = dr.GetString(dr.GetOrdinal("CreateDate"));
                        //
                        retSummerPrices = result;
                    }
                    BaseBusiness.CacheData(mykey, retSummerPrices);
                }

            }
            db.Dispose();
            return retSummerPrices;

        }

        public DataTable GetStudentsInfoByMatNoCrystal(string MatNo)
        {
            Students rt = new Students();
            DataTable NowTable = null;
            string mykey = "select s.Matricnumber,s.regno, s.Surname , s.Othernames,cs.honours +'('+substring(cs.courseofstudyname,charindex('(',cs.courseofstudyname)+1,len(cs.courseofstudyname)-charindex(cs.courseofstudyname,'(')) CourseofStudy,cs.departmentname department,";
            mykey += "cs.facultyname faculty,s.presentsession,p.Picture StudentPassport, s.academiclevel  from students s, courseofstudy cs, picturefile p ";
            mykey += "where s.courseofstudyid=cs.courseofstudyid and s.regno=p.pickey and s.matricnumber='" + MatNo + "'";
            Utility db = new Utility();

            try
            {
                DataSet ds = new DataSet("Stud");
                ds = db.SelectQuery(mykey);
                NowTable = ds.Tables[0];
            }
            catch (Exception eee)
            {
                //log
                string sss = eee.Message;
            }

            return NowTable;
        }

        public DataTable GetCourseCodeRegisteredByMatNoCrystal(string MatNo)
        {
            Students rt = new Students();
            DataTable NowTable = null;
            string mykey = "select distinct cr.coursecode,cs.coursetitle +'['+cr.coursecode+']' coursetitle from ";
            mykey += "courseregistration cr, courses cs where cr.matricnumber='" + MatNo + "' and cs.coursecode=cr.coursecode";
            Utility db = new Utility();

            try
            {
                DataSet ds = new DataSet("Stud");
                ds = db.SelectQuery(mykey);
                NowTable = ds.Tables[0];
            }
            catch (Exception eee)
            {
                //log
                string sss = eee.Message;
            }

            return NowTable;
        }

    }
}