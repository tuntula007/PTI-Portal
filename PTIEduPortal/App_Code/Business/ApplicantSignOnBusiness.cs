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
using System.Collections;

/// <summary>
/// Summary description for SignOnBusiness
/// </summary>
namespace CybSoft.EduPortal.Business
{

    public class ApplicantSignOnBusiness : BaseBusiness
    {
        private static string str = ConfigurationManager.AppSettings["ConnString"];

        public bool InsertSignOn(ApplicantSignOn  Ss, ref string FormNumber)
        {
            bool RetValue = false;
            string regNo = "";
            DBAccess db = new DBAccess();
            try
            {
                /*db.Parameters.Add(new SqlParameter("@UserName", Ss.UserName));
                db.Parameters.Add(new SqlParameter("@PassW", Ss.Password));
                db.Parameters.Add(new SqlParameter("@Email", Ss.Email));
                db.Parameters.Add(new SqlParameter("@Phone", Ss.Phone));
                db.Parameters.Add(new SqlParameter("@ApplicationPIN", Ss.ApplicationPIN));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", Ss.ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@Programme", Ss.Programme));
                db.Parameters.Add(new SqlParameter("@Surname", Ss.Surname));
                db.Parameters.Add(new SqlParameter("@OtherNames", Ss.OtherNames));
                db.Parameters.Add(new SqlParameter("@IsFastTrack", Ss.IsFastTrack));
                //db.Parameters.Add(new SqlParameter("@Pin", Ss.PIN));    */
                db.Parameters.Add(new SqlParameter("@UserName", Ss.UserName));
                db.Parameters.Add(new SqlParameter("@PassW", Ss.Password));
                db.Parameters.Add(new SqlParameter("@Email", Ss.Email));
                db.Parameters.Add(new SqlParameter("@Phone", Ss.Phone));
                db.Parameters.Add(new SqlParameter("@Surname", Ss.Surname));
                db.Parameters.Add(new SqlParameter("@OtherNames", Ss.OtherNames));
                db.Parameters.Add(new SqlParameter("@IsFastTrack", Ss.IsFastTrack));
                db.Parameters.Add(new SqlParameter("@Programme", Ss.Programme));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", Ss.ModeOfStudy));
                db.Parameters.Add(new SqlParameter("@ApplicationPIN", Ss.ApplicationPIN));
                //db.Parameters.Add(new SqlParameter("@Pin", Ss.PIN));
                int rett = db.ExecuteNonQuery("InsertApplicantSignOn");
                if (rett != -1)
                {
                    RetValue = true;
                    DataSet dtAp = new Utility().SelectQuery("Select FormNumber from applicantsignon where ApplicationPin='" + Ss.ApplicationPIN + "'");
                    DataTable dtAppNo = (dtAp != null) ? dtAp.Tables[0] : new DataTable();
                    regNo = (dtAppNo == null && dtAppNo.Rows.Count < 1) ? "" : dtAppNo.Rows[0][0].ToString();
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                throw;
                //string ss = ex.Message;
            }
            db.Dispose();
            FormNumber = regNo;
            return RetValue;
        }
        public bool InsertDocGuid(string RegNo, string Desc, string DocType, string DocGuid, string Pin)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@Desc", Desc));
                db.Parameters.Add(new SqlParameter("@DocType", DocType));
                db.Parameters.Add(new SqlParameter("@DocGuid", DocGuid));
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                int rett = db.ExecuteNonQuery("InsertDocGuid");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public bool UpdateEmailVerify(string formNumber, string email)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@formNumber", formNumber));
                db.Parameters.Add(new SqlParameter("@email", email));
                int rett = db.ExecuteNonQuery("UpdateEmailVerify");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public bool UpdateApplicationFeePin(string userName, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@UserName", userName));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                db.Parameters.Add(new SqlParameter("@Session", getCurrentApplicationSession()));
                int rett = db.ExecuteNonQuery("UpdateApplicationFormFeePin");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public bool UpdateSignOn(string MatricNo, string SkuFeePin, string StudioFeePin)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@SchoolFeePin", SkuFeePin));
                db.Parameters.Add(new SqlParameter("@StudioFeePin", StudioFeePin));
                int rett = db.ExecuteNonQuery("UpdateSignOn");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }

        public bool UpdateAdmissionLetterFeePin(string RegNo, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdateAdmissionLetterFeePin");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public ApplicantSignOn  SignOnExist(string userName)
        {
            ApplicantSignOn  RetValue = new ApplicantSignOn();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@UserName", userName ));
                //db.Parameters.Add(new SqlParameter("@MatNo", Ss.MatricNumber));
                DataSet ds = db.ExecuteDataSet("ApplicantSignOnExist");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RetValue.UserName = ds.Tables[0].Rows[0]["MatricNumber"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    RetValue.ApplicationPIN = ds.Tables[0].Rows[0]["ApplicationPIN"].ToString();
                    RetValue.ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString(); ;
                    RetValue.Programme = ds.Tables[0].Rows[0]["Programme"].ToString(); ;
                    RetValue.Surname = ds.Tables[0].Rows[0]["Surname"].ToString(); ;
                    RetValue.OtherNames = ds.Tables[0].Rows[0]["OtherNames"].ToString(); ;
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    RetValue.EmailVerificationCode = ds.Tables[0].Rows[0]["EmailVerificationCode"].ToString();
                    RetValue.VerifyFlag = int.Parse(ds.Tables[0].Rows[0]["VerifyFlag"].ToString()); 
                }
                else
                {
                    RetValue.UserName = "";
                    RetValue.Password = "";
                    RetValue.ApplicationPIN = "";
                    RetValue.Email = "";
                    RetValue.Phone = "";
                    RetValue.ModeOfStudy = "";
                    RetValue.Programme = "";
                    RetValue.Surname = "";
                    RetValue.OtherNames = "";
                    RetValue.VerifyFlag = 0;
                    RetValue.EmailVerificationCode = ""; 

                }
            }
            catch (Exception ex)
            {
                string rt = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public ApplicantSignOn VerifySignOn(string userName, string PassWord)
        {
            ApplicantSignOn RetValue = new ApplicantSignOn();
            string mykey = "VerifyApplicantSignOn_VerifySignOn_" + userName + PassWord;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@UserName", userName ));
                db.Parameters.Add(new SqlParameter("@PassWord", PassWord));
                DataSet ds = db.ExecuteDataSet("VerifyApplicantSignOn");
                if (ds.Tables[0].Rows.Count > 0 && string.Compare(PassWord, ds.Tables[0].Rows[0]["Password"].ToString(), false) == 0)
                {
                    RetValue.FormNumber = ds.Tables[0].Rows[0]["FormNumber"].ToString();
                    RetValue.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    RetValue.ApplicationPIN = ds.Tables[0].Rows[0]["ApplicationPIN"].ToString();
                    RetValue.ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString(); 
                    RetValue.Programme = ds.Tables[0].Rows[0]["Programme"].ToString(); 
                    RetValue.Surname = ds.Tables[0].Rows[0]["Surname"].ToString(); 
                    RetValue.OtherNames = ds.Tables[0].Rows[0]["OtherNames"].ToString(); 
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    RetValue.EmailVerificationCode = ds.Tables[0].Rows[0]["EmailVerificationCode"].ToString();
                    RetValue.VerifyFlag = int.Parse(ds.Tables[0].Rows[0]["VerifyFlag"].ToString()); 

                    RetValue.Verified = true;
                }
                else
                {
                    RetValue.FormNumber = ds.Tables[0].Rows[0]["FormNumber"].ToString();
                    RetValue.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    RetValue.ApplicationPIN = ds.Tables[0].Rows[0]["ApplicationPIN"].ToString();
                    RetValue.ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString(); 
                    RetValue.Programme = ds.Tables[0].Rows[0]["Programme"].ToString(); 
                    RetValue.Surname = ds.Tables[0].Rows[0]["Surname"].ToString(); 
                    RetValue.OtherNames = ds.Tables[0].Rows[0]["OtherNames"].ToString(); 
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    RetValue.EmailVerificationCode = ds.Tables[0].Rows[0]["EmailVerificationCode"].ToString();
                    RetValue.VerifyFlag = int.Parse(ds.Tables[0].Rows[0]["VerifyFlag"].ToString()); 

                    RetValue.Verified = false;
                }
            }
            catch (Exception ec)
            {

                string ffg = ec.Message;
                RetValue.Verified = false;
            }
            db.Dispose();
            return RetValue;
        }

        public PinVerifyResult VerifyApplicationFeePin(string Pin, string _UserName, string _Programme, string _ModeOfStudy)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifyApplicationFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    
                    string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                    string ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != _UserName)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Pin Already Used by Applicant with user name " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {

                        //if (Programme != _Programme)
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Programme)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Programme)";
                        //    return RetValue;
                        //}

