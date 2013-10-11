using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using System.Data.SqlClient;


public partial class ApplicationForm : System.Web.UI.Page
{
    string FormNumber = "";
    //string Semester = "Second";
    //StudentSignOn So = new StudentSignOn();
    ApplicantsBusiness ab;
    ApplicantSignOn appSo = new ApplicantSignOn();
    Applicants applicant;
    DropDownList ddlSubject, ddlGrade;
    private static string str = ConfigurationManager.AppSettings["ConnString"];
    // Students students;

    #region Page Load Events
    protected void Page_Load(object sender, EventArgs e)
    {

        #region Load Page
        lbltitleError.Visible = false;
        
        int printstatus = 0;
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
        if (appSo == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }
        else
        {
            FormNumber = appSo.FormNumber;
        }
        Session["FormNumber"] = FormNumber;

 

        if (Page.IsPostBack == false)
        {
            mtvApplicationForm.Visible = false;
            //try
            {
                LoadEntranceSubjects();

                LoadModeOfEntry();
                LoadState();


                //Load Year List Box
                LoadYears();
                LoadYears(1985);


                SwitchView();
                //FillForm(FormNumber);
                printstatus = Convert.ToInt32(Session["printstatus"].ToString());

            }
            //catch (Exception exx)
            //{
            //    string exxxx = exx.Message;
            //    Session.Contents.Clear();
            //    new Utility().MessageBox("An error as occur. Your connection will be lost. Please report the incident to Admin", this.Page);
            //    Response.Redirect("ApplicantLogin.aspx");
            //}
            if (printstatus > 0)
            {
                Response.Redirect("ApplicantPrintApplicationForm.aspx");
                btnRegister.Enabled = false;
                return;
            }

        } //end if postback

        #endregion

        
    }

    private void FillForm(string FormNumber)
    {
        
        //get students detail
        StudentsBusiness sb = new StudentsBusiness();
        ab = new ApplicantsBusiness();
        applicant = ab.GetApplicantsByFormNo(FormNumber);
        Session["Applicants"] = applicant;
        Session["printstatus"] = applicant.printStatus;
        //if (ApplicantsBusiness.SubmissionClosed(applicant.Programme, applicant.PresentSession) == true)
        //{
        //    btnRegister.Enabled = false;
        //}
        //else
        //{
        //    btnRegister.Enabled = true;
        //}
        //load student personaldetails
        lblsch.Text = applicant.Programme;
        lblcourse.Text = applicant.ModeOfStudy;
        if (applicant.EntryMode.Equals("0")) { ModeOfEntry.Items.Add("0"); }
        ModeOfEntry.SelectedValue = applicant.EntryMode;
        ModeOfEntry.Enabled = (string.IsNullOrEmpty(applicant.EntryMode) == false) ? false : true;
        lblmatno.Text = applicant.FormNumber;
        lblSession.Text = ApplicantsBusiness.ActiveSession() + "  ";

        //Load Default Values like names, email, phone etc

        txtSurname.Text = applicant.Surname;
        txtOthernames.Text = applicant.OtherNames;
        txtEmail.Text = applicant.Email;
        txtPhone.Text = applicant.PhoneNumber;
       
        if (applicant.personalInfoStatus == 1)
        {
            LoadListByText(cmbTitle, applicant.Title);

            //LoadListByValue(cmbReligion, applicant.Religion.ToLower().ToUpperFirstLetter());
            //LoadListByValue(cmbBG, applicant.BloodGroup.ToLower().ToUpperFirstLetter());
            //LoadListByValue(cmbDisability, applicant.Disability.ToLower().ToUpperFirstLetter());
            txtMaiden.Text = applicant.MaidenName;
            txtAddress.Text = applicant.HomeAddress;
            txtCorAddress.Text = applicant.PostalAddress;


            txtSponsorName.Text = applicant.SponsorName;
            txtSponsorAdd.Text = applicant.SponsorAddress;
            txtSponsorPhone.Text = applicant.SponsorPhone;
            txtSponsorEmail.Text = applicant.SponsorEmail;
            txtSponsorRelationship.Text = applicant.SponsorRelationship;
            txtsponsor.Text = applicant.Sponsor;

            //load title
            LoadListByText(cmbTitle, applicant.Title.ToLower().ToUpperFirstLetter());

            //load marital status
            LoadListByText(cmbMarital, applicant.MaritalStatus);

            //load sex
            LoadListByText(cmbSex, applicant.Sex.ToLower().ToUpperFirstLetter());


            //load bloodgroup
            //LoadListByText(cmbBG, applicant.BloodGroup.ToLower().ToUpperFirstLetter());
            //load diability
            LoadListByValue(cmbDisability, applicant.Disability.ToLower().ToUpperFirstLetter());
            //load religion
            LoadListByText(cmbReligion, applicant.Religion.ToLower().ToUpperFirstLetter());

            //load nationality
            LoadListByText(cmbNation, applicant.Nationality);
            if (applicant.Nationality.ToLower() == "nigerian")
            {
                trForNigerianState.Visible = true;
                trForNigerianLGAs.Visible = true;
                trForNonNigerian.Visible = false;
                if (applicant.State.ToUpper() == "DELTA")
                {
                    //get student lga in DELTA
                    StateLGADefault("DELTA", applicant.LocalGovernmentArea.ToUpper());
                }
                else
                {
                    StateLGADefault(applicant.State.ToUpper(), applicant.LocalGovernmentArea.ToUpper());
                }
            }
            else if (applicant.Nationality.ToLower() == "non-nigerian")
            {
                //Load Non Nigeria Previously enter
                trForNonNigerian.Visible = true;
                trForNigerianState.Visible = false;
                trForNigerianLGAs.Visible = false;
                txtForNonNigerian.Text = applicant.Country;
            }


            //get date of birth
            if (!string.IsNullOrEmpty(applicant.DateOfBirth))
            {

                string[] dob = applicant.DateOfBirth.Split(new char[] { '/', '-' });
                if (dob.Length == 3)
                {
                    LoadListByText(cmbYear, dob[0]);
                    LoadListByValue(cmdMonth, dob[1]);
                    LoadListByText(cmbDay, dob[2]);
                }
            }
        }

        //Load Choice of program
        //load course of study to dropdown


        //LoadCourse(applicant.Programme, applicant.ModeOfStudy);
        LoadCourse(applicant.Programme, "PTI");

        //Load Exam SubjectTable by default 


        if (applicant.choiceStatus == 1)
        {
            //load exam center for o-level student only
            
            
            //if (applicant.EntryMode.ToLower() == "o level")
                LoadListByText(rdbExamCenter, applicant.ExaminationCenter.ToLower().ToUppercaseWords());
            //else 
                trExamCenter.Visible = true;

            //LoadListByText(rdbTeachingCenter, applicant.TeachingCenter.ToLower().ToUppercaseWords());

            //load course of study
            LoadListByText(cmb1stChoice, applicant.CourseofStudy1);
            LoadListByText(cmb2ndChoice, applicant.CourseofStudy2);
            LoadListByText(cmb3rdChoice, applicant.CourseofStudy3);
            LoadListByText(cmbEntranceExamSubj, applicant.EntranceExamsubj1);

            //Load Teaching Subject
            //LoadTeachingSubject();
            //LoadListByText(CmbTeachingSub1, applicant.TeachingSubjectOne);
            //LoadListByText(CmbTeachingSub2, applicant.TeachingSubjectTwo);

            //load previous
            LoadListByText(cmbIsPrevious, applicant.Repeating);
            if (applicant.Repeating != null && applicant.Repeating.ToLower() == "yes")
            {
                

                txtPreviousRegNo.Text = applicant.PreviousRegNo;
                LoadListByText(cmbPreviousCourse, applicant.PreviousCourseAttended);
                LoadListByText(cmbPreviousYearFrom, applicant.PreviousAttendedFrom);
                LoadListByText(cmbPreviousYearTo, applicant.PreviousAttendedTo);
            }
            else
                trPreviousInfo.Visible = false;

            trPreviousInfo.Visible = (cmbIsPrevious.SelectedItem.Text.ToLower() == "no") ? false : true;
        }
        //Load EduBackgroud
        LoadExams();
        //if (applicant.educationStatus == 1)
        //{
        LoadQualification(FormNumber);
        //}

        //Load Post EduBackground if DE
        //if (applicant.EntryMode.ToLower() == "hnd")
        if (((applicant.EntryMode.ToLower() == "nd")&&(applicant.Programme.ToLower() == "hnd"))||((applicant.EntryMode.ToLower() == "hnd") && (applicant.Programme.ToLower() == "certificate")))
        {
            trPostEdu.Visible = true;
            if (applicant.posteducationStatus == 1)
            {
                //load post edu info

                ApplicantPostQualification pq = PostSecAppEntryQualificationBusiness.getRecord(FormNumber);
                if (pq != null)
                {
                    LoadListByText(ddlPostQual, pq.QualyType);
                    txtPostCourse.Text = pq.CourseName;
                    txtPostGrade.Text = pq.CourseGrade;
                    txtPostMatNo.Text = pq.PostMatric;
                    txtPostschool.Text = pq.Institution;
                    txtPostYear.Text = pq.QualYear;
                }

            }
        }
        else
        {
            //dissabel d 
            trPostEdu.Visible = false;
        }

        //Final/Attestation Page
        if (applicant.SubmitStatus == 1)
        {
            trPartnersDetail.Visible = false;
            trForPartners.Visible = false;
            lblPartnerDetails.Text = "";
            if (applicant.Referral.ToLower().StartsWith("partners-"))
            {
                LoadListByText(cmbRefAll, "Partners");
                LoadReferral();
                LoadListByText(cmbRefPartners, applicant.Referral.Remove(0, 9));
                lblPartnerDetails.Text = (string.IsNullOrEmpty(cmbRefPartners.SelectedItem.Value) != true) ? cmbRefPartners.SelectedValue : "";
                trPartnersDetail.Visible = true;
                trForPartners.Visible = true;
            }
            else
                LoadListByText(cmbRefAll, applicant.Referral);
            chkAgree.Checked = (applicant.SubmitStatus == 1) ? true : false;
        }
    }

 
                                                                       
   

