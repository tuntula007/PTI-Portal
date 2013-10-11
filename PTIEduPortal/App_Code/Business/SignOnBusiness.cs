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
using System.IO;

/// <summary>
/// Summary description for SignOnBusiness
/// </summary>
namespace CybSoft.EduPortal.Business 
{

    public class SignOnBusiness : BaseBusiness 
    {
        private static string strConn = ConfigurationManager.AppSettings["ConnString"];

        public bool InsertSignOn(StudentSignOn Ss)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", Ss.MatricNumber));
                db.Parameters.Add(new SqlParameter("@PassW", Ss.Password));
                db.Parameters.Add(new SqlParameter("@Email", Ss.Email));
                db.Parameters.Add(new SqlParameter("@Phone", Ss.Phone));
                //db.Parameters.Add(new SqlParameter("@Pin", Ss.PIN));
                int rett = db.ExecuteNonQuery("InsertSignOn");
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
        public bool UpdateSignOn(StudentSignOn Ss)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", Ss.MatricNumber));
                db.Parameters.Add(new SqlParameter("@PassW", Ss.Password));
                db.Parameters.Add(new SqlParameter("@Email", Ss.Email));
                db.Parameters.Add(new SqlParameter("@Phone", Ss.Phone));
                //db.Parameters.Add(new SqlParameter("@Pin", Ss.PIN));
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
        public bool InsertSignOnFresh(StudentSignOn Ss)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", Ss.MatricNumber));
                db.Parameters.Add(new SqlParameter("@RegNo", Ss.FormNumber));
                db.Parameters.Add(new SqlParameter("@PassW", Ss.Password));
                db.Parameters.Add(new SqlParameter("@Email", Ss.Email));
                db.Parameters.Add(new SqlParameter("@Phone", Ss.Phone));
                //db.Parameters.Add(new SqlParameter("@Pin", Ss.PIN));
                int rett = db.ExecuteNonQuery("InsertSignOnFresh");
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

