using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using System.Drawing;

public partial class RptPrintNotification : System.Web.UI.Page
{
    SignOnBusiness Sob = new SignOnBusiness();
    ReportDocument RptDoc = new ReportDocument();
    string RegNo = "", FormNumber="";
    protected void Page_Load(object sender, EventArgs e)
    {
        ApplicantSignOn So = new ApplicantSignOn();
        string RegNo = string.Empty;
        if (Session["RegNo"] == null)
        {
            //session expired, complain and go back to session expired page
            new Utility().MessageBox("You have not entered your Form Number. Check your admission status first.", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
            return;
        }
        else
        {
            //So = (ApplicantSignOn)Session["ApplicantSignOn"];
            RegNo = (string)Session["RegNo"];
            FormNumber = RegNo;
        }

        Session["FormNumber"] = FormNumber;

        //get enrollment detail

        ApplicantsBusiness sb = new ApplicantsBusiness();
        ////MatricNumber = "2008/ND/PAD/002";
        //applicants = sb.GetApplicantsByFormNo(FormNumber);
        // RegNo = applicants.RegNo; //(string)Session["RegNo"];
        // string Mode = applicants.ModeOfStudy; // (string)Session["Mode"];
        //string ModeofStudy = applicants.ModeOfStudy; // (string)Session["ModeofStudy"];
        DataSet ds = Sob.GetNotificationLetterByRegNo(RegNo);
        DataTable AdmDt = new DataTable();
        DataRow nr = null;
        try
        {
            if (!ds.HasErrors)
            {
                AdmDt = ds.Tables[0];
                string ss = ds.Tables[0].Rows[0]["surname"].ToString();
                //isAddmited,programme as ProgCategory
                int isAdmited = int.Parse(ds.Tables[0].Rows[0]["isAdmitted"].ToString());
                string admittedStatus = ds.Tables[0].Rows[0]["admittedStatus"].ToString();
                string remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
                string courseSuggested = ds.Tables[0].Rows[0]["courseofstudy"].ToString();
                string prog = ds.Tables[0].Rows[0]["ProgCategory"].ToString();
                //isAddmited,programme as ProgCategory

                if (isAdmited == 0)
                {
                    new Utility().MessageBox("You did not currently meet the minimum requirement for admission to the programme of study you earlier applied for. You may consider applying for: " + (remarks.Length > 0 ? remarks : courseSuggested), ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                }
                //if (prog.ToLower() == "phd")
                //{
                //    MessageBox("Please Contact The University for your interview date");
                //    return;
                //}
            }
            else
            {
                new Utility().MessageBox("There was a problem generating your letter", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                return;
            }
        }
        catch (Exception dd)
        {
            string ff = dd.Message;
            Response .Redirect ("~/ViewAdmittedStudentsSpecial.aspx");
            //new Utility().MessageBox("Your Admission Record could not be loaded: Most Likely, School Fees have not been setup for the programme of study you are admitted to. Consult with the Bursary and Admin.", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
            return;
        }

        //ReportDocument RptDoc = new ReportDocument();
        string NowID = Guid.NewGuid().ToString();
        string NowPin = (string)Session["Pinn"];
        string NowDate = DateTime.Now.ToString();
        string RptPath = "";
        RptPath = Server.MapPath("RptNotificationLetterFresh.rpt");
        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(AdmDt);
        RptDoc.DataDefinition.FormulaFields["DocID"].Text = "'" + NowID + "'";
        CrvAdmissionReq.ReportSource = RptDoc;
        Sob.InsertDocGuid(RegNo, NowDate, "Admission Letter", NowID, NowPin);
        //ApplicantsBusiness.UpdateAdmLetterPrinted(RegNo);
        PrintToPdf();

        //RptDoc.Close();
        //RptDoc.Dispose();


    }
    protected void PrintToPdf()
    {
        // string FilNamme = "F:\\PdfPickup\\" +"AdmissionLetterFor" + 101 + DateTime.Now.ToFileTime().ToString () ;

        // RptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, FilNamme);

        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "NotificationLetter For " + RegNo;

            RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);


            //ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //CrDiskFileDestinationOptions.DiskFileName = FilNamm;
            //CrExportOptions = RptDoc.ExportOptions;
            //CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //RptDoc.Export();
            //RptDoc.ExportToHttpResponse(ExportFormatType.HTML40, Response, false, FilNamm);

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
}