    private void LoadEntranceSubjects()
    {
      cmbEntranceExamSubj.Items.Insert(0, new ListItem("--Select--", ""));
                 
        cmbEntranceExamSubj .Items .Add ("MATHS AND PHYSICS");
        cmbEntranceExamSubj .Items .Add ("MATHS AND CHEMISTRY");
        cmbEntranceExamSubj.Items.Add("MATHS AND BUSINESS STUDIES");
        cmbEntranceExamSubj.Items.Add("HND APPLICANTS -INTERVIEW APPLICABLE");

    }

    private void LoadCourse(string prog, string mode)
    {
        //Load Course Based on Applicant Mode Of Study
        DataSet ds = ApplicantsBusiness.getAddmissionCourses(prog, "PTI");

        cmbEntranceExamSubj.DataSource = ds;
        cmb1stChoice.DataSource = ds;
        cmb2ndChoice.DataSource = ds;
        cmb3rdChoice.DataSource = ds;
        cmbPreviousCourse.DataSource = ds;

        cmb1stChoice.DataTextField = "Course";
        cmb1stChoice.DataValueField = "Srn";

        cmb2ndChoice.DataTextField = "Course";
        cmb2ndChoice.DataValueField = "Srn";

        cmb3rdChoice.DataTextField = "Course";
        cmb3rdChoice.DataValueField = "Srn";

        cmbPreviousCourse.DataTextField = "Course";
        cmbPreviousCourse.DataValueField = "Srn";

        cmb3rdChoice.DataBind();
        cmb2ndChoice.DataBind();
        cmb1stChoice.DataBind();

        cmbPreviousCourse.DataBind();
        cmb1stChoice.Items.Insert(0, new ListItem("--Select--", "0"));
        cmb2ndChoice.Items.Insert(0, new ListItem("--Select--", "0"));
        cmb3rdChoice.Items.Insert(0, new ListItem("--Select--", "0"));
        cmbPreviousCourse.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void LoadTeachingSubject()
    {
        //DataSet ds = ApplicantsBusiness.getTeachingSubject();
        //CmbTeachingSub1.DataSource = ds;
        //CmbTeachingSub2.DataSource = ds;
        //CmbTeachingSub1.DataTextField = "name";
        //CmbTeachingSub1.DataValueField = "name";
        //CmbTeachingSub2.DataTextField = "name";
        //CmbTeachingSub2.DataValueField = "name";
        //CmbTeachingSub1.DataBind();
        //CmbTeachingSub2.DataBind();
        //CmbTeachingSub1.Items.Insert(0, new ListItem("", "0"));
        //CmbTeachingSub2.Items.Insert(0, new ListItem("", "0"));
    }

    private void StateLGADefault(string state, string lga)
    {
        LoadListByText(cmbState, state);
        LoadLGA(state);
        LoadListByText(cmbLGA, lga);
    }

    private void LoadLGA(string state)
    {
        cmbLGA.DataSource = ApplicantsBusiness.GetLGA(state);
        cmbLGA.DataTextField = "NAME";
        cmbLGA.DataValueField = "NAME";
        cmbLGA.DataBind();
        cmbLGA.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void LoadState()
    {
        cmbState.DataSource = ApplicantsBusiness.GetState();
        cmbState.DataTextField = "NAME";
        cmbState.DataValueField = "NAME";
        cmbState.DataBind();
        cmbState.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void LoadYears()
    {
        for (int i = 1940; i < (DateTime.Now.Year - 10); i++)
        {
            cmbYear.Items.Add(i.ToString());
        }
    }

    private void LoadYears(int start)
    {
        start = (start > 1940 && start < DateTime.Now.Year) ? start : 1940;
        for (int i = start; i <= DateTime.Now.Year; i++)
        {
            cmbPreviousYearFrom.Items.Add(i.ToString());
            cmbPreviousYearTo.Items.Add(i.ToString());
            cmbExamDate.Items.Add(i.ToString());
        }
    }

    private void LoadYears(int start, int end)
    {
        start = (start > 1940 && start < end) ? start : 1940;
        end = (start > end) ? DateTime.Now.Year : end;
        for (int i = start; i <= end; i++)
        {
            cmbYear.Items.Add(i.ToString());
            cmbPreviousYearFrom.Items.Add(i.ToString());
            cmbPreviousYearTo.Items.Add(i.ToString());
            cmbExamDate.Items.Add(i.ToString());
        }
    }

    private void LoadModeOfEntry()
    {
        ModeOfEntry.DataSource = ApplicantsBusiness.GetEntryMode();
        ModeOfEntry.DataTextField = "EntryMode";
        ModeOfEntry.DataValueField = "EntryMode";
        ModeOfEntry.DataBind();
        ModeOfEntry.Items.Insert(0, new ListItem("Select Mode Of Entry", ""));
        //ModeOfEntry.SelectedItem.Text = "UTME";
    }

    private void LoadQualification(string formNumber)
    {
        //load exam list
        //get entry qualifications
        DataTable dtEnq = ApplicantEntryQualificationBusiness.getRecord(formNumber);
        grdvwEducation.DataSource = dtEnq;
        grdvwEducation.DataBind();
        Session["dtEnq"] = dtEnq;
        cmbExam.SelectedIndex = 0;
        cmbSeating.SelectedIndex = 0;
        cmbExamDate.SelectedIndex = 0;
        ResetExamText();
        cmbExam.Enabled = (grdvwEducation.Rows.Count >= 2) ? false : true;
        cmbSeating.Enabled = (grdvwEducation.Rows.Count >= 2) ? false : true;
        btnAddnew.Enabled = btnAddContinue.Enabled = btnAddContinueLater.Enabled = (grdvwEducation.Rows.Count >= 2) ? false : true;
        trEntrySubjects.Disabled = true;
    }

    private void LoadSubjects()
    {
        DataTable dtSubject = ApplicantEntryQualificationBusiness.getSubjects();
        #region Load Each Dropdown
        for (int i = 1; i <= 10; ++i)
        {
            ddlSubject = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());
            ddlSubject.Items.Clear();
            ddlSubject.DataSource = dtSubject;
            ddlSubject.DataTextField = "subject";
            ddlSubject.DataValueField = "subject";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }

    private void LoadSubjectsToText(DataRow drEnq)
    {
        #region Load Each Dropdown
        for (int i = 1; i <= 10; ++i)
        {
            ddlSubject = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());
            LoadListByText(ddlSubject, drEnq["SubjectName" + (i).ToString().Trim()].ToString());
        }
        #endregion

    }

    private void LoadGrades(string exId)
    {
        DataTable dtSubject = ApplicantEntryQualificationBusiness.getGrades(exId);
        #region Load Each Dropdown
        for (int i = 1; i <= 10; ++i)
        {
            ddlGrade = (DropDownList)PanelEntryQual.FindControl("CmbSittingGrade" + (i).ToString().Trim());
            ddlGrade.DataSource = dtSubject;
            ddlGrade.DataTextField = "grade";
            ddlGrade.DataValueField = "grade";
            ddlGrade.DataBind();
            ddlGrade.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }

    private void LoadGradesToText(DataRow drEnq)
    {
        #region Load Each Dropdown
        for (int i = 1; i <= 10; ++i)
        {
            ddlGrade = (DropDownList)PanelEntryQual.FindControl("CmbSittingGrade" + (i).ToString().Trim());
            LoadListByText(ddlGrade, drEnq["SubjectGrade" + (i).ToString().Trim()].ToString());
        }
        #endregion

    }

    private void LoadExams()
    {
        cmbExam.Items.Clear();
        cmbExam.DataTextField = "name";
        cmbExam.DataValueField = "id";
        cmbExam.DataSource = ApplicantEntryQualificationBusiness.getEntryExams();
        cmbExam.DataBind();
        cmbExam.Items.Insert(0, new ListItem("", ""));
    }

    private void LoadReferral()
    {
        cmbRefPartners.Items.Clear();
        cmbRefPartners.DataTextField = "name";
        cmbRefPartners.DataValueField = "description";
        cmbRefPartners.DataSource = ApplicantsBusiness.GetReferral();
        cmbRefPartners.DataBind();
        cmbRefPartners.Items.Insert(0, new ListItem("", ""));
    }

    private void LoadQualificationToText(string examRegNo)
    {
        if (Session["dtEnq"] == null) Response.Redirect("ApplicantControlCenter.aspx");
        DataTable dtEnq = (DataTable)Session["dtEnq"];
        DataColumn[] dcEnq = new DataColumn[1];
        string qry = "ExamRegNo='" + examRegNo + "'";
        dcEnq[0] = dtEnq.Columns["ExamRegNo"];
        DataRow[] drEnq = dtEnq.Select(qry);
        //dtEnq.PrimaryKey = dcEnq;
        //DataRow drEnq = dtEnq.Rows.Find(examRegNo);
        //var query = dtEnq.AsEnumerable()
        //            .Where(row => row.Field<string>("ExamRegNo") == formNumber);
        LoadListByText(cmbExam, drEnq[0]["ExamName"].ToString());
        txtExamNo.Text = drEnq[0]["ExamRegNo"].ToString();
        LoadListByValue(cmbSeating, drEnq[0]["Sitting"].ToString());
        LoadListByText(cmbExamDate, drEnq[0]["ExamDate"].ToString());
        LoadSubjects();
        LoadSubjectsToText(drEnq[0]);
        LoadGrades(cmbExam.SelectedItem.Value);
        LoadGradesToText(drEnq[0]);
        cmbExam.Enabled = true;
        cmbSeating.Enabled = true;
        trEntrySubjects.Disabled = false;
    }
    #endregion

    #region Page Utilities Method
    private void SwitchView()
    {
        mtvApplicationForm.Visible = true;
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
        FillForm(appSo.FormNumber);
        int currentView = CurrentView(appSo.FormNumber);
        mtvApplicationForm.ActiveViewIndex = currentView;
        SwitchPageView(currentView);
    }

    private void SwitchView( int currentView)
    {
        mtvApplicationForm.Visible = true;
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
        FillForm(appSo.FormNumber);
        //int currentView = CurrentView(appSo.FormNumber);
        mtvApplicationForm.ActiveViewIndex = currentView;
        SwitchPageView(currentView);
    }

    private void SwitchPageView(int currentView)
    {
        switch (currentView)
        {
            case 0:
                lnkattestation.Enabled = lnkchoiceProg.Enabled = lnkposteduinfo.Enabled = lnkeduinfo.Enabled = true;
                lnkpersonInfo.Enabled = false;
                break;
            case 1:
                lnkattestation.Enabled = lnkpersonInfo.Enabled = lnkposteduinfo.Enabled = lnkeduinfo.Enabled = true;
                lnkchoiceProg.Enabled = false;
                break;
            case 2:
                lnkattestation.Enabled = lnkposteduinfo.Enabled = lnkchoiceProg.Enabled = lnkpersonInfo.Enabled = true;
                lnkeduinfo.Enabled = false;
                break;
            case 3:
                lnkpersonInfo.Enabled = lnkattestation.Enabled = lnkchoiceProg.Enabled = lnkeduinfo.Enabled = true;
                lnkposteduinfo.Enabled = false;
                break;
            case 4:
                lnkpersonInfo.Enabled = lnkposteduinfo.Enabled = lnkchoiceProg.Enabled = lnkeduinfo.Enabled = true;
                lnkattestation.Enabled = false;
                break;
            default:
                break;
        }
    }

    private void FindDropDwnText(string ddlText, string ddlName)
    {
        LoadListByText(cmbState, ddlText);
    }

    private void LoadListByText(DropDownList cmb, string searchText)
    {
        searchText = (string.IsNullOrEmpty(searchText) == true) ? "" : searchText;
        ListItem li;
        li = cmb.Items.FindByText(searchText);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    private void LoadListByText(RadioButtonList cmb, string searchText)
    {
        searchText = (string.IsNullOrEmpty(searchText) == true) ? "" : searchText;
        ListItem li;
        li = cmb.Items.FindByText(searchText);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    private void LoadListByValue(DropDownList cmb, string searchValue)
    {
        searchValue = (string.IsNullOrEmpty(searchValue) == true) ? "" : searchValue;
        ListItem li;
        li = cmb.Items.FindByValue(searchValue);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    private void LoadListByValue(RadioButtonList cmb, string searchValue)
    {
        searchValue = (string.IsNullOrEmpty(searchValue) == true) ? "" : searchValue;
        ListItem li;
        li = cmb.Items.FindByValue(searchValue);
        if (li != null && string.IsNullOrEmpty(li.Value) == false)
        {
            cmb.ClearSelection();
            li.Selected = true;
        }

    }

    private void CompareOlevels(string dropdownSubject, string dropdownGrade)
    {
    }

    private int CurrentView(string regNo)
    {
        int i = 0;
        i = ApplicantsBusiness.GetCurrentView(regNo);
        return i;
    }

    private int ViewByEntryMode()
    {
        int i = 0;
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
        i = (appSo.ModeOfStudy != null && appSo.ModeOfStudy.ToLower() == "hnd") ? 3 : 4;
        return i;
    }

    #region Switch View Links

    protected void lnkpersonInfo_Click(object sender, EventArgs e)
    {
        SwitchView(0);
    }

    protected void lnkchoiceProg_Click(object sender, EventArgs e)
    {
        SwitchView(1);
    }

    protected void lnkeduinfo_Click(object sender, EventArgs e)
    {
        SwitchView(2);
    }

    protected void lnkposteduinfo_Click(object sender, EventArgs e)
    {
        SwitchView(3);
    }

    protected void lnkattestation_Click(object sender, EventArgs e)
    {
        SwitchView();
    }

    #endregion

    #endregion

    #region Save Personal Info Events

    protected void btnSaveContinue_Click(object sender, EventArgs e)
    {
        SavePersonalInfo(1);
    }

    protected void btnContinuelater_Click(object sender, EventArgs e)
    {
        SavePersonalInfo(2);
    }

    private void SavePersonalInfo(int actio)
    {
        lblEntryError1.Visible = false;
        string retMessage = ""; Control kontrol = new Control();
        if (ValidatePersonalnfo(ref retMessage, ref kontrol) == false)
        {
            lbltitleError.Text = retMessage;
            lbltitleError.Visible = true;
            kontrol.Focus();
            return;
        }
        Applicants ap = new Applicants();
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }


        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];

        //st.MatricNumber = MatricNumber;
        ap.FormNumber = appSo.FormNumber;
        ap.RegNo = appSo.FormNumber;
        ap.Title = cmbTitle.SelectedValue;
        ap.Surname = txtSurname.Text;
        ap.OtherNames = txtOthernames.Text;
        ap.MaidenName = txtMaiden.Text;
        ap.MaritalStatus = cmbMarital.SelectedValue;
        ap.Sex = cmbSex.SelectedValue;
        ap.Nationality = cmbNation.SelectedValue;
        ap.Country = (cmbNation.SelectedIndex == 0) ? ap.Nationality : txtForNonNigerian.Text;
        ap.State = (cmbNation.SelectedIndex == 0) ? cmbState.SelectedValue : "N/A";
        ap.personalInfoStatus = 1;
        //ap.BloodGroup = cmbBG.SelectedValue;
        ap.Disability = cmbDisability.SelectedValue;
        ap.SponsorAddress = txtSponsorAdd.Text;
        ap.SponsorEmail = txtSponsorEmail.Text;
        ap.SponsorName = txtSponsorName.Text;
        ap.SponsorPhone = txtSponsorPhone.Text;
        ap.SponsorRelationship = txtSponsorRelationship.Text;
        ap.Sponsor = txtsponsor.Text;
        ap.Religion = cmbReligion.SelectedValue;

        ap.LocalGovernmentArea = (cmbNation.SelectedIndex == 0) ? cmbLGA.SelectedValue : "N/A";
        //ap.EntryMode = ModeOfEntry.SelectedValue;
        ap.HomeAddress = txtAddress.Text;
        ap.Email = txtEmail.Text;
        ap.PhoneNumber = txtPhone.Text;
        ap.PostalAddress = (string.IsNullOrEmpty(txtCorAddress.Text)) ? txtCorAddress.Text : txtAddress.Text;
        ap.PresentSession = ap.AdmittedSession = ApplicantsBusiness.ActiveSession();

        if (cmbYear.SelectedValue != "" && cmdMonth.SelectedValue != "" && cmbDay.SelectedValue != "")
            ap.DateOfBirth = cmbYear.SelectedValue + "/" + cmdMonth.SelectedValue + "/" + cmbDay.SelectedValue;

        ApplicantsBusiness.UpdatePersonalRecord(ap);
        if (actio == 1)
            SwitchView();
        else if (actio == 2)
            Response.Redirect("ApplicantControlCenter.aspx");

    }

    private bool ValidatePersonalnfo(ref string ReturnMessage, ref Control focusControl)
    {
        bool bolFalseVal = false, bolTruVal = true;
        //start validating Biodata
        if (string.IsNullOrEmpty(cmbTitle.SelectedValue))
        {
            ReturnMessage = "Select your Title";
            focusControl = cmbTitle;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(cmbReligion.SelectedValue))
        {
            ReturnMessage = "Select your religion";
            focusControl = cmbReligion;
            return bolFalseVal;
        }
        //if (string.IsNullOrEmpty(cmbBG.SelectedValue))
        //{
        //    lbltitleError.Visible = true;
        //    lbltitleError.Text = "Select your bloood group";
        //    cmbYear.Focus();
        //    return;
        //}
        if (string.IsNullOrEmpty(cmbDisability.SelectedValue))
        {
            ReturnMessage = "Select your Disability status";
            focusControl = cmbDisability;
            return bolFalseVal;
        }


        if (string.IsNullOrEmpty(cmbDay.SelectedValue))
        {
            ReturnMessage = "Select your birth day";
            focusControl = cmbDay;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(cmdMonth.SelectedValue))
        {
            ReturnMessage = "Select your birth month";
            focusControl = cmdMonth;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(cmbYear.SelectedValue))
        {
            ReturnMessage = "Select your birth year";
            focusControl = cmbYear;
            return bolFalseVal;
        }
        DateTime resDate = new DateTime();
        if (DateTime.TryParse(cmbYear.SelectedValue + "/" + cmdMonth.SelectedValue + "/" + cmbDay.SelectedValue, out resDate) == false)
        {
            ReturnMessage = "Invalid Date Selected";
            focusControl = cmbDay;
            return bolFalseVal;
        }
        if (cmbNation.SelectedIndex == 0)
        {
            if (string.IsNullOrEmpty(cmbLGA.SelectedValue))
            {
                ReturnMessage = "Select your Local Government";
                focusControl = cmbLGA;
                return bolFalseVal;
            }
            if (string.IsNullOrEmpty(cmbState.SelectedValue))
            {
                ReturnMessage = "Select State";
                focusControl = cmbState;
                return bolFalseVal;
            }
        }
        else
        {
            if (string.IsNullOrEmpty(txtForNonNigerian.Text))
            {
                ReturnMessage = "Enter your nationality";
                focusControl = txtForNonNigerian;
                return bolFalseVal;
            }
        }
        if (string.IsNullOrEmpty(txtAddress.Text))
        {
            ReturnMessage = "Enter Your Home Address";
            focusControl = txtAddress;
            return bolFalseVal;
        }
        if (cmbSex.SelectedIndex <= 0)
        {
            ReturnMessage = "Select your Gender";
            focusControl = cmbSex;
            return bolFalseVal;
        }
        if (cmbSex.SelectedValue.ToLower() == "male")
        {
            //Compare Male Gender with title
            if (cmbTitle.SelectedValue.ToLower() == "miss" || cmbTitle.SelectedValue.ToLower() == "mrs."
                || cmbTitle.SelectedValue.ToLower() == "rev. mrs.")
            {
                ReturnMessage = "Invalid Title/Gender combination. You cant be MALE and be a " + cmbTitle.SelectedValue + " at the same time.";
                focusControl = cmbTitle;
                return bolFalseVal;
            }
        }
        else
        {
            //Compare Female gender with title
            if (cmbTitle.SelectedValue.ToLower() == "rev." || cmbTitle.SelectedValue.ToLower() == "mr.")
            {
                ReturnMessage = "Invalid Title/Gender combination. You cant be FEMALE and be a " + cmbTitle.SelectedValue + " at the same time.";
                focusControl = cmbTitle;
                return bolFalseVal;
            }

        }

        if (string.IsNullOrEmpty(txtSponsorName.Text))
        {
            ReturnMessage = "Enter your next of kin name";
            focusControl = txtSponsorName;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(txtSponsorAdd.Text))
        {
            ReturnMessage = "Enter your next of kin address";
            focusControl = txtSponsorAdd;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(txtSponsorPhone.Text))
        {
            ReturnMessage = "Enter your next of kin phone number";
            focusControl = txtSponsorAdd;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(txtSponsorRelationship.Text))
        {
            ReturnMessage = "Specify your relationship to your next of kin";
            focusControl = txtSponsorAdd;
            return bolFalseVal;
        }

        return bolTruVal;
    }

    protected void cmbNation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbNation.SelectedIndex == 1)
        {
            //this is non nigerian
            trForNonNigerian.Visible = true;
            trForNigerianState.Visible = false;
            trForNigerianLGAs.Visible = false;
        }
        else
        {
            trForNonNigerian.Visible = false;
            trForNigerianState.Visible = true;
            trForNigerianLGAs.Visible = true;
        }
    }

    protected void cmbState_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLGA(cmbState.SelectedValue);
    }

    #endregion

    #region Save Choice of Program Events

    protected void cmbIsPrevious_SelectedIndexChanged(object sender, EventArgs e)
    {
        trPreviousInfo.Visible = (cmbIsPrevious.SelectedValue.ToLower() == "no") ? false : true;
    }

    protected void btnChoiceSaveContinue_Click(object sender, EventArgs e)
    {
        SaveChoiceOfProgramInfo(1);
    }

    protected void btnChoiceContinueLater_Click(object sender, EventArgs e)
    {
        SaveChoiceOfProgramInfo(2);
    }

    private void SaveChoiceOfProgramInfo(int actio)
    {
        lblEntryError1.Visible = true;
        string retMessage = ""; Control kontrol = new Control();
        if (ValidateChoiceOfProgramlnfo(ref retMessage, ref kontrol) == false)
        {
            lbltitleError.Text = retMessage;
            lbltitleError.Visible = true;
            kontrol.Focus();
            return;
        }
        Applicants ap = new Applicants();
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }

       

        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];



