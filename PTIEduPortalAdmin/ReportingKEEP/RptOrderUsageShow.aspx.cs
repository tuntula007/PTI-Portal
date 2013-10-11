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

public partial class RptOrderUsageShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    string CourseText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable AdmDt = new DataTable();

        string ReportTitle = "";
        string ProgrammeCat = (string)Session["ProgrammeCategory"];
        string Programme = (string)Session["Programme"];
        string ProgrammeText = (string)Session["ProgrammeText"];
        //string CourseCode = (string)Session["CourseCode"];
        string Faculty = (string)Session["Faculty"];
        string Department = (string)Session["Department"];
        string Sess = (string)Session["Session"];
        string FilterLevel = (string)Session["FilterLevel"];
        CheckBoxList DisplayColumns = (CheckBoxList)Session["DisplayColumns"];
        string RptProgCat = "";
        string RptProg = "";
        string RptFaculty = "";
        string RptDepartment = "";
        string[] RptFieldHeader = null;
        string RptField = CourseRegReportBusiness.DisplayedColumns(DisplayColumns, ref RptFieldHeader);

        RptField += ", (cast(p.amount as varchar) + '.00') [State], p.usedDate [MatricSerial]";
        RptProgCat = "Programme Category: " + ProgrammeCat;
        if (Programme == "All")
        {
            RptProg = "Programme: All Programmes";
        }
        else
        {
            RptProg = "Programme: " + ProgrammeText;
        }
        // RptProg += "(" + FilterLevel + " Level)";
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



        string ReportTitleAdd="";

        string Rtype = (string)Session["Rtype"];
        string RptPathFile = "";

        //ReportTitleAdd = " (" + RptFaculty.ToUpper() + "-" + RptDepartment.ToUpper() + "-" + RptProg.ToUpper() + ")";

        string orderType = (Rtype == "SchoolFeesPin") ? "FEES PAYMENT" : (Rtype == "ApplicationFeesPin") ? "APPLICATION FORM" : "TOTAL PAYMENT";

        if (Rtype == "SchoolFeesPin")
        {
            AdmDt = CourseRegReportBusiness.getPinUsageDetail(ProgrammeCat, Programme, Faculty, Department, Sess, FilterLevel, RptField);
            #region OldFilter
            //if (Programme == "All" && FilterLevel == "All")
            //{
            //    AdmDt = CourseRegReportBusiness.PinUsageByAll(Rtype, Sess, Department, Faculty);
            //    //RptPathFile = "PinUsageAll.rpt";
            //}
            //if (Programme == "All" && FilterLevel != "All")
            //{
            //    AdmDt = CourseRegReportBusiness.PinUsageLevelAlone(Rtype, FilterLevel, Sess, Department, Faculty);
            //    //RptPathFile = "PinUsageAll.rpt";
            //}
            //if (Programme != "All" && FilterLevel == "All")
            //{
            //    AdmDt = CourseRegReportBusiness.PinUsageCourseAlone(Rtype, Programme, Sess, Department, Faculty);
            //    //RptPathFile = "PinUsageAll.rpt";
            //}
            //if (Programme != "All" && FilterLevel != "All")
            //{
            //    AdmDt = CourseRegReportBusiness.PinUsageByCourseLevel(Rtype, Programme, FilterLevel, Sess, Department, Faculty);
            //    //RptPathFile = "PinUsageAll.rpt";
            //}
            #endregion
        }
        else if (Rtype == "ApplicationFeesPin")
        {
        }
        else if (Rtype == "Students Total Payment")
        {
        }
        else
        {
            //unhandled report format, go back to filter page
            Session["ErrorMessage"] = "Please select your report filter first";
            Response.Redirect("~/Reporting/RptOrderUsageParam.aspx");
        }
        ReportTitle = "CONFIRMATION ORDER NUMBER REPORT FOR "+ orderType.ToUpper();

       if (AdmDt == null || AdmDt.Rows.Count < 1)
       {
           Session["ErrorMessage"] = "No Payments for "
               + RptProgCat.Replace("Programme Category: ", "") + " Programme Category"
               + " in " + RptDepartment.Replace("Department: ", "")
               + " at " + RptFaculty.Replace("Faculty: ", "")
               + " studying " + RptProg.Replace("Programme: ", "");
           Response.Redirect("~/Reporting/RptOrderUsageParam.aspx");
           return;
       }
       //ReportTitle = "COURSE REGISTRATION REPORT FOR " + Sess.ToUpper() + " REGISTRATION SESSION";// FOR STUDENTS OF "
       //+ RptProgCat.Replace("Programme Category: ", "").ToUpper() + " PROGRAMME"
       //+ " IN " + RptDepartment.Replace("Department: ", "").ToUpper()
       //  + " AT " + RptFaculty.Replace("Faculty: ", "").ToUpper()
       //+ " STUDYING " + RptProg.Replace("Programme: ", "").ToUpper();
       //get the number of columns to di
       
        //string RptPath = Server.MapPath(RptPathFile);
       string RptPath = Server.MapPath("RptPinDetail.rpt");

       RptDoc.Load(RptPath);
       RptDoc.SetDataSource(AdmDt);
       RptDoc.DataDefinition.FormulaFields["Heading"].Text = "'" + ReportTitle.ToUpper() + "'";
       RptDoc.DataDefinition.FormulaFields["ProgrammeCat"].Text = "'" + RptProgCat.ToUpper() + "'";
       RptDoc.DataDefinition.FormulaFields["Programmme"].Text = "'" + RptProg.ToUpper() + "'";
       RptDoc.DataDefinition.FormulaFields["Faculty"].Text = "'" + RptFaculty.ToUpper() + "'";
       RptDoc.DataDefinition.FormulaFields["Department"].Text = "'" + RptDepartment.ToUpper() + "'";

       if (RptFieldHeader != null)
       {
           //RptDoc.DataDefinition.FormulaFields["ColHeader3"].Text = "'" + ((RptFieldHeader[0] != null) ? RptFieldHeader[0] : "") + "'";
           RptDoc.DataDefinition.FormulaFields["ColHeaddTest"].Text = "'" + ((RptFieldHeader[0] != null) ? RptFieldHeader[0].ToUpper() : "") + "'";
           RptDoc.DataDefinition.FormulaFields["ColHeader4"].Text = "'" + ((RptFieldHeader[1] != null) ? RptFieldHeader[1].ToUpper() : "") + "'";
           RptDoc.DataDefinition.FormulaFields["ColHeader5"].Text = "'" + ((RptFieldHeader[2] != null) ? RptFieldHeader[2].ToUpper() : "") + "'";
           RptDoc.DataDefinition.FormulaFields["ColHeader6"].Text = "'" + ((RptFieldHeader[3] != null) ? RptFieldHeader[3].ToUpper() : "") + "'";
           RptDoc.DataDefinition.FormulaFields["ColHeader7"].Text = "'" + ((RptFieldHeader[4] != null) ? RptFieldHeader[4].ToUpper() : "") + "'";
       }


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

