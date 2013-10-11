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

public partial class CourseRegistrationForm : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    Students students;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        So = (StudentSignOn)Session["StudentSignOn"];
        bool fromcenter = (bool)Session["isFromControlCenter"];
        if (fromcenter ==true &&  So != null)
        {
            MatricNumber = So.MatricNumber;
          
        }
        else
        {
            //session expired, complain and go back to session expired page
            logger.Info("Session timed out - Redirecting to StudentLogin.aspx");
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
                logger.Info("Defaulted to first semester");
            }
        }

        if (Page.IsPostBack == false)
        {
           
            
            //get students detail
            StudentsBusiness sb=new StudentsBusiness();
            students = sb.GetStudentsByMatNo(MatricNumber);
            Session["Students"] = students;
            //LoadLGA("Kogi");
            Session["Level"] = students.AcademicLevel;
            Session["DepartmentId"] = students.DepartmentID;
            Session["CourseofStudyId"] = students.CourseOfStudyID;
            Session["Programme"] = students.Programme;
            Session["ModeOfStudy"] = students.ModeOfStudy;
            


            
        
        ////load student biodata
        lblsch.Text = students.Faculty;
        lblcourse.Text = students.CourseOfStudy;
        lblmatno.Text = MatricNumber;
        lblyear.Text = students.AcademicLevel;

        //show carry over panel
        try
        {
            if (grdCarryOver.Rows.Count > 0)
                PanelCarryOver.Visible = true;

            //get total credit load for semester courses
            double SemesterCredit = DeptCoursesBusiness.getSemesterCoursesTotalCredit(students.CourseOfStudyID, students.AcademicLevel, Semester, students.DepartmentID, students.Programme, students.ModeOfStudy);
            double CarryOverCredit = 0.0;
            if (grdCarryOver.Rows.Count > 0)
            {
                CarryOverCredit = DeptCoursesBusiness.getSemesterCarryOverTotalCredit(students.MatricNumber,Semester);
            }
            lblTotCredit.Text = (SemesterCredit + CarryOverCredit).ToString();
            logger.Info(students.MatricNumber + " - Total registering credit was computed successfully");
        }
        catch (Exception exx)
        {
            logger.Error(exx.Message.ToString());
        }
        } //end if postback
        
       
 
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
       
        try
        {
           
            lblEntryError1.Visible = false;

            lbltitleError.Visible = false;
           
            //logger.Info(MatricNumber  + " - Student entries were OK!");
            Students st = new Students();
            

            if (Session["Students"] != null)
            {
                st = (Students)Session["Students"];
            }
            else
            {
                StudentsBusiness sb = new StudentsBusiness();
                st = sb.GetStudentsByMatNo(MatricNumber);
            }
            //register student
            string center = "01";
            bool isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, st.CourseOfStudyID, st.AcademicLevel, Semester, st.DepartmentID, st.Programme, st.ModeOfStudy,center);
            if (isRegistered == true)
            {
                logger.Info(MatricNumber + " - Registration was successful!");
                So.MatricNumber = MatricNumber;
                Session["StudentSignOn"] = So;
                Session["fromRegForm"] = true;
                Response.Redirect("RegistrationFormSubmit.aspx?Semester=" + Semester);
            }
            else
            {
                lbltitleError.Text = "Your registration is NOT successful! Kindly logout and try again...";
                logger.Info(MatricNumber + " - " + lbltitleError.Text);
            }

        }
        catch (Exception ex)
        {

            logger.Error(ex.Message);

        }

    }
   
    protected void cmbState_SelectedIndexChanged(object sender, EventArgs e)
    {

       // LoadLGA(cmbState.SelectedValue);

    }
}