        ap.EntranceExamsubj1 = cmbEntranceExamSubj.SelectedItem.Text;
       


        ap.CourseofStudy1 = cmb1stChoice.SelectedItem.Text;
        ap.CourseofStudy2 = cmb2ndChoice.SelectedItem.Text;
        ap.CourseofStudy3 = cmb3rdChoice.SelectedItem.Text;
         

        ap.ExaminationCenter = (trExamCenter.Visible == true) ? rdbExamCenter.SelectedItem.Text.ToUpper() : "";
        ap.TeachingCenter = ""; // rdbTeachingCenter.SelectedItem.Text.ToUpper();
        ap.FormNumber = lblmatno.Text;
        ap.RegNo = ap.FormNumber;
        ap.Repeating = cmbIsPrevious.SelectedItem.Text.ToUpper();
        ap.PreviousAttendedFrom = (cmbIsPrevious.Text.ToLower() == "yes") ? cmbPreviousYearFrom.SelectedItem.Text.ToUpper() : "";
        ap.PreviousAttendedTo = (cmbIsPrevious.Text.ToLower() == "yes") ? cmbPreviousYearTo.SelectedItem.Text.ToUpper() : "";
        ap.PreviousCourseAttended = (cmbIsPrevious.Text.ToLower() == "yes") ? cmbPreviousCourse.SelectedItem.Text.ToUpper() : "";
       
