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
public partial class PrintCourseRegistrationForm : System.Web.UI.Page
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
            //Session["Semester"] = "First";
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
            lblsch.Text = students.Faculty.ToUpper();
            lblcourse.Text = students.CourseOfStudy.ToUpper();
            lblmatno.Text = students.MatricNumber.ToUpper();
            lblyear.Text = students.AdmittedSession.ToUpper();
            lbldegree.Text = students.Honours;
            //get personal detail
            lbldob.Text = students.DateOfBirth.ToUpper();
            surname.Text = students.Surname.ToUpper();
            othernames.Text = students.OtherNames.ToUpper();
            lblphonenumber.Text = students.PhoneNumber.ToUpper();
            lblacademiclevel.Text = students.AcademicLevel.ToUpper();
            lblprogram.Text = students.Programme.ToUpper();
            email.Text = students.Email;
            lblstate.Text = students.State.ToUpper();
            lblsex.Text = students.Sex.ToUpper();
            //lblsemester.Text = "First"; //CurrentSemester.ToUpper();
            lblsession.Text = CurrentSession.ToUpper();
            lblteachingsubject.Text = students.TeachingSubject.ToUpper();
            //lblTotCredit1.Text = DeptCoursesBusiness.getTotalSessionRegisteredCourses(students.MatricNumber, CurrentSession).ToString();

            lblTotCredit1.Text = DeptCoursesBusiness.getSemesterCoursesTotalCredit(students.CourseOfStudyID, students.AcademicLevel, Semester, students.DepartmentID, students.Programme, students.ModeOfStudy).ToString ();
            
                
            lblTotCarriedUnits.Text = DeptCoursesBusiness.getSemesterRegisteredCarryOverTotalCredit(students.MatricNumber, Semester, CurrentSession, students.AcademicLevel, students.CourseOfStudyID, students.DepartmentID, students.ModeOfStudy, students.Programme).ToString();
            if (lblTotCarriedUnits.Text.Equals("0"))
            {
                carryTable.Visible = false;
                totals.Visible = false;
                lblCarryOver.Visible = false;
            }
            else
            {
                carryTable.Visible = true;
                totals.Visible = true;
                lblCarryOver.Visible =true;
            }

         this.lblGrandTotalCreditUnits.Text =( double.Parse (lblTotCredit1.Text.ToString()) + double.Parse(lblTotCarriedUnits.Text.ToString())).ToString ();
            
            FormHeader1.Text = "COURSE(S) REGISTERED FOR " + Semester.ToUpper()  + " SEMESTER :";
            string semType = "";
            if (students.MatricNumber.Contains("ICE"))
            { 
                if (Semester == "First" && students .AcademicLevel =="ND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + "SEMESTER 1 OF 6:"; semType = "SEMESTER 1 OF 6"; }
                if (Semester == "Second" && students.AcademicLevel == "ND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 2 OF 6:"; semType = "SEMESTER 2 OF 6"; }
                if (Semester == "Third" && students.AcademicLevel == "ND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 3 OF 6:"; semType = "SEMESTER 3 OF 6"; }
                if (Semester == "First" && students.AcademicLevel == "ND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + "SEMESTER 4 OF 6:"; semType = "SEMESTER 4 OF 6"; }
                if (Semester == "Second" && students.AcademicLevel == "ND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 5 OF 6:"; semType = "SEMESTER 5 OF 6"; }
                if (Semester == "Third" && students.AcademicLevel == "ND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 6 OF 6:"; semType = "SEMESTER 6 OF 6"; }

                if (Semester == "First" && students.AcademicLevel == "HND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + "SEMESTER 1 OF 6:"; semType = "SEMESTER 1 OF 6"; }
                if (Semester == "Second" && students.AcademicLevel == "HND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 2 OF 6:"; semType = "SEMESTER 2 OF 6"; }
                if (Semester == "Third" && students.AcademicLevel == "HND I")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 3 OF 6:"; semType = "SEMESTER 3 OF 6"; }
                if (Semester == "First" && students.AcademicLevel == "HND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + "SEMESTER 4 OF 6:"; semType = "SEMESTER 4 OF 6"; }
                if (Semester == "Second" && students.AcademicLevel == "HND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 5 OF 6:"; semType = "SEMESTER 5 OF 6"; }
                if (Semester == "Third" && students.AcademicLevel == "HND II")
                { FormHeader1.Text = "COURSE(S) REGISTERED FOR " + " SEMESTER 6 OF 6:"; semType = "SEMESTER 6 OF 6"; }
            }



            if (string.IsNullOrEmpty(students.LocalPassportFile) == false)
            {
                imgPix.ImageUrl = students.LocalPassportFile;
            }
            else
            {
                imgPix.ImageUrl = "~/picx/blank.png";
            }
            string semesType = students.MatricNumber.Contains("ICE") ? semType : Semester;
            if (regApproval == 0)
            {
                new Utility().MessageBox("Please note that the APPROVAL of your course registration for " + crSession + " " + semesType + " semester is still PENDING at this time.", this.Page);
                return;
            }
            if (regApproval == 1)
            {
                new Utility().MessageBox("Your course registration for " + crSession + " " + semesType + " is now APPROVED! Click on EXAM CLEARANCE Form link at Student Control Center to print your Course Form!", ResolveUrl("~/StudentControlCenter.aspx"), this.Page);
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