                        //if (ModeOfStudy.ToLower() != _ModeOfStudy)
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Academic Level)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Mode of Study)";
                        //    return RetValue;
                        //}
                        RetValue.Verified = true;

                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Application Fee PIN Does Not Exist";
                }
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }

        public PinVerifyResult VerifyApplicationFeePin(string Pin, Applicants app)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifyApplicationFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string IsIndigene = ds.Tables[0].Rows[0]["IsIndigene"].ToString();
                    string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                    string ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != app.UserName)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Pin Already Used by Student with Matric no " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {
                        
                        if (Programme != app.Programme)
                        {
                            //Pin Exists But Not Valid For the Student (Based on Programme)
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Programme)";
                            return RetValue;
                        }
                        if (ModeOfStudy != app.ModeOfStudy)
                        {
                            //Pin Exists But Not Valid For the Student (Based on Academic Level)
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Mode of Study)";
                            return RetValue;
                        }
                        RetValue.Verified = true;

                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Application Fee PIN Does Not Exist";
                }
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
 
        public PinVerifyResult VerifyAdmissionLetterRegNo(string RegNo)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                DataSet ds = db.ExecuteDataSet("VerifyAdmissionLetterRegNo");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RetValue.Verified = true;
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Registration Number Does Not Exist";
                }
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public DataSet GetAdmissionLetterByRegNo(string RegNo)
        {
            string mykey = "AdmissionLetter_getAdmissionLetterByRegNo_" + RegNo;
            DataSet ds = new DataSet();
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                ds = db.ExecuteDataSet("getAdmissionLetterByRegNo");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetDistinctProgramme()
        {
            DataSet ds = new DataSet();
            string mykey = "GetDistinctProgramme_getApplicantProgramme";
            DBAccess db = new DBAccess(mykey);
            try
            {
                ds = db.ExecuteDataSet("getApplicantProgramme");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetDistinctCourses(string Programme)
        {
            if (string.IsNullOrEmpty(Programme))
            {
                Programme = " ";
            }
            DataSet ds = new DataSet();
            string mykey = "GetDistinctCourses_getApplicantCourses_" + Programme;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@Prog", Programme));
                ds = db.ExecuteDataSet("getApplicantCourses");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetCourseOfStudy(string Programme, string Mode)
        {
            DataSet ds = new DataSet();
            string mykey = "CourseOfStudy_getCoursesOfStudy_" + Programme + Mode;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@Mode", Mode));
                ds = db.ExecuteDataSet("getCoursesOfStudy");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetDistinctMode()
        {
            DataSet ds = new DataSet();
            string mykey = "GetDistinctMode_getModeOfStudy";
            DBAccess db = new DBAccess(mykey);
            try
            {
                ds = db.ExecuteDataSet("getModeOfStudy");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetDistinctLevel()
        {
            DataSet ds = new DataSet();
            string mykey = "GetDistinctLevel_getLevel";
            DBAccess db = new DBAccess(mykey);
            try
            {
                ds = db.ExecuteDataSet("getLevel");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
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
        public static ArrayList getPasswordByUserName(string userName)
        {
            string mykey = "PasswordByFormNoUserName_getPasswordByFormNoUserName_" + userName;
            ArrayList result = new ArrayList();
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@UserName", userName ));
            //db.AddParameter(new SqlParameter("@Programme", userName));
            //db.AddParameter(new SqlParameter("@ModeOfStudy", userName));
            SqlDataReader dr;

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (ArrayList)BizObject.Cache[mykey];
            }
            else
            {


                string email = "";
                try
                {
                    dr = (SqlDataReader)db.ExecuteReader("getPasswordByFormNoUserName");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result.Add(dr.GetString(dr.GetOrdinal("Email")));
                        result.Add(dr.GetString(dr.GetOrdinal("Password")));
                        BaseBusiness.CacheData(mykey, result);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            db.Dispose();
            return result;
        }
        public string getCurrentApplicationSession()
        {
            string result = "";
            string mykey = "getCurrentApplicationSession";
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (string)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    dr = (SqlDataReader)db.ExecuteReader("getCurrentApplicationSession");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetString(dr.GetOrdinal("SessionName"));
                        BaseBusiness.CacheData(mykey, result);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            db.Dispose();
            return result;
        }
        public string getCurrentAdmissionSession()
        {
            string result = "";
            string mykey = "getCurrentAdmissionSession";
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (string)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    dr = (SqlDataReader)db.ExecuteReader("getCurrentAdmissionSession");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetString(dr.GetOrdinal("SessionName"));
                        BaseBusiness.CacheData(mykey, result);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            db.Dispose();
            return result;
        }
        public string getCurrentSession()
        {
            string result = "";
            string mykey = "getCurrentSession";
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (string)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    dr = (SqlDataReader)db.ExecuteReader("getCurrentSession");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetString(dr.GetOrdinal("SessionName"));
                        BaseBusiness.CacheData(mykey, result);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            db.Dispose();
            return result;
        }
        public bool IsApplicationSalesAvailable(string session,string Programme, string ModeOfStudy, string currentdate)
        {
            string qry = "SELECT * FROM [ApplicationSales] where [Session] = '"
                + session.Trim() + "' and [Programme] = '" + Programme.Trim() + "' and ModeOfStudy = '"
                + ModeOfStudy.Trim() + "' and Cast('" + currentdate + "' as datetime) BETWEEN [StartDate] AND [Enddate]";

            bool ret = false;

            try
            {
                SqlConnection cnn = new SqlConnection(str);

                cnn.Open();

                SqlCommand cmd = null;
                SqlDataReader dr = null;

                cmd = new SqlCommand(qry, cnn);

                dr = cmd.ExecuteReader();//
                if (dr.HasRows)
                {
                    ret = true;
                }
                dr.Dispose();
                cmd.Dispose();
                cnn.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                //msg = ex.Message + "||" + ex.StackTrace;
                ////LogError(msg, "Payroll", "");
                //showmassage(msg);
            }

            return ret;
        }
        public DataSet getAcademicSessions()
        {
            DataSet result = new DataSet();
            string mykey = "getAcademicSessions";
            DBAccess db = new DBAccess();
            SqlDataReader dr;
            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                result = (DataSet)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    result = db.ExecuteDataSet("getAcademicSessions");
                    BaseBusiness.CacheData(mykey, result);
                }
                catch (Exception ex)
                {

                }
            }
            db.Dispose();
            return result;
        }
    }

}
