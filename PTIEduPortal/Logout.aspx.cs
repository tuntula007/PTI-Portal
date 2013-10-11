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
using log4net;
using log4net.Config;  


public partial class Logout : System.Web.UI.Page
{
    ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
protected void Page_Load(object sender, EventArgs e)
    {
        logger.Info("Loging out and redirecting to  StudentLogin.aspx");
        Session.Contents.Clear();
        Response.Redirect("StudentLogin.aspx");
    }
}
