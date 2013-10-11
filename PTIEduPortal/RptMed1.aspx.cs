using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
//using Cyberspace.CurrencyInWords;



public partial class ReportPo : System.Web.UI.Page
{
    private static string str = ConfigurationManager.AppSettings["ConnString"];
    
    private static string AmountWords = "";

    public static string PtCode = "";
    public static string ReportTypeGlobal = "";
    ReportDocument RptDoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {

        ////string Optype = (string)Session["optype"];
        ////string Ponumber = (string)Session["PONumber"];
        //StudentSignOn So = new StudentSignOn();
        //So = (StudentSignOn)Session["StudentSignOn"];


        //string Title = "UNIVERSITY OF IBADAN, IBADAN, NIGERIA";
        //string Title2 = "DISTANCE LEARNING CENTRE";
        


        DataTable dt = new DataTable();
        string RptPath = "";

        string MatricNumber = Request.QueryString["StudID"];


        if (MatricNumber != null)
        {
            RptPath = Server.MapPath("MedStatusForm.rpt");
            dt = Getreports(MatricNumber);

            RptDoc.Load(RptPath);
            RptDoc.SetDataSource(dt);

            //RptDoc.DataDefinition.FormulaFields["Title"].Text = "'" + Title + "'";
            //RptDoc.DataDefinition.FormulaFields["Title2"].Text = "'" + Title2 + "'";
           // RptDoc.DataDefinition.FormulaFields["AmountWords"].Text = "'" + AmountWords + "'";
        }
        
        
        CrystalReportViewer1.ReportSource = RptDoc;

        string Optype = "MedicalStatus";
        PrintToPdf(Optype, MatricNumber);
    }

    private DataTable Getreports(string matno)
    {
        DataSet MedicalEntr = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();

            
            //string query = "SELECT D.[MatricNumber],D.[ExamRegCode] as ExamNO, E.[PaymentType] ,B.[Picture],(A.[Surname] + ',' + ' '+ A.[OtherNames]) as [Names], A.[AcademicLevel],A.[AdmittedSession],A.[PresentSession],A.[EntryMode],A.[Sex],(select C.[FacultyName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Faculty,(select C.[DepartmentName] from [CourseOfStudy] C where C.[CourseOfStudyID] = D.[CourseOfStudyID]) as Department FROM [Students] A, PictureFile B, CourseRegistration D, studentpayment E  where D.[MatricNumber] = A.[MatricNumber] and D.[Regno] = B.[PicKey] and D.[MatricNumber] = E.[MatricNumber] and D.[SessionName] = E.[Session] and D.[SessionName] = '" + Yearr + "' and D.[CourseCode] = '" + CourseCode + "' and D.[Programme] = '" + Programme + "' and D.[ModeOfStudy] = '" + StudyMode + "'";
            string query = "SELECT D. *,B.[Picture],A.[Surname],A.[OtherNames], A.[DateOfBirth],A.[Nationality],A.[State],A.[Sex] FROM [Students] A, PictureFile B, [MedicalEntrance] D where D.[MatricNumber] = A.[MatricNumber] and D.[ApplcationNo] = B.[PicKey] and D.[MatricNumber] = '" + matno + "'";


            SqlDataAdapter dat = new SqlDataAdapter(query, cnn);
            dat.Fill(MedicalEntr, "MedicalEntrance");

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {

            //msg = ex.Message + "||" + ex.StackTrace.ToString();
            //showmassage(msg);
        }
        return MedicalEntr.Tables[0];
    }

    
    protected void PrintToPdf(string supplier, string po)
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "";
            if (FilNamm.Length > 5)
            {
                FilNamm = supplier.Substring(0, 5) + po;
            }
            else
            {
                FilNamm = po;
            }

