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
using CybSoft .EduPortal.Business ;
 using CybSoft .EduPortal.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class StudentIdCrystal : System.Web.UI.Page
{
   string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    Students students;
    ReportDocument RptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        CrvAdmissionReq.Visible = true;
        So = (StudentSignOn)Session["StudentSignOn"];

        if (Request.QueryString["matricnumber"] != null)
        {
            MatricNumber = Request.QueryString["matricnumber"].ToString();

        }
        else if (Session["MatricNumber"] != null)
        {
            MatricNumber = Session["MatricNumber"].ToString();
        }
        else
        {

            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
            return;
        }
        try
        {
            if ((int)Session["RegApproval"] != 1)
            {
                new Utility().MessageBox("Your are not yet through with your registration process", "StudentControlCenter.aspx", this.Page);
                return;
            }

            Session["MatricNumber"] = MatricNumber;
            DataTable AdmDt = new DataTable();
            DeptCoursesBusiness sb = new DeptCoursesBusiness();
            AdmDt = sb.GetStudentsInfoByMatNoCrystal(MatricNumber);
            if (!(AdmDt.Rows.Count > 0))
            {
                new Utility().MessageBox("Your ID Card is pending at this time. Please contact the admin", "StudentControlCenter.aspx", this.Page);
                return;
            }
            string RptPath = Server.MapPath("StudentIdCardNew.rpt");
            RptDoc.Load(RptPath);
            RptDoc.SetDataSource(AdmDt);

            CrvAdmissionReq.ReportSource = RptDoc;
            PrintToPdf();
        }
        catch (Exception ex)
        {
            string ex1 = ex.Message;
            CrvAdmissionReq.Visible = false;
            Response.Write("<h1> Try Again or Contact Administrator </h1>");
            new Utility().MessageBox("Your ID Card is pending at this time. Please contact the admin", "StudentControlCenter.aspx", this.Page);
            return;
        }
    }
    protected void PrintToPdf()
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "Identification Card For " + MatricNumber;

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
}
