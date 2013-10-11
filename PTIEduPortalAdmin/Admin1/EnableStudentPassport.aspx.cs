using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Messaging;
using System.Text.RegularExpressions;

public partial class Admin_EnableStudentPassport : System.Web.UI.Page
{
    string msg = "";
    private static string EmailPackagePath = ".\\private$\\" + ConfigurationManager.AppSettings["EmailQ"];
    private static StringBuilder sbmsgbody = null;

    private static string str = ConfigurationManager.AppSettings["conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Role Here

    }
    protected void btnSearchParam_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSearchParameter.Text))
        {
           msg = "Please enter Form or Matric Number to Search.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }

        string thisQuery = "select matricnumber MatricNumber, email Email, case isnull(canchangepassport,0) when 0 then 'LOCKED' else 'NOT LOCKED' end as [Passport Status] from students where matricnumber='" + txtSearchParameter.Text.Trim() + "'OR [RegNo]='" + txtSearchParameter.Text.Trim() + "'";
        DataSet dtRecord = SearchData(thisQuery);
        if (dtRecord ==null | dtRecord.Tables[0].Rows.Count < 1)
        {
            msg = "There is no such student with this Form/Matric Number.";
            GridView1.Visible = false;
            showmassage(msg);
            return;
        }
        GridView1.Visible = true;
        lblPassport.Text = dtRecord.Tables[0].Rows[0][2].ToString();
        btnUpdatePassport.Text = (lblPassport.Text.ToUpper() == "LOCKED") ? "Enable Passport" : "Disable Passport";
        btnUpdatePassport.Visible = true;
        GridView1.DataSource = dtRecord.Tables[0];
        GridView1.DataBind();
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
    protected void btnUpdatePassport_Click(object sender, EventArgs e)
    {
        DataRetrieval dt = new DataRetrieval();
        string qry = "UPDATE Students SET CanchangePassport=" + ((lblPassport.Text.ToUpper() == "LOCKED") ? "1" : "0");
        qry = "; INSERT INTO TUsersLogs (StaffName, Action) values ('" + HttpContext.Current.User.Identity.Name.ToUpper() + "','PASSPORT STATUS: " + GridView1.Rows[0].Cells[0].Text + " CHANGED FROM " + ((lblPassport.Text.ToUpper() == "LOCKED") ? "LOCKED TO UNLOCKED" : "UNLOCKED TO LOCKED") + "')";
        try
        {
            dt.PerformQuery(qry);
            showmassage("Passport change updated and change successfully");

        }
        catch (Exception ex)
        {
            showmassage("There was a problem updating passport status:" + ex.Message);
            return;
        }
        string Subject = "Login Credentials";
        string Heading = "PTI - admin portal Login Credentials";
        string Attached = "";
        string email = GridView1.Rows[0].Cells[1].Text;
        string name = GridView1.Rows[0].Cells[0].Text;


        //String msgbody = "Please, be informed that the following registration numbers are not found in the original list for admission:" + " " + RegNotFound;

        sbmsgbody = new StringBuilder();

        sbmsgbody.AppendLine("");

        sbmsgbody.AppendLine("");
        sbmsgbody.AppendLine("With reference to your earlier request to change passport, your passport change is now enabled");
        sbmsgbody.AppendLine("");
        sbmsgbody.AppendLine("You can now proceed to update your passport.");
        sbmsgbody.AppendLine("");
        sbmsgbody.AppendLine("Note that after you update, passport change will be locked once again.");
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
            cm.CCEmail.Add("itsupport@dlc.ui.edu.ng");
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
            cm.ReplyTo.Add("itsupport@dlc.ui.edu.ng");
            cm.DisplayName = subject;
            cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm.SourceApplication = "UI";

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
