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
using AjaxControlToolkit;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class MasterPage : System.Web.UI.MasterPage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0 || Request.UserAgent.IndexOf("Unknown") > 0 || Request.UserAgent.IndexOf("Chrome") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

        }
        if (Page.User.Identity.IsAuthenticated == true)
        {
            if (Cache[HttpContext.Current.User.Identity.Name] != null)
            {
                string userGroup = (string)Cache[HttpContext.Current.User.Identity.Name];
                if (userGroup == string.Empty)
                {
                    Response.Redirect("loginaccess.aspx");
                }
                else
                {
                    ////Response.Redirect("~/Admin/Home.aspx");
                    switch (userGroup.ToLower())
                    {
                        case "web master":
                        case "department admin":
                        case "faculty admin":   
                            Response.Redirect("~/Admin/Home.aspx");
                            break;  
                        case "bursary admin":
                        case "bursary operation":
                        case "audit":
                            Response.Redirect("~/Bursary/BursaryHome.aspx");
                            break;
                        case "records":
                            Response.Redirect("~/Records/RecordsHome.aspx");
                            break;
                        case "admission":
                            Response.Redirect("~/Admission/AdmissionHome.aspx");
                            break;
                        case "help desk":
                            Response.Redirect("~/Helpdesk/HelpdeskHome.aspx");
                            break;
                        case "medical":
                            Response.Redirect("~/Medical/MedicalHome.aspx");
                            break;
                        case "library":
                            Response.Redirect("~/Library/LibraryHome.aspx");
                            break;
                        case "editorial":
                            Response.Redirect("~/Editorial/editorialHome.aspx");
                            break;  

                        default:
                            Response.Redirect("loginaccess.aspx");
                            break;
                    }
                }


            }

        }

    }


    public void ClientMessage(Page p, string message)
    {
        message = message.Trim();
        if (message.Length > 100)
        {
            message = message.Substring(0, 100);
        }

        if (p.Master == null)
        {

            p.ClientScript.RegisterClientScriptBlock(p.GetType(), System.Guid.NewGuid().ToString(),

            string.Format("<script>alert('{0}');</script>", message.Trim()));
        }

        //If the page has a master page, we will find the panel and the label control that we have provided for messaging and will set the label’s Text property :

        else
        {


            Label _lblClientMessage_ = p.Master.FindControl("_lblClientMessage_") as Label;

            _lblClientMessage_.Text = message.Trim();


            //We also have to find the update panel that I mentioned above and call it’s update method:

            UpdatePanel upd = p.Master.FindControl("_updClientMessage_") as UpdatePanel;

            upd.Update();

            //This will update the label’s text. Finally, we should fine the ModalPopupExtender and call it’s Show() method:

            ModalPopupExtender extender = p.Master.FindControl("mdlPopup") as ModalPopupExtender;

            extender.Show();

        }
    }
    private void MessageBox(string strMsg)
    {
        // to supply the alert box text
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + strMsg + "'" + ")</script>";

        //add the label to the page to display the alert
        Page.Controls.Add(lbl);
    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        switch (e.Item.Text.ToLower())
        {
            case "logout":

                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("loginaccess.aspx");
                break;
            default:
                break;
        }
    }
}
