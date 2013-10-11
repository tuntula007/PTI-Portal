using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Messaging;
using System.Text.RegularExpressions;


public partial class ProfileManager_RecoverPassword : System.Web.UI.Page
{
    private string str = System.Configuration.ConfigurationManager.AppSettings["conn"];
    private static string ZRMailQ = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["Clientmail"];
    private static string EmailPackagePath = ".\\private$\\" + ConfigurationManager.AppSettings["EmailQ"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private ArrayList GetEmails(string bodyin)
    {

        ArrayList ar = new ArrayList();
        try
        {


            //string patternStrict = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            //Regex rsg = new Regex(patternStrict, RegexOptions.Multiline);
            string patternStrict = ConfigurationManager.AppSettings["regexmail"];
            MatchCollection mc = Regex.Matches(bodyin, patternStrict);


            for (int i = 0; i < mc.Count; i++)
            {
                ar.Add(mc[i].ToString());

            }


        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);

        }

        return ar;

    }

    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ")</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {


        ArrayList ar = new ArrayList();
        ar = GetEmails(this.TextBox1.Text);
        if (ar.Count > 0)
        {


            SqlConnection cnn = new SqlConnection(str);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select Userid,passw from TUsers where emailaddress ='" + ar[0].ToString() + "'", cnn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {// Already exists 


                Cyberspace.Emailpackage.CMail cm = null;
                cm = new Cyberspace.Emailpackage.CMail();

                //cm.Subject = "Library Portal Credentials";
                //cm.ToEmail.Add(ar[0].ToString().ToLower());
                StringBuilder sb = new StringBuilder();
                string strnewpassword = dr.GetString(1); //CreatPassword();
                //string userid = dr.GetString(0);


                string Subject = "Login Credentials";
                string Heading = "PETROLEUM TRAINING INSTITUTE, EFFURUN ADMIN PORTAL LOGIN CREDENTIALS";
                string Attached = "";
                string email = ar[0].ToString().ToLower();
                string name = dr.GetString(0);



                sb.AppendLine("");
                sb.AppendLine("Your userid =  " + name);
                sb.AppendLine("Your password = " + strnewpassword);
                sb.AppendLine("---------------------------");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("PETROLEUM TRAINING INSTITUTE, EFFURUN");


                ////cm.Body = sb.ToString();
                //////cm.CCEmail.Add("hr@visafone.com.ng");
                //////cm.ReplyTo.Add("visafonehrmailalert@gmail.com");
                ////cm.DisplayName = "Zenith Registrar Portal Credentials";
                ////cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                ////cm.SourceApplication = "Zenith Registrar";

                
                dr.Dispose();
                cmd.Dispose();

               // sendtoMailQueue(cm);
                sendGenMail(email, sb.ToString(), Subject, Heading, Attached, name);
                MessageBox("Successfully retrieved your credentials. Please check your email to view it.");

                
                //Response.Redirect("loginaccess.aspx");
                TextBox1.Text = "";
                //HttpContext.Current.ApplicationInstance.Context.Cache.Remove(ID);

                //Response.Redirect("~/loginaccess.aspx");
                
            }
            else
            {
                MessageBox("Your email address was not found in the database.");
                return;
            }

        }
        else
        {
            MessageBox("Your email address appears to be in a wrong format");
            return;
        }
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
            sb.AppendLine("======================================================================================");
            sb.AppendLine("PETROLEUM TRAINING INSTITUTE, EFFURUN");

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
    //private void sendtoMailQueue(CMail cm)
    //{
    //    try
    //    {
    //        DefaultPropertiesToSend dfp = new DefaultPropertiesToSend();
    //        dfp.AttachSenderId = true;
    //        dfp.Recoverable = true;

    //        MessageQueue mq;
    //        if (!MessageQueue.Exists(ZRMailQ))
    //        {
    //            mq = MessageQueue.Create(ZRMailQ);
    //            mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
    //        }
    //        else
    //        {
    //            mq = new MessageQueue(ZRMailQ);
    //            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(CMail) });
    //            mq.DefaultPropertiesToSend = dfp;
    //        }

    //        mq.DefaultPropertiesToSend.Recoverable = true;
    //        mq.DefaultPropertiesToSend.AttachSenderId = true;
    //        mq.DefaultPropertiesToSend.Label = "ZR " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    //        mq.Send(cm);
    //        mq.Dispose();
    //        mq.Close();

    //    }
    //    catch (Exception er)
    //    {
    //        string err = er.Message;
    //    }
    //}
    private string CreatPassword()
    {
        Guid g = Guid.NewGuid();

        return g.ToString().Substring(0, 5);
    }
}
