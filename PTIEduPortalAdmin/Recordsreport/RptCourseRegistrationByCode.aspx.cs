
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

public partial class RptCourseRegistrationByCode : System.Web.UI.Page
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
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        if (cmbCode.SelectedIndex <1)
        {
            LblError.Text = "Select the Appropriate Course Code First";
            return;
        }

        LblError.Text = "";
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
            if (string.IsNullOrEmpty(CmbFaculty.Text))
            {
                LblError.Text = "Select the Appropriate Faculty";
                return;
            }
        }
        //if (!ChkAllLevel.Checked && cmbStudentLevel.SelectedIndex == 0)
        //{
        //    LblError.Text = "Select the level to filter or check all level";
        //    return;
        //}
        if (CanDisplayColumn() == false)
        {
            LblError.Text = "You can not add more than seven column to the report, please review the columns to display";
            return;
        }
        LblError.Text = "";
        Session["ProgrammeCategory"] = (CmbProgrammeCategory.Text == "All Category") ? "All" : CmbProgrammeCategory.Text;
        if (ChkAllProg.Checked)
        {
            Session["Programme"] = "All";
            Session["ProgrammeText"] = "All";
        }
        else
        {
            Session["Programme"] = CmbCourse.SelectedValue;
            Session["ProgrammeText"] = CmbCourse.SelectedItem.Text;
        }
        if (ChkAllFaculty.Checked)
        {
            Session["Faculty"] = "All";
        }
        else
        {
            Session["Faculty"] = CmbFaculty.Text;
        }
        if (ChkAllDepartment.Checked)
        {
            Session["Department"] = "All";
        }
        else
        {
            Session["Department"] = CmbDepartment.SelectedValue;
        }
        cmbStudentLevel.Text ="100"; //to capture level so that the programme is not  broken - not needed
        Session["FilterLevel"] = (ChkAllLevel.Checked) ? "All" : cmbStudentLevel.Text;
        Session["Session"] = CmbSession.Text;
        Session["DisplayColumns"] = CheckBoxDisplayColumn;
        Session["ReportFormat"] = cmbReportOption.SelectedValue;
        Session["CourseCode"] = cmbCode.SelectedValue;

        Response.Redirect("~/Recordsreport/RptCourseCodeRegDetailShow.aspx");
    }

    protected void CmbProgrammeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshDisplayColumn();
        FilterByCategory();
        ChkAllProg.Checked = ChkAllDepartment.Checked = ChkAllFaculty.Checked = false;
        CmbCourse.Enabled = CmbDepartment.Enabled = CmbFaculty.Enabled = true;
        if (CmbFaculty.Items.Count > 0)
        {
            CmbFaculty.SelectedIndex = 0;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any faculty. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0;
            return;
        }

        if (CmbDepartment.Items.Count > 0)
        {
            CmbDepartment.SelectedIndex = 0;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any department. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0; 
            return;
        }

        if (CmbCourse.Items.Count > 0)
        {
            CmbCourse.SelectedIndex = 0;
        }
        else
        {
            LblError.Text = "No student of " + CmbProgrammeCategory.Text + " registered for any programme course. Please review filter";
            CmbProgrammeCategory.SelectedIndex = 0;
            return;
        }

    }
    protected void CmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshDisplayColumn();
        FilterByCategoryAndFaculty();
        ChkAllProg.Checked = ChkAllDepartment.Checked = false;
        CmbCourse.Enabled = CmbDepartment.Enabled= true;
        if (CmbDepartment.Items.Count > 0)
        {
            CmbDepartment.SelectedIndex = (CmbDepartment.Items[0].Selected == false) ? 0 : CmbDepartment.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + CmbFaculty.Text + " registered for any department. Please review filter";
            FilterByCategory();
            return;
        }

        if (CmbCourse.Items.Count > 0)
        {
            CmbCourse.SelectedIndex = (CmbCourse.Items[0].Selected == false) ? 0 : CmbCourse.SelectedIndex;
        }
        else
        {
            LblError.Text = "No student of " + CmbFaculty.Text + " registered for any department. Please review filter";
            FilterByCategory();
            return;
        }

    }

    protected void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshDisplayColumn();
        FilterByCategoryFacultyAndDepartment();
        ChkAllProg.Checked= false;
        CmbCourse.Enabled = true;
        if (CmbCourse.Items.Count > 0 & CmbCourse.Items[0].Selected == false)
        {
            CmbCourse.SelectedIndex = 0;
        }
        else
        {
            LblError.Text ="No student of " + CmbDepartment.Text + " registered for any programme course. Please review filter";
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
            CmbFaculty.Enabled = false;
        }
        else
        {
            CmbFaculty.Enabled = true;
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

    protected void RefreshDisplayColumn()
    {
        //CheckBoxDisplayColumn.Items[0].Enabled=
        for (int i = 0; i < CheckBoxDisplayColumn.Items.Count; i++)
        {
            CheckBoxDisplayColumn.Items[i].Enabled = (i > 1) ? true : false;
            CheckBoxDisplayColumn.Items[i].Selected = (i > 1) ? false : true;
        }
    }
    protected bool CanDisplayColumn()
    {
        bool canBool = true;
        int selected = 0;
        for (int i = 0; i < CheckBoxDisplayColumn.Items.Count; i++)
        {
            selected = (CheckBoxDisplayColumn.Items[i].Selected == true) ? selected + 1 : selected;
        }
        canBool = (selected > 7) ? false : true;
        return canBool;
    }
    protected void FilterByCategory()
    {
        if (CmbProgrammeCategory.SelectedIndex > 0)
        {
            CmbFaculty.Items.Clear();
            CmbDepartment.Items.Clear();
            CmbCourse.Items.Clear();

            CmbFaculty.DataSourceID = ObjFacultyByCategory.ID;
            CmbDepartment.DataSourceID = ObjectDepartmentByCategoryAndFaculty.ID;
            CmbCourse.DataSourceID = ObjCoursesByCategoryFacultyAndDepartment.ID;

            CmbFaculty.DataBind();
            CmbDepartment.DataBind();
            CmbCourse.DataBind();
        }
        else
        {
            CmbFaculty.Items.Clear();
            CmbDepartment.Items.Clear();
            CmbCourse.Items.Clear();

            CmbFaculty.DataSourceID = ObjFaculty.ID;
            CmbDepartment.DataSourceID = ObjectDepartment.ID;
            CmbCourse.DataSourceID = ObjCourses.ID;

            CmbFaculty.DataBind();
            CmbDepartment.DataBind();
            CmbCourse.DataBind();
        }
    }
    protected void FilterByCategoryAndFaculty()
    {
        CmbDepartment.Items.Clear();
        CmbCourse.Items.Clear();

        CmbDepartment.DataSourceID = ObjectDepartmentByCategoryAndFaculty.ID;
        CmbCourse.DataSourceID = ObjCoursesByCategoryFacultyAndDepartment.ID; //ObjCoursesByCategoryAndFaculty.ID;

        CmbDepartment.DataBind();
        CmbCourse.DataBind();
    }
    protected void ReFilterByCategoryAndFaculty()
    {
        CmbDepartment.Items.Clear();
        CmbCourse.Items.Clear();

        CmbDepartment.DataSourceID = ObjectDepartment.ID;
        CmbCourse.DataSourceID = ObjCourses.ID;
        
        CmbDepartment.DataBind();
        CmbCourse.DataBind();

    }
    protected void FilterByCategoryFacultyAndDepartment()
    {
        CmbCourse.Items.Clear();
        CmbCourse.DataSourceID = ObjCoursesByCategoryFacultyAndDepartment.ID;
        CmbCourse.DataBind();
    }
    protected void CmbCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCourseCode.DataBind();

        //CmbCourse.Items.Clear();
        //ListItem li = new ListItem("--Select Programme--", "");
        //CmbCourse.Items.Add(li);
    }
    protected void cmbCode_DataBound(object sender, EventArgs e)
    {
        ListItem li = new ListItem("--Select--", "");
        cmbCode.Items.Insert(0, li);
    }
}
#region OldFilters
    //protected void FilterByCategory()
    //{
    //    if (CmbProgrammeCategory.SelectedIndex > 0)
    //    {
    //        CmbFaculty.Items.Clear();
    //        CmbDepartment.Items.Clear();
    //        CmbCourse.Items.Clear();

    //        ObjectDataSource objFacultyByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetCenterRegistered");
    //        ObjectDataSource objectDepartmentByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetDepartmentsRegistered");
    //        ObjectDataSource objCoursesByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetProgrammeRegistered");

    //        objFacultyByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);
    //        objectDepartmentByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);
    //        objCoursesByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);

    //        objFacultyByCategory.DataBind();
    //        objectDepartmentByCategory.DataBind();
    //        objCoursesByCategory.DataBind();

    //        ObjFaculty = objFacultyByCategory;
    //        ObjectDepartment = objectDepartmentByCategory;
    //        ObjCourses = objCoursesByCategory;

    //        ObjFaculty.DataBind();
    //        ObjectDepartment.DataBind();
    //        ObjCourses.DataBind();

    //        CmbFaculty.DataBind();
    //        CmbDepartment.DataBind();
    //        CmbCourse.DataBind();
    //    }
    //    else
    //    {
    //        CmbFaculty.Items.Clear();
    //        CmbDepartment.Items.Clear();
    //        CmbCourse.Items.Clear();

    //        ObjFaculty.SelectParameters.Clear();
    //        ObjectDepartment.SelectParameters.Clear();
    //        ObjCourses.SelectParameters.Clear();

    //        ObjFaculty.DataBind();
    //        ObjectDepartment.DataBind();
    //        ObjCourses.DataBind();

    //        CmbFaculty.DataBind();
    //        CmbDepartment.DataBind();
    //        CmbCourse.DataBind();
    //    }
    //}
    //protected void FilterByCategoryAndCenter()
    //{
    //    if (CmbProgrammeCategory.SelectedIndex > 0)
    //    {
    //        CmbDepartment.Items.Clear();
    //        CmbCourse.Items.Clear();

    //        ObjectDataSource objectDepartmentByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetDepartmentsRegistered");
    //        ObjectDataSource objCoursesByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetProgrammeRegistered");

    //        objectDepartmentByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);
    //        objectDepartmentByCategory.SelectParameters.Add("ByCenter", CmbFaculty.Text);
    //        objCoursesByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);
    //        objCoursesByCategory.SelectParameters.Add("ByCenter", CmbFaculty.Text);

    //        objectDepartmentByCategory.DataBind();
    //        objCoursesByCategory.DataBind();

    //        ObjectDepartment = objectDepartmentByCategory;
    //        ObjCourses = objCoursesByCategory;

    //        ObjectDepartment.DataBind();
    //        ObjCourses.DataBind();

    //        CmbDepartment.DataBind();
    //        CmbCourse.DataBind();
    //    }
    //    else
    //    {
    //        CmbDepartment.Items.Clear();
    //        CmbCourse.Items.Clear();

    //        ObjectDepartment.SelectParameters.Clear();
    //        ObjCourses.SelectParameters.Clear();

    //        ObjectDepartment.DataBind();
    //        ObjCourses.DataBind();

    //        CmbDepartment.DataBind();
    //        CmbCourse.DataBind();
    //    }

    //}
    //protected void FilterByCategoryCenterAndDepartment()
    //{
    //    if (CmbProgrammeCategory.SelectedIndex > 0)
    //    {
    //        CmbCourse.Items.Clear();

    //        ObjectDataSource objCoursesByCategory = new ObjectDataSource("CourseRegReportBusiness", "GetProgrammeRegistered");

    //        objCoursesByCategory.SelectParameters.Add("ByCategory", CmbProgrammeCategory.Text);
    //        objCoursesByCategory.SelectParameters.Add("ByCenter", CmbFaculty.Text);
    //        objCoursesByCategory.SelectParameters.Add("ByDepartment", CmbDepartment.Text);

    //        objCoursesByCategory.DataBind();

    //        ObjCourses = objCoursesByCategory;

    //        ObjectDepartment.DataBind();

    //        ObjCourses.DataBind();

    //        CmbCourse.DataBind();
    //    }
    //    else
    //    {
    //        CmbCourse.Items.Clear();

    //        ObjCourses.SelectParameters.Clear();

    //        ObjectDepartment.DataBind();

    //        ObjCourses.DataBind();

    //        CmbCourse.DataBind();
    //    }
    //}

#endregion