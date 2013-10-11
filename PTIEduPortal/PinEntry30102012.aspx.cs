using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using System.Drawing;
using log4net;
using log4net.Config;
public partial class PinEntry : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SignOnBusiness Sob = new SignOnBusiness();
    StudentSignOn So = new StudentSignOn();
    StudentsBusiness Stb = new StudentsBusiness();
    Students St = new Students();
    PinVerifyResult Pvr = new PinVerifyResult();
    string CurrentSession = new SignOnBusiness().getCurrentSession();
    string PayType = "";
    string CurrentSemester = new SignOnBusiness().getCurrentSemester();
    string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];

    protected void Page_Load(object sender, EventArgs e)
    {

        So = (StudentSignOn)Session["StudentSignOn"];
        bool fromlogin = false;
        if (Session["fromStudentLogin"] != null)
            fromlogin = (bool)Session["fromStudentLogin"];
        if (fromlogin == true && So != null)
        {
            PanelSkuFee.Visible = true;
            PayType = (Request.QueryString["paytype"] !=null) ? Request.QueryString["paytype"].ToString().ToUpper(): "FULL";
            LblInfo.Text = "Welcome, Enter your School Ref Number Accordingly; Click Continue to Proceed";
            logger.Info("Students - " + So.MatricNumber + " - is to pay School Fee - " + PayType);
            if (!Page.IsPostBack)
            {
                if (Session["Students"] == null || ((Students)Session["Students"]).AdmissionStatus.ToLower() != "admitted")
                    Response.Redirect("StudentControlCenter.aspx",false );

                if (PayType == "FULL")
                {
                    PaymentMode.Items.RemoveAt(2);
                }
                else
                {
                    PaymentMode.Enabled = false;
                    PaymentMode.SelectedIndex = (PayType.ToLower() == "part") ? 2 : 2;
                }
            }
        }
        else
        {
            logger.Info("Student session expired! Redirecting to student login page");
            Response.Redirect("StudentLogin.aspx");
        }
        //PaymentMode.SelectedIndex = 1;
        PaymentMode.Enabled = false;
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        bool SkuFeePinVerified = false;
        string SkuFeePinComment = "";
        byte[] buffer;
        string PinSku = "";

        St = Stb.GetStudentsByMatNo(So.MatricNumber);

        //check if school Ref Number is empty
        if (string.IsNullOrEmpty(txtSkuFeePin.Text.Trim()))
        {
            LblError.Text = "Empty School fees PIN";
            LblErrorCause.Text = "You have not entered School Fees PIN";
            PanelError.Visible = true;
            logger.Warn(St.MatricNumber + " - " + LblError.Text);
            return;
        }
        // Verifying School Ref Number - By Current Session, Student - Programme, Faculty and Mode Of Study
        buffer = CyberEncryptor.encypt(txtSkuFeePin.Text.Trim(), Key);
        PinSku = Convert.ToBase64String(buffer);

     
      //string s=CyberDecryptor.Decryption(txtSkuFeePin.Text.Trim(), Key);
      //PIN   :17127266948        59531023490     78264070448   96520034887     10414627924                                                                   
        //PinSku = txtSkuFeePin.Text.Trim();
        //Pvr = Sob.VerifySchoolFeePin(PinSku, PaymentMode.SelectedValue, St);

        Pvr = Sob.VerifySchoolFeePin(PinSku, PaymentMode.SelectedValue, St);
        SkuFeePinVerified = Pvr.Verified;
        SkuFeePinComment = Pvr.FailureComment;
      

         if( (SkuFeePinVerified) && (St.AcademicLevel =="HND II") && !(Pvr.PinValue =="14500")  )
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for HND II students !!!";
             logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for HND II students !!!");
             return;
         }
         if ((SkuFeePinVerified) && (St.AcademicLevel == "ND II") && !(Pvr.PinValue == "14500"))
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for ND II students !!!";
             logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for ND II students !!!");
             return;
         }

         if ((SkuFeePinVerified) && (St.AcademicLevel == "HND I") && !(Pvr.PinValue == "25150"))
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for ND I students !!!";
             logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for ND I students !!!");
             return;
         }
         if ((SkuFeePinVerified) && (St.AcademicLevel == "ND I") && !(Pvr.PinValue == "25550"))
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for ND I students !!!";
              logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for ND I students !!!");
             return;
         }

         if ((SkuFeePinVerified) && (St.AcademicLevel == "GW") && !(Pvr.PinValue == "33150"))
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for GW students !!!";
             logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for GW students !!!");
             return;
         }

         if ((SkuFeePinVerified) && (St.AcademicLevel == "PRE-ND") && !(Pvr.PinValue == "22250"))
         {
             PanelError.Visible = true;
             LblError.Text = "Wrong School Fees:This PIN is not for PRE-ND students !!!";
             logger.Info(St.MatricNumber + "_Wrong School Fees:This PIN is not for PRE-ND students !!!");
             return;
         }


        if (!SkuFeePinVerified)
        {
            LblError.Text = "Invalid School Fee Ref. Number";
            //LblError.Text = SkuFeePinComment;
            LblErrorCause.Text = SkuFeePinComment;
            PanelError.Visible = true;
            logger.Warn(St.MatricNumber + " - " + LblError.Text);
            return;
        }
        PanelError.Visible = false;
        bool upd = false;
        Session["Student"] = St;
        ///Updating Payment Table and Pin Table
        //buffer = CyberEncryptor.encypt(txtSkuFeePin.Text.Trim(), Key);
        //PinSku = Convert.ToBase64String(buffer);
        PinSku = txtSkuFeePin.Text.Trim();
        //upd = Sob.UpdateSignOn(So.MatricNumber, PinSku, PinStu);
        logger.Info(So.MatricNumber + " - Sign on table was updated successfully");
        //Updating School Ref Number Table
        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");

        //for dlc mode
        upd = Sob.UpdateSchoolFeePin(So.MatricNumber, NowDate, Pvr.Pin, PaymentMode.SelectedValue, Pvr.PinValue, Pvr.PinSerial, CurrentSession);
        logger.Info(So.MatricNumber + " - School Ref Number table was updated successfully");

        Session["fromLoginToControl"] = true;
        Response.Redirect("StudentControlCenter.aspx?MatricNumber=" + So.MatricNumber);
    }
}
