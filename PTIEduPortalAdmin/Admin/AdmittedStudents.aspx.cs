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
using CybSoft.EduPortal.Data;
using CybSoft.EduPortal.Business;

public partial class Admin_AdmittedStudents : System.Web.UI.Page
{
    //DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadCourse();
            LoadFaculty();
            LoadAcademicLevel();
            LoadStudyMode();
            //LoadProgramme();
            LoadAdmittedSession();
            RefreshGrid();
        }
    }
    protected void BtnShowStudents_Click(object sender, EventArgs e)
    {
        RefreshGrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        /*LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
        l.Attributes.Add("onclick", "javascript:return " +
        "confirm('Are you sure you want to delete this record " +
        DataBinder.Eval(e.Row.DataItem, "CategoryID") + "')"); */
        try
        {
            string regno = GridView1.DataKeys[e.RowIndex].Value.ToString();

            if (AdmissionBusiness.DeleteStudent(regno))
            {
                //Page.RegisterStartupScript("SS", "<script> alert(' Deleted Successfully');</script>");
                MessageBox("Congrats! Deleted Successfully!...");
            }
            RefreshGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "" + ex.Message;
        }
    }
    private void GetData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = StudentsBusiness.GetAllStudents();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                lblMessage.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblMessage.Visible = true;
                lblMessage.Text = "No data Found";

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string id = e.CommandArgument.ToString();
            Response.Redirect("AdmittedStudentProfile.aspx?Id=" + id); // to do later
        }
    }

    private void RefreshGrid()
    {
        try
        {
            DataSet ds = AdmissionBusiness.GetAdmitedStudents(ddlProgramme.SelectedValue, ddlModeOfStudy.SelectedValue, ddlCourseOfStudy.SelectedItem.Value, ddlAcademicLevel.SelectedItem.Value, ddlFaculty.SelectedItem.Value, txtRegNo.Text, ddlAcademicSession.SelectedItem.Value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                //lblMessage.Visible = false;
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.DodgerBlue;
                lblMessage.Text = "There are " + ds.Tables[0].Rows.Count.ToString() + " Students with your search";
                /*if (Session["UpdateMessage"] != null)
                {
                    Page.RegisterStartupScript("SS", "<script> alert(' Updated Successfully');</script>");
                }*/

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "No data Found";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "" + ex.Message;
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
            ddlCourseOfStudy.Items.Insert(0, new ListItem("--Choose Course of Study--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
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
            ddlProgramme.Items.Insert(0, new ListItem("--Choose Programme--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
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
            lblMessage.Text = "" + ex.Message;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        RefreshGrid();
        //rebind your gridview - GetSource(),Datasource of your GirdView
        //GridView1.DataSource = ds;
        //GridView1.DataBind();

    }

    public void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + "); window.location.href='ManageUploadedStudents.aspx'; </script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCourse(ddlProgramme.SelectedValue,ddlModeOfStudy.SelectedValue,ddlFaculty.SelectedValue);
    }

    private void LoadCourse(string programme,string mode, string faculty)
    {
        try
        {
            DataSet ds = AdmissionBusiness.getCourseOfStudy(programme,mode,faculty);
            ddlCourseOfStudy.DataSource = ds;
            ddlCourseOfStudy.DataTextField = "courseOfStudy";
            ddlCourseOfStudy.DataValueField = "courseOfStudy";
            ddlCourseOfStudy.DataBind();
            ddlCourseOfStudy.Items.Insert(0, new ListItem("--Choose Course of Study--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "" + ex.Message;
        }
    }
    protected void ddlModeOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgramme(ddlModeOfStudy.SelectedValue);
    }

        private void LoadProgramme(string mode)
    {
        try
        {
            DataSet ds = AdmissionBusiness.getProgramme(mode);
            ddlProgramme.DataSource = ds;
            ddlProgramme.DataTextField = "Programme";
            ddlProgramme.DataValueField = "Programme";
            ddlProgramme.DataBind();
            ddlProgramme.Items.Insert(0, new ListItem("--Choose Programme--", ""));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "" + ex.Message;
        }
    }
}
