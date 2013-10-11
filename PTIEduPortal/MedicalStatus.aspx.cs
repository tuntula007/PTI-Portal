using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using System.Text;


public partial class MedicalStatus : System.Web.UI.Page
{
    StudentSignOn So = new StudentSignOn();
    Students Stud = new Students();
    // StudentsBusiness Sb = new StudentsBusiness();

    private string strConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
    private string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["StudentSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        So = (StudentSignOn)Session["StudentSignOn"];
        Session["StudentSignOn"] = So;
        LabelAppNo.Text = So.FormNumber;

        StudentsBusiness sb = new StudentsBusiness();
        Stud = sb.GetStudentsByMatNo(So.MatricNumber);
        Session["Students"] = Stud;
        string CurrentSession = getSession();

        if (Page.IsPostBack == false)
        {
            LabelWelcome.Text = "Welcome to health status form";// +So.MatricNumber;
            Med medinfo = new Med();
            medinfo = getinfo(So.MatricNumber);
            Refreshinfo(medinfo);     
           

        }

    }

    private void Refreshinfo(Med medinfo)
    {
        txtContactPerson.Text = medinfo.contactPerson;
        TxtContactAddress.Text = medinfo.contactPersonAddress;
        txtPhoneContact.Text = medinfo.contactPersonPhone;
        DDListHealthStatus.Text = medinfo.healthStatus;
        DDListAdmitted.Text = medinfo.admittedInHospital;
        txtAdmitted.Text = medinfo.admissionReasons;
        DDListMedication.Text = medinfo.medication;
        TxtMedication.Text = medinfo.medicationDrugs;
        DDListDesease1.Text = medinfo.desease1;
        DDListDesease2.Text = medinfo.desease2;
        DDListDesease3.Text = medinfo.desease3;
        DDListDesease4.Text = medinfo.desease4;
        DDListDesease5.Text = medinfo.desease5;
        DDListDesease6.Text = medinfo.desease6;
        DDListDesease7.Text = medinfo.desease7;
        DDListDesease8.Text = medinfo.desease8;
        DDListDesease9.Text = medinfo.desease9;
        DDListDesease10.Text = medinfo.desease10;
        TxtDeseas.Text = medinfo.deseaseDetails;
        TxtOtherDetails.Text = medinfo.otherMedDetails;
        TxtTravelHistory.Text = medinfo.travelHistory;
        DDlistFamilyHealthy.Text = medinfo.healthFamily;
        DDListFamilyDesease1.Text = medinfo.familyDesaes1;
        DDListFamilyDesease2.Text = medinfo.familyDesaes2;
        DDListFamilyDesease3.Text = medinfo.familyDesaes3;
        DDListFamilyDesease4.Text = medinfo.familyDesaes4;
        DDListDrugReact.Text = medinfo.drugReaction;
        TxtDrugReact.Text = medinfo.drugReactionDetails;

        DDListImunization1.Text = medinfo.immunization1;
        DDListImunization2.Text = medinfo.immunization2;
        DDListImunization3.Text = medinfo.immunization3;
        DDListImunization4.Text = medinfo.immunization4;
    }

