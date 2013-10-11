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
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using log4net;
using log4net.Config;
public partial class PayClearance : System.Web.UI.Page
{
    string CurrentSession = new SignOnBusiness().getCurrentSession();
    string CurrentSemester = new SignOnBusiness().getCurrentSemester();
    protected void Page_Load(object sender, EventArgs e)
    {
        ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string MatricNumber = "";
        string Semester = "First";
        StudentSignOn So = new StudentSignOn();
        Students students;
        StudentPayment sp = new StudentPayment();


        //get matric no and semester
        So = (StudentSignOn)Session["StudentSignOn"];
        Session["isFromRegistrationSubmit"] = true;
        bool isFromSubmit = false;
        if (Request.QueryString["isFromCenter"] != null)
        {
            string isItFromCenter = Request.QueryString["isFromCenter"].ToString();
            if (isItFromCenter.Equals("1") == true)
                isFromSubmit = true;
            else
                isFromSubmit = false;
        }
        if (isFromSubmit == false)
            isFromSubmit = (bool)Session["isFromRegistrationSubmit"];

        if (isFromSubmit == true && So != null)
        {

            MatricNumber = So.MatricNumber;
        }
        else
        {
            //session expired, complain and go back to session expired page
            // Response.Write("session expired");
            // return;
            logger.Info("Session Expired");
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        Session["MatricNumber"] = MatricNumber;
        //determine semester
        if (Request.QueryString["Semester"] != null)
        {
            Semester = Request.QueryString["Semester"].ToString();
            Session["Semester"] = Semester;
        }
        else
        {

            if (Session["Semester"] != null)
            {
                Semester = Session["Semester"].ToString();
            }
            else
            {
                //default semester to first
                Semester = "First";
            }
        }

        if (!Page.IsPostBack)
        {
            //get enrollment detail
            StudentsBusiness sb = new StudentsBusiness();
            students = sb.GetStudentsByMatNo(MatricNumber);
            form1.Visible = true;
            Session["MatricNumber"] = MatricNumber;
            Session["Students"] = students;
            Session["Level"] = students.AcademicLevel;
            Session["DepartmentId"] = students.DepartmentID;
            Session["CurrentSession"] = CurrentSession;
            Session["Semester"] = "First";
            Session["Programme"] = students.Programme;
            Session["ModeOfStudy"] = students.ModeOfStudy;
            //detect if the student has NOT registered for first semester, then disallow.
            string crSession = new SignOnBusiness().getCurrentSession();

            paydate.Text =string.Format("{0:d/M/yyyy}", DateTime.Now);
            //receiptno.Text = "";
            lblcourse.Text = students.CourseOfStudy.ToUpper();
            lblmatno.Text = students.MatricNumber.ToUpper();
            //get personal detail
            name.Text = students.Surname.ToUpper() + " " + students.OtherNames.ToUpper();
            faculty.Text = students.Faculty.ToUpper();
            lblacademiclevel.Text = students.AcademicLevel.ToUpper();
            programme.Text = students.Programme;

            lblsession.Text = CurrentSession.ToUpper();

            #region Get Student Payment Detail

            sp = sb.GetStudentPayments(students.MatricNumber, CurrentSession);

            //If Has Paid(Full/Part Time) Fees For Active Session
            if (sp != null)
            {
                //Paid Fully or Paid First Installment
                //If Semester is Second and Paid First Installment
                amount.Text = string.Format("{0:N}", StudentsBusiness.GetStudentTotalPayment(sp.MatricNumber, sp.Session));

                FeesDescriptionGridView.DataSource = StudentsBusiness.GetStudentPayDescription(sp.MatricNumber, sp.Session);
                FeesDescriptionGridView.DataBind();
            }
            else
            {
                //If Not Redirect To Pin Entry
                logger.Info(students.MatricNumber + " - Redirecting to pin Entry page for current session - " + CurrentSession);
                Session["fromStudentLogin"] = true;
                new Utility().MessageBox("You must pay fees for the current session to continue", ResolveUrl("PinEntry.aspx?paytype=FULL"), this.Page);
                return;
            }

            #endregion

        } //end postback

    }

    protected void FeesDescriptionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Display the summary data in the appropriate cells
            e.Row.Cells[0].Text = "TOTAL AMOUNT";
            e.Row.Cells[1].Text = amount.Text;
        }
    }
}
