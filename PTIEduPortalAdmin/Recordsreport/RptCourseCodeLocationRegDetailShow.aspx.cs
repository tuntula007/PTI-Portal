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

public partial class RptCourseCodeLocationRegDetailShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    //string Programme = "";
    //string Faculty = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable DetailCourseRegDt = new DataTable();
        string ReportTitle = "";
        string ProgrammeCat = (string)Session["ProgrammeCategory"];
        string Programme = (string)Session["Programme"];
        string ProgrammeText = (string)Session["ProgrammeText"];
        string CourseCode = (string)Session["CourseCode"];
        string Faculty = (string)Session["Faculty"];
        string Department = (string)Session["Department"];
        string Sess = (string)Session["Session"];
        string FilterLevel = (string)Session["FilterLevel"];
        string ExamCenter = (string)Session["ExamLocation"];
        string TeachingCenter = (string)Session["TeachingCenter"];
        string Location = ExamCenter + " EXAM CENTER AND " + TeachingCenter + " INTERACTIVE CENTER";

        CheckBoxList DisplayColumns = (CheckBoxList)Session["DisplayColumns"];
        string RptProgCat = "";
        string RptProg = "";
        string RptFaculty = "";
        string RptDepartment = "";
        string[] RptFieldHeader = null;
        string RptField = CourseRegReportBusiness.DisplayedColumns(DisplayColumns, ref RptFieldHeader);
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

        DetailCourseRegDt = CourseRegReportBusiness.getCourseCodeRegDetail(ExamCenter, TeachingCenter,ProgrammeCat,CourseCode,ProgrammeText, Faculty, Department, Sess, FilterLevel, RptField);
        if (DetailCourseRegDt == null || DetailCourseRegDt.Rows.Count < 1)
        {
            Session["ErrorMessage"] = "No Student Course Registration from "
                + Location + " for "
                + RptProgCat.Replace("Programme Category: ", "") + " Programme Category"
                + " in " + RptDepartment.Replace("Department: ", "")
                + " at " + RptFaculty.Replace("Faculty: ", "")
                + " studying " + RptProg.Replace("Programme: ", "");
            Response.Redirect("~/Recordsreport/RptCourseRegistrationByCodeWithLocation.aspx");
            return;
        }
        ReportTitle = CourseCode + " REGISTRATION FOR " + Location + "(" + Sess.ToUpper() + " REGISTRATION SESSION)";// FOR STUDENTS OF "
            //+ RptProgCat.Replace("Programme Category: ", "").ToUpper() + " PROGRAMME"
            //+ " IN " + RptDepartment.Replace("Department: ", "").ToUpper()
              //  + " AT " + RptFaculty.Replace("Faculty: ", "").ToUpper()
                //+ " STUDYING " + RptProg.Replace("Programme: ", "").ToUpper();
        //get the number of columns to di
        string RptPath = Server.MapPath("RptCourseCodeRegistrationDetail.rpt");

        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(DetailCourseRegDt);
        RptDoc.DataDefinition.FormulaFields["Heading"].Text = "'" + ReportTitle.ToUpper() + "'";
        string coursecodetitle = "Course Code: " + CourseCode;
        RptDoc.DataDefinition.FormulaFields["ProgrammeCat"].Text = "'" + coursecodetitle.ToUpper() + "'";
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
        CrvCourseRegistrationDetails.ReportSource = RptDoc;
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

