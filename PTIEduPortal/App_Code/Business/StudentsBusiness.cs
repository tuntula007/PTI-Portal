

#region using statements

using System;
using System.Collections;
using CybSoft.EduPortal.Data;

using System.Data;
using System.Data.SqlClient;
using CybSoft.EduPortal.Business;
using System.Collections.Generic;
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
                rt.MatricNumber = MatNo.ToUpper();
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
                rt.TeachingSubject = nr["TeachingSubject"].ToString();
                rt.TeachingCenter = nr["TeachingCenter"].ToString();
                rt.ExaminationCenter = nr["ExaminationCenter"].ToString();
                if (rt.isNewStudent.Equals("1") == true)
                {
                    if (string.IsNullOrEmpty(rt.MatricNumber) == true)
                    {
                        rt.MatricNumber = rt.RegNo;
                    }
                }
                rt.DLCMail = (nr["dlcmail"] == null && string.IsNullOrEmpty(nr["dlcmail"].ToString())) ? "PENDING" : nr["dlcmail"].ToString();
                rt.Nationality = nr["Nationality"].ToString();
                rt.SponsorAddress = nr["SponsorAddress"].ToString();
                rt.SponsorName = nr["SponsorName"].ToString();
                rt.SponsorPhone = nr["SponsorPhone"].ToString();
                rt.SponsorRelationship = nr["SponsorRelationship"].ToString();
                rt.SponsorEmail = nr["SponsorEmail"].ToString();
                rt.Religion = nr["Religion"].ToString();
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
                rt.EntryMode = nr["EntryMode"].ToString();
                rt.PassportFile = nr["PassportFile"].ToString();
                rt.LocalPassportFile = nr["LocalPassportFile"].ToString();
                rt.CanChangePassport = (int)nr["CanChangePassport"];
                rt.Duration = nr["Duration"].ToString();
                rt.isEvening = nr["isEvening"].ToString();
                //rt.SponsorEmail = nr["isEvening"].ToString();
                ds.Dispose();
                
            }
            catch (Exception ex)
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
                    dr.Dispose();
                    db.Dispose();
                    return true;
                }
                dr.Dispose();
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            
            db.Dispose();
            return false;

        }
        public static bool isFormNumberExistsForAdmission(string formnumber)
        {
            string mykey = "isFormNumberExists_checkFormNumberExists_" + formnumber;
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            try
            {
                db.Parameters.Add(new SqlParameter("@formnumber", formnumber));
                dr = (SqlDataReader)db.ExecuteReader("checkFormNumberExistsForAdmission");
                if (dr.HasRows)
                {
                    dr.Dispose();
                    db.Dispose();
                    return true;
                }
                dr.Dispose();
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
                    dr.Dispose();
                    db.Dispose();
                    return true;
                }
                mykey = "PayStudio_checkStudioFeesbyDepartment_" + departmentid.ToString();
                db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@departmentid", departmentid));
                dr = (SqlDataReader)db.ExecuteReader("checkStudioFeesbyDepartment");
                if (dr.HasRows)
                {
                    dr.Dispose();
                    db.Dispose();
                    return true;
                }

                mykey = "PayStudio_checkStudioFeesbyCourseofStudy_" + courseofstudyid.ToString();
                db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@courseofstudyid", courseofstudyid));
                dr = (SqlDataReader)db.ExecuteReader("checkStudioFeesbyCourseOfStudy");
                if (dr.HasRows)
                {
                    dr.Dispose();
                    db.Dispose();
                    return true;
                }
                dr.Dispose();
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
                db.Parameters.Add(new SqlParameter("@TeachingCenter", st.TeachingCenter));
                db.Parameters.Add(new SqlParameter("@ExaminationCenter", st.ExaminationCenter));
                db.Parameters.Add(new SqlParameter("@Repeating", st.Repeating));               
                db.Parameters.Add(new SqlParameter("@SponsorRelationship", st.SponsorRelationship));                
                db.Parameters.Add(new SqlParameter("@Religion", st.Religion));
                db.Parameters.Add(new SqlParameter("@SponsorEmail", st.SponsorEmail));

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
        public static bool UpdateStudentCanregister(string matNumber, string flag)
        {
            string mykey = "Students_Update_CanRegister_" + matNumber;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", matNumber));
                db.Parameters.Add(new SqlParameter("@flag", flag));

                int i = db.ExecuteNonQuery("Students_Update_CanRegister");
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
        public static bool UpdateStudentCanregister2(string matNumber, string flag)
        {
            string mykey = "Students_Update_CanRegister2_" + matNumber;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", matNumber));
                db.Parameters.Add(new SqlParameter("@flag", flag));

                int i = db.ExecuteNonQuery("Students_Update_CanRegister2");
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
        public StudentPayment GetStudentPayments(string MatNo, string session)
        {
            //IList <StudentPayment>
            StudentPayment rt = new StudentPayment();
            StudentPayment val = null;
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Students_getStudenPaymentByMatricNumber_" + MatNo;
            DBAccess db = new DBAccess(mykey);
            
            try
            {
                
                db.Parameters.Add(new SqlParameter("@MatNo", MatNo));
                db.Parameters.Add(new SqlParameter("@Session", session));
                 
                //SchoolWebService.Service1 NowService = new SchoolWebService.Service1();
                DataSet ds = new DataSet("Stud");
                ds = db.ExecuteDataSet("getStudentPaymentByMatricNumber");
                //string Qstring = "SELECT * from Students WHERE MatriculationNumber = '" + MatNo + "'";
               // ds = NowService.RetriveDat(Qstring);
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                rt.MatricNumber = MatNo;
                rt.PaymentType = nr["PaymentType"].ToString();
                rt.DatePaid = nr["DatePaid"].ToString();
                rt.Pin = nr["Pin"].ToString();
                rt.PinSerialNo = nr["PinSerialNo"].ToString();
                rt.PinValue = double.Parse(nr["PinValue"].ToString());
                rt.Session = nr["Session"].ToString();
                ds.Dispose();
                
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            val = (string.IsNullOrEmpty(rt.MatricNumber) == false) ? rt : val;
            return val;
        }
        public static DataSet GetStudentPayDescription(string MatNo, string session)
        {
            //IList <StudentPayment>
            DataSet ds = new DataSet("StudentPayDescription");
            string mykey = "Students_getStudentPayDescription_" + MatNo;
            DBAccess db = new DBAccess(mykey);

            try
            {

                db.Parameters.Add(new SqlParameter("@MatricNumber", MatNo));
                db.Parameters.Add(new SqlParameter("@Session", session));

                //SchoolWebService.Service1 NowService = new SchoolWebService.Service1();
                ds = db.ExecuteDataSet("getStudentPayDescription");

            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return ds;
        }
        public StudentPayment GetStudentPayments(string MatNo, string session, string semester)
        {
            //IList <StudentPayment>
            StudentPayment rt = new StudentPayment();
            StudentPayment val = null;
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Students_getStudenPartPaymentByMatricNumber_" + MatNo;
            DBAccess db = new DBAccess(mykey);

            try
            {

                db.Parameters.Add(new SqlParameter("@MatNo", MatNo));
                db.Parameters.Add(new SqlParameter("@Session", session));
                db.Parameters.Add(new SqlParameter("@Semester", semester));

                //SchoolWebService.Service1 NowService = new SchoolWebService.Service1();
                DataSet ds = new DataSet("Stud");
                ds = db.ExecuteDataSet("getStudentPartPaymentByMatricNumber");
                //string Qstring = "SELECT * from Students WHERE MatriculationNumber = '" + MatNo + "'";
                // ds = NowService.RetriveDat(Qstring);
                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                rt.MatricNumber = MatNo;
                rt.PaymentType = nr["PaymentType"].ToString();
                rt.DatePaid = nr["DatePaid"].ToString();
                rt.Pin = nr["Pin"].ToString();
                rt.PinSerialNo = nr["PinSerialNo"].ToString();
                rt.PinValue = double.Parse(nr["PinValue"].ToString());
                rt.Session = nr["Session"].ToString();
                ds.Dispose();

            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            val = (string.IsNullOrEmpty(rt.MatricNumber) == false) ? rt : val;
            return val;
        }
        public static string GetStudentTotalPayment(string MatNo, string session)
        {
            string result = "";
            string mykey = "GetStudentTotalPayment_" + MatNo;
            DBAccess db = new DBAccess();
            SqlDataReader dr;

                try
                {
                    db.Parameters.Add(new SqlParameter("@MatricNumber", MatNo));
                    db.Parameters.Add(new SqlParameter("@Session", session));
                    dr = (SqlDataReader)db.ExecuteReader("getStudentTotalPayment");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetDecimal(dr.GetOrdinal("Amount")).ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            db.Dispose();
            return result;
        }

    }

}
