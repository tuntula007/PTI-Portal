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

public partial class ViewAdmittedStudents : System.Web.UI.Page
{
    string Programme = "";
    protected void Page_Load(object sender, EventArgs e)
    {

       //Response.Redirect("index.html");

        if (Page.IsPostBack)
        {
            //RefreshForm();
        }
        RefreshForm();
    }
    private void RefreshForm()
    {
        LblError.Visible = false;
        ObjGrid.SelectParameters.Clear();
        ObjGrid.TypeName = "CybSoft.EduPortal.Business.SignOnBusiness";
        ObjGrid.SelectMethod = "GetAdmittedStudents";
        ObjGrid.SelectParameters.Add("Programme", CmbProgramme.Text.Trim());
        ObjGrid.SelectParameters.Add("Course", CmbCourse.Text.Trim());
        ObjGrid.SelectParameters.Add("Mode", CmbMode.Text.Trim());
        ObjGrid.SelectParameters.Add("Session", CmbSession.Text.Trim());


        try
        {
            ObjGrid.DataBind();
            PanelGrid.Visible = true;
            GridView2.DataBind();
            if (GridView2.Rows.Count < 1)
            {
                //MessageBox("No Record Marches the selected criteria");
                //PanelGrid.Visible = false;
                if (string.IsNullOrEmpty(CmbProgramme.SelectedItem.Text))
                {
                    LblError.ForeColor = Color.Red;
                    LblError.Text = "";
                    LblError.Visible = true;
                    LblStud.Text = "";
                    LblStud.Visible = false;
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(CmbCourse.SelectedItem.Text) && CmbProgramme.SelectedItem.Text != "IJMB")
                    {
                        LblError.ForeColor = Color.Blue;
                        LblError.Text = "";
                        LblError.Visible = true;
                        LblStud.Text = "";
                        LblStud.Visible = false;
                        return;
                    }
                    else
                    {
                        LblError.ForeColor = Color.Red;
                        LblError.Text = "NO ADMISSION LIST UPLOADED YET FOR THE SELECTED PARAMETERS";
                        LblError.Visible = true;
                        LblStud.Text = "";
                        LblStud.Visible = false;
                        return;
                    }
                }
            }
            LblError.ForeColor = Color.Blue;
            LblError.Text = "LIST OF ADMITTED STUDENT FOR " + CmbProgramme.SelectedItem.Text.ToUpper().Trim() + "  " + CmbMode.SelectedItem.Text.ToUpper().Trim()+ " " +  CmbCourse.SelectedItem.Text.ToUpper().Trim() ;
            LblError.Visible = true; GridView2.Visible = true;
            //LblStud.ForeColor = Color.Blue;
            //LblStud.Text = "NUMBER OF STUDENTS ADMITTED: " + GridView2.Rows.Count.ToString();
            //LblStud.Visible = true;
        }
        catch (Exception exx)
        {

            string hh = exx.Message;
        }
    }
    protected void BtnViewAdmitted_Click(object sender, EventArgs e)
    {
        RefreshForm();
        
        //LblError.Visible = false;
        //ObjGrid.SelectParameters.Clear();
        //ObjGrid.TypeName = "CybSoft.EduPortal.Business.SignOnBusiness";
        //ObjGrid.SelectMethod = "GetAdmittedStudents";
        //ObjGrid.SelectParameters.Add("Programme", CmbProgramme.Text.Trim());
        //ObjGrid.SelectParameters.Add("Course", CmbCourse.Text.Trim());
        //ObjGrid.SelectParameters.Add("Mode", CmbMode.Text.Trim());

        //ObjGrid.DataBind();
        //PanelGrid.Visible = true;
        //GridView2.DataBind();
        //if (GridView2.Rows.Count < 1)
        //{
        //    //MessageBox("No Record Marches the selected criteria");
        //    //PanelGrid.Visible = false;
        //    LblError.Text = "NO ADMITTED STUDENT FOR THE SELECTED PARAMETERS";
        //    LblError.Visible = true;
        //   // return;
        //}
    }


    protected void CmbMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        CmbCourse.Items.Clear();
        CmbProgramme.Items.Clear();
        CmbProgramme.Items.Add("ND");
        CmbProgramme.Items.Add("HND");
        
        if (CmbMode.SelectedValue.Equals("Part-Time"))
        {
            CmbCourse.Items.Clear();
            CmbProgramme.Items.Clear();
            CmbProgramme.Items.Add("HND-ICE");
            CmbProgramme.Items.Add("ND-ICE");
            /*CmbCourse.Items.Add("Accountancy");
            CmbCourse.Items.Add("Public Administration");
            CmbCourse.Items.Add("Business Administration");
            CmbCourse.Items.Add("Computer Science");
            CmbCourse.Items.Add("Law");
            CmbCourse.Items.Add("Public Accounting & Auditing");
            CmbCourse.Items.Add("Statistics");
            CmbCourse.Items.Add("Office Technology and Management");*/
        }
        else
        {
            CmbCourse.Items.Add("Accountancy");
            CmbCourse.Items.Add("Architectural Technology");
            CmbCourse.Items.Add("Art and Industrial Design");
            CmbCourse.Items.Add("Business Administration");
            CmbCourse.Items.Add("Building Technology");
            CmbCourse.Items.Add("Civil Engineering");
            CmbCourse.Items.Add("Computer Science");
            CmbCourse.Items.Add("Electrical Engineering");
            CmbCourse.Items.Add("Financial Studies");
            CmbCourse.Items.Add("Fine Art");
            CmbCourse.Items.Add("Industrial Design - Graphics");
            CmbCourse.Items.Add("Industrial Design - Textiles");
            CmbCourse.Items.Add("Library and Information Science");
            CmbCourse.Items.Add("Mechanical Engineering");
            CmbCourse.Items.Add("Metallurgical Engineering");
            CmbCourse.Items.Add("Mineral Resources Engineering");
            CmbCourse.Items.Add("Mining Engineering");
            CmbCourse.Items.Add("Office Technology and Management");
            CmbCourse.Items.Add("Public Administration");
            CmbCourse.Items.Add("Science");
            CmbCourse.Items.Add("Science and laboratory Technology");
            CmbCourse.Items.Add("Statistics");
            CmbCourse.Items.Add("Survey and Geo-Informatics");
            CmbCourse.Items.Add("Urban & Regional Planning");

            CmbProgramme.Items.Add("PRE-ND");
            CmbProgramme.Items.Add("IJMB");

        }


    }
    protected void CmbProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbProgramme.SelectedItem.Text == "IJMB")
        {
            CmbCourse.SelectedIndex = 0;
            CmbCourse.Enabled = false;
        }
        else
        {
            CmbCourse.Enabled = true;
        }
        GridView2.Visible = false;
        //RefreshForm();
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nowrow = GridView2.SelectedIndex;
        string dffg = "";
        LinkButton Btt = (LinkButton)GridView2.Rows[GridView2.SelectedIndex].FindControl("LnkShoww");
        Session["RegNo"] = Btt.Text;
        //Session["RegNoForPrint"] = GridView2.Rows[nowrow].Cells[1].Text;
        Response.Redirect("AdmissionLetterParam.aspx?prog=" + CmbProgramme.SelectedItem.Text);


    }
    protected void LnkShoww_Click(object sender, EventArgs e)
    {

    }
    protected void lnkHere_Click(object sender, EventArgs e)
    {
        Response.Redirect("proceedure.html");
    }
}
