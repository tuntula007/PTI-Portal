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

public partial class AdmissionLetterParam : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    Applicants App = new Applicants();
            
    SignOnBusiness Sob = new SignOnBusiness();
    StudentSignOn So = new StudentSignOn();
    PinVerifyResult Pvr = new PinVerifyResult();
    AdmissionLetter Adm = new AdmissionLetter();
    string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
    string ModeOfStudy = "Full-Time";
    protected void Page_Load(object sender, EventArgs e)
    {
        

        //prog=PRE-ND 
        if (Request.QueryString.Get("prog") == "PRE-ND")
        {
            Session["ModeOfEntry"] = "PRE-ND";
            Session["RegNo"] = txtRegNo.Text;
            App.Programme = "ND";
        }
        else
        {
            if (Session["Applicants"] != null)
            {
                App = (Applicants)Session["Applicants"];
                string P = App.Programme;
            }
            else
            {
                Response.Redirect("~/ApplicantLogin.aspx");
            }

            So = (StudentSignOn)Session["StudentSignOn"];
            if (So != null)
            {
                txtRegNo.Text = So.MatricNumber;
                txtPin.Text = So.Password;
                logger.Info(So.MatricNumber + " - Sucessfully Opened Admission Letter Prameter Page");
            }
        }
         
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
         ModeOfStudy = cmbModeofStudy.SelectedValue;
        try
        {
            logger.Info(txtRegNo.Text + " - hits the Preview Button");
            Pvr = Sob.VerifyAdmissionLetterRegNo(txtRegNo.Text.Trim(),ModeOfStudy);
            if (!Pvr.Verified)
            {
                LblError.Text = "Invalid RegNo";
                LblErrorCause.Text = Pvr.FailureComment + "(may be through Mode of Study)";
                PanelError.Visible = true;
                logger.Warn(txtRegNo.Text + " - Invalid RegNo");
                return;
            }
            byte[] buffer = CyberEncryptor.encypt(txtPin.Text.Trim(), Key);
            string PinAdm = Convert.ToBase64String(buffer);

            Pvr = Sob.VerifyAdmissionLetterFeePin(PinAdm, txtRegNo.Text.Trim(),ModeOfStudy);
            if (!Pvr.Verified)
            {
                LblError.Text = "Invalid Admission Letter Fee PIN";
                LblErrorCause.Text = Pvr.FailureComment;
                PanelError.Visible = true;
                logger.Warn(txtRegNo.Text + " - Invalid Admission Letter Fee PIN");
                return;
            }
            LblError.Text = "";
            LblErrorCause.Text = "";
            PanelError.Visible = false;
            string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool upd = false;
            upd = Sob.UpdateAdmissionLetterFeePin(txtRegNo.Text.Trim(), NowDate, PinAdm);
            Session["RegNo"] = txtRegNo.Text.Trim();
            Session["Mode"] = "Preview";
            Session["Pinn"] = PinAdm;
            Session["ModeofStudy"] = ModeOfStudy;
            logger.Info(txtRegNo.Text.Trim() + " - entering Preview mode");
            //string s = "<" + "script language='javascript'>window.open('RptPrintAdmissionLetter.aspx','CustomPopUP','width=800,height=650,menubar=yes,scrollbars=yes')<" + "/script>";
            //ClientScript.RegisterStartupScript(GetType(), "focus", s);
            Response.Redirect("RptPrintAdmissionLetter.aspx",false );
        }
        catch (Exception ex)
        {
            logger.Error(txtRegNo.Text + " - " + ex.Message);
        }


        
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        ModeOfStudy = cmbModeofStudy.SelectedValue;
        try
        {
            logger.Info(So.MatricNumber + " - hits the Print Button, using the pin -" + txtPin.Text);
            Pvr = Sob.VerifyAdmissionLetterRegNo(txtRegNo.Text.Trim(),ModeOfStudy);
            if (!Pvr.Verified)
            {
                LblError.Text = "Invalid RegNo";
                LblErrorCause.Text = Pvr.FailureComment + "(may be through Mode of Study)";
                PanelError.Visible = true;
                logger.Warn(So.MatricNumber + " - Invalid RegNo");
                return;
            }
            Pvr = Sob.VerifyAdmissionLetterFeePin(txtPin.Text.Trim(), txtRegNo.Text.Trim(),ModeOfStudy);
            if (!Pvr.Verified)
            {
                LblError.Text = "Invalid Admission Letter Fee PIN";
                LblErrorCause.Text = Pvr.FailureComment;
                PanelError.Visible = true;
                logger.Warn(So.MatricNumber + " - " + txtPin.Text.Trim() + " - Invalid Admission Letter Fee PIN");
                return;
            }
            LblError.Text = "";
            LblErrorCause.Text = "";
            PanelError.Visible = false;
            string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            bool upd = false;
            upd = Sob.UpdateAdmissionLetterFeePin(txtRegNo.Text.Trim(), NowDate, txtPin.Text.Trim());
            Session["RegNo"] = txtRegNo.Text.Trim();
            Session["Mode"] = "Preview";
            Session["ModeofStudy"] = ModeOfStudy;
            logger.Info(txtRegNo.Text + " - VerifyAdmissionLetterFeePin ok. Entering preview mode - Used PIN: " + txtPin.Text);
            Response.Redirect("RptPrintAdmissionLetterN.aspx", true);
        }
        catch (Exception ex)
        {
            logger.Error(txtRegNo.Text + " - " + ex.Message);
        }
        finally
        {
            logger.Info(txtRegNo.Text + " try to print admission letter with the pin " + txtPin.Text);
        }
    }
  
}
