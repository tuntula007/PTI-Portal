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
public partial class RegistrationFormSubmit : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    StudentPayment sp = new StudentPayment();
    StudentPayment sp2 = new StudentPayment();
    StudentsBusiness sbiz = new StudentsBusiness(); int MinCreditLoad = 0;
    int MaxCreditLoad = 0; int CurrentCredit = 0;
    string CurrentSes = new SignOnBusiness().getCurrentSession();
    string CurrentSemester = new SignOnBusiness().getCurrentSemester();

    Students students;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentSemester.ToLower() != "first")
        {
            Session["SemesterMessage"] = "Course Registration for First semester is NOT YET available! Contact the ICT Director for more information.";
            Response.Redirect("StudentControlCenter.aspx");
            return;
        }
        So = (StudentSignOn)Session["StudentSignOn"];
        lbltitleError.Text = "";
        //bool fromregform = false;
        //fromregform = (bool)Session["fromRegForm"];
        if (Request.QueryString["matricnumber"] != null | Session["MatricNumber"] != null)
        {
            MatricNumber = (Session["MatricNumber"] != null) ? Session["MatricNumber"].ToString() : Request.QueryString["matricnumber"].ToString();
            //Session["Semester"] = Semester;
        }
        else
        {
            //session expired, complain and go back to session expired page
            // Response.Write("session expired");
            // return;
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
            }
        }
        Session["Semester"] = Semester;
        Session["StudentSignOn"] = So;
        string sss = So.MatricNumber;

        if (Page.IsPostBack == false)
        {

            Session["creditLoaded"] = 0;
            Session["CarryOverCreditText"] = 0;
            //get students detail
            students = sbiz.GetStudentsByMatNo(MatricNumber);
            if (students.AdmissionStatus.ToLower() != "admitted")
                Response.Redirect("StudentControlCenter.aspx");
            CurrentSession.Text = CurrentSes + " Session";

            //Get Student Payment Detail

            sp = sbiz.GetStudentPayments(students.MatricNumber, CurrentSes);

            //If Has Paid(Full/Part Time) Fees For Active Session
            if (sp != null)
            {
                //Paid Fully or Paid First Installment
                //If Semester is Second and Paid First Installment
                if (CurrentSemester.ToLower() != "first" & sp.PaymentType.ToLower() != "full payment")
                {
                    //Get More Payments for the active session
                    sp2 = sbiz.GetStudentPayments(students.MatricNumber, CurrentSes, CurrentSemester);
                    //if Not Paid Second Installment Redirect to Pin Entry
                    if (sp2 == null)
                    {
                        //If Not Redirect To Pin Entry
                        logger.Info(students.MatricNumber + " - Redirecting to pin Entry page for part payment");
                        Session["fromStudentLogin"] = true;
                        Response.Redirect("PinEntry.aspx?paytype=PART");
                    }
                }
            }
            else
            {
                //If Not Redirect To Pin Entry
                logger.Info(students.MatricNumber + " - Redirecting to pin Entry page for current session - " + CurrentSes);
                Session["fromStudentLogin"] = true;
                Response.Redirect("PinEntry.aspx?paytype=FULL");
            }
            //End


            Session["Students"] = students;
            //LoadLGA("Kogi");
            Session["Level"] = students.AcademicLevel;
            Session["DepartmentId"] = students.DepartmentID;
            Session["CourseofStudyId"] = students.CourseOfStudyID;
            Session["Programme"] = students.Programme;
            Session["ModeOfStudy"] = students.ModeOfStudy;

            //load student biodata
            lblsch.Text = students.Faculty;
            lblcourse.Text = students.CourseOfStudy;
            lblmatno.Text = MatricNumber.ToUpper();
            lblyear.Text = students.AcademicLevel;

            lblname.Text = " - " + students.MatricNumber.ToUpper();
            lblMess.Text = "Welcome " + students.OtherNames.ToUpper() + ", <br /> PLEASE SELECT YOUR CARRY OVERS FOR THE SEMESTER BELOW <br />Also note that: If your print out shows 'APPROVAL PENDING' Please do come back to do your final print out for submition </br> ie When APROVAL is no longer 'PENDING'";
            logger.Info(MatricNumber + " - " + lblMess.Text);
            Session["isFromRegistrationSubmit"] = true;
            string crSession = new SignOnBusiness().getCurrentSession();

            //first check if registered
            //If registered direct to print


           
           // if (DeptCoursesBusiness.getRegistrationStatus(students.MatricNumber, Semester, crSession) == true)
             bool regApproval= DeptCoursesBusiness.getRegistrationStatus(students.MatricNumber, crSession);
            if ((regApproval) == true)
            {
                PanelCanPrint.Visible = true;
                PanelCanPrint2.Visible = true;
                PanelCannotPrint.Visible = false;
                PanelCannotPrint2.Visible = false;
                RegistrationForm.Visible = false;
                RegSemester.Visible = false;
                logger.Info(MatricNumber + " - Student already registered and can print");
                lblMess.Text = "Welcome back " + students.OtherNames.ToUpper() + ", <br /> You have registered your course(s) successfully, you may wish to print your course form from the link below.";
                return;
            }
            //if not display form
            else
            {
                PanelCanPrint.Visible = false;
                PanelCanPrint2.Visible = false;
                PanelCannotPrint.Visible = true;
                PanelCannotPrint2.Visible = true;
                RegistrationForm.Visible = true;
                logger.Info(MatricNumber + " - Student has not registered and CANNOT print");
            }

            try
            {
                if (grdCarryOver.Rows.Count > 0)
                    PanelCarryOver.Visible = true;
                //Get Maximum and Minimum Credit Unit Load
                Session["MinCreditLoad"] = MinCreditLoad = DeptCoursesBusiness.getSemesterCoursesMinCredit(students.CourseOfStudyID, students.AcademicLevel, Semester, students.ModeOfStudy, students.Programme);
                Session["MaxCreditLoad"] = MaxCreditLoad = DeptCoursesBusiness.getSemesterCoursesMaxCredit(students.CourseOfStudyID, students.AcademicLevel, Semester, students.ModeOfStudy, students.Programme);
                //get total credit load for semester courses
                if (MaxCreditLoad == 0)
                {
                    PanelCanPrint.Visible = false;
                    PanelCanPrint2.Visible = false;
                    PanelCannotPrint.Visible = false;
                    PanelCannotPrint2.Visible = false;
                    RegistrationForm.Visible = false;
                    logger.Info(MatricNumber + " - Student already registered and can print");
                    lblMess.Text = lbltitleError.Text = "Maximum/Minimum Course was not Initialised for your department so far. Pls contact the admin";

                    MessageBox(lbltitleError.Text, lblmatno.Text);
                    return;
                }
                int SemesterCredit = DeptCoursesBusiness.getSemesterCoursesTotalCredit(students.CourseOfStudyID, students.AcademicLevel, Semester, students.DepartmentID, students.Programme, students.ModeOfStudy);
                this.RegistrableCourseGridView.ToolTip ="CURRENT SEMESTER CREDIT LOAD = " +SemesterCredit.ToString();
                this.grdCarryOver.ToolTip = "SELECT THE CARRY OVERS YOU WISH TO TAKE THIS SEMESTER :";
                Session["SemesterCredit"] = SemesterCredit;
                int CarryOverCredit = 0;
                if (grdCarryOver.Rows.Count > 0)
                {
                    //for PTI don't calculate at page laod:
                    //CarryOverCredit = DeptCoursesBusiness.getSemesterCarryOverTotalCredit(students.MatricNumber,CurrentSemester);
                    ;//empty statement so that alriginal code doesnt break
                }
               // lblTotCredit.Text = (SemesterCredit + CarryOverCredit).ToString();
               // lblTotCredit.Text = CarryOverCredit.ToString();
                //Session["CarryOverCreditText"] = lblTotCredit.Text;
                Session["CarryOverCreditText"] = 0;
                logger.Info(students.MatricNumber + " - Total registering credit was computed successfully");
            }
            catch (Exception exx)
            {
                logger.Error(exx.Message.ToString());
            }

        } //end if postback



    }
    protected void brnPrint_Click(object sender, EventArgs e)
    {
        Session["StudentSignOn"] = So;
        logger.Info(MatricNumber + " - Click on Print registration link");
        Session["isFromRegistrationSubmit"] = true;
        Response.Redirect("PrintRegistrationForm.aspx");
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //Check if max and min course is maintained
        MinCreditLoad = Convert.ToInt32(Session["MinCreditLoad"]);
        MaxCreditLoad = Convert.ToInt32(Session["MaxCreditLoad"]);
        CurrentCredit = Convert.ToInt32(lblTotCredit.Text);
        if (MinCreditLoad > CurrentCredit || CurrentCredit > MaxCreditLoad)
        {
            lbltitleError.Text = (CurrentCredit > MaxCreditLoad) ? "You have exceeded the maximum credit load(" + MaxCreditLoad.ToString() + ") allowed. Please try again." : "You have not selected up to the minimum credit load(" + MinCreditLoad.ToString() + ") expected. Please try again.";
            MessageBox(lbltitleError.Text, lblmatno.Text);
            return;
        }
        //Log all selected courses to db
        bool isRegistered = false;
        try
        {

            lbltitleError.Visible = false;

            //logger.Info(MatricNumber  + " - Student entries were OK!");
            Students st = new Students();


            if (Session["Students"] != null)
            {
                st = (Students)Session["Students"];
            }
            else
            {
                if (Session["MatricNumber"] != null)
                {
                    MatricNumber = Session["MatricNumber"].ToString();
                    StudentsBusiness sb = new StudentsBusiness();
                    st = sb.GetStudentsByMatNo(MatricNumber);
                }
                else
                {
                    //Timed Out - Relogin again
                    logger.Info("Session timed out - Redirecting to StudentLogin.aspx");
                    Session.Contents.Clear();
                    Response.Redirect("StudentLogin.aspx");
                }
            }
            string coursecode = "", courseunit = "", coursetype = "";
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickCourse");
                //creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                if (chkrow.Checked)
                {
                    coursecode = r.Cells[0].Text;
                    courseunit = r.Cells[2].Text;
                    coursetype = (r.Cells[3].Text.ToLower() == "c") ? "COMPULSORY" : "ELECTIVE";
                    //register student
                   // isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, Semester, coursecode,coursetype, courseunit, CurrentSes);
                    isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, coursecode, coursetype, courseunit, st.PresentSession,Semester, st.MatricNumber);// examCode)

                }
            }
            if (grdCarryOver.Rows.Count > 0)
            {
                foreach (GridViewRow r in grdCarryOver.Rows)
                {
                    CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
                    //creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                    if (chkrow.Checked)
                    {
                        coursecode = r.Cells[0].Text;
                        courseunit = r.Cells[2].Text;
                        coursetype = "CARRY OVER";
                        //register student
                        //isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, Semester, coursecode,coursetype, courseunit, CurrentSes);
                        isRegistered = DeptCoursesBusiness.Register(st.MatricNumber, coursecode, coursetype, courseunit, st.PresentSession, Semester, st.MatricNumber);// examCode)

                    }
                }
            }

            if (isRegistered == true)
            {
                logger.Info(MatricNumber + " - Registration was successful!");
                So.MatricNumber = MatricNumber;
                Session["StudentSignOn"] = So;
                Session["fromRegForm"] = true;
               
               lblSuccess.Text = "---      Registration was successful   ----";
                //Response.Redirect("RegistrationFormSubmit.aspx?Semester=" + Semester,false );
            }
            else
            {
                lbltitleError.Text = "Your registration is NOT successful! Kindly logout and try again...";
                MessageBox(lbltitleError.Text, lblmatno.Text);
                logger.Info(MatricNumber + " - " + lbltitleError.Text);
            }
        }
        catch (Exception ex)
        {

            logger.Error(ex.Message);

        }

    }
    protected void cbTickAll_CheckedChanged(object sender, EventArgs e)
    {
        //Add all credit load
        int creditLoaded = 0, unitLoaded = 0;
        //if (Session["CarryOverCreditText"] != null && int.TryParse(Session["CarryOverCreditText"].ToString(), out unitLoaded))
        //    creditLoaded = unitLoaded;
        //else
        creditLoaded = 0;
        CheckBox chk = (CheckBox)RegistrableCourseGridView.HeaderRow.FindControl("cbTickAll");
        if (chk.Checked)
        {
            creditLoaded = 0;
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickCourse");//ckTickCourse
                creditLoaded = creditLoaded + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickCourse");
                creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = false;
            }
            creditLoaded = 0;
        }

        // commented for PTI:
        //creditLoaded = creditLoaded + Convert.ToInt32(Session["CarryOverCreditText"]);
        //Session["creditLoaded"] = creditLoaded;


        //check agains minimum and maximum allowed
        MinCreditLoad = Convert.ToInt32(Session["MinCreditLoad"]);
        MaxCreditLoad = Convert.ToInt32(Session["MaxCreditLoad"]);
        if (MinCreditLoad <= creditLoaded && creditLoaded <= MaxCreditLoad)
        {
            //Is within the bounds allow
            lblTotCredit.Text = creditLoaded.ToString();
        }
        else
        {
            //Not within: raise error and deselect
            lbltitleError.Text = (MaxCreditLoad < creditLoaded) ? "You have exceeded the maximum credit load(" + MaxCreditLoad.ToString() + ") allowed. Please try again." : "You have not selected up to the minimum credit load(" + MinCreditLoad.ToString() + ") expected. Please try again.";
            MessageBox(lbltitleError.Text, lblmatno.Text);
            lbltitleError.Visible = true;
            if (chk.Checked) chk.Checked = false;
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickCourse");
                creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = false;
            }
            creditLoaded = (creditLoaded < 0) ? 0 : creditLoaded;
            //lblTotCredit.Text = Session["CarryOverCreditText"].ToString();
        }
    }
    protected void ckTickCourse_CheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;
        //Add all credit load
        int creditLoaded = 0, unitLoaded = 0;
        if (lblTotCredit.Text != null && int.TryParse(lblTotCredit.Text, out unitLoaded))
            creditLoaded = unitLoaded;
        else
            creditLoaded = 0;
        GridViewRow r = RegistrableCourseGridView.Rows[rowIndex];
        CheckBox chk = (CheckBox)r.FindControl("ckTickCourse");
        if (chk.Checked)
        {
            creditLoaded = creditLoaded + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
        }
        else
        {
            creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
        }

        Session["creditLoaded"] = creditLoaded;
        //creditLoaded = creditLoaded +  Convert.ToInt32(Session["CarryOverCreditText"]);
        //check against minimum and maximum allowed
        MinCreditLoad = Convert.ToInt32(Session["MinCreditLoad"]);
        MaxCreditLoad = Convert.ToInt32(Session["MaxCreditLoad"]);
        if (creditLoaded <= MaxCreditLoad)
        {
            //Is within the bounds allow
            lblTotCredit.Text = creditLoaded.ToString();
        }
        else
        {
            //Not within: raise error and deselect
            lbltitleError.Text = (MaxCreditLoad < creditLoaded) ? "You have exceeded the maximum credit load(" + MaxCreditLoad.ToString() + ") allowed. Please try again." : "You have not selected up to the minimum credit load(" + MinCreditLoad.ToString() + ") expected. Please try again.";
            MessageBox(lbltitleError.Text, lblmatno.Text);
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickCourse");
            creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
            chkrow.Checked = (chkrow.Checked) ? false : true;
            creditLoaded = (creditLoaded < 0) ? 0 : creditLoaded;
            lblTotCredit.Text = creditLoaded.ToString();
            lbltitleError.Visible = true;
        }
        CheckBox chkheader = (CheckBox)RegistrableCourseGridView.HeaderRow.FindControl("cbTickAll");
        if (chkheader.Checked) chkheader.Checked = false;
    }

     
    protected void ckTickedCourse_CheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;
        //Add all credit load
        int CarryOverCredit = 0, unitLoaded = 0;
        int.TryParse(lblTotCredit.Text, out unitLoaded);

        if ( unitLoaded==0)// at first page loag
            unitLoaded = int.Parse(Session["SemesterCredit"].ToString());    // initialize from current semester credit
        else
            ;   ///subsequent page loads
        GridViewRow r = grdCarryOver.Rows[rowIndex];
        CheckBox chk = (CheckBox)r.FindControl("ckTickedCourse");

       

        if (chk.Checked)
        {
            CarryOverCredit = unitLoaded + int.Parse(((HiddenField)r.FindControl("CarryOverCreditLoadHiddenField")).Value);
        }
        else
        {
            CarryOverCredit = unitLoaded - int.Parse(((HiddenField)r.FindControl("CarryOverCreditLoadHiddenField")).Value);
        }

        Session["CarryOverCreditText"] = CarryOverCredit;
        //creditLoaded = creditLoaded +  Convert.ToInt32(Session["CarryOverCreditText"]);
        //check against minimum and maximum allowed
        MinCreditLoad = Convert.ToInt32(Session["MinCreditLoad"]);
        MaxCreditLoad = Convert.ToInt32(Session["MaxCreditLoad"]);
        if (CarryOverCredit <= MaxCreditLoad)
        {
            //Is within the bounds allow
            lblTotCredit.Text = CarryOverCredit.ToString();
        }
        else
        {
            //Not within: raise error and deselect
            lbltitleError.Text = (MaxCreditLoad < CarryOverCredit) ? "You have exceeded the maximum credit load(" + MaxCreditLoad.ToString() + ") allowed. Please try again." : "You have not selected up to the minimum credit load(" + MinCreditLoad.ToString() + ") expected. Please try again.";
            MessageBox(lbltitleError.Text, lblmatno.Text);
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            CarryOverCredit = CarryOverCredit - int.Parse(((HiddenField)r.FindControl("CarryOverCreditLoadHiddenField")).Value);
            chkrow.Checked = (chkrow.Checked) ? false : true;
            CarryOverCredit = (CarryOverCredit < 0) ? 0 : CarryOverCredit;
            lblTotCredit.Text = CarryOverCredit.ToString();
            lbltitleError.Visible = true;
        }

        /// sum carryove + semester creditload:
        //int i = int.Parse(Session["SemesterCredit"].ToString());
       // CarryOverCredit = CarryOverCredit + i;
        lblTotCredit.Text = CarryOverCredit.ToString ();



        //CheckBox chkheader = (CheckBox)RegistrableCourseGridView.HeaderRow.FindControl("cbTickAll");
        //if (chkheader.Checked) chkheader.Checked = false;
    }

    public void MessageBox(string strMsg, string matno)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ");  </script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);

    }

}
