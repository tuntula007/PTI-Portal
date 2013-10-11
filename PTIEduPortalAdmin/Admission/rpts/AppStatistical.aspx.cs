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
using CybSoft.EduPortal.Data;
using System.Drawing;
using System.Data.SqlClient;


public partial class AppStatistical: System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
       if (string.IsNullOrEmpty(CmbProgramme.Text))
            {
                LblError.Text = "Select Display Type";
                return;
            }
            else if (CmbProgramme.SelectedValue.Equals("PT") || CmbProgramme.SelectedValue.Equals("FT"))
            {
                LblError.Text = "Select the Appropriate Programme";
                return;
            }
          

            //Session["SchoolId"] = CmbSchool.SelectedValue;
           // Session["School"] = CmbSchool.SelectedItem.Text   ;
            Session["Programme"] = CmbProgramme.SelectedValue;
            //Session["CourseId"] = Cmbcourse.SelectedValue;
           // Session["Course"] = Cmbcourse.SelectedItem.Text;  
            Session["ModeOfStudy"] = CmbMode.SelectedValue; //depricated by Boris for WUKARI UTME
           // Session["RptType"] = cmbInd.SelectedValue; 
            Session["GroupBy"] = "";

            Response.Redirect("~/AppStatisticalShow.aspx");
    }
    protected void CmbProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CmbMode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
