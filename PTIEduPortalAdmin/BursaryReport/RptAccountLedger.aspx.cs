
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;
using System.Drawing;
using System.Data.SqlClient;

public partial class RptAccountLedger : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        LblError.Text = "";

        if (!Page.IsPostBack)
        {
            if (Session["ErrorMessage"] != null) LblError.Text = Session["ErrorMessage"].ToString();
        }
    }
    //protected override void Render(HtmlTextWriter writer)
    //{
    //    this.Page.ClientScript.RegisterClientScriptInclude("jQuery",
    //        ResolveUrl("~/admin/jquery-1.5.1.js"));
    //    this.Page.ClientScript.RegisterClientScriptInclude("jQuery.ui.core",
    //        ResolveUrl("~/admin/jquery.ui.core.js"));
    //    this.Page.ClientScript.RegisterClientScriptInclude("jQuery.ui.widget",
    //        ResolveUrl("~/admin/jquery.ui.widget.js"));
    //    this.Page.ClientScript.RegisterClientScriptInclude("jQuery.ui.datepicker",
    //        ResolveUrl("~/admin/jquery.ui.datepicker.js"));
    //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),
    //        "startup", "<script>$(function() { $( \"#" + txtDateFrom.ClientID + "\" ).datepicker({ changeMonth: true, changeYear: true}); });</script>");
    //    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(),
    //        "startup1", "<script>$(function() { $( \"#" + txtDateTo.ClientID + "\" ).datepicker({ changeMonth: true, changeYear: true}); });</script>");
    //    base.Render(writer);
    //}

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        DateTime DtFrom;
        DateTime DtTo;
        if (string.IsNullOrEmpty(txtDateFrom.Text)==false && !DateTime.TryParse(txtDateFrom.Text, out DtFrom))
        {
            LblError.Text = "Invalid 'Date From' Entered";
            return;
        }
        if (string.IsNullOrEmpty(txtDateTo.Text) == false && !DateTime.TryParse(txtDateTo.Text, out DtTo))
        {
            LblError.Text = "Invalid 'Date To' Entered";
            return;
        }

        if (!ChkAllProg.Checked)
        {
            if (string.IsNullOrEmpty(CmbCourse.Text)) 
            {
                LblError.Text = "Select the Appropriate Programme";
                return;
            }
        }
        if (!ChkAllFaculty.Checked)
        {
            if (string.IsNullOrEmpty(Faculty.Text))
            {
                LblError.Text = "Select the Appropriate Faculty";
                return;
            }
        }
        if (!ChkAllLevel.Checked && cmbStudentLevel.SelectedIndex == 0)
        {
            LblError.Text = "Select the level to filter or check all level";
            return;
        }

        LblError.Text = "";
        Session["ProgrammeCategory"] = (CmbProgrammeCategory.Text == "All Category") ? "All" : CmbProgrammeCategory.Text;
        if (ChkAllProg.Checked)
        {
            Session["Programme"] = "All";
        }
        else
        {
            Session["ProgrammeText"] = CmbCourse.SelectedItem.Text;
            Session["Programme"] = CmbCourse.SelectedValue;
        }
        if (ChkAllFaculty.Checked)
        {
            Session["Faculty"] = "All";
        }
        else
        {
            Session["Faculty"] = Faculty.Text;
        }
        if (ChkAllDepartment.Checked)
        {
            Session["Department"] = "All";
        }
        else
        {
            Session["Department"] = CmbDepartment.SelectedValue;
        }
        Session["FilterLevel"] = (ChkAllLevel.Checked) ? "All" : cmbStudentLevel.Text;
        Session["Session"] = CmbSession.Text;
        Session["ReportFormat"] = cmbReportOption.SelectedValue;
        Session["DtFrom"] = txtDateFrom.Text.Trim();// DtFrom;
        Session["DtTo"] = txtDateTo.Text.Trim();// DtTo;
        Response.Redirect("~/BursaryReport/RptAccountLedgerShow.aspx");

    }

    protected void CmbProgrammeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterByCategory();
        ChkAllProg.Checked = ChkAllDepartment.Checked = ChkAllFaculty.Checked = false;
        CmbCourse.Enabled = CmbDepartment.Enabled = Faculty.Enabled = true;
        if (Faculty.Items.Count > 0)
        {
            Faculty.SelectedIndex = (Faculty.Items[0].Selected == false) ? 0 : Faculty.SelectedIndex; ;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any faculty. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0;
            return;
        }

        if (CmbDepartment.Items.Count > 0)
        {
            CmbDepartment.SelectedIndex = (CmbDepartment.Items[0].Selected == false) ? 0 : CmbDepartment.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any department. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0;
            return;
        }

        if (CmbCourse.Items.Count > 0)
        {
            CmbCourse.SelectedIndex = (CmbCourse.Items[0].Selected == false) ? 0 : CmbCourse.SelectedIndex; ;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any programme course. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0;
            return;
        }

    }
    protected void Faculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterByCategoryAndFaculty();
        ChkAllProg.Checked = ChkAllDepartment.Checked = false;
        CmbCourse.Enabled = CmbDepartment.Enabled = true;
        if (CmbDepartment.Items.Count > 0)
        {
            CmbDepartment.SelectedIndex = (CmbDepartment.Items[0].Selected == false) ? 0 : CmbDepartment.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + Faculty.Text + " registered for any department. Please review filter";
            FilterByCategory();
            return;
        }

        if (CmbCourse.Items.Count > 0)
        {
            CmbCourse.SelectedIndex = (CmbCourse.Items[0].Selected == false) ? 0 : CmbCourse.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + Faculty.Text + " registered for any department. Please review filter";
            FilterByCategory();
            return;
        }

    }
    protected void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterByCategoryFacultyAndDepartment();
        ChkAllProg.Checked = false;
        CmbCourse.Enabled = true;
        if (CmbCourse.Items.Count > 0)
        {
            CmbCourse.SelectedIndex = (CmbCourse.Items[0].Selected == false) ? 0 : CmbCourse.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + CmbDepartment.Text + " registered for any programme course. Please review filter";
            ReFilterByCategoryAndFaculty();
            return;
        }
    }

    protected void ChkAllProg_CheckedChanged(object sender, EventArgs e)
    {
        //RefreshDisplayColumn();

        if (ChkAllProg.Checked)
        {
            CmbCourse.Enabled = false;
        }
        else
        {
            CmbCourse.Enabled = true;
        }
    }
    protected void ChkAllFaculty_CheckedChanged(object sender, EventArgs e)
    {
        //RefreshDisplayColumn();

        if (ChkAllFaculty.Checked)
        {
            Faculty.Enabled = false;
        }
        else
        {
            Faculty.Enabled = true;
        }
    }
    protected void ChkAllDepartment_CheckedChanged(object sender, EventArgs e)
    {
        //        RefreshDisplayColumn();

        if (ChkAllDepartment.Checked)
        {
            CmbDepartment.Enabled = false;
        }
        else
        {
            CmbDepartment.Enabled = true;
        }
    }
    protected void ChkAllLevel_CheckedChanged(object sender, EventArgs e)
    {

        if (ChkAllLevel.Checked)
        {
            cmbStudentLevel.Enabled = false;
        }
        else
        {
            cmbStudentLevel.Enabled = true;
        }

    }

    //protected bool CanDisplayColumn()
    //{
    //    bool canBool = true;
    //    int selected = 0;
    //    for (int i = 0; i < CheckBoxDisplayColumn.Items.Count; i++)
    //    {
    //        selected = (CheckBoxDisplayColumn.Items[i].Selected == true) ? selected + 1 : selected;
    //    }
    //    canBool = (selected > 7) ? false : true;
    //    return canBool;
    //}

    protected void FilterByCategory()
    {
        if (CmbProgrammeCategory.SelectedIndex > 0)
        {
            Faculty.Items.Clear();
            CmbDepartment.Items.Clear();
            CmbCourse.Items.Clear();

            Faculty.DataSourceID = ObjFacultyByCategory.ID;
            CmbDepartment.DataSourceID = ObjectDepartmentByCategory.ID;
            CmbCourse.DataSourceID = ObjCoursesByCategory.ID;

            Faculty.DataBind();
            CmbDepartment.DataBind();
            CmbCourse.DataBind();
        }
        else
        {
            Faculty.Items.Clear();
            CmbDepartment.Items.Clear();
            CmbCourse.Items.Clear();

            Faculty.DataSourceID = ObjFaculty.ID;
            CmbDepartment.DataSourceID = ObjectDepartment.ID;
            CmbCourse.DataSourceID = ObjCourses.ID;

            Faculty.DataBind();
            CmbDepartment.DataBind();
            CmbCourse.DataBind();
        }
    }
    protected void FilterByCategoryAndFaculty()
    {
        CmbDepartment.Items.Clear();
        CmbCourse.Items.Clear();

        CmbDepartment.DataSourceID = ObjectDepartmentByCategoryAndFaculty.ID;
        CmbCourse.DataSourceID = ObjCoursesByCategoryAndFaculty.ID;

        CmbDepartment.DataBind();
        CmbCourse.DataBind();
    }
    protected void ReFilterByCategoryAndFaculty()
    {
        CmbDepartment.Items.Clear();
        CmbCourse.Items.Clear();

        CmbDepartment.DataSourceID = ObjectDepartmentByCategoryAndFaculty.ID;
        CmbCourse.DataSourceID = ObjCoursesByCategoryFacultyAndDepartment.ID;

        CmbDepartment.DataBind();
        CmbCourse.DataBind();

    }
    protected void FilterByCategoryFacultyAndDepartment()
    {
        CmbCourse.Items.Clear();
        CmbCourse.DataSourceID = ObjCoursesByCategoryFacultyAndDepartment.ID;
        CmbCourse.DataBind();
    }
}
