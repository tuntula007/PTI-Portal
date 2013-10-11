using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageform : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
       protected void LnkRegDetails_Click1(object sender, EventArgs e)
    {
        Session["RegDetIndicator"] = "RegStud";
        Response.Redirect("~/RptRegistration.aspx");
    }
       protected void LnkStudList_Click(object sender, EventArgs e)
       {
           Session["RegDetIndicator"] = "ListStud";
           Response.Redirect("~/RptRegistration.aspx");
       }
       protected void LnkBursaryDetails_Click(object sender, EventArgs e)
       {
           //Session["RegDetIndicator"] = "RegStud";
           Response.Redirect("~/RptPinUsageParam.aspx");
       }
       protected void LnkRegSummary_Click(object sender, EventArgs e)
       {
           Session["RegDetIndicator"] = "RegStud";
           Response.Redirect("~/RptStatistical.aspx");
       }
       protected void LnkStudSummary_Click(object sender, EventArgs e)
       {
           Session["RegDetIndicator"] = "ListStud";
           Response.Redirect("~/RptStatistical.aspx");
       }
       protected void LnkBursarySummary_Click(object sender, EventArgs e)
       {
           Session["RegDetIndicator"] = "Bursary";
           Response.Redirect("~/RptStatistical.aspx");
       }
}
