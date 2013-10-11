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
using System.Collections.Generic;
using log4net;
using log4net.Config;

public partial class RegistrationForm : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    StudentPayment sp = new StudentPayment();
    StudentPayment sp2 = new StudentPayment();
    StudentsBusiness sbiz = new StudentsBusiness();
    int SessionMinCreditLoad = 0; int SessionMaxCreditLoad = 0;
    //int SessionMinCoreLoad = 0; int SessionMaxCoreLoad = 0;
    //int SessionMinElectiveLoad = 0; int SessionMaxElectiveLoad = 0;
    int SessionCurrentCredit = 0;
    string CurrentSes = new SignOnBusiness().getCurrentSession();
    string CurrentSemester = new SignOnBusiness().getCurrentSemester();
    //bool isRegistered = false;

    Students students;
    protected void Page_Load(object sender, EventArgs e)
    {
        int regApproval = -1;
        string crSession = new SignOnBusiness().getCurrentSession();
        //if (CurrentSemester.ToLower() != "first")
        //{
        //    Session["SemesterMessage"] = "Course Registration for First semester is NOT YET available! Contact the ICT Director for more information.";
        //    Response.Redirect("StudentControlCenter.aspx");
        //    return;
        //}
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

            Session["credit1Loaded"] = 0;
            Session["CarryOver1CreditText"] = 0;
            //Session["credit2Loaded"] = 0;
            //Session["CarryOver2CreditText"] = 0;
            //get students detail
            students = sbiz.GetStudentsByMatNo(MatricNumber);
            if (students.AdmissionStatus.ToLower() != "admitted")
                Response.Redirect("StudentControlCenter.aspx");
            CurrentSession.Text = CurrentSes + " Session";
            CurrentSession2.Text = " " + CurrentSession.Text;
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
            lblsch.Text = students.Faculty.ToUpper();
            lblcourse.Text = students.CourseOfStudy.ToUpper();
            lblmatno.Text = MatricNumber.ToUpper();
            lblyear.Text = students.AcademicLevel.ToUpper();
            //Get Maximum and Minimum Credit Unit Load
            Session["SessionMinCreditLoad"] = SessionMinCreditLoad = DeptCoursesBusiness.getSessionCoursesMinCredit(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);
            Session["SessionMaxCreditLoad"] = SessionMaxCreditLoad = DeptCoursesBusiness.getSessionCoursesMaxCredit(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);
            //Session["SessionMinCoreLoad"] = SessionMinCoreLoad = DeptCoursesBusiness.getSessionCoursesMinCore(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);
            //Session["SessionMaxCoreLoad"] = SessionMaxCoreLoad = DeptCoursesBusiness.getSessionCoursesMaxCore(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);
            //Session["SessionMinElectiveLoad"] = SessionMinElectiveLoad = DeptCoursesBusiness.getSessionCoursesMinElective(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);
            //Session["SessionMaxElectiveLoad"] = SessionMaxElectiveLoad = DeptCoursesBusiness.getSessionCoursesMaxElective(students.CourseOfStudyID, students.AcademicLevel, students.ModeOfStudy, students.Programme);

            lblname.Text = " - " + students.MatricNumber.ToUpper();
            lblMess.Text = "Welcome " + students.OtherNames.ToUpper() + ", <br /> Select your COMPULSORY(C), REQUIRED(R) and ELECTIVE(E) course(s) below.";
            //lblMess.Text += "<br /> <b>NOTE:</b><br/> Minimum Elective Course Load You can Register: " + SessionMinElectiveLoad.ToString();
            //lblMess.Text += "|        |Maximum Elective Course Load You cannot exceed: " + SessionMaxElectiveLoad.ToString();
            //lblMess.Text += "<br />  Minimum Compulsory Course Load You are expected to Register: " + SessionMinCoreLoad.ToString();
            //lblMess.Text += "|        |Maximum Compulsory Course Load to Register: " + SessionMaxCoreLoad.ToString();
            lblMess.Text += "<br /> <b>NOTE:</b><br />  Overall Minimum Course Load you MUST Register: " + SessionMinCreditLoad.ToString() + " Unit(s)";
            lblMess.Text += "|        |Overall Maximum Course Load you CANNOT exceed: " + SessionMaxCreditLoad.ToString() + " Unit(s)";
            lblMess.Text += "<br /><br /> If your registration was successful, you can print your course form and wait for the neccesary approval.";
            logger.Info(MatricNumber + " - " + lblMess.Text);
            Session["isFromRegistrationSubmit"] = true;

            //first check if registered
            //If registered direct to print
            if (DeptCoursesBusiness.getRegistrationStatus(students.MatricNumber, crSession, ref regApproval) == true)
            {
                PanelCanPrint.Visible = true;
                PanelCanPrint2.Visible = true;
                PanelCannotPrint.Visible = false;
                PanelCannotPrint2.Visible = false;
                RegistrationForm1.Visible = false;
                RegSemester.Visible = false;
                logger.Info(MatricNumber + " - Student already registered and can print");
                if (string.IsNullOrEmpty(students.LocalPassportFile))
                {
                    PanelCanPrint.Visible = false;
                    PanelCanPrint2.Visible = false;
                    PanelCannotPrint.Visible = true;
                    //PanelCannotPrint2.Visible = true;
                    lblMess.Text = "Welcome back " + students.OtherNames.ToUpper() + ", <br /> You have registered your course(s) for this session. <br />You cannot print your Course Registration Form without a passport. <br /> Please go back to <a href='StudentControlCenter.aspx'>Control Center</a> to upload your passport first";
                    return;
                }
                if (regApproval == 2)
                {
                    canPrint.Visible = lblCanPrint.Visible = true;
                    reregister.Visible = lblReRegister.Visible = true;
                    adddrop.Visible = lblAddDrop.Visible = false;
                }
                else if (regApproval == 0)
                {
                    canPrint.Visible = lblCanPrint.Visible = true;
                    reregister.Visible = lblReRegister.Visible = false;
                    adddrop.Visible = lblAddDrop.Visible = true;
                }
                else
                {
                    canPrint.Visible = lblCanPrint.Visible = true;
                    reregister.Visible = lblReRegister.Visible = false;
                    adddrop.Visible = lblAddDrop.Visible = false;
                }
                lblMess.Text = "Welcome back " + students.OtherNames.ToUpper() +
                    ((regApproval == 0) ? ", <br /> You have registered your course(s) successfully, you may wish to print your course form from the link below. <br />Note that the approval for your registration is still <b>PENDING</b>. <br /> You can also <b>ADD/DROP</b> courses from the link below" :
                    (regApproval == 1) ? ", <br /> Your Course Registration has been approved for the session, you can now print your <b>EXAM CLEARANCE FORM</b> from the link below" :
                    ", <br /> Your Course Registration has been rejected. You can re-register or print the disapproved courses from the link below.");
            }
            //if not display form
            else
            {
                PanelCanPrint.Visible = false;
                PanelCanPrint2.Visible = false;
                PanelCannotPrint.Visible = true;
                PanelCannotPrint2.Visible = true;
                RegistrationForm1.Visible = true;
                logger.Info(MatricNumber + " - Student has not registered and CANNOT print");
            }

            try
            {
                if (grdCarryOver.Rows.Count > 0)
                    PanelCarryOver.Visible = true;
                //get total credit load for semester courses
                if (SessionMaxCreditLoad == 0)
                {
                    PanelCanPrint.Visible = false;
                    PanelCanPrint2.Visible = false;
                    PanelCannotPrint.Visible = false;
                    PanelCannotPrint2.Visible = false;
                    RegistrationForm1.Visible = false;
                    logger.Info(MatricNumber + " - Student already registered and can print");
                    lblMess.Text = lbltitleError.Text = "Maximum/Minimum Course was not Initialised for your department so far. Pls contact the admin";

                    new Utility().MessageBox(lbltitleError.Text, this.Page);
                    return;
                }
                //int Semester1Credit = DeptCoursesBusiness.getSessionCoursesTotalCredit(students.CourseOfStudyID, students.AcademicLevel, students.DepartmentID, students.Programme, students.ModeOfStudy);
                //int Semester2Credit = DeptCoursesBusiness.getSemesterCoursesTotalCredit(students.CourseOfStudyID, students.AcademicLevel, "Second", students.DepartmentID, students.Programme, students.ModeOfStudy);
                int CarryOver1Credit = 0;
                CarryOver1Credit = (grdCarryOver.Rows.Count > 0) ? DeptCoursesBusiness.getSessionCarryOverTotalCredit(students.MatricNumber) : CarryOver1Credit;
                //lblTotCredit.Text = (SemesterCredit + CarryOverCredit).ToString();
                lblTotCredit1.Text = CarryOver1Credit.ToString();
                Session["CarryOver1CreditText"] = lblTotCredit1.Text;
                logger.Info(students.MatricNumber + " - Total registering credit was computed successfully");

            }
            catch (Exception exx)
            {
                logger.Error(exx.Message.ToString());
            }
            if (regApproval == 2 | regApproval == 0)
            {
                PreLoadRegistration(lblmatno.Text.Trim(), crSession);
            }

        } //end if postback

    }
    protected void brnPrint_Click(object sender, EventArgs e)
    {
        Session["StudentSignOn"] = So;
        logger.Info(MatricNumber + " - Click on Print registration link");
        Session["isFromRegistrationSubmit"] = true;
        Response.Redirect("PrintCourseRegistrationForm.aspx");
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //Check if max and min course is maintained
        SessionMinCreditLoad = Convert.ToInt32(Session["SessionMinCreditLoad"]);
        SessionMaxCreditLoad = Convert.ToInt32(Session["SessionMaxCreditLoad"]);
        //SessionMinCoreLoad = Convert.ToInt32(Session["SessionMinCoreLoad"]);
        //SessionMaxCoreLoad = Convert.ToInt32(Session["SessionMaxCoreLoad"]);
        //SessionMinElectiveLoad = Convert.ToInt32(Session["SessionMinElectiveLoad"]);
        //SessionMaxElectiveLoad = Convert.ToInt32(Session["SessionMaxElectiveLoad"]);
        SessionCurrentCredit = Convert.ToInt32(lblTotCredit1.Text);
        //if (PickedElective(RegistrableCourseGridView) > SessionMaxElectiveLoad)
        //{
        //    //You have exceeded the maximum allowed elective
        //    lbltitleError.Text = "Maximum allowed elective courses units exceeded! You cannot register more than " + SessionMaxElectiveLoad.ToString() + " credit units(s) for elective course(s). Review your selected electives and try again.";
        //    new Utility().MessageBox(lbltitleError.Text, this.Page);
        //    return;
        //}
        //if (MinCore(RegistrableCourseGridView, grdCarryOver) < SessionMinCoreLoad)
        //{
        //    //You have exceeded the maximum allowed elective
        //    lbltitleError.Text = "Minimum expected compulsory course units not exhausted. You cannot register less than " + SessionMinCoreLoad.ToString() + " credit units(s) for compulsory course(s). Review your selection and try again.";
        //    new Utility().MessageBox(lbltitleError.Text, this.Page);
        //    return;
        //}
        //int MaxCorePicked=MaxCore(RegistrableCourseGridView, grdCarryOver);
        //if (MaxCorePicked > SessionMaxCoreLoad && MaxCorePicked > SessionMaxCreditLoad)
        //{
        //    //You have exceeded the maximum allowed elective
        //    lbltitleError.Text = "Maximum allowed compulsory course units have exceeded the overall(" + SessionMaxCreditLoad.ToString() + ") credit load allowed for your level and program of study. You cannot register more than " + SessionMaxCoreLoad.ToString() + " credit units(s) for compulsory courses. You may need to drop some compulsory courses till next session.";
        //    new Utility().MessageBox(lbltitleError.Text, this.Page);
        //    return;
        //} 
        if (SessionMinCreditLoad > SessionCurrentCredit || SessionCurrentCredit > SessionMaxCreditLoad)
        {
            lbltitleError.Text = (SessionCurrentCredit > SessionMaxCreditLoad) ? "You have exceeded the maximum credit load(" + SessionMaxCreditLoad.ToString() + ") allowed for this session. Please try again." : "You have not selected up to the minimum credit load(" + SessionMinCreditLoad.ToString() + ") expected for the session. Please try again.";
            new Utility().MessageBox(lbltitleError.Text, this.Page);
            return;
        }

        //Log all selected courses to db
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
            hfdExamCode.Value = DeptCoursesBusiness.getExamClearanceCode();
            if (RegisterCourse(RegistrableCourseGridView, st.MatricNumber) == true)
            {
                if (grdCarryOver.Rows.Count > 0) RegisterCourse(grdCarryOver, st.MatricNumber);
                logger.Info(MatricNumber + " - Registration was successful!");
                So.MatricNumber = MatricNumber;
                Session["StudentSignOn"] = So;
                Session["fromRegForm"] = true;
                Response.Redirect("RegistrationForm.aspx?Semester=" + Semester);
            }
            else
            {
                lbltitleError.Text = "Your registration is NOT successful! Kindly logout and try again...";
                RegistrationForm1.Visible = false;
                new Utility().MessageBox(lbltitleError.Text, this.Page);
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
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
                creditLoaded = creditLoaded + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
                creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = false;
            }
            creditLoaded = 0;
        }
        creditLoaded = creditLoaded + Convert.ToInt32(Session["CarryOver1CreditText"].ToString());
        Session["credit1Loaded"] = creditLoaded;
        //check agains minimum and maximum allowed
        SessionMinCreditLoad = (Session["SessionMinCreditLoad"] == null) ? 0 : Convert.ToInt32(Session["SessionMinCreditLoad"].ToString());
        SessionMaxCreditLoad = (Session["SessionMaxCreditLoad"] == null) ? 0 : Convert.ToInt32(Session["SessionMaxCreditLoad"].ToString());
        if (SessionMinCreditLoad <= creditLoaded && creditLoaded <= SessionMaxCreditLoad)
        {
            //Is within the bounds allow
            lblTotCredit1.Text = creditLoaded.ToString();
        }
        else
        {
            //Not within: raise error and deselect
            lbltitleError.Text = (SessionMaxCreditLoad < creditLoaded) ? "You have exceeded the maximum credit load(" + SessionMaxCreditLoad.ToString() + ") allowed for the session. Please try again." : "You have not selected up to the minimum credit load(" + SessionMinCreditLoad.ToString() + ") expected for the session. Please try again.";
            new Utility().MessageBox(lbltitleError.Text, this.Page);
            lbltitleError.Visible = true;
            if (chk.Checked) chk.Checked = false;
            foreach (GridViewRow r in RegistrableCourseGridView.Rows)
            {
                CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
                creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
                chkrow.Checked = false;
            }
            creditLoaded = (creditLoaded < 0) ? 0 : creditLoaded;
            lblTotCredit1.Text = Session["CarryOver1CreditText"].ToString();
        }
    }
    protected void ckTickedCourse_CheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((CheckBox)sender).NamingContainer).RowIndex;
        //Add all credit load
        int creditLoaded = 0, unitLoaded = 0;
        if (lblTotCredit1.Text != null && int.TryParse(lblTotCredit1.Text, out unitLoaded))
            creditLoaded = unitLoaded;
        else
            creditLoaded = 0;
        GridViewRow r = RegistrableCourseGridView.Rows[rowIndex];
        CheckBox chk = (CheckBox)r.FindControl("ckTickedCourse");
        if (chk.Checked)
        {
            creditLoaded = creditLoaded + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
        }
        else
        {
            creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
        }

        Session["credit1Loaded"] = creditLoaded;
        //creditLoaded = creditLoaded +  Convert.ToInt32(Session["CarryOverCreditText"]);
        //check against minimum and maximum allowed
        SessionMinCreditLoad = (Session["SessionMinCreditLoad"] == null) ? 0 : Convert.ToInt32(Session["SessionMinCreditLoad"].ToString());
        SessionMaxCreditLoad = (Session["SessionMaxCreditLoad"] == null) ? 0 : Convert.ToInt32(Session["SessionMaxCreditLoad"].ToString());
        if (creditLoaded <= SessionMaxCreditLoad)
        {
            //Is within the bounds allow
            lblTotCredit1.Text = creditLoaded.ToString();
        }
        else
        {
            //Not within: raise error and deselect
            lbltitleError.Text = (SessionMaxCreditLoad < creditLoaded) ? "You have exceeded the maximum credit load(" + SessionMaxCreditLoad.ToString() + ") allowed for the session. Please try again." : "You have not selected up to the minimum credit load(" + SessionMinCreditLoad.ToString() + ") expected for the session. Please try again.";
            new Utility().MessageBox(lbltitleError.Text, this.Page);
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
            chkrow.Checked = (chkrow.Checked) ? false : true;
            creditLoaded = (creditLoaded < 0) ? 0 : creditLoaded;
            lblTotCredit1.Text = creditLoaded.ToString();
            lbltitleError.Visible = true;
        }
        CheckBox chkheader = (CheckBox)RegistrableCourseGridView.HeaderRow.FindControl("cbTickAll");
        if (chkheader.Checked) chkheader.Checked = false;
    }
    protected bool RegisterCourse(GridView CourseGrid, string MatricNumber)
    {
        bool ret = false;
        DeptCoursesBusiness.DeRegister(MatricNumber, CurrentSes);
        foreach (GridViewRow r in CourseGrid.Rows)
        {
            string coursecode = "", courseunit = "", coursetype = "";
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            //creditLoaded = creditLoaded - int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value);
            if (chkrow.Checked)
            {
                coursecode = r.Cells[0].Text;
                courseunit = r.Cells[2].Text;
                coursetype = (r.Cells[3].Text.ToLower() == "c") ? "COMPULSORY" : (r.Cells[3].Text.ToLower() == "r") ? "REQUIRED" : "ELECTIVE";
                //register student
                ret = DeptCoursesBusiness.Register(MatricNumber, coursecode, coursetype, courseunit, CurrentSes, hfdExamCode.Value);
            }
        }
        //todo: update code frm here for uniquessness
        return ret;
    }

    protected int MinCore(GridView RegistrableCourseGridView, GridView CO)
    {
        int i = 0;
        if (CO.Rows.Count > 0)
        {
            i = PickedCO(CO);
        }
        foreach (GridViewRow r in RegistrableCourseGridView.Rows)
        {
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            i = (chkrow.Checked == true && r.Cells[3].Text.ToLower() == "c") ? i + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value) : i;
        }
        return i;
    }
    protected int MaxCore(GridView RegistrableCourseGridView, GridView CO)
    {
        int i = 0;
        if (CO.Rows.Count > 0)
        {
            i = PickedCO(CO);
        }
        foreach (GridViewRow r in RegistrableCourseGridView.Rows)
        {
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            i = (chkrow.Checked == true && (r.Cells[3].Text.ToLower() == "c" | r.Cells[3].Text.ToLower() == "r")) ? i + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value) : i;
        }
        return i;
    }
    protected int PickedElective(GridView RegistrableCourseGridView)
    {
        int i = 0;
        foreach (GridViewRow r in RegistrableCourseGridView.Rows)
        {
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            i = (chkrow.Checked == true && r.Cells[3].Text.ToLower() == "e") ? i + int.Parse(((HiddenField)r.FindControl("CreditLoadHiddenField")).Value) : i;
        }
        return i;
    }
    protected int PickedCO(GridView CarryOverCourseGridView)
    {
        int i = 0;
        foreach (GridViewRow r in CarryOverCourseGridView.Rows)
        {
            CheckBox chkrow = (CheckBox)r.FindControl("ckTickedCourse");
            i = (chkrow.Checked == true && r.Cells[3].Text.ToLower() == "co") ? i + int.Parse(r.Cells[2].Text) : i;
        }
        return i;
    }
    protected void adddrop_Click(object sender, EventArgs e)
    {
        if (Session["SessionMinCreditLoad"] == null | Session["SessionMaxCreditLoad"] == null)
        {
            Session.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        canPrint.Visible = lblCanPrint.Visible = false; reregister.Visible = lblReRegister.Visible = false; adddrop.Enabled = lblAddDrop.Enabled = false;
        RegistrationForm1.Visible = true;
        lblMess.Text += "<br /> <b>NOTE:</b><br />  Overall Minimum Course Load you MUST Register: " + Session["SessionMinCreditLoad"].ToString() + " Unit(s)";
        lblMess.Text += "|        |Overall Maximum Course Load you CANNOT exceed: " + Session["SessionMaxCreditLoad"].ToString() + " Unit(s)";
        lblMess.Text += "<br /><br /> If your registration was successful, you can print your course form and wait for the neccesary approval.";
    }
    protected void reregister_Click(object sender, EventArgs e)
    {
        if (Session["SessionMinCreditLoad"] == null | Session["SessionMaxCreditLoad"] == null)
        {
            Session.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        canPrint.Visible = lblCanPrint.Visible = false; adddrop.Visible = lblAddDrop.Visible = false; reregister.Enabled = lblReRegister.Enabled = false;
        RegistrationForm1.Visible = true;
        lblMess.Text += "<br /> <b>NOTE:</b><br />  Overall Minimum Course Load you MUST Register: " + Session["SessionMinCreditLoad"].ToString() + " Unit(s)";
        lblMess.Text += "|        |Overall Maximum Course Load you CANNOT exceed: " + Session["SessionMaxCreditLoad"].ToString() + " Unit(s)";
        lblMess.Text += "<br /><br /> If your registration was successful, you can print your course form and wait for the neccesary approval.";
    }
    private void PreLoadRegistration(string mat, string session)
    {
       // List<DeptCourses> lstRegisteredCourses = DeptCoursesBusiness.getSessionRegisteredCourses(mat, session);
        List<DeptCourses> lstRegisteredCourses = DeptCoursesBusiness.getSemesterRegisteredCourses (mat,Semester ,session);
        int RegisteredCredit = 0;
        RegisteredCredit = (grdCarryOver.Rows.Count > 0) ? Convert.ToInt32(lblTotCredit1.Text.Trim()) : 0;
        if (lstRegisteredCourses.Count > 0)
        {
            DataTable dtRegisteredCourses = Utility.ConvertTo<DeptCourses>(lstRegisteredCourses);

            if (RegistrableCourseGridView.Rows.Count + grdCarryOver.Rows.Count >= dtRegisteredCourses.Rows.Count)
            {
                foreach (GridViewRow row in RegistrableCourseGridView.Rows)
                {
                    for (int i = 0; i < dtRegisteredCourses.Rows.Count; i++)
                    {
                        if (dtRegisteredCourses.Rows[i]["CourseType"].ToString().ToLower() != "co"
                            & row.Cells[0].Text.ToLower() == dtRegisteredCourses.Rows[i]["CourseCode"].ToString().ToLower())
                        {
                            CheckBox chk = (CheckBox)row.FindControl("ckTickedCourse");
                            chk.Checked = true;
                            RegisteredCredit = RegisteredCredit + Convert.ToInt32(dtRegisteredCourses.Rows[i]["CreditLoad"].ToString());
                        }
                    }
                }
                lblTotCredit1.Text = RegisteredCredit.ToString();
                Session["credit1Loaded"] = lblTotCredit1.Text;
            }
        }
    }
}
