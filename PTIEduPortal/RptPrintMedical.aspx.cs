using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

public partial class RptPrintMedical : System.Web.UI.Page
{
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    Students students;
    ReportDocument RptDoc = new ReportDocument();
    private string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
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
            //Check if medical status form printed first
            string qry = "SELECT * FROM [MedicalEntrance] WHERE [MatricNumber]='" + MatricNumber + "' and [PrintStatus]=1";
            if (!Existed(qry))
            {               
                CrvAdmissionReq.Visible = false;
                //Response.Write("<h1> Try Again or Contact Administrator </h1>");
                new Utility().MessageBox("Make sure you have completed and printed the medical status form before printing authorisation letter", "StudentControlCenter.aspx", this.Page);
                return;
                
            }


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
                new Utility().MessageBox("Your Letter is pending at this time. Please contact the admin", "StudentControlCenter.aspx", this.Page);
                return;
            }
            string RptPath = Server.MapPath("RptMedicalLetter.rpt");
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
            new Utility().MessageBox("Your Letter is pending at this time. Please contact the admin", "StudentControlCenter.aspx", this.Page);
            return;
        }
    }
    private bool Existed(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;

            cmd = new SqlCommand(qry, cnn);

            dr = cmd.ExecuteReader();//
            if (dr.HasRows)
            {
                ret = true;
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }

        return ret;
    }
    protected void PrintToPdf()
    {
        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        try
        {
            string FilNamm = "Medical_" + MatricNumber;

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