            //FilNamm = ReportTypeGlobal + " PDF Report";
            RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ex = null;
        }
        finally
        {
            RptDoc.Close();
            RptDoc.Dispose();
        }

    }
    //private void populatereport()
    //{
    //    SqlConnection cnn = new SqlConnection(str);
    //    cnn.Open();

    //    SqlCommand cmd = null;
    //    SqlDataReader dr = null;
    //    SqlDataAdapter dat = null;


    //    //DataSet DataSet2 = new DataSet();
    //    DataSet PoGorundCost = new DataSet();
    //    DataSet PoInfos = new DataSet();
    //    DataSet PoSupplier = new DataSet();



    //    dat = new SqlDataAdapter("SELECT [TotalCost] FROM [PoCostTemp]", cnn);
    //    dat.Fill(PoGorundCost, "PoCostTemp");

    //    dat = new SqlDataAdapter("SELECT * FROM [POSuppliersTemp]", cnn);
    //    dat.Fill(PoSupplier, "POSuppliersTemp");

    //    dat = new SqlDataAdapter("SELECT * FROM [PurchaseOrderTemp]", cnn);
    //    dat.Fill(PoInfos, "PurchaseOrderTemp");

    //    try
    //    {
    //        ReportViewer1.Reset();
    //        ReportViewer1.Visible = true;

    //        ReportDataSource rds1 = new ReportDataSource("PoGorundCost_PoCostTemp", PoGorundCost.Tables[0]);
    //        ReportDataSource rds2 = new ReportDataSource("PoSupplier_POSuppliersTemp", PoSupplier.Tables[0]);
    //        ReportDataSource rds3 = new ReportDataSource("PoInfos_PurchaseOrderTemp", PoInfos.Tables[0]);

    //        ReportViewer1.ProcessingMode = ProcessingMode.Local;
    //        String path = Server.MapPath("~/Reports/PurchaseOrder.rdlc");

    //        //ReportViewer1.LocalReport.ReportPath = "C:\\Users\\user\\Documents\\Visual Studio 2008\\Websites\\ChequePayment\\Reports\\Report1.rdlc";// "Report.rdlc";
    //        ReportViewer1.LocalReport.ReportPath = path;

    //        ReportViewer1.LocalReport.DataSources.Add(rds1);// = DataSet1;
    //        ReportViewer1.LocalReport.DataSources.Add(rds2);
    //        ReportViewer1.LocalReport.DataSources.Add(rds3);
    //        ReportViewer1.LocalReport.Refresh();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    private void ProceccRPT()
    {
        ////if (cmbPrinter.Items.Count == 0)
        ////{
        ////    MessageBox.Show("No printer installed in this system");
        ////    cmbPrinter.Focus();
        ////    return;
        ////}

        ////if (printerReady() == true)
        ////{


        ////}
        ////else
        ////{
        ////    MessageBox.Show("Check your printer status");
        ////    return;
        ////}

        //SqlConnection cnn = new SqlConnection(str);
        //cnn.Open();

        //SqlCommand cmd = null;
        //SqlDataReader dr = null;
        //SqlDataAdapter dat = null;

        //cmd = new SqlCommand("select * from Printername", cnn);
        //dr = cmd.ExecuteReader();
        //if(dr.Read())
        //{
        //    printername = dr.GetString(0);
        //}
        //dr.Dispose();
        //cmd.Dispose();


        ////DataSet DataSet2 = new DataSet();
        //DataSet PoGorundCost = new DataSet();
        //DataSet PoInfos = new DataSet();
        //DataSet PoSupplier = new DataSet();

        //dat = new SqlDataAdapter("SELECT [TotalCost] FROM [PoCostTemp]", cnn);
        //dat.Fill(PoGorundCost, "PoCostTemp");

        //dat = new SqlDataAdapter("SELECT * FROM [POSuppliersTemp]", cnn);
        //dat.Fill(PoSupplier, "POSuppliersTemp");

        //dat = new SqlDataAdapter("SELECT * FROM [PurchaseOrderTemp]", cnn);
        //dat.Fill(PoInfos, "PurchaseOrderTemp");

        //try
        //{
        //    ReportViewer1.Reset();
        //    ReportViewer1.Visible = true;

        //    ReportDataSource rds1 = new ReportDataSource("PoGorundCost_PoCostTemp", PoGorundCost.Tables[0]);
        //    ReportDataSource rds2 = new ReportDataSource("PoSupplier_POSuppliersTemp", PoSupplier.Tables[0]);
        //    ReportDataSource rds3 = new ReportDataSource("PoInfos_PurchaseOrderTemp", PoInfos.Tables[0]);

        //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //    String path = Server.MapPath("~/Reports/PurchaseOrder.rdlc");

        //    //ReportViewer1.LocalReport.ReportPath = "C:\\Users\\user\\Documents\\Visual Studio 2008\\Websites\\ChequePayment\\Reports\\Report1.rdlc";// "Report.rdlc";
        //    LocalReport report = new LocalReport();
        //    report.ReportPath = path;
        //    report.DataSources.Add(rds1);// = DataSet1;
        //    report.DataSources.Add(rds2);
        //    report.DataSources.Add(rds3);
        //    //ReportViewer1.LocalReport.Refresh();

        //    Export(report);
        //    m_currentPageIndex = 0;
        //    Print();
        //}
        //catch (Exception ex)
        //{

        //}
    }

    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }

}
