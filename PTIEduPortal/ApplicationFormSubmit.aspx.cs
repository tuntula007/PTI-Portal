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

public partial class ApplicationFormSubmit : System.Web.UI.Page
{
    string FormNumber = "";

    ApplicantSignOn ASo = new ApplicantSignOn();
    Applicants applicant;
    protected void Page_Load(object sender, EventArgs e)
    {
        ASo = (ApplicantSignOn)Session["ApplicantSignOn"];
        if (ASo == null)
        {
            //session expired, complain and go back to session expired page
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }
        else
        {
            FormNumber = ASo.FormNumber;
        }
        Session["FormNumber"] = FormNumber;
        //determine semester

        if (Page.IsPostBack == false)
        {


            //get students detail
            lblCurrentSession.Text = new ApplicantSignOnBusiness().getCurrentApplicationSession() + " Session";
            ApplicantsBusiness sb = new ApplicantsBusiness();
            applicant = sb.GetApplicantsByFormNo(FormNumber);
            Session["Applicants"] = applicant;

            //load student biodata
            lblsch.Text = applicant.Programme.ToUpper() + "(" + applicant.ModeOfStudy.ToUpper ()+ ")";
            lblcourse.Text = applicant.ModeOfStudy;
            lblEntryMode.Text = applicant.EntryMode;
            lblschoice.Text = applicant.CourseofStudy2;
            lblyear.Text = applicant.CourseofStudy1;

            lblname.Text = " - " + applicant.FormNumber;
            lblMess.Text = "Congratulations " + applicant.Title + " " + applicant.OtherNames.ToUpper() + ", your application is successful! Kindly use the buttons below to print or review your form.";


        } //end if postback



    }


}
