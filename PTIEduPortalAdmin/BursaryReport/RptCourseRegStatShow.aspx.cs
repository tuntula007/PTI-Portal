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

public partial class RptCourseRegStatShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    //string Programme = "";
    //string StudyC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable CourseRegDt = new DataTable();
        string ReportTitle = "";
        string ReportTitle2 = "";
        string Rscope = (string)Session["Rscope"];
        string RscopeTxt = (string)Session["RscopeTxt"];
        string Rtype = (string)Session["Rtype"];
        string RtypeV = (string)Session["RtypeV"];
        string Sess = (string)Session["Sess"];
        string filter = (string)Session["Filter"];
        ReportTitle = RscopeTxt.ToUpper() + " " + Rtype.ToUpper() + "(" + filter.ToUpper() + ")";
        ReportTitle = ReportTitle.Replace(" VS ", " GROUP BY ");
        string RptProgCat = "";
        string RptProg = "";
        string RptPath = "";
        string RptFaculty = "";
        string GroupParam = "";
        string GroupParamTxt = "";
        if (RtypeV == "1")
        {
            //Student Faculty;
            GroupParam = "CS.FacultyName";
            GroupParamTxt = "Faculty";
        }
        if (RtypeV == "2")
        {
            //RptPath = Server.MapPath("RptAdmissionStatByProgrammeCategory.rpt");
            GroupParam = "CS.DepartmentName";
            GroupParamTxt = "Department";
        }
        if (RtypeV == "3")
        {
            //RptPath = Server.MapPath("RptAdmissionStatByProgramme.rpt");
            GroupParam = "CS.CourseOfStudyName";
            GroupParamTxt = "Programme Of Study";
        }
        if (RtypeV == "4")
        {
            //RptPath = Server.MapPath("RptAdmissionStatByFaculty.rpt");
            GroupParam = "S.Programme";
            GroupParamTxt = "Programme Category";
        }
        if (RtypeV == "5")
        {
            //RptPath = Server.MapPath("RptAdmissionStatByState.rpt");
            GroupParam = "Overall";
            GroupParamTxt = "Overall";
        }
        if (RtypeV == "6")
        {
            //prog vs stdcent
            GroupParam = "*";
            GroupParamTxt = "Programme Of Study";
            RptPath = Server.MapPath("StatCourseVsFaculty.rpt");
        }
        if (RtypeV == "7")
        {
            //prog cat vs prog
            GroupParam = "*";
            GroupParamTxt = "Programme Category";
            RptPath = Server.MapPath("StatProgVsCourse.rpt");
        }
        if (RtypeV == "8")
        {
            //prog Cate vs stdcent
            GroupParam = "*";
            GroupParamTxt = "Programme Category";
            RptPath = Server.MapPath("StatProgVsFaculty.rpt");
        }
        if (RtypeV == "9")
        {
            //prog cat vs stdcent
            GroupParam = "*";
            GroupParamTxt = "Department";
            RptPath = Server.MapPath("StatDepartmentVsFaculty.rpt");
        }
        if (RtypeV == "10")
        {
            //schl vs prog
            GroupParam = "*";
            GroupParamTxt = "Department";
            RptPath = Server.MapPath("StatDepartmentVsCourse.rpt");
        }
        if (RtypeV == "11")
        {
            //schl vs prog cate
            GroupParam = "*";
            GroupParamTxt = "*";
            RptPath = Server.MapPath("StatDepartmentVsProg.rpt");
        }
        if (RtypeV == "12")
        {
            //studycent vs prog
            GroupParam = "*";
            GroupParamTxt = "*";
            RptPath = Server.MapPath("StatFacultyVsCourse.rpt");
        }
        if (RtypeV == "13")
        {
            //std cent vs prog cate
            GroupParam = "*";
            GroupParamTxt = "*";
            RptPath = Server.MapPath("StatFacultyVsProg.rpt");
        }
        RptPath = GroupParam != "*" ? Server.MapPath("RptCourseRegDistribution.rpt") : RptPath;

        CourseRegDt = CourseRegReportBusiness.getCourseRegStat(Rscope, Sess, GroupParam, filter);
        if (CourseRegDt == null) MessageBox("There was a technical problem generating your report.", "~/admin/Reporting/RptCourseRegistrationStat.aspx");
        //AdmDt = OnlineAppReportBusiness.getApplicationListGroup(ProgrammeCat, Programme, StudyC, Rtype, Sess);
        string Sessionn = "Registration Session: " + Sess;
        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(CourseRegDt);
        RptDoc.DataDefinition.FormulaFields["Heading"].Text = "'" + ReportTitle + "'";
        if (GroupParam != "*") RptDoc.DataDefinition.FormulaFields["ColumnHeader"].Text = "'" + GroupParamTxt + "'";
        RptDoc.DataDefinition.FormulaFields["Heading1"].Text = "'" + Sessionn + "'";
        //RptDoc.DataDefinition.FormulaFields["Faculty"].Text = "'" + RptFaculty + "'";
        //RptDoc.DataDefinition.FormulaFields["Department"].Text = "'" + Department  + "'";
        CrvCourseRegistrationReq.ReportSource = RptDoc;
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
        RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);
        //RptDoc.ExportToHttpResponse(ExportFormatType.HTML40, Response, false, FilNamm);

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //    ex = null;
        //}
        //finally
        //{
        //    RptDoc.Close();
        //    RptDoc.Dispose();
        //}

    }

    public void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ");  </script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }
    public void MessageBox(string strMsg, string redirectUrl)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + "); window.location.href=" + "'" + redirectUrl + "';  </script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }

}

