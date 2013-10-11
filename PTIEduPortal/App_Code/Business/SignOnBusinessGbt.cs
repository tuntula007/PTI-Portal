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

    public class SignOnBusinessGbt : BaseBusiness 
    {
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
        public bool UpdateSignOn2(string MatricNo, string SkuFeePin, string StudioFeePin)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@SchoolFeePin", SkuFeePin));
                db.Parameters.Add(new SqlParameter("@StudioFeePin", StudioFeePin));
                int rett = db.ExecuteNonQuery("UpdateSignOn2");
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
        public bool UpdatePaymentFeePin(string MatricNo, string UsedDate, string PinNumber, string PaymentFor, string BranchPurchase)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                db.Parameters.Add(new SqlParameter("@PaymentFor", PaymentFor));
                db.Parameters.Add(new SqlParameter("@BranchPurchase", BranchPurchase)); 
                //db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdatePaymentFeePin");
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
        public bool UpdateSchoolFeePin(string MatricNo, string UsedDate, string PinNumber)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@UsedDate", UsedDate));
                db.Parameters.Add(new SqlParameter("@PinNumber", PinNumber));
                int rett = db.ExecuteNonQuery("UpdateSchoolFeePin");
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
                    RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["SchoolFeePin"].ToString();
                    RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["StudioFeePin"].ToString();
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                }
                else
                {
                    RetValue.MatricNumber = "";
                    RetValue.Password = "";
                    RetValue.SchoolFeePin = "";
                    RetValue.StudioFeePin = "";
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
            string mykey = "DeltasUni_VerifySignOn_VerifySignOn_" + MatricNo + PassWord;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@MatNo", MatricNo));
                db.Parameters.Add(new SqlParameter("@PassWord", PassWord));
                DataSet ds = db.ExecuteDataSet("VerifySignOn");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RetValue.MatricNumber = ds.Tables[0].Rows[0]["MatricNumber"].ToString();
                    RetValue.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    RetValue.SchoolFeePin = ds.Tables[0].Rows[0]["SchoolFeePin"].ToString();
                    RetValue.StudioFeePin = ds.Tables[0].Rows[0]["StudioFeePin"].ToString();
                    RetValue.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    RetValue.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
                    //RetValue.SchoolFeePin2 = ds.Tables[0].Rows[0]["SchoolFeePin2"].ToString();
                    RetValue.Verified = true;
                }
                else
                {
                    RetValue.MatricNumber = "";
                    RetValue.Password = "";
                    RetValue.SchoolFeePin = "";
                    //RetValue.SchoolFeePin2 = "";
                    RetValue.StudioFeePin = "";
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

            string mykey = "DeltasUni_CanRegister_VerifyCanRegister_" + MatricNo;
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

        public  PinVerifyResult VerifySchoolFeePin(string Pin, Students Stud)
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
                    //string IsIndigene = ds.Tables[0].Rows[0]["IsIndigene"].ToString();
                    string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                    string AcademicLevel = ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amount = ds.Tables[0].Rows[0]["Amount"].ToString();
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
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {
                        //if(IsIndigene != Stud.IsIndigene )
                        //{
                        //    //Pin Exists But Not Valid For the Student (Based on Indigeneship)
                        //    RetValue.Verified = false;
                        //    RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Indigeneship)";
                        //    return RetValue;
                        //}

                      //  if ((Stud.AcademicLevel == "PND1" || Stud.AcademicLevel == "ND1" || Stud.Programme == "Pre-ND") && Stud.IsIndigene == "1")
                       if (Stud.AcademicLevel == "PND1" )

                        {
                            if (amount.Equals("21700") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for ND 1 & Pre-ND PIN";
                                return RetValue;
                            }
                            
                        }
                        else
                        {
                            //if (Programme != Stud.Programme)
                            //{
                            //    //Pin Exists But Not Valid For the Student (Based on Programme)

                            //    RetValue.Verified = false;
                            //    RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Programme)";
                            //    return RetValue;
                            //}
                            //if (AcademicLevel != Stud.AcademicLevel)
                            //{
                            //    //Pin Exists But Not Valid For the Student (Based on Academic Level)
                            //    RetValue.Verified = false;
                            //    RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Academic Level)";
                            //    return RetValue;
                            //}
                        }
                        RetValue.Verified= true;

                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "School Fee PIN Does Not Exist! Check your Evening/Morning Status";
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
        public PinVerifyResult VerifyPaymentFeePin(string Pin, double Amount, string Bank, string UserId)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                //db.Parameters.Add(new SqlParameter("@amount", Amount));
                db.Parameters.Add(new SqlParameter("@bank", Bank));
                DataSet ds = db.ExecuteDataSet("VerifyPaymentFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string ActiveStatus = ds.Tables[0].Rows[0]["IsActive"].ToString();
                    //string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                   // string AcademicLevel = ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amountst = ds.Tables[0].Rows[0]["Amount"].ToString();
                    double amount = 0;
                    double.TryParse(amountst,out amount);
                    if (PinStatus == "1")
                    {
                        if (UsedBy != UserId )
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Pin Already Used by Student with User Name/Matric no " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {
                        if (ActiveStatus =="0")
                        {
                            //Pin Exists But Not Activated 
                            RetValue.Verified = false;
                            RetValue.FailureComment = "PIN Can not be used at this time, kindly inform your Administrator";
                            return RetValue;
                        }

                        if (amount != Amount)
                        {
                            //Pin Exists but has diffent amount
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected";
                            return RetValue;
                        }               
                        RetValue.Verified = true;

                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Fee PIN Does Not Exist! Check the Bank name supplied";
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
        public DataSet GetScreeningList(string Programme, string Course, string Mode)
        {
            DataSet ds = new DataSet();
            string mykey = "DeltasUni_ScreeningList_GetScreeningLists_" + Programme + Course + Mode;
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
                ds = db.ExecuteDataSet("GetScreeningList");
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }

        public PinVerifyResult VerifyPartTimeFeePin(string Pin, Students Stud,string semester)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                db.Parameters.Add(new SqlParameter("@Semester", semester));
                DataSet ds = db.ExecuteDataSet("VerifyPartTimeFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string IsIndigene = ds.Tables[0].Rows[0]["IsIndigene"].ToString();
                    string Programme = ds.Tables[0].Rows[0]["Programme"].ToString();
                    string AcademicLevel = ds.Tables[0].Rows[0]["AcademicLevel"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    string amount = ds.Tables[0].Rows[0]["Amount"].ToString();
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
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {

                        if (Stud.AcademicLevel.ToUpper().Equals("HND1") || Stud.AcademicLevel.ToUpper().Equals("HNDIII"))
                        {
                            if (amount.Equals("16850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for HND 1 & HND III PIN";
                                return RetValue;
                            }
                        }
                        else if (Stud.AcademicLevel.ToUpper().Equals("HNDII") || Stud.AcademicLevel.ToUpper().Equals("NDIII") || Stud.Programme.ToUpper().Equals("NDS") ||
                                 AcademicLevel.ToUpper().Equals("DIPIII") || Stud.AcademicLevel.ToUpper().Equals("DPAIII") || Stud.AcademicLevel.ToUpper().Equals("DPAAIII") || Stud.AcademicLevel.ToUpper().Equals("DPSIII") || Stud.AcademicLevel.ToUpper().Equals("DILIII"))
                        {
                            if (amount.Equals("14850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for Diploma III, HNDII, NDIII or NDS PIN";
                                return RetValue;
                            }
                        }

                        else if (Stud.AcademicLevel.ToUpper().Equals("ND1") || Stud.AcademicLevel.ToUpper().Equals("NDII") ||
                                 AcademicLevel.ToUpper().Equals("DIP1") || Stud.AcademicLevel.ToUpper().Equals("DPA1") || Stud.AcademicLevel.ToUpper().Equals("DPAA1") || Stud.AcademicLevel.ToUpper().Equals("DPS1") || Stud.AcademicLevel.ToUpper().Equals("DIL1"))
                        {
                            if (amount.Equals("13850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for Diploma 1, NDI or NDII PIN";
                                return RetValue;
                            }
                        }
                        else if (AcademicLevel.ToUpper().Equals("DIPII") || Stud.AcademicLevel.ToUpper().Equals("DPAII") || Stud.AcademicLevel.ToUpper().Equals("DPAAII") || Stud.AcademicLevel.ToUpper().Equals("DPSII") || Stud.AcademicLevel.ToUpper().Equals("DILII"))
                        {
                            if (amount.Equals("12850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for Diploma II PIN";
                                return RetValue;
                            }
                        }
                        else if (AcademicLevel.ToUpper().Equals("CPA1"))
                        {
                            if (amount.Equals("11850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for CPA 1 PIN";
                                return RetValue;
                            }
                        }
                        else if (AcademicLevel.ToUpper().Equals("CPAII"))
                        {
                            if (amount.Equals("10850") == false)
                            {
                                RetValue.Verified = false;
                                RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected for CPA II PIN";
                                return RetValue;
                            }
                        }


                        RetValue.Verified = true;

                    }



                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Part-Time Fee PIN Does Not Exist OR PIN already Used! Check your PIN again.";
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
        public PinVerifyResult VerifyStudioFeePin(string Pin, Students Stud)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifyStudioFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != Stud.MatricNumber)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Studio Fee Pin Already Used by Student with Matric No " + UsedBy;
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
                    RetValue.FailureComment = "Studio Fee PIN Does Not Exist";
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

        public PinVerifyResult VerifyEveningFeePin(string Pin,Students Stud)
        {
            PinVerifyResult RetValue = new PinVerifyResult();
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@Pin", Pin));
                DataSet ds = db.ExecuteDataSet("VerifyEveningFeePin");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                    string IsIndigene = ds.Tables[0].Rows[0]["Indigene"].ToString();
                    string UsedBy = ds.Tables[0].Rows[0]["UsedBy"].ToString();
                    if (PinStatus == "1")
                    {
                        if (UsedBy != Stud.MatricNumber)
                        {
                            //Pin Already used by somebody else
                            RetValue.Verified = false;
                            RetValue.FailureComment = "Evening Student Fee Pin Already Used by Student with Matric Number " + UsedBy;
                        }
                        else
                        {
                            RetValue.Verified = true;
                        }
                    }
                    if (PinStatus == "0")
                    {

                        if (IsIndigene != Stud.IsIndigene)
                        {
                            //Pin Exists But Not Valid For the Student (Based on Indigeneship)
                            RetValue.Verified = false;
                            RetValue.FailureComment = "The Amount Paid does not Correspond with the amount Expected (Based on Indigeneship)";
                            return RetValue;
                        }

                        RetValue.Verified = true;
                    }
                }
                else
                {
                    //Pin Does not Exist
                    RetValue.Verified = false;
                    RetValue.FailureComment = "Evening Student Fee PIN Does Not Exist";
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
        public DataSet GetAdmissionLetterByRegNo(string RegNo, string ModeofStudy)
        {
            string mykey = "DeltasUni_AdmissionLetter_getAdmissionLetterByRegNo_" + RegNo;
            DataSet ds = new DataSet();
            DBAccess db = new DBAccess(mykey);
            try
            {
                db.Parameters.Add(new SqlParameter("@RegNo", RegNo));
                db.Parameters.Add(new SqlParameter("@ModeofStudy", ModeofStudy));
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
            string mykey = "DeltasUni_GetDistinctProgramme_getApplicantProgramme";
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
            string mykey = "DeltasUni_GetDistinctCourses_getApplicantCourses_" + Programme;
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
            string mykey = "DeltasUni_GetStudentsCourses_getStudentsCourses_" + Programme + ModeOfStudy;
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
            string mykey = "DeltasUni_CourseOfStudy_getCoursesOfStudy_" + Programme + Mode;
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
            string mykey = "DeltasUni_GetDistinctMode_getModeOfStudy";
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
            string mykey = "DeltasUni_GetDistinctLevel_getLevel";
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
            string mykey = "DeltasUni_AdmittedStudents_getAdmittedStudents_" + Programme + Course + Mode;
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
        public DataSet GetAdmittedStudentsByParam(string Param)
        {
            DataSet ds = new DataSet();
            string mykey = "DeltasUni_AdmittedStudents_getAdmittedStudentsByParam_" + Param;
            DBAccess db = new DBAccess(mykey);
            if (string.IsNullOrEmpty(Param))
            {
                Param = "%%";
            }

            try
            {
                db.Parameters.Add(new SqlParameter("@Param", Param));
                ds = db.ExecuteDataSet("getAdmittedStudentsByParam");
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            db.Dispose();
            return ds;
        }
        public string GetApplicantCourseOfStudy(string FormNo)
        {
            string RetValue = "";
            SqlDataReader dr = null;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@FormNo", FormNo));
                //DataSet ds = db.ExecuteDataSet("GetApplicantCourseOfStudy");
                dr = (SqlDataReader)db.ExecuteReader("GetApplicantCourseOfStudy");
                while (dr.Read())
                {
                    try
                    {
                        RetValue = dr.GetString(0);
                    }
                    catch (Exception)
                    {
                        
                        
                    }
                }
                dr.Dispose();
            }
            catch (Exception ec)
            {
                string ffg = ec.Message;
            }
            //dr.Dispose();
            return RetValue;
        }
        public bool UpdateApplicantsProgrammeCourse(string AppFormNo, string CourseOfStudy, string Programme)
        {
            bool RetValue = false;
            DBAccess db = new DBAccess();
            try
            {
                db.Parameters.Add(new SqlParameter("@AppFormNo", AppFormNo));
                db.Parameters.Add(new SqlParameter("@CourseOfStudy", CourseOfStudy));
                db.Parameters.Add(new SqlParameter("@Programme", Programme));
                int rett = db.ExecuteNonQuery("UpdateApplicantsProgrammeCourse");
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
        public DataSet GetClassList(string Programme, string Course, string Mode, string Levell, string SessionName)
        {
            string mykey = "DeltasUni_ClassList_getClassList_" + Programme + Course + Mode + Levell + SessionName;
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
            string mykey = "DeltasUni_PasswordByMatNo_getPasswordByMatNo_" + matNo;
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
            string mykey = "DeltasUni_PasswordByMatNo_getAppPasswordByUsername_" + matNo;
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
            string mykey = "DeltasUni_getCurrentSession";
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

        public DataSet getAcademicSessions()
        {
            DataSet result = new DataSet();
            string mykey = "DeltasUni_getAcademicSessions";
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
    }


}
