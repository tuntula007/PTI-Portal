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

    private static string ID = "";// cp = null;
    private static string Group = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Cache[HttpContext.Current.User.Identity.Name] != null)
        {
            ID = HttpContext.Current.User.Identity.Name;
            Group = (string)Cache[HttpContext.Current.User.Identity.Name];
            if (Group.ToLower() == "admission")
            {

            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                // clear authentication cookie
                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                cookie1.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie1);
                // clear session cookie (not necessary for your current problem but i would recommend you do it anyway) 
                HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
                cookie2.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie2);
                //FormsAuthentication.RedirectToLoginPage();
                Response.Redirect("~/loginaccess.aspx");
                return;
            }
        }
        if (Page.IsPostBack == false)
        {

            if (Request.UserAgent.IndexOf("AppleWebKit") > 0 || Request.UserAgent.IndexOf("Unknown") > 0 || Request.UserAgent.IndexOf("Chrome") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

        }
        if (Page.User.Identity.IsAuthenticated == true)
        {
            //Menu1.Visible = true;
            if (Menu1.Visible == true)
            {
                Menu1.Items.Clear();
                MenuItem child = new MenuItem();
                child.Text = "<i>" + Page.User.Identity.Name + "<//i>";
                child.Value = "1";
                child.Selectable = false;

                Menu1.Items.Add(child);

                child = new MenuItem();
                child.Text = "Logout";
                child.Value = "2";
                Menu1.Items.Add(child);
                Menu1.Visible = true;
                //CDynamicMenu dm = new CDynamicMenu(mnuMain);


            }

        }
        else
        {
            Menu1.Visible = false;
        }

        

//        string script = @" theForm = document.forms[0];
//        if (!theForm){    
//        theForm = document.form1;
//        }   
//        theForm.__SCROLLPOSITIONX.value = getScrollX();
//        theForm.__SCROLLPOSITIONY.value = getScrollY();";
//        //Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "sumbit", script);
//        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "", script);


       // Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "restoreScroll();", true);

        //if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "CreateResetScrollPosition"))
        //{
        //    //Create the ResetScrollPosition() function  
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CreateResetScrollPosition", "function ResetScrollPosition() {" +
        //        Environment.NewLine + " var scrollX = document.getElementById('__SCROLLPOSITIONX');" +
        //        Environment.NewLine + " var scrollY = document.getElementById('__SCROLLPOSITIONY');" +
        //        Environment.NewLine + " if (scrollX && scrollY) {" +
        //        Environment.NewLine + " scrollX.value = 0;" +
        //        Environment.NewLine + " scrollY.value = 0;" +
        //        Environment.NewLine + " }" +
        //        Environment.NewLine + "}", true);

        //    //Add the call to the ResetScrollPosition() function  
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallResetScrollPosition", "ResetScrollPosition();", true);
        //} 



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

                //FormsAuthentication.SignOut();
                //Response.Redirect("Default.aspx");
                FormsAuthentication.SignOut();
                Session.Abandon();
                // clear authentication cookie
                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                cookie1.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie1);
                // clear session cookie (not necessary for your current problem but i would recommend you do it anyway) 
                HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
                cookie2.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie2);
                //FormsAuthentication.RedirectToLoginPage();
                Response.Redirect("~/loginaccess.aspx");
                break;
            default:
                break;


        }
    }
}
