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




public partial class StudentLogin : System.Web.UI.Page
{
    //typeof(StudentLogin)

    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SignOnBusiness Sob = new SignOnBusiness();
    StudentSignOn So = new StudentSignOn();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MessageBox"] != null)
        {
            if (string.IsNullOrEmpty(Session["MessageBox"].ToString()) == false) MessageBox(Session["MessageBox"].ToString());
        }
        if (Session["StudentSignOn"] != null)
        {
            So = (StudentSignOn)Session["StudentSignOn"];
            txtMatricNo.Text = (string.IsNullOrEmpty(So.MatricNumber)) ? "" : So.MatricNumber;
        }
        Session.Contents.Clear();
        if (Request.IsLocal != true)
        {
            logger.Info("Visiting from " + IpAddress());
        }
        if (!IsPostBack)
        {
            forgetPassword.Visible = false;
        }

    }
    public string IpAddress()
    {
        string strIpAddress;
        strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (strIpAddress == null)
        {
            strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return strIpAddress;
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        //validate entrie
        PanelError.Visible = false;
        if (string.IsNullOrEmpty(txtMatricNo.Text) == true)
        {
            PanelError.Visible = true;
            LblError.Text = "Please enter Your Matric Number.";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            txtMatricNo.Focus();
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        if (txtMatricNo.Text.ToUpper().StartsWith("E0") == false)
        {
            PanelError.Visible = true;
            LblError.Text = "Matric Number Format Incorrect. Must be in E0 i.e. Letter E and Zero";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            txtMatricNo.Focus();
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        if (string.IsNullOrEmpty(txtPassword.Text) == true)
        {
            PanelError.Visible = true;
            LblError.Text = "Please enter Your Password.";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            txtMatricNo.Focus();
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        try
        {
            //verify signon
            PanelError.Visible = true;
            So = Sob.VerifySignOn(txtMatricNo.Text, txtPassword.Text);
            if (!So.Verified)
            {
                LblError.ForeColor = Color.Red;
                LblError.Visible = true;
                forgetPassword.Visible = true;
                new Utility().MessageBox("Invalid Login Parameter! Kindly Click on Sign UP link if you have NOT done so. If you are newly admitted student, Sign up and pay your fees. Also note that Password are CASE sensitive",this.Page);
                LblError.Text = "Invalid Login Parameter! Kindly Click on Sign UP link above if you have NOT done so.<br />If you are newly admitted student, Click <a href='SignOn.aspx'>HERE<a/> to sign up and pay your fees. <br /> Also note that Password are CASE sensitive.";
                logger.Warn("Invalid Login Parameter! Kindly Click on Sign UP link above if you have NOT done so.");
                return;
            }
            if (So.Verified && So.Status == 0)
            {
                LblError.ForeColor = Color.Red;
                LblError.Visible = true;
                LblError.Text = "You cannot Login as a Student till you sign up. <br />If you are newly admitted student, Click <a href='SignOn.aspx'>HERE<a/> to sign up and pay your fees";
                //<br />If you are old/returning student who is new to the Portal, please Sign Up<a href='SignUp.aspx'>Here</a>
                logger.Warn("Kindly Login as an applicant to print your admission letter before coming to this page");
                return;
            }

            LblError.Visible = false;
            Session["StudentSignOn"] = So;
            string sss = So.MatricNumber;

            //Response.Redirect("StudentControlCenter.aspx");
            logger.Info(So.MatricNumber + " - Redirecting Students Control Center");
            Session["fromLoginToControl"] = true;
            Response.Redirect("StudentControlCenter.aspx?MatricNumber=" + So.MatricNumber);
        }
        catch (Exception ex)
        {

            logger.Error(So.MatricNumber + " - " + ex.Message);
        }
    }
    public void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ");</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }

}
