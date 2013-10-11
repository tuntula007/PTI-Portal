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
using System.IO;
using log4net;
using log4net.Config;

public partial class StudentControlCenter : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SignOnBusiness Sob = new SignOnBusiness();
    StudentsBusiness Sb = new StudentsBusiness();
    StudentPayment sp = new StudentPayment();
    StudentPayment sp2 = new StudentPayment();
    StudentSignOn So = new StudentSignOn();
    Students Stud = new Students();
    //SummerStudent summerStu = new SummerStudent();
    
    string MatricNumber = "";
    string CurrentSemester = new SignOnBusiness().getCurrentSemester();
    string CurrentSession = new SignOnBusiness().getCurrentSession();
    string haspassport = "";
    string NewMatNo = "";
    public int CourseOfStudyid = 0;
    public string StatMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
     
        
        if (Session["SemesterMessage"] != null && string.IsNullOrEmpty(Session["SemesterMessage"].ToString()) == false)
            new Utility().MessageBox(Session["SemesterMessage"].ToString(), this.Page);
        Session["SemesterMessage"] = null;
        Session["hasUploadedPix"] = false;
        if (Session["StudentSignOn"] == null)
        {
            logger.Warn("Session expires");
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        So = (StudentSignOn)Session["StudentSignOn"];
        //bool fromlogin = (bool)Session["fromLoginToControl"];
        if(So.MatricNumber.Contains("ICE"))
        {
        //this.linkPrintRegistration.Visible = false;
        //this.linkPrintFirstRegistration.Visible=true;
        //this.linkPrintSecondRegistration.Visible=true;
        //this.linkPrintThirdRegistration.Visible =true;
        }
        
        if (!(So.MatricNumber.Contains("ICE")))
        {
            //this.linkPrintThirdRegistration.Visible = false;
            //this.linkPrintFirstRegistration.Visible = false;
            //this.linkPrintSecondRegistration.Visible = false;
           // this.linkPrintRegistration.Visible = true;
        }



        Session["StudentSignOn"] = So;
        lblCurrentSession.Text = CurrentSession + " Session";

        if (Request.QueryString["matricnumber"] != null)
        {
            //MatricNumber = Request.QueryString["matricnumber"].ToString();
            String MatricNumber = Server.HtmlDecode(Request.QueryString["matricnumber"].ToString());
            //MatricNumber = Request.QueryString["matricnumber"].ToString();
            Session["MatricNumber"] = MatricNumber;
        }
        if (Session["MatricNumber"] != null)
        {
            //Get Current Session
            CurrentSession = Sob.getCurrentSession();
            //Get Current Semester
            CurrentSemester = Sob.getCurrentSemester();
            MatricNumber = (string)Session["MatricNumber"];
            //CurrentSessionSemester.Text = CurrentSession + " " + CurrentSemester + " Semester ";

            //Get Student Detail
            StudentsBusiness sb = new StudentsBusiness();
            Stud = sb.GetStudentsByMatNo(MatricNumber);
            Session["Students"] = Stud;
            //Check Student Status
            if (Stud.AdmissionStatus.ToLower().Trim() != "admitted")
            {
                //Rusticated --No ASccess
                //Suspended --No Access
                //Banned --No Acces
                if (Stud.AdmissionStatus.ToLower().Trim() != "graduated")
                {
                    //Redirect to login page:
                    new Utility().MessageBox("You are currently " + Stud.AdmissionStatus.ToLower() + ". You cannot access the portal. Thank you.", ResolveUrl("~/StudentLogin.aspx"), this.Page);
                    logger.Info("You are currently " + Stud.AdmissionStatus.ToLower() + ". You cannot access the portal. Thank you.");
                    //Session["MessageBox"] = "You are currently " + Stud.AdmissionStatus.ToLower() + ". You cannot access the portal. Thank you.";
                    //Session.Contents.Clear();
                    //Response.Redirect();
                    return;
                }
                //Graduated -Can Only Print/Transcripts
                try
                {

                    foreach (Control masterControl in Page.Controls)
                    {
                        if (masterControl is MasterPage)
                        {
                            foreach (Control formControl in masterControl.Controls)
                            {
                                if (formControl is System.Web.UI.HtmlControls.HtmlForm)
                                {
                                    foreach (Control contentControl in formControl.Controls)
                                    {
                                        if (contentControl is ContentPlaceHolder)
                                        {
                                            foreach (Control childControl in contentControl.Controls)
                                            {
                                                if (childControl is WebControl) ((WebControl)childControl).Enabled = false;
                                                else if (childControl is HtmlControl) ((HtmlControl)childControl).Disabled = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                { }
                logger.Info("You have " + Stud.AdmissionStatus.ToLower() + ". You cannot fill or edit any information on the portal. Thank you.");
                new Utility().MessageBox("You have " + Stud.AdmissionStatus.ToLower() + ". You cannot fill or edit any information on the portal. Thank you.", this.Page);

                //return;
            }
            //Admitted---can do as applicable

            
            lblWelcom.Text = "Welcome " + Stud.OtherNames + "!";
            lblMatno.Text = Stud.MatricNumber.ToUpper();
            MatricNumber = Stud.MatricNumber;
            Session["MatricNumber"] = Stud.MatricNumber;
            lblSurname.Text = Stud.Surname.ToUpper();
            lblOthernames.Text = Stud.OtherNames.ToUpper();
            lblSchool.Text = Stud.Faculty.ToUpper();
            lblDepartment.Text = Stud.Department.ToUpper();
            lblCourseOfStudy.Text = Stud.CourseOfStudy.ToUpper();
            lblLevel.Text = Stud.AcademicLevel.ToUpper();
            lblModeOfStudy.Text = Stud.ModeOfStudy.ToUpper();
            //lbldlcmail.Text = Stud.DLCMail; lbldlcmail.ToolTip = "logon to mail.p.com to check your mail box"; lbldlcmail.Font.Bold = true;
            CourseOfStudyid = Stud.CourseOfStudyID;
            if (Stud.CanChangePassport == 1) PassportUpdate.Visible = true;
            else PassportUpdate.Visible = false;
            Session["canUpdatePix"] = Stud.CanChangePassport;

            //Check if summer
            //summerStu = DeptCoursesBusiness.getSummerStudent(Stud.MatricNumber, CurrentSession);
            //disable if summer
            //if (summerStu != null && summerStu.IsActive == 1) trSummerTree.Visible = true;
            //if (CurrentSemester.ToLower() == "summer") trSummerTree.Visible = true;
            //else trSummerTree.Visible = false;

            //Session.Contents.Clear();
            Session["isFromControlCenter"] = true;
            Session["fromLoginToControl"] = true;
            So.MatricNumber = MatricNumber;
            Session["StudentSignOn"] = So;
        }
        else
        {
            //session expired, complain and go back to logon page
            logger.Warn("Session expires");
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx",false);
        }


         //Get Student Payment Detail

         sp = Sb.GetStudentPayments(Stud.MatricNumber, CurrentSession);
         
        //If Has Paid(Full/Part Time) Fees For Active Session
         if (sp != null)
         {
             //Paid Fully or Paid First Installment
             //If Semester is Second and Paid First Installment
             if (CurrentSemester.ToLower() != "first" & sp.PaymentType.ToLower() != "full payment")
             {
                 //Get More Payments for the active session
                 sp2 = Sb.GetStudentPayments(Stud.MatricNumber, CurrentSession, CurrentSemester);
                 //if Not Paid Second Installment Redirect to Pin Entry
                 if (sp2 == null)
                 {
                     //If Not Redirect To Pin Entry
                     logger.Info(Stud.MatricNumber + " - Redirecting to pin Entry page for part payment");
                     Session["fromStudentLogin"] = true;
                     Response.Redirect("PinEntry.aspx?paytype=PART");
                 }
             }
             trPayStatus.Visible = true;
             lnkPayStatus.HRef = "PayClearance.aspx";
             lblPayStatus.Text = "Payment Receipts";
         }
         else
         {
             //If Not Redirect To Pin Entry
             logger.Info(Stud.MatricNumber + " - Redirecting to pin Entry page for current session - " + CurrentSession);
             Session["fromStudentLogin"] = true;

             new Utility().MessageBox("Please enter Sch fees PIN for the current session to continue", ResolveUrl("PinEntry.aspx?paytype=FULL"), this.Page);
             return;
         }

        //determine if students already registered
        NewMatNo = Stud.MatricNumber;
        bool regstatus = false;
        int regApproval=0;
        regstatus =DeptCoursesBusiness.getRegistrationStatus(Stud.MatricNumber,CurrentSession, ref regApproval);
        if (regstatus == true)
        {
            Session["RegApproval"] = regApproval;
            trExamClearance.Visible = (regApproval == 1) ? true : false;
            trMedicalForm.Visible = (regApproval == 1) ? true : false;
            trLibraryForm.Visible = (regApproval == 1) ? true : false;
            trStudentID.Visible = (regApproval == 1) ? true : false;
            trCourseMaterial.Visible = (regApproval == 1) ? true : false;
            PanelRegStatus.Visible = true;
            logger.Info(MatricNumber + " - Students already registered");
        }
        else
        {
            PanelRegStatus.Visible = false;
            trExamClearance.Visible = false;
            logger.Info(MatricNumber + " - Students NOT YET registered");
        }
        if (Stud.ModeOfStudy.ToLower().Equals("deltas mode") && regstatus == true)
        {
            Panel4Second.Visible = true;
            Panel4First.Visible = true;
        }
        else
        {
            Panel4Second.Visible = false;
            Panel4First.Visible = false;
        }
        if (CurrentSemester.ToLower() == "second")
        {
            //Semester2Reg.Disabled = false;
            //Semester1Reg.Disabled = true;
            Panel4Second.Visible = Panel4Second.Enabled = true;
        }
        else
        {
            //Semester2Reg.Disabled = true;
            //Semester1Reg.Disabled = false;
            Panel4Second.Visible = Panel4Second.Enabled = false;
        }
        //Display Print admission letter if new student
        PrintAdmissionLetter.Visible = (Stud.isNewStudent == "1") ? true : false;
        if (string.IsNullOrEmpty(Stud.LocalPassportFile) == false)
        {
            imgPix.ImageUrl = Stud.LocalPassportFile;
            Session["hasUploadedPix"] = true;
        }
        else if (string.IsNullOrEmpty(Stud.PassportFile) == false)
        {
            imgPix.ImageUrl = Stud.LocalPassportFile;
            Session["hasUploadedPix"] = true;
        }
        else
        {
            imgPix.ImageUrl = "~/picx/blank.png";
            new Utility().MessageBox("It is noticed that you dont have any passport yet. Please upload your passport", this.Page);
            return;
        }

        //trStudentID.Visible = false;
    }
    protected void btnUploadPassport_Click(object sender, EventArgs e)
    {
        if (Session["StudentSignOn"] == null)
        {
            logger.Warn("Session expires");
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }

        panelError.Visible = true;
        Session["fromLoginToControl"] = true;
        string mainurl = Request.ServerVariables["URL"].ToString();
        string servername = Request.ServerVariables["SERVER_NAME"].ToString();
        //string url = mainurl.Replace("Personal.aspx", "picx");
        string httpref = "http://" + servername + "/picx";
        //if there is file in the upload
        if (!FileUploadPassport.HasFile)
        {
            lblerror.Text = "Browse the picture file to upload!";
            new Utility().MessageBox(lblerror.Text, this.Page);
            return;
        }
        else
        {
            int size = FileUploadPassport.PostedFile.ContentLength;
            if (size > 153000)
            {
                lblerror.Text = "File is too large than recommended size!";
                new Utility().MessageBox(lblerror.Text, this.Page);
                return;
            }
        }

        string ext = System.IO.Path.GetExtension(FileUploadPassport.FileName);
        ext = ext.ToLower().Trim();
        if (string.Equals(ext, ".png") == true || string.Equals(ext, ".jpg") == true || string.Equals(ext, ".jpeg") || string.Equals(ext, ".gif"))
        {

        } else
        {
            lblerror.Text = "Only .jpg, png and .gif files are allowed";
            new Utility().MessageBox(lblerror.Text, this.Page);
            return;
        }

        //construct file name using the mat no
        string filename = "";
        string Omatno = "";
        string Nmatno = "";
        if (string.IsNullOrEmpty(lblMatno.Text) == false)
        {
            Omatno = lblMatno.Text.Trim();

            if (Convert.ToBoolean((int)Session["canUpdatePix"]) == false)
            {
                lblerror.Text = "Disallowed! You dont have permission to update your passport";
                new Utility().MessageBox(lblerror.Text, this.Page);
                logger.Error(lblerror.Text);
                return;
            }
            
            Nmatno = Omatno.Replace("/", "");
            filename = "~/picx/" + Nmatno + ext;
        }
        else
        {
            //complain 
            lblerror.Text = "Disallowed! Session has expired, logout and logon again.";
            logger.Error(lblerror.Text);
            return;
        }
        StudentsBusiness sb = new StudentsBusiness();
        Stud = sb.GetStudentsByMatNo(lblMatno.Text);

        try
        {
            FileUploadPassport.SaveAs(Server.MapPath(filename));
            // save to database
            using (BinaryReader reader = new BinaryReader
                        (FileUploadPassport.PostedFile.InputStream))
            {
                byte[] image = reader.ReadBytes
                        (FileUploadPassport.PostedFile.ContentLength);
                PictureManager.SaveImage(Stud.RegNo, image);
            }
            logger.Info(filename + " was successfully saved");
        }
        catch (Exception exx)
        {
            lblerror.Text = "Passport could not be uploaded! Try again.";
            new Utility().MessageBox("Passport could not be uploaded! Try again.", this.Page);

            logger.Info(MatricNumber + " - " + lblerror.Text + " - " + exx.Message.ToString());
            return;
        }
        panelError.Visible = false;
        // save successful, now update the personal table with pics
        httpref = httpref + "/" + Nmatno + ext;
        bool uppass = true;
        uppass= StudentsBusiness.UpdatePassportRecord(httpref, lblMatno.Text,filename);
        if (uppass == true)
        {
            haspassport = "1";
            Session["Students"] = Stud;

            //re-determine if students already registered
            int regApprov = 0;
            if (DeptCoursesBusiness.getRegistrationStatus(NewMatNo,CurrentSession, ref regApprov) == true)
                PanelRegStatus.Visible = true;
            else
                PanelRegStatus.Visible = false;

            logger.Info(MatricNumber + " - Student uploaded password record was successfully updated!");
        }
        else
            logger.Info(MatricNumber + " - Student uploaded password record was NOT successfully updated!");
        //refresh the image passport
        
        imgPix.ImageUrl = filename;
        Response.Redirect("StudentControlCenter.aspx");

    }
    public void testIsRegistered(string var)
    {
        string sem = var;
        string matno = lblMatno.Text.Trim();
        string mode = lblModeOfStudy.Text.Trim();
        //if (sem == "First" && CurrentSemester.ToLower() != sem.ToLower()) StatMessage = "F";
        //if (sem == "Second" && CurrentSemester.ToLower() != sem.ToLower()) StatMessage = "F";
        int regApproval = 0;
        if (DeptCoursesBusiness.getRegistrationStatus(matno, CurrentSession, ref regApproval) == true && mode.ToLower().Equals("deltas mode"))
        {
            StatMessage = "T";
        }
        else
        {
            StatMessage = "F";
        }
    }
    public string isRegistered(int var)
    {
        string sem = "";
        if (var == 1)
            sem = "First";
        else
            sem = "Second";




        //if (sem == "First" && CurrentSemester.ToLower() != sem.ToLower()) return "F";
        //if (sem == "Second" && CurrentSemester.ToLower() != sem.ToLower()) return "F";

        string matno = lblMatno.Text.Trim();
        int regApproval = 0;
        if (DeptCoursesBusiness.getRegistrationStatus(matno, CurrentSession, ref regApproval) == true)
        {
            return "T";
        }
        else
        {
            return "F";
        }
    }
    public string CanRegister(int var)
    {
        string sem = "";
        if (var == 1)
            sem = "First";
        else
            sem = "Second";
        //if (sem == "First" && CurrentSemester.ToLower() != sem.ToLower()) return "F";
        //if (sem == "Second" && CurrentSemester.ToLower() != sem.ToLower()) return "F";
        if (DeptCoursesBusiness.DeptCoursesAvailable(CourseOfStudyid, sem, lblLevel.Text.Trim()) == true)
            return "T";
        else
            return "F";
    }

    protected void PrintFirstRegistration(object sender, EventArgs e)
    {
       Session["StudentSignOn"] = So;
        logger.Info(MatricNumber + " - Click on Print registration link");
        Session["isFromRegistrationSubmit"] = true;
        Response.Redirect("PrintCourseRegistrationForm.aspx");
    }
     
}
