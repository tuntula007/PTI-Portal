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
using System.Messaging;
using log4net;
using log4net.Config;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using Alerts.Email.Settings;
using Alerts.Email.Settings.Enums;
using Vas.Transaction.Messaging;
using Vas.EmailAlertMessage;
using System.Threading;


public partial class ApplicantsSignOn : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string Key = System.Configuration.ConfigurationManager.AppSettings["Key"];
    static string emailqueue = System.Configuration.ConfigurationManager.AppSettings["emailalertqueue"];
    string CurrentSession = new ApplicantSignOnBusiness().getCurrentApplicationSession();
    private static readonly string CONFIG_SECTION_NAME = "EmailSettingsSectionName";
    private static int threadCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        //new Utility().MessageBox("Page is currently under construction, Please try again at a latter date", ResolveUrl("Index.html"), this.Page);
        //return;

        this.ViewState["pwd"] = "";

        if (!IsPostBack)
        {
            ddlProg.Items.Clear();
            ddlProg.Items.Clear();

            string strQuery = "SELECT [EntryMode] FROM [EntryMode]";
            DataSet ds = new DataSet();
            ds = new SignOnBusiness().SelectQuery(strQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEntryMode.DataSource = ds;
                ddlEntryMode.DataTextField = "entryMode";
                ddlEntryMode.DataValueField = "entryMode";
                ddlEntryMode.DataBind();
                ddlEntryMode.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlProg.Items.Insert(0, new ListItem("Select Entry Mode", "NO"));
                new Utility().MessageBox("Please make sure you enter a valid and direct EMAIL/PHONE NUMBER as it may be used to contact you subsequently.                                                                                                                                IF YOUR HAVE STARTED YOUR APPLICATION PROCESS BEFORE NOW,JUST CLOSE THIS MESSAGE BOX AND CLICK ON THE RETURNING APPLICANT LINK COLOURED GREEN ON THIS APPLICANT SIGNON WEB PAGE ", this.Page);
            }
            else
            {
                ddlProg.Items.Insert(0, new ListItem("No Program for this Entry Mode", "NO"));
                ddlEntryMode.Items.Insert(0, new ListItem("No Entry Mode Created", "NO"));
                BtnSignOn.Enabled = false;
            }

        }
        else
        {
            if (!(String.IsNullOrEmpty(txtPinNo.Text.Trim()))) txtPinNo.Attributes["value"] = txtPinNo.Text;
        }
    }

    protected void BtnSignOn_Click(object sender, EventArgs e)
    {
        Session["ApplicantSignOn"] = null;
        LblError.Text = "";
        //txtPinNo.Text = this.ViewState["pwd"].ToString();
        ApplicantSignOnBusiness Aob = new ApplicantSignOnBusiness();
        ApplicantSignOn Ao = new ApplicantSignOn();
        PinVerifyResult Pvr = new PinVerifyResult();

        //if (ddlEntryMode.SelectedIndex <1)
        //Verify if application is still on sale
        string session = "";
        session = ApplicantsBusiness.ActiveSession();
        string Programme = ddlProg.SelectedValue;
        string EntryMode = ddlEntryMode.SelectedValue;
        string currentdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        // StudentSignOn So = new StudentSignOn();
        //Verifying Data

        PanelError.Visible = true;

        //check if school fees pin is empty
        if (string.IsNullOrEmpty(txtPinNo.Text.Trim()))
        {
            LblError.Text = "Empty Application Form Bank PIN <br />  You have not entered Application Form Bank PIN.";
            new Utility().MessageBox(LblError.Text.Replace(" <br />", ""), this.Page);
            PanelError.Visible = true;
            txtPinNo.Focus();
            logger.Warn(txtUserName.Text + " - " + LblError.Text);
            return;
        }

        if (string.IsNullOrEmpty(txtUserName.Text))
        {
            LblError.Text = "Enter User Name";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtUserName.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtSurname.Text))
        {
            LblError.Text = "Enter Your Surname or family name";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtSurname.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtOtherName.Text))
        {
            LblError.Text = "Enter Other Name";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtOtherName.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPassWord.Text) || string.IsNullOrEmpty(txtPasswordC.Text))
        {
            LblError.Text = "Password Must be Entered";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus();
            return;
        }
        if (txtPassWord.Text.Trim().Contains("●") || txtPasswordC.Text.Trim().Contains("●"))
        {
            LblError.Text = "Such characters are not allowed for password";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus();
            return;
        }

        if (txtPassWord.Text.Trim() != txtPasswordC.Text.Trim())
        {
            LblError.Text = "Password and Confirm Password NOT MATCH";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPassWord.Focus();
            return;
        }

        //if (txtPassWord.Text.Trim() == txtUserName.Text.Trim())
        //{
        //    LblError.Text = "User Name and Password CANNOT be the same";
        //    new Utility().MessageBox(LblError.Text, this.Page);
        //    txtPassWord.Focus();
        //    return;
        //}

        if (string.IsNullOrEmpty(txtEmail.Text))
        {
            LblError.Text = "Enter Your Email Address";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtEmail.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtPhone.Text))
        {
            LblError.Text = "Enter Your Phone Number";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtPhone.Focus();
            return;
        }

        if (ddlProg.SelectedValue.Equals("NO"))
        {
            LblError.Text = "Select appropriate Programme";
            new Utility().MessageBox(LblError.Text, this.Page);
            ddlProg.Focus();
            return;
        }
        StringBuilder sbEmailPtn = new StringBuilder();
        sbEmailPtn.Append(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)");
        sbEmailPtn.Append(@"(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|");
        sbEmailPtn.Append(@"(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

        if (!Regex.IsMatch(txtEmail.Text, sbEmailPtn.ToString()))
        {
            LblError.Text = "Wrong Email Address Format";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtEmail.Focus();
            return;
        }


        if (!Aob.IsApplicationSalesAvailable(session.Trim(), Programme, EntryMode, currentdate))
        {
            LblError.Text = "Application sales for this programme and mode of entry is over";
            new Utility().MessageBox(LblError.Text, this.Page);
            txtUserName.Focus();
            return;
        }

        //new Utility().ConfirmBox("Please confirm you have enter your information correctly before tou continue. Your correct full name with valid and direct email/phone no is very important. You may not be allow to change/edit such information later on", this.Page);

        //check if user name number in students table
        if (ApplicantsBusiness.isUserNameExists(txtUserName.Text.Trim()) == true)
        {
            LblError.ForeColor = Color.Red;
            LblError.Text = "User Name already used, Your sign on is denied beacuse the User name already exist.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.Text = "";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = false;
            return;
        }
        //check if email already exist
        if (ApplicantsBusiness.isEmailExists(txtEmail.Text.Trim()) == true)
        {
            LblError.ForeColor = Color.Red;
            LblError.Text = "Email already used. Your sign on was denied because another applicant is already tied to this Email.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.Text = "";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = false;
            return;
        }
        //check if phone already exist
        if (ApplicantsBusiness.isPhoneExists(txtPhone.Text.Trim()) == true)
        {
            LblError.ForeColor = Color.Red;
            LblError.Text = "Phone Number already used. Your sign on was denied because another applicant is already tied to this Phone Number.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.Text = "";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = false;
            return;
        }

        // test for PIN
        bool AppFormFeePinVerified = false;

        string AppFormFeePinComment = "";

        byte[] buffer;

        string AppForm = "";

        // Verifying School Fee Pin
        buffer = CyberEncryptor.encypt(txtPinNo.Text.Trim(), Key);
        AppForm = Convert.ToBase64String(buffer);
        //AppForm = txtPinNo.Text.Trim();

        #region clear password test
        //AppForm = txtPinNo.Text.Trim(); //---- used to test clear password
        #endregion

        Pvr = Aob.VerifyApplicationFeePin(AppForm, txtUserName.Text, ddlProg.SelectedValue.ToLower(), ddlEntryMode.SelectedValue.ToLower());

        AppFormFeePinVerified = Pvr.Verified;
        AppFormFeePinComment = Pvr.FailureComment;
        if (!AppFormFeePinVerified)
        {
            LblError.Text = "Invalid Admission Form Bank PIN <br /> " + AppFormFeePinComment;
            //LblErrorCause.Text = AppFormFeePinComment;
            new Utility().MessageBox(LblError.Text.Replace(" <br />", ""), this.Page);
            PanelError.Visible = true;
            txtPinNo.Attributes["value"] = "";
            txtPinNo.Text = "";
            logger.Warn(txtUserName.Text + " - " + LblError.Text);
            return;
        }

        PanelError.Visible = false;
        bool upd = false;

        ///Updateing SignOn Table and Pin Table
        //buffer = CyberEncryptor.encypt(txtPinNo.Text.Trim(), Key);
        //AppForm = Convert.ToBase64String(buffer);

        //upd = Aob.UpdateSignOn(txtUserName.Text , AppForm, "");
        //logger.Info(txtUserName.Text  + " -  Applicant Sign on table was updated successfully");
        ////Updating School Fee PIN Table

        string NowDate = DateTime.Now.ToString("yyyy-MM-dd");


        upd = Aob.UpdateApplicationFeePin(txtUserName.Text, NowDate, AppForm);

        logger.Info(Ao.UserName + " - Application Fees Ref No table was updated successfully");

        //

        Ao = Aob.SignOnExist(txtUserName.Text.Trim());
        if (string.IsNullOrEmpty(Ao.UserName))
        {
            // Sign on does not exist

            Ao.UserName = txtUserName.Text.Trim();
            Ao.Password = txtPassWord.Text.Trim();
            Ao.Phone = txtPhone.Text.Trim();
            Ao.Email = txtEmail.Text.Trim();
            Ao.Programme = ddlProg.SelectedValue;
            Ao.Surname = txtSurname.Text;
            Ao.OtherNames = txtOtherName.Text;
            Ao.IsFastTrack = rdbProgramOption.SelectedItem.Value.ToUpper();
            Ao.ModeOfStudy = ddlEntryMode.SelectedValue;
            Ao.ApplicationPIN = AppForm; // encrypted pin txtPinNo.Text;
            string FormNumber = "";
            bool inserted = Aob.InsertSignOn(Ao, ref FormNumber);
            if (inserted)
            {
                Ao.FormNumber = FormNumber;
                try
                {
                    //EmailSmtpSettings settings = GetSettings();

                    //foreach (SmtpServerSettings serv in settings.AllServers)
                    //{
                    //    //The EmailSmtpHandler encapulates the information found in the App.Config file
                    //The "Handler" reads the xml ... to create a EmailSmtpSettings instance. Console.WriteLine("About to send an email using : " + serv.SmtpServerName);

                    //string conn = ".\\Private$\\externalvasqueue";
                    // InitializeMessageQueue(conn); // initialize

                    //SendEmail(serv, Ao.Email, Ao.Password, Ao.UserName, txtSurname.Text.ToUpper() + " " + txtOtherName.Text, Ao.FormNumber, session);


                    InitializeMessageQueue(); // initialize all queues

                    //}
                    // send SMS

                    //////Thread AlertThread = new Thread(new ParameterizedThreadStart(SMSHandler));
                    //////AlertThread.Start(Ao);
                    //////Interlocked.Increment(ref threadCount);



                    Ao.ApplicationSession = session;
                    // Email Alert

                    //AlertThread = new Thread(new ParameterizedThreadStart(EmailHandler));

                    Thread AlertThread = new Thread(new ParameterizedThreadStart(EmailHandler));
                    AlertThread.Start(Ao);
                    Interlocked.Increment(ref threadCount);
                }
                catch
                {
                }
                LblError.ForeColor = Color.Blue;
                LblError.Text = "Sign On Inserted Successfully";
                LnkLogin.Text = "Click here to Login";
                LnkLogin.ForeColor = Color.Blue;
                LnkLogin.Visible = true;
                PanelError.Visible = true;
                Session["ApplicantSignOn"] = Ao;
                //call pop that will redirect back to logon page
                new Utility().MessageBox("Congrats! Your Sign on was successfully! Your Form Number is " + Ao.FormNumber + ". Just logon with your new password to continue...", ResolveUrl("ApplicantLogin.aspx"), this.Page);
            }
            else
            {
                LblError.ForeColor = Color.Red;
                LblError.Text = "Your Sign up was not successful! Try again Later";
                new Utility().MessageBox(LblError.Text, this.Page);
                LnkLogin.Text = "";
                LnkLogin.ForeColor = Color.Blue;
                LnkLogin.Visible = false;
                return;
            }
        }
        else
        {
            //sign on exist
            LblError.ForeColor = Color.Red;
            LblError.Text = "You have already signed-up with this user name.";
            new Utility().MessageBox(LblError.Text, this.Page);
            LnkLogin.Text = "Click here to Login";
            LnkLogin.ForeColor = Color.Blue;
            LnkLogin.Visible = true;
            return;
        }
        Session["ApplicantSignOn"] = Ao;

    }

    static void SMSHandler(object data)
    {
        try
        {
            ApplicantSignOn Ao = (ApplicantSignOn)data;
            TransactionMessage tm = new TransactionMessage();
            tm.DateIn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            tm.Direction = 1;
            tm.LastModified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            tm.MessageType = SmsMessageType.SMSText;
            tm.ModifiedState = ModificationState.ResponseOut;
            tm.ModifiedStatus = 0;
            tm.Msisdn = Ao.Phone.Format234();
            tm.NetworkID = System.Configuration.ConfigurationManager.AppSettings["smsnetworkid"]; // appsettings
            tm.OperatorID = System.Configuration.ConfigurationManager.AppSettings["smsoperatorid"];

            tm.RequestMessage = "nil";
            tm.ResponseMessage = "Dear " + Ao.Surname + " " + Ao.OtherNames + ", The following is your signup detail; Form no: "
                + Ao.FormNumber + ", Username: " + Ao.UserName + ", Password: " + Ao.Password
                + ". Thank you.";
            //+ ". You can access your profile through http://www.pti.edu.ng/Applicantlogin.aspx. Thank you.";
            //Hi [FullName]! Ur admission signup detail is: username-[username], password-[password]. -From PTI;
            tm.ShortCode = System.Configuration.ConfigurationManager.AppSettings["smssenderid"];




            //string conn = ".\\Private$\\" +   tm.NetworkID + "_"+ tm.OperatorID+ "_out";
            string conn = ".\\Private$\\externalvasqueue";
            //InitializeMessageQueue(conn); // initialize

            MessageQueue mq = new MessageQueue(conn);
            mq.Send(tm);

            // 


        }
        catch (Exception ex)
        {
            string exx = ex.Message;

        }


    }

    private static void InitializeMessageQueue()
    {
        try
        {
            DefaultPropertiesToSend defaultPropertiesToSend;
            defaultPropertiesToSend = new DefaultPropertiesToSend();
            defaultPropertiesToSend.AttachSenderId = true;
            defaultPropertiesToSend.Recoverable = true;
            MessageQueue mq;


            // Error Message

            if (!MessageQueue.Exists(".\\Private$\\externalvasqueue"))
            {
                mq = MessageQueue.Create(".\\Private$\\externalvasqueue");
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                mq.MaximumQueueSize = MessageQueue.InfiniteQueueSize;
            }
            else
            {
                mq = new MessageQueue(".\\Private$\\externalvasqueue");
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(TransactionMessage) });
                mq.DefaultPropertiesToSend = defaultPropertiesToSend;
                mq.MaximumQueueSize = MessageQueue.InfiniteQueueSize;
            }


            if (!MessageQueue.Exists(".\\Private$\\" + emailqueue))
            {
                mq = MessageQueue.Create(".\\Private$\\" + emailqueue);
                mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                mq.MaximumQueueSize = MessageQueue.InfiniteQueueSize;
            }
            else
            {
                //mq = new MessageQueue(".\\Private$\\DeltasUniemailalert");

                mq = new MessageQueue(".\\Private$\\" + emailqueue);                
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(CEmailAlertMessage) });
                mq.DefaultPropertiesToSend = defaultPropertiesToSend;
                mq.MaximumQueueSize = MessageQueue.InfiniteQueueSize;
            }



        }
        catch (Exception exception)
        {
            //logger.Fatal(exception.Message + "|" + exception.StackTrace + "\n");
        }
    }

    public static void EmailHandler(object data)
    {
        ApplicantSignOn Ao = (ApplicantSignOn)data;

        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Dear " + Ao.Surname.ToUpper() + " " + Ao.OtherNames + ",<br/>");
            sb.AppendLine("Welcome to the Petroleum Training Institute Applicant Control Center. <br /><br />");
            sb.AppendLine("We are pleased to inform you that your sign up was successful and Your Application Number is <b>" + Ao.FormNumber + "</b>.<br />");
            sb.AppendLine("To Login and continue with your Application,<br />");
            sb.AppendLine("Please follow this link : http://www.pti.edu.ng/ApplicantLogin.aspx<br />");

            sb.AppendLine("Find below your login details: <br />");
            sb.AppendLine();
            sb.AppendLine("<span>Username: " + Ao.UserName + "</span><br />");
            sb.AppendLine("<span>Password: " + Ao.Password + "</span><br />");
            sb.AppendLine("<br />To maintain your accessibility to this portal, you are adviced to keep your USERNAME and PASSWORD as secret as possible.<br/>");
            sb.AppendLine("You application sign up is valid for the <b>" + Ao.ApplicationSession + "</b> application session only<br />");
            sb.AppendLine("Please remember to direct all admissions related enquires to the Admission Officer or Academic Student's Support.<br/><br/><br/>");
            //sb.AppendLine("Through Email:<br />admissions@pti.edu.ng<br />studentsupport@pti.edu.ng<br />");
            sb.AppendLine("Through Email:<br />admission@pti.edu.ng :+2348032640578 for admision issues<br />itsupport@pti.edu.ng : +2348036373149 for Portal issues<br />");
            sb.AppendLine("  <br /> - CALL HOURS :>>> 8am-2pm, Mon-Fri.NO FLASING !</br> (With this no you can communicate with the admission agent)<br />");
            sb.AppendLine("You are advised to always visit the www.pti.edu.ng portal regularly for more admissions update<br />");




            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Best Regards,<br/>");
            sb.AppendLine("PTI Admissions Office<br/>");
            sb.AppendLine("Please do not reply this mail...It's not necessary<br/>");

            sb.AppendLine("======================================================================================<br/>");
            sb.AppendLine("PTI warri.<br/>");
            sb.AppendLine("Website: www.pti.edu.ng  Email: admission@pti.edu.ng<br />");


            CEmailAlertMessage cm = new CEmailAlertMessage();
            cm.SubjectProperty = "RE: Admission Portal Signup";
            cm.ToProperty.Add(Ao.Email);
            //cm.BccProperty.Add("cybsoft@cyberspace.net.ng");
            //cm.BccProperty.Add("itsupport@pti.edu.ng");
            cm.BodyProperty = sb.ToString();
            cm.ComposedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            cm.DisplayName = "PTI Admission Office";
            cm.ReplyToProperty = "itsupport@pti.edu.ng";
            cm.SenderProperty = "itsupport@pti.edu.ng";

            string conn = ".\\Private$\\" + System.Configuration.ConfigurationManager.AppSettings["emailalertqueue"];
            MessageQueue mq = new MessageQueue(conn);
            mq.Send(cm);
            //System.Net.Mail.SmtpClient email = new System.Net.Mail.SmtpClient();
            //email.Host = serv.SmtpServerName;



        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //throw ex;
            //return false;
        }
    }

    protected void ddlEntryMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        trProgramOption.Visible = false;
        if (ddlEntryMode.SelectedIndex > 0)
        {
            DataSet ds = ApplicantsBusiness.getApplicationProgramm(ddlEntryMode.SelectedItem.Text);
            ddlProg.Items.Clear();
            ddlProg.DataSource = ds;
            ddlProg.DataTextField = "Programme";
            ddlProg.DataValueField = "Programme";
            ddlProg.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                PrepareProgramOption();
                ddlProg.Items.Insert(0, new ListItem("Choose as appropriate", "NO"));
                BtnSignOn.Enabled = true;
                ddlProg.Focus();
            }
            else
            {
                ddlProg.Items.Insert(0, new ListItem("No Program for this Entry Mode", "NO"));
                BtnSignOn.Enabled = false;
                trProgramOption.Visible = false;
            }


             
                LblError.Text = "ARE YOU SURE YOU ALREADY HAVE A CERTIFICATE  FOR       "+ ddlEntryMode.SelectedItem.Text.ToUpper()+" ??? AND IT IS CURRENTLY YOUR HIGHEST QUALIFICATION? "+"  If not please re-select the actual highest qualification you have obtained from your previous school/education.If you are confused,send a mail to itsupport@pti.edu.ng.IF YOU ARE SURE ,CONTINUE:on the right hand side box, SELECT THE PROGRAMME YOU ARE QUALIFIED TO STUDY IN   P T I .";
                new Utility().MessageBox(LblError.Text, this.Page);
                txtPassWord.Focus();
             
        }
        else
        {
            ddlProg.Items.Clear();
            ddlProg.Items.Insert(0, new ListItem("Select Entry Mode First", "NO"));
            BtnSignOn.Enabled = false;
            trProgramOption.Visible = false;
        }
    }

    private void PrepareProgramOption()
    {
        trProgramOption.Visible = true;
        rdbProgramOption.Visible = false;
        switch (ddlEntryMode.SelectedItem.Text.ToLower())
        {
            case "hnd":
                                                ////populate option for fasttrack and normal
                                                //lblSummary.Text = "Holders of University Diplomas, OND, HND, Bachelors and Higher Degrees." +
                                                //    " Candiates with Bachelor and Higher Degrees" +
                                                //    " can be considered for a fast track programme (3 years).";

                lblSummary.Text = "";
               
                 // ELIGIBILITY FOR DIRECT ENTRY [DE] APPLICANTS:"+"</BR>"+
                 //"1. University of Ibadan DLC Diploma Certificate "+"</br>"+
                 //"2. Diploma in local Government studies (O.A.U) with not less than 3.00 GP and or other diplomas from recognized University "+
                 //"and polytechnics could be considered for direct entry, subject to good performance in the entrance examination ";

                break;
            case "o level":

                lblSummary.Text = "";
                
               // ELIGIBILITY FOR ORDINARY LEVEL APPLICANTS:" + "</BR>" +
               //"1. Senior Secondary School Certificate (SSCEWASSCE) passes in core English, core Mathematics." +
               //"   Core science and either core integrated science or social studies and three elective subjects," +
               //"   with an aggregate score of 24 or better. " +"</br>"+
               //"2. for A&B minimum of 5 credit passes at not more than two sitting in GCE, WAEC, SSCE, NECO, NABTEB etc." +
               //"   possession of professional certificate is an added advantage. Candidates with good scores in UTME and or IJMB/NABTEB/WAHEB etc." +
               //"   could be considered ";
                rdbProgramOption.Visible = true;
                break;

                //lblSummary.Text = "Candiate with at least 5 O/L Credits at one sitting or 6 O/L Credits at 2 sittings including English Language";
                //break;
            //case "mature candidate":
            //    lblSummary.Text = "Candidate who do not posses the minimum entry qualifications may apply on the" +
            //        " grounds of maturity/work exeperience. Such candidates must be at least 26 years old and must provide the following information: " +
            //        "Academic qualification, Professional exeperience, Self appraisal of proffessional Competence, recommendation from competent individuals." +
            //        " An oral interview/literacy test will be part of their selection process (process details online).";
            //    break;
            default:
                break;
        }
    }
}

