using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Messaging;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using CybSoft.EduPortal.Business;
//using CybSoft.EduPortal.Data;
//using System.IO;
//using log4net;
//using log4net.Config;  

public partial class Admin_SignUpReversal : System.Web.UI.Page
{
    //SignOnBusiness sb = new SignOnBusiness();
    string msg = "";
    private static string EmailPackagePath = ".\\private$\\" + ConfigurationManager.AppSettings["EmailQ"];
    private static StringBuilder sbmsgbody = null;

    private static string str = ConfigurationManager.AppSettings["conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Role Here
        if (!IsPostBack)
        {
            PanelGrid.Visible = false;
        }

    }
    protected void btnSearchParam_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtWrongFormNumber.Text))
        {
            msg = "Please enter Applicant's Form Number wrongly used to signup.";
            showmassage(msg);
            lblError.Text = msg;
            PanelGrid.Visible = false;
            return;            
        }

        if (string.IsNullOrEmpty(txtCorrectFormNumber.Text))
        {
            msg = "Please enter right Applicant's Form Number.";
            showmassage(msg);
            lblError.Text = msg;
            PanelGrid.Visible = false;
            return;
        }
        if (string.Equals(txtCorrectFormNumber.Text, txtWrongFormNumber))
        {
            msg = "Form Number cannot be the same. Please enter different Form Number.";
            showmassage(msg);
            lblError.Text = msg;
            PanelGrid.Visible = false;
            return;

        }
        //lblMessage.Text = "";
        DataTable dtWrongFormNo = BursaryBusiness.getSignUpDetail(txtWrongFormNumber.Text), 
            dtCorrectFormNo = BursaryBusiness.getSignUpDetail(txtCorrectFormNumber.Text);// sb.SelectQuery(thisQuery);

        if (dtWrongFormNo ==null || dtWrongFormNo.Rows.Count < 1)
        {
            msg = "The form number enter for wrong form number does not have any records. You cannot proceed";
            PanelGrid.Visible = false;
            lblError.Text = msg;
            showmassage(msg);
            return;
        }
        if (dtCorrectFormNo ==null || dtCorrectFormNo.Rows.Count < 1)
        {
            msg = "The form number enter for correct form number does not have any records. You cannot proceed";
            PanelGrid.Visible = false;
            lblError.Text = msg;
            showmassage(msg);
            return;
        }
        if (dtWrongFormNo.Rows[0][6].ToString().ToUpper() != dtCorrectFormNo.Rows[0][6].ToString().ToUpper()
            && BursaryBusiness.getActiveSession() != dtCorrectFormNo.Rows[0][6].ToString().ToUpper())
        {
            //session must be active and the same
            msg = "The session of admission must be current session and must match for the form numbers entered";
            PanelGrid.Visible = false;
            lblError.Text = msg;
            showmassage(msg);
            return;
        }
        if (dtWrongFormNo.Rows[0][17].ToString().ToUpper() == "FALSE")
        {
            //offer not accepted yet
            msg = "The wrong form number entered have not accepted offer yet. You cannot proceed";
            PanelGrid.Visible = false;
            lblError.Text = msg;
            showmassage(msg);
            return;
        }
        if (dtCorrectFormNo.Rows[0][17].ToString().ToUpper() != "FALSE")
        {
            //offer accepted by correct form already
            msg = "The right form number entered have accepted offer already. You cannot proceed";
            PanelGrid.Visible = false;
            lblError.Text = msg;
            showmassage(msg);
            return;
        }
        if (Convert.ToInt32(Convert.ToDouble(dtWrongFormNo.Rows[0][19])) < Convert.ToInt32(dtWrongFormNo.Rows[0][18]))
        {
            //amount paid is less amount expected
            msg = "The amount paid by wrong form number is less than the amount exptected to pay by the correct form number. You can ONLY proceed with roll back. Student will need to pay the appropriate amount of " + dtWrongFormNo.Rows[0][18].ToString() + " to signup on the portal.";
            PanelGrid.Visible = true;
            lblError.Text = msg;
            btnReverseSignUp.Visible = false;
            btnRollBackSignUp.Visible = true;
            showmassage(msg);
        }
        else
        {
            //offer not accepted yet
            msg = "The amount paid by wrong form number will be converted to the amount exptected to pay by the correct form number. You can now proceed with signup reversal.";
            PanelGrid.Visible = true;
            lblError.Text = msg;
            btnReverseSignUp.Visible = true;
            btnRollBackSignUp.Visible = false;
            showmassage(msg);
        }
        PanelGrid.Visible = true;
        //DetailsView1.Width = 1000;
        DetailsView1.DataSource = dtWrongFormNo;
        DetailsView2.DataSource = dtCorrectFormNo;
        DetailsView1.DataBind();
        DetailsView2.DataBind();
    }
    private void showmassage(string message)
    {
        message = message.Replace("'", " ").Replace("\r\n", "");
        MasterPage master = (MasterPage)this.Master;
        master.ClientMessage(this.Page, message);
        //Label lbl = new Label();
        //lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + message + "'" + ")</script>";
        //Page.Controls.Add(lbl);
    }
    private DataSet SearchData(string qry)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection cnn = new SqlConnection(str);
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

    protected void btnReverseSignUp_Click(object sender, EventArgs e)
    {
        //delete registration
        //change payment details/pin usage
        //change signup details
        //change studentdetails
        if (BursaryBusiness.ReverseSignUp(DetailsView1.Rows[0].Cells[0].ToString(),
            DetailsView2.Rows[0].Cells[0].ToString(),
            DetailsView1.Rows[0].Cells[20].ToString(),
            HttpContext.Current.User.Identity.Name.ToUpper()) == true)
        {
            msg = "Sign Up reversal was successful. The Applicant can now login with his matric number";
            mailSetUp();
        }
        else msg = "Sign Up Reversal failed";

        PanelGrid.Visible = false;
        lblError.Text = msg;
        txtCorrectFormNumber.Text = "";
        txtWrongFormNumber.Text = "";
        showmassage(msg);

    }
    protected void btnRollBackSignUp_Click(object sender, EventArgs e)
    {
        //delete reg
        //delete payment
        //blocked pin
        //delete student details
        //delete signup details
        if (BursaryBusiness.RollBackSignUp(DetailsView1.Rows[0].Cells[0].ToString(),
            DetailsView2.Rows[0].Cells[0].ToString(),
            DetailsView1.Rows[0].Cells[20].ToString(),
            HttpContext.Current.User.Identity.Name.ToUpper()) == true)
        {
            msg = "Sign Up rollback was successful. The applicant can now purchase correct order now to sign up again.";
            mailSetUp();
        }
        else
            msg = "Sign Up rollback failed";

        PanelGrid.Visible = false;
        lblError.Text = msg;
        txtCorrectFormNumber.Text = "";
        txtWrongFormNumber.Text = "";
        showmassage(msg);

    }

    private void mailSetUp()
    {
        string Subject = "Sign Up Reversal/Roll Back";
        string Heading = "PETROLEUM TRAINING INSTITUTE - admin portal Sign Up Reversal";
        string Attached = "";
        string email = DetailsView2.Rows[0].Cells[13].Text;
        string name = DetailsView2.Rows[0].Cells[1].Text;



        sbmsgbody = new StringBuilder();

        sbmsgbody.AppendLine("");
        sbmsgbody.AppendLine("");
        if (btnReverseSignUp.Visible == true)
        {
            sbmsgbody.AppendLine("Your earlier request for sign up reversal is now approved");
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("You can now proceed to your student profile with the matric number generated for you during sign up.");
        }
        else
        {
            sbmsgbody.AppendLine("Your earlier request for sign up reversal is now approved for roll back.");
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("You will need to go back to the bank and pay up so you can sign up afresh.");

        }
        sbmsgbody.AppendLine("");
        sbmsgbody.AppendLine("If you have not applied for any reversal please visit the account department immediately.");
        sbmsgbody.AppendLine("");


        sendGenMail(email, sbmsgbody.ToString(), Subject, Heading, Attached, name);

    }
    private void sendGenMail(string maillist, string messg, string subject, string Heading, string attachedFile, string StaffName)
    {
        try
        {
            Cyberspace.Emailpackage.CMail cm = null;
            //foreach (DictionaryEntry em in maillist)
            //{
            string mlist = maillist;
            string staff = StaffName;

            cm = new Cyberspace.Emailpackage.CMail();
            cm.Subject = subject;
            cm.ToEmail.Add(mlist);
            //cm.CCEmail.Add("itsupport@dlc.ui.edu.ng");
            cm.AttachedFile = attachedFile;
            //cm.FromEmail.Add();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear" + " " + staff + ",");
            //sb.AppendLine();
            sb.AppendLine(Heading.ToUpper());
            //sb.AppendLine("-------------------------------");
            sb.AppendLine();
            sb.AppendLine(messg);
            sb.AppendLine();
            sb.AppendLine("Best Regards,");
            sb.AppendLine("");
            sbmsgbody.AppendLine("Portal Support Team");
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("");
            sbmsgbody.AppendLine("PS: For further information about this mail, send a mail to Student/IT Support Team: itsupport@dlc.ui.edu.ng or studentsupport@dlc.ui.edu.ng");
            sbmsgbody.AppendLine("");
            sb.AppendLine("======================================================================================");
            sb.AppendLine("PETROLEUM TRAINING INSTITUTE, WARRI, NIGERIA");

            cm.Body = sb.ToString();
            cm.BCCEmail.Add("cybsoft@cybaaspace.net");
            cm.CCEmail.Add("itsupport@dlc.ui.edu.ng");
            cm.ReplyTo.Add("itsupport@dlc.ui.edu.ng");
            cm.DisplayName = subject;
            cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm.SourceApplication = "Mail Reminder system," + " " + "Computer Host Name:" + " " + Dns.GetHostName().ToUpper();


            sendtoMailQueue(cm);
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {


        }

        // }
    }
    private void sendtoMailQueue(Cyberspace.Emailpackage.CMail cm)
    {
        try
        {
            DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
            dfp.AttachSenderId = true;
            dfp.Recoverable = true;

            MessageQueue mq;
            if (!MessageQueue.Exists(EmailPackagePath))
            {
                mq = MessageQueue.Create(EmailPackagePath);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

            }
            else
            {
                mq = new MessageQueue(EmailPackagePath);
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Cyberspace.Emailpackage.CMail) });
                mq.DefaultPropertiesToSend = dfp;
            }


            mq.DefaultPropertiesToSend.Recoverable = true;
            mq.DefaultPropertiesToSend.AttachSenderId = true;
            mq.DefaultPropertiesToSend.Label = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            mq.Send(cm);
            mq.Dispose();
            mq.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + "||" + ex.StackTrace);
        }
    }

}
