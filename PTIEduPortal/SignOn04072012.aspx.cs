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
 
public partial class SignOn : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    PinVerifyResult Pvr = new PinVerifyResult();
    string CurrentSession = string.Empty; 
    string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];

    protected void Page_Load(object sender, EventArgs e)
    {
        //string txt = CyberDecryptor.Decryption("J27vuicZsryABgweOkyMgn8R9euU8yYJ", Key);
        if (!IsPostBack)
        { }
        else
        {
            if (!(String.IsNullOrEmpty(txtPin.Text.Trim()))) txtPin.Attributes["value"] = txtPin.Text;
        }
    }
    protected void BtnSignOn_Click(object sender, EventArgs e)
    {
        //Veryfy Entries
        //if not ok display message and loop out
        //else
        //Verify NewStudent/Status
        //if not ok display message and loop out
        //Verify Fees Pin
        //if not ok display message and loop out
        //Generate Matric
       //Replicate to Students, Signon etc
        //Create Profile
        //Redirect to Login Page and Print Admission Letter
        
        LblError.Text = "";
        CurrentSession = new SignOnBusiness().getCurrentSession();
        SignOnBusiness Sob = new SignOnBusiness();
        StudentSignOn So = new StudentSignOn();
        bool SkuFeePinVerified = false;
        string SkuFeePinComment = "";
        byte[] buffer;
        string PinSku = "";
        string newMatricNo = "";
        #region Veryfy Entries
        PanelError.Visible = true;
        if (string.IsNullOrEmpty(txtMatricNo.Text))
        {
            LblError.Text = "Please enter Your Application Form Number.";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtMatricNo.Focus();
            return;
        }
        if (txtMatricNo.Text.StartsWith("UI/DLC/UGD") == false)
        {
            LblError.Text = "Please enter your application form number in the correct format(UI/DLC/UGDXXXXXX).";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtMatricNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPin.Text))
        {
            LblError.Text = "You must enter the Confirmation Order Number obtained after paying fees at the bank.";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtMatricNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPassWord.Text) || string.IsNullOrEmpty(txtPasswordC.Text))
        {
            LblError.Text = "Password Must Be Entered";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus();
            return;
        }
        if (txtPassWord.Text.Trim().Contains("●") || txtPasswordC.Text.Trim().Contains("●"))
        {
            LblError.Text = "Such characters are not allowed for password";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus();
            return;
        }
        if (txtPassWord.Text.Trim() != txtPasswordC.Text.Trim())
        {
            LblError.Text = "Password and Confirm Password NOT MATCH";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus(); 
            return;
        }
        #endregion
        #region Verify NewStudent/Status
        //check if matric number in students table
        if (StudentsBusiness.isFormNumberExistsForAdmission(txtMatricNo.Text.Trim())==false)
        {
            LblError.ForeColor = Color.Red;
            LblError.Text = "Your sign on is denied due to WRONG form number. Verify that you have been giving admission.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.Text = "";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = false;
            logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);
            return;
        }

        So = Sob.VerifySignOn(txtMatricNo.Text.Trim());
        if (So.Verified & So.Status ==1)
        {
            //Not Admitted At All
            LblError.ForeColor = Color.Red;
            LblError.Text = "You have already sign up. Your Matric Number/Username is " + So.MatricNumber;
            LnkLogin.Text = "Click here to Login";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = true;
            logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);
            return;
        }
        #endregion
        if (!So.Verified && So.Status == 0)
        {
            #region Verify Fees Pin
            //Try to Pay Fees here
            // Verifying School Ref Number - By Current Session, Student - Programme, Faculty and Mode Of Study
            buffer = CyberEncryptor.encypt(txtPin.Text.Trim(), Key);
            PinSku = Convert.ToBase64String(buffer);

            //PinSku = txtPin.Text.Trim();
            //Pvr = Sob.VerifySchoolFeePin(PinSku, PaymentMode.SelectedValue, St);
            Pvr = Sob.VerifyNewSchoolFeePin(PinSku, txtMatricNo.Text);
            SkuFeePinVerified = Pvr.Verified;
            SkuFeePinComment = Pvr.FailureComment;
            if (!SkuFeePinVerified)
            {
                LblError.Text = "Invalid School Fee Confirmation Order Number";
                new Utility().MessageBox(LblError.Text, this.Page);
                //LblError.Text = SkuFeePinComment;
                LblErrorCause.Text = SkuFeePinComment;
                PanelError.Visible = true;
                logger.Warn(txtMatricNo.Text + " - " + LblError.Text);
                return;
            }
            #endregion
            
            PanelError.Visible = false;
            bool upd = false;
            //Session["Student"] = St;
            ///Updating Payment Table and Pin Table
            //buffer = CyberEncryptor.encypt(txtPin.Text.Trim(), Key);
            //PinSku = Convert.ToBase64String(buffer);
            //PinSku = txtPin.Text.Trim();
            newMatricNo = Sob.GenerateMatNo(txtMatricNo.Text);
            int retVal=0;
            if (int.TryParse(newMatricNo, out retVal) | newMatricNo.Length < 1)
            {
                new Utility().MessageBox("Error: " +
                    ((retVal == 0) ? " You dont have any admission record" :
                    (retVal == -1) ? " You have sign up already. Proceed to login page." :
                    (retVal == -2) ? " Your information already exist as students. Proceed to login page." :
                    (retVal == -3) ? " There was a technical problem, please try again later" :
                    (retVal == -4) ? " There was a technical problem, due to matric number generation. Please retry again." :
                    "Technical problem due to time out"), ResolveUrl("~/SignOn.aspx"), this.Page);
                return;
            }
            
            string NowDate = DateTime.Now.ToString("yyyy-MM-dd");
            //for dlc mode
            upd = Sob.UpdateSchoolFeePin(newMatricNo, NowDate, Pvr.Pin, "FULL PAYMENT", Pvr.PinValue, Pvr.PinSerial, CurrentSession);
            logger.Info(newMatricNo + " - School Ref Number table was updated successfully");

            //upd = Sob.UpdateSignOn(So.MatricNumber, PinSku, PinStu);
            logger.Info(newMatricNo + " - Sign on table was updated successfully");
            //Updating School Ref Number Table
            //Fees Payment End Here
            //LblError.ForeColor = Color.Red;
            //LblError.Visible = true;
            //LblError.Text = "You cannot Sign Up as a Student till you pay acceptance fee. Login <a href='ApplicantLogin.aspx'>HERE<a/> to your applicant page to print your admission letter first";
            //logger.Warn("Kindly Login as an applicant to print your admission letter before coming to this page");
            //return;

            if (string.IsNullOrEmpty(So.MatricNumber) )
            {
                // Update Sign On Info with New Password, Email,PhoneNumber
                So.MatricNumber = newMatricNo;
                So.FormNumber = txtMatricNo.Text.Trim();
                So.Password = txtPassWord.Text.Trim();
                So.Phone = txtPhone.Text.Trim();
                So.Email = txtEmail.Text.Trim();
                bool inserted = Sob.InsertSignOnFresh(So);
                if (inserted)
                {
                    LblError.ForeColor = Color.Blue;
                    LblError.Text = "Sign On Created Successfully";
                    LnkLogin.Text = "Click here to Login";
                    LnkLogin.ForeColor = Color.Blue;
                    LnkLogin.Visible = true;
                    PanelError.Visible = true;
                    Session["StudentSignOn"] = So;
                    //call pop that will redirect back to logon page
                    logger.Info(txtMatricNo.Text.Trim() + " - Sign on Inserted successfully");
                    LblError.Text = "Congrats! Your Sign on was successful! Just logon with your user name, which is your Matric Number(" + So.MatricNumber + ") and your password to your Student Profile Page. You are also expected to Print your Admission Letter.";
                    Session["StudentSignOn"] = So;
                    new Utility().MessageBox(LblError.Text, ResolveUrl("~/StudentLogin.aspx"), this.Page);
                    return;
                }
                else
                {
                    LblError.ForeColor = Color.Red;
                    LblError.Text = "Sign on is not successful! Try again Later";
                    new Utility().MessageBox(LblError.Text, this.Page);
                    LnkLogin.Text = "";
                    LnkLogin.ForeColor = Color.Blue;
                    LnkLogin.Visible = false;
                    logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);
                    return;
                }
            }
            else
            {
                //sign on exist
                LblError.ForeColor = Color.Red;
                LblError.Text = "You have already sign up. Your Matric Number is " + So.MatricNumber;
                LnkLogin.Text = "Click here to Login";
                new Utility().MessageBox(LblError.Text, this.Page);
                LnkLogin.ForeColor = Color.Blue;
                LnkLogin.Visible = true;
                logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);

            }
            //Session["StudentSignOn"] = So;

        }
    }

}