    private Med getinfo(string MatricNumber)
    {
        Med rt = new Med();

        DataTable NowTable = null;
        DataRow nr = null;
        //string mykey = "Applicants_getApplicantMainByFormNumber_" + FormNumber;
        //DBAccess db = new DBAccess(mykey);

        try
        {

            //db.Parameters.Add(new SqlParameter("@FormNumber", FormNumber));


            //DataSet ds = new DataSet("App");
            //ds = db.ExecuteDataSet("getApplicantsMainByFormNumber");
            //string qry
            string sql = "SELECT * FROM [MedicalEntrance] where [MatricNumber] = '" + MatricNumber + "'";
            DataSet ds = new DataSet();

            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(sql, cnn);
            dat.Fill(ds);

            cnn.Dispose();
            cnn.Close();


            NowTable = ds.Tables[0];
            nr = NowTable.Rows[0];
            //rt.ricNumber = MatNo;
            if (NowTable.Rows.Count > 0)
            {
                rt.matricNumber = MatricNumber;
                rt.applcationNo = nr["applcationNo"].ToString();
                rt.sessionName = nr["sessionName"].ToString();
                rt.contactPerson = nr["contactPerson"].ToString();
                rt.contactPersonAddress = nr["contactPersonAddress"].ToString();
                rt.contactPersonPhone = nr["contactPersonPhone"].ToString();
                rt.healthStatus = nr["healthStatus"].ToString();
                rt.admittedInHospital = nr["admittedInHospital"].ToString();
                rt.admissionReasons = nr["admissionReasons"].ToString();
                rt.medication = nr["medication"].ToString();
                rt.medicationDrugs = nr["medicationDrugs"].ToString();
                rt.desease1 = nr["desease1"].ToString();
                rt.desease2 = nr["desease2"].ToString();
                rt.desease3 = nr["desease3"].ToString();
                rt.desease4 = nr["desease4"].ToString();
                rt.desease5 = nr["desease5"].ToString();
                rt.desease6 = nr["desease6"].ToString();
                rt.desease7 = nr["desease7"].ToString();
                rt.desease8 = nr["desease8"].ToString();
                rt.desease9 = nr["desease9"].ToString();
                rt.desease10 = nr["desease10"].ToString();
                rt.deseaseDetails = nr["deseaseDetails"].ToString();
                rt.otherMedDetails = nr["otherMedDetails"].ToString();
                rt.travelHistory = nr["travelHistory"].ToString();
                rt.healthFamily = nr["healthFamily"].ToString();
                rt.familyDesaes1 = nr["familyDesaes1"].ToString();
                rt.familyDesaes2 = nr["familyDesaes2"].ToString();
                rt.familyDesaes3 = nr["familyDesaes3"].ToString();
                rt.familyDesaes4 = nr["familyDesaes4"].ToString();
                rt.drugReaction = nr["drugReaction"].ToString();
                rt.drugReactionDetails = nr["drugReactionDetails"].ToString();
                rt.immunization1 = nr["immunization1"].ToString();
                rt.immunization2 = nr["immunization2"].ToString();
                rt.immunization3 = nr["immunization3"].ToString();
                rt.immunization4 = nr["immunization4"].ToString();
                rt.printStatus = int.Parse(nr["printStatus"].ToString());
                rt.transDate = nr["transDate"].ToString();

            }


        }
        catch (Exception ex)
        {
            //log
            //throw;
        }
        //db.Dispose();
        return rt;
    }
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);
            cnn.Open();

            SqlDataAdapter dat = new SqlDataAdapter(qry, cnn);
            dat.Fill(ds);

            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception e)
        {
            string ex = e.Message;
        }
        return ds;
    }
    private string getSession()
    {
        string Asession = "";
        DataSet ds = new DataSet();
        string qry = "SELECT [SessionName] FROM [Session] where [ActiveStatus]=1";

        ds = SearchData(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int jj = 0; jj < ds.Tables[0].Rows.Count; jj++)
            {
                Asession = ds.Tables[0].Rows[jj][0].ToString();
            }
        }

        return Asession;
    }
    private void PerformInsert(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
    }

    private void PerformDelete(string Qry)
    {

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;


            cmd = new SqlCommand(Qry, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            //msg = ex.Message + "||" + ex.StackTrace;
            ////LogError(msg, "Payroll", "");
            //showmassage(msg);
        }
    }
    private void showmassage(string message)
    {
        //message = message.Replace("'", " ").Replace("\r\n", "");
        //MasterPage master = (MasterPage)this.Master;
        //master.ClientMessage(this.Page, message);
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }
    protected void BtnBiodataSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtContactPerson.Text.Trim() == "")
            {
                msg = "Enter contact person";
                showmassage(msg);
                return;
            }
            if (TxtContactAddress.Text.Trim() == "")
            {
                msg = "Enter contact persons address";
                showmassage(msg);
                return;
            }
            if (txtPhoneContact.Text.Trim() == "")
            {
                msg = "Enter contact persons phone";
                showmassage(msg);
                return;
            }


            string MatricNumber = "";
            string AcademicSession = "";


            if (Session["StudentSignOn"] == null)
            {
                Session.Contents.Clear();
                Response.Redirect("StudentLogin.aspx");
            }
            
            So = (StudentSignOn)Session["StudentSignOn"];
            MatricNumber = So.MatricNumber;
            AcademicSession = getSession();


            StudentsBusiness sb = new StudentsBusiness();
            Stud = sb.GetStudentsByMatNo(So.MatricNumber);

            string qry = "";
            string AppNo = "";

            qry = "SELECT * FROM [MedicalEntrance] WHERE [MatricNumber]='" + MatricNumber + "'";
            if (!Existed(qry))
            {
                //generate new app no
                AppNo = So.FormNumber;
            }
            else
            {
                AppNo = LabelAppNo.Text;
            }

            string transdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            
            int printstatus = 0;
            if (AppNo != "")
            {
                //delete first
                qry = "Delete FROM [MedicalEntrance] WHERE [MatricNumber]='" + MatricNumber + "'";
                PerformDelete(qry);

                qry = "INSERT INTO [MedicalEntrance]" +
                           "([ApplcationNo]" +
                              ",[MatricNumber]" +
                              ",[SessionName]" +
                              ",[ContactPerson]" +
                              ",[ContactPersonAddress]" +
                              ",[ContactPersonPhone]" +
                              ",[HealthStatus]" +
                              ",[AdmittedInHospital]" +
                              ",[AdmissionReasons]" +
                              ",[Medication]" +
                              ",[MedicationDrugs]" +
                              ",[Desease1]" +
                              ",[Desease2]" +
                              ",[Desease3]" +
                              ",[Desease4]" +
                              ",[Desease5]" +
                              ",[Desease6]" +
                              ",[Desease7]" +
                              ",[Desease8]" +
                              ",[Desease9]" +
                              ",[Desease10]" +
                              ",[DeseaseDetails]" +
                              ",[OtherMedDetails]" +
                              ",[TravelHistory]" +
                              ",[HealthFamily]" +
                              ",[FamilyDesaes1]" +
                              ",[FamilyDesaes2]" +
                              ",[FamilyDesaes3]" +
                              ",[FamilyDesaes4]" +
                              ",[DrugReaction]" +
                              ",[DrugReactionDetails]" +
                              ",[Immunization1]" +
                              ",[Immunization2]" +
                              ",[Immunization3]" +
                              ",[Immunization4]" +
                              ",[FacultyName]" +
                              ",[DepartmentName]" +
                              ",[PrintStatus]" +
                              ",[TransDate]" +
                            ") VALUES" +
                            "('" + AppNo +
                            "','" + MatricNumber +
                            "','" + AcademicSession +
                            "','" + txtContactPerson.Text.Trim().Replace("'", "''") +
                            "','" + TxtContactAddress.Text.Trim().Replace("'", "''") +
                            "','" + txtPhoneContact.Text.Trim().Replace("'", "''") +
                            "','" + DDListHealthStatus.Text +
                            "','" + DDListAdmitted.Text +
                            "','" + txtAdmitted.Text.Trim().Replace("'", "''") +
                            "','" + DDListMedication.Text +
                            "','" + TxtMedication.Text.Trim().Replace("'", "''") +
                            "','" + DDListDesease1.Text +
                            "','" + DDListDesease2.Text +
                            "','" + DDListDesease3.Text +
                            "','" + DDListDesease4.Text +
                            "','" + DDListDesease5.Text +
                            "','" + DDListDesease6.Text +
                            "','" + DDListDesease7.Text +
                            "','" + DDListDesease8.Text +
                            "','" + DDListDesease9.Text +
                            "','" + DDListDesease10.Text +
                            "','" + TxtDeseas.Text.Trim().Replace("'", "''") +
                            "','" + TxtOtherDetails.Text.Trim().Replace("'", "''") +
                            "','" + TxtTravelHistory.Text.Trim().Replace("'", "''") +
                            "','" + DDlistFamilyHealthy.Text +
                            "','" + DDListFamilyDesease1.Text +
                            "','" + DDListFamilyDesease2.Text +
                            "','" + DDListFamilyDesease3.Text +
                            "','" + DDListFamilyDesease4.Text +
                            "','" + DDListDrugReact.Text +
                            "','" + TxtDrugReact.Text.Trim().Replace("'", "''") +
                            "','" + DDListImunization1.Text +
                            "','" + DDListImunization2.Text +
                            "','" + DDListImunization3.Text +
                            "','" + DDListImunization4.Text +
                            "','" + Stud.Faculty +
                            "','" + Stud.Department +
                            "'," + printstatus +
                            ",'" + transdate + "')";
                PerformInsert(qry);                //History

               
                
                           qry = "INSERT INTO [MedicalEntranceHistory]" +
                           "([ApplcationNo]" +
                              ",[MatricNumber]" +
                              ",[SessionName]" +
                              ",[ContactPerson]" +
                              ",[ContactPersonAddress]" +
                              ",[ContactPersonPhone]" +
                              ",[HealthStatus]" +
                              ",[AdmittedInHospital]" +
                              ",[AdmissionReasons]" +
                              ",[Medication]" +
                              ",[MedicationDrugs]" +
                              ",[Desease1]" +
                              ",[Desease2]" +
                              ",[Desease3]" +
                              ",[Desease4]" +
                              ",[Desease5]" +
                              ",[Desease6]" +
                              ",[Desease7]" +
                              ",[Desease8]" +
                              ",[Desease9]" +
                              ",[Desease10]" +
                              ",[DeseaseDetails]" +
                              ",[OtherMedDetails]" +
                              ",[TravelHistory]" +
                              ",[HealthFamily]" +
                              ",[FamilyDesaes1]" +
                              ",[FamilyDesaes2]" +
                              ",[FamilyDesaes3]" +
                              ",[FamilyDesaes4]" +
                              ",[DrugReaction]" +
                              ",[DrugReactionDetails]" +
                              ",[Immunization1]" +
                              ",[Immunization2]" +
                              ",[Immunization3]" +
                              ",[Immunization4]" +
                              ",[FacultyName]" +
                              ",[DepartmentName]" +
                              ",[PrintStatus]" +
                              ",[TransDate]" +
                            ") VALUES" +
                            "('" + AppNo +
                            "','" + MatricNumber +
                            "','" + AcademicSession +
                            "','" + txtContactPerson.Text.Trim().Replace("'", "''") +
                            "','" + TxtContactAddress.Text.Trim().Replace("'", "''") +
                            "','" + txtPhoneContact.Text.Trim().Replace("'", "''") +
                            "','" + DDListHealthStatus.Text +
                            "','" + DDListAdmitted.Text +
                            "','" + txtAdmitted.Text.Trim().Replace("'", "''") +
                            "','" + DDListMedication.Text +
                            "','" + TxtMedication.Text.Trim().Replace("'", "''") +
                            "','" + DDListDesease1.Text +
                            "','" + DDListDesease2.Text +
                            "','" + DDListDesease3.Text +
                            "','" + DDListDesease4.Text +
                            "','" + DDListDesease5.Text +
                            "','" + DDListDesease6.Text +
                            "','" + DDListDesease7.Text +
                            "','" + DDListDesease8.Text +
                            "','" + DDListDesease9.Text +
                            "','" + DDListDesease10.Text +
                            "','" + TxtDeseas.Text.Trim().Replace("'", "''") +
                            "','" + TxtOtherDetails.Text.Trim().Replace("'", "''") +
                            "','" + TxtTravelHistory.Text.Trim().Replace("'", "''") +
                            "','" + DDlistFamilyHealthy.Text +
                            "','" + DDListFamilyDesease1.Text +
                            "','" + DDListFamilyDesease2.Text +
                            "','" + DDListFamilyDesease3.Text +
                            "','" + DDListFamilyDesease4.Text +
                            "','" + DDListDrugReact.Text +
                            "','" + TxtDrugReact.Text.Trim().Replace("'", "''") +
                            "','" + DDListImunization1.Text +
                            "','" + DDListImunization2.Text +
                            "','" + DDListImunization3.Text +
                            "','" + DDListImunization4.Text +
                            "','" + Stud.Faculty +
                            "','" + Stud.Department +
                            "'," + printstatus +
                            ",'" + transdate + "')";
                PerformInsert(qry);

                TxtDeseas.Text = "";
                TxtOtherDetails.Text = "";
                TxtTravelHistory.Text = "";
                TxtDrugReact.Text = "";
                txtContactPerson.Text = "";
                TxtContactAddress.Text = "";
                txtPhoneContact.Text = "";
                txtAdmitted.Text = "";
                TxtMedication.Text = "";
                LabelWelcome.Text = "";
                LabelAppNo.Text = "";

                msg = "Submission successful, make sure you print this form before printing the medical authorisation letter";
                showmassage(msg);


            }
        }
        catch (Exception ex)
        {           
           
        }            
        
        
    }
    private bool Existed(string qry)
    {
        bool ret = false;

        try
        {
            SqlConnection cnn = new SqlConnection(strConnectionString);

            cnn.Open();

            SqlCommand cmd = null;
            SqlDataReader dr = null;

            cmd = new SqlCommand(qry, cnn);

            dr = cmd.ExecuteReader();//
            if (dr.HasRows)
            {
                ret = true;
            }
            dr.Dispose();
            cmd.Dispose();
            cnn.Dispose();
            cnn.Close();
        }
        catch (Exception ex)
        {
            msg = ex.Message + "||" + ex.StackTrace;
            //LogError(msg, "Payroll", "");
            showmassage(msg);
        }

        return ret;
    }
    protected void BtnRetrieve_Click(object sender, EventArgs e)
    {
        if (Session["StudentSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        So = (StudentSignOn)Session["StudentSignOn"];
        LabelAppNo.Text = So.FormNumber;
        Med medinfo = new Med();
        medinfo = getinfo(So.MatricNumber);
        Refreshinfo(medinfo); 
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        if (Session["StudentSignOn"] == null)
        {
            Session.Contents.Clear();
            Response.Redirect("StudentLogin.aspx");
        }
        So = (StudentSignOn)Session["StudentSignOn"];
        Session["StudentSignOn"] = So;
        string sql = "Update [MedicalEntrance] set [PrintStatus]=1 where [MatricNumber] = '"+So.MatricNumber+"'";
        PerformInsert(sql);
        showwindow("RptMed1.aspx?StudID=" + So.MatricNumber);

       // showwindow("ReportShow.aspx?StudID=" + StudID + "&RptType=" + RptType + "&Yearr=" + Yearr + "&Date1=" + Date1 + "&Date2=" + Date2);

    }
    private void showwindow(string window)
    {
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.open(" + "'" + window + "'" + ")</script>";
        Page.Controls.Add(lbl);
    }
}