        public string GenerateMatNo(string regNo)
        {
            string result = "";
            string mykey = "GenerateMatric_" + regNo;
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
                    db.Parameters.Add(new SqlParameter("@RegNo", regNo));
                    dr = (SqlDataReader)db.ExecuteReader("GenerateMatric");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetString(dr.GetOrdinal("FormNumber"));
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
        public bool UpdateSchoolFeePin(string MatricNo, string UsedDate, string PinNumber, string payType, string amount, string pinserial, string session)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                session = (session.Trim().Length < 1) ? new SignOnBusiness().getCurrentSession() : session;
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                db.Parameters.Add(new SqlParameter("@Session", session));
                int rett = db.ExecuteNonQuery("UpdateSchoolFeePin");
                if (rett != -1)
                {
                    RetValue = true;
                    //Insert to StudentPayments
                    string qry = "IF NOT EXISTS (SELECT MatricNumber FROM StudentPayment WHERE MatricNumber='"+MatricNo +
                        "') BEGIN INSERT INTO [StudentPayment] ([MatricNumber],[Pin],[PaymentType],[PinValue],[PinSerialNo],[DatePaid],[Session]) VALUES ('"
                        + MatricNo + "','" + PinNumber + "','" + payType + "','" + amount + "','" + pinserial + "','" + UsedDate + "','" + session + "') END";
                    PerformQuery(qry);
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

        public bool UpdatePartTimeFeePin(string MatricNo, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdatePartTimeFeePin");
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
        public bool UpdateEveningFeePin(string MatricNo, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdateEveningFeePin");
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
        public bool UpdateStudioFeePin(string MatricNo, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdateStudioFeePin");
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
        public bool UpdateAdmissionLetterFeePin(string RegNo, string UsedDate, string PinNumber, string SessionName)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                db.Parameters.Add(new SqlParameter("@SessionName", SessionName));
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
        public StudentSignOn SignOnExist(string MatricNo)
        {
            StudentSignOn RetValue = new StudentSignOn();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                //db.Parameters.Add(new SqlParameter("@MatNo", Ss.MatricNumber));
                DataSet ds = db.ExecuteDataSet("SignOnExist");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RetValue.MatricNumber =  ds.Tables[0].Rows[0]["MatricNumber"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    //RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["SchoolFeePin"].ToString();
                    //RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["StudioFeePin"].ToString();
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                else
                {
                    RetValue.MatricNumber = "";
                    RetValue.Password = "";
                    RetValue.Email = "";
                    RetValue.Phone = "";
                }
                ds.Dispose();
           }
            catch (Exception ex)
            {
                string rt = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public StudentSignOn VerifySignOn(string MatricNo, string PassWord)
        {
            StudentSignOn RetValue = new StudentSignOn();
            string mykey = "VerifySignOn_VerifySignOn_" + MatricNo + PassWord;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@PassWord", PassWord));
                DataSet ds = db.ExecuteDataSet("VerifySignOn");
                if (ds.Tables[0].Rows.Count > 0 && string.Compare(PassWord, ds.Tables[0].Rows[0]["Password"].ToString(), false) == 0)
                {
                    RetValue.MatricNumber = ds.Tables[0].Rows[0]["MatricNumber"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    //RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["SchoolFeePin"].ToString();
                    RetValue.FormNumber = ds.Tables[0].Rows[0]["RegNo"].ToString();
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Status = (ds.Tables[0].Rows[0]["Status"] == null) ? 0 : (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Status"].ToString()) == false) ? int.Parse(ds.Tables[0].Rows[0]["Status"].ToString()) : 0;
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    RetValue.Verified = true;
                }
                else
                {
                    RetValue.MatricNumber = "";
                    RetValue.Password = "";
                    //RetValue.SchoolFeePin = "";
                    //RetValue.StudioFeePin = "";
                    RetValue.Email = "";
                    RetValue.Phone = "";
                    RetValue.Verified = false;
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {

                string ffg = ec.Message;
                RetValue.Verified = false;
                
            }
            db.Dispose();
            return RetValue;
        }
        public StudentSignOn VerifySignOn(string MatricNo)
        {
            StudentSignOn RetValue = new StudentSignOn();
            RetValue.FormNumber = MatricNo;
            string mykey = "VerifySignOn_VerifySignUo_" + MatricNo;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                DataSet ds = db.ExecuteDataSet("VerifySignUp");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RetValue.MatricNumber = ds.Tables[0].Rows[0]["MatricNumber"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    //RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["SchoolFeePin"].ToString();
                    //RetValue.StudioFeePin = ds.Tables[0].Rows[0]["StudioFeePin"].ToString();
                    RetValue.FormNumber = ds.Tables[0].Rows[0]["RegNo"].ToString();
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Status = (ds.Tables[0].Rows[0]["Status"] == null) ? 0 : (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Status"].ToString()) == false) ? int.Parse(ds.Tables[0].Rows[0]["Status"].ToString()) : 0;
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    RetValue.Verified = true;
                }
                else
                {
                    RetValue.MatricNumber = "";
                    RetValue.Password = "";
                    //RetValue.SchoolFeePin = "";
                    //RetValue.StudioFeePin = "";
                    RetValue.FormNumber = MatricNo;
                    RetValue.Email = "";
                    RetValue.Phone = "";
                    RetValue.Verified = false;
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {

                string ffg = ec.Message;
                RetValue.Verified = false;

            }
            db.Dispose();
            return RetValue;
        }
        public bool VerifyCanRegister(string MatricNo)
        {

            string mykey = "CanRegister_VerifyCanRegister_" + MatricNo;
            DBAccess db = new DBAccess();
            bool retval = false;
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));

                DataSet ds = db.ExecuteDataSet("VerifyCanRegister");
                if (ds.Tables[0].Rows.Count > 0)
                {
                   
                    retval  = true;
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
            return retval ;
        }
        public bool VerifyCanRegister2(string MatricNo)
        {

            string mykey = "CanRegister_VerifyCanRegister2_" + MatricNo;
            DBAccess db = new DBAccess();
            bool retval = false;
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));

                DataSet ds = db.ExecuteDataSet("VerifyCanRegister2");
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
        public PinVerifyResult VerifySchoolFeePin(string Pin, string PayType, Students Stud)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifySchoolFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string Faculty = (ds.Tables[0].Rows[0]["Faculty"] == null) ? "" : ds.Tables[0].Rows[0]["Faculty"].ToString();
                    string Programme = (ds.Tables[0].Rows[0]["Programme"] == null) ? "" : ds.Tables[0].Rows[0]["Programme"].ToString();
                    string AcademicLevel = (ds.Tables[0].Rows[0]["AcademicLevel"] == null) ? "" : ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string ModeOfStudy = (ds.Tables[0].Rows[0]["ModeOfStudy"] == null) ? "" : ds.Tables[0].Rows[0]["ModeOfStudy"].ToString();
                    //string PaymentType = ds.Tables[0].Rows[0]["PaymentType"].ToString();
                    //PaymentType = (PaymentType.ToLower() == "full payment") ? PaymentType : PaymentType;// "Part Payment";
                    string UsedBy = (ds.Tables[0].Rows[0]["UsedBy"] == null) ? "" : ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amount = (ds.Tables[0].Rows[0]["Amount"] == null) ? "0" : ds.Tables[0].Rows[0]["Amount"].ToString();
                    string pinNumber = ds.Tables[0].Rows[0]["PinNumber"].ToString();
                    string pinSerial = ds.Tables[0].Rows[0]["PinSerialNumber"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != Stud.MatricNumber)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Transaction Reference Already Used by Student with Matric no " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = false;
                            //RetValue.PaymentType = PaymentType;
                            RetValue.FailureComment = "Transaction Reference already used by you";
                        }
                        return RetValue;
                    }
                    if (PinStatus == "0")
                    {
                        #region OldCondition (Can be used in future, not needed now since we are using ref number/if it will be used be sure the fields are populated)
                        //if (Faculty.ToLower() != Stud.Faculty.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Indigeneship)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on College)";
                        //    return RetValue;
                        //}

                        ////if (Stud.AcademicLevel.ToLower() != AcademicLevel.ToLower())
                        ////{
                        ////    RetValue.Verified = false;
                        ////    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on your Academic Level)";
                        ////    return RetValue;
                        ////}
                        //if (Programme.ToLower() != Stud.Programme.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Programme)

                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Programme)";
                        //    return RetValue;
                        //}
                        //if (ModeOfStudy.ToLower() != Stud.ModeOfStudy.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Academic Level)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Mode Of Study)";
                        //    return RetValue;
                        //}
                        //if (PaymentType.ToLower() != PayType.ToLower())
                        //{
                        //    //Check if current pay for full/part(1 or 2) payment
                        //    //Use Current Session, Current Semester 
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with payment type selected by you";
                        //    return RetValue;
                        //}
                        #endregion

                        if (!string.IsNullOrEmpty(UsedBy) && UsedBy.ToLower() != Stud.RegNo.ToLower())// and not blank(free)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Transaction Reference does not belong to you. If you think this is not correct, verify from bank of puchase";
                            return RetValue;
                        }

                        if (string.IsNullOrEmpty(UsedBy)) { string free = "PIN free to be used..."; }

                        
                        string feesPayable = getStudentPayableFees(Stud.MatricNumber, PayType);

                        if(Stud.RegNo.Contains( "PRE"))
                        {
                            feesPayable = "22250";
                        }
                        if (feesPayable == "" || amount.Equals(feesPayable) == false)
                        {
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Transaction Reference does not Correspond with the amount Expected";
                            return RetValue;
                        }
                        RetValue.Verified = true;
                        //RetValue.PaymentType = PaymentType;
                        RetValue.Pin = pinNumber;
                        RetValue.PinSerial = pinSerial;
                        RetValue.PinValue = amount;
                        
                        //// 05/09/2012
                        ////////////if (UsedBy.ToLower() != Stud.RegNo.ToLower())
                        ////////////{
                        ////////////    //Pin Already used by somebody else
                        ////////////    RetValue.Verified = false;
                        ////////////    RetValue.FailureComment = "Transaction Reference does not belong to you. If you think this is not correct, verify from bank of puchase";
                        ////////////    return RetValue;
                        ////////////}

                        ////////////string feesPayable = getStudentPayableFees(Stud.MatricNumber, PayType);
                        ////////////if (feesPayable == "" || amount.Equals(feesPayable) == false)
                        ////////////{
                        ////////////    RetValue.Verified = false;
                        ////////////    RetValue.FailureComment = "The Transaction Reference does not Correspond with the amount Expected";
                        ////////////    return RetValue;
                        ////////////}
                        ////////////RetValue.Verified = true;
                        //////////////RetValue.PaymentType = PaymentType;
                        ////////////RetValue.Pin = pinNumber;
                        ////////////RetValue.PinSerial = pinSerial;
                        ////////////RetValue.PinValue = amount;
                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "School Fee Transaction Reference Does Not Exist! Check if you entered it correctly.";
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }


        public static bool   VerifyAdmissionLetterFeeIsPaid(string RegNo)
        {
            bool hasPaid = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                DataSet ds = db.ExecuteDataSet("VerifyAdmissionLetterFeeIsPaid");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if ((PinStatus == "1") && (UsedBy == RegNo)) 
                    {
                        //RegNo has Already paid admission fee before
                        hasPaid = true;
                           
                    }
                    if (PinStatus == "0")
                    {
                        hasPaid = false;
                    }
                }
                else
                {
                    //RegNo has not paid before
                    hasPaid = false;
                 
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return hasPaid;
        }




        public PinVerifyResult VerifyAdmissionLetterFeePin(string Pin, string RegNo, string ModeofStudy)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                db.Parameters.Add(new SqlParameter("@ModeofStudy", ModeofStudy));
                DataSet ds = db.ExecuteDataSet("VerifyAdmissionLetterFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if (PinStatus == "1")
                    {
                        if (string.Equals(UsedBy.ToUpper(), RegNo.ToUpper())==false)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Admission Letter Fee Pin Already Used by Applicant with RegNo " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {
                        RetValue.Verified = true;
                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Admission Letter Fee PIN Does Not Exist";
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public PinVerifyResult VerifyNewSchoolFeePin(string Pin, string StudRegNo)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifySchoolFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string Faculty = (ds.Tables[0].Rows[0]["Faculty"]==null)?"":ds.Tables[0].Rows[0]["Faculty"].ToString();
                    string Programme = (ds.Tables[0].Rows[0]["Programme"] == null) ? "" : ds.Tables[0].Rows[0]["Programme"].ToString();
                    string AcademicLevel = (ds.Tables[0].Rows[0]["AcademicLevel"] == null) ? "" : ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string ModeOfStudy = (ds.Tables[0].Rows[0]["ModeOfStudy"] == null) ? "" : ds.Tables[0].Rows[0]["ModeOfStudy"].ToString();
                    //string PaymentType = ds.Tables[0].Rows[0]["PaymentType"].ToString();
                    //PaymentType = (PaymentType.ToLower() == "full payment") ? PaymentType : PaymentType;// "Part Payment";
                    string UsedBy = (ds.Tables[0].Rows[0]["UsedBy"] == null) ? "" : ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amount = (ds.Tables[0].Rows[0]["Amount"] == null) ? "0" : ds.Tables[0].Rows[0]["Amount"].ToString();
                    string pinNumber = ds.Tables[0].Rows[0]["PinNumber"].ToString();
                    string pinSerial = ds.Tables[0].Rows[0]["PinSerialNumber"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != StudRegNo)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Confirmation Order Number already Used by Student with Form no/Matric no " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = false;
                            //RetValue.PaymentType = PaymentType;
                            RetValue.FailureComment = "Confirmation Order Number already used by you";
                        }
                        return RetValue;
                    }
                    if (PinStatus == "0")
                    {
                        #region OldCondition (Can be used in future, not needed now since we are using ref number/if it will be used be sure the fields are populated)                        //if (Faculty.ToLower() != Stud.Faculty.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Indigeneship)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on College)";
                        //    return RetValue;
                        //}

                        //if (Stud.AcademicLevel.ToLower() != AcademicLevel.ToLower())
                        //{
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on your Academic Level)";
                        //    return RetValue;
                        //}
                        //if (Programme.ToLower() != Stud.Programme.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Programme)

                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Programme)";
                        //    return RetValue;
                        //}
                        //if (ModeOfStudy.ToLower() != Stud.ModeOfStudy.ToLower())
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Academic Level)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Mode Of Study)";
                        //    return RetValue;
                        //}
                        //if (PaymentType.ToLower() != PayType.ToLower())
                        //{
                        //    //Check if current pay for full/part(1 or 2) payment
                        //    //Use Current Session, Current Semester 
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with payment type selected by you";
                        //    return RetValue;
                        //}
                        #endregion
                        //if (UsedBy.ToLower() != StudRegNo.ToLower())
                        //{
                        //    //Pin Already used by somebody else
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "Transaction Reference does not belong to you. If you think this is not correct, verify from bank of puchase";
                        //    return RetValue;
                        //}

                        string feesPayable = getNewStudentPayableFees(StudRegNo);
                              
                        if (feesPayable == "" || new Utility().DoubleStrings(amount).Equals(new Utility().DoubleStrings(feesPayable)) == false)
                        {
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Confirmation Order Number does not CORRESPONDS to the required amount Expected. You are expected to pay " + feesPayable;
                            return RetValue;
                        }
                        RetValue.Verified = true;
                        //RetValue.PaymentType = PaymentType;
                        RetValue.Pin = pinNumber;
                        RetValue.PinSerial = pinSerial;
                        RetValue.PinValue = amount;
                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Confirmation Order Number Does Not Exist! Check if you entered it correctly.";
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public string getNewStudentPayableFees(string matno)
        {
            string retVal = ""; string fees = "";
            StudentsBusiness sb = new StudentsBusiness();
            StudentPayment sp = new StudentPayment();
            StudentPayment sp2 = new StudentPayment();
            //Students st = sb.GetStudentsByMatNo(matno);
            sp = sb.GetStudentPayments(matno, getCurrentSession());
            if (sp == null)
            {
                //No Fees Paid So Far, Get Minimum Fees, this indicate First Semester
                fees = getNewStudentPayableFees(getCurrentSession(), matno);
                //Get Payable based on payType from FEES PIN
                retVal = fees;
            }
            else
            {
                //At least student have paid once, check if it was full or part
            }

            return retVal;
        }
        public string getNewStudentPayableFees(string session, string matno)
        {
            string retVal = "";
            string mykey = "NewStudentPayableFees_getFeesPayable_" + session + matno;
            DBAccess db = new DBAccess(mykey);
            SqlDataReader ds;

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                retVal = (string)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    db.Parameters.Add(new SqlParameter("@regno", matno));
                    db.Parameters.Add(new SqlParameter("@sessionname", session));

                    //(SqlDataReader)db.ExecuteReader("getCurrentSession");
                    ds = (SqlDataReader)db.ExecuteReader("getNewStudentFeesPayable");

                    if (ds.HasRows)
                    {
                        ds.Read();
                        retVal = ds.GetDecimal(ds.GetOrdinal("Amount")).ToString();
                        BaseBusiness.CacheData(mykey, retVal);
                    }

                }
                catch (Exception ec)
                {
                    string ffg = ec.Message;
                }
            }
            db.Dispose();

            return retVal;
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




        public PinVerifyResult VerifyAdmissionLetterRegNo(string RegNo, string ModeofStudy)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@ModeOfStudy", ModeofStudy));
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
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public DataSet GetAdmissionLetterByRegNo(string RegNo, string ModeofStudy, string Programme)
        {
            string mykey = "AdmissionLetter_getAdmissionLetterByRegNo_" + RegNo;
            DataSet ds = new DataSet();
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@ModeofStudy", ModeofStudy));
                db.Parameters.Add(new SqlParameter("@Programme", Programme)); 
                ds = db.ExecuteDataSet("getAdmissionLetterByRegNo");
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public DataSet GetNotificationLetterByRegNo(string RegNo)
        {
            string mykey = "AdmissionLetter_getNotificationLetterByRegNo_" + RegNo;
            DataSet ds = new DataSet();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                //db.Parameters.Add(new SqlParameter("@ModeofStudy", ModeofStudy));
                ds = db.ExecuteDataSet("getNotificationLetterByRegNo");

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
        public DataSet GetStudentsCourses(string Programme, string ModeOfStudy)
        {
            if (string.IsNullOrEmpty(Programme))
            {
                Programme = " ";
            }
            DataSet ds = new DataSet();
            string mykey = "GetStudentsCourses_getStudentsCourses_" + Programme + ModeOfStudy;
            DBAccess db = new DBAccess(mykey);

            try
            {
                db.Parameters.Add(new SqlParameter("@Prog", Programme));
                db.Parameters.Add(new SqlParameter("@Mode", ModeOfStudy));
                ds = db.ExecuteDataSet("getStudentsCourses");

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
        public DataSet GetAdmittedStudents(string Programme, string Course, string Mode, string Session)
        {
            DataSet ds = new DataSet();
            string mykey = "AdmittedStudents_getAdmittedStudents_" + Programme + Course + Mode + Session;
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Programme) || string.IsNullOrEmpty(Course) || string.IsNullOrEmpty(Mode) || string.IsNullOrEmpty(Session))
            {
                return ds;
            }

            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@Course", Course));
                db.Parameters.Add(new SqlParameter("@Mode", Mode));
                db.Parameters.Add(new SqlParameter("@Session", Session));
                ds = db.ExecuteDataSet("getAdmittedStudents");
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public AdmissionLetter GetAdmittedStudents(string regNo)
        {
            DataSet ds = new DataSet();
            AdmissionLetter adm = new AdmissionLetter();
            string mykey = "AdmittedStudents_getAdmittedStudents_" + regNo;
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", regNo));
                ds = db.ExecuteDataSet("getAdmittedStudentLetterByRegNo");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    adm.RegNo = "";
                    adm.Address = "";
                }
                else
                { }
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return adm;
        }

        public DataSet GetClassList(string Programme, string Course, string Mode, string Levell, string SessionName)
        {
            string mykey = "ClassList_getClassList_" + Programme + Course + Mode + Levell + SessionName;
            DataSet ds = new DataSet();
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Programme) || string.IsNullOrEmpty(Course) || string.IsNullOrEmpty(Mode) || string.IsNullOrEmpty(Levell))
            {
                return ds;
            }
            try
            {
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                db.Parameters.Add(new SqlParameter("@Course", Course));
                db.Parameters.Add(new SqlParameter("@Mode", Mode));
                db.Parameters.Add(new SqlParameter("@Levell", Levell));
                db.Parameters.Add(new SqlParameter("@SessionName", SessionName));
                ds = db.ExecuteDataSet("getClassList");

            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public static ArrayList getPasswordByMatNo(string matNo)
        {
            string mykey = "ptiPasswordByMatNo_getPasswordByMatNo_" + matNo;
            ArrayList result = new ArrayList();
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@MatricNo", matNo));
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
                    dr = (SqlDataReader)db.ExecuteReader("getPassword");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result.Add(dr.GetString(dr.GetOrdinal("Email")));
                        result.Add(dr.GetString(dr.GetOrdinal("Password")));
                        result.Add(dr.GetString(dr.GetOrdinal("dlcmail")));
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
        public static ArrayList getAppPasswordByUsername(string matNo)
        {
            string mykey = "PasswordByMatNo_getAppPasswordByUsername_" + matNo;
            ArrayList result = new ArrayList();
            DBAccess db = new DBAccess();
            db.AddParameter(new SqlParameter("@MatricNo", matNo));
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
                    dr = (SqlDataReader)db.ExecuteReader("getAppPassword");
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
        public string getCurrentSemester()
        {
            string result = "";
            string mykey = "getCurrentSemester";
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
                    dr = (SqlDataReader)db.ExecuteReader("getCurrentSemester");
                    if (dr.HasRows)
                    {
                        dr.Read();
                        result = dr.GetString(dr.GetOrdinal("Semester"));
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
        public string getStudentPayableFees(string matno, string payType)
        {
            string retVal = ""; string fees = "";
            StudentsBusiness sb = new StudentsBusiness();
            StudentPayment sp= new StudentPayment();
            StudentPayment sp2 = new StudentPayment();
            Students st = sb.GetStudentsByMatNo(matno);
            sp = sb.GetStudentPayments(matno, getCurrentSession());
            if (sp == null)
            {
                //No Fees Paid So Far, Get Minimum Fees, this indicate First Semester
                fees = getStudentPayableFees((payType.ToUpper() == "FULL PAYMENT") ? payType : (getCurrentSemester().ToLower() == "second" | getCurrentSemester().ToLower()=="summer") ? "FULL PAYMENT" : "First Installment", getCurrentSession(), st.AcademicLevel, st.ModeOfStudy, st.Faculty, st.Programme);
                //Get Payable based on payType from FEES PIN
                retVal = fees;
            }
            else
            {
                //At least student have paid once, check if it was full or part
                if (sp.PaymentType.ToUpper() != "FULL PAYMENT")
                {
                    //Part paid before, Check if Paid for Second Part Payment also
                    sp2 = sb.GetStudentPayments(matno, getCurrentSession(), getCurrentSemester());
                    if (sp2 == null)
                    {
                        //Yet to pay balance
                        fees = getStudentPayableFees("Second Installment", getCurrentSession(), st.AcademicLevel, st.ModeOfStudy, st.Faculty, st.Programme);
                        //get the balance(Second Installments) from FEES PIN
                        retVal = fees;
                    }
                }
            }

            return retVal;
        }
        public string getStudentPayableFees(string paytype, string session, string level, string mode, string faculty, string programme)
        {
            string retVal = "";
            string mykey = "StudentPayableFees_getFeesPayable_" + programme + faculty + mode + session + paytype + level;
            DBAccess db = new DBAccess(mykey);
            SqlDataReader ds;

            if (BaseBusiness.enableCache && BizObject.Cache[mykey] != null)
            {
                retVal = (string)BizObject.Cache[mykey];
            }
            else
            {

                try
                {
                    db.Parameters.Add(new SqlParameter("@Programme", programme));
                    db.Parameters.Add(new SqlParameter("@Faculty", faculty));
                    db.Parameters.Add(new SqlParameter("@Mode", mode));
                    db.Parameters.Add(new SqlParameter("@Sessionname", session));
                    db.Parameters.Add(new SqlParameter("@PaymentType", paytype));
                    db.Parameters.Add(new SqlParameter("@Level", level));


                    //(SqlDataReader)db.ExecuteReader("getCurrentSession");
                    ds = (SqlDataReader)db.ExecuteReader("getFeesPayable");

                    if (ds.HasRows)
                    {
                        ds.Read();
                        retVal = ds.GetDecimal(ds.GetOrdinal("Amount")).ToString();
                        BaseBusiness.CacheData(mykey, retVal);
                    }

                }
                catch (Exception ec)
                {
                    string ffg = ec.Message;
                }
            }
            db.Dispose();

            return retVal;
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
        public bool SaveStudentPassPort(string MatricNo, string PixPath)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                FileStream fs = null;
                SqlParameter param = null;
                fs = new FileStream(PixPath, FileMode.Open, FileAccess.Read);
                Byte[] blob = new Byte[fs.Length];
                fs.Read(blob, 0, blob.Length);
                fs.Close();
                param = new SqlParameter("@Picture", SqlDbType.VarBinary, blob.Length,ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blob);
                db.Parameters.Add(new SqlParameter("@MatricNo", MatricNo));
                db.Parameters.Add(param);
                int rett = db.ExecuteNonQuery("UpdateStudentPassport");
                if (rett != -1)
                {
                    RetValue = true;
                }
                else
                {
                    RetValue = false;
                }
                db.Dispose();

            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public void PerformQuery(string Qry)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(strConn);

                cnn.Open();

                SqlCommand cmd = null;
                //SqlDataReader dr = null;
                cmd = new SqlCommand(Qry, cnn);
                cmd.ExecuteNonQuery();
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
        }
        public DataSet SelectQuery(string Qry)
        {
            DataSet dt = new DataSet();
            try
            {
                SqlConnection cnn = new SqlConnection(strConn);

                cnn.Open();

                SqlCommand cmd = null;
                //SqlDataReader dr = null;
                SqlDataAdapter dat = new SqlDataAdapter(Qry, cnn);
                dat.Fill(dt);
                dat.Dispose();
                //cmd = new SqlCommand();
                //dt= cmd.ExecuteReader();
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
            return dt;
        }
        public PinVerifyResult VerifySummerFeePin(string Pin, int PinType, Students Stud)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifySummerFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string Faculty = ds.Tables[0].Rows[0]["Faculty"].ToString();
                    string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                    //string AcademicLevel = ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string ModeOfStudy = ds.Tables[0].Rows[0]["ModeOfStudy"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amount = Convert.ToInt32(ds.Tables[0].Rows[0]["Amount"]).ToString();
                    int LowerQty = Convert.ToInt32(ds.Tables[0].Rows[0]["LowerQty"]);
                    int UpperQty = Convert.ToInt32(ds.Tables[0].Rows[0]["UpperQty"]);
                    string pinNumber = ds.Tables[0].Rows[0]["PinNumber"].ToString();
                    string pinSerial = ds.Tables[0].Rows[0]["PinSerialNumber"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != Stud.MatricNumber)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Pin Already Used by Student with Matric no " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = false;
                            RetValue.PaymentType = PinType.ToString();
                            RetValue.FailureComment = "PIN already used by you";
                        }
                        return RetValue;
                    }
                    if (PinStatus == "0")
                    {
                        if (Faculty.ToLower() != Stud.Faculty.ToLower())
                        {
                            //Pin Exists But Not Valid For the Student (Based on Indigeneship)
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on College)";
                            return RetValue;
                        }

                        //if (Stud.AcademicLevel.ToLower() != AcademicLevel.ToLower())
                        //{
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on your Academic Level)";
                        //    return RetValue;
                        //}
                        if (Programme.ToLower() != Stud.Programme.ToLower())
                        {
                            //Pin Exists But Not Valid For the Student (Based on Programme)

                            RetValue.Verified = false;
                            RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Programme)";
                            return RetValue;
                        }
                        if (ModeOfStudy.ToLower() != Stud.ModeOfStudy.ToLower())
                        {
                            //Pin Exists But Not Valid For the Student (Based on Academic Level)
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The PIN does not Correspond with the amount Expected (Based on Mode Of Study)";
                            return RetValue;
                        }
                        if (PinType < LowerQty || UpperQty < PinType)
                        {
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The PIN does not Correspond with the amount Expected";
                            return RetValue;
                        }
                        RetValue.Verified = true;
                        RetValue.PaymentType = "Summer Cost for " + PinType.ToString() + " course(s)";
                        RetValue.Pin = pinNumber;
                        RetValue.PinSerial = pinSerial;
                        RetValue.PinValue = amount;
                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "School Fee PIN Does Not Exist! Check if you entered it correctly.";
                }
                ds.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return RetValue;
        }
        public static bool UpdateChangeOfAddmissionCourse(string MatricNo, int CourseOfStudyID)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatricNumber", MatricNo));
                db.Parameters.Add(new SqlParameter("@CourseOfStudyID", CourseOfStudyID));
                int rett = db.ExecuteNonQuery("UpdateChangeOfAddmissionCourse");
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
    }

}
