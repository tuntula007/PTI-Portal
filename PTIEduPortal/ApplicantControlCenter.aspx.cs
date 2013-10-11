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

public partial class ApplicantControlCenter : System.Web.UI.Page
{
    bool alreadyPaidAdmissionLetterfees = false;
    ApplicantSignOnBusiness Aob = new ApplicantSignOnBusiness();
    ApplicantSignOn Ao = new ApplicantSignOn();
    Applicants App = new Applicants();
    
    string FormNumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Ao = (ApplicantSignOn)Session["ApplicantSignOn"];
        if (Ao == null)
        {
            //session expired, complain
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
        }
        else
        {
            FormNumber = Ao.FormNumber;
            ApplicantsBusiness sb = new ApplicantsBusiness();
            App = sb.GetApplicantsByFormNo(FormNumber);
            object frm1 = App.FormNumber;
            if (frm1==null )
            {
                App=null;/////refresh
                App = sb.GetApplicantsFromApplicantsHistryByFormNo(FormNumber);
            }
            /*if (App.PresentSession.ToLower() != ApplicantsBusiness.ActiveSession().ToLower())
            {
                //Expired Session
                Session.Contents.Clear();
                new Utility().MessageBox("Dear " + App.RegNo + ", Your application profile for the " +
                    App.PresentSession + " session has expired. You do not have access to the applicant control panel anymore", "ApplicantLogin.aspx", this.Page);
                return;
            }*/
            Session["Applicants"] = App;
            lblWelcom.Text = "Welcome " + App.OtherNames + "!";
            lblMatno.Text = App.FormNumber.ToUpper();
            lblSurname.Text = App.Surname.ToUpper();
            lblOthernames.Text = App.OtherNames.ToUpper();
            lblprogcat.Text = App.Programme.ToUpper();
            lblModeOfStudy.Text = App.ModeOfStudy.ToUpper();
            lblpresentsession.Text = App.PresentSession.ToUpper();
            if (App.AdmissionStatus.ToLower() == "admitted") AdmissionLabel.Text = "Print Admission Letter";
            else AdmissionLabel.Text = "Check Admission Status";
            lblEntryMode.Text = (string.IsNullOrEmpty(App.EntryMode)) ? "NOT SELECTED" : App.EntryMode.ToUpper();
            if (string.IsNullOrEmpty(App.LocalPassportFile) == false)
            {
                imgPix.ImageUrl = App.LocalPassportFile;
            }
            else
            {
                imgPix.ImageUrl = "~/picx/blank.png";
                new Utility().MessageBox("It is noticed that you dont have any passport yet. Please upload your passport", this.Page);
                return;
            }
            if (App.printStatus == 1)
                PassportUpload.Visible = false;
            else PassportUpload.Visible = true;
        }
    }


    protected void LnkShoww_Click(object sender, EventArgs e)
    {
        //LinkButton Btt = (LinkButton)GridView2.Rows[GridView2.SelectedIndex].FindControl("LnkShoww");
        //Session["RegNo"] = "PTI/00001/13";          // Btt.Text;
        //Response.Redirect("RptPrintAdmissionLetter.aspx"); AdmissionLetterParam

        alreadyPaidAdmissionLetterfees = CybSoft.EduPortal.Business.SignOnBusiness.VerifyAdmissionLetterFeeIsPaid(lblMatno.Text.Trim());
        if (alreadyPaidAdmissionLetterfees)
        {
            Session["RegNo"] = lblMatno.Text.Trim();

            Response.Redirect("RptPrintAdmissionLetter.aspx", true);

        }
        if (!(alreadyPaidAdmissionLetterfees))
        {
            Response.Redirect("AdmissionLetterParam.aspx", true);
        }
    }
    protected void LnkShowFees_Click(object sender, EventArgs e)
    {
       

            Response.Redirect("FEES2012Freshstudents.pdf", false);
 
    }
    protected void btnUploadPassport_Click(object sender, EventArgs e)
    {
        if (Session["ApplicantSignOn"] == null)
        {
            //session expired, complain
            Session.Contents.Clear();
            Response.Redirect("ApplicantLogin.aspx");
        }

        panelError.Visible = true;
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

        }
        else
        {
            lblerror.Text = "Only .jpg, png and .gif files formats are allowed";
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
            Nmatno = Omatno.Replace("/", "").Replace(":","").Replace(" ","");
            filename = "~/picx/" + Nmatno + ext;
        }
        else
        {
            //complain 
            lblerror.Text = "Disallowed! Session has expired, logout and logon again.";
            new Utility().MessageBox(lblerror.Text, this.Page);
            return;
        }
        try
        {
            FileUploadPassport.SaveAs(Server.MapPath(filename));
            // save to database
            using (BinaryReader reader = new BinaryReader
                        (FileUploadPassport.PostedFile.InputStream))
            {
                byte[] image = reader.ReadBytes
                        (FileUploadPassport.PostedFile.ContentLength);
                PictureManager.SaveImage(Ao.FormNumber,image);
            }
        }
        catch (Exception exx)
        {
            lblerror.Text = "Passport could not be uploaded! Try again.";
            new Utility().MessageBox(lblerror.Text, this.Page);
            //logger.Info(MatricNumber + " - " + lblerror.Text);
            return;
        }
        panelError.Visible = false;
        // save successful, now update the personal table with pics
        httpref = httpref + "/" + Nmatno + ext;
        ApplicantsBusiness.UpdatePassportRecord(httpref, lblMatno.Text, filename);

        new Utility().MessageBox("Your passport was uploaded successfully! Please note that passport change will be locked after you print your form", this.Page);
        //refresh the image passport

        imgPix.ImageUrl = filename;


    }

}