        ap.PreviousRegNo = (cmbIsPrevious.Text.ToLower() == "yes") ? txtPreviousRegNo.Text.ToUpper() : "";
       
        string[] facInfo1 = cmb1stChoice.SelectedValue.Split(':'), facInfo2 = cmb2ndChoice.SelectedValue.Split(':'), facInfo3 = cmb3rdChoice.SelectedValue.Split(':');
        
        ap.FirstDepartment = facInfo1[2].ToUpper();
        ap.FirstDepartmentID = Convert.ToInt32(facInfo1[1]);
        ap.FirstFacultyID = Convert.ToInt32(facInfo1[0]);

        ap.SecondDepartment = facInfo2[2].ToUpper();
        ap.SecondDepartmentID = Convert.ToInt32(facInfo2[1]);
        ap.SecondFacultyID = Convert.ToInt32(facInfo2[0]);

        ap.ThirdDepartment = facInfo3[2].ToUpper();
        ap.ThirdDepartmentID = Convert.ToInt32(facInfo3[1]);
        ap.ThirdFacultyID = Convert.ToInt32(facInfo3[0]);


        //version 1.1
        ap.choiceStatus = 1;

        ApplicantsBusiness.UpdateProgramChoiceRecord(ap);
        if (actio == 1)
            SwitchView();
        else if (actio == 2)
            Response.Redirect("ApplicantControlCenter.aspx");
    }

    private bool ValidateChoiceOfProgramlnfo(ref string ReturnMessage, ref Control focusControl)
    {
        bool bolFalseVal = false, bolTruVal = true;
        string cm1 = cmb1stChoice.SelectedValue;
        string cm = cmb1stChoice.SelectedItem.Text.Substring(0, 2);

        if (((cmb1stChoice.SelectedItem.Text).Substring(0, 2)=="ND") && (cmb2ndChoice.SelectedItem.Text.Substring(0, 2)!="ND" || cmb3rdChoice.SelectedItem.Text.Substring(0, 2)!="ND"))
        {
            /// ALL CHOICES MUST BE FROM  THE SAME PROGRAME
            ReturnMessage = "ATTENSION ALL THREE COURSES MUST BE FROM  THE SAME PROGRAME";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;

        }

        if (((cmb1stChoice.SelectedItem.Text).Substring(0, 3)=="HND") && (cmb2ndChoice.SelectedItem.Text.Substring(0, 3)!="HND" || cmb3rdChoice.SelectedItem.Text.Substring(0, 3)!="HND"))
        {
            /// ALL CHOICES MUST BE FROM  THE SAME PROGRAME
            ReturnMessage = "ALL THREE COURSES MUST BE FROM  THE SAME PROGRAME";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;
        }

        if (((cmb1stChoice.SelectedItem.Text).Substring(0, 11)=="CERTIFICATE") && (cmb2ndChoice.SelectedItem.Text.Substring(0, 11)!="CERTIFICATE" || cmb3rdChoice.SelectedItem.Text.Substring(0, 11)!="CERTIFICATE"))
        {
            /// ALL CHOICES MUST BE FROM  THE SAME PROGRAME
            ReturnMessage = "ALL THREE COURSES MUST BE FROM  THE SAME PROGRAME";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;
        }

        if (((cmb1stChoice.SelectedItem.Text).Substring(0, 2) == "ND") && (cmbEntranceExamSubj.SelectedItem.Text.Substring(0, 3) == "HND"))
        {
            /// ALL CHOICES MUST BE FROM  THE SAME PROGRAME
            ReturnMessage = "PLEASE CHOOSE A VALID EXAM CENTER";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;

        }
        if (((cmb1stChoice.SelectedItem.Text).Substring(0, 11) == "CERTIFICATE") && (cmbEntranceExamSubj.SelectedItem.Text.Substring(0, 3) == "HND"))
        {
            /// ALL CHOICES MUST BE FROM  THE SAME PROGRAME
            ReturnMessage = "PLEASE CHOOSE A VALID EXAM CENTER";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;

        }
        if (string.IsNullOrEmpty(cmbEntranceExamSubj.SelectedValue))
        {
            ReturnMessage = "Please select entrance exam sumbjects";
            focusControl = cmbEntranceExamSubj;
            return bolFalseVal;
        }


        if (string.IsNullOrEmpty(cmb1stChoice.SelectedValue))
        {
            ReturnMessage = "Please select first program of choice";
            focusControl = cmb1stChoice;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(cmb2ndChoice.SelectedValue))
        {
            ReturnMessage = "Please select second program of choice";
            focusControl = cmb2ndChoice;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(cmb3rdChoice.SelectedValue))
        {
            ReturnMessage = "Please select Third program of choice";
            focusControl = cmb3rdChoice;
            return bolFalseVal;
        }


        //if (string.IsNullOrEmpty(CmbTeachingSub1.SelectedValue))
        //{
        //    ReturnMessage = "Please select first teaching subject of choice. Select NOT APPLICABLE if not applying to faculty of education";
        //    focusControl = CmbTeachingSub1;
        //    return bolFalseVal;
        //}

        //if (string.IsNullOrEmpty(CmbTeachingSub2.SelectedValue))
        //{
        //    ReturnMessage = "Please select second teaching subject of choice. Select NOT APPLICABLE if not applying to faculty of education";
        //    focusControl = CmbTeachingSub2;
        //    return bolFalseVal;
        //}
        //Session["IsEdu"] = false;
        //if (ApplicantsBusiness.isEducation(cmb1stChoice.SelectedItem.Text) == true | ApplicantsBusiness.isEducation(cmb2ndChoice.SelectedItem.Text) == true)
        //{
        //    Session["IsEdu"] = true;
        //    if (string.IsNullOrEmpty(CmbTeachingSub1.SelectedValue))
        //    {
        //        ReturnMessage = "You must select teaching subject if applying to faculty of education";
        //        focusControl = CmbTeachingSub1;
        //        return bolFalseVal;
        //    }
        //    if (string.IsNullOrEmpty(CmbTeachingSub2.SelectedValue))
        //    {
        //        ReturnMessage = "You must select teaching subject if applying to faculty of education";
        //        focusControl = CmbTeachingSub2;
        //        return bolFalseVal;
        //    }
        //    if (CmbTeachingSub1.Text.ToLower() == "not applicable" | CmbTeachingSub2.Text.ToLower() == "not applicable")
        //    {
        //        ReturnMessage = "You cannot select NOT APPLICABLE for teaching subjects if applying to faculty of education";
        //        focusControl = CmbTeachingSub1;
        //        return bolFalseVal;
        //    }
        //}
        if (cmbIsPrevious.Text.ToLower() == "yes")
        {
            if (string.IsNullOrEmpty(txtPreviousRegNo.Text))
            {
                ReturnMessage = "Specify your former Matric Number";
                focusControl = txtPreviousRegNo;
                return bolFalseVal;
            }
            if (string.IsNullOrEmpty(cmbPreviousCourse.SelectedItem.Text))
            {
                ReturnMessage = "Select your last program of study";
                focusControl = cmbPreviousCourse;
                return bolFalseVal;
            }
            if (string.IsNullOrEmpty(cmbEntranceExamSubj.SelectedItem.Text))
            {
                ReturnMessage = "Select your Entrance Exam Subjetcs Combination";
                focusControl = cmbEntranceExamSubj;
                return bolFalseVal;
            }
            if (string.IsNullOrEmpty(cmbPreviousYearFrom.SelectedItem.Text))
            {
                ReturnMessage = "Select the start year of the previous program";
                focusControl = cmbPreviousYearFrom;
                return bolFalseVal;
            }
            if (string.IsNullOrEmpty(cmbPreviousYearTo.SelectedItem.Text))
            {
                ReturnMessage = "Select the last year attended";
                focusControl = cmbPreviousYearTo;
                return bolFalseVal;
            }
            if (Convert.ToInt32(cmbPreviousYearFrom.Text) > Convert.ToInt32(cmbPreviousYearTo.Text))
            {
                ReturnMessage = "Start Year cannot be earlier than End Year.";
                focusControl = cmbPreviousYearFrom;
                return bolFalseVal;
            }

        }
        //if (rdbTeachingCenter.SelectedIndex < 0)
        //{
        //    ReturnMessage = "Select the location for your face to face lecture";
        //    focusControl = rdbTeachingCenter;
        //    return bolFalseVal;
        //}
        if (trExamCenter.Visible == true & rdbExamCenter.SelectedIndex < 0)
        {
            ReturnMessage = "Select your Examination Center";
            focusControl = rdbExamCenter;
            return bolFalseVal;
        }

        return bolTruVal;
    }


    #endregion

    #region Save Educational Info

    private void SaveEducationInfo(int actio)
    {
        lblEntryError1.Visible = true;
        string retMessage = ""; Control kontrol = new Control();
        if (ValidateEducationlnfo(ref retMessage, ref kontrol) == false)
        {
            lbltitleError.Text = retMessage;
            lbltitleError.Visible = true;
            kontrol.Focus();
            new Utility().MessageBox(retMessage, this.Page);
            return;
        }
        Applicants ap = new Applicants();
        ApplicantEntryQualification eq = new ApplicantEntryQualification();
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }
        
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];
        string Olevel1 = "";
        string SubjectGrade1 = "";
        string SubjectGrade2 = "";
        string SubjectGrade3 = "";
        string SubjectGrade4 = "";
        string SubjectGrade5 = "";
        string SubjectGrade6 = "";
        string SubjectGrade7 = "";
        string SubjectGrade8 = "";
        string SubjectGrade9 = "";
        string SubjectGrade10 = "";


        //start Entry qualification assignment for new students
        eq.ExamName = cmbExam.SelectedItem.Text;
        eq.ExamRegNo = txtExamNo.Text;
        eq.ExamDate = cmbExamDate.SelectedItem.Text;
        eq.Sitting = cmbSeating.SelectedItem.Value;
        // eq.RegNo = MatricNumber;
        // eq.MatricNumber = MatricNumber;
        eq.RegNo = appSo.FormNumber;
        eq.UserName = appSo.UserName;
        eq.SubjectName1 = CmbSittingSubj1.SelectedValue;
        eq.SubjectName2 = CmbSittingSubj2.SelectedValue;
        eq.SubjectName3 = CmbSittingSubj3.SelectedValue;
        eq.SubjectName4 = CmbSittingSubj4.SelectedValue;
        eq.SubjectName5 = CmbSittingSubj5.SelectedValue;
        eq.SubjectName6 = CmbSittingSubj6.SelectedValue;
        eq.SubjectName7 = CmbSittingSubj7.SelectedValue;
        eq.SubjectName8 = CmbSittingSubj8.SelectedValue;
        eq.SubjectName9 = CmbSittingSubj9.SelectedValue;
        eq.SubjectName10 = CmbSittingSubj10.SelectedValue;

        eq.SubjectGrade1 = CmbSittingGrade1.SelectedValue;
        eq.SubjectGrade2 = CmbSittingGrade2.SelectedValue;
        eq.SubjectGrade3 = CmbSittingGrade3.SelectedValue;
        eq.SubjectGrade4 = CmbSittingGrade4.SelectedValue;
        eq.SubjectGrade5 = CmbSittingGrade5.SelectedValue;
        eq.SubjectGrade6 = CmbSittingGrade6.SelectedValue;
        eq.SubjectGrade7 = CmbSittingGrade7.SelectedValue;
        eq.SubjectGrade8 = CmbSittingGrade8.SelectedValue;
        eq.SubjectGrade9 = CmbSittingGrade9.SelectedValue;
        eq.SubjectGrade10 = CmbSittingGrade10.SelectedValue;

        if (eq.SubjectName1.Length > 4 && eq.SubjectGrade1 != "")
        {
            SubjectGrade1 = eq.SubjectName1.Trim().Substring(0, 4) + "=" + eq.SubjectGrade1.Trim() + ",";
        }
        else
        {
            SubjectGrade1 = eq.SubjectName1.Trim() + "=" + eq.SubjectGrade1.Trim() + ",";
        }

        if (eq.SubjectName2.Length > 4 && eq.SubjectGrade2 != "")
        {
            SubjectGrade2 = eq.SubjectName2.Trim().Substring(0, 4) + "=" + eq.SubjectGrade2.Trim() + ",";
        }
        else
        {
            SubjectGrade2 = eq.SubjectName2.Trim() + "=" + eq.SubjectGrade2.Trim() + ",";
        }

        if (eq.SubjectName3.Length > 4 && eq.SubjectGrade3 != "")
        {
            SubjectGrade3 = eq.SubjectName3.Trim().Substring(0, 4) + "=" + eq.SubjectGrade3.Trim() + ",";
        }
        else
        {
            SubjectGrade3 = eq.SubjectName3.Trim() + "=" + eq.SubjectGrade3.Trim() + ",";
        }

        if (eq.SubjectName4.Length > 4 && eq.SubjectGrade4 != "")
        {
            SubjectGrade4 = eq.SubjectName4.Trim().Substring(0, 4) + "=" + eq.SubjectGrade4.Trim() + ",";
        }
        else
        {
            SubjectGrade4 = eq.SubjectName4.Trim() + "=" + eq.SubjectGrade4.Trim() + ",";
        }

        if (eq.SubjectName5.Length > 4 && eq.SubjectGrade5 != "")
        {
            SubjectGrade5 = eq.SubjectName5.Trim().Substring(0, 4) + "=" + eq.SubjectGrade5.Trim() + ",";
        }
        else
        {
            SubjectGrade5 = eq.SubjectName5.Trim() + "=" + eq.SubjectGrade5.Trim() + ",";
        }

        if (eq.SubjectName6.Length > 4 && eq.SubjectGrade6 != "")
        {
            SubjectGrade6 = eq.SubjectName6.Trim().Substring(0, 4) + "=" + eq.SubjectGrade6.Trim() + ",";
        }
        else
        {
            SubjectGrade6 = eq.SubjectName6.Trim() + "=" + eq.SubjectGrade6.Trim() + ",";
        }

        if (eq.SubjectName7.Length > 4 && eq.SubjectGrade7 != "")
        {
            SubjectGrade7 = eq.SubjectName7.Trim().Substring(0, 4) + "=" + eq.SubjectGrade7.Trim();
        }
        else
        {
            SubjectGrade7 = eq.SubjectName7.Trim() + "=" + eq.SubjectGrade7.Trim() + ",";
        }

        if (eq.SubjectName8.Length > 4 && eq.SubjectGrade8 != "")
        {
            SubjectGrade8 = eq.SubjectName8.Trim().Substring(0, 4) + "=" + eq.SubjectGrade8.Trim();
        }
        else
        {
            SubjectGrade8 = eq.SubjectName8.Trim() + "=" + eq.SubjectGrade8.Trim() + ",";
        }
        if (eq.SubjectName9.Length > 4 && eq.SubjectGrade9 != "")
        {
            SubjectGrade9 = eq.SubjectName9.Trim().Substring(0, 4) + "=" + eq.SubjectGrade9.Trim();
        }
        else
        {
            SubjectGrade9 = eq.SubjectName9.Trim() + "=" + eq.SubjectGrade9.Trim() + ",";
        }
        if (eq.SubjectName10.Length > 4 && eq.SubjectGrade10 != "")
        {
            SubjectGrade10 = eq.SubjectName10.Trim().Substring(0, 4) + "=" + eq.SubjectGrade10.Trim();
        }
        else
        {
            SubjectGrade10 = eq.SubjectName10.Trim() + "=" + eq.SubjectGrade10.Trim() + ",";
        }


        Olevel1 = SubjectGrade1.Trim() + SubjectGrade2.Trim() + SubjectGrade3.Trim() + SubjectGrade4.Trim() + SubjectGrade5.Trim() + SubjectGrade6.Trim() + SubjectGrade7.Trim() + SubjectGrade8.Trim() + SubjectGrade9.Trim() + SubjectGrade10.Trim();

        #region Upload Of Scanned Result Section- Can Be diabled from Here

        //using (BinaryReader reader = new BinaryReader
        //    (fupResulst.PostedFile.InputStream))
        //{
        //    byte[] image = reader.ReadBytes
        //            (fupResulst.PostedFile.ContentLength);
        //    eq.ScannedResult = image;
        //}
        #endregion

        //Applicants applican = new Applicants();
        ab = new ApplicantsBusiness();
          applicant =  ab.GetApplicantsByFormNo(FormNumber);

        if (actio == 1)
        {
            ap.educationStatus = 1;
            string strQuery = "";
            strQuery = "UPDATE APPLICANTS SET [EducationStatus] = 1 WHERE regno='" + applicant.RegNo.ToUpper() + "' OR FormNumber='" + applicant.FormNumber.ToUpper() + "'";
            new Utility().PerformQuery(strQuery);
        }

        ApplicantsBusiness.UpdateEducationRecord(ap, eq, Olevel1);
        //reload grid
        LoadQualification(lblmatno.Text);
        if (actio == 1)
            SwitchView(); //ViewByEntryMode()
        else if (actio == 2)
            Response.Redirect("ApplicantControlCenter.aspx");
    }

    private bool ValidateEducationlnfo(ref string ReturnMessage, ref Control focusControl)
    {
        bool bolFalseVal = false, bolTruVal = true;
        int subCounter = 0;
        if (cmbSeating.SelectedIndex < 1)
        {
            ReturnMessage = "You cannot add Education Information without selectin seating type";
            focusControl = cmbSeating;
            return bolFalseVal;
        }
        //if (grdvwEducation.Rows.Count >= 2)
        //{
        //    ReturnMessage = "You cannot add more than two sittings";
        //    focusControl = txtExamNo;
        //    return bolFalseVal;
        //}


        //firstly validate entry subjects for new students

        if (string.IsNullOrEmpty(cmbExam.Text))
        {
            ReturnMessage = "Enter Exam name";
            focusControl = cmbExam;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(cmbExamDate.Text))
        {
            ReturnMessage = "Enter Exam Date";
            focusControl = cmbExamDate;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(txtExamNo.Text))
        {
            ReturnMessage = "Enter Exam Number";
            focusControl = txtExamNo;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(CmbSittingSubj1.SelectedValue))
        {
            ReturnMessage = "Select your first subject";
            focusControl = CmbSittingSubj1;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(CmbSittingSubj2.SelectedValue))
        {
            ReturnMessage = "Select your second subject";
            focusControl = CmbSittingSubj2;
            return bolFalseVal;
        }

        #region //test if grade for english and mathematics are entered
        if (string.IsNullOrEmpty(CmbSittingSubj1.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade1.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj1.SelectedValue;
                focusControl = CmbSittingGrade1;
                return bolFalseVal;
            }
        }
        if (string.IsNullOrEmpty(CmbSittingSubj2.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade2.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj2.SelectedValue;
                focusControl = CmbSittingGrade2;
                return bolFalseVal;
            }
        }
        #endregion

        #region test if at least 5 subjects were selected
        subCounter = subjCounter();
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj3.SelectedValue))
        {
            ReturnMessage = "You must select at least five subject";
            focusControl = CmbSittingSubj3;
            return bolFalseVal;
        }
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj4.SelectedValue))
        {
            lblEntryError1.Text = "You must select at least five subject";
            focusControl = CmbSittingSubj4;
            return bolFalseVal;
        }
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj5.SelectedValue))
        {
            ReturnMessage = "You must select at least five subject";
            focusControl = CmbSittingSubj5;
            return bolFalseVal;
        }
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj6.SelectedValue))
        {
            lblEntryError1.Text = "You must select at least five subject";
            focusControl = CmbSittingSubj6;
            return bolFalseVal;
        }
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj7.SelectedValue))
        {
            ReturnMessage = "You must select at least five subject";
            focusControl = CmbSittingSubj7;
            return bolFalseVal;
        }
        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj8.SelectedValue))
        {
            lblEntryError1.Text = "You must select at least five subject";
            focusControl = CmbSittingSubj8;
            return bolFalseVal;
        }

        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj9.SelectedValue))
        {
            lblEntryError1.Text = "You must select at least five subject";
            focusControl = CmbSittingSubj9;
            return bolFalseVal;
        }

        if (subCounter < 5 & string.IsNullOrEmpty(CmbSittingSubj10.SelectedValue))
        {
            lblEntryError1.Text = "You must select at least five subject";
            focusControl = CmbSittingSubj10;
            return bolFalseVal;
        }
        #endregion

        #region Test if grades were selected for the other 8 selected subjects

        if (string.IsNullOrEmpty(CmbSittingSubj3.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade3.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj3.SelectedValue;
                focusControl = CmbSittingGrade3;
                return bolFalseVal;
            }
        }
        else if (string.IsNullOrEmpty(CmbSittingGrade3.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj3.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade3.SelectedValue + ")";
                focusControl = CmbSittingSubj3;
                return bolFalseVal;
            }
        }


        if (string.IsNullOrEmpty(CmbSittingSubj4.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade4.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj4.SelectedValue;
                focusControl = CmbSittingGrade4;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade4.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj4.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade4.SelectedValue + ")";
                focusControl = CmbSittingSubj4;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj5.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade5.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj5.SelectedValue;
                focusControl = CmbSittingGrade5;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade5.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj5.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade5.SelectedValue + ")";
                focusControl = CmbSittingSubj5;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj6.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade6.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj6.SelectedValue;
                focusControl = CmbSittingGrade6;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade6.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj6.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade6.SelectedValue + ")";
                focusControl = CmbSittingSubj6;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj7.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade7.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj7.SelectedValue;
                focusControl = CmbSittingGrade7;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade7.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj7.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade7.SelectedValue + ")";
                focusControl = CmbSittingSubj7;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj8.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade8.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj8.SelectedValue;
                focusControl = CmbSittingGrade8;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade8.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj8.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade8.SelectedValue + ")";
                focusControl = CmbSittingSubj8;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj9.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade9.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj9.SelectedValue;
                focusControl = CmbSittingGrade9;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade9.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj9.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade9.SelectedValue + ")";
                focusControl = CmbSittingSubj9;
                return bolFalseVal;
            }
        }

        if (string.IsNullOrEmpty(CmbSittingSubj10.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingGrade10.SelectedValue))
            {
                ReturnMessage = "Select grade for " + CmbSittingSubj10.SelectedValue;
                focusControl = CmbSittingGrade10;
                return bolFalseVal;
            }
        }//CmbSitting1Subj1
        else if (string.IsNullOrEmpty(CmbSittingGrade10.SelectedValue) == false)
        {
            if (string.IsNullOrEmpty(CmbSittingSubj10.SelectedValue))
            {
                ReturnMessage = "You have not selected subject for the selected grade(" + CmbSittingGrade10.SelectedValue + ")";
                focusControl = CmbSittingSubj10;
                return bolFalseVal;
            }
        }

        #endregion

        #region CompareOlevels Entry 1
        ArrayList arrSubjects = new ArrayList();
        string subject = ""; lblEntryError1.Text = "";
        int count = 0;
        for (int i = 1; i <= 10; ++i)
        {
            ddlSubject = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());
            ddlGrade = (DropDownList)PanelEntryQual.FindControl("CmbSittingGrade" + (i).ToString().Trim());
            if (ddlSubject.SelectedIndex != 0 && ddlGrade.SelectedIndex != 0)
            {
                subject = ddlSubject.SelectedValue.Trim();
                if (arrSubjects.Contains(subject))
                {
                    ReturnMessage = "You have selected " + subject + " more than once.";
                    focusControl = ddlSubject;
                    //lblMessage.Text = "You seleted a subject more than once.";
                    return bolFalseVal;
                }
                arrSubjects.Add(subject);
                count++;
            }
        }
        #endregion

        #region Check If Same Exam Type For Different Seatings
        for (int i = 0; i < grdvwEducation.Rows.Count; i++)
        {
            if (grdvwEducation.Rows[i].Cells[0].Text != cmbSeating.SelectedItem.Value)
            {
                if (grdvwEducation.Rows[i].Cells[1].Text == cmbExam.SelectedItem.Text
                    & grdvwEducation.Rows[i].Cells[3].Text == cmbExamDate.SelectedItem.Value)
                {
                    ReturnMessage = "You cannot select " + cmbExam.SelectedItem.Text + " for same year and for more than one seating";
                    focusControl = cmbExam;
                    return bolFalseVal;
                }
            }
        }
        #endregion

        //end if test for Entry Qualification
        //end of Validate Subjects for new students

        #region Commented Routine For Scanned olevels
        //#region Check if Scanned Results is uploaded
        //if (!(fupResulst.HasFile))
        //{
        //    ReturnMessage = "Add the scanned copy of the selected result. Note that the scanned image must not be more than 15kb";
        //    focusControl = fupResulst;
        //    return bolFalseVal;
        //}
        //#endregion

        //#region Check if scanned results is valid image file
        //string ext = System.IO.Path.GetExtension(fupResulst.FileName);
        //ext = ext.ToLower().Trim();
        //if (string.Equals(ext, ".png") == true || string.Equals(ext, ".jpg") == true || string.Equals(ext, ".jpeg") || string.Equals(ext, ".gif"))
        //{

        //}
        //else
        //{
        //    ReturnMessage = "You have uploaded wrong file format. Only .jpg, png and .gif files formats are allowed";
        //    focusControl = fupResulst;
        //    return bolFalseVal;
        //}
        //#endregion

        //#region Check If 15kb size limit is exceded
        //int size = fupResulst.PostedFile.ContentLength;
        //if (size > 153000)
        //{
        //    ReturnMessage = "You have exceeded the allowed size limit for result upload. Note that the scanned image must not be more than 15kb";
        //    focusControl = fupResulst;
        //    return bolFalseVal;
        //}

        //#endregion 
        #endregion

        return bolTruVal;
    }

    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        SaveEducationInfo(0);
    }

    protected void btnAddContinue_Click(object sender, EventArgs e)
    {
        SaveEducationInfo(1);
    }

    protected void btnAddContinueLater_Click(object sender, EventArgs e)
    {
        SaveEducationInfo(2);
    }

    protected void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbExam.SelectedIndex > 0)
        {
            LoadSubjects();
            LoadGrades(cmbExam.SelectedValue);
            trEntrySubjects.Disabled = false;

            if (cmbExam.SelectedIndex > 6)
            {
                cmbSeating.SelectedIndex = 2;
                txtExamNo.Focus();
             
            }

        }
        else
        trEntrySubjects.Disabled = true;
        cmbSeating.SelectedIndex = 0;
        CmbSittingSubj1.SelectedIndex = 0;
        CmbSittingSubj2.SelectedIndex = 0;
        CmbSittingSubj1.Enabled = true;
        CmbSittingSubj2.Enabled = true;
    }

    protected void cmbSeating_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(cmbExam.SelectedIndex > 0) & cmbSeating.SelectedIndex > 0)
        {
            cmbSeating.SelectedIndex = 0;
            new Utility().MessageBox("Please select exam type first", this.Page);
            cmbExam.Focus();
            return;
        }
        if (cmbSeating.SelectedItem.Value == "1")
        {
            
            if (cmbExam.SelectedIndex > 6)
            {
                cmbSeating.SelectedIndex = 2; 
                new Utility().MessageBox("Based on The Exam that is currently Selected, The system has selected Second as the Seating Type.You may continue with other items.Thank You", this.Page);
                cmbExam.Focus();
                return;
                
            }

            CmbSittingSubj1.SelectedIndex = 1;
            CmbSittingSubj2.SelectedIndex = 2;
            CmbSittingSubj1.Enabled = false;
            CmbSittingSubj2.Enabled = false;
            FilterEnglishAndMat();
        }
       
        else
        {
            LoadSubjects();
            CmbSittingSubj1.SelectedIndex = 0;
            CmbSittingSubj2.SelectedIndex = 0;
            CmbSittingSubj1.Enabled = true;
            CmbSittingSubj2.Enabled = true;

           
        }
    }

    protected void grdvwEducation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = grdvwEducation.Rows[index];

        switch (e.CommandName.ToLower())
        {
            case "edit":
                LoadQualificationToText(row.Cells[2].Text);
                cmbExam.Enabled = true;
                cmbSeating.Enabled = true;
                trEntrySubjects.Disabled = false;
                btnAddnew.Enabled = btnAddContinue.Enabled = btnAddContinueLater.Enabled = true;
                break;
            case "delete":
                new ApplicantEntryQualificationBusiness().DeleteExams(row.Cells[2].Text, lblmatno.Text);
                LoadQualification(lblmatno.Text);
                break;
            default:
                break;
        }

    }

    private int subjCounter()
    {
        #region Counter
        int count = 0;
        DropDownList ddlSub;
        for (int i = 1; i <= 10; ++i)
        {
            ddlSub = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());
            if (ddlSub.SelectedIndex != 0)
            {
                count++;
            }
        }
        #endregion
        return count;
    }

    private void ResetExamText()
    {
        #region Counter
        int count = 0;
        DropDownList ddlSub, ddlGra;
        for (int i = 1; i <= 10; ++i)
        {
            ddlSub = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());
            ddlGra = (DropDownList)PanelEntryQual.FindControl("CmbSittingGrade" + (i).ToString().Trim());
            ddlSub.SelectedIndex = 0;
            ddlGra.SelectedIndex = 0;
        }
        #endregion
    }

    protected void grdvwEducation_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    private void FilterEnglishAndMat()
    {
        for (int i = 3; i <= 10; i++)
        {
            ddlSubject = (DropDownList)PanelEntryQual.FindControl("CmbSittingSubj" + (i).ToString().Trim());

            ddlSubject.Items.RemoveAt(1);
            ddlSubject.Items.RemoveAt(1);
        }
    }

    #endregion

    #region Save Post Education Info (for DE only)

    private void SavePostEducationInfo(int actio)
    {
        lblEntryError1.Visible = true;
        string retMessage = ""; Control kontrol = new Control();
        if (ValidatePostEducationlnfo(ref retMessage, ref kontrol) == false)
        {
            lbltitleError.Text = retMessage;
            lbltitleError.Visible = true;
            kontrol.Focus();
            new Utility().MessageBox(retMessage, this.Page);
            return;
        }


        Applicants ap = new Applicants();
        ApplicantPostQualification eq = new ApplicantPostQualification();
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }

        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];

        eq.RegNo = appSo.FormNumber;
        eq.CourseGrade = txtPostGrade.Text;
        eq.CourseName = txtPostCourse.Text;
        eq.Institution = txtPostschool.Text;
        eq.PostMatric = txtPostMatNo.Text;
        eq.QualYear = txtPostYear.Text;
        eq.QualyType = ddlPostQual.SelectedItem.Text;
        ApplicantsBusiness.UpdatePostEducationRecord(eq);
        if (actio == 1)
            SwitchView();
        else if (actio == 2)
            Response.Redirect("ApplicantControlCenter.aspx");
    }

    private bool ValidatePostEducationlnfo(ref string ReturnMessage, ref Control focusControl)
    {
        bool bolFalseVal = false, bolTruVal = true;

        if (ddlPostQual.SelectedIndex < 1)
        {
            ReturnMessage = "You must select a Post Education qualification.";
            focusControl = ddlPostQual;
            return bolFalseVal;
        }


        //firstly validate post qualification entry for new students

        if (string.IsNullOrEmpty(txtPostschool.Text))
        {
            ReturnMessage = "Enter Your previous Institution name";
            focusControl = txtPostschool;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(txtPostYear.Text))
        {
            ReturnMessage = "Enter the effective date of the qualification (MM/YYYY)";
            focusControl = txtPostYear;
            return bolFalseVal;
        }
        if (string.IsNullOrEmpty(txtPostMatNo.Text))
        {
            ReturnMessage = "Enter your Previous Student Number (Matric, Reg No etc.)";
            focusControl = txtPostMatNo;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(txtPostCourse.Text))
        {
            ReturnMessage = "Enter your qualification course or discipline";
            focusControl = txtPostCourse;
            return bolFalseVal;
        }

        if (string.IsNullOrEmpty(txtPostGrade.Text))
        {
            ReturnMessage = "Enter your qualification grade";
            focusControl = txtPostGrade;
            return bolFalseVal;
        }


        return bolTruVal;
    }

    protected void btnAddPostContinue_Click(object sender, EventArgs e)
    {
        SavePostEducationInfo(1);
    }

    protected void btnAddPostContinueLater_Click(object sender, EventArgs e)
    {
        SavePostEducationInfo(2);
    }

    #endregion

    #region Complete Attestation Info

    protected void chkAgree_CheckedChanged(object sender, EventArgs e)
    {
        btnRegister.Enabled = chkAgree.Checked;
    }

    private bool ValidateAttestationlnfo(ref string ReturnMessage, ref Control focusControl)
    {
        bool bolFalseVal = false, bolTruVal = true;

        if (string.IsNullOrEmpty(cmbRefAll.SelectedValue))
        {
            ReturnMessage = "Please let us know how you hear about the program";
            focusControl = cmbRefAll;
            new Utility().MessageBox(ReturnMessage, this.Page);
            return bolFalseVal;
        }

        if (cmbRefAll.SelectedItem.Text.ToLower() == "partners" && cmbRefPartners.SelectedIndex < 1)
        {
            ReturnMessage = "Select the specific partner who introduce you to the program";
            focusControl = cmbRefPartners;
            new Utility().MessageBox(ReturnMessage, this.Page);
            return bolFalseVal;
        }

        if (chkAgree.Checked == false)
        {
            ReturnMessage = "You Have To Agree With The Declarations";
            focusControl = chkAgree;
            new Utility().MessageBox(ReturnMessage, this.Page);
            return bolFalseVal;
        }

        return bolTruVal;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        lbltitleError.Visible = false;
        bool isSubmitted = false;
        lblEntryError1.Visible = true;
        string retMessage = ""; Control kontrol = new Control();
        if (ValidateAttestationlnfo(ref retMessage, ref kontrol) == false)
        {
            lbltitleError.Text = retMessage;
            lbltitleError.Visible = true;
            kontrol.Focus();
            return;
        }
        Applicants ap = new Applicants();
        ApplicantEntryQualification eq = new ApplicantEntryQualification();
        if (Session["ApplicantSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
            return;
        }

        ap.RegNo = lblmatno.Text;
        ap.FormNumber = lblmatno.Text;
        ap.Referral = cmbRefAll.SelectedItem.Text + ((cmbRefAll.SelectedItem.Text.ToLower() == "partners") ?  "-" + cmbRefPartners.SelectedItem.Text : "");
        appSo = (ApplicantSignOn)Session["ApplicantSignOn"];

        isSubmitted = ApplicantsBusiness.UpdateAttestationRecord(ap);

        if (isSubmitted == true)
            Response.Redirect("ApplicationFormSubmit.aspx");
        else
            lbltitleError.Text = "Your Application is NOT successful! Kindly logout and try again.";


    }

    protected void cmbRefAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        trPartnersDetail.Visible = false;
        if (cmbRefAll.SelectedItem.Text.ToLower() == "partners")
        {
            LoadReferral();
            trForPartners.Visible = true;
        }
        else
        {
            trForPartners.Visible = false;
        }
    }

    protected void cmbRefPartners_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbRefPartners.SelectedIndex > 0)
        {
            trPartnersDetail.Visible = true;
            lblPartnerDetails.Text = cmbRefPartners.SelectedItem.Value;
        }
        else
        {
            trPartnersDetail.Visible = false;
        }
    }

    #endregion

}
