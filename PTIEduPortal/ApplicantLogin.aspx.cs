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

public partial class ApplicantLogin : System.Web.UI.Page
{
    
    ApplicantSignOnBusiness Aob = new ApplicantSignOnBusiness();
    ApplicantSignOn Ao = new ApplicantSignOn();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        //new Utility().MessageBox("Page is currently under construction, Please try again at a latter date", ResolveUrl("Index.html"), this.Page);
        //return;

        Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
        if (Ao != null)
        {
            txtUserName.Text = Ao.UserName;
            //txtPassword.Text = Ao.Password;
        }
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        Session.Contents.Clear();
        PanelError.Visible = true;
        if (string.IsNullOrEmpty(txtUserName.Text) == true)
        {
            PanelError.Visible = true;
            LblError.Text = "Please enter Your Username.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            txtUserName.Focus();
            return;
        }
        //if (txtUserName.Text.ToUpper().StartsWith("UI/DLC/UGD") == false)
        //{
        //    PanelError.Visible = true;
        //    LblError.Text = "Matric Number Format Incorrect. Must be in UID/DLC/UGD";
        //    LblError.ForeColor = Color.Red;
        //    LblError.Visible = true;
        //    txtUserName.Focus();
        //    new Utility().MessageBox(LblError.Text, this.Page);
        //    return;
        //}
        if (string.IsNullOrEmpty(txtPassword.Text) == true)
        {
            PanelError.Visible = true;
            LblError.Text = "Please enter Your Password.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            txtPassword.Focus();
            return;
        }

        Ao = Aob.VerifySignOn(txtUserName.Text, txtPassword.Text);
        if (!Ao.Verified)
        {
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            LblError.Text = "Invalid Login Parameter! Kindly Click on Sign UP link if you have NOT done so. Also note that passwords are case sensitive i.e. 'PASS' is not same as 'pass'.";
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        Session["ApplicantSignOn"] = Ao;
        string sss = Ao.UserName;
        LblError.Visible = false;

        //if (Ao.VerifyFlag == 0)
        //{
        //    Response.Redirect("ApplicantEmailValidation.aspx?UserName=" + Ao.UserName);
        //    return;
        //}

        if (string.IsNullOrEmpty(Ao.ApplicationPIN))
        {
            Response.Redirect("ApplicantsSignOn.aspx");
        }
        else
        {
            //Response.Redirect("StudentControlCenter.aspx");
            Response.Redirect("ApplicantControlCenter.aspx?UserName=" + Ao.UserName);
        }
    }
}
