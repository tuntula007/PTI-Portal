using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using System.Data;
public partial class ApplicantProfile : System.Web.UI.Page
{
    string FormNumber = "";
    //string Semester = "Second";
    //StudentSignOn So = new StudentSignOn();
    ApplicantSignOn appSo = new ApplicantSignOn();
    Applicants applicant;    
    protected void Page_Load(object sender, EventArgs e)
    {
      
         
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
             return;
        }
        else
        {
         appSo = (ApplicantSignOn) Session["ApplicantSignOn"];
            FormNumber = appSo.FormNumber;
        }
        Session["FormNumber"] = FormNumber;

        if (Page.IsPostBack == false)
        {

            //get students detail
            StudentsBusiness sb = new StudentsBusiness();
            ApplicantsBusiness ab = new ApplicantsBusiness();
            applicant = ab.GetApplicantsByFormNo(FormNumber);
            Session["Applicants"] = applicant;

            ListItem li;

            //li = ddlSmod.Items.FindByValue(appSo.ModeOfStudy);
            //if (li != null)
            //{
            //    ddlSmod.ClearSelection();
            //    li.Selected = true;
            //}

            txtEmailAdd.Text = applicant.Email;
            txtSurname.Text = applicant .Surname;
            txtOthernames.Text = applicant.OtherNames;

            lblVoln.InnerHtml = applicant.RegNo;
            LoadCourse(appSo.Programme);
            //li = ddlProg.Items.FindByValue(appSo.Programme);
            //if (li != null)
            //{
            //    //ddlProg.ClearSelection();
            //    li.Selected = true;
            //}
            //li = ddlFirstC.Items.FindByValue(applicant.FirstDepartmentID.ToString());
            //if (li != null)
            //{
            //    ddlFirstC.ClearSelection();
            //    li.Selected = true;
            //}
            //li = ddlFirstC.Items.FindByValue(applicant.FirstDepartmentID.ToString());
            StateLGADefault(applicant.State.ToUpper(), applicant.LocalGovernmentArea.ToUpper());

        }
    }
    private void LoadCourse(string prog)
    {
        string progCode;
        progCode = prog;
        if (prog.ToUpper() == "DIPLOMA")
        {
            progCode = "D";
        }
        if (prog.ToUpper() == "CERTIFICATE")
        {
            progCode = "C";
        }
        //DataSet ds = ApplicantsBusiness.getAddmissionCourses(prog);
        //ddlFirstC.DataSource = ds;
        //ddlFirstC.DataSource = ds;
        //ddlFirstC.DataTextField = "Course";
        //ddlFirstC.DataValueField = "Srn";
        //cmb2ndChoice.DataTextField = "Course";
        //cmb2ndChoice.DataValueField = "Srn";
        //cmb2ndChoice.DataBind();
        //ddlFirstC.DataBind();
        //ddlFirstC.Items.Insert(0, new ListItem("--Select--", "0"));
        //cmb2ndChoice.Items.Insert(0, new ListItem("--Select--", "0"));


    }

    private void StateLGADefault(string state, string lga)
    {
        ListItem li;

        li = ddlState.Items.FindByText(state);
        if (li != null)
        {
            ddlState.ClearSelection();
            li.Selected = true;
        }

        LoadLGA(state);
        li = ddlLga.Items.FindByText(lga);
        if (li != null)
        {
            ddlLga.ClearSelection();
            li.Selected = true;
        }
    }
    private void LoadLGA(string state)
    {
        ddlLga.DataSource = ApplicantsBusiness.GetLGA(state);
        ddlLga.DataTextField = "NAME";
        ddlLga.DataValueField = "NAME";
        ddlLga.DataBind();
        ddlLga.Items.Insert(0, new ListItem("--Select--", ""));
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLGA(ddlState.SelectedValue);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // get the vol number 
        // pass the values
        string VolSerial = "";
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }
        else
        {
            appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
            FormNumber = appSo.FormNumber;
        }
        int lind = lblVoln.InnerText.LastIndexOf("/");
        VolSerial = lblVoln.InnerText.Substring(lind + 1);
        try
        {

            ApplicantsBusiness.UpdateProfile(txtSurname .Text ,txtOthernames .Text,VolSerial, "",0, ddlState.SelectedItem.Text, ddlLga.SelectedItem.Text, txtEmailAdd.Text, "", FormNumber);
            lblMess.InnerHtml = "Update Succesful";
        }
        catch (Exception ex)
        {
            lblMess.InnerHtml = "Error Occured ";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApplicantControlCenter.aspx");
        return;
    }
}
