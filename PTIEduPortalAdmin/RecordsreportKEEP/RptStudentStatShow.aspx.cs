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

public partial class RptStudentStatShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable AdmDt = new DataTable();
        string ReportTitle = "";
        string Rscope = (string)Session["Rscope"];
        string RscopeTxt = (string)Session["RscopeTxt"];
        string Rtype = (string)Session["Rtype"];
        string RtypeV = (string)Session["RtypeV"];
        string Sess = (string)Session["Sess"];
        ReportTitle = RscopeTxt.ToUpper() + " " + Rtype.ToUpper();
        string RptPath = "";
        string GroupParam = "";
        string GroupParamTxt = "";
        if (RtypeV == "1")
        {
            GroupParam = "S.Programme";
            GroupParamTxt = "Programme Category";
        }
        if (RtypeV == "2")
        {
            GroupParam = "CS.FacultyName";
            GroupParamTxt = "Faculty";
        }
         if (RtypeV == "3")
        {
            GroupParam = "CS.DepartmentName";
            GroupParamTxt = "Department";
        }
       if (RtypeV == "4")
        {
            GroupParam = "CS.CourseOfStudyName";
            GroupParamTxt = "Programme Of Study";
        }
        
        RptPath = Server.MapPath("RptStudentStatistics.rpt");

        AdmDt = StudentReportBusiness.getStudentListStat(Rscope, Sess, GroupParam);
        if (AdmDt == null || AdmDt.Rows.Count < 1)
        {
            //return back to filter page
            Response.Redirect("~/Recordsreport/RptStudentStat.aspx");
        }
        string Sessionn = "Academic Session: " + Sess;
        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(AdmDt);
        RptDoc.DataDefinition.FormulaFields["Heading"].Text = "'" + ReportTitle + "'";
        RptDoc.DataDefinition.FormulaFields["ColumnHeader"].Text = "'" + GroupParamTxt + "'";
        RptDoc.DataDefinition.FormulaFields["Heading1"].Text = "'" + Sessionn + "'";

        CrvAdmissionReq.ReportSource = RptDoc;
        PrintToPdf();
    }
    protected void PrintToPdf()
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        //try
        //{
        string FilNamm = " Registration Report";
        if (Session["ReportFormat"] != null)
        {
            string reportoption = Session["ReportFormat"].ToString();

            if (reportoption.Equals("Excel"))
                RptDoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, FilNamm);
            else
                RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);
        }
        else
        {
            RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);
        }

    }
}

