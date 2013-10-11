
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

public partial class RptStudentStat : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }
    protected void BtnShow_Click(object sender, EventArgs e)
    {
        LblError.Text = "";
        Session["Rscope"] = CmbRscope.SelectedValue;
        Session["RscopeTxt"] = CmbRscope.SelectedItem.Text;
        Session["Rtype"] = CmbRtype.SelectedItem.Text;
        Session["RtypeV"] = CmbRtype.SelectedItem.Value;
        Session["ReportFormat"] = cmbReportOption.SelectedValue;
        Session["Sess"] = CmbSession.Text;
        Response.Redirect("~/Reporting/RptStudentStatShow.aspx");
        

    }
   
}
