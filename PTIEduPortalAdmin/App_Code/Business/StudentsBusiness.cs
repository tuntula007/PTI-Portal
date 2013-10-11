

#region using statements

using System;
using System.Collections;
using CybSoft.EduPortal.Data;

using System.Data;
using System.Data.SqlClient;
using CybSoft.EduPortal.Business;
#endregion


namespace CybSoft.EduPortal.Business
{

    public class StudentsBusiness
    {


        /// <summary>
        /// This method gets Students record by Matric Number
        /// </summary>
        /// <param name="RegNoo"> Students Matric No</param>
        /// <returns></returns>
        public Students GetStudentsByMatNo(string MatNo)
        {
            Students rt = new Students();
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Students_getStudentByMatricNumber_" + MatNo;
            DBAccess db = new DBAccess(mykey);
            
            try
            {
                
                db.Parameters.Add(new SqlParameter("@MatNo", MatNo));
                 
                //SchoolWebService.Service1 NowService = new SchoolWebService.Service1();
                DataSet ds = new DataSet("Stud");
                ds = db.ExecuteDataSet("getStudentByMatricNumber");
                //string Qstring = "SELECT * from Students WHERE MatriculationNumber = '" + MatNo + "'";
               // ds = NowService.RetriveDat(Qstring);
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                rt.MatricNumber = MatNo;
                rt.RegNo = nr["RegNo"].ToString();
                rt.AdmittedLevel = nr["AdmittedLevel"].ToString();
                rt.AcademicLevel = nr["AcademicLevel"].ToString();
                rt.AdmissionStatus = nr["AdmissionStatus"].ToString();
                rt.Country = nr["Country"].ToString();
                rt.DateOfBirth = nr["DateOfBirth"].ToString();
                rt.DepartmentID = (int)nr["DepartmentID"];
                rt.AdmittedSession = nr["AdmittedSession"].ToString();
                rt.Email = nr["Email"].ToString();
                rt.FacultyID = (int)nr["FacultyID"];
                rt.Surname = nr["Surname"].ToString();
                rt.OtherNames = nr["OtherNames"].ToString();
                rt.HomeAddress = nr["HomeAddress"].ToString();
                rt.MaidenName = nr["MaidenName"].ToString();
                rt.LocalGovernmentArea = nr["LocalGovernmentArea"].ToString();
                rt.MaritalStatus = nr["MaritalStatus"].ToString();
                rt.MatricNumber = nr["MatricNumber"].ToString();
                rt.isNewStudent = nr["isNewStudent"].ToString();
                if (rt.isNewStudent.Equals("1") == true)
                {
                    if (string.IsNullOrEmpty(rt.MatricNumber) == true)
                    {
                        rt.MatricNumber = rt.RegNo;
                    }
                }

                rt.Nationality = nr["Nationality"].ToString();
                rt.SponsorAddress = nr["SponsorAddress"].ToString();
                rt.SponsorName = nr["SponsorName"].ToString();
                rt.SponsorPhone = nr["SponsorPhone"].ToString();
                rt.PhoneNumber = nr["PhoneNumber"].ToString();
                rt.PresentSession = nr["PresentSession"].ToString();
                rt.PlaceOfBirth = nr["PlaceOfBirth"].ToString();
                rt.RegNo = nr["RegNo"].ToString();
                rt.RoomNo = nr["RoomNo"].ToString();
                rt.ResidentialAddress = nr["ResidentialAddress"].ToString();
                rt.Sex = nr["Sex"].ToString();
                rt.State = nr["State"].ToString();
                rt.Surname = nr["Surname"].ToString();
                rt.Programme = nr["Programme"].ToString();
                rt.ModeOfStudy = nr["ModeOfStudy"].ToString();
                rt.Department = nr["DepartmentName"].ToString();
                rt.Faculty = nr["FacultyName"].ToString();
                rt.CourseOfStudy = nr["CourseOfStudyName"].ToString();
                rt.CourseOfStudyID = (int)nr["CourseOfStudyID"];
                rt.Title = nr["Title"].ToString();
                rt.Repeating = nr["Repeating"].ToString();
                rt.IsIndigene = nr["IsIndigene"].ToString();
               
                rt.LocalPassportFile = nr["PassportFile"].ToString();
                rt.isEvening = nr["isEvening"].ToString();
                ds.Dispose();
                
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return rt;
        }

        public static DataSet GetLGA(string state)
        {
            string mykey = "GetLGA_LGA_FetchAll_" + state;
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@state", state));
                return db.ExecuteDataSet("LGA_FetchAll");
            }
            catch (Exception ex)
            {
                string err=ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }
        public static bool isMatricNumberExists(string matricnumber)
        {
            string mykey = "isMatricNumberExists_checkMatricNumberExists_" + matricnumber;
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            try
            {
                db.Parameters.Add(new SqlParameter("@matricnumber", matricnumber));
                dr = (SqlDataReader)db.ExecuteReader("checkMatricNumberExists");
                if (dr.HasRows)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return false;

        }

        public static bool CheckPayStudio(int facultyid, int departmentid, int courseofstudyid)
        {
            string mykey = "";
            mykey = "PayStudio_checkStudioFeesbyFaculty_" + facultyid.ToString();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@facultyid", facultyid));
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader("checkStudioFeesbyFaculty");
                if (dr.HasRows)
                {
                    return true;
                }
                mykey = "PayStudio_checkStudioFeesbyDepartment_" + departmentid.ToString();
                db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@departmentid", departmentid));
                dr = (SqlDataReader)db.ExecuteReader("checkStudioFeesbyDepartment");
                if (dr.HasRows)
                {
                    return true;
                }

                mykey = "PayStudio_checkStudioFeesbyCourseofStudy_" + courseofstudyid.ToString();
                db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@courseofstudyid", courseofstudyid));
                dr = (SqlDataReader)db.ExecuteReader("checkStudioFeesbyCourseOfStudy");
                if (dr.HasRows)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return false;


        }
        public static bool UpdateRecord(Students st)
        {
            string mykey = "Students_getStudentByMatricNumber_" + st.MatricNumber;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", st.MatricNumber));
                db.Parameters.Add(new SqlParameter("@Surname", st.Surname));
                db.Parameters.Add(new SqlParameter("@Othernames", st.OtherNames));
                db.Parameters.Add(new SqlParameter("@Title", st.Title));
                db.Parameters.Add(new SqlParameter("@Maidenname", st.MaidenName));
                db.Parameters.Add(new SqlParameter("@DateOfBirth", st.DateOfBirth));
                db.Parameters.Add(new SqlParameter("@MaritalStatus", st.MaritalStatus));
                db.Parameters.Add(new SqlParameter("@Sex", st.Sex));
                db.Parameters.Add(new SqlParameter("@Nationality", st.Nationality));
                db.Parameters.Add(new SqlParameter("@State", st.State));
                db.Parameters.Add(new SqlParameter("@LGA", st.LocalGovernmentArea));
                db.Parameters.Add(new SqlParameter("@HomeAddress", st.HomeAddress));
                db.Parameters.Add(new SqlParameter("@Email", st.Email));
                db.Parameters.Add(new SqlParameter("@Phone", st.PhoneNumber));
                db.Parameters.Add(new SqlParameter("@ResidentialAddress", st.ResidentialAddress));
                db.Parameters.Add(new SqlParameter("@RoomNo", st.RoomNo));
                db.Parameters.Add(new SqlParameter("@AdmittedSession", st.AdmittedSession));
                db.Parameters.Add(new SqlParameter("@SponsorName", st.SponsorName));
                db.Parameters.Add(new SqlParameter("@SponsorAddress", st.SponsorAddress));
                db.Parameters.Add(new SqlParameter("@SponsorPhone", st.SponsorPhone));
                db.Parameters.Add(new SqlParameter("@Repeating", st.Repeating));

                int i = db.ExecuteNonQuery("Students_Update");
                db.Dispose();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
                return false;
        }

        public static bool UpdatePassportRecord(string href,string matno, string localfile)
        {
            string mykey = "Students_getStudentByMatricNumber_" + matno;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", matno));
                db.Parameters.Add(new SqlParameter("@PassportFile", href));
                db.Parameters.Add(new SqlParameter("@LocalPassportFile", localfile));
                int i = db.ExecuteNonQuery("Students_Update_Passport");
                db.Dispose();
                if (i > 0)
                {
                    //StudentsBusiness sb = new StudentsBusiness();
                    //Students st = new Students();
                    //string Semester = "First";
                    //st = sb.GetStudentsByMatNo(matno);
                    //bool isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, st.CourseOfStudyID, st.AcademicLevel, Semester, st.DepartmentID, st.Programme, st.ModeOfStudy);                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }

        public static bool IsStudentExist(string MatricNumber)
        {
            string mykey = "IsStudentExist_" + MatricNumber;
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", MatricNumber));
                ds = db.ExecuteDataSet("IsStudentExist");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                ds.Dispose();
                db.Dispose();
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }
        public static bool IsCourseCodeExist(string CourseCode, int CourseOfStudyID, string AcademicLevel, string Semester)
        {
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@CourseCode", CourseCode));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", CourseOfStudyID));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Semester", Semester));
                ds = db.ExecuteDataSet("IsCourseCodeExist");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                ds.Dispose();
                db.Dispose();
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }
        public static bool AlreadyUploadCourse(string MatricNo, string CourseCode, string AcademicLevel, string Semester)
        {
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@CourseCode", CourseCode));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Semester", Semester));
                ds = db.ExecuteDataSet("AlreadyUploadCourse");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                ds.Dispose();
                db.Dispose();
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }
        public static bool InsertStudentRecord(string RegNo, string MatricNumber, string Surname, string Othernames, int CourseOfStudyID, string ModeOfStudy, string Programme, string AcademicLevel, string PresentSession, int CanRegister, string UploadIntoAdmissionList, int IsIndigene, int IsEvening, int IjmbFeeOption, string Duration)
        {
            string mykey = "InsertStudentRec_" + RegNo;
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@MatricNumber", MatricNumber));
                db.Parameters.Add(new SqlParameter("@Surname", Surname));
                db.Parameters.Add(new SqlParameter("@Othernames", Othernames));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", CourseOfStudyID));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@PresentSession", PresentSession));
                db.Parameters.Add(new SqlParameter("@CanRegister", CanRegister));
                db.Parameters.Add(new SqlParameter("@UploadIntoAdmissionList", UploadIntoAdmissionList));
                db.Parameters.Add(new SqlParameter("@IsIndigene", IsIndigene));
                db.Parameters.Add(new SqlParameter("@IsEvening", IsEvening));
                db.Parameters.Add(new SqlParameter("@IjmbFeeOption", IjmbFeeOption));
                db.Parameters.Add(new SqlParameter("@Duration", Duration));
                int i = db.ExecuteNonQuery("InsertUploadedStudents");
                db.Dispose();
                if (i > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }
        public static bool InsertDeptCourses(string CourseCode, string CourseTitle, int CourseUnit, string AcademicLevel, string ModeOfStudy, string Programme, string Semester, int CourseOfStudyID, string OperationType)
        {
            string mykey = "InsertStudentRec_" + CourseCode;
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@CourseCode", CourseCode));
                db.Parameters.Add(new SqlParameter("@CourseTitle", CourseTitle));
                db.Parameters.Add(new SqlParameter("@CourseUnit", CourseUnit));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@Semester", Semester));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", CourseOfStudyID));
                db.Parameters.Add(new SqlParameter("@OperationType", OperationType));
                int i = db.ExecuteNonQuery("InsertDeptCourses");
                db.Dispose();
                if (i > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                db.Dispose();
            }
            return false;
        }
        public static DataSet StudentsSearcher(string SearchParam)
        {

            string mykey = "KogiPoly_StudSearcher_" + SearchParam;
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(SearchParam))
            {
                SearchParam = "%%";
                //return ds;
            }
            try
            {
                db.Parameters.Add(new SqlParameter("@SearchParam", SearchParam));
                return db.ExecuteDataSet("StudentSearcher");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }
        public static DataSet GetDeptCourses(int CourseOfStudyID, string AcademicLevel)
        {
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", CourseOfStudyID));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                ds= db.ExecuteDataSet("GetDeptCourses");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }


        public static DataSet GetStudentsByParamm(string Programme, string ModeOfStudy, string CourseOfStudyID, string AcademicLevel, string Semester)
        {
            Students rt = new Students();
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet("dfs2www");
            if (string.IsNullOrEmpty(Programme))
            {
                Programme = "";
            }
            if (string.IsNullOrEmpty(ModeOfStudy))
            {
                ModeOfStudy = "";
            }
            int Cid = 0;
            bool bdn = int.TryParse(CourseOfStudyID, out Cid);
            if (string.IsNullOrEmpty(AcademicLevel))
            {
                AcademicLevel = "";
            }
            if (string.IsNullOrEmpty(Semester))
            {
                Semester = "";
            }
            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", Cid));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Semester", Semester));
                ds = db.ExecuteDataSet("GetStudentssByParam");
            }
            catch (Exception ddss)
            {
                string sdsds = ddss.Message;
            }
            db.Dispose();
            return ds;
        }


        public static DataSet GetAllStudents()
        {
            Students rt = new Students();
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet("dfs2www");
            try
            {
                ds = db.ExecuteDataSet("getAllAdmittedStudents");
            }
            catch (Exception ex)
            {
                string sdsds = ex.Message;
                throw;
            }
            db.Dispose();
            return ds;
        }
    }



}
