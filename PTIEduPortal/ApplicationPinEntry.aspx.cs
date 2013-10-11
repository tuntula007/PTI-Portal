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
public partial class ApplicationPinEntry : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    ApplicantSignOnBusiness Aob = new ApplicantSignOnBusiness();
    ApplicantSignOn Ao = new ApplicantSignOn();


    ApplicantsBusiness Appb = new ApplicantsBusiness();
    Applicants app = new Applicants();
    PinVerifyResult Pvr = new PinVerifyResult();
    string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
    protected void Page_Load(object sender, EventArgs e)
    {

        Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
        bool fromlogin = (bool)Session["fromApplicantLogin"];
        if (fromlogin == true && Ao != null)
        {
            app = Appb.GetApplicantsByFormNo(Ao.FormNumber);
            logger.Info("Applicant information retrieved, Form No: " + Ao.FormNumber );
        }
        else
        {
            logger.Info("Applicant session expired! Redirecting to Applicant login page");
            Response.Redirect("ApplicantLogin.aspx");
        }
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        bool AppFormFeePinVerified = false;

        string AppFormFeePinComment = "";
        
        byte[] buffer;
       
        string AppForm = "";
       

        //check if school fees pin is empty
        if (string.IsNullOrEmpty(txtAppFormFeePin.Text.Trim()))
        {
            LblError.Text = "Empty Application Form PIN";
            LblErrorCause.Text = "You have not entered Application Form Fee PIN";
            PanelError.Visible = true;
            logger.Warn(app.UserName + " - " + LblError.Text);
            return;
        }
       
     
            // Verifying School Fee Pin
            buffer = CyberEncryptor.encypt(txtAppFormFeePin.Text.Trim(), Key);
            AppForm = Convert.ToBase64String(buffer);

            Pvr = Aob.VerifyApplicationFeePin(AppForm, app);

            AppFormFeePinVerified = Pvr.Verified;
            AppFormFeePinComment = Pvr.FailureComment;
            if (!AppFormFeePinVerified)
            {
                LblError.Text = "Invalid Addmission Form Fee PIN";
                LblErrorCause.Text = AppFormFeePinComment;
                PanelError.Visible = true;
                logger.Warn(app.UserName  + " - " + LblError.Text);
                return;
            }
        
        PanelError.Visible = false;
        bool upd = false;
        Session["Applicant"] = app;

        ///Updateing SignOn Table and Pin Table
        buffer = CyberEncryptor.encypt(txtAppFormFeePin.Text.Trim(), Key);
        AppForm = Convert.ToBase64String(buffer);

        upd = Aob.UpdateSignOn(Ao.UserName, AppForm, "");
        logger.Info(Ao.UserName + " -  Applicant Sign on table was updated successfully");
        //Updating School Fee PIN Table
        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");


        upd = Aob.UpdateApplicationFeePin(Ao.UserName, NowDate, AppForm);
  
        logger.Info(Ao.UserName + " - Application Fees Pin table was updated successfully");

        Session["fromLoginToControl"] = true;
        Response.Redirect("ApplicantControlCenter.aspx");
    }
}
