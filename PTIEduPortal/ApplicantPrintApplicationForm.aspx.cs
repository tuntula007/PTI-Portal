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
using System.Data.SqlClient;
using CybSoft.EduPortal.Data;

public partial class ApplicantPrintApplicationForm : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string FormNumber = "";

        ApplicantSignOn So = new ApplicantSignOn();
        Applicants applicants;
        ApplicantEntryQualification Enq;


        //get matric no and semester

        if (Session["ApplicantSignOn"] == null)
        {
            //session expired, complain and go back to session expired page
            // Response.Write("session expired");
            // return;
            Response.Redirect("ApplicantLogin.aspx");
        }
        else
        {
            So = (ApplicantSignOn)Session["ApplicantSignOn"];
            FormNumber = So.FormNumber;


        }

        Session["FormNumber"] = FormNumber;


        if (Page.IsPostBack == false)
        {
            //get enrollment detail

            ApplicantsBusiness sb = new ApplicantsBusiness();

            applicants = sb.GetApplicantsByFormNo(FormNumber);

            if (string.IsNullOrEmpty(applicants.LocalPassportFile) == true)
            {
                //no passport
                lblEMes.Text = "No Passport Uploaded!!<br /> Go back to the <a href='ApplicantControlCenter.aspx'>Control Centre</a> and upload your passport";
                pnlForm.Visible = false;
                pnlMsg.Visible = true;
                return;
            }
            if (applicants.SubmitStatus == 0)
            {
                lblEMes.Text = "Application Form not submitted completely!!<br /> Go back to the <a href='ApplicationForm.aspx'>Application Form</a> and complete your information entry";
                pnlForm.Visible = false;
                pnlMsg.Visible = true;
                return;
            }
            if (applicants.AdmissionStatus.ToLower() == "admitted")
            {
                lblEMes.Text = "You Cannot Print Application Form again!!<br /> Go to <a href='ApplicantControlCenter.aspx'>Applicant Control Center</a> to Check Your Admission Status";
                pnlForm.Visible = false;
                pnlMsg.Visible = true;
                return;
            }
            Session["FormNumber"] = FormNumber;
            Session["applicants"] = applicants;
            Session["Level"] = applicants.AcademicLevel;
            Session["DepartmentId"] = applicants.DepartmentID;
            //Session["CourseofStudyId"] = applicants.CourseOfStudyID;
            Session["Programme"] = applicants.Programme;
            Session["ModeOfStudy"] = applicants.ModeOfStudy;
            lblProgramme.Text = applicants.Programme.ToUpper() + " PROGRAMMES"; //" ("+ applicants.ModeOfStudy.ToUpper () + ")";
            //lblcourse.Text = applicants.FirstDepartment;
            lblTitle.Text = applicants.Title.ToUpper();  //FormNumber;
            lblReligious.Text = applicants.Religion.ToString();


            //get personal detail
            surname.Text = applicants.Surname.ToUpper();
            othernames.Text = applicants.OtherNames.ToUpper();
            dob.Text = applicants.DateOfBirth;
            lblMaritalStatus.Text = applicants.MaritalStatus.ToUpper();
            sex.Text = applicants.Sex.ToUpper();
            lblNationality.Text = applicants.Nationality.ToUpper(); //+ ((applicants.Nationality.ToLower() == "non-nigerian") ? "-" + applicants.Country.ToUpper() : "")
            lblState.Text = (applicants.Nationality.ToLower() == "non-nigerian") ? "N/A" : applicants.State.ToUpper();
            lblLGA.Text = (applicants.Nationality.ToLower() == "non-nigerian") ? "N/A" : applicants.LocalGovernmentArea.ToUpper();
            lblCountry.Text = applicants.Country.ToUpper();
            homeadd.Text = applicants.HomeAddress;
            email.Text = applicants.Email;
            phone.Text = applicants.PhoneNumber;
            ApplicationForm.Text = " " + applicants.RegNo.ToUpper();
            ApplicationForm.Font.Underline = false;
            //lblBG.Text = applicants.BloodGroup.ToUpper();

            //corrAdd.Text = applicants.PostalAddress;


            lblNextOfKinName.Text = applicants.SponsorName.ToUpper();
            lblNextOfKinAddress.Text = applicants.SponsorAddress.ToUpper();
            lblNextOfKinPhone.Text = applicants.SponsorPhone.ToUpper();
            lblNextOfKinEmail.Text = applicants.SponsorEmail;
            lblNextOfKinRelationship.Text = applicants.SponsorRelationship.ToUpper();
            //lblSponsor.Text = applicants.Sponsor.ToUpper();  

            lblRef.Text = applicants.Referral.ToUpper();
            // wrk experience
            //lblPresEmp.Text = applicants.PresentEmployment;
            //lblPrePos.Text = applicants.PositionHeld;
            //lblPrevEmp.Text = applicants.PreviousEmployment;

            // edu qual
            //lblPreQual.Text = applicants.PresentHighestQualification;
            //lblSchAtt.Text = applicants.SchoolAttended;
            trPreviousInfo.Visible = false;
            if (applicants.Repeating != null && applicants.Repeating.ToUpper() == "YES")
            {
                lblPreviousMatric.Text = applicants.PreviousRegNo.ToUpper();
                lblPreviousCourse.Text = applicants.PreviousCourseAttended.ToUpper();
                lblPreviousStartDate.Text = applicants.PreviousAttendedFrom.ToUpper();
                lblPreviousEndDate.Text = applicants.PreviousAttendedTo.ToUpper();
                trPreviousInfo.Visible = true;
            }

            lblDLCFirstChoice.Text = applicants.CourseofStudy1;
            lblDLCSecondChoice.Text = applicants.CourseofStudy2;
            lblDLCThirdChoice.Text = applicants.CourseofStudy3;
            //LectureTown.Text = applicants.Center.ToUpper();
            ExamTown.Text = applicants.ExaminationCenter.ToUpper();
            this.prvSch.Text  = applicants.MyInstitution.ToUpper();
            this.prevQual.Text = applicants.MyPostProgramme.ToUpper();
            this.prvmnthyr.Text = applicants.MyQualYear.ToUpper();
            this.prvExmatno.Text = applicants.MyPostMatric.ToUpper();
            this.pvrDec.Text = applicants.MyCourseName.ToUpper();
            this.prvGrade.Text = applicants.MyCourseGrade.ToUpper(); 
            

            //picture
            if (string.IsNullOrEmpty(applicants.LocalPassportFile) == false)
            {
                imgPix.ImageUrl = applicants.LocalPassportFile;
            }
            else
            {
                imgPix.ImageUrl = "~/picx/blank.png";
            }

            //  shown entry qualification panel
            PanelEntryQual.Visible = true;

            //get entry qualifications
            Enq = ApplicantEntryQualificationBusiness.getRecord(FormNumber, "1");
            exam1.Text = Enq.ExamName;
            center1.Text = Enq.ExamRegNo;
            date1.Text = Enq.ExamDate;
            S13.Text = Enq.SubjectName3;
            S14.Text = Enq.SubjectName4;
            S15.Text = Enq.SubjectName5;
            S16.Text = Enq.SubjectName6;
            S17.Text = Enq.SubjectName7;
            S18.Text = Enq.SubjectName8;
            S19.Text = Enq.SubjectName9;
            S10.Text = Enq.SubjectName10;

            G11.Text = Enq.SubjectGrade2;
            G12.Text = Enq.SubjectGrade1;
            G13.Text = Enq.SubjectGrade3;
            G14.Text = Enq.SubjectGrade4;
            G15.Text = Enq.SubjectGrade5;
            G16.Text = Enq.SubjectGrade6;
            G17.Text = Enq.SubjectGrade7;
            G18.Text = Enq.SubjectGrade8;
            G19.Text = Enq.SubjectGrade9;
            G10.Text = Enq.SubjectGrade10;

            Enq = new ApplicantEntryQualification();
            Enq = ApplicantEntryQualificationBusiness.getRecord(FormNumber, "2");
            if (Enq == null || Enq.RegNo == null || Enq.RegNo == "")
            {
                trSecondSeating.Visible = false;
            }
            else
            {
                trSecondSeating.Visible = true;
                exam2.Text = Enq.ExamName;
                center2.Text = Enq.ExamRegNo;
                date2.Text = Enq.ExamDate;
                S21.Text = Enq.SubjectName1;
                S22.Text = Enq.SubjectName2;
                S23.Text = Enq.SubjectName3;
                S24.Text = Enq.SubjectName4;
                S25.Text = Enq.SubjectName5;
                S26.Text = Enq.SubjectName6;
                S27.Text = Enq.SubjectName7;
                S28.Text = Enq.SubjectName8;
                S29.Text = Enq.SubjectName9;
                S20.Text = Enq.SubjectName10;

                G21.Text = Enq.SubjectGrade1;
                G22.Text = Enq.SubjectGrade2;
                G23.Text = Enq.SubjectGrade3;
                G24.Text = Enq.SubjectGrade4;
                G25.Text = Enq.SubjectGrade5;
                G26.Text = Enq.SubjectGrade6;
                G27.Text = Enq.SubjectGrade7;
                G28.Text = Enq.SubjectGrade8;
                G29.Text = Enq.SubjectGrade9;
                G20.Text = Enq.SubjectGrade10;
            }
            Unit ht;

            //if (PanelEntryQual.Visible == false)
            //{
            //    PanelCourseSelected.CssClass = "pagebreaker";

            //    ht = Unit.Parse("15");
            //}
            //else
            //{
            //    PanelCourseSelected.CssClass = "nopagebreaker";
            //    ht = Unit.Parse("370");
            //}
            PanelCourseSelected.Visible = (lblProgramme.Text.ToLower() == "degree") ? true : false;
            //PanelEntryQual.Height = ht;
            //pnlSectionC.CssClass = "pagebreaker";
            //ht = Unit.Parse("200");
            // pnlSectionC.Height = ht;

        } //end postback
        if (FormNumber != "")
        {
            new ApplicantsBusiness().UpdatePrintStatus(FormNumber);
        }
    }

    protected void UpdatePrintStatus(object sender, EventArgs e)
    {
        Applicants apps= new Applicants();
        if (Session["applicants"] != null) apps = (Applicants)Session["applicants"];
        else Response.Redirect("ApplicantControlCenter");
        Session["ctrl"] = pnlForm;
        //ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','location=no,status=no,toolbar=no,scrollbars=yes,width=1000,height=680,dependent=yes');</script>");
        Control ctrl = (Control)Session["ctrl"];
        PrintHelper.PrintWebControl(ctrl);
    }


}

