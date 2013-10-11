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

public partial class ViewAdmittedStudentsSpecial : System.Web.UI.Page
{
    bool alreadyPaidAdmissionLetterfees = false;

    protected void Page_Load(object sender, EventArgs e)
    {ApplicantSignOn Ao=new ApplicantSignOn();
        if (!Page.IsPostBack)
        {
            //RefreshForm();
            //LoadCourse("Certificate");
            Session["CHANGEOFCOURSE"] = null;
            Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
            this.txtSearch.Text = Ao.FormNumber;
        }
        else
        {
            Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
            this.txtSearch.Text = Ao.FormNumber;
        }
        //RefreshForm();
    }
    private void RefreshForm()
    {
        GridView2.Visible = true;
        if (string.IsNullOrEmpty(txtSearch.Text.Trim()) == true)
        {
            GridView2.Visible = false;
            LblStud.Text = "";
            //LblError.Text = "Please enter your application form number";


            LblError.Text = "SORY! THIS CANDIDATE HAS NO ADMISSION INFORMATION";
            Response.Write("SORY! THIS CANDIDATE HAS NO ADMISSION INFORMATION");
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        if (txtSearch.Text.ToUpper().Trim().StartsWith("PTI") == false)
        {
            GridView2.Visible = false;
            LblStud.Text = "";
            LblError.Text = "Please enter your application form number in the correct format(PTI/XXXXX/12" + ApplicantsBusiness.ActiveSession().Right(2) + ")";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }

        LblError.Visible = false;
        PanelGrid.Visible = true;
        LblError.Visible = false;
        ObjGrid.SelectParameters.Clear();
        ObjGrid.TypeName = "CybSoft.EduPortal.Business.ApplicantsBusiness";
        ObjGrid.SelectMethod = "GetAdmittedStudentsByFormNo";
        string Param = txtSearch.Text.Trim();
        ObjGrid.SelectParameters.Add("Param", Param);
        LblError.Text = "";

        try
        {
            ObjGrid.DataBind();
            PanelGrid.Visible = true;
            GridView2.DataBind();
            if (GridView2.Rows.Count < 1)
            {
                Label2.Text= "CONGRATULATIONS !";
                //MessageBox("No Record Marches the selected criteria");
                //PanelGrid.Visible = false;
                LblError.ForeColor = Color.Red;
                GridView2.Visible = false;
                LblError.Text = "NO ADMISSION RECORD FOUND FOR " + Param.ToUpper();
                new Utility().MessageBox(LblError.Text, this.Page);
                LblError.Visible = true;
                LblStud.Text = "";
                LblStud.Visible = false;
                ResetField();
                return;
            }
            bool IsAdmitted = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("IsAdmitted")).Value.Trim() == "0" ? false : true;
            string Remarks = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("Remarks")).Value.Trim();
            string AdmittedStatus = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("AdmittedStatus")).Value.Trim();
            string AppNo = ((LinkButton)GridView2.Rows[0].Cells[0].FindControl("LnkShoww")).Text.Trim();
            if (IsAdmitted == false && AdmittedStatus.ToLower() =="s")
            {
                Session["CHANGEOFCOURSE"] = null;
                LblError.ForeColor = Color.Red;
                //LblError.Text = "Sorry you did not meet the minimum requirement for admission to the programme of study you earlier applied for. You may consider applying for: " + ((Remarks.Length > 0) ? Remarks : GridView2.Rows[0].Cells[5].Text);
                LblError.Text = "Sorry you did not meet the minimum requirement for admission to the programme of study you earlier applied for. Another programm of study will be suggested for you soon.";
                Session["CHANGEOFCOURSE"] = AppNo;
                //LblStud.Text = "If you are OK with the programme of study suggested for you, Click <b><a href='ChangeOfCourse.aspx'>here</a></b> to ACCEPT";
                LblStud.Text = "Check back to try again.";
                new Utility().MessageBox(LblError.Text, this.Page);
                GridView2.Visible = false;
                LblError.Visible = true;
                LblStud.Visible = true;
                ResetField();
                return;
            }
            if (IsAdmitted == false && AdmittedStatus.ToLower() == "p")
            {
                Session["CHANGEOFCOURSE"] = null;
                LblError.ForeColor = Color.Red;
                LblError.Text = "Your application to change to another programme of study is still pending. Check back at a latter date.";
                LblStud.Text = "";
                new Utility().MessageBox(LblError.Text, this.Page);
                GridView2.Visible = false;
                LblError.Visible = true;
                ResetField();
                return;
            }
            LblError.ForeColor = Color.Blue;
            LblError.Text = "";
            LblError.Visible = true;
            //LblStud.ForeColor = Color.Blue;
            LblStud.Text = "You have been admitted. Click on your Application Form Number in the grid below to Print your Notification Letter.";
            LblStud.Visible = true;
        }
        catch (Exception exx)
        {

            //throw;
        }
        ResetField();
    }
    private void RefreshForm(string Surname, string SecondName, string PhoneNumber, string Email)
    {
        GridView2.Visible = true;
        if (string.IsNullOrEmpty(SurnameText.Text) == true)
        {
            GridView2.Visible = false;
            LblStud.Text = "";
            LblError.Text = "Please enter your Surname";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        if (string.IsNullOrEmpty(EmailText.Text + PhoneText.Text) == true)
        {
            GridView2.Visible = false;
            LblStud.Text = "";
            LblError.Text = "Please enter either your email or phone number";
            LblError.ForeColor = Color.Red;
            LblError.Visible = true;
            new Utility().MessageBox(LblError.Text, this.Page);
            return;
        }
        LblError.Visible = false;
        PanelGrid.Visible = true;
        LblError.Visible = false;
        ObjGrid.SelectParameters.Clear();
        ObjGrid.TypeName = "CybSoft.EduPortal.Business.ApplicantsBusiness";
        ObjGrid.SelectMethod = "GetAdmittedStudentByBiodata";
        ObjGrid.SelectParameters.Add("Surname", Surname.Trim());
        ObjGrid.SelectParameters.Add("SecondName", SecondName.Trim());
        ObjGrid.SelectParameters.Add("PhoneNumber", PhoneNumber.Trim());
        ObjGrid.SelectParameters.Add("Email", Email.Trim());
        LblError.Text = "";

        try
        {
            ObjGrid.DataBind();
            PanelGrid.Visible = true;
            GridView2.DataBind();
            if (GridView2.Rows.Count < 1)
            {
                //MessageBox("No Record Marches the selected criteria");
                //PanelGrid.Visible = false;
                GridView2.Visible = false;
                LblError.ForeColor = Color.Red;
                LblError.Text = "NO ADMISSION RECORD FOUND FOR " + (Surname + " " + SecondName).ToUpper();
                new Utility().MessageBox(LblError.Text, this.Page);
                LblError.Visible = true;
                LblStud.Text = "";
                LblStud.Visible = false;
                ResetField();
                return;
            }
            bool IsAdmitted = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("IsAdmitted")).Value.Trim() == "0" ? false : true;
            string Remarks = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("Remarks")).Value.Trim();
            string AdmittedStatus = ((HiddenField)GridView2.Rows[0].Cells[0].FindControl("AdmittedStatus")).Value.Trim();
            string AppNo = ((LinkButton)GridView2.Rows[0].Cells[0].FindControl("LnkShoww")).Text.Trim();
            if (IsAdmitted == false && AdmittedStatus.ToLower() == "s")
            {
                GridView2.Visible = false;
                Session["CHANGEOFCOURSE"] = null;
                LblError.ForeColor = Color.Red;
                //LblError.Text = "Sorry you did not meet the minimum requirement for admission to the programme of study you earlier applied for. You may consider applying for: " + ((Remarks.Length > 0) ? Remarks : GridView2.Rows[0].Cells[5].Text);
                LblError.Text = "Sorry you did not meet the minimum requirement for admission to the programme of study you earlier applied for. Another programm of study will be suggested for you soon.";
                Session["CHANGEOFCOURSE"] = AppNo;
                //LblStud.Text = "If you are OK with the programme of study suggested for you, Click <b><a href='ChangeOfCourse.aspx'>here</a></b> to ACCEPT";
                LblStud.Text = "Check back to try again.";
                new Utility().MessageBox(LblError.Text, this.Page);
                GridView2.Visible = false;
                LblError.Visible = true;
                LblStud.Visible = true;
                ResetField();
                return;
            }
            if (IsAdmitted == false && AdmittedStatus.ToLower() == "p")
            {
                Session["CHANGEOFCOURSE"] = null;
                LblError.ForeColor = Color.Red;
                LblError.Text = "Your application to change to another programme of study is still pending. Check back at a latter date.";
                LblStud.Text = "";
                new Utility().MessageBox(LblError.Text, this.Page);
                GridView2.Visible = false;
                LblError.Visible = true;
                ResetField();
                return;
            }

            LblError.ForeColor = Color.Blue;
            LblError.Text = "";
            LblError.Visible = true;
            //LblStud.ForeColor = Color.Blue;
            LblStud.Text = "You have been admitted. Click on your Application Form Number in the grid below to Print your Notification Letter.";
            LblStud.Visible = true;
        }
        catch (Exception exx)
        {

            //throw;
        }
        ResetField();
    }
    private void ResetField()
    {
        txtSearch.Text = "";
        PhoneText.Text = "";
        EmailText.Text = "";
        SurnameText.Text = "";
        SecondNameText.Text = "";
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton Btt = (LinkButton)GridView2.Rows[GridView2.SelectedIndex].FindControl("LnkShoww");
        Session["RegNo"] = Btt.Text.Trim();
        string FormNo = Btt.Text.Trim();
        Response.Redirect("FEES2012Freshstudents.pdf");
        //Response.Redirect("RptPrintAdmissionLetter.aspx", false);

    }
    protected void LnkShoww_Click(object sender, EventArgs e)
    {
        //LinkButton Btt = (LinkButton)GridView2.Rows[GridView2.SelectedIndex].FindControl("LnkShoww");
        //Session["RegNo"] = "PTI/00001/13";          // Btt.Text;
        //Response.Redirect("RptPrintAdmissionLetter.aspx"); AdmissionLetterParam

        alreadyPaidAdmissionLetterfees = CybSoft.EduPortal.Business.SignOnBusiness.VerifyAdmissionLetterFeeIsPaid (txtSearch.Text.Trim());
        if (alreadyPaidAdmissionLetterfees)
        {
            Session["RegNo"] = txtSearch.Text.Trim();
          
            Response.Redirect("RptPrintAdmissionLetter.aspx", true);

        }
        if (!(alreadyPaidAdmissionLetterfees))
        {
             Response.Redirect("AdmissionLetterParam.aspx",true );
        }
    }
    protected void LnkShowFees_Click(object sender, EventArgs e)
    {
        

            Response.Redirect("FEES2012Freshstudents.pdf",true );
            return;
 
    }
    //private bool VerifyAdmissionLetterFeeIsPaid(string p)
    //{
    //    throw new NotImplementedException();
    //}
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        RefreshForm();
    }
    protected void BtnSearchBiodata_Click(object sender, EventArgs e)
    {
        RefreshForm(SurnameText.Text, SecondNameText.Text, PhoneText.Text, EmailText.Text);
    }

    protected void lkbtnAdvanceSearch_Click(object sender, EventArgs e)
    {
        AdvanceSearch.Visible = true;
        SimpleSearch.Visible = false;
    }
    protected void lkbtnSimpleSearch_Click(object sender, EventArgs e)
    {
        AdvanceSearch.Visible = false;
        SimpleSearch.Visible = true;
    }
}
