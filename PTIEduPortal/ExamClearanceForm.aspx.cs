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
public partial class ExamClearanceForm : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(students.LocalPassportFile))
            {
                form1.Visible = false;
                new Utility().MessageBox("You cannot print your Course Registration Form without a passport. Please upload your passport first", ResolveUrl("~/StudentControlCenter.aspx"), this.Page);
                return;
            }
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
            int regApproval = 0;
            if (DeptCoursesBusiness.getRegistrationStatus(MatricNumber, crSession, ref regApproval) == false)
            {
                new Utility().MessageBox("You have NOT registered courses for " + crSession + " session, so Printing is disallowed! Click on Course Registration link to Register!", ResolveUrl("~/StudentControlCenter.aspx"), this.Page);
                return;
            }
            lblmarital.Text = students.MaritalStatus.ToUpper();
            lblcourse.Text = students.CourseOfStudy.ToUpper();
            lblmatno.Text = students.MatricNumber.ToUpper();
            //get personal detail
            name.Text = students.Surname.ToUpper() + " " + students.OtherNames.ToUpper();
            lblphonenumber.Text = students.PhoneNumber.ToUpper();
            lblacademiclevel.Text = students.AcademicLevel.ToUpper();
            email.Text = students.Email;
            lblsex.Text = students.Sex.ToUpper();

            lblsession.Text = "(" + CurrentSession.ToUpper() + " SESSION)";
            lblTotCredit1.Text = DeptCoursesBusiness.getTotalSessionRegisteredCourses(students.MatricNumber, CurrentSession).ToString();
            FormHeader1.Text = "COURSES REGISTERED FOR " + CurrentSession + " SESSION";
            if (string.IsNullOrEmpty(students.LocalPassportFile) == false)
            {
                imgPix.ImageUrl = students.LocalPassportFile;
            }
            else
            {
                imgPix.ImageUrl = "~/picx/blank.png";
            }
            if (regApproval == 0)
            {
                new Utility().MessageBox("Please note that your course registration for " + crSession + " session is still PENDING at this time. You can olny print your Pending Registration Form", ResolveUrl("~/StudentControlCenter.aspx"), this.Page);
                return;
            }
            if (regApproval == 1)
            {
                new Utility().MessageBox("Your course registration for " + crSession + " session is now APPROVED! You can print your EXAM CLEARANCE Form!", this.Page);
                return;
            }
            if (regApproval == 2)
            {
                new Utility().MessageBox("Please note that your last course registration has been DISAPPROVED! You can register again by clicking on Course Registration link at Student Control Center", ResolveUrl("~/StudentControlCenter.aspx"), this.Page);
                return;
            }


        } //end postback

    }

}
