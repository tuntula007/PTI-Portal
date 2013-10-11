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

public partial class PersonalBiodata : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string MatricNumber = "";
    string Semester = "First";
    StudentSignOn So = new StudentSignOn();
    Students students;
    protected void Page_Load(object sender, EventArgs e)
    {
        So = (StudentSignOn)Session["StudentSignOn"];
        bool fromcenter = false;
        if (Session["isFromControlCenter"] != null)
            fromcenter = (bool)Session["isFromControlCenter"];
        if (fromcenter == true && So != null)
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

            try
            {
                Sessions.Text = new SignOnBusiness().getCurrentSession() + " Session";
                //get students detail
                StudentsBusiness sb = new StudentsBusiness();

                students = sb.GetStudentsByMatNo(MatricNumber);

                if (students.AdmissionStatus.ToLower() != "admitted")
                    Response.Redirect("StudentControlCenter.aspx");
                Session["Students"] = students;
                Session["Level"] = students.AcademicLevel;
                Session["DepartmentId"] = students.DepartmentID;
                Session["CourseofStudyId"] = students.CourseOfStudyID;
                Session["Programme"] = students.Programme;
                Session["ModeOfStudy"] = students.ModeOfStudy;

                //detect if the student has registered for first semester, then disallow.

                //if (DeptCoursesBusiness.getRegistrationStatus(MatricNumber, Semester) == true && students.ModeOfStudy.ToLower().Equals("dlc mode"))
                //{
                //    MessageBox("Since you have registered before, registration for 2009/2010 First Semester is CLOSED for you as DLC MODE Student. For more information, contact your ICT Director", MatricNumber);
                //    return;
                //}

                LoadState();
                ArrayList lgalist = new ArrayList();
                cmbLGA.DataSource = lgalist;
                cmbLGA.DataBind();


                //load student biodata
                lblsch.Text = students.Faculty.ToUpper();
                lblcourse.Text = students.CourseOfStudy.ToUpper();
                lblmatno.Text = MatricNumber.ToUpper();
                lblyear.Text = students.AcademicLevel.ToUpper();
                txtSurname.Text = students.Surname.ToUpper();
                txtOthernames.Text = students.OtherNames.ToUpper();
                txtMaiden.Text = students.MaidenName.ToUpper();
                txtAddress.Text = students.HomeAddress.ToUpper();
                txtEmail.Text = students.Email;
                txtPhone.Text = students.PhoneNumber;
                txtHall.Text = students.ResidentialAddress.ToUpper();
                txtRoom.Text = students.RoomNo.ToUpper();

                TxtNextOfKinAddres.Text = students.SponsorAddress;
                TxtNextKinName.Text = students.SponsorName;
                TxtNextOfKinPhone.Text = students.SponsorPhone;
                TxtNextOfKinRela.Text = students.SponsorRelationship;
                cmbReligion.Text = students.Religion;
                TxtNextmail.Text = students.SponsorEmail;

                for (int i = 1940; i < (DateTime.Now.Year - 10); i++)
                    cmbYear.Items.Add(i.ToString());

                ListItem li;
                //load title
                li = cmbTitle.Items.FindByText(students.Title);
                if (li != null)
                {
                    cmbTitle.ClearSelection();
                    li.Selected = true;
                }

                //load marital status
                li = cmbMarital.Items.FindByText(students.MaritalStatus);
                if (li != null)
                {
                    cmbMarital.ClearSelection();
                    li.Selected = true;
                }
                //load sex
                string gender = (students.Sex.Trim().Substring(0, 1).ToLower() == "m") ? "Male" : (students.Sex.Trim().Substring(0, 1).ToLower() == "f") ? "Female" : students.Sex;
                li = cmbSex.Items.FindByText(gender);
                if (li != null)
                {
                    cmbSex.ClearSelection();
                    li.Selected = true;
                }

                //load nationality
                students.Nationality = (students.Nationality.ToLower().StartsWith("nigeria")) ? "Nigerian" : students.Nationality;
                li = cmbNation.Items.FindByText(students.Nationality);
                if (li != null)
                {
                    cmbNation.ClearSelection();
                    li.Selected = true;
                }


                if (students.State.ToUpper() == "OYO")
                {
                    //get student lga in ONDO
                    StateLGADefault("OYO", students.LocalGovernmentArea.ToUpper());
                }
                else
                {
                    StateLGADefault(students.State.ToUpper(), students.LocalGovernmentArea.ToUpper());
                }

                //get exam and lecture center
                //load exam center for o-level student only

                if (students.EntryMode.ToLower() == "o level")
                {
                    li = rdbExamCenter.Items.FindByText(students.ExaminationCenter);
                    if (li != null)
                    {
                        rdbExamCenter.ClearSelection();
                        li.Selected = true;
                        rdbExamCenter.Enabled = false;
                    }
                }
                else trExamCenter.Visible = false;

                li = rdbTeachingCenter.Items.FindByText(students.TeachingCenter);
                if (li != null)
                {
                    rdbTeachingCenter.ClearSelection();
                    li.Selected = true;
                    rdbTeachingCenter.Enabled = false;
                }

                //get date of birth
                if (!string.IsNullOrEmpty(students.DateOfBirth))
                {

                    string[] dob = students.DateOfBirth.Trim().Split(new char[] { '/', '-' });
                    if (dob.Length == 3)
                    {
                        li = cmbYear.Items.FindByText(dob[0].Trim());
                        if (li != null)
                        {
                            cmbYear.ClearSelection();
                            li.Selected = true;
                        }

                        li = cmbMonth.Items.FindByValue(dob[1].Trim());
                        if (li != null)
                        {
                            cmbMonth.ClearSelection();
                            li.Selected = true;
                        }

                        li = cmbDay.Items.FindByText(dob[2].Trim());
                        if (li != null)
                        {
                            cmbDay.ClearSelection();
                            li.Selected = true;
                        }
                        cmbDay.Enabled = false;
                        cmbMonth.Enabled = false; cmbYear.Enabled = false;
                    }
                    else
                    {
                        cmbDay.Enabled = true;
                        cmbMonth.Enabled = true; cmbYear.Enabled = true;
                    }
                }
                else
                {
                    cmbDay.Enabled = true;
                    cmbMonth.Enabled = true; cmbYear.Enabled = true;
                }
                logger.Info(students.MatricNumber + " - Personal data loaded successfully!");
                //show carry over panel

            }
            catch (Exception exx)
            {
                logger.Error(exx.Message.ToString());
            }

        } //end if postback



    }
    private void StateLGADefault(string state, string lga)
    {
        ListItem li;

        li = cmbState.Items.FindByText(state);
        if (li != null)
        {
            cmbState.ClearSelection();
            li.Selected = true;
        }

        LoadLGA(state);
        li = cmbLGA.Items.FindByText(lga);
        if (li != null)
        {
            cmbLGA.ClearSelection();
            li.Selected = true;
        }
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
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Students st = new Students();
        if (Session["Students"] == null)
        {
            //Expired Session
            Response.Redirect("StudentLogin.aspx");
            return;
        }
        st = (Students)Session["Students"];
        lbltitleError.Visible = true;
        //fistly validate entry subjects for new students
        try
        {
            ////start validating Biodata
            if (string.IsNullOrEmpty(cmbTitle.SelectedValue))
            {
                lbltitleError.Visible = true;
                lbltitleError.Text = "Select your Title";
                cmbTitle.Focus();
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                return;
            }

            if (cmbYear.SelectedValue == "" && cmbMonth.SelectedValue == "" && cmbDay.SelectedValue == "")
            {
                lbltitleError.Visible = true;
                lbltitleError.Text = "Please enter a valid date of birth!";
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                cmbDay.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbMarital.SelectedValue) == true)
            {
                lbltitleError.Visible = true;
                lbltitleError.Text = "Please enter a Marital Status!";
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                cmbMarital.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cmbLGA.SelectedValue) == true)
            {
                lbltitleError.Visible = true;
                lbltitleError.Text = "Please enter your LGA!";
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                cmbLGA.Focus();
                return;

            }
            if (string.IsNullOrEmpty(txtAddress.Text) == true)
            {
                lbltitleError.Visible = true;
                lbltitleError.Text = "Please enter your Permanent Address!";
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                txtAddress.Focus();
                return;

            }
            if (cmbSex.SelectedValue.ToLower() == "male")
            {
                //Compare Male Gender with title
                if (cmbTitle.SelectedValue.ToLower() == "miss" || cmbTitle.SelectedValue.ToLower() == "mrs."
                    || cmbTitle.SelectedValue.ToLower() == "rev. mrs.")
                {
                    lbltitleError.Text = "Invalid Title/Gender combination. You can't be MALE and be a " + cmbTitle.SelectedValue + " at the same time.";
                    new Utility().MessageBox(lbltitleError.Text, this.Page);
                    lbltitleError.Focus();
                    return;
                }
            }
            else
            {
                //Compare Female gender with title
                if (cmbTitle.SelectedValue.ToLower() == "rev." || cmbTitle.SelectedValue.ToLower() == "mr.")
                {
                    lbltitleError.Text = "Invalid Title/Gender combination. You can't be FEMALE and be a " + cmbTitle.SelectedValue + " at the same time.";
                    new Utility().MessageBox(lbltitleError.Text, this.Page);
                    lbltitleError.Focus();
                    return;
                }

            }
           

            // THIS CODE IS NOT NEEDFUL FOR PTI ---- just not to let original implementation code break :
            if (st.EntryMode.ToLower() == "o level")
            {
                if (rdbExamCenter.SelectedIndex <0)
                {
                    lbltitleError.Text = "Please choose your are a Delta indegene";
                    new Utility().MessageBox(lbltitleError.Text, this.Page);
                    lbltitleError.Focus();
                    return;
                }
            }
            // END : THIS CODE IS NOT NEEDFUL FOR PTI ---- just not to let original implementation code break :


            // THIS CODE IS NOT NEEDFUL FOR PTI ---- just not to let original implementation code break :
            if (rdbTeachingCenter.SelectedIndex <0)
            {
                lbltitleError.Text = "Please Choose whether you are a non-Nigerain";
                new Utility().MessageBox(lbltitleError.Text, this.Page);
                lbltitleError.Focus();
                return;
            }
          // END : THIS CODE IS NOT NEEDFUL FOR PTI ---- just not to let original implementation code break :

            new Utility().ConfirmBox("Click Yes to continue if you are sure of the information, you wish to update", this.Page);

            lbltitleError.Visible = false;
            logger.Info(MatricNumber + " - Student entries were OK!");
            st.MatricNumber = lblmatno.Text;
            st.Title = cmbTitle.SelectedValue;
            st.Surname = txtSurname.Text;
            st.OtherNames = txtOthernames.Text;
            st.MaidenName = txtMaiden.Text;
            st.MaritalStatus = cmbMarital.SelectedValue;
            st.Sex = (cmbSex.SelectedValue.Trim().ToLower() == "male") ? "M" : (cmbSex.SelectedValue.Trim().ToLower() == "female") ? "F" : "";
            st.Nationality = cmbNation.SelectedValue;
            st.State = cmbState.SelectedValue;
            st.LocalGovernmentArea = cmbLGA.SelectedValue;
            st.HomeAddress = txtAddress.Text;
            st.Email = txtEmail.Text;
            st.PhoneNumber = txtPhone.Text;
            st.ResidentialAddress = txtHall.Text;
            st.RoomNo = txtRoom.Text;
            st.SponsorAddress = TxtNextOfKinAddres.Text.Trim().Replace("'","''");
            st.SponsorName = TxtNextKinName.Text.Trim().Replace("'", "''");
            st.SponsorPhone = TxtNextOfKinPhone.Text.Trim().Replace("'", "''");
            st.SponsorRelationship = TxtNextOfKinRela.Text.Trim().Replace("'", "''");
            st.Religion = cmbReligion.SelectedValue;
            st.ExaminationCenter = (st.EntryMode.ToLower() == "o level") ? rdbExamCenter.SelectedItem.Text : "";
            st.TeachingCenter = rdbTeachingCenter.SelectedItem.Text;
            st.SponsorEmail = TxtNextmail.Text;

            if (cmbYear.SelectedValue != "" && cmbMonth.SelectedValue != "" && cmbDay.SelectedValue != "")
                st.DateOfBirth = cmbYear.SelectedValue + "/" + cmbMonth.SelectedValue + "/" + cmbDay.SelectedValue;
            else
                st.DateOfBirth = "";
            StudentsBusiness.UpdateRecord(st); //update biodata
            logger.Info(lblmatno.Text + " - Record biodata update was successful!");

            if (Session["Students"] != null)
            {
                st = (Students)Session["Students"];
            }
            else
            {
                StudentsBusiness sb = new StudentsBusiness();
                st = sb.GetStudentsByMatNo(lblmatno.Text);
            }
            Session["Students"] = st;
            lbltitleError.Visible = false;
            new Utility().MessageBox("Biodata updated successfull", ResolveUrl("~/StudentControlCenter.aspx?matricnumber=" + st.MatricNumber), this.Page);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            new Utility().MessageBox("There was a technical problem process your request. Please try again", ResolveUrl("~/StudentControlCenter.aspx?matricnumber=" + st.MatricNumber), this.Page);
        }

    }
    protected void cmbState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbState.SelectedIndex > 0)
        {
            LoadLGA(cmbState.Text);
        }
    }
}
