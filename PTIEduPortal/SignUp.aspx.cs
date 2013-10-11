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
public partial class SignUp : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSignOn_Click(object sender, EventArgs e)
    {
        LblError.Text = "";
        SignOnBusiness Sob = new SignOnBusiness();
        StudentSignOn So = new StudentSignOn();
        //Verifying Data
        PanelError.Visible = true;
        if (string.IsNullOrEmpty(txtMatricNo.Text))
        {
            LblError.Text = "Enter your Matric Number to proceed";
            txtMatricNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPassWord.Text) || string.IsNullOrEmpty(txtPasswordC.Text))
        {
            LblError.Text = "Password Must be Entered";
            txtPassWord.Focus();
            return;
        }
        if (txtPassWord.Text.Trim() != txtPasswordC.Text.Trim())
        {
            LblError.Text = "Password and Confirm Password NOT MATCH";
            txtPassWord.Focus();
            return;
        }
        //check if matric number in students table
        if (StudentsBusiness.isMatricNumberExists(txtMatricNo.Text.Trim()) == false)
        {
            LblError.ForeColor = Color.Red;
            LblError.Text = "Your sign on is denied due to WRONG matric number. Verify your matric number or contact your School officer.";
            LnkLogin.Text = "";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = false;
            logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);
            return;
        }

        So = Sob.VerifySignOn(txtMatricNo.Text.Trim());
        if (!So.Verified)
        {
            //Not Admitted At All
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            LblError.Text = "Invalid Sign Up Matric! Your Matric Number Does Not Exist";
            logger.Warn("Invalid Sign Up Matric! Your Matric Number Does Not Exist");
            return;
        }
        else
        {
            if (So.Status == 0)
            {
                if (string.IsNullOrEmpty(So.MatricNumber) == false)
                {
                    // Update Sign On Info with New Password, Email,PhoneNumber
                    So.MatricNumber = txtMatricNo.Text.Trim();
                    So.Password = txtPassWord.Text.Trim();
                    So.Phone = txtPhone.Text.Trim();
                    So.Email = txtEmail.Text.Trim();
                    bool inserted = Sob.UpdateSignOn(So);
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
                        new Utility().MessageBox("Congrats! Your Sign on is successfully! Just logon with your new password to continue...", this.Page);
                        logger.Info(txtMatricNo.Text.Trim() + " - Sign on Inserted successfully");

                    }
                    else
                    {
                        LblError.ForeColor = Color.Red;
                        LblError.Text = "Sign on is not successful! Try again Later";
                        LnkLogin.Text = "";
                        LnkLogin.ForeColor = Color.Blue;
                        LnkLogin.Visible = false;
                        logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);

                    }
                }
                else
                {

                }

            }
            else
            {
                //sign on exist
                LblError.ForeColor = Color.Red;
                LblError.Text = "You cannot Sign On with this matric number.";
                LnkLogin.Text = "Click here to Login";
                LnkLogin.ForeColor = Color.Blue;
                LnkLogin.Visible = true;
                logger.Warn(txtMatricNo.Text.Trim() + " - " + LblError.Text);
            }
            Session["StudentSignOn"] = So;
        }

    }

}
