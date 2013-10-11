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
using System.Net.Mail;
using System.Net;
using System.Text;
using CybSoft.EduPortal.Business;
using CybSoft.EduPortal.Data;
using Alerts.Email.Settings;
using Alerts.Email.Settings.Enums;

public partial class ApplicantForgetPassword : System.Web.UI.Page
{
    private static readonly string CONFIG_SECTION_NAME = "EmailSettingsSectionName";
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string matricNo = "";
        matricNo = HttpUtility.HtmlEncode(txtMno.Text);
        string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
        byte[] buffer;
        string PinSku = "";
        buffer = CyberEncryptor.encypt("48705084986", Key);
        PinSku = Convert.ToBase64String(buffer);
        try
        {
            //1. getPasswordByMatNo(matricNo)
            string[] stringAr = new string[2];
            ArrayList ar = new ArrayList();

            ar = ApplicantSignOnBusiness.getPasswordByUserName(matricNo);  //  Report.getEmailBody(mtype, i);
            int cnt = 0;
            foreach (string star in ar)
            {
                stringAr[cnt++] = star;
            }
            EmailSmtpSettings settings = GetSettings();

            foreach (SmtpServerSettings serv in settings.AllServers)
            {
                //The EmailSmtpHandler encapulates the information found in the App.Config file
                //The "Handler" reads the xml ... to create a EmailSmtpSettings instance. Console.WriteLine("About to send an email using : " + serv.SmtpServerName);

                ltrMessage.Text = (SendEmail(serv, stringAr[0], stringAr[1])) ?
                    "A mail has been sent to your email address: " + stringAr[0] + " <br /> If you have any difficulty opening your mail box visit the ICT Center or send a mail to itsupport@pti.edu.ng" :
                    "Password cannot be sent to your email address: " + stringAr[0] + " <br /> Visit the ICT Center or send an email to itsupport@pti.edu.ng. Thank you.";
                //An Error Occured while sending mail, If you have any difficulty opening your mail box visit the ICT Center or send a mail to itsupport@pti.edu.ng
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (null != ex.InnerException)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            ltrMessage.Text = "An Error Occured while retriving your email address:  Confirm that you enter the correct Form Number/User name above. <br /> If you have any difficulty opening your mail box visit the ICT Center or send a mail to itsupport@pti.edu.ng";
        }
        finally
        {
            //Console.WriteLine("Press Enter to Exit");
            //Console.ReadLine();
            txtMno.Text = "";
        }
    }
    private static Alerts.Email.Settings.EmailSmtpSettings GetSettings()
    {
        Alerts.Email.Settings.EmailSmtpSettings returnSettings;

        returnSettings = ((Alerts.Email.Settings.EmailSmtpSettings)(System.Configuration.ConfigurationManager.GetSection(CONFIG_SECTION_NAME)));

        return returnSettings;
    }
    public static bool SendEmail(SmtpServerSettings serv, string addressTo, string passWord)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear Applicant,<br/>");
            sb.AppendLine("We are pleased to inform you that your password is: " + passWord);
            //sb.AppendLine("We are pleased to inform you that your password is: <br/>");
            sb.AppendLine();
            sb.AppendLine("<br />To maintain your accessibility to this portal, you are adviced to keep you credentials as secret as posssible.<br/>");
            sb.AppendLine("Please remember to direct personal questions to the Administrator.<br/><br/><br/>");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Best Regards,<br/>");
            sb.AppendLine("PTI Portal Team<br/>");
            sb.AppendLine("Please be advised not to reply this mail.<br/>");

            sb.AppendLine("======================================================================================<br/>");
            sb.AppendLine("Petroleum Training Institute,Warri.<br/>");
            string sBody = sb.ToString();
            //string toAddress = "itsupport@pti.edu.ng";
            string toAddress = addressTo;
            string emailSubject = "RE: Admission Portal Password";
            System.Net.Mail.SmtpClient email = new System.Net.Mail.SmtpClient();
            email.Host = serv.SmtpServerName;

            if (serv.PortNumber.Length > 0)
            {
                email.Port = Convert.ToInt32(serv.PortNumber);
            }
            switch (serv.AuthenicationMethod)
            {
                case AuthenticationType.None:
                    break;
                case AuthenticationType.Basic:
                    email.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    email.UseDefaultCredentials = false;
                    email.Credentials = new NetworkCredential(serv.SmtpUserName, serv.SmtpUserPassword);
                    break;
                case AuthenticationType.SSL:
                    email.EnableSsl = true;
                    email.Credentials = new NetworkCredential(serv.SmtpUserName, serv.SmtpUserPassword);
                    //email.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network ;
                    break;
            }
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage(serv.DefaultFromAddress, toAddress, emailSubject, sBody);
            mailMsg.IsBodyHtml = true;
            mailMsg.Bcc.Add(new MailAddress("itsupport@pti.edu.ng"));
            mailMsg.Bcc.Add(new MailAddress("cybsoft@cyberspace.net.ng"));
            email.Send(mailMsg);
            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //throw ex;
            return false;
        }
    }
}
