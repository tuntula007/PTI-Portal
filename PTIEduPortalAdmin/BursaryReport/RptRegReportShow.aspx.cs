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

public partial class RptRegReportShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    string CourseText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable AdmDt = new DataTable();
        string ReportTitle = "", ReportTitleAdd="";
        string CourseValue = (string)Session["CourseValue"];
        CourseText = (string)Session["CourseText"];
        string Level = (string)Session["Level"];
        string Programme = (string)Session["Programme"];
        string AcadSession = (string)Session["AcadSession"];
        string Faculty = (string)Session["Faculty"];
        string Department = (string)Session["Department"];

        string Rtype = (string)Session["Rtype"];
        string RptPath = "";
        string RptProgCat = "";
        string RptProg = "";
        string RptFaculty = "";
        string RptDepartment = "";
        RptProgCat = "Programme Category: " + Programme;

        if (CourseText == "All")
        {
            RptProg = "Programme Of Study: All Programmes";
        }
        else
        {
            RptProg = "Programme Of Study: " + CourseText;
        }
        RptProg += "(" + Level + " Level)";
        if (Faculty == "All")
        {
            RptFaculty = "Faculty: All Faculties";
        }
        else
        {
            RptFaculty = "Faculty: " + Faculty;
        }
        if (Department == "All") RptDepartment = "Department: All Departments";
        else RptDepartment = "Department: " + Department;

        ReportTitleAdd = " (" + RptFaculty.ToUpper() + "-" + RptDepartment.ToUpper() + "-" + RptProg.ToUpper() + ")";

        if (Rtype == "Registered Students")
        {
            ReportTitle = "REGISTERED STUDENTS IN: " + ReportTitleAdd;
            if (CourseValue == "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByAll(AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationAll.rpt");
            }
            if (CourseValue == "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByLevelAlone(Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationLevelAlone.rpt");
            }
            if (CourseValue != "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseAlone(CourseValue, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationCourseAlone.rpt");
            }
            if (CourseValue != "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseLevel(CourseValue, Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationCourseLevel.rpt");
            }

        }
        else if (Rtype == "Students Not Yet Registered")
        {
            ReportTitle = "STUDENTS YET TO REGISTER IN: " + ReportTitleAdd;
            if (CourseValue == "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByAllNot(AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationAllNot.rpt");
            }
            if (CourseValue == "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByLevelAloneNot(Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationLevelAlone.rpt");
            }
            if (CourseValue != "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseAloneNot(CourseValue, AcadSession, Department , Faculty);
                RptPath = Server.MapPath("RegistrationCourseAlone.rpt");
            }
            if (CourseValue != "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseLevelNot(CourseValue, Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("RegistrationCourseLevel.rpt");
            }

        }
        else if (Rtype == "Students Registration Details")
        {
            if (CourseValue == "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByAllDetails(AcadSession, Department, Faculty);
            }
            if (CourseValue == "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByLevelAloneDetails(Level, AcadSession, Department, Faculty);
            }
            if (CourseValue != "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseAloneDetails(CourseValue, AcadSession, Department, Faculty);
            }
            if (CourseValue != "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.ByCourseLevelDetails(CourseValue, Level, AcadSession, Department, Faculty);
            }
            ReportTitle = "STUDENTS REGISTRATION DETAILS FOR: " + ReportTitleAdd;
            RptPath = Server.MapPath("RegistrationDetails.rpt");


        }
        else if (Rtype == "Students Listing")
        {
            ReportTitle = "REGISTERED STUDENTS LIST FOR: " + ReportTitleAdd;

            if (CourseValue == "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.StudentsListAll(AcadSession, Department, Faculty);
                RptPath = Server.MapPath("admin/Reports/StudentsListAll.rpt");

            }
            if (CourseValue == "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.StudentsListLevelAlone(Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("StudentsListAll.rpt");
            }
            if (CourseValue != "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.StudentsListCourseAlone(CourseValue, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("StudentsListAll.rpt");
            }
            if (CourseValue != "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.StudentsListCourseLevel(CourseValue, Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("StudentsListAll.rpt");
            }
        }
        else if (Rtype == "SchoolFeesPin" || Rtype == "ApplicationFeesPin" || Rtype == "Students Total Payment")
        {
            if (CourseValue == "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.PinUsageByAll(Rtype, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("PinUsageAll.rpt");
            }
            if (CourseValue == "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.PinUsageLevelAlone(Rtype, Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("PinUsageAll.rpt");
            }
            if (CourseValue != "All" && Level == "All")
            {
                AdmDt = CourseRegReportBusiness.PinUsageCourseAlone(Rtype, CourseValue, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("PinUsageAll.rpt");
            }
            if (CourseValue != "All" && Level != "All")
            {
                AdmDt = CourseRegReportBusiness.PinUsageByCourseLevel(Rtype, CourseValue, Level, AcadSession, Department, Faculty);
                RptPath = Server.MapPath("PinUsageAll.rpt");
            }
            ReportTitle = "PAID STUDENT IN: " + ReportTitleAdd;

        }
        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(AdmDt);
        RptDoc.DataDefinition.FormulaFields["ReportTitle"].Text = "'" + ReportTitle + "'";
        RptDoc.DataDefinition.FormulaFields["AcadSession"].Text = "'" + AcadSession + "'";
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
        string FilNamm = CourseText + " Registration Report";
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

