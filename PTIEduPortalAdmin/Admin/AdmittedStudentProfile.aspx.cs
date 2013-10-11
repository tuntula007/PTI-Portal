using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;

public partial class Admin_AdmittedStudentProfile : System.Web.UI.Page
{
    Admission admttdStudent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                LoadCourse();
                LoadFaculty();
                LoadAcademicLevel();
                LoadAdmittedSession();
                LoadProgramme();
                LoadStudyMode();
                loadDay();
                loadYear();
                LoadMonth();
                Select();
            }
        }

    }

    private void LoadMonth()
    {
        ddlMonth.Items.Add(new ListItem("January","01"));
        ddlMonth.Items.Add(new ListItem("February","02"));
        ddlMonth.Items.Add(new ListItem("March","02"));
        ddlMonth.Items.Add(new ListItem("April","03"));
        ddlMonth.Items.Add(new ListItem("May","05"));
        ddlMonth.Items.Add(new ListItem("June","06"));
        ddlMonth.Items.Add(new ListItem("July","07"));
        ddlMonth.Items.Add(new ListItem("August","08"));
        ddlMonth.Items.Add(new ListItem("September","09"));
        ddlMonth.Items.Add(new ListItem("October","10"));
        ddlMonth.Items.Add(new ListItem("November","11"));
        ddlMonth.Items.Add(new ListItem("December","12"));
    }

    private void loadYear()
    {
        for (int i = DateTime.Now.Year - 40; i <= DateTime.Now.Year - 10; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
    }

    private void loadDay()
    {
        for (int i = 1; i <= 31; i++)
        {
            ddlDay.Items.Add("" + i.ToString());
        }
    }

    private void Select()
    {
        try
        {
            AdmissionBusiness ab = new AdmissionBusiness();
            //ListItem ly,ld,lm;

            admttdStudent = ab.GetAmmitedStudentByRegNo(Request.QueryString["id"].ToString());

            txtSurname.Text = admttdStudent.Surname;
            txtOtherNames.Text = admttdStudent.OtherNames;
            ddlProgramme.SelectedValue = admttdStudent.Programme;
            ddlModeOfStudy.SelectedValue = admttdStudent.ModeOfStudy;
            ddlCourseOfStudy.SelectedValue = admttdStudent.CourseOfStudy;
            string[] dob = admttdStudent.DateOfBirth.Split(new char[] { '/', '-' });
            loadYear();
            ddlYear.SelectedValue = dob[0].ToString().Trim();
            LoadMonth();
            ddlMonth.SelectedValue = dob[1].ToString().Trim();
            loadDay();
            ddlDay.SelectedValue = dob[2].ToString().Trim();
            //txtAddress.Text = admttdStudent.DateOfBirth;
            ddlFaculty.SelectedValue = admttdStudent.Faculty;
            ddlAcademicSession.SelectedValue = admttdStudent.SessionName;
            ddlAcademicLevel.SelectedValue = admttdStudent.AcademicLevel;
            txtEmailAdd.Text = admttdStudent.Email;
        }
        catch (Exception ex)
        {
            lblMess.InnerHtml = "Error Occured " + ex.Message;
        }


        //Surname,OtherNames,RegNo,SessionName,Programme,CourseOfStudy,Faculty
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string dob = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "-" + ddlDay.SelectedValue;
            AdmissionBusiness.UpdateProfile(Request.QueryString["id"].ToString(), txtSurname.Text, txtOtherNames.Text, ddlAcademicLevel.SelectedValue, ddlAcademicSession.SelectedValue, ddlProgramme.SelectedValue, ddlModeOfStudy.SelectedValue, ddlCourseOfStudy.SelectedValue, dob, ddlFaculty.SelectedValue,txtEmailAdd.Text);
            MessageBox("Congrats! Your Update is successfully!...");
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            //lblMess.InnerHtml = "Error Occured " + ex.Message;
            lblMessage.Text = "Error Occured " + ex.Message;
        }
    }

    private void LoadFaculty()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getFaculty();
            ddlFaculty.DataSource = ds;
            ddlFaculty.DataTextField = "FacultyName";
            ddlFaculty.DataValueField = "FacultyName";
            ddlFaculty.DataBind();
            ddlFaculty.Items.Insert(0, new ListItem("--Choose Faculty--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }

    private void LoadAcademicLevel()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getAcademicLevel();
            ddlAcademicLevel.DataSource = ds;
            ddlAcademicLevel.DataTextField = "AcademicLevel";
            ddlAcademicLevel.DataValueField = "AcademicLevel";
            ddlAcademicLevel.DataBind();
            ddlAcademicLevel.Items.Insert(0, new ListItem("--Choose Level--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }

    private void LoadAdmittedSession()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getAdmittedSession();
            ddlAcademicSession.DataSource = ds;
            ddlAcademicSession.DataTextField = "AdmittedSession";
            ddlAcademicSession.DataValueField = "AdmittedSession";
            ddlAcademicSession.DataBind();
            ddlAcademicSession.Items.Insert(0, new ListItem("", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }

    private void LoadCourse()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getAddmissionCourses();
            ddlCourseOfStudy.DataSource = ds;
            ddlCourseOfStudy.DataTextField = "courseOfStudy";
            ddlCourseOfStudy.DataValueField = "courseOfStudy";
            ddlCourseOfStudy.DataBind();
            ddlCourseOfStudy.Items.Insert(0, new ListItem("--Choose Course of Study--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }

    private void LoadStudyMode()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getModeOfStudy();
            ddlModeOfStudy.DataSource = ds;
            ddlModeOfStudy.DataTextField = "ModeOfStudy";
            ddlModeOfStudy.DataValueField = "ModeOfStudy";
            ddlModeOfStudy.DataBind();
            ddlModeOfStudy.Items.Insert(0, new ListItem("--Choose Mode of Study--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }

    private void LoadProgramme()
    {
        try
        {
            DataSet ds = AdmissionBusiness.getProgramme();
            ddlProgramme.DataSource = ds;
            ddlProgramme.DataTextField = "Programme";
            ddlProgramme.DataValueField = "Programme";
            ddlProgramme.DataBind();
            ddlProgramme.Items.Insert(0, new ListItem("--Choose Programme--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdmittedStudents.aspx");
    }

    public void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + "); window.location.href='AdmittedStudents.aspx'; </script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }
}
