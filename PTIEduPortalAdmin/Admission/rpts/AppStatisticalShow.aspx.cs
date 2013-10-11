﻿using System;
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
//using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;
using System.Drawing;
using System.IO;

public partial class AppStatisticalShow : System.Web.UI.Page
{
    CourseRegReportBusiness CrB = new CourseRegReportBusiness();
    ReportDocument RptDoc = new ReportDocument();
    DataTable AdmDt = new DataTable();
    DataSet ds =new DataSet();
    string CourseText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable AdmDt = new DataTable();
        string ReportTitle = "";
        string ReportTitle2 = "";
       // string CourseValue = (string)Session["CourseValue"] ;
        //CourseText = (string)Session["CourseText"] ;
    
       // string School = (string)Session["School"] ;
       // int Schoolid = int.Parse ( Session["SchoolId"].ToString ()) ;
        string Programme = (string)Session["Programme"];
        //string AcadSession = (string)Session["AcadSession"];
        string Mode = (string)Session["ModeOfStudy"];
        //string Rtype = (string)Session["Rtype"];
        //int CourseId =  int.Parse (Session["CourseId"].ToString ());
        //string course = Session["Course"].ToString ();
       // string RptType = Session["RptType"].ToString();
        string RptPath = "";
        //string StudCategory = (string)Session["GroupBy"];


        AdmDt = OnlineAppReportBusiness.ApplicationStat(Programme);
        
        

    //AdmDt = OnlineAppReportBusiness.ApplicationStat(Programme);

    ReportTitle = "Application Exam Card"; // +"  " + Mode.ToUpper();
    ReportTitle2 = "PTI ICE ONLINE APPLICATION :      " + Programme.ToUpper(); //+" Study Mode:- " + Mode.ToUpper(); ;
    RptPath = Server.MapPath("ApplicantsExamCards.rpt");


    RptDoc.Load(RptPath);
    RptDoc.SetDataSource(AdmDt);
    RptDoc.DataDefinition.FormulaFields["ReportTitle"].Text = "'" + ReportTitle + "'";
    RptDoc.DataDefinition.FormulaFields["ReportTitle2"].Text = "'" + ReportTitle2 + "'";
    //RptDoc.DataDefinition.FormulaFields["School"].Text = "'" + School  + "'";
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
        string FilNamm = CourseText + " Application Statistics Report";
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


//5.And this one to load the image:

private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
{
try
{
FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
byte[] Image = new byte[fs.Length];
fs.Read(Image, 0, Convert.ToInt32(fs.Length));
fs.Close();
objDataRow[strImageField] = Image;
}
catch (Exception ex)
{
Response.Write("<font color=red>"+ex.Message+"</font>");
}
}

//6. Before assigning the dataset to the SetDataSource of your report, add the following code:
private  void  AddImageColumn(DataTable objDataTable, string strFieldName)  //  objDataTable => AdmDt
{
try
{
DataColumn objDataColumn = new DataColumn(strFieldName, Type.GetType(":System.Byte[]"));
objDataTable.Columns.Add(objDataColumn);
//AdmDt.Columns.Add(objDataColumn);
}
catch (Exception ex)
{
Response.Write("<font color=red>"+ex.Message+"</font>");
}
}


 


}