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
using System.Drawing;

public partial class ChangeOfCourse : System.Web.UI.Page
{
    ApplicantsBusiness ab = new ApplicantsBusiness();
    AdmissionLetter al = new AdmissionLetter();
    DataSet ds, dsc = new DataSet();
    DataTable dt, dtc = new DataTable();
    string FormNumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["CHANGEOFCOURSE"] == null)
            {
                new Utility().MessageBox("Please Check your admission status first", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                return;
            }
            else
            {
                FormNumber = Session["CHANGEOFCOURSE"].ToString();
                ds = ab.GetAdmittedStudentsByFormNo(FormNumber);
                //Get applicant details
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ApplicationNo"]!=null)
                {
                    dt = ds.Tables[0];
                    lblFormNumber.Text = dt.Rows[0]["ApplicationNo"].ToString();
                    lblFullname.Text = dt.Rows[0]["Surname"].ToString();
                    lblProgramOfStudy.Text = dt.Rows[0]["CourseOfStudy"].ToString();
                    bool IsAdmitted = dt.Rows[0]["IsAdmitted"] == "0" ? false : true;
                    string Remarks = dt.Rows[0]["Remarks"].ToString().Trim();
                    string AdmittedStatus = dt.Rows[0]["AdmittedStatus"].ToString().Trim();
                    string[] courseSuggested = Remarks.Split(',');
                    string SearchIn = "";
                    if (courseSuggested.Length > 0)
                    {
                        for (int i = 0; i < courseSuggested.Length - 1; i++)
                        {
                            SearchIn = "'" + courseSuggested[i] + "',";
                        }
                        SearchIn = SearchIn.TrimEnd(',');
                        dsc = ApplicantsBusiness.getAddmissionChangeOfCourses(SearchIn);
                        if (dsc != null && dsc.Tables[0].Rows.Count >0)
                        {
                            CmbCourse.Items.Clear();
                            dtc = dsc.Tables[0];
                            CmbCourse.DataSource = dtc;
                            CmbCourse.DataTextField = "Course";
                            CmbCourse.DataValueField = "Srn";
                            CmbCourse.DataBind();
                            CmbCourse.Items.Insert(0, new ListItem("Please Select Programme", "0"));
                        }
                        else
                        {
                            //Courses Suggested do not match
                            new Utility().MessageBox("Programme Of Study(ies) suggested did not matched. Please consult with your admission offical to re-upload with the correct Programme Of Study", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                            return;
                        }
                    }
                    else
                    {
                        //No Course(s) Suggested
                        new Utility().MessageBox("Please Check your admission status first", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                        return;
                    }
                }
            }
        }
    }
    protected void BtnViewAdmitted_Click(object sender, EventArgs e)
    {
        //UpdateChangeOfAddmissionCourse
        if (CmbCourse.SelectedIndex > 0)
        {
            if (chkAgree.Checked == false)
            {
                new Utility().MessageBox("You need to agree with operation to continue.", this.Page);
                return;
            }
            if (SignOnBusiness.UpdateChangeOfAddmissionCourse(lblFormNumber.Text, Convert.ToInt32(CmbCourse.SelectedValue)) == true)
            {
                Session["RegNo"] = lblFormNumber.Text.Trim();
                new Utility().MessageBox("You have successfully changed to the suggested programme of study. You can now proceed to print your notification of admission.", ResolveUrl("~/RptPrintNotification.aspx"), this.Page);
                //new Utility().MessageBox("Your application to change to the suggested programme of study was successfull and is been processed. You can check your admission status at a later date.", ResolveUrl("~/ViewAdmittedStudentsSpecial.aspx"), this.Page);
                return;
            }
        }
        else
        {
            if (chkAgree.Checked == false)
            {
                new Utility().MessageBox("Select Programme of study you wish to change to.", this.Page);
                return;
            }

        }
    }

}
