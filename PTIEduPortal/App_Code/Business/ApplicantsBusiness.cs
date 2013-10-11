#region using statements

using System;
using System.Collections;
using CybSoft.EduPortal.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CybSoft.EduPortal.Business;

#endregion


namespace CybSoft.EduPortal.Business
{
    /// <summary>
    /// Summary description for ApplicantsBusiness
    /// </summary>
    public class ApplicantsBusiness
    {
        private static string str = ConfigurationManager.AppSettings["ConnString"];

        public ApplicantsBusiness()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This method gets Students record by Matric Number
        /// </summary>
        /// <param name="RegNoo"> Students Matric No</param>
        /// <returns></returns>
        /// 

        public Applicants GetApplicantsFromApplicantsHistryByFormNo(string FormNo)
        {
         
            Applicants rt = new Applicants();
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Applicants_GetApplicantsFromApplicantsHistryByFormNo_" + FormNo;
            DBAccess db = new DBAccess(mykey);

            try
            {

                db.Parameters.Add(new SqlParameter("@FormNumber", FormNo));

                
                DataSet ds = new DataSet("App");
                ds = db.ExecuteDataSet("getApplicantFromApplicantsHistryByFormNo");

                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                //rt.ricNumber = MatNo;
                rt.FormNumber = FormNo;
                rt.RegNo = nr["RegNo"].ToString();
                rt.AdmissionStatus = nr["AdmissionStatus"].ToString();
                rt.Country = nr["Country"].ToString();
                rt.DateOfBirth = nr["DateOfBirth"].ToString();
                rt.AdmittedSession = nr["AdmittedSession"].ToString();
                rt.Email = nr["Email"].ToString();
                rt.Surname = nr["Surname"].ToString();
                rt.OtherNames = nr["OtherNames"].ToString();
                rt.HomeAddress = nr["HomeAddress"].ToString();
                rt.PostalAddress = nr["PostalAddress"].ToString();
                rt.MaidenName = nr["MaidenName"].ToString();
                rt.LocalGovernmentArea = nr["LocalGovernmentArea"].ToString();
                rt.MaritalStatus = nr["MaritalStatus"].ToString();
                rt.UserName = nr["UserName"].ToString();
                rt.Nationality = nr["Nationality"].ToString();
                rt.SponsorAddress = nr["SponsorAddress"].ToString();
                rt.SponsorName = nr["SponsorName"].ToString();
                rt.SponsorPhone = nr["SponsorPhone"].ToString();
                rt.SponsorEmail = nr["SponsorEmail"].ToString();
                rt.SponsorRelationship = nr["SponsorRelationship"].ToString();
                rt.Sponsor = nr["Sponsor"].ToString();
                rt.PhoneNumber = nr["PhoneNumber"].ToString();
                rt.PresentSession = nr["PresentSession"].ToString();
                rt.PlaceOfBirth = nr["PlaceOfBirth"].ToString();
                //rt.RegNo = nr["RegNo"].ToString();
                rt.Referral = (nr["Referral"] != null) ? nr["Referral"].ToString() : "";
                rt.Sex = nr["Sex"].ToString();
                rt.State = nr["State"].ToString();
                
                rt.Programme = nr["Programme"].ToString();
                rt.ModeOfStudy = nr["ModeOfStudy"].ToString();
                rt.EntryMode = nr["EntryMode"].ToString();
                rt.FirstDepartmentID = (int) nr["FirstDepartmentID"];
                rt.SecondDepartmentID = (int) nr["SecondDepartmentID"];
                //rt.FirstFacultyID = (int)nr["FirstFacultyId"];
                //rt.SecondFacultyID = (int)nr["SecondFacultyId"];
                rt.FirstDepartment = nr["FirstDepartment"].ToString();//ApplicantsBusiness.courseofStudyName(rt.FirstDepartmentID);
                rt.SecondDepartment = nr["SecondDepartment"].ToString();//ApplicantsBusiness.courseofStudyName(rt.SecondDepartmentID);
                rt.ThirdDepartment = nr["ThirdDepartment"].ToString();
                rt.CourseofStudy1 = nr["CourseofStudy1"].ToString();
                rt.CourseofStudy2 = nr["CourseofStudy2"].ToString();
                rt.CourseofStudy3 = nr["CourseofStudy3"].ToString();
                rt.Title = nr["Title"].ToString();
                rt.IsIndigene = nr["IsIndigene"].ToString();
                rt.UTMEFirstChoice = nr["UTMEFirstChoice"].ToString();
                rt.UTMEFirstChoiceCourse = nr["UTMEFirstChoiceCourse"].ToString();
                rt.UTMERegNumber = nr["UTMERegNo"].ToString();
                rt.UTMEScore = (nr["UTMEScore"] != null) ? int.Parse(nr["UTMEScore"].ToString()) : 0;
                rt.UTMESecondChoice = nr["UTMESecondChoice"].ToString();
                rt.UTMESecondChoiceCourse = nr["UTMESecondChoiceCourse"].ToString();
                rt.LocalPassportFile = nr["LocalPassportFile"].ToString();
                rt.PassportFile = nr["PassportFile"].ToString();
                rt.PresentEmployment = nr["PresentEmployment"].ToString();
                rt.PositionHeld = nr["PositionHeld"].ToString();
                rt.PreviousEmployment = nr["PreviousEmployment"].ToString();
                
                rt.PresentHighestQualification = nr["PresentHighestQualification"].ToString();
                rt.SchoolAttended = nr["SchoolAttended"].ToString();

                rt.Religion = (nr["Religion"] != null) ? nr["Religion"].ToString() : "";
                rt.Disability = (nr["Disability"] != null) ? nr["Disability"].ToString() : "";
                rt.BloodGroup = (nr["BloodGroup"] != null) ? nr["BloodGroup"].ToString() : "";
                rt.ExaminationCenter = (nr["ExaminationCenter"] != null) ? nr["ExaminationCenter"].ToString() : "";
                rt.choiceStatus = (nr["ChoiceStatus"] != null) ? int.Parse(nr["ChoiceStatus"].ToString()) : 0;
                rt.educationStatus = (nr["EducationStatus"] != null) ? int.Parse(nr["EducationStatus"].ToString()) : 0;
                rt.posteducationStatus = (rt.EntryMode.ToLower() != "hnd") ? 1 : (nr["PostEducationStatus"] != null) ? int.Parse(nr["PostEducationStatus"].ToString()) : 0;
                rt.posteducationStatus = (rt.EntryMode.ToLower() != "nd") ? 1 : (nr["PostEducationStatus"] != null) ? int.Parse(nr["PostEducationStatus"].ToString()) : 0;
                rt.personalInfoStatus = (nr["PersonalInfoStatus"] != null) ? int.Parse(nr["PersonalInfoStatus"].ToString()) : 0; 
                rt.PreviousAttendedFrom = (nr["PreviousAttendedFrom"] != null) ? nr["PreviousAttendedFrom"].ToString() : "";
                rt.PreviousAttendedTo = (nr["PreviousAttendedTo"] != null) ? nr["PreviousAttendedTo"].ToString() : "";
                rt.TeachingCenter = (nr["TeachingCenter"] != null) ? nr["TeachingCenter"].ToString() : "";

                rt.PreviousCourseAttended = nr["PreviousCourseAttended"].ToString();
                rt.PreviousGrade = nr["PreviousGrade"].ToString();
                rt.PreviousRegNo = nr["PreviousRegNo"].ToString();
                rt.Center = nr["Center"].ToString();
                rt.EntranceExamsubj1 = nr["EntranceExamsubj1"].ToString();
                rt.printStatus = int.Parse(nr["PrintStatus"].ToString());
                rt.SubmitStatus = int.Parse(nr["SubmitStatus"].ToString());
                rt.AdmissionLetterPrintedStatus = int.Parse(nr["AdmissionLetterPrintedStatus"].ToString());
                rt.MyInstitution = nr["myInstitution"].ToString();
                rt.MyCourseName = nr["myCourseName"].ToString();
                rt.MyCourseGrade = nr["myCourseGrade"].ToString();
                rt.MyPostMatric = nr["myPostMatric"].ToString();
                rt.MyQualYear = nr["myQualYear"].ToString();
                rt.MyQualYear = nr["myQualYear"].ToString();
                rt.MyPostProgramme = nr["myPostProgramme"].ToString();
                //rt.MyOtherSchsInfo = nr["myOtherSchsInfo"].ToString();
                //[PrintStatus]

            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return rt;
        }
        public Applicants GetApplicantsByFormNo(string FormNo)
        {
         
            Applicants rt = new Applicants();
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Applicants_getApplicantByFormNumber_" + FormNo;
            DBAccess db = new DBAccess(mykey);

            try
            {

                db.Parameters.Add(new SqlParameter("@FormNumber", FormNo));

                
                DataSet ds = new DataSet("App");
                ds = db.ExecuteDataSet("getApplicantByFormNumber");

                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                //rt.ricNumber = MatNo;
                rt.FormNumber = FormNo;
                rt.RegNo = nr["RegNo"].ToString();
                rt.AdmissionStatus = nr["AdmissionStatus"].ToString();
                rt.Country = nr["Country"].ToString();
                rt.DateOfBirth = nr["DateOfBirth"].ToString();
                rt.AdmittedSession = nr["AdmittedSession"].ToString();
                rt.Email = nr["Email"].ToString();
                rt.Surname = nr["Surname"].ToString();
                rt.OtherNames = nr["OtherNames"].ToString();
                rt.HomeAddress = nr["HomeAddress"].ToString();
                rt.PostalAddress = nr["PostalAddress"].ToString();
                rt.MaidenName = nr["MaidenName"].ToString();
                rt.LocalGovernmentArea = nr["LocalGovernmentArea"].ToString();
                rt.MaritalStatus = nr["MaritalStatus"].ToString();
                rt.UserName = nr["UserName"].ToString();
                rt.Nationality = nr["Nationality"].ToString();
                rt.SponsorAddress = nr["SponsorAddress"].ToString();
                rt.SponsorName = nr["SponsorName"].ToString();
                rt.SponsorPhone = nr["SponsorPhone"].ToString();
                rt.SponsorEmail = nr["SponsorEmail"].ToString();
                rt.SponsorRelationship = nr["SponsorRelationship"].ToString();
                rt.Sponsor = nr["Sponsor"].ToString();
                rt.PhoneNumber = nr["PhoneNumber"].ToString();
                rt.PresentSession = nr["PresentSession"].ToString();
                rt.PlaceOfBirth = nr["PlaceOfBirth"].ToString();
                //rt.RegNo = nr["RegNo"].ToString();
                rt.Referral = (nr["Referral"] != null) ? nr["Referral"].ToString() : "";
                rt.Sex = nr["Sex"].ToString();
                rt.State = nr["State"].ToString();
                
                rt.Programme = nr["Programme"].ToString();
                rt.ModeOfStudy = nr["ModeOfStudy"].ToString();
                rt.EntryMode = nr["EntryMode"].ToString();
                rt.FirstDepartmentID = (int) nr["FirstDepartmentID"];
                rt.SecondDepartmentID = (int) nr["SecondDepartmentID"];
                //rt.FirstFacultyID = (int)nr["FirstFacultyId"];
                //rt.SecondFacultyID = (int)nr["SecondFacultyId"];
                rt.FirstDepartment = nr["FirstDepartment"].ToString();//ApplicantsBusiness.courseofStudyName(rt.FirstDepartmentID);
                rt.SecondDepartment = nr["SecondDepartment"].ToString();//ApplicantsBusiness.courseofStudyName(rt.SecondDepartmentID);
                rt.ThirdDepartment = nr["ThirdDepartment"].ToString();
                rt.CourseofStudy1 = nr["CourseofStudy1"].ToString();
                rt.CourseofStudy2 = nr["CourseofStudy2"].ToString();
                rt.CourseofStudy3 = nr["CourseofStudy3"].ToString();
                rt.Title = nr["Title"].ToString();
                rt.IsIndigene = nr["IsIndigene"].ToString();
                rt.UTMEFirstChoice = nr["UTMEFirstChoice"].ToString();
                rt.UTMEFirstChoiceCourse = nr["UTMEFirstChoiceCourse"].ToString();
                rt.UTMERegNumber = nr["UTMERegNo"].ToString();
                rt.UTMEScore = (nr["UTMEScore"] != null) ? int.Parse(nr["UTMEScore"].ToString()) : 0;
                rt.UTMESecondChoice = nr["UTMESecondChoice"].ToString();
                rt.UTMESecondChoiceCourse = nr["UTMESecondChoiceCourse"].ToString();
                rt.LocalPassportFile = nr["LocalPassportFile"].ToString();
                rt.PassportFile = nr["PassportFile"].ToString();
                rt.PresentEmployment = nr["PresentEmployment"].ToString();
                rt.PositionHeld = nr["PositionHeld"].ToString();
                rt.PreviousEmployment = nr["PreviousEmployment"].ToString();
                
                rt.PresentHighestQualification = nr["PresentHighestQualification"].ToString();
                rt.SchoolAttended = nr["SchoolAttended"].ToString();

                rt.Religion = (nr["Religion"] != null) ? nr["Religion"].ToString() : "";
                rt.Disability = (nr["Disability"] != null) ? nr["Disability"].ToString() : "";
                rt.BloodGroup = (nr["BloodGroup"] != null) ? nr["BloodGroup"].ToString() : "";
                rt.ExaminationCenter = (nr["ExaminationCenter"] != null) ? nr["ExaminationCenter"].ToString() : "";
                rt.choiceStatus = (nr["ChoiceStatus"] != null) ? int.Parse(nr["ChoiceStatus"].ToString()) : 0;
                rt.educationStatus = (nr["EducationStatus"] != null) ? int.Parse(nr["EducationStatus"].ToString()) : 0;
                rt.posteducationStatus = (rt.EntryMode.ToLower() != "hnd") ? 1 : (nr["PostEducationStatus"] != null) ? int.Parse(nr["PostEducationStatus"].ToString()) : 0;
                rt.posteducationStatus = (rt.EntryMode.ToLower() != "nd") ? 1 : (nr["PostEducationStatus"] != null) ? int.Parse(nr["PostEducationStatus"].ToString()) : 0;
                rt.personalInfoStatus = (nr["PersonalInfoStatus"] != null) ? int.Parse(nr["PersonalInfoStatus"].ToString()) : 0; 
                rt.PreviousAttendedFrom = (nr["PreviousAttendedFrom"] != null) ? nr["PreviousAttendedFrom"].ToString() : "";
                rt.PreviousAttendedTo = (nr["PreviousAttendedTo"] != null) ? nr["PreviousAttendedTo"].ToString() : "";
                rt.TeachingCenter = (nr["TeachingCenter"] != null) ? nr["TeachingCenter"].ToString() : "";

                rt.PreviousCourseAttended = nr["PreviousCourseAttended"].ToString();
                rt.PreviousGrade = nr["PreviousGrade"].ToString();
                rt.PreviousRegNo = nr["PreviousRegNo"].ToString();
                rt.Center = nr["Center"].ToString();
                rt.EntranceExamsubj1 = nr["EntranceExamsubj1"].ToString();
                rt.printStatus = int.Parse(nr["PrintStatus"].ToString());
                rt.SubmitStatus = int.Parse(nr["SubmitStatus"].ToString());
                rt.AdmissionLetterPrintedStatus = int.Parse(nr["AdmissionLetterPrintedStatus"].ToString());
                rt.MyInstitution = nr["myInstitution"].ToString();
                rt.MyCourseName = nr["myCourseName"].ToString();
                rt.MyCourseGrade = nr["myCourseGrade"].ToString();
                rt.MyPostMatric = nr["myPostMatric"].ToString();
                rt.MyQualYear = nr["myQualYear"].ToString();
                rt.MyQualYear = nr["myQualYear"].ToString();
                rt.MyPostProgramme = nr["myPostProgramme"].ToString();
                //rt.MyOtherSchsInfo = nr["myOtherSchsInfo"].ToString();
                //[PrintStatus]

            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return rt;
        }

        public AdmissionLetter GetAdmittedApplicantsByFormNo(string FormNo)
        {

            var rt = new AdmissionLetter() ;
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Applicants_getApplicantByFormNumber_" + FormNo;
            DBAccess db = new DBAccess(mykey);

            try
            {

                db.Parameters.Add(new SqlParameter("@FormNumber", FormNo));


                DataSet ds = new DataSet("App");
                ds = db.ExecuteDataSet("getAdmittedApplicantByFormNumber");

                NowTable = ds.Tables[0];
                nr = NowTable.Rows[0];
                //rt.ricNumber = MatNo;
                rt.RegNo = FormNo;
                rt.RegNo = nr["RegNo"].ToString();
                rt.Surname = nr["Surname"].ToString();
                rt.OtherNames = nr["OtherNames"].ToString();
                rt.Address = nr["HomeAddress"].ToString();
              
                //rt.RegNo = nr["RegNo"].ToString();
             
                rt.Programme = nr["Programme"].ToString();
                rt.EntryMode = nr["EntryMode"].ToString();
              

                
                rt.CourseOfStudy = nr["CourseOfStudy"].ToString();
               
               
              

            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return rt;
        }

        public DataSet GetAdmittedApplicantByFormNo(string FormNo)
        {

            var rt = new AdmissionLetter();
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "Applicants_getApplicantByFormNumber_" + FormNo;
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet("App");

            try
            {

                db.Parameters.Add(new SqlParameter("@FormNumber", FormNo));

             
                ds = db.ExecuteDataSet("getAdmittedApplicantByFormNumber");

                NowTable = ds.Tables[0];
              



            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            return ds ;
        }


        public static int GetCurrentView(string regNo)
        {

            int iView = 0, iPers = 0, iChoice = 0, iEdu = 0, iPostEdu = 0, iSubmit = 0, tst = 0;

            string sQueryTest = "SELECT ChoiceStatus,EducationStatus,PersonalInfoStatus,PostEducationStatus,SubmitStatus  FROM Applicants WHERE regNo='" + regNo + "' OR FormNumber='" + regNo + "'";
            DataTable dt = new DataTable();
            try
            {
                dt = new Utility().SelectQuery(sQueryTest).Tables[0];
                tst = (dt.Rows[0]["PostEducationStatus"] != null) ? int.Parse(dt.Rows[0]["PostEducationStatus"].ToString()) : 0;
            }
            catch
            {
                throw;
            }
            if (tst == 1)
            {
                iPers = (dt.Rows[0]["PersonalInfoStatus"] != null) ? int.Parse(dt.Rows[0]["PersonalInfoStatus"].ToString()) : 0;
                iChoice += (dt.Rows[0]["ChoiceStatus"] != null) ? int.Parse(dt.Rows[0]["ChoiceStatus"].ToString()) : 0;
                iEdu += (dt.Rows[0]["EducationStatus"] != null) ? int.Parse(dt.Rows[0]["EducationStatus"].ToString()) : 0;
                iPostEdu += (dt.Rows[0]["PostEducationStatus"] != null) ? int.Parse(dt.Rows[0]["PostEducationStatus"].ToString()) : 1;
                iSubmit += (dt.Rows[0]["SubmitStatus"] != null) ? int.Parse(dt.Rows[0]["SubmitStatus"].ToString()) : 0;
            }
            else
            {
                string sQuery = "SELECT ChoiceStatus,EducationStatus,PersonalInfoStatus,SubmitStatus, "
                    //+ " CASE WHEN EntryMode<>'HND' THEN 1 ELSE PostEducationStatus END AS [PostEducationStatus]"
                    + "CASE WHEN EntryMode = 'HND' THEN 0 WHEN EntryMode = 'ND' THEN 0 ELSE 1 END AS [PostEducationStatus]"
                    + " FROM Applicants WHERE regNo='" + regNo + "' OR FormNumber='" + regNo + "'";
                DataTable nr = new DataTable();
                try
                {
                    nr = new Utility().SelectQuery(sQuery).Tables[0];
                    iPers = (nr.Rows[0]["PersonalInfoStatus"] != null) ? int.Parse(nr.Rows[0]["PersonalInfoStatus"].ToString()) : 0;
                    iChoice += (nr.Rows[0]["ChoiceStatus"] != null) ? int.Parse(nr.Rows[0]["ChoiceStatus"].ToString()) : 0;
                    iEdu += (nr.Rows[0]["EducationStatus"] != null) ? int.Parse(nr.Rows[0]["EducationStatus"].ToString()) : 0;
                    iPostEdu += (nr.Rows[0]["PostEducationStatus"] != null) ? int.Parse(nr.Rows[0]["PostEducationStatus"].ToString()) : 1;
                    iSubmit += (nr.Rows[0]["SubmitStatus"] != null) ? int.Parse(nr.Rows[0]["SubmitStatus"].ToString()) : 0;
                }
                catch
                {

                }
            }

            if (iPers == 0) iView = 0;
            else if (iChoice == 0) iView = 1;
            else if (iEdu == 0) iView = 2;
            else if (iPostEdu == 0) iView = 3;
            else if (iView == 0) iView = 4;
            else iView = 4;
            return iView;
        }


        // not in use:
        public static DataSet GetEntranceSubjects(string subjs)
        {
            string mykey = "GetEntranceSubjects_subj_FetchAll_" + subjs;
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@entranceSubjects", subjs));
                return db.ExecuteDataSet("GetEntranceSubjects_FetchAll");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
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
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }

        public static DataSet GetState()
        {
            string mykey = "GetSate_States_FetchAll_";
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            try
            {
                //db.Parameters.Add(new SqlParameter("@state", state));
                return db.ExecuteDataSet("States_FetchAll");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }

        public static DataSet GetEntryMode()
        {
            return new SignOnBusiness().SelectQuery("Select [EntryMode], [Description] From [EntryMode]");
        }

        public static DataSet GetReferral()
        {
            return new SignOnBusiness().SelectQuery("Select [Srn],[Name], [Description] From [Referral]");
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


                db.Parameters.Add(new SqlParameter("@entranceExamsubj1", st.EntranceExamsubj1 ));

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
                db.Parameters.Add(new SqlParameter("@EntryMode", st.EntryMode));

                db.Parameters.Add(new SqlParameter("@Programme", st.Programme));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", st.ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@AdmittedLevel", st.AdmittedLevel));
                db.Parameters.Add(new SqlParameter("@PresentEmployment", (st.PresentEmployment == null) ? "" : st.PresentEmployment));
                db.Parameters.Add(new SqlParameter("@PositionHeld", (st.PositionHeld == null) ? "" : st.PositionHeld));
                db.Parameters.Add(new SqlParameter("@PreviousEmployment", (st.PreviousEmployment == null) ? "" : st.PreviousEmployment));
                db.Parameters.Add(new SqlParameter("@PresentHighestQualification", st.PresentHighestQualification));
                db.Parameters.Add(new SqlParameter("@SchoolAttended", st.SchoolAttended));
                db.Parameters.Add(new SqlParameter("@CourseofStudy1", st.CourseofStudy1));
                db.Parameters.Add(new SqlParameter("@CourseofStudy2", st.CourseofStudy2));
                db.Parameters.Add(new SqlParameter("@CourseofStudy3", st.CourseofStudy3));
                db.Parameters.Add(new SqlParameter("@PreviousCourseAttended", (st.PreviousCourseAttended == null) ? "" : st.PreviousCourseAttended));
                db.Parameters.Add(new SqlParameter("@PreviousGrade", (st.PreviousGrade == null) ? "" : st.PreviousGrade));
                db.Parameters.Add(new SqlParameter("@PreviousRegNo", (st.PreviousRegNo == null) ? "" : st.PreviousRegNo));
                db.Parameters.Add(new SqlParameter("@IsIndigene", st.IsIndigene));
                db.Parameters.Add(new SqlParameter("@Center", (st.Center == null) ? "" : st.Center));
                db.Parameters.Add(new SqlParameter("@UTMERegNo", st.UTMERegNumber));
                db.Parameters.Add(new SqlParameter("@UTMEScore", st.UTMEScore.ToString()));
                db.Parameters.Add(new SqlParameter("@UTMEFirstChoice", st.UTMEFirstChoice));
                db.Parameters.Add(new SqlParameter("@UTMESecondChoice", st.UTMESecondChoice));
                db.Parameters.Add(new SqlParameter("@UTMEFirstChoiceCourse", st.UTMEFirstChoiceCourse));
                db.Parameters.Add(new SqlParameter("@UTMESecondChoiceCourse", st.UTMESecondChoiceCourse));
                db.Parameters.Add(new SqlParameter("@SubmitStatus", st.SubmitStatus));

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

        public static bool UpdatePersonalRecord(Applicants ap)
        {
            //[BloodGroup]='" + ap.BloodGroup.ToUpper() + "',
            string strQuery = "";
            strQuery = "UPDATE APPLICANTS SET ";
            strQuery += "[Surname]='" + ap.Surname.ToUpper() + "',[OtherNames]='" + ap.OtherNames.ToUpper() + "',[MaidenName]='" + ap.MaidenName.ToUpper()
                + "',[Sex]='" + ap.Sex.ToUpper() + "',[DateOfBirth]='" + ap.DateOfBirth.ToUpper() + "',[MaritalStatus]='" + ap.MaritalStatus.ToUpper() + "',[Religion]='" + ap.Religion.ToUpper()
                + "',[Nationality]='" + ap.Nationality.ToUpper() + "',[State]='" + ap.State.ToUpper() + "',[LocalGovernmentArea]='" + ap.LocalGovernmentArea.ToUpper()
                + "',[HomeAddress]='" + ap.HomeAddress.ToUpper() + "',[PostalAddress]='" + ap.PostalAddress.ToUpper() + "',[Country]='" + ap.Country.ToUpper() + "',[Email]='" + ap.Email
                + "',[PhoneNumber]='" + ap.PhoneNumber.ToUpper() + "',[SponsorName]='" + ap.SponsorName.ToUpper() + "',[SponsorAddress]='" + ap.SponsorAddress.ToUpper()
                + "',[SponsorPhone]='" + ap.SponsorPhone.ToUpper() + "',[SponsorEmail]='" + ap.SponsorEmail + "',[SponsorRelationship]='" + ap.SponsorRelationship.ToUpper()
                + "',[Title]='" + ap.Title.ToUpper() + "',[BloodGroup]='',[IsIndigene]='0',[Disability]='" + ap.Disability.ToUpper()
                + "',[Sponsor]='" + ap.Sponsor.ToUpper() + "',[PersonalInfoStatus]=1 WHERE regno='" + ap.RegNo.ToUpper() + "' OR [FormNumber]='" + ap.FormNumber.ToUpper() + "'";

            strQuery += ";UPDATE ApplicantSignOn SET [Email]='" + ap.Email + "',[Phone]='" + ap.PhoneNumber.ToUpper() + "' WHERE [FormNumber]='" + ap.FormNumber.ToUpper() + "';";
            bool retTruVal = true, retFalseVal = false;
            try
            {
                new Utility().PerformQuery(strQuery);
            }
            catch
            {
                return retFalseVal;
            }
            return retTruVal;
        }

        public static bool UpdateProgramChoiceRecord(Applicants ap)
        {
            //string strQuery = "";
            //strQuery = "UPDATE APPLICANTS SET ";
            //strQuery += "[DepartmentID] = " + ap.FirstDepartmentID.ToString() + ",[CourseOfStudy1] = '" + ap.CourseofStudy1.ToUpper()
            //    + "',[CourseOfStudy2] = '" + ap.CourseofStudy2.ToUpper() + "',[FirstFacultyID] = " + ap.FirstFacultyID.ToString()
            //    + ",[SecondFacultyID] = " + ap.SecondFacultyID.ToString() + ",[FirstDepartmentID] = " + ap.FirstDepartmentID.ToString()
            //    + ",[SecondDepartmentID] = " + ap.SecondDepartmentID + ",[PreviousCourseAttended] = '" + ap.PreviousCourseAttended.ToUpper()

            //    + "',[EntranceExamsubj1] = '" + ap.EntranceExamsubj1.ToUpper()
                
            //    + "',[PreviousRegNo] = '" + ap.PreviousRegNo.ToUpper() + "',[PreviousAttendedFrom] = '" + ap.PreviousAttendedFrom.ToUpper()
            //    + "',[PreviousAttendedTo] = '" + ap.PreviousAttendedTo.ToUpper() + "',[Repeating] ='" + ap.Repeating.ToUpper()
            //    + "',[TeachingCenter] = '" + ap.TeachingCenter.ToUpper() + "',[ExaminationCenter] = '" + ap.ExaminationCenter.ToUpper()
            //    + "',[ChoiceStatus] = 1 WHERE regno='" + ap.RegNo.ToUpper() + "' OR FormNumber='" + ap.FormNumber.ToUpper() + "'";

            string StuEntry = "",StudProgramme="";
            if (((ap.CourseofStudy1).Substring(0, 2) == "ND")) { StuEntry = "O LEVEL"; StudProgramme = "ND"; }
            if (((ap.CourseofStudy1).Substring(0, 3) == "HND")) { StuEntry = "ND"; StudProgramme = "HND"; }
            if (((ap.CourseofStudy1).Substring(0, 11) == "CERTIFICATE")) { StuEntry = "O LEVEL"; StudProgramme = "CERTIFICATE"; }
            
            //if (((ap.CourseofStudy2).Substring(0, 2) == "ND")) { StuEntry = "O Level"; StudProgramme = "ND"; }
            //if (((ap.CourseofStudy2).Substring(0, 3) == "HND")) { StuEntry = "ND"; StudProgramme = "HND"; }
            //if (((ap.CourseofStudy2).Substring(0, 11) == "CERTIFICATE")) { StuEntry = "O LEVEL"; StudProgramme = "CERTIFICATE"; }

            //if (((ap.CourseofStudy3).Substring(0, 2) == "ND")) { StuEntry = "O Level"; StudProgramme = "ND"; }
            //if (((ap.CourseofStudy3).Substring(0, 3) == "HND")) { StuEntry = "ND"; StudProgramme = "HND"; }
            //if (((ap.CourseofStudy3).Substring(0, 11) == "CERTIFICATE")) { StuEntry = "O LEVEL"; StudProgramme = "CERTIFICATE"; }

            string strQuery = "";
            strQuery = "UPDATE APPLICANTS SET ";
            strQuery += "[DepartmentID] = " + ap.FirstDepartmentID.ToString() + ",[CourseOfStudy1] = '" + ap.CourseofStudy1.ToUpper()
                + "',[CourseOfStudy2] = '" + ap.CourseofStudy2.ToUpper() + "',[FirstFacultyID] = " + ap.FirstFacultyID.ToString()
                + ",[CourseOfStudy3] = '" + ap.CourseofStudy3.ToUpper() + "',[ThirdFacultyID] = " + ap.ThirdFacultyID.ToString() + ",[ThirdDepartmentID] = " + ap.ThirdDepartmentID
                + ",[SecondFacultyID] = " + ap.SecondFacultyID.ToString() + ",[FirstDepartmentID] = " + ap.FirstDepartmentID.ToString()
                + ",[SecondDepartmentID] = " + ap.SecondDepartmentID + ",[PreviousCourseAttended] = '" + ap.PreviousCourseAttended.ToUpper()

                + "',[EntranceExamsubj1] = '" + ap.EntranceExamsubj1.ToUpper()

                + "',[PreviousRegNo] = '" + ap.PreviousRegNo.ToUpper() + "',[PreviousAttendedFrom] = '" + ap.PreviousAttendedFrom.ToUpper()
                + "',[PreviousAttendedTo] = '" + ap.PreviousAttendedTo.ToUpper() + "',[Repeating] ='" + ap.Repeating.ToUpper()
                + "',[TeachingCenter] = '" + ap.TeachingCenter.ToUpper() + "',[ExaminationCenter] = '" + ap.ExaminationCenter.ToUpper()

                + "',[EntryMode] = '" + StuEntry.ToUpper() + "',[Programme] = '" + StudProgramme.ToUpper()

                + "',[ChoiceStatus] = 1 WHERE regno='" + ap.RegNo.ToUpper() + "' OR FormNumber='" + ap.FormNumber.ToUpper() + "'";

                 strQuery += " UPDATE APPLICANTSIGNON SET "+ "[ModeOfStudy] = '" + StuEntry.ToUpper() + "',[Programme] = '" + StudProgramme.ToUpper()
                 + "' WHERE FormNumber='" + ap.FormNumber.ToUpper()  + "'";
            bool retTruVal = true;//, retFalseVal = false;
            //try
            //{
                new Utility().PerformQuery(strQuery);
            //}
            //catch
            //{
            //    return retFalseVal;
            //}
            return retTruVal;
        }

        public static bool UpdateEducationRecord(Applicants ap, ApplicantEntryQualification eq, string Olevel)
        {
            bool retTruVal = true, retFalseVal = false;
            try
            {
                ApplicantEntryQualificationBusiness.SaveRecord(eq);
                new ApplicantsBusiness().UpdateOlevels(Olevel, eq.Sitting, eq.RegNo);
            }
            catch
            {
                return retFalseVal;
            }
            return retTruVal;
        }

        public static bool UpdatePostEducationRecord(ApplicantPostQualification eq)
        {
            bool retTruVal = true, retFalseVal = false;
            try
            {
                retTruVal = PostSecAppEntryQualificationBusiness.SavePostRecord(eq);
                if (retTruVal == true)
                {
                    //posteducationStatus
                    new ApplicantsBusiness().PerformUpdate("UPDATE Applicants SET posteducationStatus=1 WHERE FormNumber='" + eq.RegNo + "'");
                }
            }
            catch
            {
                return retFalseVal;
            }
            return retTruVal;
        }        

        public static bool UpdateAttestationRecord(Applicants ap)
        {
            string strQuery = "";
            strQuery = "UPDATE APPLICANTS SET ";
            strQuery += "[SubmitStatus]=1,[Referral]='" + ap.Referral + "' WHERE regno='" + ap.RegNo.ToUpper() + "' OR FormNumber='" + ap.FormNumber.ToUpper() + "'";

            bool retTruVal = true, retFalseVal = false;
            try
            {
                new Utility().PerformQuery(strQuery);
            }
            catch
            {
                return retFalseVal;
            }
            return retTruVal;
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

        public static bool isEmailExists(string email)
        {
            var session = ActiveSession();
            string mykey = "select srn from applicantsignon where email='" + email + "' And PresentSession='" + session  + "'";
            Utility db = new Utility();
            DataTable dr = new DataTable();
            try
            {
                dr = db.SelectQuery(mykey).Tables[0];
                if (dr.Rows.Count >0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            return false;

        }

        public static bool isPhoneExists(string phone)
        {
            string mykey = "select srn from applicantsignon where phone='" + phone + "'";
            Utility db = new Utility();
            DataTable dr= new DataTable();
            try
            {
                dr = db.SelectQuery(mykey).Tables[0];
                if (dr.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            return false;

        }

        public static bool isEducation(string courseName)
        {
            string mykey = "select facultyname from Courseofstudy where courseofstudyname='" + courseName + "'";
            //'education'
            Utility db = new Utility();
            DataSet dr;
            try
            {
                dr = db.SelectQuery(mykey);
                if (dr != null && dr.Tables[0] != null && dr.Tables[0].Rows.Count > 0)
                {
                    if (dr.Tables[0].Rows[0][0].ToString().Trim().ToLower() == "education")
                        return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                return false;
            }
            return false;
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

        public static DataSet getAddmissionChangeOfCourses(string pcode)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@searchText", pcode));
            DataSet ds;
            ds = db.ExecuteDataSet("getAddmissionChangeOfCourse");
            db.Dispose();
            return ds;
        }

        public static DataSet getAddmissionCourses(string pcode, string  mode)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@ProgrammeTag", pcode));
            db.AddParameter(new SqlParameter("@Mode", "PTI"));
            DataSet ds;
            ds = db.ExecuteDataSet("getAddmissionCoursesByModeFull");
            db.Dispose();
            return ds;
        }

        public static DataSet getTeachingSubject()
        {
            
            Utility db = new Utility();
            DataSet ds;
            ds = db.SelectQuery("select name from teachingsubject");
            return ds;
        }

        public DataSet GetAdmittedStudents(string Programme, string Course, string Mode)
        {
            DataSet ds = new DataSet();
            string mykey = "AdmittedStudents_getAdmittedStudents_" + Programme + Course + Mode;
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Programme) || string.IsNullOrEmpty(Course) || string.IsNullOrEmpty(Mode))
            {
                return ds;
            }

            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@Course", Course));
                db.Parameters.Add(new SqlParameter("@Mode", Mode));
                ds = db.ExecuteDataSet("getAdmittedStudents");
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }

        public DataSet GetAdmittedStudentsByFormNo(string Param)
        {
            DataSet ds = new DataSet();
            string mykey = "AdmittedStudents_getAdmittedStudents_" + Param;
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Param))
            {
                ds.Tables.Add();
                return ds;
            }

            try
            {
                db.Parameters.Add(new SqlParameter("@Param", Param));
                ds = db.ExecuteDataSet("getAdmittedStudentByForm");
            }
            catch (Exception ec)
            {
                ds.Tables.Clear();
                ds.Tables.Add();
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public static DataSet getAdmittedCoursesByProg(string Programme)
        {
            if (string.IsNullOrEmpty(Programme))
            {
                Programme = "";
            }
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@Programme", Programme));
            DataSet ds;
            ds = db.ExecuteDataSet("getAdmittedCoursesByProg");
            db.Dispose();
            return ds;
        }
        public DataSet GetEntranceRawScoresByFormNo(string Param)
        {
            DataSet ds = new DataSet();
            string mykey = "EntranceRawScores_getEntranceRawScores_" + Param;
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Param))
            {
                ds.Tables.Add();
                return ds;
            }

            try
            {
                db.Parameters.Add(new SqlParameter("@Param", Param));
                ds = db.ExecuteDataSet("GetEntranceRawScoresByFormNo");
            }
            catch (Exception ec)
            {
                ds.Tables.Clear();
                ds.Tables.Add();
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetAdmittedStudentsByBiodata(string Surname, string SecondName, string PhoneNumber, string Email)
        {
            DataSet ds = new DataSet();
            string mykey = "AdmittedStudents_getAdmittedStudentByBiodata_" + Surname + "_" + SecondName;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@surname", Surname));
                db.Parameters.Add(new SqlParameter("@secondname", SecondName));
                db.Parameters.Add(new SqlParameter("@phonenumber", PhoneNumber));
                db.Parameters.Add(new SqlParameter("@email", Email));
                ds = db.ExecuteDataSet("getAdmittedStudentByByBiodata");
            }
            catch (Exception ec)
            {
                ds.Tables.Add();
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }

        public static void UpdateProfile(string surname,string otherNames,string VolNo, string prog, int firstC, string state, string lga, string email, string smode, string formNumber)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@volSerial", VolNo));
            db.AddParameter(new SqlParameter("@firstC", firstC));
            db.AddParameter(new SqlParameter("@state", state));
            db.AddParameter(new SqlParameter("@lga", lga));
            db.AddParameter(new SqlParameter("@email", email));
            db.AddParameter(new SqlParameter("@Programme", prog));
            db.AddParameter(new SqlParameter("@ModeOfStudy", smode));
            db.AddParameter(new SqlParameter("@FormNumber", formNumber));
            db.AddParameter(new SqlParameter("@Surname", surname ));
            db.AddParameter(new SqlParameter("@OtherNames", otherNames ));

            db.ExecuteNonQuery("UpdateProfile");
            db.Dispose();
        }

        public static bool MarkAddmisionLeterHasPrint(string formnumber)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@FormNumber", formnumber));
            db.ExecuteNonQuery("ApplicationFormPrinted");
            db.Dispose();
            return true;
        }

        public static bool MarkNotificationLeterHasPrint(string formnumber)
        {
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@FormNumber", formnumber));
            db.ExecuteNonQuery("NotificationLetterPrinted");
            db.Dispose();
            return true;
        }

        public static bool UpdateAdmLetterPrinted(string RegNo)
        {
            string mykey = "ptiUni_Applicants_UpdateAdmLetterP" + RegNo;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                int i = db.ExecuteNonQuery("UpdateAdmissionLetterPrinted");
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

        public static string ActiveSession()
        {
            string sessionName = "";
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "ActiveApplicantSession_Session_FetchActive";
            DBAccess db = new DBAccess(mykey);
            try
            {

                
                DataSet ds = new DataSet("Session");
                //ds = db.ExecuteDataSet("Session_FetchActive");
                ds = db.ExecuteDataSet("getCurrentApplicationSession");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NowTable = ds.Tables[0];
                    nr = NowTable.Rows[0];
                    sessionName = nr["SessionName"].ToString();
                }
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();

            return sessionName;
        }

        public static bool SubmissionClosed(string programme, string sessionName)
        {
            bool isClosed = false;
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "SubmissionClosed_Submission_Closed";
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@programName", programme));
                db.Parameters.Add(new SqlParameter("@SessionName", sessionName));
                DataSet ds = new DataSet("ApplicationSales");
                ds = db.ExecuteDataSet("Submission_Closed");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NowTable = ds.Tables[0];
                    nr = NowTable.Rows[0];
                    isClosed = false;
                }
                else isClosed = true;
            }
            catch (Exception)
            {
                //log
                //throw;
                isClosed = true;
            }
            db.Dispose();
            return isClosed;
        }

        public static DataSet getApplicationProgramm()
        {
            string mykey = "getApplicationProgramm_ApplicantProgramme";
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            try
            {
                //db.Parameters.Add(new SqlParameter("@Session", sessionName));
                return db.ExecuteDataSet("ApplicantProgramme");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }

        public static DataSet getApplicationProgramm( string modeOfStudy)
        {
            string mykey = "getApplicationProgramm_ApplicantProgrammeByMode_" + modeOfStudy;
            DBAccess db = new DBAccess(mykey);

            DataSet ds = new DataSet();
            try
            {
                db.Parameters.Add(new SqlParameter("@Mode", modeOfStudy));
                return db.ExecuteDataSet("ApplicantProgrammeByMode");
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
            db.Dispose();
            return ds;
        }

        public static string getEntranceDate(string sessionName, string modeofStudy, string courseofStudy, string Programme)
        {
            object screeningDate=null;
            string formatedScreeningDate = "NOT AVAILABLE";
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "getEntranceDate_GetEntranceExamDate";
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@courseofStudy", courseofStudy));
                db.Parameters.Add(new SqlParameter("@modeofStudy", modeofStudy));
                db.Parameters.Add(new SqlParameter("@Session", sessionName));
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                DataSet ds = new DataSet("EntranceExamDate");
                ds = db.ExecuteDataSet("GetEntranceExamDate");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NowTable = ds.Tables[0];
                    nr = NowTable.Rows[0];
                    screeningDate = nr["ExamDate"];
                }
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();
            formatedScreeningDate =(screeningDate!=null)? string.Format("{0:dddd, MMMM d, yyyy}", screeningDate): formatedScreeningDate;
            return formatedScreeningDate;
        }

        public static string courseofStudyName(int courseId)
        {
            string courseName = "";
            DataTable NowTable = null;
            DataRow nr = null;
            string mykey = "courseofStudyName_GetCourseofStudy";
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@courseid", courseId));
                DataSet ds = new DataSet("GetCourseofStudy");
                ds = db.ExecuteDataSet("GetCourseofStudy");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    NowTable = ds.Tables[0];
                    nr = NowTable.Rows[0];
                    courseName = nr["CourseofStudyName"].ToString();
                }
            }
            catch (Exception)
            {
                //log
                //throw;
            }
            db.Dispose();

            return courseName;
        }

        public void UpdatePrintStatus(string formNo)
        {
            string Qry = "Update applicants set printstatus = 1, AdmissionStatus='PENDING',[CreatedDate]='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where formnumber='" + formNo + "' and printstatus=0";
            PerformUpdate(Qry);
        }

        public void UpdateAdmissionLetterStatus(string formNo)
        {
            string Qry = "Update applicants set AdmissionLetterPrintedStatus = 1 where regno='" + formNo + "' AND AdmissionLetterPrintedStatus =0";
            PerformUpdate(Qry);
            Qry = "Update [StudentSignOn] set [Status] ='1' where [RegNo]='" + formNo + "' AND [Status]=0";
            PerformUpdate(Qry);
        }

        public void UpdateOlevels(string Olevel1, string Olevel2, string RegNo)
        {
            string qry = "";
            qry = "update [Applicants] set [Olevel"+Olevel2 +"]='" + Olevel1 + "' where [FormNumber]= '" + RegNo + "'";
            PerformUpdate(qry);
        }

        private void PerformUpdate(string Qry)
        {
            {
                SqlConnection cnn = new SqlConnection(str);

                cnn.Open();

                SqlCommand cmd = null;
                SqlDataReader dr = null;
                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cnn.Dispose();
                cnn.Close();
            }
            //catch (Exception ex)
            //{
            //    //msg = ex.Message + "||" + ex.StackTrace;
            //    ////LogError(msg, "Payroll", "");
            //    //showmassage(msg);
            //}
        }
    }
}