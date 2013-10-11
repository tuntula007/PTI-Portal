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

public partial class RptPrintAdmissionLetter : System.Web.UI.Page
{
    Applicants App = new Applicants();

    //SignOnBusiness Sob = new SignOnBusiness();
    ApplicantsBusiness Sob = new ApplicantsBusiness();
    StudentSignOn So = new StudentSignOn();
    ReportDocument RptDoc = new ReportDocument();
    string RegNo = "";
    private static string str = ConfigurationManager.AppSettings["ConnString"];
    string detminePath;

    protected void Page_Load(object sender, EventArgs e)
    {


        //1. CHECK IF STUDENT ADMISSION PIN IS ALREADY REGISTERED ELSE:
        //2. request for PIN HERE





        //if (Session["StudentSignOn"] == null)
        //{
        //    Response.Redirect("StudentLogin.aspx");
        //}
        //So = (StudentSignOn)Session["StudentSignOn"];
        //RegNo = So.FormNumber;
        var Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
        if (Ao == null)
        {
            //session expired, complain
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
        }
        else
        {
            var FormNumber = Ao.FormNumber;
            var sb = new ApplicantsBusiness();
            App = sb.GetApplicantsByFormNo(FormNumber);
            object frm1 = App.RegNo;
            if (frm1 == null)
            {
                App = null; /////refresh
                App = sb.GetApplicantsFromApplicantsHistryByFormNo(FormNumber);
            }
        }
        /*if (Session["ModeOfEntry"] != null)
        {
            //Programme = "PRE-ND";
            Programme = "ND";
        }
        else
        {
            App = (Applicants)Session["Applicants"];
            Programme = App.Programme;
        } */

        RegNo = (string)(Session["RegNo"]);

        //string Mode = (string)Session["Mode"];
        //string ModeofStudy = "Full-Time";
        string ModeofStudy = App.ModeOfStudy;
        DataSet ds = Sob.GetAdmittedApplicantByFormNo(RegNo);
        DataTable AdmDt = new DataTable();
        DataRow nr = null;
        try
        {
            if (!ds.HasErrors)
            {
                AdmDt = ds.Tables[0];
                string ss = ds.Tables[0].Rows[0]["surname"].ToString();
                detminePath = ds.Tables[0].Rows[0]["programme"].ToString();
            }
        }
        catch (Exception dd)
        {
            string ff = dd.Message;
        }

        //ReportDocument RptDoc = new ReportDocument();
        string NowID = Guid.NewGuid().ToString();
        string NowPin = "";
        string NowDate = DateTime.Now.ToString();
        string RptPath = "";
        if (detminePath == "HND-ICE")
        {
            RptPath = Server.MapPath("RptAdmissionLetterICEHND.rpt");
        }
        else if (detminePath == "ND-ICE")
        {
            RptPath = Server.MapPath("RptAdmissionLetterICEND.rpt");
        }
        /*if (Session["ModeOfEntry"] != null)*/
        else if (detminePath == "PRE-ND")
        {
            RptPath = Server.MapPath("RptAdmissionLetterPRE.rpt");
        }

        else
        {
            RptPath = Server.MapPath("RptAdmissionLetter.rpt");
            //RptPath = Server.MapPath("RptAdmissionLetterSUP.rpt");
        }
        //RptPath = Server.MapPath("RptAdmissionLetterSUP.rpt");
        //RptPath = Server.MapPath("RptAdmissionLetter.rpt");
        RptDoc.Load(RptPath);
        RptDoc.SetDataSource(AdmDt);
        RptDoc.DataDefinition.FormulaFields["DocID"].Text = "'" + NowID + "'";
        CrvAdmissionReq.ReportSource = RptDoc;
        //Sob.InsertDocGuid(RegNo, NowDate, "Admission Letter", NowID, NowPin);
        new ApplicantsBusiness().UpdateAdmissionLetterStatus(RegNo);

        PrintToPdf();
        //RptDoc.Close();
        //RptDoc.Dispose();


    }

    protected void PrintToPdf()
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "AdmissionLetter For " + RegNo;
            RptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, FilNamm);
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
