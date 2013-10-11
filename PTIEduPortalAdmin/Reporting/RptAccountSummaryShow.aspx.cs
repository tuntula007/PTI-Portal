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

public partial class RptAccountSummaryShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    //string Programme = "";
    //string StudyC = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ProgrammeCat = (string)Session["ProgrammeCategory"];
        string Programme = (string)Session["Programme"];
        string ProgrammeText = (string)Session["ProgrammeText"];
        string Faculty = (string)Session["Faculty"];
        string Department = (string)Session["Department"];
        string Sess = (string)Session["Session"];
        string FilterLevel = (string)Session["FilterLevel"];
        string DtFrom = (string)Session["DtFrom"];
        string DtTo = (string)Session["DtTo"];

        string RptProgCat = "";
        string RptProg = "";
        string RptFaculty = "";
        string RptDepartment = "";


        RptProgCat = "Programme Category: " + ProgrammeCat;
        if (Programme == "All")
        {
            RptProg = "Programme: All Programme of Study";
        }
        else
        {
            RptProg = "Programme: " + ProgrammeText;
        }
        RptProg += "(" + FilterLevel + " Level)";
        if (Faculty == "All")
        {
            RptFaculty = "Faculty: All Faculty";
        }
        else
        {
            RptFaculty = "Faculty: " + Faculty;
        }
        if (Department == "All") RptDepartment = "Department: All Departments";
        else RptDepartment = "Department: " + Department;



        DataTable AdmDt = new DataTable();
        string ReportTitle = "";

        string RptPath = Server.MapPath("RptAccountSummaryRpt.rpt");

        //AdmDt = OnlineAppReportBusinessGbt.getAccountLedger(Param, ParamValue, DtFrom, DtTo);
        AdmDt = BursaryBusiness.getAccountSummary(ProgrammeCat, Faculty, Department, Programme, FilterLevel, Sess, DtFrom, DtTo);

        if (AdmDt == null || AdmDt.Rows.Count < 1)
        {
            Session["ErrorMessage"] = "No Student Account Summary for "
                + RptProgCat.Replace("Programme Category: ", "") + " Programme Category"
                + " in " + RptDepartment.Replace("Department: ", "")
                + " at " + RptFaculty.Replace("Faculty: ", "")
                + " studying " + RptProg.Replace("Programme: ", "");
            Response.Redirect("~/Reporting/RptAccountSummary.aspx");
            return;
        }
        ReportTitle = "ACCOUNT SUMMARY REPORT FOR " + RptProgCat.ToUpper() + " STUDENTS IN" + RptFaculty.ToUpper() + ": " + RptDepartment.ToUpper() + ": " + RptProg.ToUpper() + " FOR " + Sess.ToUpper() + " REGISTRATION SESSION";// FOR STUDENTS OF "
        string DateRange = "";
        if (DtFrom == DtTo)
        {
            DateRange = (string.IsNullOrEmpty(DtFrom) == false) ? "Date Range: " + DtFrom : "";
        }
        else
        {
            DateRange = "Date Range: " + DtFrom + " to " + DtTo;
        }


        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(AdmDt);
        RptDoc.DataDefinition.FormulaFields["Heading"].Text = "'" + ReportTitle + "'";
        RptDoc.DataDefinition.FormulaFields["DateRange"].Text = "'" + DateRange.ToUpper() + "'";

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
        string FilNamm = "Account Summary Report";
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
}

