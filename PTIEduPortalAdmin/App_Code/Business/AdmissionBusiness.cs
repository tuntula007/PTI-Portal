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


    /// <summary>
    /// Summary description for AdmissionBusiness
    /// </summary>
    public class AdmissionBusiness
    {
        public AdmissionBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method delete Admitted Students record by Reg Number
        /// </summary>
        /// <param name="RegNoo"> Reg Number</param>
        /// <returns></returns>
        public static bool DeleteStudent(string RegNo)
        {
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                int i = db.ExecuteNonQuery("AdmittedStudent_DeleteByRegNo");
                db.Dispose();
                if (i > 0)
                    return true;
            }
            catch (Exception ex)
            {
                db.Dispose();
                throw;
            }
            return false;
        }

        public static bool isFormNumberExists(string formnumber)
        {
            string mykey = "isFormNumberExists_checkFormNumberExists_" + formnumber;
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            try
            {
                db.Parameters.Add(new SqlParameter("@formnumber", formnumber));
                dr = (SqlDataReader)db.ExecuteReader("checkFormNumberExists");
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

        public static bool UpdateRecord(Applicants st)
        {
            //Applicants ap = new Applicants ();
            string mykey = "Applicants_getApplicantByFormNumber_" + st.FormNumber;
            DBAccess db = new DBAccess(mykey);
          
            try
            {

                db.Parameters.Add(new SqlParameter("@FormNumber", st.FormNumber));
                db.Parameters.Add(new SqlParameter("@Surname", st.Surname));
                db.Parameters.Add(new SqlParameter("@Othernames", st.OtherNames));
                db.Parameters.Add(new SqlParameter("@Title", st.Title));
                db.Parameters.Add(new SqlParameter("@Maidenname", st.MaidenName));
                db.Parameters.Add(new SqlParameter("@DateOfBirth", st.DateOfBirth));
                db.Parameters.Add(new SqlParameter("@MaritalStatus", st.MaritalStatus));
                db.Parameters.Add(new SqlParameter("@Sex", st.Sex));
                db.Parameters.Add(new SqlParameter("@Nationality", st.Nationality));
                db.Parameters.Add(new SqlParameter("@State", st.State));
                db.Parameters.Add(new SqlParameter("@LocalGovernmentArea", st.LocalGovernmentArea));
                db.Parameters.Add(new SqlParameter("@HomeAddress", st.HomeAddress));
                db.Parameters.Add(new SqlParameter("@Email", st.Email));
                db.Parameters.Add(new SqlParameter("@PhoneNumber", st.PhoneNumber));
                db.Parameters.Add(new SqlParameter("@PostalAddress", st.PostalAddress));
                
                db.Parameters.Add(new SqlParameter("@AdmittedSession", st.AdmittedSession));
                db.Parameters.Add(new SqlParameter("@SponsorName", st.SponsorName));
                db.Parameters.Add(new SqlParameter("@SponsorAddress", st.SponsorAddress));
                db.Parameters.Add(new SqlParameter("@SponsorPhone", st.SponsorPhone));
              //  db.Parameters.Add(new SqlParameter("@Repeating", st.Repeating));

              db.Parameters.Add(new SqlParameter("@Programme", st.Programme));
              db.Parameters.Add(new SqlParameter("@ModeOfStudy", st.ModeOfStudy));
              db.Parameters.Add(new SqlParameter("@AdmittedLevel", st.AdmittedLevel));
              db.Parameters.Add(new SqlParameter("@PresentEmployment", st.PresentEmployment));
              db.Parameters.Add(new SqlParameter("@PositionHeld", st.PositionHeld));
              db.Parameters.Add(new SqlParameter("@PreviousEmployment", st.PreviousEmployment));
              db.Parameters.Add(new SqlParameter("@PresentHighestQualification", st.PresentHighestQualification));
              db.Parameters.Add(new SqlParameter("@SchoolAttended", st.SchoolAttended));
              db.Parameters.Add(new SqlParameter("@FirstDepartmentID", st.FirstDepartmentID));
              db.Parameters.Add(new SqlParameter("@SecondDepartmentID", st.SecondDepartmentID));
              db.Parameters.Add(new SqlParameter("@PreviousCourseAttended", st.PreviousCourseAttended));
              db.Parameters.Add(new SqlParameter("@PreviousGrade", st.PreviousGrade));
              db.Parameters.Add(new SqlParameter("@PreviousRegNo", st.PreviousRegNo));
              db.Parameters.Add(new SqlParameter("@IsIndigene", st.IsIndigene));
              db.Parameters.Add(new SqlParameter("@Center", st.Center));

                int i = db.ExecuteNonQuery("Applicants_Update");
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

        public static bool UpdatePassportRecord(string href, string formno, string localfile)
        {
            string mykey = "Applicants_getApplicantByFormNumber_" + formno;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@FormNumber", formno));
                db.Parameters.Add(new SqlParameter("@PassportFile", href));
                db.Parameters.Add(new SqlParameter("@LocalPassportFile", localfile));
                int i = db.ExecuteNonQuery("Applicant_Update_Passport");
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

        public static bool isUserNameExists(string userName)
        {
            string mykey = "isUserNameExists_checkUserNameExists_" + userName ;
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            try
            {
                db.Parameters.Add(new SqlParameter("@UserName", userName));
                dr = (SqlDataReader)db.ExecuteReader("checkUserNameExists");
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
            catch (Exception)
            {
                throw;
            }
            db.Dispose();
            return ds;
        }
        public static DataSet getAddmissionCourses()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getAddmissionCourseOfStudy");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataSet getCourseOfStudy(string programme, string mode, string faculty)
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                db.Parameters.Add(new SqlParameter("@programme", programme));
                db.Parameters.Add(new SqlParameter("@mode", mode));
                db.Parameters.Add(new SqlParameter("@faculty", faculty));
                ds = db.ExecuteDataSet("getCourseOfStudyByFaculty");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet getProgramme(string mode)
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", mode));
                ds = db.ExecuteDataSet("getProgrammeByStudyMode");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet getModeOfStudy()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getAddmissionModeOfStudy");
                db.Dispose();
                return ds;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static DataSet getFaculty()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getFaculty");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet getProgramme()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getAddmissionProgramme");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public static DataSet getAcademicLevel()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getAcademicLevel");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet getAdmittedSession()
        {
            DBAccess db = new DBAccess();
            DataSet ds;
            try
            {
                ds = db.ExecuteDataSet("getAdmittedSession");
                db.Dispose();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void UpdateProfile(string RegNo, string Surname, string OtherNames, string AcademicLevel, string AdmittedSession, string Programme, string ModeOfStudy, string CourseOfStudy, string dateofBirth, string Faculty,string Email)
        {
            DBAccess db = new DBAccess();
            
            db.AddParameter(new SqlParameter("@RegNo", RegNo));
            db.AddParameter(new SqlParameter("@Surname", Surname));
            db.AddParameter(new SqlParameter("@OtherNames", OtherNames));
            db.AddParameter(new SqlParameter("@AcademicLevel", AcademicLevel));
            db.AddParameter(new SqlParameter("@AdmittedSession", AdmittedSession));
            db.AddParameter(new SqlParameter("@Programme", Programme));
            db.AddParameter(new SqlParameter("@ModeOfStudy", ModeOfStudy));
            db.AddParameter(new SqlParameter("@CourseOfStudy", CourseOfStudy));
            db.AddParameter(new SqlParameter("@dateofBirth", dateofBirth));
            db.AddParameter(new SqlParameter("@Faculty", Faculty));
            db.AddParameter(new SqlParameter("@Email", Email));

            try
            {
                db.ExecuteNonQuery("UpdateAdmittedStudentProfile");
                db.Dispose();
            }
            catch (Exception)
            {
                db.Dispose();
                throw;
            }
        }

        public Admission GetAmmitedStudentByRegNo(string RegNo)
        {

            Admission admtdStudnt = new Admission();
            DataTable dt = null;
            DataRow dr = null;
            DBAccess db = new DBAccess();
            try
            {

                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));

                DataSet ds = new DataSet("App");
                ds = db.ExecuteDataSet("getAdmittedStudentByRegNo");

                dt = ds.Tables[0];
                dr = dt.Rows[0];
                admtdStudnt.Surname = dr["Surname"].ToString();
                admtdStudnt.OtherNames = dr["OtherNames"].ToString();
                admtdStudnt.Address = dr["Address"].ToString();
                admtdStudnt.AcademicLevel = dr["AcademicLevel"].ToString();	  
                admtdStudnt.RegNo = dr["RegNo"].ToString();
                admtdStudnt.SessionName = dr["AdmittedSession"].ToString();
                admtdStudnt.Programme = dr["Programme"].ToString();
                admtdStudnt.ModeOfStudy = dr["ModeOfStudy"].ToString();
                admtdStudnt.CourseOfStudy = dr["courseOfStudy"].ToString();
                admtdStudnt.DateOfBirth = dr["DateOfBirth"].ToString();
                admtdStudnt.Faculty = dr["Faculty"].ToString();
                admtdStudnt.Email = dr["Email"].ToString();

            }
            catch (Exception)
            {
                //log
                throw;
            }
            db.Dispose();
            return admtdStudnt;
        }


        public static Admission GetAdmitedStudentsob(string Programme, string ModeOfStudy, string CourseOfStudyID, string AcademicLevel, string RegNo)
        {
            Admission admtdStudnt = new Admission();
            DataTable dt = null;
            DataRow dr = null;
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
            if (string.IsNullOrEmpty(RegNo))
            {
                RegNo = "";
            }
            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", Cid));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Semester", RegNo));
                ds = db.ExecuteDataSet("getAdmittedStudents");

                dt = ds.Tables[0];
                dr = dt.Rows[0];
                admtdStudnt.Surname = dr["Surname"].ToString();
                admtdStudnt.OtherNames = dr["OtherNames"].ToString();
                admtdStudnt.AcademicLevel = dr["AcademicLevel"].ToString();
                admtdStudnt.RegNo = dr["RegNo"].ToString();
                admtdStudnt.SessionName = dr["AdmittedSession"].ToString();
                admtdStudnt.Programme = dr["Programme"].ToString();
                admtdStudnt.ModeOfStudy = dr["ModeOfStudy"].ToString();
                admtdStudnt.CourseOfStudy = dr["courseOfStudy"].ToString();
                admtdStudnt.Faculty = dr["Faculty"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            db.Dispose();
            return admtdStudnt;
        }

        public static DataSet GetAdmitedStudents(string Programme, string ModeOfStudy, string CourseOfStudy, string AcademicLevel, string Faculty, string RegNo,string admsnsession)
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
            if (string.IsNullOrEmpty(AcademicLevel))
            {
                AcademicLevel = "";
            }
            if (string.IsNullOrEmpty(Faculty))
            {
                Faculty = "";
            }
            if (string.IsNullOrEmpty(RegNo))
            {
                RegNo = "";
            }
            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@CourseOfStudy", CourseOfStudy));
                db.Parameters.Add(new SqlParameter("@AcademicLevel", AcademicLevel));
                db.Parameters.Add(new SqlParameter("@Faculty", Faculty));
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@Session", admsnsession)); 
                ds = db.ExecuteDataSet("getAdmittedStudents7");
            }
            catch (Exception)
            {
                throw;
            }
            db.Dispose();
            return ds;
        }

    }
}